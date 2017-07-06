/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/08
 *  Modifier : Shu Jian Bo             Date: 2007/09/08
 * 
 * ***********************************************************************/
using System;
using System.Data;
using DBAccess.EAI;
using ChartDirector;

namespace DB.EAI
{
	/// <summary>
	/// clsDBTopNDefect 的摘要描述。
	/// </summary>
	public class clsDBTopNDefect
	{
		public clsDBTopNDefect()
		{
			//
			// TODO: 在此加入建構函式的程式碼
			//
		}


		private static string GetDefectTotal(string strStartDate,string strEndDate,string strLine,string strModel,string strWO,int intType,int intRegion,int intStatisticsType,int intStationIndex,bool blRepair)
		{
			string strTemp = "";
			switch(intRegion)
			{
				case 0:
					break;
				case 1:
                    //Tabel have deleted.
                    //strTemp = " UNION ALL SELECT DISTINCT PRODUCT_ID,'Other' STATION_CODE,DEFECT_CODE ERROR_CODE,ERROR_MESSAGE ERROR_MSG FROM MES_REPAIR_UNTEST T "
                    //    +" WHERE CREATION_DATE BETWEEN TO_DATE('"+strStartDate+"', 'yyyy/mm/dd hh24:mi') AND TO_DATE('"
                    //    +strEndDate+"', 'yyyy/mm/dd hh24:mi')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";
					break;
				case 2:
					strTemp = " UNION ALL SELECT DISTINCT PRODUCT_ID,STATION_ID STATION_CODE,DEFECT_CODE ERROR_CODE,'' ERROR_MSG FROM MES_PRODUCT_FAIL_HISTORY  T "
						+" WHERE CREATION_DATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"
						+ strEndDate +"', 'YYYY/MM/DD HH24:MI')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";
					switch (intStationIndex)
					{
						case 1:
							strTemp = strTemp + " AND STATION_ID IN ('FQC')";
							break;
						case 2:
							strTemp = strTemp + " AND STATION_ID IN ('OQC')";
							break;
						case 3:
							strTemp = strTemp + " AND STATION_ID IN ('OOB')";
							break;
						default:
							strTemp = strTemp + " AND STATION_ID IN ('FQC','OQC','OOB')";
							break;
					}
					break;
			}

			string strSql = " SELECT COUNT(*) FROM (SELECT /*+RULE*/DISTINCT PRODUCT_ID,STATION_CODE,ERROR_CODE,ERROR_MSG FROM "
				+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+".PRODUCT_HISTORY_V A "
				+GetWhereStr(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair,true)
				+strTemp+" ) ";
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
			return dt.Rows[0][0].ToString();
		}
		public static DataTable GetPCBAData(string strStartDate,string strEndDate,string strLine,string strTopN,int intRegion,int intType,int intStatisticsType,int intStationIndex,
			string strModel,string strWO,bool blRepair,out string ResultMsg)
		{		
			//string strTemp = "";
			string strSql = "SELECT * FROM (SELECT DEFECT_CODE,DEFECT_DESC_CHT,QTY,CC,TO_CHAR(ROUND(QTY / CC * 100, 3),'990.000') || '%' RATE,STATION_CODE "
				+" FROM (SELECT ERROR_CODE DEFECT_CODE,ERROR_MSG DEFECT_DESC_CHT,STATION_CODE,COUNT(*)QTY,"+GetDefectTotal(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair)
				+" CC FROM ( SELECT  PRODUCT_ID,STATION_CODE,ERROR_CODE,ERROR_MSG FROM "
				+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+".PRODUCT_HISTORY_V A "+GetWhereStr(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair,true)
				+") GROUP BY ERROR_CODE,ERROR_MSG,STATION_CODE  ORDER BY COUNT(*) DESC))  ";
			if (!strTopN.Equals(""))
				strSql = strSql+" WHERE ROWNUM<="+strTopN;

			DataTable dt = null ;
			try
			{
				dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
				ResultMsg = "";
			}
			catch (Exception e)
			{
				ResultMsg =  e.Message;
			}
			return dt;
		}

