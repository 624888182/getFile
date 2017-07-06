<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.SmtBom" CodeFile="wfrmSmtBom.ascx.cs" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table border="0" cellpadding="0" cellspacing="0" class="DataGridFont">
    <tr>
        <td rowspan="4" width="50">
        </td>
        <td style="height: 29px">
            <asp:Label ID="lbPn" runat="server" Width="100px">PN</asp:Label></td>
        <td style="width: 283px; height: 29px">
            <asp:TextBox ID="tbPn" runat="server" Width="275px"></asp:TextBox></td>
        <td style="height: 29px; width: 1px;">
        </td>
        <td style="width: 82px; height: 29px">
        </td>
        <td rowspan="4">
            <font face="新細明體"></font><asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="Query" Width="80px" Height="28px" />
            <br />
            <asp:Button ID="btnImport" runat="server" Text="Import" Width="80px" Height="28px" OnClick="btnImport_Click"/></td>
    </tr>
    <tr>
        <td style="height: 29px">
            <asp:Label ID="lbPath" runat="server" Width="100px">tbPath</asp:Label></td>
        <td style="width: 283px; height: 29px">
            <input id="File2" style="width: 277px" type="file" runat ="server"/></td>
        <td style="height: 29px; width: 1px;">
            </td>
        <td style="width: 82px; height: 29px">
            </td>
    </tr>
    <tr>
        <td style="height: 12px">
            <font face="新細明體">
            </font></td>
        <td style="width: 283px; height: 12px">
            </td>
        <td style="height: 12px; width: 1px;">
        </td>
        <td style="width: 82px; height: 12px">
            </td>
    </tr>
</table>
<HR>
<asp:Label ID="Label1" runat="server" CssClass="DataGridFont"></asp:Label>
<br />
<asp:DataGrid ID="dgSMTBOM" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
    CssClass="DataGridFont" Font-Names="Verdana" Font-Size="10px" OnPageIndexChanged="dgSMTBOM_PageIndexChanged"
    PageSize="30" ShowFooter="True" Width="596px">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" />
    <AlternatingItemStyle BackColor="WhiteSmoke" />
    <ItemStyle BackColor="Cornsilk" Font-Size="10pt" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Italic="False" Font-Overline="False"
        Font-Size="Small" Font-Strikeout="False" Font-Underline="False" ForeColor="White"
        Height="20px" HorizontalAlign="Center" />
    <Columns>
        <asp:BoundColumn DataField="COMPONEMT" HeaderText="COMPONEMT">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="LOCATION" HeaderText="LOCATION">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="IMPORT_DATE" HeaderText="IMPORT_DATE"></asp:BoundColumn>
    </Columns>
</asp:DataGrid>
