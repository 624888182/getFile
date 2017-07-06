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
public partial class BBSCMPODetail : System.Web.UI.Page
{

    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();

    Convertlib ConvertlibPointer = new Convertlib();
    public string connstr = "";
    //public string connstr = "Server=10.148.8.168 ;User id=FIH_IHUB_WEB;Pwd=FIH_IHUB_WEB!;Database=NOKIA_IOF";
    public string DocID = string.Empty;
    public string strUserName = string.Empty;
    public string strRegion = string.Empty;
    public string  ID = string.Empty;
    public string LineNO = string.Empty;
    public string ProCode = string.Empty;
    public string DBType = "SQL";
    public static string BBSCMDIR;
    protected void Page_Load(object sender, EventArgs e)
    {
        connstr = ConfigurationManager.AppSettings["BBSCM"]; 
        //connstr = Session["strconn"].ToString();
        //if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
        //{
        //    strUserName = Session["UserName"].ToString();
        //    strRegion = Session["Region"].ToString();
        ID = Session["ID"].ToString();
        BBSCMDIR = Session["Param7"].ToString();
        //    LineNO = Session["LineNO"].ToString();
        //    ProCode = Session["ProCode"].ToString();
        //    connstr = Session["strconn"].ToString();
        //    DBType = Session["dbtype"].ToString();
        //}
        //else
        //{
        //    //Response.Write("<script>window.open( 'I_HubLogin.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //    Server.Transfer("I_HubLogin.aspx");
        //}

           if (!IsPostBack)
           {
               connstr = ConfigurationManager.AppSettings["BBSCM"]; 
               selecttable();
           }
       
    }
    public void selecttable()
    {

        string strSQL = @"SELECT  ID ,[POID], ITEMID,INVOICEVERIFYINDICATOR,INTERNALID,STANDARDID,PRODUCTBUYERID,PRODUCTSELLERID,PRODUCTCATEGORYID,AMOUNT,BASEQTY,";
        strSQL = strSQL + " PRODUCTRECIPIENTPARTYID,DESCRIPTION,POCONFIRMATION,DELIVERYNOTIFICATION,INVOICEREQUEST,SCHEDULELINEID,DELIVERYSTARTDT,SCHEDULEQUANTITY,";
        strSQL = strSQL + "PO_CREATE_DT_UF1,PO_CREATE_DT_UF2,PO_CREATE_DT_UF3,PO_CREATE_DT_UF4,PO_CREATE_DT_UF5,PO_CREATE_DT_UF6,PO_CREATE_DT_UF7,PO_CREATE_DT_UF8,";
        strSQL = strSQL + "PO_CREATE_DT_UF9,PO_CREATE_DT_UF10 FROM " + BBSCMDIR + ".[dbo].[PO_CHANGE_DT]  where 1=1 ";

        strSQL = strSQL + " and ID='" + ID + "'";
      //  strSQL = strSQL + wheresql("1");

        //connstr = ReadParaTxt("WebReadParam.txt", "23524");
        //connstr = ConvertlibPointer.DeEncCodeWithoutEclcode(connstr, ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");

        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, strSQL);
        if (dt.Rows.Count > 0)
        {


Label2.Text  = dt.Rows[0]["ID"].ToString();
Label6.Text = dt.Rows[0]["INVOICEVERIFYINDICATOR"].ToString();
Label10.Text = dt.Rows[0]["STANDARDID"].ToString();
Label14.Text = dt.Rows[0]["PRODUCTSELLERID"].ToString();
Label18.Text = dt.Rows[0]["AMOUNT"].ToString();
Label22.Text = dt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString();
Label26.Text = dt.Rows[0]["POCONFIRMATION"].ToString();
Label30.Text = dt.Rows[0]["INVOICEREQUEST"].ToString();
Label34.Text = dt.Rows[0]["DELIVERYSTARTDT"].ToString();
Label38.Text = dt.Rows[0]["PO_CREATE_DT_UF1"].ToString();
Label42.Text = dt.Rows[0]["PO_CREATE_DT_UF3"].ToString();
Label46.Text = dt.Rows[0]["PO_CREATE_DT_UF5"].ToString();
Label50.Text = dt.Rows[0]["PO_CREATE_DT_UF7"].ToString();
Label63.Text = dt.Rows[0]["PO_CREATE_DT_UF9"].ToString();
Label4.Text = dt.Rows[0]["ITEMID"].ToString();
Label8.Text = dt.Rows[0]["INTERNALID"].ToString();
Label12.Text = dt.Rows[0]["PRODUCTBUYERID"].ToString();
Label16.Text = dt.Rows[0]["PRODUCTCATEGORYID"].ToString();
Label20.Text = dt.Rows[0]["BASEQTY"].ToString();
Label24.Text = dt.Rows[0]["DESCRIPTION"].ToString();
Label28.Text = dt.Rows[0]["DELIVERYNOTIFICATION"].ToString();
Label32.Text = dt.Rows[0]["SCHEDULELINEID"].ToString();
Label36.Text = dt.Rows[0]["SCHEDULEQUANTITY"].ToString();
Label40.Text = dt.Rows[0]["PO_CREATE_DT_UF2"].ToString();
Label44.Text = dt.Rows[0]["PO_CREATE_DT_UF4"].ToString();
Label48.Text = dt.Rows[0]["PO_CREATE_DT_UF6"].ToString();
Label52.Text = dt.Rows[0]["PO_CREATE_DT_UF8"].ToString();
Label65.Text = dt.Rows[0]["PO_CREATE_DT_UF10"].ToString();
Label69.Text = dt.Rows[0]["POID"].ToString();

            
        }

