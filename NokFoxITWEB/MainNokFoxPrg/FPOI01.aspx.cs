using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using SFC.TJWEB;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.Data.OracleClient;
using System.Data.OleDb; //操作EXECL表要用的命名空间
using Economy.Publibrary;
using Economy.BLL;
using SCM.GSCMDKen;
using EconomyUser;
//using Microsoft.Office.Interop.Excel; // Excel 下的名称空间
//using System.Reflection;    //反射名称空间
//using System.Data.SqlClient;

public partial class MainBBRYPrg_FPOI01 : System.Web.UI.Page
{

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();

    Convertlib ConvertlibPointer = new Convertlib();
    private static PDataBaseOperation dbo;

    public string tmpdate = "";
    public string strconn = "";
    public string DBType = "SQL";
    public static string BBSCMDIR;
    //public string strconn = "Server=10.134.140.104,7621 ;User id=Nokia_view;Pwd=viewsi101;Database=NOKIA_IOF";
    //10.148.8.168  databasename=NOKIA_IOF  username="FIH_IHUB_WEB"  password="FIH_IHUB_WEB!"
    //public string strconn = "Server=10.148.8.168 ;User id=FIH_IHUB_WEB;Pwd=FIH_IHUB_WEB!;Database=NOKIA_IOF";
    public string strDuns = "";
    public string Foxconn_Location = "";
    public string strUserName = "";
    public string strIsAdmin = "";
    public string Foxconn_Division = "";
    public string dtime = "";
    BBSCMlib BBSCMlibPointer = new BBSCMlib();

    protected void Page_Load(object sender, EventArgs e)
    {
        Label94.Visible = false;
        //if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
        //{
        //    strUserName = Session["UserName"].ToString();
        //    strDuns = Session["strDuns"].ToString();
        //    Foxconn_Location = Session["Region"].ToString();
        //    strIsAdmin = Session["IsAdmin"].ToString();
        //    Foxconn_Division = Session["strBU"].ToString();
        strconn = Session["Param3"].ToString();// ReadParaTxt("WebReadParam.txt", "23525");// ConfigurationManager.AppSettings["BBSCM"]; 
        BBSCMDIR = Session["Param7"].ToString();
        //    strconn = "Server=10.186.19.108 ;User id=sa;Pwd=Sa123456;Database=BBSCM";//108
        //   strconn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";//108

        //    DBType = Session["dbtype"].ToString();
        //}
        //else
        //{
        //    //Response.Write("<script>window.open( 'MainNokPrg/I_HubLogin.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //    Server.Transfer("I_HubLogin.aspx");
        //}

        dbo = new PDataBaseOperation("sql", strconn);
        if (!IsPostBack)
        {
            Check();
            
        }
    }

    protected void BtnGO_Click(object sender, EventArgs e)
    {
        Check();
    }

