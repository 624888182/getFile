<%@ Page language="c#" Inherits="SFCQuery.Boundary.WFrmStationDetail" CodeFile="WFrmStationDetail.aspx.cs" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Station Detail</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
  </HEAD>
	<body background="../images/1170744771831.jpg">
		<form id="Form1" method="post" runat="server">
			<FONT face="·s²Ó©úÅé">
				<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD>
							<asp:DataGrid id="dgmainsec" runat="server" CellPadding="4" BorderColor="#CC9966" BackColor="White"
								BorderWidth="1px" BorderStyle="None" AutoGenerateColumns="False">
								<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
								<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
								<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="wo_no" HeaderText="Work Order">
										<HeaderStyle Wrap="False"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="main_ID" HeaderText="Main ID">
										<HeaderStyle Wrap="False"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Secondary_ID" HeaderText="Secondary ID">
										<HeaderStyle Wrap="False"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="LCM_ID" HeaderText="LCM ID">
										<HeaderStyle Wrap="False"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
							</asp:DataGrid>
      <P>&nbsp;</P></TD>
					</TR>
					<TR>
						<TD>
							<cwc:WebDataGrid id="dgStation" runat="server" CssClass="DataGridFont" UserID="Any" ShowFooter="True"
								CellPadding="3" DisablePaging="False" sUserLanguage="en-us" AllowSorting="True" BorderColor="#CCCCCC"
								DisableSetButton="False" Font-Names="Verdana" Font-Size="10px" AllowPaging="True" DisableSort="False"
								SaveSettings="False" BackColor="White" BorderWidth="1px" BorderStyle="None" AutoGenerateColumns="False"
								SetShowFooter="True" Width="272px" PageSize="20">
<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
</PagerStyle>

<AlternatingItemStyle BackColor="WhiteSmoke">
</AlternatingItemStyle>

<FooterStyle ForeColor="#000066" BackColor="White">
</FooterStyle>

<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999">
</SelectedItemStyle>

<ItemStyle ForeColor="#000066" BackColor="Cornsilk">
</ItemStyle>

<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699">
</HeaderStyle>

<Columns>
<asp:BoundColumn DataField="FCDate" SortExpression="FCDate" HeaderText="Creation Date">
<HeaderStyle Wrap="False">
</HeaderStyle>

<ItemStyle Wrap="False">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="FStationID" SortExpression="FStationID" HeaderText="Station ID">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="FStateID" SortExpression="FStateID" HeaderText="State ID">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="FLine" SortExpression="FLine" HeaderText="Line">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="EMP_ID" SortExpression="EMP_ID" HeaderText="Employee ID">
<HeaderStyle Wrap="False">
</HeaderStyle>
</asp:BoundColumn>
</Columns>
							</cwc:WebDataGrid></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
