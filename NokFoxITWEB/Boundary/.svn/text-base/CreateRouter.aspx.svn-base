<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateRouter.aspx.cs" Inherits="Boundary_CreateRouter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>未命名頁面</title>
</head>
<body background="../Images/1170744771831.jpg">
    <form id="form1" runat="server">
    <div>
        <span style="color: #0000ff">創建新路由界面:</span>
        <table>
            <tr>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 296px">
                    機種:&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlModel" runat="server" Width="188px" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                    </asp:DropDownList><br />
                    31料號:
                    <asp:DropDownList ID="DropDownListSPart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSPart_SelectedIndexChanged"
                        Width="187px">
                    </asp:DropDownList></td>
                    <td>
                    
                    </td>
                    <td>
                        <strong><span style="font-size: 9pt">保存類型：</span></strong><asp:DropDownList ID="DropDownListRoute" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRoute_SelectedIndexChanged"
                         Width="118px">
                         <asp:ListItem Value="S">SMT路由</asp:ListItem>
                         <asp:ListItem Value="T">SMT_TEST路由</asp:ListItem>
                         <asp:ListItem Value="A">Assembly路由</asp:ListItem>
                         <asp:ListItem Value="P">PACK路由</asp:ListItem>
                     </asp:DropDownList>&nbsp; &nbsp;<asp:Button ID="ButtonSave" runat="server" Text="保存路由" Width="74px" OnClick="ButtonSave_Click"  /></td>
            </tr>
            <tr>
                <td style="width: 296px" valign="top">
                    <asp:ListBox ID="ListBoxRouter" runat="server" Height="228px" Width="295px">
                    </asp:ListBox>
                </td>
                <td style="width: 16px">
                    <asp:Button ID="ButtonSelect" runat="server" Text=">>" Width="58px" OnClick="ButtonSelect_Click"  /><br />
                    <br />
                    <asp:Button ID="ButtonUnselect" runat="server" Text="<<" Width="58px" OnClick="ButtonUnselect_Click" />
                </td>
                <td>
                <asp:ListBox ID="ListBoxRouterEdit" runat="server" Height="228px" Width="295px" SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td>
                    <asp:Button ID="ButtonUP" runat="server" Text="UP" OnClick="ButtonUP_Click" Width="61px" /><br />
                    <br />
                    <asp:Button ID="ButtonDOWN" runat="server" Text="DOWN" OnClick="ButtonDOWN_Click" />
                 
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>