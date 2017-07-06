using System;
using System.Collections.Generic;
using System.Text;
namespace DB.EAI
{
    public static class FieldDefine
    {
        public struct InvInfo
        {
            public string InvMFGOrg;
            public string InvNum;
            public string InvCus;
            public string InvMod;
            public string InvQty;
            public string TYPE;   //GSM or CDMA
            public string ShipDate;
            public string Shiptocountry; //add by zhang 090508 for GSM TRANSFER IF usa OR NOT

        }


        public struct UPDField
        {
            public static Boolean SerialNumberTypeFlag;
            public static string SerialNumberTypeModel;   // 1 "IMEI"

            public static string SerialNumberFlag;
            public static string SerialNumberModel;       // 2 CMCS_SFC_SHIPPING_DATA      IMEI

            public static Boolean FactoryCodeFlag;
            public static string FactoryCodeModel;        // 3 "CMCSTNN" or "CMCSMCME10" or "CMCSMCMG2"

            public static Boolean GenerationDateFlag;
            public static string GenerationDateModel;     // 4 E2PCONFIG                   E2PDATE (YYYY-MM-DD 24HH:MI:SS)

            public static string APCFlagFlag;
            public static string APCFlagModel;            // 5 CMCS_SFC_IMEINUM            MID(CUSTOMER_NUM,1,3)

            public static string TransceiverModel;        // 6 CMCS_SFC_SHIPPING_DATA      CUST_PNO
            public static string CustomerModel;           // 7 CMCS_SFC_SHIPPING_DATA      SA_NO
            public static string MarketName;              // 8 ROS_TCH_PN                  MARKET_NAME
            public static string ItemCode;                // 9 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_ITEM
            public static string WarrantyCode;            //10 ""
            public static string ShipDate;                //11 CMCS_SFC_SHIPPING_DATA      IN_DATE (YYYY-MM-DD 24HH:MI:SS)
            public static string ShipToCustomerID;        //12 ""
            public static string ShipToCustomerAddressID; //13 ""
            public static string ShipToCustomerName;      //14 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CUSTOMERNAME
            public static string ShipToCity;              //15 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CITY
            public static string ShipToCountry;           //16 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_COUNTRY
            public static string SoldToCustomerID;        //17 ""
            public static string SoldToCustomerName;      //18 "MOTOROLA"
            public static string SoldDate;                //19 CMCS_SFC_PACKING_LINES_ALL  CREATION_DATE
            public static string TrackingID;              //20 CMCS_SFC_SHIPPING_DATA      SERIAL_NO
            public static string TANumber;                //21 ""
            public static string CartonID;                //22 CMCS_SFC_PACKING_LINES_ALL  INTERNAL_CARTON
            public static string PONumber;                //23 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_PO
            public static string SONumber;                //24 CMCS_SFC_PACKING_LINES_ALL  SO_NUMBER
            public static string FOSequenceNumber;        //25 CMCS_SFC_SHIPPING_DATA      WORK_ORDER


            public static string NetworkUnlockCode;
            public static string NetworkUnlockCodeModel;  //   NKEY

            public static string PrimeCoUnlockCode;       //27 ""
            public static string VerizonUnlockCode;       //28 ""

            public static string SIMUnlockCode;
            public static string SIMUnlockCodeModel;      //29 E2PCONFIG                   NSKEY

            public static string ServicePasscode;
            public static string ServicePasscodeModel;    //30 E2PCONFIG                   PRIVILEGEPWD

            public static string Lock4;                   //31 ""
            public static string Lock5;                   //32 ""
            public static string AKeyRandom;              //33 ""
            public static string AKeyZero;                //34 ""
            public static string MSN;                     //35 CMCS_SFC_IMEINUM            CUSTOMER_NUM
            public static string BT;                      //36 CMCS_SFC_IMEINUM            BTADDRESS
            public static string WLAN;                    //37 ""
        }

