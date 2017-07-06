<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubNotice.aspx.cs"
    Inherits="Pub_Notice" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告管理</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="公告管理"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindLeft" style="height: 22px">
                                &nbsp;<asp:Label ID="lblTitle" runat="server" SkinID="lblMain">主旨:&nbsp;</asp:Label></td>
                            <td class="TdFindRight" style="height: 22px">
                                <asp:TextBox ID="txtTitle" runat="server" SkinID="txtMain" Width="300px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="查詢" />
                    <asp:Button ID="btnAdd" runat="server" Text="新增" SkinID="MainButton" OnClick="btnAdd_Click"
                        AccessKey="N" ToolTip="ALT+N" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" DataKeyNames="NoticeCode" OnRowCreated="gvList_RowCreated" OnPageIndexChanging="gvList_PageIndexChanging"  OnRowDeleting="gvList_RowDeleting">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/delete.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主旨">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="Detail" Text='<%# Bind("Title") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Memo" HeaderText="內容" />
                <asp:BoundField DataField="UserName" HeaderText="創建人" />
                <asp:BoundField DataField="CreateDate" DataFormatString="{0:yyyy/M/dd}" HeaderText="創建日期" />
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="PagerStyleLeft" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <EditRowStyle BorderStyle="None" />
        </asp:GridView>
    </form>
</body>
</html>
