using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour {

	public GameObject player;  //プレイヤーオブジェクトへの参照を格納する
    private Vector3 targetPos; //ベクトル

    // Use this for initialization
    void Start () {
        player = GameObject.Find("unitychan");
        targetPos = player.transform.position;
    }

    // Update is called once per frame
    void Update () {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;

        //I押してる間
        if (Input.GetKey(KeyCode.I))
        {
            // targetの位置のY軸を中心に、左回転する
            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * -200f);
        }

        //O押してる間
        if (Input.GetKey(KeyCode.O))
        {
            // targetの位置のY軸を中心に、右回転する
            transform.RotateAround(targetPos, Vector3.up, Time.deltaTime * 200f);
        }
    }
}