        //-----------------------------------For GSM-----------------------------------
        public struct GSMUPDInfo
        {
            public string SerialNumberType;   // 1 "IMEI"
            public string SerialNumber;            // 2 CMCS_SFC_SHIPPING_DATA      IMEI
            public string FactoryCode;             // 3 "CMCSTNN" or "CMCSMCME10" or "CMCSMCMG2"
            public string GenerationDate;          // 4 E2PCONFIG                   E2PDATE (YYYY-MM-DD 24HH:MI:SS)
            public string APC;                     // 5 CMCS_SFC_IMEINUM            MID(CUSTOMER_NUM,1,3)
            public string TransceiverModel;        // 6 CMCS_SFC_SHIPPING_DATA      CUST_PNO
            public string CustomerModel;           // 7 CMCS_SFC_SHIPPING_DATA      SA_NO
            public string MarketName;              // 8 ROS_TCH_PN                  MARKET_NAME
            public string ItemCode;                // 9 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_ITEM
            public string WarrantyCode;            //10 ""
            public string ShipDate;                //11 CMCS_SFC_SHIPPING_DATA      IN_DATE (YYYY-MM-DD 24HH:MI:SS)
            public string ShipToCustomerID;        //12 "123456"
            public string ShipToCustomerAddressID; //13 "123456"
            public string ShipToCustomerName;      //14 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CUSTOMERNAME
            public string ShipToCity;              //15 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CITY
            public string ShipToCountry;           //16 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_COUNTRY
            public string SoldToCustomerID;       //17 ""  ---------- CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_PO ----------
            public string SoldToCustomerName;      //18 "MOTOROLA"
            public string SoldDate;                //19 CMCS_SFC_PACKING_LINES_ALL  CREATION_DATE
            public string TrackingID;              //20 CMCS_SFC_SHIPPING_DATA      SERIAL_NO
            public string TANumber;                //21 ""
            public string CartonID;                //22 CMCS_SFC_PACKING_LINES_ALL  INTERNAL_CARTON
            public string PONumber;                //23 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_PO
            public string SONumber;                //24 CMCS_SFC_PACKING_LINES_ALL  SO_NUMBER
            public string FOSequenceNumber;        //25 CMCS_SFC_SHIPPING_DATA      WORK_ORDER
            public string NetworkUnlockCode;       //26 E2PCONFIG                   NKEY
            public string PrimeCoUnlockCode;       //27 ""
            public string VerizonUnlockCode;       //28 ""
            public string SIMUnlockCode;           //29 E2PCONFIG                   NSKEY
            public string ServicePasscode;         //30 E2PCONFIG                   PRIVILEGEPWD
            public string Lock4;                   //31 ""
            public string Lock5;                   //32 ""
            public string AKeyRandom;              //33 ""
            public string AKeyZero;                //34 ""
            public string MSN;                     //35 CMCS_SFC_IMEINUM            CUSTOMER_NUM
            public string BT;                      //36 CMCS_SFC_IMEINUM            BTADDRESS
            public string WLAN;                    //37 ""

