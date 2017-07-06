<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmScanHistory" CodeFile="WFrmScanHistory.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center" border="0">
	<tr>
		<td><asp:label id="lblProductID" runat="server" Width="265px">ProductID or IMEI or PICASSO or PPID</asp:label></td>
		<td>
			<P><asp:textbox id="tbProductID" runat="server" Width="200px"></asp:textbox><br>
				<asp:label id="Label4" runat="server" Visible="False" BackColor="White" ForeColor="Red"></asp:label></P>
		</td>
		<td align="right" width="100">
            &nbsp;<asp:button id="btnQuery" Text="Query" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
</table>
<hr>
<table align="center">
	<tr>
		<td vAlign="top">
			<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center">
				<tr>
					<td align="center"><asp:label id="Label1" runat="server" Visible="False" CssClass="DataGridFont">Work Order Information</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="dgWO" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False" BorderStyle="None"
							BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="MFG_TYPE" HeaderText="MFG_TYPE"></asp:BoundColumn>
								<asp:BoundColumn DataField="WO_NO" HeaderText="Work Order">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Model" HeaderText="Model">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PN" HeaderText="PN">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="WO_Qty" HeaderText="Work Order Qty">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="Label3" runat="server" Visible="False" CssClass="DataGridFont">Product,IMEI,Picasso,PPID RelationShip</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="dgRelationShip" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False"
							BorderStyle="None" BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="ProductID" HeaderText="Product ID">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="IMEI" HeaderText="IMEI">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Picasso" HeaderText="Picasso">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PPID_NUM" HeaderText="PPID">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CartonID" HeaderText="Carton ID">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MSN#" HeaderText="MSN#"></asp:BoundColumn>
								<asp:BoundColumn DataField="INDATE" HeaderText="In Date"></asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="Status"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center"><FONT><asp:label id="Label2" runat="server" Visible="False" CssClass="DataGridFont">Process Information</asp:label></FONT></td>
				</tr>
				<tr>
					<td align="center"><asp:datagrid id="dgProduct" runat="server" Width="272px" CssClass="DataGridFont" AutoGenerateColumns="False"
							BorderStyle="None" BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana"
							CellPadding="3" ShowFooter="True" PageSize="30">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="FCDate" HeaderText="Creation Date">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FStationID" HeaderText="Station ID">
									<HeaderStyle Wrap="False"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FStateID" HeaderText="State ID">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FLine" HeaderText="Line">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="EMP_ID" HeaderText="Employee ID">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ERROR_CODE" HeaderText="ERROR CODE">
									<HeaderStyle Wrap="False"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
		<td vAlign="top" align="center">
			<table>
				<tr>
					<td align="center"><asp:label id="lblPanelInfo" runat="server" Visible="False" CssClass="DataGridFont">Panel Information</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="dgPanelInfo" runat="server" CssClass="DataGridFont" BorderStyle="None" BorderWidth="1px"
							BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" OnItemCommand="dgPanelInfo_ItemCommand">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
                                <asp:TemplateColumn HeaderText="PANEL ID" SortExpression="PANEL_ID">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="PANELID" Text='<%# DataBinder.Eval(Container, "DataItem.PANEL_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="SEQUENCE_ID" HeaderText="SEQUENCE ID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="WO_NO" HeaderText="WO NO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ITEM_NO" HeaderText="ITEM NO"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="Label5" runat="server" Visible="False" CssClass="DataGridFont">E2P Information</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="dgE2P" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False" BorderStyle="None"
							BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="COMPUTERNAME" HeaderText="COMPUTERNAME"></asp:BoundColumn>
								<asp:BoundColumn DataField="WORKORDER" HeaderText="WORK ORDER"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCTID" HeaderText="PRODUCT ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="IMEI" HeaderText="IMEI"></asp:BoundColumn>
								<asp:BoundColumn DataField="PICASSO" HeaderText="PICASSO"></asp:BoundColumn>
								<asp:BoundColumn DataField="BTADDRESS" HeaderText="BTADDRESS"></asp:BoundColumn>
								<asp:BoundColumn DataField="E2PDATE" HeaderText="E2P DATE"></asp:BoundColumn>
								<asp:BoundColumn DataField="STATUS" HeaderText="STATUS"></asp:BoundColumn>
								<asp:BoundColumn DataField="ERRORMSG" HeaderText="ERRORMSG"></asp:BoundColumn>
								<asp:BoundColumn DataField="EMPLOYEE" HeaderText="EMPLOYEE"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="Label6" runat="server" Visible="False" CssClass="DataGridFont">出貨信息</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="dgShip" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False" BorderStyle="None"
							BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="INVOICE_NUMBER" HeaderText="INVOICENUMBER"></asp:BoundColumn>
								<asp:BoundColumn DataField="ITEM_NUMBER" HeaderText="ITEMNUMBER"></asp:BoundColumn>
								<asp:BoundColumn DataField="QUANTITY" HeaderText="QUANTITY"></asp:BoundColumn>
								<asp:BoundColumn DataField="INTERNAL_CARTON" HeaderText="INTERNALCARTON"></asp:BoundColumn>
								<asp:BoundColumn DataField="SHIP_DATE" HeaderText="SHIPDATE"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
				</tr>
				<tr>
					<td align="center"><asp:label id="Label7" runat="server" CssClass="DataGridFont" Visible="False">數據庫中的綁定信息</asp:label></td>
				</tr>
				<tr>
					<td align="center" vAlign="top"><asp:datagrid id="DatagridLink" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False" BorderStyle="None"
							BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="WORKORDER" HeaderText="WORK ORDER"></asp:BoundColumn>
								<asp:BoundColumn DataField="PPART" HeaderText="PPART"></asp:BoundColumn>
								<asp:BoundColumn DataField="IMEINUM" HeaderText="IMEI"></asp:BoundColumn>
								<asp:BoundColumn DataField="SERIAL_NUM" HeaderText="PICASSO"></asp:BoundColumn>
								<asp:BoundColumn DataField="MSN" HeaderText="MSN"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODUCT_ID" HeaderText="PID"></asp:BoundColumn>
								<asp:BoundColumn DataField="CREATION_DATE" HeaderText="CREATION_DATE"></asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
