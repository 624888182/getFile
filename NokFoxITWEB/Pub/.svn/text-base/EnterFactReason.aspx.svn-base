<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactReason.aspx.cs"
    Inherits="Pub_EnterFactReason" StylesheetTheme="SkinFile" EnableEventValidation="false"
    meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入廠原因資料</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" SkinID="lblTitle" runat="server" Text="入廠原因資料"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindRight">
                                <asp:Label ID="lblReasonCode" runat="server" SkinID="lblMain" Width="87px" meta:resourcekey="lblReasonCodeResource1">原因代碼:</asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtReasonCode" runat="server" SkinID="txtMain" CssClass="inputcss"
                                    MaxLength="100" meta:resourcekey="txtModuleCodeResource1"></asp:TextBox></td>
                            <td class="TdFindRight">
                                <asp:Label ID="lblDescription" runat="server" SkinID="lblMain" Width="84px" meta:resourcekey="lblDescriptionResource1">原因描述:</asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="inputcss" MaxLength="100"
                                    SkinID="txtMain" meta:resourcekey="txtDescriptionResource1"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="查詢" meta:resourcekey="btnSearchResource1" />
                    <asp:Button ID="btnAdd" runat="server" Text="新增" SkinID="MainButton" OnClick="btnAdd_Click"
                        meta:resourcekey="btnAddResource1" AccessKey="N" ToolTip="ALT+N" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" meta:resourcekey="btnOutResource1" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="15"
            CssClass="GridView" DataSourceID="dsEnterFactReason" SkinID="gvMain" DataKeyNames="ReasonCode" OnRowDeleting="gvList_RowDeleting" OnRowDataBound="gvList_RowDataBound" >
            <FooterStyle CssClass="FooterStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/App_Themes/SkinFile/images/delete.gif" CommandName="Delete" AlternateText="刪除" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" CommandName="Edit"  AlternateText="修改"/>
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="原因代碼" SortExpression="ReasonCode">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnReasonCode" runat="server" Text='<%# Bind("ReasonCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" ReadOnly="True" />
            </Columns>
            <RowStyle CssClass="RowStyle" />
            <EditRowStyle BorderStyle="None" />
            <PagerStyle CssClass="LeftPagerStyle" />
            <HeaderStyle CssClass="HeaderStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        </asp:GridView>
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
        <asp:SqlDataSource ID="dsEnterFactReason" runat="server" ConnectionString="<%$ ConnectionStrings:ForeignStaffSqlServer %>"
            SelectCommand="SELECT ReasonCode, Description FROM pubEnterFactReason &#13;&#10;WHERE (ReasonCode LIKE N'%' + @ReasonCode + '%'  or   @ReasonCode  is null or @ReasonCode='' or @ReasonCode='''')&#13;&#10;and (Description LIKE N'%' + @Description  + '%'  or   @Description  is null or  @Description= '' or  @Description='''')&#13;&#10;&#13;&#10;" DeleteCommand="DELETE FROM pubEnterFactReason where ReasonCode = @ReasonCode">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtReasonCode" ConvertEmptyStringToNull="False"
                    Name="ReasonCode" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtDescription" ConvertEmptyStringToNull="False"
                    Name="Description" PropertyName="Text" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="ReasonCode" />
            </DeleteParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
