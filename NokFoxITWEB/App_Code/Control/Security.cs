using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.HtmlControls;

using DBAccess.EAI;

namespace WebControler.Security
{
    //*********************************************************************
    //
    // PortalSecurity Class
    //
    // The PortalSecurity class encapsulates two helper methods that enable
    // developers to easily check the role status of the current browser client.
    //
    //*********************************************************************

    public class PortalSecurity 
	{
        //*********************************************************************
        //
        // PortalSecurity.IsInRole() Method
        //
        // The IsInRole method enables developers to easily check the role
        // status of the current browser client.
        //
        //*********************************************************************

        public static bool IsInRole(string role)
		{
            return HttpContext.Current.User.IsInRole(role);
        }

        //*********************************************************************
        //
        // PortalSecurity.IsInRoles() Method
        //
        // The IsInRoles method enables developers to easily check the role
        // status of the current browser client against an array of roles
        //
        //*********************************************************************

        public static bool IsInRoles(string roles) 
		{
           /* HttpContext context = HttpContext.Current;

            foreach (string role in roles.Split( new char[] {';'} )) 
			{
                if (role != "" && role != null && ((role == "All Users") || (context.User.IsInRole(role)))) 
				{
                    return true;
                }
            }

            return false;*/return true;
        }
    }


    //*********************************************************************
    //
    // UsersDB Class
    //
    // The UsersDB class encapsulates all data logic necessary to add/login/query
    // users within the Portal Users database.
    //
    // Important Note: The UsersDB class is only used when forms-based cookie
    // authentication is enabled within the portal.  When windows based
    // authentication is used instead, then either the Windows SAM or Active Directory
    // is used to store and validate all username/password credentials.
    //
    //*********************************************************************

    public class UsersDB 
	{
		//*********************************************************************
		//
		// UsersDB.GetRolesByUser() Method <a name="GetRolesByUser"></a>
		//
		// The DeleteUser method deleted a  user record from the "Users" database table.
		//
		// Other relevant sources:
		//     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
		//
		//*********************************************************************

        public DataSet GetRolesByUser(string Email) 
        {
			string strSQL = "Select FRoleName from tblUsers "
				+ " WHERE FEMail = " + ClsCommon.GetSqlString(Email);

			return ClsGlobal.objDataConnect.DataQuery(strSQL);
        }

        //*********************************************************************
        //
        // GetSingleUser Method
        //
        // The GetSingleUser method returns a OleDbDataReader containing details
        // about a specific user from the Users database table.
        //
        //*********************************************************************

        public DataSet GetSingleUser(string EMail) 
        {
			string strSQL = "Select FEMail, 'PASSWORD', FName, FE_Mail "
                + " From tblUsers where FEMail = " + ClsCommon.GetSqlString(EMail);

            // Return the DataSet
			try
			{
				return ClsGlobal.objDataConnect.DataQuery(strSQL);
			}
			catch
			{
				return null;
			}
        } 
        //*********************************************************************
        //
        // GetRoles() Method <a name="GetRoles"></a>
        //
        // The GetRoles method returns a list of role names for the user.
        //
        // Other relevant sources:
        //     + <a href="GetRolesByUser.htm" style="color:green">GetRolesByUser Stored Procedure</a>
        //
        //*********************************************************************

        public string[] GetRoles(string Email) 
        {
			string strSQL = "Select station_id from mes_comm_employee "
				+ " WHERE EMP_ID = " + ClsCommon.GetSqlString(Email);

            DataSet dsTmp = ClsGlobal.objDataConnect.DataQuery(strSQL);

            //create a String array from the data
            ArrayList userRoles = new ArrayList();

			for (int i=0; i<dsTmp.Tables[0].Rows.Count; i++)
				userRoles.Add(dsTmp.Tables[0].Rows[i]["station_id"].ToString());

            // Return the String array of roles
            return (string[]) userRoles.ToArray(typeof(String));
        }


        //*********************************************************************
        //
        // UsersDB.Login() Method <a name="Login"></a>
        //
        // The Login method validates a userID/password pair against credentials
        // stored in the users database.  If the userID/password pair is valid,
        // the method returns user's name.
        //
        // Other relevant sources:
        //     + <a href="UserLogin.htm" style="color:green">UserLogin Stored Procedure</a>
        //
        //*********************************************************************

        public string Login(string EMail, string password) 
 		{
			
            string strSQL = "Select LOGINNAME from New_Users where LOGINNAME=" + ClsCommon.GetSqlString(EMail)
				+ " and LOGINPSW=" + ClsCommon.GetSqlString(password) +" AND EFFECTIVE_FROM < SYSDATE AND   EFFECTIVE_TO > SYSDATE";
			DataSet dsName = ClsGlobal.objDataConnect.DataQuery(strSQL);

			if (dsName.Tables[0].Rows.Count > 0)
			{
				return dsName.Tables[0].Rows[0]["LOGINNAME"].ToString();
			}
			else
			{
				return string.Empty;
			}
        }
    }
}