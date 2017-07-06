<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfrmtesttationdatafailrecord.aspx.cs" Inherits="Boundary_wfrmtesttationdatafailrecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
    <br>
    <asp:datagrid id="dgTestStationData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" onitemdatabound="Data_ItemDataBound">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
    </div>
    </form>
</body>
</html>