		public static DataTable GetAssyData(string strStartDate,string strEndDate,string strLine,string strTopN,int intRegion,int intType,int intStatisticsType,int intStationIndex,
			string strModel,string strWO,bool blRepair,out string ResultMsg)
		{
            //string strTemp = " UNION ALL SELECT DISTINCT PRODUCT_ID,'Other' STATION_CODE,DEFECT_CODE ERROR_CODE,ERROR_MESSAGE ERROR_MSG FROM MES_REPAIR_UNTEST T "
            //    +" WHERE CREATION_DATE BETWEEN TO_DATE('"+strStartDate+"', 'yyyy/mm/dd hh24:mi') AND TO_DATE('"
            //    +strEndDate+"', 'yyyy/mm/dd hh24:mi')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";
			string strSql = "SELECT * FROM (SELECT DEFECT_CODE,DEFECT_DESC_CHT,QTY,CC,TO_CHAR(ROUND(QTY / CC * 100, 3),'990.000') || '%' RATE,STATION_CODE "
				+" FROM (SELECT ERROR_CODE DEFECT_CODE,ERROR_MSG DEFECT_DESC_CHT,STATION_CODE,COUNT(*)QTY,"+GetDefectTotal(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair)
				+" CC FROM ( SELECT /*+RULE*/DISTINCT PRODUCT_ID,STATION_CODE,ERROR_CODE,ERROR_MSG FROM "
				+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+".PRODUCT_HISTORY_V  A "+GetWhereStr(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair,true)//+strTemp				
				+" ) GROUP BY ERROR_CODE,ERROR_MSG,STATION_CODE "
				+" ORDER BY COUNT(*) DESC))  ";
			if (!strTopN.Equals(""))
				strSql = strSql+" WHERE ROWNUM<="+strTopN;

			//strTemp = "";
			DataTable dt = null;
			try
			{
				dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
				ResultMsg = "";
			}
			catch (Exception e)
			{
				ResultMsg = e.Message;
				//Page.RegisterStartupScript("Error","<script language=javascript>alert('"+e.ToString()+"');</script>");
				//return;
			}
			return dt;
		}

		public static DataTable GetPACKData(string strStartDate,string strEndDate,string strLine,string strTopN,int intRegion,int intType,int intStatisticsType,int intStationIndex,
			string strModel,string strWO,bool blRepair,out string ResultMsg)
		{
			string strTemp = " UNION ALL SELECT DISTINCT PRODUCT_ID,STATION_ID STATION_CODE,DEFECT_CODE ERROR_CODE,'' ERROR_MSG FROM MES_PRODUCT_FAIL_HISTORY  T "
				+" WHERE CREATION_DATE BETWEEN TO_DATE('" + strStartDate + "', 'YYYY/MM/DD HH24:MI') AND TO_DATE('"
				+ strEndDate +"', 'YYYY/MM/DD HH24:MI')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";
			switch (intStationIndex)
			{
				case 1:
					strTemp = strTemp + " AND STATION_ID IN ('FQC')";
					break;
				case 2:
					strTemp = strTemp + " AND STATION_ID IN ('OQC')";
					break;
				case 3:
					strTemp = strTemp + " AND STATION_ID IN ('OOB')";
					break;
				default:
					strTemp = strTemp + " AND STATION_ID IN ('FQC','OQC','OOB')";
					break;
			}

			string strSql = "SELECT * FROM (SELECT DEFECT_CODE,DEFECT_DESC_CHT,QTY,CC,TO_CHAR(ROUND(QTY / CC * 100, 3),'990.000') || '%' RATE,STATION_CODE "
				+" FROM (SELECT ERROR_CODE DEFECT_CODE,ERROR_MSG DEFECT_DESC_CHT,STATION_CODE,COUNT(*)QTY,"+GetDefectTotal(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair)
				+" CC FROM ( SELECT  PRODUCT_ID,STATION_CODE,ERROR_CODE,ERROR_MSG FROM "
				+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+".PRODUCT_HISTORY_V A "+GetWhereStr(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair,true)+strTemp				
				+" ) GROUP BY ERROR_CODE,ERROR_MSG,STATION_CODE "
				+" ORDER BY COUNT(*) DESC))  ";
			if (!strTopN.Equals(""))
				strSql = strSql+" WHERE ROWNUM<="+strTopN;

			strTemp = "";
			DataTable dt = null;
			try
			{
				dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
				ResultMsg = "";
			}
			catch (Exception e)
			{
				ResultMsg = e.Message;
				//Page.RegisterStartupScript("Error","<script language=javascript>alert('"+e.ToString()+"');</script>");
				//return;
			}
			return dt;
		}

