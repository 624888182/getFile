using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DBAccess.EAI;
using System.Reflection;
using System.Resources;
using System.Globalization;
using DB.EAI;
using System.Threading;
using System.Data.OracleClient;
using System.Data.Odbc;

public partial class Boundary_wfrmLHSNImport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            lbalert.Visible = false;
            lbalert.Text = "";
            lbalter1.Visible = false;
            MultiLanguage();
        }
    }

    private void MultiLanguage()
    { 
        lbPorder.Text = (String)GetGlobalResourceObject("SFCQuery", "LHPorder");//rm.GetString("WoInfo");
        lbSN.Text = (String)GetGlobalResourceObject("SFCQuery", "LHParth");//rm.GetString("ProductInfo");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strPorder = tbPorder.Text.Trim().ToUpper();
        string strPPart = "";
        if (strPorder.Equals(""))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入你要導入的P工單！！');</script>");
            return;
        }
        string strsql = "SELECT PPART from SHP.CMCS_SFC_PORDER where porder='" + strPorder + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];

        if (dt.Rows.Count > 0)
        {
            strPPart = dt.Rows[0]["PPART"].ToString().ToUpper();
            Insert(strPorder, strPPart);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('此工單不存在！！');</script>");
            return;
        }
    }

    private void Insert(string porder,string ppart)
    {
        string filePath;
        string fileName; 
        string strSql;
        string strpart;
        string strorder;
        DataSet ds;
        int k = 0;
        int count=0;
        string temp = "";
        string notemp = "";

        ds = null;
        fileName = FileUpload.FileName.ToString();
        filePath = FileUpload.PostedFile.FileName.ToString();
        //filePath = filePath.Substring(0, filePath.Length - fileName.Length);
        string path = Server.MapPath("~/Data/");

        if (FileUpload.HasFile)
        {
            try
            {
                FileUpload.SaveAs(path + FileUpload.FileName);
            }
            catch
            {
                Response.Write(" <script> alert( '這個文件已經上傳了！') </script> ");
                return;
            }
        }

        if (fileName == "")
        {
            //Response.Write(" <script> alert( '您没有选择要的文件！') </script> ");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('您没有选择要導入的文件！！');</script>");
            return;
        }

        ds = GetDataSetFromExcel(filePath, fileName);

        if (ds == null)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請檢查要導入的文件是否正確！');</script>");
            return;
        }
        else
        {
            //ds = GetDataSetFromExcel(path, fileName);
            if (ds.Tables.Count == 0)
            {
                //Response.Write("<script>  alert('請檢查正在上傳的文件是否已經打開，若是，請先關閉它再上傳！')</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請檢查正在導入的文件是否已經打開，若是，請先關閉它再導入！');</script>");
                return;
            }

            string[] yiImport = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string[] list = new string[ds.Tables[0].Columns.Count];
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    count++;
                    strSql = "";
                    list[j] = ds.Tables[0].Rows[i][j].ToString();
                    //strSql = "SELECT * from SHP.ZJJ_CMCS_SFC_IMEINUM where SERIAL_NUM='" + ds.Tables[0].Rows[i][j].ToString().ToUpper() + "'";
                    strSql = "SELECT * from SHP.CMCS_SFC_IMEINUM where SERIAL_NUM='" + list[j].ToUpper() + "' OR IMEINUM='" + list[j].ToUpper() + "'";

                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                    if (dt1.Rows.Count > 0)
                    { //tysfc db 有此SN   ---需比較料號
                        strpart = dt1.Rows[0]["PPART"].ToString().ToUpper();
                        strorder = dt1.Rows[0]["PORDER"].ToString().ToUpper();
                        if (strpart == ppart)
                        {
                            strSql = "";
                            if (strorder == porder)
                            {
                                yiImport[k] = list[j];
                                temp += yiImport[k].ToString().ToUpper() + ",";
                                k++;
                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該sn已經導入,請重新導入文件！！');</script>");
                                //return;
                            }
                            else
                            { //該功能相當于: 轉工單
                                strSql = "update SHP.CMCS_SFC_IMEINUM set SHIFT_PORDER='" + strorder + "',SHIFT_USER='SFCIT',PORDER ='" + porder + "' where serial_num='" + list[j].ToUpper() + "' or imeinum='" + list[j].ToUpper() + "'";
                                ClsGlobal.objDataConnect.DataExecute(strSql);
                            }
                        }
                        else
                        {
                            notemp += list[j] + ",";
                            lbalter1.Items.Add(list[j]);
                        }

                        //{
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該sn不能導入,請核對料號是否一致！！');</script>");
                        //    return;
                        //}
                    }
                    else
                    {
                        strSql = "SELECT * from SHP.CMCS_SFC_IMEINUM@MCMLH where SERIAL_NUM='" + list[j].ToUpper() + "'  or imeinum='" + list[j].ToUpper() + "'";
                        DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            strpart = dt2.Rows[0]["PPART"].ToString().ToUpper();
                            strorder = dt2.Rows[0]["PORDER"].ToString().ToUpper();
                            if (strpart == ppart)
                            {
                                strSql = "";
                                strSql = "INSERT INTO SHP.CMCS_SFC_IMEINUM  (SELECT * FROM  SHP.CMCS_SFC_IMEINUM@MCMLH WHERE SERIAL_NUM='" + list[j].ToUpper() + "' OR IMEINUM='" + list[j].ToUpper() + "')";
                                ClsGlobal.objDataConnect.DataExecute(strSql);
                                strSql = "";
                                strSql = "update SHP.CMCS_SFC_IMEINUM set SHIFT_PORDER='" + strorder + "',SHIFT_USER='SFCIT',PORDER ='" + porder + "' where serial_num='" + list[j].ToUpper() + "'  or imeinum='" + list[j].ToUpper() + "'";
                                ClsGlobal.objDataConnect.DataExecute(strSql);
                            }
                            else
                            {
                                notemp += list[j];
                                lbalter1.Items.Add(list[j]);
                            }
                            //{

                            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該sn不能導入,請核對料號是否一致！！');</script>");
                            //    return;
                            //}
                        }
                        else
                        {
                            notemp += list[j];
                            lbalter1.Items.Add(list[j]);
                        }
                        //{
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該sn不存在,請檢查你的輸入是否有誤！！');</script>");
                        //    return;
                        //}
                    }
                }

            }

            ds.Dispose();
            //Response.Write(" <script> alert( '數據上傳成功！') </script> ");
            //if (temp.Equals(""))
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SN導入成功');</script>");
            //    return;
            //}
            //else
            //    if (notemp.Equals(""))
            //    {
            //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該文件一共有" + count + "個SN,其中SN:" + temp + "已經導入,請導入其他文件!!!');</script>");
            //        lbalert.Visible = true;
            //        tbalert.Visible = true;
            //        lbalert.Text = "該文件一共有" + count + "個SN,其中如下已經導入,請導入其他文件!!!";
            //        tbalert.Text = temp.Substring(0, (temp.Length - 1));
            //        return;
            //    }
            //    else
            //    {
            //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該文件一共有" + count + "個SN,其中SN:" + notemp + "不能成功導入,請檢查你的輸入或核對料號是否一致!!!');</script>");
            //        lbalert.Visible = true;
            //        tbalert.Visible = true;
            //        lbalert.Text ="該文件一共有" + count + "個SN,其中如下SN不能成功導入,請檢查你的輸入或核對料號是否一致!!!";
            //        tbalert.Text = notemp.Substring(0, (notemp.Length - 1));
            //        return;
            //    }


            if (notemp.Equals(""))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SN導入成功,請導入其他文件!!!');</script>");
                lbalert.Visible = false;
                lbalter1.Visible = false;
                return;
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('該文件一共有" + count + "個SN,其中SN:" + notemp + "不能成功導入,請檢查你的輸入或核對料號是否一致!!!');</script>");
                lbalert.Visible = true;
                lbalter1.Visible = true;
                lbalert.Text = "以下SN不能成功導入,請檢查你的輸入或核對料號!!!";
                return;
            }
        }
    }

    public DataSet GetDataSetFromExcel(string filePath, string fileName)
    {
        //string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
        //strConn += filePath;
        //strConn += ";Extensions=asc,csv,tab,txt;";

        string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties=Excel 8.0;";
        //string path = @"c:\book1.xls";
        //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;";

        OleDbConnection objConn = new OleDbConnection(strConn);
        //OdbcConnection objConn = new OdbcConnection(strConn);
        try
        {
            objConn.Open();

            DataSet dsExcel = new DataSet();
            try
            {
                string strSql = "select * from [Sheet1$] ";
                OleDbDataAdapter oledbDataAdapter = new OleDbDataAdapter(strSql, objConn);
                oledbDataAdapter.Fill(dsExcel);
            }
            catch
            {


            }
            return dsExcel;
        }
        catch
        {
            return null;
        }
    }
}
