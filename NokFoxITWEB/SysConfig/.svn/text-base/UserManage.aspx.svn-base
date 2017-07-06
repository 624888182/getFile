<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManage.aspx.cs" Inherits="SysConfig_UserManage2"
    EnableEventValidation="false" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用戶管理</title>
</head>
<body>
    <form id="formUserManage" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" Text="用戶管理" SkinID="lblTitle"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 27px">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblUserID" runat="server" SkinID="lblMain">用戶ID : </asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtUserID" runat="server" CssClass="inputcss" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblUserName" runat="server" SkinID="lblMain">用戶名稱 : </asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="inputcss" MaxLength="100"
                                    SkinID="txtMain"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblRoleCode" runat="server" SkinID="lblMain">操作權限  :</asp:Label></td>
                            <td class="TD">
                                <asp:DropDownList ID="ddlRole" runat="server" SkinID="ddlMain">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnFind_Click" Text="查詢" SkinID="mainButton" />
                    <asp:Button ID="btnOut" runat="server" OnClick="btnOut_ServerClick" Text="返回" AccessKey="E"
                        ToolTip="ALT+E" SkinID="MainButton" /></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="15" OnRowCreated="gvList_RowCreated" OnRowDataBound="gvList_RowDataBound"
                        OnRowDeleting="gvList_RowDeleting" DataSourceID="dsOper" SkinID="gvMain">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblItemHead" runat="server" Height="100%" Text="序號" Width="100%" Visible="true"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Width="100%" Visible="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblUserIDHead" runat="server" Height="100%" Text="用戶ID" Width="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="#" onmouseover="this.style.cursor='hand';">
                                        <asp:Label ID="lblUserID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'
                                            Width="100%"></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblUserNameHead" runat="server" Height="100%" Text="用戶名稱" Width="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserName") %>'
                                        Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoleNameHead" runat="server" Height="100%" Text="操作權限" Width="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "OperRoleGpCode") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMailHead" runat="server" Height="100%" Text="郵箱" Width="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Mail") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblTelHead" runat="server" Height="100%" Text="電話" Width="100%"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Tel") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle CssClass="LeftPagerStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="dsOper" runat="server" ConvertNullToDBNull="True" DataObjectTypeName="FIH.Security.db.UsersInfo"
            DeleteMethod="Delete" EnablePaging="True" OnSelecting="dsOper_Selecting" SelectCountMethod="findCount"
            SelectMethod="find" TypeName="FIH.Security.db.Users" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="users" Type="Object" />
                <asp:ControlParameter ControlID="gvList" Name="startRowIndex" PropertyName="PageIndex"
                    Type="Int32" />
                <asp:ControlParameter ControlID="gvList" Name="maximumRows" PropertyName="PageSize"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
