<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserOperateDept.aspx.cs"
    Inherits="SysConfig_UserOperateDept" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>選擇員工負責的部門</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImgAdd" colspan="6">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd">選擇部門</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblDeptCode" runat="server" SkinID="lblMain">部門編號 : </asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDeptCode" runat="server" CssClass="inputcss" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblDeptName" runat="server" SkinID="lblMain">部門名稱 : </asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDeptName" runat="server" CssClass="inputcss" MaxLength="100"
                                    SkinID="txtMain"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblPrice" runat="server" SkinID="lblMain">費用代碼 : </asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="inputcss" MaxLength="100" SkinID="txtMain"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="buttonAread">
                    <asp:Button ID="btnFind" runat="server" Text="查詢" SkinID=mainButton OnClick="btnFind_Click" />&nbsp;
                    <asp:Button ID="btnExit" runat="server" Text="關閉" SkinID=mainButton OnClick="btnExit_Click" />
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvDept" runat="server" SkinID="gvMain" AllowPaging="True" OnPageIndexChanging="gvDept_PageIndexChanging" DataKeyNames="DepartmentID" OnRowDeleting="gvDept_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="DepartmentID" HeaderText="部門ID" Visible=false />
                            <asp:TemplateField HeaderText="部門編號">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" CommandName="Delete" CommandArgument='<%# Bind("DepartmentID")%>' runat="server" Text= '<%# Bind("DepartmentCode") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DepartmentName" HeaderText="部門名稱" />
                            <asp:BoundField DataField="PriceCode" HeaderText="費用代碼" />
                        </Columns>
                    </asp:GridView>
                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
