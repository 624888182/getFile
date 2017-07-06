<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysModule.aspx.cs" Inherits="SysConfig_SysModule2"
    StylesheetTheme="SkinFile" EnableEventValidation="false" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系統模組管理</title>
</head>
<body>
    <form id="formBu" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" SkinID="lblTitle" runat="server" Text="系統模組管理"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindRight">
                                <asp:Label ID="lblModuleCode" runat="server" SkinID="lblMain" Width="87px" meta:resourcekey="lblModuleCodeResource1">模組代碼 : </asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtModuleCode" runat="server" SkinID="txtMain" CssClass="inputcss"
                                    MaxLength="100" meta:resourcekey="txtModuleCodeResource1"></asp:TextBox></td>
                            <td class="TdFindRight">
                                <asp:Label ID="lblModuleNameEn" runat="server" SkinID="lblMain" Width="84px" meta:resourcekey="lblModuleNameEnResource1">模組英文名稱: </asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtModuleNameEn" runat="server" CssClass="inputcss" MaxLength="100"
                                    SkinID="txtMain" meta:resourcekey="txtModuleNameEnResource1"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <input id="btnReset" visible="false" runat="server" SkinID="MainButton" onserverclick="btnReset_ServerClick"
                        type="button" value="重置" accesskey="T" />
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="查詢" meta:resourcekey="btnSearchResource1" />
                    &nbsp;<asp:Button ID="btnAdd" runat="server" Text="新增" SkinID="MainButton" OnClick="btnAdd_Click"
                        meta:resourcekey="btnAddResource1" AccessKey="N" ToolTip="ALT+N" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" meta:resourcekey="btnOutResource1" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="15" OnRowCreated="gvList_RowCreated" OnRowDataBound="gvList_RowDataBound" CssClass="GridView" OnRowDeleting="gvList_RowDeleting"
            DataSourceID="dsOper" meta:resourcekey="gvListResource1" SkinID="gvMain">
            <Columns>
                <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="&lt;img   src=&quot;../App_Themes/SkinFile/images/delete.gif&quot;     border=&quot;0&quot;&gt;"
                            ToolTip="删除?"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <HeaderTemplate>
                        <asp:Label ID="lblItemHead" runat="server" Height="100%" Text="序號" Width="100%" Visible="false"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text="item" Width="100%" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                    <HeaderTemplate>
                        <asp:Label ID="lblModuleCode" runat="server" Height="100%" Text="模組代碼" Width="100%"
                            meta:resourcekey="lblModuleCodeResource2"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <a href="#" onmouseover="this.style.cursor='hand';">
                            <asp:Label ID="lblModuleCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModuleCode") %>'
                                Width="100%" meta:resourcekey="lblModuleCodeResource3"></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                        <asp:Label ID="lblBModuleNameEn" runat="server" Height="100%" Text="模組英文名稱" Width="100%"
                            meta:resourcekey="lblBModuleNameEnResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModuleNameEn" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModuleNameEn") %>'
                            Width="100%" meta:resourcekey="lblModuleNameEnResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                    <HeaderTemplate>
                        <asp:Label ID="lblModuleNameCn" runat="server" Height="100%" Text="模組中文名稱" Width="100%"
                            meta:resourcekey="lblModuleNameCnResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModuleNameCn" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ModuleNameCn") %>'
                            Width="100%" meta:resourcekey="lblModuleNameCnsource3"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                    <HeaderTemplate>
                        <asp:Label ID="lblParentModuleCode" runat="server" Height="100%" Text="父模組" Width="100%"
                            meta:resourcekey="lblParentModuleCodeResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblParentModuleCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ParentModuleCode") %>'
                            Width="100%" meta:resourcekey="lblParentModuleCodeResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                    <HeaderTemplate>
                        <asp:Label ID="lblOperCodeGroup" runat="server" Height="100%" Text="操作權限組" Width="100%"
                            meta:resourcekey="lblOperCodeGroupResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblOperCodeGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OperCodeGroup") %>'
                            Width="100%" meta:resourcekey="lblOperCodeGroupResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                    <HeaderTemplate>
                        <asp:Label ID="lblURL" runat="server" Height="100%" Text="連接程序" Width="100%" meta:resourcekey="lblURLResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblURL" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.URL") %>'
                            Width="100%" meta:resourcekey="lblURLResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                    <HeaderTemplate>
                        <asp:Label ID="lblSysName" runat="server" Height="100%" Text="系統名稱" Width="100%" meta:resourcekey="lblSysNameResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSysName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SysName") %>'
                            Width="100%" meta:resourcekey="lblSysNameResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                    <HeaderTemplate>
                        <asp:Label ID="lblIsOperModule" runat="server" Height="100%" Text="是否功能模組" Width="100%"
                            meta:resourcekey="lblIsOperModuleResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsOperModule" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsOperModule") %>'
                            Width="100%" meta:resourcekey="lblIsOperModuleResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource10">
                    <HeaderTemplate>
                        <asp:Label ID="lblIsRole" runat="server" Height="100%" Text="是否權限管控" Width="100%"
                            meta:resourcekey="lblIsRoleResource1"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsRole" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsRole") %>'
                            Width="100%" meta:resourcekey="lblIsRoleResource2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="LeftPagerStyle" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <EditRowStyle BorderStyle="None" />
        </asp:GridView>
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
        <asp:ObjectDataSource ID="dsOper" runat="server" ConvertNullToDBNull="True"
            DataObjectTypeName="FIH.Security.db.SysModuleInfo" DeleteMethod="Delete" EnablePaging="True"
            OnSelecting="dsOper_Selecting" SelectCountMethod="findCount" SelectMethod="find"
            TypeName="FIH.Security.db.SysModule">
            <SelectParameters>
                <asp:Parameter Name="sysModule" Type="Object" />
                <asp:ControlParameter ControlID="gvList" Name="startRowIndex" PropertyName="PageIndex"
                    Type="Int32" />
                <asp:ControlParameter ControlID="gvList" Name="maximumRows" PropertyName="PageSize"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
