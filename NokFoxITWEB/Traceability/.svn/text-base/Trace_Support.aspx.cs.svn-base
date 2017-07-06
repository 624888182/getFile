using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data ;
public partial class Temp_Trace_BigSN : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DbAccessORA md = new DbAccessORA();
        string Sql_value;
        Sql_value = "select rownum, iqcpo_NO,key_part_no,items_no,qty,creator,create_time,Lot_no,d_c,supply,disk_sn,checkin_type,supply_pn ,update_time,wh_emp, wh_time from  SFISM4.C_SEE_IQC_CHECK_DETAIL   where DISK_SN='"+TextBox1.Text +"'";
        DataTable Dt1 = md.GetDataTable(Sql_value, "DB86");        
        GridView1.DataSource = Dt1;
        GridView1.DataBind ();
       // strOut = md.ExecuteStoredProcedure(strStoredProcedurName, strParams, strValue);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string ss;
    }
}