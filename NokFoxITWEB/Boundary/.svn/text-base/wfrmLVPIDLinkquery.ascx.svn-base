<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmLVPIDLinkquery.ascx.cs" Inherits="Boundary_wfrmLVPIDLinkquery" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center" border="0" >
	<tr>
		<td align="center" width="200px"><asp:label id="lblLVID" runat="server"></asp:label></td>
		<td width="200px">
			<asp:textbox id="tbProductID" runat="server" Width="150px"></asp:textbox> 
		</td>
		<td align="center" width="200px">
		    <asp:button id="btnQuery" Text="Query" Width="100px" Runat="server" onclick="btnQuery_Click"></asp:button></td>
	</tr>
	<tr>
		<td style="height: 19px"></td>
		<td style="height: 19px">
		   <asp:label id="Label2" runat="server" Visible="False" BackColor="White" ForeColor="Red"></asp:label>
		</td>
		<td style="height: 19px"></td>
	</tr>
</table>
<hr>
<table cellSpacing="0" cellPadding="0" border="0" width="80%" align="center"  >     
    <tr align="center" width="100%" height="100px">
       <td width="100%">  
           <asp:DataGrid ID="dgLVIDLink" runat="server" AutoGenerateColumns="true" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="2px" CssClass="DataGridFont" 
                Font-Names="Verdana" Font-Size="15px" PageSize="20" CellSpacing="1" CellPadding="2">
                <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <AlternatingItemStyle BackColor="WhiteSmoke" />
                <ItemStyle BackColor="Cornsilk" ForeColor="#000066" HorizontalAlign="Center"  Font-Size="15px" BorderWidth="2px" Height="20px" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Font-Size="15px" />
                <FooterStyle BackColor="White" ForeColor="#000066" Font-Size="15px"/>
          </asp:DataGrid>        
       </td>
    </tr>
</table>