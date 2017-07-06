<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactConfirm.aspx.cs"
    Inherits="App_EnterFactApplyMaster" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�D�޼f��</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="�D�޼f��"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindLeft" width="100px" style="height: 22px">
                                <asp:Label ID="lblConfirmType" runat="server" SkinID="lblMain">�֭�ŧO:</asp:Label></td>
                            <td class="TdFindRight" width="100px" style="height: 22px">
                                <asp:DropDownList ID="ddlConfirmType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConfirmType_SelectedIndexChanged" Width="80px">
                                    <asp:ListItem Selected="True" Value="1">�t���D��</asp:ListItem>
                                    <asp:ListItem Value="2">�B�ťD��</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="TdFindRight" width="100px" style="height: 22px">
                                <asp:DropDownList ID="ddlConfirmType1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConfirmType1_SelectedIndexChanged" Width="80px">
                                    <asp:ListItem Value="All">����</asp:ListItem>
                                    <asp:ListItem Value="ConfirmN" Selected="True">���B�z</asp:ListItem>
                                    <asp:ListItem Value="ConfirmY">�w�B�z</asp:ListItem>
                                </asp:DropDownList></td>                                                            
                            <td class="TdFindLeft" width="50px" style="height: 22px">
                                <asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain">�׸�:</asp:Label></td>
                            <td class="TdFindRight" style="height: 22px">
                                <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>                             
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="�d��" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="��^" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" DataKeyNames="ApplyCode" OnRowCreated="gvList_RowCreated" OnRowCommand="gvList_RowCommand" OnPageIndexChanging="gvList_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="ibtnConfirmY" runat="server" AlternateText="�֭�" CommandName="ConfirmY" ImageUrl="~/App_Themes/SkinFile/images/Approve_Y.GIF"/>--%>
                        <asp:LinkButton ID="lbtnConfirmY" CommandName="ConfirmY" runat="server">�q�L</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:ImageButton ID="ibtnConfirmN" runat="server" AlternateText="�ڵ�" CommandName="ConfirmN" ImageUrl="~/App_Themes/SkinFile/images/Approve_N.GIF" />--%>
                        <asp:LinkButton ID="lbtnConfirmN" CommandName="ConfirmN" runat="server">�ڵ�</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�渹">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnApplyCode" CommandName="Detail" runat="server" Text='<%# Bind("ApplyCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                </asp:TemplateField>
                <asp:BoundField DataField="DepartmentName" HeaderText="�ӽг���" />
                <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="�ӽФ��">
                    <ItemTemplate>
                        <asp:Label ID="lblApplyDate" runat="server" Text='<%# Bind("ApplyDate", "{0:yyyy/M/d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="�ӽФH" />
                <asp:BoundField DataField="Tel" HeaderText="�p�Y�q��" />
                <asp:BoundField DataField="IsBUMgrConfirm" HeaderText="�ݳB�Ů֭�" />
                <asp:TemplateField HeaderText="���A">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnWorkFlow" AlternateText="ñ�֬y�{" runat="server" CommandName="WorkFlow" ImageUrl="~/App_Themes/SkinFile/images/Process.gif" />
                        <%# PubFunction.GetStatusNameByStatus((DataBinder.Eval(Container, "DataItem.Status")).ToString(), (DataBinder.Eval(Container, "DataItem.IsBUMgrConfirm")).ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName1" HeaderText="�t���֭�D��" />
                <asp:BoundField DataField="UserName2" HeaderText="�B�Ů֭�D��" />
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
