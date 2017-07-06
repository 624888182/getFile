<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlterRouter.aspx.cs" Inherits="Boundary_AlterRouter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
</head>
<body background="../Images/1170744771831.jpg">
    <form id="form1" runat="server">
    <div>
        <div>
            <span style="color: #0000ff">修改路由界面:</span>
            <table>
                <tr>
                </tr>
            </table>
            <table>
                <tr>
                    <td >
                        機種:&nbsp;&nbsp; &nbsp;
                        <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" Width="188px" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                        </asp:DropDownList><br />
                        31料號: <asp:DropDownList ID="DropDownListSPart" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="DropDownListSPart_SelectedIndexChanged"
                            Width="186px">
                        </asp:DropDownList></td>
                        <td >
                        </td>
                        <td >
                        有效路由:&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownListRoute" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownListRoute_SelectedIndexChanged" Width="118px">
                            <asp:ListItem Value="S">SMT路由</asp:ListItem>
                            <asp:ListItem Value="T">SMT_TEST路由</asp:ListItem>
                            <asp:ListItem Value="A">Assembly路由</asp:ListItem>
                            <asp:ListItem Value="P">PACK路由</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 296px" valign="top">
                        <asp:ListBox ID="ListBoxRouter" runat="server" Height="228px" Width="295px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="ButtonSelect" runat="server" OnClick="ButtonSelect_Click" Text=">>"
                            Width="58px" /><br />
                        <br />
                        <asp:Button ID="ButtonUnselect" runat="server" OnClick="ButtonUnselect_Click" Text="<<"
                            Width="58px" />
                    </td>
                    <td>
                        <asp:ListBox ID="ListBoxRouterEdit" runat="server" Height="228px" SelectionMode="Multiple"
                            Width="295px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="ButtonUP" runat="server" OnClick="ButtonUP_Click" Text="UP" Width="61px" /><br />
                        <br />
                        <asp:Button ID="ButtonDOWN" runat="server" OnClick="ButtonDOWN_Click" Text="DOWN" />
                    </td>
                    <td>
                        <input name="CHECKBOX1" type="checkbox" value="開始時間" />開始時間<br />
                        <input name="CHECKBOX2" type="checkbox" value="結束時間" />結束時間</td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="保存路由" Width="74px"  /></td>
                </tr>
            </table>
        </div>
    
    </div>
    </form>
</body>
</html>
