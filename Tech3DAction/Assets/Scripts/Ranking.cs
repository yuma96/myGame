using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public float gameTime;        //ゲームシーンからのタイム少数保存
    public static int timePoint;  //ゲームシーンからのタイム[整数]保存
    public InputField inputField; //名前入力取得

    [SerializeField] private Button nameSaveButton = default; //ボタン取得
    [SerializeField] private GameObject rankingOption = default; //ランキング登録画面

    void Start()
    {
        //On End Editを扱えるように取得
        inputField = inputField.GetComponent<InputField>();

        //ランキング登録画面
        rankingOption.SetActive(true);

        gameTime = TimeManager.TimeGet(); //ゲームシーンからタイムを持ってくる。
        timePoint = (int)gameTime; //整数にする
    }

    //ランキングに保存ボタン
    public void NameSaveButton()
    {
        
        inputField.enabled = true;      //on end editを制御してボタンで登録する
        rankingOption.SetActive(false); //ランキング画面閉じる
    }

    //ランキングに登録しないボタン
    public void NotNameSaveButton()
    {
        inputField.text = default;      //名前登録でテキスト空白にする
        rankingOption.SetActive(false); //ランキング画面閉じる
    }

    //タイム渡す
    public static int getPoint()
    {
        return timePoint;
    }
}