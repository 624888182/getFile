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
using System.Data.OleDb;
using DBAccess.EAI;

public partial class Boundary_wfrmUPDInfoQuery : System.Web.UI.UserControl
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl1.Visible = false;
            lbl2.Visible = false;
            lbl3.Visible = false;
            lbl4.Visible = false;
            lbl5.Visible = false;
            lbl6.Visible = false;

            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = false;
            GridView5.Visible = false;
            GridView6.Visible = false;

            MultiLanguage(); 
        }
    }

    private void MultiLanguage()
    { 
        Button1.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.TextBox1.Text.Trim() == "")
        {
            lbl1.Visible = false;
            lbl2.Visible = false;
            lbl3.Visible = false;
            lbl4.Visible = false;
            lbl5.Visible = false;
            lbl6.Visible = false;

            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView4.Visible = false;
            GridView5.Visible = false;
            GridView6.Visible = false;
            //Response.Write("<script>alert('dkfjdkf')</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('請輸入Invoice NO！！');</script>");
            return;
        }
        else
        {
            lbl6.Visible = false;
            GridView6.Visible = false;
            BindDetailData(this.TextBox1.Text.Trim());
            BindHandData(this.TextBox1.Text.Trim());
            BindSAPData(this.TextBox1.Text.Trim());
            BindCartonMapData(this.TextBox1.Text.Trim());
            BindCartonQuantity(this.TextBox1.Text.Trim());
            BindDropList(this.TextBox1.Text.Trim());
        }
    }
    private void BindDetailData(string invoice)
    {
        sql = "SELECT * FROM SHP.UPD_DATALOAD_DETAIL_T where invoice='" + invoice + "'";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            lbl1.Visible = true;
            GridView1.Visible = true;
            this.GridView1.DataSource = reader;
            this.GridView1.DataBind();
        }
        else
        {
            lbl1.Visible = true;
            GridView1.Visible = false;
        }
        cmd.Connection.Close();
    }
    private void BindHandData(string invoice)
    {
        sql = "SELECT * FROM SHP.UPD_INVOICE_HAND_TEMP where invoice='" + invoice + "'";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            lbl2.Visible = true;
            GridView2.Visible = true;
            this.GridView2.DataSource = reader;
            this.GridView2.DataBind();
        }
        else
        {
            lbl2.Visible = true;
            GridView2.Visible = false;
        }
        cmd.Connection.Close();
    }
    private void BindSAPData(string invoice)
    {
        sql = "select DISTINCT A.PLANT, A.invoice, A.customer_name, A.ITEM_MODULE, "
            + " (select sum(quantity) from CMCS_SFC_PACKING_LINES_ALL b where b.INVOICE_NUMBER=a.invoice) shipped_qty, LAST_SHIPMENT_DATE,C.CUSTOMER_TYPE "
            + "    from Sfc.MES_MIT_INVOICE A,CMCS_SFC_PACKING_LINES_ALL B,SFC.CMCS_SFC_MODEL C "
            + "    Where  a.invoice IN( '" + invoice + "' ) AND B.INVOICE_NUMBER = A.Invoice "
            + "    AND A.ITEM_MODULE=C.MODEL "
            + "    and (UPPER(A.CUSTOMER_NAME) LIKE '%MOTO%' or UPPER(A.CUSTOMER_NAME) LIKE '%摩托羅拉%'"
            + "    or UPPER(A.CUSTOMER_NAME) LIKE '%SUTECH%' or UPPER(A.CUSTOMER_NAME) LIKE '%TX4-SOI%') "
            + "    order by A.LAST_SHIPMENT_DATE desc ";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            
            lbl3.Visible = true;
            GridView3.Visible = true;
            this.GridView3.DataSource = reader;
            this.GridView3.DataBind();
        }
        else
        {
            lbl3.Visible = true;
            GridView3.Visible = false;
        } 
        cmd.Connection.Close();
    }
    private void BindCartonMapData(string invoice)
    {
        sql = "select * from cmcs_sfc_ship_carton_map where INVOICE_NO='" + invoice + "' order by order_carton_no ";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            div4.Attributes.Add("style", "height:200px; overflow:auto;");
            lbl4.Visible = true;
            GridView4.Visible = true;
            this.GridView4.DataSource = reader;
            this.GridView4.DataBind();
        }
        else
        {
            lbl4.Visible = true;
            GridView4.Visible = false;
        }
        cmd.Connection.Close();
    }
    private void BindCartonQuantity(string invoice)
    {
        sql = " select carton_no,count(*) "
              + "  from cmcs_sfc_shipping_data "
              + "  where carton_no in"
              + "          (select order_carton_no "
              + "          from cmcs_sfc_ship_carton_map "
              + "          where INVOICE_NO='" + invoice + "')"
              + "  group by carton_no"
              + "  order by carton_no asc";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            div5.Attributes.Add("style", "height:200px; overflow:auto;");
            lbl5.Visible = true;
            GridView5.Visible = true;
            this.GridView5.DataSource = reader;
            this.GridView5.DataBind();
        }
        else
        {
            lbl5.Visible = true;
            GridView5.Visible = false;
        }
        cmd.Connection.Close();
    }
    private void BindDropList(string invoice)
    {
        sql = " select distinct order_carton_no "
              + "  from cmcs_sfc_ship_carton_map "
              + "  where INVOICE_NO='" + invoice + "' order by order_carton_no ";
        OleDbCommand cmd = new OleDbCommand(sql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        cmd.Connection.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        adapter.SelectCommand = cmd;
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        DropDownList1.DataSource = ds.Tables[0].DefaultView;
        DropDownList1.DataTextField = "order_carton_no"; //dropdownlist的Text的字段
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0,"All");
        cmd.Connection.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //sql = " SELECT * FROM SHP.CMCS_SFC_SHIPPING_DATA Where CARTON_NO = '" + this.DropDownList1.SelectedItem.Text.Trim() + "'";
        //OleDbCommand cmd = new OleDbCommand(sql);
        //cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tysfc;user id=SHP;password=SHP");
        //cmd.Connection.Open();

        if (this.DropDownList1.SelectedItem.Text.Trim().ToUpper() == "ALL")
        {
            Button1_Click(sender, null);
        }
        else
        {
            div4.Attributes.Clear();
            string strsql1 = "select * from shp.cmcs_sfc_ship_carton_map where order_carton_no='" + this.DropDownList1.SelectedItem.Text.Trim() + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            this.GridView4.DataSource = dt1.DefaultView;
            this.GridView4.DataBind();

            div5.Attributes.Clear();
            string strsql2 = " select '" + this.DropDownList1.SelectedItem.Text.Trim() + "' carton_no,count(*) from shp.cmcs_sfc_shipping_data where carton_no='" + this.DropDownList1.SelectedItem.Text.Trim() + "' order by carton_no ";
            DataTable dt2 = ClsGlobal.objDataConnect.DataQuery(strsql2).Tables[0];
            this.GridView5.DataSource = dt2.DefaultView;
            this.GridView5.DataBind();

            string strsql3 = "select * from shp.cmcs_sfc_shipping_data where carton_no='" + this.DropDownList1.SelectedItem.Text.Trim() + "' ";
            DataTable dt3 = ClsGlobal.objDataConnect.DataQuery(strsql3).Tables[0];
            this.GridView6.DataSource = dt3.DefaultView;
            this.GridView6.DataBind();
            lbl6.Visible = true;
            GridView6.Visible = true; 
        }
    }
}
