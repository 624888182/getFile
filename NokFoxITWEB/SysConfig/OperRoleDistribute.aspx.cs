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

public partial class SysConfig_RoleGroup : System.Web.UI.Page
{
    DbAccessing myDbAccessing = new DbAccessing();
    SecFunction mySecFunction = new SecFunction();

    FIH.Security.db.SysModule mySysModule = new FIH.Security.db.SysModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;
        if (!IsPostBack)
        {
             //邦定ddl控件
           PubFunction.dlstBound(ddlRole, "OperRoleGpCode", "OperRoleGpName","usyOperRoleGp",true);
		
           BindTree();

           //操作權限管控開始------------
           BindOperPermission();
        }
    }

    public void BindOperPermission()
    {
        #region //操作權限管控開始------------
        string isPurview = "";
        string RoleCode = System.Web.HttpContext.Current.Session["RoleCode"].ToString();
        isPurview = SecFunction.HasOperPermissionString(RoleCode, "Z04");
        string isAllPurview = SecFunction.HasAllOperPermissionString("Z04");

        //修改
        if (SecFunction.IsPurview(isAllPurview, "modify"))
        {
            try
            {
                this.btnCommit.Visible = SecFunction.IsPurview(isPurview, "modify");

            }
            catch { }
        }
        //查詢
        if (SecFunction.IsPurview(isAllPurview, "browse"))
        {
            try
            {
                this.ddlRole.Enabled = SecFunction.IsPurview(isPurview, "browse");
            }
            catch { }
        }

        #endregion　//操作權限管控結束------------
    }

    public void BindTree()
    {
        DataTable dataTable = GetTrees();
        TreeView1.Nodes.Clear();
        TreeNode rootnode = new TreeNode();
        //從資源文件SysDisplay抓取《OperRoleDistribute_RoleSetting》的值
        rootnode.Text = GetGlobalResourceObject("SysDisplay", "OperRoleDistribute_RoleSetting").ToString();
        rootnode.Value = "Root";
        rootnode.Expanded = true;
        TreeView1.Nodes.Add(rootnode);
        CreateChildNode(rootnode, dataTable);

        TreeView1.ExpandAll(); 
    }



    public DataTable GetTrees()
    {
        string strCol = "";
        if (Session["culture"].ToString().ToLower() == "en-us")
        {
            strCol = "ModuleNameEn";  
        }
        else
        {
            strCol = "ModuleNameCn"; 
        }
        //得到所有受權限管控的Module集合
        //string strSql = "Select ModuleCode,ParentModuleCode," + strCol + " as ModuleName, OperCodeGroup,IsOperModule from tbrole_SysModule " +
        //    " where IsRole='Y' order by ModuleCode ";
        DataTable dt = mySecFunction.GetTreeTable(strCol);//mySysModule.GetTreeTable(strCol);//myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }


    //得到所有操作的集合
    public DataTable GetOperate()
    {
        //string strSql = "select OperCode,OperName from tbrole_Operate ";
        DataTable dt = mySecFunction.GetOperateTable();//myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }

    //根據節點ID和當前選擇的權限組代碼 得到所有操作Detail的集合
    public DataTable GetRoleCode(string sTreeID)
    {
        //string strSql = "select OperRoleGpCode,ModuleCode,OperCode from tbrole_OperRoleDetail " +
        //    " where OperRoleGpCode='" + this.DdlRole.SelectedValue.ToString() + "'" + " and ModuleCode='" + sTreeID + "'";
        DataTable dt = mySecFunction.GetRoleCode(this.ddlRole.SelectedValue.ToString(), sTreeID);//myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }

    //創立子節點
    public void CreateChildNode(TreeNode parentnode, DataTable dataTable)
    {
        DataRow[] rowList = dataTable.Select("ParentModuleCode='" + parentnode.Value + "'");
        string sOperCode = "";
        string sOperCodeGroup = "";
        DataTable dtOperate = GetOperate(); 
        foreach (DataRow row in rowList)  
        {
            TreeNode tempnode = new TreeNode();
            tempnode.Text = row["ModuleName"].ToString();
            tempnode.Value = row["ModuleCode"].ToString();
            tempnode.ToolTip = "N";
            tempnode.ShowCheckBox = true;
            parentnode.ChildNodes.Add(tempnode);

            DataTable dt = GetRoleCode(row["ModuleCode"].ToString());  
            if (dt.Rows.Count > 0)
            {
                tempnode.Checked = true;
                sOperCode = dt.Rows[0]["OperCode"].ToString();
            }
            else
            {
                tempnode.Checked = false;
                sOperCode = "";
            }

            if (row["IsOperModule"].ToString() == "Y")  
            {
                sOperCodeGroup = row["OperCodeGroup"].ToString();
                foreach (DataRow rowOper in dtOperate.Rows)
                {
                    if (sOperCodeGroup.IndexOf(rowOper["OperCode"].ToString()) != -1)
                    {
                        TreeNode tempnode1 = new TreeNode();
                        tempnode1.Text = rowOper["OperName"].ToString();
                        tempnode1.Value = row["ModuleCode"].ToString();//+rowOper["OperCode"].ToString();//
                        tempnode1.ShowCheckBox = true;
                        tempnode1.ToolTip = "Y";
                        tempnode.ChildNodes.Add(tempnode1);
                        if (sOperCode.IndexOf(rowOper["OperCode"].ToString()) != -1)
                            tempnode1.Checked = true;
                        else
                            tempnode1.Checked = false;
                    }
                }
                tempnode.Expanded = true;
            }
            parentnode.Expanded = true;
            CreateChildNode(tempnode, dataTable);
        }
    }

    //保存，用到遞歸
    public bool SaveChildNode(TreeNode parentnode)
    {
        string sModuleCode;
        string sOperCode;
        string sSQL;
        string sIsOperModule;
        string sRoleCode = this.ddlRole.SelectedValue.ToString();

        try
        {
            for (int i = 0; i < parentnode.ChildNodes.Count; i++)
            {
                if (parentnode.ChildNodes[i].Checked)
                {
                    sModuleCode = parentnode.ChildNodes[i].Value;
                    sIsOperModule = parentnode.ChildNodes[i].ToolTip;
                    if (sIsOperModule == "N")
                    {
                        sSQL = "insert into usyOperRoleDetail(OperRoleGpCode,ModuleCode,OperCode) values(" +
                            "'" + sRoleCode + "','" + sModuleCode + "','')";
                    }
                    else //
                    {
                        DataTable dt = GetRoleCode(sModuleCode);
                        string strSql = "select OperCode,OperName from usyOperate where OperName='" + parentnode.ChildNodes[i].Text + "'";//OperCode='" + parentnode.ChildNodes[i].Value + "'";//
                        DataTable dtOperate = myDbAccessing.ExecuteSqlTable(strSql); 
                        if (dt.Rows.Count > 0)   
                        {
                            sOperCode = dt.Rows[0]["OperCode"].ToString();

                            if (dtOperate.Rows.Count > 0)
                            {
                                if (sOperCode != "")
                                {
                                    sOperCode = sOperCode + "/";
                                }
                                sOperCode = sOperCode + dtOperate.Rows[0]["OperCode"].ToString();
                                sSQL = "update usyOperRoleDetail set OperCode='" + sOperCode + "'" +
                                    " where OperRoleGpCode='" + sRoleCode + "'" +
                                    " and ModuleCode='" + sModuleCode + "'";
                            }
                            else sSQL = "";

                        }
                        else  
                        {
                            if (dtOperate.Rows.Count > 0)
                            {
                                sSQL = "insert into usyOperRoleDetail(OperRoleGpCode,ModuleCode,OperCode) values(" +
                                    "'" + sRoleCode + "','" + sModuleCode + "','" + dtOperate.Rows[0]["OperCode"].ToString() + "')";

                            }
                            else sSQL = "";

                        }
                    }
                    myDbAccessing.ExecuteSql(sSQL);
                    SaveChildNode(parentnode.ChildNodes[i]);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }

    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.ddlRole.SelectedValue.ToString() != "")
        {
            myDbAccessing.ExecuteSql("delete from usyOperRoleDetail where OperRoleGpCode='" + this.ddlRole.SelectedValue.ToString() + "'");
            if (!SaveChildNode(TreeView1.Nodes[0]))
            {
                Response.Write("<script>alert('" + GetGlobalResourceObject("Message", "Save_Fail").ToString() + "')</script>");
                return;
            }
            else
            {
                lblMessage.Text = GetGlobalResourceObject("Message", "Save_Success").ToString();
                //"系統訊息：<b><FONT color= Teal>" +"保存成功！"+ "</FONT></b>";
            }
        }
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTree();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");	
    }
}
