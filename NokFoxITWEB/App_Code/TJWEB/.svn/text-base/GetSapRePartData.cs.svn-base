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
public class GetSapBOMINVData
{

   // GetSapRePartData (  廠區, 成品料號, 料號, 倉別1, 倉別2 ) , 
    public int GetSapRePartData(string Plantcode, string Model, string PartNo, string Location1, string Location2, string DBReadString, string DBWriString)
    { 
        //string DBReadString =  ConfigurationManager.AppSettings["ConnectionSqlServer"];
        //string DBWriString  =  ConfigurationManager.AppSettings["tjl8SMTConnectionString"];

        //DBReadString = DBReadString; // ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        //DBWriString = DBWriString;   // ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        DataBaseOperation dboRead  = new DataBaseOperation("ORACLE", DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString);
        int iRet = 1;
        int i1 = 2;
        //string dbtype="oracle";
        try
        {   
            string sqlstr1 = "select mb002 workorder ,mb007 partno from mrp02.fdlmb";
            DataTable DT1 = dboWrite.SelectSQLDT(sqlstr1);
           // GetSapBOM("WSJB", DT1.Rows[0]["partno"].ToString()); 
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
 
     public int ExecuteGetSap( string Dread, string DWrite,string type)
     {
        //string DBReadString = ConfigurationManager.AppSettings["LFSFCConnectionString"];
        //string DBWriString = ConfigurationManager.AppSettings["tjl8SMTConnectionString"];

        string DBReadString = Dread;
        string DBWriString =  DWrite;

        DataBaseOperation dboRead  = new DataBaseOperation("ORACLE", DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString);
        string Startdate = DateTime.Now.AddDays(-7).ToString("yyyyMMddHHmm");
        string sqlstr1 = "";
        if (type == "1")
        {
            sqlstr1 = "select distinct mb002 workorder ,mb004 partno from mrp02.fdlmb where create_date >= '" + Startdate + "' and mb002  not in ( select wo from  mrp02.woctl ) ";
        }
        else
        {
            sqlstr1 = "select distinct mb002 workorder ,mb004 partno from mrp02.fdlmb where mb002  not in ( select wo from  mrp02.woctl )";
            //create_date like '" + Startdate.Substring(0,8) + "%' and
        }
       //  string sqlstr1 = "select  distinct mb002 workorder ,mb004 partno from mrp02.fdlmb ";
        DataTable DT1 = dboRead.SelectSQLDT(sqlstr1);
        //try
        //{   
          for (int i = 0; i < DT1.Rows.Count; i++)
            {
                try
                {
                    GetSapBOM("WSJB", DT1.Rows[i]["partno"].ToString(), DBReadString, DBWriString);
                }
                catch (Exception ex)
                {
                    string sxstr = ex.Message;
                }
                sqlstr1 = " insert into mrp02.woctl ( wo,wo_date)   values ('" + DT1.Rows[i]["workorder"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                dboRead.ExecSQL(sqlstr1);
            }
          
          return 0;
        //}
        //catch(Exception ex)
        //{
        //    string sxstr = ex.Message;
        //   // return 1;
        //}

    }
     public int GetSapWorkorder(string workorder, string dbRead, string dbWrite)
    {
        //string DBReadString = ConfigurationManager.AppSettings["LFSFCConnectionString"];
        //string DBWriString  = ConfigurationManager.AppSettings["tjl8SMTConnectionString"];
        string DBWriString = dbWrite;
        string DBReadString = dbRead;
        DataBaseOperation dboRead  = new DataBaseOperation("ORACLE", DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString );
        int iRet = 1;
        int i1   = 2; 
        try
        {
            string sqlstr1 = "select mb002 workorder ,mb004 partno from mrp02.fdlmb where mb002='"+workorder +"'";
            DataTable DT1 = dboWrite.SelectSQLDT(sqlstr1);
            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                GetSapBOM("WSJB", DT1.Rows[0]["partno"].ToString(), DBReadString, DBWriString);
            } 
            sqlstr1 = " insert into mrp02.woctl ( wo,wo_date)   values ('" + workorder + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            dboRead.ExecSQL(sqlstr1);
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

    public string   GetSapBOM( string plant, string partno,string dbRead,string dbWrite )
    {
        string documentid;        
        documentid =DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //string DBWriString = ConfigurationManager.AppSettings["LFTestL6"];
        //string DBReadString = ConfigurationManager.AppSettings["LFTestL6"]; 
        string DBWriString = dbWrite;
        string DBReadString = dbRead;
        DataBaseOperation dboRead = new DataBaseOperation("ORACLE", DBReadString);
        DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString);
        SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=en");
        //SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802test;SYSNR=00;USER=lfsupitsap;PASSWD=99foxcon;LANG=ZH");
        con.Open();
        SAPCommand cmd = new SAPCommand(con);
        cmd.CommandText = "EXEC ZRFC_MM_BOM_EXPL  @MATERIAL=@MATERIALV,@PLANT=@PLANTV,@ALT_BOM=@ALT_BOMV,@BOM_APP=@BOM_APPV ,@VALID_FROM=@VALID_FROMV  ,";
        cmd.CommandText = cmd.CommandText + " @ITEM_CAT=@ITEM_CATV ,@TOPMAT=@TOPMATV output   ,@BOM_ITEMS=@BOM_ITEMSV output   ";


        SAPParameter MATERIALV = new SAPParameter("@MATERIALV", ParameterDirection.Input);
        MATERIALV.Value = partno ;
        cmd.Parameters.Add(MATERIALV);
        SAPParameter PLANTV = new SAPParameter("@PLANTV", ParameterDirection.Input);
        PLANTV.Value = plant;
        cmd.Parameters.Add(PLANTV);
        SAPParameter ALT_BOMV = new SAPParameter("@ALT_BOMV", ParameterDirection.Input);
        ALT_BOMV.Value = "1".ToString();
        cmd.Parameters.Add(ALT_BOMV);
        SAPParameter BOM_APPV = new SAPParameter("@BOM_APPV", ParameterDirection.Input);
        BOM_APPV.Value = "PP01".ToString();
        cmd.Parameters.Add(BOM_APPV);
        SAPParameter VALID_FROMV = new SAPParameter("@VALID_FROMV", ParameterDirection.Input);
        VALID_FROMV.Value = DateTime.Now.ToString("yyyyMMdd");
        cmd.Parameters.Add(VALID_FROMV);
        SAPParameter ITEM_CATV = new SAPParameter("@ITEM_CATV", ParameterDirection.Input);
        ITEM_CATV.Value = "L".ToString();
        cmd.Parameters.Add(ITEM_CATV);
        SAPParameter TOPMATV = new SAPParameter("@TOPMATV", ParameterDirection.Output);      
        cmd.Parameters.Add(TOPMATV);
        SAPParameter BOM_ITEMSV = new SAPParameter("@BOM_ITEMSV", ParameterDirection.InputOutput);
        cmd.Parameters.Add(BOM_ITEMSV);
        SAPDataReader dr = cmd.ExecuteReader();
        DataTable SAPdt1 = (DataTable)cmd.Parameters["@BOM_ITEMSV"].Value;
        try
        {string DOCUMENTID=System.DateTime.Now.ToLongDateString ();    

            if (SAPdt1.Rows.Count > 0)
            {
                //去除刪除動作----20120615------by--ysq
                //string sql = "delete mrp02.SAPREPARTGP_TMP  where MODEL ='" + partno + "' ";
                //dboWrite.ExecSQL(sql);

                //FLAG2,FLAG1,EQTY,KITINV,KITQTY,GOODINV,GOODQTY,MAININV,MAINQTY,ALLOCATED, allocated,flag1
                for (Int16 i = 0; i < SAPdt1.Rows.Count;i++ )
                {
                    string StrSql = "insert into  mrp02.SAPREPARTGP_TMP (SEQNO,KEY_PART_NO,SAPGPNO,MODEL,PLANTCODE, DOCUMENTID,CDATE,FLAG1,ALLOCATED,DATATYPE) values( '";
                    StrSql = StrSql + SAPdt1.Rows[i]["WEGXX"].ToString() + "','";
                    StrSql = StrSql + SAPdt1.Rows[i]["IDNRK"].ToString() + "','";
                    StrSql = StrSql + SAPdt1.Rows[i]["ALPGR"].ToString() + "','";
                    StrSql = StrSql + partno + "','";
                    StrSql = StrSql + plant + "','";
                    StrSql = StrSql + documentid + "', '";
                    StrSql = StrSql + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','";
                    StrSql = StrSql + SAPdt1.Rows[i]["SORTF"].ToString() + "', '";
                    StrSql = StrSql + SAPdt1.Rows[i]["EWAHR"].ToString() + "','1')";
                    int iii = dboWrite.ExecSQL(StrSql);
                }
                string sql = "select plantcode as WERKS,key_part_no as MATNR,'' as LGORT   from mrp02.SAPREPARTGP_TMP where    DOCUMENTID='" + documentid + "'";
                //string sql = "select  PLANTCODE as WERKS, KEY_PART_NO  as MATNR,'' as LGORT,MODEL ,SAPGPNO, SEQNO , CDATE ,ALLOCATED ,MAINQTY, MAININV , GOODINV,  KITINV, FLAG2,DOCUMENTID  from mrp02.SAPREPARTGP_TMP where DOCUMENTID='" + documentid + "'";
                  DataTable dt2=  dboWrite.SelectSQLDT(sql);
                  GetSapInv(dt2, documentid, dboRead, dboWrite);
             
            }
    

        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            //return "False";
        }

         return "";
    }


    public string GetSapInv(DataTable dt2, string Documnetid, DataBaseOperation dboRead, DataBaseOperation dboWrite)
    {
        DataTable SAPdt1;
        try
        {
            //string DBWriString = ConfigurationManager.AppSettings["LFTestL6"];
            //string DBReadString = ConfigurationManager.AppSettings["LFTestL6"];
            //DataBaseOperation dboRead = new DataBaseOperation("ORACLE", DBReadString);
            //DataBaseOperation dboWrite = new DataBaseOperation("ORACLE", DBWriString);
            SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802;SYSNR=00;USER=RFCTJIDM;PASSWD=TJSAPRFC;LANG=en");
            //SAPConnection con = new SAPConnection("ASHOST=10.134.28.98; CLIENT=802test;SYSNR=00;USER=lfsupitsap;PASSWD=99foxcon;LANG=ZH");
            con.Open();
            SAPCommand cmd = new SAPCommand(con);
            cmd.CommandText = "EXEC ZRFC_MM_NPIMS_0001  @INPUT=@INPUTv output,@output=@outputv output";

            SAPParameter INPUTv = new SAPParameter("@INPUTv", ParameterDirection.InputOutput);

             
             INPUTv.Value = dt2;
            cmd.Parameters.Add(INPUTv);
            SAPParameter outputv = new SAPParameter("@outputv", ParameterDirection.InputOutput);
            cmd.Parameters.Add(outputv);
            SAPDataReader dr = cmd.ExecuteReader();
            SAPdt1 = (DataTable)cmd.Parameters["@outputv"].Value;  

            for (Int16 i = 0; i < SAPdt1.Rows.Count; i++)
            {
                string StrSql = "insert into  mrp02.SAPREPARTGP_TMP (KEY_PART_NO,MAININV,mainqty,goodqty,plantcode, DOCUMENTID, DATATYPE) values( '";

                StrSql = StrSql + SAPdt1.Rows[i]["MATNR"].ToString() + "','";
                StrSql = StrSql + SAPdt1.Rows[i]["LGORT"].ToString() + "',";
                StrSql = StrSql + SAPdt1.Rows[i]["LABST"].ToString() + ",";
                StrSql = StrSql + SAPdt1.Rows[i]["INSME"].ToString() + ",'";
                StrSql = StrSql + SAPdt1.Rows[i]["WERKS"].ToString() + "','";
                StrSql = StrSql + Documnetid + "','2')";
                int iii = dboWrite.ExecSQL(StrSql);
            }
            //去除更新刪除動作更新Flag2 更改為更新DATATYPE欄位----20120619------by--ysq
             //string Update_Sql ="update mrp02.SAPREPARTGP_TMP  a ";
             //Update_Sql = Update_Sql + "set plantcode=(select plantcode  from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "' and FLAG2 !='1' and rownum=1 ), ";
             //Update_Sql = Update_Sql + "model=(select model from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "' and FLAG2 !='1' and rownum=1),";
             //Update_Sql = Update_Sql + "cdate=(select cdate from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "' and FLAG2 !='1' and rownum=1 ),";
             //Update_Sql = Update_Sql + "Sapgpno=(select Sapgpno from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "'and FLAG2 !='1' and rownum=1 ),";
             //Update_Sql = Update_Sql + "seqno=(select seqno from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "'and FLAG2 !='1' and rownum=1 ),";
             //Update_Sql = Update_Sql + "FLAG1=(select FLAG1 from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "'and FLAG2 !='1' and rownum=1 ),";
             //Update_Sql = Update_Sql + "ALLOCATED=(select ALLOCATED from mrp02.SAPREPARTGP_TMP where key_part_no =a.key_part_no and DOCUMENTID ='" + Documnetid + "'and FLAG2 !='1' and rownum=1 )";
             //Update_Sql = Update_Sql + " where DOCUMENTID='" + Documnetid + "' AND   FLAG2 ='1'";
             //dboWrite.ExecSQL(Update_Sql);
             //string Del_Sql = "delete mrp02.SAPREPARTGP_TMP where  DOCUMENTID ='" + Documnetid + "'  AND   FLAG2 !='1' ";
             //dboWrite.ExecSQL(Del_Sql);             
        }

        catch (Exception ex)
        {
            string tmp = ex.ToString();
            //return "False";
        }
        return "";
    }

}
