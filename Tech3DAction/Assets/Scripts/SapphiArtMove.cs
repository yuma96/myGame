using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SapphiArtMove : MonoBehaviour
{
    public float runSpeed = 10;      //走るスピード
    public float jumpPower;          //ジャンプ力
    public int jumpCount;            //キーが押された数
    public bool isJumping;           //空中ジャンプ制御         
    public GameObject cameraObject;　//カメラのオブジェクト

    private Vector3 moveForward;     //カメラ向きで移動方向を取得
    private Rigidbody rb;
    private Collider playerCollider;
    private Animator anim;
    private float moveX;             //Horaizontalの値
    private float moveZ;             //Verticalの値
    private Vector3 velocity = Vector3.zero; //ベクトル(0, 0, 0)にする
    private Vector3 moveVector;      //入力された移動速度を取得


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerCollider = GameObject.Find("SapphiArtchan").GetComponent<CapsuleCollider>();
        jumpCount = 1; //カウントする数
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //カメラの方向から、X-Z平面のベクトルを取得
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1));//.normalized;

        //wasdキーの入力値とカメラの向きから、移動方向を決定
        moveForward = cameraForward * moveZ * runSpeed + cameraObject.transform.right * moveX * runSpeed;

        //入力された移動速度
        rb.velocity = moveForward + new Vector3(0, rb.velocity.y, 0);

        //キャラクターの向きを進行方向に向ける
        if (moveForward != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveForward);

        anim.SetBool("param_idletorunning", false);

        //WASD押してる時
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            anim.SetBool("param_idletorunning", true);
        }

        //ジャンプ連打防止のため１の時だけ呼び出す
        if (jumpCount == 1)
        {
            playerCollider.enabled = true; //着地時してから常時colliderを出す

            if (Input.GetMouseButtonDown(0) && isJumping == true) //マウスで←が押された時
            {
                anim.SetBool("param_idletorunning", false);
                jumpCount++; //クリックすると増える

                anim.SetBool("param_idletojump", true);
                runSpeed = 8;  //ジャンプ時スピード落とす
                jumpPower = 230;
                rb.AddForce(Vector3.up * jumpPower);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gorl"))
        {
            SceneManager.LoadScene("Result"); //ゲームシーンに移行
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        //床に付いてる時
        if (col.gameObject.CompareTag("Floor"))
        {
            runSpeed = 10;      //着地時スピード戻す
            playerCollider.enabled = true; //着地時すぐにcolliderをtrueにする。
            anim.SetBool("param_idletojump", false);

            jumpCount = 1;    //着地時クリック許可
            isJumping = true; //jump開始
        }
    }

    private void OnCollisionExit(Collision col)
    {
        //床から離れた時
        if (col.gameObject.tag == "Floor")
        {
            isJumping = false;  //空中ジャンプさせない
            playerCollider.enabled = false; //足場離れてるときはコライダーを消す
        }
    }
}
