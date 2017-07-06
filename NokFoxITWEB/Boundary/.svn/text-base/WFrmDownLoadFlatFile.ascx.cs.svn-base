namespace SFCQuery.Boundary
{
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
    using DBAccess.EAI;
    using System.Globalization;
    using System.IO;
    //using IMS.ServerControls;

    public partial class WFrmDownLoadFlatFile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindModel();
                MultiLanguage();
                tbInvoiceNO.Attributes["onKeyDown"] = "focusSignInButton()";
            }
        }

        private void BindModel()
        {
            string strProcedureName = "SFCQUERY.GETMODELNAME";
            OracleParameter[] orapara = new OracleParameter[] { new OracleParameter("DATA", OracleType.Cursor) };
            orapara[0].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];
            ddlModel.DataTextField = "MODEL";
            ddlModel.DataValueField = "MODEL";
            ddlModel.DataSource = dt.DefaultView;
            ddlModel.DataBind();
        }

        private void MultiLanguage()
        {
            lblModel.Text = (String)GetGlobalResourceObject("SFCQuery", "Model");
            rblDataType.Items[0].Text = (String)GetGlobalResourceObject("SFCQuery", "InvoiceNO");
            rblDataType.Items[1].Text = (String)GetGlobalResourceObject("SFCQuery", "WO");
            btnDownLoad.Text = (String)GetGlobalResourceObject("SFCQuery", "DownLoad");
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (tbInvoiceNO.Text.Trim().Equals(""))
            {
                ClsCommon.ShowMessage(this.Page, (String)GetGlobalResourceObject("SFCQuery", "NoInput"));
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "NoInput", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoInput") + "');</script>");
                return;
            }
            string strModel = ddlModel.SelectedValue;
            string strInvoiceNO = tbInvoiceNO.Text.Trim().ToUpper();
            int intSelectIndex = rblDataType.SelectedIndex;
            string strProcedureName = "SFCQUERY.GETFLATFILEDATA";
            OracleParameter[] orapara = new OracleParameter[] {new OracleParameter("MODEL",OracleType.VarChar,10),
                                                            new OracleParameter("INVOICENO",OracleType.VarChar,10),
                                                            new OracleParameter("PORDER",OracleType.VarChar,10),
                                                            new OracleParameter("COUNTRY",OracleType.VarChar,30),
                                                            new OracleParameter("DATA",OracleType.Cursor)};
            orapara[0].Value = strModel;
            switch (intSelectIndex)
            {
                case 0:
                    orapara[1].Value = strInvoiceNO;
                    orapara[2].Value = "";
                    break;
                case 1:
                    orapara[1].Value = "";
                    orapara[2].Value = strInvoiceNO;
                    break;
            }
            orapara[3].Direction = ParameterDirection.Output;
            orapara[4].Direction = ParameterDirection.Output;
            DataTable dt = ClsGlobal.objDataConnect.DataQuery(strProcedureName, orapara).Tables[0];


            //string strConn = "Data Source=TYSFC;Persist Security Info=True;User ID=mcmsmo;Password=mysmo;Unicode=True";
            //OracleConnection conn = new OracleConnection(strConn);
            //OracleCommand cmd = conn.CreateCommand();
            //cmd.CommandText = strProcedureName;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("MODEL", OracleType.NVarChar,10);
            //cmd.Parameters["MODEL"].Direction = ParameterDirection.Input;
            //cmd.Parameters["MODEL"].Value = strModel;
            //switch (intSelectIndex)
            //{
            //    case 0:
            //        cmd.Parameters.Add("INVOICENO", OracleType.NVarChar,10);
            //        cmd.Parameters["INVOICENO"].Direction = ParameterDirection.Input;
            //        cmd.Parameters["INVOICENO"].Value = strInvoiceNO;
            //        cmd.Parameters.Add("PORDER", OracleType.NVarChar,10);
            //        cmd.Parameters["PORDER"].Direction = ParameterDirection.Input;
            //        cmd.Parameters["PORDER"].Value = ""; 
            //        break;
            //    case 1:
            //        cmd.Parameters.Add("INVOICENO", OracleType.NVarChar,10);
            //        cmd.Parameters["INVOICENO"].Direction = ParameterDirection.Input;
            //        cmd.Parameters["INVOICENO"].Value = "";
            //        cmd.Parameters.Add("PORDER", OracleType.NVarChar,10);
            //        cmd.Parameters["PORDER"].Direction = ParameterDirection.Input;
            //        cmd.Parameters["PORDER"].Value = strInvoiceNO;
            //        break;
            //}
            //cmd.Parameters.Add("COUNTRY", OracleType.NVarChar,30);
            //cmd.Parameters["COUNTRY"].Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("DATA", OracleType.Cursor);
            //cmd.Parameters["DATA"].Direction = ParameterDirection.Output;

            //OracleDataAdapter oda = new OracleDataAdapter();
            //oda.SelectCommand = cmd;
            //DataSet ds = new DataSet();
            //oda.Fill(ds);
            //DataTable dt = ds.Tables[0];
            //string strCountry = cmd.Parameters["COUNTRY"].Value.ToString();


            string strCountry = orapara[3].Value.ToString();
            if (dt.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script language=javascript>alert('" + (String)GetGlobalResourceObject("SFCQuery", "NoData") + "');</script>");
                return;

            }

            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = "";
            if (strModel.Equals("GLM"))
                strFileName = "G-24_L_LC_" + dt.Rows[0]["SA_NO"].ToString() + "_" + dt.Rows[0]["INVOICE_NUMBER"].ToString() + ".dat";
            else if (strModel.Equals("SCY") && strCountry.Equals("SINGAPORE"))
                strFileName = "CMCS_" + strInvoiceNO + ".dat";
            else
                strFileName = "CMCS_" + strInvoiceNO + ".txt";

            FileStream fs = File.Create(ExportPath + strFileName);
            //fs.Close();
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                string line = "";
                //寫入文件
                switch (intSelectIndex)
                {
                    case 0:  //以發票號
                        if (strModel.Equals("GLM"))
                        {
                            line = "H," + dt.Rows[0]["CUSTOMER_PO"].ToString() + "," + dt.Rows[0]["SA_NO"].ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd") + "," + DateTime.Now.ToString("HH:mm:ss")
                                + "," + dt.Rows[0]["INVOICE_NUMBER"].ToString() + "," + dt.Rows.Count.ToString();
                            sw.WriteLine(line);
                            line = "";
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                line = "S," + dt.Rows[i]["SERIAL_NUM"].ToString() + "," + dt.Rows[i]["IMEI"].ToString() + "," + dt.Rows[i]["NWSCP"].ToString() + "," + dt.Rows[i]["SSCP"].ToString() + ","
                                    + dt.Rows[i]["SERVICE"].ToString() + "," + dt.Rows[i]["SA_NO"].ToString() + "," + dt.Rows[i]["SOFTWARE_VER"].ToString() + "," + dt.Rows[i]["CARTON_NO"].ToString() + ","
                                    + dt.Rows[i]["MANUFACTURINGDATE"].ToString() + "," + dt.Rows[i]["MANUFACTURINGTIME"].ToString();
                                sw.WriteLine(line);
                                line = "";
                            }
                            line = "";
                            line = "E," + dt.Rows.Count.ToString();
                            sw.WriteLine(line);

                        }
                        else
                        {
                            sw.WriteLine("Addition to " + dt.Rows[0][12].ToString());
                            sw.WriteLine("Date: " + DateTime.Now.ToShortDateString());
                            sw.WriteLine("RecCount: " + dt.Rows.Count);
                            sw.WriteLine(Environment.NewLine);

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                                {
                                    line = line + dt.Rows[i][j].ToString() + "|";
                                }

                                line = line.Substring(0, line.Length - 1);
                                sw.WriteLine(line);
                                line = "";
                            }
                        }
                        break;
                    case 1:  //以工單號
                        //for (int i = 0; i <= dt.Columns.Count - 1; i++)
                        //{
                        //    line = line + dt.Columns[i].ColumnName + "|";
                        //    line = line.Substring(0, line.Length - 1);
                        //    sw.WriteLine(line);
                        //    line = "";
                        //}
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                line = line + dt.Rows[i][j].ToString() + "|";
                            }

                            line = line.Substring(0, line.Length - 1);
                            sw.WriteLine(line);
                            line = "";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                sw.Close();
            }

            //下載文件
            Response.Clear();

            Response.ContentType = "APPLICATION/OCTET-STREAM";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.Charset = "";
            this.EnableViewState = false;
            Response.Flush();
            Response.WriteFile(ExportPath + strFileName);
            Response.End();

        }
    }
}