		private static string GetWhereStr(string strStartDate,string strEndDate,string strLine,string strModel,string strWO,int intType,int intRegion,int intStatisticsType,int intStationIndex,bool blRepair,bool Flag) 
		{
			string WhereStr = "";
		
			if (strStartDate!="" && strEndDate!="") 
			{
				WhereStr = "AND PDDATE BETWEEN TO_DATE('" + strStartDate + "','yyyy/mm/dd hh24:mi') AND TO_DATE('" + strEndDate + "','yyyy/mm/dd hh24:mi') ";
			}
		
			if (Flag.Equals(true))
            {
                //-------------------NexTexst
                if (strModel == "GNG" || strModel == "RCX" || strModel == "DVR" || strModel == "SLG" || strModel == "TWN" || strModel == "DVL")
                    WhereStr = WhereStr + " AND STATUS = 'F' AND UPPER(PRODUCT_ID) IN (SELECT S.PRODUCT_ID FROM Sfc.MES_PCBA_PANEL_DETAIL R,Sfc.MES_PCBA_PANEL_LINK S,SHP.CMCS_SFC_SORDER T WHERE S.PANEL_ID=R.PANEL_ID AND R.WO_NO=T.SORDER AND T.MODEL= '" + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[0] + "') " + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[1];
                else
                    WhereStr = WhereStr + " AND STATUS = 'F' AND UPPER(EMPLOYEE) <> 'REPAIR'  AND UPPER(PRODUCT_ID) LIKE '" + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[0] + "%' " + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[1];
            }
                
            else
				WhereStr = WhereStr +" AND UPPER(PRODUCT_ID)<>'SCY_INVALID_SN' "+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[1];

			if (blRepair)
				WhereStr = WhereStr + " AND REPAIR = 0 ";

			switch(intRegion)
			{
				case 0: 
					WhereStr = WhereStr + " AND STATION_CODE IN ('PT','CA','B1','B2','DL','PW','FL','BD','BB1','BB2','BT','CAPT','GPS','WCAWPT','WIFI') " ;
					break;
				case 1:
                    WhereStr = WhereStr + " AND STATION_CODE IN ('A1','A2','A3','A4','A5','WL','D2','FL','BD','PH','PRT','CI','BL','HT','CT','CM','FC','AD','PK','RA','QA','WT','CF','WLA','GP','AUT','BTWL','CAT','FAT','GPSWL','GSMWL','JTAG','MT','SCAT','TPT','WFWL') ";
					break;
				case 2:
					if (intStationIndex.Equals(0) || intStationIndex.Equals(2))
					{
						WhereStr = WhereStr + " AND STATION_CODE IN ('E2') " ;
					}
					else
					{
						WhereStr = WhereStr + " AND STATION_CODE IN ('') " ;
					}
					break;
			}
			switch(intStatisticsType)
			{
//				case 0:
//					WhereStr = WhereStr + " AND STATUS='F' ";
//					break;
				case 1:
                    WhereStr = WhereStr + " AND PDDATE = (SELECT MAX(PDDATE) FROM " + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[0] + ".PRODUCT_HISTORY_V WHERE PRODUCT_ID = A.PRODUCT_ID AND STATION_CODE = A.STATION_CODE and status='F' AND PDDATE BETWEEN TO_DATE('" + strStartDate + "','yyyy/mm/dd hh24:mi') AND TO_DATE('" + strEndDate + "','yyyy/mm/dd hh24:mi')) ";
					break;
				case 2:
                    WhereStr = WhereStr + " AND PDDATE = (SELECT MIN(PDDATE) FROM " + GetModelAndWhere(strLine, strModel, strWO, intType, intRegion).Split(':')[0] + ".PRODUCT_HISTORY_V WHERE PRODUCT_ID = A.PRODUCT_ID AND STATION_CODE = A.STATION_CODE and status='F' AND PDDATE BETWEEN TO_DATE('" + strStartDate + "','yyyy/mm/dd hh24:mi') AND TO_DATE('" + strEndDate + "','yyyy/mm/dd hh24:mi') ) ";
					break;
			}
			if (WhereStr.CompareTo("")!=0)
				WhereStr = "WHERE " + WhereStr.Substring(4);

			return WhereStr;
		}

