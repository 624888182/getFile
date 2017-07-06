using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for TGETWO2
/// </summary>
public class TGETWO2
{
	public TGETWO2()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int StrToIntDef(string str)
    {
        int iRet = 0;
        try
        {
            iRet = Convert.ToInt32(str);
        }
        catch
        {
            iRet = 0;
        }
        return iRet;
    }

    private string GetSTKRM(string STKRM)
    {
        string sRet = "";
        if (STKRM.Substring(STKRM.Length-1) == "G")
            sRet = "INV";
        if (STKRM.Substring(STKRM.Length-1) == "K")
            sRet = "KIT";
        return sRet;
    }


    /*   Update MRP02.JINVM2  Sum Inv & Kit quantity
     *   parameter
     *   dbtype: "oracle"
     *   dbRead: read database connection string 
     *   dbWrite: write database connection string
     *   UpdateDate: updating date time formate: yyyyMMddHHmmss note: no space
     *   PlantCode: Sap plant code
     *   KeyPartNo: Key part no
     *   STKRM: storage location
     *   Qty: quantity
     */
    public int UpdateStock(string dbtype, string dbRead, string dbWrite, string UpdateDate,string PlantCode, string KeyPartNo, string STKRM, int Qty)
    {
        int iRet = 0;
        string sBeginQty = "";
        string sEndQty = "";
        string CDATE = "";
        bool CalcStock = false;
        DataBaseOperation dbo = new DataBaseOperation(dbtype, dbWrite);
        string sSqlSel = "Select Begin_QTY,END_QTY,UPDDATE From MRP02.JINVM2 Where PLANTCODE = :V_PLANT And PART = :V_PART And STKRM = :V_ST ";
        string sSqlExec = "";
        DataTable dtStock = dbo.SelectSQLDT(sSqlSel, new string[] { "V_PLANT", "V_PART", "V_ST" }, new object[] { PlantCode, KeyPartNo, STKRM });
        if (dtStock.Rows.Count > 0)
        {
            sBeginQty = dtStock.Rows[0][0].ToString();
            sEndQty = dtStock.Rows[0][1].ToString();
            CDATE = dtStock.Rows[0][2].ToString();
            if (UpdateDate.CompareTo(CDATE) > 0)
            {
                sSqlExec = "Update  MRP02.JINVM2 Set Begin_Qty = END_QTY  Where PLANTCODE = :V_PLANT And PART = :V_PART And STKRM = :V_ST ";
                int iTmp = dbo.ExecSQL(sSqlExec, new string[] { "V_PLANT", "V_PART", "V_ST" }, new object[] { PlantCode, KeyPartNo, STKRM });
                sBeginQty = sEndQty;
                sSqlExec = "Update MRP02.JINVM2 Set END_QTY = :V_QTY ,UPDDATE = :V_DATE Where PLANTCODE = :V_PLANT And PART = :V_PART And STKRM = :V_ST ";
                iTmp = dbo.ExecSQL(sSqlExec, new string[] { "V_PLANT", "V_PART", "V_ST", "V_QTY","V_DATE" }, new object[] { PlantCode, KeyPartNo, STKRM, Qty,UpdateDate});
                CalcStock = true;
            }
        }
        else
        {
            sSqlExec = "Insert Into MRP02.JINVM2  (PLANTCODE,PART,STKRM,BEGIN_QTY,END_QTY,CREDATE,UPDDATE ) " +
                        "Values (:V_PLANT,:V_PART,:V_ST,0,:V_QTY,to_char(sysdate,'yyyyMMddHH24miss'),:V_UPDDATE )";
            int iTmp = dbo.ExecSQL(sSqlExec, new string[] { "V_PLANT", "V_PART", "V_ST", "V_QTY", "V_UPDDATE" }, new object[] { PlantCode, KeyPartNo, STKRM, Qty, UpdateDate });
            CalcStock = true;        
        }
        dtStock.Dispose();
        if (CalcStock)
        {
            string sType = GetSTKRM(STKRM);
            if (sType != "")
            {
                int iBegin = StrToIntDef(sBeginQty);
                sSqlExec = "Update MRP02.JINVM2 Set Begin_QTY = END_QTY Where PLANTCODE = :V_PLANT And PART = :V_PART And STKRM = :V_ST ";
                int iCount = dbo.ExecSQL(sSqlExec, new string[] {  "V_PLANT", "V_PART", "V_ST" }, new object[] {  PlantCode, KeyPartNo, sType });
                sSqlExec = "Update MRP02.JINVM2 Set END_QTY = END_QTY - :V_BEGIN + :V_END,UPDDATE = :V_DATE Where PLANTCODE = :V_PLANT And PART = :V_PART And STKRM = :V_ST ";
                iCount = dbo.ExecSQL(sSqlExec, new string[] { "V_BEGIN", "V_END", "V_PLANT", "V_PART", "V_ST","V_DATE" }, new object[] { iBegin, Qty, PlantCode, KeyPartNo, sType,UpdateDate});
                if (iCount <= 0)
                {
                    sSqlExec = "Insert Into MRP02.JINVM2 (END_QTY,PLANTCODE,PART,STKRM,UPDDATE,CREDATE) Values (:V_QTY,:V_PLANT,:V_PART,:V_ST,:V_DATE,To_Char(sysdate,'yyyyMMddHH24Miss')) ";
                    iCount = dbo.ExecSQL(sSqlExec,new string[] {"V_QTY","V_PLANT","V_PART","V_ST","V_DATE"},new object[] {Qty,PlantCode,KeyPartNo,sType,UpdateDate} );
                }
            }
        }
        dbo.Dispose();
        return iRet;
    }

