<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmOBAquery.ascx.cs" Inherits="Boundary_wfrmOBAquery" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><asp:label id="lblCartonNo" runat="server" style="WIDTH:100px"></asp:label></td>
		<td width="200px">
            <asp:textbox id="tbCartonNo" runat="server" Width="150px"></asp:textbox></td>
		<td><asp:label id="lblPID" runat="server" style="WIDTH:100px">PID/IMEI</asp:label></td>
		<td width="200px">
            <asp:textbox id="tbPID" runat="server" Width="150px"></asp:textbox> </td>
        <td Width="200px" align="center"><asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button></td>    
	    <td Width="200px" align="left"><asp:button id="btnExportExcel" runat="server" Width="100px" OnClick="btnExportExcel_Click"></asp:button></td>    
	</tr>	
	<tr>
		<td></td>
		<td></td>
		<td></td>
		<td></td>			
		<td Width="200px" align="center">
			<asp:label id="Label3" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
	<asp:datagrid id="dgOBA" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="20" ShowFooter="True" OnPageIndexChanged="dgOBA_PageIndexChanged">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
