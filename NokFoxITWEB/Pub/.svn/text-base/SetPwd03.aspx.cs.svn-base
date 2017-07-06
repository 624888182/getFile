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

public partial class MainSetPwd03 : System.Web.UI.Page
{
    //string login_no = "";
    //string login_name = "";
    static string web = "";
    static string SetPed02DBRaed = "";
    static string DBRead = ConfigurationManager.AppSettings["Sql221String"];
    static string oraledb = "oracle", OraDBRead = ConfigurationManager.AppSettings["NormalDbConnectionString"];  // 76
    
  
    //ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUSR1Pointer = new ClassLibraryUSR1.Class1();
    FPubLib FPubLibPointer = new FPubLib();
    Convertlib ConvertlibPointer = new Convertlib();
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    // Protclass   ProtclassPointer = new Protclass();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string p1 = "", p2 = "", p3 = "", p4 = "", p5 = "", p6 = "", p7 = "", p8 = "", p9="";
        if(!Page.IsPostBack)
        {

            if (Session["Param1"] != null)
            {
                web = Session["Param1"].ToString();
                p1 = Session["Param1"].ToString();
                p2 = Session["Param2"].ToString();
                p3 = Session["Param3"].ToString();
                p4 = Session["Param4"].ToString();
                p5 = Session["Param5"].ToString();
                p6 = Session["Param6"].ToString(); // Supervisor Login 
                p7 = Session["Param7"].ToString(); // ITSystem 
                p8 = Session["Param8"].ToString(); // oracle 
                p9 = Session["Param9"].ToString(); // Oracle Server  L10 Server 
                oraledb = p8;
                OraDBRead = p9;
                DBRead = p5;
            }
            
         // DBtype = p2; 
          SetPed02DBRaed = p3;
         // DBWri  = p4;
          TextBox2.Text = p7;          
          this.user_id.Text = "";
          this.user_pwd.Text = "";
          TextBox1.Text = "";
          TextBox4.Focus();
          TextBox7.Text = DBRead;
          TextBox8.Text = "";
          TextBox9.Text = "";
          TextBox10.Text = "";
          TextBox11.Text = DateTime.Today.ToString("yyyyMMdd"); 
          TextBox12.Text = "";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;

        if ((TextBox4.Text.ToLower() != "admin") && (TextBox4.Text.ToLower() != "ken")) return;
        if (!checkLoginUser(TextBox4.Text, TextBox5.Text, "1"))
        {
            RegisterClientScriptBlock("0", "<script>alert('工號或密碼不正確，請重新輸入！')</script>");
            this.user_id.Focus();
            return;
        }

        string s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", t3 = "", spcode="";
        string tmpsql = "select * from FUser1 where UserID = '" + user_id.Text + "' ";
        DataSet tmpds = DataBaseOperation.SelectSQLDS("sql", SetPed02DBRaed, tmpsql); // CGetDataByPara(ssql1);
        //tmpds = LibSCM1Pointer.GetDataByDataPath(tmpsql, DBstring);
        if (tmpds.Tables.Count <= 0) return;
        if (tmpds.Tables[0].Rows.Count <= 0) return;
        else
        {
            s2 = (tmpds.Tables[0].Rows.Count).ToString();
            s3 = tmpds.Tables[0].Rows[0]["PassWD"].ToString();
            s4 = tmpds.Tables[0].Rows[0]["CheckSumNum"].ToString();
            s5 = tmpds.Tables[0].Rows[0]["OperRoleGpCode"].ToString();  // Random Code
            s6 = tmpds.Tables[0].Rows[0]["ITsystem"].ToString();
            s7 = tmpds.Tables[0].Rows[0]["PositionSeries"].ToString();  // 第 2 密碼

            t3 = LibUsrPointer.ITrsPassVal("2", user_id_v, s3); // Decode Test

            // t3 = LibUsrPointer.PSaddEclCode(user_pwd_v, s5, spcode); // Convert PassWord
            // if (t3 != s4) return;  // password error 
        }

        TextBox13.Text = "PS:" + t3;

        return;
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
        if (t1 == "-1") return isOk;
        if ((TextBox4.Text.ToLower() != "admin") && (TextBox4.Text.ToLower() != "ken") && (TextBox4.Text.ToLower() != "supervisor")) return isOk;
        isOk = true;
        return isOk;   
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

    protected void Button5_Click(object sender, EventArgs e)
    {
        string t2 = "";
        if (TextBox10.Text == "")
        {
            t2 = ConfigurationManager.AppSettings["LicenseString"];
            if (t2 == "")
            {
                Response.Write("<script>alert('There are not allow Encode = space！')</script>");
                return;
            }
            else
                TextBox10.Text = t2;
        }

        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;

        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string[] ProtTxtArray = new string[ArrMax];
        string[] CProtTxtArray = new string[ArrMax];

        string ProDBSTring = "", PCode = "", PStrSpilt = "", InString="";

        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, DBRead);
        if (t1 != "0")
        {
            Response.Write("<script>alert('There are not allow admin, Password = space！')</script>");
            return;
        }

        if ((TextBox4.Text.ToLower() != "admin") && (TextBox4.Text.ToLower() != "supervisor"))
        {
            Response.Write("<script>alert('There are not allow admin or supervisor, Password error！')</script>");
            return;
        }
       

        ProDBSTring = TextBox7.Text;
        InString = TextBox7.Text;
        PCode = TextBox10.Text;
        PStrSpilt = ",";
        string  EnCodeStr = ConvertlibPointer.EncCode(ProDBSTring, PCode, PStrSpilt, "1ken");
        TextBox8.Text = EnCodeStr;
        string DnCodeStr = ConvertlibPointer.DeEncCode(EnCodeStr, PCode, PStrSpilt, "1ken");
        TextBox9.Text = DnCodeStr;

        if (  TextBox9.Text == TextBox7.Text )
              Response.Write("<script>alert('Convert Test OK ！')</script>");
        else
              Response.Write("<script>alert('Convert Test Fail ！')</script>");

    }

