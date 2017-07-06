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

    public class Convertlib  // 20120701 
    {
        ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        

        ////////////////////////////////////////////////////////////////
        // Algorithm : 
        // Para1 : Input String
        // Para2 : Convert Code
        // Para3 : Covvert Data Spilt 
        // Para4 : Protect type
        // Read Para1 XOR Para2 不同得 1, 相同為 0, 將每個加密依 Splitcode 分開
        // Para4 : 加強加密方法 1ken 加入並用第 1 code 表方法, 第 2 code 表 model 10 定出第幾位置 ken 
        // // <add key="LicenseString" value="License Document By IT Team 20120131 Donot Copy and Delete this file"/>
        public string EncCode(string PPassword, string Eclcode, string StringSpilt, string pway )
        {
            int v1 = 0, v2 = 0, v3 = 0;
            string s1 = "", s2 = "";
            string s3 = "", s4 = "";
            int v8 = 0, v9 = 0;
                             
            v2 = PPassword.Length;
            v3 = Eclcode.Length;

            char[] PPasswordarr = new char[v2];  // char 內放 Asc Value
            char[] Eclcodearr = new char[v2];
            char[] ProtPSarr = new char[v2];
            char[] CProtPSarr = new char[v2];
            int arrcnt = v2;

            if (v2 >= arrcnt) v2 = arrcnt;
            if (v3 >= arrcnt) v3 = arrcnt;

            for (v1 = 0; v1 < v2; v1++) PPasswordarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));
            for (v1 = 0; v1 < v3; v1++) Eclcodearr[v1] = Convert.ToChar(Eclcode.Substring(v1, 1));
            for (v1 = 0; v1 < v2; v1++) ProtPSarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));

            for (v1 = 0; v1 < arrcnt; v1++) ProtPSarr[v1] ^= Eclcodearr[v1];
            for (v1 = 0; v1 < arrcnt; v1++) CProtPSarr[v1] = ProtPSarr[v1];
            for (v1 = 0; v1 < arrcnt; v1++) CProtPSarr[v1] ^= Eclcodearr[v1];

            for (v1 = 0; v1 < arrcnt; v1++) s2 = s2 + (Convert.ToInt32(ProtPSarr[v1])).ToString() + StringSpilt;  // Test 
            // for (v1 = 0; v1 < arrcnt; v1++) s1 = s1 + (Convert.ToInt32(ProtPSarr[v1] + 1 + v1)).ToString() + StringSpilt; // Add CNT

            return (s2);
            // s2 = Convert.ToChar(tUsername.Substring(0, 1));
        }  // end PSaddEclCode


        public string DeEncCode(string PPassword, string Eclcode, string StringSpilt, string pway)
        {
            int v1 = 0, v2 = 0, v3 = 0,v4=0, Shead5=5;
            string s1 = "", s2 = "";
            string s3 = "", s4 = "";
            int v8 = 0, v9 = 0;
         

            v2 = PPassword.Length;
            v3 = Eclcode.Length;
            int head1=0, tail1=0, arri=0;
            string tmpc="", tmpd="";

            char[] PPasswordarr = new char[v2];  // char 內放 Asc Value
            char[] Eclcodearr = new char[v2];
            char[] ProtPSarr = new char[v2];
            int arrcnt = v2;

            if (v2 >= arrcnt) v2 = arrcnt;
            if (v3 >= arrcnt) v3 = arrcnt;

            // head1 = Shead5+1;
            for (v1 = 0; v1 < v2; v1++)
            {  
                tmpc = PPassword.Substring(head1,1);
                if (tmpc != StringSpilt)
                {
                    tmpd = tmpd + tmpc;
                }
                else
                {
                    v4 = Convert.ToInt32(tmpd);
                    PPasswordarr[arri] = Convert.ToChar(v4);
                    ProtPSarr[arri] = Convert.ToChar(v4);
                    arri++;
                    tmpd = "";
                }
                head1++;
                //PPasswordarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));
         }
            for (v1 = 0; v1 < v3; v1++) Eclcodearr[v1] = Convert.ToChar(Eclcode.Substring(v1, 1));
            // for (v1 = 0; v1 < v2; v1++) ProtPSarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));

            for (v1 = 0; v1 < arrcnt; v1++) ProtPSarr[v1] ^= Eclcodearr[v1];

            for (v1 = 0; v1 < arri; v1++) s2 = s2 + ProtPSarr[v1].ToString();  // 取的有效 Character
            return (s2);
             // s2 = Convert.ToChar(tUsername.Substring(0, 1));
        }  // end PSaddEclCode



        ////////////////////////////////////////////////////////////////
        // Algorithm : 
        // Para1 : Input String
        // Para2 : Convert Code
        // Para3 : Covvert Data Spilt 
        // Para4 : Protect type
        // Read Para1 XOR Para2 不同得 1, 相同為 0, 將每個加密依 Splitcode 分開
        // Para4 : 加強加密方法 1ken 加入並用第 1 code 表方法, 第 2 code 表 model 10 定出第幾位置 ken 
        // // <add key="LicenseString" value="License Document By IT Team 20120131 Donot Copy and Delete this file"/>
        // public string DBEncCode(string PPassword, string Eclcode, string StringSpilt, string pway)
        // {
        //     int v1 = 0, v2 = 0, v3 = 0;
        //     string s1 = "", s2 = "";
        //     string s3 = "", s4 = "";
        //     int v8 = 0, v9 = 0;
        //     string s5 = "";
                   
            // 如果不為空白就加入此直在第 ? 開始
        //     if ((pway.Substring(0, 1) == "2") && (pway.Length > 1))
        //     {
        //         char[] Ecode = new char[2];                          // 找第一字
        //         Ecode[0] = Convert.ToChar(pway.Substring(1, 1));     // Char 轉 Digital
        //         v8 = Convert.ToInt32(Ecode[0]);                      // Model 10  
        //         v9 = v8 % 10;  // model 10 
        //         if (Eclcode.Length > v9)
        //         {
        //             s4 = Eclcode.Substring(0, v9 - 1) + pway.Substring(1, pway.Length - 1) + Eclcode.Substring(v9 - 1, Eclcode.Length - v9 + 1);
        //             Eclcode = s4;
        //         }
        //     }


        //     v2 = PPassword.Length;
        //     v3 = Eclcode.Length;

        //     char[] PPasswordarr = new char[v2];  // char 內放 Asc Value
        //     char[] Eclcodearr = new char[v2];
        //     char[] ProtPSarr = new char[v2];
        //     char[] CProtPSarr = new char[v2];
        //     int arrcnt = v2;

        //     if (v2 >= arrcnt) v2 = arrcnt;
        //     if (v3 >= arrcnt) v3 = arrcnt;

        //     for (v1 = 0; v1 < v2; v1++) PPasswordarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));
        //     for (v1 = 0; v1 < v3; v1++) Eclcodearr[v1] = Convert.ToChar(Eclcode.Substring(v1, 1));
        //     for (v1 = 0; v1 < v2; v1++) ProtPSarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));

        //     for (v1 = 0; v1 < arrcnt; v1++) ProtPSarr[v1] ^= Eclcodearr[v1];
        //     for (v1 = 0; v1 < arrcnt; v1++) CProtPSarr[v1] = ProtPSarr[v1];
        //     for (v1 = 0; v1 < arrcnt; v1++) CProtPSarr[v1] ^= Eclcodearr[v1];

        //     for (v1 = 0; v1 < arrcnt; v1++) s2 = s2 + (Convert.ToInt32(ProtPSarr[v1])).ToString() + StringSpilt;  // Test 
        //     // for (v1 = 0; v1 < arrcnt; v1++) s1 = s1 + (Convert.ToInt32(ProtPSarr[v1] + 1 + v1)).ToString() + StringSpilt; // Add CNT

        //     return (s2);
        //     // s2 = Convert.ToChar(tUsername.Substring(0, 1));
        // }  // end PSaddEclCode


     //    public string DBDeEncCode(string PPassword, string Eclcode, string StringSpilt, string pway)
     //    {
     //        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, Shead5 = 5;
     //        string s1 = "", s2 = "";
     //        string s3 = "", s4 = "";
     //        int v8 = 0, v9 = 0;
     // 
     //        if (Eclcode == "")
     //            Eclcode = ConfigurationManager.AppSettings["LicenseString"];  // Replace Eclcode
     //        // 如果不為空白就加入此直在第 ? 開始
     //        if ((pway.Substring(0, 1) == "2") && (pway.Length > 1))
     //        {
     //            char[] Ecode = new char[2];                          // 找第一字
     //            Ecode[0] = Convert.ToChar(pway.Substring(1, 1));     // Char 轉 Digital
     //            v8 = Convert.ToInt32(Ecode[0]);                      // Model 10  
     //            v9 = v8 % 10;  // model 10 
     //            if (Eclcode.Length > v9)
     //            {
     //                s4 = Eclcode.Substring(0, v9 - 1) + pway.Substring(1, pway.Length - 1) + Eclcode.Substring(v9 - 1, Eclcode.Length - v9 + 1);
     //                Eclcode = s4;
     //            }
     //           }

        //     v2 = PPassword.Length;
        //     v3 = Eclcode.Length;
        //     int head1 = 0, tail1 = 0, arri = 0;
        //     string tmpc = "", tmpd = "";

        //     char[] PPasswordarr = new char[v2];  // char 內放 Asc Value
        //     char[] Eclcodearr = new char[v2];
        //     char[] ProtPSarr = new char[v2];
        //     int arrcnt = v2;

        //     if (v2 >= arrcnt) v2 = arrcnt;
        //     if (v3 >= arrcnt) v3 = arrcnt;

        //     // head1 = Shead5+1;
        //     for (v1 = 0; v1 < v2; v1++)
        //     {
        //         tmpc = PPassword.Substring(head1, 1);
        //         if (tmpc != StringSpilt)
        //         {
        //             tmpd = tmpd + tmpc;
        //         }
        //         else
        //         {
        //             v4 = Convert.ToInt32(tmpd);
        //             PPasswordarr[arri] = Convert.ToChar(v4);
        //             ProtPSarr[arri] = Convert.ToChar(v4);
        //             arri++;
        //             tmpd = "";
        //         }
        //         head1++;
        //         //PPasswordarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));
        //     }
        //     for (v1 = 0; v1 < v3; v1++) Eclcodearr[v1] = Convert.ToChar(Eclcode.Substring(v1, 1));
        //     // for (v1 = 0; v1 < v2; v1++) ProtPSarr[v1] = Convert.ToChar(PPassword.Substring(v1, 1));

        //     for (v1 = 0; v1 < arrcnt; v1++) ProtPSarr[v1] ^= Eclcodearr[v1];

        //     for (v1 = 0; v1 < arri; v1++) s2 = s2 + ProtPSarr[v1].ToString();  // 取的有效 Character
        //     return (s2);
        //     // s2 = Convert.ToChar(tUsername.Substring(0, 1));
        // }  // end PSaddEclCode

        // 不送組合碼
        // <add key="LicenseString" value="License Document By IT Team 20120131 Donot Copy and Delete this file"/>
        // 123456789101112131415161718192021222324252627282930 = 123456DBA789101112131415161718192021222324252627282930
        public string EncCodeWithoutEclcode(string PPassword, string Eclcode, string StringSpilt, string pway)
        {
            string s2 = "", t2 = "";
            if (PPassword == "") return (s2); 
            int v2=0;
            if ( Eclcode == "")
            {
                t2 = ConfigurationManager.AppSettings["LicenseString"];  // Replace Eclcode
                if (t2 == "")
                {
                    //Response.Write("<script>alert('There are not allow Encode = space！')</script>");
                    return (s2);
                }
                else
                {
                    s2 = LibUSR1Pointer.DBEncCode(PPassword, t2, StringSpilt, pway);
                }

            }
            else
                   s2 = LibUSR1Pointer.DBEncCode(PPassword, Eclcode, StringSpilt, pway);
        
            return (s2);
            // s2 = Convert.ToChar(tUsername.Substring(0, 1));
        }  // end PSaddEclCodeWithoutEclcode


        public string DeEncCodeWithoutEclcode(string PPassword, string Eclcode, string StringSpilt, string pway)
        {
            string s2 = "", t2 = "";
            int v2 = 0;

            if (PPassword == "") return (s2); 
            if (Eclcode == "")
            {
                t2 = ConfigurationManager.AppSettings["LicenseString"];  // Replace Eclcode
                if (t2 == "")
                {
                    // Response.Write("<script>alert('There are not allow Encode = space！')</script>");
                    return (s2);
                }
                else
                {
                    s2 = LibUSR1Pointer.DBDeEncCode(PPassword, t2, StringSpilt, pway);
                }

            }
            else
                s2 = LibUSR1Pointer.DBDeEncCode(PPassword, Eclcode, StringSpilt, pway);

            return (s2);

        }  // end PSaddEclCodeWithoutEclcode



           
      // GetSecPass("P1", tcodedate, CodeConstructType, tmp1P1, ref P1SecPS);
    public string GetSecPass(string fieldloc, string tcodedate, string tcode, string P1Val, ref string RSecPS )
    {
        string s1 = "";  
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, v8 = 0;
        v3 = tcodedate.Length;
        v4 = P1Val.Length;
        v1 = Convert.ToInt32(tcodedate); // tmp
        for (v2 = 1; v2 < v3; v2++)
            s1 = s1 + tcodedate.Substring(v3-v2, 1);
        v1 = Convert.ToInt32(s1);
        v2 = Convert.ToInt32(P1Val);
        v5 = Convert.ToInt32(tcodedate.Substring(6,2));

        v6 = v1 + v2 + 9999999;
            //if (v1 > v2)  v6 = v1 % v5 + v2 * 10;
            //else v6 = v2 % v3 + v1 * 10;

        RSecPS = v6.ToString();
        v8 = RSecPS.Length; 
        if ( v8 >= 8 ) RSecPS = RSecPS.Substring(0, 7);
                
        return ( RSecPS );
    }

    public string CheckSecPass(string p1, string DBType, string ReadDB, string fieldloc, string tcodedate, string tcode, string P1Val, string RSecPS)
    {
        string s1 = "", tmpP1 = "", chkRSecPS = "", OrgchkRSecPS = "", Rval = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, v8 = 0;
        string st1 = "";
        
        if ( ( tcodedate == "" )  || ( tcodedate.Length < 8 ) ) return ("");
        
        tcodedate = "2012" + tcodedate.Substring( 4,4 );   
       
        if ( RSecPS == "" ) return( "");  // Space  

        string st2 = "select * from PUBLIB.JCOD01 where codedate = '" + tcodedate + "' and  code = '" + tcode + "' ";
        if (DBType == "sql") st2 = "select * from JCOD01 where codedate = '" + tcodedate + "' and  code = '" + tcode + "' ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(DBType, ReadDB, st2);
        if (ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 <= 0)
        {
            return (""); // Not Data
        }
        else
            tmpP1 = ds1.Tables[0].Rows[0]["P1"].ToString();

        P1Val = tmpP1;

        v3 = tcodedate.Length;
        v4 = P1Val.Length;
        v1 = Convert.ToInt32(tcodedate); // tmp
        for (v2 = 1; v2 < v3; v2++)
            s1 = s1 + tcodedate.Substring(v3 - v2, 1);
        v1 = Convert.ToInt32(s1);
        v2 = Convert.ToInt32(P1Val);
        v5 = Convert.ToInt32(tcodedate.Substring(6, 2));

        v6 = v1 + v2 + 9999999;
        //if (v1 > v2)  v6 = v1 % v5 + v2 * 10;
        //else v6 = v2 % v3 + v1 * 10;

        chkRSecPS = v6.ToString();
        v8 = chkRSecPS.Length;
        if (v8 >= 8)
            chkRSecPS = chkRSecPS.Substring(0, 7);
        
        OrgchkRSecPS = RSecPS;

        if (OrgchkRSecPS.Length >= 8) OrgchkRSecPS = OrgchkRSecPS.Substring(0, 7);

        if (chkRSecPS == OrgchkRSecPS ) Rval = "Y";

        return ( Rval);
    }


    }  // end class Convertlib  




}      // EconomyUser
// ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
// ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
// ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
// UsrCtllib UsrCtllibPointer = new UsrCtllib();
