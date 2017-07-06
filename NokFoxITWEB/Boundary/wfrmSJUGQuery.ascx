<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmSJUGQuery.ascx.cs" Inherits="Boundary_wfrmSJUGQuery" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>   
 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50"></td>
		<td Width="100px"> <asp:label id="Label1" runat="server" Text="請輸入料號：" ></asp:label></td>
		<td style="WIDTH: 150px">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>     
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="Button1" runat="server" Width="80px" OnClick="Button1_Click" ></asp:button></td> 	   
	</tr> 
</table>   
<hr> 
<asp:Label ID="Label2" runat="server" Text="總計：0條" Width="94px"></asp:Label>		
<asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  Font-Names="Verdana"
     BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true" PagerSettings-Mode="Numeric"
     PageSize="30" AllowPaging="True" CssClass="DataGridFont" OnPageIndexChanging="GridView1_PageIndexChanging">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <PagerStyle  HorizontalAlign="Left" />
    <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
    <AlternatingRowStyle BackColor="WhiteSmoke" />
</asp:GridView> 
 