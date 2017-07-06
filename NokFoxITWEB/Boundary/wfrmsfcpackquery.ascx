<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmSFCPackQuery" debug="True" CodeFile="WFrmSFCPackQuery.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="4"></td>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
        </td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td>
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
		<td rowSpan="5"><FONT face="·s²Ó©úÅé">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:Label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
		<td></td>
		<td>
			<asp:Label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
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
		<%--<td>
			<asp:label id="lblLine" runat="server" Width="100px">Line</asp:label>
			</td>
		<%--<td style="WIDTH: 198px">
			<asp:dropdownlist id="ddlLine" runat="server" Width="155px">
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
			</asp:dropdownlist></td>--%>
		<td style="HEIGHT: 23px"><asp:label id="lblItem" runat="server" Width="56px">Item</asp:label></td>
		<td style="HEIGHT: 23px"><asp:textbox id="tbItem" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
		<td>
			<asp:CheckBox id="ckbRepair" runat="server" Text="Without Repair"></asp:CheckBox></td>
		<td></td>
	</tr>
</table>
<hr>
<table width="100%" border="0">
	<tr>
		<td width="55%" vAlign="top">
			<cwc:webdatagrid id="dgSMT" runat="server" Width="272px" Visible="False" SetShowFooter="True" AutoGenerateColumns="False"
				BorderStyle="None" BorderWidth="1px" BackColor="White" SaveSettings="False" DisableSort="False"
				AllowPaging="True" Font-Size="10px" Font-Names="Verdana" DisableSetButton="False" BorderColor="#CCCCCC"
				AllowSorting="True" sUserLanguage="en-us" DisablePaging="False" CellPadding="3" ShowFooter="True"
				UserID="Any" CssClass="DataGridFont" PageSize="20">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
					BackColor="#006699"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="FWO_NO" HeaderText="Work Order">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id=Label4 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FModel" HeaderText="Model">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label id=Label5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FLine" HeaderText="Line">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLine") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLine") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="PN" HeaderText="Item">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="WO_Qty" HeaderText="WO Qty">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FInput" HeaderText="Input">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FInput") %>' CommandName="Input" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FTouchUP" HeaderText="Touch UP">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FTouchUP") %>' CommandName="TouchUP" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FXRay" HeaderText="X-Ray">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FXRay") %>' CommandName="XRay" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FRouter" HeaderText="Router">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FRouter") %>' CommandName="Router" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FDownLoad" HeaderText="DownLoad">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fdownload") %>' CommandName="download" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FCalibration" HeaderText="Calibration">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FCalibration") %>' CommandName="Calibration" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FPreTest" HeaderText="PreTest">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FPreTest") %>' CommandName="PreTest" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FBaseBand1" HeaderText="BaseBand1">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FBaseBand1") %>' CommandName="BaseBand1" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FBaseBand2" HeaderText="BaseBand2">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FBaseBand2") %>' CommandName="BaseBand2" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FGlue" HeaderText="Glue">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FGlue") %>' CommandName="Glue" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cwc:webdatagrid>
			<cwc:webdatagrid id="dgAssembly" runat="server" Width="272px" Visible="False" SetShowFooter="True"
				AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" BackColor="White" SaveSettings="False"
				DisableSort="False" AllowPaging="True" Font-Size="10px" Font-Names="Verdana" DisableSetButton="False"
				BorderColor="#CCCCCC" AllowSorting="True" sUserLanguage="en-us" DisablePaging="False" CellPadding="3"
				ShowFooter="True" UserID="Any" CssClass="DataGridFont" PageSize="20">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
					BackColor="#006699"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="FWO_NO" HeaderText="Word Order">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FModel" HeaderText="Model">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FLine" HeaderText="Line">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLine") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLine") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="PN" HeaderText="Item">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>' ID="Label7" NAME="Label7">
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>' ID="Textbox3" NAME="Textbox3">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="WO_Qty" HeaderText="WO Qty">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>' ID="Label8" NAME="Label8">
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>' ID="Textbox4" NAME="Textbox4">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FInput" HeaderText="Input">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FInput") %>' CommandName="Input" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FABaseBand1" HeaderText="ABaseBand1">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABaseBand1") %>' CommandName="ABaseBand1" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FABaseBand2" HeaderText="ABaseBand2">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABaseBand2") %>' CommandName="ABaseBand2" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FABaseBand3" HeaderText="ABaseBand3">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABaseBand3") %>' CommandName="ABaseBand3" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FABaseBand4" HeaderText="ABaseBand4">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABaseBand4") %>' CommandName="ABaseBand4" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FWireLess" HeaderText="FWireLess">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWireLess") %>' CommandName="WireLess" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FReDownload" HeaderText="ReDownload">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FReDownload") %>' CommandName="ReDownload" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FFQC" HeaderText="FQC">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FFQC") %>' CommandName="FQC" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FReWork" HeaderText="ReWork">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FReWork") %>' CommandName="ReWork" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cwc:webdatagrid>
			<cwc:webdatagrid id="dgPacking" runat="server" Width="272px" Visible="False" SetShowFooter="True"
				AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" BackColor="White" SaveSettings="False"
				DisableSort="False" AllowPaging="True" Font-Size="10px" Font-Names="Verdana" DisableSetButton="False"
				BorderColor="#CCCCCC" AllowSorting="True" sUserLanguage="en-us" DisablePaging="False" CellPadding="3"
				ShowFooter="True" UserID="Any" CssClass="DataGridFont" PageSize="20">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
					BackColor="#006699"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="FWO_NO" HeaderText="Word Order">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWO_NO") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FModel" HeaderText="Model">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FModel") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="PN" HeaderText="Item">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>' ID="Label9" NAME="Label9">
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PN") %>' ID="Textbox5" NAME="Textbox5">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="WO_Qty" HeaderText="WO Qty">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>' ID="Label10" NAME="Label10">
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WO_Qty") %>' ID="Textbox6" NAME="Textbox6">
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FE2P" HeaderText="E2P">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FE2P") %>' CommandName="E2P" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn> 
				<%--	<asp:TemplateColumn SortExpression="FSHIPPING" HeaderText="SHIPPING">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSHIPPING") %>' CommandName="SHIPPING" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>--%>
					<asp:TemplateColumn SortExpression="FPACK" HeaderText="PACK">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FPACK") %>' CommandName="PACK" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FOQC" HeaderText="OQC">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOQC") %>' CommandName="OQC" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="FOOB" HeaderText="OOB">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOOB") %>' CommandName="OOB" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn SortExpression="FWH" HeaderText="WH">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FWH") %>' CommandName="WH" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cwc:webdatagrid>
		</td>
		<td vAlign="top"><cwc:webdatagrid id="dgPIDDetail" runat="server" Width="272px" CssClass="DataGridFont" UserID="Any"
				ShowFooter="True" CellPadding="3" DisablePaging="False" sUserLanguage="en-us" AllowSorting="True" BorderColor="#CCCCCC"
				DisableSetButton="False" Font-Names="Verdana" Font-Size="10px" AllowPaging="True" DisableSort="False" SaveSettings="False"
				BackColor="White" BorderWidth="1px" BorderStyle="None" AutoGenerateColumns="False" SetShowFooter="True" PageSize="20">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn SortExpression="FPID" HeaderText="Product ID">
						<ItemTemplate>
							<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FPID") %>' CommandName="PID" CausesValidation="false">
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="FWO_NO" SortExpression="FWO_NO" HeaderText="Word Order">
						<HeaderStyle Wrap="False"></HeaderStyle>
					</asp:BoundColumn>
					<%--<asp:BoundColumn DataField="FLine" SortExpression="FLine" HeaderText="Line">
						<HeaderStyle Wrap="False"></HeaderStyle>
					</asp:BoundColumn>--%>
					<asp:TemplateColumn SortExpression="FStatus" HeaderText="Status">
						<HeaderStyle Wrap="False"></HeaderStyle>
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FStatus") %>'>
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FStatus") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</cwc:webdatagrid></td>
	</tr>
</table>
