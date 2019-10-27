using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargSearchArea : MonoBehaviour {

    WargAI wargAI;  //WargAIをwargAIに入れる

	// Use this for initialization
	void Start () {
        wargAI = transform.root.GetComponent<WargAI>(); //親にあるEnemyAIを読み込み
    }

    //Wargのサーチコライダーに入ってきたら
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") //tagがプレイヤーだった場合
        {
            wargAI.SetAttackTarget(other.transform); //wargAIにプレイヤーをターゲットにする情報送る
        }       
    }

     // Update is called once per frame
    void Update () {

    }
}
