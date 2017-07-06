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
using System.Data.OracleClient;
using DBAccess.EAI;
using System.IO;

public partial class Boundary_B2B_CarrierCode : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage();
            bind();
            //bindmcid(ddlMCID); 

            //string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
            //OracleConnection conn = new OracleConnection(strConn);
            //string strsql = "select distinct MCID from DELL_B2B_CARRIER_CODE where ORIGIN ='FTY'";
            //OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);
            //DataSet ds = new DataSet();
            //sda.Fill(ds, "MCID");
            //ddlMCID.DataTextField = "MCID";
            //ddlMCID.DataValueField = "MCID";
            //ddlMCID.DataSource = ds.Tables[0].DefaultView;
            //ddlMCID.DataBind();
            //ddlMCID.Items.Insert(0, "ALL");
        }
    }

    private void MultiLanguage()
    {
        btnSearch.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    protected void gvCarrierCode_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

          if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((LinkButton)e.Row.Cells[10].Controls[0]).Text.Equals("更新"))
            {
                ((LinkButton)e.Row.Cells[10].Controls[0]).Attributes.Add("onclick", " return confirm('确定要更新嗎?')");
                //DropDownList ddlEMCID;
                //ddlEMCID = (DropDownList)e.Row.Cells[3].FindControl("ddlEMCID");
                //bindmcid(ddlEMCID);      
            }

            //if (e.Row.RowState == DataControlRowState.Edit)
            //{
                //DropDownList ddlEMCID;
                //ddlEMCID = (DropDownList)e.Row.Cells[3].FindControl("ddlEMCID");
                //bindmcid(ddlEMCID);    
            //}
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind();   
    }

    //private void bindmcid(DropDownList ddlmcid)
    //{ 
    //    string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
    //    OracleConnection conn = new OracleConnection(strConn);
    //    string strsql = "select distinct MCID from DELL_B2B_CARRIER_CODE where ORIGIN ='FTY'";
    //    OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);
    //    DataSet ds = new DataSet();
    //    sda.Fill(ds, "MCID");
    //    ddlmcid.DataTextField = "MCID";
    //    ddlmcid.DataValueField = "MCID";
    //    ddlmcid.DataSource = ds.Tables[0].DefaultView;
    //    ddlmcid.DataBind();
    //}

    private void bind()
    { 
        string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
        OracleConnection conn = new OracleConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        OracleDataAdapter sda = new OracleDataAdapter(strsql, conn);
 
        DataSet ds = new DataSet();
        sda.Fill(ds, "carriercode");
        this.gvCarrierCode.DataSource = this.GetIDTable(ds.Tables[0]).DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvCarrierCode.DataSource = ds;
            gvCarrierCode.DataBind();
            int columnCount = gvCarrierCode.Rows[0].Cells.Count;
            gvCarrierCode.Rows[0].Cells.Clear();
            gvCarrierCode.Rows[0].Cells.Add(new TableCell());
            gvCarrierCode.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvCarrierCode.Rows[0].Cells[0].Text = "No Records Found.";
            gvCarrierCode.Rows[0].Visible = true;
        }
        else
        {
            this.gvCarrierCode.DataBind();
        }
        conn.Close();
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
        string strRegion = txtRegion.Text.Trim().ToUpper();
        string strCarrierCode = txtCarrierCode.Text.Trim().ToUpper();
        //string strMCID = ddlMCID.SelectedValue.Trim().ToUpper();
        string strMCID = txtMCID.Text.Trim().ToUpper();
        string strShipMode = ddlShipMode.SelectedValue.Trim().ToUpper();  

        string strsql = "select * from dell_b2b_carrier_code where 1=1 ";
        if (!strRegion.Equals(""))
        {
            strRegion = Split(strRegion);
            if (strRegion.Equals("split error"))
                return "input error";
            else
                strsql += " and region in (" + strRegion + ")";
        }

        if (!strCarrierCode.Equals(""))
        {
            strCarrierCode = Split(strCarrierCode);
            if (strCarrierCode.Equals("split error"))
                return "input error";
            else
                strsql += " and scacid in (" + strCarrierCode + ")";
        }

        if (!strShipMode.Equals("ALL"))
        {
            strShipMode = strShipMode.Substring(0, 1);
            strsql += " and ship_mode ='" + strShipMode + "'";
        }

        if (!strMCID.Equals(""))
        {
            strMCID = Split(strMCID);
            if (strMCID.Equals("split error"))
                return "input error";
            else
                strsql += " and MCID in (" + strMCID + ")";
        }

        return strsql;
    }

    private DataTable GetIDTable(DataTable dt)
    {
        DataColumn col = new DataColumn("新增", Type.GetType("System.Int32"));
        dt.Columns.Add(col);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (0 == i)
                dt.Rows[0][col] = 1;
            else
                dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
        }
        return dt;
    }

    protected void gvCarrierCode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCarrierCode.EditIndex = -1;
        bind();
    }

    protected void gvCarrierCode_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ((LinkButton)gvCarrierCode.HeaderRow.FindControl("lkbAddItem")).Enabled = false;
        gvCarrierCode.EditIndex = e.NewEditIndex;
        bind();
    }

    protected void gvCarrierCode_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ViewState["UserName"] = dt.Rows[0]["USERNAME"].ToString();

        string strlblEregion = ((Label)gvCarrierCode.Rows[e.RowIndex].FindControl("lblEregion")).Text.Trim();
        string strtxtEregion = ((TextBox)gvCarrierCode.Rows[e.RowIndex].FindControl("txtEregion")).Text.Trim();
        string strlblEMCID = ((Label)gvCarrierCode.Rows[e.RowIndex].FindControl("lblEMCID")).Text.Trim();
        //string strddlEMCID = ((DropDownList)gvCarrierCode.Rows[e.RowIndex].FindControl("ddlEMCID")).SelectedValue.ToString();
        string strtxtEMCID = ((TextBox)gvCarrierCode.Rows[e.RowIndex].FindControl("txtEMCID")).Text.Trim();
        string strlblShipMode = ((Label)gvCarrierCode.Rows[e.RowIndex].FindControl("lblEShipMode")).Text.ToString();
        string strddlEShipMode = ((DropDownList)gvCarrierCode.Rows[e.RowIndex].FindControl("ddlEShipMode")).SelectedValue.ToString().Substring(0, 1);
        string strlblSCACID = ((Label)gvCarrierCode.Rows[e.RowIndex].FindControl("lblSCACID")).Text.Trim();
        string strtxtESCACID = ((TextBox)gvCarrierCode.Rows[e.RowIndex].FindControl("txtESCACID")).Text.Trim();
        string strlblEupdatedby = ((Label)gvCarrierCode.Rows[e.RowIndex].FindControl("lblEupdatedby")).Text.Trim();
        string strtxtEupdatedby = ((TextBox)gvCarrierCode.Rows[e.RowIndex].FindControl("txtEupdatedby")).Text.Trim();

        if (strlblEregion != strtxtEregion || strlblEMCID != strtxtEMCID || strlblShipMode != strddlEShipMode || strlblSCACID != strtxtESCACID)
        {
            string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
            OracleConnection conn = new OracleConnection(strConn);

            if (strlblEregion != strtxtEregion || strlblEMCID != strtxtEMCID || strlblShipMode != strddlEShipMode)
            {
                string strsqlnew = "select * from dell_b2b_carrier_code where origin='FTY' and region='" + strtxtEregion + "' and mcid='" + strtxtEMCID + "'";

                OracleDataAdapter sdanew = new OracleDataAdapter(strsqlnew, conn);

                DataSet dsnew = new DataSet();
                sdanew.Fill(dsnew, "carriercodenew");

                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('已經存在該記錄,請檢查你的輸入!!');</script>");
                    return;
                }
            }

            string strsql = "update dell_b2b_carrier_code set region='" + strtxtEregion + "',mcid='" + strtxtEMCID + "',ship_mode='"
                + strddlEShipMode + "',scacid='" + strtxtESCACID + "',updated_by='" + strtxtEupdatedby + "',updated_date=sysdate  where region='"
                + strlblEregion + "' and mcid='" + strlblEMCID + "' and ship_mode='"+ strlblShipMode + "'";
            if (!strlblSCACID.Equals(""))
                strsql = strsql + " and scacid='" + strlblSCACID + "'";

            OracleCommand com = new OracleCommand(strsql, conn);
            try
            {
                conn.Open();
                try
                {
                    if (strtxtEregion.Length == 0 )
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Region不能為空,請檢查你的輸入！！');</script>");
                        return;
                    }
                    else
                    {
                        com.ExecuteNonQuery(); 
                        try
                        {
                            if (!Directory.Exists(@"c:\sfcquery Log"))
                            {
                                Directory.CreateDirectory(@"c:\sfcquery Log");
                            }
                            string path = @"c:\sfcquery Log\carriercode.log";
                            StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default);
                            writer.WriteLine("---------------Update Action Start-------------------");
                            writer.WriteLine("  Update User: " + strtxtEupdatedby + "");
                            writer.WriteLine("  table:dell_b2b_carrier_code");
                            //writer.WriteLine("  PK: blanketpo='" + strtxtEBlanketPO + "'");
                            if (strlblEregion != strtxtEregion)
                                writer.WriteLine("  Update Columns:region,old value='" + strlblEregion + "',now value='" + strtxtEregion + "'");
                            if (strlblEMCID != strtxtEMCID)
                                writer.WriteLine("  Update Columns:mcid,old value='" + strlblEMCID + "',now value='" + strtxtEMCID + "'");
                            if (strlblEregion != strtxtEregion)
                                writer.WriteLine("  Update Columns:region,old value='" + strlblEregion + "',now value='" + strtxtEregion + "'");
                            if (strlblShipMode != strddlEShipMode)
                                writer.WriteLine("  Update Columns:ship_mode,old value='" + strlblShipMode + "',now value='" + strddlEShipMode + "'");
                            if (strlblSCACID != strtxtESCACID)
                                writer.WriteLine("  Update Columns:scacid,old value='" + strlblSCACID + "',now value='" + strtxtESCACID + "'");

                            writer.WriteLine("  Update Time:" + DateTime.Now.ToString());
                            writer.WriteLine("---------------End-------------------");
                            //File.SetAttributes(path, FileAttributes.ReadOnly);
                            writer.Close();
                            writer.Dispose();
                        }
                        catch
                        {
                            conn.Close();
                            conn.Dispose();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('寫日誌異常！！');</script>");
                            return;
                        }
                    }
                }
                catch
                {
                    conn.Close();
                    conn.Dispose();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫操作異常！！');</script>");
                    return;
                }
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫連接異常！！');</script>");
                return;
            }
            conn.Close();
            conn.Dispose();
        }
        gvCarrierCode.EditIndex = -1;
        bind();
    }

    protected void gvCarrierCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;

        //old -------------
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    olabel = (Label)e.Row.Cells[0].FindControl("lblID");
        //    olabel.Text = Convert.ToString(e.Row.RowIndex + 1);
       
        //    if (e.Row.RowState == DataControlRowState.Edit)
        //    {
        //        DropDownList ddlEMCID;
        //        ddlEMCID = (DropDownList)e.Row.Cells[3].FindControl("ddlEMCID");
        //        bindmcid(ddlEMCID);

        //        string strmcid = ((Label)e.Row.Cells[3].FindControl("lblEMCID")).Text.ToString();
        //        string strshipmode = ((Label)e.Row.Cells[4].FindControl("lblEShipMode")).Text.ToString();

        //        DropDownList ddlShipMode;

        //        ddlShipMode = (DropDownList)e.Row.Cells[4].FindControl("ddlEShipMode");
        //        ddlEMCID.SelectedValue = strmcid;
        //        if (strshipmode == "A")
        //            strshipmode = "A-Air";
        //        else
        //            if (strshipmode == "G")
        //                strshipmode = "G-Ground";
        //            else
        //                if (strshipmode == "S")
        //                    strshipmode = "S-Sea";

        //        ddlShipMode.SelectedValue = strshipmode;
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblID");
            olabel.Text = Convert.ToString(e.Row.RowIndex + 1);

            //DropDownList ddlEMCID;
            //ddlEMCID = (DropDownList)e.Row.Cells[3].FindControl("ddlEMCID");
            //if (ddlEMCID != null)
            //{
            //    bindmcid(ddlEMCID);
            //    string strmcid = ((Label)e.Row.Cells[3].FindControl("lblEMCID")).Text.ToString();
            //    ddlEMCID.SelectedValue = strmcid;
            //}

            DropDownList ddlShipMode;
            ddlShipMode = (DropDownList)e.Row.Cells[4].FindControl("ddlEShipMode");
            if (ddlShipMode != null)
            {
                string strshipmode = ((Label)e.Row.Cells[4].FindControl("lblEShipMode")).Text.ToString();

                if (strshipmode == "A")
                    strshipmode = "A-Air";
                else
                    if (strshipmode == "G")
                        strshipmode = "G-Ground";
                    else
                        if (strshipmode == "S")
                            strshipmode = "S-Sea";

                ddlShipMode.SelectedValue = strshipmode;
            }
        }
       
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //DropDownList ddlFMCID;
            //ddlFMCID = (DropDownList)e.Row.Cells[3].FindControl("ddlFMCID");
            //bindmcid(ddlFMCID);
        }

    }

    protected void gvCarrierCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            gvCarrierCode.ShowFooter = true;
            gvCarrierCode.Columns[6].Visible = false;
            gvCarrierCode.Columns[7].Visible = false;
            gvCarrierCode.Columns[8].Visible = false;
            gvCarrierCode.Columns[9].Visible = false;
            gvCarrierCode.Columns[10].Visible = false;
            ((LinkButton)gvCarrierCode.HeaderRow.FindControl("lkbAddItem")).Enabled = false;
            //bindmcid(ddlFMCID);
            bind();
        }
        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            gvCarrierCode.ShowFooter = false;
            gvCarrierCode.Columns[6].Visible = true;
            gvCarrierCode.Columns[7].Visible = true;
            gvCarrierCode.Columns[8].Visible = true;
            gvCarrierCode.Columns[9].Visible = true;
            gvCarrierCode.Columns[10].Visible = true;
            //gvBlanketPO.HeaderRow.FindControl("lkbAddItem").Visible = true;
            bind();
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            string strSql = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            ViewState["UserName"] = dt.Rows[0]["USERNAME"].ToString();

            gvCarrierCode.ShowFooter = false;
            gvCarrierCode.Columns[10].Visible = true;
            //gvBlanketPO.HeaderRow.FindControl("lkbAddItem").Visible = true;
            ((LinkButton)gvCarrierCode.HeaderRow.FindControl("lkbAddItem")).Enabled = true;

            OracleDataAdapter da;
            DataSet myds;
            myds = new DataSet();
 
            string strConn = ConfigurationManager.ConnectionStrings["DellSapConnectionString"].ConnectionString;
            OracleConnection oracn = new OracleConnection(strConn);
            oracn.Open();

            da = new OracleDataAdapter("select * from dell_b2b_carrier_code where 0=1 ", oracn);
            da.Fill(myds, "dellb2bcarriercode");

            //----------- Insert new row----------

            DataRow mydr = myds.Tables["dellb2bcarriercode"].NewRow();

            mydr["ORIGIN"] = "FTY";

            if (((TextBox)this.gvCarrierCode.FooterRow.FindControl("txtFregion")).Text.Trim().Length > 0)
                mydr["REGION"] = ((TextBox)this.gvCarrierCode.FooterRow.FindControl("txtFregion")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Region欄位不能為空...');</script>");
                return;
            }

            //mydr["MCID"] = ((DropDownList)this.gvCarrierCode.FooterRow.FindControl("ddlFMCID")).SelectedValue.ToString();
            mydr["MCID"] = ((TextBox)this.gvCarrierCode.FooterRow.FindControl("txtFMCID")).Text.Trim();
            mydr["SHIP_MODE"] = ((DropDownList)this.gvCarrierCode.FooterRow.FindControl("ddlFShipMode")).SelectedValue.ToString().Substring(0,1);

            if (((TextBox)this.gvCarrierCode.FooterRow.FindControl("txtFSCACID")).Text.Trim().Length > 0)
                mydr["SCACID"] = ((TextBox)this.gvCarrierCode.FooterRow.FindControl("txtFSCACID")).Text.Trim();
            else
                mydr["SCACID"] = "";                   

           
            mydr["CREATED_DATE"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            mydr["CREATED_BY"] = ViewState["UserName"].ToString();

            // 判斷DB中是否存在 
            string strsqlnew = "select * from dell_b2b_carrier_code where origin='FTY' and region='"+mydr["REGION"]+"' and mcid='"+mydr["MCID"]+"' and ship_mode='"+ mydr["SHIP_MODE"]+"'";

            OracleDataAdapter sdanew = new OracleDataAdapter(strsqlnew, oracn);
     
            DataSet dsnew = new DataSet();
            sdanew.Fill(dsnew, "carriercodenew");

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('已經存在該記錄,請選擇'更新'操作！！');</script>");
                return;
            }
            // end

            myds.Tables["dellb2bcarriercode"].Rows.Add(mydr);

            // ---------------end -------------------
 
            OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            da.Update(myds, "dellb2bcarriercode");
            oracn.Close();

            try
            {
                if (!Directory.Exists(@"c:\sfcquery Log"))
                {
                    Directory.CreateDirectory(@"c:\sfcquery Log");
                }
                string path = @"c:\sfcquery Log\carriercode.log";
                StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default);
                writer.WriteLine("---------------Insert Action Start-------------------");
                writer.WriteLine("  Insert User: " + ViewState["UserName"] + "");
                writer.WriteLine("  table:dell_b2b_carrier_code");
                writer.WriteLine("  Insert Columns:origin='" + mydr["ORIGIN"] + "';region='" + mydr["REGION"] + "';mcid='" + mydr["MCID"] + "';ship_mode='" + mydr["SHIP_MODE"] + "';scacid='" + mydr["SCACID"] + "';created_date='" + mydr["CREATED_DATE"] + "';created_by='" + mydr["CREATED_BY"] + "'");
                writer.WriteLine("  Insert Time:" + mydr["CREATED_DATE"]);
                writer.WriteLine("---------------End-------------------");
                //File.SetAttributes(path, FileAttributes.ReadOnly);
                writer.Close();
                writer.Dispose();
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('寫日誌異常！！');</script>");
                return;
            }

            gvCarrierCode.EditIndex = -1;
            bind();
        }
    }
}
