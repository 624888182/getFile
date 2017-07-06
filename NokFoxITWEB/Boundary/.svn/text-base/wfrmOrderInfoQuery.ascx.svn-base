<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmOrderInfoQuery.ascx.cs" Inherits="Boundary_wfrmOrderInfoQuery" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"> 
	<tr>
	    <td width="50" rowSpan="4"></td>
		<td><asp:label id="lblPpart" runat="server" Width="100px">P Part</asp:label></td>
		<td style="WIDTH: 198px">
			<asp:TextBox id="txtPpart" runat="server"></asp:TextBox></td>
		<td>
			<asp:Label id="lblPorder" runat="server" Width="100px">P Order</asp:Label></td>
		<td><asp:textbox id="txtPorder" runat="server"></asp:textbox></td>
		<td rowSpan="4"> <FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" OnClick="btnQuery_Click"></asp:button></td>
		<td rowSpan="4"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnExportExcel" Visible="false" runat="server" Width="100px" Text="Export To Excel" OnClick="btnExportExcel_Click"></asp:button>
	    </td>
	</tr>	 
</table>
<hr>
<asp:Label id="Label1" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
   <asp:datagrid id="dgOrder" runat="server" CssClass="DataGridFont"  AllowPaging="True" PageSize="15"
		BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana"
		ShowFooter="True" BackColor="White" OnPageIndexChanged="dgOrder_PageIndexChanged" >							
        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
        <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
        <ItemStyle Font-Size="10pt" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
        <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
        <Columns> 
            <asp:BoundColumn>
                <HeaderStyle Font-Size="12pt" Wrap="False"></HeaderStyle>
                <ItemStyle Font-Size="20pt"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
        <PagerStyle HorizontalAlign="Center" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>