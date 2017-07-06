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
    using System.Configuration;
    using CallSapBom802;
    using SAP.Connector;

    /// <summary>
    ///		WFrmMachineRate 的摘要描述。
    /// </summary>
    public partial class SapBomImport : System.Web.UI.UserControl
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
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            //btComPare.Text = (String)GetGlobalResourceObject("SFCQuery", "ComPare");
            dgSapBom.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery","SAPHONHAIPN");
            dgSapBom.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery","SAPLOCATION");
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dgSapBom.CurrentPageIndex = 0;
            ShowData();
        }

        private void ShowData()
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
                dgSapBom.DataSource = subitem;
                // ( OR ) DataTable ss = subitem.ToADODataTable();
                //        dgSapBom.DataSource = ss.DefaultView;
                int i = subitem.Count;
                if (i == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SAP中無資料返回!');</script>");
                    dgSapBom.Visible = false;
                    Label1.Visible = false;
                    return;
                }



                dgSapBom.DataBind();
                dgSapBom.Visible = true;
                Label1.Visible = true;
                Label1.Text = "Current Page:" + (dgSapBom.CurrentPageIndex + 1).ToString() + "/" + dgSapBom.PageCount.ToString() + " Total:" + i.ToString();   
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void dgSapBom_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgSapBom.CurrentPageIndex = e.NewPageIndex;
            ShowData();
        }
}
}

