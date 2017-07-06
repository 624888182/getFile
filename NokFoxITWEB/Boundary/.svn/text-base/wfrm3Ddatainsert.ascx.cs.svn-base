using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.OracleClient;
using DBAccess.EAI;

public partial class Boundary_wfrmdatainsert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Insert();
    }

    public DataSet GetDataSetFromCSV(string filePath, string fileName)
    {
        string strConn = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=";
        strConn += filePath;                                                       
        strConn += ";Extensions=asc,csv,tab,txt;";
        OdbcConnection objConn = new OdbcConnection(strConn);
        DataSet dsCSV = new DataSet();
        try
        {
            string strSql = "select * from " + fileName;                    
            OdbcDataAdapter odbcCSVDataAdapter = new OdbcDataAdapter(strSql, objConn);
            odbcCSVDataAdapter.Fill(dsCSV);            
        }
        catch
        {
            
            
        }
        return dsCSV;
    }

    public string DateTimeFormat(string str)
    {
        const int time = 12;
        string dateTime;
        string noon;
        int hour;

        if (str.Substring(4, 1).CompareTo("0") == 0)
        {
            hour = int.Parse(str.Substring(5, 1));
        }
        else
        {
            hour = int.Parse(str.Substring(4, 2));
        }
        noon = str.Substring(1,2);
        if (noon.CompareTo("下午") == 0)
        {
            hour = hour + time;
        }

        dateTime = hour.ToString() + str.Substring(6);
        return dateTime;
    }

    private void Insert()
    {
        string filePath;
        string fileName;
        string line;
        string strSql;
        DataSet ds;

        ds = null;
        fileName = FileUpload.FileName.ToString();
        filePath = FileUpload.PostedFile.FileName.ToString();
        filePath = filePath.Substring(0,filePath.Length - fileName.Length);
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('您没有选择要的文件！！');</script>");
            return;
        }
       
        line = fileName.Substring(4, 2).ToUpper();
        ds = GetDataSetFromCSV(filePath,fileName);
        if (ds.Tables.Count == 0)
        {
            //Response.Write("<script>  alert('請檢查正在上傳的文件是否已經打開，若是，請先關閉它再上傳！')</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請檢查正在上傳的文件是否已經打開，若是，請先關閉它再上傳！');</script>");
            return;
        }
        for (int i = 5; i < ds.Tables[0].Rows.Count;i++)
        {
            string temp;
            
            string[] list = new string[ds.Tables[0].Columns.Count];
            
            for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
            {
                list[j] = ds.Tables[0].Rows[i][j].ToString();
            }
            //temp = list[0].Substring(0,9) + list[1].Substring(10);
           
           
            temp = list[1].Substring(0, 10) + DateTimeFormat(list[2].Substring(10));


            strSql = "INSERT INTO PANL_PRINT_ANALY (DATETIME,LINE,BOARD,LOCATION,FEATURE,HEIGHTRESULT,HEIGHT,HEIGHTUPFAIL,HEIGHTLOWFAIL,HEIGHTTARGET,AREARESULT,AREA,AREAUPFAIL,AREALOWFAIL,AREATARGET,VOLUMERESULT,VOLUME,VOLUMEUPFAIL,VOLUMELOWFAIL,VOLUMETARGET,VALID,REGRESULT,XOFFSET,YOFFSET,REGSHORT,REGLONG,REGSHORTFAIL,REGLONGFAIL,BRIDGERESULT,BRIDGELENGTH,BRIDGEFAIL,PANELID)"
                     + "VALUES (to_date('" + temp + "','YYYY/MM/DD HH24:MI:SS'),'" + line + "','" + list[4] + "','" + list[5].ToUpper() + "','" + list[6].ToUpper() + "','" + list[7] + "','" + list[8] + "','" + list[9] + "','" + list[10] + "','" + list[11] + "','" + list[12] + "','" + list[13] + "','" + list[14] + "','" + list[15] + "','" + list[16] + "','" + list[17] + "','" + list[18] + "','" + list[19] + "','"
                     + list[20] + "','" + list[21] + "','" + list[22] + "','" + list[23] + "','" + list[24] + "','" + list[25] + "','" + list[26] + "','" + list[27] + "','" + list[28] + "','" + list[29] + "','" + list[30] + "','" + list[31] + "','" + list[32] + "','" + list[3] + "')";
            ClsGlobal.objDataConnect.DataExecute(strSql);


        }
        ds.Dispose();
        //Response.Write(" <script> alert( '數據上傳成功！') </script> ");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據上傳成功！');</script>");
    }

}
