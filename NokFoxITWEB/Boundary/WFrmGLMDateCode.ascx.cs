namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using DBAccess.EAI;
    using System.Reflection;
    using System.Resources;
    using System.Globalization;
    using System.Configuration;
    using Excel = Microsoft.Office.Interop.Excel;
    using DB.EAI;
    using System.Data.OracleClient;

    /// <summary>
    ///		WFrmGLMDateCode 的摘要描述。
    /// </summary>
    public partial class WFrmGLMDateCode : System.Web.UI.UserControl
    {
        private static string strLine; 
        private static string strStation;
        private static string strDiskNo;
        private static string strInDate;
        private static string strOutDate;
        private static string strPartNo; 
        private static string strDateCode;
        private static string strRcvNo;
        private static string strSLDate;
        private static string strTrack;
        private static string strEmployee;
        private static string strVendorName;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            // 在這裡放置使用者程式碼以初始化網頁
            if (!IsPostBack)
            {
                MultiLanguage();
            }
        }

        #region Web Form 設計工具產生的程式碼
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 此為 ASP.NET Web Form 設計工具所需的呼叫。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
        ///		這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgDateCode.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDateCode_ItemCommand);

        }
        #endregion

        private void MultiLanguage()
        {
            //			Label1.Text = rm.GetString("WoInfo");
            //			Label2.Text = rm.GetString("ProductInfo");
            //			Label3.Text = rm.GetString("IMEIRelationShip");
            //			dgWO.Columns[1].HeaderText = rm.GetString("WO");
            //			dgWO.Columns[2].HeaderText = rm.GetString("Model");
            //			dgWO.Columns[3].HeaderText = rm.GetString("Item");
            //			dgWO.Columns[4].HeaderText = rm.GetString("WOQty");
        }

        protected void btnQuery_Click(object sender, System.EventArgs e)
        {
            //ClsDBScanHistory objScanHistory = new ClsDBScanHistory();
            //string ReturnMsg = "";
            //int InputLength = tbProductID.Text.Trim().Length; 
            //string strModel = "";
            //string strSql = "SELECT MODEL,SPART FROM Sfc.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID = " + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper());
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
 
            //if (dt.Rows.Count > 0)
            //{
            //    InputLength = 14;
            //    strModel = dt.Rows[0]["SPART"].ToString().Substring(2, 3);
            //    if (strModel.Equals("HER"))
            //        strModel = "HAIER";
            //}

            //switch (InputLength)
            //{
            //    case 14:  //Product ID
            //        ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(), strModel);
            //        break;
            //    case 15:  //IMEI
            //        if (strModel.Equals("HAIER"))
            //        {
            //            ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(), strModel);
            //            objScanHistory.FLabelType = 0;
            //        }
            //        else
            //        {
            //            ReturnMsg = objScanHistory.setIMEI(tbProductID.Text.Trim().ToUpper());
            //            objScanHistory.FLabelType = 1;
            //            objScanHistory.FIMEI = tbProductID.Text.ToUpper().Trim();
            //        }
            //        break;
            //    case 10:  //Picasso
            //        ReturnMsg = objScanHistory.setPICASSO(tbProductID.Text.Trim().ToUpper()); 
            //        break;
            //    default:
            //        ReturnMsg = "Input Error!";
            //        break;
            //}

            ClsDBScanHistory objScanHistory = new ClsDBScanHistory();
            string ReturnMsg = "";
            int InputLength = tbProductID.Text.Trim().Length;
            string strModel = "";
            string strSql = "SELECT MODEL,SPART FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL WHERE SPART = (SELECT SPART FROM SFC.PRODUCT_PANEL_SORDER WHERE PRODUCT_ID=" + ClsCommon.GetSqlString(tbProductID.Text.Trim().ToUpper())+")";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
 
            if (dt.Rows.Count > 0)
            {
                InputLength = 14;
                strModel = dt.Rows[0]["MODEL"].ToString();
            }
            if (tbProductID.Text.Trim().Substring(0, 1).ToUpper().Equals("A"))
                InputLength = 15;

            switch (InputLength)
            {
                case 14:  //Product ID
                    ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(), strModel);
                    objScanHistory.FLabelType = 0;
                    break;
                case 15:  //IMEI
                    if (strModel.Equals("HAIER"))
                    {
                        ReturnMsg = objScanHistory.setProductID(tbProductID.Text.Trim().ToUpper(), strModel);
                        objScanHistory.FLabelType = 0;
                    }
                    else
                    {
                        ReturnMsg = objScanHistory.setIMEI(tbProductID.Text.Trim().ToUpper());
                        objScanHistory.FLabelType = 1;
                        objScanHistory.FIMEI = tbProductID.Text.ToUpper().Trim();
                    }

                    break;
                case 10:  //Picasso
                    ReturnMsg = objScanHistory.setPICASSO(tbProductID.Text.Trim().ToUpper());
                    objScanHistory.FLabelType = 2;
                    objScanHistory.FPicasso = tbProductID.Text.ToUpper().Trim(); 
                    break;
                default:
                    ReturnMsg = "Input Error!";
                    break;
            }

            if (!ReturnMsg.Equals(""))
            {
                Label4.Text = ReturnMsg;
                Label4.Visible = true;
                //lblPanelInfo.Visible = false;
                dgDateCode.Visible = false;
                return;
            }
            Label4.Visible = false;
            dgDateCode.Visible = true;
            GetMaterial(objScanHistory);
        }

        private void GetMaterial(ClsDBScanHistory objHistory)
        {
            string strPanelID;
            string strSorder;
            string strModel;
            string strSql = "SELECT B.PANEL_ID,B.SORDER,A.SPART ,A.MODEL FROM SFC.CMCS_SFC_PCBA_BARCODE_CTL A, SFC.PRODUCT_PANEL_SORDER B WHERE A.SPART=B.SPART AND B.PRODUCT_ID='" + objHistory.FProductID + "'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[00];
            if (dt1.Rows.Count > 0)
            {
                strPanelID = dt1.Rows[0]["PANEL_ID"].ToString();
                strSorder = dt1.Rows[0]["SORDER"].ToString();
                strModel = dt1.Rows[0]["MODEL"].ToString();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoRecord") + "');</script>");
                return;
            }
            //string strsql = "SELECT B.KEY_PART_NO PartNo,S.DATECODE DateCode,SUBSTR(S.DISKNO,1,14) RcvNo,S.DISKNO,MAX(S.CREATION_DATE) SLDate,S.MACHINE_NAME Line,D.STATION_ID Stationid,S.TRACK_NO Track,S.EMP_NO Employee FROM Sfc.TRACK_COMP_DATECODE  S,Sfc.MES_PCBA_PANEL_DETAIL T,Sfc.MES_PCBA_PANEL_LINK	A,Sfc.K_SEND_HISTORY B,SAP.SAP_WO_COMP C,Sfc.MES_PCBA_PANEL_HISTORY D WHERE TO_DATE( TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'), 'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE(TO_CHAR(D.CREATION_DATE-1,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') AND TO_DATE(TO_CHAR (D.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') AND A.PRODUCT_ID='" + objHistory.FProductID + "' AND  SUBSTR(D.STATION_ID,3,2) ='IA' AND A.PANEL_ID=T.PANEL_ID AND A.PANEL_ID = SUBSTR(D.PANEL_ID,2,20)	AND S.DISKNO=B.DISKNO AND B.KEY_PART_NO = C.COMPONEMT AND C.WO_NO=T.WO_NO AND CHR(ASCII('A') + SUBSTR(S.MACHINE_NAME,3,2) -1)=SUBSTR(D.STATION_ID,2,1) GROUP BY B.KEY_PART_NO,S.DATECODE,S.DISKNO,S.DISKNO,S.CREATION_DATE,S.MACHINE_NAME,D.STATION_ID,S.TRACK_NO,S.EMP_NO";
            string strsql = "SELECT M.PartNo,N.VENDOR_NAME,M.DateCode,M.RcvNo,M.DISKNO,M.SLDate,M.Line,M.Stationid,M.Track,M.Employee FROM (SELECT B.KEY_PART_NO PartNo,S.DATECODE DateCode,SUBSTR(S.DISKNO,1,14) RcvNo,S.DISKNO,MAX(S.CREATION_DATE) SLDate,S.MACHINE_NAME Line,D.STATION_ID Stationid,S.TRACK_NO Track,S.EMP_NO Employee,ROW_NUMBER() OVER (PARTITION BY S.TRACK_NO ORDER BY S.CREATION_DATE desc ) RN  FROM Sfc.TRACK_COMP_DATECODE  S,Sfc.MES_PCBA_PANEL_DETAIL T,Sfc.MES_PCBA_PANEL_LINK	A,Sfc.K_SEND_HISTORY B,SAP.SAP_WO_COMP C,Sfc.MES_PCBA_PANEL_HISTORY D WHERE S.CREATION_DATE BETWEEN D.CREATION_DATE-1 AND D.CREATION_DATE AND A.PRODUCT_ID='" + objHistory.FProductID + "' AND  SUBSTR(D.STATION_ID,3,2) IN ('IA','IN') AND A.PANEL_ID=T.PANEL_ID AND A.PANEL_ID = SUBSTR(D.PANEL_ID,2,20)	AND S.DISKNO=B.DISKNO AND B.KEY_PART_NO = C.COMPONEMT AND C.WO_NO=T.WO_NO GROUP BY B.KEY_PART_NO,S.DATECODE,S.DISKNO,S.DISKNO,S.CREATION_DATE,S.MACHINE_NAME,D.STATION_ID,S.TRACK_NO,S.EMP_NO) M JOIN SAP.SAP_PO_RCV_INFO N ON M.PartNo=N.ITEM_NUMBER AND N.RECEIPT_NUM=SUBSTR(RCVNO,1,10) AND M.RN=1";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Panel1.Visible = true;
                strPartNo = dt.Rows[0]["PartNo"].ToString();
                strVendorName = dt.Rows[0]["VENDOR_NAME"].ToString();
                strDateCode = dt.Rows[0]["DateCode"].ToString();
                strRcvNo = dt.Rows[0]["RcvNo"].ToString();
                strSLDate = dt.Rows[0]["SLDate"].ToString();
                strLine = dt.Rows[0]["Line"].ToString();
                strTrack = dt.Rows[0]["Track"].ToString();
                strEmployee = dt.Rows[0]["Employee"].ToString();
                strDiskNo = dt.Rows[0]["DISKNO"].ToString();
                strStation = dt.Rows[0]["Stationid"].ToString();
                dgDateCode.DataSource = dt.DefaultView;
                dgDateCode.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoRecord") + "');</script>");
                return;
            }  
        }

        //private void GetMaterial(ClsDBScanHistory objHistory)
        //{
        //    string strSql = "SELECT WO_NO,PANEL_ID FROM SFC.MES_PCBA_PANEL_DETAIL WHERE PANEL_ID = (SELECT PANEL_ID FROM SFC.MES_PCBA_PANEL_LINK WHERE PRODUCT_ID = '" + objHistory.FProductID + "')";
        //    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

        //    if (dt.Rows.Count > 0)
        //    {
        //        strWONO = dt.Rows[0][0].ToString();
        //        strPanelID = dt.Rows[0][1].ToString();
        //        string strSql1;
        //        strSql1 = "SELECT SORDER,MODEL FROM CMCS_SFC_SORDER WHERE SORDER=" + ClsCommon.GetSqlString(strWONO.ToUpper());
        //        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strSql1).Tables[00];
        //        if (dt1.Rows.Count > 0)
        //        {
        //            strModel = dt1.Rows[0]["MODEL"].ToString();
        //        }
        //        //獲得InputDate
        //        strSql = "SELECT TO_CHAR (CREATION_DATE-3,'YYYY/MM/DD HH24:MI:SS') INPUTDATE,TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') CREATION_DATE,STATION_ID FROM MES_PCBA_PANEL_HISTORY WHERE PANEL_ID ='A'||" + ClsCommon.GetSqlString(dt.Rows[0][1].ToString())
        //            + " AND STATION_ID LIKE '%IA'";
        //        dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

        //        strSql = "SELECT B.KEY_PART_NO PartNo,S.DATECODE DateCode,SUBSTR( S.DISKNO,1,14) RcvNo,S.DISKNO,MAX(S.CREATION_DATE) SLDate,B.LINE_NAME  Line,D.STATION_ID STATION,S.TRACK_NO Track,S.EMP_NO Employee FROM Sfc.TRACK_COMP_DATECODE S,Sfc.K_SEND_HISTORY B,SAP.SAP_WO_COMP C,Sfc. MES_PCBA_PANEL_HISTORY D"
        //            + " WHERE TO_DATE( TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'), 'YYYY/MM/DD HH24:MI:SS') BETWEEN  TO_DATE('" + dt.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + dt.Rows[0][1].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND S.DISKNO=B.DISKNO AND B.KEY_PART_NO = C.COMPONEMT AND C.WO_NO="
        //            + ClsCommon.GetSqlString(strWONO.ToUpper()) + " AND CHR(ASCII('A') + SUBSTR(B.LINE_NAME,3,2) -1)=SUBSTR(D.STATION_ID,2,1)  AND D.PANEL_ID ='A'||" + ClsCommon.GetSqlString(strPanelID.ToUpper()) + " AND D.STATION_ID LIKE '%IA' GROUP BY B.KEY_PART_NO,S.DATECODE,SUBSTR( S.DISKNO,1,14),S.DISKNO,S.CREATION_DATE,B.LINE_NAME,D.STATION_ID,S.TRACK_NO,S.EMP_NO";

        //        //strSql = "SELECT S.KEY_PART_NO,T.MACHINE_NAME,T.TRACK_NO,T.DISKNO,T.DATECODE,SUBSTR(T.DISKNO,1,14) RCVNO,T.CREATION_DATE,T.EMP_NO FROM Sfc.TRACK_COMP_DATECODE T,K_RECEIVE_DISKNO S"
        //        //+ "  WHERE CREATION_DATE BETWEEN TO_DATE('" + dt.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + dt.Rows[0][1].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND T.DISKNO = S.DISKNO";
        //        dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
        //        if (dt.Rows.Count > 0)
        //        { 
        //            strPartNo = dt.Rows[0]["PartNo"].ToString();
        //            strDateCode = dt.Rows[0]["DateCode"].ToString();
        //            strRcvNo = dt.Rows[0]["RcvNo"].ToString(); 
        //            strLine = dt.Rows[0]["Line"].ToString();
        //            strTrack = dt.Rows[0]["Track"].ToString();
        //            strEmployee = dt.Rows[0]["Employee"].ToString();
        //            strDiskNo = dt.Rows[0]["DISKNO"].ToString();
        //            strStation = dt.Rows[0]["STATION"].ToString();
        //        } 
        //        dgDateCode.DataSource = dt.DefaultView;
        //        dgDateCode.DataBind();
        //    }
        //    else
        //    {
        //    }
        //}

        private void dgDateCode_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

            string strPartNo = ((LinkButton)(e.Item.Cells[0].Controls[1])).Text;
            string strDateCode = ((LinkButton)(e.Item.Cells[2].Controls[1])).Text;
            string strRcvNo = ((LinkButton)(e.Item.Cells[3].Controls[1])).Text;
            string strDiskNo = ((Label)(e.Item.Cells[4].Controls[1])).Text;
            string strSLDate = ((Label)(e.Item.Cells[5].Controls[1])).Text;
            string strLine = ((Label)(e.Item.Cells[6].Controls[1])).Text;
            string strTrack = ((Label)(e.Item.Cells[7].Controls[1])).Text;
            string strEmployee = ((Label)(e.Item.Cells[8].Controls[1])).Text;

            if (e.CommandName.ToUpper() == "DATECODE")
            {
                string strsql;

                //string strDateCode = ((Label)(e.Item.Cells[5].Controls[1])).Text;

                //string strPartNo = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                //string strSLDate = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                //string strLine = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                //string strTrack = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                //string strEmployee= ((Label)(e.Item.Cells[5].Controls[1])).Text;
                //string strDiskNo = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                
                strsql = "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') INDATE,TRACK_NO FROM Sfc.TRACK_COMP_DATECODE WHERE DISKNO=" + "'" + strDiskNo + "'";
                DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                strInDate = dt0.Rows[0]["INDATE"].ToString();
                
                
                string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE  A.CREATION_DATE >TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = '" + strLine + "' GROUP BY A.MACHINE_NAME,A.TRACK_NO";
                
                //string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = 'ES07'  GROUP BY A.MACHINE_NAME,A.TRACK_NO";
                DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    Panel2.Visible = true;
                    strOutDate = dt.Rows[0]["OUTDATE"].ToString();
                    dt = null;

                    lb1.Visible = true;
                    lb11.Visible = true;
                    lb11.Text = strDateCode;

                    lbl9.Text = strDiskNo;

                    lb33.Text = strPartNo;
                    lb44.Text = strSLDate;
                    lb55.Text = strLine;
                    lb66.Text = strTrack;
                    lb77.Text = strEmployee;
                    string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  S.CREATION_DATE  BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
                    strsql1 = strsql1 + " UNION  ";
                    strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE  S.CREATION_DATE  BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
                    DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                    dgDateCodeDetail.DataSource = dt1.DefaultView;
                    dgDateCodeDetail.DataBind();
                    Label04.Text = "Current Page: 1 / Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt1.Rows.Count.ToString();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoOut") + "');</script>");
                    return;
                }
            }
            else
                if (e.CommandName.ToUpper() == "RCVNO")
                {
                    string strsql; 
                    //string strDiskNo = ((Label)(e.Item.Cells[5].Controls[1])).Text;
                    strsql = "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') INDATE,TRACK_NO FROM Sfc.TRACK_COMP_DATECODE WHERE DISKNO=" + "'" + strDiskNo + "'";
                    DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                    strInDate = dt0.Rows[0]["INDATE"].ToString();

                    string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = '" + strLine + "' GROUP BY A.MACHINE_NAME,A.TRACK_NO";
                    //string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = 'ES07'  GROUP BY A.MACHINE_NAME,A.TRACK_NO";
                    DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        Panel2.Visible = true;
                        strOutDate = dt.Rows[0]["OUTDATE"].ToString();
                        dt = null;

                        lb2.Visible = true;
                        lb22.Visible = true;
                        lb22.Text = strRcvNo;

                        lb33.Text = strPartNo;
                        lb44.Text = strSLDate;
                        lb55.Text = strLine;
                        lb66.Text = strTrack;
                        lb77.Text = strEmployee;

                        lbl9.Text = strDiskNo;
                        string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
                        strsql1 = strsql1 + " UNION ALL ";
                        strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
                        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                        dgDateCodeDetail.DataSource = dt1.DefaultView;
                        dgDateCodeDetail.DataBind();
                        Label04.Text = "Current Page: 1 / Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt1.Rows.Count.ToString();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoOut") + "');</script>");
                        return;
                    }
                    //lb33.Text = strPartNo;
                    //lb44.Text = strSLDate;
                    //lb55.Text = strLine;
                    //lb66.Text = strTrack;
                    //lb77.Text = strEmployee;
                    //string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
                    //strsql1 = strsql1 + " UNION ALL ";
                    //strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
                    //DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                    //dgDateCodeDetail.DataSource = dt1.DefaultView;
                    //dgDateCodeDetail.DataBind();
                    //Label04.Text = "Current Page: 1 / Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt1.Rows.Count.ToString();

                }
                else

                    if (e.CommandName.ToUpper() == "PARTNO")
                    {
                        //----------------note by zhangjijing----------------
                        //string strProcedureName = "SFCQUERY.GETDATACODE";
                        //OracleParameter[] oraPara = new OracleParameter[]{new OracleParameter("PARTNO",OracleType.VarChar,20),
                        //                       new OracleParameter("DATA",OracleType.Cursor)};
                        //oraPara[0].Value = strPartNo;
                        //oraPara[1].Direction = ParameterDirection.Output;
                        //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara).Tables[0];
                        //----------------note by zhangjijing end----------------------------

                        //------------------note at 20090617----------
                        //OracleConnection orcn = null; 
                        //OracleDataAdapter da = new OracleDataAdapter();
                        //DataSet myds;
                        //myds = new DataSet();
                        //string strSql = "";

                        //orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");
                        //orcn.Open();
                        //string strsql = "select 'S'||chr(substr(machine_name,3,2)+64)||'IA' station_id,indate,outdate "+
                        //    "from (select s.diskno,t.track_no,t.machine_name,to_char(t.creation_date,'YYYY/MM/DD HH24:MI:SS') indate,"+
                        //    "to_char(r.creation_date,'YYYY/MM/DD HH24:MI:SS') outdate,row_number() over(partition by s.diskno,t.track_no,"+
                        //    "t.machine_name,t.creation_date order by r.creation_date) rn from sfc.k_send_history s,sfc.track_comp_datecode t,"+
                        //    "sfc.track_comp_datecode r where s.diskno=t.diskno and key_part_no='" + strPartNo + "' and send_type<>'0' "+
                        //    "and r.creation_date>t.creation_date and t.track_no=r.track_no and t.machine_name=r.machine_name) where rn=1 ";
                        //DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
                        //if (dt1.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < dt1.Rows.Count; i++)
                        //    {
                        //        string strstationid = dt1.Rows[i]["station_id"].ToString();
                        //        string strindate = dt1.Rows[i]["indate"].ToString();
                        //        string stroutdate = dt1.Rows[i]["outdate"].ToString();
                        //        strSql = "select t.product_id productid,(select decode(imei,null,'',imei) imei from shp.cmcs_sfc_shipping_data where productid=t.product_id) imei,"+
                        //            "t.sorder sorder,t.spart spart,s.creation_date inputdate from sfc.mes_pcba_panel_history s,sfc.product_panel_sorder t "+
                        //            "where s.creation_date between to_date('" + strindate + "','YYYY/MM/DD HH24:MI:SS') and to_date('" + stroutdate + "','YYYY/MM/DD HH24:MI:SS') "+
                        //            " and s.station_id='"+strstationid+"' and substr(s.panel_id,2,20)=t.panel_id"; 
                        //        OracleCommand commd = new OracleCommand(strSql);
                        //        commd.Connection = orcn;
                        //        da.SelectCommand = commd; 
                        //        da.Fill(myds); 
                        //    }
                        //} 

                        //DataTable dt = myds.Tables[0];
                        // ----------------note at 20090617 end----------------------------


                        DataTable dt = getpndatacodeimei(strPartNo);

                        if (dt.Rows.Count > 0)
                        {
                            Panel2.Visible = true;

                            lb2.Visible = false;
                            lb22.Visible = false;
                            btnExportExcel.Visible = false;
                            btnExportExcelPartNo.Visible = true;
                            //lb22.Text = strRcvNo;

                            lb33.Text = strPartNo;
                            lb44.Text = "";
                            lb55.Text = "";
                            lb66.Text = "";
                            lb77.Text = "";
                            lbl9.Text = "";
                            dgDateCodeDetail.DataSource = dt.DefaultView;
                            dgDateCodeDetail.DataBind();
                            Label04.Text = "Current Page: 1 / Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt.Rows.Count.ToString();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoOut") + "');</script>");
                            return;
                        }
                        //lb33.Text = strPartNo;
                        //lb44.Text = strSLDate;
                        //lb55.Text = strLine;
                        //lb66.Text = strTrack;
                        //lb77.Text = strEmployee;
                        //string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
                        //strsql1 = strsql1 + " UNION ALL ";
                        //strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
                        //DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
                        //dgDateCodeDetail.DataSource = dt1.DefaultView;
                        //dgDateCodeDetail.DataBind();
                        //Label04.Text = "Current Page: 1 / Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt1.Rows.Count.ToString();

                    }
        }

        protected void dgDateCodeDetail_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgDateCodeDetail.CurrentPageIndex = e.NewPageIndex;

            string strsql;
            strsql = "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') INDATE,TRACK_NO FROM Sfc.TRACK_COMP_DATECODE WHERE DISKNO=" + "'" + strDiskNo + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            strInDate = dt0.Rows[0]["INDATE"].ToString();

            string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE A.CREATION_DATE>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = '" + strLine + "' GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            //string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = 'ES07'  GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];

           
            Panel2.Visible = true;
            strOutDate = dt.Rows[0]["OUTDATE"].ToString();
            dt = null;

            string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  S.CREATION_DATE BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
            strsql1 = strsql1 + " UNION  ";
            strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE S.CREATION_DATE BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            dgDateCodeDetail.DataSource = dt1.DefaultView;
            dgDateCodeDetail.DataBind(); 
            Label04.Text = "Current Page:" + (dgDateCodeDetail.CurrentPageIndex + 1).ToString() + "/ Total Page:" + dgDateCodeDetail.PageCount.ToString() + " /Total Num:" + dt1.Rows.Count.ToString();
  
        }

        protected void btnExportExcel_Click(object sender, System.EventArgs e)
        {
            string strsql;
            string strDiskNo = lbl9.Text;
            strsql = "SELECT TO_CHAR(CREATION_DATE,'YYYY/MM/DD HH24:MI:SS') INDATE,TRACK_NO FROM Sfc.TRACK_COMP_DATECODE WHERE DISKNO=" + "'" + strDiskNo + "'";
            DataTable dt0 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            strInDate = dt0.Rows[0]["INDATE"].ToString();

            string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = '" + strLine + "' GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            //string strSql = "SELECT TO_CHAR(MIN(A.CREATION_DATE),'YYYY/MM/DD HH24:MI:SS') OUTDATE,A.MACHINE_NAME,A.TRACK_NO FROM Sfc.TRACK_COMP_DATECODE A WHERE TO_DATE(TO_CHAR(A.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS')>TO_DATE('" + dt0.Rows[0][0].ToString() + "','YYYY/MM/DD HH24:MI:SS') AND TRACK_NO='" + dt0.Rows[0][1].ToString() + "' AND  A.MACHINE_NAME = 'ES07'  GROUP BY A.MACHINE_NAME,A.TRACK_NO";
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strSql).Tables[0];
 
            strOutDate = dt.Rows[0]["OUTDATE"].ToString();  
            string strsql1 = "SELECT PRODUCTID,MAX(IMEI) IMEI,SORDER,SPART,INPUTDATE FROM(SELECT T.PRODUCT_ID PRODUCTID,'' IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL U,SHP.CMCS_SFC_SORDER X WHERE  TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND U.PANEL_ID= T.PANEL_ID AND U.WO_NO=X.SORDER";
            strsql1 = strsql1 + " UNION ALL ";
            strsql1 = strsql1 + "SELECT T.PRODUCT_ID PRODUCTID,V.IMEI,U.WO_NO SORDER,X.SPART SPART,S.CREATION_DATE INPUTDATE FROM Sfc.MES_PCBA_PANEL_HISTORY S,Sfc.MES_PCBA_PANEL_LINK T,Sfc.MES_PCBA_PANEL_DETAIL_LK U,SHP.CMCS_SFC_SHIPPING_DATA V,SHP.CMCS_SFC_SORDER X WHERE TO_DATE(TO_CHAR(S.CREATION_DATE,'YYYY/MM/DD HH24:MI:SS'),'YYYY/MM/DD HH24:MI:SS') BETWEEN TO_DATE('" + strInDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + strOutDate + "','YYYY/MM/DD HH24:MI:SS') AND S.STATION_ID='" + strStation + "' AND SUBSTR(S.PANEL_ID,2,20)= U.PANEL_ID AND SUBSTR(S.PANEL_ID,2,20)= T.PANEL_ID AND U.WO_NO=X.SORDER AND  T.PRODUCT_ID =V.PRODUCTID) GROUP BY  PRODUCTID,SORDER,SPART,INPUTDATE";
         
            ExortToExcel1(strsql1);
        }

        private void ExortToExcel1(string strSql)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            //string strFileName;
            //strFileName = lb11.Text + lb22.Text + ".xls";

            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //string strFileName =DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing missing = Missing.Value;
            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;
            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(missing));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet;

                clsDBToExcel.ExportToExcel(objSheet, strSql);

                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                objBook.Close(false, missing, missing);
                objBooks.Close();
                objExcel.Quit();
            }
            finally
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
                if (!objSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
                if (objBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                if (objBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                if (objExcel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                GC.Collect();
            }
            //保存或打開報表
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName);
            Response.End();
        }

        //private void ExortToExcel1(string strSql)
        //{
        //    string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
        //    string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
        //    //新建一Excel應用程式
        //    Missing missing = Missing.Value;
        //    Excel.ApplicationClass objExcel = null;
        //    Excel.Workbooks objBooks = null;
        //    Excel.Workbook objBook = null;
        //    Excel.Worksheet objSheet = null;
        //    try
        //    {
        //        objExcel = new Excel.ApplicationClass();
        //        objExcel.Visible = false;
        //        objBooks = (Excel.Workbooks)objExcel.Workbooks;
        //        objBook = (Excel.Workbook)(objBooks.Add(missing));
        //        objSheet = (Excel.Worksheet)objBook.ActiveSheet;

        //        clsDBToExcel.ExportToExcel(objSheet, strSql);

        //        //關閉Excel
        //        objBook.SaveAs(ExportPath + strFileName, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
        //        objBook.Close(false, missing, missing);
        //        objBooks.Close();
        //        objExcel.Quit();
        //    }
        //    finally
        //    {
        //        //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
        //        if (!objSheet.Equals(null))
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
        //        if (objBook != null)
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
        //        if (objBooks != null)
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
        //        if (objExcel != null)
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
        //        GC.Collect();
        //    }
        //    //保存或打開報表
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
        //    Response.Charset = "";
        //    this.EnableViewState = false;
        //    Response.WriteFile(ExportPath + strFileName);
        //    Response.End();
        //}


        private void ExortToExcel(DataTable dt)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName; 
            strFileName = DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing miss = Missing.Value;

            Excel.ApplicationClass objExcel = null;
            Excel.Workbooks objBooks = null;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null;

            try
            {
                objExcel = new Excel.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excel.Workbooks)objExcel.Workbooks;
                objBook = (Excel.Workbook)(objBooks.Add(miss));
                objSheet = (Excel.Worksheet)objBook.ActiveSheet;

                //  后續修改  

                int intColumn = dt.Columns.Count;
                for (int i = 1; i <= intColumn; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[1, i] = dt.Columns[i-1].ColumnName;
                }

                int RowID = 2;

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i <= intColumn; i++)
                    {
                        //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                        objSheet.Cells[RowID, i] = row[i-1].ToString();
                    }

                    RowID++;
                }
                //

                objSheet.Columns.AutoFit();
                objSheet.Rows.AutoFit();

                //頁面設置
                try
                {
                    objSheet.PageSetup.LeftMargin = 20;
                    objSheet.PageSetup.RightMargin = 20;
                    objSheet.PageSetup.TopMargin = 35;
                    objSheet.PageSetup.BottomMargin = 15;
                    objSheet.PageSetup.HeaderMargin = 7;
                    objSheet.PageSetup.FooterMargin = 10;
                    objSheet.PageSetup.CenterHorizontally = true;
                    objSheet.PageSetup.CenterVertically = false;
                    objSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                    objSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                    objSheet.PageSetup.Zoom = false;
                    objSheet.PageSetup.FitToPagesWide = 1;
                    objSheet.PageSetup.FitToPagesTall = false;
                }
                catch
                {
                    throw;
                }
                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                objBook.Close(false, miss, miss);
                objBooks.Close();
                objExcel.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
            }
            catch
            {
                throw;
            }
            finally
            {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(objRange);
                if (!objSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objSheet);
                if (objBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                if (objBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                if (objExcel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExcel);
                GC.Collect();
            }

            //保存或打開報表
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "";
            this.EnableViewState = false;
            Response.WriteFile(ExportPath + strFileName);
            Response.End();
        }

        private DataTable getpndatacodeimei(string partno)
        {
            OracleConnection orcn = null;
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet myds;
            myds = new DataSet();
            string strSql = "";

            orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");
            orcn.Open();
            string strsql = "select 'S'||chr(substr(machine_name,3,2)+64)||'IA' station_id,indate,outdate " +
                "from (select s.diskno,t.track_no,t.machine_name,to_char(t.creation_date,'YYYY/MM/DD HH24:MI:SS') indate," +
                "to_char(r.creation_date,'YYYY/MM/DD HH24:MI:SS') outdate,row_number() over(partition by s.diskno,t.track_no," +
                "t.machine_name,t.creation_date order by r.creation_date) rn from sfc.k_send_history s,sfc.track_comp_datecode t," +
                "sfc.track_comp_datecode r where s.diskno=t.diskno and key_part_no='" + strPartNo + "' and send_type<>'0' " +
                "and r.creation_date>t.creation_date and t.track_no=r.track_no and t.machine_name=r.machine_name) where rn=1 ";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string strstationid = dt1.Rows[i]["station_id"].ToString();
                    string strindate = dt1.Rows[i]["indate"].ToString();
                    string stroutdate = dt1.Rows[i]["outdate"].ToString();
                    strSql = "select t.product_id productid,(select decode(imei,null,'',imei) imei from shp.cmcs_sfc_shipping_data where productid=t.product_id) imei," +
                        "t.sorder sorder,t.spart spart,s.creation_date inputdate from sfc.mes_pcba_panel_history s,sfc.product_panel_sorder t " +
                        "where s.creation_date between to_date('" + strindate + "','YYYY/MM/DD HH24:MI:SS') and to_date('" + stroutdate + "','YYYY/MM/DD HH24:MI:SS') " +
                        " and s.station_id='" + strstationid + "' and substr(s.panel_id,2,20)=t.panel_id";
                    OracleCommand commd = new OracleCommand(strSql);
                    commd.Connection = orcn;
                    da.SelectCommand = commd;
                    da.Fill(myds);
                }
            }
            DataTable dt = myds.Tables[0];
            return dt;
        }

        protected void btnExportExcelPartNo_Click(object sender, EventArgs e)
        {
            string strPartNo = lb33.Text;
            //string strProcedureName = "SFCQUERY.GETDATACODE";
            //OracleParameter[] oraPara = new OracleParameter[]{new OracleParameter("PARTNO",OracleType.VarChar,20),
            //                                   new OracleParameter("DATA",OracleType.Cursor)};
            //oraPara[0].Value = strPartNo;
            //oraPara[1].Direction = ParameterDirection.Output;
            //DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, oraPara).Tables[0];


            //-------------note -----------------
            //OracleConnection orcn = null;
            //OracleDataAdapter da = new OracleDataAdapter();
            //DataSet myds;
            //myds = new DataSet();
            //string strSql = "";

            //orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");
            //orcn.Open();
            //string strsql = "select 'S'||chr(substr(machine_name,3,2)+64)||'IA' station_id,indate,outdate " +
            //    "from (select s.diskno,t.track_no,t.machine_name,to_char(t.creation_date,'YYYY/MM/DD HH24:MI:SS') indate," +
            //    "to_char(r.creation_date,'YYYY/MM/DD HH24:MI:SS') outdate,row_number() over(partition by s.diskno,t.track_no," +
            //    "t.machine_name,t.creation_date order by r.creation_date) rn from sfc.k_send_history s,sfc.track_comp_datecode t," +
            //    "sfc.track_comp_datecode r where s.diskno=t.diskno and key_part_no='" + strPartNo + "' and send_type<>'0' " +
            //    "and r.creation_date>t.creation_date and t.track_no=r.track_no and t.machine_name=r.machine_name) where rn=1 ";
            //DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            //if (dt1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt1.Rows.Count; i++)
            //    {
            //        string strstationid = dt1.Rows[i]["station_id"].ToString();
            //        string strindate = dt1.Rows[i]["indate"].ToString();
            //        string stroutdate = dt1.Rows[i]["outdate"].ToString();
            //        strSql = "select t.product_id productid,(select decode(imei,null,'',imei) imei from shp.cmcs_sfc_shipping_data where productid=t.product_id) imei," +
            //            "t.sorder sorder,t.spart spart,s.creation_date inputdate from sfc.mes_pcba_panel_history s,sfc.product_panel_sorder t " +
            //            "where s.creation_date between to_date('" + strindate + "','YYYY/MM/DD HH24:MI:SS') and to_date('" + stroutdate + "','YYYY/MM/DD HH24:MI:SS') " +
            //            " and s.station_id='" + strstationid + "' and substr(s.panel_id,2,20)=t.panel_id";
            //        OracleCommand commd = new OracleCommand(strSql);
            //        commd.Connection = orcn;
            //        da.SelectCommand = commd;
            //        da.Fill(myds);
            //    }
            //}

            //DataTable dt = myds.Tables[0];

            DataTable dt = getpndatacodeimei(strPartNo);

            ExortToExcel(dt);
        }
}
}
