<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyMasterSearch.aspx.cs"
    Inherits="App_EnterFactApplyMasterSearch" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�~�ӥӽЬd��</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="�~�ӥӽЬd��"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable" style="width: 100%">
                        <tr>
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain">�׸�:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>            
            <tr>           
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="�d��" />                  
                    <asp:Button ID="btnColligateSearch" runat="server" SkinID="MainButton" Text="��X�d��"
                        AccessKey="Q" ToolTip="ALT+Q" OnClick="btnColligateSearch_Click" />    
                    <asp:Button ID="btnToExcel" runat="server" SkinID="MainButton" Text="�ɥX" OnClick="btnToExcel_Click"/>                                           
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="��^" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" SkinID="gvMain" DataKeyNames="ApplyCode" AllowPaging="True" OnRowCreated="gvList_RowCreated">
            <Columns>
                <asp:BoundField HeaderText="�渹" DataField="ApplyCode" />
                <asp:TemplateField HeaderText="�ӽФH">
                    <ItemTemplate>
                        <asp:Label ID="lblApplyID" runat="server" Text='<%# Bind("ApplyID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="�ӽг���" DataField="ApplyDepartment" />
                <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="�ӽФ��">
                    <ItemTemplate>
                       <asp:Label ID="lblApplyDate" runat="server" Text='<%# Bind("ApplyDate", "{0:yyyy/M/d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="�ӳX�H�m�W" DataField="StaffName" />
                <asp:BoundField HeaderText="���" DataField="Company" />
                <asp:BoundField HeaderText="�i�t�ت�" DataField="EnterFactReason" />
                <asp:BoundField HeaderText="���ݳ���" DataField="ReceptionDept" />
                <asp:BoundField HeaderText="���ݤH��" DataField="ReceptionStaff" />
                <asp:BoundField HeaderText="�J�t���" DataField="ActualEnterDate" />
                <asp:BoundField HeaderText="�X�t���" DataField="ActualLeaveDate" />
                <asp:BoundField HeaderText="�J�t���^" DataField="GateHouse" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
