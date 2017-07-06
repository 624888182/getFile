<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyInCompany.aspx.cs" Inherits="App_EnterFactApplyInCompany" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�~�ӤH���J�t</title>
</head>
<body>
    <form id="formInCompany" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="�~�ӤH���J�t"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable" style="width: 100%">
                        <tr>                               
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblStaffName" runat="server" SkinID="lblMain">�m�W:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtStaffName" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>
                                
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblCompany" runat="server" SkinID="lblMain">���q:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtCompany" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>    
                                
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblCardNo" runat="server" SkinID="lblMain">�d��:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>                                                                                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="�d��" />&nbsp;
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="��^" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" DataKeyNames="ApplyCode,ItemNo" OnRowCreated="gvList_RowCreated" OnPageIndexChanging="gvList_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnIn" runat="server">�J�t</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>             
                <asp:BoundField DataField="StaffName" HeaderText="�m�W" />
                <asp:BoundField DataField="IDCardNo" HeaderText="�ҥ󸹽X" />
                <asp:BoundField DataField="Company" HeaderText="���q" />
                <asp:BoundField DataField="Tel" HeaderText="�q��" />
                <asp:BoundField DataField="ApplyCode" HeaderText="�渹" />
                <asp:BoundField DataField="ItemNo" HeaderText="����" />
                <asp:TemplateField HeaderText="�w�p�J�t���">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedEnterDate" runat="server" Text='<%# Bind("ExpectedEnterDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="takeItems" HeaderText="��a���~" />
                <asp:BoundField DataField="Memo" HeaderText="�Ƶ�" />
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
