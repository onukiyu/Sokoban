using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //�z��̐錾
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 2, 0, 0 };
        //�ǉ��B������̐錾�Ə�����
        string debugText = "";
        for(int i=0;i<map.Length; i++)
        {
            //�ύX�B������Ɍ������Ă���
            debugText += map[i].ToString() + ",";
        }
        //����������������o��
        Debug.Log(debugText);
    }

    // Update is called once per frame
  
    int GetplayerIndex()//���\�b�h��
    {
        for(int i = 0; i<map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveForm, int moveTo)
    {
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo < 0 || moveTo >= map.Length) { return false; } //�����Ȃ��������ɏ����A���^�[������B�������^�[��
        //�ړ����2(��)��������
        if (map[moveTo] == 2)
        {
            //�ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveForm;
            //�v���C���[�̈ړ��悩��A����ɐ��2(��)���ړ�������B
            //���̈ړ������BMoveNumber���\�b�h����MoveNumber���\�b�h��
            //�ĂсA�������ċA���Ă���B�ړ��s��bool�ŋL�^
            bool succes = MoveNumber(2, moveTo, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if (!succes) { return false; }
        }
        //�v���C���[�E���ւ�炸�̈ړ�����
        map[moveTo] = number;
        map[moveForm] = 0;
        return true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //�����Ɉړ�����������
           

            //������Ȃ��������̂��߂�-1�ŏ�����

            //���\�b�h�������������g�p
            int playerIndex = GetplayerIndex();

            //�ړ��������֐���
            MoveNumber(1, playerIndex, playerIndex + 1);

           

            //����
            /*
              playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂ŁA
            �@playerIndex-1��肳��ɏ������C���f�b�N�X�̎��̂݌����������s��
             */

         


            string debugText = "";
            for (int i = 0; i<map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }
            Debug.Log(debugText);
        }

        //���Ɉړ�
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetplayerIndex();

            //�ړ��������֐���
            MoveNumber(1, playerIndex, playerIndex - 1);


            string debugText = "";
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }
            Debug.Log(debugText);
        }
    }
}