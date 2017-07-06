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

public partial class App_Default : System.Web.UI.Page
{

    DbAccessing DAL = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //InitTree(TreeView1.Nodes, "1");
        }

    }

    //private void InitTree(TreeNodeCollection tnc,String ParentID)
    //{
    //    DataTable dt = DAL.ExecuteSqlTable("select DepartmentID ,DepartmentName,ParentID from usyDepartment where ParentID = '" + ParentID + "'");
        
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            TreeNode tn = new TreeNode();
    //            tn.Text = dr[1].ToString();
    //            tn.Value = dr[0].ToString();
    //            tnc.Add(tn);
    //            InitTree(tn.ChildNodes, dr[0].ToString());
    //        }
    //    }
    //}
}
