using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using DBAccess.EAI;

public partial class BOUNDARY_WfrmCheckDiskNumber : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            MultiLanguage();
        }
    }

    private void MultiLanguage()
    {
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    public string setIMEI(string v_imei)
    { 
        string strpid = "";
        string strmodel = "";
        string strIMEI = "";
        string strModel = "";

        try
        {
            string StrSql;
            if (v_imei.Substring(0, 1).ToUpper().Equals("A"))
                StrSql = "SELECT PRODUCTID,model,imei FROM CMCS_SFC_SHIPPING_DATA a "
                + " WHERE IMEI LIKE  '" + v_imei + "%'";
            else
                StrSql = "select product_id,ppart,imeinum from SHP.CMCS_SFC_IMEINUM where imeinum like  '" + v_imei + "%' or ppid_num like '" + v_imei + "%' or CUSTOMER_NUM like '" + v_imei + "%'";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strIMEI = dt.Rows[0][2].ToString();
                if (dt.Rows[0][0].ToString().Trim() == "")
                {
                    strmodel = dt.Rows[0][1].ToString().Substring(2, 3);
                    string strsql0 = "SELECT PRODUCTID FROM CMCS_SFC_SHIPPING_DATA a " + " WHERE IMEI LIKE  '" + strIMEI + "%'";
                    DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql0).Tables[0];
                    if (dt0.Rows.Count > 0)
                    {
                        strIMEI = dt.Rows[0][2].ToString();
                        strpid = dt0.Rows[0][0].ToString();
                    }
                    else
                    {
                        string strsql1 = "SELECT  PRODUCTID FROM " + strmodel + ".E2PCONFIG T WHERE PICASSO=" + ClsCommon.GetSqlString(v_imei) + " OR IMEI LIKE " + ClsCommon.GetSqlString("%" + strIMEI + "%");
                        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            strpid = dt1.Rows[0][0].ToString();
                        }
                        else
                        {
                            strpid = "error";
                        }
                    }
                }
                else
                {
                    strpid = dt.Rows[0][0].ToString();
                }
            }
            else //如果在出入庫中找不到，則在E2P里找.
            {
                StrSql = "SELECT SUBSTR(type_code,1,3) MODEL FROM CMCS_SFC_IMEINUM T,SHP.ROS_TCH_PN s WHERE t.ppart=s.ppart and (SERIAL_NUM =" + ClsCommon.GetSqlString(v_imei) + " OR IMEINUM LIKE " + ClsCommon.GetSqlString(v_imei + "%") + ")";
                dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                if (dt.Rows.Count > 0)
                    strModel = dt.Rows[0][0].ToString();
                if (strModel == "DVR" || strModel == "RCX" || strModel == "SLG" || strModel == "RUY" || strModel == "TWN" || strModel == "MRO" || strModel == "CAS")
                    StrSql = "SELECT SERIAL_NUM FROM CMCS_SFC_IMEINUM T WHERE SERIAL_NUM =" + ClsCommon.GetSqlString(v_imei) + " OR IMEINUM LIKE " + ClsCommon.GetSqlString(v_imei + "%");
                else
                    StrSql = "SELECT  PRODUCTID FROM " + dt.Rows[0][0].ToString() + ".E2PCONFIG T WHERE PICASSO=" + ClsCommon.GetSqlString(v_imei) + " OR IMEI LIKE " + ClsCommon.GetSqlString("%" + v_imei + "%");//+" AND E2PDATE=(SELECT MAX(E2PDATE) FROM "+dt.Rows[0][0].ToString()+".E2PCONFIG WHERE T.PICASSO=PICASSO) ";
                dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
                if (!dt.Rows[0][0].ToString().Equals(""))
                    strpid=dt.Rows[0][0].ToString();
                else
                    strpid = "error";
            }
        }
        catch
        {
            strpid = "error";
        }
        return strpid;
    }

    private string GetData(string type)
    {
        string strpid = txtDiskno.Text.ToString().Trim().ToUpper(); 
        string strSql; 
        strSql = "SELECT c.apart,B.MAIN_ID pid,a.key_part_no keypart,e.vendor_name vendor,d.datacode datecode,d.lot_code lotcode,"
              + " a.qty,diskno,mo_number wo,b.creation_Date inputmaterialdate,line_name line,'' empno"
              + " FROM SFC.K_SEND_HISTORY A,SFC.MES_ASSY_PID_JOIN B,SHP.CMCS_SFC_AORDER C,SAP.SAP_GR_SEQ d,SAP.SAP_PO_RCV_INFO e"
              + " Where A.MO_NUMBER=B.WO_NO AND A.MO_NUMBER=C.AORDER and substr(diskno,1,10)=d.gr_no"
              + " and lpad(d.seq_no,4,'0')=substr(diskno,11,4) and d.gr_no=e.receipt_num";
        
        switch (type)
        {
            case "PID":  //Product ID
                strSql = strSql + " AND B.MAIN_ID='" + strpid + "'"; 
                break; 
            case "IMEI":  //IMEI 
                strpid = setIMEI(strpid);
                strSql = strSql + " AND B.MAIN_ID='" + strpid + "'"; 
                break;
            case "APART":  //PPID
                strSql = strSql + " AND c.apart='" + strpid + "'";
                break;
        }
        return strSql;        
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        string strpid = txtDiskno.Text.ToString().Trim().ToUpper();
        string type = "";
        string typesql;
        DataTable dt;
        typesql = " select * from SHP.CMCS_SFC_AORDER where apart='" + strpid + "'";
        dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
        if (dt.Rows.Count > 0)
            type = "APART";
        else
        {
            typesql = " select * from sfc.mes_assy_pid_join where main_id='" + strpid + "'";
            dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
            if (dt.Rows.Count > 0)
                type = "PID";
            else
            {
                typesql = " select * from SHP.CMCS_SFC_IMEINUM  where imeinum='" + strpid + "'";
                dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    type = "IMEI";
                    strpid = setIMEI(strpid);
                    if (strpid.Equals("error"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI尚未使用！！');</script>");
                        return;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息輸入錯誤！！');</script>");
                    return;
                }
            }
        }        

        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(GetData(type)).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            Label4.Visible = true;
            GridView1.Visible = true;

            double dpagecount = dt1.Rows.Count / GridView1.PageSize;
            double pagecount = Math.Ceiling(dpagecount);
            Label4.Text = "Current Page:" + (GridView1.PageIndex + 1).ToString() + "/" + pagecount + " Total:" + dt1.Rows.Count.ToString();

            GridView1.DataSource = dt1.DefaultView;
            GridView1.DataBind();
        }
        else
        {
            Label4.Visible = false;
            GridView1.Visible = false;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在你要查詢的數據！！');</script>");
            return;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strpid = txtDiskno.Text.ToString().Trim().ToUpper();
        string type = "";
        string typesql;
        DataTable dt;
        typesql = " select * from SHP.CMCS_SFC_AORDER where apart='" + strpid + "'";
        dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
        if (dt.Rows.Count > 0)
            type = "APART";
        else
        {
            typesql = " select * from sfc.mes_assy_pid_join where main_id='" + strpid + "'";
            dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
            if (dt.Rows.Count > 0)
                type = "PID";
            else
            {
                typesql = " select * from SHP.CMCS_SFC_IMEINUM  where imeinum='" + strpid + "'";
                dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    type = "IMEI";
                    strpid = setIMEI(strpid);
                    if (strpid.Equals("error"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI尚未使用！！');</script>");
                        return;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息輸入錯誤！！');</script>");
                    return;
                }
            }
        }

        Label4.Visible = true;
        GridView1.Visible = true;
        GridView1.PageIndex = e.NewPageIndex;
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(GetData(type)).Tables[0];
        GridView1.DataSource = dt1.DefaultView;
        GridView1.DataBind();
        Label4.Text = "Current Page:" + (GridView1.PageIndex + 1).ToString() + "/" + GridView1.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();

        dt.Dispose();
        dt1.Dispose();
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        string strpid = txtDiskno.Text.ToString().Trim().ToUpper();
        string type = "";
        string typesql;
        DataTable dt;
        typesql = " select * from SHP.CMCS_SFC_AORDER where apart='" + strpid + "'";
        dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
        if (dt.Rows.Count > 0)
            type = "APART";
        else
        {
            typesql = " select * from sfc.mes_assy_pid_join where main_id='" + strpid + "'";
            dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
            if (dt.Rows.Count > 0)
                type = "PID";
            else
            {
                typesql = " select * from SHP.CMCS_SFC_IMEINUM  where imeinum='" + strpid + "'";
                dt = ClsGlobal.objDataConnect.DataQuery(typesql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    type = "IMEI";
                    strpid = setIMEI(strpid);
                    if (strpid.Equals("error"))
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('IMEI尚未使用！！');</script>");
                        return;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('信息輸入錯誤！！');</script>");
                    return;
                }
            }
        }

        Label4.Visible = true;
        GridView1.Visible = true;
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(GetData(type)).Tables[0];
        GridView1.DataSource = dt1.DefaultView;
        GridView1.DataBind();
        Label4.Text = "Current Page:" + (GridView1.PageIndex + 1).ToString() + "/" + GridView1.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();

        dt.Dispose();
        dt1.Dispose();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "DateCode")
        //{
        //    string strDateCode = ((LinkButton)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text; 

        //    string strScript = "<script language='jscript'>var res = window.open('./B2B_PO_ItemReport.aspx?Sendid=" + strSendid + "&Receid=" + strReceid
        //   + "&Msegid=" + strMsegid + "&Pono=" + strPono + "','_blank', '');</script>";
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        //}
        //if (e.CommandName == "LotCode")
        //{
        //    string strLotCode = ((LinkButton)gvPO.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;

        //    string strScript = "<script language='jscript'>var res = window.open('./B2B_PO_ItemReport.aspx?Sendid=" + strSendid + "&Receid=" + strReceid
        //   + "&Msegid=" + strMsegid + "&Pono=" + strPono + "','_blank', '');</script>";
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        //}
    }
}
