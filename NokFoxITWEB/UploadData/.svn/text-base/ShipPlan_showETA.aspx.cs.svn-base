using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Economy.BLL;
using Economy.Publibrary;
using System.Data.SqlClient;
 
 
//using System.Xml.Linq;
using DataReader = System.Data.SqlClient.SqlDataReader;
 
 



public partial class UploadData_ShipPlan_showETA : System.Web.UI.Page
{
   DbAccessing db1 =new DbAccessing() ;
    protected void Page_Load(object sender, EventArgs e)
    {  if (!IsPostBack ){
        
        string sql_cocumentod ="select distinct DocumentID from MemoryETDToETA order by DocumentID desc";
        
       DataTable dt1=  db1.ExecuteSqlTable (sql_cocumentod) ;

        for (int i=1 ; i<= dt1.Rows.Count ;i++ )
        {
            DropDownList2.Items.Add(dt1.Rows[i-1]["DocumentID"].ToString());

        }  
        string CustomerSite = "select distinct rtrim( CustomerSite)  CustomerSite  from MemoryETDToETA     order by CustomerSite  ";

        DataTable dt2 = db1.ExecuteSqlTable(CustomerSite);

        for (int i = 1; i <= dt2.Rows.Count; i++)
        {
            DropDownList3.Items.Add(dt2.Rows[i - 1]["CustomerSite"].ToString());
        }
        //showview();
    }         
    }  
    public void showview()
    {   string sql1 = "SELECT   AccountNum ,[ReleaseDate],[DocumentID],[CustomerSite],[FoxconnSite],[CustomerPN],[LeadTime],[SPDate_8Bytes],[ETD_SPQty],[Org_Spilt_SPQty],";
        sql1 = sql1 + "  [Leadtime_SPQty],[Leadtime_GIT_SPQty],[TodayGIT_SPQty],[SumSPQty],[SumC3],[EVWeekOfDay],[SWWeekDay],[SPweek]    FROM  [FIH_NOKIA_SYNCRO_DV].[dbo].[MemoryETDToETA]  where 1=1  ";
        if (DropDownList2.SelectedItem.Text  != string.Empty     ) {sql1 = sql1 + " and   DocumentID = '" + DropDownList2.SelectedItem.Text + "'   ";}
        if (DropDownList3.SelectedItem.Text != string.Empty) { sql1 = sql1 + " and CustomerSite = '" + DropDownList3.SelectedItem.Text + "'   "; }
        if (TextBox1.Text != string.Empty) { sql1 = sql1 + " and  CustomerPN = '" + TextBox1.Text + "'   "; }
        sql1 = sql1 +  " order by [DocumentID], [CustomerSite], [FoxconnSite], [CustomerPN],[SPDate_8Bytes] ";
        string strProvider = ConfigurationManager.AppSettings["DefaultConnectionString"];       
        SqlConnection myConn = new SqlConnection(strProvider);
        myConn.Open();
        SqlDataAdapter myAdapter = new SqlDataAdapter(sql1, myConn);
        DataSet   ds = new DataSet();
        myAdapter.Fill(ds);
        LinkButton1.Text = "查詢結果總筆數:"+ds.Tables[0].Rows.Count.ToString (); 
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        myConn.Close();
        ds.Dispose();
        myAdapter.Dispose();
 
    }
 
     


    public void ControlToExcel(System.Web.UI.Control ctl)
    {
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //一定要重载此方法。否则会报错
    }


    protected void DownloadToClient(string File)
    {
        HttpResponse httpResponse = HttpContext.Current.Response;
        httpResponse.Clear();
        httpResponse.WriteFile(File);
        string httpHeader = "attachment;filename=Tmp.xls";
        httpResponse.AppendHeader("Content-Disposition", httpHeader);
        httpResponse.Flush();
        httpResponse.End();
    }



