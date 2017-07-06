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
using DBAccess.EAI;
using System.Data.OracleClient;

public partial class Boundary_wfrmShipOrderMaintenance : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiLanguage(); 
        }
    }

    private void MultiLanguage()
    { 
        btnQuery.Text = (String)GetGlobalResourceObject("SFCQuery", "Query");
    }

    private void BindData(string shoporder)
    {
        dgShop.Visible = true;
        string strsql = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO where ORDER_NUMBER='" + txtShoporder.Text.ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            lbcount.Text = "Total:" + dt1.Rows.Count;
            dgShop.DataSource = this.GetIDTable(dt1).DefaultView;
            dgShop.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在該ShopOrder...');</script>");
            return;
        }
    }
    private DataTable GetIDTable(DataTable dt)
    {
        DataColumn col = new DataColumn("新增", Type.GetType("System.Int32"));
        dt.Columns.Add(col);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (0 == i)
                dt.Rows[0][col] = 1;
            else
                dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
        }
        return dt;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string strsql = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO where ORDER_NUMBER = '" + this.txtShoporder.Text.Trim().ToUpper() + "'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgShop.Visible = true;
            lbcount.Text = "Total:" + dt1.Rows.Count;
            lbcount.Visible = true;

            dgShop.DataSource = this.GetIDTable(dt1).DefaultView;
            dgShop.DataBind();
        }
        else
        {
            lbcount.Visible = false;
            dgShop.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('不存在此ShopOrder...');</script>");
            return;
        }
    } 
    protected void dgShop_UpdateCommand(object source, DataGridCommandEventArgs e)
    { 
        string strOrdernumber = ((Label)e.Item.Cells[17].Controls[1]).Text.Trim();

        //string strHModel = ((TextBox)e.Item.Cells[1].Controls[3]).Text;
        //string strHHW = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
        //string strHUp = ((TextBox)e.Item.Cells[3].Controls[3]).Text;
        //string strHLow = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
        //string strHDaughter = ((TextBox)e.Item.Cells[5].Controls[3]).Text;
        //string strHLastby = ((TextBox)e.Item.Cells[6].Controls[1]).Text;

        OracleConnection orcn = null;
        OracleDataAdapter da;
        DataSet myds;
        myds = new DataSet();

        try
        {
            orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tysfc");

            orcn.Open();

            da = new OracleDataAdapter("select * from SFC.CDMA_MOTO_ORDERNO where ORDER_NUMBER='"+strOrdernumber+"'", orcn);
            da.Fill(myds, "CDMA_MOTO_ORDERNO");

            //----------- update part-----------
            //DataTable myDt = myds.Tables["CDMA_MOTO_ORDERNO"];
            //myDt.PrimaryKey = new DataColumn[] { myDt.Columns["ORDER_NUMBER"] };
            //出错就是因为少了上面这一句。这条语句指定了DataTable的主键。或者用下一条语句也可以，下一条语句是让适配器自动加上表的架构（Key约束）
            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey; 

            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["ALLOWABLE_XCVR_MODEL"] = ((TextBox)e.Item.Cells[0].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["ADDON_ID"] = ((TextBox)e.Item.Cells[1].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CONFIG_ID"] = ((TextBox)e.Item.Cells[2].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["DEAKTIVATE"] = ((TextBox)e.Item.Cells[3].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FAMILY"] = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["MULTIUP"] = ((TextBox)e.Item.Cells[5].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["NEXTTEST_VERSION"] = ((TextBox)e.Item.Cells[6].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["RECIPE_DELETE_CHECK"] = ((TextBox)e.Item.Cells[7].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["REL_VERSION"] = ((TextBox)e.Item.Cells[8].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["PROD_UNIT"] = ((TextBox)e.Item.Cells[9].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["LV_ULMA_TYPE"] = ((TextBox)e.Item.Cells[10].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FLASHFILE_NAME"] = ((TextBox)e.Item.Cells[11].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FLASHFILE_PATH"] = ((TextBox)e.Item.Cells[12].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FLEXFILE_NAME"] = ((TextBox)e.Item.Cells[13].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FLEXFILE_PATH"] = ((TextBox)e.Item.Cells[14].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FLEXFILE_VERSION"] = ((TextBox)e.Item.Cells[15].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["LANGUAGE_PACK"] = ((TextBox)e.Item.Cells[16].Controls[3]).Text;
            //ORDER NUMBER
            //myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["ORDER_NUMBER"] = ((TextBox)e.Item.Cells[17].Controls[3]).Text; 
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["ORDER_NUMBER"] = strOrdernumber;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["PRODUCT_FAMILY"] = ((TextBox)e.Item.Cells[18].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["PROTOCOL"] = ((TextBox)e.Item.Cells[19].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["QUANTITY"] = ((TextBox)e.Item.Cells[20].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["QUANTITY_PROCESSED"] = ((TextBox)e.Item.Cells[21].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["RECIPE_NAME"] = ((TextBox)e.Item.Cells[22].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["RECIPE_REVISION"] = ((TextBox)e.Item.Cells[23].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["SITE_CODE"] = ((TextBox)e.Item.Cells[24].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["SWVERSION"] = ((TextBox)e.Item.Cells[25].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["XCVR_MODEL"] = ((TextBox)e.Item.Cells[26].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["SALES_MODEL"] = ((TextBox)e.Item.Cells[27].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["SALES_MODEL_REVISION"] = ((TextBox)e.Item.Cells[28].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TOPTION"] = ((TextBox)e.Item.Cells[29].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TA_HEADER"] = ((TextBox)e.Item.Cells[30].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY1"] = ((TextBox)e.Item.Cells[31].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE1"] = ((TextBox)e.Item.Cells[32].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH1"] = ((TextBox)e.Item.Cells[33].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME1"] = ((TextBox)e.Item.Cells[34].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION1"] = ((TextBox)e.Item.Cells[35].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT1"] = ((TextBox)e.Item.Cells[36].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY2"] = ((TextBox)e.Item.Cells[37].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE2"] = ((TextBox)e.Item.Cells[38].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH2"] = ((TextBox)e.Item.Cells[39].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME2"] = ((TextBox)e.Item.Cells[40].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION2"] = ((TextBox)e.Item.Cells[41].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT2"] = ((TextBox)e.Item.Cells[42].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["IMPORT_DATE"] = ((TextBox)e.Item.Cells[43].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY3"] = ((TextBox)e.Item.Cells[44].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE3"] = ((TextBox)e.Item.Cells[45].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH3"] = ((TextBox)e.Item.Cells[46].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION3"] = ((TextBox)e.Item.Cells[47].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT3"] = ((TextBox)e.Item.Cells[48].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME3"] = ((TextBox)e.Item.Cells[49].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["AKEY1_TYPE"] = ((TextBox)e.Item.Cells[50].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["AKEY2_TYPE"] = ((TextBox)e.Item.Cells[51].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY4"] = ((TextBox)e.Item.Cells[52].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE4"] = ((TextBox)e.Item.Cells[53].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME4"] = ((TextBox)e.Item.Cells[54].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH4"] = ((TextBox)e.Item.Cells[55].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION4"] = ((TextBox)e.Item.Cells[56].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT4"] = ((TextBox)e.Item.Cells[57].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY5"] = ((TextBox)e.Item.Cells[58].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE5"] = ((TextBox)e.Item.Cells[59].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME5"] = ((TextBox)e.Item.Cells[60].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH5"] = ((TextBox)e.Item.Cells[61].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION5"] = ((TextBox)e.Item.Cells[62].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT5"] = ((TextBox)e.Item.Cells[63].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY6"] = ((TextBox)e.Item.Cells[64].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE6"] = ((TextBox)e.Item.Cells[65].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME6"] = ((TextBox)e.Item.Cells[66].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH6"] = ((TextBox)e.Item.Cells[67].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION6"] = ((TextBox)e.Item.Cells[68].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT6"] = ((TextBox)e.Item.Cells[69].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY7"] = ((TextBox)e.Item.Cells[70].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE7"] = ((TextBox)e.Item.Cells[71].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME7"] = ((TextBox)e.Item.Cells[72].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH7"] = ((TextBox)e.Item.Cells[73].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION7"] = ((TextBox)e.Item.Cells[74].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT7"] = ((TextBox)e.Item.Cells[75].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY8"] = ((TextBox)e.Item.Cells[76].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE8"] = ((TextBox)e.Item.Cells[77].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME8"] = ((TextBox)e.Item.Cells[78].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH8"] = ((TextBox)e.Item.Cells[79].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION8"] = ((TextBox)e.Item.Cells[80].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT8"] = ((TextBox)e.Item.Cells[81].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY9"] = ((TextBox)e.Item.Cells[82].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE9"] = ((TextBox)e.Item.Cells[83].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME9"] = ((TextBox)e.Item.Cells[84].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH9"] = ((TextBox)e.Item.Cells[85].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION9"] = ((TextBox)e.Item.Cells[86].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT9"] = ((TextBox)e.Item.Cells[87].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY10"] = ((TextBox)e.Item.Cells[88].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE10"] = ((TextBox)e.Item.Cells[89].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME10"] = ((TextBox)e.Item.Cells[90].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH10"] = ((TextBox)e.Item.Cells[91].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION10"] = ((TextBox)e.Item.Cells[92].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT10"] = ((TextBox)e.Item.Cells[93].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY11"] = ((TextBox)e.Item.Cells[94].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE11"] = ((TextBox)e.Item.Cells[95].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME11"] = ((TextBox)e.Item.Cells[96].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH11"] = ((TextBox)e.Item.Cells[97].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION11"] = ((TextBox)e.Item.Cells[98].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT11"] = ((TextBox)e.Item.Cells[99].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["CATEGORY12"] = ((TextBox)e.Item.Cells[100].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["TYPE12"] = ((TextBox)e.Item.Cells[101].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_NAME12"] = ((TextBox)e.Item.Cells[102].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["FILE_PATH12"] = ((TextBox)e.Item.Cells[103].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["VERSION12"] = ((TextBox)e.Item.Cells[104].Controls[3]).Text;
            myds.Tables["CDMA_MOTO_ORDERNO"].Rows[0]["COMMENT12"] = ((TextBox)e.Item.Cells[105].Controls[3]).Text;  

            //--------end----------


            //----------- Insert new row----------

            //DataRow mydr = myds.Tables["CDMA_MOTO_ORDERNO"].NewRow(); 

            //mydr["ALLOWABLE_XCVR_MODEL"] = ((TextBox)e.Item.Cells[0].Controls[3]).Text;
            //mydr["ADDON_ID"] = ((TextBox)e.Item.Cells[1].Controls[3]).Text;
            //mydr["CONFIG_ID"] = ((TextBox)e.Item.Cells[2].Controls[3]).Text;
            //mydr["DEAKTIVATE"] = ((TextBox)e.Item.Cells[3].Controls[3]).Text;
            //mydr["FAMILY"] = ((TextBox)e.Item.Cells[4].Controls[3]).Text;
            //mydr["MULTIUP"] = ((TextBox)e.Item.Cells[5].Controls[3]).Text;
            //mydr["NEXTTEST_VERSION"] = ((TextBox)e.Item.Cells[6].Controls[3]).Text;
            //mydr["RECIPE_DELETE_CHECK"] = ((TextBox)e.Item.Cells[7].Controls[3]).Text;
            //mydr["REL_VERSION"] = ((TextBox)e.Item.Cells[8].Controls[3]).Text;
            //mydr["PROD_UNIT"] = ((TextBox)e.Item.Cells[9].Controls[3]).Text;
            //mydr["LV_ULMA_TYPE"] = ((TextBox)e.Item.Cells[10].Controls[3]).Text;
            //mydr["FLASHFILE_NAME"] = ((TextBox)e.Item.Cells[11].Controls[3]).Text;
            //mydr["FLASHFILE_PATH"] = ((TextBox)e.Item.Cells[12].Controls[3]).Text;
            //mydr["FLEXFILE_NAME"] = ((TextBox)e.Item.Cells[13].Controls[3]).Text;
            //mydr["FLEXFILE_PATH"] = ((TextBox)e.Item.Cells[14].Controls[3]).Text;
            //mydr["FLEXFILE_VERSION"] = ((TextBox)e.Item.Cells[15].Controls[3]).Text; 
            //mydr["LANGUAGE_PACK"] = ((TextBox)e.Item.Cells[16].Controls[3]).Text;
            ////ORDER NUMBER
            ////mydr["ORDER_NUMBER"] = ((TextBox)e.Item.Cells[17].Controls[3]).Text; 
            //mydr["ORDER_NUMBER"] = strOrdernumber;
            //mydr["PRODUCT_FAMILY"] = ((TextBox)e.Item.Cells[18].Controls[3]).Text;
            //mydr["PROTOCOL"] = ((TextBox)e.Item.Cells[19].Controls[3]).Text;
            //mydr["QUANTITY"] = ((TextBox)e.Item.Cells[20].Controls[3]).Text;
            //mydr["QUANTITY_PROCESSED"] = ((TextBox)e.Item.Cells[21].Controls[3]).Text;
            //mydr["RECIPE_NAME"] = ((TextBox)e.Item.Cells[22].Controls[3]).Text;
            //mydr["RECIPE_REVISION"] = ((TextBox)e.Item.Cells[23].Controls[3]).Text;
            //mydr["SITE_CODE"] = ((TextBox)e.Item.Cells[24].Controls[3]).Text;
            //mydr["SWVERSION"] = ((TextBox)e.Item.Cells[25].Controls[3]).Text;
            //mydr["XCVR_MODEL"] = ((TextBox)e.Item.Cells[26].Controls[3]).Text;
            //mydr["SALES_MODEL"] = ((TextBox)e.Item.Cells[27].Controls[3]).Text;
            //mydr["SALES_MODEL_REVISION"] = ((TextBox)e.Item.Cells[28].Controls[3]).Text;
            //mydr["TOPTION"] = ((TextBox)e.Item.Cells[29].Controls[3]).Text;
            //mydr["TA_HEADER"] = ((TextBox)e.Item.Cells[30].Controls[3]).Text;
            //mydr["CATEGORY1"] = ((TextBox)e.Item.Cells[31].Controls[3]).Text;
            //mydr["TYPE1"] = ((TextBox)e.Item.Cells[32].Controls[3]).Text;
            //mydr["FILE_PATH1"] = ((TextBox)e.Item.Cells[33].Controls[3]).Text;
            //mydr["FILE_NAME1"] = ((TextBox)e.Item.Cells[34].Controls[3]).Text;
            //mydr["VERSION1"] = ((TextBox)e.Item.Cells[35].Controls[3]).Text;
            //mydr["COMMENT1"] = ((TextBox)e.Item.Cells[36].Controls[3]).Text;
            //mydr["CATEGORY2"] = ((TextBox)e.Item.Cells[37].Controls[3]).Text;
            //mydr["TYPE2"] = ((TextBox)e.Item.Cells[38].Controls[3]).Text;
            //mydr["FILE_PATH2"] = ((TextBox)e.Item.Cells[39].Controls[3]).Text;
            //mydr["FILE_NAME2"] = ((TextBox)e.Item.Cells[40].Controls[3]).Text;
            //mydr["VERSION2"] = ((TextBox)e.Item.Cells[41].Controls[3]).Text;
            //mydr["COMMENT2"] = ((TextBox)e.Item.Cells[42].Controls[3]).Text;
            //mydr["IMPORT_DATE"] = ((TextBox)e.Item.Cells[43].Controls[3]).Text;
            //mydr["CATEGORY3"] = ((TextBox)e.Item.Cells[44].Controls[3]).Text;
            //mydr["TYPE3"] = ((TextBox)e.Item.Cells[45].Controls[3]).Text;
            //mydr["FILE_PATH3"] = ((TextBox)e.Item.Cells[46].Controls[3]).Text;
            //mydr["VERSION3"] = ((TextBox)e.Item.Cells[47].Controls[3]).Text;
            //mydr["COMMENT3"] = ((TextBox)e.Item.Cells[48].Controls[3]).Text;
            //mydr["FILE_NAME3"] = ((TextBox)e.Item.Cells[49].Controls[3]).Text;
            //mydr["AKEY1_TYPE"] = ((TextBox)e.Item.Cells[50].Controls[3]).Text;
            //mydr["AKEY2_TYPE"] = ((TextBox)e.Item.Cells[51].Controls[3]).Text;
            //mydr["CATEGORY4"] = ((TextBox)e.Item.Cells[52].Controls[3]).Text;
            //mydr["TYPE4"] = ((TextBox)e.Item.Cells[53].Controls[3]).Text;
            //mydr["FILE_NAME4"] = ((TextBox)e.Item.Cells[54].Controls[3]).Text;
            //mydr["FILE_PATH4"] = ((TextBox)e.Item.Cells[55].Controls[3]).Text;
            //mydr["VERSION4"] = ((TextBox)e.Item.Cells[56].Controls[3]).Text;
            //mydr["COMMENT4"] = ((TextBox)e.Item.Cells[57].Controls[3]).Text;
            //mydr["CATEGORY5"] = ((TextBox)e.Item.Cells[58].Controls[3]).Text;
            //mydr["TYPE5"] = ((TextBox)e.Item.Cells[59].Controls[3]).Text;
            //mydr["FILE_NAME5"] = ((TextBox)e.Item.Cells[60].Controls[3]).Text;
            //mydr["FILE_PATH5"] = ((TextBox)e.Item.Cells[61].Controls[3]).Text;
            //mydr["VERSION5"] = ((TextBox)e.Item.Cells[62].Controls[3]).Text;
            //mydr["COMMENT5"] = ((TextBox)e.Item.Cells[63].Controls[3]).Text;
            //mydr["CATEGORY6"] = ((TextBox)e.Item.Cells[64].Controls[3]).Text;
            //mydr["TYPE6"] = ((TextBox)e.Item.Cells[65].Controls[3]).Text;
            //mydr["FILE_NAME6"] = ((TextBox)e.Item.Cells[66].Controls[3]).Text;
            //mydr["FILE_PATH6"] = ((TextBox)e.Item.Cells[67].Controls[3]).Text;
            //mydr["VERSION6"] = ((TextBox)e.Item.Cells[68].Controls[3]).Text;
            //mydr["COMMENT6"] = ((TextBox)e.Item.Cells[69].Controls[3]).Text;
            //mydr["CATEGORY7"] = ((TextBox)e.Item.Cells[70].Controls[3]).Text;
            //mydr["TYPE7"] = ((TextBox)e.Item.Cells[71].Controls[3]).Text;
            //mydr["FILE_NAME7"] = ((TextBox)e.Item.Cells[72].Controls[3]).Text;
            //mydr["FILE_PATH7"] = ((TextBox)e.Item.Cells[73].Controls[3]).Text;
            //mydr["VERSION7"] = ((TextBox)e.Item.Cells[74].Controls[3]).Text;
            //mydr["COMMENT7"] = ((TextBox)e.Item.Cells[75].Controls[3]).Text;
            //mydr["CATEGORY8"] = ((TextBox)e.Item.Cells[76].Controls[3]).Text;
            //mydr["TYPE8"] = ((TextBox)e.Item.Cells[77].Controls[3]).Text;
            //mydr["FILE_NAME8"] = ((TextBox)e.Item.Cells[78].Controls[3]).Text;
            //mydr["FILE_PATH8"] = ((TextBox)e.Item.Cells[79].Controls[3]).Text;
            //mydr["VERSION8"] = ((TextBox)e.Item.Cells[80].Controls[3]).Text;
            //mydr["COMMENT8"] = ((TextBox)e.Item.Cells[81].Controls[3]).Text;
            //mydr["CATEGORY9"] = ((TextBox)e.Item.Cells[82].Controls[3]).Text;
            //mydr["TYPE9"] = ((TextBox)e.Item.Cells[83].Controls[3]).Text;
            //mydr["FILE_NAME9"] = ((TextBox)e.Item.Cells[84].Controls[3]).Text;
            //mydr["FILE_PATH9"] = ((TextBox)e.Item.Cells[85].Controls[3]).Text;
            //mydr["VERSION9"] = ((TextBox)e.Item.Cells[86].Controls[3]).Text;
            //mydr["COMMENT9"] = ((TextBox)e.Item.Cells[87].Controls[3]).Text;
            //mydr["CATEGORY10"] = ((TextBox)e.Item.Cells[88].Controls[3]).Text;
            //mydr["TYPE10"] = ((TextBox)e.Item.Cells[89].Controls[3]).Text;
            //mydr["FILE_NAME10"] = ((TextBox)e.Item.Cells[90].Controls[3]).Text;
            //mydr["FILE_PATH10"] = ((TextBox)e.Item.Cells[91].Controls[3]).Text;
            //mydr["VERSION10"] = ((TextBox)e.Item.Cells[92].Controls[3]).Text;
            //mydr["COMMENT10"] = ((TextBox)e.Item.Cells[93].Controls[3]).Text;
            //mydr["CATEGORY11"] = ((TextBox)e.Item.Cells[94].Controls[3]).Text;
            //mydr["TYPE11"] = ((TextBox)e.Item.Cells[95].Controls[3]).Text;
            //mydr["FILE_NAME11"] = ((TextBox)e.Item.Cells[96].Controls[3]).Text;
            //mydr["FILE_PATH11"] = ((TextBox)e.Item.Cells[97].Controls[3]).Text;
            //mydr["VERSION11"] = ((TextBox)e.Item.Cells[98].Controls[3]).Text;
            //mydr["COMMENT11"] = ((TextBox)e.Item.Cells[99].Controls[3]).Text;
            //mydr["CATEGORY12"] = ((TextBox)e.Item.Cells[100].Controls[3]).Text;
            //mydr["TYPE12"] = ((TextBox)e.Item.Cells[101].Controls[3]).Text;
            //mydr["FILE_NAME12"] = ((TextBox)e.Item.Cells[102].Controls[3]).Text;
            //mydr["FILE_PATH12"] = ((TextBox)e.Item.Cells[103].Controls[3]).Text;
            //mydr["VERSION12"] = ((TextBox)e.Item.Cells[104].Controls[3]).Text;
            //mydr["COMMENT12"] = ((TextBox)e.Item.Cells[105].Controls[3]).Text; 


            //myds.Tables["CDMA_MOTO_ORDERNO"].Rows.Add(mydr);
            
            // ---------------end -------------------

            OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            da.Update(myds, "CDMA_MOTO_ORDERNO"); 
            //da.Update(myds, "CDMA_MOTO_ORDERNO");
            orcn.Close();           
        }
        
        catch (Exception ex)
        {
            orcn.Close();
            return;
        }
        dgShop.EditItemIndex = -1;
        string strShop = txtShoporder.Text.ToUpper().Trim();
        BindData(strShop);
    }
    protected void dgShop_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        string strShop = txtShoporder.Text.ToUpper().Trim();
        dgShop.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData(strShop);
    }
    protected void dgShop_EditCommand(object source, DataGridCommandEventArgs e)
    {
        string strShop = txtShoporder.Text.ToUpper().Trim();
        dgShop.EditItemIndex = e.Item.ItemIndex;   //更新表格的EditItemIndex属性，获得当前行的内容让其为可以为编　                                                                                                                                                   //辑状态   
        BindData(strShop);
    } 
    protected void dgShop_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgShop.CurrentPageIndex = e.NewPageIndex;
        string strsql = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO where ORDER_NUMBER = '" + this.txtShoporder.Text.Trim().ToUpper() + "'";
        DataTable dt = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dgShop.DataSource = this.GetIDTable(dt).DefaultView;
            dgShop.DataBind();
            lbcount.Text = "Current Page:" + (dgShop.CurrentPageIndex + 1).ToString() + "/" + dgShop.PageCount.ToString() + " Total:" + dt.Rows.Count.ToString();

            dt.Dispose();
        }
        else
        {
            string strsql1 = "SELECT * FROM SFC.CDMA_MOTO_ORDERNO";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            dgShop.DataSource = this.GetIDTable(dt1).DefaultView;
            dgShop.DataBind();
            lbcount.Text = "Current Page:" + (dgShop.CurrentPageIndex + 1).ToString() + "/" + dgShop.PageCount.ToString() + " Total:" + dt1.Rows.Count.ToString();

            dt1.Dispose();
        }
    }
}
