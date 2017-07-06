using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Runtime.InteropServices;

/// <summary>
/// IniFile 的摘要描述
/// </summary>
public class IniFile
{
    public IniFile()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    ///  <summary>
    ///  ini文件名稱(帶路逕)
    ///  </summary>
    public string filePath;

    /// <summary>
    /// 申明寫INI文件的API函數
    /// </summary>
    /// <param name="section">字段名稱</param>
    /// <param name="key">鍵名稱</param>
    /// <param name="val">鍵值</param>
    /// <param name="filePath">文件路逕</param>
    /// <returns>是否正確執行</returns>
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    /// <summary>
    /// 申明讀寫INI文件的API函數
    /// </summary>
    /// <param name="section">字段名稱</param>
    /// <param name="key">鍵名稱</param>
    /// <param name="def">默認值</param>
    /// <param name="retVal">鍵值</param>
    /// <param name="size">值大小</param>
    /// <param name="filePath">文件路逕</param>
    /// <returns>是否正確執行</returns>
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


    ///  <summary>
    ///  類的構造函數
    ///  </summary>
    ///  <param  name="INIPath">INI文件路逕</param>  
    public IniFile(string INIPath)
    {
        filePath = INIPath;
    }

    ///  <summary>
    ///   寫INI文件
    ///  </summary>
    ///  <param  name="strSection">字段名稱</param>
    ///  <param  name="strKey">鍵名稱</param>
    ///  <param  name="strvalue">鍵值</param>
    public void WriteInivalue(string strSection, string strKey, string Strvalue)
    {
        WritePrivateProfileString(strSection, strKey, Strvalue, filePath);
    }


    ///  <summary>
    ///    讀取INI文件指定部分
    ///  </summary>
    ///  <param  name="strSection">字段名稱</param>
    ///  <param  name="strKey">鍵名稱</param>
    ///  <returns>返回鍵值</returns>  
    public string ReadInivalue(string strSection, string strKey)
    {
        StringBuilder temp = new StringBuilder(10000);
        int i = GetPrivateProfileString(strSection, strKey, "", temp, 10000, filePath);
        return temp.ToString();
    }
}
