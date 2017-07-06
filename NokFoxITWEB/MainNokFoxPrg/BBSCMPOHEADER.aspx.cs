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

public partial class BBSCMPOHEADER : System.Web.UI.Page
{

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    
    Convertlib ConvertlibPointer = new Convertlib();
    private static PDataBaseOperation dbo;

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
    public string Foxconn_Division="";
    public string dtime = "";
  
    protected void Page_Load(object sender, EventArgs e)
    {


 
        //if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
        //{
        //    strUserName = Session["UserName"].ToString();
        //    strDuns = Session["strDuns"].ToString();
        //    Foxconn_Location = Session["Region"].ToString();
        //    strIsAdmin = Session["IsAdmin"].ToString();
        //    Foxconn_Division = Session["strBU"].ToString();
        strconn = Session["Param3"].ToString();// ReadParaTxt("WebReadParam.txt", "23525");// ConfigurationManager.AppSettings["BBSCM"]; 
        BBSCMDIR = Session["Param7"].ToString();
        //strconn = "Server=10.186.19.108 ;User id=sa;Pwd=Sa123456;Database=BBSCM";
        //    DBType = Session["dbtype"].ToString();
        //}
        //else
        //{
        //    //Response.Write("<script>window.open( 'MainNokPrg/I_HubLogin.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //    Server.Transfer("I_HubLogin.aspx");
        //}

        if (strIsAdmin != "2")
        {
            CheckBox1.Visible = false;
            DropDownList5.Visible = false;
            btnsap.Visible = false;
            btnsbi.Visible = false;
            btnadd.Visible = false;
            btnissue.Visible = false;
            Label6.Visible = false;
            CheckBox2.Visible = false;
            TextBox4.Visible = false;
        }
        if (!IsPostBack)
        {
            //string strDunsSql = "";
            //dtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //txtBeginDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //txtendDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //strDunsSql = "Select Distinct T1.SBI_From_GBI As DUNS,T2.Site As SITE From Nokia_IOF.dbo.SBI_Main T1 Left Join Nokia_IOF.dbo.DUNS2Site T2 On T1.SBI_From_GBI = T2.DUNS " +
            //                     "Where T1.SBI_From_GBI <> '' And T1.SBI_From_GBI Is Not Null";
            //System.Data.DataTable dtDuns = DataBaseOperation.SelectSQLDT(DBType, strconn, strDunsSql);

            //DropDownList2.DataSource = dtDuns.DefaultView;
            //DropDownList2.DataTextField = dtDuns.Columns[1].ToString();
            //DropDownList2.DataValueField = dtDuns.Columns[0].ToString();
            //DropDownList2.DataBind();
            //DropDownList2.Items.Add("---ALL---");
            //DropDownList2.Items[this.DropDownList2.Items.Count - 1].Text = "---ALL---";
            //DropDownList2.Items[this.DropDownList2.Items.Count - 1].Value = "ALL";
            //DropDownList2.SelectedIndex = this.DropDownList2.Items.Count - 1;


            //DropDownList5.Items.Add(new ListItem("Check_Issue", "Check_Issue"));
            //DropDownList5.Items.Add(new ListItem("Confirm_BY_Site", "Confirm_BY_Site"));
            //DropDownList5.Items.Add(new ListItem("Confirm_BY_Material", "Confirm_BY_Material"));
            //DropDownList5.Items.Add(new ListItem("Confirm_BY_Doc", "Confirm_BY_Doc"));
            //DropDownList5.SelectedValue = "Check_Issue";
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
        StrSql = "SELECT DT.ITEMID,DT.CONFIRM_ADD_FLAG ,DT.CONFIRMFLAG  , T.ID,substring( CREATIONDT, 1,10 )  CREATIONDT,T.TESTDATAINDICATOR,T.SENDERID,T.POID,T.BUYERPOSTDT,T.TRANSMISSIONINDICATOR,T.SELLERPARTYID,T.CLASSIFICATIONCODE,T.TRANSFERLOCATIONNAME,";
        StrSql = StrSql + "SENDFLAG=(   SELECT top 1 SENDFLAG  FROM  PO_CONFIRMATION_MT WHERE ID =T.ID)";
        StrSql = StrSql + ",T.UPLOAD_SAP,T.SAP_LOG ,DT.POCNT,DT.INTERNALID,DT.CostAmount, DT.IncoTermsCode,DT.IncoTermsName,DT.OriginID,DT.ProductRecipientID    FROM  PO_CREATE_MT T ,dbo.PO_CREATE_DT DT   where 1=1 and T.ID=DT.ID   ";

        
       


        if (TextBox1.Text != "")
        { 
        
        StrSql = StrSql + " and T.POID= '"+TextBox1.Text+"'";      
        
        }
        StrSql = StrSql + "  order by CREATIONDT DESC";



        //StrSql = StrSql + strwhere();
        //StrSql = StrSql + " and T2.Foxconn_Location = '"+Foxconn_Location+"'  and  T2.Foxconn_Division= '"+Foxconn_Division+"'" +
        //         " order by (T1.Receive_Date + ' ' + T1.Receive_Time) desc, convert(decimal,T2.LineNumber)";
       // strconn = ReadParaTxt("WebReadParam.txt", "23524");         
      // strconn = ConvertlibPointer.DeEncCodeWithoutEclcode( strconn ,  ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");

      //  dt2 = PDataBaseOperation.PSelectSQLDS(DBType, DBWriString, sqlw);


        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, strconn, StrSql);
        Session["SQL104"] = dt;
        GridView1.DataSource = Session["SQL104"];
        GridView1.DataBind();
        Label7.Text = "Total:" + dt.Rows.Count.ToString() + " Rows";
        Label7.Visible = true;
 
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
            //             Response.Write(ReadTxtArray[i]);
            //             Response.Write("<br>");
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
        if (DropDownList2.SelectedItem.Value != "ALL")//ShipFrom_DUNS
        {
            StrSQL = StrSQL + " AND  T1.SBI_From_GBI='" + DropDownList2.SelectedValue + "'";
        }
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
        if (Downlist1.Value == "Upload_SAP" && Downlist2.Value != "ALL")
        {
            StrSQL = StrSQL + " and T2." + Downlist1.Value + "='" + Downlist2.Value + "'";
        }
        else
            if (Downlist1.Value == "Price_Flag" && Downlist2.Value != "ALL")
            {
                if (Downlist2.Value == "N")
                {
                    StrSQL = StrSQL + "and T2." + Downlist1.Value + "!='Y' and T2." + Downlist1.Value + "!='M'";
                }
                else
                {
                    StrSQL = StrSQL + " and T2." + Downlist1.Value + "='" + Downlist2.Value + "'";
                }
            }
        return StrSQL;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string  ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
       // string ID =GridView1.Rows[e.

        //string LineNO = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label12"))).Text;
        //string ProCode = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;
        Session["IDDD"] =  ID;
        Session["BBSCMDIR"] = BBSCMDIR;
        //Session["ProCode"] = ProCode;
        //Session["strconn"] = strconn;
        //Response.Redirect("Nokia3C7Detail.aspx");
        //Response.Write("<script>window.open( 'Nokia3C7Detail.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        // Server.Transfer("<script>window.open( 'BBSCMPODetail.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
     //  Server.Transfer("BBSCMPODetail.aspx");
      //  Response.Redirect("BBSCMPODetail.aspx");
        Response.Write("<script>window.open('BBSCMPODetail.aspx','one','width=500,height=800,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=no,top=220,left=350');</script>");

    }

