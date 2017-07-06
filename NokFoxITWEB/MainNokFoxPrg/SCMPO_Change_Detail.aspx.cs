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

public partial class MainBBRYPrg_Change_BBSCMPODetail : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {

        ID = Session["IDDD"].ToString();
        BBSCMDIR = Session["Param7"].ToString();

        if (!IsPostBack)
        {
            Label71.Text = "0";
            connstr = Session["Param3"].ToString(); // ConfigurationManager.AppSettings["BBSCM"]; 
            selecttable();

            for (Int32 i = 0; i < GridView1.Rows.Count ; i++)
            {

                if (GridView1.Rows[i].Cells[1].Text == "Y")


                {


                    GridView1.Rows[i].BackColor = System.Drawing.Color.Red ; 
                
                
                }
            
            }


        }

    }
    public void selecttable()
    {

        string strSQL = @"SELECT  ID ,[POID], ITEMID,INVOICEVERIFYINDICATOR,INTERNALID,STANDARDID,PRODUCTBUYERID,PRODUCTSELLERID,PRODUCTCATEGORYID,AMOUNT,BASEQTY,";
        strSQL = strSQL + " PRODUCTRECIPIENTPARTYID,DESCRIPTION,POCONFIRMATION,DELIVERYNOTIFICATION,INVOICEREQUEST,SCHEDULELINEID,DELIVERYSTARTDT,SCHEDULEQUANTITY,POCNT,CostAmount,PO_CHANGE_DT_UF3  FROM " + BBSCMDIR + ".[dbo].[PO_CHANGE_DT]  where 1=1 ";

        strSQL = strSQL + " and ID='" + Session["IDDD"].ToString()  +"' AND ITEMID ='"+ Session["ITEMID"].ToString ()+"'";


        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, strSQL);
        if (dt.Rows.Count > 0)
        {


            Label2.Text = dt.Rows[0]["ID"].ToString();
            Label6.Text = dt.Rows[0]["INVOICEVERIFYINDICATOR"].ToString();
            Label10.Text = dt.Rows[0]["STANDARDID"].ToString();
            Label14.Text = dt.Rows[0]["PRODUCTSELLERID"].ToString();
            Label18.Text = dt.Rows[0]["AMOUNT"].ToString();
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
            Label55a.Text = dt.Rows[0]["CostAmount"].ToString();
            Label57a.Text = dt.Rows[0]["PO_CHANGE_DT_UF3"].ToString();

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
      FROM " + BBSCMDIR + ".dbo.PO_CHANGE_DT where ID='" + ID + "'";

        DataTable dtt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, GetSql);
        // Label53.Text="Total: "+dtt.Rows.Count+" Rows";

        GridView1.DataSource = dtt;
        GridView1.DataBind();
        Label70.Text = dtt.Rows[0]["SCHEDULEQUANTITY"].ToString();


        GetSql = "SELECT  sum( CAST(QTY AS NUMERIC(10))) AS QTY  FROM  PO_CONFIRMATION_MT where ID='" + ID + "'    and  POID= '" + dtt.Rows[0]["POID"].ToString() + "' and ITEMID ='" + dtt.Rows[0]["ITEMID"].ToString() + "' and CHANGEFLAG ='Y'";
        DataTable dttT = PDataBaseOperation.PSelectSQLDT(DBType, connstr, GetSql);



        Label71.Text = dttT.Rows[0]["QTY"].ToString();



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
        connstr = Session["Param3"].ToString();
        string strSQL = @"insert into  PO_CONFIRMATION_MT (ID,CREATIONDT,POID,ACCEPTSTATUSCODE,ItemAcceptStatusCode,ITEMID,SCHEDULELINEID,QTY,DELIVERYSTARTDT,DELIVERYENDDT ,CHANGEFLAG,POCNT) values (";
        strSQL = strSQL + "'" + ID + "',CONVERT(varchar(50), getdate(),120),";
        strSQL = strSQL + "'" + POID + "',";
        strSQL = strSQL + "'" + ACCEPTSTATUSCODE + "' ,";
        strSQL = strSQL + "'" + ACCEPTSTATUSCODE + "' ,";
        strSQL = strSQL + "'" + ITEMID + "' ,";
        strSQL = strSQL + "'" + SCHEDULELINEID + "' ,";
        strSQL = strSQL + "'" + QTY + "' ,'" + DELIVERYSTARTDT + "','" + DELIVERYENDDT + "' ,'Y','"+Label72.Text +"')";
        Response.Write("<script>alert(' " + strSQL + "');</script>");
        int dt = PDataBaseOperation.PExecSQL(DBType, connstr, strSQL);




        strSQL = "   update PO_CHANGE_DT  set CONFIRM_ADD_FLAG='Y' where ID='" + ID + "'  and ITEMID='" + ITEMID + "' ";

         int t1 = PDataBaseOperation.PExecSQL(DBType, connstr, strSQL);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {


        try
        {
            Int32 ii;
            ii = 0;

            if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length > 0 && Calendar12.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox2.Text); }
            if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length > 0 && Calendar2.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox4.Text); }
            if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length > 0 && Calendar4.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox6.Text); }
            if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length > 0 && Calendar6.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox8.Text); }
            if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length > 0 && Calendar8.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox10.Text); }
            if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length > 0 && Calendar10.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox12.Text); }
            if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length > 0 && Calendar14.DateTextBox.Text.Length > 0) 
                     { ii = ii + System.Convert.ToInt32(TextBox14.Text); }
            if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length > 0 && Calendar16.DateTextBox.Text.Length > 0) 
                      { ii = ii + System.Convert.ToInt32(TextBox16.Text); }
      
            //Int32 ssss = System.Convert.ToInt32(Label71.Text) + ii;
            if (Label71.Text == "") { Label71.Text = "0"; }
            if (System.Convert.ToInt32( System.Convert.ToDouble(  Label70.Text)) != System.Convert.ToInt32(Label71.Text) + ii)
            {

               
                Response.Write("<script>alert('Total Demand QTY is:" + Label70.Text + ",All Iput qty is: " + System.Convert.ToInt32( Label70.Text) + ii + " ,Please check your key in data!!');</script>");
              
            }

            else
            {



                if (TextBox2.Text.Length > 0 && TextBox17.Text.Length > 0 && TextBox1.Text.Length > 0 && Calendar11.DateTextBox.Text.Length > 0 && Calendar12.DateTextBox.Text.Length > 0) 
                 { InsertRow(Label2.Text, Label69.Text, DropDownList1.Text, TextBox17.Text, TextBox1.Text, TextBox2.Text, Calendar11.DateTextBox.Text.Replace("/", "-"), Calendar12.DateTextBox.Text.Replace("/", "-")); }


                if (TextBox4.Text.Length > 0 && TextBox18.Text.Length > 0 && TextBox3.Text.Length > 0 && Calendar1.DateTextBox.Text.Length > 0 && Calendar2.DateTextBox.Text.Length > 0) 
                { InsertRow(Label2.Text, Label69.Text, DropDownList2.Text, TextBox18.Text, TextBox3.Text, TextBox4.Text, Calendar1.DateTextBox.Text.Replace("/", "-"), Calendar2.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox6.Text.Length > 0 && TextBox19.Text.Length > 0 && TextBox5.Text.Length > 0 && Calendar3.DateTextBox.Text.Length > 0 && Calendar4.DateTextBox.Text.Length > 0) 
                { InsertRow(Label2.Text, Label69.Text, DropDownList3.Text, TextBox19.Text, TextBox5.Text, TextBox6.Text, Calendar3.DateTextBox.Text.Replace("/", "-"), Calendar4.DateTextBox.Text.Replace("/", "-")); }


                if (TextBox8.Text.Length > 0 && TextBox20.Text.Length > 0 && TextBox7.Text.Length > 0 && Calendar5.DateTextBox.Text.Length > 0 && Calendar6.DateTextBox.Text.Length > 0) 
                { InsertRow(Label2.Text, Label69.Text, DropDownList4.Text, TextBox20.Text, TextBox7.Text, TextBox8.Text, Calendar5.DateTextBox.Text.Replace("/", "-"), Calendar6.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox10.Text.Length > 0 && TextBox21.Text.Length > 0 && TextBox9.Text.Length > 0 && Calendar7.DateTextBox.Text.Length > 0 && Calendar8.DateTextBox.Text.Length > 0) 
                { InsertRow(Label2.Text, Label69.Text, DropDownList5.Text, TextBox21.Text, TextBox9.Text, TextBox10.Text, Calendar7.DateTextBox.Text.Replace("/", "-"), Calendar8.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox12.Text.Length > 0 && TextBox22.Text.Length > 0 && TextBox11.Text.Length > 0 && Calendar9.DateTextBox.Text.Length > 0 && Calendar10.DateTextBox.Text.Length > 0) 
                { InsertRow(Label2.Text, Label69.Text, DropDownList6.Text, TextBox22.Text, TextBox11.Text, TextBox12.Text, Calendar9.DateTextBox.Text.Replace("/", "-"), Calendar10.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox14.Text.Length > 0 && TextBox23.Text.Length > 0 && TextBox13.Text.Length > 0 && Calendar13.DateTextBox.Text.Length > 0 && Calendar14.DateTextBox.Text.Length > 0) { InsertRow(Label2.Text, Label69.Text, DropDownList7.Text, TextBox23.Text, TextBox13.Text, TextBox14.Text, Calendar13.DateTextBox.Text.Replace("/", "-"), Calendar14.DateTextBox.Text.Replace("/", "-")); }

                if (TextBox16.Text.Length > 0 && TextBox24.Text.Length > 0 && TextBox15.Text.Length > 0 && Calendar15.DateTextBox.Text.Length > 0 && Calendar16.DateTextBox.Text.Length > 0)
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
    protected void GridView_Edit(object sender, GridViewEditEventArgs e)
    {
        Session["ID"] = GridView1.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session["Item"] = GridView1.DataKeys[e.NewEditIndex].Values[1].ToString();

        Server.Transfer("SCMPO_Change_ITEM_Detail.aspx");


    }
    protected void Button1_Click(object sender, EventArgs e)
    {     
        Response.Write("<script>window.close()");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Label70.Text = GridView1.Rows[e.NewSelectedIndex].Cells[54].Text;




        string GetSql = "SELECT  sum( CAST(QTY AS NUMERIC(10))) AS QTY  FROM  PO_CONFIRMATION_MT where ID= '" + GridView1.Rows[e.NewSelectedIndex].Cells[3].Text + "' and ITEMID ='" + GridView1.Rows[e.NewSelectedIndex].Cells[4].Text + "' and CHANGEFLAG='Y'";
        DataTable dttT = PDataBaseOperation.PSelectSQLDT(DBType, Session["Param3"].ToString(), GetSql);



       Label71.Text = dttT.Rows[0]["QTY"].ToString();

    }
}
