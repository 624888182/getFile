<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmFirststationRate" CodeFile="WFrmFirststationRate.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<uc1:ModelTitle runat="server" ID="modeltitle1"></uc1:ModelTitle>
<table class="DataGridFont" id="Table1" cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td style="height: 68px"><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 199px; height: 68px;">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
            <br>
			<asp:label id="Label28" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
		<td style="HEIGHT: 68px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 68px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
			<asp:label id="Label29" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
		<td vAlign="bottom" rowSpan="2"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" Width="80px" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<TR>
		<TD style="height: 22px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></TD>
		<TD style="height: 22px; width: 199px;"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></TD>
		<TD colspan="2" style="height: 22px">
            <asp:Label ID="lblLine" runat="server" Width="100px">Line</asp:Label>
            <asp:dropdownlist id="ddlLine" runat="server" Width="155px">
            </asp:DropDownList></TD>
	</TR>
	<TR>
		<TD style="height: 45px"><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></TD>
		<TD style="width: 199px; height: 45px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px"></asp:dropdownlist></TD>
		<TD colspan="2" style="height: 45px">
			<asp:RadioButtonList id="RadioButtonList1" runat="server" CssClass="DataGridFont" RepeatDirection="Horizontal" TextAlign="Left" Width="274px">
				<asp:ListItem Value="SMT" Selected="True">SMT</asp:ListItem>
				<asp:ListItem Value="ASSEMBLY">ASSEMBLY</asp:ListItem>
			</asp:RadioButtonList></TD>
	</TR>
</table>
<HR>
<table>
	<tr>
		<td vAlign="top"><asp:datagrid id="dgFirstLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
				PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana"
				ShowFooter="True" CssClass="DataGridFont" BackColor="White">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle Font-Bold="True" Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px" ForeColor="White"
					BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="LINEID" HeaderText="Line ID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PASSQTY" HeaderText="PASS QTY">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="FAILQTY" HeaderText="FAIL QTY">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TOTALQTY" HeaderText="TOTAL QTY">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="YIELD" HeaderText="YIELD(%)" DataFormatString="{0:N2}%">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid></td>
		<td vAlign="top"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<chart:WebChartViewer id="wcvFirstLineRate" runat="server" Visible="False"></chart:WebChartViewer>
			</FONT>
		</td>
	</tr>
</table>
