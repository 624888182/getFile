<%@Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmDailyReport" CodeFile="WFrmDailyReport.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server"/>
            <br>
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblModel" runat="server" Width="100px"> Model</asp:label></td>
		<td width="155">
			<asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
		<td></td>
		<td>
			<asp:CheckBox id="ckbRepair" runat="server" Text="Without Repair" Checked="True"></asp:CheckBox></td>
	</tr>
	<tr>
		<td align="right" colSpan="4"><br/>
			<asp:button id="btnReport" runat="server" Width="132px" Text="General Report" Height="40px" onclick="btnReport_Click"></asp:button></td>
	</tr>
</table>
<hr/>
