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

/// <summary>
/// Summary description for Users
/// </summary>

namespace EconomyUser
{
    public class User
    {
        protected string _id;
        protected string _userName = String.Empty;
        protected string _password = String.Empty;
        
        protected string _UserBU = String.Empty;
        protected string _UserAuthority = String.Empty;
        
        protected string _CardID = String.Empty;
        protected string _UserGroup = String.Empty;
        protected DateTime _CreateDate;
        protected DateTime _lastEditDT;
        protected string _lastEditBy = String.Empty;
                
        
        #region Public Properties
        
        public string UserId
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string UserBU
        {
            get { return _UserBU; }
            set { _UserBU = value; }
        }

        public string UserAuthority
        {
            get { return _UserAuthority; }
            set { _UserAuthority = value; } 
        }

        public string UserGroup
        {
            get { return _UserGroup; }
            set { _UserGroup = value; }
        }

        public string CardID
        {
            get { return _CardID; }
            set { _CardID = value; }
        }

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public string LastEditBy
        {
            get { return _lastEditBy; }
            set { _lastEditBy = value; }
        }

        public DateTime LastEditDT
        {
            get { return _lastEditDT; }
            set { _lastEditDT = value; }
        }

        
        #endregion          
    }
    public class Check
    {
        public bool check(User users)
        {
            try
            {
                if (users.UserName.Trim() != "")
                {
                    return true;
                }
                else
                {
                    return false ;
                }
            }
            catch
            {
                return false;
            }
        }

        public static int weekofyear(DateTime dtime)
        {
            int weeknum = 0;
            DateTime tmpdate = DateTime.Parse(dtime.Year.ToString() + "-1" + "-1");
            DayOfWeek firstweek = tmpdate.DayOfWeek;
            
            //if(firstweek) 
            for (int i = (int)firstweek + 1; i <= dtime.DayOfYear; i = i + 7)
            {
                weeknum = weeknum + 1;
            }
            return weeknum;
        } 


        public bool Get_print(User users, int pageid)
        {
            if (users.UserGroup != "USER")
            {
                return true;
            }
            else
            {
                string ConnString = ConfigurationManager.AppSettings["DefaultConnectionString"];
                SqlConnection scnn = new SqlConnection(ConnString);

                try
                {
                    scnn.Open();
                    string sql = "select a.print_Authority from dbo.User_authority_detail a "
                                  + "where a.groups = '" + users.UserName + "' and a.page_authority = " + pageid;
                    SqlCommand scmm = new SqlCommand(sql, scnn);

                    int count = Convert.ToInt32(scmm.ExecuteScalar());
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    scnn.Close();
                }
                catch
                {
                    scnn.Close();
                    return false;
                }
                finally
                {
                    scnn.Close();
                }
            }
        }
    }
}
