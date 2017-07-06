<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysSetting.aspx.cs" Inherits="SysConfig_SysSetting"
    StylesheetTheme="SkinFile" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系統設置</title>
</head>
<body>
    <form id="formOperate" runat="server" method="post">
        <table border="0" align="center" cellpadding="0" cellspacing="0" width="100%" id="TABLE6"
            class="FounctionTable">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" Text="系統設計" SkinID="lblTitle"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <table id="Table2" style="width: 100%; height: 10px;" cellspacing="0" cellpadding="0"
                        width="100%" border="0">
                        <tr>
                            <td class="buttonArea" colspan="2" style="width: 502px;">
                                <asp:Button ID="btnReset" runat="server" SkinID="MainButton" Text="重設" Visible="false"
                                    OnClick="btnReset_Click" meta:resourcekey="btnResetResource1" AccessKey="T" ToolTip="ALT+T">
                                </asp:Button>
                                <asp:Button ID="btnCommit" runat="server" SkinID="MainButton" Text="保存" OnClick="btnCommit_Click"
                                    meta:resourcekey="btnCommitResource1" AccessKey="S" ToolTip="ALT+S"></asp:Button>
                                <asp:Button ID="btnExit" runat="server" SkinID="MainButton" Text="退出" CausesValidation="False"
                                    OnClick="btnExit_Click" meta:resourcekey="btnExitResource1" AccessKey="E" ToolTip="ALT+E">
                                </asp:Button></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px; width: 502px;" align="left">
                                &nbsp;<asp:Label ID="lblMessage" runat="server" meta:resourcekey="lbMessageResource1"
                                    ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="TD" style="width: 155px" align="right">
                                <asp:Label ID="lblPageRow" runat="server" SkinID="lblMain" meta:resourcekey="lblPageRowResource1">每頁顯示行數 :</asp:Label></td>
                            <td class="TD" align="center" style="width: 10px">
                            </td>
                            <td class="TdLeft" style="width: 155px">
                                <asp:TextBox ID="txtPageRow" runat="server" MaxLength="20" SkinID="txtMain" meta:resourcekey="txtPageRowResource1"></asp:TextBox></td>
                            <td class="TdLeft" style="width: 93px">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 155px; height: 10pt" align="right">
                                <asp:Label ID="lblDefaultPasswd" runat="server" SkinID="lblMain" meta:resourcekey="lblDefaultPasswdResource1">系統默認密碼 :</asp:Label></td>
                            <td class="TD" style="height: 10pt; width: 10px;" align="center">
                            </td>
                            <td class="TD" style="height: 10pt; width: 155px;" align="left">
                                <asp:TextBox ID="txtDefaultPasswd" runat="server" MaxLength="30" meta:resourcekey="txtDefaultPasswdResource1"></asp:TextBox></td>
                            <td class="TD" style="height: 10pt; width: 93px;" align="left">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
