<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserUpdatePwd.aspx.cs" Inherits="SysConfig_UserUpdatePwd2"
    StylesheetTheme="SkinFile" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密碼修改</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID=lblTitle Text="修改密碼"></asp:Label></td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="2">
                    <asp:Button ID="btnModify" runat="server" SkinID="MainButton" Text="保存" OnClick="btnModify_Click"
                        meta:resourcekey="btnModifyResource1" AccessKey="S" ToolTip="ALT+S"></asp:Button>
                    <asp:Button ID="btnExit" runat="server" SkinID="MainButton" Text="退出" CausesValidation="False"
                        OnClick="btnExit_Click" meta:resourcekey="btnExitResource1"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 5px" align="left">
                    &nbsp;<asp:Label ID="lblMessage" runat="server" meta:resourcekey="LbMessageResource1"
                        ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td class="founction" colspan="2">
                    <table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="TD" style="width: 99px; height: 12pt;" align="right">
                                <asp:Label ID="lblUserID_l" runat="server" SkinID=lblRed meta:resourcekey="lblUserID_lResource1"
                                    Text="用戶ID :"></asp:Label></td>
                            <td class="TD" align="center" style="width: 30px; height: 12pt;">
                            </td>
                            <td class="TdLeft" style="height: 12pt; width: 155px;">
                                <asp:Label ID="lblUserID" runat="server" meta:resourcekey="lbUserIDResource1"></asp:Label></td>
                            <td class="TdLeft" style="height: 12pt">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 99px; height: 12pt;" align="right">
                                <asp:Label ID="lblOldPW" runat="server" SkinID=lblRed meta:resourcekey="lblOldPWResource1"
                                    Text="舊密碼 :"></asp:Label></td>
                            <td class="TD" align="center" style="width: 30px; height: 12pt;">
                            </td>
                            <td class="TD" style="height: 12pt; width: 155px;" align="left">
                                <asp:TextBox ID="txtOldPW" runat="server" MaxLength="20" TextMode="Password" meta:resourcekey="txtOldPWResource1"></asp:TextBox></td>
                            <td class="TdLeft" style="height: 12pt">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 99px; height: 10pt" align="right">
                                <asp:Label ID="lblUserName" runat="server" SkinID=lblRed meta:resourcekey="lblUserNameResource1"
                                    Text="新密碼 :"></asp:Label></td>
                            <td class="TD" style="height: 10pt; width: 30px;" align="center">
                            </td>
                            <td class="TD" style="height: 10pt; width: 155px;" align="left">
                                <asp:TextBox ID="txtNewPW" runat="server" MaxLength="20" TextMode="Password" meta:resourcekey="txtNewPWResource1"></asp:TextBox></td>
                            <td class="TD" style="height: 10pt" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 99px; height: 10pt" align="right">
                                <asp:Label ID="lblNewPW" runat="server" SkinID=lblRed meta:resourcekey="lblNewPWResource1"
                                    Text="新密碼(again) :"></asp:Label></td>
                            <td class="TD" style="height: 10pt; width: 30px;" align="center">
                            </td>
                            <td class="TD" style="height: 10pt; width: 155;" align="left">
                                <asp:TextBox ID="txtNewPWagain" runat="server" MaxLength="20" TextMode="Password"
                                    meta:resourcekey="txtNewPWagainResource1"></asp:TextBox></td>
                            <td class="TD" style="height: 10pt" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" align="right" style="width: 136px; height: 20px;" colspan="4">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
