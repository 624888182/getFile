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
using System.Drawing;

using System.Data.SqlClient;

using SQLHelperA;
using System.Globalization;
using FIH.Security.db;
using DataReader = System.Data.SqlClient.SqlDataReader;



public partial class DepRoleDistributeAdd : System.Web.UI.Page
{

    FIH.Security.db.DeptRoleGp myEntity = new DeptRoleGp();
    FIH.Security.db.DeptRoleGpInfo myEntityInfo = new DeptRoleGpInfo();

    DbAccessing db = new DbAccessing();
    string StrAction = "";     string StrKeyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;

        StrAction = Request.QueryString["action"];
        if (!IsPostBack)
        {
            //邦定部門權限CheckBoxList
            
            BindCheckBoxList();

            if (StrAction == "edit")
            {
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                
                textUnable(true);
          
                BindData(StrKeyId);  
            }
            if (StrAction == "view")
            {
                StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
                 textUnable(false);
                btnCommit.Visible = false;

                BindData(StrKeyId);
            }
            if (StrAction == "add")
            {
               textUnable(true);
               

               //tbDivNo.Text = this.CheckBoxList1.Items.Count.ToString();
            }
        }
        
    }



    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lbMessage.Text = CommitError().Trim();
            return;
        }

//CHECKBOXLIST 部門勾選
        tbDivNo.Text = "";
        for(int   i=0;i<this.CheckBoxList1.Items.Count;i++)   
     {
      if (this.CheckBoxList1.Items[i].Selected)   
          {
              tbDivNo.Text += this.CheckBoxList1.Items[i].Value + "/";
          
          }   
     } 

        
        string StrAction = Request.QueryString["action"];

        myEntityInfo.DeptRoleGpCode = tbDeptRoleGpCode.Text.Trim().ToString();
        myEntityInfo.DeptRoleGpName = tbDeptRoleGpName.Text.Trim().ToString();
        myEntityInfo.DivNo = tbDivNo.Text.Trim().ToString();

        if (StrAction == "edit")
        {
            //myEntityInfo.ModiID = Session["loginUser"].ToString();
            //myEntityInfo.ModiDate = DateTime.Now;

            myEntity.Update(myEntityInfo);
            Response.Write("<script>window.opener.document.formBu.submit();</script>");
            lbMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();
            tbDivNo.Text = "";
            
        }
        else if (StrAction == "add")
        {
            try
            {
                //myEntityInfo.CreaterID = Session["loginUser"].ToString();
                //myEntityInfo.CreaterDate = DateTime.Now;

                myEntity.Insert(myEntityInfo);
                clearText(sender,e);
                Response.Write("<script>window.opener.document.formBu.submit();</script>");
                lbMessage.Text = GetGlobalResourceObject("Message", "Add_Success").ToString();
                //btnCommit.Visible = false; textUnable(false);
                
                
  
            }
            catch 
            {
                lbMessage.Text = GetGlobalResourceObject("Message", "Add_Fail").ToString();
            }
     }


    }
    protected void btnExist_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    #region  失效设置
    private void textUnable(bool isAble)
    {
        tbDeptRoleGpCode.Enabled = tbDeptRoleGpName.Enabled = tbDivNo.Enabled =CheckBoxList1.Enabled =  isAble;
        System.Drawing.Color myColor = new Color();
        myColor = (isAble == true) ? Color.Red : Color.Black;
        lbDeptRoleGpCode.ForeColor = lbDeptRoleGpName.ForeColor = lbDivNo.ForeColor = myColor;
        string StrAction = Request.QueryString["action"];
        if (StrAction == "edit")
        {
            tbDeptRoleGpCode.Enabled = false;
        }
    }
    #endregion

    #region   清除 text
    private void clearText(object sender, System.EventArgs e)
    {
        tbDeptRoleGpCode.Text = tbDeptRoleGpName.Text =   "";
     
    }

    #endregion

    #region   数据绑定
    private void BindData(string sCondition)
    {
        myEntityInfo = myEntity.getDeptRoleGp(sCondition);
        tbDeptRoleGpCode.Text = myEntityInfo.DeptRoleGpCode;
        tbDeptRoleGpName.Text = myEntityInfo.DeptRoleGpName;
        //tbDivNo.Text = myEntityInfo.DivNo;
        //tbDivNo.Text = this.CheckBoxList1.Items.Count.ToString();


        string strDept = GetRoleCode();
        string str = "";
        for ( int i = 0; i < this.CheckBoxList1.Items.Count; i++)  
        {
            str = this.CheckBoxList1.Items[i].Value.ToString();
            if (PubFunction.IsPurview(strDept, str))
            {
                CheckBoxList1.Items[i].Selected = true;
            }
            //DataTable dt = GetRoleCode(str);

            //if (dt.Rows.Count > 0)
            //{
            //    CheckBoxList1.Items[i].Selected = true;
            //}
         }

    }


    #endregion


    public DataTable GetRoleCode(string sTreeID)
    {
        string strSql = "";
        
        strSql = "select DivNo  from tbrole_DeptRoleGp " +
        " where  DivNo  like '%" + sTreeID + "%' and DeptRoleGpCode = '" +tbDeptRoleGpCode.Text.Trim().ToString() +"'";

        DataTable dt = db.ExecuteSqlTable(strSql);

        return (dt);
    }

    public string GetRoleCode()
    {
        string strSql = "";

        strSql = "select DivNo  from tbrole_DeptRoleGp " +
        " where   DeptRoleGpCode = '" + tbDeptRoleGpCode.Text.Trim().ToString() + "'";

        DataTable dt = db.ExecuteSqlTable(strSql);
        foreach (DataRow dr in dt.Rows)
        {
            strSql=  dr[0].ToString();
        }
        return (strSql);
    }

    #region   数据验证
    private string CommitError()
    {
        string return_value = "";
        lbMessage.Text = "";
        if (tbDeptRoleGpCode.Text.Trim().ToString() == "") { return_value += lbDeptRoleGpCode.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
        if (tbDeptRoleGpName.Text.Trim().ToString() == "") { return_value += lbDeptRoleGpName.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }
       // if (tbDivNo.Text.Trim().ToString() == "") { return_value += lbDivNo.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString(); }

        return return_value;
    }
    #endregion


    private void BindCheckBoxList( )
    {
        string sqlBind = "select DepartmentID,DepartmentName from tbbase_Department";
        DataTable tb = db.ExecuteSqlTable(sqlBind);
        CheckBoxList1.DataSource = tb.DefaultView;
        CheckBoxList1.DataValueField = "DepartmentID";
        CheckBoxList1.DataTextField = "DepartmentName";
        CheckBoxList1.DataBind();
    }

}

