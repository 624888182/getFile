<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmCDMATestDetailQuery.ascx.cs" Inherits="Boundary_WFrmCDMATestDetailQuery" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="4"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
		<td style="HEIGHT: 18px"><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px"></asp:dropdownlist></td>
		<td rowSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button><br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" Text="Export To Excel" onclick="btnExportExcel_Click" ></asp:button></td>
	</tr>
	<tr>
		<td><asp:label id="lblErrorCode" runat="server" Width="100px">Test Code</asp:label></td>
		<td><asp:textbox id="txtErrorCode" runat="server"></asp:textbox></td>		
		<td><asp:label id="lblPorF" runat="server" Width="60px">Pass/Fail</asp:label></td>
		<td><asp:dropdownlist id="ddlPorF" runat="server" Width="155px">
				<asp:ListItem Value="Fail" Selected="True"></asp:ListItem>
				<asp:ListItem Value="Pass"></asp:ListItem>
			</asp:dropdownlist></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="lblPID" runat="server" Width="100px">ProductID</asp:label></td>
		<td><asp:textbox id="txtPID" runat="server"></asp:textbox></td>	 
		<td></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px">
	<asp:datagrid id="dgTestStationData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" OnPageIndexChanged="dgTestStationData_PageIndexChanged">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
</div>