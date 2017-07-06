using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TJSY
/// </summary>
public class TJSY
{
    public TJSY()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private OracleConnection Rcon;
    private OracleConnection Wcon;

    public void getcon(string DBReadString, string DBWriString)
    {
        Rcon = new OracleConnection(DBReadString);
        Wcon = new OracleConnection(DBWriString);
    }

    public void check3s4s(string DBReadString, string DBWriString)
    {
        int i = 0, j = 0, k = 0;
        string sqlcheck = null;
        string[] dn = new string[0];
        string[] nocheckdn = new string[0];
        getcon(DBReadString, DBWriString);
        try
        {
            Rcon.Open();
            Wcon.Open();
            sqlcheck = "select a.INVOICE from PUBLIB.UPD_PODN_LIST_T a where a.FLAG3S4S is null";
            using (OracleCommand com = new OracleCommand(sqlcheck, Wcon))
            {
                OracleDataReader odr = null;
                odr = com.ExecuteReader();
                while (odr.Read())
                {
                    Array.Resize(ref dn, dn.GetLength(0) + 1);
                    dn[i] = odr[0].ToString();
                    i++;
                }
            }
            for (j = 0; j <= i - 1; j++)
            {
                string sqlcheck1 = " SELECT DISTINCT invoice_number  FROM sap.cmcs_sfc_packing_lines_all where invoice_number='" + dn[j] + "' and internal_carton IS NULL";
                using (OracleCommand com1 = new OracleCommand(sqlcheck1, Rcon))
                {
                    OracleDataReader odr1 = null;
                    odr1 = com1.ExecuteReader();
                    if (!odr1.Read())
                    {
                        Array.Resize(ref nocheckdn, nocheckdn.GetLength(0) + 1);
                        nocheckdn[nocheckdn.GetLength(0) - 1] = dn[j];
                    }

                }
            }
            string a = null;
            for (k = 0; k <= j; k++)
            {
                string sqlcheck2 = "update PUBLIB.UPD_PODN_LIST_T set FLAG3S4S = 'Y' where INVOICE = '" + nocheckdn[k] + "'";
                OracleCommand com2 = null;
                com2 = new OracleCommand(sqlcheck2, Wcon);
                int ret = com2.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            Rcon.Close();
            Wcon.Close();
        }
    }

    public void check3s4sByCustomer(string DBReadString, string DBWriString, string Usertype)
    {
        int i = 0, j = 0, k = 0;
        string sqlcheck = null;
        string[] dn = new string[0];
        string[] nocheckdn = new string[0];
        getcon(DBReadString, DBWriString);
        try
        {
            Rcon.Open();
            Wcon.Open();
            sqlcheck = "select a.INVOICE from " + Usertype + ".UPD_PODN_LIST_T a where a.FLAG3S4S is null";
            using (OracleCommand com = new OracleCommand(sqlcheck, Wcon))
            {
                OracleDataReader odr = null;
                odr = com.ExecuteReader();
                while (odr.Read())
                {
                    Array.Resize(ref dn, dn.GetLength(0) + 1);
                    dn[i] = odr[0].ToString();
                    i++;
                }
            }
            for (j = 0; j <= i - 1; j++)
            {
                string sqlcheck1 = " SELECT DISTINCT invoice_number  FROM sap.cmcs_sfc_packing_lines_all where invoice_number='" + dn[j] + "' and internal_carton IS NULL";
                using (OracleCommand com1 = new OracleCommand(sqlcheck1, Rcon))
                {
                    OracleDataReader odr1 = null;
                    odr1 = com1.ExecuteReader();
                    if (!odr1.Read())
                    {
                        Array.Resize(ref nocheckdn, nocheckdn.GetLength(0) + 1);
                        nocheckdn[nocheckdn.GetLength(0) - 1] = dn[j];
                    }

                }
            }
            string a = null;
            for (k = 0; k <= j - 1; k++)
            {
                string sqlcheck2 = "update " + Usertype + ".UPD_PODN_LIST_T set FLAG3S4S = 'Y' where INVOICE = '" + nocheckdn[k] + "'";
                OracleCommand com2 = null;
                com2 = new OracleCommand(sqlcheck2, Wcon);
                int ret = com2.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            Rcon.Close();
            Wcon.Close();
        }
    }
}
