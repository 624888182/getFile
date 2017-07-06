<%@ Control Language="C#"  CodeFile="WFrmDownLoadFlatFile.ascx.cs" Inherits="SFCQuery.Boundary.WFrmDownLoadFlatFile" %>
<%@ Register Src="../WebControler/modeltitle.ascx" TagName="modeltitle" TagPrefix="uc1" %>
<uc1:modeltitle ID="Modeltitle1" runat="server" />
<table class="DataGridFont">
    <tr>
        <td><asp:Label ID="lblModel" runat=server Text="Model" Width="100px" CssClass="Font"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlModel" runat="server" Width="155px">
            </asp:DropDownList></td>
        <td>
            <asp:RadioButtonList ID="rblDataType" runat="server" Width="134px" CssClass="DataGridFont">
                <asp:ListItem Selected="True">Invoice NO</asp:ListItem>
                <asp:ListItem>P Order</asp:ListItem>
            </asp:RadioButtonList></td>
        <td>
            <asp:TextBox ID="tbInvoiceNO" runat="server"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Button ID="btnDownLoad" runat="server" Text="DownLoad" OnClick="btnDownLoad_Click" CssClass="DataGridFont" Width="90px" /></td>
    </tr>
</table>