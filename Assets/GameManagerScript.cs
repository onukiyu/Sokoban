using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //配列の宣言
    public GameObject playerPrefab;
    int[,] map;

    GameObject[,] field;
    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
         map = new int[,]{
             { 1,0,0,0,1 },
             { 0,0,1,0,0 },
             { 1,0,0,0,1 },
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
                   // GameObject instanc
                   field[y,x]= Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - 1 - y),
                        Quaternion.identity);
                };
            }
        }

        for (int y=0; y < map.GetLength(0); y++)
        {
            for(int x=0; x < map.GetLength(1); x++)
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

    //int GetplayerIndex()//メソッド化
    //{
    //    for(int y = 0; y<field.GetLength(0); y++)
    //    {
    //        for (int x = 0; x < field.GetLength(1); x++)
    //        {  
    //            if (field[y, x] == 1)
    //            {
    //                return field[y, x];
    //            }

    //        }
             
    //    }
    //    return new Vector2Int(y, x);
    //}

    //bool MoveNumber(int number, int moveForm, int moveTo)
    //{
    //    //移動先が範囲外なら移動不可
    //    if(moveTo < 0 || moveTo >= map.Length) { return false; } //動けない条件を先に書き、リターンする。早期リターン
    //    //移動先に2(箱)がいたら
    //    if (map[moveTo] == 2)
    //    {
    //        //どの方向へ移動するかを算出
    //        int velocity = moveTo - moveForm;
    //        //プレイヤーの移動先から、さらに先へ2(箱)を移動させる。
    //        //箱の移動処理。MoveNumberメソッド内でMoveNumberメソッドを
    //        //呼び、処理が再帰している。移動可不可をboolで記録
    //        bool succes = MoveNumber(2, moveTo, moveTo + velocity);
    //        //もし箱が移動失敗したら、プレイヤーの移動も失敗
    //        if (!succes) { return false; }
    //    }
    //    //プレイヤー・箱関わらずの移動処理
    //    map[moveTo] = number;
    //    map[moveForm] = 0;
    //    return true;
    //}

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        //ここに移動処理を書く
           

    //        //見つからなかった時のために-1で初期化

    //        //メソッド化した処理を使用
    //        int playerIndex = GetplayerIndex();

    //        //移動処理を関数化
    //        MoveNumber(1, playerIndex, playerIndex + 1);

           

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
}
