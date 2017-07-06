using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DelchkProduct
/// </summary>
public class DelchkProduct
{
    public string CheckQtyInfo(string dbtype, string DBReadString, string DBWriString, string scan)
	{
        string F = "未找到对应DN";
        string F1 = "3S/4S出貨數量與消單票據數量不符，請將此消單掃描完畢！";
        string P = "出貨數量檢查OK";
        #region 查詢扫入信息对应DN号

        string sql = @"select INVOICE_NUMBER from SAP.CMCS_SFC_PACKING_LINES_ALL where (INTERNAL_CARTON='" + scan + "' or INVOICE_NUMBER='" + scan + "' or PALLET_NUMBER_NEW='" + scan + "') group by INVOICE_NUMBER";
        System.Data.DataTable dt = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql);
        if (dt.Rows.Count > 0)
        {

        }
        else
        {
            return F;
        }
        string DN = dt.Rows[0][0].ToString();

        #endregion

        #region 查詢DN对应出货数量

        string sql1 = @"select SHIPPED_QTY from SAP.SAP_INVOICE_INFO where INVOICE='" + DN + "'";
        System.Data.DataTable dt1 = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql1);
        string DNqty = dt1.Rows[0][0].ToString();

        #endregion

        #region 查詢3S/4S对应出货数量

        string sql2 = @"select count(*) from SAP.CMCS_SFC_PACKING_LINES_ALL A,SHP.CMCS_SFC_SHIPPING_DATA B where a.INVOICE_NUMBER='" + DN + "' and a.INTERNAL_CARTON=b.CARTON_NO";
        System.Data.DataTable dt2 = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql2);
        string Shipqty = dt2.Rows[0][0].ToString();

        #endregion

        #region 查詢3S/4S对应出货数量

        if (DNqty != Shipqty)
        {
            return F1;
        }
        #endregion
        return "1";
	}
}
