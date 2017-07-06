using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using EconomyUser;
public partial class BBSCMPO_ITEM_Detail : System.Web.UI.Page
{
    public string connstr = "";
    //public string connstr = "Server=10.148.8.168 ;User id=FIH_IHUB_WEB;Pwd=FIH_IHUB_WEB!;Database=NOKIA_IOF";
    public string DocID = string.Empty;
    public string strUserName = string.Empty;
    public string strRegion = string.Empty;
    public string DocumentID = string.Empty;
    public string LineNO = string.Empty;
    public string ProCode = string.Empty;
    public string DBType = "SQL";
    private static string BBSCMDIR;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        connstr = Session["Param3"].ToString();
        BBSCMDIR = Session["Param7"].ToString();
         // connstr = ConfigurationManager.AppSettings["BBSCM"]; 
        //connstr = Session["strconn"].ToString();
        //if (Session["UserName"].ToString() != "" && Session["UserName"].ToString() != null)
        //{
        //    strUserName = Session["UserName"].ToString();
        //    strRegion = Session["Region"].ToString();
        //    DocumentID = Session["DocumentID"].ToString();
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
            connstr = Session["Param3"].ToString();
            selecttable();
        }
       
    }
    public void selecttable()
    {




        string ID;
        string Item;

        ID = Session["ID"].ToString();

        Item = Session["Item"].ToString();



        string strSQL=@"SELECT ID,POID,ITEMID,INVOICEVERIFYINDICATOR,CostCurrencyCode,CostAmount,INTERNALID,STANDARDID,PRODUCTBUYERID,PRODUCTSELLERID";
        strSQL=strSQL+",ProductRecipientID,PRODUCTCATEGORYID,AMOUNT,Currency,BASEQTY,Unit,PRODUCTRECIPIENTPARTYID,DESCRIPTION,IncoTermsCode,IncoTermsName";
        strSQL=strSQL+",OriginID,OriginItemID,POCONFIRMATION,DELIVERYNOTIFICATION,INVOICEREQUEST,SCHEDULELINEID,DELIVERYSTARTDT,ZoneCode,SCHEDULEQUANTITY";
        strSQL = strSQL + ",ScheduleLineUnit,CONFIRMFLAG,CostAmount,PO_CREATE_DT_UF3 FROM " + BBSCMDIR + ".dbo.PO_CREATE_DT  where 1=1 ";


        strSQL = strSQL + " and ID='" + ID + "' and ITEMID ='"+Item+"'";



        //  strSQL = strSQL + wheresql("1");
        //connstr = ReadParaTxt("WebReadParam.txt", "23524");
        //connstr = ConvertlibPointer.DeEncCodeWithoutEclcode(connstr, ConfigurationManager.AppSettings["LicenseString"], ",", "2DBA");
       
        DataTable dt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, strSQL);
        if (dt.Rows.Count > 0)
        { 
            Label2.Text = dt.Rows[0]["ID"].ToString();
            Label4.Text = dt.Rows[0]["POID"].ToString();
            Label6.Text = dt.Rows[0]["ITEMID"].ToString();
            Label8.Text = dt.Rows[0]["INVOICEVERIFYINDICATOR"].ToString();
            Label10.Text = dt.Rows[0]["CostCurrencyCode"].ToString();
            Label12.Text = dt.Rows[0]["INTERNALID"].ToString();
            Label14.Text = dt.Rows[0]["STANDARDID"].ToString();
            Label16.Text = dt.Rows[0]["PRODUCTBUYERID"].ToString();
            Label18.Text = dt.Rows[0]["PRODUCTSELLERID"].ToString();
            Label20.Text = dt.Rows[0]["ProductRecipientID"].ToString();
            Label22.Text = dt.Rows[0]["PRODUCTCATEGORYID"].ToString();
            Label24.Text = dt.Rows[0]["AMOUNT"].ToString();
            Label26.Text = dt.Rows[0]["Currency"].ToString();
            Label28.Text = dt.Rows[0]["BASEQTY"].ToString();
            Label30.Text = dt.Rows[0]["Unit"].ToString();
            Label32.Text = dt.Rows[0]["PRODUCTRECIPIENTPARTYID"].ToString();
            Label34.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            Label36.Text = dt.Rows[0]["IncoTermsCode"].ToString();
            Label38.Text = dt.Rows[0]["IncoTermsName"].ToString();
            Label40.Text = dt.Rows[0]["OriginID"].ToString();
            Label42.Text = dt.Rows[0]["OriginItemID"].ToString();
            Label44.Text = dt.Rows[0]["POCONFIRMATION"].ToString();
            Label46.Text = dt.Rows[0]["DELIVERYNOTIFICATION"].ToString();
            Label48.Text = dt.Rows[0]["INVOICEREQUEST"].ToString();
            Label62.Text = dt.Rows[0]["SCHEDULELINEID"].ToString();
            Label63.Text = dt.Rows[0]["DELIVERYSTARTDT"].ToString();
            Label64.Text = dt.Rows[0]["ZoneCode"].ToString();
            Label65.Text = dt.Rows[0]["CONFIRMFLAG"].ToString();

         
        }

        string GetSql = @" SELECT   [ID]
      ,[CREATIONDT]
      ,[POID]
      ,[ACCEPTSTATUSCODE]
      ,[ITEMID]
      ,[SCHEDULELINEID]
      ,[QTY]
      ,[DELIVERYSTARTDT]
      ,[DELIVERYENDDT]
      ,[CONFIRMFLAG]
      ,[ERPFLAG]
      ,[SENDFLAG]
      ,[SENDTIME]
      ,[SendLog] 
      ,[ItemAcceptStatusCode]
  FROM " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT]   where ID='" + ID + "' and  ITEMID='" + Item + "'";

        DataTable dtt = PDataBaseOperation.PSelectSQLDT(DBType, connstr, GetSql);
 

        GridView1.DataSource = dtt;
        GridView1.DataBind();

    }

 
 
    protected void Button11_Click(object sender, EventArgs e)
    {
        Response.Redirect("Nokiaihub3c7.aspx");
    }
    public string wheresql(string i)
    {
        string strsql = "";
        connstr = Session["strconn"].ToString();
        if (i == "1")
        {
            if (DocumentID != "")
            {
                strsql = strsql + "  T1.Document_ID like '" + DocumentID + "%'";
            }
            if (LineNO != "")
            {
                strsql = strsql + " and T2.LineNumber= '" + LineNO + "'";
            }
            if (ProCode != "")
            {
                strsql = strsql + " and T2.ProductID= '" + ProCode + "'";
            }
        }
        else
        {
            if (DocumentID != "")
            {
                strsql = strsql + "  T1.Document_ID like '" + DocumentID + "%'";
            }
            if (LineNO != "")
            {
                strsql = strsql + " and T1.LineNumber= '" + LineNO + "'";
            }
            if (ProCode != "")
            {
                strsql = strsql + " and T1.ProductID= '" + ProCode + "'";
            }
        }
        return strsql;
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

    protected void btn3B2_Click(object sender, EventArgs e)
    {
        int buttonArgument = Convert.ToInt32(((Button)sender).CommandArgument);
        string DocumentID = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label57"))).Text;
        string sDocRefTypeCode = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label56"))).Text;
        string sDocRefLineNumber = ((Label)(GridView1.Rows[buttonArgument - GridView1.PageIndex * GridView1.PageSize].FindControl("Label58"))).Text;
        Session["DocID"] = Label2.Text;
        Session["DocumentID"] = DocumentID;
        Session["sDocRefTypeCode"] = sDocRefTypeCode;
        Session["sDocRefLineNumber"] = sDocRefLineNumber;
        Response.Write("<script>window.open( 'List3C7vs3B2.aspx','one','width=1000,height=600,status=yes,resizable=yes,scrollbars=yes,titlebar=yes,toolbar=yes,top=10,left=00');</script>");
        //Server.Transfer("List3C7vs3B2.aspx");
        //Response.Redirect("List3C7vs3B2.aspx");
    }
 
    
}
