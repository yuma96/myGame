using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator anim;
    private Collider Lefthand;   //左手パンチコライダー
    private Collider Rightfoot;  //右足ハイキックコライダー
    private Collider Rightfoot2; //右足スピンキックコライダー

    // 最後にキーが押された数
    private int AttackPoint;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Lefthand = GameObject.Find("Character1_LeftHand").GetComponent<SphereCollider>();
        Rightfoot = GameObject.Find("Character1_RightToeBase").GetComponent<SphereCollider>();
        Rightfoot2 = GameObject.Find("Character1_RightToeBase2").GetComponent<SphereCollider>();
        AttackPoint = 1; //カウントする数
    }

    // Update is called once per frame
    void Update()
    {
        //J押した時とAttackPointが１の場合
        if (Input.GetKeyDown(KeyCode.J) && AttackPoint == 1)
        {
            AttackPoint++; //L押すとカウント
            anim.SetBool("is_Jab", true);

            //左手コライダーをオンにする
            Invoke("LefthandColliderRestart", 0.0f);
            Debug.Log("Punch");

            //一定時間後にコライダーの機能をオフにする
            Invoke("AttackColliderReset", 0.2f);
            StartCoroutine(Punch()); //コルーチン呼び出し
        }
        else
        {
            anim.SetBool("is_Jab", false);
        }

        //K押した時とAttackPointが１の場合
        if (Input.GetKeyDown(KeyCode.K) && AttackPoint == 1)
        {
            AttackPoint++; //L押すとカウント
            anim.SetBool("is_Hikick", true);

            //キックコライダーをオンにする
            //Rightfoot.enabled = true;
            Invoke("RightfootColliderRestart", 0.0f);
            Debug.Log("HiKick");

            //一定時間後にキックコライダーの機能をオフにする
            Invoke("RightfootColliderReset", 0.3f);
            StartCoroutine(Hikick()); //コルーチン呼び出し
        }
        else
        {
            anim.SetBool("is_Hikick", false);
        }

        //L押した時とAttackPointが１の場合
        if (Input.GetKeyDown(KeyCode.L) && AttackPoint == 1)
        {
            AttackPoint++; //L押すとカウント
            anim.SetBool("is_Spinkick", true);

            //回し蹴りコライダーをオンにする
            Invoke("SpinKickColliderRestart", 0.0f);
            Debug.Log("SpinKick");

            //一定時間後に回し蹴りコライダーの機能をオフにする
            Invoke("SpinKickColliderReset", 0.5f);
            StartCoroutine(Spinkick()); //コルーチン呼び出し
        }
        else
        {
            anim.SetBool("is_Spinkick", false);
        }
    }

    //パンチ連打防止
    IEnumerator Punch()
    {
        Debug.Log("パンチ");
        yield return new WaitForSeconds(0.3f); //クールタイム
        AttackPoint = 1;
        Debug.Log("パンチタイム終了");
    }

    //ハイキック連打防止
    IEnumerator Hikick()
    {
        Debug.Log("ハイキック");
        yield return new WaitForSeconds(0.3f); //クールタイム
        AttackPoint = 1;
        Debug.Log("ハイキックタイム終了");
    }

    //スピンキック連打防止
    IEnumerator Spinkick()
    {
        Debug.Log("スピンキック");
        yield return new WaitForSeconds(0.8f); //クールタイム
        AttackPoint = 1;
        Debug.Log("スピンキックタイム終了");
    }

    //パンチ攻撃が一定時間たったらコライダーをオンにするの関数
    private void LefthandColliderRestart()
    {
        Lefthand.enabled = true;
    }

    //パンチ攻撃が一定時間たったらコライダーをオフにするの関数
    private void AttackColliderReset()
    {
        Lefthand.enabled = false;
    }

    //キック攻撃が一定時間たったらコライダーをオンにするの関数
    private void RightfootColliderRestart()
    {
        Rightfoot.enabled = true;
    }

    //キック攻撃が一定時間たったらコライダーをオフにするの関数
    private void RightfootColliderReset()
    {
        Rightfoot.enabled = false;
    }

    //回し蹴り攻撃が一定時間たったらコライダーをオンにするの関数
    private void SpinKickColliderRestart()
    {
        Rightfoot2.enabled = true;
    }

    //回し蹴り攻撃が一定時間たったらコライダーをオフにするの関数
    private void SpinKickColliderReset()
    {
        Rightfoot2.enabled = false;
    }
}