    public void Check()   //SELECT  DT.ID,DT.POID,DT.ITEMID,DT.CONFIRM_ADD_FLAG FROM 
    {
        string StrSql = "";

        StrSql = "SELECT  B.* ," +
       " C.ITEMID ," +
        "  C.CONFIRM_ADD_FLAG ," +
        "  C.CONFIRMFLAG ," +
        "  C.POCNT ," +
        "  C.INTERNALID ," +
        "  c.CostAmount ," +
       "   C.IncoTermsCode ," +
        "  c.IncoTermsName ," +
        "  C.OriginID ," +
       "   c.ProductRecipientID,  dataFrom='pocreate', c.SCHEDULEQUANTITY" +
 " FROM    ( SELECT    A.ID ," +
                    "  SUBSTRING(CREATIONDT, 1, 10) CREATIONDT ," +
                   "   A.TESTDATAINDICATOR ," +
                  "    A.SENDERID ," +
                    "  A.POID ," +
                   "   A.BUYERPOSTDT ," +
                   "   A.TRANSMISSIONINDICATOR ," +
                   "   A.SELLERPARTYID ," +
                   "   A.CLASSIFICATIONCODE ," +
                   "   A.TRANSFERLOCATIONNAME ," +
                    "  SENDFLAG = ( SELECT TOP 1" +
                                       "   SENDFLAG" +
                                 "  FROM   PO_CONFIRMATION_MT" +
                                "   WHERE  ID = A.ID" +
                              "   ) ," +
                   "   A.UPLOAD_SAP ," +
                    "  A.SAP_LOG" +
         "   FROM      PO_CREATE_MT A" +
          "  WHERE     A.POID NOT IN ( SELECT  POID" +
                                    "  FROM    dbo.PO_CHANGE_MT where (CONFIRMFLAG !='' and CONFIRMFLAG IS NOT NULL and CONFIRMFLAG !='Y'))" +
       "   ) B" +
        "  JOIN dbo.PO_CREATE_DT C ON B.ID = C.ID WHERE (c.CONFIRMFLAG='' OR c.CONFIRMFLAG='Y' or c.CONFIRMFLAG is null)" +
 " UNION" +
 " SELECT  E.* ," +
       "   F.ITEMID ," +
        "  F.CONFIRM_ADD_FLAG ," +
       "   F.CONFIRMFLAG ," +
        "  F.POCNT ," +
        "  F.INTERNALID ," +
        "  F.CostAmount ," +
        "  F.IncoTermsCode ," +
       "   F.IncoTermsName ," +
       "   F.OriginID ," +
        "  F.ProductRecipientID, dataFrom='pochange',    f.SCHEDULEQUANTITY" +
 " FROM    ( SELECT    G.ID ," +
                    "  SUBSTRING(G.CREATIONDT, 1, 10) CREATIONDT ," +
                   "   G.TESTDATAINDICATOR ," +
                   "   G.SENDERID ," +
                   "   G.POID ," +
                   "   G.BUYERPOSTDT ," +
                    "  G.TRANSMISSIONINDICATOR ," +
                    "  G.SELLERPARTYID ," +
                   "   G.CLASSIFICATIONCODE ," +
                   "   G.TRANSFERLOCATIONNAME ," +
                   "   SENDFLAG = ( SELECT TOP 1" +
                                        "  SENDFLAG" +
                                "   FROM   PO_CONFIRMATION_MT" +
                                "   WHERE  ID = G.ID" +
                            "     ) ," +
                   "   G.UPLOAD_SAP ," +
                   "   G.SAP_LOG" +
          "  FROM      PO_CHANGE_MT G" +
                   "   LEFT OUTER JOIN PO_CREATE_MT H ON G.POID = H.POID" +
        "  ) E" +
        "   JOIN dbo.PO_CHANGE_DT F ON E.ID = F.ID  WHERE (F.CONFIRMFLAG='' OR F.CONFIRMFLAG='Y' or F.CONFIRMFLAG is null) ";


        StrSql = "select * from (" + StrSql + ") M where 1=1 ";
        if (TextBox1.Text != "")
        {
            StrSql = StrSql + " and POID= '" + TextBox1.Text + "'";
        }
        if (txtBeginDate.Text != "")
        {
            string begintime = txtBeginDate.Text.Replace("/", "-").Trim();
            begintime = begintime + "T00:00:00.000Z";
            StrSql = StrSql + " and CREATIONDT> '" + begintime + "'";
        }
        if (txtendDate.Text != "")
        {
            string endtime = txtendDate.Text.Replace("/", "-").Trim();
            endtime = endtime + "T00:00:00.000Z";
            StrSql = StrSql + " and CREATIONDT< '" + endtime + "'";

        }

        StrSql = StrSql + "  order by CONVERT(INT,M.POID) desc,M.POCNT";

        //  --, CREATIONDT  排序

        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, strconn, StrSql);
        Session["SQL104"] = dt;
        lblTotal.Text = "";
        lblTotal.Text = "Total:&nbsp;<span style='font-weight:bold;'>" + dt.Rows.Count.ToString() + "</span>&nbsp;Rows";

        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
    }

    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        int ArrMax = 300;
        string[] ReadTxtArray = new string[ArrMax];
        string FileName = "SetReadParam.txt";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "";
        int i = 0, strlen = 0;

        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            ReadTxtArray[i] = InString;
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
                {
                    retPara = InString.Substring(6, strlen - 5);
                    i = ArrMax;  // Break
                }
            }
            i++;
        }
        sr.Close();
        return (retPara);
    }
    public string strwhere()
    {
        string StrSQL = "";
        int Begin = 0;
        int End = 0;
        string BeginDate = txtBeginDate.Text.Replace("/", "");
        string EndDate = txtendDate.Text.Replace("/", "");

        if (TextBox1.Text != "" && TextBox1.Text.Length == 10)//Document_ID
        {
            StrSQL = StrSQL + " and T1.Document_ID like '" + TextBox1.Text.ToString() + "%'";
        }
        if (BeginDate.Length == 8 && EndDate.Length == 8 && int.TryParse(BeginDate, out Begin) && int.TryParse(EndDate, out End))//datetime
        {
            if (Begin > End)
            {
                BeginDate = txtendDate.Text.Replace("/", "");
                EndDate = txtBeginDate.Text.Replace("/", "");
            }
            StrSQL = StrSQL + " and T1.Receive_Date >= '" + BeginDate + "' and T1.Receive_Date <= '" + EndDate + "'";
        }
        else
        {
            StrSQL = StrSQL + "and T1.Receive_Date >= '" + BeginDate + "' and T1.Receive_Date <= '" + EndDate + "'";
        }
        return StrSQL;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
        Session["IDDD"] = ID;
        Response.Write("<script>window.open('BBSCMPODetailNew.aspx','one','width=500,height=800,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=no,top=220,left=350');</script>");
    }

    //protected void Button_Confirm(object sender, EventArgs e)
    //{
    //    int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
    //    string ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
    //    Check();
    //}

    //protected void Button_Clear(object sender, EventArgs e)
    //{
    //    int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
    //    string ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
    //    Session["ID"] = ID;
    //    Check();
    //}
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
    }

    private void fixGrid(int n)         //n为需要固定的列的个数
    {
        for (int i = 0; i < GridView1.Columns.Count; i++)
        {
            if (i < n)
            {
                GridView1.Columns[i].HeaderStyle.CssClass = "fixed";
                GridView1.Columns[i].ItemStyle.CssClass = "fixed";
            }

            else
            {
                GridView1.Columns[i].HeaderStyle.CssClass = "unfixed";
                GridView1.Columns[i].ItemStyle.CssClass = "unfixed";
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();


            string addFlage = DataBinder.Eval(e.Row.DataItem, "CONFIRM_ADD_FLAG").ToString().Trim();

            string confirmFlag = DataBinder.Eval(e.Row.DataItem, "CONFIRMFLAG").ToString().Trim();

            Button B2 = (Button)e.Row.FindControl("btnUserConfirm");
            Button B3 = (Button)e.Row.FindControl("btnClearQty");
            Button B4 = (Button)e.Row.FindControl("btnClearSendFlag");
            if (string.IsNullOrEmpty(addFlage) && string.IsNullOrEmpty(confirmFlag))
            {
                B2.Enabled = false;
                B3.Enabled = false;
                B4.Enabled = false;
            }
            else if (addFlage == "Y" && string.IsNullOrEmpty(confirmFlag))
            {
                B2.Enabled = true;
                B3.Enabled = true;
                B4.Enabled = false;
            }
            else if (addFlage == "Y" && confirmFlag == "Y")
            {
                B2.Enabled = false;
                B3.Enabled = false;
                B4.Enabled = true;
            }

            B4.Enabled = confirmFlag == "Y" ? true : false;

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
                ID = ID + e.Row.Cells[i].Text;
            }

            e.Row.Attributes.Add("onclick", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText='background-color:#e6c5fc';window.oldtr=this");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            string id = ((Label)(e.Row.FindControl("Label91"))).Text.ToString().Trim();
            string itemid = ((Label)(e.Row.FindControl("Label92"))).Text.ToString().Trim();
            string sqlitem = "select QTY,DELIVERYSTARTDT from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and ITEMID='" + itemid + "' order by SCHEDULELINEID";
            DataTable dtitem = PDataBaseOperation.PSelectSQLDT(DBType, strconn, sqlitem);// dbo.PSelectSQLDT(sqlitem);
            if (dtitem.Rows.Count > 0)
            {
                int pp = GridView1.Rows.Count;
                ((GridView)e.Row.FindControl("GridView2")).DataSource = dtitem;
                ((GridView)e.Row.FindControl("GridView2")).DataBind();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        bool bCheck = ((CheckBox)sender).Checked;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = bCheck;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string StrSql = @"SELECT T4.Vendor_Code, T4.Nokia_Site, T1.Document_ID, T3.TTooldatetime as Movement_Date, T1.Receive_Date + ' ' + T1.Receive_Time AS Receive_DT,
                            T2.ProductID AS Material, T2.LineNumber AS [Line No.], 
                            sum(convert(bigint, (CASE WHEN T3.DocRefTypeCode = 'Scheduling Agreement' THEN T3.DocumentID ELSE '' END)))  AS [Parchars doc.], 
                            sum(convert(bigint,(CASE WHEN T3.DocRefTypeCode = 'Shipping Reference Identifier' THEN T3.DocumentID  ELSE '' END)))AS [Material Doc.], 	      
                            sum(convert(bigint,(CASE WHEN T3.DocRefTypeCode = 'Transfer Order' THEN T3.DocumentID ELSE '' END)))  AS [Consumption Reference No.], 	     
                            '' AS Mtype, T3.ProductQty AS [Qty Used], 
                            T2.Total_Line_Amnt AS [value], T3.UnitPrice AS Price, T3.Currency_Code AS [Curr.],
                            T2.Upload_SAP, T2.UnitPrice, T2.SAP_Price, T2.Price_Flag, T2.Z_CONFIRM, T2.Check_Result as ISSUE, T2.MSGTYP1, T2.MSGTYP2,
                            cast(T2.SAP_Log as nvarchar(100)) as SAP_LOG,cast(T2.SAP_Log1 as nvarchar(100)) as SAP_LOG1  
                            FROM sbi_main T1, SBI_InvcItem T2, SBI_InvcItem_Product T3, SBI_Vendor_Code T4 
                            WHERE T1.Document_ID = T2.Document_ID 
                            and T2.Document_ID = T3.Document_ID 
                            and T2.LineNumber = T3.LineNumber 
                            and T2.ProductID = T3.ProductID
                            and T1.SBI_From_GBI = T4.SBI_From_DUNS 
                            and T1.SBI_To_GBI = T4.SBI_To_DUNS 

                            and T2.Foxconn_Location = '" + Foxconn_Location + "' and  T2.Foxconn_Division= '" + Foxconn_Division + "' ";
        StrSql = StrSql + strwhere();
        StrSql = StrSql + @"    group by T4.Vendor_Code, T4.Nokia_Site, T1.Document_ID, T3.TTooldatetime, 
                            T1.Receive_Date + ' ' + T1.Receive_Time ,T2.ProductID, T2.LineNumber,T3.ProductQty,
                            T2.Total_Line_Amnt, T3.UnitPrice,T3.Currency_Code,T2.Upload_SAP, T2.UnitPrice,T2.SAP_Price, 
                            T2.Price_Flag, T2.Z_CONFIRM, T2.Check_Result, T2.MSGTYP1, T2.MSGTYP2, cast(T2.SAP_Log as nvarchar(100)),cast(T2.SAP_Log1 as nvarchar(100))  
                            order by Receive_DT  desc, convert(decimal, T2.LineNumber)";
        DataTable dt = DataBaseOperation.SelectSQLDT(DBType, strconn, StrSql);
        string excelName = "output3C7bnmtcr.xls";

        this.ExportDataTable("application/ms-excel", excelName, dt);
    }

    private void ExportDataTable(string FileType, string excelName, DataTable dt)
    {

        GridView gv = new GridView();
        gv.DataSource = dt;
        gv.DataBind();
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            gv.Rows[i].Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
        gv.HeaderRow.Cells[0].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[1].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[2].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[3].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[4].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[5].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[6].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[7].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[8].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[9].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[10].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[11].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[12].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[13].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[14].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[15].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[16].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[17].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[18].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[19].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[20].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[21].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[22].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[23].BackColor = System.Drawing.Color.Gray;
        gv.HeaderRow.Cells[24].BackColor = System.Drawing.Color.Gray;
        Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(excelName, System.Text.Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnissue_Click(object sender, EventArgs e)
    {
        int iRet = 0;
        int iSql = 0;
        int y = 0;
        string NokiaSite = string.Empty;
        string strSql = string.Empty;
        string DocID = string.Empty;
        string PriID = string.Empty;
        string LineNO = string.Empty;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked == true)
            {
                iRet++;
                NokiaSite = ((Label)GridView1.Rows[i].FindControl("Label9")).Text;
                DocID = ((Label)GridView1.Rows[i].FindControl("Label10")).Text;
                PriID = ((Label)GridView1.Rows[i].FindControl("Label13")).Text;
                LineNO = ((Label)GridView1.Rows[i].FindControl("Label12")).Text;
                if (NokiaSite != "")
                {
                    if (NokiaSite != "DNMP" && NokiaSite != "BNMT")
                    {
                        Response.Write("<script>alert('You can not filter the oversea SBI price!, because the " + NokiaSite + " is oversea!!')</script>");
                        return;
                    }
                }
                strSql = "update SBI_InvcItem SET Check_Result='M',ModificationUser ='" + strUserName + "',ModificationDate='" + dtime + "' ";
                strSql = strSql + " FROM SBI_Main T1 INNER JOIN SBI_InvcItem T2 ON T1.Document_ID = T2.Document_ID LEFT OUTER JOIN ";
                strSql = strSql + " SBI_Vendor_Code T3 ON T1.SBI_From_GBI = T3.SBI_From_DUNS AND T1.SBI_To_GBI = T3.SBI_To_DUNS";
                strSql = strSql + "  WHERE T3.nokia_site = '" + NokiaSite + "' and T1.Document_ID ='" + DocID + "' and T2.ProductID ='" + PriID + "' and T2.LineNumber ='" + LineNO + "'";
                iSql = DataBaseOperation.ExecSQL(DBType, strconn, strSql);
                if (iSql == 1)
                {
                    y++;
                }
                else
                {
                    Response.Write("<script>alert('Update Fail!')</script>");
                }
            }
        }
        if (iRet == 0)
        {
            Response.Write("<script>alert('You must select at least one Item to do check_Issue!!')</script>");
        }
        if (iRet == y && (y != 0 || iRet != 0))
        {
            Response.Write("<script>alert('Processing OK!')</script>");
        }
        else
        {
            Response.Write("<script>alert('Update Fail!')</script>");
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Label94.Visible = true;
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label91"))).Text.Trim();
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text.Trim();
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label92"))).Text.Trim();
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblPOCNT"))).Text.Trim();

        string tablename = "";
        if (pocnt == "1")
        {
            tablename = "" + BBSCMDIR + ".[dbo].[PO_CREATE_MT]";
        }
        else
        {
            tablename = "" + BBSCMDIR + ".[dbo].[PO_CHANGE_MT]";
        }

        string sqlcheck = "SELECT UPLOAD_SAP,USERCONFIRMFLAG FROM " + tablename + " where ID='" + id + "' and POID='" + poid + "'  and POCNT='" + pocnt + "'";
        DataTable dtcheck = dbo.PSelectSQLDT(sqlcheck);
        if (dtcheck.Rows.Count > 0)
        {
            string sapflag = dtcheck.Rows[0]["USERCONFIRMFLAG"].ToString().Trim();
            if (sapflag == "Y")
            {
                Label94.Text = "PO：" + poid + "已經創建過SO，不能重複創建！";
            }
            else if (sapflag == "V")
            {
                string tmp = BBSCMlibPointer.CallSapAndCreateSO("1", DBType, strconn, strconn, "menu", tmpdate, "01", poid, pocnt,BBSCMDIR);
                Label94.Text = "該PO：" + poid + "：" + tmp;
            }
            else
            {
                Label94.Text = "PO：" + poid + "還沒有Verify,或Verify出現異常！";
            }
        }
        Check();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Label94.Visible = true;
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label91"))).Text.Trim();
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text.Trim();
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label92"))).Text.Trim();
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblPOCNT"))).Text.Trim();

        string tablename = "";
        if (pocnt == "1")
        {
            tablename = "" + BBSCMDIR + ".[dbo].[PO_CREATE_MT]";
        }
        else
        {
            tablename = "" + BBSCMDIR + ".[dbo].[PO_CHANGE_MT]";
        }

        string sqlcheck = "SELECT UPLOAD_SAP,USERCONFIRMFLAG FROM " + tablename + " where ID='" + id + "' and POID='" + poid + "'  and POCNT='" + pocnt + "'";
        DataTable dtcheck = dbo.PSelectSQLDT(sqlcheck);
        if (dtcheck.Rows.Count > 0)
        {
            string sapflag = dtcheck.Rows[0]["USERCONFIRMFLAG"].ToString().Trim();
            if (sapflag == "V")
            {
                Label94.Text = "PO：" + poid + "已經Verify！";
            }
            else if (sapflag == "" || sapflag == "E")
            {
                string tmp = BBSCMlibPointer.CallSapAndCreateSO("1", DBType, strconn, strconn, "menu", tmpdate, "00", poid, pocnt,BBSCMDIR);
                Label94.Text = "該PO：" + poid + "：" + tmp;
            }
            else if (sapflag == "Y")
            {
                Label94.Text = "PO：" + poid + "已經創建過SO,不能再Verify！";
            }
        }
        Check();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
        //        lbtnTitle.CommandArgument = GridView1.DataKeys[e.Row.RowIndex].ToString();
        //    }
        //    if (e.Row.RowIndex < 0) return;
        //    if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#cccccc';");
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        //    }
    }

    protected void Gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //Session["IDDD"] = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
        //Session["ITEMID"] = GridView1.DataKeys[e.RowIndex].Values[1].ToString();

    
        string Iddd = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
        string poid = GridView1.DataKeys[e.RowIndex].Values[1].ToString();
        string itemid = GridView1.DataKeys[e.RowIndex].Values[2].ToString();
        string pocnt = GridView1.DataKeys[e.RowIndex].Values[3].ToString();
        string datafrom = GridView1.DataKeys[e.RowIndex].Values[4].ToString();

        //  Response.Redirect("BBSCMPODetail.aspx?IDDD=" + Iddd + "&itemid=" + itemid );

        Response.Write("<script>window.open('BBSCMPODetailNew.aspx?IDDD=" + Iddd + "&poid=" + poid + "&itemid=" + itemid + "&datafrom=" + datafrom + "&pocnt=" + pocnt + "','newwindow','width=\"100%\",height=\"100%\",status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=no,top=0,left=0');</script>");
        return;
    }

    #region modify by hu 2014 10 28
    string mtablename = "",dtablename="";

    protected void btnUserConfirm_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label91"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label92"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;

        string squantity = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblHidSCHEDULEQUANTITY"))).Text;

        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblPOCNT"))).Text;

        string datafrom = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblHidDataFrom"))).Text;

        if (datafrom == "pocreate")
        {
            mtablename = "PO_CREATE_MT";
            dtablename = "PO_CREATE_DT";
        }
        else
        {
            mtablename = "PO_CHANGE_MT";
            dtablename = "PO_CHANGE_DT";
        }

        //檢查PO是否已經Confirm
        string str = "select * from " + dtablename + " where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and CONFIRMFLAG='Y' and POCNT='" + pocnt + "'";
        DataTable dtstr = dbo.PSelectSQLDT(str);
        if (dtstr.Rows.Count > 0)
        {
            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('該PO已經Confirm，不可重複Confirm，請重新選擇！');</script>");
            return;
        }

        string strsum = "select SUM(CONVERT(float,QTY)) QTY from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and POID='" + poid + "' and  ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";

        DataTable dtsum = dbo.PSelectSQLDT(strsum);
        if (dtsum.Rows.Count > 0)
        {
            double sum = 0;
            if (dtsum.Rows[0][0].ToString() != "") sum = double.Parse(dtsum.Rows[0][0].ToString());
            if ((double.Parse(squantity)) == sum)
            {
                string strflag1 = "update " + dtablename + " set UserCONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";
                int idet1 = dbo.PExecSQL(strflag1);
                string strflag2 = "update " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] set UserCONFIRMFLAG='Y',CONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";
                int idet2 = dbo.PExecSQL(strflag2);
                string strflag3 = "update " + dtablename + " set CONFIRMFLAG='Y' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";
                int idet3 = dbo.PExecSQL(strflag3);

                if ((idet1 > 0) && (idet2 > 0))
                {

                    this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('Confirm成功！');</script>");
                    Check();
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('Confirm失敗，請核對數量！');</script>");

                    ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
                    ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
                }
            }
            else
            {

                this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('數量不等，請核對！');</script>");
                ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
                ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
            }
        }
        else
        {

            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('Confirm失敗，該PO沒有Confirmation數量！');</script>");

            ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label7"))).Text = "N";
            ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text = "N";
        }
    }
    protected void btnClearQty_Click(object sender, EventArgs e)
    {

        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label91"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label92"))).Text;
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblPOCNT"))).Text;

        string datafrom = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblHidDataFrom"))).Text;
        if (datafrom == "pocreate")
        {
            mtablename = "PO_CREATE_MT";
            dtablename = "PO_CREATE_DT";
        }
        else
        {
            mtablename = "PO_CHANGE_MT";
            dtablename = "PO_CHANGE_DT";
        }
        //檢查PO是否已經Confirm
        string str = "select * from " + dtablename + " where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and CONFIRMFLAG='Y' and  POCNT='" + pocnt + "'";
        DataTable dtstr = dbo.PSelectSQLDT(str);
        if (dtstr.Rows.Count > 0)
        {

            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('該PO已經Confirm，不能ClearQTY,請先ClearSendFlag！');</script>");
            return;
        }

        string strdelete = "delete from " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";
        int idetdelete = dbo.PExecSQL(strdelete);
        string strflag = "update " + dtablename + " set UserCONFIRMFLAG='',CONFIRMFLAG='',CONFIRM_ADD_FLAG='' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and  POCNT='" + pocnt + "'";
        int idetflag = dbo.PExecSQL(strflag);
        if (idetflag > 0)
        {
            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('此PO已經Clear!');</script>");
        }
        else
        {
            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('此筆資料Clear失敗!');</script>");
        }
        Check();
    }
    protected void btnClearSendFlag_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string id = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label91"))).Text;
        string poid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;
        string itemid = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label92"))).Text;
        string pocnt = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblPOCNT"))).Text;

        string datafrom = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("lblHidDataFrom"))).Text;
        if (datafrom == "pocreate")
        {
            mtablename = "PO_CREATE_MT";
            dtablename = "PO_CREATE_DT";
        }
        else
        {
            mtablename = "PO_CHANGE_MT";
            dtablename = "PO_CHANGE_DT";
        }
        string clearMT = "update " + mtablename + " set CONFIRMFLAG='' where ID='" + id + "' and POID='" + poid + "' and POCNT='" + pocnt + "'";
        int idetMT = dbo.PExecSQL(clearMT);
        string clearDT = "update " + dtablename + " set UserCONFIRMFLAG='',CONFIRMFLAG=''  where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and POCNT='" + pocnt + "'";
        int idetDT = dbo.PExecSQL(clearDT);
        string clearCON = "update PO_CONFIRMATION_MT set UserCONFIRMFLAG='',CONFIRMFLAG='',SENDFLAG='' where ID='" + id + "' and POID='" + poid + "' and ITEMID='" + itemid + "' and POCNT='" + pocnt + "'";
        int idetCON = dbo.PExecSQL(clearDT);
        if (idetMT > 0 && idetDT > 0 && idetCON > 0)
        {
            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('此PO已經ClearSendFlag！');</script>");
        }
        else
        {
            this.ClientScript.RegisterStartupScript(GetType(), "Mess", "<script>alert('此筆資料Clear失敗！');</script>");
        }
        Check();
    }
    #endregion
}