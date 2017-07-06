<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmThirdtestfailunlock.ascx.cs" Inherits="Boundary_wfrmThirdtestfailunlock" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="6"><FONT face="新細明體"></FONT></td>		
		<td><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td><asp:dropdownlist id="ddlStation" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist></td>
		
	</tr> 
	<tr>
	    <td height="5"> </td>
	</tr>
	<tr>
		<td><asp:label id="lblProductID" runat="server" Width="100px">Product ID</asp:label></td>
		<td><asp:textbox id="txtProductID" runat="server" ></asp:textbox></td>
	
	<td Width="200px" align="center"><asp:button id="btnQuery" runat="server"  Width="100px" Text="UNLOCK" onclick="btnQuery_Click"></asp:button></td>    
	    <td Width="200px" align="left"></td>  
	    </tr>  	
</table>
<hr>
<asp:Label id="Label4"  runat="server" CssClass="DataGridFont" Font-Size="80px" Visible="false" ForeColor="green" Font-Bold="true"></asp:Label>
<br>
	