using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using EconomyUser;
using System.IO;

public partial class MainBBRYPrg_BBSCMPODetailNew : System.Web.UI.Page
{

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();

    Convertlib ConvertlibPointer = new Convertlib();
    public string connstr = "";
    public static string BBSCMDIR;
    //public string connstr = "Server=10.148.8.168 ;User id=FIH_IHUB_WEB;Pwd=FIH_IHUB_WEB!;Database=NOKIA_IOF";
    public string DocID = string.Empty;
    public string strUserName = string.Empty;
    public string strRegion = string.Empty;
    //public string  ID = string.Empty;
    public string LineNO = string.Empty;
    public string ProCode = string.Empty;
    public string DBType = "SQL";
    public Int32 i;
    public Int32 ssss;
    string itemid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        // ID = Session["IDDD"].ToString();
        ID = Request.QueryString["IDDD"].ToString();
        itemid = Request.QueryString["itemid"].ToString();


        if (!IsPostBack)
        {
            Label71.Text = "0";
            // connstr = "Server=10.186.19.108 ;User id=sa;Pwd=Sa123456;Database=BBSCM";//108
            // connstr = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";
            connstr = Session["Param3"].ToString(); // ConfigurationManager.AppSettings["BBSCM"]; 
            BBSCMDIR = Session["Param7"].ToString();
            selecttable();

            for (Int32 i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[1].Text == "Y")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.Red;
                }
            }
            if (GridView1.Rows.Count > 0)
            {
                if (GridView1.Rows[0].Cells[1].Text == "Y")
                {
                    ImageButton1.Enabled = false;

                    TextBox1.Enabled = false;
                    TextBox2.Enabled = false;
                    TextBox3.Enabled = false;
                    TextBox4.Enabled = false;
                    TextBox5.Enabled = false;
                    TextBox6.Enabled = false;
                    TextBox7.Enabled = false;
                    TextBox8.Enabled = false;
                    TextBox9.Enabled = false;
                    TextBox10.Enabled = false;
                    TextBox11.Enabled = false;
                    TextBox12.Enabled = false;
                    TextBox13.Enabled = false;
                    TextBox14.Enabled = false;
                    TextBox15.Enabled = false;
                    TextBox16.Enabled = false;
                    TextBox17.Enabled = false;
                    TextBox18.Enabled = false;
                    TextBox19.Enabled = false;
                    TextBox20.Enabled = false;
                    TextBox21.Enabled = false;
                    TextBox22.Enabled = false;
                    TextBox23.Enabled = false;
                    TextBox24.Enabled = false;
                }
            }
           
        }
        
    }
    public void selecttable()
    {
        string poid = Request.QueryString["poid"].ToString().Trim();
        string datafrom = Request.QueryString["datafrom"].ToString().Trim();
        string pocnt = Request.QueryString["pocnt"].ToString().Trim();
        string dtablename = "",ufstr="";
        if (datafrom == "pocreate")
        {
            dtablename = "PO_CREATE_DT";
            ufstr = "PO_CREATE_DT_UF1,PO_CREATE_DT_UF2,PO_CREATE_DT_UF3,PO_CREATE_DT_UF4,PO_CREATE_DT_UF5,PO_CREATE_DT_UF6,PO_CREATE_DT_UF7,PO_CREATE_DT_UF8,PO_CREATE_DT_UF9,PO_CREATE_DT_UF10,";
        }
        else
        {
            dtablename = "PO_CHANGE_DT";
            ufstr = "PO_CHANGE_DT_UF1,PO_CHANGE_DT_UF2,PO_CHANGE_DT_UF3,PO_CHANGE_DT_UF4,PO_CHANGE_DT_UF5,PO_CHANGE_DT_UF6,PO_CHANGE_DT_UF7,PO_CHANGE_DT_UF8,PO_CHANGE_DT_UF9,PO_CHANGE_DT_UF10,";
        }

        string strSQL = @"SELECT  ID ,[POID], ITEMID,INVOICEVERIFYINDICATOR,INTERNALID,STANDARDID,PRODUCTBUYERID,PRODUCTSELLERID,PRODUCTCATEGORYID,AMOUNT,Currency,BASEQTY,";
        strSQL = strSQL + " PRODUCTRECIPIENTPARTYID,DESCRIPTION,POCONFIRMATION,DELIVERYNOTIFICATION,INVOICEREQUEST,SCHEDULELINEID,DELIVERYSTARTDT,SCHEDULEQUANTITY,";
        strSQL = strSQL + ufstr;
        strSQL = strSQL + "POCNT,CostAmount,CostCurrencyCode FROM " + dtablename + "  where 1=1 ";

        strSQL = strSQL + " and ID='" + ID + "' and POID ='" + poid + "' and ITEMID ='" + itemid + "' and POCNT='" + pocnt + "'";

        var dt = new DataTable();
        dt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, strSQL);
        if (dt.Rows.Count > 0)
        {
            Label2.Text = dt.Rows[0]["ID"].ToString();
            Label6.Text = dt.Rows[0]["INVOICEVERIFYINDICATOR"].ToString();
            Label10.Text = dt.Rows[0]["STANDARDID"].ToString();
            Label14.Text = dt.Rows[0]["PRODUCTSELLERID"].ToString();
            Label18.Text = dt.Rows[0]["AMOUNT"].ToString() + dt.Rows[0]["Currency"].ToString();
            Label22.Text = dt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString();
            Label26.Text = dt.Rows[0]["POCONFIRMATION"].ToString();
            Label30.Text = dt.Rows[0]["INVOICEREQUEST"].ToString();
            Label34.Text = dt.Rows[0]["DELIVERYSTARTDT"].ToString();
            //Label38.Text = dt.Rows[0]["PO_CREATE_DT_UF1"].ToString();
            //Label42.Text = dt.Rows[0]["PO_CREATE_DT_UF3"].ToString();
            //Label46.Text = dt.Rows[0]["PO_CREATE_DT_UF5"].ToString();
            //Label50.Text = dt.Rows[0]["PO_CREATE_DT_UF7"].ToString();
            //Label63.Text = dt.Rows[0]["PO_CREATE_DT_UF9"].ToString();
            Label4.Text = dt.Rows[0]["ITEMID"].ToString();
            Label8.Text = dt.Rows[0]["INTERNALID"].ToString();
            Label12.Text = dt.Rows[0]["PRODUCTBUYERID"].ToString();
            Label16.Text = dt.Rows[0]["PRODUCTCATEGORYID"].ToString();
            Label20.Text = dt.Rows[0]["BASEQTY"].ToString();
            Label24.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            Label28.Text = dt.Rows[0]["DELIVERYNOTIFICATION"].ToString();
            Label32.Text = dt.Rows[0]["SCHEDULELINEID"].ToString();
            Label36.Text = dt.Rows[0]["SCHEDULEQUANTITY"].ToString();
            //Label40.Text = dt.Rows[0]["PO_CREATE_DT_UF2"].ToString();
            //Label44.Text = dt.Rows[0]["PO_CREATE_DT_UF4"].ToString();
            //Label48.Text = dt.Rows[0]["PO_CREATE_DT_UF6"].ToString();
            //Label52.Text = dt.Rows[0]["PO_CREATE_DT_UF8"].ToString();
            //Label65.Text = dt.Rows[0]["PO_CREATE_DT_UF10"].ToString();
            Label69.Text = dt.Rows[0]["POID"].ToString();
            Label72.Text = dt.Rows[0]["POCNT"].ToString();

            Label38a.Text = dt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString();

           
            var dtNew = new DataTable();
            if (datafrom == "pocreate")
            {
                 dtNew = PDataBaseOperation.PSelectSQLDT(DBType, connstr, "select GivenName from  POSoldToAddress where ID='" + dt.Rows[0]["ID"].ToString() + "'");
            }
            else {

                dtNew = PDataBaseOperation.PSelectSQLDT(DBType, connstr, "select GivenName from  POSoldToAddress where ID=(select ID  from PO_CREATE_DT where POID=(select POID from PO_CHANGE_DT where ID='" + dt.Rows[0]["ID"].ToString() +"'))");

            }
            var geiveName = "";
            if (dtNew.Rows.Count > 0)
            { 
                geiveName=dtNew.Rows[0]["GivenName"].ToString();
            }
            Label40a.Text = geiveName;



            Label55a.Text = dt.Rows[0]["CostAmount"].ToString() + dt.Rows[0]["CostCurrencyCode"].ToString();
            if (dtablename == "PO_CREATE_DT")
            {
                Label57a.Text = dt.Rows[0]["PO_CREATE_DT_UF3"].ToString();
            }
            else
            {
                Label57a.Text = dt.Rows[0]["PO_CHANGE_DT_UF3"].ToString();
            }
        }
        //20150114 add show referenced PO
        DataTable dtpotext = new DataTable();
        if (datafrom == "pocreate")
        {
            dtpotext = PDataBaseOperation.PSelectSQLDT(DBType, connstr, "select TypeCode,ContextText from POText where ID='" + dt.Rows[0]["ID"].ToString() + "' and POID='" + dt.Rows[0]["POID"].ToString() + "' and TypeCode='F01'");
        }
        else
        {
            dtpotext = PDataBaseOperation.PSelectSQLDT(DBType, connstr, "select TypeCode,ContextText from POText where ID=(select ID  from PO_CREATE_DT where POID=(select POID from PO_CHANGE_DT where ID='" + dt.Rows[0]["ID"].ToString() + "')) and POID='" + dt.Rows[0]["POID"].ToString() + "' and TypeCode='F01'");
        }
        if (dtpotext.Rows.Count > 0)
        {
            Label38b.Text = dtpotext.Rows[0]["TypeCode"].ToString() + "_" + dtpotext.Rows[0]["ContextText"].ToString();
        }

        string GetSql = @" SELECT   ID,POID
      ,ITEMID
      ,INVOICEVERIFYINDICATOR
      ,CostCurrencyCode
      ,INTERNALID
      ,STANDARDID
      ,PRODUCTBUYERID
      ,PRODUCTSELLERID
      ,ProductRecipientID
      ,PRODUCTCATEGORYID
      ,AMOUNT
      ,Currency
      ,BASEQTY
      ,Unit
      ,PRODUCTRECIPIENTPARTYID
      ,[DESCRIPTION]
      ,OriginID
      ,OriginItemID
      ,POCONFIRMATION
      ,DELIVERYNOTIFICATION
      ,INVOICEREQUEST
      ,SCHEDULELINEID
      ,DELIVERYSTARTDT
      ,ZoneCode
      ,SCHEDULEQUANTITY
      ,ScheduleLineUnit      
      ,IncoTermsCode
      ,IncoTermsName,CONFIRM_ADD_FLAG
      FROM " + dtablename + " where ID='" + ID + "' and POID ='" + poid + "' and ITEMID ='" + itemid + "' and POCNT='" + pocnt + "'";
        var dtt = new DataTable();
         dtt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, GetSql);

         string ddTStr = "";

        // Label53.Text="Total: "+dtt.Rows.Count+" Rows";
         if (dtt.Rows.Count > 0)
         {
             GridView1.DataSource = dtt;
             GridView1.DataBind();
             Label70.Text = dtt.Rows[0]["SCHEDULEQUANTITY"].ToString();
             ddTStr = "SELECT  sum( CAST(QTY AS NUMERIC(10))) AS QTY  FROM  PO_CONFIRMATION_MT where POID= '" + dtt.Rows[0]["POID"].ToString() + "' and ITEMID ='" + dtt.Rows[0]["ITEMID"].ToString() + "' and ID ='" + dtt.Rows[0]["ID"].ToString() + "'";
         }
       
       

        var dttT = new DataTable();
        dttT = PDataBaseOperation.PSelectSQLDT(DBType, connstr, ddTStr);

         if (dttT.Rows.Count > 0)
         {
             Label71.Text = dttT.Rows[0]["QTY"].ToString();
         }
     
        TextBox17.Text = Label4.Text.Trim();
        TextBox18.Text = Label4.Text.Trim();
        TextBox19.Text = Label4.Text.Trim();
        TextBox20.Text = Label4.Text.Trim();
        TextBox21.Text = Label4.Text.Trim();
        TextBox22.Text = Label4.Text.Trim();
        TextBox23.Text = Label4.Text.Trim();
        TextBox24.Text = Label4.Text.Trim();

        TextBox1.Text = "1";
        TextBox3.Text = "2";
        TextBox5.Text = "3";
        TextBox7.Text = "4";
        TextBox9.Text = "5";
        TextBox11.Text = "6";
        TextBox13.Text = "7";
        TextBox15.Text = "8";

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string s = ((Label)(e.Row.FindControl("Label56"))).Text;
        //    if (s != "Transfer Order")
        //    {
        //        ((Button)(e.Row.FindControl("btn3B2"))).Visible = false;
        //    }

        //}
    }


    //if (e.Row.RowType == DataControlRowType.DataRow)
    //   {
    //       string NoticeCode = gvList.DataKeys[e.Row.RowIndex].Value.ToString();
    //       ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
    //       ibtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";

    //       ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
    //       ibtnEdit.OnClientClick = "window.open('PubNoticeAdd.aspx?NoticeCode=" + NoticeCode + "&op=edit','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";

    //       LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
    //       lbtnTitle.OnClientClick = "window.open('PubNoticeAdd.aspx?NoticeCode=" + NoticeCode + "&op=view','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";
    //   }




    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        int ArrMax = 100;
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

    protected void btn3B2_Click(object sender, EventArgs e)
    {
        //int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        //string DocumentID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label57"))).Text;
        //string sDocRefTypeCode = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label56"))).Text;
        //string sDocRefLineNumber = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label58"))).Text;
        //Session["DocID"] = Label2.Text;
        //Session["DocumentID"] = DocumentID;
        //Session["sDocRefTypeCode"] = sDocRefTypeCode;
        //Session["sDocRefLineNumber"] = sDocRefLineNumber;
        //Response.Write("<script>window.open( 'List3C7vs3B2.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        ////Server.Transfer("List3C7vs3B2.aspx");
        ////Response.Redirect("List3C7vs3B2.aspx");
    }
    protected void InsertRow(string ID, string POID, string ACCEPTSTATUSCODE, string ITEMID, string SCHEDULELINEID, string QTY, string DELIVERYSTARTDT, string DELIVERYENDDT)
    {
        int dt = 0, t1 = 0;
        connstr = Session["Param3"].ToString();
        string strSQL = @"insert into  PO_CONFIRMATION_MT (ID,CREATIONDT,POID,ACCEPTSTATUSCODE,ItemAcceptStatusCode,ITEMID,SCHEDULELINEID,QTY,DELIVERYSTARTDT,DELIVERYENDDT,POCNT ) values (";
        strSQL = strSQL + "'" + Label2.Text + "',CONVERT(varchar(50), getdate(),120),";
        strSQL = strSQL + "'" + Label69.Text + "',";
        strSQL = strSQL + "'" + ACCEPTSTATUSCODE + "' ,";
        strSQL = strSQL + "'" + ACCEPTSTATUSCODE + "' ,";
        strSQL = strSQL + "'" + ITEMID + "' ,";
        strSQL = strSQL + "'" + SCHEDULELINEID + "' ,";
        strSQL = strSQL + "'" + QTY + "' ,'" + DELIVERYSTARTDT + "','" + DELIVERYENDDT + "','" + Label72.Text + "' )";//Label72.Text=POCNT
        Response.Write("<script>alert(' " + strSQL + "');</script>");
        dt = PDataBaseOperation.PExecSQL(DBType, connstr, strSQL);

        string dtablename = "PO_CREATE_DT";
        if (Int32.Parse(Label72.Text.ToString().Trim()) == 1)
        {
            dtablename = "PO_CREATE_DT";
        }
        else
        {
            dtablename = "PO_CHANGE_DT";
        }

        if (dt > 0)
        {
            strSQL = "   update " + dtablename + "  set CONFIRM_ADD_FLAG='Y' where ID='" + ID + "'  and ITEMID='" + ITEMID + "' and POID='" + POID + "' and POCNT='" + Label72.Text + "'";
            t1 = PDataBaseOperation.PExecSQL(DBType, connstr, strSQL);
        }
        else
            Response.Write("<script>alert(' The PO_CONFIRMATION Insert Fail !!');</script>");

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        int POCNT = 10, v1 = 0, v2 = 0, v3 = 0, v4 = 0;
        string[,] arrPO = new string[POCNT + 1, POCNT + 1];
        for (v1 = 0; v1 <= POCNT; v1++)
            for (v2 = 0; v2 <= POCNT; v2++)
                arrPO[v1, v2] = "";

        //TextBox17.Text = Label4.Text.Trim();
        //TextBox18.Text = Label4.Text.Trim();
        //TextBox19.Text = Label4.Text.Trim();
        //TextBox20.Text = Label4.Text.Trim();
        //TextBox21.Text = Label4.Text.Trim();
        //TextBox22.Text = Label4.Text.Trim();
        //TextBox23.Text = Label4.Text.Trim();
        //TextBox24.Text = Label4.Text.Trim();

        arrPO[1, 1] = TextBox17.Text.Trim();
        arrPO[2, 1] = TextBox18.Text.Trim();
        arrPO[3, 1] = TextBox19.Text.Trim();
        arrPO[4, 1] = TextBox20.Text.Trim();
        arrPO[5, 1] = TextBox21.Text.Trim();
        arrPO[6, 1] = TextBox22.Text.Trim();
        arrPO[7, 1] = TextBox23.Text.Trim();
        arrPO[8, 1] = TextBox24.Text.Trim();

        //TextBox1.Text = "1";
        //TextBox3.Text = "2";
        //TextBox5.Text = "3";
        //TextBox7.Text = "4";
        //TextBox9.Text = "5";
        //TextBox11.Text = "6";
        //TextBox13.Text = "7";
        //TextBox15.Text = "8";

        arrPO[1, 2] = TextBox1.Text.Trim();
        arrPO[2, 2] = TextBox3.Text.Trim();
        arrPO[3, 2] = TextBox5.Text.Trim();
        arrPO[4, 2] = TextBox7.Text.Trim();
        arrPO[5, 2] = TextBox9.Text.Trim();
        arrPO[6, 2] = TextBox11.Text.Trim();
        arrPO[7, 2] = TextBox13.Text.Trim();
        arrPO[8, 2] = TextBox15.Text.Trim();

        arrPO[1, 3] = TextBox2.Text.Trim();
        arrPO[2, 3] = TextBox4.Text.Trim();
        arrPO[3, 3] = TextBox6.Text.Trim();
        arrPO[4, 3] = TextBox8.Text.Trim();
        arrPO[5, 3] = TextBox10.Text.Trim();
        arrPO[6, 3] = TextBox12.Text.Trim();
        arrPO[7, 3] = TextBox14.Text.Trim();
        arrPO[8, 3] = TextBox16.Text.Trim();

        arrPO[1, 4] = DropDownList1.Text;
        arrPO[2, 4] = DropDownList2.Text;
        arrPO[3, 4] = DropDownList3.Text;
        arrPO[4, 4] = DropDownList4.Text;
        arrPO[5, 4] = DropDownList5.Text;
        arrPO[6, 4] = DropDownList6.Text;
        arrPO[7, 4] = DropDownList7.Text;
        arrPO[8, 4] = DropDownList8.Text;

        arrPO[1, 5] = Calendar11.DateTextBox.Text;
        arrPO[2, 5] = Calendar1.DateTextBox.Text;
        arrPO[3, 5] = Calendar3.DateTextBox.Text;
        arrPO[4, 5] = Calendar5.DateTextBox.Text;
        arrPO[5, 5] = Calendar7.DateTextBox.Text;
        arrPO[6, 5] = Calendar9.DateTextBox.Text;
        arrPO[7, 5] = Calendar11.DateTextBox.Text;
        arrPO[8, 5] = Calendar13.DateTextBox.Text;

        arrPO[1, 6] = Calendar12.DateTextBox.Text;
        arrPO[2, 6] = Calendar2.DateTextBox.Text;
        arrPO[3, 6] = Calendar4.DateTextBox.Text;
        arrPO[4, 6] = Calendar6.DateTextBox.Text;
        arrPO[5, 6] = Calendar8.DateTextBox.Text;
        arrPO[6, 6] = Calendar10.DateTextBox.Text;
        arrPO[7, 6] = Calendar12.DateTextBox.Text;
        arrPO[8, 6] = Calendar14.DateTextBox.Text;


        // Put im tmp array
        //for (v1 = 0; v1 < DNCnt1; v1++)
        //{
        //    arrayPO_CREATE_MT[v1 + 1, 0] = (v1 + 1).ToString();
        //    arrayPO_CREATE_MT[v1 + 1, 1] = DNdt.Tables[0].Rows[v1]["ID"].ToString().Trim();
        //    arrayPO_CREATE_MT[v1 + 1, 2] = DNdt.Tables[0].Rows[v1]["CreationDT"].ToString().Trim();
        //    arrayPO_CREATE_MT[v1 + 1, 3] = DNdt.Tables[0].Rows[v1]["DNID"].ToString().Trim();
        //    // arrayPO_CREATE_MT[v1 + 1, 3] = "80168556"; //  "80168537";  // Test Only
        //    arrayPO_CREATE_MT[v1 + 1, 4] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 5] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 6] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 7] = "0";
        //    arrayPO_CREATE_MT[v1 + 1, 8] = "N";
        //    tmpDN = arrayPO_CREATE_MT[v1 + 1, 3];
        // }
        string retval = "", Str1 = "";

        v2 = POCNT;
        /////////////////////////////////////////////////////////
        // 1. Check PO does not in PO_COMFORMATION_MT

        string DBType = Session["Param2"].ToString(); //  = DbSql; // DBReadString
        string DBReadString = Session["Param3"].ToString(); // = SqlWebDBA; // DBReadString   44
        string ERWrite = Session["Param4"].ToString(); // = SqlWebDBA; // DBWriteString  215

        string id = Label2.Text.ToString().Trim();
        string poid = Label69.Text.ToString().Trim();
        string itemid = Label4.Text.ToString().Trim();
        string pocnt = Label72.Text.ToString().Trim();
        string sqlr = @"select * from PO_CONFIRMATION_MT where ID='" + id + "' and POID = '" + poid + "' and ITEMID = '" + itemid + "' and POCNT = '" + pocnt + "'";
        DataSet DNdt = PDataBaseOperation.PSelectSQLDS(DBType, DBReadString, sqlr);
        if ((DNdt == null) || (DNdt.Tables[0].Rows.Count > 0))
        {
            Response.Write("<script>alert(' The POID has already been created. !!');</script>");
            return;
        }


        /////////////////////////////////////////////////////////
        // 2. Check Need Qty is Number not Character
        for (v1 = 0; v1 < v2; v1++)
        {
            Str1 = arrPO[v1 + 1, 3]; // PO QTY
            if (Str1.Length > 0)
            {
                if ((arrPO[v1 + 1, 5].Trim() == "") || (arrPO[v1 + 1, 6].Trim() == "")) // Date Not be Space
                {
                    Response.Write("<script>alert(' The Delivery Start/End can not be Empty !!');</script>");
                    return;
                }

                retval = CheckStrToNum(Str1);
                if (retval == "") // Error Not Number 
                {
                    Response.Write("<script>alert(' The Data can not be Number !!');</script>");
                    return;
                }

                v3 = v3 + Convert.ToInt32(Str1);  // Sum Qty

                arrPO[v1 + 1, 9] = "Y";  // Oqy OK and Write switch
            }

        }


        ///////////////////////////////////////
        // 3. Check Sum Qty = PO qty

        double d1 = Convert.ToDouble(Label70.Text); // PO Qty
        v4 = Convert.ToInt32(d1);

        if (v4 != v3)
        {
            Response.Write("<script>alert(' The Input Qty <> PO Qty, Return !!');</script>");
            return;
        }


        try
        {
            Int32 ii;
            ii = 0;

            if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length == 16 && Calendar12.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox2.Text); }
            if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length == 16 && Calendar2.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox4.Text); }
            if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length == 16 && Calendar4.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox6.Text); }
            if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length == 16 && Calendar6.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox8.Text); }
            if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length == 16 && Calendar8.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox10.Text); }
            if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length == 16 && Calendar10.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox12.Text); }
            if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length == 16 && Calendar14.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox14.Text); }
            if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length == 16 && Calendar16.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox16.Text); }

            //Int32 ssss = System.Convert.ToInt32(Label71.Text) + ii;
            if (Label71.Text == "") { Label71.Text = "0"; }

            if (ii == System.Convert.ToInt32(System.Convert.ToDouble(Label70.Text)))
            {
                if (System.Convert.ToInt32(System.Convert.ToDouble(Label70.Text)) != System.Convert.ToInt32(Label71.Text) + ii)
                {

                    Response.Write("<script>alert('Total ITEM QTY is:" + Label70.Text + ",Confirm, Input is: " + Label71.Text + ii + " ,Please check your key in data!!');</script>");

                }
            

                else
                {



                    if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length == 16 && Calendar12.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList1.Text, TextBox17.Text, TextBox1.Text, TextBox2.Text, Calendar11.DateTextBox.Text.Replace("/", "-"), Calendar12.DateTextBox.Text.Replace("/", "-")); }


                    if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length == 16 && Calendar2.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList2.Text, TextBox18.Text, TextBox3.Text, TextBox4.Text, Calendar1.DateTextBox.Text.Replace("/", "-"), Calendar2.DateTextBox.Text.Replace("/", "-")); }

                    if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length == 16 && Calendar4.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList3.Text, TextBox19.Text, TextBox5.Text, TextBox6.Text, Calendar3.DateTextBox.Text.Replace("/", "-"), Calendar4.DateTextBox.Text.Replace("/", "-")); }


                    if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length == 16 && Calendar6.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList4.Text, TextBox20.Text, TextBox7.Text, TextBox8.Text, Calendar5.DateTextBox.Text.Replace("/", "-"), Calendar6.DateTextBox.Text.Replace("/", "-")); }

                    if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length == 16 && Calendar8.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList5.Text, TextBox21.Text, TextBox9.Text, TextBox10.Text, Calendar7.DateTextBox.Text.Replace("/", "-"), Calendar8.DateTextBox.Text.Replace("/", "-")); }

                    if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length == 16 && Calendar10.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList6.Text, TextBox22.Text, TextBox11.Text, TextBox12.Text, Calendar9.DateTextBox.Text.Replace("/", "-"), Calendar10.DateTextBox.Text.Replace("/", "-")); }

                    if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length == 16 && Calendar14.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList7.Text, TextBox23.Text, TextBox13.Text, TextBox14.Text, Calendar13.DateTextBox.Text.Replace("/", "-"), Calendar14.DateTextBox.Text.Replace("/", "-")); }

                    if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length == 16 && Calendar16.DateTextBox.Text.Length == 16)
                    { InsertRow(Label2.Text, Label69.Text, DropDownList8.Text, TextBox24.Text, TextBox15.Text, TextBox16.Text, Calendar15.DateTextBox.Text.Replace("/", "-"), Calendar16.DateTextBox.Text.Replace("/", "-")); }

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox12.Text = "";
                    TextBox13.Text = "";
                    TextBox14.Text = "";
                    TextBox15.Text = "";
                    TextBox16.Text = "";
                    TextBox17.Text = "";
                    TextBox18.Text = "";
                    TextBox19.Text = "";
                    TextBox20.Text = "";
                    TextBox21.Text = "";
                    TextBox22.Text = "";
                    TextBox23.Text = "";
                    TextBox24.Text = "";

                    Response.Write("<script>alert(' Complete!!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('日期輸入錯誤,請點擊日曆輸入！');</script>");
            }
        }
        catch (Exception ex)
        {
            string strerr = ex.Message;
            Response.Write("<script>alert('Error Raise:" + i.ToString() + "*" + ssss + ex.Message + "Please check your keyin data!!');</script>");
        }


    }

    // Backup
    protected void BackupImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        string str1 = "", str2 = "", str3 = "";

        str1 = CheckStrToNum(TextBox2.Text);

        try
        {
            Int32 ii;
            ii = 0;

            if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length == 16 && Calendar12.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox2.Text); }
            if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length == 16 && Calendar2.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox4.Text); }
            if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length == 16 && Calendar4.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox6.Text); }
            if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length == 16 && Calendar6.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox8.Text); }
            if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length == 16 && Calendar8.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox10.Text); }
            if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length == 16 && Calendar10.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox12.Text); }
            if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length == 16 && Calendar14.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox14.Text); }
            if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length == 16 && Calendar16.DateTextBox.Text.Length == 16)
            { ii = ii + System.Convert.ToInt32(TextBox16.Text); }

            //Int32 ssss = System.Convert.ToInt32(Label71.Text) + ii;
            if (Label71.Text == "") { Label71.Text = "0"; }
            if (System.Convert.ToInt32(System.Convert.ToDouble(Label70.Text)) != System.Convert.ToInt32(Label71.Text) + ii)
            {

                Response.Write("<script>alert('Total ITEM QTY is:" + Label70.Text + ",Confirm, Input is: " + Label71.Text + ii + " ,Please check your key in data!!');</script>");

            }

            else
            {



                if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length == 16 && Calendar12.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList1.Text, TextBox17.Text, TextBox1.Text, TextBox2.Text, Calendar11.DateTextBox.Text.Replace("/", "-"), Calendar12.DateTextBox.Text.Replace("/", "-")); }


                if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length == 16 && Calendar2.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList2.Text, TextBox18.Text, TextBox3.Text, TextBox4.Text, Calendar1.DateTextBox.Text.Replace("/", "-"), Calendar2.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length == 16 && Calendar4.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList3.Text, TextBox19.Text, TextBox5.Text, TextBox6.Text, Calendar3.DateTextBox.Text.Replace("/", "-"), Calendar4.DateTextBox.Text.Replace("/", "-")); }


                if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length == 16 && Calendar6.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList4.Text, TextBox20.Text, TextBox7.Text, TextBox8.Text, Calendar5.DateTextBox.Text.Replace("/", "-"), Calendar6.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length == 16 && Calendar8.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList5.Text, TextBox21.Text, TextBox9.Text, TextBox10.Text, Calendar7.DateTextBox.Text.Replace("/", "-"), Calendar8.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length == 16 && Calendar10.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList6.Text, TextBox22.Text, TextBox11.Text, TextBox12.Text, Calendar9.DateTextBox.Text.Replace("/", "-"), Calendar10.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length == 16 && Calendar14.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList7.Text, TextBox23.Text, TextBox13.Text, TextBox14.Text, Calendar13.DateTextBox.Text.Replace("/", "-"), Calendar14.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length == 16 && Calendar16.DateTextBox.Text.Length == 16)
                { InsertRow(Label2.Text, Label69.Text, DropDownList8.Text, TextBox24.Text, TextBox15.Text, TextBox16.Text, Calendar15.DateTextBox.Text.Replace("/", "-"), Calendar16.DateTextBox.Text.Replace("/", "-")); }




                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox13.Text = "";
                TextBox14.Text = "";
                TextBox15.Text = "";
                TextBox16.Text = "";

                TextBox17.Text = "";
                TextBox18.Text = "";
                TextBox19.Text = "";
                TextBox20.Text = "";
                TextBox21.Text = "";
                TextBox22.Text = "";
                TextBox23.Text = "";
                TextBox24.Text = "";




                Response.Write("<script>alert(' Complete!!');</script>");
            }
        }
        catch (Exception ex)
        {
            string strerr = ex.Message;
            Response.Write("<script>alert('Error Raise:" + i.ToString() + "*" + ssss + ex.Message + "Please check your keyin data!!');</script>");
        }


    }


    // 
    protected void GridView_Edit(object sender, GridViewEditEventArgs e)
    {
        Session["ID"] = GridView1.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session["Item"] = GridView1.DataKeys[e.NewEditIndex].Values[1].ToString();
        string datafrom = Request.QueryString["datafrom"].ToString().Trim();
        Server.Transfer("BBSCMPO_ITEM_DetailNew.aspx?datafrom=" + datafrom);


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close()");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Label70.Text = GridView1.Rows[e.NewSelectedIndex].Cells[54].Text;




        string GetSql = "SELECT  sum( CAST(QTY AS NUMERIC(10))) AS QTY  FROM  PO_CONFIRMATION_MT where ID= '" + GridView1.Rows[e.NewSelectedIndex].Cells[3].Text + "' and ITEMID ='" + GridView1.Rows[e.NewSelectedIndex].Cells[4].Text + "' ";
        DataTable dttT = PDataBaseOperation.PSelectSQLDT(DBType, Session["Param3"].ToString(), GetSql);



        Label71.Text = dttT.Rows[0]["QTY"].ToString();

    }
    // protected void TextBox2_TextChanged(object sender, EventArgs e)
    // {
    //     string retval = "", Str1 = "";
    //     Str1 = TextBox2.Text.Trim();
    //     if ( Str1.Length > 0)
    //     {
    //         retval = CheckStrToNum( Str1 );
    //         if (retval == "")
    //         {
    //             TextBox2.Text = "";
    //             Response.Write("<script>alert(' The Data can not be Character !!');</script>");
    //         } 
    //
    //     }
    // 
    // }


    private string CheckStrToNum(string IStr1)
    {
        int i1 = 0, i2 = 0, i3 = 0;
        string OStr1 = IStr1, tmpstr1 = "";

        char c1 = '0';

        i2 = IStr1.Length;
        for (i1 = 0; i1 < i2; i1++)
        {
            tmpstr1 = IStr1.Substring(i1, 1);
            c1 = Convert.ToChar(tmpstr1);
            if ((c1 < '0') || (c1 > '9'))
            {
                OStr1 = "";
                return (OStr1);
            }

        }

        return (OStr1);

    }


} // end Main Program
