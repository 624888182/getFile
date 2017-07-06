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

public partial class Boundary_wfrmwodetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            binddata();
        }
    }

    private void binddata()
    { 
        string strWO = Request.QueryString["WO"].ToString();
        string strWOqty = Request.QueryString["qty"].ToString();
        string strtype = strWO.Substring(0, 1).ToUpper();
        string StrSql = "";
        switch (strtype)
        {
            case "S":
                StrSql = "select '" + strWO + "' WO,'" + strWOqty + "' Qty,d.starttime,c.sequence,input_qty input,output from shp.cmcs_sfc_sorder a,"
                   + " (select count(*) output from sfc.product_panel_sorder where sorder='" + strWO + "') b,"
                   + " (SELECT NVL(BOARD_QTY,0) sequence FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL WHERE SPART=(SELECT SPART FROM SHP.CMCS_SFC_SORDER WHERE SORDER='" + strWO + "'))c, "
                   + " (select min(a.creation_date) starttime from sfc.mes_pcba_panel_history a,sfc.mes_pcba_panel_detail b where (a.panel_id='A'||b.panel_id or a.panel_id='B'||b.panel_id) and b.wo_no='" + strWO + "') d"
                   + " Where a.SORDER ='" + strWO + "'";
                break;
            case "A":
                StrSql = "select '" + strWO + "' WO,'" + strWOqty + "' Qty,a.starttime,'1' sequence,a.input,output from "
                    + " (select count(*) input,min(creation_date) starttime from SFC.MES_ASSY_PID_JOIN where wo_no='" + strWO + "') a,"
                    + " (SELECT count(*) output FROM SFC.MES_ASSY_HISTORY c,SFC.MES_ASSY_PID_JOIN d"
                    + " where c.product_id=d.main_id and c.wo_no='" + strWO + "'"
                    + " and (station_id like 'A_FO' OR STATION_ID='FQC') AND STATE_ID='P'"
                    + " AND c.WO_NO=d.wo_no and c.product_id=d.main_id ) b"; 
                break;
            case "P":
                StrSql = "select '" + strWO + "' WO,'" + strWOqty + "' Qty,a.starttime,'1' sequence,a.input,output from "
                    + "(select count(*) input,min(ddate) starttime from SHP.CMCS_SFC_SHIPPING_DATA where work_order='" + strWO + "') a,"
                    + " (select count(c.productid) output FROM shp.CMCS_SFC_SHIPPING_DATA c,SHP.CMCS_SFC_SHIP_CARTON_MAP  d"
                    + " where c.CARTON_NO=d.ORDER_CARTON_NO and c.work_order='" + strWO + "') b";
                break;
        }
         
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
        dgwodetail.DataSource = dt.DefaultView;
        dgwodetail.DataBind();
        dt.Dispose();
    }

}
