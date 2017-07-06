<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmLabelInfoQuery.ascx.cs" Inherits="Boundary_wfrmLabelInfoQuery" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>  
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">

	<tr>
		<td width="50" rowSpan="6"><FONT face="新細明體"></FONT></td>
		<td><asp:label id="lblPO" runat="server" Width="100px">PO</asp:label></td>
		<td><asp:textbox id="tbPO" runat="server"></asp:textbox></td>
		<td><asp:label id="lblPN" runat="server" Width="100px">PN</asp:label></td>
		<td><asp:textbox id="tbPN" runat="server"></asp:textbox></td>
		<td rowSpan="6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button><br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel" Visible="false"  onclick="btnExportExcel_Click"></asp:button></td>
	</tr> 
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Ship Date</asp:label></td>
		<td style="WIDTH: 198px"> 
            <asp:textbox id="tbShipDate" runat="server" ToolTip="請輸入正確的時間格式:2007/1/20 "></asp:textbox> 
            
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>		
	</tr>  
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
    <asp:datagrid id="dgShipLabel" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
	    BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
	    BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" OnPageIndexChanged="dgShipLabel_PageIndexChanged">
	    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	    <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	    <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
		    BackColor="SteelBlue"></HeaderStyle>
	    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	    <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
    </asp:datagrid>