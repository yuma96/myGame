using System.Collections;
using System.Collections.Generic;
using MiniJSON; // Json

/// <summary>
/// Json response manager.
/// </summary>
public class MemberDataModel
{
    /// <summary>
    /// Deserialize from json.
    /// MemberData型のリストがjsonに入っていると仮定して
    /// </summary>
    /// <returns>The from json.</returns>
    /// <param name="sStrJson">S string json.</param>
    public static List<MemberData> DeserializeFromJson(string sStrJson)
    {
        var ret = new List<MemberData>(); //取得したデータ入れるクラス

        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト      
        IList jsonList = (IList)Json.Deserialize(sStrJson);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        foreach (IDictionary jsonOne in jsonList)
        {
            //新レコード解析開始
            var tmp = new MemberData();

            //該当するキー名が jsonOne に存在するか調べ、存在したら取得して変数に格納する。
            if (jsonOne.Contains("Name"))
            {
                tmp.Name = (string)jsonOne["Name"]; //[NameはWebで表示されてるから大文字]
            }

            if (jsonOne.Contains("Time"))
            {
                tmp.Time = (long)jsonOne["Time"];//[TimeはWebで表示されてから大文字]
            }
            //現レコード解析終了
            ret.Add(tmp);
        }

        return ret;
    }
}