    // Read Txt Data 
    protected void Button6_Click(object sender, EventArgs e)
    {
        if (TextBox10.Text == "")
        {
            Response.Write("<script>alert('There are not allow Encode = space！')</script>");
            return;
        }

        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;
        string retPara = "";
        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string[] ProtTxtArray = new string[ArrMax];
        string[] CProtTxtArray = new string[ArrMax];

        string DBString = "", ProDBSTring = "", PCode = "", ConString = "", PStrSpilt = "", InString = "";

        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, DBRead);
        if (t1 != "0")
        {
            Response.Write("<script>alert('There are not allow admin, Password = space！')</script>");
            return;
        }


        string FileName = "WebReadParam.txt";
        string FilePara = "", ParaNum = TextBox10.Text.Trim();
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string EnCodeStr = "", DnCodeStr = "";
        int i = 0, strlen = 0, Chead = 5;
        PCode = TextBox10.Text;
        PStrSpilt = ",";
        

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
                    retPara   = InString.Substring(6, strlen - 5);
                    EnCodeStr = InString.Substring(Chead + 1, strlen - Chead);
                    DnCodeStr = ConvertlibPointer.DeEncCode(EnCodeStr, PCode, PStrSpilt, "");
                    TextBox7.Text = InString;
                    TextBox8.Text = EnCodeStr;
                    TextBox9.Text = DnCodeStr;
                    i = ArrMax; // Break

