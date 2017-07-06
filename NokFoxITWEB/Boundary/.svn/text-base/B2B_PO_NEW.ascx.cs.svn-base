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
using System.IO;

public partial class Boundary_B2B_PO_NEW : System.Web.UI.UserControl
{
    //#region Web Form Designer generated code

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

    //#endregion

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
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            //InitiaPage();
            MultiLanguage();
            bindplantcode();
            bindcotmod();
        }
    }

    private void bindcotmod()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = "select distinct ctomod from purchaseordersmain ";
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "ctomod");
        ddlCtoMod.DataTextField = "ctomod";
        ddlCtoMod.DataValueField = "ctomod";
        ddlCtoMod.DataSource = ds.Tables[0].DefaultView;
        ddlCtoMod.DataBind();
        ddlCtoMod.Items.Insert(0, "All");
    }

    private void bindplantcode()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = "select distinct plantcode from purchaseordersmain where upper(plantcode) not in('NULL') ";
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "plantcode");
        ddlPlantCode.DataTextField = "plantcode";
        ddlPlantCode.DataValueField = "plantcode";
        ddlPlantCode.DataSource = ds.Tables[0].DefaultView;
        ddlPlantCode.DataBind();
        ddlPlantCode.Items.Insert(0, "All");
    }

    private void MultiLanguage()
    {
        lblgeshi1.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
        lblgeshi2.Text = (String)GetGlobalResourceObject("SFCQuery", "ErrorDateTime");//rm.GetString("ErrorDateTime");
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvPO.PageIndex = 0;
        if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = true;
            lblgeshi2.Visible = false;
            btnExport.Visible = false;
            gvPO.Visible = false;
            panel3.Visible = false;
            return;
        }

        if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
        {
            lblgeshi1.Visible = false;
            lblgeshi2.Visible = true;
            btnExport.Visible = false;
            gvPO.Visible = false;
            panel3.Visible = false;
            return;
        }
        lblgeshi1.Visible = false;
        lblgeshi2.Visible = false;

        bind();        
    }

    private string Split(string strPoNo)
    {
        string strInput = strPoNo;
        string strOutput = "";
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
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strPlantCode = ddlPlantCode.SelectedValue.Trim().ToUpper();
        string strCsoNum = txtCsoNum.Text.Trim();
        string strMesgID = txtMesgID.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strUploadFlag = ddlUploadFlag.SelectedValue.Trim().ToUpper();
        string strCtoMod = ddlCtoMod.SelectedValue.Trim().ToUpper();
        string strSoNo = txtSoNo.Text.Trim();
        string strIdocNo = txtIdocNo.Text.Trim();
        string strSFCFlag = ddlSfcFlag.SelectedValue.Trim().ToUpper() ;
        string strACKFlag = ddlAckFlag.SelectedValue.Trim().ToUpper();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows);
        
        string strsql = "select distinct t1.*,t2.sendflag as acksendflag,t2.senddate as acksenddate,t2.errormsg as ackerrormsg ,t2.reason,t2.reason_desc "
            + " from purchaseordersmain t1(nolock) left join purchaseordersack t2(nolock) on t1.ordnum=t2.ordnum and t1.uploadflag="
            + " isnull(t2.uploadflag,'F') where  isnull(t1.errormsg,'F')<>'Duplicate Order Number' "
            + " and  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";
        if (!strMesgID.Equals(""))
        {
            strMesgID = Split(strMesgID);
            if (strMesgID.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.mesgid in (" + strMesgID + ")";
        }

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.ordnum in (" + strPoNo + ")";
        }

        if (!strUploadFlag.Equals("ALL"))
        {
            strUploadFlag = strUploadFlag.Substring(0, 1);
            strsql += " and t1.uploadflag ='" + strUploadFlag + "'";
        }

        if (!strSoNo.Equals(""))
        {
            strSoNo = Split(strSoNo);
            if (strSoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.sono in (" + strSoNo + ")";
        }

        if (!strIdocNo.Equals(""))
        {
            strIdocNo = Split(strIdocNo);
            if (strIdocNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.idocno in (" + strIdocNo + ")";
        }

        if (!strSFCFlag.Equals("ALL"))
        {

            strSFCFlag = strSFCFlag.Substring(0, 1);
            strsql += " and t1.insfcflag ='" + strSFCFlag + "'";
        }

        if (!strPlantCode.Equals("ALL"))
        {
            strsql += " and t1.plantcode ='" + strPlantCode + "'";
        }

        if (!strACKFlag.Equals("ALL"))
        {
            strACKFlag = strACKFlag.Substring(0, 1);
            strsql += " and t2.sendflag ='" + strACKFlag + "'";
        }

        if (!strCsoNum.Equals(""))
        {
            strCsoNum = Split(strCsoNum);
            if (strCsoNum.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.csonum in (" + strCsoNum + ")";
        }

        if (!strCtoMod.Equals("ALL"))
        {
            strsql += " and t1.ctomod ='" + strCtoMod + "'";
        }

        strsql += " order by t1.lasteditdt " + strOrder;

        return strsql;
    }

    private void bind()//数据绑定方法
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
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
            btnExport.Visible = false;
            panel3.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
            this.gvPO.AllowPaging = true;
            string  pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvPO.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

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
                if (count * gvPO.PageSize < ds.Tables[0].Rows.Count)
                    count = count + 1;
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
            if(((LinkButton)e.Row.Cells[45].Controls[0]).Text.Equals("更新"))
                ((LinkButton)e.Row.Cells[45].Controls[0]).Attributes.Add("onclick", " return confirm('确定要更新嗎?')");

        }     
    }

    protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        Label uplabel;
        Label rlabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvPO.PageIndex * gvPO.PageSize + e.Row.RowIndex + 1);

            uplabel = (Label)e.Row.Cells[45].FindControl("lblUploadFlag");
            rlabel = (Label)e.Row.Cells[1].FindControl("lblMesgid");
            string strreceid = rlabel.Text.ToString();
            string strUpload = uplabel.Text.ToUpper();

            if (strUpload.Equals("E"))
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[45].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[45].Enabled = true;

            }
            else
            {
                e.Row.Cells[45].Enabled = false;
                //e.Row.Cells[46].Text = "";
            }

            //if (strreceid.Equals("9972231d-896f-423a-b84b-6df0f6901"))
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Red;
            //    e.Row.Cells[46].ForeColor = System.Drawing.Color.Blue;
            //    e.Row.Cells[46].Enabled = true;

            //}
            //else
            //{
            //    e.Row.Cells[46].Enabled = false;
            //    e.Row.Cells[46].Text = "";
            //}     
      
            e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[25].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[26].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[43].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[45].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[47].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
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

    protected void btnExport_Click(object sender, EventArgs e)
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
            if (strsql.Equals("input error"))
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

    protected void gvPO_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strMesgid = ((Label)gvPO.Rows[e.RowIndex].FindControl("lblMesgid")).Text;
        string strSendid = ((Label)gvPO.Rows[e.RowIndex].FindControl("lblSendid")).Text;
        string strReceid = ((Label)gvPO.Rows[e.RowIndex].FindControl("lblReceid")).Text;
        string strPono = ((LinkButton)gvPO.Rows[e.RowIndex].FindControl("lbtnPoNo")).Text;
        string strUploadOld=((Label)gvPO.Rows[e.RowIndex].FindControl("lblUploadFlag")).Text;
        string strUpload = ((DropDownList)gvPO.Rows[e.RowIndex].FindControl("ddlUploadFlag")).SelectedValue;
        if (strUploadOld.Equals("E") && strUpload.Equals("N"))
        {
            //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
            //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            string strsql = "update purchaseordersmain set uploadflag='" + strUpload + "' where mesgid='" + strMesgid + "' and sendid='" + strSendid + "' and receid='" + strReceid + "' and ordnum='" + strPono + "'";
            SqlCommand com = new SqlCommand(strsql, conn);
            try
            {
                conn.Open();
                try
                {
                    com.ExecuteNonQuery();
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫操作異常！！');</script>");
                    return;
                }
                string strSql = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
                ViewState["UserName"] = dt.Rows[0]["USERNAME"].ToString();

                //string path = Request.PhysicalApplicationPath + "Log\\" + DateTime.Now.ToString("yyyy/MM/dd hhmmss") + ".log";
                
                //string path = Request.PhysicalApplicationPath + "Log\\purchaseordersmain.log";

                if (!Directory.Exists(@"c:\sfcquery Log\purchaseordersmain.log"))
                {
                    Directory.CreateDirectory(@"c:\sfcquery Log\purchaseordersmain.log");
                }

                string path = @"c:\sfcquery Log\purchaseordersmain.log";
                StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default);
                writer.WriteLine("---------------Update Action Start-------------------");
                writer.WriteLine("  Update User: " + ViewState["UserName"] + "");
                writer.WriteLine("  table:purchaseordersmain");
                writer.WriteLine("  PK: mesgid='" + strMesgid + "',sendid='" + strSendid + "',receid='" + strReceid + "',ordnum='" + strPono + "'");
                writer.WriteLine("  Update Columns:uploadflag,old value='" + strUploadOld + "',now value='" + strUpload + "'");
                writer.WriteLine("  Update Time:" + DateTime.Now.ToString());
                writer.WriteLine("---------------End-------------------");
                //File.SetAttributes(path, FileAttributes.ReadOnly);
                writer.Close();
                writer.Dispose();

            }
            catch (SqlException c)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫連接異常！！');</script>");
                return;
            }        
            conn.Close();
            conn.Dispose();
        }
        gvPO.EditIndex=-1;
        bind();
    }

    protected void gvPO_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvPO.Rows[e.RowIndex].FindControl("lblMesgid")).Text;
        gvPO.EditIndex = e.NewEditIndex;
        bind();
    }

    protected void gvPO_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPO.EditIndex = -1;
        bind();
    }
}
