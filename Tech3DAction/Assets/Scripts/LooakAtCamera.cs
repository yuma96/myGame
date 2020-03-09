using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooakAtCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //　カメラと同じ向きにHP名前バー設定
        transform.rotation = Camera.main.transform.rotation;
    }
}
