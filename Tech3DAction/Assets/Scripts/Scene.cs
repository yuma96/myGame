using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] private GameObject operation = default; //操作説明の画面

    //スタートボタン
    public void StartButton()
    {
        SceneManager.LoadScene("Game"); //ゲームシーンに移行
    }

    //操作説明ボタン
    public void OperationButton()
    {
        operation.SetActive(true); //操作説明の画面開く
    }

    //操作説明から戻るボタン
    public void OptionExitButton()
    {
        operation.SetActive(false); //説明画面を閉じる
    }

    //　ゲーム終了ボタンを押したら実行する
    public void EndGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
		Application.Quit();
    #endif
    }

    //タイトルに戻るボタン
    public void TitleExitButton()
    {
        SceneManager.LoadScene("Title"); //タイトルシーンに移行
    }
}
