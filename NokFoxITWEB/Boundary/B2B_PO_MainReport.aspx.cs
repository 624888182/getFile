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
using System.Data.SqlClient;
using DBAccess.EAI;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using DB.EAI;
using System.IO;

public partial class Boundary_B2B_PO_MainReport : System.Web.UI.Page
{
    #region Web Form Designer generated code

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    #endregion

    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }

            if (gv.Controls[i].HasControls())
            {
                DisableControls(gv.Controls[i]);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strUploadFlag = Request.QueryString["UploadFlag"].ToString();
        string strSoNo = Request.QueryString["SoNo"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strSFCFlag = Request.QueryString["SFCFlag"].ToString();
        string strPlantCode = Request.QueryString["PlantCode"].ToString();
        string strACKFlag = Request.QueryString["ACKFlag"].ToString();
        string strOrder = Request.QueryString["Order"].ToString();
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());


        lblFromDate1.Text = strStartDate;
        lblToDate1.Text = strEndDate;

        if (!IsPostBack)
        {
            bind();//调用数据绑定方法
        }
    }  

    private void bind()//数据绑定方法
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        if(strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "PURCHASEORDERSMAIN");
        this.gvPO.DataSource = ds.Tables[0].DefaultView; 

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvPO.DataSource = ds;
            gvPO.DataBind();
            int columnCount = gvPO.Rows[0].Cells.Count;
            gvPO.Rows[0].Cells.Clear();
            gvPO.Rows[0].Cells.Add(new TableCell());
            gvPO.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvPO.Rows[0].Cells[0].Text = "No Records Found.";
            gvPO.Rows[0].Visible = true;
            btnAll.Visible = false;
            lblAll.Visible = false;
            lblSingle.Visible = false;
            btnSingle.Visible = false;
            panel3.Visible = false;

        }
        else
        {
            panel2.Visible = true;
            this.gvPO.AllowPaging = true;
            this.gvPO.PageSize = Convert.ToInt16(Request.QueryString["PageRows"].ToString());//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

            if (ds.Tables[0].Rows.Count <= gvPO.PageSize)
                panel3.Visible = false;
            else
            {
                panel3.Visible = true;
                if (this.gvPO.PageIndex == 0)
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                else
                {
                    this.btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                if (gvPO.PageIndex == gvPO.PageCount - 1)
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                else
                {
                    this.btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
                lblNow.Visible = true;
                int now = gvPO.PageIndex + 1;
                lblNow.Text = "第" + now.ToString() + "頁 / ";
                lblTotal.Visible = true;
                txtNo.Text = now.ToString();
                int count = ds.Tables[0].Rows.Count / gvPO.PageSize;
                lblTotal.Text = "共" + count + "頁   轉到";
            }
            this.gvPO.DataBind();
        }
        conn.Close();
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        //gvPO.PageIndex = Convert.ToInt32(((ImageButton)sender).CommandName) - 1; 
        switch (((ImageButton)sender).CommandArgument.ToString())
        {
            case "First":
                gvPO.PageIndex = 0;
                break;
            case "Previous":
                if (gvPO.PageIndex >= 1)
                {
                    gvPO.PageIndex = gvPO.PageIndex - 1;
                }
                else
                {
                    this.btnFirst.Enabled = false;
                    this.btnPrevious.Enabled = false;
                }
                break;
            case "Next":
                if (gvPO.PageIndex < gvPO.PageCount - 1)
                {
                    gvPO.PageIndex = gvPO.PageIndex + 1;
                }
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "Last":
                if (gvPO.PageIndex < gvPO.PageCount - 1)
                    gvPO.PageIndex = gvPO.PageCount - 1;
                else
                {
                    this.btnNext.Enabled = false;
                    this.btnLast.Enabled = false;
                }
                break;
            case "-1":
                {
                    try
                    {
                        int index = int.Parse(txtNo.Text.Trim());
                        if (index > 0 && index < gvPO.PageCount + 1)
                            gvPO.PageIndex = index - 1;
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('輸入錯誤！！');</script>");
                            return;
                        }
                    }
                    catch
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('輸入錯誤！！');</script>");
                        return;
                    }
                }
                break;
        }
        bind();
    }

    //private DataTable GetIDTable(DataTable dt)
    //{
    //    DataColumn col = new DataColumn("Index", Type.GetType("System.Int32"));
    //    dt.Columns.Add(col);
    //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //    {
    //        if (0 == i)
    //            dt.Rows[0][col] = 1;
    //        else
    //            dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
    //    }
    //    return dt;
    //}

    protected void gvPO_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        LinkButton lbtpono;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtpono = (LinkButton)e.Row.Cells[5].Controls[1];
            if (lbtpono != null)
            {
                if (lbtpono.CommandName == "PONO")
                    lbtpono.CommandArgument = e.Row.RowIndex.ToString();
            
            }
        }       
    }

    protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPO.PageIndex * gvPO.PageSize + e.Row.RowIndex + 1);

            e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[24].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[25].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[44].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[46].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[48].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

            LinkButton lbtpono;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lbtpono = (LinkButton)e.Row.Cells[5].Controls[1];
                if (lbtpono != null)
                {
                    if (lbtpono.CommandName == "PONO")
                        lbtpono.CommandArgument = e.Row.RowIndex.ToString();
                }
            }   
        }
    }

    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Write("<script>window.close();</script>");// 会弹出询问是否关闭 
        Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问 
    }

    protected void btnSingle_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        DisableControls(gvPO);
        string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        Response.ClearContent();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-excel";
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        //Response.AppendHeader("Content-Disposition", "attachment;filename=" + strFileName + "'");

        Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_PO_MainReport.xls");
        this.EnableViewState = false;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvPO.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();  
    }

    protected void btnAll_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear();
        try
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_PO_MainReport.xls");
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
            //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            string strsql = GetData();
            if(strsql.Equals("input error"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
                return;
            }
            SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

            DataSet ds = new DataSet();
            sda.Fill(ds, "PURCHASEORDERSMAIN1");
            this.gvPO.DataSource = ds.Tables[0].DefaultView;

            this.gvPO.AllowPaging = false;
            this.gvPO.DataBind();

            DisableControls(gvPO);
            gvPO.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            //gvPO.AllowPaging = true;
            //bind(); 
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            return;
        }       

    }
   
    private string Split(string strPoNo)
    {
 	    string strInput=strPoNo;
        string strOutput="";
        try
        {
            string[] strInput1 = strInput.Split(new Char[] { ',' });
            for (int i = 0; i < strInput1.Length; i++)
                strOutput = strOutput + "'" + strInput1[i].Trim().ToString() + "',";
            strOutput = strOutput.Substring(0, strOutput.Length - 1);

            return strOutput;
        }
        catch
        {
            return "split error";
        }
    } 

    private string GetData()
    {
        string strStartDate = Request.QueryString["StartDate"].ToString();
        string strEndDate = Request.QueryString["EndDate"].ToString();
        //DateTime dtStartDate = Convert.ToDateTime(Request.QueryString["StartDate"].ToString());
        //DateTime dtEndDate = Convert.ToDateTime(Request.QueryString["EndDate"].ToString());
        string strPlantCode = Request.QueryString["PlantCode"].ToString().ToUpper();
        string strCsoNum = Request.QueryString["CsoNum"].ToString();
        string strMesgID = Request.QueryString["MesgID"].ToString();
        string strPoNo = Request.QueryString["PoNo"].ToString();
        string strUploadFlag = Request.QueryString["UploadFlag"].ToString().ToUpper();
        string strCtoMod = Request.QueryString["CtoMod"].ToString().ToUpper();
        string strSoNo = Request.QueryString["SoNo"].ToString();
        string strIdocNo = Request.QueryString["IdocNo"].ToString();
        string strSFCFlag = Request.QueryString["SFCFlag"].ToString().ToUpper();
        string strACKFlag = Request.QueryString["ACKFlag"].ToString().ToUpper();
        string strOrder = Request.QueryString["Order"].ToString();
        int PageRows = Convert.ToInt16(Request.QueryString["PageRows"].ToString());

        string strsql = "select distinct t1.*,t2.sendflag as acksendflag,t2.senddate as acksenddate,t2.errormsg as ackerrormsg "
            + " from purchaseordersmain t1(nolock) left join purchaseordersack t2(nolock) on t1.ordnum=t2.ordnum and t1.uploadflag="
            + " isnull(t2.uploadflag,'F') where  isnull(t1.errormsg,'F')<>'Duplicate Order Number' "
            + " and  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";
        if(!strMesgID.Equals(""))
        {
            strMesgID=Split(strMesgID);
            if(strMesgID.Equals("split error"))
                return "input error";
            else
                strsql+=" and t1.mesgid in ("+strMesgID+")";
        }

        if(!strPoNo.Equals(""))
        {
            strPoNo=Split(strPoNo);
            if(strPoNo.Equals("split error"))
                return "input error";
            else
                strsql+=" and t1.ordnum in ("+strPoNo+")";
        }

        if(!strUploadFlag.Equals("ALL"))
        {
            strUploadFlag = strUploadFlag.Substring(0, 1);
            strsql+=" and t1.uploadflag ='"+strUploadFlag+"'";
        }

        
        if(!strSoNo.Equals(""))
        {
            strSoNo=Split(strSoNo);
            if(strSoNo.Equals("split error"))
                return "input error";
            else
                strsql+=" and t1.sono in ("+strSoNo+")";
        }

        if(!strIdocNo.Equals(""))
        {
            strIdocNo=Split(strIdocNo);
            if(strIdocNo.Equals("split error"))
                return "input error";
            else
                strsql+=" and t1.idocno in ("+strIdocNo+")";
        }

        if(!strSFCFlag.Equals("ALL"))
        {

            strSFCFlag = strSFCFlag.Substring(0, 1);
            strsql+=" and t1.insfcflag ='"+strSFCFlag+"'";
        }

        if (!strPlantCode.Equals("ALL"))
        {
            strsql += " and t1.plantcode ='" + strPlantCode + "'";
        }

        if(!strACKFlag.Equals("ALL"))
        {
            strACKFlag = strACKFlag.Substring(0, 1);
            strsql+=" and t2.sendflag ='"+strACKFlag+"'";
        }

        if(!strCsoNum.Equals(""))
        {
            strCsoNum=Split(strCsoNum);
            if(strCsoNum.Equals("split error"))
                return "input error";
            else
                strsql+=" and t1.csonum in ("+strCsoNum+")";
        }

        if (!strCtoMod.Equals("ALL"))
        {
            strsql += " and t1.ctomod ='" + strCtoMod + "'";
        }

        strsql+=" order by t1.lasteditdt " +strOrder;

        return strsql;
    }

    protected void gvPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PONO")
        {

            string strPono = ((LinkButton)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
            string strSendid = ((Label)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strReceid = ((Label)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strMsegid = ((Label)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;

            string strScript = "<script language='jscript'>var res = window.open('./B2B_PO_ItemReport.aspx?Sendid=" + strSendid + "&Receid=" + strReceid
           + "&Msegid=" + strMsegid + "&Pono=" + strPono + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
}
