using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�z��̐錾
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefub;
    int[,] map;
    public GameObject clearText;


    GameObject[,] field;
    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[,]{
             { 0,0,0,0,0 },
             { 0,3,1,3,0 },
             { 0,0,2,0,0 },
             { 0,2,3,2,0 },
             { 0,0,0,0,0 },
         };

        field = new GameObject
        [
         map.GetLength(0),
         map.GetLength(1)
        ];


        //�ǉ��B������̐錾�Ə�����
        string debugText = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y,x] = Instantiate(
                         playerPrefab,
                         new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                         boxPrefab,
                         new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
                if (map[y, x] == 3)
                {
                    Instantiate(
                         goalPrefub,
                         new Vector3(x, map.GetLength(0) - y, 0), Quaternion.identity);
                }
            }
        }

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n"; //���s
        }
        Debug.Log(debugText);
        //for(int i=0;i<map.Length; i++)
        //{
        //    //�ύX�B������Ɍ������Ă���
        //    debugText += map[i].ToString() + ",";
        //}
        ////����������������o��
        //Debug.Log(debugText);
    }

    // Update is called once per frame

    Vector2Int GetplayerIndex()//���\�b�h��
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                //
                //
                //
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }

            }

        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(Vector2Int moveForm, Vector2Int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        //�����Ȃ��������ɏ����A���^�[������B�������^�[��
        //�ړ����2(��)��������
        if (field[moveTo.y,moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveForm;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        //{
        //�ǂ̕����ֈړ����邩���Z�o
        //    int velocity = moveTo - moveForm;
        //�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������B
        //���̈ړ������BMoveNumber���\�b�h����MoveNumber���\�b�h��
        //�ĂсA�������ċA���Ă���B�ړ��s��bool�ŋL�^
        //    bool succes = MoveNumber(2, moveTo, moveTo + velocity);
        //���������ړ����s������A�v���C���[�̈ړ������s
        //    if (!succes) { return false; }
        //}
        //�v���C���[�E���ւ�炸�̈ړ�����
        field[moveTo.y, moveTo.x] = field[moveForm.y, moveForm.x];
        
        //field[moveForm.y, moveForm.x].transform.position = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveForm.y, moveForm.x].GetComponent<MoveScript>().MoveTo(moveToPosition);
        field[moveForm.y, moveForm.x] = null;
        return true;
    }

    bool IsCleard()
    {
        //Vector2Int�^�̉ϒ��z��̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���ۂ��𔻒f
                if (map[y, x] == 3)
                {
                    //�i�[�ꏊ�̃C���f�b�N�X���T���Ă���
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        //�v�f����goals.Count�Ŏ擾
        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //��ł���������������������B��
                return false;
            }
        }
        //�������B���łȂ���Ώ����B��
        return true;
    }

    void Update()
    {
        //�����Ɉړ�����������
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
        }
        //�����N���A���Ă�����
        if (IsCleard())
        {
            //�Q�[���I�u�W�F�N�g��SetActive���\�b�h���g���L����
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
        }
        //�����N���A���Ă�����
        if (IsCleard())
        {
            //�Q�[���I�u�W�F�N�g��SetActive���\�b�h���g���L����
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
        }
        //�����N���A���Ă�����
        if (IsCleard())
        {
            //�Q�[���I�u�W�F�N�g��SetActive���\�b�h���g���L����
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
        }
        //�����N���A���Ă�����
        if(IsCleard())
        {
            //�Q�[���I�u�W�F�N�g��SetActive���\�b�h���g���L����
            clearText.SetActive(true);
        }
    }

}

            //������Ȃ��������̂��߂�-1�ŏ�����

            //���\�b�h�������������g�p
            //int playerIndex = GetplayerIndex();

            //�ړ��������֐���
           // MoveNumber(1, playerIndex, playerIndex + 1);

           

    //        //����
    //        /*
    //          playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂ŁA
    //        �@playerIndex-1��肳��ɏ������C���f�b�N�X�̎��̂݌����������s��
    //         */

         


    //        string debugText = "";
    //        for (int i = 0; i<map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }

    //    //���Ɉړ�
    //    if(Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        int playerIndex = GetplayerIndex();

    //        //�ړ��������֐���
    //        MoveNumber(1, playerIndex, playerIndex - 1);


    //        string debugText = "";
    //        for (int i = 0; i < map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }
    //}

