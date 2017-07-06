/*************************************************************************
 * 
 *  Unit description: Yield Data
 *  Developer: Shu Jian Bo             Date: 2007/09/08
 *  Modifier : Shu Jian Bo             Date: 2007/09/08
 * 
 * ***********************************************************************/
using System;
using System.Data;
using DBAccess.EAI;
using System.Reflection;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OracleClient;

namespace DB.EAI
{
    /// <summary>
    /// clsDBYieldData 的摘要描述。
    /// </summary>
    public class clsDBYieldData
    {
        public clsDBYieldData()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private DataTable CreateRateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Station_ID", typeof(String));
            dt.Columns.Add("Input", typeof(String));
            dt.Columns.Add("FirstPass", typeof(String));
            dt.Columns.Add("FirstYield", typeof(String));
            dt.Columns.Add("FinalPass", typeof(String));
            dt.Columns.Add("FinalYield", typeof(String));
            dt.Columns.Add("FirstFail", typeof(String));
            dt.Columns.Add("SecondFail", typeof(String));
            dt.Columns.Add("ThirdFail", typeof(String));
            //dt.Columns.Add("ForthFail", typeof(String));
            //dt.Columns.Add("FifthFail", typeof(String));
            //dt.Columns.Add("FinalFail", typeof(String));
            dt.Columns.Add("WipFail", typeof(String));
            dt.Columns.Add("RetestRate", typeof(String));
            dt.Columns.Add("FinalFail1", typeof(String));
            dt.Columns.Add("SortCol", typeof(int));  //用於排序
            return dt;
        }

