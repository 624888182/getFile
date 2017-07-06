<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmUnlinkCITPass.ascx.cs" Inherits="Boundary_wfrmUnlinkCITPass" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lbIMEI" runat="server" Text="IMEI/SN"></asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtIMEI"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnUnlink" runat="server" Width="80px" OnClick="btnUnlink_Click" ></asp:button></td> 	
    </tr> 
</table>
