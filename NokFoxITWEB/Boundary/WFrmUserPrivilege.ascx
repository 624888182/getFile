<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmUserPrivilege.ascx.cs" Inherits="Boundary_WFrmUserPrivilege" %>

<table width="100%" height="100%" border="1">
  <tr>
    <td height="27" valign="top">&nbsp;
      <asp:Label ID="LblUserID" runat="server" Text="User ID" Width="78px"></asp:Label>
        <asp:TextBox ID="TbUserID" runat="server"></asp:TextBox>
    <asp:Button ID="BtnQuery" runat="server" Text="Query" OnClick="BtnQuery_Click" /></td>
  </tr>
  <tr>
    <td valign="top"><asp:GridView ID="GridPrivileges" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" CssClass="FontDataGrid" Height="1px" OnDataBound="GridPrivileges_DataBound">
        <Columns>
            <asp:BoundField DataField="ftabid" HeaderText="ftabid" />
            <asp:BoundField DataField="fdesktopsrc" HeaderText="fdesktopsrc" />
            <asp:BoundField DataField="description" HeaderText="description" />
            <asp:TemplateField HeaderText="Privileges">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserID") %>' Visible="False"></asp:Label>
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="True" CssClass="FontDataGrid"
                        OnCheckedChanged="CheckAll_CheckedChanged" Text="All Privileges" />
                </HeaderTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Wrap="False" />
        </asp:GridView>
        <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="Update" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </td>
  </tr>
</table>
