﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFC.TJWEB;

public partial class MainMSPrg_DnnReport : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string BegTime;
    private static string EndTime;
    private static string tparam;
    private static string HMDDnn;
    private static string Shipping;
    private static DataTable dt = new DataTable();
    PassDNN PassDnnFun = new PassDNN();
    string rowcount;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        #region 數據庫連接


        string tmp1 = "";
        if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

        if (tmp1 == "1")
        {
            //tparam = Session["tparam"].ToString();
            conntype = Session["Param2"].ToString();
            Conn = Session["Param3"].ToString();//93
            dbWrite = Session["Param4"].ToString();//23801
            Autoprg = Session["Param5"].ToString();
            tmpdate = Session["Param6"].ToString();
            BBSCMDIR = Session["Param7"].ToString();
        }
        else if (tmp1 == "")
        {
            conntype = "sql";
            Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
            dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
        }
        else
        {
            Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
            return;
        }
        #endregion
        if (!Page.IsPostBack)
        {
            div1.Visible = false;
            div2.Visible = false;
            div3.Visible = false;

            BegTime = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            EndTime = DateTime.Now.ToString("yyyyMMdd");
            textFrom.Text = BegTime;
            textEnd.Text = EndTime;
            HMDDnn = textDnn.Text;
            Shipping = textShipping.Text;


            ShowData(BegTime, EndTime, HMDDnn, Shipping);
        }
        //20170215T140244.000Z   [ReceiveTime]

    }


    public void ShowData(string strbeingdate, string strenddate, string HMDDnn, string Shipping)
    {

        string StartHeaderSql = "Select * FROM [IMSCM].[dbo].[DNN_MT]  where 1=1";
        if (strbeingdate != "")
        {
            StartHeaderSql += " and SUBSTRING(receiveTime,1,8) >= '" + strbeingdate + "' ";
        }
        if (strenddate != "") 
        {
            StartHeaderSql += " and SUBSTRING(receiveTime,1,8) <= '" + strenddate + "'";
        }
        if (HMDDnn != "") 
        {
            StartHeaderSql += " and DNID = '" + HMDDnn + "'";
        }
        if (Shipping != "") 
        {
            StartHeaderSql += " and DNID = '" + HMDDnn + "'";
        }

        DataTable StartHeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, StartHeaderSql);

        Session["SQL104"] = StartHeaderSqldt;
        //lblTotal.Text = "";
        //lblTotal.Text = "Total:&nbsp;<span style='font-weight:bold;'>" + dt.Rows.Count.ToString() + "</span>&nbsp;Rows";  




        div1.Visible = false;
        div2.Visible = false;
        div3.Visible = false;

        Label2.Visible = false;
        lblDetail.Visible = false;
        Label1.Visible = false;
        GridView1.Visible = true;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
    }

    //reset
    protected void Button2_Click(object sender, EventArgs e)
    {
        textFrom.Text = "";
        textEnd.Text = "";
        textDnn.Text = "";
        textShipping.Text = "";
        //Label0.Visible = false;
        div1.Visible = false;
        div2.Visible = false;
        div3.Visible = false;

        GridView1.Visible = false;
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView4.Visible = false;

    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {

        ShowData(textFrom.Text, textEnd.Text, textDnn.Text, textShipping.Text);

    }

    protected void lbtDnn_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string strDnn = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        GridView2Bind(strDnn,"link");
    }

    protected void Modify_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string strDnn = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        GridView2Bind(strDnn,"Modify");

    }

    protected void Save_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string strDnn =GridView4.Rows[row].Cells[1].Text;
        string strDnnid = GridView4.Rows[row].Cells[2].Text;
        string strpoid = ((TextBox)GridView4.Rows[row].FindControl("textpoid")).Text.ToString();
        string strpoitem = ((TextBox)GridView4.Rows[row].FindControl("textpoitem")).Text.ToString();

        string status = UpdateDnnPOlink(strDnn, strDnnid, strpoid, strpoitem,row);

        // textbox1.Enable = true
        //((TextBox)GridView4.Rows[row].FindControl("textpoid")).ReadOnly = false;
        
        TextBox TextBox2 = ((TextBox)GridView4.Rows[row].FindControl("textpoid"));
        TextBox2.Enabled = false;

        TextBox TextBox3 = ((TextBox)GridView4.Rows[row].FindControl("textpoitem"));
        TextBox3.Enabled = false;

        if (status == "suss") 
        {
            Label2.Visible = true;
            Label2.Text = "Successfully saved";
        }
        if (status == "notexist")
        {
            Label2.Visible = true;
            Label2.Text = "This po = " + strpoid + "' poitem = " + strpoitem + "' are not exist.";
        }
        if (status == "notsuss")
        {
            Label2.Visible = true;
            Label2.Text = "Error";
        }

        // GridView2Bind(strDnn);
    }

    public string UpdateDnnPOlink(string dnn, string dnnid, string poid, string poitem,int row) 
    {
         string statusss = "";

         string checkposql = "select  * from [IMSCM].[dbo].[PO_CREATE_DT] where poid = '" + poid + "' and ITEMID = '" + poitem + "'";
        DataTable checkposqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, checkposql);

        if (checkposqldt.Rows.Count > 0)
        {
            
            string updatednnpo = "update [IMSCM].[dbo].[DNNITEM]  set poid = '" + poid + "' , POItemID = '" + poitem + "' where dnid = '" + dnn + "' and itemid = '" + dnnid + "'";
            int a1 = PDataBaseOperation.PExecSQL(conntype, Conn, updatednnpo);
            if (a1 > 0)
            {
                statusss = "suss";
            }
            else 
            {
                statusss = "notsuss";
            }
            //Response.Write("<script>alert('This po = " + poid + "' poid = "+poitem+"' are exist.');</script>");
            //Response.Write("<script>alert('Create Wo Success! WO = " + SapWo + "');</script>");
        }
        else 
        {
            statusss = "notexist";
           
        }

        return statusss ;

    }

    public void Confirm_Click(object sender, EventArgs e) 
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string strDnn = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        string checkPosql = "select Count(*) count1  from [IMSCM].[dbo].[DNNITEM] where dnid = '" + strDnn + "' and( (poid is null or poid = '') or (poitemid  is null or poitemid = ''))";
        DataTable checkPosqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, checkPosql);
        int strponullcount = Convert.ToInt32(checkPosqldt.Rows[0]["count1"].ToString());

        if (strponullcount == 0)
        {
            PassDnnFun.PassDNN1(conntype, Conn, "TEST", strDnn);

        } 
        else
        {
            Label2.Visible = true;
            Label2.Text = "Please enter the po and poitem";
        }
        //string connType, string connStr, string SapFlag,string DnnID

    }

    public void GridView2Bind(string strDnn,string type)
    {
        string dnitemsql = "SELECT *  FROM [IMSCM].[dbo].[DNNITEM] where DNID = '" + strDnn + "'";
        DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, dnitemsql);

        string SoldtoSql = "SELECT * FROM [IMSCM].[dbo].[DNNSoldToAddress] where DNID ='" + strDnn + "'";
        DataTable SoldtoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, SoldtoSql);

        string ShiptoSql = "SELECT * FROM [IMSCM].[dbo].[DNNShipToAddress]where DNID ='" + strDnn + "'";
        DataTable ShiptoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, ShiptoSql);

       //HeaderSqldt.Rows[0]["poid"] = "123456";
       //HeaderSqldt.Rows[0]["poitemid"] = "123456";

        div1.Visible = true;
        div2.Visible = true;
        div3.Visible = true;
        Label2.Visible = false;

        Session["dt_DT"] = SoldtoSqldt;
        GridView2.Visible = true;
        lblDetail.Visible = false;
        GridView2.DataSource = Session["dt_DT"];
        GridView2.DataBind();

        Session["dt_DT1"] = ShiptoSqldt;
        GridView3.Visible = true;
        lblDetail.Visible = false;
        GridView3.DataSource = Session["dt_DT1"];
        GridView3.DataBind();

        Session["dt_DT2"] = HeaderSqldt;
        GridView4.Visible = true;
        lblDetail.Visible = false;
        GridView4.DataSource = Session["dt_DT2"];
        GridView4.DataBind();

        if (type == "link")
        {
            for (int i = 0; i < HeaderSqldt.Rows.Count; i++) 
            {
                TextBox TextBox2 = ((TextBox)GridView4.Rows[i].FindControl("textpoid"));
                TextBox2.Enabled = false;

                TextBox TextBox3 = ((TextBox)GridView4.Rows[i].FindControl("textpoitem"));
                TextBox3.Enabled = false;

            }
        }
        if (type == "Modify") 
        {
            for (int i = 0; i < HeaderSqldt.Rows.Count; i++)
            {
                TextBox TextBox2 = ((TextBox)GridView4.Rows[i].FindControl("textpoid"));
                TextBox2.Enabled = true;

                TextBox TextBox3 = ((TextBox)GridView4.Rows[i].FindControl("textpoitem"));
                TextBox3.Enabled = true;

            }
        }


    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;

        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLComp"];
        GridView1.DataBind();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;

        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLComp"];
        GridView1.DataBind();
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;

        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLComp"];
        GridView1.DataBind();
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }

    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;

        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLComp"];
        GridView1.DataBind();
    }
    //protected void ButtonExcel_Click(object sender, EventArgs e)
    //{

    //    string dtime = DateTime.Now.ToString("MMddhhmmss");
    //    this.Export_OutStockInfo("application/ms-excel", "" + dtime + "OutputInfo.xls");

    //}
    //public void Export_OutStockInfo(string FileType, string FileName)
    //{

    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
    //    Response.ContentType = FileType;
    //    this.EnableViewState = false;

    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    GridView4.AllowPaging = false;
    //    GridView4.DataSource = Session["dt_DT2"];
    //    GridView4.DataBind();
    //    GridView4.RenderControl(hw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    //}
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //base.VerifyRenderingInServerForm(control);
    //}

}