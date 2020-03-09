using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public GameObject timer_object = null; // Textオブジェクト
    public static float timer = 100; // スコア変数

    public float countEnd = 0f; //カウントアップ

    void Start()
    {
        timer = 100; //ゲーム始まる際にタイムリセット
    }

    // 削除時の処理
    void OnDestroy()
    {
        // スコアを保存
        PlayerPrefs.SetFloat("TIME", timer);
        PlayerPrefs.Save(); //スコアセーブ
    }

    //Resultにかかった時間を送る
    public static float TimeGet()
    {
        timer = PlayerPrefs.GetFloat("TIME", 0); //タイムをロード
        return timer;
    }

    //保存データすべて削除
    public void DeleteDate()
    {
        PlayerPrefs.DeleteAll(); //ゲームシーンが始まるときstartに呼ぶ
    }

    // 更新
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text timer_text = timer_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        timer_text.text = "Score:" + timer;

        timer_text.text = timer.ToString("f0") + "秒"; //少数時間を表示する
        timer -= Time.deltaTime; //時間増やしていく
        //時間が０以下になったら
        if (timer <= countEnd)
        {
            timer_text.text = ("0") + "秒";
            SceneManager.LoadScene("Title"); //ゲームシーンに移行
        }
    }
}
