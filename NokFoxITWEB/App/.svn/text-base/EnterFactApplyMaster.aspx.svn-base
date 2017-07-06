<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyMaster.aspx.cs"
    Inherits="App_EnterFactApplyMaster" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外來申請</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="外來申請主檔"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindLeft" style="height: 22px">
                                &nbsp;<asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain">單號:&nbsp;</asp:Label></td>
                            <td class="TdFindRight" style="height: 22px">
                                <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="12" SkinID="txtMain"></asp:TextBox></td>
                            <td class="TdFindLeft" style="height: 22px">
                                &nbsp;<asp:Label ID="lblStatus" runat="server" SkinID="lblMain">狀態:&nbsp;</asp:Label></td>                                
                            <td class="TdFindRight" style="height: 22px">  
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0">全部</asp:ListItem>
                                    <asp:ListItem Value="1">審核完成</asp:ListItem>
                                    <asp:ListItem Value="2">審核未完成</asp:ListItem>
                                    <asp:ListItem Value="4">審核完成發卡完成</asp:ListItem>
                                    <asp:ListItem Value="3">審核完成發卡未完成</asp:ListItem>
                                </asp:DropDownList></td>
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
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" OnRowDeleting="gvList_RowDeleting" DataKeyNames="ApplyCode" OnRowEditing="gvList_RowEditing" OnRowCreated="gvList_RowCreated" OnPageIndexChanging="gvList_PageIndexChanging" OnRowCommand="gvList_RowCommand" OnSelectedIndexChanged="gvList_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/delete.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="單號">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnApplyCode" CommandName="Detail" runat="server" Text='<%# Bind("ApplyCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="申請人" />
                <asp:BoundField DataField="Tel" HeaderText="聯係電話" />
                <asp:BoundField DataField="DepartmentName" HeaderText="申請部門" />
                <asp:BoundField DataField="IsBUMgrConfirm" HeaderText="需處級核准" />
                <asp:TemplateField HeaderText="狀態">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnWorkFlow" AlternateText="簽核流程" runat="server" CommandName="WorkFlow" ImageUrl="~/App_Themes/SkinFile/images/Process.gif" />
                        <%# PubFunction.GetStatusNameByStatus((DataBinder.Eval(Container, "DataItem.Status")).ToString(), (DataBinder.Eval(Container, "DataItem.IsBUMgrConfirm")).ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RejectReason" HeaderText="拒絕原因" />
                <asp:BoundField DataField="UserName1" HeaderText="廠部核准主管" />
                <asp:BoundField DataField="UserName2" HeaderText="處級核准主管" />
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
