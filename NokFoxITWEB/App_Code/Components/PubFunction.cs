using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataReader = System.Data.SqlClient.SqlDataReader;
using System.Data.SqlClient;
using System.Collections;
using System.IO;



/// <summary>
/// PubFunction 的摘要说明
/// </summary>
public class PubFunction
{

    protected static SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionSqlServer"]);
    protected static DataSet ds = new DataSet();
    

	public PubFunction()
	{
       
	}

    public static bool DataExist(string sqlfound)
    {
        SqlDataAdapter da_Exit = new SqlDataAdapter(sqlfound, conn);
        DataSet ds_Exit = new DataSet();
        if(conn.State != ConnectionState.Open)
            conn.Open();
        bool returnValue;
        da_Exit.Fill(ds_Exit, "tableExit");
        if (ds_Exit.Tables["tableExit"].Rows.Count > 0)
        {
            returnValue = true;
        }
        else
        {
            returnValue = false;
        }
        conn.Close();
        return returnValue;
    }


    public static bool dlstBound(DropDownList list, string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            list.Items.Clear();
            list.Items.Add(new ListItem("", ""));
            foreach (DataRow dr in tb.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ListBoxBound(ListBox Lb, string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            Lb.Items.Clear();
            foreach (DataRow dr in tb.Rows)
            {
                Lb.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool TextBound(TextBox list, string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            list.Text = "";
            foreach (DataRow dr in tb.Rows)
            {
                list.Text = dr[0].ToString();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static String StringBound( string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            string str = "";
            foreach (DataRow dr in tb.Rows)
            {
                str = dr[0].ToString();
            }
            return str;
        }
        catch
        {
            return "";
        }
    }

    public static bool dlstBound( DropDownList list,string store, string ID)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable dt = newDbAccessing.ExecuteStoreTable(store, ID);//GetNewGoodsDetailsBodyItem
            list.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;

        }
        catch
        {
            return false;
        }
    }

    public static bool HaveValues(string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);

            if (tb.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public static bool dlstBoundNoEmpty(DropDownList list, string sql)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            list.Items.Clear();
            foreach (DataRow dr in tb.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
 
    //Get ItemNo
    public static string GeItemNO(string TableName, string KeyID,string KeyIDName)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable dt = newDbAccessing.ExecuteGetItemNO(TableName, KeyID,KeyIDName);
            string MaxItemNO = "";
            foreach (DataRow dr in dt.Rows)
            {
                MaxItemNO = dr[0].ToString();
            }
            if (MaxItemNO.Length > 0)
            {
                int item = System.Convert.ToInt32(MaxItemNO) + 1;
                return item.ToString();
            }
            else
                return "1";
        }
        catch
        {
            return "1";
        }
    }

    //Get the Flow ID＋yyyyMMdd+001
    public static string GetFlowID(string TableName, string KeyID)
    {
        try
        {
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable dt = newDbAccessing.ExecuteGetFlowID(TableName, KeyID);
            string MaxID = "";
            string TodayDate = System.DateTime.Now.ToString("yyyyMMdd");
            foreach (DataRow dr in dt.Rows)
            {
                MaxID = dr[0].ToString();
            }
            if (MaxID.Length > 0)
                return TodayDate+PubFunction.FlowIDAddOne(MaxID, 3);
            else
                return TodayDate+PubFunction.FlowIDAddOne("000", 3); ;
        }
        catch
        {
            return "X9912120001";
        }
    }
    public static string FlowIDAddOne(string MaxID,int lenght)
    {
        int i = 0;
        for (int j = 0; j < lenght; j++)
        {
            if (MaxID.Length >= (lenght - j))
            {
                i = System.Convert.ToInt32(MaxID.Substring(MaxID.Length - (lenght - j), lenght - j)) + 1;
                break;
            }
        }
        string zero = "0", zeros = "";
        for (int j = 0; j < lenght - i.ToString().Length; j++)
        {
            zeros += zero;
        }
        MaxID = zeros + i.ToString();
        return MaxID;
    }


    public static bool dlstBound(DropDownList list, string ValuePara, string TextPara, string TableName, bool IsFirstRowNull)
    {
        try
        {
            string sql = "select " + ValuePara + "," + TextPara + " from " + TableName;
            DbAccessing newDbAccessing = new DbAccessing();
            DataTable tb = newDbAccessing.ExecuteSqlTable(sql);
            list.Items.Clear();
            if (IsFirstRowNull == true)
            {
                list.Items.Add(new ListItem("--All--", ""));
            }
            foreach (DataRow dr in tb.Rows)
            {
                list.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }


    public static void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0) return;
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#91cdff';this.style.cursor='hand';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
            
        }
    }

    public static void gvList_RowAction(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0) return;
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            

        }
    }