    public void DataToExcel(DataTable dt, string FileMode)
    {
        try
        {
            string randCode = DateTime.Now.ToString("yyyyMMddhhmmss");
            string PathTemplate = Server.MapPath("Template/");
            string PathTemp = Server.MapPath("Temp/");
            System.IO.FileInfo mode = new System.IO.FileInfo(PathTemplate + FileMode);
            mode.CopyTo(PathTemp + randCode + FileMode, true);
            object missing = System.Reflection.Missing.Value;
            Excel.Application myExcel = new Excel.Application();
            myExcel.Application.Workbooks.Open(PathTemp + randCode + FileMode, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            myExcel.Visible =false ;     

            for (int i = 1; i < dt.Rows.Count+1 ; i++)
            {
                myExcel.Cells[i + 1, 1] = dt.Rows[i-1][0].ToString();
                myExcel.Cells[i + 1, 2] = dt.Rows[i-1][1].ToString();
                myExcel.Cells[i + 1, 3] = dt.Rows[i-1][2].ToString();
                myExcel.Cells[i + 1, 4] = dt.Rows[i-1][3].ToString();
                myExcel.Cells[i + 1, 5] = dt.Rows[i-1][4].ToString();
                myExcel.Cells[i + 1, 6] = dt.Rows[i-1][5].ToString();
                myExcel.Cells[i + 1, 7] = dt.Rows[i-1][6].ToString();
                myExcel.Cells[i + 1, 8] = dt.Rows[i-1][7].ToString();
                myExcel.Cells[i + 1, 9] = dt.Rows[i-1][8].ToString();
                myExcel.Cells[i + 1, 10] = dt.Rows[i-1][9].ToString();
                myExcel.Cells[i + 1, 11] = dt.Rows[i-1][10].ToString();
                myExcel.Cells[i + 1, 12] = dt.Rows[i-1][11].ToString();
                myExcel.Cells[i + 1, 13] = dt.Rows[i-1][12].ToString();
                myExcel.Cells[i + 1, 14] = dt.Rows[i-1][13].ToString();
                myExcel.Cells[i + 1, 15] = dt.Rows[i-1][14].ToString();
                myExcel.Cells[i + 1, 16] = dt.Rows[i-1][15].ToString();
                myExcel.Cells[i + 1, 17] = dt.Rows[i-1][16].ToString();
                myExcel.Cells[i + 1, 18] = dt.Rows[i-1][17].ToString();                
            }
            
            //将列标题和实际内容选中 
            Excel.Workbook myBook = myExcel.Workbooks[1];
            Excel.Worksheet mySheet = (Excel.Worksheet)myBook.Worksheets[1];
            myBook.Save();
            myExcel.Quit();
            GC.Collect();
            DownloadToClient(PathTemp + randCode + FileMode);
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            //RESPONSE. tmp
            Response.Write(tmp);
        }
        finally
        {

        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {

       
    }
 
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
foreach (GridViewRow gvr in GridView1.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                gvr.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FBE8C4';");
                gvr.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
            }
        }
    }
    protected void Button3_Command(object sender, CommandEventArgs e)
    {
        ControlToExcel(GridView1);
    }


    public void ToExcel()
    {//System.Web.UI.Control ctl
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("Excel.xls"));
        // Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        // Response.AddHeader("Content-Length", file.Length.ToString()); 
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
        GridView1.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        GridView1.RenderControl(hw);
  
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }


    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Command(object sender, CommandEventArgs e)
    {
        showview();

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {


        DropDownList3.Items.Clear();

        DropDownList3.Items.Add("");
        string CustomerSite = "select distinct rtrim( CustomerSite)  CustomerSite  from MemoryETDToETA where DocumentID   ='" + DropDownList2.SelectedItem.Text   + "'      order by CustomerSite  ";

        DataTable dt2 = db1.ExecuteSqlTable(CustomerSite);

        for (int i = 1; i <= dt2.Rows.Count; i++)
        {
            DropDownList3.Items.Add(dt2.Rows[i - 1]["CustomerSite"].ToString());

        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1.Items.Clear();

        DropDownList1.Items.Add("");
        string CustomerSite = "select distinct rtrim( FoxconnSite)  FoxconnSite  from MemoryETDToETA where DocumentID   ='" + DropDownList2.SelectedItem.Text + "'and CustomerSite  ='" + DropDownList3.SelectedItem.Text + "'  order by FoxconnSite  ";

        DataTable dt2 = db1.ExecuteSqlTable(CustomerSite);

        for (int i = 1; i <= dt2.Rows.Count; i++)
        {
            DropDownList1.Items.Add(dt2.Rows[i - 1]["FoxconnSite"].ToString());

        }
    }




    protected void Button5_Click(object sender, EventArgs e)
    {
        Panel1.Visible = !Panel1.Visible;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        ListBox1.Items.Clear();

        string CustomerSite = "select distinct rtrim( CustomerPN)  CustomerPN  from MemoryETDToETA where DocumentID   ='" + DropDownList2.SelectedItem.Text + "'and CustomerSite  ='" + DropDownList3.SelectedItem.Text + "' and foxconnsite  ='" + DropDownList1.SelectedItem.Text + "'  order by CustomerPN  ";

        DataTable dt2 = db1.ExecuteSqlTable(CustomerSite);

        for (int i = 1; i <= dt2.Rows.Count; i++)
        {
            ListBox1.Items.Add(dt2.Rows[i - 1]["CustomerPN"].ToString());

        }




    }
     
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    
    //}
    protected void ListBox1_SelectedIndexChanged1(object sender, EventArgs e)
    {
 TextBox1.Text = ListBox1.SelectedItem.Text;
        Panel1.Visible = false ;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sql1 = "SELECT   AccountNum ,[ReleaseDate],[DocumentID],[CustomerSite],[FoxconnSite],[CustomerPN],[LeadTime],[SPDate_8Bytes],[ETD_SPQty],[Org_Spilt_SPQty],";
        sql1 = sql1 + "  [Leadtime_SPQty],[Leadtime_GIT_SPQty],[TodayGIT_SPQty],[SumSPQty],[SumC3],[EVWeekOfDay],[SWWeekDay],[SPweek]    FROM  [FIH_NOKIA_SYNCRO_DV].[dbo].[MemoryETDToETA]  where 1=1  ";
        if (DropDownList2.SelectedItem.Text != string.Empty) { sql1 = sql1 + " and   DocumentID = '" + DropDownList2.SelectedItem.Text + "'   "; }
        if (DropDownList3.SelectedItem.Text != string.Empty) { sql1 = sql1 + " and CustomerSite = '" + DropDownList3.SelectedItem.Text + "'   "; }
        if (TextBox1.Text != string.Empty) { sql1 = sql1 + " and  CustomerPN = '" + TextBox1.Text + "'   "; }
        sql1 = sql1 + " order by [DocumentID], [CustomerSite], [FoxconnSite], [CustomerPN],[SPDate_8Bytes] ";
        string strProvider = ConfigurationManager.AppSettings["DefaultConnectionString"];
        SqlConnection myConn = new SqlConnection(strProvider);
        myConn.Open();
        SqlDataAdapter myAdapter = new SqlDataAdapter(sql1, myConn);
        DataSet ds = new DataSet();
        myAdapter.Fill(ds); 
        DataTable table = ds.Tables[0];
        DataToExcel(table, "DeptTemplate.xls");

        myConn.Close();
        ds.Dispose();
        myAdapter.Dispose();
    }
}
