using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargAI : MonoBehaviour {

    //CharacterMoveスクリプトをcharacterMoveに入れる
    CharacterMove characterMove;
    //WargDamageスクリプト入れる
    WargDamage wargDamage;
    // 待機時間は２秒とする
    public float waitBaseTime = 2.0f;
    // 残り待機時間
    float waitTime;
    // 移動範囲５メートル
    public float walkRange = 5.0f;
    // 初期位置を保存しておく変数
    public Vector3 basePosition;
    //Wargアニメーション
    private Animator anim;
    //噛むコライダー
    [SerializeField] private Collider kamistuku;
    //Wargがターゲットにするもの
    Transform attackTarget;
    //キャラコン入れる
    CharacterController characterController;
    //吹っ飛びベクトル
    private Vector3 moveDirection = Vector3.zero;
    //落下ベクトル
    private Vector3 velocity;
    //落ちる重力時間
    private float totalFallTime = 0f;
    //死ぬフラグ
    public bool Diedflg;
    //時間制限
    public float timeOut;
    //経過時間
    public float timeElapsed;

    // ステートの種類.
    enum State
    {
        Walking,	// 探索
        Chasing,	// 追跡
        Attacking,	// 攻撃
        Died,		// 死亡
    };

    State state = State.Walking;        // 現在のステート.
    State nextState = State.Walking;	// 次のステート.

    void Start() {
        //キャラコンを受け取る
        characterController = GetComponent<CharacterController>();
        //死ぬフラグfalse
        Diedflg = false;
        //CharacterMoveスクリプトを受け取る
        characterMove = GetComponent<CharacterMove>();
        //WargDamageスクリプト受け取る
        wargDamage = GetComponent<WargDamage>();
        //アニメーション取得
        anim = GetComponent<Animator>();
        // 初期位置を保持
        basePosition = transform.position;
        // 待機時間
        waitTime = waitBaseTime;
        //HEAD1_CにあるSphereColliderをkamistukuで取得
        //×生成する時最初の一体の"HEAD1_C"だけ当たり判定を取得してしまう問題がある。
        //kamistuku = GameObject.Find("HEAD1_C").GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のステート
        switch (state)
        {
            case State.Walking:   //探索ステートだったら
                Walking();        //探索関数呼ぶ
                break;            //swich文でcase内の処理を終了する
                
            case State.Chasing:   //追跡ステートだったら
                Chasing();        //追跡関数呼ぶ
                break;            //処理を終了する

            case State.Attacking: //攻撃ステートだったら
                Attacking();      //攻撃関数呼ぶ
                break;            //処理を終了する
        }

        //次のステートに移行するための処理,StateがnextStateと違ったら
        if(state != nextState)
        {
            state = nextState;       //nextStateにあるステートをStateに入れる
            switch (state)
            {
                case State.Walking:  //探索だったら
                    WalkStart();     //WalkStart呼び出し
                    break;           //処理を終了する

                case State.Chasing:  //追跡だったら
                    ChaseStart();    //ChaseStart呼び出し
                    break;           //処理を終了する

                case State.Attacking://攻撃だったら
                    AttackStart();   //AttackStart呼び出し
                    break;           //処理を終了する

                case State.Died:     //死亡ステートだったら
                    Died();          //死亡呼び出し
                    break;           //処理を終了する
            }
        }
        //死ぬフラグ立った時
        if (Diedflg == true)
        {
            Died();
        }
    }
    
    // ステートを変更(チェンジ)する.
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    //探索
    void WalkStart()
    {
        StateStartCommon(); //ステートが始まる前にステータスを初期化する.
    }

    //探索中
    void Walking()
    {
        anim.SetBool("is_Warg_Attack", false); //走るアニメージョンoff
        anim.SetBool("is_Warg_Run", true); //走るアニメージョンon
       
        // 待機時間がまだあったら
        if (waitTime > 0.0f)
        {
            anim.SetBool("is_Warg_Run", false); //走るアニメージョンoff
            // 待機時間を減らす
            waitTime -= Time.deltaTime;
            // 待機時間が無くなったら
            if (waitTime <= 0.0f)
            {
                // ランダムに範囲内の何処か点を返す
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // 移動先の設定
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                //　目的地の指定.
                SendMessage("SetDestination", destinationPosition);
            }
        }
        else
        {
            // 目的地へ到着
            if (characterMove.Arrived())
            {
                // 待機状態へ
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }

            // ターゲットを発見したら追跡
            if (attackTarget)
            {
                //追跡ステートに移行
                ChangeState(State.Chasing);
            }
        }
    }

    // 追跡開始
    void ChaseStart()
    {
        StateStartCommon(); //ステートが始まる前にステータスを初期化する.
    }

    void Chasing()
    {
        // 移動先をプレイヤーに設定
        SendMessage("SetDestination", attackTarget.position);

        // プレイヤーと4m以上に離れたらまた探索する
        if (Vector3.Distance(attackTarget.position, transform.position) >= 4.0f)
        {
            attackTarget = null;          //追いかけるものを初期化nullにする
            ChangeState(State.Walking);   //ステートを(探索)にする
        }
        
        // 2m以内に近づいたら攻撃
        else if (Vector3.Distance(attackTarget.position, transform.position) <= 2.0f)
        {
            ChangeState(State.Attacking); //攻撃ステートにする
        }
    }

    // 攻撃ステートが始まる前に呼び出される.
    void AttackStart()
    {
        StateStartCommon(); //初期化する

        // プレイヤーの方向に振り向かせる.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        SendMessage("SetDirection", targetDirection); //"CharaMove"にSetDirection関数送る

        // 移動を止める."CharaMove"にStopMove関数送る
        SendMessage("StopMove");
    }

    // 攻撃中の処理.
    void Attacking()
    {    
        //攻撃アニメージョンon
        anim.SetBool("is_Warg_Attack", true);
        // ターゲットをリセットする
        attackTarget = null;
        // 待機時間を再設定
        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        //ステートを(探索)にする
        ChangeState(State.Walking);
        //一定時間後にコライダーの機能をオンにする
        Invoke("ColliderRestart", 0.6f);
        //一定時間後にコライダーの機能をオフにする
        Invoke("ColliderReset", 0.8f);
    }

    //死んだときの処理
    void Died()
    {
        characterMove.enabled = false; //characterMoveのスクリプトをoffにする
        Diedflg = true; //死ぬフラグたてる
        ChangeState(State.Died); //死ぬステートにする

        //死ぬフラグ
        if (Diedflg == true)
        {
            //吹っ飛び処理
            transform.position -= transform.forward * 20f * Time.deltaTime; //後ろに飛ばす
            transform.position += new Vector3(0f, 0.1f, 0f * Time.deltaTime); //角度
            characterController.Move(moveDirection * Time.deltaTime); //キャラコンのベクトル動かす

            timeElapsed += Time.deltaTime; //タイムを増やしていく

            //ElapsedがtimeOutより大きくなったら吹っ飛び落下処理
            if (timeElapsed > timeOut)
            {
                totalFallTime += Time.deltaTime;
                velocity.y = Physics.gravity.y * totalFallTime; //重力計算
                characterController.Move(velocity * Time.deltaTime); //キャラコンの重力与える

                //地面に付いたら
                if (characterController.isGrounded)
                {
                    Diedflg = false; //フラグで吹っ飛ばし止める

                    //討伐数追加
                    FindObjectOfType<WargCount>().AddPoint(1);//WargCountスクリプトに１体倒したを送る
                    Destroy(gameObject); //Wargを消す
                    Debug.Log("Warg死亡"); //死にました
                }
            }
        }
    }

    // ステートが始まる前にステータスを初期化する.
    void StateStartCommon()
    {
       //Debug.Log("リセット"); //初期化
    }
    
    // 攻撃対象を設定する
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target; //attackTargetにtarget入れる
    }

    //攻撃が一定時間たったらコライダーをオンにするの関数
    private void ColliderRestart()
    {
        kamistuku.enabled = true;
    }

    //攻撃が一定時間たったらコライダーをオフにするの関数
    private void ColliderReset()
    {
        kamistuku.enabled = false;
    }
}
