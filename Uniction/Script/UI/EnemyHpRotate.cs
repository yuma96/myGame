using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpRotate : MonoBehaviour {

    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {
        // Playerカメラと同じ向きにWargのHP名前バー設定
        transform.rotation = Camera.main.transform.rotation;
    }
}
