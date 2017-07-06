<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmPhoneKeyQuery.ascx.cs" Inherits="Boundary_wfrmPhoneKeyQuery" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="3"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="120px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
		<td style="HEIGHT: 18px"><asp:label id="Label1" runat="server" Width="120px">PID/IMEI/Picasso</asp:label></td>
		<td style="HEIGHT: 18px"><asp:textbox id="tbIMEI" runat="server"></asp:textbox></td>
		<td rowSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button><br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<%--<asp:button id="btnExportExcel" runat="server" Width="100px" Text="Export To Excel" onclick="btnExportExcel_Click" ></asp:button>--%></td>
	</tr>
	<%--<tr>
		<td><asp:label id="lblIMEI" runat="server" Width="100px">IMEI/Picasso</asp:label></td>
		<td><asp:textbox id="tbIMEI" runat="server"></asp:textbox></td>
		<td></td>
		<td></td>
	</tr>--%>
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="120px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="120px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px">
	<asp:datagrid id="dgKeyList" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="20" ShowFooter="True" OnPageIndexChanged="dgKeyList_PageIndexChanged">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
</div>