using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;
using Excelg = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.Data.OracleClient;

public partial class Boundary_wfrmPhoneKeyQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            InitiaPage();
            MultiLanguage();
        }
    }

    /// <summary>
    /// Load data from database to DropDownList contain Model and Station
    /// </summary>
    private void InitiaPage()
    {
        string StrSql = "SELECT MODEL FROM CMCS_SFC_MODEL WHERE ID <> -1 AND ABLED='Y' ORDER BY MODEL";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        ddlModel.DataTextField = "MODEL";
        ddlModel.DataValueField = "MODEL";
        ddlModel.DataSource = dt.DefaultView;
        ddlModel.DataBind();        
    }

    /// <summary>
    /// Change the english and chinese
    /// </summary>
    private void MultiLanguage()
    {
        lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        lblStartDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateFrom");
        lblEndDate.Text = (String)GetGlobalResourceObject("SFCQuery", "DateTo");
        Label28.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        Label29.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
       // btnExportExcel.Text = (String)GetGlobalResourceObject("SFCQuery", "ExportExcel");
    }

    private string GetSql()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strModel = ddlModel.SelectedValue.Trim().ToUpper().ToString();
        string strtbModel="";
        string strSql="";
        string strSN="";

        if ((strModel.Equals("DVR")) || (strModel.Equals("RCX")) || (strModel.Equals("HER")))
            // strSql = "select a.*,b.btaddress from sfc.cdma_upd_parser_meid_v a,shp.cmcs_sfc_imeinum b," + strModel + "." + strModel + "_CIT_PATS_TH c where a.meid_id=b.imeinum and c.track_id=B.SERIAL_NUM ";
            strSql = "select * from (select a.ssn,a.akey1,a.akey2,a.pesn,a.dmeid,b.SERIAL_NUM track_id,a.master_sublock,a.onetime_sublock,b.btaddress ulma1,c.model_num sjug,c.test_date createdate from SFC.CDMA_EMS_MEID_INFO a,shp.cmcs_sfc_imeinum b," + strModel + "." + strModel + "_CIT_PATS_TH c where a.ssn=b.imeinum and c.track_id=B.SERIAL_NUM and test_result='P' order by createdate asc ) where ";
        else
            if(strModel.Equals("CAS"))
                strSql = "select * from (select a.ssn,a.akey1,a.akey2,a.pesn,a.dmeid,b.SERIAL_NUM track_id,a.master_sublock,a.onetime_sublock,b.btaddress ulma1,'' sjug,'' createdate from SFC.CDMA_EMS_MEID_INFO a,shp.cmcs_sfc_imeinum b where a.ssn=b.imeinum) where ";
       
            else
            {
                if ((strModel.Equals("GNG")) || (strModel.Equals("MRO")) || (strModel.Equals("MRE")) || (strModel.Equals("TWN")))
                    strSql = "select * from (select '" + strModel + "' model,imei,privilegepwd,nkey,nskey,spkey,ckey,pkey,fskey,picasso,btaddress,sugnumber sjug,e2pdate createdate from " + strModel + ".e2pconfig) where ";
                else
                {
                    if (strModel.Equals("SLG"))
                    {
                        string strsql = "select imeinum,substr(ppart,3,3) model,SERIAL_NUM from shp.cmcs_sfc_imeinum where serial_num='" + tbIMEI.Text.Trim().ToUpper() + "' or imeinum='" + tbIMEI.Text.Trim().ToUpper() + "' or product_id='" + tbIMEI.Text.Trim().ToUpper() + "'";
                        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string strImei0 = dt.Rows[0]["imeinum"].ToString();
                            string strSN0 = dt.Rows[0]["SERIAL_NUM"].ToString();
                            if (strImei0 == strSN0)
                                strSql = "select * from (select '" + strModel + "' model,imei,privilegepwd,nkey,nskey,spkey,ckey,pkey,fskey,'' picasso,'' btaddress,'' sjug,e2pdate createdate from SFC.NT_IMEI_KEY_LIST ) where ";

                            else
                                strSql = "select * from (select '" + strModel + "' model,imei,privilegepwd,nkey,nskey,spkey,ckey,pkey,fskey,picasso,btaddress,sugnumber sjug,e2pdate createdate from " + strModel + ".e2pconfig) where ";
                        }
                        else
                            strSql = "select * from (select '" + strModel + "' model,imei,privilegepwd,nkey,nskey,spkey,ckey,pkey,fskey,picasso,btaddress,sugnumber sjug,e2pdate createdate from " + strModel + ".e2pconfig) where ";
                    }
                    else
                        if (strModel.Equals("RUY"))
                            strSql = "select * from (select '" + strModel + "' model,a.track_id,a.porder,a.sn,a.esn,a.dec_akey,a.hex_akey,a.trkey_index,a.encoded_akey,a.trkey_index_skt,a.bt_address,b.model_num sjug,b.test_date createdate from sfc.cdma_esn_info a,ruy.ruy_pack_pats_th b where a.track_id=b.track_id and test_result='P' order by test_date asc ) where ";           
                        else
                             strSql = "select * from (select '" + strModel + "' model,a.imei,a.privilegepwd,a.nkey,a.nskey,a.spkey,a.ckey,a.pkey,a.fskey,b.picasso,b.btaddress,b.sugnumber sjug,b.e2pdate createdate from " + strModel + ".KEY_LIST a," + strModel + ".E2PCONFIG b where a.imei=b.imei ) where ";
                }
            }
        if (!tbIMEI.Text.Trim().Equals(""))
        {
            string strImei="";
            string strsql = "select imeinum,substr(ppart,3,3) model,SERIAL_NUM from shp.cmcs_sfc_imeinum where serial_num='" + tbIMEI.Text.Trim().ToUpper() + "' or imeinum='" + tbIMEI.Text.Trim().ToUpper() + "' or product_id='" + tbIMEI.Text.Trim().ToUpper() + "'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            strsql = "";
            if (dt.Rows.Count > 0 && !strModel.Equals("RUY"))
            {
                strImei = dt.Rows[0]["imeinum"].ToString();
                strtbModel = dt.Rows[0]["model"].ToString();
                strSN = dt.Rows[0]["SERIAL_NUM"].ToString();

            }
            else
            {
                if ((strModel.Equals("DVR")) || (strModel.Equals("RCX")) || (strModel.Equals("HER")))
                    return "dvr/rcx";

                else
                    if (!strModel.Equals("RUY"))
                    {
                        strsql = "select distinct a.imei,substr(b.ppart,3,3) model from " + strModel + ".e2pconfig a,shp.cmcs_sfc_porder b  where A.WORKORDER=b.porder and  ( a.serialno='" + tbIMEI.Text.Trim().ToUpper() + "' or a.imei='" + tbIMEI.Text.Trim().ToUpper() + "' or picasso='" + tbIMEI.Text.Trim().ToUpper() + "')";
                        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            strImei = dt1.Rows[0]["imei"].ToString();
                            strtbModel = dt.Rows[0]["model"].ToString();
                        }
                    }
                    else
                    {
                        strtbModel = "RUY";
                        strImei = tbIMEI.Text.Trim().ToUpper();
                    }
            }
            if (!strtbModel.Equals(strModel) && !strModel.Equals("HER") && !strModel.Equals("CAS"))
            {
                return "";
            }
            else
            {
                if ((strModel.Equals("DVR")) || (strModel.Equals("RCX")) || (strModel.Equals("HER")) || (strModel.Equals("CAS")))
                {
                    tbStartDate.DateTextBox.Text = "";
                    tbEndDate.DateTextBox.Text = "";
                    strSql = strSql + " ssn= '" + strImei + "' and rownum<=1 ";
                }
                else
                    if (strModel.Equals("RUY"))
                    {
                        tbStartDate.DateTextBox.Text = "";
                        tbEndDate.DateTextBox.Text = "";
                        strSql = strSql + " track_id= '" + strImei + "'";
                    }
                    else
                    {
                        tbStartDate.DateTextBox.Text = "";
                        tbEndDate.DateTextBox.Text = "";
                        strSql = strSql + " imei= '" + strImei + "'";
                    }
            }
        }
        else
        {
            System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            if (intday.TotalDays > 1)
            {
                return "time";
            }
            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = true;
                Label29.Visible = false;
            }

            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label28.Visible = false;
                Label29.Visible = true;
            }
            strSql = strSql + "  createdate >= TO_DATE(" + ClsCommon.GetSqlString(strStartDate) + ",'YYYY/MM/DD HH24:MI') AND  createdate <=TO_DATE(" + ClsCommon.GetSqlString(strEndDate) + ",'YYYY/MM/DD HH24:MI')  ";
        }
        return strSql;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgKeyList.CurrentPageIndex = 0;
        DataTable dt;
        Label28.Visible = false;
        Label29.Visible = false;
        string strReturn = GetSql();

        if (strReturn.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PlsSelectModel", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "PlsSelectModel") + "');</script>");
            return;
        }
        else
        {
            if (strReturn.Equals("time"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
                return;
            }
            else
            {
                if (strReturn.Equals("dvr/rcx"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('此手機尚未寫入密碼！');</script>");
                    return;
                }
                else
                {
                    try
                    {
                        dt = ClsGlobal.objDataConnect.DataQuery(strReturn).Tables[0];
                        dgKeyList.DataSource = dt.DefaultView;
                        dgKeyList.DataBind();

                        Label4.Text = "Current Page:" + (dgKeyList.CurrentPageIndex + 1).ToString() + "/" + dgKeyList.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

                        dt.Dispose();
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('bug,請聯繫資訊！');</script>");
                        return;
                    }
                }
            }
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string strSql = GetSql();
        ExortToExcel1(strSql);
    }
    protected void dgKeyList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgKeyList.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSql(), DBKey.CDMA).Tables[0];
        dgKeyList.DataSource = dt.DefaultView;
        dgKeyList.DataBind();
        Label4.Text = "Current Page:" + (dgKeyList.CurrentPageIndex + 1).ToString() + "/" + dgKeyList.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

        dt.Dispose();
    }

    private void ExortToExcel1(string strSql)
    {
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //新建一Excel應用程式
        Missing missing = Missing.Value;
        Excelg.ApplicationClass objExcel = null;
        Excelg.Workbooks objBooks = null;
        Excelg.Workbook objBook = null;
        Excelg.Worksheet objSheet = null;
        try
        {
            objExcel = new Excelg.ApplicationClass();
            objExcel.Visible = false;
            objBooks = (Excelg.Workbooks)objExcel.Workbooks;
            objBook = (Excelg.Workbook)(objBooks.Add(missing));
            objSheet = (Excelg.Worksheet)objBook.ActiveSheet;

            clsDBToExcel.ExportToExcel(objSheet, strSql);

            //關閉Excel
            objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excelg.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
            objBook.Close(false, missing, missing);
            objBooks.Close();
            objExcel.Quit();
        }
        finally
        {
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            if (!objSheet.Equals(null))
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
            if (objBook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
            if (objBooks != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
            if (objExcel != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
            GC.Collect();
        }
        //保存或打開報表
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
        Response.Charset = "";
        this.EnableViewState = false;
        Response.WriteFile(ExportPath + strFileName);
        Response.End();
    }
}
