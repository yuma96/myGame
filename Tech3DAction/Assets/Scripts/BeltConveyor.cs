using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour
{
    [SerializeField] private float m_uvSpeed = 1.0f; //ストライプスクロール速度
    [SerializeField] private float m_moveSpeed = 1.0f; //ベルトコンベアのスピード

    void Update()
    {
        ScrollUV();
    }

    /// <summary>
    /// テクスチャをスクロールさせて、ベルトコンベアの見た目を表現する
    /// </summary>
    void ScrollUV()
    {
        var material = GetComponent<Renderer>().material;
        Vector2 offset = material.mainTextureOffset;
        offset += Vector2.right * m_uvSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }

    void OnCollisionStay(Collision other)
    {
        var body = other.gameObject.GetComponent<Rigidbody>();
        if (body != null)
        {
            Vector3 add = transform.right * m_moveSpeed * Time.deltaTime;
            body.MovePosition(other.transform.position + add);
        }
    }
}
