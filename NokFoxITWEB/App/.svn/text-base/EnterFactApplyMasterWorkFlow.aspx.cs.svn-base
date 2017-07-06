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
using FIH.ForeignStaff.db;

public partial class App_EnterFactApplyMasterWorkFlow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ApplyCode = Request.QueryString["ApplyCode"];

        EnterFactApplyMaster myEntityHead = new EnterFactApplyMaster();
        EnterFactApplyMasterInfo myEntityInfoHead = new EnterFactApplyMasterInfo();
        

        if (!IsPostBack)
        {
            try
            {
                lblApplyCode.Text = ApplyCode;
                myEntityInfoHead = myEntityHead.getEnterFactApplyMaster(ApplyCode);
                lblApplyID.Text = myEntityInfoHead.ApplyId + " " + PubFunction.GetEmployeeNameByEmployeeID(myEntityInfoHead.ApplyId);

                lblDivision.Text = myEntityInfoHead.DivisionId + " " + PubFunction.GetEmployeeNameByEmployeeID(myEntityInfoHead.DivisionId);
                if (myEntityInfoHead.DivisionConfirmDate != null)
                {
                    lblDivisionDate.Text = myEntityInfoHead.DivisionConfirmDate.ToString();
                }
                if (myEntityInfoHead.IsBUMgrConfirm==true)
                {
                    fieldBU.Visible = true;
                }
                else
                {
                    fieldBU.Visible = false;
                }

                lblBU.Text = myEntityInfoHead.BUId + " " + PubFunction.GetEmployeeNameByEmployeeID(myEntityInfoHead.BUId);
                if (myEntityInfoHead.BUConfirmDate != null)
                {
                    lblBUDate.Text = myEntityInfoHead.BUConfirmDate.ToString();
                }
                switch (myEntityInfoHead.Status)
                {
                    case 0:
                        rblDivision.Items[0].Selected = false;
                        rblDivision.Items[1].Selected = false;
                        rblBU.Items[0].Selected = false;
                        rblBU.Items[1].Selected = false;
                        break;
                    case 1:
                        rblDivision.Items[0].Selected = true;
                        rblDivision.Items[1].Selected = false;
                        rblBU.Items[0].Selected = false;
                        rblBU.Items[1].Selected = false;
                        break;
                    case 2:
                        rblDivision.Items[0].Selected = true;
                        rblDivision.Items[1].Selected = false;
                        rblBU.Items[0].Selected = true;
                        rblBU.Items[1].Selected = false;
                        break;
                    case 8:
                        rblDivision.Items[0].Selected = false;
                        rblDivision.Items[1].Selected = true;
                        rblBU.Items[0].Selected = false;
                        rblBU.Items[1].Selected = false;
                        break;
                    case 9:
                        rblDivision.Items[0].Selected = true;
                        rblDivision.Items[1].Selected = false;

                        rblBU.Items[0].Selected = false;
                        rblBU.Items[1].Selected = true;
                        break;
                }

            }
            catch
            {
            }
            
        }
    }

}
