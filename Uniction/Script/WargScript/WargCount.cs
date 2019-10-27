using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WargCount : MonoBehaviour {

    public Text WargCountText; //Wargの倒した数を表示するText型の変数

    public int WargFinishCount = 10; //目的討伐数
    public int WargLimit = 0; //現在Warg倒した数

    public bool gameClear = false; //ゲームクリアフラグ

    public GameObject GameClearUI; //GameClearを表示するUI画面

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        WargCountText.text = "Warg " + WargLimit.ToString("f0") + "体"; //討伐数 (f1増えるごとに少数が増える。)

        //何体以上倒したらゲームクリア宣言
        /*if (WargLimit >= WargFinishCount) { //討伐数10体以上になったら
            //WargCountText.text = ("0") + "/ 10"; //10体以上になったらTextストップする。
            GameClear();
        }*/
	}
    
    //敵を倒した数カウント
    /*public void EnemyCount()
    {
        WargLimit += 1;
    }*/
    
    //Warg倒した数Point
    public void AddPoint(int point)
    {
        WargLimit = WargLimit + point;
    }

    //一定数warg倒したらゲームクリア
    /*public void GameClear()
    {
        Debug.Log("GameClear");
        gameClear = true;
        GameClearUI.SetActive(true);
    }*/
}
