<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.wfrmShippingControlInformation" CodeFile="wfrmShippingControlInformation.ascx.cs" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table border="0" cellpadding="0" cellspacing="0" class="DataGridFont">
    <tr>
        <td rowspan="4" width="50">
        </td>
        <td style="height: 19px">
        </td>
        <td style="width: 198px; height: 19px">
        </td>
        <td style="height: 19px">
        </td>
        <td style="width: 156px; height: 19px">
        </td>
        <td rowspan="4">
            <font face="新細明體">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </font>
            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="Query" Width="80px" /></td>
    </tr>
    <tr>
        <td style="height: 24px">
            <asp:Label ID="lblWorkOrder" runat="server" Width="100px">WorkOrder</asp:Label></td>
        <td style="width: 198px; height: 24px">
            <asp:TextBox ID="tbWorkOrder" runat="server"></asp:TextBox></td>
        <td style="height: 24px">
            <asp:Label ID="lblCartonID" runat="server" Width="100px">Carton ID</asp:Label></td>
        <td style="width: 156px; height: 24px">
            <asp:TextBox ID="tbCartonID" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 24px">
            <font face="新細明體">
                <asp:Label ID="lblIMEI" runat="server" Width="100px">IMEI&Picasso&PID</asp:Label></font></td>
        <td style="width: 198px; height: 24px">
            <asp:TextBox ID="tbIMEI" runat="server"></asp:TextBox></td>
        <td style="height: 24px">
        </td>
        <td style="width: 156px; height: 24px">
        </td>
    </tr>
</table>
<HR>
<asp:Label ID="Label1" runat="server" CssClass="DataGridFont"></asp:Label><asp:DataGrid
    ID="dgShippingControlInfor" runat="server" AllowPaging="True" BackColor="White"
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CssClass="DataGridFont"
    Font-Names="Verdana" Font-Size="10px" PageSize="30" ShowFooter="True" AutoGenerateColumns="False" OnPageIndexChanged="dgShippingControlInfor_PageIndexChanged">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" />
    <AlternatingItemStyle BackColor="WhiteSmoke" />
    <ItemStyle BackColor="Cornsilk" Font-Size="10pt" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Font-Italic="False" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" Height="20px" />
    <Columns>
        <asp:BoundColumn DataField="model" HeaderText="MODEL">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <HeaderStyle Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="work_order" HeaderText="WORK_ORDER">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="serial_no" HeaderText="SERIAL_NO">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="imei" HeaderText="IMEI">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="ddate" HeaderText="DDATE">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="in_date" HeaderText="IN_DATE">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="status" HeaderText="STATUS">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="productid" HeaderText="PRODUCTID">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="cust_pno" HeaderText="CUST_PNO">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
        <asp:BoundColumn DataField="carton_no" HeaderText="CARTON_NO">
            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" />
        </asp:BoundColumn>
    </Columns>
</asp:DataGrid>
