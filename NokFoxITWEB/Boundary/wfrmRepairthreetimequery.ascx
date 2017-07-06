<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmRepairthreetimequery.ascx.cs" Inherits="Boundary_wfrmRepairthreetimequery" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<TABLE class="DataGridFont" id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
            <br>
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td rowspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button id="btnQuery" runat="server" Text="Query" Width="80px" onclick="btnQuery_Click"></asp:Button></td>
	</tr>
	<TR>
	    <TD>
			<asp:Label id="lblModel" runat="server" Width="100px"> Model</asp:Label></TD>
		<td>
			<asp:DropDownList id="ddlModel" runat="server" Width="155px"></asp:DropDownList></td>
		<TD>
			<asp:Label id="lblProductID" runat="server" Width="100px">Product ID</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbProductID" runat="server"></asp:TextBox></TD>
		
	</TR>
	 
</TABLE>
<hr>

<br>

<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="100%" border="0">
    <tr>
		<td align="center" width="30%"><asp:label id="Label2" runat="server" Visible="False">Repair Qty</asp:label></td>
		<td align="center" width="70%"><asp:label id="Label3" runat="server" Visible="False">Repair Detail</asp:label></td>
	</tr>
<tr>
	<td vAlign="top"  align="center" width="30%">
        <asp:DataGrid id="dgRepairHistory" runat="server" CssClass="DataGridFont" Font-Size="10px" AutoGenerateColumns="false"
	        BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
	        BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" OnItemCommand="dgRepairHistory_ItemCommand">
	        <Columns>
	        <asp:TemplateColumn HeaderText="ProductID">
		        <ItemStyle HorizontalAlign="Right"></ItemStyle>
		        <ItemTemplate>
			        <asp:LinkButton id="lbtnpid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRODUCT_ID") %>' CausesValidation="false" CommandName="PRODUCTID">
			        </asp:LinkButton>
                </ItemTemplate>
		        <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
	        </asp:TemplateColumn>
	        <asp:TemplateColumn HeaderText="Model">
		        <ItemStyle HorizontalAlign="Right"></ItemStyle>
		        <ItemTemplate>
			        <asp:Label ID="lblmodel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MODEL_ID") %>'></asp:Label> 
                </ItemTemplate>
		        <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
	        </asp:TemplateColumn> 
	        <asp:TemplateColumn HeaderText="Repair Qty">
		        <ItemStyle HorizontalAlign="Right"></ItemStyle>
		        <ItemTemplate>
			        <asp:Label ID="lblqty" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QTY") %>'></asp:Label> 
                </ItemTemplate>
		        <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
	        </asp:TemplateColumn> 
	        </Columns>				
	        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	        <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	        <ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	        <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
        <asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
    </td>
		<td vAlign="top" align="center" width="70%" rowSpan="2">
		    <asp:datagrid id="dgDtail" runat="server" CssClass="DataGridFont" AutoGenerateColumns="true" 
				BorderStyle="None" BorderWidth="1px" BackColor="White" Font-Size="10px" Font-Names="Verdana" BorderColor="#CCCCCC" ShowFooter="True">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
			</asp:datagrid>
		</td>
	 </tr>
 </table>
