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
using System.Web.Configuration;
using SCM.GSCMDKen;
using System.Threading;

public partial class MainMotPrg_DB_Sort : System.Web.UI.Page
{
    public static string connRead;//211
    public static string connWrite;//221 sql
    public static string connCheck;//215
    public static string dbType;
    public static string Autoprg;

    static DataBaseOperation dboRead;
    static DataBaseOperation dboWrite;
    static DataBaseOperation dboCheck;
    static int count = 0;
    static int sum = 0;
    static int tmp = 0;
    static int seccess = 0;
    static int seccess2 = 0, seccess3 = 0;
    static bool stopThread = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Param1"].ToString() == "1")
            {
                dbType = Session["Param2"].ToString();
                connRead = Session["Param3"].ToString();
                connWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
            }
            else
            {
                Response.Write("<script>alert('您所傳遞的字符串地址不正確，請重新傳遞!')</script>");
                return;
            connRead = WebConfigurationManager.AppSettings["L8StandByConnectionString"];
            connWrite = WebConfigurationManager.AppSettings["Sql221String"];
            //connCheck = WebConfigurationManager.AppSettings["NormalBakupConnectionString"];
            dbType = "oracle";
            }
            dboRead = new DataBaseOperation(dbType, connRead);
            dboWrite = new DataBaseOperation("sql", connWrite);
            dboCheck = new DataBaseOperation(dbType, connCheck);
            ViewState["SortOrder"] = "last_update_date";
            ViewState["OrderDire"] = "DESC";
            DDL_Year();
            //Button1.Attributes.Add("onclick", "return do_click();");
            //BtnY.Attributes.Add("onclick", "return do_click();");
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Thread sendMain_process;
        seccess = 0;
        seccess2 = 0;
        seccess3 = 0;
        ww.Style["display"] = "none";
        LbMassage.Text = "";
        string year = DDLYear.SelectedItem.Text;
        string mon = DDLMM.SelectedItem.Text;
        DateTime date = DateTime.ParseExact(year + mon + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        stopThread = false;
       if (Check(date) == 1)
       {
           //Thread report_test_Proc = new Thread(() => Data(1, date));
           //report_test_Proc.Start();
           //Thread thread = new Thread(Xxx);
           //thread.Start();
           Data(1, date);
       }
       else
       {
           LbMassage.Text = "此月數據已產出，是否從新產生！";
           BtnY.Visible = true;
           BtnN.Visible = true;
       }
    }
    private void DDL_Year()
    {
        DDLYear.Items.Clear();
        for (int i = 0; i < 3; i++)
        {
            string year = DateTime.Now.AddYears(i-2).ToString("yyyy");
            DDLYear.Items.Add(year);
        }
        DDLMM.Items.Clear();
        for (int i = 0; i < 12; i++)
        {
            string Months = DateTime.Now.AddMonths(i).ToString("MM");
            DDLMM.Items.Add(Months);
        }
    }