		private static string GetModelAndWhere(string strLine,string strModel,string strWO,int intType,int intRegion)
		{
			string strTemp = "";
			string strModel_1 = "";
			if (!strLine.Equals(""))
				//strTemp = " AND UPPER(LINE_CODE) = 'LINE'||TO_CHAR(ASCII("+ClsCommon.GetSqlString(ddlLine.SelectedValue.Trim())+")-64) ";
				strTemp = " AND UPPER(LINE_CODE) = "+ClsCommon.GetSqlString(strLine);//'LINE'||TO_CHAR(ASCII("+ClsCommon.GetSqlString(ddlLine.SelectedValue.Trim())+")-64) ";
			switch (intType)
			{
				case 0:
					strModel_1 = strModel;
					break;
				case 1:
					strModel_1 = GetPN(strWO,intRegion).Substring(2,3);
					strTemp = strTemp + " AND WORK_ORDER = "+ ClsCommon.GetSqlString(strWO);
					break;
			}
			strTemp = strModel_1 +":"+ strTemp ;
			return strTemp;
		}

		private static string GetPN(string WO_NO,int intRegion) 
		{
			string StrSql = "";
			switch(intRegion)
			{
				case 0:	
					StrSql = "SELECT SPART FROM CMCS_SFC_SORDER WHERE SORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case 1:
					StrSql = "SELECT APART FROM CMCS_SFC_AORDER WHERE AORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
				case 2:
					StrSql = "SELECT PPART FROM CMCS_SFC_PORDER WHERE PORDER =" +ClsCommon.GetSqlString(WO_NO);
					break;
			}
			DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
			if (dt.Rows.Count>0)
			{
				return dt.Rows[0][0].ToString();			
			}
			else
			{
				return "";
			}

		}

		public static string GetDetailData(string strStation,string strStartDate,string strEndDate,string strLine,string strTopN,int intRegion,int intType,int intStatisticsType,int intStationIndex,
			string strModel,string strWO,bool blRepair,string strDefectCode,string strDefectMsg)
		{
			string strSql = "SELECT PRODUCT_ID,WORK_ORDER WO_NO,STATION_CODE STATION_ID,PDDATE CREATION_DATE,EMPLOYEE EMP_ID,ERROR_CODE DEFECT_CODE,ERROR_MSG DEFECT_DESC FROM "
				+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+".PRODUCT_HISTORY_V A " + GetWhereStr(strStartDate,strEndDate,strLine,strModel,strWO,intType,intRegion,intStatisticsType,intStationIndex,blRepair,true) ;
			if (strDefectCode.Equals(""))
				strSql = strSql + " AND (ERROR_CODE IS NULL OR ERROR_CODE='') ";
			else
				strSql = strSql + " AND ERROR_CODE = "+ClsCommon.GetSqlString(strDefectCode);

			if (strDefectMsg.Equals(""))
				strSql = strSql + " AND (ERROR_MSG IS NULL OR ERROR_MSG='') ";
			else
				strSql = strSql + " AND ERROR_MSG = "+ClsCommon.GetSqlString(strDefectMsg);

			if (intRegion == 1)
				strSql = strSql + "UNION SELECT PRODUCT_ID,''WO_NO,''STATION_ID,CREATION_DATE,''EMP_ID,DEFECT_CODE,ERROR_MESSAGE DEFECT_DESC FROM MES_REPAIR_UNTEST "
					+" WHERE CREATION_DATE BETWEEN TO_DATE('"+strStartDate+"', 'yyyy/mm/dd hh24:mi') AND TO_DATE('"
					+strEndDate+"', 'yyyy/mm/dd hh24:mi')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";


			if (intRegion == 2)
			{
				strSql = strSql + "UNION SELECT PRODUCT_ID,''WO_NO,STATION_ID,CREATION_DATE,EMP_ID,DEFECT_CODE,'' DEFECT_DESC FROM MES_PRODUCT_FAIL_HISTORY  "
					+" WHERE CREATION_DATE BETWEEN TO_DATE('"+strStartDate+"', 'yyyy/mm/dd hh24:mi') AND TO_DATE('"
					+strEndDate+"', 'yyyy/mm/dd hh24:mi')  AND PRODUCT_ID LIKE '"+GetModelAndWhere(strLine,strModel,strWO,intType,intRegion).Split(':')[0]+"%' ";
				switch (intStationIndex)
				{
					case 1:
						strSql = strSql + " AND STATION_ID IN ('FQC')";
						break;
					case 2:
						strSql = strSql + " AND STATION_ID IN ('OQC')";
						break;
					case 3:
						strSql = strSql + " AND STATION_ID IN ('OOB')";
						break;
					default:
						strSql = strSql + " AND STATION_ID IN ('FQC','OQC','OOB')";
						break;
				}

			}
            strSql += " and STATION_CODE='" + strStation + "'";
			return strSql;
		}