    protected void Button_Confirm(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string  ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
        //string LineNO = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label12"))).Text;
        //string ProCode = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;
       // Session["ID"] =  ID;

      //  string SqlStr = "update PO_CONFIRMATION_MT  set SENDFLAG='Y',SENDTIME=getdate()   WHERE POID ='" + ID + "'";


        //strconn = ReadParaTxt("WebReadParam.txt", "23524");
        //strconn = ConvertlibPointer.DeEncCodeWithoutEclcode(strconn, ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");

        //PDataBaseOperation.PSelectSQLDT

       // int I = dbo.PExecSQL(SqlStr);
        
        
        Check();
    }

    protected void Button_Clear(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string ID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label9"))).Text;
        //string LineNO = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label12"))).Text;
        //string ProCode = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label13"))).Text;
        Session["ID"] = ID;

       // string SqlStr = "update PO_CONFIRMATION_MT  set SENDFLAG='N',SENDTIME=null  WHERE POID ='" + ID + "'";

        //strconn = ReadParaTxt("WebReadParam.txt", "23524");
        //strconn = ConvertlibPointer.DeEncCodeWithoutEclcode(strconn, ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");


       // int I = dbo.PExecSQL(SqlStr);
        Check();
    }

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
         //ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string ID = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

           
              //string ID = GridView1.Rows[e.Row.RowIndex].Cells[0].Text; 
            //this.GridView1.Columns[0].HeaderStyle.CssClass = "fixColleft";
            //this.GridView1.Columns[1].HeaderStyle.CssClass = "fixColleft";
            //this.GridView1.Columns[2].HeaderStyle.CssClass = "fixColleft";
            //this.GridView1.Columns[0].ItemStyle.CssClass = "fixColleft";
            //this.GridView1.Columns[1].ItemStyle.CssClass = "fixColleft";

