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

public partial class Boundary_wfrmAutoMailQuery : System.Web.UI.UserControl
{
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        sql = "SELECT PROJECT, INVOICE, UPPER(COUNTRY) COUNTRY, SUM(SHIPPED_QTY) SHIPPED_QTY "
              + "  FROM (SELECT substr(ORDER_ITEM,3,3) PROJECT,T.INVOICE,"
              + "  ("
              + "      SELECT DISTINCT S.SHIP_TO_COUNTRY  "
              + "      FROM SAP.CMCS_SFC_PACKING_LINES_ALL S "
              + "      WHERE S.INVOICE_NUMBER = T.INVOICE"
              + "  ) COUNTRY, "
              + " ("
              + "      SELECT DISTINCT S.SHIP_DATE "
              + "      FROM SAP.CMCS_SFC_PACKING_LINES_ALL S  "
              + "      WHERE S.INVOICE_NUMBER = T.INVOICE AND ROWNUM=1"
              + "  ) SHIP_DATE,SHIPPED_QTY "
              + "  FROM SAP.SAP_INVOICE_INFO T "
              + "  WHERE substr(ORDER_ITEM,3,3) IN ('ZNT','GLM')  AND (ORDER_ITEM LIKE '1%' OR ORDER_ITEM LIKE '9%')  "
              + "  ) "
              + "  WHERE SHIP_DATE >= TRUNC(SYSDATE) -10  AND (SELECT COUNT(*) FROM SHP.CMCS_SFC_SHIP_CARTON_MAP X WHERE X.INVOICE_NO=INVOICE AND (X.STATUS<>'Y' OR X.STATUS IS NULL))>0  "
              + "  GROUP BY PROJECT, INVOICE, COUNTRY "
              + "  union "
              + "  SELECT PROJECT, INVOICE, UPPER(COUNTRY) COUNTRY, SUM(SHIPPED_QTY) SHIPPED_QTY "
              + "  FROM ("
              + "          SELECT substr(ORDER_ITEM,3,3) PROJECT,T.INVOICE,"
              + "          (SELECT DISTINCT S.SHIP_TO_COUNTRY  "
              + "          FROM SAP.CMCS_SFC_PACKING_LINES_ALL S"
              + "          WHERE S.INVOICE_NUMBER = T.INVOICE) COUNTRY, "
              + "          (SELECT DISTINCT S.SHIP_DATE "
              + "          FROM SAP.CMCS_SFC_PACKING_LINES_ALL S  "
              + "          WHERE S.INVOICE_NUMBER = T.INVOICE AND ROWNUM=1) SHIP_DATE,SHIPPED_QTY "
              + "  FROM SAP.SAP_INVOICE_INFO T "
              + "  WHERE substr(ORDER_ITEM,3,3) IN ('SCY','LPI','SLI','DUO')  AND ORDER_ITEM LIKE '1%'   "
              + "  ) "
              + "  WHERE SHIP_DATE >= TRUNC(SYSDATE) -10  AND "
              + "  ("
              + "      SELECT COUNT(*) "
              + "      FROM SHP.CMCS_SFC_SHIP_CARTON_MAP X "
              + "      WHERE X.INVOICE_NO=INVOICE AND (X.STATUS<>'Y' OR X.STATUS IS NULL))>0 "
              + "      GROUP BY PROJECT, INVOICE, COUNTRY";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt.DefaultView;
            this.GridView1.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前沒有要發送的DN！！');</script>");
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sql = "SELECT  /*+RULE*/DISTINCT A.INVOICE_NUMBER,A.CUSTOMER_PO,C.IMEI,DECODE(B.NKEY,NULL,'',SUBSTR(B.NKEY, 2, 1) || SUBSTR(B.NKEY, 4, 1) ||  SUBSTR(B.NKEY, 6, 1) || SUBSTR(B.NKEY, 8, 1) ||SUBSTR(B.NKEY, 10, 1) || SUBSTR(B.NKEY, 12, 1) ||SUBSTR(B.NKEY, 14, 1) || SUBSTR(B.NKEY, 16, 1)) NWSCP, DECODE(B.NSKEY,NULL,'',SUBSTR(B.NSKEY, 2, 1) || SUBSTR(B.NSKEY, 4, 1) ||SUBSTR(B.NSKEY, 6, 1) || SUBSTR(B.NSKEY, 8, 1) ||  SUBSTR(B.NSKEY, 10, 1) || SUBSTR(B.NSKEY, 12, 1) ||SUBSTR(B.NSKEY, 14, 1) || SUBSTR(B.NSKEY, 16, 1)) SSCP,B.PRIVILEGEPWD SERVICE,  TO_CHAR(B.E2PDATE, 'YYYY-MM-DD') MANUFACTURINGDATE,TO_CHAR(B.E2PDATE, 'HH24:MI:SS') MANUFACTURINGTIME,C.CARTON_NO,D.SOFTWARE_VER,  E.PHONE_MODEL SA_NO,F.CUSTOMER_NUM SERIAL_NUM "
               + " FROM CMCS_SFC_PACKING_LINES_ALL A,GLM.E2PCONFIG B,CMCS_SFC_PORDER D,CMCS_SFC_SHIPPING_DATA C, ROS_TCH_PN E,CMCS_SFC_IMEINUM F "
               + " WHERE A.INTERNAL_CARTON = C.CARTON_NO "
               + "         AND C.WORK_ORDER = D.PORDER "
               + "         AND B.PRODUCTID = C.PRODUCTID"
               + "         AND C.IMEI=F.IMEINUM  "
               + "         AND D.PPART = E.PPART  "
               + "         AND B.STATUS = 'PASS' "
               + "         AND A.INVOICE_NUMBER = '" + this.TextBox1.Text.Trim() + "' "
               + "         AND B.E2PDATE = ("
               + "                         SELECT MAX(E2PDATE) "
               + "                         FROM GLM.E2PCONFIG I "
               + "                         WHERE I.PRODUCTID = B.PRODUCTID AND I.STATUS = 'PASS'"
               + "                         )";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt.DefaultView;
            this.GridView1.DataBind();
            this.Label1.Text = "總計：" + dt.Rows.Count + "條";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('目前沒有這個INVOICE的發送數據！！');</script>");
            return;
        }
    }

}
