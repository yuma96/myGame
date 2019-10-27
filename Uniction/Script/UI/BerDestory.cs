using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BerDestory : MonoBehaviour {

    public GameObject HpName;    //HPと名前
    public Slider WargHitPoint2; //WargのHP

    // Use this for initialization
    void Start () {
        HpName.gameObject.SetActive(false);//WargのHPと名前false
    }

    //コライダーに当たってる間
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") //プレイヤーの数m以内にいたら
        {
            HpName.gameObject.SetActive(true); //HPと名前表示
        }
    }

    //コライダーに外れた時
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HpName.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (WargHitPoint2.value <= 0) //Warg体力０になったら
        {
            HpName.gameObject.SetActive(false); //HP,名前を消す
        }
    }
}