            //this.GridView1.Columns[2].ItemStyle.CssClass = "fixColleft";


            ID = DataBinder.Eval(e.Row.DataItem, "SendFlag").ToString();
            Button B2 = (Button)e.Row.FindControl("Button2");
            Button B3 = (Button)e.Row.FindControl("Button3");
            if (ID.Trim() == "Y")
            {
                B3.Enabled = false;
                B2.Enabled = true;
            }
            else 
            {
                B3.Enabled = true;
                B2.Enabled = false;
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");

                ID = ID + e.Row.Cells[i].Text;
            }

            e.Row.Attributes.Add("onclick", "if(window.oldtr!=null){window.oldtr.runtimeStyle.cssText='';}this.runtimeStyle.cssText='background-color:#e6c5fc';window.oldtr=this");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            string id = ((Label)(e.Row.FindControl("Label91"))).Text.ToString().Trim();
            string itemid = ((Label)(e.Row.FindControl("Label92"))).Text.ToString().Trim();
                string sqlitem = "select QTY,DELIVERYSTARTDT from "+BBSCMDIR+".[dbo].[PO_CONFIRMATION_MT] where ID='" + id + "' and ITEMID='" + itemid + "' order by SCHEDULELINEID";
                DataTable dtitem = PDataBaseOperation.PSelectSQLDT(DBType, strconn, sqlitem);// dbo.PSelectSQLDT(sqlitem);
                if (dtitem.Rows.Count > 0)
                {
                    int pp = GridView1.Rows.Count;
                    //((GridView)GridView1.Rows[e.Row.RowIndex].FindControl("GridView2")).DataSource = dtitem;
                   ((GridView) e.Row.FindControl ("GridView2")).DataSource = dtitem;
                   // ((GridView)GridView1.Rows[e.Row.RowIndex].FindControl("GridView2")).DataBind();

                   ((GridView)e.Row.FindControl("GridView2")).DataBind();

                }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    protected void btnsap_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)//true為多選，false為單選
        {
            if (DropDownList5.SelectedValue != "Check_Issue")
            {
                Response.Write("<script>alert('You can only select a record!')</script>");
                return;
            }
            else
            {
                Response.Write("<script>alert('The Check_Issue item can not do confirm!')</script>");
                return;
            }
        }
        else
        {
            int iRet = 0;
            int x = 0;
            string NokiaSite = "";
            string strDocID = "";
            string strPriFlag = "";
            string strLineNO = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked == true)
                {
                    if (iRet == 2)
                    {
                        Response.Write("<script>alert('You can only select a record!')</script>");
                        return;
                    }
                    else
                    {
                        iRet++;
                        NokiaSite = ((Label)GridView1.Rows[i].FindControl("Label9")).Text;
                        strDocID = ((Label)GridView1.Rows[i].FindControl("Label10")).Text;
                        strPriFlag = ((Label)GridView1.Rows[i].FindControl("Label17")).Text;
                        strLineNO = ((Label)GridView1.Rows[i].FindControl("Label12")).Text;
                    }
                }
            }
            if (DropDownList5.SelectedValue == "Check_Issue")
            {
                Response.Write("<script>alert('The Check_Issue item can not do confirm!')</script>");
                return;
            }
            if (NokiaSite != "")
            {
                if (NokiaSite != "DNMP" && NokiaSite != "BNMT")
                {
                    Response.Write("<script>alert('The Site is " + NokiaSite + " You can not confirm the oversea SBI price!')</script>");
                    return;
                }
                if (txtBeginDate.Text != txtendDate.Text)
                {
                    Response.Write("<script>alert('if you confirm the material by site or by documnet, you must make the enddate equal the start date !')</script>");
                    return;
                }
                if (DropDownList5.SelectedValue == "Confirm_BY_Material")
                {
                    string strSap = "Update SBI_InvcItem set  Z_CONFIRM = 'B', ModificationUser ='"+strUserName+"', ModificationDate = '"+dtime+"' where ";
                    if (CheckBox1.Checked == true)
                    {
                        strSap = strSap + "Document_ID='" + strDocID + "'  and Price_Flag='" + strPriFlag + "' and Z_CONFIRM='I'";
                    }
                    else
                    {
                        strSap = strSap + "Document_ID='" + strDocID + "'  and LineNumber='" + strLineNO + "' and Z_CONFIRM='I'";
                    }
                    x = DataBaseOperation.ExecSQL("sql", strconn, strSap);
                    if (iRet == x && (iRet != 0 || x != 0))
                    {
                        Response.Write("<script>alert('Processing OK!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Update Fail!')</script>");
                    }
                }
                else
                {
                    string strSap = "Update SBI_InvcItem set  Z_CONFIRM = 'B', ModificationUser = '"+strUserName+"', ModificationDate = '"+dtime+"' FROM SBI_Main T1 , SBI_Vendor_Code T3 ,SBI_InvcItem T2 where ";
                    if (DropDownList5.SelectedValue == "Confirm_BY_Site")
                    {
                        strSap = strSap + " T1.Document_ID = T2.Document_ID AND T1.SBI_From_GBI = T3.SBI_From_DUNS and T1.SBI_To_GBI = T3.SBI_To_DUNS ";
                        strSap = strSap + " and T2.Foxconn_Location ='" + Foxconn_Location + "' AND T3.Nokia_Site = '" + NokiaSite + "' and T1.Receive_Date >= '" + txtBeginDate.Text + "' ";
                        strSap = strSap + " and T1.Receive_Date <='" + txtendDate + "' AND (T2.Check_Result = '' OR T2.Check_Result IS NULL) and T2.Z_CONFIRM = 'I'";
                        if (Downlist1.Value == "Price_Flag")
                        {
                            if (Downlist2.Value != "ALL")
                            {
                                strSap = strSap + " AND Price_Flag = '" + Downlist2.Value + "'";
                            }
                        }
                        else
                        {
                            if (Downlist2.Value != "ALL")
                            {
                                strSap = strSap + " AND UpLoad_SAP = '" + Downlist2.Value + "'";
                            }
                        }
                        x = DataBaseOperation.ExecSQL(DBType, strconn, strSap);
                        if (iRet == x && (iRet != 0 || x != 0))
                        {
                            Response.Write("<script>alert('Processing OK!')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Update Fail!')</script>");
                        }
                    }
                    else
                    {
                        strSap = strSap + " T1.Document_ID = T2.Document_ID AND T1.SBI_From_GBI = T3.SBI_From_DUNS and T1.SBI_To_GBI = T3.SBI_To_DUNS and T2.Document_ID='" + strDocID + "'";
                        strSap = strSap + " and T2.Foxconn_Location='" + Foxconn_Location + "' AND T3.Nokia_Site = '" + NokiaSite + "' and T1.Receive_Date >= '" + txtBeginDate.Text + "'";
                        strSap = strSap + " and T1.Receive_Date <='" + txtendDate + "' AND (T2.Check_Result = '' OR T2.Check_Result IS NULL) and T2.Z_CONFIRM = 'I'";
                        if (Downlist1.Value == "Price_Flag")
                        {
                            if (Downlist2.Value != "ALL")
                            {
                                strSap = strSap + " AND Price_Flag = '" + Downlist2.Value + "'";
                            }
                        }
                        else
                        {
                            if (Downlist2.Value != "ALL")
                            {
                                strSap = strSap + " AND UpLoad_SAP = '" + Downlist2.Value + "'";
                            }
                        }
                        x = DataBaseOperation.ExecSQL(DBType, strconn, strSap);
                        if (iRet == x && (iRet != 0 || x != 0))
                        {
                            Response.Write("<script>alert('Processing OK!')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Update Fail!')</script>");
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('You not select a record!')</script>");
            }
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

                            and T2.Foxconn_Location = '"+Foxconn_Location+"' and  T2.Foxconn_Division= '"+Foxconn_Division+"' ";
        StrSql = StrSql + strwhere();
        StrSql = StrSql + @"    group by T4.Vendor_Code, T4.Nokia_Site, T1.Document_ID, T3.TTooldatetime, 
                            T1.Receive_Date + ' ' + T1.Receive_Time ,T2.ProductID, T2.LineNumber,T3.ProductQty,
                            T2.Total_Line_Amnt, T3.UnitPrice,T3.Currency_Code,T2.Upload_SAP, T2.UnitPrice,T2.SAP_Price, 
                            T2.Price_Flag, T2.Z_CONFIRM, T2.Check_Result, T2.MSGTYP1, T2.MSGTYP2, cast(T2.SAP_Log as nvarchar(100)),cast(T2.SAP_Log1 as nvarchar(100))  
                            order by Receive_DT  desc, convert(decimal, T2.LineNumber)";
        DataTable dt = DataBaseOperation.SelectSQLDT(DBType, strconn, StrSql);
        string excelName = "output3C7bnmtcr.xls";

        this.ExportDataTable("application/ms-excel", excelName,dt);
    }

    private void ExportDataTable(string FileType, string excelName,DataTable dt)
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

    protected void btnsbi_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)//true為多選，false為單選
        {
            if (DropDownList5.SelectedValue != "Check_Issue")
            {
                Response.Write("<script>alert('You can only select a record!')</script>");
                return;
            }
            else
            {
                Response.Write("<script>alert('The Check_Issue item can not do confirm!')</script>");
                return;
            }
        }
        else
        {
            int iRet = 0;
            int x = 0;
            string NokiaSite = "";
            string strDocID = "";
            string strPriFlag = "";
            string strLineNO = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked == true)
                {
                    
                    if (iRet > 0)
                    {
                        Response.Write("<script>alert('You can only select a record!')</script>");
                        return; 
                    }
                    else
                    {
                        iRet++;
                        NokiaSite = ((Label)GridView1.Rows[i].FindControl("Label9")).Text;
                        strDocID = ((Label)GridView1.Rows[i].FindControl("Label10")).Text;
                        strPriFlag = ((Label)GridView1.Rows[i].FindControl("Label17")).Text;
                        strLineNO = ((Label)GridView1.Rows[i].FindControl("Label12")).Text;   
                    }
                }
            }
            if (DropDownList5.SelectedValue == "Check_Issue")
                {
                    Response.Write("<script>alert('The Check_Issue item can not do confirm!')</script>");
                    return;
                }
                if (NokiaSite != "")
                {
                    if (NokiaSite != "DNMP" && NokiaSite != "BNMT")
                    {
                        Response.Write("<script>alert('The Site is " + NokiaSite + " You can not confirm the oversea SBI price!')</script>");
                        return;
                    }
                    if (txtBeginDate.Text != txtendDate.Text)
                    {
                        Response.Write("<script>alert('if you confirm the material by site or by documnet, you must make the enddate equal the start date !')</script>");
                        return;
                    }
                    if (DropDownList5.SelectedValue == "Confirm_BY_Material")
                    {
                        string strSap = "Update SBI_InvcItem set  Z_CONFIRM = 'A', ModificationUser ='"+strUserName+"', ModificationDate = '"+dtime+"' where ";
                        if (CheckBox1.Checked == true)
                        {
                            strSap = strSap + "Document_ID='" + strDocID + "'  and Price_Flag='" + strPriFlag + "' and Z_CONFIRM='I'";
                        }
                        else
                        {
                            strSap = strSap + "Document_ID='" + strDocID + "'  and LineNumber='" + strLineNO + "' and Z_CONFIRM='I'";
                        }
                        x = DataBaseOperation.ExecSQL("sql", strconn, strSap);
                        if (iRet == x && (x != 0 || iRet != 0))
                        {
                            Response.Write("<script>alert('Processing OK!')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Update Fail!')</script>");
                        }
                    }
                    else
                    {
                        string strSap = "Update SBI_InvcItem set  Z_CONFIRM = 'A', ModificationUser = '"+strUserName+"', ModificationDate = '"+dtime+"' ";
                        if (DropDownList5.SelectedValue == "Confirm_BY_Site")
                        {
                            strSap = strSap + " FROM SBI_Main T1 , SBI_Vendor_Code T3 ,SBI_InvcItem T2 where";
                            strSap = strSap + " T1.Document_ID = T2.Document_ID AND T1.SBI_From_GBI = T3.SBI_From_DUNS and T1.SBI_To_GBI = T3.SBI_To_DUNS ";
                            strSap = strSap + " and T2.Foxconn_Location = '" + Foxconn_Location + "' AND T3.Nokia_Site = '" + NokiaSite + "' and T1.Receive_Date >= '"+txtBeginDate+"'";
                            strSap = strSap + " and T1.Receive_Date <='"+txtendDate+"'  AND (T2.Check_Result = '' OR T2.Check_Result IS NULL) and T2.Z_CONFIRM = 'I'";
                            if (Downlist1.Value == "Price_Flag")
                            {
                                if (Downlist2.Value != "ALL")
                                {
                                    strSap = strSap + " AND Price_Flag = '" + Downlist2.Value + "'";
                                }
                            }
                            else
                            {
                                if (Downlist2.Value != "ALL")
                                {
                                    strSap = strSap + " AND UpLoad_SAP = '" + Downlist2.Value + "'";
                                }
                            }
                            x = DataBaseOperation.ExecSQL(DBType, strconn, strSap);
                            if (iRet == x && (x != 0 || iRet != 0))
                            {
                                Response.Write("<script>alert('Processing OK!')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Update Fail!')</script>");
                            }
                        }
                        else
                        {
                            strSap = strSap + " FROM SBI_Main T1 , SBI_Vendor_Code T3 ,SBI_InvcItem T2 where ";
                            strSap = strSap + " T1.Document_ID = T2.Document_ID AND T1.SBI_From_GBI = T3.SBI_From_DUNS and T1.SBI_To_GBI = T3.SBI_To_DUNS and T2.Document_ID='"+strDocID+"'";
                            strSap = strSap + " and T2.Foxconn_Location='" + Foxconn_Location + "'  AND T3.Nokia_Site = '" + NokiaSite + "' and T1.Receive_Date >= '" + txtBeginDate + "'";
                            strSap = strSap + "  and T1.Receive_Date <='" + txtendDate + "' AND (T2.Check_Result = '' OR T2.Check_Result IS NULL)  and T2.Z_CONFIRM = 'I'";
                            if (Downlist1.Value == "Price_Flag")
                            {
                                if (Downlist2.Value != "ALL")
                                {
                                    strSap = strSap + " AND Price_Flag = '" + Downlist2.Value + "'";
                                }
                            }
                            else
                            {
                                if (Downlist2.Value != "ALL")
                                {
                                    strSap = strSap + " AND UpLoad_SAP = '" + Downlist2.Value + "'";
                                }
                            }
                            x = DataBaseOperation.ExecSQL(DBType, strconn, strSap);
                            if (iRet == x && (x != 0 || iRet != 0))
                            {
                                Response.Write("<script>alert('Processing OK!')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Update Fail!')</script>");
                            }
                        }

                    }
                }
                else
                {
                    Response.Write("<script>alert('You not select a record!')</script>");
                }
            }        
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
                strSql = "update SBI_InvcItem SET Check_Result='M',ModificationUser ='"+strUserName+"',ModificationDate='"+dtime+"' ";
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

    protected void btnadd_Click(object sender, EventArgs e)
    {
        int x = 0;
        float PriNUM = 0F;
        int SapPri = 0;
        string mmm = "";
        string NokiaSite = string.Empty;
        string strSql = string.Empty;
        string DocID = string.Empty;
        string PriID = string.Empty;
        string LineNO = string.Empty;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked == true)
            {
                x++;
                NokiaSite = ((Label)GridView1.Rows[i].FindControl("Label9")).Text;
                DocID = ((Label)GridView1.Rows[i].FindControl("Label10")).Text;
                PriID = ((Label)GridView1.Rows[i].FindControl("Label13")).Text;
                LineNO = ((Label)GridView1.Rows[i].FindControl("Label12")).Text;
            }       
        }
        if (TextBox4.ToString() == "")
        {
            Response.Write("<script>alert('The price is Null!')</script>");
            return;
        }
        mmm = TextBox4.Text;
        //PriNUM = float.Parse(TextBox4.ToString());  //float.Parse 這個方法轉換容易拋出異常，對于格式相當嚴格
        if (!float.TryParse(TextBox4.Text, out PriNUM))  //這個轉換函數比較好，不會拋出異常報錯，如果錯誤，會返回數值型的原值
        {
            Response.Write("<script>alert('Please input correct price!')</script>");
            return;
        }
        if (x > 1 || x == 0)
        {
            Response.Write("<script>alert('You can only select a record!')</script>");
            return;
        }
        if (NokiaSite != "DNMP" && NokiaSite != "BNMT")
        {
            Response.Write("<script>alert('You can not maintain the oversea SAP price!')</script>");
            return;
        }
        strSql = "Update SBI_InvcItem set SAP_Price ='"+TextBox4.ToString()+"',  ModificationUser ='"+strUserName+"', ModificationDate = '"+dtime+"' where ";
        if (CheckBox2.Checked)//true
        {
            strSql = strSql + " Document_ID='" + DocID + "' and ProductID='" + PriID + "' and Z_CONFIRM='I'";
        }
        else//false
        {
            strSql = strSql + " Document_ID='" + DocID + "' and LineNumber='" + LineNO + "' and Z_CONFIRM='I'";
        }
        SapPri = DataBaseOperation.ExecSQL("sql", strconn, strSql);
        if (SapPri == 1)
        {
            Response.Write("<script>alert('Processing OK!')</script>");
        }       
    }

  


    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
      //  PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            //ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
            //ibtnDelete.CommandArgument = GridView1.DataKeys[e.Row.RowIndex].ToString();
            //ibtnEdit.CommandArgument = GridView1.DataKeys[e.Row.RowIndex].ToString();
            lbtnTitle.CommandArgument = GridView1.DataKeys[e.Row.RowIndex].ToString();
        }
        if (e.Row.RowIndex < 0) return;
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#cccccc';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }
 
    }
    protected void gvPubName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        //'80168543','80168544','80168545'
        // Fail 82420633
        string DN = "82613607";
     

        BBSCMlib ww = new BBSCMlib();
        ww.GetPackingFromSFCTOBUFByDN("", "", Session["Param3"].ToString(), Session["Param3"].ToString(), "", "", "", DN);
         
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

         BBSCMlib ww = new BBSCMlib();
         string w111 = ww.GetSapDN("", Session["Param3"].ToString(), Session["Param3"].ToString(), "20140606", "20140620", BBSCMDIR);
   //    string  return_var=   ww.GetSapDN("82420682", "", "");
      //  GetSAP3B2 w1=new GetSAP3B2 ();
      // string w111=  w1.GetSapONE_DN("", "", "");

         
    }
 
 
    protected void Gridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       //  Session["ID"] = GridView1.Rows[e.RowIndex].
         Session["IDDD"] = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
         Session["ITEMID"] = GridView1.DataKeys[e.RowIndex].Values[1].ToString();
         Response.Write("<script>window.open('BBSCMPODetail.aspx','newwindow','width=800,height=800,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=no,top=220,left=350');</script>");
         //    Response.Write("window.open('BBSCMPODetail.aspx','one','width=480,height=200,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=220,left=350');");
                
        
        
        return;
           

    }
}
