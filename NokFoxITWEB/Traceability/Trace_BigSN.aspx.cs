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
     

 

       Sql_value = "select ROWNUM,serial_number,MO_NUMBER,MODEL_NAME,LINE_NAME,GROUP_NAME, STATION_NAME ,IN_STATION_TIME,IN_lINE_TIME,SHIPPING_SN ,EMP_NO from ";
       Sql_value =Sql_value+ "r_wip_tracking_t t where serial_number='" + TextBox1.Text.TrimEnd() + "' ";


        DataTable Dt1 = md.GetDataTable(Sql_value, "L8StandByConnectionString");
        GridView1.DataSource = Dt1;
        GridView1.DataBind ()  ;
        if (Dt1.Rows.Count > 0)
        {
            LinkButton1.Text = TextBox1.Text;
            LinkButton2.Text = Dt1.Rows[0]["in_line_time"].ToString();
            LinkButton3.Text = Dt1.Rows[0]["MO_number"].ToString();
            LinkButton5.Text = Dt1.Rows[0]["serial_number"].ToString();

            Sql_value = "SELECT SPART ,MODEL ,pid_QTY FROM SHP.CMCS_SFC_SORDER where sorder = '" + Dt1.Rows[0]["MO_number"].ToString() + "' ";
            DataTable Dt2 = md.GetDataTable(Sql_value, "L8StandByConnectionString");
            LinkButton4.Text = Dt2.Rows[0]["SPART"].ToString();
            LinkButton6.Text = Dt2.Rows[0]["MODEL"].ToString();
            LinkButton7.Text = Dt2.Rows[0]["pid_QTY"].ToString();

            Sql_value = "select serial_number from sfc.r_wip_tracking_t_pid where BIG_SN='" + TextBox1.Text + "' ";
            DataTable Dt3 = md.GetDataTable(Sql_value, "L8StandByConnectionString");

            int DT3_COUNT;
            DT3_COUNT = Dt3.Rows.Count;
            for (int i = 1; i <= DT3_COUNT; i++)
            {


                switch (i)
                {
                    case 1:
                        LinkButton7.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    case 2:
                        LinkButton8.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    case 3:
                        LinkButton9.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    case 4:
                        LinkButton10.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    case 5:
                        LinkButton11.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    case 6:
                        LinkButton12.Text = Dt3.Rows[i - 1]["serial_number"].ToString();
                        break;
                    default: break;
                }



            }
        }

       // strOut = md.ExecuteStoredProcedure(strStoredProcedurName, strParams, strValue);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string ss;
    }
}