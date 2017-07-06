<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmRetestRateWeeklyReport.ascx.cs" Inherits="Boundary_WFrmRetestRateWeeklyReport" %>

<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>	
<script type="text/javascript" language="javascript">
function go()
{
	document.forms[0].message.style.display="block";
}
</script>
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
		<td rowSpan="5"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
		<TD rowSpan="5"><FONT face="新細明體"><IMG id="message" style="DISPLAY: none" alt="" src="../Images/Message.JPG"></FONT></TD>
	</tr>
	<tr>
		<td></td>
		<td><asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td></td>
		<td><asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="lblLine" runat="server" Width="100px">Line</asp:label></td>
		<td style="WIDTH: 198px"><asp:dropdownlist id="ddlLine" runat="server" Width="155px">
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
			</asp:dropdownlist></td>
		<td style="HEIGHT: 8px"><asp:label id="lblModel" runat="server" Width="100px">Model Name</asp:label></td>
		<td style="HEIGHT: 8px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
	</tr>
    <tr>
        <td>
            <asp:Label ID="lblStationID" runat="server" Width="100px">Station ID</asp:Label></td>
        <td style="width: 198px">
            <asp:dropdownlist id="ddlStationID" runat="server" Width="155px">
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
            </asp:DropDownList></td>
        <td style="height: 8px">
        </td>
        <td style="height: 8px">
        </td>
    </tr>
	<tr>
		<td><asp:checkbox id="ckbRepair" runat="server" Text="Without Repair" Checked="True"></asp:checkbox></td>
		<td style="WIDTH: 198px">
            <asp:RadioButtonList ID="rblWOType" runat="server" CssClass="DataGridFont"
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">正常工單</asp:ListItem>
                <asp:ListItem>重工工單</asp:ListItem>
            </asp:RadioButtonList></td>
		<td></td>
		<td>
            <asp:RadioButtonList ID="rblChartType" runat="server" CssClass="DataGridFont" RepeatDirection="Horizontal" Width="175px">
                <asp:ListItem Selected="True">Line Chart</asp:ListItem>
                <asp:ListItem>Bar Chart</asp:ListItem>
            </asp:RadioButtonList></td>
	</tr>
</table>
<hr/>
<asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px"
	BackColor="White" BorderColor="#CCCCCC" Font-Names="Verdana" CssClass="DataGridFont">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	<Columns>
        <asp:TemplateColumn HeaderText="Station ID">
            <ItemStyle BackColor="#006699" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_ID") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_ID") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
		<asp:BoundColumn DataField="Input" HeaderText="Input"></asp:BoundColumn>
		<asp:BoundColumn DataField="FirstPass" HeaderText="First Pass"></asp:BoundColumn>
		<asp:BoundColumn DataField="FirstYield" HeaderText="First Yield">
			<ItemStyle ForeColor="Red"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="FinalPass" HeaderText="Final Pass"></asp:BoundColumn>
		<asp:BoundColumn DataField="FinalYield" HeaderText="Final Yield">
			<ItemStyle ForeColor="Red"></ItemStyle>
		</asp:BoundColumn>
		<asp:BoundColumn DataField="FirstFail" HeaderText="First Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="SecondFail" HeaderText="Second Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="ThirdFail" HeaderText="Third Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="ForthFail" HeaderText="Forth Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="FifthFail" HeaderText="Fifth Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="FinalFail" HeaderText="Final Fail"></asp:BoundColumn>
		<asp:BoundColumn DataField="FinalFail1" HeaderText="Final Fail1" Visible="False"></asp:BoundColumn>
		<asp:BoundColumn DataField="RetestRate" HeaderText="Retest Rate" Visible="False">
			<ItemStyle ForeColor="Red"></ItemStyle>
		</asp:BoundColumn>
        <asp:ButtonColumn CommandName="RetestRate" DataTextField="RetestRate" HeaderText="Retest Rate">
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" ForeColor="Red" />
        </asp:ButtonColumn>
	</Columns>
</asp:datagrid>
<table width="100%">
	<tr>
		<td>
			<chart:WebChartViewer id="WebChartViewer1" runat="server" Visible="False"></chart:WebChartViewer>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<chart:WebChartViewer id="WebChartViewer2" runat="server" Visible="False"></chart:WebChartViewer></td>
		<td></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
	</tr>
</table>