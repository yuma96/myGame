using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRuleCtrl : MonoBehaviour {

    public Text timeText; //時間を表示するText型の変数
    public float countup = 0.0f; //カウントアップ
    public float timeLimit = 5.0f * 60.0f; //制限時間5分
    public bool gameOver = false; //ゲームオーバーフラグ
    public GameObject GameOverUI; //GameOverを表示するUI画面

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update () {
        timeLimit -= Time.deltaTime; //時間減らしていく
        timeText.text = timeLimit.ToString("f1") + "秒"; //時間を表示する

        //時間が０以下になったら
        if (timeLimit <= countup) {
            timeText.text = ("0") + "秒"; //０秒になる
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        gameOver = true;
        // ゲームオーバー表示する。
        GameOverUI.SetActive(true);
    }
}
