using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainMSPrg_EDI204aspx : System.Web.UI.Page
{
    private static string connType;
    private static string conn;
    private static string dbWrite;
    private static string Autoprg;
    private static string tmpdate;
    private static string BBSCMDIR;
    private static string BegTime;
    private static string EndTime;
    private static string dnid;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            #region 數據庫連接
            string tmp1 = "";
            if (Session["Param1"] != null) tmp1 = Session["Param1"].ToString();

            if (tmp1 == "1")
            {
                connType = Session["Param2"].ToString();
                conn = Session["Param3"].ToString();
                dbWrite = Session["Param4"].ToString();
                Autoprg = Session["Param5"].ToString();
                tmpdate = Session["Param6"].ToString();
                BBSCMDIR = Session["Param7"].ToString();
                dnid = Session["Param8"].ToString();
                BegTime = Session["Param9"].ToString();
                EndTime = Session["Param10"].ToString();
            }
            else if (tmp1 == "")
            {
                connType = "sql";
                conn = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                dbWrite = "Server=10.186.19.205 ;User id=sa;Pwd=Sa123456;Database=IMSCM";//205
                conn = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//205
                dbWrite = "Server=10.83.18.93 ;User id=IMSCM;Pwd=Foxconn88;Database=IMSCM";//205
                BBSCMDIR = "IMSCM";
            }
            else
            {
                Response.Write("<script>alert('You passed string address is not correct,Please login again!')</script>");//您所傳遞的字符串地址不正確，請重新登錄!
                return;
            }
            #endregion

            #region 初始化gridview
            string strdnmt = "select Top 20 WayBillID,IssueDT,ArrivalDT,DNID from " + BBSCMDIR + ".[dbo].[Delivery_Notification_MT] where 1=1";
            if(dnid!="")
            {
                strdnmt = strdnmt + " and DNID ='"+dnid+"'";
            }
            else if (BegTime != "")
            {
                strdnmt = strdnmt + " and IssueDT >='" + BegTime + "'";
            }
            else if(EndTime != "")
            {
                strdnmt = strdnmt + " and IssueDT <='" + EndTime + "'";
            }
            strdnmt = strdnmt + " order by IssueDT desc";
            DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strdnmt);
            string[,] arrF = new string[21, 5];
            arrF[0, 0] = "204 ID";
            arrF[0, 1] = "Picking Date";
            arrF[0, 2] = "Delivery Date";
            arrF[0, 3] = "Load ID";
            arrF[0, 4] = "Delivery Number";
            if(dt.Rows.Count>0)
            {
                for(int ir=0;ir<dt.Rows.Count;ir++)
                {
                    arrF[ir + 1, 0] = dt.Rows[ir]["WayBillID"].ToString().Trim();
                    arrF[ir + 1, 1] = dt.Rows[ir]["IssueDT"].ToString().Trim();
                    arrF[ir + 1, 2] = dt.Rows[ir]["ArrivalDT"].ToString().Trim();
                    arrF[ir + 1, 3] = dt.Rows[ir]["WayBillID"].ToString().Trim();
                    arrF[ir + 1, 4] = dt.Rows[ir]["DNID"].ToString().Trim();
                }
            }
            else
            {
                for (int j = 1; j < 21; j++)
                {
                    for (int i = 0; i < 5; i++)
                        arrF[j, i] = "";
                } 
            }        
            dt = ConvertToDataTable(arrF);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            #endregion
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool b = true;
        string insertDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string[] arrUpdate = new string[5];
        //把因無DN而標記為紅色的欄位,重置為白色
        for (int j = 0; j < 20; j++)
        {
            GridView1.Rows[j].Cells[6].BackColor = Color.White;
        }
        for (int i = 0; i < 20; i++)
        {
            arrUpdate[0] = ((TextBox)GridView1.Rows[i].FindControl("textLoadID")).Text.ToString().Trim();
            arrUpdate[1] = ((TextBox)GridView1.Rows[i].FindControl("textPickingDate")).Text.ToString().Trim();
            arrUpdate[2] = ((TextBox)GridView1.Rows[i].FindControl("textDeliveryDate")).Text.ToString().Trim();
            arrUpdate[3] = ((TextBox)GridView1.Rows[i].FindControl("textLoadID1")).Text.ToString().Trim();
            arrUpdate[4] = ((TextBox)GridView1.Rows[i].FindControl("textDeliveryNumber")).Text.ToString().Trim();

            //判斷有無空數據,為空時退出循環
            if (arrUpdate[0] == "" || arrUpdate[1] == "" || arrUpdate[2] == "" || arrUpdate[3] == "" || arrUpdate[4] == "")
            {

                ((TextBox)GridView1.Rows[i].FindControl("textLoadID")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textLoadID1")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textPickingDate")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textDeliveryDate")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textDeliveryNumber")).Text = "";
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = false;
                if (i == 0)
                {
                    Response.Write("<script>alert('The first line of data can not be empty!')</script>");
                    b = false;
                }
                break;
            }
            else
            {
                arrUpdate[1] = arrUpdate[1].Replace("/", "");
                arrUpdate[2] = arrUpdate[2].Replace("/", "");
                //判斷有無此DN
                string strDN = "select * from " + BBSCMDIR + ".[dbo].[DN_Header] where w0502='" + arrUpdate[4] + "'";
                DataTable dt = PDataBaseOperation.PSelectSQLDT(connType, conn, strDN);
                int idet = dt.Rows.Count;
                //有此DN插入數據
                if (idet > 0)
                {
                    //插入 [IMSCM].[dbo].[EDI204HEADER]
                    #region [IMSCM].[dbo].[EDI204HEADER]
                    //查看EDI204HEADER是否有此BOL 有則更新,沒有insert
                    string strSel = "select B204 from " + BBSCMDIR + ".[dbo].[EDI204HEADER] where B204='" + arrUpdate[0] + "'and rcount='1'";
                    DataTable dt1 = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
                    int idet1 = dt1.Rows.Count;
                    if (idet1 == 0)
                    {
                        string strInsert = string.Format(@"insert into " + BBSCMDIR + ".[dbo].[EDI204HEADER] values('{0}',NULL,'{1}','{2}','{3}','{4}',"+
                                "NULL,NULL,NULL,NULL,NULL,'{5}','{6}','{7}','{8}',NULL,NULL,'{9}','1',NULL,'{10}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)",
                               arrUpdate[0], "DMCQ", "PP", "00", "DMCQ", arrUpdate[1], arrUpdate[2], "N", "N", arrUpdate[3], insertDate);
                        int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strInsert);
                    }
                    else
                    {
                        string strUpdate = "update " + BBSCMDIR + ".[dbo].[EDI204HEADER] set pickdate='" + arrUpdate[1] + "',deliverydate='" + arrUpdate[2] + "',L1101='" + arrUpdate[3] + 
                            "' where B204='" + arrUpdate[0] + "' and rcount='1'";
                        int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strUpdate);
                    }
                    #endregion
                    //插入 [IMSCM].[dbo].[EDI204L11]
                    #region [IMSCM].[dbo].[EDI204L11]
                    //查看EDI204L11是否有此BOL AND DN 無則insert,因EDI204L11表只是BOL AND DN 對應關係,不做update
                    strSel = "select * from " + BBSCMDIR + ".[dbo].[EDI204L11] where B204='" + arrUpdate[0] + "' AND L1101='" + arrUpdate[4] + "' and rcount='1'";
                    DataTable dt2 = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
                    idet1 = dt2.Rows.Count;
                    if (idet1 == 0)
                    {
                        string strInsert = string.Format(@"insert into " + BBSCMDIR + ".[dbo].[EDI204L11] values('{0}','{1}','{2}','{3}','1','{4}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)",
                               arrUpdate[0], arrUpdate[4], "DO", "N",insertDate);
                        int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strInsert);
                    }
                    #endregion
                    //插入 [IMSCM].[dbo].[EDI204S5G62]
                    #region [IMSCM].[dbo].[EDI204S5G62]
                    //查看EDI204S5G62是否有此BOL ,因同一BOL address 固定,所以不做update
                    string strSel3 = "select * from " + BBSCMDIR + ".[dbo].[EDI204S5G62] where B204='" + arrUpdate[0] + "' and rcount='1'";
                    DataTable dt3 = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel3);
                    idet1 = dt3.Rows.Count;
                    //查看Address中是否有此DN對應地址
                    strSel = "select TOP 1 * from " + BBSCMDIR + ".[dbo].[Address] where w0502='" + arrUpdate[4] + "' and addresstype='AG' ";
                    DataTable dt4 = PDataBaseOperation.PSelectSQLDT(connType, conn, strSel);
                    int idet2 = dt4.Rows.Count;
                    //EDI204S5G62中有BOL AND Address有DN的對應地址則更新,無則insert
                    if (idet1 == 0 && idet2 > 0)
                    {
                        string[] arrAddress = new string[7];
                        arrAddress[0] = dt4.Rows[0]["addresstocode"].ToString().Trim();
                        arrAddress[1] = dt4.Rows[0]["addresstoname"].ToString().Trim();
                        arrAddress[2] = dt4.Rows[0]["addressinfodesc"].ToString().Trim();
                        arrAddress[3] = dt4.Rows[0]["cityname"].ToString().Trim();
                        arrAddress[4] = dt4.Rows[0]["statecode"].ToString().Trim();
                        arrAddress[5] = dt4.Rows[0]["postcode"].ToString().Trim();
                        arrAddress[6] = dt4.Rows[0]["countrycode"].ToString().Trim();

                        string strInsert = string.Format(@"insert into " + BBSCMDIR + ".[dbo].[EDI204S5G62] values('{0}','1',NULL,'37',NULL,NULL,NULL,NULL,'ST','{1}','91','IN20',"+
                            "'{2}','{3}','{4}','{5}','{6}','{7}','1','{8}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)",
                               arrUpdate[0], arrAddress[0], arrAddress[1], arrAddress[2], arrAddress[3], arrAddress[4], arrAddress[5], arrAddress[6],insertDate);
                        int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strInsert);
                    }
                    #endregion
                    //插入 [IMSCM].[dbo].[EDI204OIDLAD]
                    #region [IMSCM].[dbo].[EDI204OIDLAD]
                    //從dn_header,DN_detail抓取值
                    string strIDLADData = "select dh.deliverRefer, dd.Mgpartnum,dd.qty,dd.lineitemweight,dd.cartonqty" +
                        " from " + BBSCMDIR + ".dbo.dn_header dh, " + BBSCMDIR + ".dbo.DN_detail dd where dd.w0502 ='" + arrUpdate[4] + "'  and dd.w0502=dh.w0502";
                    DataTable dt5 = PDataBaseOperation.PSelectSQLDT(connType, conn, strIDLADData);
                    int idet3 = dt5.Rows.Count;
                    if (idet3 > 0)
                    {
                        string[] arrIDLAD = new string[5];
                        arrIDLAD[0] = dt5.Rows[0]["deliverRefer"].ToString().Trim();
                        arrIDLAD[1] = dt5.Rows[0]["Mgpartnum"].ToString().Trim();
                        arrIDLAD[2] = dt5.Rows[0]["qty"].ToString().Trim();
                        arrIDLAD[3] = dt5.Rows[0]["lineitemweight"].ToString().Trim();
                        arrIDLAD[4] = dt5.Rows[0]["cartonqty"].ToString().Trim();
                        //查看EDI204OIDLAD中是否已存在此數據有則更新,無則insert
                        string strIDLAD = "select * from " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] where B204='" + arrUpdate[0] + "' AND OID01='" + arrUpdate[4] + "' and rcount='1'";
                        DataTable dt6 = PDataBaseOperation.PSelectSQLDT(connType, conn, strIDLAD);
                        int idet4 = dt6.Rows.Count;
                        if (idet4 > 0)
                        {
                            string strUpdateIDLAD = "update " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] set OID01='" + arrUpdate[4] + "',OID02='" +
                                arrIDLAD[0] + "',OID03='" + arrIDLAD[1] + "',OID05='" + arrIDLAD[2] + "',OID07='" + arrIDLAD[3] +
                                "',LAD02='" + arrIDLAD[4] + "' where B204='" + arrUpdate[0] + "' AND OID01='" + arrUpdate[4] + "' and rcount='1'";
                            int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strUpdateIDLAD);
                        }
                        else
                        {
                            string strInsertIDLAD = string.Format(@"insert into " + BBSCMDIR + ".[dbo].[EDI204OIDLAD] values('{0}','1','1','{1}','{2}','{3}','EA','{4}','L','{5}','CTN','{6}',"+
                                "NULL,NULL,NULL,'{7}',NULL,NULL,NULL,'1',NULL,'1','{8}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)",
                                arrUpdate[0], arrUpdate[4], arrIDLAD[0], arrIDLAD[1], arrIDLAD[2], arrIDLAD[3], arrIDLAD[4],arrUpdate[4],insertDate);
                            int idetCount = PDataBaseOperation.PExecSQL(connType, conn, strInsertIDLAD);
                        }
                    }
                    ((Label)GridView1.Rows[i].FindControl("lblStatus")).Text = "Insert OK";
                    #endregion
                }
                else
                {
                    //沒有DN的欄位標記為紅色
                    GridView1.Rows[i].Cells[6].BackColor = Color.Red;
                    ((Label)GridView1.Rows[i].FindControl("lblStatus")).Text = "DN don't exist";
                    b = false;
                }
            }
        }

        if (b)
        {
            Response.Write("<script>alert('Insert OK!')</script>");
        }
    }

    #region
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Pink'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //单击行的任意列会自动选中此行
            //e.Row.Attributes.Add("onclick", "__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("nowrap", "true");
            }
        }
    }
    public static DataTable ConvertToDataTable(string[,] arr)
    {

        DataTable dataSouce = new DataTable();
        for (int i = 0; i < arr.GetLength(1); i++)
        {
            try
            {
                DataColumn newColumn = new DataColumn(arr[0, i].ToString(), arr[0, 0].GetType());
                dataSouce.Columns.Add(newColumn);
            }
            catch
            {
                continue;
            }
        }
        for (int i = 1; i < arr.GetLength(0); i++)
        {
            DataRow newRow = dataSouce.NewRow();
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                try
                {
                    newRow[arr[0, j].ToString()] = arr[i, j];
                }
                catch
                {
                    continue;
                }
            }
            dataSouce.Rows.Add(newRow);  //  .ItemArray  
        }
        return dataSouce;
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        bool bCheck = ((CheckBox)sender).Checked;

        if (bCheck)
        {
            string[] arrF = new string[5];
            arrF[0] = ((TextBox)GridView1.Rows[0].FindControl("textLoadID")).Text.ToString().Trim();
            arrF[1] = ((TextBox)GridView1.Rows[0].FindControl("textPickingDate")).Text.ToString().Trim();
            arrF[2] = ((TextBox)GridView1.Rows[0].FindControl("textDeliveryDate")).Text.ToString().Trim();
            arrF[3] = ((TextBox)GridView1.Rows[0].FindControl("textLoadID1")).Text.ToString().Trim();
            arrF[4] = ((TextBox)GridView1.Rows[0].FindControl("textDeliveryNumber")).Text.ToString().Trim();
            if (arrF[0] != "" && arrF[1] != "" && arrF[2] != "" && arrF[3] != ""&&arrF[4]!="")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = bCheck;
                    ((TextBox)GridView1.Rows[i].FindControl("textLoadID")).Text = ((TextBox)GridView1.Rows[0].FindControl("textLoadID")).Text.ToString().Trim();
                    ((TextBox)GridView1.Rows[i].FindControl("textPickingDate")).Text = ((TextBox)GridView1.Rows[0].FindControl("textPickingDate")).Text.ToString().Trim();
                    ((TextBox)GridView1.Rows[i].FindControl("textDeliveryDate")).Text = ((TextBox)GridView1.Rows[0].FindControl("textDeliveryDate")).Text.ToString().Trim();
                    ((TextBox)GridView1.Rows[i].FindControl("textLoadID1")).Text = ((TextBox)GridView1.Rows[0].FindControl("textLoadID1")).Text.ToString().Trim();
                }
                return;
            }
            else
            {
                Response.Write("<script>alert('The first line of data can not be empty')</script>");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = false;
                }
                ((CheckBox)sender).Checked = false;
                return;
            }
        }
        else
        {
            ((CheckBox)GridView1.Rows[0].FindControl("CheckBox3")).Checked = bCheck;
            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = bCheck;
                ((TextBox)GridView1.Rows[i].FindControl("textLoadID")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textLoadID1")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textPickingDate")).Text = "";
                ((TextBox)GridView1.Rows[i].FindControl("textDeliveryDate")).Text = "";
            }
        }

    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        //獲取gridview中CheckBox Checked改變行的行號
        CheckBox cb = sender as CheckBox;
        GridViewRow row = cb.NamingContainer as GridViewRow;
        int id = row.RowIndex;
        bool bCheck = ((CheckBox)sender).Checked;
        if (bCheck)
        {
            if (id == 0)
            {
                string[] arrF = new string[5];
                arrF[0] = ((TextBox)GridView1.Rows[id].FindControl("textLoadID")).Text.ToString().Trim();
                arrF[1] = ((TextBox)GridView1.Rows[id].FindControl("textPickingDate")).Text.ToString().Trim();
                arrF[2] = ((TextBox)GridView1.Rows[id].FindControl("textDeliveryDate")).Text.ToString().Trim();
                arrF[3] = ((TextBox)GridView1.Rows[id].FindControl("textLoadID1")).Text.ToString().Trim();
                arrF[4] = ((TextBox)GridView1.Rows[id].FindControl("textDeliveryNumber")).Text.ToString().Trim();
                if (arrF[0] != "" && arrF[1] != "" && arrF[2] != "" && arrF[3] != ""&& arrF[4]!="")
                {
                    return;
                }
                else
                {
                    Response.Write("<script>alert('The first line of data can not be empty')</script>");
                    ((CheckBox)sender).Checked = false;
                    return;
                }
            }
            else
            {
                ((TextBox)GridView1.Rows[id].FindControl("textLoadID")).Text = ((TextBox)GridView1.Rows[0].FindControl("textLoadID")).Text.ToString().Trim();
                ((TextBox)GridView1.Rows[id].FindControl("textLoadID1")).Text = ((TextBox)GridView1.Rows[0].FindControl("textLoadID1")).Text.ToString().Trim();
                ((TextBox)GridView1.Rows[id].FindControl("textPickingDate")).Text = ((TextBox)GridView1.Rows[0].FindControl("textPickingDate")).Text.ToString().Trim();
                ((TextBox)GridView1.Rows[id].FindControl("textDeliveryDate")).Text = ((TextBox)GridView1.Rows[0].FindControl("textDeliveryDate")).Text.ToString().Trim();
                return;
            }
        }
        else
        {
            if (id == 0)
            {
                return;
            }
            else
            {
                ((TextBox)GridView1.Rows[id].FindControl("textLoadID")).Text = "";
                ((TextBox)GridView1.Rows[id].FindControl("textLoadID1")).Text = "";
                ((TextBox)GridView1.Rows[id].FindControl("textPickingDate")).Text = "";
                ((TextBox)GridView1.Rows[id].FindControl("textDeliveryDate")).Text = "";
                return;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ((CheckBox)GridView1.HeaderRow.FindControl("CheckBox4")).Checked = false;
        for (int i = 0; i < 20; i++)
        {
            ((TextBox)GridView1.Rows[i].FindControl("textLoadID")).Text = "";
            ((TextBox)GridView1.Rows[i].FindControl("textLoadID1")).Text = "";
            ((TextBox)GridView1.Rows[i].FindControl("textPickingDate")).Text = "";
            ((TextBox)GridView1.Rows[i].FindControl("textDeliveryDate")).Text = "";
            ((TextBox)GridView1.Rows[i].FindControl("textDeliveryNumber")).Text = "";
            ((CheckBox)GridView1.Rows[i].FindControl("CheckBox3")).Checked = false;
            ((Label)GridView1.Rows[i].FindControl("lblStatus")).Text = "";
            GridView1.Rows[i].Cells[6].BackColor = Color.White;
        }
    }
    #endregion
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

}