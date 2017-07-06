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
public class GetSAP3B2
{

    public int GetSapDn3B2( string infokind, string dbtype, string DBReadString, string DBWriString, string systype )
    {
        //DBReadString= ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        //DBWriString = ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        DBReadString = DBReadString; // ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        DBWriString = DBWriString;   // ConfigurationManager.AppSettings["tjl8SMTConnectionString"];


        DataBaseOperation dboRead = new DataBaseOperation("ORACLE", DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString); 
         int iRet =1;
         int i1 = 2;
        //string dbtype="oracle";
 

         try
         {
             string sql = "select INVOICE,PO, UPDFILENAME, SAPFLAG  from PUBLIB.UPD_PODN_LIST_T where     nvl(updflag,'N') <> 'Y' and sapflag='Y' ";
             //string sql = "select INVOICE,PO, UPDFILENAME, SAPFLAG  from PUBLIB.UPD_PODN_LIST_T where    INVOICE ='82269321'"; // nvl(updflag,'N') <> 'Y' and sapflag='Y' ";
            // DataTable DT1 = DataBaseOperation.SelectSQLDT("oracle", DBReadString, sql);
             DataTable DT1 = dboWrite.SelectSQLDT(sql);

             if (DT1.Rows.Count > 0)
             {
 
 
                 for (int i = 0; i < DT1.Rows.Count; i++)
 
                 {
                     string dt = GetSapONE_DN(DT1.Rows[i]["INVOICE"].ToString(), DBReadString, DBWriString);
                  // dt.Rows. 
 
                   

                 }
             }
         }
         catch (Exception ex)
         {
             string sxstr = ex.Message;
             iRet = 0;
             return iRet;
         }
         finally
         {
             
         }

         return iRet;

         
    }


    
     

