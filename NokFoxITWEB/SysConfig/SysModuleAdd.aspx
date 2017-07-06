<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysModuleAdd.aspx.cs" Inherits="SysConfig_SysModuleAdd2"
    StylesheetTheme="SkinFile" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="formOperate" runat="server" method="post">
        <table border="0" align="center" cellpadding="0" cellspacing="0" width="95%" id="TABLE6"
            class="FounctionTable">
            <tr>
                <td class="titleImgAdd" style="height: 20px">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd" Text="新增系統模組"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="1">
                    <table id="Table2" style="width: 100%; height: 10px;" cellspacing="0" cellpadding="0"
                        width="100%" border="0">
                        <tr>
                            <td class="buttonArea" colspan="2" style="width: 502px;">
                                &nbsp; &nbsp;&nbsp;<asp:Button ID="btnCommit" runat="server" SkinID="MainButton"
                                    OnClick="btnCommit_Click" Text="保存" meta:resourcekey="btnCommitResource1" AccessKey="S"
                                    ToolTip="ALT+S" /><asp:Button ID="btnExit" runat="server" CausesValidation="False"
                                        SkinID="MainButton" OnClick="btnExist_Click" Text="返回" meta:resourcekey="btnExitResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px; width: 502px;" align="left">
                                &nbsp;<asp:Label ID="lblMessage" runat="server" SkinID="lblMessage" meta:resourcekey="lblMessageResource1"
                                    ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="1">
                    <table class="styleBodyTableInTable" id="Table3" cellspacing="0" cellpadding="2"
                        width="100%" border="0">
                        <tr>
                            <td class="TD" style="width: 137px" align="right">
                                <asp:Label ID="lblModuleCode" runat="server" SkinID="lblRed" Width="96px" meta:resourcekey="lbModuleCodeResource1">模組代碼 :</asp:Label></td>
                            <td class="TD" align="center" style="width: 19px">
                            </td>
                            <td class="TdLeft" style="height: 100%; width: 155px;">
                                <asp:TextBox ID="txtModuleCode" runat="server" MaxLength="20" meta:resourcekey="txtModuleCodeResource1"></asp:TextBox></td>
                            <td class="TdLeft" style="width: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 10pt" align="right">
                                <asp:Label ID="lblModuleNameCn" runat="server" SkinID="lblRed" Width="100px" meta:resourcekey="lblModuleNameCnResource1">模組中文名稱 :</asp:Label></td>
                            <td class="TD" style="height: 10pt; width: 19px;" align="center">
                            </td>
                            <td class="TD" style="height: 10pt; width: 155px;" align="left">
                                <asp:TextBox ID="txtModuleNameCn" runat="server" MaxLength="30" SkinID="txtMain"
                                    meta:resourcekey="txtModuleNameCnResource1"></asp:TextBox></td>
                            <td class="TD" style="height: 10pt; width: 8px;" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px" align="right">
                                <asp:Label ID="lblModuleNameEn" runat="server" SkinID="lblRed" Width="101px" meta:resourcekey="lblModuleNameEnResource1">模組英文名稱 :</asp:Label></td>
                            <td class="TD" align="center" style="width: 19px">
                            </td>
                            <td class="TdLeft" width="155">
                                <asp:TextBox ID="txtModuleNameEn" runat="server" MaxLength="30" meta:resourcekey="txtModuleNameEnResource1"></asp:TextBox></td>
                            <td class="TdLeft" style="width: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 0.196pt" align="right">
                                <asp:Label ID="lblParentModuleCode" runat="server" SkinID="lblRed" Width="101px"
                                    meta:resourcekey="lblParentModuleCodeResource1">父模組 :</asp:Label></td>
                            <td class="TD" style="height: 0.196pt; width: 19px;" align="center">
                            </td>
                            <td class="TD" style="height: 0.196pt; width: 155px;" align="left">
                                <asp:TextBox ID="txtParentModuleCode" runat="server" MaxLength="30" SkinID="txtMain"
                                    meta:resourcekey="txtParentModuleCodeResource1"></asp:TextBox></td>
                            <td class="TD" style="height: 0.196pt; width: 8px;" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 22px;" align="right">
                                <asp:Label ID="lblOperCodeGroup" runat="server" SkinID="lblRed" meta:resourcekey="lblOperCodeGroupResource1">操作權限組 :</asp:Label></td>
                            <td class="TD" align="center" style="width: 19px; height: 22px;">
                            </td>
                            <td class="TdLeft" colspan="2" style="height: 22px">
                                <asp:TextBox ID="txtOperCodeGroup" runat="server" Width="317px" MaxLength="120" SkinID="txtMain"
                                    meta:resourcekey="txtOperCodeGroupResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px" align="right">
                                <asp:Label ID="lblURL" runat="server" SkinID="lblRed" Width="89px" meta:resourcekey="lbURLResource1">連接程序 :</asp:Label></td>
                            <td class="TD" align="center" style="width: 19px">
                            </td>
                            <td class="TdLeft" colspan="2">
                                <asp:TextBox ID="txtURL" runat="server" Width="317px" MaxLength="80" SkinID="txtMain"
                                    meta:resourcekey="txtURLResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 12pt;" align="right">
                                <asp:Label ID="lblSysName" runat="server" ForeColor="#000033" meta:resourcekey="lblSysNameResource1">系統名稱 :</asp:Label>&nbsp;</td>
                            <td class="TD" align="center" style="width: 19px; height: 12pt;">
                            </td>
                            <td class="TdLeft" colspan="2" style="height: 12pt">
                                <asp:TextBox ID="txtSysName" runat="server" MaxLength="30" Width="317px" SkinID="txtMain"
                                    meta:resourcekey="txtSysNameResource1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 12pt;" align="right">
                                <asp:Label ID="lblIsOperModule" runat="server" ForeColor="#000033" SkinID="lblRed"
                                    meta:resourcekey="lblIsOperModuleResource1">是否功能模組 :</asp:Label>&nbsp;</td>
                            <td class="TD" align="center" style="width: 19px; height: 12pt;">
                            </td>
                            <td class="TDLeft" colspan="2" style="height: 12pt">
                                <asp:CheckBox ID="chkIsOperModule" runat="server" meta:resourcekey="chkIsOperModuleResource1" /></td>
                        </tr>
                        <tr>
                            <td class="TD" style="width: 137px; height: 12pt;" align="right">
                                <asp:Label ID="lblIsRole" runat="server" ForeColor="#000033" meta:resourcekey="lblIsRoleResource1">是否權限管控 :</asp:Label>&nbsp;</td>
                            <td class="TD" align="center" style="width: 19px; height: 12pt;">
                            </td>
                            <td class="TDLeft" colspan="2" style="height: 12pt">
                                <asp:CheckBox ID="chkIsRole" runat="server" meta:resourcekey="chkIsRoleResource1" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
