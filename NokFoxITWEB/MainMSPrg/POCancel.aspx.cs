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
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Drawing;

public partial class MainMSPrg_POCancel : System.Web.UI.Page
{

    private static string connType;
    private static string conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string BegTime;
    private static string EndTime;
    private static string tparam;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            #region 數據庫連接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                tparam = Session["C_3A4C"].ToString();
                connType = Session["Param2"].ToString();
                conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
            }
            else if (tmp1 == "")
            {
                connType = "sql";
                conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                //dbWrite = ReadParaTxt("WebReadParam.txt", "23703");
                //conn = ReadParaTxt("WebReadParam.txt", "23703");
                //BBSCMDIR = "IMSCM";
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            lblHeader.Visible = false;
            lblDetail.Visible = false;
            divGridView2.Visible = false;
            BegTime = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            EndTime = DateTime.Now.ToString("yyyy/MM/dd");
            txtBeginDate.Text = BegTime;
            txtendDate.Text = EndTime;
            GridView1Bind();
        }

    }

    /// <summary>
    /// select 3A4 PO
    /// </summary>
    public void GridView1Bind()
    {
        string StrSql = "select distinct " +
                 "mt.SENDERID as  'MSFT DUNS'," +
                 "mt.poid as 'MSFT PO'," +
                 "mt.creationdt as CreationDT," +
                 "mt.PO_Create_MT_UF5 as Incoterm," +
                 "mt.PO_Create_MT_UF4 AS Payment," +
            //"dt.DeliveryStartDT as 'Pick Up Date'," +
            //"mt.PO_Create_MT_UF6 AS 'ShipTo DUNS Code'," +
                 "mt.PO_Create_MT_UF7 AS ShipToCode," +
            //"dt.PO_Create_DT_UF1 AS 'Request Delivery Date',"+
                 "mt.PO_Create_MT_UF2 AS PurchaseGroup," +
                 "mt.PO_Create_MT_UF3 AS DateTimeStamp,mt.id," +
                 "mt.CONFIRMFLAG" +
                  ",(select count(*) as '940Count' from (select distinct dh.Create_flag  from imscm.[dbo].[DN_Header]dh where dh.[deliverRefer]=Mt.poid ) D) as '940Count'" +
                 ",(select top 1 dh.Create_flag as '940Flag'  from imscm.[dbo].[DN_Header]dh where dh.[deliverRefer]=Mt.poid ) as '940Flag'" +
            " from " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] Mt where 1=1 ";
        StrSql = "select distinct * from (" + StrSql + ") M where 1=1 ";
        if (txtPOID.Text != "")
        {
            StrSql = StrSql + " and \"MSFT PO\"= '" + txtPOID.Text.ToString().Trim() + "'";
        }
        if (txtBeginDate.Text != "")
        {
            string begintime = txtBeginDate.Text.Replace("/", "-").Trim();
            begintime = begintime + " 00:00:00";
            StrSql = StrSql + " and CREATIONDT> '" + begintime + "'";
        }
        if (txtendDate.Text != "")
        {
            string endtime = txtendDate.Text.Replace("/", "-").Trim();
            endtime = endtime + " 23:59:59";
            StrSql = StrSql + " and CREATIONDT< '" + endtime + "'";
        }
        StrSql = StrSql + "  order by CREATIONDT desc";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, StrSql);
        if (dt.Rows.Count > 0)
        {
            Session["dt_MT"] = dt;
            divGridView1.Visible = true;
            GridView1.Visible = true;
            lblHeader.Visible = false;
            GridView1.DataSource = Session["dt_MT"];
            GridView1.DataBind();
        }
        else
        {
            lblHeader.Text = "NO Data found!";
            divGridView1.Visible = true;
            lblHeader.Visible = true;
            GridView1.Visible = false;
        }        
    }

    /// <summary>
    /// select 3A4 detail
    /// </summary>
    /// <param name="strPOID">POID</param>
    public void GridView2Bind(string strPOID)
    {
        string strDT = "select " +
                   "dt.POID as 'MSFT PO'," +
                   "dt.ItemID AS 'MSFT PO ITEM'," +
                   "dt.InternalID AS 'MSFT P/N'," +
                   "mt.CREATIONDT," +
                   "dt.Description," +
                   "'' 'PO Price Base Unit'," +
                   "dt.CostAmount," +
                   "dt.CostCurrencyCode," +
                   "'' 'Oreder QTY'," +
                   "dt.BaseQty,dt.Unit," +
                   "dt.DeliveryStartDT as 'Pick Up Date'," +
                   "dT.IncoTermsCode AS 'Payment Term'," +
                   "dt.PO_Create_DT_UF1 AS 'Request Delivery Date'," +
                   "dt.PO_Create_DT_UF2 AS 'Storage Location'," +
                   "dt.CONFIRMFLAG" +
                   " from " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] mt, " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] dt " +
                   " where 1=1 and dt.poid='" + strPOID + "' and mt.poid=dt.poid";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strDT);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //PO Price Base Unit
                string CostAmount = dt.Rows[i]["CostAmount"].ToString().Trim();
                string CostCurrencyCode = dt.Rows[i]["CostCurrencyCode"].ToString().Trim();
                if (CostAmount != "" && CostCurrencyCode != "")
                {
                    dt.Rows[i]["PO Price Base Unit"] = CostAmount + CostCurrencyCode;
                }
                //Oreder QTY
                string BaseQty = dt.Rows[i]["BaseQty"].ToString().Trim();
                string Unit = dt.Rows[i]["Unit"].ToString().Trim();
                if (BaseQty != "" && Unit != "")
                {
                    dt.Rows[i]["Oreder QTY"] = BaseQty + Unit;
                }
            }

            dt.Columns.Remove("CostAmount");
            dt.Columns.Remove("CostCurrencyCode");
            dt.Columns.Remove("BaseQty");
            dt.Columns.Remove("Unit");
            Session["dt_DT"] = dt;
            divGridView2.Visible = true;
            GridView2.Visible = true;
            lblDetail.Visible = false;
            GridView2.DataSource = Session["dt_DT"];
            GridView2.DataBind();
        }
        else
        {
            lblDetail.Text = "";
            lblDetail.Visible = true;
            divGridView2.Visible = true;
            GridView2.Visible = false;
        }
       
    }

    /// <summary>
    /// select 3A4 PO
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1Bind();
    }

    /// <summary>
    /// reset
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtendDate.Text = "";
        txtBeginDate.Text = "";
        txtPOID.Text = "";
        divGridView1.Visible = false;
        divGridView2.Visible = false;
    }

    /// <summary>
    /// 3A4 detail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtPO_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string strPOID = ((LinkButton)GridView1.Rows[row].FindControl("Label3")).Text.ToString();
        GridView2Bind(strPOID);
    }

    /// <summary>
    /// 3A4 Cencel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearPO_Click(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(((Button)sender).CommandArgument);
        index = index % 7;
        //string strPoid = ((Label)GridView1.Rows[index].FindControl("Label3")).Text.ToString();
        string strPoid = ((LinkButton)GridView1.Rows[index].FindControl("Label3")).Text.ToString();
        #region 检测940 3B2 204 中此PO对应的DN是否Confirm
        //判斷940中此PO對應的DN是否confirm
        string str940 = "select * from " + BBSCMDIR + ".[dbo].[DN_Header] where deliverRefer='" + strPoid + "'";
        DataTable dt940 = PDataBaseOperation.PSelectSQLDT(connType, conn, str940);
        if (dt940.Rows.Count > 0)
        {
            for (int i = 0; i < dt940.Rows.Count; i++)
            {
                string strDnid = dt940.Rows[i]["w0502"].ToString().Trim();
                string strConfrim = dt940.Rows[i]["Create_flag"].ToString().Trim();
                string strSendFlag = dt940.Rows[i]["send_flag"].ToString().Trim();
                if (strConfrim == "Y")
                {
                    Response.Write("<script>alert('The PO( " + strPoid + ") exist 940( "+strDnid+" ) event ,please revert it before cancel the PO.!');</script>");
                    return;
                }
                else
                {
                    #region 判斷204中此PO對應的DN是否confirm
                    string str204 = "select * from " + BBSCMDIR + ".[dbo].[EDI204L11] where L1101='" + strDnid + "' and L1102='DO'";
                    DataTable dt204 = PDataBaseOperation.PSelectSQLDT(connType, conn, str204);
                    for (int j = 0; j < dt204.Rows.Count; j++)
                    {
                        string str204CONFIRMFLAG = dt204.Rows[j]["CONFIRMFLAG"].ToString().Trim();
                        string str204ID = dt204.Rows[j]["B204"].ToString().Trim();
                        if (str204CONFIRMFLAG == "Y")
                        {
                            Response.Write("<script>alert('The PO( " + strPoid + ") exist 204( "+str204ID+" ) event ,please revert it before cancel the PO.!');</script>");
                            return;
                        }
                    }
                    #endregion
                    #region 判斷3B2中此PO對應的DN是否confirm
                    string str3B2 = "select * from " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] where DNID='" + strDnid + "'";
                    DataTable dt3B2 = PDataBaseOperation.PSelectSQLDT(connType, conn, str3B2);
                    for (int k = 0; k < dt3B2.Rows.Count; k++)
                    {
                        string str3B2CONFIRMFLAG = dt3B2.Rows[k]["CONFIRMFLAG"].ToString().Trim();
                        if (str3B2CONFIRMFLAG == "Y")
                        {
                            Response.Write("<script>alert('The PO(" + strPoid + ") exist 3b2( "+strDnid+" ) event ,please revert it before cancel the PO.!');</script>");
                            return;
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion
        #region update 3A4 ConfirmFlag='D'
        string strPOMT = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_MT] set confirmflag='D' where poid='" + strPoid + "'";
        int idetMT = PDataBaseOperation.PExecSQL(connType, conn, strPOMT);
        string strPODT = "update " + BBSCMDIR + ".[dbo].[PO_CREATE_DT] set confirmflag='D' where poid='" + strPoid + "'";
        int idetDT = PDataBaseOperation.PExecSQL(connType, conn, strPODT);
        if (idetDT > 0 && idetMT > 0)
        {
            Response.Write("<script>alert('Cancel OK!')</script>");
        }
        else if (idetMT <= 0 && idetDT > 0)
        {
            Response.Write("<script>alert('Update PO_CREATE_MT Error')</script>");
        }
        else if (idetDT <= 0 && idetMT > 0)
        {
            Response.Write("<script>alert('Update PO_CREATE_DT Error')</script>");
        }
        else
        {
            Response.Write("<script>alert('Cancel Error')</script>");
        }
        #endregion
        GridView1Bind();
        GridView2Bind(strPoid);
    }

    #region GridView
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
        GridView1.DataSource = Session["dt_MT"];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((Button)e.Row.Cells[2].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you sure Reset?')");
                ((Button)e.Row.Cells[0].FindControl("btnClearPO")).Attributes.Add("onclick", "javascript:return confirm('Are you sure Cancel?')");
            }
        } 

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string confirmFlag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();
            int str940Count = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "940Count").ToString().Trim());
            string str940Flag = DataBinder.Eval(e.Row.DataItem, "940Flag").ToString().Trim();
            Button B3 = (Button)e.Row.FindControl("btnClearPO");
            if (tparam == "BOTH")
            {
                if (str940Count>1 || (str940Count==1 && str940Flag == "Y"))
                {
                    B3.Enabled = false;
                }
                if (confirmFlag == "Y" || confirmFlag == "y")
                {
                    e.Row.BackColor = Color.LightGreen;
                    //B3.Enabled = false;
                }
                else if (confirmFlag == "D" || confirmFlag == "d")
                {
                    e.Row.BackColor = Color.Red;
                    B3.Enabled = false;
                }
            }
            else
            {
                if (confirmFlag == "Y" || confirmFlag == "y")
                {
                    e.Row.BackColor = Color.LightGreen;                   
                }
                else if (confirmFlag == "D" || confirmFlag == "d")
                {
                    e.Row.BackColor = Color.Red;                   
                }
                B3.Enabled = false;
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
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string confirmFlag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();
            //Button B3 = (Button)e.Row.FindControl("btnClearPO");
            if (confirmFlag == "Y" || confirmFlag == "y")
            {
                e.Row.BackColor = Color.LightGreen;

                //B3.Enabled = false;
            }
            else if (confirmFlag == "D" || confirmFlag == "d")
            {
                e.Row.BackColor = Color.Red;
                //B3.Enabled = false;
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

    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        //int ArrMax = 300;
        string[] ReadTxtArray = new string[400];
        string FileName = "SetReadParam.txt";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "";
        int i = 0, strlen = 0;

        while (((InString = sr.ReadLine()) != null) && (i < 400))
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
                    i = 400;  // Break
                }

            }
            i++;

        }

        sr.Close();
        return (retPara);
    }
}