using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WargDamage : MonoBehaviour {

    WargAI wargAI;  //WargAiにwargAIを入れる
    public Slider WargHitPoint;    //WargのHPバー
    private Animator anim;  //アニメーション
    public GameObject DamageParticle; //ダメージエフェクト
    public bool Diedflg; //死亡フラグ

    // Use this for initialization
    void Start () {
        wargAI = transform.root.GetComponent<WargAI>();////EnemyAIを読み込み,SendMessageをWargAIに送るのに必要
        anim = GetComponent<Animator>();
        Diedflg = false; //最初死亡false
	}

    //プレイヤー攻撃時ダメージ処理
    private void OnTriggerEnter(Collider other)
    {
        //Punchタグ限定
        if (other.gameObject.tag == "Punch")
        {
            WargHitPoint.value -= 10; //体力ゲージ10減らす
            Debug.Log("Punchダメージ");
            
            // エフェクトの発生.
            GameObject effect = Instantiate(DamageParticle, transform.position, Quaternion.identity) as GameObject; //エフェクト生成
            effect.transform.localPosition = transform.position + new Vector3(0.0f, -0.5f, 0.0f); //エフェクト位置
            Destroy(effect, 0.3f); //0.3秒後に消す
        }

        //Hikickタグ限定
        if (other.gameObject.tag == "HiKick")
        {
            WargHitPoint.value -= 25; //体力ゲージ25減らす
            Debug.Log("HiKickダメージ");

            // エフェクトの発生.
            GameObject effect = Instantiate(DamageParticle, transform.position, Quaternion.identity) as GameObject; //エフェクト生成
            effect.transform.localPosition = transform.position + new Vector3(0.0f, -0.5f, 0.0f); //エフェクト位置
            Destroy(effect, 0.3f); //0.3秒後に消す
        }

        //Spinkickタグ限定
        if (other.gameObject.tag == "SpinKick")
        {
            WargHitPoint.value -= 50; //体力ゲージ50減らす
            Debug.Log("SpinKickダメージ");

            // エフェクトの発生.
            GameObject effect = Instantiate(DamageParticle, transform.position, Quaternion.identity) as GameObject; //エフェクト生成
            effect.transform.localPosition = transform.position + new Vector3(0.0f, -0.5f, 0.0f); //エフェクト位置
            Destroy(effect, 0.3f); //0.3秒後に消す
        }

        //wargのHPが０だったら
        if (WargHitPoint.value <= 0)
        {
            Diedflg = true;//死亡フラグたてる
            if (Diedflg == true)
            {
                SendMessage("Died"); //WargAiのDieに情報送る
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
