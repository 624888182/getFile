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
using System.Data.SqlClient;
using Economy.BLL;
using Economy.Publibrary;



/// <summary>
/// Summary description for Users
/// </summary>
 
namespace EconomyUser
{

    public class Userlib
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

    public class UsrCtllib  // 20101011 
    {
        ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        // ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
        public string GetTextByCode( ref string[] ReadTxtArray, int ArrMax, string ParaCode, string dtype ) // dtype : 不傳換 
        {

            int v1 = 0, v2 = 0, v3 = 0;
            string s1 ="", s2="", s3 = "", s4="";
            for ( v1=0; v1 < ArrMax -1; v1++ )
            {   
                  s1 =  ReadTxtArray[v1];
                  if ( (s1 != "") && ( s1 != null )) s2 = s1.Substring(0, 5);
                  else s2 = "";
                  if ( ( s2 == ParaCode ) && ( dtype == "1" ) )
                  {
                       v3 = s1.Length; 
                       s3 = s1.Substring(6, v3 - 5 - 1);
                       v1 = ArrMax;
                  }
            }


            //string t1 = InString.Substring(0, 5); // Pre 5 code 
            //v1 = Convert.ToInt32( t1 );
            //if ((v1 >= 23201) && (v1 <= 23999) && (dtype == "2"))
            //{
            //    retPara = InString.Substring(6, strlen - 5);
            //}

            return (s3);
        
        }

        //   Loc1 = UsrCtllibPointer.GetTextBySite(ref ReadTxtArray, ArrMax, "TJ", "1", ref OraL10DBAMain, ref OraL10DBAStandBy, ref OraL6DBAMain, ref OraL6DBAStandBy, ref OraWebCTLDBA, ref SqlWebCTLDBA, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8 );  // "1" 不傳換
        public string GetTextBySite(ref string[] ReadTxtArray, int ArrMax, string Sitecode, string dtype, ref string OraL10DBAMain, ref string OraL10DBAStandBy, ref string OraL6DBAMain, ref string OraL6DBAStandBy, ref string OraWebCTLDBA, ref string SqlWebCTLDBA, ref string t1, ref string t2, ref string t3, ref string t4, ref string t5, ref string t6, ref string t7, ref string t8 ) // dtype : 不傳換 
        {
            string t11 = "", t22 = "", t33 = "", t44 = "", t55 = "", t66 = "", t77 = "";
            if ( Sitecode == "TJSFC")
            {
                t11 = GetTextByCode(ref ReadTxtArray, ArrMax, "23101", "1");
                t22 = GetTextByCode(ref ReadTxtArray, ArrMax, "23201", "1");
                t33 = GetTextByCode(ref ReadTxtArray, ArrMax, "23202", "1");
                t44 = GetTextByCode(ref ReadTxtArray, ArrMax, "23203", "1");
                t55 = GetTextByCode(ref ReadTxtArray, ArrMax, "23204", "1");
                t66 = GetTextByCode(ref ReadTxtArray, ArrMax, "23205", "1");
                t77 = GetTextByCode(ref ReadTxtArray, ArrMax, "23206", "1");


                // if (t11 == "") return ( t11 );

                if (t22 != "") OraL10DBAMain = t22;
                if (t33 != "") OraL10DBAStandBy = t33;
                if (t44 != "") OraL6DBAMain = t44;
                if (t55 != "") OraL6DBAStandBy = t55;
                if (t66 != "") OraWebCTLDBA = t66;
                if (t77 != "") SqlWebCTLDBA = t77;

            }
            else
            if ( Sitecode == "ZZSFC")
            {
                t11 = GetTextByCode(ref ReadTxtArray, ArrMax, "23101", "1");
                t22 = GetTextByCode(ref ReadTxtArray, ArrMax, "23301", "1");
                t33 = GetTextByCode(ref ReadTxtArray, ArrMax, "23302", "1");
                t44 = GetTextByCode(ref ReadTxtArray, ArrMax, "23303", "1");
                t55 = GetTextByCode(ref ReadTxtArray, ArrMax, "23304", "1");
                t66 = GetTextByCode(ref ReadTxtArray, ArrMax, "23305", "1");
                t77 = GetTextByCode(ref ReadTxtArray, ArrMax, "23306", "1");
                
                // if (t11 == "") return ( t11 );

                if (t22 != "") OraL10DBAMain = t22;
                if (t33 != "") OraL10DBAStandBy = t33;
                if (t44 != "") OraL6DBAMain = t44;
                if (t55 != "") OraL6DBAStandBy = t55;
                if (t66 != "") OraWebCTLDBA = t66;
                if (t77 != "") SqlWebCTLDBA = t77;

            }
            else
            if (Sitecode == "BJTV")
            {
                t11 = GetTextByCode(ref ReadTxtArray, ArrMax, "23101", "1");
                t22 = GetTextByCode(ref ReadTxtArray, ArrMax, "23401", "1");
                t33 = GetTextByCode(ref ReadTxtArray, ArrMax, "23402", "1");
                t44 = GetTextByCode(ref ReadTxtArray, ArrMax, "23403", "1");
                t55 = GetTextByCode(ref ReadTxtArray, ArrMax, "23404", "1");
                t66 = GetTextByCode(ref ReadTxtArray, ArrMax, "23405", "1");
                t77 = GetTextByCode(ref ReadTxtArray, ArrMax, "23406", "1");
                
                // if (t11 == "") return ( t11 );
                if (t22 != "") OraL10DBAMain = t22;
                if (t33 != "") OraL10DBAStandBy = t33;
                if (t44 != "") OraL6DBAMain = t44;
                if (t55 != "") OraL6DBAStandBy = t55;
                if (t66 != "") OraWebCTLDBA = t66;
                if (t77 != "") SqlWebCTLDBA = t77;
            }

            t1 = t11;
            
            return (t11);

        }


