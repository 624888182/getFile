<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.BomComPare" CodeFile="wfrmBomComPare.ascx.cs" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table border="0" cellpadding="0" cellspacing="0" class="DataGridFont" style="width: 555px">
    <tr>
        <td rowspan="4" width="50">
        </td>
        <td style="height: 27px">
        </td>
        <td style="width: 198px; height: 27px">
        </td>
        <td style="height: 27px">
        </td>
        <td style="width: 61px; height: 27px">
        </td>
        <td rowspan="4" style="width: 137px">
            <font face="新細明體">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </font>
            <asp:Button ID="btnComPare" runat="server"  Text="ComPare" Width="80px" OnClick="btnComPare_Click" /></td>
        <td rowspan="4">
        </td>
    </tr>
    <tr>
        <td style="height: 38px">
            <asp:Label ID="lbPn" runat="server" Width="100px">PN</asp:Label></td>
        <td style="width: 198px; height: 38px">
            <asp:TextBox ID="tbPN" runat="server" Width="206px"></asp:TextBox></td>
        <td style="height: 38px">
            </td>
        <td style="width: 61px; height: 38px">
            </td>
    </tr>
    <tr>
        <td style="height: 27px">
            <font face="新細明體">
                </font></td>
        <td style="width: 198px; height: 27px">
            </td>
        <td style="height: 27px">
        </td>
        <td style="width: 61px; height: 27px">
        </td>
    </tr>
</table>
<HR>
<asp:Label ID="Label1" runat="server" CssClass="DataGridFont"></asp:Label><asp:DataGrid
    ID="dgBom" runat="server" AllowPaging="True" BackColor="White"
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CssClass="DataGridFont"
    Font-Names="Verdana" Font-Size="10px" PageSize="30" ShowFooter="True" AutoGenerateColumns="False"  Width="830px">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" />
    <AlternatingItemStyle BackColor="WhiteSmoke" />
    <ItemStyle BackColor="Cornsilk" Font-Size="10pt" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" 
        Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" Height="20px" />
    <Columns>
        <asp:BoundColumn DataField="PN" HeaderText="PN">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="SAPHonhaipn" HeaderText="SAPHonhaipn"></asp:BoundColumn>
        <asp:BoundColumn DataField="SAPLocation" HeaderText="SAPLocation"></asp:BoundColumn>
        <asp:BoundColumn DataField="SMTHonhaipn" HeaderText="SMTHonhaipn"></asp:BoundColumn>
        <asp:BoundColumn DataField="SMTLocation" HeaderText="SMTLocation"></asp:BoundColumn>
    </Columns>
</asp:DataGrid>
