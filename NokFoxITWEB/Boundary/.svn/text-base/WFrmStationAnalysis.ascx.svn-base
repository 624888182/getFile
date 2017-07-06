<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmStationAnalysis" CodeFile="WFrmStationAnalysis.ascx.cs" %>
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
		<td vAlign="bottom" rowSpan="3"><FONT face="新細明體">
			</FONT>
			<asp:button id="btnQuery" Width="80px" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button>
			<asp:button id="btnOutPut" Width="80px" Text="OutPut" Runat="server" onclick="btnOutPut_Click"></asp:button>
			</td>
	</tr>
	<TR>
		<TD style="HEIGHT: 20px"><asp:label id="lblModel" runat="server" Width="100px"> Model</asp:label></TD>
		<TD style="HEIGHT: 20px" width="155"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></TD>
		<TD style="HEIGHT: 20px"><asp:label id="lblStation" runat="server" Width="100px"> Station ID</asp:label></TD>
		<TD style="HEIGHT: 20px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px"></asp:dropdownlist></TD>
	</TR>
	<tr>
		<td style="height: 20px"></td>
		<td style="height: 20px"></td>
		<td style="height: 20px"></td>
		<td style="height: 20px"><asp:checkbox id="ckbRepair" runat="server" Text="Without Repair" Visible="False" Checked="True"></asp:checkbox></td>
	</tr>
</TABLE>
<hr />
			<asp:datagrid id="dgMachineRate" runat="server" BackColor="White" CssClass="DataGridFont" ShowFooter="True"
				Font-Names="Verdana" Font-Size="10px" BorderColor="#CCCCCC" BorderWidth="1px" BorderStyle="None"
				PageSize="20" AllowPaging="True" AutoGenerateColumns="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle Height="30px" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px" ForeColor="White"
					BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
                    <asp:BoundColumn DataField="Employee" HeaderText="Employee">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Width="100px" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="LineID" HeaderText="LineID">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Width="70px" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FixtureID" HeaderText="FixtureID">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
					<asp:BoundColumn DataField="FailureItem" HeaderText="Failure Item">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="QTY" HeaderText="QTY">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
					Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
