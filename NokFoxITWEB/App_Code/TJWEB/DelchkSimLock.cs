using System;
using System.Data;
using System.Collections;
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
/// Summary description for DelchkSimLock
/// </summary>
public class DelchkSimLock
{
    public string checkLockInfo(string dbtype, string DBReadString, string DBWriString, string scan)
	{
        string F = "未找到对应DN";
        string F1 = "此料号产品应含有SimLock功能，消单下有商品未上传Lock解码，请核对！";
        string F2 = "此料号产品不应含有SimLock功能，消单下有商品上传Lock解码，请核对！";
        string P = "Simlock檢查OK";
        #region 查詢扫入信息对应DN号

        string sql = @"select INVOICE_NUMBER from SAP.CMCS_SFC_PACKING_LINES_ALL where (INTERNAL_CARTON='" + scan + "' or INVOICE_NUMBER='" + scan + "' or PALLET_NUMBER_NEW='" + scan + "') group by INVOICE_NUMBER";
        System.Data.DataTable dt = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql);
        if (dt.Rows.Count > 0)
        {

        }
        else
        {
            //errorMessagex = "未找到对应DN！";
            //Page.RegisterStartupScript("aa", "<script>alert('未找到对应DN！');</script>");
            //Response.Write("<script>alert('未找到对应DN')</script>");
            return F;
        }
        string DN = dt.Rows[0][0].ToString();

        #endregion

        #region 查詢DN对应机种料号是否有SimLock功能

        string sql1 = @"select a.CONTENT from SHP.ROS_TCH_PN A,SAP.CMCS_SFC_PACKING_LINES_ALL B where a.PPART=b.ITEM_NUMBER and INVOICE_NUMBER='" + DN + "' group by a.CONTENT";
        System.Data.DataTable dt1 = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql1);
        if (dt1.Rows.Count > 0)
        {

        }
        else
        {
            //errorMessagex = "未找到对应DN！";
            //Page.RegisterStartupScript("aa", "<script>alert('未找到对应DN！');</script>");
            //Response.Write("<script>alert('未找到对应DN')</script>");
            return F;
        }
        string SimLock = dt1.Rows[0][0].ToString();

        #endregion

        #region Check出货手机是否符合要求

        if (SimLock == "SL")
        {
            string sql2 = @"select a.SERIAL_NUMBER from WCDMA_TSE.R_FUNCTION_HEAD_T A,SHP.CMCS_SFC_SHIPPING_DATA B ,SAP.CMCS_SFC_PACKING_LINES_ALL C where c.INTERNAL_CARTON=b.CARTON_NO and b.SERIAL_NO=a.SERIAL_NUMBER and c.INVOICE_NUMBER='" + DN + "' and a.P_UC is null ";
            System.Data.DataTable dt2 = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql2);
            if (dt2.Rows.Count > 0)
            {
                return F1;
            }

        }
        else
        {
            string sql2 = @"select a.SERIAL_NUMBER from WCDMA_TSE.R_FUNCTION_HEAD_T A,SHP.CMCS_SFC_SHIPPING_DATA B ,SAP.CMCS_SFC_PACKING_LINES_ALL C where c.INTERNAL_CARTON=b.CARTON_NO and b.SERIAL_NO=a.SERIAL_NUMBER and c.INVOICE_NUMBER='" + DN + "' and a.P_UC is not null ";
            System.Data.DataTable dt2 = DataBaseOperation.SelectSQLDT(dbtype, DBReadString, sql2);
            if (dt2.Rows.Count > 0)
            {
                return F2;
            }
        }

        #endregion
        return "1";
    }

}
