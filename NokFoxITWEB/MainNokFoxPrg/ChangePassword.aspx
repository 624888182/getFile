<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="MainMSPrg_ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        * {
            padding: 0px;
            margin: 0px;
        }

        .auto-style1 {
            width: 200px;
            height:25px;
            border: 1px solid blue;
            text-align:left;
        }

        .auto-style2 {
            width: 200px;
            border: 1px solid blue;
            padding: 4px;
            text-align:left;
        }

        #lblUserName, #lblUser, #txtOldPassword, #txtNewPassword, #txtConNewPassword {
            width: 200px;
            height: 24px;
            font-size: 20px;
            color:green;
        }
     
        #ChangePassword {
            margin-top: 50px;
            margin-left: auto;
            margin-right: auto;
        }       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="ChangePassword"  align=center>
            <table align="center" style="border: 1px solid blue; border-collapse: collapse;">
                <tr>
                    <td colspan="2" style="height: 50px; font-size: 30px; text-align: center; color: blue;">ChangePassword</td>
                </tr>
                <tr>
                    <td class="auto-style2">User Name:
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblUserName" runat="server" Text="UserName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">UserID:
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="lblUser" runat="server" Text="UserID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Old Password:
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">New Password:
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Confirm New Password:
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtConNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" style="text-align: center">
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
                    </td>
                    <td class="auto-style1" style="text-align: center">
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>   
    </form>
</body>
</html>
