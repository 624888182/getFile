<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IMSCMPODetailNew.aspx.cs" Inherits="MainMSPrg_IMSCMPODetailNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>
<%@ Register Src="~/WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css"
    title="wood" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="PODetail.js" type="text/javascript"></script>

    <style type="text/css">
        .style1 {
            width: 100%;
            border: 0px;
            background-color: #FF9900;
        }

            .style1 td {
                background-color: #fff;
                height: 30px;
            }

        .style5 {
            width: 385px;
        }

        .style7 {
            width: 207px;
        }

        .style8 {
        }

        .style9 {
            width: 211px;
        }

        #form1 {
            height: 885px;
        }

        .gvHeader {
            background-color: #F7DFB5;
            color: #8C4510;
            height: 40px;
        }
    </style>
</head>
<body bgcolor="#ffffff" style="overflow: scroll; width: 100%;">
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <table cellpadding="0" cellspacing="1" class="style1">
                <tr>
                    <td class="style9" bgcolor="#FF9933">
                        <asp:Label ID="Label1" runat="server" Text="ID:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label3" runat="server" Text="MSFT PO ITEM:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label5" runat="server" Text="MSFT DUNS:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label6" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label7" runat="server" Text="MSFT PO:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label9" runat="server" Text="MSFT P/N:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label10" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label11" runat="server" Text="CREATIONDT:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label13" runat="server" Text="Description:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label14" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label15" runat="server" Text="POCNT:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label17" runat="server" Text="PO Price Base Unit:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label18" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label19" runat="server" Text="Oreder QTY:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label21" runat="server" Text="Pick Up Date:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label22" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label23" runat="server" Text="ShipToAdress:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label24" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <%--<asp:Label ID="Label25" runat="server" Text="Payment Term:"></asp:Label>--%>
                        <asp:Label ID="Label39" runat="server" Text="DateTimeStamp:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label26" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label27" runat="server" Text="Incoterm:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label29" runat="server" Text="Payment:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label30" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label31" runat="server" Text="ShipTo DUNS Code:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label32" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label33" runat="server" Text="ShipToCode:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label34" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Label35" runat="server" Text="Request Delivery Date:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label36" runat="server"></asp:Label>
                    </td>
                </tr>
             <tr>
                    <td class="style9">
                        <asp:Label ID="Label37" runat="server" Text="PurchaseGroup:"></asp:Label>
                    </td>
                    <td class="style5">
                        <asp:Label ID="Label38" runat="server"></asp:Label>
                    </td>
                    <td class="style7">
                        <%--<asp:Label ID="Label39" runat="server" Text="DateTimeStamp:"></asp:Label>--%>
                        Storage Location:</td>
                    <td>
                        <asp:Label ID="Label40" runat="server"></asp:Label>
                    </td>
                </tr>
               </table>
            </div>
    </form>
</body>
</html>
