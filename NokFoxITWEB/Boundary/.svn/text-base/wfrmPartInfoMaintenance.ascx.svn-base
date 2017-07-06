<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmPartInfoMaintenance.ascx.cs" Inherits="Boundary_wfrmPartInfoMaintenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lblSpart" runat="server" >S Part</asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtSpart"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click"  ></asp:button></td> 	
    </tr> 
</table>
<hr>
  <asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
    <asp:datagrid id="dgSpart" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnCancelCommand="dgSpart_CancelCommand" OnEditCommand="dgSpart_EditCommand" OnItemCommand="dgSpart_ItemCommand" OnItemDataBound="dgSpart_ItemDataBound" OnPageIndexChanged="dgSpart_PageIndexChanged" OnUpdateCommand="dgSpart_UpdateCommand"   >
		<Columns>  
		    <asp:TemplateColumn> 
              <HeaderTemplate>
                  <asp:LinkButton ID="lkbAddItem" runat="server" CommandName="AddItem">新增</asp:LinkButton>
              </HeaderTemplate>
              <ItemTemplate>
                  <asp:Label ID="lblID" runat="server"  ></asp:Label>         
              </ItemTemplate>
               <FooterTemplate>
                  <asp:LinkButton  ID="btnCommit" runat="server" Text="確定" CommandName="ItemSure"/>
                  <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>
              </FooterTemplate> 
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="S Part"  ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lbspart" runat="server" Text='<%# Bind("SPART") %>' ></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFspart" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Sequence ID" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbsequenceid" runat="server" Text='<%# Bind("SEQUENCE_ID") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEsequenceid" runat="server" Text='<%#Bind("SEQUENCE_ID") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEsequenceid" runat="server" Text='<%# Bind("SEQUENCE_ID") %>'></asp:TextBox>
              </EditItemTemplate>   
              <FooterTemplate>
                  <asp:TextBox ID="txtFsequenceid" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Creation Date"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcreationdate" runat="server" Text='<%#Bind("CREATION_DATE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcreationdate" runat="server" Text='<%#Bind("CREATION_DATE") %>'></asp:Label> 
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:Label ID="lbFcreationdate" runat="server" Text=""></asp:Label>         
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Description" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdescription" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEdescription" runat="server" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEdescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFdescription" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Model" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbmodel" runat="server" Text='<%#Bind("MODEL") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEmodel" runat="server" Text='<%#Bind("MODEL") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEmodel" runat="server" Text='<%# Bind("MODEL") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFmodel" runat="server" Text=""></asp:TextBox>      
              </FooterTemplate>
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="Barcode Prefix" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbbarcodeprefix" runat="server" Text='<%#Bind("BARCODE_PREFIX") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEbarcodeprefix" runat="server" Text='<%#Bind("BARCODE_PREFIX") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEbarcodeprefix" runat="server" Text='<%# Bind("BARCODE_PREFIX") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFbarcodeprefix" runat="server" Text=""></asp:TextBox>      
              </FooterTemplate>
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Order Prefix" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lborderprefix" runat="server" Text='<%#Bind("ORDER_PREFIX") %>'></asp:Label>
              </ItemTemplate>
               <EditItemTemplate>
                  <asp:Label ID="lbEorderprefix" runat="server" Text='<%#Bind("ORDER_PREFIX") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEorderprefix" runat="server">
                      <asp:ListItem Value="S" Text="S"></asp:ListItem>
                      <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                  </asp:DropDownList>
              </EditItemTemplate>
              <FooterTemplate>                  
                  <asp:DropDownList ID="ddlForderprefix" runat="server">
                      <asp:ListItem Value="S" Text="S" Selected="True"></asp:ListItem>
                      <asp:ListItem Value="Y" Text="Y"></asp:ListItem>
                  </asp:DropDownList>
              </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Board Qty" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbboardqty" runat="server" Text='<%#Bind("BOARD_QTY") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEboardqty" runat="server" Text='<%#Bind("BOARD_QTY") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEboardqty" runat="server" Text='<%# Bind("BOARD_QTY") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFboardqty" runat="server" Text=""></asp:TextBox>      
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Route Code" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbroutecode" runat="server" Text='<%#Bind("ROUTE_CODE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEroutecode" runat="server" Text='<%#Bind("ROUTE_CODE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEroutecode" runat="server" Text='<%# Bind("ROUTE_CODE") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFroutecode" runat="server" Text=""></asp:TextBox>      
              </FooterTemplate>
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbname" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEname" runat="server" Text='<%#Bind("NAME") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEname" runat="server" Text='<%# Bind("NAME") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFname" runat="server" Text=""></asp:TextBox>      
              </FooterTemplate>
        </asp:TemplateColumn>   
        <asp:EditCommandColumn HeaderText="Edit"  ButtonType="LinkButton" CancelText="取消" EditText="編輯" UpdateText="更新"  ItemStyle-ForeColor="blue" />
        <asp:TemplateColumn>
           <HeaderTemplate>
                  <asp:Label ID="lbldel" runat="server" text="Delete"></asp:Label>
              </HeaderTemplate>
              <ItemTemplate>
                  <asp:LinkButton  ID="btnDelete" runat="server" Text="刪除" CommandName="ItemDelete" ForeColor="red"/>
                  <%--<asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>   --%>    
              </ItemTemplate>
        </asp:TemplateColumn>
        </Columns>
	    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#cccc99"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<EditItemStyle BackColor="#99cccc" />
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
</asp:datagrid>