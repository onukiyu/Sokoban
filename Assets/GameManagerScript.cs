using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //配列の宣言
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefub;
    int[,] map;
    public GameObject clearText;


    GameObject[,] field;
    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
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


        //追加。文字列の宣言と初期化
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
            debugText += "\n"; //改行
        }
        Debug.Log(debugText);
        //for(int i=0;i<map.Length; i++)
        //{
        //    //変更。文字列に結合していく
        //    debugText += map[i].ToString() + ",";
        //}
        ////結合した文字列を出力
        //Debug.Log(debugText);
    }

    // Update is called once per frame

    Vector2Int GetplayerIndex()//メソッド化
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
        //移動先が範囲外なら移動不可
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        //動けない条件を先に書き、リターンする。早期リターン
        //移動先に2(箱)がいたら
        if (field[moveTo.y,moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveForm;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }
        //{
        //どの方向へ移動するかを算出
        //    int velocity = moveTo - moveForm;
        //プレイヤーの移動先から、さらに先へ2(箱)を移動させる。
        //箱の移動処理。MoveNumberメソッド内でMoveNumberメソッドを
        //呼び、処理が再帰している。移動可不可をboolで記録
        //    bool succes = MoveNumber(2, moveTo, moveTo + velocity);
        //もし箱が移動失敗したら、プレイヤーの移動も失敗
        //    if (!succes) { return false; }
        //}
        //プレイヤー・箱関わらずの移動処理
        field[moveTo.y, moveTo.x] = field[moveForm.y, moveForm.x];
        
        //field[moveForm.y, moveForm.x].transform.position = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveForm.y, moveForm.x].GetComponent<MoveScript>().MoveTo(moveToPosition);
        field[moveForm.y, moveForm.x] = null;
        return true;
    }

    bool IsCleard()
    {
        //Vector2Int型の可変長配列の作成
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //格納場所か否かを判断
                if (map[y, x] == 3)
                {
                    //格納場所のインデックスを控えておく
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }
        //要素数はgoals.Countで取得
        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //一つでも箱が無かったら条件未達成
                return false;
            }
        }
        //条件未達成でなければ条件達成
        return true;
    }

    void Update()
    {
        //ここに移動処理を書く
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
        }
        //もしクリアしていたら
        if (IsCleard())
        {
            //ゲームオブジェクトのSetActiveメソッドを使い有効化
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
        }
        //もしクリアしていたら
        if (IsCleard())
        {
            //ゲームオブジェクトのSetActiveメソッドを使い有効化
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
        }
        //もしクリアしていたら
        if (IsCleard())
        {
            //ゲームオブジェクトのSetActiveメソッドを使い有効化
            clearText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetplayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
        }
        //もしクリアしていたら
        if(IsCleard())
        {
            //ゲームオブジェクトのSetActiveメソッドを使い有効化
            clearText.SetActive(true);
        }
    }

}

            //見つからなかった時のために-1で初期化

            //メソッド化した処理を使用
            //int playerIndex = GetplayerIndex();

            //移動処理を関数化
           // MoveNumber(1, playerIndex, playerIndex + 1);

           

    //        //続き
    //        /*
    //          playerIndex+1のインデックスの物と交換するので、
    //        　playerIndex-1よりさらに小さいインデックスの時のみ交換処理を行う
    //         */

         


    //        string debugText = "";
    //        for (int i = 0; i<map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }

    //    //左に移動
    //    if(Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        int playerIndex = GetplayerIndex();

    //        //移動処理を関数化
    //        MoveNumber(1, playerIndex, playerIndex - 1);


    //        string debugText = "";
    //        for (int i = 0; i < map.Length; i++)
    //        {
    //            debugText += map[i].ToString() + ",";
    //        }
    //        Debug.Log(debugText);
    //    }
    //}

