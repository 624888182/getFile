<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmShippingInfo" CodeFile="WFrmShippingInfo.ascx.cs" %>
<uc1:Modeltitle id="ModelTitle1" runat="Server"></uc1:Modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="4"></td>
		<td><asp:label id="lblStartDate" runat="server" Width="100px" Visible="False">Ship Date From</asp:label></td>
		<td style="WIDTH: 198px"><asp:textbox id="tbStartDate" runat="server" Visible="False"></asp:textbox><asp:button id="btnDateFrom" runat="server" Width="25px" Text="..." Visible="False"></asp:button></td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px" Visible="False">Ship Date To</asp:label></td>
		<td><asp:textbox id="tbEndDate" runat="server" Visible="False"></asp:textbox><asp:button id="btnDateTo" runat="server" Width="25px" Text="..." Visible="False"></asp:button></td>
		<td rowSpan="4"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
		<td rowSpan="4"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="btnExportExcel" runat="server" Width="100px" Text="Export To Excel" OnClick="btnExportExcel_Click" ></asp:button>
	    </td>
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
		<td><asp:label id="lblInvoiceNO" runat="server" Width="100px">Invoice Number</asp:label></td>
		<td style="WIDTH: 198px">
			<asp:TextBox id="tbInvoiceNO" runat="server"></asp:TextBox></td>
		<td>
			<asp:Label id="lblCartonID" runat="server" Width="100px">Carton ID</asp:Label></td>
		<td><asp:textbox id="tbCartonID" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td><FONT face="新細明體">
				<asp:label id="lblIMEI" runat="server" Width="100px">IMEI&Picasso</asp:label></FONT></td>
		<td style="WIDTH: 198px">
			<asp:TextBox id="tbIMEI" runat="server"></asp:TextBox></td>
		<td></td>
		<td></td>
	</tr>
</table>
<hr>
<asp:Label id="Label1" runat="server" CssClass="DataGridFont"></asp:Label>
<br> 

<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px">
	<asp:GridView ID="dgProduct" runat="server"  Font-Size="10px" AutoGenerateColumns="false"
	                  AllowPaging="false"  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="None"  
                      OnRowDataBound="dgProduct_RowDataBound"  OnSelectedIndexChanging="dgProduct_SelectedIndexChanging">
                   <Columns>    
                        <asp:BoundField DataField="invoice" HeaderText="invoice" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="so_number" HeaderText="so_number" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="pn" HeaderText="pn" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="imei" HeaderText="imei" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="sn" HeaderText="sn" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="carton_id" HeaderText="carton_id" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="pallet_id" HeaderText="pallet_id" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="po_no" HeaderText="po_no" ReadOnly="True"></asp:BoundField>  
                        <asp:BoundField DataField="sw_version" HeaderText="sw_version" ReadOnly="True"></asp:BoundField>                          
                        <asp:BoundField DataField="delivery_date" HeaderText="delivery_date" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="sim_lock" HeaderText="sim_lock" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="unlock_code" HeaderText="unlock_code" ReadOnly="True"></asp:BoundField> 
                    </Columns>
                    <RowStyle BackColor="#F1F8F1"/>
                    <HeaderStyle  HorizontalAlign="Center" Font-Size="15px" Font-Bold="true" />
                    <AlternatingRowStyle BackColor="White" />        
                    <PagerSettings Visible="true"  />   
                </asp:GridView>
                </div>
