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
using System.Data.OracleClient;
using SCM.GSCMDKen;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Economy.BLL;
using Economy.Publibrary;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;

public partial class MainSetPwd02 : System.Web.UI.Page
{
    //string login_no = "";
    //string login_name = "";
    static string web = "";
    public static string SetPed02DBRaed = "";
  
    //ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    //ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    FPubLib FPubLibPointer = new FPubLib();
    Convertlib ConvertlibPointer = new Convertlib();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack){

          web = Session["Param1"].ToString();

          string p1 = Session["Param1"].ToString();
          string p2 = Session["Param2"].ToString();
          string p3 = Session["Param3"].ToString();
          string p4 = Session["Param4"].ToString();
          string p5 = Session["Param5"].ToString();
          string p6 = Session["Param6"].ToString(); // Supervisor Login 
          string p7 = Session["Param7"].ToString(); // ITSystem 


         // DBtype = p2; 
          SetPed02DBRaed = p3;
         // DBWri  = p4;
          TextBox2.Text = p7;          
          this.user_id.Text = "";
          this.user_pwd.Text = "";
          TextBox1.Text = "";
          TextBox4.Focus();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;
    //    if (user_id_v.Equals(""))
    //    {
    //        RegisterClientScriptBlock("0", "<script>alert('請輸入工號！')</script>");
    //        this.user_id.Focus();
    //        return;
    //    }
    //    if(user_pwd_v.Equals("")){
    //        RegisterClientScriptBlock("0", "<script>alert('請輸入密碼！')</script>");
    //        this.user_pwd.Focus();
    //        return;
    //    }
        if (!checkLoginUser(user_id_v, user_pwd_v, "1"))
        {
            RegisterClientScriptBlock("0", "<script>alert('工號或密碼不正確，請重新輸入！')</script>");
            this.user_id.Focus();
            return;
        }
        else
        {
          //  Response.Redirect(web+"?delflag=true");
           //Response.Write("<script>window.close();</script>");
    
        }
    }

    private bool checkLoginUser(string user_id, string password, string functype)
    {
        bool isOk = false;
        Button5.Text = "須輸入Admin及密碼";
      //  TextBox4.Text.S
        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, SetPed02DBRaed);
        if ((t1 == "-1") || (TextBox4.Text.ToLower() != "admin" )) return isOk;
        
        string tfunc = functype;

        string Loc1 = "", DCode = "12701", PStrSpilt=",", t2="";
        // Loc1 = ReadParaTxt("WebReadParam.txt", DCode);

        string retPara = "";
        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string[] ProtTxtArray = new string[ArrMax];
        string[] CProtTxtArray = new string[ArrMax];
        string FileName = "WebReadParam.txt";
        string FilePara = "", ParaNum = "";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "", str1="", str2="";
        int i = 0, strlen = 0, Chead=5, v1=0, v2=0, v3=0;
        string DBString = "", ProDBSTring="", PCode = "", ConString="";

        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            ReadTxtArray[i] = InString;
            //             Response.Write(ReadTxtArray[i]);
            //             Response.Write("<br>");
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
                {
                    retPara = InString.Substring(6, strlen - 5);
                  //  i = ArrMax;  // Break
                }
                DBString = InString.Substring(Chead + 1, strlen - Chead);
                ProDBSTring = ConfigurationManager.AppSettings[DBString];
                if ((ProDBSTring != "") && ( ProDBSTring != null ) ) // Not Space
                {
                     str1 = InString.Substring(0, 5);
                     v1 = ProDBSTring.Length;
                     for ( v2=0; v2 < v1; v2++ )
                     {
                           PCode = PCode +  str1.Substring(v3,1);
                           v3++;
                           if ( v3 >= Chead ) v3=0;
                     }

                     ConString = ConvertlibPointer.EncCode(ProDBSTring, PCode, PStrSpilt, "");
                     ProtTxtArray[i] = InString.Substring(0, 5) + "," + ConString;
                     str2 = ProtTxtArray[i].Substring(Chead + 1, ProtTxtArray[i].Length - 6);
                     CProtTxtArray[i] = ConvertlibPointer.DeEncCode(str2, PCode, PStrSpilt, "");
                     CProtTxtArray[i] = InString.Substring(0, 5) + "," + CProtTxtArray[i];
                     
                } // end if ProDBSTring != ""
            }
            i++;

        }

        sr.Close();


        string WFileName = "SFCConf.txt";
        string WFilePara = "";
        if ( WFilePara != "") WFileName = WFilePara;
        string WServerPath = Server.MapPath("~\\" + WFileName);
        //FileInfo Wfi = new FileInfo(WServerPath);
        // StreamReader Wsr = Wfi.OpenText();
        StreamWriter Wptr = new StreamWriter(WServerPath);

        for (i = 0; i < ArrMax; i++)
        {
            if (CProtTxtArray[i] != null )
            {
                str1 = ProtTxtArray[i];
                Wptr.WriteLine(str1);
                
               // CProtTxtArray[i] = sr.W   .ReadLine())
            }

        }

        Wptr.Flush();
        Wptr.Close();

               
        isOk = true;
        return isOk;     
                
       
    }

  //  private DataSet queryByDefinedSql(string sqlstr)
  //  {
  //      OracleDataAdapter ada = new OracleDataAdapter();
  //      DataSet dsPos = new DataSet();
  //      OracleConnection con = new OracleConnection(ConfigurationManager.AppSettings["dbconnstr"]);
  //      OracleCommand com = new OracleCommand(sqlstr, con);
  //      com.CommandType = CommandType.Text;
  //      ada.SelectCommand = com;
  //      con.Open();
  //      ada.Fill(dsPos, "allUser");
  //      con.Close();
  //      return dsPos;
  //  }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<SCRIPT   LANGUAGE='JavaScript'>window.opener=null;window.close();</SCRIPT> "); // close system
        //string web = Request.QueryString["web"].ToString();
        //Response.Redirect(web);
    }
   // protected void TextBox1_TextChanged(object sender, EventArgs e)
   // {
   //
   // }   


    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
        int ArrMax = 100;
        string[] ReadTxtArray = new string[ArrMax];
        string FileName = "SetReadParam.txt";
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string InString = "";
        int i = 0, strlen = 0;

        while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
        {
            ReadTxtArray[i] = InString;
            //             Response.Write(ReadTxtArray[i]);
            //             Response.Write("<br>");
            if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
            {
                strlen = InString.Length - 1;
                if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
                {
                    retPara = InString.Substring(6, strlen - 5);
                    i = ArrMax;  // Break
                }

            }
            i++;

        }

        sr.Close();
        return (retPara);
    }


    private string tmpEncCode(string PPassword, string Eclcode, string StringSpilt, string pway)
    {
        int v1 = 0, v2 = 0, v3 = 0;
        string s1 = "", s2 = "";
        
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

    private string tmpDeEncCode(string PPassword, string Eclcode, string StringSpilt, string pway)
    {
        int v1 = 0, v2 = 0, v3 = 0,v4=0, Shead5=5;
        string s1 = "", s2 = "";

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

}