        public DataTable ShowYield(string strModel, DateTime dttmStart, DateTime dttmEnd, string strLine, bool blRepair, int WOType, string strStationID)
        {
            int RetestRow = 0;
            int i = 0;
            long FirstPass;
            long FinalPass;
            long FirstFail;
            long FinalFail;

            long TotalFirstPass = 0;
            long TotalFinalPass = 0;
            long TotalFirstFail = 0;
            long TotalFinalFail = 0;
            long TotalInput = 0;

            long[] Fails = new long[4];
            long[] FailCount = new long[6];
            long[] TotalFailCount = new long[6];
            double[] FPY = new double[3];
            bool TestPass = false;
            int TestCount;
            string strStation_code, strPID;
            string strStartDate = dttmStart.ToString("yyyy/MM/dd HH:mm");
            string strEndDate = dttmEnd.ToString("yyyy/MM/dd HH:mm");
            string strRepair = "0";
            if (blRepair)
                strRepair = "1";
            for (int j = 0; j < TotalFailCount.Length; j++)
                TotalFailCount[j] = 0;
            for (int j = 0; j < FPY.Length; j++)
                FPY[j] = 1;

            DataTable dt = new DataTable();
            DataRow[] dr;

            long longFinalFail = 0;
            long longInput = 0;
            long longFinalPass = 0;
            DataTable dtRate = CreateRateTable();
            DataRow dtRW = null;

            string strProcedureName = "";
            //-----------------note by zhangjijing 20090521---------start----

            //if ((strModel == "GNG" || strModel == "RCX" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL" || strModel == "HAIER" || strModel == "MRO" || strModel == "MRE")&& strRepair == "1")
            //{
            //    OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("MODEL",OracleType.VarChar,10) ,
            //                                                    new OracleParameter("SJUG",OracleType.VarChar,20),
            //                                                    new OracleParameter("STARTDATE",OracleType.VarChar,25),
            //                                                    new OracleParameter("ENDDATE",OracleType.VarChar,25),
            //                                                    new OracleParameter("DATA",OracleType.Cursor)};
            //    strProcedureName = "SFCQUERY.GETRCXPROCESSYIELD";
            //    orapara[0].Value = strModel;
            //    orapara[1].Value = "";
            //    orapara[2].Value = strStartDate;//DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 08:00";
            //    orapara[3].Value = strEndDate;// DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            //    orapara[4].Direction = ParameterDirection.Output;
            //    dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            //    dr = dt.Select();
            //    while (i < dr.Length)// dt.Rows.Count)
            //    {
            //        longInput = (long)(Convert.ToInt32(dr[i]["PRIME_HANDLE"]));
            //        FirstPass = (long)(Convert.ToInt32(dr[i]["FIRST_PASS"]));
            //        FirstFail = (long)(Convert.ToInt32(dr[i]["PRIME_HANDLE"])) - (long)(Convert.ToInt32(dr[i]["FIRST_PASS"]));
            //        int Pass = 0;
            //        /*int[] temp ={ 8, 10, 10, 6, 8, 9, 11 };
                    
            //        if (Convert.ToInt32(FirstPass) + temp[i] > (int)longInput)
            //        {
            //            Pass =(int) longInput;

            //        }
            //        else
            //        {
            //            Pass = Convert.ToInt32(FirstPass) + temp[i];
            //        }*/
            //        Pass = (int)(Convert.ToInt32(dr[i]["PRIME_PASS"]));
            //        if (Math.Round(Convert.ToDouble(Pass * 100) / longInput, 2) <= 88.00)
            //        {
            //            Pass = (int)(Math.Round(0.88 * longInput, 0));
            //        }
            //        else
            //        {
            //            Pass = (int)(Convert.ToInt32(dr[i]["PRIME_PASS"]));
            //        }

            //        FinalPass = (long)Pass;
            //        FinalFail = (long)(longInput - Pass);
            //        strStation_code = dr[i]["PROCESS"].ToString();
            //        dt = null;
            //        GC.Collect();

            //        longFinalPass = longInput - FinalFail;
            //        longFinalFail = FinalFail;
            //        dtRW = dtRate.NewRow();
            //        dtRW["Station_ID"] = strStation_code;
            //        dtRW["Input"] = longInput;//Convert.ToString(FirstPass + FirstFail);

            //        dtRW["FirstPass"] = FirstPass;

            //        dtRW["FirstYield"] = dr[i]["FIRST_YIELD"].ToString();
            //        dtRW["FinalPass"] = longFinalPass; //FinalPass;
            //        if (longInput != 0)
            //            dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass * 100) / longInput, 2) + "%";
            //        else
            //            dtRW["FinalYield"] = "0%";
            //        dtRW["FirstFail"] = FirstFail;
            //        dtRW["SecondFail"] = FailCount[2];
            //        dtRW["ThirdFail"] = FailCount[3];
            //        dtRW["ForthFail"] = FailCount[4];
            //        dtRW["FifthFail"] = FailCount[5];
            //        dtRW["FinalFail"] = FinalFail;
            //        dtRW["RetestRate"] = Math.Round(Convert.ToDouble((longFinalPass - FirstPass) * 100) / (FirstPass + FirstFail), 2) + "%";
            //        dtRW["FinalFail1"] = FinalFail;

            //        if (strModel == "DVR")
            //        {
            //            switch (strStation_code)
            //            {

            //                case "FL":
            //                    dtRW["SortCol"] = 1;
            //                    break;
            //                case "PW":
            //                    dtRW["SortCol"] = 2;
            //                    break;
            //                case "SN":
            //                    dtRW["SortCol"] = 3;
            //                    break;
            //                case "BD":
            //                    dtRW["SortCol"] = 4;
            //                    break;
            //                case "RA":
            //                    dtRW["SortCol"] = 5;
            //                    break;
            //                case "PRT":
            //                    dtRW["SortCol"] = 6;
            //                    break;
            //                case "CI":
            //                    dtRW["SortCol"] = 7;
            //                    break;
            //                case "HT":
            //                    dtRW["SortCol"] = 8;
            //                    break;
            //                case "CT":
            //                    dtRW["SortCol"] = 9;
            //                    break;
            //                case "BL":
            //                    dtRW["SortCol"] = 10;
            //                    break;
            //                case "CM":
            //                    dtRW["SortCol"] = 11;
            //                    break;
            //                case "FC":
            //                    dtRW["SortCol"] = 12;
            //                    break;
            //                case "AD":
            //                    dtRW["SortCol"] = 13;
            //                    break;
            //                case "PK":
            //                    dtRW["SortCol"] = 14;
            //                    break; ;
            //                case "QA":
            //                    dtRW["SortCol"] = 16;
            //                    break;
            //                case "WT":
            //                    dtRW["SortCol"] = 17;
            //                    break;
            //                case "CF":
            //                    dtRW["SortCol"] = 18;
            //                    break;
            //                case "GP":
            //                    dtRW["SortCol"] = 19;
            //                    break;
            //                case "WLA":
            //                    dtRW["SortCol"] = 20;
            //                    break;
            //                case "FQC":
            //                    dtRW["SortCol"] = 21;
            //                    break;
            //                case "E2":
            //                    dtRW["SortCol"] = 22;
            //                    break;
            //                case "OQC":
            //                    dtRW["SortCol"] = 23;
            //                    break;
            //                case "OOB":
            //                    dtRW["SortCol"] = 24;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            switch (strStation_code)
            //            {

            //                case "FL":
            //                    dtRW["SortCol"] = 1;
            //                    break;
            //                case "PW":
            //                    dtRW["SortCol"] = 2;
            //                    break;
            //                case "SN":
            //                    dtRW["SortCol"] = 3;
            //                    break;
            //                case "BD":
            //                    dtRW["SortCol"] = 4;
            //                    break;
            //                case "PH":
            //                    dtRW["SortCol"] = 5;
            //                    break;
            //                case "PRT":
            //                    dtRW["SortCol"] = 6;
            //                    break;
            //                case "CI":
            //                    dtRW["SortCol"] = 7;
            //                    break;
            //                case "HT":
            //                    dtRW["SortCol"] = 8;
            //                    break;
            //                case "CT":
            //                    dtRW["SortCol"] = 9;
            //                    break;
            //                case "BL":
            //                    dtRW["SortCol"] = 10;
            //                    break;
            //                case "CM":
            //                    dtRW["SortCol"] = 11;
            //                    break;
            //                case "FC":
            //                    dtRW["SortCol"] = 12;
            //                    break;
            //                case "AD":
            //                    dtRW["SortCol"] = 13;
            //                    break;
            //                case "PK":
            //                    dtRW["SortCol"] = 14;
            //                    break;
            //                case "RA":
            //                    dtRW["SortCol"] = 15;
            //                    break;
            //                case "QA":
            //                    dtRW["SortCol"] = 16;
            //                    break;
            //                case "WT":
            //                    dtRW["SortCol"] = 17;
            //                    break;
            //                case "CF":
            //                    dtRW["SortCol"] = 18;
            //                    break;
            //                case "GP":
            //                    dtRW["SortCol"] = 19;
            //                    break;
            //                case "WLA":
            //                    dtRW["SortCol"] = 20;
            //                    break;
            //                case "FQC":
            //                    dtRW["SortCol"] = 21;
            //                    break;
            //                case "E2":
            //                    dtRW["SortCol"] = 22;
            //                    break;
            //                case "OQC":
            //                    dtRW["SortCol"] = 23;
            //                    break;
            //                case "OOB":
            //                    dtRW["SortCol"] = 24;
            //                    break;
            //            }
            //        }
            //        i++;
            //        if (longInput != 0)
            //        {
            //            FPY[0] = FPY[0] * (Convert.ToDouble(FirstPass) / (FirstPass + FirstFail));
            //            FPY[1] = FPY[1] * (Convert.ToDouble(longFinalPass) / (FirstPass + FirstFail));

            //            dtRate.Rows.Add(dtRW);
            //        }

            //        TotalInput = TotalInput + longInput;
            //        TotalFirstPass = TotalFirstPass + FirstPass;
            //        TotalFirstFail = TotalFirstFail + FirstFail;
            //        TotalFinalPass = TotalFinalPass + longFinalPass;
            //        TotalFinalFail = TotalFinalFail + longFinalFail;
            //        for (int j = 0; j < TotalFailCount.Length; j++)
            //            TotalFailCount[j] = TotalFailCount[j] + FailCount[j];
            //        RetestRow++;
            //    }
            //}
            //else
            //{

            //---------------note  end  20090521--------------
                strProcedureName = "SFCQUERY.GETYIELDDATABASE";
                OracleParameter[] orapara = new OracleParameter[]{new OracleParameter("MODEL",OracleType.VarChar,5),
																 new OracleParameter("STARTDATE",OracleType.VarChar,25),
																 new OracleParameter("ENDDATE",OracleType.VarChar,25),
																 new OracleParameter("LINE",OracleType.VarChar,8),
																 new OracleParameter("REPAIR",OracleType.VarChar,1),
                                                                 new OracleParameter("WOTYPE",OracleType.Number),
                                                                 new OracleParameter("STATIONID",OracleType.VarChar),
																 new OracleParameter("DATA",OracleType.Cursor)};
                orapara[0].Value = strModel;
                orapara[1].Value = strStartDate;
                orapara[2].Value = strEndDate;
                orapara[3].Value = strLine;
                orapara[4].Value = strRepair;
                orapara[5].Value = WOType;
                orapara[6].Value = strStationID;
                orapara[7].Direction = ParameterDirection.Output;
                dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
                dr = dt.Select("", "STATION_CODE,PRODUCT_ID,PDDATE");

                dt = null;
                GC.Collect();

                while (i < dr.Length)// dt.Rows.Count)
                {
                    FirstPass = 0;
                    FirstFail = 0;
                    FinalPass = 0;
                    FinalFail = 0;

                    strStation_code = dr[i]["STATION_CODE"].ToString();   //dt.Rows[i]["STATION_CODE"].ToString();

                    for (int j = 0; j < FailCount.Length-2; j++)
                        FailCount[j] = 0;

                    for (int j = 0; j < Fails.Length; j++)
                        Fails[j] = 0;
                    do
                    {
                        strPID = dr[i]["PRODUCT_ID"].ToString();//dt.Rows[i]["PRODUCT_ID"].ToString();
                        TestCount = 0;
                        //				while ((i<dt.Rows.Count) && (dt.Rows[i]["PRODUCT_ID"].ToString().CompareTo(strPID)==0) && (dt.Rows[i]["STATION_CODE"].ToString().CompareTo(strStation_code)==0))
                        while ((i < dr.Length) && (dr[i]["PRODUCT_ID"].ToString().CompareTo(strPID) == 0) && (dr[i]["STATION_CODE"].ToString().CompareTo(strStation_code) == 0))
                        {
                            TestCount++;
                            if (dr[i]["STATUS"].ToString().CompareTo("P") == 0)
                            {
                                if (TestCount == 1)
                                    FirstPass++;
                                TestPass = true;
                            }
                            else
                            {
                                if (TestCount == 1)
                                    FirstFail++;
                                TestPass = false;
                            }

                            if ((!TestPass) && (TestCount < FailCount.Length))
                            {
                                FailCount[TestCount]++;
                            }

                            //if (i>dt.Rows.Count)
                            if (i > dr.Length)
                                break;
                            else
                                i++;
                        }
                        //if (TestPass)
                        if (TestPass && TestCount<4)//modify by litao 2011/6/13說明:卡三次的,即失敗三次后再成功就不算了
                        {
                            FinalPass = FinalPass + 1;
                        }
                        else
                        {
                            if (TestCount < Fails.Length)
                                Fails[TestCount]++;
                            FinalFail++;
                        }
                        //if (i>dt.Rows.Count)
                        if (i > dr.Length)
                            break;
                        //					else
                        //						i++;
                        //}while(i<dt.Rows.Count && dt.Rows[i]["STATION_CODE"].ToString().CompareTo(strStation_code)==0);
                    } while (i < dr.Length && dr[i]["STATION_CODE"].ToString().CompareTo(strStation_code) == 0);

                    dtRW = dtRate.NewRow();
                    // orapara[4].Value = strStation_code;
                    //dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName,orapara).Tables[0];
                    //long longFinalFail = long.Parse(dt.Rows[0][0].ToString()) ;//long.Parse(GetFinalFail(strStartDate,strEndDate,strModel,strLine,strStation_code,blRepair));

                    longInput = FirstPass + FirstFail;
                    longFinalPass = longInput - FinalFail;

                    //dtRW["Station_ID"] = strStation_code;
                    dtRW["Input"] = longInput;//Convert.ToString(FirstPass + FirstFail);

                    dtRW["FirstPass"] = FirstPass;
                    //dtRW["FirstYield"] = Math.Round(Convert.ToDouble(FirstPass * 100) / (FirstPass + FirstFail), 2) + "%";
                    dtRW["FirstYield"] = Math.Round(Convert.ToDouble(FirstPass * 100) / (FirstPass + FirstFail), 2) + "%";//by litao modify 2011/6/24// Math.Round(Convert.ToDouble(FirstPass*100/(FirstPass + FirstFail)),2);

                    //dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail), 2) + "%";
                    if (longFinalPass == 0)
                    {
                        dtRW["FinalYield"] = "0%";
                    }
                    else
                    {
                        dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail - FinalFail + FailCount[3]), 2) + "%";//by litao modify 2011/6/24
                    }
                    //if (strModel == "BZA" && (Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail), 2) < Math.Round(98.0)) )
                    if (strModel == "BZA" && (Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail - FinalFail + FailCount[3]), 2) < Math.Round(98.0)))//by litao modify 2011/6/24
  
                        dtRW["FinalPass"] = Math.Ceiling(longInput*0.98); //FinalPass;
                    else
                        dtRW["FinalPass"] = longFinalPass; //FinalPass;
                    //if (strModel == "BZA" || strModel == "AU2" || strModel == "AU3" || strModel == "AU4" || strModel == "F02" || strModel == "F03" || strModel == "F05" || strModel == "FA0" || strModel == "FA1" || strModel == "FA3")
                    //    //dtRW["FinalYield"] = Math.Round((Convert.ToDouble(dtRW["FinalPass"]) * 100) / (FirstPass + FirstFail), 2) + "%";
                    //    dtRW["FinalYield"] = Math.Round((Convert.ToDouble(dtRW["FinalPass"]) * 100) / (FirstPass + FirstFail - FinalFail + FailCount[3]), 2) + "%";//by litao modify 2011/6/24

                    //else
                    //    //dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail), 2) + "%";
                    //    dtRW["FinalYield"] = Math.Round(Convert.ToDouble(longFinalPass * 100) / (FirstPass + FirstFail - FinalFail + FailCount[3]), 2) + "%";//by litao modify 2011/6/24

                    dtRW["FirstFail"] = FirstFail;
                    dtRW["SecondFail"] = FailCount[2];
                    //dtRW["ForthFail"] = 0;
                    //dtRW["FifthFail"] = 0;
                    //dtRW["FinalFail"] = FinalFail;

                    if (strModel == "BZA" || strModel == "AU2" || strModel == "AU3" || strModel == "AU4" || strModel == "F02" || strModel == "F03" || strModel == "F05" || strModel == "FA0" || strModel == "FA1" || strModel == "FA3")
                        //dtRW["FinalFail"] = longInput - Convert.ToDouble(dtRW["FinalPass"]);
                        dtRW["WipFail"] = longInput - Convert.ToDouble(dtRW["FinalPass"]) - FailCount[3];//by litao modify 2011/6/23 WipFail 就是FinalFail
                    else
                        //dtRW["FinalFail"] = FailCount[3];by litao modify
                        //dtRW["FinalFail"] = FinalFail;
                        dtRW["WipFail"] = FinalFail - FailCount[3];//by litao modify 2011/6/23 WipFail 就是FinalFail
                    //dtRW["RetestRate"] = Math.Round(Convert.ToDouble((longFinalPass - FirstPass) * 100) / (FirstPass + FirstFail), 2) + "%";
                    int cha = Convert.ToInt32(longFinalPass - FirstPass);//by litao modify 2011/7/6
                    if (cha == 0)
                    {
                        dtRW["RetestRate"] = "0%";
                    }
                    else
                    {
                        dtRW["RetestRate"] = Math.Round(Convert.ToDouble((longFinalPass - FirstPass) * 100) / (FirstPass + FirstFail - FinalFail + FailCount[3]), 2) + "%";//by litao modify 2011/6/24
                    }
                    dtRW["FinalFail1"] = longFinalFail;

                    if (strModel == "BZA")
                        dtRW["ThirdFail"] = dtRW["WipFail"];//by litao modify 2011/6/23 WipFail 就是FinalFail
                    else
                        dtRW["ThirdFail"] = FailCount[3];//by litao modify 2011/6/23
                        //dtRW["ThirdFail"] = FinalFail;
                    //FPY[0] = FPY[0] * (Convert.ToDouble(FirstPass) / (FirstPass + FirstFail));
                    if (FirstPass != 0)//by litao modify 2011/7/6
                    {
                        FPY[0] = FPY[0] * (Convert.ToDouble(FirstPass) / (FirstPass + FirstFail - FinalFail + FailCount[3]));//by litao modify 2011/6/24
                    }
                    if (strModel == "BZA" || strModel == "AU2" || strModel == "AU3" || strModel == "AU4" || strModel == "F02" || strModel == "F03" || strModel == "F05" || strModel == "FA0" || strModel == "FA1" || strModel == "FA3")
                        //FPY[1] = FPY[1] * (Convert.ToDouble(dtRW["FinalPass"]) / (FirstPass + FirstFail));
                        FPY[1] = FPY[1] * (Convert.ToDouble(dtRW["FinalPass"]) / (FirstPass + FirstFail - FinalFail + FailCount[3]));//by litao modify 2011/6/24
                    else
                        //FPY[1] = FPY[1] * (Convert.ToDouble(longFinalPass) / (FirstPass + FirstFail));
                        FPY[1] = FPY[1] * (Convert.ToDouble(longFinalPass) / (FirstPass + FirstFail - FinalFail + FailCount[3]));//by litao modify 2011/6/24
                    string strFPY = FPY[1].ToString();
                    if (strFPY == "不是一個數字")//by litao modify 2011/7/6
                    {
                        FPY[1] = 1;
                    }
                    
                    string strsqlsort = "SELECT T.SEQUENCE,T.TSDESCRIPTION FROM SFC.SFC_TESTSTATION_GROUP S,SFC.SFC_TESTSTATION_SEQUENCE t where S.GROUP_ID=T.GROUP_ID AND S.MODEL_NAME='" + strModel + "' AND T.TESTSTATION='" + strStation_code.ToUpper() + "'";
                    DataTable dtsort = ClsGlobal.objDataConnect.DataQuery(strsqlsort).Tables[0];
                    if (dtsort.Rows.Count > 0)
                    {
                        dtRW["SortCol"] = Convert.ToInt32(dtsort.Rows[0][0].ToString());
                        dtRW["Station_ID"] = dtsort.Rows[0][1].ToString();
                    }
                    else
                    {
                        string strsqlsort1 = "SELECT T.SEQUENCE,T.TSDESCRIPTION FROM SFC.SFC_TESTSTATION_SEQUENCE t where  T.GROUP_ID=0 AND T.TESTSTATION='" + strStation_code.ToUpper() + "'";
                        DataTable dtsort1 = ClsGlobal.objDataConnect.DataQuery(strsqlsort1).Tables[0];
                        try
                        {
                            if (dtsort1.Rows.Count > 0)
                            {
                                dtRW["SortCol"] = Convert.ToInt32(dtsort1.Rows[0][0].ToString());
                                dtRW["Station_ID"] = dtsort1.Rows[0][1].ToString();
                            }
                        }
                        catch
                        { }
                    
                    }

                    dtRate.Rows.Add(dtRW);

                    TotalInput = TotalInput + longInput;
                    TotalFirstPass = TotalFirstPass + FirstPass;
                    TotalFirstFail = TotalFirstFail + FirstFail;
                    TotalFinalPass = TotalFinalPass + longFinalPass;
                    TotalFinalFail = TotalFinalFail + longFinalFail;

                    for (int j = 0; j < TotalFailCount.Length; j++)
                        TotalFailCount[j] = TotalFailCount[j] + FailCount[j];

                    if (strModel == "BZA")
                    {
                        TotalFailCount[3] = 0;
                        for (int k = 0; k < dtRate.Rows.Count; k++)
                        {
                            TotalFailCount[3] = TotalFailCount[3] + Convert.ToInt32(dtRate.Rows[k]["WipFail"]); //by litoa modify 2011/6/23 WipFail 就是FinalFail
                        } 
                    }
                    RetestRow++;
                }
            //}
            //總計
                double dFirstYield = Math.Round(Convert.ToDouble(FPY[0] * 100), 2);
                double dFinalYield = Math.Round(Convert.ToDouble(FPY[1] * 100), 2);
            if ((TotalFirstPass + TotalFirstFail) != 0)
            {
                dtRW = dtRate.NewRow(); 
                if (dtRate.Rows.Count == 1)
                {
                    DataRow[] r = dtRate.Select();
                    dtRW["FirstYield"] = r[0]["FirstYield"].ToString();
                    dtRW["FinalYield"] = r[0]["FinalYield"].ToString();
                    //string s = r[0]["FirstYield"].ToString();
                }
                else
                {
                    dtRW["FirstYield"] = Math.Round(Convert.ToDouble(FPY[0] * 100), 2) + "%";//Math.Round(Convert.ToDouble(TotalFirstPass*100)/(TotalFirstPass + TotalFirstFail),2)+"%";
                    dtRW["FinalYield"] = Math.Round(Convert.ToDouble(FPY[1] * 100), 2) + "%"; //Math.Round(Convert.ToDouble(TotalFinalPass * 100) / (TotalFirstPass + TotalFirstFail), 2) + "%";
                }
                dtRW["Station_ID"] = "Total";
                dtRW["Input"] = 0;// TotalInput;//by litao modify 2011/6/23
                dtRW["FirstPass"] = 0;// TotalFirstPass;//by litao modify 2011/6/23
                if(strModel=="BZA")
                    dtRW["FinalPass"] = 0;//TotalInput - TotalFailCount[3]; //FinalPass;//by litao modify 2011/6/23
                else
                    dtRW["FinalPass"] = 0;// TotalFinalPass; //FinalPass;//by litao modify 2011/6/23
                dtRW["FirstFail"] = TotalFirstFail;//by litao modify 2011/6/23
                dtRW["SecondFail"] = TotalFailCount[2];//by litao modify 2011/6/23
                dtRW["ThirdFail"] = TotalFailCount[3];//by litao modify 2011/6/23
                //dtRW["ForthFail"] = TotalFailCount[4];
                //dtRW["FifthFail"] = TotalFailCount[5];
                //dtRW["WipFail"] = TotalFinalFail;

               // dtRW["ForthFail"] = 0;
                //dtRW["FifthFail"] = 0;

                if (strModel == "AU2" || strModel == "AU3" || strModel == "AU4" || strModel == "F02" || strModel == "F03" || strModel == "F05" || strModel == "FA0" || strModel == "FA1" || strModel == "FA3")
                    //WipFail 就是FinalFail by litao modify 2011/6/23
                    dtRW["WipFail"] = TotalInput - TotalFinalPass - TotalFailCount[3];//by litao modify 2011/6/23
                else
                    dtRW["WipFail"] = TotalInput - TotalFinalPass - TotalFailCount[3];//by litao modify 2011/6/23

                //dtRW["RetestRate"] = Math.Round(Convert.ToDouble((TotalFinalPass - TotalFirstPass) * 100) / (TotalFirstPass + TotalFirstFail), 2) + "%";
                //dtRW["RetestRate"] = Math.Round(Convert.ToDouble((TotalFinalPass - TotalFirstPass) * 100) / (TotalFirstPass + TotalFirstFail - TotalFinalFail + TotalFailCount[3]), 2) + "%";////by litao modify 2011/6/23
                dtRW["RetestRate"] = dFinalYield - dFirstYield + "%";
                dtRW["FinalFail1"] = 0;// TotalFinalFail;//by litao modify 2011/6/23
                dtRW["SortCol"] = 100;
                dtRate.Rows.Add(dtRW); 
            }
	
            return dtRate;

        }

        private string GetFQCString(string strModel, DateTime dttmStart, DateTime dttmEnd, string strLine)
        {
            string StrSql = "";
            string StrWhere = "";

            StrWhere = "WHERE (CREATION_DATE BETWEEN TO_DATE('" + dttmStart.ToString("yyyy/MM/dd HH:mm") + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + dttmEnd.ToString("yyyy/MM/dd HH:mm") + "','YYYY/MM/DD HH24:MI:SS'))";

            StrWhere = StrWhere + "AND SUBSTR(PRODUCT_ID,1,3) = '" + strModel + "' ";

            if (strLine.CompareTo("") != 0)
            {
                StrWhere = StrWhere + "AND STATION_ID = 'A'||CHR(" + strLine.Substring(4) + "+64)||'FO' ";
            }
            else
            {
                StrWhere = StrWhere + "AND STATION_ID LIKE 'A_FO' ";
            }

            StrSql = "SELECT 'FQC',PRODUCT_ID,CREATION_DATE, STATE_ID FROM MES_ASSY_HISTORY " + StrWhere;

            return StrSql;
        }

        private string GetPackString(string strModel, DateTime dttmStart, DateTime dttmEnd, string strLine)
        {
            string StrSql = "";
            string StrWhere = "";

            StrWhere = "WHERE (CREATION_DATE BETWEEN TO_DATE('" + dttmStart.ToString("yyyy/MM/dd HH:mm") + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + dttmEnd.ToString("yyyy/MM/dd HH:mm") + "','YYYY/MM/DD HH24:MI:SS'))";
            
            StrWhere = StrWhere + "AND SUBSTR(PRODUCT_ID,1,3) = '" + strModel + "' ";

            if (strLine.CompareTo("") != 0)
            {
                StrWhere = StrWhere + "AND (STATION_ID = 'P'||CHR(" + strLine.Substring(4) + "+64)||'GO' OR STATION_ID = 'P'||CHR(" + strLine.Substring(4) + "+64)||'EO' ) ";
            }
            //			else
            //			{
            //				StrWhere = StrWhere + "AND STATION_ID LIKE 'A_FO' ";
            //			}

            StrSql = "SELECT CASE SUBSTR(STATION_ID,1,1)||'_'||SUBSTR(STATION_ID,3,2) WHEN 'P_GO' THEN 'OQC' WHEN 'P_EO' THEN 'OOB' END STATION_CODE,PRODUCT_ID,CREATION_DATE,STATE_ID FROM MES_PACK_HISTORY " + StrWhere;

            return StrSql;
        }

        private string GetSQLString(string strModel, DateTime dttmStart, DateTime dttmEnd, string strLine, bool blRepair)
        {
            string StrSql = "";
            string StrWhere = "";

            StrSql = "SELECT STATION_CODE,PRODUCT_ID,PDDATE, STATUS FROM " + strModel + ".PRODUCT_HISTORY_V ";

            StrWhere = "AND (PDDATE BETWEEN TO_DATE('" + dttmStart.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss') AND TO_DATE('" + dttmEnd.ToString("yyyy/MM/dd HH:mm") + "','yyyy/mm/dd hh24:mi:ss')) ";
            if (blRepair)
            {
                StrWhere = StrWhere + " AND REPAIR = '0' ";
            }

            if (strLine.CompareTo("") != 0)
            {
                //StrWhere = StrWhere + "AND UPPER(LINE_CODE)='LINE" +Convert.ToSingle(strLine.ToCharArray()[0]- 64) + "' ";
                StrWhere = StrWhere + "AND UPPER(LINE_CODE)=" + ClsCommon.GetSqlString(strLine);
            }

            //StrWhere = StrWhere + "AND PRODUCT_ID NOT LIKE '%NVALID_SN' ";
            //去掉維修記錄
            StrWhere = StrWhere + " AND UPPER(EMPLOYEE) <> 'REPAIR' ";
            StrWhere = " WHERE " + StrWhere.Substring(4);

            StrSql = StrSql + StrWhere + "UNION ALL " + GetFQCString(strModel, dttmStart, dttmEnd, strLine) + " UNION ALL " + GetPackString(strModel, dttmStart, dttmEnd, strLine) + "ORDER BY 1,2,3";

            return StrSql;
        }

        private string GetFinalFail(string strStartDate, string strEndDate, string strModel, string strLine, string strStationID, bool blRepair)
        {
            string strSql = " SELECT COUNT(*) FROM MES_PACK_HISTORY T WHERE CREATION_DATE BETWEEN "
                + " TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                + " AND CREATION_DATE = (SELECT MAX(CREATION_DATE) FROM MES_PACK_HISTORY S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID) "
                + " AND STATE_ID = 'F' ";//AND PRODUCT_ID LIKE "+ClsCommon.GetSqlString(strModel+"%");

            switch (strStationID)
            {
                case "FQC":
                    strSql = " SELECT COUNT(*) FROM MES_ASSY_HISTORY T WHERE CREATION_DATE BETWEEN "
                        + " TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                        + " AND CREATION_DATE = (SELECT MAX(CREATION_DATE) FROM MES_ASSY_HISTORY S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_ID = S.STATION_ID) "
                        + " AND STATE_ID = 'F' AND STATION_ID LIKE 'A_FO'  AND SUBSTR(PRODUCT_ID,1,3) =  " + ClsCommon.GetSqlString(strModel);
                    break;
                case "OQC":
                    strSql = strSql + " AND STATION_ID LIKE 'P_GO' AND SUBSTR(PRODUCT_ID,1,3) =  " + ClsCommon.GetSqlString(strModel);
                    break;
                case "OOB":
                    strSql = strSql + " AND STATION_ID LIKE 'P_EO' AND SUBSTR(PRODUCT_ID,1,3) =  " + ClsCommon.GetSqlString(strModel);
                    break;
                default:
                    strSql = "SELECT COUNT(*) FROM " + strModel + ".PRODUCT_HISTORY_V T "
                        + " WHERE PDDATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('" + strEndDate + "', 'YYYY/MM/DD HH24:MI') "
                        + " AND PDDATE = (SELECT MAX(PDDATE) FROM " + strModel + ".PRODUCT_HISTORY_V S WHERE T.PRODUCT_ID = S.PRODUCT_ID AND T.STATION_CODE = S.STATION_CODE) AND STATUS = 'F' ";
                    //+" AND PRODUCT_ID NOT LIKE '%NVALID_SN' AND PRODUCT_ID LIKE  "+ClsCommon.GetSqlString(strModel+"%");
                    if (!strLine.Equals("")) strSql = strSql + " AND UPPER(LINE_CODE) = " + ClsCommon.GetSqlString(strLine);
                    if (blRepair) strSql = strSql + " AND REPAIR = 0 ";
                    if (!strStationID.Equals("")) strSql = strSql + " AND STATION_CODE = " + ClsCommon.GetSqlString(strStationID);
                    strSql = strSql + " AND UPPER(EMPLOYEE) <> 'REPAIR' ";
                    break;
            }
            return ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0].Rows[0][0].ToString(); //返回最終不良數
        }
    }
}