    public string GetSapONE_DN(string DN, string dboReadS, string dboWriteS)
	{
        try
        {
            DataBaseOperation dboRead = new DataBaseOperation("ORACLE", dboReadS);
            DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", dboWriteS);
            string sql = "select * from publib.dn_3b2header where dn='" + DN + "' ";
            //int mm = dboWrite.ExecSQL(sql);
            DataTable dtdn = dboRead.SelectSQLDT(sql);
            int mm = dtdn.Rows.Count;
            if (mm > 0)
            {
                SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=zh");
               // SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802test;SYSNR=00;USER=lfsupitsap;PASSWD=88foxcon;LANG=zh");
                // ASHOST=10.134.140.157; CLIENT=502;SYSNR=00;USER=SIDCFI;PASSWD=FI61625;LANG=zh");
                con.Open();
                SAPCommand cmd = new SAPCommand(con);
                cmd.CommandText = "EXEC ZRFC_INTEL_B2B_3B2  @T_INPUT=@TINPUT output, @T_OUTPUT=@TOUTPUT output,@T_MSG=@TMSG output";
                SAPParameter TINPUT = new SAPParameter("@TINPUT", ParameterDirection.InputOutput);
                DataTable dt = new DataTable();
                dt.Columns.Add("VBELP");
                DataRow row = dt.NewRow();
                row["VBELP"] ="00"+ DN.ToString ();
                dt.Rows.Add(row);
                TINPUT.Value = dt;
                cmd.Parameters.Add(TINPUT);
                SAPParameter TOUTPUT = new SAPParameter("@TOUTPUT", ParameterDirection.InputOutput);
                cmd.Parameters.Add(TOUTPUT);
                SAPParameter TMSG = new SAPParameter("@TMSG", ParameterDirection.InputOutput);
                cmd.Parameters.Add(TMSG);
                SAPDataReader dr = cmd.ExecuteReader();
                DataTable SAPdt1 = (DataTable)cmd.Parameters["@TOUTPUT"].Value;
                DataTable SAPdt2 = (DataTable)cmd.Parameters["@TMSG"].Value;


                string Error = "";
              if (SAPdt1.Rows.Count > 0)
              {
                  string StrSql = "update publib.dn_3b2header   set  ";
               // StrSql = StrSql + "DN= '" + SAPdt1.Rows[0]["VBELN"] + "',".ToString();
                  StrSql = StrSql + " PO_NUMBER='" + SAPdt1.Rows[0]["BSTKD"] + "',".ToString();
                  StrSql = StrSql + " pro_up='" + SAPdt1.Rows[0]["NETPR"] + "',".ToString();
                  StrSql = StrSql + " pro_upcc='" + SAPdt1.Rows[0]["WAERK"] + "',".ToString();
                  StrSql = StrSql + " trackingtype='" + SAPdt1.Rows[0]["VSART"] + "',".ToString();
                  StrSql = StrSql + " tracking_num= '" + SAPdt1.Rows[0]["TXT2"] + "',".ToString();
                  StrSql = StrSql + " shipfromplant='CS15',".ToString(); //'" + SAPdt1.Rows[0]["WERKS"] + "',".ToString();
                  StrSql = StrSql + " shiptoplant= '" + SAPdt1.Rows[0]["TXT1"] + "',".ToString();
                  StrSql = StrSql + " thirdplplant='" + SAPdt1.Rows[0]["TXT1"] + "',".ToString();
                  StrSql = StrSql + " salesorderno= '" + SAPdt1.Rows[0]["SONUM"] + "' ".ToString();
                  StrSql = StrSql + " WHERE  DN ='" + DN + "'";
//                  string F1 = SAPdt1.Rows[0]["BSTKD"].ToString();
//                  string F2 = SAPdt1.Rows[0]["NETPR"].ToString();
//                  string F3 = SAPdt1.Rows[0]["WAERK"].ToString();
//                  string F4 = SAPdt1.Rows[0]["VSART"].ToString();
//                  string F5 = SAPdt1.Rows[0]["TXT2"].ToString();
//                  string F6 = "CS15";//'" + SAPdt1.Rows[0]["WERKS"] + "',".ToString();
//                  string F7 = SAPdt1.Rows[0]["TXT1"].ToString();
//                  string F8 = SAPdt1.Rows[0]["TXT1"].ToString();
//                  string F9 = SAPdt1.Rows[0]["SONUM"].ToString();
//                  string F10 = DN;
//                  string StrSql1 = @"update publib.dn_3b2header set PO_NUMBER=:V_F1,pro_up=:V_F2,pro_upcc=:V_F3,trackingtype=:V_F4,
//                                    tracking_num=:V_F5,shipfromplant=:V_F6,shiptoplant=:V_F7,thirdplplant=:V_F8,salesorderno=:V_F9 where DN=:V_F10";
//                  int mmm = dboWrite.ExecSQL(StrSql1, new string[] { "V_F1", "V_F2", "V_F3", "V_F4", "V_F5", "V_F6", "V_F7", "V_F8", "V_F9", "V_F10" },
//                    new object[] { F1, F2, F3, F4, F5, F6, F7, F8, F9,F10 });
                  int iii = dboWrite.ExecSQL(StrSql);
                  if (iii > 0)
                  {
                      string StrSqlD = "update publib.dn_3b2DETAIL   set  ";
                      StrSqlD = StrSqlD + "linenumber= '" + SAPdt1.Rows[0]["POSEX"] + "' ,".ToString();//20120726 by ysq 李海陽確認欄位錯誤更改sap抓取欄位（原欄位SOITM）
                      StrSqlD = StrSqlD + "subline_number='" + SAPdt1.Rows[0]["POSEX"] + "'  ".ToString();//20120726 by ysq 李海陽確認欄位錯誤更改sap抓取欄位（原欄位SOITM）
                      StrSqlD = StrSqlD + " WHERE  DN ='" + DN + "'";
                      int aaa=dboWrite.ExecSQL(StrSqlD);
                      if (aaa >= 1)
                      {
                          
                          string docmentId = dtdn.Rows[0]["DOCUMENTID"].ToString();
                          string sqlupdate = "update PUBLIB.UPD_PODN_LIST_T set updflag='Y',UPDFILENAME='" + docmentId + "' where invoice='" + DN + "'";
                          int kkk = dboWrite.ExecSQL(sqlupdate);
                          if (kkk > 0)
                          {
                              string strsql2 = "update publib.dn_3b2header set SAP_TO_UPDFLAG ='Y' where DN='" + DN + "'";
                              int lll = dboWrite.ExecSQL(strsql2);
                              if (lll == 0)
                              {
                                  Error = "Update UPDFLAG Error";
                              }
                          }
                          else
                          {
                              Error = "Update UPD_PODN_LIST_T UPDFLAG Error";
                          }
                      }
                      else
                      {
                          Error = "Update DETAIL Error";
                      }
                      
                  }
                  else
                  {
                      Error = "Update Header Error";
                  }
              }
              else
              {
                  Error = "SAP NO DATA";
              }
              
              string sqlRemark = "update publib.dn_3b2header set REMARK='" + Error + "' where DN='" + DN + "'";
              int ddd = dboWrite.ExecSQL(sqlRemark);
               
            }

     
        return "True";

      } 
      catch (Exception ex)
      {
          string tmp = ex.ToString();
          return "False";
      }
         
     
  }

  


}
