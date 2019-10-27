using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

    // ボタンをクリックするとTitleSceneに移動します
    public void TitleChangeButton () {
        SceneManager.LoadScene("Title"); //タイトルに戻る
	}
}
