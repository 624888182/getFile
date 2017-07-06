<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmMaterialAnalysis" CodeFile="WFrmMaterialAnalysis.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
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
		<td rowSpan="4"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:Label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
		<td></td>
		<td>
			<asp:Label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
	</tr>
	<tr>
		<td style="HEIGHT: 14px">
			<asp:label id="lblFactory" runat="server" Width="100px">Factory</asp:label></td>
		<TD style="WIDTH: 198px; HEIGHT: 14px">
			<asp:dropdownlist id="ddlFactory" runat="server" Width="155px" AutoPostBack="True" onselectedindexchanged="ddlFactory_SelectedIndexChanged">
				<asp:ListItem Value="G02_2F">G02_2F</asp:ListItem>
				<asp:ListItem Value="E10_4F">E10_4F</asp:ListItem>
				<asp:ListItem Value="TY_C1_2F">TY_C1_2F</asp:ListItem>
				<asp:ListItem Value="India_B1">India_B1</asp:ListItem>
			</asp:dropdownlist></TD>
		<td style="HEIGHT: 14px"><FONT face="新細明體">
				<asp:label id="lblLine" runat="server" Width="100px">Line</asp:label></FONT></td>
		<td style="HEIGHT: 14px">
			<asp:dropdownlist id="ddlLine" runat="server" Width="155px"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td>
			<asp:label id="lblMONumber" runat="server" Width="100px">MO Number</asp:label></td>
		<td style="WIDTH: 198px">
			<asp:dropdownlist id="ddlMONumber" runat="server" Width="155px"></asp:dropdownlist></td>
		<td>
			<asp:label id="lblMaterialNO" runat="server" Width="100px">Material NO</asp:label></td>
		<td>
			<asp:textbox id="tbMaterialNO" runat="server"></asp:textbox></td>
	</tr>
</table>
<hr>
<asp:datagrid id="dgMaterial" runat="server" CssClass="DataGridFont" BorderStyle="None" BorderWidth="1px"
	BackColor="White" Font-Size="11px" Font-Names="Verdana" BorderColor="#CCCCCC">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
</asp:datagrid>