            public string BaseProcessorID;         //38  ""
            public string FASTTID;                 //39  ""
            public string LocationType;            //'40  ""
            public string PackingList;             //41  Invoice
            public string FabDate;                 //42  CMCS_SFC_PACKING_LINES_ALL  CREATION_DATE
            public string SoftwareVersion;         //43  ""
            public string OrganizationCode;        //44  ""
            public string FlexOption;              //45  ""
            public string ICCID;                   //46  ""
            public string SOLineNumber;            //47  ""
            public string DirectShipRegionCode;    //48  ""
            public string DirectShipSONumber;      //49  ""
            public string DirectShipPONumber;      //50  ""
            public string DirectShipCustomerNumber;    //51  ""
            public string DirectShiptoAddressID;   //52  ""
            public string DirectShipCustomerCountry;   //53  ""
            public string DirectShipCustomerName;  //54  ""
            public string DirectShipBillToID;      //55  ""
            public string ShipmentNumber;          //56  Invoice
            public string WIPDJ;                   //57  CMCS_SFC_PACKING_LINES_ALL  SO_NUMBER
            public string DeleteFlag;              //58  "N"
            public string UltimateCountry;         //59  CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_COUNTRY
            public string CustomerSN;              //60  ""
            public string Min;                     //61  ""
            public string BilltoID;                //62  ""
            public string CURR_FLEX_VER;           //63  ""
            public string CURR_FLASH_NAME;         //64  ""
            public string CURR_PRI_VER;            //65  ""
            public string LANG_PKG_ID;             //66  ""
            public string KJAVA_VER;               //67  ""
            public string BOOTLOADER_VER;          //68  ""
            public string HARDWARE_VER;            //69  ""
            public string FOTA_ENABLED;            //70  ""
            public string DUAL_SERIAL_NO;          //71  ""
            public string DUAL_SERIAL_NO_TYPE;     //72  ""
            public string TBD2;                    //73  ""
            public string TBD3;                    //74  ""
            public string TBD4;                    //75  ""
            public string TBD5;                    //76  ""
            public string TBD6;                    //77  ""
        }


        //-----------------------------------For CDMA-----------------------------------
        public struct CDMAUPDInfo
        {
            public string SerialNumberType;        // 1 "MEID"
            public string SerialNumber;            // 2 CMCS_SFC_SHIPPING_DATA      IMEI
            public string FactoryCode;             // 3 "CMCSTNN" or "CMCSMCME10" or "CMCSMCMG2" or "CMCSMCMTY"
            public string GenerationDate;          // 4 CMCS_SFC_IMEINUM                   CREATION_DATE (YYYY-MM-DD 24HH:MI:SS)
            public string Procotol;                // 5 ""
            public string APC;                     // 6 CMCS_SFC_IMEINUM            MID(CUSTOMER_NUM,1,3)
            public string TransceiverModel;        // 7 CMCS_SFC_SHIPPING_DATA      CUST_PNO
            public string CustomerModel;           // 8 CMCS_SFC_SHIPPING_DATA      SA_NO
            public string MarketName;              // 9 ROS_TCH_PN                  MARKET_NAME
            public string ItemCode;                // 10 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_ITEM
            public string WarrantyCode;            //11 ""
            public string ShipDate;                //12 CMCS_SFC_SHIPPING_DATA      IN_DATE (YYYY-MM-DD 24HH:MI:SS)
            public string ShipToCustomerID;        //13 "123456"
            public string ShipToCustomerAddressID; //14 "123456"
            public string ShipToCustomerName;      //15 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CUSTOMERNAME
            public string ShipToCity;              //16 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_CITY
            public string ShipToCountry;           //17 CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_COUNTRY
            public string SoldToCustomerID;        //18 ""  ---------- CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_PO ----------
            public string SoldToCustomerName;      //19 "MOTOROLA"
            public string SoldDate;                //20 CMCS_SFC_PACKING_LINES_ALL  CREATION_DATE
            public string TrackingID;              //21 CMCS_SFC_SHIPPING_DATA      SERIAL_NO
            public string TANumber;                //22 ""
            public string CartonID;                //23 CMCS_SFC_PACKING_LINES_ALL  INTERNAL_CARTON
            public string PONumber;                //24 CMCS_SFC_PACKING_LINES_ALL  CUSTOMER_PO
            public string SONumber;                //25 CMCS_SFC_PACKING_LINES_ALL  SO_NUMBER
            public string FOSequenceNumber;        //26 CMCS_SFC_SHIPPING_DATA      WORK_ORDER

