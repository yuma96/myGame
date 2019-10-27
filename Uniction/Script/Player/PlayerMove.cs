using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float RunSpeed;             //走るスピード
    public GameObject cameraObject;　  //カメラのオブジェクト

    private Animator anim;  //アニメーション
    private CharacterController characterController; //キャラクターコライダー
    private float moveX;    //Horaizontalの値
    private float moveZ;    //Verticalの値
    private Vector3 velocity = Vector3.zero; //ベクトル(0, 0, 0)にする
    private Vector3 moveForward; //カメラ向きで移動方向を取得
    private Vector3 moveVector;  //入力された移動速度を取得

    [SerializeField] private float jumpPower = 20f; //ジャンプに与える力
    [SerializeField] private Vector3 addForceDownPower = Vector3.down;  //下方向に強制的に加える力(0,-1,0)

    PlayerAttack playerAttack; //プレイヤー攻撃スクリプト
    
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        //キャラクターコライダー地面に付いてる時
        if (characterController.isGrounded)
        {
            //地面に付いてるとき攻撃スクリプトonにする
            playerAttack.enabled = true;

            //moveXYにHV代入
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");

            //WASD押してる時
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                //動いてるとき攻撃できないよう攻撃スクリプトoff
                playerAttack.enabled = false;

                //カメラの方向から、X-Z平面の単位ベクトルを取得
                Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1));

                //wasdキーの入力値とカメラの向きから、移動方向を決定
                moveForward = cameraForward * moveZ + cameraObject.transform.right * moveX;

                //入力された移動速度
                moveVector = moveForward * RunSpeed + new Vector3(0, velocity.y, 0);

                //キャラクターの向きを進行方向に向ける
                if (moveForward != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(moveForward);

                //走る動作
                transform.Translate(Vector3.forward * Time.deltaTime * RunSpeed * 1);
                anim.SetBool("is_Run", true);
            }
            else
            {
                anim.SetBool("is_Run", false);
            }

            if (Input.GetButtonDown("Jump")) //space押したとき
            {
                velocity.y += jumpPower; //ジャンプ高さ
            }
        }
        velocity.y -= 10 * Time.deltaTime; //重力
        characterController.Move(velocity * Time.deltaTime); //Moveに重力渡す
    }
}
