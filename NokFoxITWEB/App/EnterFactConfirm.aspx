<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactConfirm.aspx.cs"
    Inherits="App_EnterFactApplyMaster" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主管審核</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="主管審核"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindLeft" width="100px" style="height: 22px">
                                <asp:Label ID="lblConfirmType" runat="server" SkinID="lblMain">核准級別:</asp:Label></td>
                            <td class="TdFindRight" width="100px" style="height: 22px">
                                <asp:DropDownList ID="ddlConfirmType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConfirmType_SelectedIndexChanged" Width="80px">
                                    <asp:ListItem Selected="True" Value="1">廠部主管</asp:ListItem>
                                    <asp:ListItem Value="2">處級主管</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="TdFindRight" width="100px" style="height: 22px">
                                <asp:DropDownList ID="ddlConfirmType1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConfirmType1_SelectedIndexChanged" Width="80px">
                                    <asp:ListItem Value="All">全部</asp:ListItem>
                                    <asp:ListItem Value="ConfirmN" Selected="True">未處理</asp:ListItem>
                                    <asp:ListItem Value="ConfirmY">已處理</asp:ListItem>
                                </asp:DropDownList></td>                                                            
                            <td class="TdFindLeft" width="50px" style="height: 22px">
                                <asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain">案號:</asp:Label></td>
                            <td class="TdFindRight" style="height: 22px">
                                <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>                             
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="查詢" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" DataKeyNames="ApplyCode" OnRowCreated="gvList_RowCreated" OnRowCommand="gvList_RowCommand" OnPageIndexChanging="gvList_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="ibtnConfirmY" runat="server" AlternateText="核准" CommandName="ConfirmY" ImageUrl="~/App_Themes/SkinFile/images/Approve_Y.GIF"/>--%>
                        <asp:LinkButton ID="lbtnConfirmY" CommandName="ConfirmY" runat="server">通過</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="ibtnConfirmN" runat="server" AlternateText="拒絕" CommandName="ConfirmN" ImageUrl="~/App_Themes/SkinFile/images/Approve_N.GIF" />--%>
                        <asp:LinkButton ID="lbtnConfirmN" CommandName="ConfirmN" runat="server">拒絕</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="單號">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnApplyCode" CommandName="Detail" runat="server" Text='<%# Bind("ApplyCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                </asp:TemplateField>
                <asp:BoundField DataField="DepartmentName" HeaderText="申請部門" />
                <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="申請日期">
                    <ItemTemplate>
                        <asp:Label ID="lblApplyDate" runat="server" Text='<%# Bind("ApplyDate", "{0:yyyy/M/d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="申請人" />
                <asp:BoundField DataField="Tel" HeaderText="聯係電話" />
                <asp:BoundField DataField="IsBUMgrConfirm" HeaderText="需處級核准" />
                <asp:TemplateField HeaderText="狀態">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnWorkFlow" AlternateText="簽核流程" runat="server" CommandName="WorkFlow" ImageUrl="~/App_Themes/SkinFile/images/Process.gif" />
                        <%# PubFunction.GetStatusNameByStatus((DataBinder.Eval(Container, "DataItem.Status")).ToString(), (DataBinder.Eval(Container, "DataItem.IsBUMgrConfirm")).ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
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
