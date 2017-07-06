using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DB.EAI;
using DBAccess.EAI;
using System.Reflection;
using System.Drawing;
using Excela = Microsoft.Office.Interop.Excel;
namespace SFCQuery.Boundary
{

    public partial class WFrmDataQuery : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            Label5.Visible = false;
            Label6.Visible = false;
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
            {
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
            }
            GridView.Attributes.Add("style", "table-layout:fixed");
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strSql;
            DataSet ds = null;

            if (!ClsCommon.CheckIsDateTime(tbStartDate.DateTextBox.Text.Trim()))
            {
                Label5.Text = ViewState["ErrorDate"].ToString();
                Label5.Visible = true;
                Label6.Visible = false;
                return;
            }
            if (!ClsCommon.CheckIsDateTime(tbEndDate.DateTextBox.Text.Trim()))
            {
                Label6.Text = ViewState["ErrorDate"].ToString();
                Label6.Visible = true;
                Label5.Visible = false;
                return;
            }

            Label5.Visible = false;
            Label6.Visible = false;

            System.TimeSpan intday = Convert.ToDateTime(tbEndDate.DateTextBox.Text.Trim()) - Convert.ToDateTime(tbStartDate.DateTextBox.Text);
            if (intday.TotalDays > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "DateTime", "<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
                //this.RegisterStartupScript("DateTime","<script language=javascript>alert('選取的時間不能大於24小時！');</script>");
                return;
            }

            strSql = GetSql();
            ds = ClsGlobal.objDataConnect.DataQuery(strSql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('無符合條件的數據！');</script>");
                GridView.Visible = false;
                return;
            }
            else
            {
                
                GridView.Visible = true;
                GridView.DataSource = ds.Tables[0].DefaultView;
                GridView.DataBind();
                   
            }

        }

        public string GetSql()
        {
            string strSql;
            string strWhere;
            string startDate;
            string endDate;
            string feature;
            startDate = tbStartDate.DateTextBox.Text.Trim();
            endDate = tbEndDate.DateTextBox.Text.Trim();
            strSql = "SELECT TO_CHAR(DATETIME,'YYYY/MM/DD HH24:MI:SS') DATETIME,LINE,BOARD,LOCATION,FEATURE,HEIGHTRESULT,HEIGHT,HEIGHTUPFAIL,HEIGHTLOWFAIL,HEIGHTTARGET,AREARESULT,AREA,AREAUPFAIL,AREALOWFAIL,AREATARGET,VOLUMERESULT,VOLUME,VOLUMEUPFAIL,VOLUMELOWFAIL,VOLUMETARGET,VALID,REGRESULT,XOFFSET,YOFFSET,REGSHORT,REGLONG,REGSHORTFAIL,REGLONGFAIL,BRIDGERESULT,BRIDGELENGTH,BRIDGEFAIL FROM PANL_PRINT_ANALY ";
            strWhere = "WHERE DATETIME BETWEEN TO_DATE('" + startDate + "','YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + endDate + "','YYYY/MM/DD HH24:MI:SS') ";
            feature = tbFeature.Text.Trim().ToUpper();
            if(ddlLine.Text.Trim() != "")
            {
                strWhere = strWhere + "AND LINE ='" + ddlLine.Text.Trim() + "'";
            }

            if (tbFeature.Text.Trim() != "")
            {
                strWhere = strWhere + "AND FEATURE ='" + feature + "'";
            }

            if (tbLocation.Text.Trim() != "")
            {
                strWhere = strWhere + "AND LOCATION ='" + tbLocation.Text.Trim().ToUpper() + "'";
            }
            if (ddlBoard.Text.Trim() != "")
            {
                strWhere = strWhere + "AND BOARD ='" + ddlBoard.Text.Trim() + "'";
            }
            strSql = strSql + strWhere;
            return strSql;
        }

        //分頁
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string strSql;
            DataSet ds = null;
            strSql = GetSql();
            ds = ClsGlobal.objDataConnect.DataQuery(strSql);
            if (GridView.PageIndex >= 0 && GridView.PageIndex < GridView.PageCount)
            {
                GridView.PageIndex = e.NewPageIndex;
                GridView.DataSource = ds.Tables[0].DefaultView;
                GridView.DataBind();
            }

        }

        //單元格變色
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Style["width"] = "125px";
            //for (int i = 1; i < 31; i++)
            //{
            //    e.Row.Cells[i].Style["width"] = "50px";
               
            ////}
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Style["width"] = "110px";  
                if (e.Row.Cells[5].Text != "P")
                {
                    e.Row.Cells[6].ForeColor = Color.Red;
                }

                if (e.Row.Cells[10].Text != "P")
                {
                    e.Row.Cells[11].ForeColor = Color.Red;
                }

            }
        }
        
        protected void btnToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(GetSql()).Tables[0];
            ExortToExcel(dt);
        }
      
        private void ExortToExcel(DataTable dt)
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            //新建一Excel應用程式
            Missing miss = Missing.Value;

            Excela.ApplicationClass objExcel = null;
            Excela.Workbooks objBooks = null;
            Excela.Workbook objBook = null;
            Excela.Worksheet objSheet = null;

            try
            {
                objExcel = new Excela.ApplicationClass();
                objExcel.Visible = false;
                objBooks = (Excela.Workbooks)objExcel.Workbooks;
                objBook = (Excela.Workbook)(objBooks.Add(miss));
                objSheet = (Excela.Worksheet)objBook.ActiveSheet;
                //objSheet.Name = ddlLine.SelectedValue + "_" + ddlStation.SelectedValue;
                objSheet.Name = ddlLine.SelectedValue + "_" + ddlBoard.SelectedValue;

                int intColumn = dt.Columns.Count;
                for (int i = 1; i <= intColumn; i++)
                {
                    //SetRangeValue(objSheet,GetCellName(i,1),GetCellName(i,1),dt.Columns[i].ColumnName,true,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                    objSheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
                }

                int RowID = 2;

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i <= intColumn; i++)
                    {
                        //SetRangeValue(objSheet,GetCellName(i,RowID),GetCellName(i,RowID),row[i].ToString(),false,"",-1,Color.Empty,"Left","Top",0,0,false,true);
                        objSheet.Cells[RowID, i] = row[i - 1].ToString();
                    }

                    RowID++;
                }
                //

                objSheet.Columns.AutoFit();
                objSheet.Rows.AutoFit();

                //關閉Excel
                objBook.SaveAs(ExportPath + strFileName, miss, miss, miss, miss, miss, Excela.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
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
        
    }
}
