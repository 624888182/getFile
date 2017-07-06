/*************************************************************************
 * 
 *  Unit description: Search the HW version by PID or Model
 *  Developer: Zhang Ji Jing            Date: 2008/09/16
 *  Modifier : Zhang Ji Jing            Date:  
 * 
 * ***********************************************************************/

    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using DB.EAI;
    using System.Reflection;
    using System.Globalization;
    using System.Resources;
    using DBAccess.EAI;
    using System.Data.OracleClient;
    using Excel = Microsoft.Office.Interop.Excel;

    public partial class Boundary_WfrmHWqueryAssy : System.Web.UI.UserControl
    {
        private static string strCount; 
        protected void Page_Load(object sender, EventArgs e)
        {
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                MultiLanguage();
                //BindModel();
            }
        }

        private void MultiLanguage()
        {

            btnquery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query"); 
            //lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");  
            //lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
        }

        //private void BindModel()
        //{
        //    string strSql = "SELECT distinct model FROM MES_ASSY_HW_CONTROL order by model";
        //    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        //    ddlModel.DataTextField = "MODEL";
        //    ddlModel.DataValueField = "MODEL";
        //    ddlModel.DataSource = dt.DefaultView;
        //    ddlModel.DataBind();
        //}      

        //protected void btnQueryByModel_Click(object sender, EventArgs e)
        //{
        //    string strModel = ddlModel.SelectedValue.ToString();
        //    string strsql = "Select Model,HW_REV,LOWER_PN,DAUGHTER_PN,UP_PN,LAST_UPDATE_DATE,LAST_UPDATED_BY from MES_ASSY_HW_CONTROL where model ='" + strModel + "' order by LOWER_PN";
        //    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        //    dgHWByModel.DataSource = dt.DefaultView;
        //    dgHWByModel.DataBind(); 
        //    dt.Dispose();
        //    string sql = "SELECT MAX(COUNT) COUNT FROM(SELECT DISTINCT  MODEL,'1' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND LOWER_PN IS NOT NULL UNION SELECT DISTINCT  MODEL,'2' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL) UNION SELECT DISTINCT  MODEL,'3' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL AND DAUGHTER_PN IS NOT NULL)) WHERE MODEL='"+strModel+"'";
        //    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        //    strCount = dt1.Rows[0]["COUNT"].ToString();
        //    lbInfo.Text = strModel + "機種: 由" + strCount + "塊板子組成！";
        //}

        //protected void btnQueryByPID_Click(object sender, EventArgs e)
        //{
        //    string strPID = ClsCommon.GetSqlString(txtProductID.Text.Trim().ToUpper());
        //    lbInfo1.Text = txtProductID.Text.Trim().ToUpper()+"在組裝段綁定的板子信息如下：";
        //    string strsql = "SELECT * FROM MES_ASSY_PID_JOIN  WHERE MAIN_ID=" + strPID + " OR SECONDARY_ID=" + strPID + "  OR THIRD_ID=" + strPID; 

        //    //strsql="SELECT SORDER, MODEL, SPART,BOM_VER FROM SFC.PRODUCT_PANEL_SORDER Where PRODUCT_ID = " + strPID;
        //    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];

        //    if (dt.Rows.Count > 0)
        //    {
        //       // if(dt.Columns[strCount].ToString().Equals())
        //        string strMainID = dt.Rows[0]["MAIN_ID"].ToString(); 
        //        string strSecondaryID = dt.Rows[0]["SECONDARY_ID"].ToString();
        //        string strThirdID = dt.Rows[0]["THIRD_ID"].ToString();
        //        string strSql = "SELECT '" + strMainID + "' PRODUCTID,SORDER,MODEL,SPART,BOM_VER,SPART||'R'||BOM_VER LOWER_PN,'' DAUGHTER_PN,'' UP_PN FROM SFC.PRODUCT_PANEL_SORDER Where PRODUCT_ID ='" + strMainID + "' UNION  SELECT '" + strSecondaryID + "' PRODUCTID,SORDER,MODEL,SPART,BOM_VER,'' LOWER_PN,SPART||'R'||BOM_VER  DAUGHTER_PN,'' UP_PN  FROM SFC.PRODUCT_PANEL_SORDER Where PRODUCT_ID ='" + strSecondaryID + "' UNION SELECT '" + strThirdID + "' PRODUCTID,SORDER,MODEL,SPART,BOM_VER,'' LOWER_PN,'' DAUGHTER_PN,SPART||'R'||BOM_VER  UP_PN FROM SFC.PRODUCT_PANEL_SORDER Where PRODUCT_ID ='" + strThirdID+"'";
        //        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        //        dgHWByPID.DataSource = dt1.DefaultView;
        //        dgHWByPID.DataBind();
        //    }
        //    else
        //    {
        //        //此板子沒有進“組裝投入”工站，請先投入！
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "InputError1", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "InputError1") + "');</script>");
        //        return;
        //    }         
        //}

        //protected void dgHWByPID_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    if (e.CommandName == "PRODUCTID")
        //    {
        //        string strProductID = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
        //        string strPanelID;
        //        string sql = "SELECT PANEL_ID FROM SFC.PRODUCT_PANEL_SORDER where Product_id='" + strProductID + "'";
        //        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        //        strPanelID = dt1.Rows[0]["PANEL_ID"].ToString(); 
        //        dgProduct.Visible = true;
        //        Label2.Visible = true;

        //        string strProcedureName = "SFCQUERY.GETPROCESSINFO";
        //        OracleParameter[]  oraPara = new OracleParameter[]{new OracleParameter("PRODUCTID",OracleType.VarChar,20),
        //                                    new OracleParameter("PANELID",OracleType.VarChar,9),
        //                                    new OracleParameter("DATA",OracleType.Cursor)};
        //        oraPara[0].Value = strProductID;
        //        oraPara[1].Value = strPanelID;

        //        //oraPara[1].Value = "";
        //        oraPara[2].Direction = ParameterDirection.Output;
        //        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara).Tables[0];

        //        dgProduct.DataSource = dt.DefaultView;
        //        dgProduct.DataBind();
        //    }

        //}
        //protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string strModel = ddlModel.SelectedValue.ToString();
        //    //string strsql = "Select Model,HW_REV,LOWER_PN,DAUGHTER_PN,UP_PN,LAST_UPDATE_DATE,LAST_UPDATED_BY from MES_ASSY_HW_CONTROL where model ='" + strModel + "' order by LOWER_PN";
        //    //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        //    //dgHWByModel.DataSource = dt.DefaultView;
        //    //dgHWByModel.DataBind();
        //    //dt.Dispose();
        //    string sql = "SELECT MAX(COUNT) COUNT FROM(SELECT DISTINCT  MODEL,'1' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND LOWER_PN IS NOT NULL UNION SELECT DISTINCT  MODEL,'2' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL) UNION SELECT DISTINCT  MODEL,'3' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL AND DAUGHTER_PN IS NOT NULL)) WHERE MODEL='" + strModel + "'";
        //    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sql).Tables[0];
        //    strCount = dt1.Rows[0]["COUNT"].ToString();
        //    lbInfo.Text = "機種:"+strModel +  "由" + strCount + "塊板子組成!";
        //}
        protected void btnquery_Click(object sender, EventArgs e)
        {
            string strsql="";
            string strMainID = ClsCommon.GetSqlString(txtMainID.Text.Trim().ToUpper());
            string strSecondaryID = ClsCommon.GetSqlString(txtSecondaryID.Text.Trim().ToUpper());
            string strThirdID = ClsCommon.GetSqlString(txtThirdID.Text.Trim().ToUpper());
            string strModel = txtMainID.Text.Trim().ToUpper().Substring(0, 3);
            string strSql="SELECT * FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
            if(dt0.Rows.Count>0)
            {
                string sqlcount = "SELECT MAX(COUNT) COUNT FROM(SELECT DISTINCT  MODEL,'1' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND LOWER_PN IS NOT NULL UNION SELECT DISTINCT  MODEL,'2' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL) UNION SELECT DISTINCT  MODEL,'3' COUNT FROM MES_ASSY_HW_CONTROL WHERE MODEL='" + strModel + "' AND (LOWER_PN IS NOT NULL AND UP_PN IS NOT NULL AND DAUGHTER_PN IS NOT NULL)) WHERE MODEL='" + strModel + "'";
                DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(sqlcount).Tables[0];
                string strCount = dt1.Rows[0]["COUNT"].ToString();
                lbInfo.Text = "此幾種需投入" + strCount + "塊板子!其HW版本信息如下:";

                if (strCount == "1")
                    strsql = "select * from (SELECT DISTINCT a.main_id product_id, e.hw_rev,substr(e.lower_pn,1,11) main_pn,substr(e.lower_pn,13) MAIN_BOM,a.lcm_id lcm_id FROM sfc.mes_assy_pid_join a,sfc.mes_pcba_panel_link b,sfc.mes_pcba_panel_detail c,shp.cmcs_sfc_sorder d,sfc.mes_assy_hw_control e WHERE a.main_id = b.product_id AND c.panel_id = b.panel_id AND c.wo_no = d.sorder AND SUBSTR (a.main_id, 1, 3) = e.model AND (d.spart || 'R' || d.bom_ver) = e.lower_pn) where product_id=" + strMainID;

                if (strCount == "2")
                    strsql = "select * from (SELECT DISTINCT a.main_id product_id,b.hw_rev,a.main_id,SUBSTR (m.main_pn,1,11) main_pn,SUBSTR (m.main_pn,13) main_bom,a.secondary_id,SUBSTR (s.secondary_pn,1,11) secondary_pn,SUBSTR (s.secondary_pn,13) secondary_bom FROM (SELECT b.product_id main_id, c.spart ||'R'|| bom_ver main_pn,a.item_no main_item FROM sfc.mes_pcba_panel_detail a,sfc.mes_pcba_panel_link b,shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND a.wo_no = c.sorder) m,(SELECT b.product_id secondary_id,c.spart ||'R'|| bom_ver secondary_pn,a.item_no secondary_item FROM sfc.mes_pcba_panel_detail a,sfc.mes_pcba_panel_link b,shp.cmcs_sfc_sorder c WHERE a.panel_id = b.panel_id AND a.wo_no = c.sorder) s,sfc.mes_assy_pid_join a,sfc.mes_assy_hw_control b  WHERE m.main_id = a.main_id AND s.secondary_id = a.secondary_id AND m.main_pn = b.lower_pn AND s.secondary_pn = b.up_pn) where main_id=" + strMainID + " and secondary_id=" + strSecondaryID;

                if (strCount == "3")
                    strsql = "select * from (SELECT DISTINCT x.main_id product_id,h.hw_rev,x.main_id,SUBSTR (x.main_pn1,1,11) main_pn,SUBSTR (x.main_pn1,13) main_bom,x.secondary_id,SUBSTR (y.secondary_pn1,1,11) secondary_pn,SUBSTR (y.secondary_pn1,13) secondary_bom,x.third_id,SUBSTR (z.third_pn1,1,11) third_pn,SUBSTR (z.third_pn1,13) third_bom FROM (SELECT a.main_id,a.secondary_id,a.third_id,d.spart main_pn,d.spart||'R'|| bom_ver main_pn1 FROM sfc.mes_assy_pid_join a,sfc.mes_pcba_panel_detail b,sfc.mes_pcba_panel_link c,shp.cmcs_sfc_sorder d WHERE a.main_id = c.product_id AND b.panel_id = c.panel_id AND b.wo_no = d.sorder) x LEFT OUTER JOIN (SELECT a.main_id,a.secondary_id,a.third_id,d.spart secondary_pn,d.spart||'R'|| bom_ver secondary_pn1 FROM sfc.mes_assy_pid_join a,sfc.mes_pcba_panel_detail b,sfc.mes_pcba_panel_link c,shp.cmcs_sfc_sorder d WHERE a.secondary_id = c.product_id AND b.panel_id = c.panel_id AND b.wo_no = d.sorder) y ON x.main_id = y.main_id AND x.secondary_id = y.secondary_id LEFT OUTER JOIN (SELECT a.main_id,a.secondary_id,a.third_id,d.spart third_pn,d.spart||'R'|| bom_ver third_pn1 FROM sfc.mes_assy_pid_join a,sfc.mes_pcba_panel_detail b,sfc.mes_pcba_panel_link c,shp.cmcs_sfc_sorder d WHERE a.third_id = c.product_id AND b.panel_id = c.panel_id AND b.wo_no = d.sorder) z ON x.main_id = z.main_id AND x.third_id = z.third_id LEFT OUTER JOIN sfc.mes_assy_hw_control h ON x.main_pn = h.lower_pn AND y.secondary_pn = h.up_pn AND z.third_pn = h.daughter_pn) WHERE product_id=" + strMainID + " and secondary_id=" + strSecondaryID + " and third_id=" + strThirdID;

                //strsql="SELECT SORDER, MODEL, SPART,BOM_VER FROM SFC.PRODUCT_PANEL_SORDER Where PRODUCT_ID = " + strPID;
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    dgHWByPID.DataSource = dt.DefaultView;
                    dgHWByPID.DataBind();
                }
                else
                {
                    //此板子沒有進“組裝投入”工站，請先投入！
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "InputError1", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "InputError1") + "');</script>");
                    return;
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language=javascript>alert('此板子不能使用該查詢!');</script>");
                return;
            }            
        }
}
