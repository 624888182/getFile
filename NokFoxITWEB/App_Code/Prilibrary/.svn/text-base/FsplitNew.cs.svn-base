using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
/// <summary>
/// Summary description for FsplitNew
/// </summary>
public class FsplitNew : System.Web.UI.Page
{
    DbAccessing myAccessing = new DbAccessing();

    public void ExecConvert4A5CN(int Type_N) // 拆分365天到矩陣 
    {
        int  Forecast_Qty,Qty16,Qty7,arrowint,splitdays;
        string Document_ID, CustomerPN, CustomerSite, Date_str, getdatetime;
        string dCurrent, BeginDate;
        DateTime BeginDatewk;
        //  V______根據 Syncro_4A3_Detail_Alldata 表開始取出所有數據___________________________________________________________

        string getalldata = "SELECT  a.DocumentTime, a.CustomerSite,a.forecast_begindate forecast_begindatea,b.* ";
        getalldata = getalldata + "   FROM SCM.dbo.Syncro_4A3_Detail_PNOneSet  a ,syncro_4A3_Detail b  where ( a.Split_Count !='1' or a.Split_Count is null)  ";
        getalldata = getalldata + "and a.Document_ID=b.Document_ID and a.Forecast_CustomerPN=b.Forecast_CustomerPN and b.Forecast_QtyTypeCode='Discrete Gross Demand'";
        getalldata = getalldata + " order by b.Document_ID,b.Forecast_CustomerPN,b.Forecast_BeginDate";
        DataTable Alldata = myAccessing.ExecuteSqlTable(getalldata);
        if (Alldata.Rows.Count ==0) {return ;} //如果沒有需要執行的料號那摩退出程式.
        //  A______根據 Syncro_4A3_Detail_Alldata 表開始取出所有數據________________________________________________________
        DataTable dt1 = myAccessing.ExecuteSqlTable("select count(*) as rowscount   FROM Syncro_4A3_Detail_PNOneSet   where ( Split_Count !='1' or Split_Count is null)");
         int arrayrows = Convert.ToInt16 (  dt1.Rows[0]["rowscount"].ToString() );
        //int arrayrows = Alldata.Rows.Count ;
        string[,] arraySplit = new string[arrayrows  , 500 ];
        //  V______根據 syncro_4A3_Detail 表開始取出所有數據到數組___________________________________________________________

            Document_ID  = Alldata.Rows[0]["Document_ID"].ToString();
            CustomerPN   = Alldata.Rows[0]["Forecast_CustomerPN"].ToString();
            CustomerSite = Alldata.Rows[0]["CustomerSite"].ToString();
            getdatetime  = Alldata.Rows[0]["forecast_begindatea"].ToString();
            arrowint = 0;
         
        for (int i = 0; i < Alldata.Rows.Count; i++)
        {
            if (Document_ID != Alldata.Rows[i]["Document_ID"].ToString() || CustomerPN != Alldata.Rows[i]["Forecast_CustomerPN"].ToString() || arrowint == 0)
            {//此段代碼為: 1 , 初始化時執行  2 , 新料號出現開始新一行時執行
                arrowint = arrowint + 1;
                arraySplit[arrowint, 1]  = Alldata.Rows[i]["Document_ID"].ToString();
                arraySplit[arrowint, 2]  = Alldata.Rows[i]["CustomerSite"].ToString();
                arraySplit[arrowint, 3]  = Alldata.Rows[i]["Forecast_CustomerPN"].ToString();
                //arraySplit[arrowint, 4]  = Alldata.Rows[i]["DocumentTime"].ToString(); 
                arraySplit[arrowint, 4] = Alldata.Rows[i]["forecast_begindatea"].ToString(); 
                arraySplit[arrowint, 5]  = Alldata.Rows[i]["Conversation_ID"].ToString();
                arraySplit[arrowint, 6]  = Alldata.Rows[i]["Week"].ToString();
                arraySplit[arrowint, 7]  = Alldata.Rows[i]["PartnerLevel_ID"].ToString();
                arraySplit[arrowint, 8]  = Alldata.Rows[i]["Forecast_ID"].ToString();
                arraySplit[arrowint, 9]  = Alldata.Rows[i]["Forecast_LineID"].ToString();
                arraySplit[arrowint, 10] = Alldata.Rows[i]["Forecast_QtyTypeCode"].ToString();
                arraySplit[arrowint, 11] = Alldata.Rows[i]["Forecast_BeginDate"].ToString();
                arraySplit[arrowint, 12] = Alldata.Rows[i]["Forecast_IntervalCode"].ToString();
                arraySplit[arrowint, 13] = Alldata.Rows[i]["Forecast_Qty"].ToString();
                arraySplit[arrowint, 14] = Alldata.Rows[i]["Forecast_UnitOfMeasure"].ToString();
                arraySplit[arrowint, 15] = Alldata.Rows[i]["Forecast_QtyCommit"].ToString();
                arraySplit[arrowint, 16] = Alldata.Rows[i]["ReplyNokia_Flag"].ToString();
                arraySplit[arrowint, 17] = Alldata.Rows[i]["FoxconnUpload_Flag"].ToString();
                arraySplit[arrowint, 18] = Alldata.Rows[i]["Document_Version"].ToString();
                arraySplit[arrowint, 19] = Alldata.Rows[i]["WeekVersion"].ToString();
            }
             


          //getdatetime = Alldata.Rows[i - 1]["DocumentTime"].ToString();            
          //string getdatetime="select Convert(DateTime,SubString(Document_Time,1,8) ,112)  as DateTime1 from syncro_4A3_Main Where Document_ID='" + Document_ID + "'".ToString();

            dCurrent = getdatetime.Substring(0, 8);
            int BtwConut;  
                
                    BeginDate = "";
                    BeginDate = Alldata.Rows[i]["Forecast_BeginDate"].ToString();
                    BeginDate = BeginDate.Substring(0, 8);  
                      
                    DataTable BtwConut1 = myAccessing.ExecuteSqlTable(" select  datediff(dd,cast(  '" + dCurrent + "'  as datetime),cast( '" + BeginDate + "'   as datetime)) as datediff1");
                    //if (Convert.ToInt32(BeginDate) < Convert.ToInt32(dCurrent))
                    //{
                    //    continue;
                    //}
                    if (Alldata.Rows[i]["Forecast_IntervalCode"].ToString() == "Day")
                    {
                        arraySplit[arrowint, Convert.ToInt32(BtwConut1.Rows[0]["datediff1"]) + 20] = Alldata.Rows[i]["Forecast_Qty"].ToString();
                    }
                    else
                    {//如果是week 數據 ,根據當前天後幾天需轉換成Day,在進行轉換 .
                        

                        Forecast_Qty = Convert.ToInt32(Alldata.Rows[i]["Forecast_Qty"]);
                        BeginDatewk = Convert.ToDateTime(BeginDate.Substring(0, 4) + "-" + BeginDate.Substring(4, 2) + "-" + BeginDate.Substring(6, 2));
                        splitdays= 8-Convert.ToInt16( BeginDatewk.DayOfWeek );
                        if (splitdays==8)
                        {  
                            splitdays=1 ;
                            Qty16 = 0;
                            Qty7 = Forecast_Qty ;
                        }
                        else
                        {  
                            Qty16 = Forecast_Qty /splitdays ;
                            Qty7 = Forecast_Qty - Qty16 * (splitdays-1);
                        }

                        for (int k = 0; k < splitdays; k++)
                        {
                            Date_str = BeginDate.Substring(0, 4) + "-" + BeginDate.Substring(4, 2) + "-" + BeginDate.Substring(6, 2);
                            Date_str = Convert.ToDateTime(Date_str).AddDays(k).ToString("d");
                            Date_str = Date_str.Replace("/", "");
                            if (k ==splitdays-1)
                            {
                                arraySplit[arrowint, k + Convert.ToInt32(BtwConut1.Rows[0]["datediff1"]) + 20] = Convert.ToString(Qty7);
                            }
                            else
                            {
                                arraySplit[arrowint, k + Convert.ToInt32(BtwConut1.Rows[0]["datediff1"]) + 20] = Convert.ToString(Qty16);
                            }
                        }
                    }

                    Document_ID = Alldata.Rows[i]["Document_ID"].ToString();
                    CustomerPN = Alldata.Rows[i]["Forecast_CustomerPN"].ToString();
                    CustomerSite = Alldata.Rows[i]["CustomerSite"].ToString();
                    getdatetime = Alldata.Rows[i]["forecast_begindatea"].ToString();
             
        }
        //  A_____根據 syncro_4A3_Detail 表開始取出所有數據到數組___________________________________________________________
        insert4A5(arraySplit, arrowint, Type_N);    // c+n 數據寫入數據庫表 4A5 
       //  ShowArray(arraySplit,   arrayrows); 

    }

