using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SFC.TJWEB;

public partial class MainNokFoxPrg_PoQuerySo : System.Web.UI.Page
{
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string dbName;
    private static string BegTime;
    private static string EndTime;
    private static string tparam;
    private static string HMDPO;
    private static string LoactionCode;
    private static string tmp1;
    //private static DataTable dt = new DataTable();
    PassDNN PassDnnFun = new PassDNN();
    N_CreateSO createso = new N_CreateSO();
    string rowcount;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 數據庫連接


        tmp1 = Session["Param5"].ToString();
        //if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

        if (tmp1 == "TEST")
        {
            //tparam = Session["tparam"].ToString();
            conntype = Session["Param2"].ToString(); //sql
            Conn = Session["Param3"].ToString();//93
            dbWrite = Session["Param4"].ToString();//23801
            //Autoprg = Session["Param5"].ToString();
            tmpdate = Session["Param6"].ToString();
            dbName = Session["Param7"].ToString();
            //tmp1 = Session["Param5"].ToString();
        }
        else if (tmp1 == "")
        {
            conntype = "sql";
            Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCMWS";//205
            dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCMWS";//205
        }
        else
        {
            Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
            return;
        }
        #endregion

        BegTime = textFrom.Text ;
        EndTime = textEnd.Text ;

        if (!Page.IsPostBack)
        {
            BegTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            textFrom.Text = BegTime;
            textEnd.Text = EndTime;
            HMDPO = textpo.Text;
            LoactionCode = textCode.Text;

            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            sostatus.Visible = false;

            ShowData(BegTime, EndTime, HMDPO, LoactionCode);
        }

    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowData(BegTime, EndTime, HMDPO, LoactionCode);
        
    }

    protected void lbtPO_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string strpo = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        //strpo  = "0022501014";
        GridView2Bind(strpo, "link");
    }

    public void GridView2Bind(string strPO, string type)
    {
        string strid = "";
        string poitemsql = "SELECT *  FROM " + dbName + ".[dbo].[PO_CREATE_DT] where POID = '" + strPO + "' order by itemid";
        DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, poitemsql);
        if (HeaderSqldt.Rows.Count > 0)
        {
            strid = HeaderSqldt.Rows[0]["ID"].ToString();
        }

        string SoldtoSql = "SELECT *  FROM " + dbName + ".[dbo].[POShipToAddress] where ID ='" + strid + "'";
        DataTable SoldtoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, SoldtoSql);

        string ShiptoSql = "SELECT *  FROM " + dbName + ".[dbo].[POSoldToAddress] WHERE ID  ='" + strid + "'";
        DataTable ShiptoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, ShiptoSql);

        //HeaderSqldt.Rows[0]["poid"] = "123456";
        //HeaderSqldt.Rows[0]["poitemid"] = "123456";

        //div1.Visible = true;
        sostatus.Visible = false;
        div2.Visible = true;
        div3.Visible = true;
        div4.Visible = true;
        //Label2.Visible = false;

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
        //lblDetail.Visible = false;
        GridView4.DataSource = Session["dt_DT2"];
        GridView4.DataBind();

        //if (type == "link")
        //{
        //    for (int i = 0; i < HeaderSqldt.Rows.Count; i++)
        //    {
        //        TextBox TextBox2 = ((TextBox)GridView4.Rows[i].FindControl("textpoid"));
        //        TextBox2.Enabled = false;

        //        TextBox TextBox3 = ((TextBox)GridView4.Rows[i].FindControl("textpoitem"));
        //        TextBox3.Enabled = false;

        //    }
        //}
        //if (type == "Modify")
        //{
        //    for (int i = 0; i < HeaderSqldt.Rows.Count; i++)
        //    {
        //        TextBox TextBox2 = ((TextBox)GridView4.Rows[i].FindControl("textpoid"));
        //        TextBox2.Enabled = true;

        //        TextBox TextBox3 = ((TextBox)GridView4.Rows[i].FindControl("textpoitem"));
        //        TextBox3.Enabled = true;

        //    }
        //}


    }

    //protected void Save_Click(object sender, EventArgs e)
    //{
    //    //int row = Convert.ToInt32(((Button)sender).CommandArgument);
    //    //row = row % 7;
    //    //string strDnn = GridView4.Rows[row].Cells[1].Text;
    //    //string strDnnid = GridView4.Rows[row].Cells[2].Text;
    //    //string strpoid = ((TextBox)GridView4.Rows[row].FindControl("textpoid")).Text.ToString();
    //    //string strpoitem = ((TextBox)GridView4.Rows[row].FindControl("textpoitem")).Text.ToString();

    //    //string status = UpdateDnnPOlink(strDnn, strDnnid, strpoid, strpoitem, row);

    //    //// textbox1.Enable = true
    //    ////((TextBox)GridView4.Rows[row].FindControl("textpoid")).ReadOnly = false;

    //    //TextBox TextBox2 = ((TextBox)GridView4.Rows[row].FindControl("textpoid"));
    //    //TextBox2.Enabled = false;

    //    //TextBox TextBox3 = ((TextBox)GridView4.Rows[row].FindControl("textpoitem"));
    //    //TextBox3.Enabled = false;

    //    //if (status == "suss")
    //    //{
    //    //    Label2.Visible = true;
    //    //    Label2.Text = "Successfully saved";
    //    //}
    //    //if (status == "notexist")
    //    //{
    //    //    Label2.Visible = true;
    //    //    Label2.Text = "This po = " + strpoid + "' poitem = " + strpoitem + "' are not exist.";
    //    //}
    //    //if (status == "notsuss")
    //    //{
    //    //    Label2.Visible = true;
    //    //    Label2.Text = "Error";
    //    //}

    //    // GridView2Bind(strDnn);
    //}

    //protected void Modify_Click(object sender, EventArgs e)
    //{
    //    int row = Convert.ToInt32(((Button)sender).CommandArgument);
    //    row = row % 7;
    //    string strDnn = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
    //    //GridView2Bind(strDnn, "Modify");

    //}

    public void ShowData(string strbeingdate, string strenddate, string HMDPO, string LoactionCode)
    {
        string f6, f7;

        strbeingdate = strbeingdate.Substring(0, 4) + strbeingdate.Substring(5, 2) + strbeingdate.Substring(8, 2);
        strenddate = strenddate.Substring(0, 4) + strenddate.Substring(5, 2) + strenddate.Substring(8, 2);

        string StartHeaderSql = "SELECT *  FROM " + dbName + ".[dbo].[PO_CREATE_MT]  where 1=1";
        if (strbeingdate != "")
        {
            //StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) >= '" + strbeingdate + "' ";
            StartHeaderSql += " and SUBSTRING(DOCUMENTID,1,8) >= '" + strbeingdate + "' ";
        }
        if (strenddate != "")
        {
           // StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) <= '" + strenddate + "'";
            StartHeaderSql += " and SUBSTRING(DOCUMENTID,1,8) <= '" + strenddate + "'";
        }
        if (HMDPO != "")
        {
            StartHeaderSql += " and POID = '" + HMDPO + "'";
        }
        if (LoactionCode != "")
        {
            StartHeaderSql += " and POID = '" + HMDPO + "'";
        }

        StartHeaderSql += "order by poid ";
        DataTable StartHeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, StartHeaderSql);


        Session["SQL104"] = StartHeaderSqldt;
        GridView1.Visible = true;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();

        //CONFIRM ENABLE
        enabledbutton();

    }

    public void enabledbutton() 
    {
        DataTable headertable = (DataTable)Session["SQL104"];

        string f6, f7;

        if (headertable.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string po = ((LinkButton)GridView1.Rows[i].FindControl("Label3")).Text.ToString();

                string poitemsql = "SELECT *  FROM " + dbName + ".[dbo].[PO_CREATE_DT] where POID = '" + po + "'";
                DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, poitemsql);

                f6 = HeaderSqldt.Rows[0]["F6"].ToString();
                f7 = HeaderSqldt.Rows[0]["F7"].ToString();

                if (f6 == "2" && f7 == "1")
                {
                    for (int j = 0; j < GridView1.Rows.Count; j++)
                    {
                        if (((LinkButton)GridView1.Rows[j].FindControl("Label3")).Text.ToString() == po)
                        {
                            Button aaa = ((Button)GridView1.Rows[j].FindControl("btnConfirm"));
                            aaa.Enabled = true;
                            aaa.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC00");

                        }
                    }
                }
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //textFrom.Text = "";
        //textEnd.Text = "";
        //textDnn.Text = "";
        //textShipping.Text = "";
        ////Label0.Visible = false;
        //div1.Visible = false;
        //div2.Visible = false;
        //div3.Visible = false;

        //GridView1.Visible = false;
        //GridView2.Visible = false;
        //GridView3.Visible = false;
        //GridView4.Visible = false;

    }
    public void Confirm_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string strPO = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();

        string status = createso.WebCreateSO(conntype, Conn, tmp1, strPO, dbName);

        if (status == "Y")
        {

            sostatus.Visible = true;
            sostatus.Text = "SO Create Sussess";
        }
        else 
        {
            sostatus.Visible = true;
            sostatus.Text = "Error：SAP log have error message";
        }

        //string checkPosql = "select Count(*) count1  from [IMSCM].[dbo].[DNNITEM] where dnid = '" + strDnn + "' and( (poid is null or poid = '') or (poitemid  is null or poitemid = ''))";
        //DataTable checkPosqldt = PDataBaseOperation.PSelectSQLDT(conntype, Conn, checkPosql);
        //int strponullcount = Convert.ToInt32(checkPosqldt.Rows[0]["count1"].ToString());

        //if (strponullcount == 0)
        //{
        //    PassDnnFun.PassDNN1(conntype, Conn, "TEST", strDnn);

        //}
        //else
        //{
        //    Label2.Visible = true;
        //    Label2.Text = "Please enter the po and poitem";
        //}
        ////string connType, string connStr, string SapFlag,string DnnID

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
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
        enabledbutton();
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
            GridViewRow pagerRow = GridView4.BottomPagerRow;
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
        newPageIndex = newPageIndex >= GridView4.PageCount ? GridView4.PageCount - 1 : newPageIndex;

        GridView4.PageIndex = newPageIndex;
        GridView4.DataSource = Session["dt_DT2"];
        GridView4.DataBind();
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
            GridViewRow pagerRow = GridView2.BottomPagerRow;
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
        newPageIndex = newPageIndex >= GridView2.PageCount ? GridView2.PageCount - 1 : newPageIndex;

        GridView2.PageIndex = newPageIndex;
        GridView2.DataSource = Session["dt_DT"];
        GridView2.DataBind();
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
            GridViewRow pagerRow = GridView3.BottomPagerRow;
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
        newPageIndex = newPageIndex >= GridView3.PageCount ? GridView3.PageCount - 1 : newPageIndex;

        GridView3.PageIndex = newPageIndex;
        GridView3.DataSource = Session["dt_DT1"];
        GridView3.DataBind();
    }
}
