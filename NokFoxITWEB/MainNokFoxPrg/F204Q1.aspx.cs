using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Drawing;

public partial class MainMSPrg_F204Q1 : System.Web.UI.Page
{
    private static PDataBaseOperation dbo;
    private static string conntype;
    private static string Conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string param;
    private static string tablename;
    private static string pocnt;
    private static string tprivate;
    private static string tuser;
    private static int count;
    private static DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                conntype = Session["Param2"].ToString();
                Conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
                tprivate = Session["C_204"].ToString();
                tuser = Session["userid"].ToString();
            }
            else if (tmp1 == "")
            {
                conntype = "sql";
                Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            //conntype = "sql";
            ////Conn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93
            ////dbWrite = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//93
            //BBSCMDIR = "IMSCM";
            //Conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108
            //dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//108

            dbo = new PDataBaseOperation(conntype, Conn);
            txtBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtendDate.Text = DateTime.Now.AddDays(+7).ToString("yyyy/MM/dd");
            Label1.Visible = false;
            Label2.Visible = false;
            GridViewBind();

            //根據權限設定按鈕有效性
            if (tprivate == "DISPLAY" || tprivate == "")
            {
                Button5.Enabled = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtBeginDate.Text = "";
        txtendDate.Text = "";
        dnNum.Text = "";
        plant.Text = "";
        Label1.Visible = false;
        Label2.Visible = false;
        GridView1.Visible = false;
        GridView2.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        bool b = true;
        string hbol = ((LinkButton)GridView1.Rows[row].FindControl("lbtnBOL")).Text.ToString().Trim();
        string hpickdate = ((TextBox)GridView1.Rows[row].FindControl("pickdate")).Text.ToString().Replace("/", "").Trim();
        string hdeliverydate = ((TextBox)GridView1.Rows[row].FindControl("deliverydate")).Text.ToString().Replace("/", "").Trim();
        string hb202 = ((TextBox)GridView1.Rows[row].FindControl("B202")).Text.ToString().Trim();
        string rcount = GridView1.DataKeys[row].Values[1].ToString().Trim();
        string strupd = "update " + BBSCMDIR + ".[dbo].[EDI204HEADER] set pickdate='" + hpickdate + "',deliverydate='" + hdeliverydate + "',B202='" + hb202 + "'" +
                        " where B204='" + hbol + "' and RCOUNT='" + rcount + "'";
        int idet = dbo.PExecSQLTran(strupd);
        string strSel = "select * from " + BBSCMDIR + ".[dbo].[EDI204L11] where B204='" + hbol + "' and L1102='DO' and RCOUNT='" + rcount + "'";
        DataTable dtSelect = dbo.PSelectSQLDT(strSel);
        for (int i = 0; i < dtSelect.Rows.Count; i++)
        {
            string dnid = dtSelect.Rows[i]["L1101"].ToString().Trim();
            string tdnid = dnid;
            if (tdnid.Length == 10)
            {
                tdnid = dnid.Substring(2, 8);
            }
            string strUpdate = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT]  set IssueDt='" + hpickdate + "',ArrivalDT='" + hdeliverydate +
                "',ProductRecipientID='" + hb202 + "' where DNID='" + tdnid + "'";
            int idet1 = dbo.PExecSQLTran(strUpdate);
            if (idet1 <= 0)
            {
                Response.Write("<script>alert('Update failed, please confirm whether there is 3B2!');</script>");
                b = false;
                break;
            }
        }
        if (idet == 1 && b)
        {
            Response.Write("<script>alert('Update Successfully!');</script>");
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        bool b = true;
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hbol = ((LinkButton)GridView1.Rows[row].FindControl("lbtnBOL")).Text.ToString().Trim();
        string pickDate = ((TextBox)GridView1.Rows[row].FindControl("pickdate")).Text.ToString().Trim();
        string carrierID = ((TextBox)GridView1.Rows[row].FindControl("B202")).Text.ToString().Trim();
        string hl1101 = GridView1.DataKeys[row].Values[0].ToString().Trim();
        string rcount = GridView1.DataKeys[row].Values[1].ToString().Trim();
        string deDate = ((TextBox)GridView1.Rows[row].FindControl("deliverydate")).Text.ToString().Trim();
        //check:if LoadID has been sent,return message
        string checksend = "select distinct ackflag  FROM " + BBSCMDIR + ".[dbo].[EDI204HEADER] where B204='" + hbol + "' and RCOUNT!='" + rcount + "' and ackflag!=''";
        DataTable dtsend = dbo.PSelectSQLDT(checksend);
        if (dtsend.Rows.Count > 0)
        {
            Response.Write("<script>alert('Change is not possible, 3B2 have been sent!');</script>");
            return;
        }
        string strSel = "select distinct * from " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] where B204='" + hbol + "' and RCOUNT='" + rcount + "' AND G6201='37'";
        DataTable dtSel = dbo.PSelectSQLDT(strSel);

        #region      判斷3B2是否有此204對應所有的DN,有Confirm,無提示信息
        for (int i = 0; i < dtSel.Rows.Count; i++)
        {
            string strL1101 = dtSel.Rows[i]["OID01"].ToString().Trim();
            string tdnid = strL1101;
            if (tdnid.Length == 10)
            {
                tdnid = strL1101.Substring(2, 8);
            }
            //20151230--add check 940QTY ==3B2QTY
            string checkqty = "select distinct dndt.qty,dndt.cartonqty,dnitem.Total_QTY,dnitem.Carton_QTY"+
                              " from [IMSCM].[dbo].[DN_Detail] dndt,[IMSCM].[dbo].[Delivery_DNITEM] dnitem"+
                              " where dndt.w0502=dnitem.DNID and dndt.Selleritemno=dnitem.ItemID and dndt.w0502='" + tdnid + "'";
            DataTable dtqty = dbo.PSelectSQLDT(checkqty);
            if (dtqty.Rows.Count > 0)
            {
                for (int r = 0; r < dtqty.Rows.Count; r++)
                {
                    double tqty940=Convert.ToDouble(dtqty.Rows[r]["qty"].ToString());
                    double cqty940 = Convert.ToDouble(dtqty.Rows[r]["cartonqty"].ToString());
                    double tqty3B2 = Convert.ToDouble(dtqty.Rows[r]["qty"].ToString());
                    double cqty3B2 = Convert.ToDouble(dtqty.Rows[r]["cartonqty"].ToString());
                    if (tqty940 != tqty3B2)
                    {
                        Response.Write("<script>alert('940 TotalQTY is " + tqty940 + ",3B2 TotalQTY is " + tqty3B2 + ",Please Check!');</script>");
                        return;
                    }
                    else if (cqty940 != cqty3B2)
                    {
                        Response.Write("<script>alert('940 CartonQTY is " + cqty940 + ",3B2 CartonQTY is " + cqty3B2 + ",Please Check!');</script>");
                        return;
                    }
                }
            }
            string strL11 = "select distinct mt.DNID MDNID,mt.POID MPOID,dnitem.DNID,dnitem.POID, * "+
                "from " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] hu," + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] mt,"
                + BBSCMDIR + ".[dbo].[Delivery_DNITEM] dnitem where mt.dnid=hu.dnid and mt.dnid=dnitem.dnid AND MT.DNID='" + tdnid + "'";
            DataTable dtL11 = dbo.PSelectSQLDT(strL11);
            if (dtL11.Rows.Count <= 0)
            {
                b = false;
                break;
            }
            else
            {
                //check 3B2 MT POID
                for (int ii = 0; ii < dtL11.Rows.Count; ii++)
                {
                    if (dtL11.Rows[ii]["MPOID"].ToString().Trim() == "")
                    {
                        string updpoid = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] set POID='" + dtL11.Rows[ii]["POID"] + "' where DNID='" + tdnid + "'";
                        int idetpoid = dbo.PExecSQLTran(updpoid);
                        if (idetpoid == 1) break;
                    }
                }
            }
        }
        // 判斷3B2是否有此204對應所有的DN,有Confirm,無提示信息
        if (b)
        {
            string strupd = "update " + BBSCMDIR + ".[dbo].[EDI204HEADER] set CONFIRMFLAG='Y' where B204='" + hbol + "' and RCOUNT='" + rcount + "'";
            int idet = dbo.PExecSQLTran(strupd);
            string strupd1 = "update " + BBSCMDIR + ".[dbo].[EDI204L11] set CONFIRMFLAG='Y' where B204='" + hbol + "' and RCOUNT='" + rcount + "'";
            int idet1 = dbo.PExecSQLTran(strupd1);
            string strselect = "select distinct LAD.*" +
                               " from " + BBSCMDIR + ".[dbo].[EDI204L11] L11," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] LAD" +
                               " where L11.L1101=LAD.OID01 and L11.L1102='DO' and LAD.B204='" + hbol + "' and LAD.RCOUNT='" + rcount + "'";
            DataTable dtselect = dbo.PSelectSQLDT(strselect);
           
            int count = 0;
            if (dtselect.Rows.Count > 0)
            {
                for (int rc = 0; rc < dtselect.Rows.Count; rc++)
                {
                    string tdnid = dtselect.Rows[rc]["OID01"].ToString().Trim();
                    string ttdnid = tdnid;
                    if (tdnid.Length == 10)
                    {
                        ttdnid = tdnid.Substring(2, 8);
                    }
                    string upddn = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] set WayBillID='" + hl1101 +
                        "',CONFIRMFLAG='Y',IssueDT='" + pickDate + "',ArrivalDT='" + deDate + "',ProductRecipientID='"+carrierID+"'" +
                        " where DNID='" + ttdnid + "'";
                    int idet2 = dbo.PExecSQLTran(upddn);
                    string updhu = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] set LoadProductRecipientID='" + hl1101 +
                        "',CONFIRMFLAG='Y'" + " where DNID='" + ttdnid + "'";
                    int idet5 = dbo.PExecSQLTran(updhu);
                    string upddn940 = "update " + BBSCMDIR + ".[dbo].[DN_Header] set uf9='" + hbol + "'" +
                                   "where w0502='" + tdnid + "'";
                    int idet3 = dbo.PExecSQLTran(upddn940);
                    string upddn940d = "update " + BBSCMDIR + ".[dbo].[DN_Detail] set uf9='" + hbol + "'" +
                                   "where w0502='" + tdnid + "'";
                    int idet4 = dbo.PExecSQLTran(upddn940d);
                    count++;
                }
            }
           
            if (idet == 1 && idet1 >= 1 && count == dtselect.Rows.Count)
            {
                Response.Write("<script>alert('Create Successfully!');</script>");
            }
            GridViewBind();
        }
        else
        {
            Response.Write("<script>alert('3B2 without the DN,  please go to the 3B2 interface, click the GetSAP button!');</script>");
        }
           
        #endregion
        return;
    }
    #region Button4_Click
    //protected void Button4_Click(object sender, EventArgs e)
    //{
    //    int row = Convert.ToInt32(((Button)sender).CommandArgument);
    //    string hbol = ((LinkButton)GridView1.Rows[row].FindControl("lbtnBOL")).Text.ToString().Trim();
    //    string hl1101 = GridView1.DataKeys[row].Values[0].ToString().Trim();
    //    string strupd = "update " + BBSCMDIR + ".[dbo].[EDI204HEADER] set CONFIRMFLAG='Y' where B204='" + hbol + "'";
    //    int idet = dbo.PExecSQLTran(strupd);
    //    string strupd1 = "update " + BBSCMDIR + ".[dbo].[EDI204L11] set CONFIRMFLAG='Y' where B204='" + hbol + "'";
    //    int idet1 = dbo.PExecSQLTran(strupd1);
    //    string strselect = "select LAD.*" +
    //                       " from " + BBSCMDIR + ".[dbo].[EDI204L11] L11," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] LAD" +
    //                       " where L11.L1101=LAD.OID01 and L11.L1102='DO' and LAD.B204='" + hbol + "'";
    //    DataTable dtselect = dbo.PSelectSQLDT(strselect);
    //    int count = 0;
    //    if (dtselect.Rows.Count > 0)
    //    {   
    //        for (int rc = 0; rc < dtselect.Rows.Count; rc++)
    //        {
    //            string tdnid = dtselect.Rows[rc]["OID01"].ToString().Trim();
    //            string ttdnid = tdnid;
    //            if (tdnid.Length == 10)
    //            {
    //                ttdnid = tdnid.Substring(2, 8);
    //            }
    //            string upddn = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] set WayBillID='" + hl1101 + "',CONFIRMFLAG='Y'" +
    //                           "where DNID='" + ttdnid + "'";
    //            int idet2 = dbo.PExecSQLTran(upddn);
    //            string updhu = "update " + BBSCMDIR + ".[dbo].[Delivery_Notification_HU] set LoadProductRecipientID='" + hl1101 + "',CONFIRMFLAG='Y'" +
    //                           "where DNID='" + ttdnid + "'";
    //            int idet5 = dbo.PExecSQLTran(updhu);
    //            string upddn940 = "update " + BBSCMDIR + ".[dbo].[DN_Header] set uf9='" + hbol + "'" +
    //                           "where w0502='" + tdnid + "'";
    //            int idet3 = dbo.PExecSQLTran(upddn940);
    //            string upddn940d = "update " + BBSCMDIR + ".[dbo].[DN_Detail] set uf9='" + hbol + "'" +
    //                           "where w0502='" + tdnid + "'";
    //            int idet4 = dbo.PExecSQLTran(upddn940d);
    //            count++;
    //        }
    //    }
    //    if (idet == 1 && idet1 >= 1 && count == dtselect.Rows.Count)
    //    {
    //        Response.Write("<script>alert('Create Successfully!');</script>");
    //    }
    //    GridViewBind();
    //}
    #endregion
    protected void Button5_Click(object sender, EventArgs e)
    {
        Session["Param1"] = 1;
        Session["Param2"] = conntype; // DBReadString
        Session["Param3"] = Conn; // DBReadString   44
        Session["Param4"] = Conn; // DBWriteString  215
        Session["Param5"] = "menu";   // Menu input
        Session["Param6"] = tmpdate;
        Session["Param7"] = BBSCMDIR;  // PROLINE 
        Session["Param8"] = dnNum.Text.Trim();  // DNID 
        Session["Param9"] = TimeFormat(txtBeginDate.Text);  // begindate
        Session["Param10"] = TimeFormat(txtendDate.Text);   // enddate
        Response.Write("<script>window.open( 'EDI204aspx.aspx','','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string dtime = DateTime.Now.ToString("yyyyMMddhhmmss");
        this.ExportDataTable("application/ms-excel", "EDI204-" + dtime + ".xls");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((Button)sender).CommandArgument);
        row = row % 7;
        string hbol = ((LinkButton)GridView1.Rows[row].FindControl("lbtnBOL")).Text.ToString().Trim();
        //檢查一下是否存在發送3B2，如有，提示已發送，不能將204刪除設為無效。
        string check3B2 = "select distinct hd.B204,mt.DNID,mt.SENDFlag"+
                          " from " + BBSCMDIR + ".[dbo].[EDI204HEADER] hd," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] lad," + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] mt" +
                          " where hd.B204=lad.B204 and lad.OID01=mt.DNID and hd.B204='" + hbol + "' and mt.SENDFlag='Y'";
        DataTable dtcheck3B2 = dbo.PSelectSQLDT(check3B2);
        if (dtcheck3B2.Rows.Count > 0)
        {
            Response.Write("<script>alert('3B2 has been sent,204 cannot be deleted!!');</script>");
            //Label1.Text = "3B2 has been sent,204 cannot be deleted!";
            //Label1.Visible = true;
            return;
        }
        else
        {
            Session["Param1"] = "1";
            Session["Param2"] = conntype; // DBReadString
            Session["Param3"] = Conn; // DBReadString   44
            Session["Param4"] = dbWrite; // DBWriteString  215
            Session["Param5"] = Autoprg;   // Menu input
            Session["Param6"] = tmpdate;
            Session["Param7"] = BBSCMDIR;  // PROLINE 
            //Response.Write("<script>window.showModalDialog('DeletePage.aspx?type=940&DNID="+hdn+"','width=100,height=100,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00')</script>");
            Response.Write("<script language='javascript'>var result = window.showModalDialog('DeletePage.aspx?type=204&DNID=" + hbol + "',window,'dialogHeight:200px;dialogWidth:300px;');if(result==1){ window.location.reload();}</script>");
        }
        GridViewBind();
    }
    public void ExportDataTable(string FileType, string FileName)
    {
        string dntxt = dnNum.Text.Trim();
        string begintime = TimeFormat(txtBeginDate.Text);
        string endtime = TimeFormat(txtendDate.Text);
        string plantid = plant.Text.Trim();
        string strselect = "  select distinct LAD.OID01 DN_NO,hd.pickdate Pickup_Date,LAD.OID03 Material_Code,LAD.OID02 MSFT_PO," +
                           " hd.deliverydate Delivery_Date,LAD.OID05 Qty,(select Carton_QTY from IMSCM.[dbo].[Delivery_DNITEM] where DNID=LAD.OID01 and LAD.OID03=InternalID and LAD.OID05=Total_QTY) Carton_QTY,hd.MS301 CarrierID,hd.L1101 LoadID," +
                           " hd.MS304 Transportation_Method,hd.SEND3B2FLAG,hd.ackflag,hd.SEND3B2Time,hd.SEND3B2Log,G62.N401 City_Name,hd.RCOUNT " +
                           " from " + BBSCMDIR + ".[dbo].[EDI204HEADER] hd," + BBSCMDIR + ".[dbo].[EDI204S5G62] G62," +
                           " " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] LAD" +
                           " where hd.B204=LAD.B204 and LAD.B204=G62.B204 " +
                           " and hd.RCOUNT=LAD.RCOUNT and LAD.RCOUNT=G62.RCOUNT and " +
                           " (hd.L1101+(case when hd.confirmflag='Y' then '2200-01-01 00:00:01' else hd.insertdate end)  " +
                           "in (select H1.L1101+max (case when H1.confirmflag='Y' then '2200-01-01 00:00:01'  else H1.insertdate end) as insertdate1 " +
                           "from  IMSCM.[dbo].[EDI204HEADER] H1 group by H1.L1101)) and (hd.Confirmflag!='D' or hd.Confirmflag is null) " +
                           " and LAD.S501='1' and G62.G6201='54' ";
        //string strselect = "select distinct L11.L1101 DN_NO,hd.pickdate Pickup_Date,LAD.OID03 Material_Code,LAD.OID02 MSFT_PO,"+
        //                   "hd.deliverydate Delivery_Date,LAD.OID05 Qty,DNITEM.Carton_QTY,hd.MS301 CarrierID," +
        //                   "hd.L1101 LoadID,hd.MS304 Transportation_Method,hd.SEND3B2FLAG,hd.ackflag,hd.SEND3B2Time,hd.SEND3B2Log,G62.N401 City_Name,hd.RCOUNT" +
        //                   " from " + BBSCMDIR + ".[dbo].[EDI204HEADER] hd," + BBSCMDIR + ".[dbo].[EDI204L11] L11," + BBSCMDIR + ".[dbo].[EDI204S5G62] G62," +
        //                   "" + BBSCMDIR + ".[dbo].[EDI204OIDLAD] LAD," + BBSCMDIR + ".[dbo].[Delivery_DNITEM] DNITEM" +
        //                   " where hd.B204=L11.B204 and L11.B204=G62.B204 and L11.B204=LAD.B204 and L11.L1101=DNITEM.DNID"+
        //                   " and L11.L1102='DO' and G62.G6201='54' and LAD.OID01=L11.L1101 and G62.RCOUNT=hd.RCOUNT" +
        //                   " and (hd.L1101+(case when hd.confirmflag='Y' then '2200-01-01 00:00:01' else hd.insertdate end) "+ 
        //                   " in (select H1.L1101+max (case when H1.confirmflag='Y' then '2200-01-01 00:00:01'  else H1.insertdate end) as insertdate1"+
        //                   " from  IMSCM.[dbo].[EDI204HEADER] H1 group by H1.L1101)) and (hd.Confirmflag!='D' or hd.Confirmflag is null)";
        if (dntxt != "") { strselect = strselect + " and LAD.OID01 like '%" + dntxt + "%'"; }
        if (begintime != "") { strselect = strselect + " and pickdate>='" + begintime + "'"; }
        if (endtime != "") { strselect = strselect + " and pickdate<='" + endtime + "'"; }
        if (plantid != "") { strselect = strselect + ""; }//MT表中工廠欄位？？？
        strselect = strselect + " order by hd.pickdate,hd.L1101,hd.RCOUNT";
        DataTable dtdetail = dbo.PSelectSQLDT(strselect);
        if (dtdetail.Rows.Count > 0)
        {
            for (int i = 0; i < dtdetail.Rows.Count; i++)
            {
                string tranmathod = dtdetail.Rows[i]["Transportation_Method"].ToString().Trim();
                if (tranmathod != "")
                {
                    switch (tranmathod)
                    {
                        case "A": dtdetail.Rows[i]["Transportation_Method"] = "By Air"; break;
                        case "M": dtdetail.Rows[i]["Transportation_Method"] = "By Road"; break;
                        case "R": dtdetail.Rows[i]["Transportation_Method"] = "By Rail"; break;
                        case "S": dtdetail.Rows[i]["Transportation_Method"] = "By Sea"; break;
                    }
                }
            }
            GridView gv = new GridView();
            gv.DataSource = dtdetail;
            gv.DataBind();
            Response.Charset = "UNICODE";
            //Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
            Response.ContentType = FileType;
            this.EnableViewState = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            Response.Write("<script>alert('No Data Found!');</script>");
        }
    }
    protected void lbtnBOL_Click(object sender, EventArgs e)
    {
        int row = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        row = row % 7;
        string bol = ((LinkButton)GridView1.Rows[row].FindControl("lbtnBOL")).Text.ToString();
        string rcount = GridView1.DataKeys[row].Values[1].ToString().Trim();
        string strselect = "select distinct LAD.[B204],[OID01],[OID02],[OID03],[OID04],[OID05],[OID06],[OID07]"+
                           ",[LAD01],[LAD02],[LAD05],[LAD06],[LAD07],[LAD08],[LAD09],[LAD010],[LAD011],[LAD012],LAD.RCOUNT"+
                           " from " + BBSCMDIR + ".[dbo].[EDI204L11] L11," + BBSCMDIR + ".[dbo].[EDI204OIDLAD] LAD" +
                           " where L11.L1101=LAD.OID01 and L11.L1102='DO' and LAD.B204='" + bol + "' and LAD.RCOUNT='" + rcount + "'";
        DataTable dtselect = dbo.PSelectSQLDT(strselect);
        if (dtselect.Rows.Count > 0)
        {
            GridView2.Visible = true;
            Session["SQLDNDT"] = dtselect;
            GridView2.DataSource = Session["SQLDNDT"];
            GridView2.DataBind();
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
            Label2.Text = "NO Data Found!";
            GridView2.Visible = false;
        }
    }
    protected string TimeFormat(string datetime)
    {
        string dateform = "";
        if (datetime != "")
        {
            //dateform = datetime.Replace("/", "-").Trim();
            dateform = datetime.Replace("/", "").Trim();
        }
        return dateform;
    }
    protected void GridViewBind()
    {
        string dntxt = dnNum.Text.Trim();
        string begintime = TimeFormat(txtBeginDate.Text);
        string endtime = TimeFormat(txtendDate.Text);
        string plantid = plant.Text.Trim();
        //string sqlselect = "", sqlselect1 = "", sqlselect2 = "";
        //sqlselect1 = "select b.* from(select B204,Max(RCOUNT) as RCOUNT FROM [IMSCM].[dbo].[EDI204HEADER] Group by B204) a inner JOIN"+
        //             " [IMSCM].[dbo].[EDI204HEADER] b on a.B204=b.B204 and a.RCOUNT=b.RCOUNT where 1=1";
        //sqlselect2 = "union  select * from [IMSCM].[dbo].[EDI204HEADER] where ConfirmFlag='Y'";
        //if (dntxt != "") 
        //{ 
        //    sqlselect1 = sqlselect1 + " and B204 in(select B204 from " + BBSCMDIR + ".[dbo].[EDI204L11] where L1101 like '%" + dntxt + "%')";
        //    sqlselect2 = sqlselect2 + " and B204 in(select B204 from " + BBSCMDIR + ".[dbo].[EDI204L11] where L1101 like '%" + dntxt + "%')"; 
        //}
        //if (begintime != "") 
        //{ 
        //    sqlselect1 = sqlselect1 + " and pickdate>='" + begintime + "'";
        //    sqlselect2 = sqlselect2 + " and pickdate>='" + begintime + "'"; 
        //}
        //if (endtime != "") 
        //{ 
        //    sqlselect1 = sqlselect1 + " and pickdate<='" + endtime + "'";
        //    sqlselect2 = sqlselect2 + " and pickdate<='" + endtime + "'"; 
        //}
        string sqlselect = "  select * from " + BBSCMDIR + ".[dbo].[EDI204HEADER] " +
                           " where B204+ (case when confirmflag='Y' then '2200-01-01 00:00:01' else insertdate end) in "+
                           " (select H1.B204+max (case when H1.confirmflag='Y' then '2200-01-01 00:00:01'  else H1.insertdate end) as insertdate1"+
                           " from  " + BBSCMDIR + ".[dbo].[EDI204HEADER] H1 group by H1.B204) and (Confirmflag!='D' or Confirmflag is null)";
        if (dntxt != "")
        {
            sqlselect = sqlselect + " and B204+insertdate in(select distinct B204+insertdate from IMSCM.[dbo].[EDI204OIDLAD] where OID01 like '%" + dntxt + "%')";
        }
        if (begintime != "")
        {
            sqlselect = sqlselect + " and pickdate>='" + begintime + "'";
        }
        if (endtime != "")
        {
            sqlselect = sqlselect + " and pickdate<='" + endtime + "'";
        }
        if (plantid != "") { sqlselect = sqlselect + ""; }//MT表中工廠欄位？？？
        sqlselect = sqlselect + " order by B204,RCOUNT";
        DataTable dtdetail = dbo.PSelectSQLDT(sqlselect);
        count = dtdetail.Rows.Count;
       
        if (dtdetail.Rows.Count > 0)
        {
            #region Transportation Method
            for (int i = 0; i < count; i++)
            {
                string ms304 = dtdetail.Rows[i]["MS304"].ToString().Trim();
                switch (ms304)
                {
                    case "A":
                        dtdetail.Rows[i]["MS304"] = "By Air";
                        break;
                    case "H":
                        dtdetail.Rows[i]["MS304"] = "Customer Pickup";
                        break;
                    case "M":
                        dtdetail.Rows[i]["MS304"] = "By Road";
                        break;
                    case "R":
                        dtdetail.Rows[i]["MS304"] = "By Rail";
                        break;
                    case "S":
                        dtdetail.Rows[i]["MS304"] = "By Sea";
                        break;
                    case "U":
                        dtdetail.Rows[i]["MS304"] = "Private Parcel Service";
                        break;
                    case "X":
                        dtdetail.Rows[i]["MS304"] = "Intermodal (Piggyback)";
                        break;
                    case "LT":
                        dtdetail.Rows[i]["MS304"] = "Less Than Trailer Load (LTL)";
                        break;
                    case "RC":
                        dtdetail.Rows[i]["MS304"] = "Rail, Less than Carload";
                        break;
                    case "RR":
                        dtdetail.Rows[i]["MS304"] = "Roadrailer";
                        break;
                    case "SE":
                        dtdetail.Rows[i]["MS304"] = "Sea/Air";
                        break;
                    case "TA":
                        dtdetail.Rows[i]["MS304"] = "Towaway Service";
                        break; 
                    default:
                        dtdetail.Rows[i]["MS304"] = dtdetail.Rows[i]["MS304"].ToString().Trim();
                        break;
                }
            }
            #endregion

            GridView1.Visible = true;
            Session["SQLHeader"] = dtdetail;
            GridView1.DataSource = Session["SQLHeader"];
            GridView1.DataBind();
            Label1.Visible = false;
            Label2.Visible = false;
            GridView2.Visible = false;
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "No Data found,Please click 'KeyData' button to maintenance!";
            GridView1.Visible = false;
            Label2.Visible = false;
            GridView2.Visible = false;
        }
    }

    #region GV1
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView1.PageCount ? GridView1.PageCount - 1 : newPageIndex;
        //得到新的值 
        GridView1.PageIndex = newPageIndex;
        GridView1.DataSource = Session["SQLHeader"];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string sendFlag = DataBinder.Eval(e.Row.DataItem, "SEND3B2FLAG").ToString().Trim();
            string confirmflag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();
            Button B3 = (Button)e.Row.FindControl("Button3");
            Button B4 = (Button)e.Row.FindControl("Button4");
            Button B7 = (Button)e.Row.FindControl("Button7");
            if (sendFlag == "Y")
            {
                B3.Enabled = false;
                B4.Enabled = false;
                e.Row.BackColor = Color.LightGreen;
            }
            if (confirmflag=="Y")
            {
                B4.Enabled = false;
            }
            if (tprivate == "DISPLAY" || tprivate == "")
            {
                B3.Enabled = false;
                B4.Enabled = false;
            }
            if (tuser.ToUpper() != "GMO_IT")
            {
                B7.Enabled = false;
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
        //edit function
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((Button)e.Row.Cells[2].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('Are you sure Reset?')");
                ((Button)e.Row.Cells[2].FindControl("Button7")).Attributes.Add("onclick", "javascript:return confirm('Are you sure Delete?')");
            }
        } 
    }
    #endregion

    #region GV2
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)  //点击了Go按钮 
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = GridView2.BottomPagerRow;
            if (pagerRow != null)
            {
                //得到text控件 
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引 
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
            }
        }
        else
        {
            //点击了其他的按钮 
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出 
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= GridView2.PageCount ? GridView2.PageCount - 1 : newPageIndex;
        //得到新的值 
        GridView2.PageIndex = newPageIndex;
        GridView2.DataSource = Session["SQLDNDT"];
        GridView2.DataBind();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //after confirm,no saving
            string b204 = DataBinder.Eval(e.Row.DataItem, "B204").ToString().Trim();
            string strselect = "select SEND3B2FLAG FROM " + BBSCMDIR + ".[dbo].[EDI204HEADER] where B204='" + b204 + "'";
            DataTable dtselect = dbo.PSelectSQLDT(strselect);
            if (dtselect.Rows.Count > 0)
            {
                string confirmflag = dtselect.Rows[0]["SEND3B2FLAG"].ToString().Trim();
                if (confirmflag != "N")
                {
                    e.Row.BackColor = Color.LightGreen;
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }
    #endregion
}