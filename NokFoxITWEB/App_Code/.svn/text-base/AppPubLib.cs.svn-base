using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using Economy.Publibrary;
using SCM.GSCMDKen;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Globalization;
using System;
using System.Data;


namespace SCM.GSCMDKen
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class AppPubLib
{
    protected DateTime Pubtmptoday = DateTime.Today;
    protected string PubCurrtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string PubCuurDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CurrDate = DateTime.Today.ToString("yyyyMMdd");
    protected string CtlDate = "99999999"; // DateTime.Today.ToString("yyyyMMdd");
    protected string PStrSpilt = "";
    protected string DBsql = "sql"; 
    public static string PubDefaultConnString = "Data Source=10.83.16.74;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    public string PubRunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    FSplitlib FSplitlibPointer = new FSplitlib();
    // FPubLib FPubLibPointer = new FPubLib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    // FPriLib FPriLibPointer = new FPriLib(); 
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();


    ////////////////////////////////////////////////////////////////////////////
    // Add 20120718
    // InPuting : COming in data string
    // CntByte  : 第幾個 Bytes , 從 1 開始
    // CntBit   : 第幾個 Bit
    // ReadWrite: Read or Write
    // WValue   : Writw Value
    // Retuen   : Read  : return bit by characrter 
    //            Write : reurn String ( InPutstring )
    ///////////////////////////////////////
    public string SetBitProc(string InPutstring, string CntByte, string CntBitdatano, string ReadWrite, string WValue)
    {
        
        int v1=0, v2=0, v3=0;
        string Ret1 = "", SByte = "";

        //if ((InPutstring == "") || (CntByte == "") || (CntBitdatano == "") || ( ReadWrite == "")) return (Ret1);
        if ((InPutstring == "") || (CntByte == "") || (ReadWrite == "")) return (Ret1);
        v1 = InPutstring.Length - 1;
        v2 = Convert.ToInt32(CntByte) - 1;
        v3 = Convert.ToInt32(CntBitdatano) - 1;

        if ( (v1 < 0) || (v2 < 0) || (v1 < v2) ) return (Ret1);
        SByte = InPutstring.Substring( v2, 1);

        if (ReadWrite.ToLower() == "read") return (SByte);
        if (ReadWrite.ToLower() == "write") return (InPutstring.Substring(0, v2) + WValue + InPutstring.Substring(v2 + 1, v1 - v2));

        return (Ret1);
    }   // end PubGet_Ticket 
  
    
     

}  // end public class AppPubLib
}  // end namespace Economy.GCMDKen