            public string MotorolaAlgorithm;       //27 SFC.CDMA_EMS_MEID_INFO      AKEY1
            public string PrimeCoAlgorithm;        //28 ""
            public string VerizonAlgorithm;        //29 ""
            public string OneTimeUnlockCode;       //30 E2PCONFIG                   NSKEY
            public string ServicePasscode;         //31 E2PCONFIG                   PRIVILEGEPWD
            public string Lock4;                   //32 ""
            public string Lock5;                   //33 ""
            public string AKeyRandom;              //34 ""
            public string AKeyZero;                //35 ""
            public string MSN;                     //36 CMCS_SFC_IMEINUM            CUSTOMER_NUM
            public string CSN;                     //37 CMCS_SFC_IMEINUM            BTADDRESS
            public string BT;                      //38 CMCS_SFC_IMEINUM            BTADDRESS
            public string WLAN;                    //39 ""
            public string EVDOPassword;            //40 ""

            public string AKey2Type;               //41 ""
            public string AKey2;                   //42 ""
            public string FASTTID;                 //43  ""
            public string LocationType;            //44  ""
            public string BaseProcessorID;         //45  ""
            public string AKeyIndex;               //46  ""
            public string CASNumber;               //47  ""

            public string SoftwareVersion;         //48  ""
            public string OrganizationCode;        //49  ""
            public string FlexOption;              //50  ""
            public string ICCID;                   //51  ""
            public string SOLineNumber;            //52  ""
            public string DirectShipRegionCode;    //53  ""
            public string DirectShipSONumber;      //54  ""
            public string DirectShipPONumber;      //55  ""
            public string DirectShipCustomerNumber;    //56  ""
            public string DirectShiptoAddressID;   //57  ""
            public string DirectShipCustomerCountry;   //58  ""
            public string DirectShipCustomerName;  //59  ""
            public string DirectShipBillToID;      //60  ""
            public string ShipmentNumber;          //61  Invoice
            public string WIPDJ;                   //62  CMCS_SFC_PACKING_LINES_ALL  SO_NUMBER
            public string DeleteFlag;              //63  "N"
            public string UltimateCountry;         //64  CMCS_SFC_PACKING_LINES_ALL  SHIP_TO_COUNTRY
            public string CustomerSN;              //65  ""
            public string Min;                     //66  ""
            public string BilltoID;                //67  ""
            public string CURR_FLEX_VER;           //68  ""
            public string CURR_FLASH_NAME;         //69  ""
            public string CURR_PRI_VER;            //70  ""
            public string LANG_PKG_ID;             //71  ""
            public string KJAVA_VER;               //72  ""
            public string BOOTLOADER_VER;          //73  ""
            public string HARDWARE_VER;            //74  ""
            public string FOTA_ENABLED;            //75  ""
            public string DUAL_SERIAL_NO;          //76  ""
            public string DUAL_SERIAL_NO_TYPE;     //77  ""
            public string TBD2;                    //78  ""
            public string TBD3;                    //79  ""
            public string TBD4;                    //80  ""
            public string TBD5;                    //81  ""
            public string TBD6;                    //82  ""
        }


        public struct RMAInfo
        {
            public string SERIAL_NO;
            public string SERIAL_NO_TYPE;
            public string RL_ACTION;
            public string RL_ACTION_DATE;
            public string LOCATION_CODE;
            public string LOCATION_CODE_COUNTRY;
            public string UPLOAD_DATE;
            public string OWNER_NAME;
            public string OWNER_NUMBER;
        }

        public struct MUAInfo
        {
            public string SERIAL_NO;
            public string SERIAL_NO_TYPE;
            public string STATUS;
            public string PKI_SOURCE;
            public string PKI_SOURCE_TYPE;
            public string MUA;
            public string MUA_TYPE;
            public string MUA_DATE;
            public string MUA_ACTION;
        }
        public enum GSMTTYPE
        {
            /// <summary>
            /// 
            /// </summary>
            UPD,

            /// <summary>
            /// 
            /// </summary>
            IMEI,

            /// <summary>
            /// 
            /// </summary>
            ASN,
            /// <summary>
            /// 
            /// </summary>
        }
        public enum CDMATYPE
        {

            /// <summary>
            /// 
            /// </summary>
            UPD,

            /// <summary>
            /// 
            /// </summary>
            GFS,

        }

    }

}