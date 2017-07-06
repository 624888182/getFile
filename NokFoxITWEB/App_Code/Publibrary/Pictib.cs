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

    public class Pictlib  // 20101011 
    {
        ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
        ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
        // ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();

        public string GetPictByUserName(string tUername, string[,] arrpict, int arrpictLong, string tRunDBPath, ref int DataPicLong, ref int DataNewLong)
        {
            if (tUername == "")  tUername="web";

            int v1 = 0, v2 = 0, v3 = 0, v4 = 0, retlong=0, retlong2=0;


            string s1 = " select * from UserPictFile where UserName = '" + tUername + "' order by Ticket desc";
            DataSet Initds1 = LibSCM1Pointer.GetDataByDataPath(s1, tRunDBPath);
            if (Initds1.Tables.Count <= 0) v1 = 0;   // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
            else
            {
                v1 = Initds1.Tables[0].Rows.Count;  // Record Length
                retlong = Initds1.Tables[0].Rows.Count;
                DataPicLong = Initds1.Tables[0].Rows.Count;

                v3 = 0;                             // 最後一筆最前面  
                for (v2 = 0; v2 < v1; v2++)       // 
                {
                    v3 = v2 + 1;
                    arrpict[v3, 1] = Initds1.Tables[0].Rows[v2]["Username"].ToString();
                    arrpict[v3, 2] = Initds1.Tables[0].Rows[v2]["Ticket"].ToString();
                    arrpict[v3, 3] = Initds1.Tables[0].Rows[v2]["OwnerR"].ToString();
                    arrpict[v3, 4] = Initds1.Tables[0].Rows[v2]["GroupR"].ToString();
                    arrpict[v3, 5] = Initds1.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpict[v3, 6] = Initds1.Tables[0].Rows[v2]["PictName"].ToString();
                    arrpict[v3, 7] = Initds1.Tables[0].Rows[v2]["PictLoc"].ToString();
                    arrpict[v3, 8] = Initds1.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpict[v3, 9] = Initds1.Tables[0].Rows[v2]["DataType"].ToString();
                    arrpict[v3, 10] = Initds1.Tables[0].Rows[v2]["Desp"].ToString();

                    if (v2 >= arrpictLong) v2 = v1 + 1; // break

                }
            }

            string s2 = " select * from UserNewsFile where UserName = '" + tUername + "' order by Ticket desc";
            DataSet Initds2 = LibSCM1Pointer.GetDataByDataPath(s2, tRunDBPath);
            if (Initds2.Tables.Count <= 0) v1 = 0;   // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
            else
            {
                v1 = Initds2.Tables[0].Rows.Count;  // Record Length
                retlong2 = Initds2.Tables[0].Rows.Count;
                DataNewLong = Initds2.Tables[0].Rows.Count;

                v3 = 0;                             // 最後一筆最前面  
                for (v2 = 0; v2 < v1; v2++)       // 
                {
                    v3 = v2 + 1;
                    arrpict[v3, 11] = Initds2.Tables[0].Rows[v2]["Username"].ToString();
                    arrpict[v3, 12] = Initds2.Tables[0].Rows[v2]["Ticket"].ToString();
                    arrpict[v3, 13] = Initds2.Tables[0].Rows[v2]["OwnerR"].ToString();
                    arrpict[v3, 14] = Initds2.Tables[0].Rows[v2]["GroupR"].ToString();
                    arrpict[v3, 15] = Initds2.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpict[v3, 16] = Initds2.Tables[0].Rows[v2]["NewsName"].ToString();
                    arrpict[v3, 17] = Initds2.Tables[0].Rows[v2]["NewsLoc"].ToString();
                    arrpict[v3, 18] = Initds2.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpict[v3, 19] = Initds2.Tables[0].Rows[v2]["DataType"].ToString();
                    arrpict[v3, 20] = Initds1.Tables[0].Rows[v2]["Desp"].ToString();

                    if (v2 >= arrpictLong) v2 = v1 + 1; // break

                }
            }

            return ( retlong.ToString() );
        }

        ///////////////////////////////////////////////////////
        ///  Get Pict for Private 
        public string GetPictByPriUserName(string tUername, string[,] arrpictPri, int arrpictPriLong, string tRunDBPath, ref int DataPicPriLong, ref int DataNewLong)
        {
            if (tUername == "") return("");

            int v1 = 0, v2 = 0, v3 = 0, v4 = 0, retlong = 0, retlong2 = 0;

            string s1 = " select * from UserPictFile where UserName = '" + tUername + "' order by Ticket desc";
            DataSet Initds3 = LibSCM1Pointer.GetDataByDataPath(s1, tRunDBPath);
            if (Initds3.Tables.Count <= 0) v1 = 0;   // Not Data Response.Write("There are not data in Syncro_4A3 table from coming Para "); //    return;
            else
            {
                v1 = Initds3.Tables[0].Rows.Count;  // Record Length
                retlong = Initds3.Tables[0].Rows.Count;
                DataPicPriLong = Initds3.Tables[0].Rows.Count;

                v3 = 0;                             // 最後一筆最前面  
                for (v2 = 0; v2 < v1; v2++)       // 
                {
                    v3 = v2 + 1;
                    arrpictPri[v3, 1] = Initds3.Tables[0].Rows[v2]["Username"].ToString();
                    arrpictPri[v3, 2] = Initds3.Tables[0].Rows[v2]["Ticket"].ToString();
                    arrpictPri[v3, 3] = Initds3.Tables[0].Rows[v2]["OwnerR"].ToString();
                    arrpictPri[v3, 4] = Initds3.Tables[0].Rows[v2]["GroupR"].ToString();
                    arrpictPri[v3, 5] = Initds3.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpictPri[v3, 6] = Initds3.Tables[0].Rows[v2]["PictName"].ToString();
                    arrpictPri[v3, 7] = Initds3.Tables[0].Rows[v2]["PictLoc"].ToString();
                    arrpictPri[v3, 8] = Initds3.Tables[0].Rows[v2]["OtherR"].ToString();
                    arrpictPri[v3, 9] = Initds3.Tables[0].Rows[v2]["DataType"].ToString();
                    arrpictPri[v3, 10] = Initds3.Tables[0].Rows[v2]["Desp"].ToString();

                    if (v2 >= arrpictPriLong) v2 = v1 + 1; // break

                }
            }
                     

            return (retlong.ToString());
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

    }  // end class UsrCtllib

}      // EconomyUser
// ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
// ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
// ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
// UsrCtllib UsrCtllibPointer = new UsrCtllib();
