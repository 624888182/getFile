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


        Sql_value = "select rownum,LINE_NAME,MACHINE_CODE,STATION_NUMBER,TRACK_NO,FEEDER_NO,TRAN_FLAG,MO_NUMBER,MODEL_NAME,BOM_NO,VER,KEY_PART_NO,LOT_NO,QTY,UNIT,";
        Sql_value = Sql_value + " CREATE_TIME ,EMP_NO,SN,Dc,sUPPLY_KEY,IQC_SN from sfism6.R_SMT_INV_TRAN_T  where CREATE_TIME >=to_date('" + Startdate.DateTextBox.Text + "' ,'YYYY-MM-DD hh24:MI') and CREATE_TIME<=to_date('" + Enddate.DateTextBox.Text + "' ,'YYYY-MM-DD hh24:MI')";

        if (Txtpart.Text != "")
        { 
        Sql_value = Sql_value + " and KEY_PART_NO ='"+Txtpart.Text+"'";
        
        }
        if (TxtLine.Text != "")
        {
            Sql_value = Sql_value + " and LINE_NAME ='" + TxtLine.Text + "'";
        
        }

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