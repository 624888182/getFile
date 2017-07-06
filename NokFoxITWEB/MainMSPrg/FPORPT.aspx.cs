using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using System.Text;
using Excel;

public partial class MainBBRYPrg_Reports_FPORPT : System.Web.UI.Page
{
    private static string BBSCMDIR;
    protected void Page_Load(object sender, EventArgs e)
    {
        BBSCMDIR = Session["Param7"].ToString();
        if (!IsPostBack)
        {
            GetData();
            dataBind();
        }
    }
    private void dataBind()
    {

        //綁定數據源
        try
        {
            var ds = (DataSet)Session["ds"];
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
        }
        catch (Exception)
        {
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            //獲取到POID 
            var datarow = (System.Data.DataRowView)e.Row.DataItem;
            string poid = datarow.Row.ItemArray[5].ToString();
            var dts = (DataSet)Session["ds"];
            //第一明細表
            DataRow[] dr = dts.Tables[1].Select("poid='" + poid + "'");
            //第二明細表
            DataRow[] dr1 = dts.Tables[2].Select("poid='" + poid + "'");

            if (dr.Length > 0)
            {
                for (int j = 0; j < dr.Length; j++)
                {
                    e.Row.Cells[9].Text += dr[j]["QTY"].ToString() + "</br>";
                    e.Row.Cells[10].Text += dr[j]["DELIVERYSTARTDT"].ToString() + "</br>";
                }
            }

            if (dr1.Length > 0)
            {

                for (int k = 0; k < dr1.Length; k++)
                {
                    e.Row.Cells[11].Text += dr1[k]["SerialIDQTY"].ToString() + "</br>";
                    e.Row.Cells[12].Text += dr1[k]["SENDTIME"].ToString() + "</br>";
                    e.Row.Cells[13].Text += dr1[k]["DNID"].ToString() + "</br>";
                    e.Row.Cells[14].Text += dr1[k]["IssueDT"].ToString() + "</br>";
                }
            }

            int dt2ChildCount = Convert.ToInt32(dr.Length);
            int dt3ChildCount = Convert.ToInt32(dr1.Length);

            if (dt2ChildCount > 0 || dt3ChildCount > 0)
            {

                if (dt2ChildCount != dt3ChildCount)
                {
                    if (dt2ChildCount > dt3ChildCount)
                    {
                        for (int m = 0; m < dt2ChildCount; m++)
                        {

                            int SerialIDQTY = 0;

                            if (m < dt3ChildCount && dt3ChildCount > 0)
                            {
                                SerialIDQTY = Convert.ToInt32(dr1[m]["SerialIDQTY"]);
                            }
                            else
                            {
                                SerialIDQTY = 0;
                            }
                            e.Row.Cells[15].Text += (Convert.ToInt32(dr[m]["QTY"]) - SerialIDQTY).ToString() + "</br>";
                        }
                    }
                    else
                    {
                        for (int n = 0; n < dt3ChildCount; n++)
                        {

                            int QTY = 0;
                            if (n < dt2ChildCount && dt2ChildCount > 0)
                            {
                                QTY = Convert.ToInt32(dr[n]["QTY"]);
                            }
                            else
                            {
                                QTY = 0;
                            }
                            e.Row.Cells[15].Text += (QTY - Convert.ToInt32(dr1[n]["SerialIDQTY"])).ToString() + "</br>";

                        }
                    }
                }
                else
                {
                    if (dt2ChildCount != 0)
                    {
                        for (int m = 0; m < dt2ChildCount; m++)
                        {

                            var SerialIDQTY = dr1[m]["SerialIDQTY"];
                            if (SerialIDQTY == null)
                            {
                                SerialIDQTY = 0;
                            }

                            e.Row.Cells[15].Text += (Convert.ToInt32(dr[m]["QTY"]) - Convert.ToInt32(SerialIDQTY)).ToString() + "</br>";

                        }
                    }
                }
            }

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        try
        {
            dataBind();
        }
        catch (Exception)
        {
        }
    }
    //   private string ReStr()
    //   {
    //       string ReStr = "  SELECT * INTO #comfirmTable FROM( SELECT    F.[POID] , [QTY] ,  [DELIVERYSTARTDT] " +
    //" FROM      [BBSCM].[dbo].[PO_CONFIRMATION_MT] F" +
    //          " JOIN ( SELECT   MAX(pocnt) AS pocnt ," +
    //                         "  SCHEDULELINEID , POID" +
    //               "   FROM     dbo.PO_CONFIRMATION_MT" +
    //                "  GROUP BY POID , SCHEDULELINEID ) G ON F.POID = G.POID" +
    //                                    "   AND F.SCHEDULELINEID = G.SCHEDULELINEID" +
    //                " WHERE     SENDFLAG = 'Y'   ) A " +
    //                "  SELECT * INTO #shiptoTable FROM( SELECT   [POID] ,SerialIDQTY,[SendTime] " +
    //                " FROM      [BBSCM].[dbo].[Delivery_Notification_MT]" +
    //                "  WHERE     SendFlag = 'Y' AND POID IS not NULL)A" +
    //                "  SELECT    A.*,B.ConfirmStr,C.ShiptoStr" +
    //                "  FROM      ( SELECT    [PRODUCTCATEGORYID] ," +
    //                     "  [PRODUCTBUYERID] ," +
    //                     "  [DESCRIPTION] ," +
    //                    "   [OriginID] ," +
    //                    "   [OriginItemID] ," +
    //                    "   D.[POID] ," +
    //                    "   [ITEMID] ," +
    //                    "   [DELIVERYSTARTDT] ," +
    //                     "  [SCHEDULEQUANTITY] ," +
    //                     "  D.[POCNT]" +
    //           "  FROM      dbo.PO_CHANGE_DT D" +
    //                    "   RIGHT JOIN ( SELECT MAX(POCNT) AS pocnt ,  POID  FROM   PO_CHANGE_DT  GROUP BY POID" +
    //                                "  ) E ON D.POID = E.POID  AND D.POCNT = e.pocnt" +
    //          "    UNION ALL" +
    //          "   SELECT    [PRODUCTCATEGORYID] ," +
    //                   "    [PRODUCTBUYERID] ," +
    //                   "     [DESCRIPTION] ," +
    //                  "     [OriginID] ," +
    //                  "     [OriginItemID] ," +
    //                 "      [POID] ," +
    //                   "    [ITEMID] ," +
    //                 "    CONVERT(DATE, [DELIVERYSTARTDT],103) AS DELIVERYSTARTDT  ," +
    //                  "     [SCHEDULEQUANTITY] ," +
    //                   "    [POCNT]" +
    //           "  FROM      dbo.PO_CREATE_DT" +
    //           "   WHERE     POID IN (" +
    //                "       SELECT  POID" +
    //                "       FROM    dbo.PO_CREATE_DT" +
    //                "       WHERE   POID NOT  IN ( SELECT   POID" +
    //             "                                 FROM     dbo.PO_CHANGE_DT ) )" +
    //         "    ) A LEFT OUTER JOIN(SELECT    POID ," +
    //         "  ConfirmStr = STUFF(( SELECT  QTY + '|' + CONVERT(NVARCHAR(10),[DELIVERYSTARTDT])+';'" +
    //                          "     FROM   #comfirmTable t" +
    //                          "      WHERE  POID = #comfirmTable.POID" +
    //                          "    FOR" +
    //                          "     XML PATH('')" +
    //                          "   ), 10, 1, '')" +
    //                        " FROM      #comfirmTable" +
    //                        " GROUP BY  POID) B ON A.POID=B.POID" +
    //           "    LEFT OUTER JOIN(  SELECT    POID ," +
    //           "   ShiptoStr = STUFF(( SELECT CONVERT(VARCHAR(10), SerialIDQTY) + '|' + CONVERT(VARCHAR(10),SendTime)+';'" +
    //           "                         FROM   #shiptoTable t" +
    //                       "         WHERE  POID = #shiptoTable.POID" +
    //                        "         FOR" +
    //                        "        XML PATH('')" +
    //                        "      ), 10, 1, '')" +
    //                                    " FROM      #shiptoTable" +
    //                                    " GROUP BY  POID" +
    //                                   ") C ON a.POID=C.POID";
    //       return ReStr;
    //   }

    //獲取數據集
    private void GetData()
    {
        //string strconn = "Server=10.83.18.93 ;User id=BBRY;Pwd=Qa123456;Database=BBSCM";
        string strconn = Session["Param3"].ToString();
        //PRODUCTCATEGORYID
        string pf = "";
        if (!string.IsNullOrEmpty(this.txtFamily.Text.Trim()))
        {
            pf = "and PRODUCTCATEGORYID='" + txtFamily.Text.Trim() + "'";
        }

        string t1, t2, t3;
        t1 = "  SELECT  *   FROM    ( SELECT    [PRODUCTCATEGORYID] ," +
                            "  [PRODUCTBUYERID] ," +
                             "   [DESCRIPTION] ," +
                             "   [OriginID] ," +
                             "   [OriginItemID] ," +
                             "   D.[POID] ," +
                             "   [ITEMID] ," +
                             "   substring(DELIVERYSTARTDT,0,11) as DELIVERYSTARTDT  ," +//convert(date,[DELIVERYSTARTDT],101)
                             "   [SCHEDULEQUANTITY] ," +
                             "   D.[POCNT]" +
                  "    FROM      dbo.PO_CHANGE_DT D " +
                             "   RIGHT JOIN ( SELECT MAX(POCNT) AS pocnt , POID " +
                                           "  FROM   PO_CHANGE_DT  GROUP BY POID" +
                                         "  ) E ON D.POID = E.POID   AND D.POCNT = e.pocnt" +
                    "  UNION ALL" +
                   "   SELECT    [PRODUCTCATEGORYID] ," +
                             "   [PRODUCTBUYERID] ," +
                            "    [DESCRIPTION] ," +
                             "   [OriginID] ," +
                             "   [OriginItemID] ," +
                             "   [POID] ," +
                             "   [ITEMID] ," +
                             "   substring(DELIVERYSTARTDT,0,11) as DELIVERYSTARTDT," +//DELIVERYSTARTDT
                             "   [SCHEDULEQUANTITY] ," +
                             "   [POCNT]" +
                  "    FROM      dbo.PO_CREATE_DT " +
                   "   WHERE     POID IN (" +
                             "   SELECT  POID" +
                             "   FROM    dbo.PO_CREATE_DT" +
                             "   WHERE   POID NOT  IN ( SELECT" +
                                                        "  POID" +
                                                     "  FROM" +
                                                       "   dbo.PO_CHANGE_DT ) )  ) A where 1=1 " + pf;
        t2 = "SELECT  F.[POID] ,"
                 + " [ITEMID] ,"
                 + "  g.[SCHEDULELINEID] ,"
                 + "  [QTY] ,"
                 + "  substring(DELIVERYSTARTDT,0,11) as DELIVERYSTARTDT,"//CONVERT(NVARCHAR(10), [DELIVERYSTARTDT]) AS [DELIVERYSTARTDT]
                 + "  [CONFIRMFLAG] ,"
                   + "[ERPFLAG] ,"
                   + "[SENDFLAG] ,"
                   + "[SENDTIME] ,"
                  + " [SendLog] ,"
                  + " [USERCONFIRMFLAG] ,"
                  + " [CHANGEFLAG] ,"
                  + " g.[POCNT]"
           + " FROM    " + BBSCMDIR + ".[dbo].[PO_CONFIRMATION_MT] F"
                  + " JOIN ( SELECT   MAX(pocnt) AS pocnt ,"
                                 + "  SCHEDULELINEID ,"
                                 + "  POID"
                        + "  FROM     dbo.PO_CONFIRMATION_MT"
                        + "  GROUP BY POID ,"
                                 + "  SCHEDULELINEID"
                      + "  ) G ON F.POID = G.POID AND f.POCNT=g.pocnt "
                             + "  AND F.SCHEDULELINEID = G.SCHEDULELINEID";

        t3 = "SELECT  [DNID] ,"
                  + " [POID] ,CONVERT(NVARCHAR(10),[IssueDT]) AS IssueDT, "
                    + "  CONVERT(NVARCHAR(10),[SendTime]) AS SendTime  ,"
                    + " [WayBillID] ,"
                    + " [SerialIDQTY]"
             + " FROM     " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] ";

        //數據淶源
        int dataFrom = 0;

        //條件語句
        string whereStr = "";
        whereStr = Request.QueryString["ws"];

        switch (dpDataFrom.SelectedValue.ToString())
        {
            case "0":
                dataFrom = 1;
                whereStr = "1=1 ";
                if (txtFrom.Text.Trim() != "")
                {
                    whereStr += " and CREATIONDT>='" + txtFrom.Text.Trim().Replace('/', '-') + "'";
                }
                if (txtTo.Text.Trim() != "")
                {
                    whereStr += " and CREATIONDT<='" + txtTo.Text.Trim().Replace('/', '-') + "'";
                }

                if (txtPOIDFrom.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions1.Text + "'" + txtPOIDFrom.Text.Trim() + "'";
                }
                if (txtPOIDTo.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions2.Text + "'" + txtPOIDTo.Text.Trim() + "'";
                }
                break;
            case "1":
                dataFrom = 2;
                whereStr = "1=1 ";
                if (txtFrom.Text.Trim() != "")
                {
                    whereStr += " and DELIVERYSTARTDT>='" + txtFrom.Text.Trim().Replace('/', '-') + "'";
                }
                if (txtTo.Text.Trim() != "")
                {
                    whereStr += " and DELIVERYSTARTDT<='" + txtTo.Text.Trim().Replace('/', '-') + "'";
                }

                if (txtPOIDFrom.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions1.Text + "'" + txtPOIDFrom.Text.Trim() + "'";
                }
                if (txtPOIDTo.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions2.Text + "'" + txtPOIDTo.Text.Trim() + "'";
                }
                break;
            case "2":
                dataFrom = 3;
                whereStr = "1=1 ";
                if (txtFrom.Text.Trim() != "")
                {
                    whereStr += " and SendTime>='" + txtFrom.Text.Trim().Replace('/','-') + "'";
                }
                if (txtTo.Text.Trim() != "")
                {
                    whereStr += " and SendTime<='" + txtTo.Text.Trim().Replace('/', '-') + "'";
                }
                if (txtPOIDFrom.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions1.Text + "'" + txtPOIDFrom.Text.Trim() + "'";
                }
                if (txtPOIDTo.Text.Trim() != "")
                {
                    whereStr += " and POID " + dpConditions2.Text + "'" + txtPOIDTo.Text.Trim() + "'";
                }
                break;
        }



        //查詢語句
        string sqlstr = "";

        //默認情況
        if (dataFrom == 0)
        {

            sqlstr = t1 + " ORDER BY POID desc  ";

            sqlstr += t2 + " WHERE   SENDFLAG = 'Y'  ORDER BY F.POID desc,F.ITEMID,SCHEDULELINEID,DELIVERYSTARTDT ASC	";

            sqlstr += t3 + "  WHERE    SendFlag = 'Y'  ORDER BY POID desc, SendTime ASC    ";
        }

        /*------------------------条件1---------------------------*/
        if (dataFrom == 1)
        {
            sqlstr = "";
            sqlstr = t1 + "   and   A.POID IN ( SELECT  POID   FROM    dbo.PO_CREATE_MT   WHERE  1=1 AND " + whereStr + " ) ORDER BY POID desc  ";

            sqlstr += t2 + " WHERE   SENDFLAG = 'Y'        AND F.POID IN ( SELECT  POID   FROM    dbo.PO_CREATE_MT  WHERE  1=1 AND " + whereStr + " )"
                          + " ORDER BY  F.POID desc,F.ITEMID,SCHEDULELINEID,DELIVERYSTARTDT ASC	";

            sqlstr += t3 + "  WHERE    SendFlag = 'Y' AND POID IN ( SELECT    POID  FROM      dbo.PO_CREATE_MT "
                                          + " WHERE    1=1  AND " + whereStr + ")"
                        + " ORDER BY POID desc, SendTime ASC    ";
        }
        /*------------------------条件2---------------------------*/
        if (dataFrom == 2)
        {
            sqlstr = "";
            sqlstr = t1 + "   and   A.POID IN ( SELECT  POID   FROM    dbo.PO_CONFIRMATION_MT" +
                                        "     WHERE  SENDFLAG = 'Y' AND " + whereStr + " )  ORDER BY POID desc  ";

            sqlstr += t2 + " WHERE   SENDFLAG = 'Y'   and " + whereStr + " ORDER BY F.POID desc,F.ITEMID,SCHEDULELINEID,DELIVERYSTARTDT ASC	";

            sqlstr += t3 + "  WHERE    SendFlag = 'Y' AND POID IN ( SELECT    POID  FROM      dbo.PO_CONFIRMATION_MT"
                            + " WHERE   SENDFLAG = 'Y'  AND " + whereStr + ")"
                             + " ORDER BY POID desc, SendTime ASC    ";
        }
        /*------------------------条件3---------------------------*/
        if (dataFrom == 3)
        {
            sqlstr = "";
            sqlstr = t1 + "   and   A.POID IN ( SELECT  POID  FROM    dbo.Delivery_Notification_MT" +
                                "     WHERE  SENDFLAG = 'Y' AND " + whereStr + " )  ORDER BY POID desc  ";

            sqlstr += t2 + "  WHERE   SendFlag ='Y'   AND " + whereStr + " "
                        + " ORDER BY  F.POID desc,F.ITEMID,SCHEDULELINEID,DELIVERYSTARTDT ASC	";

            sqlstr += t3 + "  WHERE    SendFlag = 'Y' AND " + whereStr + " ORDER BY POID desc, SendTime ASC    ";
        }


        var dbo = new PDataBaseOperation("sql", strconn);
        var dts = new DataSet();
        dts = dbo.PSelectSQLDS(sqlstr);

        Session["ds"] = dts;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GetData();
        dataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ResponseStream();
    }
    public void ResponseStream()
    {
        var dts = new DataSet();
        dts = (DataSet)Session["ds"];
        string res = "";
        if (dts.Tables.Count > 0)
        {
            if (dts.Tables[0].Rows.Count > 0)
            {

                res += " <table cellpadding=\"0\" style='font-size:12.0pt;' cellspacing=\"0\" id=\"tReport\" width=\"100%\">  <thead>  <tr><td colspan='16' style='text-align:center;font-weight:bold; height:50px; font-size:16px;'>PO Trace Report</td></tr>  <tr style='height:50px; background-color:#00B0F0; color:#fff;font-weight:700;'> <th > ProductFamily" +
                    "  </th> <th > PRD# </th> <th >  PRD_Description</th> <th> Order#</th> <th > Order_line/item# </th>" +
                     " <th >BlackBerry_PO#</th> <th > BlackBerry_PO_item#</th><th > Customer_Request_Date </th> <th > Total_Order_Qty </th>  <th >" +
                         " confirm qty</th> <th > Foxconn_Commit_Date  </th> <th >  Shipped_Qty </th> <th > Ship_Date </th><th > DNID </th><th > IssueDT </th><th >  Open_Qty </th>  </tr>  </thead>  <tbody >";

                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    res += "<tr>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["PRODUCTCATEGORYID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["PRODUCTBUYERID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["DESCRIPTION"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["OriginID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["OriginItemID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["POID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["ITEMID"].ToString() + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + Convert.ToDateTime(dts.Tables[0].Rows[i]["DELIVERYSTARTDT"]).ToString("yyyy-MM-dd") + "</td>";
                    res += "<td style='background-color:#fff; border:0.5pt solid #000;'>" + dts.Tables[0].Rows[i]["SCHEDULEQUANTITY"].ToString() + "</td>";

                    DataRow[] dr;
                    dr = dts.Tables[1].Select("poid='" + dts.Tables[0].Rows[i]["POID"].ToString() + "'");

                    if (dr.Length > 0)
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr[j]["QTY"].ToString();
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";

                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td1\">";
                        res += "<table>";
                        for (int j = 0; j < dr.Length; j++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + Convert.ToDateTime(dr[j]["DELIVERYSTARTDT"]).ToString("yyyy-MM-dd");
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";
                    }
                    else
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td1\"></td>";
                    }

                    DataRow[] dr1;
                    dr1 = dts.Tables[2].Select("poid='" + dts.Tables[0].Rows[i]["POID"].ToString() + "'");

                    if (dr1.Length > 0)
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        res += "<table>";
                        for (int k = 0; k < dr1.Length; k++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr1[k]["SerialIDQTY"].ToString();
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";

                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        res += "<table>";
                        for (int k = 0; k < dr1.Length; k++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + Convert.ToDateTime(dr1[k]["SENDTIME"]).ToString("yyyy-MM-dd");
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";

                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        res += "<table>";
                        for (int k = 0; k < dr1.Length; k++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + dr1[k]["DNID"].ToString();
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";

                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td2\">";
                        res += "<table>";
                        for (int k = 0; k < dr1.Length; k++)
                        {
                            res += "<tr>";
                            res += "<td style='background-color:#fff; border:0.5pt solid #000;' class='childTd'>" + Convert.ToDateTime(dr1[k]["IssueDT"]).ToString("yyyy-MM-dd");
                            res += "</td>";
                            res += "</tr>";
                        }
                        res += "</table>";
                        res += "</td>";
                    }
                    else
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;'  class=\"td2\"></td>";
                    }


                    int dt2ChildCount = Convert.ToInt32(dr.Length);
                    int dt3ChildCount = Convert.ToInt32(dr1.Length);
                    if (dt2ChildCount > 0 || dt3ChildCount > 0)
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td3\">";
                        res += "<table>";

                        if (dt2ChildCount != dt3ChildCount)
                        {
                            if (dt2ChildCount > dt3ChildCount)
                            {
                                for (int m = 0; m < dt2ChildCount; m++)
                                {
                                    res += "<tr>";

                                    int SerialIDQTY = 0;

                                    if (m < dt3ChildCount && dt3ChildCount > 0)
                                    {
                                        SerialIDQTY = Convert.ToInt32(dr1[m]["SerialIDQTY"]);
                                    }
                                    else
                                    {
                                        SerialIDQTY = 0;
                                    }
                                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' align='right' class='childTd'>" + (Convert.ToInt32(dr[m]["QTY"]) - SerialIDQTY).ToString();
                                    res += "</td>";
                                    res += "</tr>";
                                }
                                res += "</table>";
                                res += "</td>";

                            }
                            else
                            {
                                for (int n = 0; n < dt3ChildCount; n++)
                                {
                                    res += "<tr>";
                                    int QTY = 0;
                                    if (n < dt2ChildCount && dt2ChildCount > 0)
                                    {
                                        QTY = Convert.ToInt32(dr[n]["QTY"]);
                                    }
                                    else
                                    {
                                        QTY = 0;
                                    }
                                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' align='right' class='childTd'>" + (QTY - Convert.ToInt32(dr1[n]["SerialIDQTY"])).ToString();
                                    res += "</td>";
                                    res += "</tr>";
                                }
                                res += "</table>";
                                res += "</td>";
                            }
                        }
                        else
                        {
                            if (dt2ChildCount != 0)
                            {
                                for (int m = 0; m < dt2ChildCount; m++)
                                {
                                    res += "<tr>";
                                    var SerialIDQTY = dr1[m]["SerialIDQTY"];
                                    if (SerialIDQTY == null)
                                    {
                                        SerialIDQTY = 0;
                                    }

                                    res += "<td style='background-color:#fff; border:0.5pt solid #000;' align='right' class='childTd'>" + (Convert.ToInt32(dr[m]["QTY"]) - Convert.ToInt32(SerialIDQTY)).ToString();
                                    res += "</td>";
                                    res += "</tr>";
                                }
                                res += "</table>";
                                res += "</td>";
                            }
                        }
                    }
                    else
                    {
                        res += "<td style='background-color:#fff; border:0.5pt solid #000;' class=\"td3\"></td>";
                    }
                    res += "</tr>";
                }
                res += "<tr><td colspan='14'></td></tr>";
                res += " </tbody> </table>";
            }
        }
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;

        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("POReport" + DateTime.Now.ToString("yyyyMMdd") + ".xls"));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        Response.AddHeader("Content-Length", res.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载 
        Response.ContentType = "application/ms-excel";
        //// 把文件流发送到客户端 
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
        Response.Write(res);
    }
}