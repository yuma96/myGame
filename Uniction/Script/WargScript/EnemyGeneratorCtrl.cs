using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGeneratorCtrl : MonoBehaviour {

    private GameObject[] enemyObjects; //配列（同じ種類の複数のデータを収納するための箱を作る)
    public GameObject enemyPrefab; //生まれてくる敵プレハブ
    public int maxEnemy = 2; //敵の最大数
    GameObject[] existEnemys; //敵を格納

    // Use this for initialization
    void Start() {
        existEnemys = new GameObject[maxEnemy]; //maxEnemy配列確保
        StartCoroutine(Exec()); //周期的に実行
    }

    //敵を呼び出す
    IEnumerator Exec()
    {
        while (true)
        {
            Generate(); //関数呼ぶ
            yield return new WaitForSeconds(5.0f); //生成に5秒間間隔をあける
        }
    }

    //敵生成
    void Generate()
    {
        //existEnemyのmaxEnemy数２以下の時enemyCountを増やす
        for (int enemyCount = 0;
            enemyCount < existEnemys.Length; ++ enemyCount)
        {
            //オブジェクト生成
            if (existEnemys[enemyCount] == null)
            {
                existEnemys[enemyCount] = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject; //プレファブから生成
                var obj = existEnemys[enemyCount]; //existEnmeyをobjに入れ名前変更
                obj.name = "Warg"; //名前変更
                return; //値を返す
            }
        }
    }

	// Update is called once per frame
	void Update () {
        // Enemyというタグが付いているオブジェクトのデータを箱の中に入れる。
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // エネミーの入った箱の数をコンソール画面に表示する。
        //print(enemyObjects.Length);

        // データの入った箱のデータが０に等しくなった時（Enemyというタグが付いているオブジェクトが全滅したとき）
        if (enemyObjects.Length == 0)
        {
            // ゲームクリアーシーンに遷移する。
            //SceneManager.LoadScene("Title");
        }
    }
}
