using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
//Create by james 2006/08/09

/// <summary>
/// Tree 的摘要说明

/// </summary>
public class Tree
{
    DbAccessing myDbAccessing = new DbAccessing();
    SecFunction mySecFunction = new SecFunction();
    public static string ApplicationPath = "localhost";
    
	public Tree()
	{
        //if (System.Web.HttpContext.Current.Session["culture"].ToString().ToLower() == "en-us")
        //{
        //    "ModuleNameCn" = "ModuleNameEn";
        //}
        //else
        //{
        //    "ModuleNameCn" = "ModuleNameCn";
        //}        

	}


    //Binding the Tree, display the information of tree .  
    public void BindTree(TreeView treeView)
    {
        Tree tree = new Tree();
        DataTable dtRoot = tree.GetTrees("ROOT");
        treeView.Nodes.Clear();

        foreach (DataRow dr in dtRoot.Rows)
        {
            TreeNode rootnode = new TreeNode();
            rootnode.Value = dr["ModuleCode"].ToString();            
            rootnode.Text = dr["ModuleNameCn"].ToString();

            if ((rootnode.Value == "AA") || (rootnode.Value == "F") || (rootnode.Value == "Y"))
                rootnode.Expanded = false;
            else
                rootnode.Expanded = true;

            rootnode.ImageUrl = "~/App_Themes/SkinFile/images/266.gif";



            rootnode.NavigateUrl = FormatAdminUrl(dr["URL"].ToString());
            rootnode.Target = "_blank";
            rootnode.SelectAction = TreeNodeSelectAction.Expand;

            treeView.Nodes.Add(rootnode);

            //get the Datas of each Node.
            DataTable dtChild = tree.GetTrees(dr["ModuleCode"].ToString().Trim());
            CreateChildNode(rootnode);
        }
    }
    public void BindTreecheck(TreeView treeView)
    {
        Tree tree = new Tree();
        DataTable dtRoot = tree.GetTrees("ROOT");
        treeView.Nodes.Clear();

        foreach (DataRow dr in dtRoot.Rows)
        {
            TreeNode rootnode = new TreeNode();
            rootnode.Value = dr["ModuleCode"].ToString();
            rootnode.Text = dr["ModuleNameCn"].ToString();

            //if ((rootnode.Value == "AA") || (rootnode.Value == "F") || (rootnode.Value == "Y"))
            //    rootnode.Expanded = false;
            //else
                rootnode.Expanded = true;

            rootnode.ImageUrl = "~/App_Themes/SkinFile/images/266.gif";



            //rootnode.NavigateUrl = FormatAdminUrl(dr["URL"].ToString());
             rootnode.Target = "_blank";
            rootnode.SelectAction = TreeNodeSelectAction.Expand;
            treeView.Nodes.Add(rootnode);
            //get the Datas of each Node.
            DataTable dtChild = tree.GetTrees(dr["ModuleCode"].ToString().Trim());
            CreateChildNode_check(rootnode);
        }
    }

    // get the DataTable of each Node's childNode
    public DataTable GetTrees(string ParentCode)
    {



        string strSql = "";



        strSql = "select a.*,a.ModuleCode as ModuleCodeFlag from usySysMenu a where parentmodulecode='"
                + ParentCode + "' order by a.ModuleCode ";
        // DataTable dt = mySecFunction.GetMainLeftTree(ParentCode);//myDbAccessing.ExecuteSqlTable(strSql);
      




        string strProvider = ConfigurationManager.AppSettings["ConnectionSqlServer"];


        SqlConnection myConn = new SqlConnection(strProvider);
        myConn.Open();

        try
        {
            if (myConn.State != ConnectionState.Open)
                myConn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(strSql, myConn);
            DataSet ds = new DataSet("ds");
            sda.Fill(ds);
            return ds.Tables[0];


        }
        catch (System.Data.SqlClient.SqlException e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            myConn.Close();
        }




        



    }



    public void CreateChildNode(TreeNode parentnode)
    {
        DataTable dtChild = GetTrees(parentnode.Value);
        foreach (DataRow row in dtChild.Rows)
        {
            TreeNode tempnode = new TreeNode();
            tempnode.Text = row["ModuleNameCn"].ToString();
            tempnode.Value = row["ModuleCode"].ToString();
            tempnode.Expanded = false;
            tempnode.NavigateUrl = FormatAdminUrl(row["Url"].ToString());
            tempnode.Target = "MainFrame";
            //tempnode.SelectAction = TreeNodeSelectAction.Expand;
            parentnode.ChildNodes.Add(tempnode);
            CreateChildNode(tempnode);
        }
    }

    public void CreateChildNode_check(TreeNode parentnode)
    {
        DataTable dtChild = GetTrees(parentnode.Value);
        foreach (DataRow row in dtChild.Rows)
        {
            TreeNode tempnode = new TreeNode();
            tempnode.Text = row["ModuleNameCn"].ToString();
            tempnode.Value = row["ModuleCode"].ToString();
            tempnode.Expanded = false;
            //tempnode.NavigateUrl = FormatAdminUrl(row["Url"].ToString());
            //tempnode.Target = "MainFrame";
            //tempnode.SelectAction = TreeNodeSelectAction.Expand;
            parentnode.ChildNodes.Add(tempnode);
            CreateChildNode_check(tempnode);
        }
    }

    private string FormatAdminUrl(string adminUrl)
    {
        if (adminUrl.IndexOf("Admins") > -1)
        {
            return ("Http://" + ApplicationPath + "/CostApp" + "/" + adminUrl);
        }
        else
        {
            return (adminUrl);
        }
    }







}
