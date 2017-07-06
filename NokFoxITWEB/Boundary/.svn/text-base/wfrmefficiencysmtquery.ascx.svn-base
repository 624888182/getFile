<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmEfficiencySMTQuery" CodeFile="WFrmEfficiencySMTQuery.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
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
		<td rowSpan="4"><FONT face="·s²Ó©úÅé">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
	<%--	<td><asp:radiobuttonlist id="rblType" runat="server" Width="168px" AutoPostBack="True" CssClass="DataGridFont" onselectedindexchanged="rblType_SelectedIndexChanged">
				<asp:ListItem Value="By Model Name" Selected="True">By Model Name</asp:ListItem>
				<asp:ListItem Value="By WordOrder NO">By WordOrder NO</asp:ListItem>
			</asp:radiobuttonlist></td>--%>		
		<td><asp:label id="lblModel" runat="server" Width="100px">Model Name</asp:label></td>	
		<td><asp:dropdownlist id="ddlType" runat="server" Width="155px"></asp:dropdownlist><asp:textbox id="tbType" runat="server" Visible="False"></asp:textbox></td>
	</tr>
	<tr>
		<td></td>
		<td style="WIDTH: 198px">
			<asp:CheckBox id="ckbRepair" runat="server" Text="Without Repair"></asp:CheckBox></td>
		<td></td>
		<td></td>
	</tr>
</table>
<hr>
<asp:datagrid id="dgPCBAgsm" runat="server" Width="272px" CssClass="DataGridFont" CellPadding="3"
	Font-Names="Verdana" Font-Size="10px" BorderColor="#CCCCCC" BackColor="White" BorderWidth="1px"
	BorderStyle="None" AutoGenerateColumns="False">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="LINE_ID" HeaderText="LINE"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Input">
			<ItemTemplate>
				<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_AI_P") %>' CommandName="S_AI_P" CausesValidation="false">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="XRAY">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label1" runat="server" ForeColor="White">XRay</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label3" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id=LinkButton1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_BO_P") %>' CausesValidation="false" CommandName="S_BO_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id=Linkbutton2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_BO_F") %>' CausesValidation="false" CommandName="S_BO_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Touch UP">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label4" runat="server" ForeColor="White">Touch UP</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label5" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label6" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_CO_P") %>' CausesValidation="false" CommandName="S_CO_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_CO_F") %>' CausesValidation="false" CommandName="S_CO_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Router">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label7" runat="server" ForeColor="White">Router</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label8" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label9" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_P") %>' CausesValidation="false" CommandName="T_AI_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_F") %>' CausesValidation="false" CommandName="T_AI_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="DownLoad">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label10" runat="server" ForeColor="White">DownLoad</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label11" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label12" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_BT_P") %>' CausesValidation="false" CommandName="T_BT_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_BT_F") %>' CausesValidation="false" CommandName="T_BT_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Clibration">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label13" runat="server" ForeColor="White">Clibration</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label14" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label15" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_ET_P") %>' CausesValidation="false" CommandName="T_ET_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_ET_F") %>' CausesValidation="false" CommandName="T_ET_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="PreTest">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label16" runat="server" ForeColor="White">PreTes</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label17" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label18" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_JT_P") %>' CausesValidation="false" CommandName="T_JT_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_JT_F") %>' CausesValidation="false" CommandName="T_JT_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="TBaseBand1">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label19" runat="server" ForeColor="White">TBaseBand1</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label20" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label21" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_CT_P") %>' CausesValidation="false" CommandName="T_CT_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_CT_F") %>' CausesValidation="false" CommandName="T_CT_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="TBaseBand2">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label22" runat="server" ForeColor="White">TBaseBand2</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label23" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label24" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton15" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_NT_P") %>' CausesValidation="false" CommandName="T_NT_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton16" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_NT_F") %>' CausesValidation="false" CommandName="T_NT_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Glue">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label25" runat="server" ForeColor="White">Glue</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label26" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label27" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton17" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_P") %>' CausesValidation="false" CommandName="T_AI_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton18" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_F") %>' CausesValidation="false" CommandName="T_AI_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

<asp:datagrid id="dgPCBAnextest" runat="server" Width="272px" CssClass="DataGridFont" CellPadding="3"
	Font-Names="Verdana" Font-Size="10px" BorderColor="#CCCCCC" BackColor="White" BorderWidth="1px"
	BorderStyle="None" AutoGenerateColumns="False">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	<Columns>
		<asp:BoundColumn DataField="LINE_ID" HeaderText="LINE"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Input">
			<ItemTemplate>
				<asp:LinkButton ID="LinkButton19" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_AI_P") %>' CommandName="S_AI_P" CausesValidation="false">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="XRAY">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label1" runat="server" ForeColor="White">XRay</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label3" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id=LinkButton1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_BO_P") %>' CausesValidation="false" CommandName="S_BO_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id=Linkbutton2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_BO_F") %>' CausesValidation="false" CommandName="S_BO_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Touch UP">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label4" runat="server" ForeColor="White">Touch UP</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label5" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label6" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_CO_P") %>' CausesValidation="false" CommandName="S_CO_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.S_CO_F") %>' CausesValidation="false" CommandName="S_CO_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Router">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label7" runat="server" ForeColor="White">Router</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label8" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label9" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_P") %>' CausesValidation="false" CommandName="T_AI_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_F") %>' CausesValidation="false" CommandName="T_AI_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="DownLoad">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label10" runat="server" ForeColor="White">DownLoad</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label11" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label12" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_BT_P") %>' CausesValidation="false" CommandName="T_BT_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_BT_F") %>' CausesValidation="false" CommandName="T_BT_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Flashing">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label16" runat="server" ForeColor="White">Flashing</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label17" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label18" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FL") %>' CausesValidation="false" CommandName="FL">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FL") %>' CausesValidation="false" CommandName="FL">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="PowerOn">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label13" runat="server" ForeColor="White">PowerOn</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label14" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label15" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PW") %>' CausesValidation="false" CommandName="PW">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PW") %>' CausesValidation="false" CommandName="PW">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="BoardTest">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label19" runat="server" ForeColor="White">BoardTest</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label20" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label21" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BD") %>' CausesValidation="false" CommandName="BD">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton14" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BD") %>' CausesValidation="false" CommandName="BD">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn> 
		<asp:TemplateColumn HeaderText="Glue">
			<HeaderTemplate>
				<TABLE class="DataGridFont" width="100%" border="1">
					<TR>
						<TD align="center" colSpan="2">
							<asp:Label id="Label25" runat="server" ForeColor="White">Glue</asp:Label></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label26" runat="server" ForeColor="White">Pass</asp:Label></TD>
						<TD>
							<asp:Label id="Label27" runat="server" ForeColor="White">Fail</asp:Label></TD>
					</TR>
				</TABLE>
			</HeaderTemplate>
			<ItemTemplate>
				<TABLE class="DataGridFont" border="1">
					<TR>
						<TD>
							<asp:LinkButton id="Linkbutton17" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_P") %>' CausesValidation="false" CommandName="T_AI_P">
							</asp:LinkButton></TD>
						<TD>
							<asp:LinkButton id="Linkbutton18" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.T_AI_F") %>' CausesValidation="false" CommandName="T_AI_F">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
<TABLE class="DataGridFont" id="tblEffiency" cellSpacing="1" cellPadding="1" align="center"
	border="1" runat="server">
	<TR>
		<TD></TD>
	</TR>
</TABLE>
