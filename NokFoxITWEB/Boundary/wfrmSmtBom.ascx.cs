/*************************************************************************
 * 
 *  Unit description: Top N Defect
 *  Developer: Shu Jian Bo             Date: 2007/09/11
 *  Modifier : Shu Jian Bo             Date: 2007/09/11
 * 
 * ***********************************************************************/
namespace SFCQuery.Boundary
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Reflection;
    using System.Resources;
    using DBAccess.EAI;
    using DB.EAI;
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    ///		WFrmMachineRate 的摘要描述。
    /// </summary>
    public partial class SmtBom : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiLanaguage();
            }
        }

        private void MultiLanaguage()
        {
            dgSMTBOM.Visible = false;
            Label1.Visible = false;
            lbPn.Text = (String)GetGlobalResourceObject("SFCQuery", "MaterialNO");
            lbPath.Text = (String)GetGlobalResourceObject("SFCQuery", "ForSelect");
            btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
            btnImport.Text = (String)GetGlobalResourceObject("SFCQuery", "Import");
            dgSMTBOM.Columns[0].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SMTHONHAIPN");
            dgSMTBOM.Columns[1].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "SMTLOCATION");
            dgSMTBOM.Columns[2].HeaderText = (String)GetGlobalResourceObject("SFCQuery", "Import_date");
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ShowData();
        }


        protected void btChose_Click(object sender, EventArgs e)
        {

        }

        private void Import()
        {
            //Modified by shujianbo at 2008/02/20
            #region ExcelLwb    
            string ExcelServerPath = Server.MapPath("../temp/test.xls");
            File2.PostedFile.SaveAs(ExcelServerPath);
            Excel.ApplicationClass ObjApp = null ;
            Excel.Workbooks ObjBooks = null;
            Excel.Workbook ObjBook = null;
            Excel.Worksheet ObjSheet = null;
            Excel.Range Rng1 = null;
            Excel.Range Rng2 = null;
            Missing miss = Missing.Value;
            try
            {
                ObjApp = new Excel.ApplicationClass();
                ObjBooks = (Excel.Workbooks)ObjApp.Workbooks;
                ObjBook = ObjBooks.Add(ExcelServerPath);
                ObjSheet = (Excel.Worksheet)ObjBook.Worksheets[1];

                string a;
                string b;
                int c = ObjSheet.Rows.Count;
                for (int i = 1; i <= ObjSheet.Rows.Count; i++)
                {
                    Rng1 = (Excel.Range)ObjSheet.Cells[i, 4];
                    Rng2 = (Excel.Range)ObjSheet.Cells[i, 7];
                    if (Rng1.Value2 == null || Rng2.Value2 == null)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Right", "<script language='javascript'>alert('Import Has Doned OK ！');</script>");
                        break;
                    }
                    else
                    {
                        a = Rng1.Value2.ToString();
                        b = Rng2.Value2.ToString();
                        ImportData(a, b);

                    }
                }
            }
            #endregion
            #region Quit 
            //if (ObjBooks.Count > 0)
            //{
            //    foreach (Excel.Workbook wb in ObjBooks)
            //    {
            //        if (wb == ObjBook)
            //        {
            //            wb.Close(miss, File2.PostedFile.FileName, miss);
            //        }
            //    }
            //}

            //if (ObjBooks.Count == 0)
            //{
            //    ObjApp.Quit();
            //}

            catch
            {
                throw;
            }
            finally
            {
                ObjBook.Close(false, miss, miss);
                ObjBooks.Close();
                ObjApp.Quit();

                if (!ObjSheet.Equals(null))
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjSheet);
                if (ObjBook != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjBook);
                if (ObjBooks != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjBooks);
                if (ObjApp != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjApp);
                if (Rng1 != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Rng1);
                if (Rng2 != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(Rng2);
                GC.Collect();
            }
            #endregion 

        }

        private void ImportData(string Str1,string Str2)
        {
            string tablenm ;
            string StrSql ;
            string StrHaiPn = Str1;
            string StrLoaction = Str2;
            tablenm = "MCM_SMT_PROGRAMBOM";

            StrSql = "insert into " + tablenm +
                     " (MATERIAL_NUMBER,COMPONEMT,LOCATION,IMPORT_DATE) values (" 
                     + ClsCommon.GetSqlString(tbPn.Text.ToUpper().Trim()) +","
                     + ClsCommon.GetSqlString(StrHaiPn) +","
                     + ClsCommon.GetSqlString(StrLoaction)+","
                     + "sysdate"
                     + ")";
            ClsGlobal.objDataConnect.DataExecute(StrSql);
            
        }

        private void BeforImport()
        {
            string tablenm;
            string StrSql;
            tablenm = "MCM_SMT_PROGRAMBOM";
            StrSql = "delete from " + tablenm + " where MATERIAL_NUMBER =" + ClsCommon.GetSqlString(tbPn.Text.ToUpper().Trim());
            ClsGlobal.objDataConnect.DataExecute(StrSql);
        }

        #region QuitExcel
        //private void Quit()
        //{
        //    if (ObjBooks.Count > 0)
        //    {
        //        foreach (Microsoft.Office.Interop.Excel.Workbook wb in ObjBooks)
        //        {
        //            if (wb == ObjBook)
        //            {
        //                wb.Close(miss, tbPath.Text, miss);
        //            }
        //        }
        //    }

        //    if (ObjBooks.Count == 0)
        //    {
        //        ObjApp.Quit();
        //    }

        //    foreach (Process process in System.Diagnostics.Process.GetProcesses())
        //    {
        //        if (process.ProcessName.ToUpper().Equals("EXCEL"))
        //            process.Kill();
        //    }

        //}
        #endregion
        private void ShowData()
        {
            string StrSql = "select MATERIAL_NUMBER,COMPONEMT,LOCATION,to_char(IMPORT_DATE,'YYYY/MM/DD') IMPORT_DATE from MCM_SMT_PROGRAMBOM where MATERIAL_NUMBER =" + ClsCommon.GetSqlString(tbPn.Text.ToUpper().Trim());
            System.Data.DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('SMT BOM未導入！');</script>");
                dgSMTBOM.Visible = false;
                return;
            }
            dgSMTBOM.DataSource = dt.DefaultView;
            dgSMTBOM.DataBind();
            Label1.Text =  "Current Page:" + (dgSMTBOM.CurrentPageIndex + 1).ToString() + "/" + dgSMTBOM.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            Label1.Visible = true;
            dgSMTBOM.Visible = true;
            
        }

        protected void dgSMTBOM_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            string StrSql = "select MATERIAL_NUMBER,COMPONEMT,LOCATION,to_char(IMPORT_DATE,'YYYY/MM/DD')  IMPORT_DATE from MCM_SMT_PROGRAMBOM where MATERIAL_NUMBER =" + ClsCommon.GetSqlString(tbPn.Text.ToUpper().Trim());
            dgSMTBOM.CurrentPageIndex = e.NewPageIndex;
            System.Data.DataTable dt = ClsGlobal.objDataConnect.DataQuery(StrSql).Tables[0];
            dgSMTBOM.DataSource = dt.DefaultView;
            dgSMTBOM.DataBind();
            Label1.Text = "Current Page:" + (dgSMTBOM.CurrentPageIndex + 1).ToString() + "/" + dgSMTBOM.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();
            Label1.Visible = true;
            dgSMTBOM.Visible = true;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            dgSMTBOM.CurrentPageIndex = 0;
            BeforImport();
            Import();
            //Quit();
            ShowData();
        }
}
}

