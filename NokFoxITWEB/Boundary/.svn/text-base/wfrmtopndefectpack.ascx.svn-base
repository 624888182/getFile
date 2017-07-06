<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmTopNDefectPack" CodeFile="WFrmTopNDefectPack.ascx.cs" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="6"></td>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px"><asp:textbox id="tbStartDate" runat="server"></asp:textbox><asp:button id="btnDateFrom" runat="server" Width="25px" Text="..."></asp:button></td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td><asp:textbox id="tbEndDate" runat="server"></asp:textbox><asp:button id="btnDateTo" runat="server" Width="25px" Text="..."></asp:button></td>
		<td rowSpan="5"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td></td>
		<td><asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
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
		<td><asp:textbox id="tbTopN" runat="server" Width="60px">5</asp:textbox><asp:label id="Label1" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblRegion" runat="server" Width="100px">Region</asp:label></td>
		<td style="WIDTH: 198px"><asp:dropdownlist id="ddlRegion" runat="server" Width="155px" Enabled="False">
				<asp:ListItem Value="PCBA" Selected="True">PCBA</asp:ListItem>
				<asp:ListItem Value="Assembly">Assembly</asp:ListItem>
				<asp:ListItem Value="Packing">Packing</asp:ListItem>
			</asp:dropdownlist></td>
		<td><asp:radiobuttonlist id="rblType" runat="server" Width="168px" AutoPostBack="True" CssClass="DataGridFont" onselectedindexchanged="rblType_SelectedIndexChanged">
				<asp:ListItem Value="By Model Name" Selected="True">By Model Name</asp:ListItem>
				<asp:ListItem Value="By WordOrder NO">By WordOrder NO</asp:ListItem>
			</asp:radiobuttonlist></td>
		<td><asp:dropdownlist id="ddlType" runat="server" Width="155px"></asp:dropdownlist><asp:textbox id="tbType" runat="server" Visible="False"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="lblStatisticsType" runat="server" Width="100px">Statistics Type</asp:label></td>
		<td style="WIDTH: 198px"><FONT face="新細明體"><asp:dropdownlist id="DropDownList1" runat="server" Width="96px">
					<asp:ListItem Value="Final Fail" Selected="True">Final Fail</asp:ListItem>
					<asp:ListItem Value="First Fail">First Fail</asp:ListItem>
				</asp:dropdownlist></FONT></td>
		<td><FONT face="新細明體"><asp:label id="lblStation" runat="server" Width="100px">Station ID</asp:label></FONT></td>
		<td><asp:dropdownlist id="ddlStationID" runat="server" Width="96px">
				<asp:ListItem Selected="True"></asp:ListItem>
				<asp:ListItem Value="FQC">FQC</asp:ListItem>
				<asp:ListItem Value="E2P">E2P</asp:ListItem>
				<asp:ListItem Value="OQC">OQC</asp:ListItem>
				<asp:ListItem Value="OOB">OOB</asp:ListItem>
			</asp:dropdownlist></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
		<td><asp:checkbox id="CheckBox1" runat="server" Text="Without Repair"></asp:checkbox></td>
		<td>
            <asp:RadioButtonList ID="rblWOType" runat="server" CssClass="DataGridFont" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">正常工單</asp:ListItem>
                <asp:ListItem>重工工單</asp:ListItem>
            </asp:RadioButtonList></td>
	</tr>
</table>
<hr>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="center" width="30%"><asp:label id="Label2" runat="server" Visible="False">Defect Top N</asp:label></td>
		<td align="center" width="70%"><asp:label id="Label3" runat="server" Visible="False">Defect Detail</asp:label></td>
	</tr>
	<tr>
		<td vAlign="top" align="center" width="30%"><cwc:webdatagrid id="dgDefectTopN" runat="server" Width="272px" CssClass="DataGridFont" SetShowFooter="True"
				AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" BackColor="White" SaveSettings="False" DisableSort="False" AllowPaging="True"
				Font-Size="10px" Font-Names="Verdana" DisableSetButton="False" BorderColor="#CCCCCC" AllowSorting="True" sUserLanguage="en-us" DisablePaging="False"
				CellPadding="3" ShowFooter="True" UserID="Any" CurrentStatus="" SummaryText="">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<Columns>
                    <asp:BoundColumn DataField="Item" HeaderText="Item"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="DEFECT CODE">
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_CODE") %>'>
							</asp:Label>
						
</ItemTemplate>
						<HeaderStyle Wrap="False"></HeaderStyle>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_CODE") %>'>
							</asp:TextBox>
						
</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="DEFECT DESC">
						<ItemStyle Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_DESC_CHT") %>'>
							</asp:Label>
						
</ItemTemplate>
						<HeaderStyle Wrap="False"></HeaderStyle>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEFECT_DESC_CHT") %>'>
							</asp:TextBox>
						
</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="QTY">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QTY") %>' CommandName="QTY" CausesValidation="false">
							</asp:LinkButton>
						
</ItemTemplate>
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="CC" HeaderText="Inspection Qty">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Rate" HeaderText="Defect Rate">
						<ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
						<HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="STATION_CODE" HeaderText="Station"></asp:BoundColumn>
				</Columns>
			</cwc:webdatagrid></td>
		<td vAlign="top" align="center" width="70%" rowSpan="2"><asp:datagrid id="dgDefectDtail" runat="server" CssClass="DataGridFont" AutoGenerateColumns="False"
				BorderStyle="None" BorderWidth="1px" BackColor="White" Font-Size="11px" Font-Names="Verdana" BorderColor="#CCCCCC">
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
		<td><chart:WebChartViewer id="WebChartViewer1" runat="server" Visible="False"></chart:WebChartViewer></td>
	</tr>
</table>
