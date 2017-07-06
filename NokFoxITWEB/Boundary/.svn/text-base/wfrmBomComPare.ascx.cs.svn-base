/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/11
 * 
 * ***********************************************************************/
namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Reflection;
    using System.Resources;
    using DBAccess.EAI;
    using DB.EAI;
    using SAP.Connector;
    using CallSapBom802;
    using System.Data.OracleClient;
    using System.Configuration;

    /// <summary>
    ///		WFrmMachineRate 的摘要描述。
    /// </summary>
    public partial class BomComPare : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiLanaguage();
            }
        }

        private void MultiLanaguage()
        {
            lbPn.Text = (String)GetGlobalResourceObject("SFCQuery", "MaterialNO");
            btnComPare.Text = (String)GetGlobalResourceObject("SFCQuery", "ComPare");

            dgBom.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "MaterialNO");
            dgBom.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SAPHONHAIPN");
            dgBom.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SAPLOCATION");
            dgBom.Columns[3].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SMTHONHAIPN");
            dgBom.Columns[4].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SMTLOCATION");

        }

        private bool InsertData()
        {
            try
            {
                string Datuv = DateTime.Now.ToString("yyyy/MM/dd");
                string Dwerk = "";
                string Matnr = tbPN.Text.ToUpper().Trim();
                string Revlv = "";
                string SapConn = ConfigurationSettings.AppSettings["CallSap"].ToString();
                SAPProxy1 SapClient = new SAPProxy1(SapConn);
                ZBOM_SUBITEMTable subitem = new ZBOM_SUBITEMTable();
                SapClient.Zrfc_Mcm_Bom_Subitem(Datuv, Dwerk, Matnr, Revlv, ref subitem);
                DataTable ss = subitem.ToADODataTable();

                string HONHAIPN;
                string LOCATION;
                string StrSql;

                StrSql = "delete from SAP.SAP_BOM_LOCATION where MATERIAL_NUMBER=" + ClsCommon.GetSqlString(Matnr);
                ClsGlobal.objDataConnect.DataExecute(StrSql);

                foreach (DataRow rw in ss.Rows)
                {
                    HONHAIPN = rw["IDNRK"].ToString();
                    LOCATION = rw["EBORT"].ToString();
                    StrSql = "insert into SAP.SAP_BOM_LOCATION (MATERIAL_NUMBER,COMPONEMT,LOCATION) values ("
                             + ClsCommon.GetSqlString(Matnr) + ","
                             + ClsCommon.GetSqlString(HONHAIPN) + ","
                             + ClsCommon.GetSqlString(LOCATION) + ")";
                    ClsGlobal.objDataConnect.DataExecute(StrSql);
                }

                if (ss.Rows.Count == 0)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SAP中無資料返回!');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
                return false;
            }

            return true;
        }

        private void ComPareBom()
        {
            string DOV = tbPN.Text.ToUpper().Trim();
            string Sql = "select PN,SAPHonhaipn,SAPLocation,SMTHonhaipn,SMTLocation from ("
                        + " select s.MATERIAL_NUMBER PN,s.COMPONEMT SAPHonhaipn,s.LOCATION SAPLocation,t.COMPONEMT SMTHonhaipn,t.LOCATION SMTLocation "
                        + "  from (select a.MATERIAL_NUMBER,a.COMPONEMT,a.LOCATION from SAP.SAP_BOM_LOCATION a"
                        + "          where a.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "   minus"
                        + "   select b.MATERIAL_NUMBER,b.COMPONEMT,b.LOCATION from MCM_SMT_PROGRAMBOM b"
                        + "          where b.MATERIAL_NUMBER= " + ClsCommon.GetSqlString(DOV)
                        + "    ) s"
                        + "   right join"
                        + "   (select c.MATERIAL_NUMBER,c.COMPONEMT,c.LOCATION from MCM_SMT_PROGRAMBOM c"
                        + "           where c.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "    minus"
                        + "    select d.MATERIAL_NUMBER,d.COMPONEMT,d.LOCATION from SAP.SAP_BOM_LOCATION d"
                        + "           where d.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "    ) t"
                        + "    on (s.LOCATION = t.LOCATION or s.COMPONEMT = t.COMPONEMT)"
                        + " union "
                        + " select s.MATERIAL_NUMBER PN,s.COMPONEMT SAPHonhaipn,s.LOCATION SAPLocation,t.COMPONEMT SMTHonhaipn,t.LOCATION SMTLocation "
                        + "  from "
                        + "  (select a.MATERIAL_NUMBER,a.COMPONEMT,a.LOCATION from SAP.SAP_BOM_LOCATION a"
                        + "          where a.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "   minus"
                        + "   select b.MATERIAL_NUMBER,b.COMPONEMT,b.LOCATION from MCM_SMT_PROGRAMBOM b"
                        + "          where b.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "  ) s"
                        + "  left join"
                        + " (select c.MATERIAL_NUMBER,c.COMPONEMT,c.LOCATION from MCM_SMT_PROGRAMBOM c"
                        + "         where c.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "  minus"
                        + "  select d.MATERIAL_NUMBER,d.COMPONEMT,d.LOCATION from SAP.SAP_BOM_LOCATION d"
                        + "         where d.MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOV)
                        + "  ) t"
                        + " on (s.LOCATION = t.LOCATION or s.COMPONEMT = t.COMPONEMT)"
                        + " ) where SAPHonhaipn<>" + ClsCommon.GetSqlString(DOV);

            DataTable ComPateData = new DataTable();
            ComPateData = ClsGlobal.objDataConnect.DataQuery(Sql).Tables[0];
            dgBom.DataSource = ComPateData.DefaultView;
            dgBom.DataBind();
            Label1.Visible = true;
            dgBom.Visible = true;
            Label1.Text = "Current Page:" + (dgBom.CurrentPageIndex + 1).ToString() + "/" + dgBom.PageCount.ToString() + " Total:" + ComPateData.Rows.Count.ToString();   
        }

        private bool CheckSmt()
        {
            string DOCUMENTNO = tbPN.Text.ToUpper().Trim();
            string StrSql = "select *  from MCM_SMT_PROGRAMBOM where MATERIAL_NUMBER=" + ClsCommon.GetSqlString(DOCUMENTNO);
            if (ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0].Rows.Count == 0)
            {
               // Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SMT BOM未導入!');</script>");
                return false;
            }
            return true;
        }

        protected void dgSapBom_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgBom.CurrentPageIndex = e.NewPageIndex;
            ComPareBom();
        }
        protected void btnComPare_Click(object sender, EventArgs e)
        {
            dgBom.CurrentPageIndex = 0;
            if (!CheckSmt())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SMT BOM未導入!');</script>");
                dgBom.Visible = false;
                Label1.Visible = false;
                return;
            };
            if (!InsertData())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SAP中無資料返回!');</script>");
                dgBom.Visible = false;
                Label1.Visible = false;
                return;
            };
            ComPareBom();
        }
}
}

