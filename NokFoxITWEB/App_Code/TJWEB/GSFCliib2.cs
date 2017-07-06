using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SFC.TJWEB;

/// <summary>
/// Summary description for AutoDn
/// </summary>
/// 
namespace SFC.TJWEB
{
       
      
    public class GSFCliib2
    {
        N_CreateSO createso = new N_CreateSO();
        //GSFClib1 GSFClib1Pointer = new GSFClib1();
        public void AutoRunSo(string dbtype, string dbconn, string evntype, string dbname) 
        {

                string selectpo = "select distinct poid  from " + dbname + ".[dbo].[PO_CREATE_DT] where  F6 = '2' and F7 = '1'  ";
                DataTable HeaderSqldt = PDataBaseOperation.PSelectSQLDT(dbtype, dbconn, selectpo);

                if (HeaderSqldt.Rows.Count > 0) 
                {
                    for(int i = 0 ; i <HeaderSqldt.Rows.Count  ; i++ )
                    {
                             string poid = HeaderSqldt.Rows[i]["poid"].ToString();
                             string status = createso.WebCreateSO(dbtype, dbconn, evntype, poid,dbname);

                             if (status == "Y") 
                             {
                                 string updatestatusuf7 = "update " + dbname + ".[dbo].[PO_CREATE_DT] set F7 = '2' where poid = '" + poid + "'";
                                 int updatecount = PDataBaseOperation.PExecSQL(dbtype, dbconn, updatestatusuf7);
                             }
                            //createso.WebCreateSO(dbtype, dbconn, evntype, poid);
                    }
                }
            }

            public void AutoRunDn()
            {
              //  string status = createdn
                
            }
    }
}