                    //  i = ArrMax;  // Break
                }
               
            }
            i++;

        }

        sr.Close();           


    }

    // Test Link DataBase 20121202
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (TextBox10.Text == "")
        {
            Response.Write("<script>alert('There are not allow Encode = space！')</script>");
            return;
        }

        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;
        string retPara = "";
        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string[] ProtTxtArray = new string[ArrMax];
        string[] CProtTxtArray = new string[ArrMax];

        string DBString = "", ProDBSTring = "", PCode = "", ConString = "", PStrSpilt = "", InString = "";

        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, DBRead);
        if (t1 != "0")
        {
            Response.Write("<script>alert('There are not allow admin, Password = space！')</script>");
            return;
        }


        string FileName = "WebReadParam.txt";
        string FilePara = "", ParaNum = TextBox10.Text.Trim();
        if (FilePara != "") FileName = FilePara;
        string ServerPath = Server.MapPath("~\\" + FileName);
        FileInfo fi = new FileInfo(ServerPath);
        StreamReader sr = fi.OpenText();
        string EnCodeStr = "", DnCodeStr = "";
        int i = 0, strlen = 0, Chead = 5;
        PCode = TextBox10.Text;
        PStrSpilt = ",";


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
                    EnCodeStr = InString.Substring(Chead + 1, strlen - Chead);
                    DnCodeStr = ConvertlibPointer.DeEncCode(EnCodeStr, PCode, PStrSpilt, "");
                    TextBox7.Text = InString;
                    TextBox8.Text = EnCodeStr;
                    TextBox9.Text = DnCodeStr;


                    i = ArrMax; // Break

                    //  i = ArrMax;  // Break
                }

            }
            i++;

        }

        sr.Close();


        DataSet dt1 = null;

        string CurrDay = DateTime.Today.ToString("yyyyMMdd");  // 20100320

        string sqls1 = "select  * from MessLog where Substring(F0,1,8) = '" + CurrDay + "' order by F1 desc";

        string dbstr = "Server=10.186.171.221;User id=sa;Pwd=Sa123456;Database=ERPDBF";  // Test Only
        // dbstr = DBRead;
        dt1 = DataBaseOperation.SelectSQLDS("sql", dbstr, sqls1);
        if ( dt1.Tables.Count <= 0 )  
             return;
        int v3 = dt1.Tables[0].Rows.Count;
        //   string[,] arrDeLinkRou = new string[v3 + 2, 10 + 1];
        int v1 = 0, v2 = 0;
        string p1, p2, p3, p4, p5;

        dt1 = DataBaseOperation.ProtSelectSQLDS("sql", EnCodeStr, sqls1, "23001");
        v3 = dt1.Tables[0].Rows.Count;
        //   string[,] arrDeLinkRou = new string[v3 + 2, 10 + 1];
          
        for (v1 = 0; v1 < v3; v1++)
        {
            p1 = v1.ToString();
            p2 = dt1.Tables[0].Rows[v1]["F0"].ToString();
            p3 = dt1.Tables[0].Rows[v1]["F1"].ToString();  // Convert to CustomerSite
            p4 = dt1.Tables[0].Rows[v1]["F2"].ToString();  // Convert to CustomerSite
            p5 = dt1.Tables[0].Rows[v1]["F3"].ToString();  // Convert to CustomerSite
            //arrDeLinkRou[v1 + 1, 4] = dt1.Tables[0].Rows[v1]["DeLinkLineWS"].ToString();  // Convert to CustomerSite
           
        }

    } // end

    protected void Button8_Click(object sender, EventArgs e)
    {
        //return;
        int v1 = 0, v2 = 0, v3=0, v4=0;
        Random rd = new Random();//隨機數(最小值，最大值)rd.Next(1000,9999)
        Double d1 = rd.NextDouble() * 100000 + rd.NextDouble() * 100000;
        int i = Convert.ToInt32(d1);
        string a = i.ToString("D5").Substring(1, 4);
        // DataSet ds1 = null, d2 = null;
        string st1 = "", st2 = "", st3, tcodedate = "20120101", tcode = "PS";;
        string sql221DB = DBRead; //  ConfigurationManager.AppSettings["Sql221String"]; 
        string startdate = "20120101", tday="";
        DateTime dt1 = DateTime.Today;
        
        tday =  ShipPlanlibPointer.TrsstringToDateTime(startdate);
        dt1 = Convert.ToDateTime(tday);

        for (v1 = 0; v1 <= 365; v1++)
        {
            if ( ( v1 %  2 ) == 0 ) 
                 d1 = rd.NextDouble() * 10000000;
            else
                 d1 = rd.NextDouble() * 1000000;
            // d1 = rd.NextDouble() * 10000000;
            v3 = Convert.ToInt32(d1);
            st1 = v3.ToString();
            tcodedate = dt1.ToString("yyyyMMdd");
            // dt1 = DataBaseOperation.SelectSQLDS("sql", sql221DB, st2);
            // sql 
            st2 = "Update jcod01 set P1 = '" + st1 + "'  where codedate = '" + tcodedate + "' and code = '" + tcode + "' ";
            v4 = DataBaseOperation.ExecSQL("sql", DBRead, st2);
            if ( v4 <= 0 )
            {
                st3 = "Insert into jcod01 ( codedate , code, P1 ) Values ( '" + tcodedate + "', '" + tcode + "', '" + st1 + "' ) ";
                v4 = DataBaseOperation.ExecSQL("sql", DBRead, st3);
            }

            // oracle   oraledb = p8;   OraDBRead = p9; 
            st2 = "Update PUBLIB.JCOD01 set P1 = '" + st1 + "'  where codedate = '" + tcodedate + "' and code = '" + tcode + "' ";
            v4 = DataBaseOperation.ExecSQL(oraledb, OraDBRead, st2);
            if (v4 <= 0)
            {
                st3 = "Insert into  PUBLIB.JCOD01 ( codedate , code, P1 ) Values ( '" + tcodedate + "', '" + tcode + "', '" + st1 + "' ) ";
                v4 = DataBaseOperation.ExecSQL(oraledb, OraDBRead, st3);
            }

            //dt1 = Convert.ToDateTime(tday);
            dt1 = dt1.AddDays(+1);
           

            if ( tcodedate == "20130101" ) 
                  v1 = 365;

        }

    }

    //////////////////////////////////////////////////////
    // Get Sec-Password 一律改 2013                     //
    protected void Button9_Click(object sender, EventArgs e)
    {


        string st1 = "", st2 = "", st3 = "20120101", tcodedate = "20120101", tcode = "PS", tmpP1 = "", P1SecPS = "", st4 = "";
        int v1 = 0, v2 = 0, v3 = 0;

        string t2 = "", tEMPTYPE = "";
        string user_id_v = this.user_id.Text;
        string user_pwd_v = this.user_pwd.Text;
        // t2 = FPubLibPointer.ChkUsrPSExistAllDB("1", "oracle", OraDBRead, TextBox1.Text, TextBox2.Text, TextBox5.Text, ref tEMPTYPE );
        t2 = FPubLibPointer.ChkUsrPSExistAllDB("1", "sql", DBRead, user_id_v, user_pwd_v, "", ref tEMPTYPE);
        if ( t2 == "-1" )  
        {
            Response.Write("<script>alert('Open Users Fail ! You are not the Right Admin Pls Entry Again ! ')</script>");
            return;
        }

        if ( (tEMPTYPE != "1") && (tEMPTYPE != "2") ) 
        {
            Response.Write("<script>alert('Open Users Fail ! You are not the Level Admin Pls Entry Again ! ')</script>");
            return;
        } 

        tcodedate = st3.Substring(0, 4) + TextBox11.Text.Substring(4,4);
        st2 = "select * from PUBLIB.JCOD01 where codedate = '" + tcodedate + "' and  code = '" + tcode + "' ";
        DataSet ds1 = DataBaseOperation.SelectSQLDS(oraledb, OraDBRead, st2);
        if ( ds1.Tables.Count > 0) v1 = ds1.Tables[0].Rows.Count;
        if (v1 == 0)
        {
            TextBox12.Text = "Not Data";
            return;
        } 
        else
            tmpP1 = ds1.Tables[0].Rows[0]["P1"].ToString();

        if (tmpP1 != "") st4 = ConvertlibPointer.GetSecPass("P1", tcodedate, tcode, tmpP1, ref P1SecPS);
        TextBox12.Text = st4;

        // Check LoopBack
        st1 = ConvertlibPointer.CheckSecPass("1", oraledb, OraDBRead, "P1", tcodedate, tcode, "", P1SecPS);
    
                
    }

    // Convert Pass DBA
    protected void Button10_Click(object sender, EventArgs e)
    {
        string t2 = "";
        if (TextBox10.Text == "")
        {
            t2 = ConfigurationManager.AppSettings["LicenseString"];
            if (t2 == "")
            {
                Response.Write("<script>alert('There are not allow Encode = space！')</script>");
                return;
            }
            else
                TextBox10.Text = t2;
        }

        string user_id_v = this.user_id.Text;
        string aa = user_id.Text;
        string user_pwd_v = this.user_pwd.Text;

        int ArrMax = 200;
        string[] ReadTxtArray = new string[ArrMax];
        string[] ProtTxtArray = new string[ArrMax];
        string[] CProtTxtArray = new string[ArrMax];

        string ProDBSTring = "", PCode = "", PStrSpilt = "", InString = "";

        string t1 = FPubLibPointer.ChkUsrPSExist(TextBox4.Text, TextBox5.Text, DBRead);
        if (t1 != "0")
        {
            Response.Write("<script>alert('There are not allow admin or supervisor, Password error！')</script>");
            return;
        }

        if ((TextBox4.Text.ToLower() != "admin") && ( TextBox4.Text.ToLower() != "supervisor"))  
        {
            Response.Write("<script>alert('There are not allow admin or supervisor, Password error！')</script>");
            return;
        }

        ProDBSTring = TextBox7.Text;
        InString = TextBox7.Text;
        PCode = TextBox10.Text;
        PStrSpilt = ",";
        string EnCodeStr = LibUSR1Pointer.DBEncCode(ProDBSTring, PCode, PStrSpilt, "2DBA");
        TextBox8.Text = EnCodeStr;
        string DnCodeStr = LibUSR1Pointer.DBDeEncCode(EnCodeStr, PCode, PStrSpilt, "2DBA");
        TextBox9.Text = DnCodeStr;       

        if (TextBox9.Text == TextBox7.Text)
            Response.Write("<script>alert('Convert Test OK ！')</script>");
        else
            Response.Write("<script>alert('Convert Test Fail ！')</script>");
    }
}  // end SetPwd03

