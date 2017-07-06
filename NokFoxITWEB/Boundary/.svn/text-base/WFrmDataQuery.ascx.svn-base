<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmDataQuery" CodeFile="WFrmDataQuery.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="100%" align="center"
	border="0">
	<tr>
		<td colSpan="8" height="20">
			<hr>
		</td>
	</tr>
	<tr>
		<TD style="HEIGHT: 10px" align="right" width="10%"><FONT face="宋体"><asp:label id="lblModel" runat="server"></asp:label></FONT></TD>
		<td style="WIDTH: 140px; HEIGHT: 10px" width="140"><asp:dropdownlist id="DropDownList1" runat="server" AutoPostBack="True" Width="88px" onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:dropdownlist></td>
		<td style="HEIGHT: 10px" align="right" width="10%"><asp:label id="lblTStation" runat="server"></asp:label></td>
		<td style="WIDTH: 140px; HEIGHT: 10px" width="140"><asp:dropdownlist id="DropDownList2" runat="server" AutoPostBack="True" Width="160px" onselectedindexchanged="DropDownList2_SelectedIndexChanged"></asp:dropdownlist></td>
		<td style="HEIGHT: 10px" align="right" width="10%"><FONT face="宋体"><asp:label id="lblProductid" runat="server"></asp:label></FONT></td>
		<td style="HEIGHT: 10px" colSpan="2"><asp:textbox id="txtProductid" runat="server" Width="152px"></asp:textbox></td>
	</tr>
	<tr>
		<td style="HEIGHT: 21px" colSpan="7">
			<hr>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 17px" width="20%" colSpan="2"><FONT face="宋体"><asp:label id="lblcountname" runat="server" Width="100%"></asp:label></FONT></td>
		<td style="WIDTH: 106px; HEIGHT: 21px" align="right" width="106"></td>
		<td style="WIDTH: 168px; HEIGHT: 21px"></td>
		<td style="WIDTH: 101px; HEIGHT: 21px" align="right" width="101"></td>
		<td style="WIDTH: 174px; HEIGHT: 21px" width="174"></td>
		<td style="HEIGHT: 21px" width="20%"><FONT face="宋体"></FONT></td>
	</tr>
	<tr>
		<td style="WIDTH: 222px" width="222" colSpan="2"><FONT face="宋体"><asp:label id="lbltestcount" runat="server" Width="192px"></asp:label></FONT></td>
		<td style="HEIGHT: 10px" align="right" width="10%"><FONT face="宋体"><asp:checkbox id="CheckBox1" runat="server" AutoPostBack="True" Width="111px" oncheckedchanged="CheckBox1_CheckedChanged"></asp:checkbox></FONT></td>
		<td id="startDate" style="WIDTH: 168px">
            &nbsp;<uc2:Calendar1 ID="txtSTARTTIME" runat="server" ShowsTime="true" />
		</td>
		<td align="right" width="10%"><asp:label id="lblENDTIME" Runat="server"></asp:label></td>
		<td id="endDate" width="20%">
            &nbsp;<uc2:Calendar1 ID="txtEndDate" runat="server" />
		</td>
		<td align="center" width="20%"><asp:button id="btnQUERY" Width="80px" Runat="server" onclick="btnQUERY_Click"></asp:button><FONT face="宋体">&nbsp;
			</FONT>
			<asp:button id="btnOUTPUT" Width="80px" Runat="server" onclick="btnOUTPUT_Click"></asp:button></td>
	</tr>
</table>
<HR>
<TABLE width="100%" align="center">
	<tr>
		<td vAlign="top">
			<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="80%" align="center">
				<tr>
					<td align="center"><FONT face="宋体"></FONT></td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<div id="showtext"></div>
						<asp:datagrid id="dgdata1" runat="server" Width="1086px" BackColor="White" ShowFooter="True" BorderWidth="1px"
							BorderColor="#CCCCCC" Font-Size="X-Small" Font-Names="Verdana" CssClass="DataGridFont" BorderStyle="None"
							PageSize="50" AllowPaging="True">
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<PagerStyle Font-Size="Larger" HorizontalAlign="Right" ForeColor="#000066" BackColor="#006699"
								Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