        string GetSql = @" SELECT   ID
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
      ,PO_CREATE_DT_UF1
      ,PO_CREATE_DT_UF2
      ,PO_CREATE_DT_UF3
      ,PO_CREATE_DT_UF4
      ,PO_CREATE_DT_UF5
      ,PO_CREATE_DT_UF6
      ,PO_CREATE_DT_UF7
      ,PO_CREATE_DT_UF8
      ,PO_CREATE_DT_UF9
      ,PO_CREATE_DT_UF10
      ,IncoTermsCode
      ,IncoTermsName
      FROM " + BBSCMDIR + ".dbo.PO_CREATE_DT where ID='" + ID + "'";
         
        DataTable dtt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, GetSql);
        // Label53.Text="Total: "+dtt.Rows.Count+" Rows";

        GridView1.DataSource = dtt;
        GridView1.DataBind();
         Label70.Text = dtt.Rows[0]["SCHEDULEQUANTITY"].ToString();
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
    protected void InsertRow(string ID,string POID, string ACCEPTSTATUSCODE,string ITEMID,string SCHEDULELINEID,string QTY,string DELIVERYSTARTDT,string DELIVERYENDDT )
    {



        string strSQL = @"insert into PO_CONFIRMATION_MT (ID,CREATIONDT,POID,ACCEPTSTATUSCODE,ITEMID,SCHEDULELINEID,QTY,DELIVERYSTARTDT,DELIVERYENDDT ) values (";
        strSQL = strSQL + "'" + Label2.Text + "',cast(getdate() as varchar(50)),";
        strSQL = strSQL + "'" + Label69.Text + "',";
        strSQL = strSQL + "'" + ACCEPTSTATUSCODE + "' ,";
        strSQL = strSQL + "'" + Label4.Text + "' ,";
        strSQL = strSQL + "'" + SCHEDULELINEID + "' ,";
        strSQL = strSQL + "'" + QTY + "' ,'" + DELIVERYSTARTDT + "','" + DELIVERYENDDT + "')";
        int dt = PDataBaseOperation.PExecSQL(DBType, connstr, strSQL);

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
     
    {
        if (TextBox1.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList1.Text, Label6.Text, TextBox1.Text, TextBox2.Text, Calendar11.DateTextBox.Text, Calendar12.DateTextBox.Text); }

        if (TextBox3.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList2.Text, Label6.Text, TextBox3.Text, TextBox4.Text, Calendar1.DateTextBox.Text, Calendar2.DateTextBox.Text); }

        if (TextBox5.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList3.Text, Label6.Text, TextBox5.Text, TextBox6.Text, Calendar3.DateTextBox.Text, Calendar4.DateTextBox.Text); }

        if (TextBox7.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList4.Text, Label6.Text, TextBox7.Text, TextBox8.Text, Calendar5.DateTextBox.Text, Calendar6.DateTextBox.Text); }

        if (TextBox9.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList5.Text, Label6.Text, TextBox9.Text, TextBox10.Text, Calendar7.DateTextBox.Text, Calendar8.DateTextBox.Text); }

        if (TextBox11.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList6.Text, Label6.Text, TextBox11.Text, TextBox12.Text, Calendar9.DateTextBox.Text, Calendar10.DateTextBox.Text); }

        if (TextBox13.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList7.Text, Label6.Text, TextBox13.Text, TextBox14.Text, Calendar13.DateTextBox.Text, Calendar14.DateTextBox.Text); }

        if (TextBox15.Text != "") { InsertRow(Label2.Text, Label4.Text, DropDownList8.Text, Label6.Text, TextBox15.Text, TextBox16.Text, Calendar15.DateTextBox.Text, Calendar16.DateTextBox.Text); }

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
        Response.Write("<script>alert(' Complete!!');</script>");

         
     
    }
    protected void GridView_Edit(object sender, GridViewEditEventArgs e)
    {
        Session["ID"] = GridView1.DataKeys[e.NewEditIndex ].Values[0].ToString();
        Session["Item"] = GridView1.DataKeys[e.NewEditIndex].Values[1].ToString();

        Server.Transfer("BBSCMPO_ITEM_CHANGE_Detail.aspx");


    }
}
