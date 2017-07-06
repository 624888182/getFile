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



    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;



        if (!IsPostBack)
        {
            string sqlBind = "select RoleCode ,RoleName from R_OperRole ";  // Bind the ddlOrgLevel control
            PubFunction.dlstBound(DdlRole, sqlBind);		
            BindTree();


            //權限管控開始------------
            this.btnCommit.Visible = PubFunction.HasOperPermission("Z0404", "M0");
            //權限管控結束------------
        }
    }


    public void BindTree()
    {
        DataTable dataTable = GetTrees();
        TreeView1.Nodes.Clear();
        TreeNode rootnode = new TreeNode();
        rootnode.Text = "權限設置";
        rootnode.Value = "Root";
        rootnode.Expanded = true;
        TreeView1.Nodes.Add(rootnode);
        CreateChildNode(rootnode, dataTable);

        TreeView1.ExpandAll(); 
    }



    public DataTable GetTrees()
    {
        string strCol = "";
        if (Session["Language"].ToString() == "Cn")
        {
            strCol = "ModuleNameCn";  
        }
        else
        {
            strCol = "ModuleNameEn"; 
        }
        string strSql = "Select ModuleCode,ParentModuleCode," + strCol + ",OperCodeGroup,IsOperModule from R_SysModule " +
            " where IsRole='Y' order by ModuleCode ";
        DataTable dt = myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }


    public DataTable GetOperate()
    {
        string strSql = "select OperCode,OperName from R_Operate ";
        DataTable dt = myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }

    public DataTable GetRoleCode(string sTreeID)
    {
        string strSql = "select RoleCode,ModuleCode,OperCode from R_OperRoleDetail " +
            " where RoleCode='" + this.DdlRole.SelectedValue.ToString() + "'" + " and ModuleCode='" + sTreeID + "'";
        DataTable dt = myDbAccessing.ExecuteSqlTable(strSql);
        return (dt);
    }


    public void CreateChildNode(TreeNode parentnode, DataTable dataTable)
    {
        DataRow[] rowList = dataTable.Select("ParentModuleCode='" + parentnode.Value + "'");
        string sOperCode = "";
        string sOperCodeGroup = "";
        DataTable dtOperate = GetOperate(); 
        foreach (DataRow row in rowList)  
        {
            TreeNode tempnode = new TreeNode();
            tempnode.Text = row["ModuleNameCn"].ToString();
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
                        tempnode1.Value = row["ModuleCode"].ToString();//+rowOper["OperCode"].ToString();
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

    public bool SaveChildNode(TreeNode parentnode)
    {
        string sModuleCode;
        string sOperCode;
        string sSQL;
        string sIsOperModule;
        string sRoleCode = this.DdlRole.SelectedValue.ToString();

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
                        sSQL = "insert into R_OperRoleDetail(RoleCode,ModuleCode,OperCode) values(" +
                            "'" + sRoleCode + "','" + sModuleCode + "','')";
                    }
                    else //
                    {
                        DataTable dt = GetRoleCode(sModuleCode);
                        string strSql = "select OperCode,OperName from R_Operate where OperName='" + parentnode.ChildNodes[i].Text + "'";
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
                                sSQL = "update R_OperRoleDetail set OperCode='" + sOperCode + "'" +
                                    " where RoleCode='" + sRoleCode + "'" +
                                    " and ModuleCode='" + sModuleCode + "'";
                            }
                            else sSQL = "";

                        }
                        else  
                        {
                            if (dtOperate.Rows.Count > 0)
                            {
                                sSQL = "insert into R_OperRoleDetail(RoleCode,ModuleCode,OperCode) values(" +
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
        if (this.DdlRole.SelectedValue.ToString() != "")
        {
            myDbAccessing.ExecuteSql("delete from R_OperRoleDetail where RoleCode='" + this.DdlRole.SelectedValue.ToString() + "'");
            if (!SaveChildNode(TreeView1.Nodes[0]))
            {
                Response.Write("<script>alert('對不起! 數據保存失敗!')</script>");
                return;
            }
            else
            {
                lbMessage.Text = "系統訊息：<b><FONT color= Teal>" +"保存成功！"+ "</FONT></b>";
            }
        }
    }
    protected void DdlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTree();
    }
    protected void btnExist_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");	
    }
}