    public void insert4A5(string[,] arraySplit, int arrayrows, int Type_N) //將矩陣數據c+n 數據寫入數據庫表 4A5 
    {
        string Date_str, Date_temp = "";
        string StrSql = "";
        int splitdays;
        for (int ii = 1; ii < arrayrows + 1; ii++)
        {
            Date_str = arraySplit[ii, 4].Substring(0, 4) + "-" + arraySplit[ii, 4].Substring(4, 2) + "-" + arraySplit[ii, 4].Substring(6, 2);
            splitdays = Convert.ToInt32(Convert.ToDateTime(Date_str).DayOfWeek);
            splitdays = 8 - splitdays;
            if (splitdays == 8) { splitdays = 1; } // 如果當前天是週日,當前周天數定為 1.
            splitdays = (7 * Type_N) + Convert.ToInt32(splitdays);
                        StrSql = "";
                        for (int k = 0; k < splitdays; k++)
                        {
                            Date_temp = arraySplit[ii, 4].Substring(0, 4) + "-" + arraySplit[ii, 4].Substring(4, 2) + "-" + arraySplit[ii, 4].Substring(6, 2);
                            Date_temp = Convert.ToDateTime(Date_temp).AddDays(k).ToString("s");
                            Date_temp = Date_temp.Substring(0, 10);
                            Date_temp = Date_temp.Replace("-", "");
                            Date_temp = Date_temp.Replace("/", "") + "Z";
                            StrSql = StrSql + "insert into Syncro_4A5_Detail_guozhen ";
                            StrSql = StrSql + "(Conversation_ID,[Week],Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN,Forecast_LineID,Forecast_QtyTypeCode";
                            StrSql = StrSql + ",Forecast_BeginDate,Forecast_IntervalCode,Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,ReplyNokia_Flag,FoxconnUpload_Flag   ";
                            StrSql = StrSql + ",Document_Version,WeekVersion) VALUES('";
                            StrSql = StrSql + arraySplit[ii, 5] + "','";   //Conversation_ID
                            StrSql = StrSql + arraySplit[ii, 6] + "','";   //Week
                            StrSql = StrSql + arraySplit[ii, 1] + "','";   //Document_ID
                            StrSql = StrSql + arraySplit[ii, 7] + "','";   //PartnerLevel_ID
                            StrSql = StrSql + arraySplit[ii, 8] + "','";   //Forecast_ID
                            StrSql = StrSql + arraySplit[ii, 3] + "','";   //Forecast_CustomerPN
                            StrSql = StrSql + arraySplit[ii, 9] + "','";   //Forecast_LineID
                            StrSql = StrSql + arraySplit[ii, 10] + "','";  //Forecast_QtyTypeCode
                          //StrSql = StrSql + arraySplit[ii, 11] + "','";  //Forecast_BeginDate
                            StrSql = StrSql + Date_temp + "','"; //Forecast_BeginDate
                          //  StrSql = StrSql + arraySplit[ii, 12] + "','";//Forecast_IntervalCode
                            StrSql = StrSql +     "Day','"; //Forecast_IntervalCode
                            StrSql = StrSql + arraySplit[ii, 20 + k] + "','"; //Forecast_Qty
                            StrSql = StrSql + arraySplit[ii, 14] + "','";  //Forecast_UnitOfMeasure
                            StrSql = StrSql + arraySplit[ii, 15] + "','";  //Forecast_QtyCommit
                            StrSql = StrSql + arraySplit[ii, 16] + "','";  //ReplyNokia_Flag
                            StrSql = StrSql + arraySplit[ii, 17] + "','";  //FoxconnUpload_Flag
                            StrSql = StrSql + arraySplit[ii, 18] + "','";  //Document_Version
                            StrSql = StrSql + arraySplit[ii, 19] + " ') ;";  //WeekVersion 
                         }
                            //--------------------Append 非需要拆分數據到4a5 表----------------------------
                            StrSql = StrSql + "insert into  Syncro_4A5_Detail_guozhen ";
                            StrSql = StrSql + "(Conversation_ID,[Week],Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN,Forecast_LineID,Forecast_QtyTypeCode";
                            StrSql = StrSql + ",Forecast_BeginDate,Forecast_IntervalCode,Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,ReplyNokia_Flag,FoxconnUpload_Flag   ";
                            StrSql = StrSql + ",Document_Version,WeekVersion)  ";
                            StrSql = StrSql + "select Conversation_ID,[Week],Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN,Forecast_LineID,Forecast_QtyTypeCode";
                            StrSql = StrSql + ",Forecast_BeginDate,Forecast_IntervalCode,Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,ReplyNokia_Flag,FoxconnUpload_Flag   ";
                            StrSql = StrSql + ",Document_Version,WeekVersion from Syncro_4A3_Detail ";
                            StrSql = StrSql + " Where Document_ID='" + arraySplit[ii, 1] + "' ";
                            StrSql = StrSql + " And Forecast_CustomerPN='" + arraySplit[ii, 3] + "' and (Forecast_QtyTypeCode <>'Discrete Gross Demand' or ( ";
                            StrSql = StrSql + "Forecast_QtyTypeCode ='Discrete Gross Demand' and   Forecast_BeginDate> '" + Date_temp + "'));";
                            StrSql = StrSql + "update Syncro_4A3_Detail_PNOneSet set Split_Count ='1'  where Document_ID='" + arraySplit[ii, 1] + "' and Forecast_CustomerPN='" + arraySplit[ii, 3] + "'";
                            myAccessing.ExecuteSql(StrSql);
                           
 
    } 
}
    public void ShowArray(string[,] arraySplit, int arrayrows)//使用顯示矩陣數據
    {
        StringBuilder strHtml = new StringBuilder();
        strHtml.Append(" <table width='1040px' border='1' bordercolor='#0099FF' cellspacing='0' cellpadding='0'  style='border-collapse: collapse;  text-align: center;'>");
        strHtml.Append(" <tr >");
        strHtml.Append(" <td    align='center'>   &nbsp; SEQNO </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_Qty </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Document_ID</td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; CustomerSIte   </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; CustomerPN </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; BeginDate</td> ");

        strHtml.Append(" <td    align='center'>   &nbsp; Conversation_ID </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Week </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; PartnerLevel_ID </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_ID </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_LineID </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_BeginDate </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_IntervalCode </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_Qty </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_UnitOfMeasure </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Forecast_QtyCommit </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; ReplyNokia_Flag </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; FoxconnUpload_Flag </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; Document_Version </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; WeekVersion </td> ");
        strHtml.Append(" <td    align='center'>   &nbsp; others </td> ");

        for (int i0 = 1; i0 <= 376; i0++)
        {
            strHtml.Append(" <td    align='center'>   &nbsp; F" + i0.ToString() + " </td> ");
        }
        strHtml.Append(" </tr>  ");
        for (int ii = 1; ii < arrayrows + 1; ii++)
        {
            strHtml.Append("<tr >");
            strHtml.Append(" <td align='center'>   &nbsp;" + Convert.ToInt32(ii) + "</td> ");
            for (int i1 = 0; i1 < 386; i1++)
            {
                strHtml.Append(" <td align='center'>   &nbsp;" + arraySplit[ii, i1] + "</td> ");
            }
            strHtml.Append(" </tr> ");
        }
        strHtml.Append("  </table> ");
        Response.Write(strHtml);
    }

