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
using DBAccess.EAI;
using System.Data.SqlClient;
using System.IO;

public partial class Boundary_B2B_BLANKETPO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            MultiLanguage();
            bind();   
        }
    }

    private void MultiLanguage()
    {
        btnSearch.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind();   
    }
    protected void gvBlanketPO_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if (((LinkButton)e.Row.Cells[10].Controls[0]).Text.Equals("更新"))
                ((LinkButton)e.Row.Cells[10].Controls[0]).Attributes.Add("onclick", " return confirm('确定要更新嗎?')");
           
        }
    }
    protected void gvBlanketPO_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvBlanketPO.EditIndex = -1;
        bind();
    } 
    protected void gvBlanketPO_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gvPO.Rows[e.RowIndex].FindControl("lblMesgid")).Text; 
        ((LinkButton)gvBlanketPO.HeaderRow.FindControl("lkbAddItem")).Enabled = false;
        gvBlanketPO.EditIndex = e.NewEditIndex;
        bind();
    }
    protected void gvBlanketPO_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "SELECT USERNAME FROM NEW_USERS WHERE LOGINNAME =" + ClsCommon.GetSqlString(Context.User.Identity.Name);
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        ViewState["UserName"] = dt.Rows[0]["USERNAME"].ToString();

        string strlblEBlanketPO = ((Label)gvBlanketPO.Rows[e.RowIndex].FindControl("lblEBlanketPO")).Text.Trim();
        string strtxtEBlanketPO = ((TextBox)gvBlanketPO.Rows[e.RowIndex].FindControl("txtEBlanketPO")).Text.Trim();
        string strlblELastEditBy = ((Label)gvBlanketPO.Rows[e.RowIndex].FindControl("lblElasteditby")).Text.Trim();
        string strtxtELastEditBy = ((TextBox)gvBlanketPO.Rows[e.RowIndex].FindControl("txtElasteditby")).Text.Trim();
        string strlblEregion = ((Label)gvBlanketPO.Rows[e.RowIndex].FindControl("lblEregion")).Text.Trim();
        string strtxtEregion = ((TextBox)gvBlanketPO.Rows[e.RowIndex].FindControl("txtEregion")).Text.Trim();
        string strlblEediid = ((Label)gvBlanketPO.Rows[e.RowIndex].FindControl("lblEediid")).Text.Trim();
        string strtxtEediid = ((TextBox)gvBlanketPO.Rows[e.RowIndex].FindControl("txtEediid")).Text.Trim();
        string strlblEgloviatpid = ((Label)gvBlanketPO.Rows[e.RowIndex].FindControl("lblEgloviatpid")).Text.Trim();
        string strtxtEgloviatpid = ((TextBox)gvBlanketPO.Rows[e.RowIndex].FindControl("txtEgloviatpid")).Text.Trim();

        if (strlblEBlanketPO != strtxtEBlanketPO || strlblELastEditBy != strtxtELastEditBy || strlblEregion != strtxtEregion || strlblEediid != strtxtEediid || strlblEgloviatpid != strtxtEgloviatpid)
        {
            //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
            //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";

            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            //string strsql = "update xmlblanketpoconfig set blanketpo='" + strtxtEBlanketPO + "',lasteditby='" + strtxtELastEditBy + "',lasteditdt=getdate()  where blanketpo='" + strlblEBlanketPO + "'";
            string strsql = "update xmlblanketpoconfig set blanketpo='" + strtxtEBlanketPO + "',lasteditby='" + strtxtELastEditBy + "',lasteditdt=getdate() ,region='" + strtxtEregion + "',ediid='" + strtxtEediid + "',gloviatpid='" + strtxtEgloviatpid + "' where blanketpo='" + strlblEBlanketPO + "'";
            SqlCommand com = new SqlCommand(strsql, conn);
            try
            {
                conn.Open();
                try
                {
                    if (strtxtEBlanketPO.Length == 0 || strtxtELastEditBy.Length == 0 || strtxtEregion.Length == 0 || strtxtEediid.Length == 0 || strtxtEgloviatpid.Length == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('欄位不能為空,請檢查你的輸入！！');</script>");
                        return;
                    }
                    else
                    {
                        com.ExecuteNonQuery();


                        //string path = Request.PhysicalApplicationPath + "Log\\" + DateTime.Now.ToString("yyyy/MM/dd hhmmss") + ".log";

                        //string path = Request.PhysicalApplicationPath + "Log\\purchaseordersmain.log";
                        try
                        {
                            if (!Directory.Exists(@"c:\sfcquery Log"))
                            {
                                Directory.CreateDirectory(@"c:\sfcquery Log");
                            }
                            string path = @"c:\sfcquery Log\blanketpo.log";
                            StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default);
                            writer.WriteLine("---------------Update Action Start-------------------");
                            writer.WriteLine("  Update User: " + strtxtELastEditBy + "");
                            writer.WriteLine("  table:xmlblanketpoconfig");
                            writer.WriteLine("  PK: blanketpo='" + strtxtEBlanketPO + "'");
                            if (strlblEBlanketPO != strtxtEBlanketPO)
                                writer.WriteLine("  Update Columns:blanketpo,old value='" + strlblEBlanketPO + "',now value='" + strtxtEBlanketPO + "'");
                            if (strlblELastEditBy != strtxtELastEditBy)
                                writer.WriteLine("  Update Columns:lasteditby,old value='" + strlblELastEditBy + "',now value='" + strtxtELastEditBy + "'");
                            if (strlblEregion != strtxtEregion)
                                writer.WriteLine("  Update Columns:region,old value='" + strlblEregion + "',now value='" + strtxtEregion + "'");
                            if (strlblEediid != strtxtEediid)
                                writer.WriteLine("  Update Columns:ediid,old value='" + strlblEediid + "',now value='" + strtxtEediid + "'");
                            if (strlblEgloviatpid != strtxtEgloviatpid)
                                writer.WriteLine("  Update Columns:gloviatpid,old value='" + strlblEgloviatpid + "',now value='" + strtxtEgloviatpid + "'");

                            writer.WriteLine("  Update Time:" + DateTime.Now.ToString());
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
                    }
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫操作異常！！');</script>");
                    return;
                } 
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('數據庫連接異常！！');</script>");
                return;
            }
            conn.Close();
            conn.Dispose();
        }
        gvBlanketPO.EditIndex = -1;
        bind();
    }

    private void bind()
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
        sda.Fill(ds, "blanketpo");
        this.gvBlanketPO.DataSource = this.GetIDTable(ds.Tables[0]).DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {

            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvBlanketPO.DataSource = ds;
            gvBlanketPO.DataBind();
            int columnCount = gvBlanketPO.Rows[0].Cells.Count;
            gvBlanketPO.Rows[0].Cells.Clear();
            gvBlanketPO.Rows[0].Cells.Add(new TableCell());
            gvBlanketPO.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvBlanketPO.Rows[0].Cells[0].Text = "No Records Found.";
            gvBlanketPO.Rows[0].Visible = true;

        }
        else
        { 
            this.gvBlanketPO.DataBind();
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
        string strBlanketPO = txtBlanketPO.Text.Trim();

        string strsql = "select * from xmlblanketpoconfig where 1=1 ";
        if (!strBlanketPO.Equals(""))
        {
            strBlanketPO = Split(strBlanketPO);
            if (strBlanketPO.Equals("split error"))
                return "input error";
            else
                strsql += " and blanketpo in (" + strBlanketPO + ")";
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
    protected void gvBlanketPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblID");
            olabel.Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
    protected void gvBlanketPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            gvBlanketPO.ShowFooter = true;
            gvBlanketPO.Columns[10].Visible = false;
            //gvBlanketPO.HeaderRow.FindControl("lkbAddItem").Visible = false;
            ((LinkButton)gvBlanketPO.HeaderRow.FindControl("lkbAddItem")).Enabled = false;
            bind(); 
        }
        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            gvBlanketPO.ShowFooter = false;
            gvBlanketPO.Columns[10].Visible = true;
            //gvBlanketPO.HeaderRow.FindControl("lkbAddItem").Visible = true;
            bind();
        }

        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            gvBlanketPO.ShowFooter = false;
            gvBlanketPO.Columns[10].Visible = true;
            //gvBlanketPO.HeaderRow.FindControl("lkbAddItem").Visible = true;
            ((LinkButton)gvBlanketPO.HeaderRow.FindControl("lkbAddItem")).Enabled = true;
 
            SqlDataAdapter da;
            DataSet myds;
            myds = new DataSet();
 
            //sqlcn = new SqlConnection(@"Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq");
            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection sqlcn = new SqlConnection(strConn);
            sqlcn.Open();

            da = new SqlDataAdapter("select * from xmlblanketpoconfig where 0=1 ", sqlcn);
            da.Fill(myds, "xmlblanketpoconfig");

            //----------- Insert new row----------

            DataRow mydr = myds.Tables["xmlblanketpoconfig"].NewRow();

            if (((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFblanketpo")).Text.Trim().Length > 0)
                mydr["BLANKETPO"] = ((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFblanketpo")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BlanketPO欄位不能為空...');</script>");
                return;
            }

            if (((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFlasteditby")).Text.Trim().Length > 0)
                mydr["LASTEDITBY"] = ((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFlasteditby")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('LastEditBy欄位不能為空...');</script>");
                return;
            }


            if (((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFregion")).Text.Trim().Length > 0)
                mydr["REGION"] = ((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFregion")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Region欄位不能為空...');</script>");
                return;
            }
            if (((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFediid")).Text.Trim().Length > 0)
                mydr["EDIID"] = ((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFediid")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('EDIID欄位不能為空...');</script>");
                return;
            }
            if (((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFgloviatpid")).Text.Trim().Length > 0)
                mydr["GLOVIATPID"] = ((TextBox)this.gvBlanketPO.FooterRow.FindControl("txtFgloviatpid")).Text.Trim();
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('GloviatPID欄位不能為空...');</script>");
                return;
            }
            mydr["LASTEDITDT"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            myds.Tables["xmlblanketpoconfig"].Rows.Add(mydr);

            // ---------------end -------------------

            SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(da);
            da.Update(myds, "xmlblanketpoconfig");
            sqlcn.Close();

            try
            {
                if (!Directory.Exists(@"c:\sfcquery Log"))
                {
                    Directory.CreateDirectory(@"c:\sfcquery Log");
                }
                string path = @"c:\sfcquery Log\blanketpo.log";
                StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default);
                writer.WriteLine("---------------Insert Action Start-------------------");
                writer.WriteLine("  Insert User: " + mydr["LASTEDITBY"] + "");
                writer.WriteLine("  table:xmlblanketpoconfig");
                writer.WriteLine("  Insert Columns:blanketpo='" + mydr["BLANKETPO"] + "';lasteditby='" + mydr["LASTEDITBY"] + "';lasteditdt='" + mydr["LASTEDITDT"] + "';region='" + mydr["REGION"] + "';ediid='" + mydr["EDIID"] + "';gloviatpid='" + mydr["GLOVIATPID"] + "'");
                writer.WriteLine("  Insert Time:" + mydr["LASTEDITDT"]);
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

            gvBlanketPO.EditIndex = -1;
            bind(); 
        }

    }
}
