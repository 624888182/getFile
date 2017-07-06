using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Economy.Publibrary;
using System.Data;
using SCM.GSCMDKen;

public partial class Pub_JDeleteData : System.Web.UI.Page
{

    private static string dbCheck;
    private static string dbWrite;
    private static bool bRun;
    protected void Page_Load(object sender, EventArgs e)
    {

        /*dataBaseType = Session["Param2"].ToString();  // Oracle
        readConnection = Session["Param3"].ToString();  // 讀資料庫
        writeConnection = Session["Param4"].ToString();   // 寫資料庫
        prgType = Session["Param5"].ToString(); // menu or auto 
        */

        if (!Page.IsPostBack)
        {
            bRun = false;
            div2.Visible = false;
            CheckBox1.Checked = true;
            CheckBox2.Checked = true;
            CheckBox3.Checked = true;
            CheckBox4.Checked = true;
            try
            {
                string s1 = Session["Param1"].ToString();
                string s2 = Session["Param2"].ToString();
                string s3 = Session["Param3"].ToString();
                string s4 = Session["Param4"].ToString();
                string s5 = Session["Param5"].ToString();
                if (s1 == null) s1 = "";
                if (s2 == null) s2 = "";
                if (s3 == null) s3 = "";
                if (s4 == null) s4 = "";
                if (s5 == null) s5 = "";
                if (s1 == "1")
                {
                    dbCheck = s3;
                    dbWrite = s4;
                }
                else
                {
                    dbCheck = ConfigurationManager.AppSettings["Sql221String"].ToString();
                    dbWrite = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.186.171.221)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=tjsfc)));uid=sfc;pwd=sfc";
                }
            }
            catch
            {
                dbCheck = ConfigurationManager.AppSettings["Sql221String"].ToString();
                dbWrite = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.186.171.221)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=tjsfc)));uid=sfc;pwd=sfc";
            }
        }


        
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        string sPSW = TextBox3.Text;
        if (TextBox3.Text.Trim() == "")
        {
            Label5.Text = "Please input password ";
            return;
        }
        if (TextBox1.Text.Trim() == "")
        {
            Label5.Text = "Please input begin date ";
            return;
        }
        if (TextBox2.Text.Trim() == "")
        {
            Label5.Text = "Please input end date ";
            return;
        }


        if ((bRun == true) || (sPSW == "foxconn99"))
        {
            string sBegin = TextBox1.Text;
            string sEnd = TextBox2.Text;
            bool b1 = CheckBox1.Checked;
            bool b2 = CheckBox2.Checked;
            bool b3 = CheckBox3.Checked;
            bool b4 = CheckBox4.Checked;
            DeleteTableDate(dbCheck, "sql", dbWrite, "oracle", sBegin, sEnd, b1, b2, b3, b4);

        }
        else
        { 
        
        }
        
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        if (div2.Visible == false)
        {
            div2.Visible = true;
            btn2.Text = "Hide Note";
        }
        else
        {
            div2.Visible = false;
            btn2.Text = "Note";
        }
        
    }

    private void DeleteTableDate(string CheckDB,string CheckType,string DelDB,string DelType,string BeginDate,string EndDate,bool b1,bool b2,bool b3,bool b4)
    {
        DataBaseOperation dboRead = new DataBaseOperation(CheckType, CheckDB);
        DataBaseOperation dboWrite = new DataBaseOperation(DelType, DelDB);
        AppPubLib AppPubLibPointer = new AppPubLib();
        BeginDate = BeginDate.Replace("/", "");
        EndDate = EndDate.Replace("/", "");
        string sSql = "Select IMEI,PID,Cartion,Flag1 From DelIMEIctl Where Delivery_Date >= @VD1 And Delivery_Date < @VD2 ";
        DataTable dtSet = dboRead.SelectSQLDT(sSql, new string[] { "@VD1", "@VD2" }, new object[] { BeginDate, EndDate });
        int iCount1 = 0;
        int iCount2 = 0;
        int iCount3 = 0;
        int iCount4 = 0;
        for (int i = 0; i < dtSet.Rows.Count; i++)
        {
            bool bUpdate = false;
            string sFlag = dtSet.Rows[i]["Flag1"].ToString();
            string s1 = AppPubLibPointer.SetBitProc(sFlag, "2", "2", "Read", "");
            string s2 = AppPubLibPointer.SetBitProc(sFlag, "3", "3", "Read", "");
            string s3 = AppPubLibPointer.SetBitProc(sFlag, "4", "4", "Read", "");
            string s4 = AppPubLibPointer.SetBitProc(sFlag, "5", "5", "Read", "");
            string sPID = dtSet.Rows[i]["PID"].ToString().Trim();
            string sIMEI = dtSet.Rows[i]["IMEI"].ToString().Trim();

            #region Delete_SHP.CMCS_SFC_IMEINUM
            int iDel;
            if ((s1 == "1") && (b1 == true))
            {
                sSql = "Delete SHP.CMCS_SFC_IMEINUM Where IMEINUM = :V_IMEI ";
                iDel = dboWrite.ExecSQL(sSql, new string[] { "V_IMEI" }, new object[] { sIMEI });
                sFlag = AppPubLibPointer.SetBitProc(sFlag, "2", "2", "Write", "D");
                if (iDel >= 0) iCount1 += iDel;
                bUpdate = true;
            }
            #endregion

            #region SFC.MES_ASSY_PID_JOIN
            if ((s2 == "1") && (b2 == true))
            {
                sSql = "Delete From SFC.MES_ASSY_PID_JOIN Where MAIN_ID = :V_ID ";
                iDel = dboWrite.ExecSQL(sSql, new string[] { "V_ID" }, new object[] { sPID });
                sFlag = AppPubLibPointer.SetBitProc(sFlag, "3", "3", "Write", "D");
                if (iDel >= 0) iCount2 += iDel;
                bUpdate = true;
            }
            #endregion

            #region SFC.MES_ASSY_HISTORY
            if ((s3 == "1") && (b3 == true))
            {
                sSql = "Delete From SFC.MES_ASSY_HISTORY Where PRODUCT_ID = :V_ID ";
                iDel = dboWrite.ExecSQL(sSql, new string[] { "V_ID" }, new object[] { sPID });
                sFlag = AppPubLibPointer.SetBitProc(sFlag, "4", "4", "Write", "D");
                if (iDel >= 0) iCount3 += iDel;
                bUpdate = true;
            }
            #endregion

            #region SFC.R_WIP_TRACKING_T_PID
            if ((s4 == "1") && (b4 == true))
            {
                sSql = "Delete From SFC.R_WIP_TRACKING_T_PID Where SERIAL_NUMBER = :V_ID ";
                iDel = dboWrite.ExecSQL(sSql, new string[] { "V_ID" }, new object[] { sPID });
                sFlag = AppPubLibPointer.SetBitProc(sFlag, "5", "5", "Write", "D");
                if (iDel >= 0) iCount4 += iDel;
                bUpdate = true;
            }
            #endregion

            #region UpdateInfo
            if (bUpdate == true)
            {
                string sDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                sSql = "Update DelIMEIctl set Flag1 = :V_Flag , Delete_Date = :V_Date Where IMEI = :V_IMEI ";
                iDel = dboRead.ExecSQL(sSql, new string[] { "V_Flag", "V_Date", "V_IMEI" }, new object[] { sFlag, sDate, sIMEI });
            }
            #endregion
        }
        Label5.Text = "Delete Table1(SHP.CMCS_SFC_IMEINUM)): " + iCount1.ToString();
        Label6.Text = "Delete Table2(SFC.MES_ASSY_PID_JOIN)): " + iCount2.ToString();
        Label7.Text = "Delete Table1(SHP.MES_ASSY_HISTORY)): " + iCount3.ToString();
        Label8.Text = "Delete Table1(SHP.R_WIP_TRACKING_T_PID)): " + iCount4.ToString();
    
    
    }
}