		//產生圖表
		public static void createChart(WebChartViewer viewer, string[] XLabel,double[] Values,string strTitle)
			//double[] software, double[] hardware, double[] services)
		{
			// Create a XYChart object of size 600 x 300 pixels, with a light grey (eeeeee)
			// background, black border, 1 pixel 3D border effect and rounded corners.
			XYChart c = new XYChart(600, 300, 0xeeeeee, 0x000000, 1);
			c.setRoundedFrame();

			// Set the plotarea at (60, 60) and of size 520 x 200 pixels. Set background
			// color to white (ffffff) and border and grid colors to grey (cccccc)
			c.setPlotArea(60, 60, 520, 200, c.linearGradientColor(60, 40, 60, 280, 0xeeeeff,0x0000cc), -1,0xffffff, 0xffffff);// 0xcccccc, 0xccccccc);

			// Add a title to the chart using 15pts Times Bold Italic font, with a light blue
			// (ccccff) background and with glass lighting effects.
			c.addTitle(strTitle,
				"Times New Roman Bold Italic", 15).setBackground(0xccccff, 0x000000,
				Chart.glassEffect());

			// Add a legend box at (70, 32) (top of the plotarea) with 9pts Arial Bold font
			c.addLegend(70, 32, false, "Arial Bold", 9).setBackground(Chart.Transparent);

			// Add a line chart layer using the supplied data
			//LineLayer layer = c.addLineLayer2();
			//BarLayer layer = c.addBarLayer3(Values);

			// Add a multi-color bar chart layer using the supplied data
			BarLayer layer = c.addBarLayer3(Values);


			//ChartDirector.TextBox textbox = 
			layer.addDataSet(Values,0x00FF00,strTitle).setDataLabelStyle("Arial Bold Italic");

			//Set to 3D
			//layer.set3D();

			// Set the data label font color for the  data set to yellow (0x0000FF)
			//textbox.setFontColor(0x0000ff);
			//Set the Node's Style
			layer.getDataSet(0).setDataSymbol(Chart.DiamondShape, 11);	
		
			// Use glass lighting effect with light direction from the left
			layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare,
				Chart.Left));


			// Set the line width to 3 pixels
			layer.setLineWidth(3);

			//c.xAxis().
			c.xAxis().setLabels(XLabel);

			// Set the y axis title
			c.yAxis().setTitle(strTitle);

			// Set the labels on the x axis. Rotate the labels by 60 degrees.
			c.xAxis().setLabels(XLabel).setFontAngle(345);

			// Set axes width to 2 pixels
			c.xAxis().setWidth(2);
			c.yAxis().setWidth(2);

			// Set all axes to transparent
			c.xAxis().setColors(Chart.Transparent);
			c.yAxis().setColors(Chart.Transparent);
			c.yAxis2().setColors(Chart.Transparent);


			// Output the chart
			viewer.Image = c.makeWebImage(Chart.PNG);

			// Include tool tip for the chart
			viewer.ImageMap = c.getHTMLImageMap("", "",
				"title='{dataSetName}  for {xLabel} =  {value}%'");
		}	
	}
}
