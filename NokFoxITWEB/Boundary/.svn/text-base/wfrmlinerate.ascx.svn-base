<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmLineRate" CodeFile="WFrmLineRate.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="modeltile1" runat="server"></uc1:modeltitle>
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
		<td vAlign="bottom" rowSpan="2"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" Width="80px" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<TR>
		<TD><asp:label id="lblModel" runat="server" Width="100px"> Model</asp:label></TD>
		<TD width="155"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></TD>
		<TD></TD>
		<TD><asp:checkbox id="ckbRepair" runat="server" Text="Without Repair" Visible="False" Checked="True"></asp:checkbox></TD>
	</TR>
</TABLE>
<HR>
<table>
	<tr>
		<td vAlign="top"><asp:datagrid id="dgLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
				BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True"
				CssClass="DataGridFont" BackColor="White">
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
		<td vAlign="top"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT>
			<chart:webchartviewer id="wcvLineRate" runat="server" Visible="False"></chart:webchartviewer></td>
	</tr>
</table>
<br>
