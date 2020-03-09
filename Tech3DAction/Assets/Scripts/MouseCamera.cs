using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 targetPos;

    void Start()
    {
        player = GameObject.Find("SapphiArtchan");
        targetPos = player.transform.position;
    }

    void Update()
    {
        // Playerの移動量分(カメラ)も移動する
        transform.position += player.transform.position - targetPos;
        targetPos = player.transform.position;

        // マウスの移動量
        float mouseInputX = Input.GetAxis("Mouse X");

        // targetの位置のY軸を中心に回転する
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
    }
}
