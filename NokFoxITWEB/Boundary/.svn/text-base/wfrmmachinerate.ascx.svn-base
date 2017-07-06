<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmMachineRate" CodeFile="WFrmMachineRate.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<TABLE class="DataGridFont" id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
            <br>
			<asp:label id="Label28" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
			<asp:label id="Label29" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
		<td vAlign="bottom" rowSpan="3"><FONT face="·s²Ó©úÅé">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" Width="80px" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<TR>
		<TD style="HEIGHT: 20px"><asp:label id="lblModel" runat="server" Width="100px"> Model</asp:label></TD>
		<TD style="HEIGHT: 20px" width="155"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"  AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 20px"><asp:label id="lblStation" runat="server" Width="100px"> Station ID</asp:label></TD>
		<TD style="HEIGHT: 20px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px"></asp:dropdownlist></TD>
	</TR>
	<tr>
		<td></td>
		<td></td>
		<td></td>
		<td><asp:checkbox id="ckbRepair" runat="server" Text="Without Repair" Visible="False" Checked="True"></asp:checkbox></td>
	</tr>
</TABLE>
<HR>
<table class="DataGridFont" borderColor="silver" cellSpacing="0" cellPadding="0" border="1">
	<tr>
		<td colspan="2" align="left">
			<asp:Label id="lblMachineRate" runat="server" Font-Bold="True" Visible="False" Font-Size="X-Large">Machine Yield</asp:Label></td>
	</tr>
	<tr>
		<td vAlign="top">
			<asp:datagrid id="dgMachineRate" runat="server" BackColor="White" CssClass="DataGridFont" ShowFooter="True"
				Font-Names="Verdana" Font-Size="10px" BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="None"
				PageSize="20" AllowPaging="True" AutoGenerateColumns="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle Height="30px" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px" ForeColor="White"
					BackColor="#006699" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="LINEID" HeaderText="Line ID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="TESTSTATION" HeaderText="TEST STATION">
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
                    <asp:BoundColumn DataField="DEFECTRATE" DataFormatString="{0:N2}%" HeaderText="DEFECT RATE(%)">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
				</Columns>
				<PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</td>
		<td vAlign="top">
			<chart:WebChartViewer id="wcvMachineRate" runat="server" Visible="False"></chart:WebChartViewer>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="left">
			<asp:Label id="lblSMTLineRate" runat="server" Font-Bold="True" Visible="False" Font-Size="X-Large">SMT Line Yield</asp:Label></td>
	</tr>
	<tr>
		<td vAlign="top">
			<asp:datagrid id="dgSMTLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
				PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px"
				Font-Names="Verdana" ShowFooter="True" CssClass="DataGridFont" BackColor="White">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk"></ItemStyle>
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
		<td vAlign="top">
			<chart:webchartviewer id="wcvSMTLineRate" runat="server" Visible="False"></chart:webchartviewer></td>
	</tr>
	<tr>
		<td colspan="2" align="left">
			<asp:Label id="lblAssyLineRate" runat="server" Font-Bold="True" Visible="False" Font-Size="X-Large">Assembly Line Yield</asp:Label></td>
	</tr>
	<tr>
		<td vAlign="top">
			<asp:datagrid id="dgAssyLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
				PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px"
				Font-Names="Verdana" ShowFooter="True" CssClass="DataGridFont" BackColor="White">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk"></ItemStyle>
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
		<td vAlign="top">
			<chart:webchartviewer id="wcvAssyLineRate" runat="server" Visible="False"></chart:webchartviewer></td>
	</tr>
</table>
