<%@ Page Language="C#" AutoEventWireup="true" CodeFile="I_HubLogin.aspx.cs" Inherits="I_HubLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 458px;
            border: 2px ;
        }
        .style2
        {
            width: 587px;
        }
        .style3
        {
            height: 44px;
        }
        .style4
        {
            width: 158px;
        }
    </style>
</head>
<body bgcolor="#996600">
    <form id="form1" runat="server" >
    <div style="height: 772px">
    
        <table cellpadding="2" class="style1">
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large"  
                        Text="Nokia SI i-Hub Web"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" class="style2">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" 
                        Text="UserName:"></asp:Label>
&nbsp; </td>
                <td align="left" class="style4">
                    <asp:TextBox ID="TextBox1" runat="server" Width="149px"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox1" ErrorMessage="*用戶名不能為空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
                        Text="Password:"></asp:Label>
&nbsp;</td>
                <td align="left">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TextBox2" ErrorMessage="*密碼不能為空"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" class="style3">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" 
                        Height="38px" Text="Region:"></asp:Label>
&nbsp;</td>
                <td align="left" class="style3" colspan="2">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="38px" Width="125px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnLogin" runat="server" BackColor="#3399FF" 
                        BorderColor="#0066FF" Font-Bold="True" onclick="btnLogin_Click" Text="Login" />
                </td>
                <td align="left" colspan="2">
                    &nbsp;<asp:Button ID="btnRes" runat="server" BackColor="#CC3300" 
                        BorderColor="#CC3300" Text="Reset" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Red" 
                        Text="© Foxconn Precision Component (BJ) Co., Ltd. "></asp:Label>
                    <br />
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#3399FF" 
                        Text="Powered ByBF-IT Group© 2003"></asp:Label>
                    <br />
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#3399FF" 
                        Text="Si iHub Web Author:BF-IT Web Group "></asp:Label>
                </td>
            </tr>
        </table>
    
        <br />
    
    </div>
    </form>
</body>
</html>
