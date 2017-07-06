using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace Economy.BLL
{
    /// <summary>
    /// Summary description for CoverOrNot
    /// </summary>

    public class CoverOrNot
    {
        public CoverOrNot()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool DailyIncomingExist(string tableDate,string bu)
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["DefaultConnectionString"];
                SqlConnection sconn = new SqlConnection(connString);
                sconn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sconn;
                if (bu == "NLV")
                {
                    cmd.CommandText = "SELECT count(*) FROM MPE_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }
                else if (bu == "FKD")
                {
                    cmd.CommandText = "SELECT count(*) FROM FKD_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }
                else if (bu == "MPB")
                {
                    cmd.CommandText = "SELECT count(*) FROM MPB_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DailyIncomingCover(string tableDate, string bu)
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["DefaultConnectionString"];
                SqlConnection sconn = new SqlConnection(connString);
                sconn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sconn;
                if (bu == "NLV")
                {
                    cmd.CommandText = "DELETE FROM MPE_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }
                else if (bu == "FKD")
                {
                    cmd.CommandText = "DELETE FROM FKD_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }
                else if (bu == "MPB")
                {
                    cmd.CommandText = "DELETE FROM MPB_DailyIncoming WHERE Table_Date='" + tableDate + "'";
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }
    }
}