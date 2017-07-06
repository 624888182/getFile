using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using SCM.GSCMDKen;
using System.Configuration;
using Microsoft.Adapter.SAP;
using System.Web.UI.WebControls;
using System.Web.UI;
public class DelchkPO
{

    public string CheckPOInfo(string dbtype, string DBReadString, string DBWriString, string scan)
    {
        string F = "未找到对应DN";
        string F1 = "外交管未维护PO信息，或PO信息维护不完全！請速聯繫外交管！";
        string P = "PO信息檢查OK";
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

        #region Check外交管维护的PO信息

        string sql2 = @"select DISTINCT b.PO_NUMBER from SAP.CMCS_SFC_PACKING_LINES_ALL A,SHP.UPD_ORDER_INFORMATION B where b.PO_NUMBER=a.CUSTOMER_PO and a.INVOICE_NUMBER='" + DN + "' and b.SO_NUMBER is not null and b.CUSTOMER_ID is not null and b.SHIP_ID is not null and b.SHIP_TOCUSTOMER_NAME is not null and b.SOLD_TOCUSTOMER_NAME is not null  ";
        System.Data.DataTable ds = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql2);
        if (ds.Rows.Count > 0)
        {
        }

        else
        {
            return F1;
        }

        #endregion

        return "1";
    }


}






