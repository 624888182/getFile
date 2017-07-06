<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs" Inherits="SysConfig_UserAdd2"
    StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用戶新增</title>

    <script language="javascript" type="text/javascript">
 function EnterToTab()
 {
    if(window.event.keyCode==13)
      window.event.keyCode=9;
 }
    </script>

</head>
<body>
    <form id="formUser" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImgAdd">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd">用戶新增</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="1" height="20">
                    <asp:Button ID="btnCommit" runat="server" Text="保存" OnClick="btnCommit_Click" AccessKey="S"
                        ToolTip="ALT+S" SkinID="mainButton"></asp:Button>
                    <asp:Button ID="btnEdit" runat="server" Text="修改" AccessKey="M" ToolTip="ALT+M" SkinID="mainButton"
                        OnClick="btnEdit_Click"></asp:Button>
                    <asp:Button ID="btnExit" runat="server" Text="退出" CausesValidation="False" OnClick="btnExit_Click"
                        SkinID="mainButton"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="4" class="TdMessage" style="height: 12px">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td class="founction" colspan="1">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="TextTable">
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblUserID" runat="server" SkinID="lblMain">用戶ID :</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtUserID" runat="server" MaxLength="20" SkinID="txtMain" onkeydown="EnterToTab();"
                                    Enabled="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblUserName" runat="server" SkinID="lblMain">用戶名稱 :</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" SkinID="txtMain" onkeydown="EnterToTab();"
                                    Enabled="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblMail" runat="server" SkinID="lblMain">郵箱 :</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtMail" runat="server" MaxLength="50" SkinID="txtMain" onkeydown="EnterToTab();" Width="319px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight">
                                <asp:Label ID="lblTel" runat="server" SkinID="lblMain">電話 :</asp:Label></td>
                            <td class="Tdleft">
                                <asp:TextBox ID="txtTel" runat="server" MaxLength="20" SkinID="txtMain" onkeydown="EnterToTab();"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblRemark" runat="server" SkinID="lblMain">備注 :</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="20" SkinID="txtMain" TextMode="MultiLine"
                                    Width="324px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblRoleCode" runat="server" SkinID="lblMain">操作權限 :</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:DropDownList ID="ddlRoleCode" runat="server" SkinID="ddlMain" onkeydown="EnterToTab();">
                                </asp:DropDownList></td>
                        </tr>
                        <tr visible=false>
                            <td style="height: 22px">
                            </td>
                            <td style="height: 22px" visible=false>
                                <asp:TextBox ID="txtDeptNo" runat="server" Width="28px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtPassWD" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtIsOnline" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtGradeCode" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtGradeName" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtPositionCode" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtPositionName" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtPositionSeries" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtPositionSeriesName" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtInCompanyDate" runat="server" Width="26px" Visible=false></asp:TextBox>
                                <asp:TextBox ID="txtStatus" runat="server" Width="26px" Visible=false></asp:TextBox>
                            </td>
                        </tr>                        
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" runat=server visible=false>
                        <tr>
                            <td class="buttonArea" style="width: 625px">
                                <asp:Button ID="btnSX" runat="server" Text="刷新" SkinID="mainButton" OnClick="btnSX_Click" />
                                <asp:Button ID="btnAddDept" runat="server" Text="新增" SkinID="mainButton" /></td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvUserOperateDept" runat="server" SkinID="gvMain" DataKeyNames="ID"
                                    OnRowDeleting="gvUserOperateDept_RowDeleting" OnRowDataBound="gvUserOperateDept_RowDataBound"
                                    PageSize="500">
                                    <Columns>
                                        <asp:TemplateField HeaderText="刪除">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/delete.gif" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DepartmentCode" HeaderText="部門編號" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="部門名稱" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
