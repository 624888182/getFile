/*************************************************************************
 * 
 *  Unit description: SFCConfiguration entity.
 *  Developer: Shu Jian Bo             Date: 2007/12/12
 *  Modifier : Shu Jian Bo             Date: 2007/12/12
 * 
 * ***********************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// clsSFCConfiguration 的摘要描述
/// </summary>
public class clsSFCConfiguration
{
	public clsSFCConfiguration()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
    private string PModel;
    private string PStationID;
    private string PColumnName;
    private string PColumnDesc;
    private string PFunctionID;
    private string PFunctionValue;
    private string PCreationDate;
    private string PRowID;

    /// <summary>
    /// 機種名稱
    /// </summary>
    public string FModel
    {
        set
        {
            PModel = value ;
        }
        get
        {
            return PModel;
        }
    }
    /// <summary>
    /// 工站ID：如DL代表DownLoad;A1代表AB1
    /// </summary>
    public string FStationID
    {
        set
        {
            PStationID = value;
        }
        get
        {
            return PStationID;
        }
    }
    /// <summary>
    /// 欄位名稱
    /// </summary>
    public string FColumnName
    {
        set
        {
            PColumnName = value;
        }
        get
        {
            return PColumnName;
        }
    }
    /// <summary>
    /// 欄位描述
    /// </summary>
    public string FColumnDesc
    {
        set
        {
            PColumnDesc = value;
        }
        get
        {
            return PColumnDesc;
        }
    }
    /// <summary>
    /// 功能ID，來自於表：tblfunction_name 
    /// </summary>
    public string FFunctionID
    {
        set
        {
            PFunctionID = value;
        }
        get
        {
            return PFunctionID;
        }
    }
    /// <summary>
    /// 功能項的值，來自於表：tblfunction_values 
    /// </summary>
    public string FFunctionValue
    {
        set
        {
            PFunctionValue = value;
        }
        get
        {
            return PFunctionValue;
        }
    }
    /// <summary>
    /// 建立日期
    /// </summary>
    public string FCreationDate
    {
        set
        {
            PCreationDate = value;
        }
        get
        {
            return PCreationDate;
        }
    }
    /// <summary>
    /// 自來資料庫表的一個偽列，用於在刪除資料時的ID號
    /// </summary>
    public string FRowID
    {
        set
        {
            PRowID = value;
        }
        get
        {
            return PRowID;
        }
    }
}
