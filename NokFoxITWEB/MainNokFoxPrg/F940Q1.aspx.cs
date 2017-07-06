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
using SFC.TJWEB;
using System.Drawing;

public partial class MainMSPrg_F940Q1 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbRead;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string param;
    private static string tablename;
    private static string pocnt;
    private static string tparam;
    private static string tprivate;
    private static string tuser;
    private static int count;
    int editRow = -1;
    bool isEdit = false;
    private static DataTable dt = new DataTable();
    BBSCMlib BBSCMlibPointer = new BBSCMlib();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                conntype = Session["Param2"].ToString();
                Conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
                tparam = Session["tparam"].ToString();  // PROLINE 
                tprivate = Session["C_940"].ToString();
                tuser = Session["userid"].ToString();
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
            //conntype = "sql";
            ////Conn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93
            ////dbWrite = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93

            //Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108
            //dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108

            dbo = new PDataBaseOperation(conntype, Conn);
            txtBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtendDate.Text = DateTime.Now.AddDays(+7).ToString("yyyy/MM/dd");
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            GridViewBind();

            //根據權限設定按鈕有效性
            if (tprivate == "DISPLAY"||tprivate =="")
            {
                Button6.Enabled = false;
            }
        } 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        dnNum.Text = "";
        plant.Text = "";
        Label1.Visible = false;
        Label2.Visible = false;
        Label3.Visible = false;
        GridView1.Visible = false;
        GridView2.Visible = false;
        GridView3.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hdn = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString().Trim();
        string hshipdate = ((TextBox)GridView1.Rows[row].FindControl("shippingdate")).Text.ToString().Trim();
        if (hshipdate != "") hshipdate = hshipdate.Replace("/", "").Trim();
        string hrequestdate = ((TextBox)GridView1.Rows[row].FindControl("requestdate")).Text.ToString().Trim();
        if (hrequestdate != "") hrequestdate = hrequestdate.Replace("/", "").Trim();
        string hcname = ((TextBox)GridView1.Rows[row].FindControl("contractname")).Text.ToString().Trim();
        string hcmail = ((TextBox)GridView1.Rows[row].FindControl("contractEmail")).Text.ToString().Trim();
        string hcphone = ((TextBox)GridView1.Rows[row].FindControl("contractPhone")).Text.ToString().Trim();
        string hrouting = ((TextBox)GridView1.Rows[row].FindControl("routing")).Text.ToString().Trim();
        string hfobpoint = ((TextBox)GridView1.Rows[row].FindControl("FOBpoint")).Text.ToString().Trim();
        string hincode = ((TextBox)GridView1.Rows[row].FindControl("industrycode")).Text.ToString().Trim();
        string strupd = "update " + BBSCMDIR + ".[dbo].[DN_Header] set shippingdate='" + hshipdate + "',requestdate='" + hrequestdate + "',contractname='" + hcname + "',contractEmail='" + hcmail + "'," +
                        "contractPhone='" + hcphone + "',routing='" + hrouting + "',FOBpoint='" + hfobpoint + "',industrycode='" + hincode + "' where w0502='" + hdn + "'";
        int idet = dbo.PExecSQLTran(strupd);
        if (idet == 1)
        {
            Response.Write("<script>alert('Update Successfully!');</script>");
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        //string dtdn = GridView2.Rows[row].FindControl["w0502"].Text.ToString();
        //string dtdnitem = GridView2.Rows[row].FindControl["Selleritemno"].Text.ToString();
        string dtdn = GridView2.DataKeys[row].Values[0].ToString().Trim();
        string dtdnitem = GridView2.DataKeys[row].Values[1].ToString().Trim();
        string dtqty = ((TextBox)GridView2.Rows[row].FindControl("qty")).Text.ToString();
        string dtweigh = ((TextBox)GridView2.Rows[row].FindControl("lineitemweight")).Text.ToString();
        string strupd = "update " + BBSCMDIR + ".[dbo].[DN_Detail] set qty='" + dtqty + "',lineitemweight='" + dtweigh + "' where w0502='" + dtdn + "' and Selleritemno='" + dtdnitem + "'";
        int idet = dbo.PExecSQLTran(strupd);
        if (idet == 1)
        {
            Response.Write("<script>alert('Update Successfully!');</script>");
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hdn = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString().Trim();
        string hpoid = GridView1.DataKeys[row].Values[1].ToString().Trim();
        string orderqty = GridView1.DataKeys[row].Values[2].ToString().Trim();
        //poid is null,No send
        if (hpoid == "")
        {
            Response.Write("<script>alert('No PO,it is not allowed to send!');</script>");
            return;
        }
        if (orderqty != "")
        {
            double qty = Double.Parse(orderqty);
            if (qty > 6720)
            {
                Response.Write("<script>alert('The orderqty has more than 6720,it is not allowed to send!');</script>");
                return;
            }
        }
        string strcheck = "select CONFIRMFLAG FROM " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] where POID='" + hpoid + "'";
        DataTable dtcheck = dbo.PSelectSQLDT(strcheck);
        if (dtcheck.Rows.Count > 0)
        {
            string poconfirmflag = dtcheck.Rows[0]["CONFIRMFLAG"].ToString().Trim();
            if (poconfirmflag == "D")
            {
                Response.Write("<script>alert('The DN correspond the MSFT PO have already Cancel ,you can’t send 940!');</script>");
            }
            else
            {
                //update 940 Create_flag='Y'
                string strupd = "update " + BBSCMDIR + ".[dbo].[DN_Header] set Create_flag='Y' where w0502='" + hdn + "'";
                int idet = dbo.PExecSQLTran(strupd);

                //update 3A4 CONFIRMFLAG='Y'
                string strdetail = "select * from " + BBSCMDIR + ".[dbo].[DN_Detail] where w0502='" + hdn + "'";
                DataTable dtdetail = dbo.PSelectSQLDT(strdetail);
                string err = "N";
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        string materialno = dtdetail.Rows[i]["MGpartnum"].ToString();
                        string buyeritem = dtdetail.Rows[i]["buyeritemno"].ToString().Substring(1,5);
                        string strupd1 = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] set CONFIRMFLAG='Y' where POID='" + hpoid + "'" +
                                         "and INTERNALID='" + materialno + "' and ITEMID='" + buyeritem + "'";
                        int idet1 = dbo.PExecSQLTran(strupd1);
                        if (idet1 == 0)
                        {
                            err = "Y";
                        }
                    }
                }
               
                //check po_dt,update po_mt
                string strpodt = "select distinct CONFIRMFLAG from " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] where POID='" + hpoid + "'";
                DataTable dtpodt = dbo.PSelectSQLDT(strpodt);
                if (dtpodt.Rows.Count == 1)
                {
                    string strupd2 = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] set CONFIRMFLAG='Y' where POID='" + hpoid + "'";
                    int idet2 = dbo.PExecSQLTran(strupd2);
                    if (err == "N" && idet2 == 0)
                    {
                        Response.Write("<script>alert('Confirm 3A4 failed,Please check again!');</script>");
                    }
                }

                if (idet == 1 && err == "N")
                {
                    Response.Write("<script>alert('Create Successfully!');</script>");
                }
                else if (idet == 0)
                {
                    Response.Write("<script>alert('Create 940 failed,Please check again!');</script>");
                }
                
            }
        }
        GridViewBind();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string dnid = dnNum.Text.ToString().Trim();
        string begindate = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
        string enddate = DateTime.Now.ToString("yyyyMMdd");
        if (dnid == "")
        {
            Response.Write("<script>alert('Please input DN number,it is mandatory condition !');</script>");
            return;
        }
        //20151117
        string check = "select * from " + BBSCMDIR + ".[dbo].[DN_Header] where w0502='" + dnid + "'";
        DataTable dtcheck = dbo.PSelectSQLDT(check);
        if (dtcheck.Rows.Count>0)
        {           
            GridViewBind();
            Label1.Text = "Have get the SAP Data!";
            Label1.Visible = true;
            return;
        }

        if (tparam == "TEST")
        {
            dbRead = "ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh";
            //dbRead = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";
        }
        else if (tparam == "PROD")
        {
            dbRead = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";
        }


        string mes = GSFClib1.GetSap940(dnid, dbRead, dbWrite, begindate, enddate);
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        if (mes == "True")
        {
            GridViewBind();
            Label1.Text = "Get DN from SAP successfully!";
            Label1.Visible = true;
        }
        else
        {
            Label1.Text = "Get DN from SAP failed!";
            Label1.Visible = true;
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hdn = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString().Trim();
        string begindate = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
        string enddate = DateTime.Now.ToString("yyyyMMdd");

        string mes = "", mes1 = "";
        if (tparam == "TEST")
        {
            dbRead = "ASHOST=10.134.92.27; CLIENT=802;SYSNR=00;USER=FIHBJKFC;PASSWD=FOXCONN8;LANG=zh";
        }
        else if (tparam == "PROD")
        {
            dbRead = "ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCSHARE02;PASSWD=it0215;LANG=zh";
        }

        //delete 3B2,Read from SAP
        string str3B2MT = "delete from " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + hdn + "'";
        int idetMT = dbo.PExecSQLTran(str3B2MT);
        string str3B2DT = "delete from " + BBSCMDIR + ".[dbo].[Delivery_DNITEM] where DNID='" + hdn + "'";
        int idetDT = dbo.PExecSQLTran(str3B2DT);
        string str3B2HU = "delete from " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] where DNID='" + hdn + "'";
        int idetHU = dbo.PExecSQLTran(str3B2HU);
        if (idetMT > 0 && idetDT > 0 && idetHU > 0)
        {
            mes = GSFClib1.GetSapDN(hdn, dbRead, dbWrite, begindate, enddate);
        }

        if (mes.ToLower() == "true")
        {
            //delete 940,Read from SAP
            string str940header = "delete from " + BBSCMDIR + ".[dbo].[DN_Header] where w0502='" + hdn + "'";
            int idetheader = dbo.PExecSQLTran(str940header);
            string str940detail = "delete from " + BBSCMDIR + ".[dbo].[DN_Detail] where w0502='" + hdn + "'";
            int idetdetail = dbo.PExecSQLTran(str940detail);
            string str940address = "delete from " + BBSCMDIR + ".[dbo].[Address] where w0502='" + hdn + "'";
            int idetaddress = dbo.PExecSQLTran(str940address);

            if (idetheader > 0 && idetdetail > 0 && idetaddress > 0)
            {
                mes1 = GSFClib1.GetSap940(hdn, dbRead, dbWrite, begindate, enddate);
            }

            txtBeginDate.Text = "";
            txtendDate.Text = "";
            if (mes1.ToLower() == "true")
            {
                GridViewBind();
                Label1.Text = "Reset data successfully!";
                Label1.Visible = true;
            }
            else
            {
                Label1.Text = "Reset data failed!";
                Label1.Visible = true;
            }
        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hdn = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString().Trim();
        //檢查一下是否存在有效的204，如有，提示存在204，讓用戶先將204刪除設為無效。
        string check204 = "  select distinct hd.B204,lad.OID01,hd.CONFIRMFLAG,hd.RCOUNT"+
                          " from " + BBSCMDIR + ".[dbo].[EDI204HEADER] hd," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] lad" +
                          " where hd.B204=lad.B204 and hd.RCOUNT=lad.RCOUNT and (hd.CONFIRMFLAG!='D' or hd.CONFIRMFLAG is null) and "+
                          " hd.B204+ (case when confirmflag='Y' then '2200-01-01 00:00:01' else hd.insertdate end) in  "+
                          " (select H1.B204+max (case when H1.confirmflag='Y' then '2200-01-01 00:00:01'  else H1.insertdate end) "+
                          " as insertdate1 "+
                          " from  IMSCM.[dbo].[EDI204HEADER] H1 group by H1.B204)"+
                          " and lad.OID01='" + hdn + "'";
        DataTable dtcheck204 = dbo.PSelectSQLDT(check204);
        if (dtcheck204.Rows.Count > 0)
        {
            Response.Write("<script>alert('204,there is effective.Please check!!');</script>");
            //Label1.Text = "204,there is effective.Please check!";
            //Label1.Visible = true;
            return;
        }
        else
        {
            Session["Param1"] = "1";
            Session["Param2"] = conntype; // DBReadString
            Session["Param3"] = Conn; // DBReadString   44
            Session["Param4"] = dbWrite; // DBWriteString  215
            Session["Param5"] = Autoprg;   // Menu input
            Session["Param6"] = tmpdate;
            Session["Param7"] = BBSCMDIR;  // PROLINE 
            //Response.Write("<script>window.showModalDialog('DeletePage.aspx?type=940&DNID="+hdn+"','width=100,height=100,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00')</script>");
            Response.Write("<script language='javascript'>var result = window.showModalDialog('DeletePage.aspx?type=940&DNID=" + hdn + "',window,'dialogHeight:200px;dialogWidth:300px;');if(result==1){ window.location.reload();}</script>");
        }
        GridViewBind();
    }
    protected void lbtnDnID_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string dnid = ((LinkButton)GridView1.Rows[row].FindControl("lbtnDnID")).Text.ToString();
        string strselect = "select * from " + BBSCMDIR + ".[dbo].[DN_Detail] where w0502='" + dnid + "'";
        DataTable dtselect = dbo.PSelectSQLDT(strselect);
        if (dtselect.Rows.Count > 0)
        {
            GridView2.Visible = true;
            Session["SQLDNDT"] = dtselect;
            GridView2.DataSource = Session["SQLDNDT"];
            GridView2.DataBind();
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "NO Data Found!";
            GridView2.Visible = false;
        }
        string strselect1 = "select * from " + BBSCMDIR + ".[dbo].[Address] where w0502='" + dnid + "'";
        DataTable dtselect1 = dbo.PSelectSQLDT(strselect1);
        if (dtselect1.Rows.Count > 0)
        {
            GridView3.Visible = true;
            Session["SQLADD"] = dtselect1;
            GridView3.DataSource = Session["SQLADD"];
            GridView3.DataBind();
            Label3.Visible = false;
        }
        else
        {
            Label3.Visible = true;
            Label3.Text = "NO Data Found!";
            GridView3.Visible = false;
        }
    }
    protected string TimeFormat(string datetime)
    {
        string dateform = "";
        if (datetime != "")
        {
            //dateform = datetime.Replace("/", "-").Trim();
            dateform = datetime.Replace("/", "").Trim();
        }
        return dateform;
    }
    protected void GridViewBind()
    {
        string dntxt = dnNum.Text.Trim();
        string begintime = TimeFormat(txtBeginDate.Text);
        string endtime = TimeFormat(txtendDate.Text);
        string plantid = plant.Text.Trim();
        //string sqlselect = "select substring(requestdate,1,10) trequestdate,substring(shippingdate,1,10) tshippingdate,* from " + BBSCMDIR + ".[dbo].[DN_Header] where 1=1";
        string sqlselect = "select * from " + BBSCMDIR + ".[dbo].[DN_Header] where (Create_flag!='D' or Create_flag is null)";
        if (dntxt != "") { sqlselect = sqlselect + " and w0502 like '%" + dntxt + "%'"; }
        if (begintime != "") { sqlselect = sqlselect + " and requestdate>='" + begintime + "'"; }
        if (endtime != "") { sqlselect = sqlselect + " and requestdate<='" + endtime + "'"; }
        if (plantid != "") { sqlselect = sqlselect + ""; }//MT表中工廠欄位？？？
        sqlselect = sqlselect + " order by requestdate desc";
        DataTable dtdetail = dbo.PSelectSQLDT(sqlselect);
        count = dtdetail.Rows.Count;
        if (dtdetail.Rows.Count > 0)
        {
            GridView1.Visible = true;
            Session["SQLHeader"] = dtdetail;
            GridView1.DataSource = Session["SQLHeader"];
            GridView1.DataBind();
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = false;
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "No Data found!You can input this DN in textbox,then click 'GetSAP' button to get the record!";
            GridView1.Visible = false;
            Label2.Visible = false;
            GridView2.Visible = false;
            Label3.Visible = false;
            GridView3.Visible = false;
        }
    }

    #region GV1
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
        //得到新的值 
        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLHeader"];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string sendFlag = DataBinder.Eval(e.Row.DataItem, "send_flag").ToString().Trim();
            Button B3 = (Button)e.Row.FindControl("Button3");
            Button B5 = (Button)e.Row.FindControl("Button5");
            Button B7 = (Button)e.Row.FindControl("Button7");
            Button B8 = (Button)e.Row.FindControl("Button8");
            if (sendFlag != "N")
            {
                B3.Enabled = false;
                B5.Enabled = false;
                e.Row.BackColor = Color.LightGreen;
            }
            if (tprivate == "DISPLAY" || tprivate == "")
            {
                B3.Enabled = false;
                B5.Enabled = false;
                B7.Enabled = false;
            }
            if (tuser.ToUpper() != "GMO_IT"&&tuser.ToUpper() != "SCM_DCC")
            {
                B8.Enabled = false;
            }
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
        //edit function
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((Button)e.Row.Cells[2].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you sure Reset?')");
                ((Button)e.Row.Cells[2].FindControl("Button7")).Attributes.Add("onclick", "javascript:return confirm('Are you sure Reset?')");
                ((Button)e.Row.Cells[2].FindControl("Button8")).Attributes.Add("onclick", "javascript:return confirm('Are you sure Delete?')");
            }
        } 
    }
    #endregion

    #region GV2
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
        //得到新的值 
        GridView2.PageIndex = newPageIndex;
        GridView2.DataSource = Session["SQLDNDT"];
        GridView2.DataBind();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string dnid = DataBinder.Eval(e.Row.DataItem, "w0502").ToString().Trim();
            string strselect = "select send_flag FROM " + BBSCMDIR + ".[dbo].[DN_Header] where w0502='" + dnid + "'";
            DataTable dtselect = dbo.PSelectSQLDT(strselect);
            if (dtselect.Rows.Count > 0)
            {
                string confirmflag = dtselect.Rows[0]["send_flag"].ToString().Trim();
                Button B4 = (Button)e.Row.FindControl("Button4");
                if (confirmflag != "N")
                {
                    B4.Enabled = false;
                    e.Row.BackColor = Color.LightGreen;
                }
                if (tprivate == "DISPLAY" || tprivate == "")
                {
                    B4.Enabled = false;
                }
            }


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
    #endregion

    #region GV3
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
        //得到新的值 
        GridView3.PageIndex = newPageIndex;
        GridView3.DataSource = Session["SQLADD"];
        GridView3.DataBind();

    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string dnid = DataBinder.Eval(e.Row.DataItem, "w0502").ToString().Trim();
            string strselect = "select send_flag FROM " + BBSCMDIR + ".[dbo].[DN_Header] where w0502='" + dnid + "'";
            DataTable dtselect = dbo.PSelectSQLDT(strselect);
            if (dtselect.Rows.Count > 0)
            {
                string confirmflag = dtselect.Rows[0]["send_flag"].ToString().Trim();
                if (confirmflag != "N")
                {
                    e.Row.BackColor = Color.LightGreen;
                }
            }

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
    #endregion

}