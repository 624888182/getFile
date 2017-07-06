<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmRepairWip" CodeFile="WFrmRepairWip.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<uc1:ModelTitle id="ModelTitle1" runat="server"></uc1:ModelTitle>
<TABLE class="DataGridFont" id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
            <br>
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td rowspan="3">
			<asp:Button id="btnQuery" runat="server" Text="Query" Width="80px" onclick="btnQuery_Click"></asp:Button></td>
	</tr>
	<TR>
		<TD>
			<asp:Label id="lblLine" runat="server" Width="100px"> Line</asp:Label></TD>
		<TD>
			<asp:DropDownList id="ddlLine" runat="server" Width="155px">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="E10_4F">E10_4F</asp:ListItem>
				<asp:ListItem Value="G02_2F">G02_2F</asp:ListItem>
				<asp:ListItem Value="TY_C1">TY_C1_2F</asp:ListItem>
				<asp:ListItem Value="India_B1">India_B1</asp:ListItem>
			</asp:DropDownList></TD>
		<TD>
			<asp:Label id="lblModel" runat="server" Width="100px"> Model</asp:Label></TD>
		<td>
			<asp:DropDownList id="ddlModel" runat="server" Width="155px"></asp:DropDownList></td>
	</TR>
	<TR>
		<TD>
			<asp:Label id="lblProductID" runat="server" Width="100px">Product ID</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbProductID" runat="server"></asp:TextBox></TD>
		<TD>
			<asp:Label id="lblStationID" runat="server" Width="100px">Station ID</asp:Label></TD>
		<td>
			<asp:DropDownList id="ddlStationID" runat="server" Width="155px"></asp:DropDownList></td>
	</TR>
</TABLE>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<asp:DataGrid id="dgRepairWip" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
	BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
	BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="SteelBlue"></HeaderStyle>
	<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
