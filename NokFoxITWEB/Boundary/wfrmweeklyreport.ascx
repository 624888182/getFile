<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmWeeklyReport" CodeFile="WFrmWeeklyReport.ascx.cs" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<TABLE class="DataGridFont" id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD style="HEIGHT: 26px"><asp:label id="lblWeek" runat="server" Width="100px"> Week</asp:label></TD>
		<TD width="155" style="HEIGHT: 26px"><asp:dropdownlist id="ddlMonth" runat="server" Width="128px"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 26px"><asp:label id="lblModel" runat="server" Width="100px"> Model</asp:label></TD>
		<td style="HEIGHT: 26px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
	</TR>
	<tr>
		<td align="right" colSpan="4"><br>
			<asp:button id="btnReport" runat="server" Width="132px" Height="40px" Text="General Report" onclick="btnReport_Click"></asp:button></td>
	</tr>
</TABLE>
<hr>
