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
using System.Drawing;
using EconomyUser;
using Economy.Publibrary;
using SCM.GSCMDKen;

public partial class Pub_Jbackdataquery : System.Web.UI.Page
{

    public static string dbback = ConfigurationManager.AppSettings["Sql221String"].ToString();

    AppPubLib AppPubLibPointer = new AppPubLib();

    public static DataTable dsback = new DataTable();

    public static DataTable dsnoback = new DataTable();

    public static string dbCheck = string.Empty;

    public DataTable QueryNoBack(DataTable ds)
    {
        dsnoback.Clear();
        if(dsnoback.Columns.Count == 0)
        {
            dsnoback.Columns.Add("IMEI");
            dsnoback.Columns.Add("PID");
            dsnoback.Columns.Add("Cartion");
            dsnoback.Columns.Add("Delivery_Date");
            dsnoback.Columns.Add("Delete_Date");
            dsnoback.Columns.Add("Flag1");
        }
        for (int i = 0; i < ds.Rows.Count; i++)
        {
            if (ds.Rows[i]["Flag1"].ToString().Substring(0, 4).IndexOf("0") >= 0 || ds.Rows[i]["Flag1"].ToString().Substring(0, 4).IndexOf("E") >= 0)
            {
                DataRow dr = dsnoback.NewRow();
                dr[0] = ds.Rows[i][0].ToString();
                dr[1] = ds.Rows[i][1].ToString();
                dr[2] = ds.Rows[i][2].ToString();
                dr[3] = ds.Rows[i][3].ToString();
                dr[4] = ds.Rows[i][4].ToString();
                dr[5] = ds.Rows[i][5].ToString();
                dsnoback.Rows.Add(dr);

                ds.Rows[i].Delete();
            }
        }
        return ds;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Session["Param3"] != null) dbCheck = Session["Param3"].ToString();
            //if (Session["Param4"] != null) webConnect214 = Session["Param4"].ToString();
            Panel1.Visible = false;
            Panel2.Visible = false;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            string yearNow = DateTime.Now.Year.ToString();
            string yearString = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                yearchiose.Items.Add(Convert.ToString(Convert.ToInt32(yearNow) - i));
            }
            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    yearString = '0' + Convert.ToString(i);
                }
                else
                {
                    yearString = Convert.ToString(i);
                }
                motchchiose.Items.Add(yearString);
            }

            dbchiose.Items.Add("dbBack108");
            dbchiose.Items.Add("dbBack215");

            dbchiose0.Items.Add("N. Close ");
            dbchiose0.Items.Add("Y. Run ! ");

            TextBox4.Text = "0";
            TextBox5.Text = "500";
            // tmp23Tax = ((dbchiose0.SelectedItem.Value).Trim()).Substring(0, 1);
        }

    }

    protected void querybutton_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        int count = 0;
        string db = string.Empty;
        string dateChoise = string.Empty;
        string dbChoise = string.Empty;
        //string dbCheck = string.Empty;
        DataSet ds = new DataSet();
        DataSet ds211 = new DataSet();
        DataSet dsbackdetail = new DataSet();
        db = ConfigurationManager.AppSettings["L8StandByConnectionString"].ToString();
        dbCheck = ConfigurationManager.AppSettings["Sql221String"].ToString();
        dateChoise = yearchiose.SelectedValue.ToString() + motchchiose.SelectedValue.ToString();
        //DateTime beginTime = DateTime.ParseExact(yearchiose.SelectedValue.ToString() + motchchiose.SelectedValue.ToString() + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        //dateChoise = beginTime.ToString("yyyyMMdd");
        //string endDate = beginTime.AddMonths(+1).ToString("yyyyMMdd");
        
        if (dbchiose.SelectedValue.ToString().Contains("dbBack108"))
        {
            dbChoise = ConfigurationManager.AppSettings["tjmaxstoreConnectionString"].ToString();
        }
        else if (dbchiose.SelectedValue.ToString().Contains("dbBack215"))
        {
            dbChoise = ConfigurationManager.AppSettings["NormalBakupConnectionString"].ToString();
        }
       // 20120803
        TextBox1.Text = TextBox1.Text + "000000";
        TextBox2.Text = TextBox2.Text + "999999";
        TextBox3.Text = "";
        string sql = @"SELECT * FROM [ERPDBF].[dbo].[DelIMEIctl] WHERE Delivery_Date LIKE '" + dateChoise + "%'   "  +
            " and Delivery_Date >=  '" + TextBox1.Text + "'  and Delivery_Date <=  '" + TextBox2.Text + "'  ";
        //string sql = @"SELECT * FROM [ERPDBF].[dbo].[DelIMEIctl] WHERE Delivery_Date >= convert(datetime,'" + dateChoise + "',112) AND Delivery_Date < convert(datetime,'" + endDate + "',112)";
        ds = DataBaseOperation.SelectSQLDS("sql", dbCheck, sql);
        //string sql1 = "select imei,CARTON_NO,PRODUCTID,LAST_UPDATE_DATE from  SHP.CMCS_SFC_SHIPPING_DATA A,(";
        //       sql1 = sql1 + " select distinct INTERNAL_CARTON,LAST_UPDATE_DATE from sap.cmcs_sfc_packing_lines_all";
        //       sql1 = sql1 + " where LAST_UPDATE_DATE >=To_Date('" + dateChoise + "','yyyy-MM-dd')  ";
        //       sql1 = sql1 + " and LAST_UPDATE_DATE <To_Date('" + endDate + "','yyyy-MM-dd')  and INVOICE_NUMBER not in(";
        //       sql1 = sql1 + " select distinct INVOICE_NUMBER from sap.cmcs_sfc_packing_lines_all ";
        //       sql1 = sql1 + " where LAST_UPDATE_DATE >=To_Date('" + dateChoise + "','yyyy-MM-dd')  ";
        //       sql1 = sql1 + " and LAST_UPDATE_DATE <To_Date('" + endDate + "','yyyy-MM-dd')  and INTERNAL_CARTON is null )";
        //       sql1 = sql1 + " ) B where a.CARTON_NO=b.INTERNAL_CARTON order by LAST_UPDATE_DATE";

        //dsback = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sql1);
        TextBox3.Text = (ds.Tables[0].Rows.Count).ToString();
        if (((dbchiose0.SelectedItem.Value).Trim()).Substring(0, 1) != "Y") return;
        //Response.Write(TextBox3.Text);
        //Response.Write("<script>alert('Total Data :' + '" + TextBox2.Text + "' )</script>");
        int i = 0,  tmpi = 0, stopi=0;
        if (( TextBox4.Text != "" ) ) 
        {
            tmpi  = Convert.ToInt32(TextBox4.Text);
            stopi = Convert.ToInt32(TextBox5.Text);
        }

        for (  i = tmpi; i < ds.Tables[0].Rows.Count; i++)
        {
            bool ret = false;
            if (!AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "2", "2", "Read", "").ToString().Equals("D"))
            {
                string sqlImeitabel = @"SELECT COUNT(*) FROM SHP.CMCS_SFC_IMEINUM WHERE IMEINUM = '" + ds.Tables[0].Rows[i]["IMEI"].ToString().Trim() + "'";
                dsbackdetail = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sqlImeitabel);
                if (!dsbackdetail.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "2", "2", "Write", "1");
                }
                else
                {
                    ret = true;
                    ds211 = DataBaseOperation.SelectSQLDS("oracle", db, sqlImeitabel);
                    if (!ds211.Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "2", "2", "Write", "E");
                    }
                }
            }
            if (!AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "3", "2", "Read", "").ToString().Equals("D"))
            {
                string sqljiontabel = @"SELECT COUNT(*) FROM  SFC.MES_ASSY_PID_JOIN WHERE MAIN_ID = '" + ds.Tables[0].Rows[i]["PID"].ToString().Trim() + "'";
                dsbackdetail = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sqljiontabel);
                if (!dsbackdetail.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "3", "3", "Write", "1");
                }
                else
                {
                    ret = true;
                    ds211 = DataBaseOperation.SelectSQLDS("oracle", db, sqljiontabel);
                    if (!ds211.Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "3", "2", "Write", "E");
                    }
                }
            }
            if (!AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "4", "2", "Read", "").ToString().Equals("D"))
            {
                string sqlhistorytabel = @"SELECT COUNT(*) FROM SFC.MES_ASSY_HISTORY WHERE PRODUCT_ID = '" + ds.Tables[0].Rows[i]["PID"].ToString().Trim() + "'";
                dsbackdetail = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sqlhistorytabel);
                if (!dsbackdetail.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "4", "4", "Write", "1");
                }
                else
                {
                    ret = true;
                    ds211 = DataBaseOperation.SelectSQLDS("oracle", db, sqlhistorytabel);
                    if (!ds211.Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "4", "2", "Write", "E");
                    }
                }
            }
            if (!AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "5", "2", "Read", "").ToString().Equals("D"))
            {
                string sqlpidtabel = @"SELECT COUNT(*) FROM SFC.R_WIP_TRACKING_T_PID WHERE SERIAL_NUMBER = '" + ds.Tables[0].Rows[i]["PID"].ToString().Trim() + "'";
                dsbackdetail = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sqlpidtabel);
                if (!dsbackdetail.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "5", "1", "Write", "1");
                }
                {
                    ret = true;
                    ds211 = DataBaseOperation.SelectSQLDS("oracle", db, sqlpidtabel);
                    if (!ds211.Tables[0].Rows[0][0].ToString().Equals("0"))
                    {
                        ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "5", "2", "Write", "E");
                    }
                }
            }

            //string sqlheadtabel = @"SELECT COUNT(*) FROM WCDMA_TSE.R_function_head_t WHERE SERIAL_NUMBER = '" + ds.Tables[0].Rows[i]["PID"].ToString().Trim() + "'";
            //dsbackdetail = DataBaseOperation.SelectSQLDS("oracle", dbChoise, sqlheadtabel);
            //if (!dsbackdetail.Tables[0].Rows[0][0].ToString().Equals("0"))
            //{
            //    ds.Tables[0].Rows[i]["Flag1"] = AppPubLibPointer.SetBitProc(ds.Tables[0].Rows[i]["Flag1"].ToString(), "6", "1", "Write", "1");
            //}
            //{
            //    ret = true;
            //}
            if (ret)
            {
                count++;
            }
            string sqlupdate = @"UPDATE [ERPDBF].[dbo].[DelIMEIctl] SET Flag1 = '" + ds.Tables[0].Rows[i]["Flag1"].ToString() + "' WHERE IMEI = '" + ds.Tables[0].Rows[i]["IMEI"].ToString() + "'";
            DataBaseOperation.ExecSQL("sql", dbCheck, sqlupdate);

            //int FLAG =Convert.ToInt32(Convert.ToDouble(flag.Substring(0, 1).ToString()) * Math.Pow(2, 7) + Convert.ToDouble(flag.Substring(2, 1).ToString()) * Math.Pow(2, 5) + Convert.ToDouble(flag.Substring(4, 1).ToString()) * Math.Pow(2, 3) + Convert.ToDouble(flag.Substring(6, 1).ToString()) * Math.Pow(2, 1));
            if (i >= stopi) 
                 i = ds.Tables[0].Rows.Count; // break
        }
        LabelMessage.Visible = true;
        LabelMessage.Text = "共檢查" + dbchiose.SelectedValue.ToString() + "備份" + ds.Tables[0].Rows.Count + "條，其中未完全備份完成的有" + count + "條";
        
    }

    protected void qurey_button_Click(object sender, EventArgs e)
    {
        Panel2.Visible = true;
        LabelMessage.Visible = false;
        RadioButton1.Visible = true;
        RadioButton2.Visible = true;
        Panel1.Visible = false;
        string dbCheck = string.Empty;
        string dateChoise = string.Empty;
        
        dbCheck = ConfigurationManager.AppSettings["Sql221String"].ToString();
        dateChoise = yearchiose.SelectedValue.ToString() + motchchiose.SelectedValue.ToString();
        string sql = @"SELECT * FROM [ERPDBF].[dbo].[DelIMEIctl] WHERE Delivery_Date LIKE '" + dateChoise + "%'";
        //DateTime beginTime = DateTime.ParseExact(yearchiose.SelectedValue.ToString() + motchchiose.SelectedValue.ToString() + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        //dateChoise = beginTime.ToString("yyyyMMdd");
        //string endDate = beginTime.AddMonths(+1).ToString("yyyyMMdd");
        //string sql = @"SELECT * FROM [ERPDBF].[dbo].[DelIMEIctl] WHERE Delivery_Date >= convert(datetime,'" + dateChoise + "',112) AND Delivery_Date < convert(datetime,'" + endDate + "',112)";
        dsback = DataBaseOperation.SelectSQLDT("sql", dbCheck, sql);
        dsback = QueryNoBack(dsback);
        GridView1.DataSource = dsback;
        GridView1.DataBind();
        GridViewPageControl(GridView1, lblNow, lblTotal, btnFirst, btnPrevious, btnNext, btnLast, txtNo);
        if (dsback.Rows.Count == 0)
        {
            LabelMessage.Visible = true;
            LabelMessage.Text = "無對應數據";
        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        LabelMessage.Visible = false;
        if (dsback.Rows.Count == 0)
        {
            LabelMessage.Visible = true;
        }
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = dsback;
        GridView1.DataBind();
    }

    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        LabelMessage.Visible = false;
        if (dsnoback.Rows.Count == 0)
        {
            LabelMessage.Visible = true;
        }
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.DataSource = dsnoback;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dsback;
            GridView1.DataBind();
        }
        if (RadioButton2.Checked == true)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = dsnoback;
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    }

    protected void PagebtnClick(object sender, ImageClickEventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            switch (((ImageButton)sender).CommandArgument.ToString())
            {
                case "First":
                    GridView1.PageIndex = 0;

                    break;
                case "Previous":
                    GridView1.PageIndex = GridView1.PageIndex - 1;

                    break;
                case "Next":
                    GridView1.PageIndex = GridView1.PageIndex + 1;

                    break;
                case "Last":
                    GridView1.PageIndex = GridView1.PageCount - 1;

                    break;
                case "-1":
                    {
                        try
                        {
                            int index = int.Parse(txtNo.Text.Trim());
                            if (index > 0 && index < GridView1.PageCount + 1)
                                GridView1.PageIndex = index - 1;
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
            GridView1.DataSource = dsback;
            GridView1.DataBind();
            GridViewPageControl(GridView1, lblNow, lblTotal, btnFirst, btnPrevious, btnNext, btnLast, txtNo);
        }
        else if (RadioButton2.Checked == true)
        {
            switch (((ImageButton)sender).CommandArgument.ToString())
            {
                case "First":
                    GridView1.PageIndex = 0;

                    break;
                case "Previous":
                    GridView1.PageIndex = GridView1.PageIndex - 1;

                    break;
                case "Next":
                    GridView1.PageIndex = GridView1.PageIndex + 1;

                    break;
                case "Last":
                    GridView1.PageIndex = GridView1.PageCount - 1;

                    break;
                case "-1":
                    {
                        try
                        {
                            int index = int.Parse(txtNo.Text.Trim());
                            if (index > 0 && index < GridView1.PageCount + 1)
                                GridView1.PageIndex = index - 1;
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
            GridView1.DataSource = dsnoback;
            GridView1.DataBind();
            GridViewPageControl(GridView1, lblNow, lblTotal, btnFirst, btnPrevious, btnNext, btnLast, txtNo);
        }
    }

    protected void GridViewPageControl(GridView gvList, Label lblPageIndex, Label lblPageCount,
          ImageButton ibtnFirst, ImageButton ibtnPrevious, ImageButton ibtnNext, ImageButton ibtnLast, TextBox tb)
    {
        lblPageIndex.Text = "第" + (gvList.PageIndex + 1).ToString() + "頁 / ";
        lblPageCount.Text = "共" + gvList.PageCount.ToString() + "頁   轉到";
        tb.Text = (gvList.PageIndex + 1).ToString();
        ibtnFirst.Enabled = true;
        ibtnPrevious.Enabled = true;
        ibtnNext.Enabled = true;
        ibtnLast.Enabled = true;
        if (gvList.PageIndex == 0)
        {
            ibtnFirst.Enabled = false;
            ibtnPrevious.Enabled = false;
        }
        if (gvList.PageIndex == gvList.PageCount - 1)
        {
            ibtnNext.Enabled = false;
            ibtnLast.Enabled = false;
        }
    }
}
