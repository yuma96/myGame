using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text
using System; // File
using UnityEngine.Networking;

/// <summary>
/// Manager main.
/// </summary>
public class ManagerMain : MonoBehaviour
{
    [SerializeField] private Text _displayField = default; //テーブルtext
    [SerializeField] private Text inputName = default; //押された名前

    public int getTime = default;
    private List<MemberData> _memberList; //データベースのクラス取得

    void Start()
    {
        getTime = Ranking.getPoint();            //タイム関数取得
    }

    /// <summary>
    /// Raises the click clear display event.
    /// </summary>
    public void OnClickClearDisplay()
    {
        _displayField.text = " ";
    }

    /// <summary>
    /// Raises the click get json from www event.
    /// </summary>

    //名前登録ボタン追加
    public void OnClickGetJsonFromWebRequest()
    {
        //登録中
        _displayField.text = "";
        GetJsonFromWebRequest();
    }

    /// <summary>
    /// Raises the click show member list
    /// </summary>
    public void OnClickShowMemberList()
    {
        string sStrOutput = "";

        if (null == _memberList)
        {
            sStrOutput = "no list !";
        }
        else
        {
            //リストの内容を表示
            foreach (MemberData memberOne in _memberList)
            { //テキストに表示
                sStrOutput += $"{memberOne.Name}           {memberOne.Time + "点"}\n\n";
            }
        }

        _displayField.text = sStrOutput;
    }


    /// <summary>
    /// Gets the json from www.
    /// </summary>
    private void GetJsonFromWebRequest()
    {
        // API を呼んだ際に想定されるレスポンス
        // [{"name":"\u3072\u3068\u308a\u3081","age":123,"hobby":"\u30b4\u30eb\u30d5"},{"name":"\u3075\u305f\u308a\u3081","age":25,"hobby":"walk"},{"name":"\u3055\u3093\u306b\u3093\u3081","age":77,"hobby":"\u5c71"}]
        //

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(
            DownloadJson(
                CallbackWebRequestSuccess, // APIコールが成功した際に呼ばれる関数を指定
                CallbackWebRequestFailed // APIコールが失敗した際に呼ばれる関数を指定
            )
        );
    }

    /// <summary>
    /// Callbacks the www success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void CallbackWebRequestSuccess(string response)
    {
        //Json の内容を MemberData型のリストとしてデコードする。
        _memberList = MemberDataModel.DeserializeFromJson(response);

        //memberList ここにデコードされたメンバーリストが格納される。
        //成功
        _displayField.text = "";
    }

    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void CallbackWebRequestFailed()
    {
        // jsonデータ取得に失敗した
        _displayField.text = "";
    }

    /// <summary>
    /// Downloads the json.
    /// </summary>
    /// <returns>The json.</returns>
    /// <param name="cbkSuccess">Cbk success.</param>
    /// <param name="cbkFailed">Cbk failed.</param>
    private IEnumerator DownloadJson(Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        //データWebから受け取り
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/rankingproject/ranktable/getRankings");
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(www.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (www.isDone)
        {
            // リクエスト成功の場合
            Debug.Log($"{www.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(www.downloadHandler.text); //CallbackWebRequestSuccess呼ばれる
            }
        }
    }

    public void OnClickSetMessage()
    {
        _displayField.text = "";
        SetJsonFromWWW();
    }

    private void SetJsonFromWWW()
    {
        string sTqtURL = "http://localhost/rankingproject/ranktable/setRanking";

        //string name = inputName.text;
        string name = inputName.text; //データベースに送る名前
        int time = getTime;    //データベースに送るタイム

        StartCoroutine(SetMessage(sTqtURL, name, time, WebRequestSuccess, CallbackWebRequestFailed));
    }

    private IEnumerator SetMessage(string url, string name, int time, Action<string> cbkSuccess = null, Action cbkFaild = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);        //rankBoardの"name"を送信
        form.AddField("time", time);        //rankBoardの"time"を送信

        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);

        webRequest.timeout = 5; //しばらくつながらないと5秒経つとタイムアウト

        yield return webRequest.SendWebRequest(); //リモートサーバーと通信

        //エラー返ったら時
        if (webRequest.error != null)
        {
            if (null != cbkFaild)
            {
                cbkFaild(); //関数を再度呼ぶ
                Debug.Log(webRequest.error);
            }
        }
        else if (webRequest.isDone)
        {
            Debug.Log($"{webRequest.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }

    private void WebRequestSuccess(string response)
    {
        _displayField.text = response;
    }
}