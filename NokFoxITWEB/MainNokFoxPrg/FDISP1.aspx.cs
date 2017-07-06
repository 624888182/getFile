using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMSPrg_FDISP1 : System.Web.UI.Page
{
    private static string begTime = "";
    private static string endTime = "";
    private static string connType;
    private static string conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 數據庫連接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
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
                BBSCMDIR = "[IMSCM]";
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion
            begTime = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            endTime = DateTime.Now.ToString("yyyy/MM/dd");
            textBegTime.Text = begTime;
            textEndTime.Text = endTime;
            begTime = begTime.Replace("/", "");
            endTime = endTime.Replace("/", "");
            begTime = begTime.Substring(2) + "00000000";
            endTime = endTime.Substring(2) + "23595999";
            string strSel = "select distinct top 20 dd.w0502,dd.buyeritemno,dd.Selleritemno,dd.qty," +
                //"dd.uf8,dd.uf9,dd.uf10,dd.ID,"+
                "dh.uf8,dh.uf9,dh.uf10,dh.ID," +
                "dd.creationdt,dd.MGpartnum,dh.deliverRefer" +
                " from " + BBSCMDIR + ".[dbo].[DN_Detail] dd," + BBSCMDIR + ".[dbo].[DN_Header] dh," + BBSCMDIR + 
                ".[dbo].[PO_CREATE_MT] pomt" +
                " where dd.w0502=dh.w0502 and dh.deliverRefer=pomt.poid and dd.creationdt>='" + begTime + "' and dd.creationdt<='" + endTime + "'" +
                " order by creationdt desc";
            DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
            Session["dt"] = dt;
            int idet = dt.Rows.Count;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        string strPO = textPO.Text.Trim();
        string strDN = textDN.Text.Trim();
        string strWhere="";
        begTime = textBegTime.Text.Trim();
        endTime = textEndTime.Text.Trim();
        begTime = begTime.Replace("/", "");
        endTime = endTime.Replace("/", "");
        begTime = begTime.Substring(2) + "00000000";
        endTime = endTime.Substring(2) + "23595999";
        strWhere = " where dd.w0502=dh.w0502 and dh.deliverRefer=pomt.poid and dd.creationdt>='" + begTime + "' and dd.creationdt<='" + endTime + "'";
        if(strPO!=""&&strDN!="")
        {
            strWhere = strWhere+" and dh.deliverRefer='" + strPO + "' and dd.w0502='" + strDN + "'";
        }
        else if(strPO!="")
        {
            strWhere = strWhere + " and dh.deliverRefer='" + strPO + "'";
        }
        else if(strDN!="")
        {
            strWhere = strWhere + " and dd.w0502='" + strDN + "'";
        }
        string strSel = "select distinct dd.w0502,dd.buyeritemno,dd.Selleritemno,dd.qty," +
            //"dd.uf1,dd.uf2,dd.uf3,dd.uf4,"+
            "dh.uf8,dh.uf9,dh.uf10,dh.ID," +
            "dd.creationdt,dd.MGpartnum,dh.deliverRefer" +
            " from " + BBSCMDIR + ".[dbo].[DN_Detail] dd," + BBSCMDIR + ".[dbo].[DN_Header] dh," + BBSCMDIR + ".[dbo].[PO_CREATE_MT] pomt" + strWhere + 
            " order by dd.creationdt desc";
        DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
        Session["dt"] = dt;
        int idet = dt.Rows.Count;
        GridView1.DataSource = dt;
        GridView1.DataBind();         
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        textPO.Text = "";
        textDN.Text = "";
        GridView1.Visible = false;
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
        //得到新的值 
        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["dt"];
        GridView1.DataBind();

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
}