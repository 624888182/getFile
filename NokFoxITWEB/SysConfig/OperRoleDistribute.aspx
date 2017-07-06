<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperRoleDistribute.aspx.cs"
    Inherits="SysConfig_RoleGroup" StylesheetTheme="SkinFile" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form2" runat="server">
        <table border="0" align="center" cellpadding="0" cellspacing="0" width="100%" style="text-align: left"
            width="100%" id="TABLE6" class="FounctionTable">
            <tr>
                <td class="titleImg" align="left">
                    <asp:Label ID="lblHead" SkinID="lblTitle" runat="server" meta:resourcekey="lbHeadResource1">權限分配</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="founction" colspan="2" style="height: 50px">
                    <table id="Table7" style="height: 10px;" cellspacing="0" cellpadding="0" width="100%"
                        border="0">
                        <tr>
                            <td style="height: 5px;" align="left">
                                <asp:Label ID="lblRole" runat="server" SkinID="lblMain" meta:resourcekey="lblRoleResource1">權限組:</asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" Width="141px" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"
                                    meta:resourcekey="DdlRoleResource1">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="buttonArea" align="left" colspan="2">
                                <asp:Button ID="btnCommit" runat="server" SkinID="MainButton" Text="保存" OnClick="btnCommit_Click"
                                    meta:resourcekey="btnCommitResource1" AccessKey="S" ToolTip="ALT+S"></asp:Button>
                                <asp:Button ID="btnExit" runat="server" SkinID="MainButton" Text="退出" CausesValidation="False"
                                    OnClick="btnExit_Click" meta:resourcekey="btnExitResource1" AccessKey="E" ToolTip="ALT+E">
                                </asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px" align="left">
                                &nbsp;<asp:Label ID="lblMessage" runat="server" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                    </table>
                    <table class="styleBodyTableInTable" cellspacing="0" cellpadding="2" align=left
                        border="0">
                        <tr valign="top">
                            <td>
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:TreeView ID="TreeView1" runat="server" Height="100%" meta:resourcekey="TreeView1Resource1"
                                                ShowLines="True" Width="100">
                                            </asp:TreeView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
