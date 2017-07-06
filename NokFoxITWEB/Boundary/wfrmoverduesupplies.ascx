<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmoverduesupplies.ascx.cs" Inherits="Boundary_wfrmoverduesupplies" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>  
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>	
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="4"></td>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
        </td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td>
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
		<td rowSpan="4"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" OnClick="btnQuery_Click" ></asp:button></td>
		<TD rowSpan="4"><FONT face="新細明體"><IMG id="message" style="DISPLAY: none" alt="" src="../Images/Message.JPG"></FONT></TD>
	</tr> 
	<tr>
		<td></td>
		<td><asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td></td>
		<td><asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
</table>
<hr/>
<asp:Label id="lbltotal" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px">
	<asp:datagrid id="dgData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" OnPageIndexChanged="dgData_PageIndexChanged">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
</div>