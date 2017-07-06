<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmTopNDefectSMT" CodeFile="WFrmTopNDefectSMT.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="5"></td>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
        </td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td>
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
		<td rowSpan="5"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:label id="Label28" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
		<td></td>
		<td><asp:label id="Label29" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblLine" runat="server" Width="100px">Line</asp:label></td>
		<td><FONT face="新細明體"><asp:dropdownlist id="ddlLine" runat="server" Width="96px">
					<asp:ListItem></asp:ListItem>
					<asp:ListItem Value="LINE1">LINE1</asp:ListItem>
					<asp:ListItem Value="LINE2">LINE2</asp:ListItem>
					<asp:ListItem Value="LINE3">LINE3</asp:ListItem>
					<asp:ListItem Value="LINE4">LINE4</asp:ListItem>
					<asp:ListItem Value="LINE5">LINE5</asp:ListItem>
					<asp:ListItem Value="LINE6">LINE6</asp:ListItem>
					<asp:ListItem Value="LINE7">LINE7</asp:ListItem>
					<asp:ListItem Value="LINE8">LINE8</asp:ListItem>
					<asp:ListItem Value="LINE9">LINE9</asp:ListItem>
					<asp:ListItem Value="LINE10">LINE10</asp:ListItem>
					<asp:ListItem Value="LINE11">LINE11</asp:ListItem>
					<asp:ListItem Value="LINE12">LINE12</asp:ListItem>
					<asp:ListItem Value="LINE13">LINE13</asp:ListItem>
					<asp:ListItem Value="LINE14">LINE14</asp:ListItem>
					<asp:ListItem Value="LINE15">LINE15</asp:ListItem>
					<asp:ListItem Value="LINE16">LINE16</asp:ListItem>
					<asp:ListItem Value="LINE17">LINE17</asp:ListItem>
					<asp:ListItem Value="LINE18">LINE18</asp:ListItem>
					<asp:ListItem Value="LINE19">LINE19</asp:ListItem>
					<asp:ListItem Value="LINE20">LINE20</asp:ListItem>
					<asp:ListItem Value="LINE21">LINE21</asp:ListItem>
					<asp:ListItem Value="LINE22">LINE22</asp:ListItem>
					<asp:ListItem Value="LINE23">LINE23</asp:ListItem>
					<asp:ListItem Value="LINE24">LINE24</asp:ListItem>
					<asp:ListItem Value="LINE25">LINE25</asp:ListItem>
					<asp:ListItem Value="LINE26">LINE26</asp:ListItem>
					<asp:ListItem Value="LINE27">LINE27</asp:ListItem>
				</asp:dropdownlist></FONT></td>
		<td><asp:label id="lblTopN" runat="server" Width="100px">Top N</asp:label></td>
		<td><asp:textbox id="tbTopN" runat="server" Width="60px">5</asp:textbox><asp:label id="Label1" runat="server" ForeColor="Red" Visible="False"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblRegion" runat="server" Width="100px">Region</asp:label></td>
		<td style="WIDTH: 198px"><asp:dropdownlist id="ddlRegion" runat="server" Width="155px" Enabled="False">
				<asp:ListItem Value="PCBA" Selected="True">PCBA</asp:ListItem>
				<asp:ListItem Value="Assembly">Assembly</asp:ListItem>
				<asp:ListItem Value="Packing">Packing</asp:ListItem>
			</asp:dropdownlist></td>
		<td><asp:radiobuttonlist id="rblType" runat="server" Width="168px" CssClass="DataGridFont" AutoPostBack="True" onselectedindexchanged="rblType_SelectedIndexChanged">
				<asp:ListItem Value="By Model Name" Selected="True">By Model Name</asp:ListItem>
				<asp:ListItem Value="By WordOrder NO">By WordOrder NO</asp:ListItem>
			</asp:radiobuttonlist></td>
		<td><asp:dropdownlist id="ddlType" runat="server" Width="155px"></asp:dropdownlist><asp:textbox id="tbType" runat="server" Visible="False"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="lblStatisticsType" runat="server" Width="100px">Statistics Type</asp:label></td>
		<td style="WIDTH: 198px"><FONT face="新細明體"><asp:dropdownlist id="DropDownList1" runat="server" Width="96px">
					<asp:ListItem></asp:ListItem>
					<asp:ListItem Value="Final Fail" Selected="True">Final Fail</asp:ListItem>
					<asp:ListItem Value="First Fail">First Fail</asp:ListItem>
				</asp:dropdownlist></FONT></td>
		<td><asp:checkbox id="CheckBox1" runat="server" Text="Without Repair"></asp:checkbox></td>
		<td></td>
	</tr>
</table>
<hr>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="center" width="30%"><asp:label id="Label2" runat="server" Visible="False">Defect Top N</asp:label></td>
		<td align="center" width="70%"><asp:label id="Label3" runat="server" Visible="False">Defect Detail</asp:label></td>
	</tr>
	<tr>
		<td vAlign="top" align="center" width="30%"><cwc:webdatagrid id="dgDefectTopN" runat="server" Width="272px" CssClass="DataGridFont" UserID="Any"
				ShowFooter="True" CellPadding="3" DisablePaging="False" sUserLanguage="en-us" AllowSorting="True" BorderColor="#CCCCCC" DisableSetButton="False"
				Font-Names="Verdana" Font-Size="10px" AllowPaging="True" DisableSort="False" SaveSettings="False" BackColor="White" BorderWidth="1px"
				BorderStyle="None" AutoGenerateColumns="False" SetShowFooter="True">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="DEFECT CODE">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_CODE") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_CODE") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="DEFECT DESC">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_DESC_CHT") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_DESC_CHT") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="QTY">
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QTY") %>' CommandName="QTY" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="CC" HeaderText="Inspection Qty">
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Rate" HeaderText="Defect Rate">
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
					</asp:BoundColumn>				
					<asp:TemplateColumn HeaderText="Station">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATION_CODE") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cwc:webdatagrid></td>
		<td vAlign="top" align="center" width="70%" rowSpan="2"><asp:datagrid id="dgDefectDtail" runat="server" CssClass="DataGridFont" BorderColor="#CCCCCC"
				Font-Names="Verdana" Font-Size="11px" BackColor="White" BorderWidth="1px" BorderStyle="None" AutoGenerateColumns="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="PRODUCT_ID" HeaderText="PRODUCT ID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="WO_NO" HeaderText="Work Order">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="STATION_ID" HeaderText="STATION ID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="CREATION_DATE" HeaderText="CREATION DATE">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="EMP_ID" HeaderText="EMPloyee ID">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="DEFECT_CODE" HeaderText="DEFECT CODE">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="DEFECT_DESC" HeaderText="DEFECT DESC">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemStyle Wrap="False"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td><chart:webchartviewer id="WebChartViewer1" runat="server"></chart:webchartviewer></td>
	</tr>
</table>
