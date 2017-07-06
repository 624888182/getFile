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
       
        Sql_value = "select rownum,serial_number, MO_number,line_name,group_name ,station_name ,in_line_time ,emp_no from r_wip_tracking_t t where serial_number  in (";
        Sql_value = Sql_value + " select big_sn  from r_wip_tracking_t_pid t where serial_number = '" + TextBox1.Text + "') order by in_line_time";
        DataTable Dt1 = md.GetDataTable(Sql_value, "L8StandByConnectionString");
        GridView1.DataSource = Dt1;
        GridView1.DataBind ()  ;
        if (Dt1.Rows.Count > 0)
        {
            LinkButton1.Text = TextBox1.Text;
            LinkButton2.Text = Dt1.Rows[0]["in_line_time"].ToString();
            LinkButton3.Text = Dt1.Rows[0]["MO_number"].ToString();
            LinkButton5.Text = Dt1.Rows[0]["serial_number"].ToString();

            Sql_value = "SELECT SPART ,MODEL ,pid_QTY FROM SHP.CMCS_SFC_SORDER      where sorder     = '" + Dt1.Rows[0]["MO_number"].ToString() + "' ";
            DataTable Dt2 = md.GetDataTable(Sql_value, "L8StandByConnectionString");

            LinkButton4.Text = Dt2.Rows[0]["SPART"].ToString();

            LinkButton6.Text = Dt2.Rows[0]["MODEL"].ToString();
            LinkButton7.Text = Dt2.Rows[0]["pid_QTY"].ToString();
            Label2.Text = Dt1.Rows.Count.ToString ();
        }
        else
        {
            Label2.Text = "No Data!!";
        }
       // strOut = md.ExecuteStoredProcedure(strStoredProcedurName, strParams, strValue);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string ss;
    }
}