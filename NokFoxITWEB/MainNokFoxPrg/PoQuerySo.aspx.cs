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
    //private static string Conn;
    //private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string dbName;
    private static string BegTime;
    private static string EndTime;
    private static string tparam;
    private static string HMDPO;
    // private static string LoactionCode;
    private static string tmp1;
    //private  string t3;
    //private static DataTable dt = new DataTable();
    PassDNN PassDnnFun = new PassDNN();
    N_CreateSO createso = new N_CreateSO();
    N_PassInfo pi = new N_PassInfo();
    string rowcount;

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region 數據庫連接


        //tmp1 = Session["Param5"].ToString();
        ////if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

        //if (tmp1 != "")
        //{
        //    //tparam = Session["tparam"].ToString();
        //    conntype = Session["Param2"].ToString(); //sql
        //    Conn = Session["Param3"].ToString();//93
        //    dbWrite = Session["Param4"].ToString();//23801
        //    //Autoprg = Session["Param5"].ToString();
        //    tmpdate = Session["Param6"].ToString();
        //    dbName = Session["Param7"].ToString();
        //    LoactionCode = Session["NokVendID"].ToString().Trim();
        //    //tmp1 = Session["Param5"].ToString();
        //    string t1 = "", t2="", t4="", t5="", t6="", t7="", t8="",t9="";
        //    t1 = Session["NokVendID"].ToString().Trim(); // = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
        //    t2 = Session["Nokcstatus"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
        //    t3 = Session["NokRunEnv"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
        //    t4 = Session["NokSalesOrg"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
        //    t5 = Session["NokSapPlant"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
        //    t6 = Session["NokB2BIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
        //    t7 = Session["NokSFCIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status
        //    t8 = Session["CURRNokB2BIP"].ToString().Trim();  // Curr B2B IP
        //    t9 = Session["CURRNokB2BDBType"].ToString().Trim(); //  DBA Type sql
        //}
        //else if (tmp1 == "")
        //{
        //    conntype = "sql";
        //    Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCMWS";//205
        //    dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCMWS";//205
        //}
        //else
        //{
        //    Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
        //    return;
        //}
        //#endregion

        //BegTime = textFrom.Text ;
        //EndTime = textEnd.Text ;

        if (!Page.IsPostBack)
        {

            #region 數據庫連接

            InitVar();

            tmp1 = Session["Param5"].ToString();
            //if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 != "")
            {
                //tparam = Session["tparam"].ToString();
                conntype = Session["Param2"].ToString(); //sql
                txtConn.Text = Session["Param3"].ToString();//93
                //dbWrite = Session["Param4"].ToString();//23801
                //Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                dbName = Session["Param7"].ToString();
                // LoactionCode = Session["NokVendID"].ToString().Trim();
                textVendID.Text = Session["NokVendID"].ToString().Trim();
                //tmp1 = Session["Param5"].ToString();
                string t1 = "", t2 = "", t4 = "", t5 = "", t6 = "", t7 = "", t8 = "", t9 = "";
                t1 = Session["NokVendID"].ToString().Trim(); // = DN1.Tables[0].Rows[0]["F6"].ToString().Trim();   // 1000253
                t2 = Session["Nokcstatus"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F10"].ToString().Trim();  // 1,4
                txtt3.Text = Session["NokRunEnv"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();  // TEST REAL
                t4 = Session["NokSalesOrg"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F14"].ToString().Trim();  // factory status
                t5 = Session["NokSapPlant"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F16"].ToString().Trim();  // factory status
                t6 = Session["NokB2BIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F18"].ToString().Trim();  // factory status
                t7 = Session["NokSFCIP"].ToString().Trim(); //  = DN1.Tables[0].Rows[0]["F20"].ToString().Trim();  // factory status
                t8 = Session["CURRNokB2BIP"].ToString().Trim();  // Curr B2B IP
                t9 = Session["CURRNokB2BDBType"].ToString().Trim(); //  DBA Type sql
            }
            else if (tmp1 == "")
            {
                conntype = "sql";
                txtConn.Text = ReadParaTxt("WebReadParam.txt", "23716");
                //dbWrite = ReadParaTxt("WebReadParam.txt", "23716");
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            BegTime = textFrom.Text;
            EndTime = textEnd.Text;

            BegTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            EndTime = DateTime.Now.ToString("yyyy-MM-dd");
            textFrom.Text = BegTime;
            textEnd.Text = EndTime;
            HMDPO = textpo.Text;
            //LoactionCode = textCode.Text;

            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            div5.Visible = false;
            sostatus.Visible = false;
            Button4.Visible = false;

            // if (LoactionCode == "1000253") Button4.Visible = true; textVendID.Text

            if (textVendID.Text == "1000253") Button4.Visible = true;
            Button4.Attributes["onclick"] = "this.disabled = true;this.value = 'please wait..';" + Page.ClientScript.GetPostBackEventReference(Button4, "");

            // ShowData(BegTime, EndTime, HMDPO, LoactionCode);
            ShowData(BegTime, EndTime, HMDPO, textVendID.Text);

            //Response.Redirect(Request.RawUrl); 
        }

    }


    protected void InitVar()
    {
        textVendID.Text = "";

    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        //textFrom.Text = BegTime;
        //textEnd.Text = EndTime;
        BegTime = textFrom.Text;
        EndTime = textEnd.Text;
        HMDPO = textpo.Text;
        //LoactionCode = textCode.Text;
        div2.Visible = false;
        div3.Visible = false;
        div4.Visible = false;
        // ShowData(BegTime, EndTime, HMDPO, LoactionCode);
        ShowData(BegTime, EndTime, HMDPO, textVendID.Text);
        
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
        DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, poitemsql);
        if (HeaderSqldt.Rows.Count > 0)
        {
            strid = HeaderSqldt.Rows[0]["POID"].ToString();
        }

        string SoldtoSql = "SELECT *  FROM " + dbName + ".[dbo].[POShipToAddress] where POID ='" + strid + "'";
        DataTable SoldtoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, SoldtoSql);

        string ShiptoSql = "SELECT *  FROM " + dbName + ".[dbo].[POSoldToAddress] WHERE POID  ='" + strid + "'";
        DataTable ShiptoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, ShiptoSql);

        string contextsql = "SELECT *  FROM " + dbName + ".[dbo].[POText] WHERE POID  ='" + strid + "' AND typecode = '10011'";
        DataTable contextsqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, contextsql);

        //HeaderSqldt.Rows[0]["poid"] = "123456";
        //HeaderSqldt.Rows[0]["poitemid"] = "123456";

        //div1.Visible = true;
        sostatus.Visible = false;
        div2.Visible = true;
        div3.Visible = true;
        div4.Visible = true;
        if (contextsqldt.Rows.Count > 0)
        {
            div5.Visible = true;

            Session["dt_DT5"] = contextsqldt;
            GridView5.Visible = true;
            //lblDetail.Visible = false;
            GridView5.DataSource = Session["dt_DT5"];
            GridView5.DataBind();
        }
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

        

    }


    public void ShowData(string strbeingdate, string strenddate, string HMDPO, string SubLoactionCode)
    {
        string f6, f7;

        if (strbeingdate != "")
        {
            strbeingdate = strbeingdate.Substring(0, 4) + strbeingdate.Substring(5, 2) + strbeingdate.Substring(8, 2);
            
        }
        if (strenddate != "")
        {
            
            strenddate = strenddate.Substring(0, 4) + strenddate.Substring(5, 2) + strenddate.Substring(8, 2);
        }

        //string StartHeaderSql = "SELECT *  FROM " + dbName + ".[dbo].[PO_CREATE_MT]  where 1=1 and PO_CREATE_DT_UF2 = '" + Session["NokVendID"].ToString().Trim() + "' ";
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
        if (SubLoactionCode != "")
        {
            StartHeaderSql += " and SellerPartyID  = '" + SubLoactionCode + "'";
        }
        if (DropDownListdn.Text == "YES") 
        {
            StartHeaderSql += "AND DNID != '' AND SOID != ''";
        }
        if (DropDownListdn.Text == "NO")
        {
            StartHeaderSql += "AND DNID IS NULL AND SOID IS NULL ";
        }
        if(sptext.Text != "")
        {
            string sptextcode = sptext.Text;
            string findcode = sptextcode.IndexOf("*").ToString();
            StartHeaderSql += "and POID in (select POID  from  IMSCMWS.[dbo].[PO_CREATE_DT] where INTERNALID = '" + sptextcode + "')";
        }
        StartHeaderSql += "order by cast(poid as int)";
        DataTable StartHeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, StartHeaderSql);


        Session["SQL104"] = StartHeaderSqldt;
        GridView1.Visible = true;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();

        //CONFIRM ENABLE
        enabledbutton();

        buttonList.Value = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            buttonList.Value += GridView1.Rows[i].FindControl("btnConfirm").ClientID + ",";
        }

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
                DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, poitemsql);

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

        //Button aaa = ((Button)GridView1.Rows[row].FindControl("btnConfirm"));
        //aaa.Enabled = false;
        //aaa.BorderColor = System.Drawing.Color.White;

        txtt3.Text = Session["NokRunEnv"].ToString().Trim();

        if (txtt3.Text != "REAL" && txtt3.Text != "TEST" && txtt3.Text != "UAT")
        {
            Response.Write("<script>alert('The Clinet " + txtt3.Text + " error')</script>");
            return;
        }


        string status = createso.WebCreateSO(conntype, txtConn.Text, txtt3.Text, strPO, dbName);
        string status1 = "N";

        if (status == "Y") 
        {
            status1 = pi.WebPassPOInfo(conntype, txtConn.Text, txtt3.Text, strPO, dbName);
        }

        if (status == "Y" && status1 == "Y")
        {
            string updatestatusuf7 = "update " + dbName + ".[dbo].[PO_CREATE_DT] set F7 = '2' where poid = '" + strPO + "'";
            int updatecount = PDataBaseOperation.PExecSQL(conntype, txtConn.Text, updatestatusuf7);
            sostatus.Visible = true;
            sostatus.Text = "SO Create Sussess";
        }
        else 
        {
            sostatus.Visible = true;
            sostatus.Text = "Error：SAP log have error message";
        }
        textFrom.Text = BegTime;
        textEnd.Text = EndTime;
        HMDPO = textpo.Text;
        //LoactionCode = textCode.Text;
        //ShowData(BegTime, EndTime, HMDPO, LoactionCode);
        ShowData(BegTime, EndTime, HMDPO, textVendID.Text);
        GridView2Bind(strPO, "link");

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
        Button b = (Button)e.Row.FindControl("btnConfirm");
        if (b != null)
            b.Attributes["onclick"] = "this.disabled = true;this.value = 'please wait..';" + Page.ClientScript.GetPostBackEventReference(b, ""); 
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


    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        GridView5.PageIndex = newPageIndex;
        GridView5.DataSource = Session["dt_DT5"];
        GridView5.DataBind();
        //enabledbutton();

    }

    protected void Download_Click(object sender, EventArgs e)
    {
        string dtime = DateTime.Now.ToString("yyyyMMddhhmmss");
        string startdate = "";
        string enddate = "";
        if (textFrom.Text != "")
        {
            startdate = textFrom.Text.Substring(0, 4) + textFrom.Text.Substring(5, 2) + textFrom.Text.Substring(8, 2);

        }
        if (textEnd.Text != "")
        {

            enddate = textEnd.Text.Substring(0, 4) + textEnd.Text.Substring(5, 2) + textEnd.Text.Substring(8, 2);
        }
        this.ExportDataTable("application/ms-excel", "RECEIVE " + startdate + " TO " + enddate + " POLIST.xls");
    }

    #region ExportDataTable
    public void ExportDataTable(string FileType, string FileName)
    {
        string startdate = "";
        string enddate = "";

        string excelsql = "SELECT PODETAIL.poid,PODETAIL.ItemID poitemid,podetail.SOID SO,podetail.DNID DN ,InternalID CustomerPN,DESCRIPTION,BASEQTY QTY,UNIT,DeliveryStartDT pickupdate	";
        excelsql += ",PO_Create_DT_UF1 deliverydate,CostAmount unitprice,CostCurrencyCode Currency,AMOUNT ,F6 ackflag,FWCODE Carrier,OriginID TNSPO,OriginalSOID TNSSO,	";
        excelsql += "Orincoterms Terms,Orincoterms2 termdescription ,";
        if (textVendID.Text == "1000253")
        {
            excelsql += " poheader.SAP_LOG,";
        }
        excelsql += "SHIPTO.NumberDefaultIndicator SHIPTOCompanycode ,SHIPTO.Address_UF2 SHIPTOName , SHIPTO.Address_UF3 SHIPTOAddressLine1 ,";
        excelsql += "SHIPTO.Address_UF4 SHIPTOAddressLine2 , SHIPTO.CareOfName SHIPTOFloorOrigina , SHIPTO.StreetNamE SHIPTOHouseNumbe , SHIPTO.GivenName SHIPTOStreet , 	";
        excelsql += "SHIPTO.Address_UF5 SHIPTOAddressLine4,SHIPTO.Address_UF6 SHIPTOAddressLine5 , SHIPTO.PostalCODE  SHIPTOPostalCode , SHIPTO.CityName SHIPTOCityName , 	";
        excelsql += "SHIPTO.Address_UF1 SHIPTOState , SHIPTO.CountryCode SHIPTOCountry ,SHIPTO.RegionCode  SHIPTORegionCode , poheader.PO_CREATE_MT_UF9  ,poheader.PO_CREATE_MT_UF10 ,poheader.DOCUMENTID ";
        excelsql += "FROM [IMSCMWS].[dbo].[PO_CREATE_DT] PODETAIL , [IMSCMWS].[dbo].[POShipToAddress] SHIPTO , [IMSCMWS].[dbo].[PO_CREATE_MT] poheader  WHERE PODETAIL.POID = SHIPTO.POID AND PODETAIL.ITEMID = SHIPTO.ITEMID	";
        excelsql += "AND PODETAIL.ITEMID = SHIPTO.ITEMID and PODETAIL.POID = poheader.POID"; 


//SELECT PODETAIL.poid,PODETAIL.ItemID poitemid,podetail.SOID SO,podetail.DNID DN ,InternalID CustomerPN,DESCRIPTION,BASEQTY QTY,UNIT,DeliveryStartDT pickupdate	
//,PO_Create_DT_UF1 deliverydate,CostAmount unitprice,CostCurrencyCode Currency,AMOUNT ,F6 ackflag,FWCODE Carrier,OriginID TNSPO,OriginalSOID TNSSO,	
//Orincoterms Terms,Orincoterms2 termdescription ,SHIPTO.NumberDefaultIndicator SHIPTOCompanycode ,SHIPTO.Address_UF2 SHIPTOName , SHIPTO.Address_UF3 SHIPTOAddressLine1 ,	
//SHIPTO.Address_UF4 SHIPTOAddressLine2 , SHIPTO.CareOfName SHIPTOFloorOrigina , SHIPTO.StreetNamE SHIPTOHouseNumbe , SHIPTO.GivenName SHIPTOStreet , 	
//SHIPTO.Address_UF5 SHIPTOAddressLine4,SHIPTO.Address_UF6 SHIPTOAddressLine5 , SHIPTO.PostalCODE  SHIPTOPostalCode , SHIPTO.CityName SHIPTOCityName , 	
//SHIPTO.Address_UF1 SHIPTOState , SHIPTO.CountryCode SHIPTOCountry ,SHIPTO.RegionCode  SHIPTORegionCode	, poheader.PO_CREATE_MT_UF9  ,      poheader.PO_CREATE_MT_UF10 ,poheader.DOCUMENTID
//FROM [IMSCMWS].[dbo].[PO_CREATE_DT] PODETAIL , [IMSCMWS].[dbo].[POShipToAddress] SHIPTO , [IMSCMWS].[dbo].[PO_CREATE_MT] poheader 
//WHERE PODETAIL.POID = SHIPTO.POID AND PODETAIL.ITEMID = SHIPTO.ITEMID	 and PODETAIL.POID = poheader.POID 


        if (textFrom.Text != "")
        {
            startdate = textFrom.Text.Substring(0, 4) + textFrom.Text.Substring(5, 2) + textFrom.Text.Substring(8, 2);

        }
        if (textEnd.Text != "")
        {

            enddate = textEnd.Text.Substring(0, 4) + textEnd.Text.Substring(5, 2) + textEnd.Text.Substring(8, 2);
        }

       
        if (startdate != "")
        {
            //StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) >= '" + strbeingdate + "' ";
            excelsql += " and SUBSTRING(PODETAIL.DOCUMENTID,1,8) >= '" + startdate + "' ";
        }
        if (enddate != "")
        {
            // StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) <= '" + strenddate + "'";
            excelsql += " and SUBSTRING(PODETAIL.DOCUMENTID,1,8) <= '" + enddate + "'";
        }
        if (HMDPO != "")
        {
            excelsql += " and PODETAIL.POID = '" + HMDPO + "'";
        }
        if (textVendID.Text != "")
        {
            excelsql += " and SellerPartyID  = '" + textVendID.Text + "'";
        }
        if (DropDownListdn.Text == "YES")
        {
            excelsql += "and  PODETAIL.DNID != '' AND PODETAIL.SOID != ''";
        }
        if (DropDownListdn.Text == "NO")
        {
            excelsql += "AND PODETAIL.DNID IS NULL AND PODETAIL.SOID IS NULL ";
        }

        DataTable excelsqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, excelsql);

        Session["sqlexcel"] = excelsqldt;
        GridView gv = new GridView();
        gv.DataSource = Session["sqlexcel"];
        gv.DataBind();
        Response.Charset = "UNICODE";
        //Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();
    }
    #endregion
    protected void CreateallPO_Click(object sender, EventArgs e)
    {

        string startdate = "";
        string enddate = "";

        BegTime = textFrom.Text;
        EndTime = textEnd.Text;
        HMDPO = textpo.Text;



        txtt3.Text = Session["NokRunEnv"].ToString().Trim();

        string DownallpoSql = "SELECT distinct POID   FROM " + dbName + ".[dbo].[PO_CREATE_DT]  Where F6 = '2' and F7 = '1' ";
        if (textFrom.Text != "")
        {
            startdate = textFrom.Text.Substring(0, 4) + textFrom.Text.Substring(5, 2) + textFrom.Text.Substring(8, 2);

        }
        if (textEnd.Text != "")
        {

            enddate = textEnd.Text.Substring(0, 4) + textEnd.Text.Substring(5, 2) + textEnd.Text.Substring(8, 2);
        }


        if (startdate != "")
        {
            //StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) >= '" + strbeingdate + "' ";
            DownallpoSql += " and SUBSTRING(DOCUMENTID,1,8) >= '" + startdate + "' ";
        }
        if (enddate != "")
        {
            // StartHeaderSql += " and SUBSTRING(po_create_mt_uf3,1,8) <= '" + strenddate + "'";
            DownallpoSql += " and SUBSTRING(DOCUMENTID,1,8) <= '" + enddate + "'";
        }
        if (HMDPO != "")
        {
            DownallpoSql += " and POID = '" + HMDPO + "'";
        }
        if ( textVendID.Text != "" ) // (LoactionCode != "")
        {
            DownallpoSql += " and PO_CREATE_DT_UF2  = '" + textVendID.Text + "'";
        }
        DataTable DownallpoSqldt = PDataBaseOperation.PSelectSQLDT(conntype, txtConn.Text, DownallpoSql);

        if (DownallpoSqldt.Rows.Count > 0)
        {
            for (int i = 0; i < DownallpoSqldt.Rows.Count; i++)
            {
                string strPO = DownallpoSqldt.Rows[i]["POID"].ToString();

                string status = createso.WebCreateSO(conntype, txtConn.Text, txtt3.Text, strPO, dbName);
                string status1 = "N";

                if (status == "Y") 
                {
                    status1 = pi.WebPassPOInfo(conntype, txtConn.Text, txtt3.Text, strPO, dbName);
                }
                if (status == "Y" && status1 == "Y") 
                {
                    string updatestatusuf7 = "update " + dbName + ".[dbo].[PO_CREATE_DT] set F7 = '2' where poid = '" + strPO + "'";
                    int updatecount = PDataBaseOperation.PExecSQL(conntype, txtConn.Text, updatestatusuf7);
                }

            }

        }

        //Response.Redirect(Request.RawUrl);
        ShowData(BegTime, EndTime, HMDPO, textVendID.Text);
        //Response.Redirect(Request.RawUrl); 
       
    }

    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string FileName = "SetReadParam.txt";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "";
        int i = 0, strlen = 0;
        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            ReadTxtArray[i] = InString;
            //             Response.Write(ReadTxtArray[i]);
            //             Response.Write("<br>");
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
                {
                    retPara = InString.Substring(6, strlen - 5);
                    i = ArrMax;  // Break
                }
            }
            i++;
        }
        sr.Close();
        return (retPara);
    }

}
