<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmCheckDiskNoMantenance.ascx.cs" Inherits="Boundary_wfrmCheckDiskNoMantenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lblpartno" runat="server" >A Part</asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtPart"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click" ></asp:button></td> 	
    </tr> 
</table>
<hr>
  <asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
    <asp:datagrid id="dgpart" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnCancelCommand="dgpart_CancelCommand" 
		OnEditCommand="dgpart_EditCommand" OnItemCommand="dgpart_ItemCommand" OnItemDataBound="dgpart_ItemDataBound" 
		OnPageIndexChanged="dgpart_PageIndexChanged" OnUpdateCommand="dgpart_UpdateCommand"  >
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
        <asp:TemplateColumn HeaderText="A Part" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblapart" runat="server" Text='<%#Bind("APART") %>'></asp:Label>
              </ItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFapart" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Key Part No" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblkeypart" runat="server" Text='<%#Bind("KEYPART") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate> 
                  <asp:Label ID="txtEkeypart" runat="server" Text='<%# Bind("KEYPART") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtkeypart" runat="server" Text='<%# Bind("KEYPART") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFkeypart" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Emp No" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblempno" runat="server" Text='<%#Bind("EMPNO") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate> 
                  <asp:TextBox ID="txtempno" runat="server" Text='<%# Bind("EMPNO") %>'></asp:TextBox>
              </EditItemTemplate> 
        </asp:TemplateColumn>           
        <asp:TemplateColumn HeaderText="Create Date" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblcreatedate" runat="server" Text='<%#Bind("CREATEDATE") %>'></asp:Label>
              </ItemTemplate>
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