<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NexTestCPKquery.ascx.cs" Inherits="Boundary_NexTestCPKquery" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="3"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px"></asp:dropdownlist></td>
		<td style="HEIGHT: 18px"><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px"></asp:dropdownlist></td>
		<td rowSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button><br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel"  onclick="btnExportExcel_Click" ></asp:button></td>
	</tr>
	<tr>
		<td><asp:label id="lblItem" runat="server" Width="100px">Items</asp:label></td>
		<td><asp:textbox id="tbItem" runat="server"></asp:textbox></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>

<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px" align="center" >
    <asp:Panel ID="Panel1" runat="server" Visible="false" Width="100%">
	    <asp:datagrid id="dgTestStationData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		    BackColor="White"   BorderStyle="None" BorderWidth="1px" Font-Names="Verdana" AutoGenerateColumns="false"
		    BorderColor="#CCCCCC" CellPadding="3"  ShowFooter="True" OnItemCommand="dgTestStationData_ItemCommand" >
		    <Columns>
		        <asp:TemplateColumn HeaderText="Items">
				    <ItemStyle Wrap="False"></ItemStyle>
				    <ItemTemplate>
					    <asp:Label ID="Label1"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Items") %>'>
					    </asp:Label>						
                    </ItemTemplate>
				    <HeaderStyle Wrap="False"></HeaderStyle>
				    <EditItemTemplate>
					    <asp:TextBox ID="TextBox1"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Items") %>'>
					    </asp:TextBox>				
                    </EditItemTemplate>
			    </asp:TemplateColumn> 
			    <asp:TemplateColumn HeaderText="Lo_Limit">
				    <ItemStyle Wrap="False"></ItemStyle>
				    <ItemTemplate>
					    <asp:Label ID="Label11"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lo_Limit") %>'>
					    </asp:Label>						
                    </ItemTemplate>
				    <HeaderStyle Wrap="False"></HeaderStyle>
				    <EditItemTemplate>
					    <asp:TextBox ID="TextBox11"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Lo_Limit") %>'>
					    </asp:TextBox>				
                    </EditItemTemplate>
			    </asp:TemplateColumn> 
			    <asp:TemplateColumn HeaderText="Up_Limit">
				    <ItemStyle Wrap="False"></ItemStyle>
				    <ItemTemplate>
					    <asp:Label ID="Label12"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Up_Limit") %>'>
					    </asp:Label>						
                    </ItemTemplate>
				    <HeaderStyle Wrap="False"></HeaderStyle>
				    <EditItemTemplate>
					    <asp:TextBox ID="TextBox12"  runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Up_Limit") %>'>
					    </asp:TextBox>				
                    </EditItemTemplate>
			    </asp:TemplateColumn> 			     
                <asp:BoundColumn DataField="Max" HeaderText="Max"></asp:BoundColumn>
                <asp:BoundColumn DataField="Min" HeaderText="Min"></asp:BoundColumn>
                <asp:BoundColumn DataField="Average" HeaderText="Average"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Tetval_Sample"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1"  runat="server" CausesValidation="false" CommandName="Sample" Text='<%# DataBinder.Eval(Container, "DataItem.Tetval_Sample") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Tetval_CP" HeaderText="Tetval_CP"></asp:BoundColumn>
                <asp:BoundColumn DataField="Tetval_CPK" HeaderText="Tetval_CPK"></asp:BoundColumn>
	        </Columns>		 
		    <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			    BackColor="SteelBlue"></HeaderStyle>
		    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle> 
	    </asp:datagrid>
	</asp:Panel>
</div>	
	<asp:Panel ID="Panel2" runat="server" Visible="false">
	
	    <cwc:webdatagrid id="dgpiddetail" runat="server" Width="272px" SetShowFooter="True" AutoGenerateColumns="False"
			BorderStyle="None" BorderWidth="1px" BackColor="White" SaveSettings="False" DisableSort="False"
			AllowPaging="True" Font-Size="10px" Font-Names="Verdana" DisableSetButton="False" BorderColor="#CCCCCC"
			AllowSorting="True" sUserLanguage="en-us" DisablePaging="False" CellPadding="3" ShowFooter="True"
			UserID="Any" CssClass="DataGridFont" PageSize="25">
            <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
            </PagerStyle>
            <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
            <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
            <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="ITEMS"  HeaderText="Items">
                <ItemStyle Wrap="False"></ItemStyle>
                <HeaderStyle Wrap="False"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TEST_VALUE"   HeaderText="Test_Data">
                <ItemStyle Wrap="False"></ItemStyle>
                <HeaderStyle Wrap="False"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TEST_TIME"  HeaderText="Test_Time">
                <ItemStyle Wrap="False"></ItemStyle>
                <HeaderStyle Wrap="False"></HeaderStyle>
                </asp:BoundColumn>
            </Columns>
		</cwc:webdatagrid>
	    <%--<asp:datagrid id="dgpiddetail1" runat="server"  Width="60%" CssClass="DataGridFont" Font-Size="10px"
		    BackColor="White"   BorderStyle="None" BorderWidth="1px" Font-Names="Verdana" AutoGenerateColumns="false"
		    BorderColor="#CCCCCC" CellPadding="3"  ShowFooter="True">
		    <Columns> 
                <asp:BoundColumn DataField="ITEMS" HeaderText="Items"></asp:BoundColumn>
                <asp:BoundColumn DataField="TEST_VALUE"   HeaderText="Test_Data"></asp:BoundColumn>
                <asp:BoundColumn DataField="TEST_TIME"  HeaderText="Test_Time"></asp:BoundColumn> 
	        </Columns>		 
		    <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="DataGridFixedHeader"
			    BackColor="SteelBlue"></HeaderStyle>
		    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle> 
	    </asp:datagrid>--%>
	</asp:Panel>