    public void Appendto4A5(string Document_ID, string CustomerPN)
    {
        //string sSql="Delete From syncro_4a3_detail Where Document_ID='"+Document_ID +"'  And  Forecast_CustomerPN='"+CustomerPN+"'";
        //myAccessing.ExecuteSql(sSql);
        //    sSql = "Insert into syncro_4a5_detail";
        //    sSql = sSql + "(Conversation_ID,Week,Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN, ";
        //    sSql = sSql + " Forecast_LineID,Forecast_QtyTypeCode,Forecast_BeginDate,Forecast_IntervalCode, ";
        //    sSql = sSql + " Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,Foxconn_Site,ReplyNokia_Flag,";
        //    sSql = sSql + " ReplyNokia_ID,ReplyNokia_DateTime,NokiaACK_ID,NokiaACK_DateTime,Split_Count,Merge_Count,";
        //    sSql = sSql + " Check_Count,FoxconnUpload_Flag,Gross_Demand,Net_Demand,Document_Version,WeekVersion) ";
        //    sSql = sSql + "select  ";
        //    sSql = sSql + " Conversation_ID,Week,Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN,";
        //    sSql = sSql + " Forecast_LineID,Forecast_QtyTypeCode,Forecast_BeginDate,Forecast_IntervalCode, ";
        //    sSql = sSql + " Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,Foxconn_Site,ReplyNokia_Flag, ";
        //    sSql = sSql + " ReplyNokia_ID,ReplyNokia_DateTime,NokiaACK_ID,NokiaACK_DateTime,Split_Count,Merge_Count, ";
        //    sSql = sSql + " Check_Count,FoxconnUpload_Flag,Forecast_Qty,Forecast_Qty,Document_Version,WeekVersion ";
        //    sSql = sSql + "from syncro_4A3_Detail ";
        //    sSql = sSql + "Where Document_ID='" + Document_ID + "' ";
        //    sSql = sSql + "And Forecast_CustomerPN='" + CustomerPN + "'";
        //    sSql = sSql + "And Forecast_BeginDate < '" + sNextMonday + "' ";
        //    sSql = sSql + "And Forecast_IntervalCode=''Day''";
        // myAccessing.ExecuteSql(sSql);
        //     sSql = "Insert into syncro_4a5_detail";
        //     sSql =  sSql+" (Conversation_ID,Week,Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN, ";
        //     sSql =  sSql+" Forecast_LineID,Forecast_QtyTypeCode,Forecast_BeginDate,Forecast_IntervalCode, ";
        //     sSql =  sSql+" Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,Foxconn_Site,ReplyNokia_Flag, ";
        //     sSql =  sSql+" ReplyNokia_ID,ReplyNokia_DateTime,NokiaACK_ID,NokiaACK_DateTime,Split_Count,Merge_Count, ";
        //     sSql =  sSql+" Check_Count,FoxconnUpload_Flag,Gross_Demand,Net_Demand,Document_Version,WeekVersion) ";
        //     sSql =  sSql+"select ";
        //     sSql =  sSql+" Conversation_ID,Week,Document_ID,PartnerLevel_ID,Forecast_ID,Forecast_CustomerPN, ";
        //     sSql =  sSql+" Forecast_LineID,Forecast_QtyTypeCode,Forecast_BeginDate,Forecast_IntervalCode, ";
        //     sSql =  sSql+" Forecast_Qty,Forecast_UnitOfMeasure,Forecast_QtyCommit,Foxconn_Site,ReplyNokia_Flag,";
        //     sSql =  sSql+" ReplyNokia_ID,ReplyNokia_DateTime,NokiaACK_ID,NokiaACK_DateTime,Split_Count,Merge_Count, ";
        //     sSql =  sSql+" Check_Count,FoxconnUpload_Flag,Forecast_Qty,Forecast_Qty,Document_Version,WeekVersion ";
        //     sSql =  sSql+"from syncro_4A3_Detail ";
        //     sSql =  sSql + "Where Document_ID='" + Document_ID + "' ";
        //     sSql =  sSql + "And Forecast_CustomerPN='" + CustomerPN + "'";
        //     sSql =  sSql+"And Forecast_BeginDate>='"+sNextMonday+"' ";
        //     sSql =  sSql + "And Forecast_BeginDate<='"+sLastMonday+"' ";
        //  myAccessing.ExecuteSql(sSql);       
    }

