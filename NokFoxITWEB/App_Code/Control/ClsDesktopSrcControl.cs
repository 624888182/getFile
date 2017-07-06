using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Reflection;
//using DB.EAI;
using DBAccess.EAI;
//using Entity.EAI;


namespace WebControler.DesktopSrcControl
{
	/// <summary>
	/// Summary description for ClsUserPortalDesktopSrcControl.
	/// </summary>
	public class ClsDesktopSrcControl
	{
		public ClsDesktopSrcControl()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// 在設置User的DesktopSrc的時候檢查
		/// 指定User對指定的Menu tabid是否有權限
		/// </summary>
		/// <param name="strUserID"></param>
		/// <param name="intPortalID"></param>
		/// <param name="intTabID"></param>
		/// <returns></returns>
		public bool CheckSetUserDesktopSrc(string strUserID, int intTabID, int intPortalID)
		{
			#region 支持多個Portal, 但每一個模組只能屬於一個Portal
			string strSQL
				= "SELECT COUNT (*) "
				+ "  FROM tbltabs "
				+ " WHERE ftabid = {1} "
				+ "   AND substr(tblTabs.FPortalID3, length(tblTabs.FPortalID3) - {2}, 1) = '1' "
				+ "   AND ftabid NOT IN (SELECT fparentid "
				+ "                        FROM tbltabsdefinitions) "
				+ "   AND fshowtab = 1 "
				+ "   AND (   INSTR (fauthorizedroles || ';', NVL ((SELECT frolename "
				+ "                                                   FROM tblusers "
				+ "                                                  WHERE femail = '{0}'), '*****') || ';') <> 0 "
				+ "        OR INSTR (fauthorizedroles || ';', 'All Users;') <> 0 "
				+ "       ) ";
			#endregion


			strSQL = string.Format(strSQL, strUserID, intTabID, intPortalID);

			try
			{
				return (ClsGlobal.objDataConnect.DataQuery(strSQL).Tables[0].Rows[0][0].ToString() == "1"); 
			}
			catch
			{
				return false;
			}
		}

		public bool CheckUserRedirectToDesktopSrc(string strUserID, int intTabID, int intPortalID)
		{

			#region 支持多個Portal, 但每一個模組只能屬於一個Portal
			string strSQL
				= "SELECT COUNT (*) "
				+ "  FROM tbltabs "
				+ " WHERE ftabid = {1} "
				+ "   AND substr(tblTabs.FPortalID3, length(tblTabs.FPortalID3) - {2}, 1) = '1' "
				+ "   AND ftabid NOT IN (SELECT fparentid "
				+ "                        FROM tbltabsdefinitions) "
				+ "   AND fshowtab = 1 "
				+ "   AND (   INSTR (fauthorizedroles || ';', NVL ((SELECT frolename "
				+ "                                                   FROM tblusers "
				+ "                                                  WHERE femail = '{0}'), '*****') || ';') <> 0 "
				+ "        OR INSTR (fauthorizedroles || ';', 'All Users;') <> 0 "
				+ "       ) ";
			#endregion

			strSQL = string.Format(strSQL, strUserID, intTabID, intPortalID);

			try
			{
				return (ClsGlobal.objDataConnect.DataQuery(strSQL).Tables[0].Rows[0][0].ToString() == "1"); 
			}
			catch
			{
				return false;
			}            
		}
	}
}