    private int Check(DateTime date)
    {
        int flag = 1;
        string month = date.ToString("yyyyMM");
        //string endtime = date.AddMonths(1).ToString("yyyyMMdd");

        string sql = "select * from DelIMEIctl where  Delivery_Date like '" + month + "%'";
        DataTable dt= dboWrite.SelectSQLDT(sql);
        if (dt.Rows.Count > 1)
        {
            flag = 0;
        }
        return flag;
    }
    protected void BtnY_Click(object sender, EventArgs e)
    {
        Clear();
        string year = DDLYear.SelectedItem.Text;
        string mon = DDLMM.SelectedItem.Text;
        DateTime date = DateTime.ParseExact(year + mon + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
        Data(2, date);
        //Thread report_test_Proc = new Thread(() => Data(2, date));
        //report_test_Proc.Start();
    }
    private void Clear()
    {
        LbMassage.Text = "";
        BtnY.Visible = false;
        BtnN.Visible = false;
    }
    private void Data(int f,DateTime date)
    {
       
        DateTime nextdate = date.AddMonths(1);
        string begintime = date.ToString("yyyyMMdd");
        string endtime = date.AddMonths(1).ToString("yyyyMMdd");
        string month = date.ToString("yyyyMM");
        DataTable dt = new DataTable();
        string sql = "select imei,CARTON_NO,PRODUCTID, to_char(LAST_UPDATE_DATE,'yyyyMMddHH24MISS') LAST_UPDATE_DATE from  SHP.CMCS_SFC_SHIPPING_DATA A,(";
        sql = sql + " select distinct INTERNAL_CARTON,LAST_UPDATE_DATE from sap.cmcs_sfc_packing_lines_all";
        sql = sql + " where LAST_UPDATE_DATE >=To_Date('" + begintime + "','yyyyMMdd')  ";
        sql = sql + " and LAST_UPDATE_DATE <To_Date('" + endtime + "','yyyyMMdd')  and INVOICE_NUMBER not in(";
        sql = sql + " select distinct INVOICE_NUMBER from sap.cmcs_sfc_packing_lines_all ";
        sql = sql + " where LAST_UPDATE_DATE >=To_Date('" + begintime + "','yyyyMMdd')  ";
        sql = sql + " and LAST_UPDATE_DATE <To_Date('" + endtime + "','yyyyMMdd')  and INTERNAL_CARTON is null )";
        sql = sql + " ) B where a.CARTON_NO=b.INTERNAL_CARTON and a.STATUS='已出庫' order by LAST_UPDATE_DATE";
        dt = dboRead.SelectSQLDT(sql);
        sum = dt.Rows.Count;
        if (dt.Rows.Count > 0)
        {
            #region 非線程更新
            /*
            string imei = "", cartion = "", pid = "", ddate = "";
            string strsql = "";
            string strsql2 = "";
            int seccess = 0;
            int seccess2 = 0, seccess3=0;
            int mon = 0;
            if (f == 2)
            {
                string sqlm = "select * from DelIMEIctl where Delivery_Date like '" + month + "%'";
                DataTable dtm = dboWrite.SelectSQLDT(sqlm);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    imei = dt.Rows[i]["imei"].ToString();
                    cartion = dt.Rows[i]["CARTON_NO"].ToString();
                    pid = dt.Rows[i]["PRODUCTID"].ToString();
                    ddate = dt.Rows[i]["LAST_UPDATE_DATE"].ToString();
                    DataRow[] dr = dtm.Select("imei ='" + imei + "'");
                    int iii = 0;
                    if (dr.Length != 0)
                    {
                        if (!dr[0].ItemArray[3].ToString().Equals(ddate))
                        {
                            AppPubLib AppPubLibPointer = new AppPubLib();
                            string re1 = AppPubLibPointer.SetBitProc(dtm.Rows[0]["Flag1"].ToString(), "1", "6", "Write", "1");
                            strsql = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=@F3,Flag1=@F5 where IMEI=@F4; ";
                            iii = dboWrite.ExecSQL(strsql, new string[] { "@F1", "@F2", "@F3", "@F4", "@F5" }, new object[] { cartion, pid, ddate, imei, re1 });
                            seccess += iii;
                        }
                        else
                        {
                            seccess3 += 1;
                        }
                    }
                    else
                    {
                        strsql2 = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                        iii = dboWrite.ExecSQL(strsql2, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                        seccess2 += iii;
                    }
                    tmp = i + 1;
                    count = 100 * tmp / sum;
                    //count = i / dt.Rows.Count;
                    
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    imei = dt.Rows[i]["imei"].ToString();
                    cartion = dt.Rows[i]["CARTON_NO"].ToString();
                    pid = dt.Rows[i]["PRODUCTID"].ToString();
                    ddate = dt.Rows[i]["LAST_UPDATE_DATE"].ToString();
                    strsql = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                    int iii = dboWrite.ExecSQL(strsql, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                    if (iii == 0)
                    {
                        string sqlm = "select * from DelIMEIctl where imei='" + imei + "'";
                        DataTable dtm = dboWrite.SelectSQLDT(sqlm);
                        if (dtm.Rows.Count > 0)
                        {
                            if (!dtm.Rows[0]["Delivery_Date"].ToString().Equals(ddate))
                            {
                                AppPubLib AppPubLibPointer = new AppPubLib();
                                string re1 = AppPubLibPointer.SetBitProc(dtm.Rows[0]["Flag1"].ToString(), "1", "6", "Write", "1");
                                strsql2 = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=@F3,Flag1=@F5 where IMEI=@F4; ";
                                int ii = dboWrite.ExecSQL(strsql2, new string[] { "@F1", "@F2", "@F3", "@F4", "@F5" }, new object[] { cartion, pid, ddate, imei, re1 });
                                seccess2 += ii;
                            }
                            else
                            {
                                seccess3 += 1;
                            }
                        }
                        
                    }
                    seccess += iii;
                    tmp = i+1 ;
                    count = 100 * tmp / sum;
                    //count = i / dt.Rows.Count;
                }
            }
            #region 直接更新
            /*
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imei = dt.Rows[i]["imei"].ToString();
                cartion = dt.Rows[i]["CARTON_NO"].ToString();
                pid = dt.Rows[i]["PRODUCTID"].ToString();
                ddate = dt.Rows[i]["LAST_UPDATE_DATE"].ToString();
                if (f == 1)
                {
                    //strsql = "insert into DBSort(Cartion,PID,Delivery_Date,IMEI) values('" + cartion + "','" + pid + "','" + ddate + "','" + imei + "'); ";
                    //strsql2 = "update DBSort set Cartion='" + cartion + "',PID='" + pid + "',Delivery_Date=convert(datetime,'" + ddate + "',21) where IMEI='" + imei + "'; ";
                    strsql = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                    strsql2 = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=convert(datetime,@F3,21) where IMEI=@F4; ";

                }
                else
                {
                    //strsql = "update DBSort set Cartion='" + cartion + "',PID='" + pid + "',Delivery_Date=convert(datetime,'" + ddate + "',21) where IMEI='" + imei + "'; ";
                    //strsql2 = "insert into DBSort(Cartion,PID,Delivery_Date,IMEI) values('" + cartion + "','" + pid + "','" + ddate + "','" + imei + "'); ";
                    strsql = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=convert(datetime,@F3,21) where IMEI=@F4; ";
                    strsql2 = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                    
                }
                int iii = dboWrite.ExecSQL(strsql, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                if (iii == 0)
                {
                    iii = dboWrite.ExecSQL(strsql2, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                }
                seccess += iii;
                count = seccess * 100 / dt.Rows.Count;
                if (mon != count)
                {
                    Response.Flush();
                    mon = count;
                }
            }
             */
         
            #endregion
            //ww.Style["display"] = "block";
            //Button1.Enabled = false;
            Thread th = new Thread(() => StarThread(dt, month, f));
            th.Start();
            //LbMassage.Text = "執行成功！共" + dt.Rows.Count + "條數據,插入:" + seccess + " 條，更新:" + seccess2 + " 條，已存在:" + seccess3 + " 條.";
        }
        else
        {
            LbMassage.Text = "此月無數據！";
            
        }
        //ww.Style["display"] = "none";
        //Button1.Enabled = true;
        //seccess = 0;
        //seccess2 = 0;
        //seccess3 = 0;
    }
    protected void BtnN_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public void StarThread(DataTable dt, string month, int f)
    { 
        string imei = "", cartion = "", pid = "", ddate = "";
        string strsql = "";
        string strsql2 = "";
        
        int mon = 0;
        if (f == 2)
        {
            string sqlm = "select * from DelIMEIctl where Delivery_Date like '" + month + "%'";
            DataTable dtm = dboWrite.SelectSQLDT(sqlm);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imei = dt.Rows[i]["imei"].ToString();
                cartion = dt.Rows[i]["CARTON_NO"].ToString();
                pid = dt.Rows[i]["PRODUCTID"].ToString();
                ddate = dt.Rows[i]["LAST_UPDATE_DATE"].ToString();
                DataRow[] dr = dtm.Select("imei ='" + imei + "'");
                int iii = 0;
                if (dr.Length != 0)
                {
                    if (!dr[0].ItemArray[3].ToString().Equals(ddate))
                    {
                        AppPubLib AppPubLibPointer = new AppPubLib();
                        string re1 = AppPubLibPointer.SetBitProc(dtm.Rows[0]["Flag1"].ToString(), "1", "6", "Write", "1");
                        strsql = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=@F3,Flag1=@F5 where IMEI=@F4; ";
                        iii = dboWrite.ExecSQL(strsql, new string[] { "@F1", "@F2", "@F3", "@F4", "@F5" }, new object[] { cartion, pid, ddate, imei, re1 });
                        seccess += iii;
                    }
                    else
                    {
                        seccess3 += 1;
                    }
                }
                else
                {
                    strsql2 = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                    iii = dboWrite.ExecSQL(strsql2, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                    seccess2 += iii;
                }
                tmp = i + 1;
                count = 100 * tmp / sum;
                //count = i / dt.Rows.Count;
                
            }
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                imei = dt.Rows[i]["imei"].ToString();
                cartion = dt.Rows[i]["CARTON_NO"].ToString();
                pid = dt.Rows[i]["PRODUCTID"].ToString();
                ddate = dt.Rows[i]["LAST_UPDATE_DATE"].ToString();
                strsql = "insert into DelIMEIctl(Cartion,PID,Delivery_Date,IMEI) values(@F1,@F2,@F3,@F4); ";
                int iii = dboWrite.ExecSQL(strsql, new string[] { "@F1", "@F2", "@F3", "@F4" }, new object[] { cartion, pid, ddate, imei });
                if (iii == 0)
                {
                    string sqlm = "select * from DelIMEIctl where imei='" + imei + "'";
                    DataTable dtm = dboWrite.SelectSQLDT(sqlm);
                    if (dtm.Rows.Count > 0)
                    {
                        if (!dtm.Rows[0]["Delivery_Date"].ToString().Equals(ddate))
                        {
                            AppPubLib AppPubLibPointer = new AppPubLib();
                            string re1 = AppPubLibPointer.SetBitProc(dtm.Rows[0]["Flag1"].ToString(), "1", "6", "Write", "1");
                            strsql2 = "update DelIMEIctl set Cartion=@F1,PID=@F2,Delivery_Date=@F3,Flag1=@F5 where IMEI=@F4; ";
                            int ii = dboWrite.ExecSQL(strsql2, new string[] { "@F1", "@F2", "@F3", "@F4", "@F5" }, new object[] { cartion, pid, ddate, imei, re1 });
                            seccess2 += ii;
                        }
                        else
                        {
                            seccess3 += 1;
                        }
                    }
                    
                }
                seccess += iii;
                tmp = i+1 ;
                count = 100 * tmp / sum;
                //count = i / dt.Rows.Count;
            }
        }
        stopThread = true;
        Thread.Sleep(2000);
        //Lbs.InnerText = "執行成功！共" + sum + "條數據,插入:" + seccess + " 條，更新:" + seccess2 + " 條，已存在:" + seccess3 + " 條.";
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    string year = DDLYear.SelectedItem.Text;
    //    string mon = DDLMM.SelectedItem.Text;
    //    DateTime date = DateTime.ParseExact(year + mon + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
    //    string begintime = date.ToString("yyyyMMdd");
    //    string endtime = date.AddMonths(1).ToString("yyyyMMdd");
    //    string sql = "select * from DelIMEIctl where Delivery_Date>=convert(datetime,@F1,112) and Delivery_Date<convert(datetime,@F2,112)";
    //    DataTable dt = dboWrite.SelectSQLDT(sql, new string[] { "@F1", "@F2" }, new object[] {begintime,endtime });
    //    if (dt.Rows.Count > 0)
    //    {
    //        DataTable dtc1 = dt.Clone();
    //        int checkOK = 0;
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            string pid = dt.Rows[i]["PID"].ToString();
    //            string strsql1 = "select * from sfc.mes_assy_pid_join where main_id='" + pid.Trim() + "'";
    //            if (dboCheck.SelectSQLDT(strsql1).Rows.Count > 0) 
    //            {
    //                checkOK += 1;
    //            }
    //            else
    //            {
    //                DataRow dr = dt.Rows[i];
    //                dtc1.ImportRow(dr);
    //            }

    //        }
    //        if (checkOK == dt.Rows.Count)
    //        {
    //            LbMassage.Text = "sfc.mes_assy_pid_jion表數量同為:"+checkOK+"，可以刪除";
    //        }
    //        else
    //        {
    //            LbMassage.Text = "sfc.mes_assy_pid_jion表數量為：" + checkOK + ", \n";  
    //            LbMassage.Text += "備份表數量為：" + dt.Rows.Count + ", \n 數量不同 不能刪除";
    //        }
    //    }
    //    else
    //    {
    //        LbMassage.Text = "此月無備份數據！";
    //        return;
    //    }
    //}

    public void Xxx()
    {
        //for (int i = 0; i < 20; i++)
        //{
        
        while (!stopThread)
        {
           
            Thread.Sleep(1000);
           
            if (!sum.Equals(0))
            {
                
                count = 100 * tmp / sum;
            }
            else
            {
                count = 100;
            }
            if (count == 100)
            {
                stopThread = true;
            }
            
        }
        //}
        //ww.Visible = false;
       

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        bar1.Style["width"] = count + "%";
        text.InnerText = count + "%";
        
        aaa.InnerText = "後臺正在執行，共" + sum + "條，已執行" + tmp + "條";
        if (!stopThread)
        {
            ww.Style["display"] = "block";
        }
        else
        {
            //ww.Style["display"] = "none";
            //jz.Style["display"] = "none";
            //aaa.Style["color"] = "Red";
            aaa.InnerText = "執行成功！共" + sum + "條數據,插入:" + seccess + " 條，更新:" + seccess2 + " 條，已存在:" + seccess3 + " 條.";
            //if (sum.Equals(seccess + seccess2 + seccess3) && !sum.Equals(0))
            //{
            //    Lbs.InnerText = "執行成功！共" + sum + "條數據,插入:" + seccess + " 條，更新:" + seccess2 + " 條，已存在:" + seccess3 + " 條.";
            //}
        }
        
       
    }

}
