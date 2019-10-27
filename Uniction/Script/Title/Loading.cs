using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    public GameObject loadUI; // シーンロード中に表示するUI画面
    public Slider slider; // 読み込み率を表示するスライダー
    public Text ProgressText; // ロード?%

    //インターフェースyuma呼ぶ
    public void NextScene(int yuma)
    {
        // コルーチンを開始
        StartCoroutine(LoadData(yuma));
    }

    IEnumerator LoadData(int yuma)
    {
        // ゲームシーンの読み込みをする
        AsyncOperation operation = SceneManager.LoadSceneAsync("yuma");

        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        //読み込みが終わるまでスライダー値を反映
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); //ゲームシーンに移るまで％計算

            slider.value = progress; //ロードゲージ代入
            ProgressText.text = progress * 100f + "%"; //ロードゲージと何%まで溜まったか表示

            yield return null; //1フレームコルーチンの処理を中断→再開する
        }
    }
}

        
    