    public int SAPREPARTGPUPdate(string dbtype, string dbRead, string dbWrite)
    {
        int iRet = 0;
        DataBaseOperation dboRead  = new DataBaseOperation(dbtype,dbRead);
        DataBaseOperation dboWrite  = new DataBaseOperation(dbtype,dbWrite);
        string sSql = "Select PLANTCODE,MODEL,SAPGPNO,KEY_PART_NO,SEQNO,MAINQTY,MAININV,DOCUMENTID,FLAG1,ALLOCATED,DataType from MRP02.SAPREPARTGP_TMP Where Flag2 = '' or Flag2 is null order by DOCUMENTID,PLANTCODE,MODEL,SAPGPNO,KEY_PART_NO,SEQNO";
        DataTable dt = dboRead.SelectSQLDT(sSql);
        string O_Plant = "";
        string O_Model = "";
        string O_SAPGPNO = "";
        string O_DocID = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string PlantCode = dt.Rows[i]["PlantCode"].ToString();
            string Model = dt.Rows[i]["MODEL"].ToString();
            string SAPGPNO = dt.Rows[i]["SAPGPNO"].ToString();
            string KEY_PART_NO = dt.Rows[i]["KEY_PART_NO"].ToString();
            string SEQNO = dt.Rows[i]["SEQNO"].ToString();
            string MAINQTY = dt.Rows[i]["MAINQTY"].ToString();
            string MAININV = dt.Rows[i]["MAININV"].ToString();
            string DOCUMENTID = dt.Rows[i]["DOCUMENTID"].ToString();
            string FLAG1 = dt.Rows[i]["FLAG1"].ToString();
            string DataType = dt.Rows[i]["DATATYPE"].ToString();
            string ALLOCATED = dt.Rows[i]["ALLOCATED"].ToString();
            string sCDATE = dt.Rows[i]["DOCUMENTID"].ToString();
            sCDATE = sCDATE.Substring(0,14);
            string sSqlSel = "";
            string sSqlExec = "";
            if (DataType.Trim() == "1")
            {
                if ((PlantCode.Trim() != "") && (Model.Trim() != "") && (KEY_PART_NO.Trim() != ""))
                {
                    sSqlSel = "Select Count(*) From MRP02.SAPREPARTGP Where PLANTCODE = :V_PLANT And MODEL = :V_MODEL And KEY_PART_NO = :V_Part ";
                    DataTable dtSel = dboWrite.SelectSQLDT(sSqlSel, new string[] { "V_PLANT", "V_MODEL", "V_Part" }, new object[] { PlantCode, Model, KEY_PART_NO });

                    if (dtSel.Rows[0][0].ToString() == "0")
                        sSqlExec = @"Insert Into MRP02.SAPREPARTGP 
                                    (PLANTCODE,MODEL,SAPGPNO,KEY_PART_NO,SEQNO,CDATE,FLAG1,DOCUMENTID,ALLOCATED)
                                    Values
                                    (:V_PLANTCODE,:V_MODEL,:V_SAPGPNO,:V_KeyPT,:V_SEQNO,'" + DateTime.Now.ToString("yyyyMMdd") + "',:V_FLag1,:V_ID,:V_ALLOCATED)";
                    else
                        sSqlExec = @"Update MRP02.SAPREPARTGP Set SAPGPNO = :V_SAPGPNO,SEQNO = :V_SEQNO,FLAG1 = :V_FLag1,DOCUMENTID = :V_ID,ALLOCATED = :V_ALLOCATED
                                     Where  PLANTCODE = :V_PLANTCODE And MODEL = :V_MODEL And KEY_PART_NO = :V_KeyPT ";
                    int iCount = dboWrite.ExecSQL(sSqlExec, new string[] { "V_PLANTCODE", "V_MODEL", "V_SAPGPNO", "V_KeyPT", "V_SEQNO", "V_FLag1", "V_ID","V_ALLOCATED" },
                                        new object[] { PlantCode, Model, SAPGPNO, KEY_PART_NO, SEQNO, FLAG1, DOCUMENTID,ALLOCATED });
                    dtSel.Dispose();

                }
            }
            else
            {
                int QTY = StrToIntDef(MAINQTY);
                this.UpdateStock(dbtype, dbRead, dbWrite, sCDATE, PlantCode, KEY_PART_NO, MAININV, QTY);
            }
            if ((O_DocID != DOCUMENTID) || (i == dt.Rows.Count -1))
            {
                if (O_DocID != "") 
                {
                    string sSqlUpdate = @"Update MRP02.SAPREPARTGP_TMP set Flag2 = 'Y' where DocumentID = :V_ID ";
                    dboRead.ExecSQL(sSqlUpdate, new string[] { "V_ID" }, new object[] { O_DocID });
                }
                O_DocID = DOCUMENTID;
            }
        }
        dt.Dispose();
        return iRet;
    }
}