        public string CheckServer(ref string OraL10DBAMain, ref string OraL10DBAStandBy, ref string OraL6DBAMain, ref string OraL6DBAStandBy, ref string OraWebCTLDBA, ref string SqlWebCTLDBA, ref string t1, ref string t2, ref string t3, ref string t4, ref string t5, ref string t6, ref string t7, ref string t8) // dtype : 不傳換 
        {
            t1 = "";
            int v1 = 0;
            DataSet ds1 = null;
            string tmpl10 = "select count(*) from PUBLIB.JCOD01 ";
            string tmpl6  = "select count(*) from PUBLIB.L6TOL8T1 ";
            string tmpl6sql  = "select count(*) from JCOD01 ";
            ds1 = DataBaseOperation.SelectSQLDS( "oracle", OraL10DBAMain, tmpl10);
            if (ds1 == null)  t1 = "1";
            else if (ds1.Tables.Count > 0) t1 = "0";

            ds1 = DataBaseOperation.SelectSQLDS("oracle", OraL10DBAStandBy, tmpl10);
            if (ds1 == null) t2 = "1";
            else if (ds1.Tables.Count > 0) t2 = "0";

            ds1 = DataBaseOperation.SelectSQLDS("oracle", OraL6DBAMain, tmpl6);
            if (ds1 == null) t3 = "1";
            else if (ds1.Tables.Count > 0) t3 = "0";

            ds1 = DataBaseOperation.SelectSQLDS("oracle", OraL6DBAStandBy, tmpl6);
            if (ds1 == null) t4 = "1";
            else if (ds1.Tables.Count > 0) t4 = "0";

            ds1 = DataBaseOperation.SelectSQLDS("oracle", OraWebCTLDBA, tmpl6);
            if (ds1 == null) t5 = "1";
            else if (ds1.Tables.Count > 0) t5 = "0";

            ds1 = DataBaseOperation.SelectSQLDS("sql", SqlWebCTLDBA, tmpl6sql);
            if (ds1 == null) t6 = "1";
            else if (ds1.Tables.Count > 0) t6 = "0";

            return( t1+t2+t3+t4+t5+t6 );           


        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Encl String Algorithm : 
        // 1. Input string  default for (v1 = 0; v1 < 20; v1++) trschar[v1] = Convert.ToChar((v1*v1+47) % 255 );
        //    Put Password in this array from start
        // 2. encchar string default  encchar[v1] = Convert.ToChar((v1*v1+4703) % 255 );
        // 3. OutPut string  rchar = trschar[v1] ^= encchar[v1]; 
        // 4. PSType : (Q) Query (U) Update (D) Delete (I) Insert (O) DueDate
        //
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string ChkUsr(string tUsername, string tFPassword, string tSPassword, string tPSDB, string tPSPath, string tPSType)
        {
            int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6;
            int arr15 = 15;
            string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", tCheckSum = "", rets = "";
            char[] trschar = new char[20];  // InPut String 
            char[] encchar = new char[20];  // Encl  string 

            s1 = tUsername; s2 = tFPassword;

            tCheckSum = LibUSR1Pointer.Usr1ConvertPS(s1, s2);
            v1 = (tCheckSum.Trim()).Length;
            if (v1 > 0) for (v2 = 0; v2 < v1; v2++) trschar[v2 + 1] = Convert.ToChar(tCheckSum.Substring(v2, 1));
            tCheckSum = "";
            for (v2 = 1; v2 < arr15 + 1; v2++) tCheckSum = tCheckSum + trschar[v2];

            if (tPSType.ToUpper() == "Q")   // Search  
            {

                s3 = "select * from FUser1 where UserName = '" + s1 + "' ";
                // DataSet tds1 = LibSCM1Pointer.GetDataByDataPath(s3, tPSPath);  // PGetDataByPara
                DataSet tds1 = ShipPlanlib.PGetDataByPara(s3, tPSPath);
                if (tds1.Tables.Count <= 0) rets = "-1"; // Not Data Response.Write("There are not data in FUsrr1 table from coming Para "); //    return;
                else
                {
                    v3 = tds1.Tables[0].Rows.Count;
                    if (v3 <= 0) rets = "-1";  // Not-Found 
                    else
                    {
                        s4 = tds1.Tables[0].Rows[0]["PassWD"].ToString();
                        s5 = tds1.Tables[0].Rows[0]["CheckSumNum"].ToString();
                        v2 = (s5.Trim()).Length;
                        if (v2 > 0) for (v6 = 0; v6 < v2; v6++) encchar[v6 + 1] = Convert.ToChar(s5.Substring(v6, 1));
                        s5 = "";
                        for (v2 = 1; v2 < arr15 + 1; v2++) s5 = s5 + encchar[v2];
                        if ((tCheckSum != s5) || (s4 != s2)) rets = "-1";
                    }

                }

                return (rets);
            } // end if "Q"


            // if ((tPSType.ToUpper() == "U") && ( rets !="-1"))
            if ((tPSType.ToUpper() == "U"))
            {                                                                             // tCheckSum trschar
                s3 = "UPDATE FUser1 SET PassWD = '" + tFPassword + "', CheckSumNum = '" + tCheckSum + "' WHERE UserName = '" + tUsername + "' ";
                if (!LibSCM1Pointer.DBExcuteByDataPath(s3, tPSPath))
                {
                    // Response.Write("<script>alert('Test_record Update 失敗，請稍后重試！')</script>");
                    return ("-1");
                }
                return ("1"); // OK
            }



            // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "', CheckSumNum = Rtrschar WHERE UserName = '" + Username + "' ";
            // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "', CheckSumNum = '" + Rtrschar + "' WHERE UserName = '" + Username + "' ";
            // sql01 = "UPDATE FUser1 SET PassWD = '" + FPassword + "' WHERE UserName = '" + Username + "' ";
            // if (!LibSCM1Pointer.DBExcuteByDataPath(sql01, RunDBPath))
            //    Response.Write("<script>alert('Test_record Update 失敗，請稍后重試！')</script>");



            return (rets);
        }  // end ChkUsr

        // public string GetFirstSPacefromString(string InString, ref string retString)
        public string GetFirstSPacefromString(string InString )
        {
            string Rstr1 = "", R1 = "";

            if (InString == "")
                return (Rstr1);
            
            int v2 = InString.Length;
            int v1 = 0;
            for (v1 = 0; v1 < v2; v1++)
            {
                R1 = InString.Substring(v1, 1);
                if (InString.Substring(v1, 1) == " ") // get the location
                {
                    Rstr1 = v1.ToString();
                    v1 = v2; // Retuen
                }
            }
            
            return (Rstr1);
        } // end GetFirstSPacefromString
    }  // end class UsrCtllib


    public class Checklib
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
                } // end finially
            }
        }  //  end Get_print   
    }  // end class Checklib
}      // EconomyUser
// ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
// ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
// ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
// UsrCtllib UsrCtllibPointer = new UsrCtllib();
