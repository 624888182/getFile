<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmwipstatusquery.ascx.cs" Inherits="Boundary_wfrmwipstatusquery" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>	
<script type="text/javascript" language="javascript">
function go()
{
	document.forms[0].message.style.display="block";
}
</script>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="4"></td>
		<td style="height: 49px"><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px; height: 49px;">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
        </td>
		<td style="height: 49px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="height: 49px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
		<td rowSpan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button>
			<br />  
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel" OnClick="btnExportExcel_Click"  ></asp:button></td>
		<TD rowSpan="4"><FONT face="新細明體"><IMG id="message" style="DISPLAY: none" alt="" src="../Images/Message.JPG"></FONT></TD>
	</tr>
	<tr>
		<td></td>
		<td><asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td></td>
		<td><asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
	<tr>		 
		<td style="HEIGHT: 8px"><asp:label id="lblModel" runat="server" Width="100px">Model Name</asp:label></td>
		<td style="HEIGHT: 8px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
	</tr>
</table>
<hr/>
<TABLE class="DataGridFont" id="tbwip" cellSpacing="1" cellPadding="1" align="center"
	border="1" runat="server">
	<TR>
		<TD></TD>
	</TR>
</TABLE>