    public void Convert4A5CN()
    {
        string sSql = "Select Forecast_BeginDate,Convert(DateTime,Substring(Forecast_BeginDate,1,8),112) BD ";
        sSql = sSql + " From syncro_4A3_Detail  ";
        sSql = sSql + " Where Document_ID=:V_ID  ";
        sSql = sSql + " And Forecast_CustomerPN=:V_PN  ";
        sSql = sSql + " And Forecast_BeginDate < :V_BD  ";
        sSql = sSql + " And Forecast_QtyTypeCode='Discrete Gross Demand'";
        sSql = sSql + " And Forecast_IntervalCode='Week'";
        DataTable dt1 = myAccessing.ExecuteSqlTable(sSql);

        if (1 == 1)
        {
            sSql = "  INSERT INTO syncro_4a5_detail ";
            sSql = sSql + "  ([Conversation_ID],[Week],[Document_ID],[PartnerLevel_ID],  ";
            sSql = sSql + "  [Forecast_ID],[Forecast_CustomerPN],[Forecast_LineID],  ";
            sSql = sSql + "  [Forecast_QtyTypeCode],[Forecast_BeginDate],[Forecast_IntervalCode], ";
            sSql = sSql + "  [Forecast_Qty],[Forecast_UnitOfMeasure],[Forecast_QtyCommit], ";
            sSql = sSql + "  [Foxconn_Site],[ReplyNokia_Flag],[ReplyNokia_ID],  ";
            sSql = sSql + "  [ReplyNokia_DateTime],[NokiaACK_ID],[NokiaACK_DateTime],  ";
            sSql = sSql + "  [Split_Count],[Merge_Count],[Check_Count],[FoxconnUpload_Flag],  ";
            sSql = sSql + "  [Gross_Demand],[Net_Demand],[Document_Version],WeekVersion)  ";
            sSql = sSql + "  SELECT  ";
            sSql = sSql + "  [Conversation_ID],[Week],[Document_ID],[PartnerLevel_ID], ";
            sSql = sSql + "  [Forecast_ID],[Forecast_CustomerPN],:LineID,  ";
            sSql = sSql + "  [Forecast_QtyTypeCode],:CurrentDate,''Day'',  ";
            sSql = sSql + "  Convert(float,[Forecast_Qty]) - Round([Forecast_Qty] / Convert(int,:vDayCount1),0) * (Convert(int,:vDayCount2)-1),  ";
            sSql = sSql + "  [Forecast_UnitOfMeasure],[Forecast_QtyCommit],  ";
            sSql = sSql + "  [Foxconn_Site],[ReplyNokia_Flag],[ReplyNokia_ID],  ";
            sSql = sSql + "  [ReplyNokia_DateTime],[NokiaACK_ID],[NokiaACK_DateTime],  ";
            sSql = sSql + "  [Split_Count],[Merge_Count],[Check_Count],[FoxconnUpload_Flag],  ";
            sSql = sSql + "  Convert(float,[Forecast_Qty]) - Round([Forecast_Qty] / Convert(Int,:vDayCount3),0) * (convert(int,:vDayCount4)-1),  ";
            sSql = sSql + "  Convert(float,[Forecast_Qty]) - Round([Forecast_Qty] / Convert(int,:vDayCount5),0) * (convert(int,:vDayCount6)-1),  ";
            sSql = sSql + "  [Document_Version],WeekVersion  ";
            sSql = sSql + "  FROM dbo.Syncro_4A3_Detail  ";
            sSql = sSql + "  WHERE Forecast_QtyTypeCode=''Discrete Gross Demand'' ";
            sSql = sSql + "  And Document_ID = :vID AND Forecast_CustomerPN = :vPartNo  ";
            sSql = sSql + "  AND Forecast_BeginDate = :vBeginDate  ";
        }
        else
        {
            sSql = "INSERT INTO syncro_4a5_detail ";
            sSql = sSql + "([Conversation_ID],[Week],[Document_ID],[PartnerLevel_ID], ";
            sSql = sSql + "[Forecast_ID],[Forecast_CustomerPN],[Forecast_LineID],  ";
            sSql = sSql + " [Forecast_QtyTypeCode],[Forecast_BeginDate],[Forecast_IntervalCode], ";
            sSql = sSql + " [Forecast_Qty],[Forecast_UnitOfMeasure],[Forecast_QtyCommit], ";
            sSql = sSql + " [Foxconn_Site],[ReplyNokia_Flag],[ReplyNokia_ID],  ";
            sSql = sSql + " [ReplyNokia_DateTime],[NokiaACK_ID],[NokiaACK_DateTime], ";
            sSql = sSql + " [Split_Count],[Merge_Count],[Check_Count],[FoxconnUpload_Flag],  ";
            sSql = sSql + " [Gross_Demand],[Net_Demand],[Document_Version],WeekVersion)  ";
            sSql = sSql + " SELECT  ";
            sSql = sSql + " [Conversation_ID],[Week],[Document_ID],[PartnerLevel_ID],  ";
            sSql = sSql + " [Forecast_ID],[Forecast_CustomerPN],:LineID,  ";
            sSql = sSql + " [Forecast_QtyTypeCode],:CurrentDate,''Day'', ";
            sSql = sSql + " Round(convert(float,[Forecast_Qty])/Convert(int,:vDayCount),0),  ";
            sSql = sSql + " [Forecast_UnitOfMeasure],[Forecast_QtyCommit], ";
            sSql = sSql + " [Foxconn_Site],[ReplyNokia_Flag],[ReplyNokia_ID], ";
            sSql = sSql + " [ReplyNokia_DateTime],[NokiaACK_ID],[NokiaACK_DateTime],  ";
            sSql = sSql + " [Split_Count],[Merge_Count],[Check_Count],[FoxconnUpload_Flag],  ";
            sSql = sSql + " Round([Forecast_Qty]/convert(int,:vDayCount1),0),Round([Forecast_Qty]/convert(int,:vDayCount2),0),  ";
            sSql = sSql + " [Document_Version],WeekVersion  ";
            sSql = sSql + " FROM dbo.Syncro_4A3_Detail ";
            sSql = sSql + " WHERE Forecast_QtyTypeCode=''Discrete Gross Demand''  ";
            sSql = sSql + " And Document_ID = :vID AND Forecast_CustomerPN = :vPartNo  ";
            sSql = sSql + " AND Forecast_BeginDate = :vBeginDate  ";
        }




    }










}
