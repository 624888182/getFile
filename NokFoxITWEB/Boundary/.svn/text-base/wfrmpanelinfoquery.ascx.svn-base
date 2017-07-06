<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmPanelInfoQuery" CodeFile="WFrmPanelInfoQuery.ascx.cs" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center" border="0">
	<tr>
		<td><asp:label id="lblPanelID" runat="server" Width="70px">Panel ID </asp:label></td>
		<td><asp:textbox id="tbPanelID" runat="server"></asp:textbox><br>
			<asp:label id="lblErrorMsg" runat="server" ForeColor="Red"></asp:label></td>
		<td align="right" width="100"><asp:button id="btnQuery" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
</table>
<hr id="hr1" runat="server"/>
<asp:Panel id="Panel1" runat="server" CssClass="DataGridFont" Visible="False">
	<TABLE class="DataGridFont" id="Table1" cellSpacing="1" cellPadding="1" border="1">
		<TR>
			<TD align="center"><STRONG>
					<asp:label id="lblBaseInfo" runat="server">Base Information</asp:label><BR>
					<BR>
				</STRONG>
			</TD>
			<TD width="60"></TD>
			<TD align="center" width="100%"><STRONG><BR>
					<asp:label id="lblHistory" runat="server">History Information</asp:label><BR>
				</STRONG>
			</TD>
		<TR>
			<TD style="height: 292px" valign="top">
				<TABLE class="DataGridFont" width="272" border="1">
					<TR>
						<TD width="80">
							<asp:label id="lblWO" runat="server" Width="90px">Work Order</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblWOValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:label id="lblModel" runat="server" Width="90px"> Model</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblModelValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:label id="lblItem" runat="server" Width="90px">Item</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblItemValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:label id="lblWODate" runat="server" Width="90px">Order Date</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblWODateValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:label id="lblBOMVer" runat="server" Width="90px">BOM Version</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblBOMVerValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:label id="lblSerialQty" runat="server" Width="90px">Serial Qty</asp:label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:label id="lblSerialQtyValue" runat="server"></asp:label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:Label id="lblPrintDate" runat="server" Width="90px">Print Date</asp:Label></TD>
						<TD style="width: 169px"><STRONG>
								<asp:Label id="lblPrintDateValue" runat="server"></asp:Label></STRONG></TD>
					</TR>
					<TR>
						<TD width="80">
							<asp:Label id="lblProductID" runat="server" Width="90px">Product ID</asp:Label></TD>
						<TD style="width: 169px">
							<asp:Label id="lblProductIDValue" runat="server" Font-Bold="True"></asp:Label></TD>
					</TR>
				</TABLE>
			</TD>
			<TD style="height: 292px"></TD>
			<TD vAlign="top" style="height: 292px">
				<TABLE class="DataGridFont" height="100%" cellSpacing="1" cellPadding="1" width="100%"
					border="1">
					<TR>
						<TD style="HEIGHT: 117px" vAlign="top">
							<asp:datagrid id="dgHistory" runat="server" CssClass="DataGridFont" BorderStyle="None" BorderWidth="1px"
								BackColor="White" Font-Size="11px" Font-Names="Verdana" BorderColor="#CCCCCC">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 23px" align="center"><STRONG>
								<asp:label id="lblRepairInfo" runat="server">Repair Information</asp:label></STRONG></TD>
					</TR>
					<TR height="100%">
						<TD vAlign="top">
							<asp:datagrid id="dgDefectDtail" runat="server" CssClass="DataGridFont" BorderStyle="None" BorderWidth="1px"
								BackColor="White" Font-Size="11px" Font-Names="Verdana" BorderColor="#CCCCCC">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
								<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</asp:Panel>
