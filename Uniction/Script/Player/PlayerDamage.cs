using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    public GameObject GameOverUI; //GameOverを表示するUI画面
    public GameObject PlayerHpName; //プレイヤーのHP名前取得
    public Slider PlayerHitPoint;    //プレイヤーのHPバー

    private Animator anim;  //アニメーション

    PlayerMove playerMove; //PlayerMoveスクリプト
    PlayerAttack playerAttack; //PlayerAttackスクリプト

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other) //衝突時の処理
    {
        if (other.gameObject.tag == "EnemyAttack") //タグで限定（他のオブジェクトに衝突した場合は呼び出さない
        {
            PlayerHitPoint.value -= 4; //体力ゲージ１０減らす
            Debug.Log("ダメージ");
        }

        //もしプレイヤーのHPが０になったら
        if (PlayerHitPoint.value <= 0)
        {
            //死んだら一切行動できなくなる
            playerMove.enabled = false;
            playerAttack.enabled = false;
            //死んだらHP,Nameを消す
            PlayerHpName.gameObject.SetActive(false);
            //アニメーションfalse
            anim.SetBool("is_Run", false);
            anim.SetBool("is_Jab", false);
            anim.SetBool("is_Hikick", false);
            anim.SetBool("is_Spinkick", false);
            //倒れるアニメション
            anim.SetBool("is_DamageDown", true);
            Debug.Log("PlayerDied"); //死にました
            GameOverUI.SetActive(true); //GameOverUI表示
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