    public static void gvList_RowCreatedNoMouseHand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0) return;
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#cccccc';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }
    }


    public static void btnAttributes(System.Web.UI.WebControls.Button vBtn)
    {
        vBtn.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.color='#ff0000'");
        vBtn.Attributes.Add("onmouseout", "this.style.color='#000000'");
    }
    /// <summary>
    /// 判断TextBox,可以是否为空，返回值string型：返回错误信息。
    /// </summary>
    /// <param name="myTextBox">TextBox名字</param>
    /// <param name="ErrorName">错误信息名字</param>
    /// <returns></returns>
    public static string JustifyNumTextNotNull(System.Web.UI.WebControls.TextBox myTextBox, decimal StartNum, decimal EndNum, string ErrorName, bool JustifyNull)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (myTextBox.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if (return_value.Trim().ToString() == "")
        {
            try
            {
                decimal dmyTextBox = Convert.ToDecimal(myTextBox.Text.ToString());
                if ((dmyTextBox > EndNum) || (dmyTextBox < StartNum))
                {
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> numerical range must between " + StartNum.ToString() + " and " + EndNum.ToString();
                     }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>數字範圍必須在" + StartNum.ToString() + "和" + EndNum.ToString() + "之間!";
                     }
               }
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be numeric ";                  
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為數字"; 
                }
                
            }
        }
        return return_value;
    }
    public static DataTable ConvertDataReaderToDataTable(IDataReader reader)
    {
        DataTable objDataTable = new DataTable();
        int intFieldCount = reader.FieldCount;
        for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
        {
            objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
        }

        objDataTable.BeginLoadData();

        object[] objValues = new object[intFieldCount];
        while (reader.Read())
        {
            reader.GetValues(objValues);
            objDataTable.LoadDataRow(objValues, true);
        }
        reader.Close();
        objDataTable.EndLoadData();

        return objDataTable;
    }
    public void DeleteFile(string fileName)
    {
        #region 用于刪除所上傳的文件
        try
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
        catch { }
        #endregion
    }
    

    public static bool HasOperPermission(string ModuleCode, string OperCode)
    {
        bool reValue = false;
        DbAccessing newDbAccessing = new DbAccessing();

        if (System.Web.HttpContext.Current.Session["loginUser"].ToString() == "admins")
        {
            reValue = true;
        }
        else
        {
            string strSql = "Select OperCode from R_OperRoleDetail where RoleCode='" + System.Web.HttpContext.Current.Session["RoleCode"].ToString()
                + "' and ModuleCode='" + ModuleCode + "' and OperCode like '%" + OperCode + "%'";
            DataTable dt = newDbAccessing.ExecuteSqlTable(strSql);
            if (dt.Rows.Count > 0)
            {
                reValue = true;
            }
            else
            {
                reValue = false;
            }
        }
        return reValue;
    }

    public static bool HasOperPermission(string RoleCode,string ModuleCode, string OperCode)
    {
        bool reValue = false;
        DbAccessing newDbAccessing = new DbAccessing();

       
            string strSql = "Select OperCode from R_OperRoleDetail where RoleCode='" + RoleCode
                + "' and ModuleCode='" + ModuleCode + "' and OperCode like '%" + OperCode + "%'";
            DataTable dt = newDbAccessing.ExecuteSqlTable(strSql);
            if (dt.Rows.Count > 0)
            {
                reValue = true;
            }
            else
            {
                reValue = false;
            }
        
        return reValue;
    }
    public static string HasOperPermissionString(string RoleCode, string ModuleCode)
    {
        string reValue = "";
        DbAccessing newDbAccessing = new DbAccessing();


        string strSql = "Select OperCode from R_OperRoleDetail where RoleCode='" + RoleCode
            + "' and ModuleCode='" + ModuleCode + "' ";
        DataTable dt = newDbAccessing.ExecuteSqlTable(strSql);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                reValue = dr[0].ToString();
            }
            
        }
        else
        {
            reValue = "";
        }

        return reValue;
    }

    public static bool IsPurview(string Purviews, string OperCode)
    {
        bool reValue = false;
        if (Purviews.Contains(OperCode))
            reValue = true;
        return reValue;
    }

  

    public static string BindOperPermission(System.Web.UI.Page page, string ModuleCode,string Flage)
    {　//僅用于基本資料的操作權限
        #region //操作權限管控開始------------
        string isPurview = "";
        try
        {
            string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
            isPurview = SecFunction.HasOperPermissionString(RoleCode, ModuleCode);
            
            //瀏覽
            Button btnBrowser = (Button)page.FindControl("btnSearch");
            if (btnBrowser != null)
            {
                btnBrowser.Visible = SecFunction.IsPurview(isPurview, "browse");
            }

            //瀏覽
            Button btnColligateSearch = (Button)page.FindControl("btnColligateSearch");
            if (btnColligateSearch != null)
            {
                btnColligateSearch.Visible = SecFunction.IsPurview(isPurview, "browse");
            }

            //重置
            Button btnReset = (Button)page.FindControl("btnReset");
            if (btnReset != null)
            {
                btnReset.Visible = SecFunction.IsPurview(isPurview, "browse");
            }

            //新增
            Button btnAdd = (Button)page.FindControl("btnAdd");
            if (btnAdd != null)
            {
                btnAdd.Visible = SecFunction.IsPurview(isPurview, "add");
            }

            //導出
            Button btnToExcel = (Button)page.FindControl("btnToExcel");
            if (btnToExcel != null)
            {
                btnToExcel.Visible = SecFunction.IsPurview(isPurview, "OutExcel");
            }

            //修改
            Button btnModify = (Button)page.FindControl("btnModify");
            if (btnModify != null)
            {
                btnModify.Visible = SecFunction.IsPurview(isPurview, "modify") || SecFunction.IsPurview(isPurview, "EditCard");
            }

            //修改
            Button btnEdit = (Button)page.FindControl("btnEdit");
            if (btnEdit != null)
            {
                btnEdit.Visible = SecFunction.IsPurview(isPurview, "modify") || SecFunction.IsPurview(isPurview, "EditCard");
            }


            //刪除
            Button btnDelete = (Button)page.FindControl("btnDelete");
            if (btnDelete != null)
            {
                btnDelete.Visible = SecFunction.IsPurview(isPurview, "delete");
            }

            //審核
            Button btnConfirm = (Button)page.FindControl("btnConfirm");
            if (btnConfirm != null)
            {
                btnConfirm.Visible = SecFunction.IsPurview(isPurview, "confirm");
            }
            ImageButton ibtnConfirmY = (ImageButton)page.FindControl("ibtnConfirmY");



            //復審
            Button btnReConfirm = (Button)page.FindControl("btnReConfirm");
            if (btnReConfirm != null)
            {
                btnReConfirm.Visible = SecFunction.IsPurview(isPurview, "ReConfirm");
            }

            //入廠
            Button btnInCompany = (Button)page.FindControl("btnInCompany");
            if (btnReConfirm != null)
            {
                btnReConfirm.Visible = SecFunction.IsPurview(isPurview, "btnInCompany");
            }

            //出廠
            Button btnOutCompany = (Button)page.FindControl("btnOutCompany");
            if (btnReConfirm != null)
            {
                btnReConfirm.Visible = SecFunction.IsPurview(isPurview, "btnOutCompany");
            }
            
            //設定GridView的刪除和修改權限
            GridView myGridView = (GridView)page.FindControl("gvList");
            if (myGridView != null)
            {
                if (Flage == "")
                {
                    myGridView.Columns[0].Visible = SecFunction.IsPurview(isPurview, "delete");
                    myGridView.Columns[1].Visible = SecFunction.IsPurview(isPurview, "modify");
                }
                else if (Flage == "Confirm")
                {
                    myGridView.Columns[0].Visible = SecFunction.IsPurview(isPurview, "confirm");
                    myGridView.Columns[1].Visible = SecFunction.IsPurview(isPurview, "confirm");
                }
                else if (Flage == "InCompany")
                {
                    myGridView.Columns[0].Visible = SecFunction.IsPurview(isPurview, "InCompany");
                }
                else
                {
                    myGridView.Columns[0].Visible = true;
                    myGridView.Columns[1].Visible = true;
                }
            }
            
        }
        catch { }
        return isPurview;
        #endregion　//操作權限管控結束------------
    }
   
    public static bool PathAutoCreate(string fullFileName)
    {
        #region 自動在服務器上創立此目錄
        string pathName = Path.GetDirectoryName(fullFileName);
        if (Directory.Exists(pathName) == false)
        { //若目录不存在，则建立该目录。
            try
            {
                Directory.CreateDirectory(pathName);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        else if (File.Exists(fullFileName) == true)
        { //若指定的文件名已存在，则返回false。
            return false;
        }
        else
        {
            return true;
        }
        #endregion
    }

    public static bool CheckLogin(System.Web.UI.Page page)
    {
        #region 登陸超時代碼
        //page.Session.Timeout = 20;
        //登陸超時代碼
        if ((page.Session["loginUser"] == null) || (page.Session["PageSize"] == null))
        {
            //if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            //{
            //    page.Response.Write("<script>alert('Time out，please again！');top.location.replace('../Default.aspx');</script>");
            //}
            //else
            //{
                page.Response.Write("<script>alert('登陸超時，請重新登陸！');top.location.replace('../Default.aspx');</script>");
            //}
            
            return false;
        }
        else
            return true;
        #endregion
    }

    #region   得到綜合查詢的窗口
    public static string GetPubQueryWin(string PMPath,string TableName,string FromName)
    {
        return "window.open('../PubQuery.aspx?XMLFile=ForeignStaff.xml&TableName=" + TableName + "&ReturnAspx=" + FromName + "','one','width=525,height=415,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
    }
    #endregion
    #region   在GridView中顯示綜合查詢的結果
    public static void ShowPubQueryWin(string SqlQuery, GridView gvList, string TableName)
    {
        if ((SqlQuery != null) && (SqlQuery != ""))
        {
            try
            {
                DbAccessing newDbAccessing = new DbAccessing();
                string sql = " select * from " + TableName + "  where " + SqlQuery + " order by CreaterDate DESC ";
                DataTable dt = newDbAccessing.ExecuteSqlTable(sql);
                gvList.DataSource = dt.DefaultView;
                gvList.DataBind();
                if (!gvList.Visible)
                    gvList.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

    #region   在GridView中顯示綜合查詢的結果
    public static void ShowPubQueryWinBOM(string SqlQuery, GridView gvList, string TableName)
    {
        if ((SqlQuery != null) && (SqlQuery != ""))
        {
            try
            {
                DbAccessing newDbAccessing = new DbAccessing();
                string sql = " select * from " + TableName + "  where " + SqlQuery + "  order by StoreBOMNO desc ";
                DataTable dt = newDbAccessing.ExecuteSqlTable(sql);
                gvList.DataSource = dt.DefaultView;
                gvList.DataBind();
                if (!gvList.Visible)
                    gvList.Visible = true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

    #region   在GridView中顯示綜合查詢的結果
    public static DataTable ShowPubQueryWin1(string SqlSelect,string SqlQuery, GridView gvList, string TableName)
    {
        DataTable dt = new DataTable();
        if ((SqlQuery != null) && (SqlQuery != ""))
        {
            try
            {
                DbAccessing newDbAccessing = new DbAccessing();
                string sql = SqlSelect + " from " + TableName + "  where " + SqlQuery + " order by CreaterDate DESC ";
                 dt = newDbAccessing.ExecuteSqlTable(sql);
           
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return dt;
    }
    #endregion

    public static string JustifyDecimalTwoTextNotNull(System.Web.UI.WebControls.TextBox myTextBox1, System.Web.UI.WebControls.TextBox myTextBox2, string ErrorName, bool JustifyNull)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (myTextBox1.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }

            if (myTextBox2.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if (return_value.Trim().ToString() == "")
        {
            try
            {
                Decimal dmyTextBox1 = Convert.ToDecimal(myTextBox1.Text.ToString());
                Decimal dmyTextBox2 = Convert.ToDecimal(myTextBox2.Text.ToString());
                if (dmyTextBox1 > dmyTextBox2)
                {
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> number is wrong";
                    }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>兩數字大小出錯";
                    }
                }
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be numeric";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為數字";
                }
            }
        }
        return return_value;
    }

    public static string JustifyDecimalTwoTextNotNull(System.Web.UI.WebControls.TextBox myTextBox1, System.Web.UI.WebControls.TextBox myTextBox2, string ErrorName, bool JustifyNull, bool FlagZero)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (myTextBox1.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }

            if (myTextBox2.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if (return_value.Trim().ToString() == "")
        {
            try
            {
                Decimal dmyTextBox1 = Convert.ToDecimal(myTextBox1.Text.ToString());
                Decimal dmyTextBox2 = Convert.ToDecimal(myTextBox2.Text.ToString());
                if ((FlagZero) && (dmyTextBox1 <= 0))
                {
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be plus";
                    }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>該數字不可小于等于零";
                    }
                }
                else if (dmyTextBox1 > dmyTextBox2)
                {
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> number is wrong";
                    }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>兩數字大小出錯";
                    }
                }

            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be numeric";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為數字";
                }
            }
        }
        return return_value;

    }
    public static string JustifyIntTextNotNull(System.Web.UI.WebControls.TextBox myTextBox, int StartNum, int EndNum, string ErrorName, bool JustifyNull)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (myTextBox.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if ((return_value.Trim().ToString() == "") && (myTextBox.Text.Trim().Length > 0))
        {
            try
            {
                Int32 dmyTextBox = Convert.ToInt32(myTextBox.Text.ToString());
                if ((dmyTextBox > EndNum) || (dmyTextBox < StartNum))
                {
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> numerical range must between " + StartNum.ToString() + " and " + EndNum.ToString();
                    }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>數字範圍必須在" + StartNum.ToString() + "和" + EndNum.ToString() + "之間!";
                    }
                }
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be integer";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為整數字";
                }
            }
        }
        return return_value;
    }

    public static string JustifyIntNotNull(System.Web.UI.WebControls.TextBox tb, string ErrorName, bool JustifyNull)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (tb.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if (tb.Text != "")
        {
            try
            {
                int myInt;
                myInt = Convert.ToInt32(tb.Text.Trim());
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be integer";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為整數字";
                }
            }
        }
        return return_value;
    }

    public static string JustifyDecimalNotNull(System.Web.UI.WebControls.TextBox tb, string ErrorName, bool JustifyNull)
    {
        string return_value = "";
        if (JustifyNull == true)
        {
            if (tb.Text.Trim() == "")
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
                }
            }
        }
        if (tb.Text != "")
        {
            try
            {
                decimal myDecimal;
                myDecimal = Convert.ToDecimal(tb.Text.Trim());
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be integer";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為整數字";
                }
            }
        }
        return return_value;
    }

    /// <summary>
    /// 判断TextBox,可以是否为空，返回值string型：返回错误信息。
    /// </summary>
    /// <param name="myTextBox">TextBox名字</param>
    /// <param name="ErrorName">错误信息名字</param>
    /// <returns></returns>
    public static string JustifyTextNotNull(System.Web.UI.WebControls.TextBox myTextBox, string ErrorName)
    {
        string return_value = "";
        if (myTextBox.Text.Trim() == "")
        {
            if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
            }
            else
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
            }
        }
        return return_value;
    }


    public static string JustifyDropdownlistNotNull(System.Web.UI.WebControls.DropDownList myDropDownList, string ErrorName)
    {
        string return_value = "";
        if (myDropDownList.SelectedValue.Trim() == "")
        {
            if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> not null！";
            }
            else
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>不能為空！";
            }
        }
        return return_value;
    }


    public static string JustifyDatetime(System.Web.UI.WebControls.TextBox tb, string ErrorName)
    {
        string return_value = "";
        if (tb.Text.Trim() != "")
        {
            try
            {
                DateTime dtDate;
                dtDate = DateTime.Parse(tb.Text.Trim());
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be date！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為日期類型！";
                }
            }
        }
        return return_value;
    }

    public static string JustifyDatetimeAfterNow(System.Web.UI.WebControls.TextBox tb, string ErrorName)
    {
        string return_value = "";
        if (tb.Text.Trim() != "")
        {
            try
            {
                DateTime dtDate;
                dtDate = DateTime.Parse(tb.Text.Trim());
                if (dtDate < System.Convert.ToDateTime(System.DateTime.Now.ToShortDateString()))
                    if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must greater than today！";
                    }
                    else
                    {
                        return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須大于當前日期！";
                    }
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be date！";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為日期類型！";
                }
            }
        }
        return return_value;
    }

    public static string JustifyDecimal(System.Web.UI.WebControls.TextBox tb, string ErrorName)
    {
        string return_value = "";
        if (tb.Text.Trim() != "")
        {
            try
            {
                decimal myDecimal;
                myDecimal = Convert.ToDecimal(tb.Text.Trim());
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be numeric";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為數值類型";
                }
            }
        }
        return return_value;
    }

    public static string JustinfyTwoDateTime(string start, string end)
    {
        string return_value = "";
        try
        {
            if (System.Convert.ToDateTime(start) <= System.Convert.ToDateTime(end))
                return_value = "";
            else if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            {
                return_value = "Beginning date greater than end date！";
            }
            else
            {
                return_value = "開始時間不可大于結束時間！";
            }
        }
        catch
        {
            if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            {
                return_value = "Date format is wrong";
            }
            else
            {
                return_value = "時間格式有誤";
            }

        }
        return return_value;
    }

    public static string JustifyInt(System.Web.UI.WebControls.TextBox tb, string ErrorName)
    {
        string return_value = "";
        if (tb.Text.Trim() != "")
        {
            try
            {
                decimal myDecimal;
                myDecimal = Convert.ToInt32(tb.Text.Trim());
            }
            catch
            {
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> must be numeric";
                }
                else
                {
                    return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>必須為數值類型";
                }
            }
        }
        return return_value;
    }

    public static string JustinfyStringLength(string str, string ErrorName, int length)
    {
        string return_value = "";
        try
        {
            if ((str.Trim().Length > 0) && (str.Trim().Length == length))
                return_value = "";
            else
                if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
                {
                    return_value = "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>" + " length is not " + length.ToString();
                }
                else
                {
                    return_value = "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>" + "長度不為" + length.ToString();
                }

        }
        catch
        {
            if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b> type is wrong！";
            }
            else
            {
                return_value += "<b><FONT color= #ff0000>" + ErrorName + "</FONT></b>類型錯誤！";
            }
        }
        return return_value;
    }

    /// <summary>
    /// 得到員工姓名
    /// </summary>
    /// <param name="employeeID">員工工號</param>
    /// <returns></returns>
    public static string GetEmployeeNameByEmployeeID(string employeeID)
    {
        DbAccessing DAL = new DbAccessing();
        string employeeName = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select UserName from usyUsers where UserID = '" + employeeID + "'");
            if (dt.Rows.Count > 0)
            {
                employeeName = dt.Rows[0]["UserName"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
        return employeeName;
    }


    public static string GetDeptNameByDeptID(string deptID)
    {
        DbAccessing DAL = new DbAccessing();
        string DeptName = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select DepartmentName from usyDepartment where DepartmentID = '"+deptID+"'");
            if (dt.Rows.Count > 0)
            {
                DeptName = dt.Rows[0]["DepartmentName"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
        return DeptName;
    }

    public static string GetEnterReasonNameByID(string reasonID)
    {
        DbAccessing DAL = new DbAccessing();
        string Name = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select Description from pubEnterFactReason where ReasonCode = '" + reasonID + "'");
            if (dt.Rows.Count > 0)
            {
                Name = dt.Rows[0]["Description"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
        return Name;
    }

    public static string GetGateHouseNameByID(string gateID)
    {
        DbAccessing DAL = new DbAccessing();
        string Name = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select Description from pubGatehouse where GatehouseCode = '" + gateID + "'");
            if (dt.Rows.Count > 0)
            {
                Name = dt.Rows[0]["Description"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
        return Name;
    }


    public static string GetStatusByApplyCode(string applyCode)
    {
        DbAccessing DAL = new DbAccessing();
        string rtn = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select Status from appEnterFactApplyMaster where ApplyCode = '" + applyCode + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["Status"].ToString();
            }
        }
        catch { }
        return rtn;
    }

    public static string GetIsBuByApplyCode(string applyCode)
    {
        DbAccessing DAL = new DbAccessing();
        string rtn = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select IsBUMgrConfirm from appEnterFactApplyMaster where ApplyCode = '" + applyCode + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["IsBUMgrConfirm"].ToString();
            }
        }
        catch { }
        return rtn;
    }


    public static string GetCardStatusByItemNo(string ApplyCode,string ItemNo)
    {
        DbAccessing DAL = new DbAccessing();
        string rtn = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select CardStatus from appEnterFactApplyDetail where ApplyCode = '" + ApplyCode + "' and ItemNo = '" + ItemNo + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["CardStatus"].ToString();
            }
        }
        catch { }
        return rtn;
    }

    public static string GetStatusNameByStatus(string status,string IsBU)
    {
        string rtn = "";
        switch (IsBU)
        {
            case "N":
                switch (status)
                {
                    case "0":
                        rtn = "初始狀態";
                        break;
                    case "1":
                        rtn = "廠(部)通過";
                        break;
                    case "2":
                        rtn = "處級通過";
                        break;
                    case "8":
                        rtn = "廠(部)拒絕";
                        break;
                    case "9":
                        rtn = "處級拒絕";
                        break;
                }
                break;
            case "Y":
                switch (status)
                {
                    case "0":
                        rtn = "初始狀態";
                        break;
                    case "1":
                        rtn = "廠(部)通過";
                        break;
                    case "2":
                        rtn = "處級通過";
                        break;
                    case "8":
                        rtn = "廠(部)拒絕";
                        break;
                    case "9":
                        rtn = "處級拒絕";
                        break;
                }
                break;
        }
        return rtn;
    }

}
