<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmIMEIChangePorder.ascx.cs" Inherits="Boundary_wfrmIMEIChangePorder" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="60px"> <asp:label id="lbPorder" runat="server"></asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtPorder"></asp:TextBox>
        </td>
        <td Width="150px"  align="right" rowspan="3" valign="bottom"> 
			<asp:button id="btnchange" runat="server" Width="80px" Text="changeporder" OnClick="btnchange_Click" ></asp:button></td> 	
    </tr>
    <tr height="5">
		<td></td>
		<td></td>
		</tr>
    <tr height="15">
        <td width="50" rowSpan="2"></td>
		<td Width="60px"><asp:label id="lbIMEI" runat="server" ></asp:label></td>
		<td Width="60px">
            <asp:TextBox runat="server" ID="txtIMEI"></asp:TextBox>
        </td>
	</tr>
	<tr height="15">
		<td></td>
		<td></td>
		<td></td>
		<td></td>
	</tr>
</table>
<table  class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
	    <td width="50"></td>
		<td><asp:checkbox id="ckbSJUG" runat="server" Text=" Check SJUG " Checked="True"></asp:checkbox></td>
		<td width="50"></td>
		<td><asp:checkbox id="ckbPPart" runat="server" Text=" Check P Part " Checked="True"></asp:checkbox></td>
		<td></td> 
	</tr>
</table>