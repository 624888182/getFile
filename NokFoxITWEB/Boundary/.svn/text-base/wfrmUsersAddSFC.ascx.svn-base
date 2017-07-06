<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmUsersAddSFC.ascx.cs" Inherits="Boundary_wfrmUsersAddSFC" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50"></td>
		<td Width="100px"> <asp:label id="lblUser" runat="server" Text="User Name" ></asp:label></td>
		<td style="WIDTH: 150px">
            <asp:TextBox ID="txtUser" runat="server" ></asp:TextBox>
        </td>  
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click" ></asp:button></td> 	
    </tr> 
</table>
<hr> 
<asp:datagrid id="dgUser" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="left"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White"  BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		OnCancelCommand="dgUser_CancelCommand" OnEditCommand="dgUser_EditCommand" OnItemCommand="dgUser_ItemCommand" 
		OnItemDataBound="dgUser_ItemDataBound" OnPageIndexChanged="dgUser_PageIndexChanged" OnUpdateCommand="dgUser_UpdateCommand">
		<FooterStyle ForeColor="#0066ff" BackColor="White"></FooterStyle>
		<EditItemStyle BackColor="#0066ff" HorizontalAlign="Left" />
	    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#CCCC99"></SelectedItemStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
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
          <asp:TemplateColumn HeaderText="LoginName"> 
              <ItemTemplate>
                  <asp:Label ID="lbLoginName" runat="server" Text='<%# Bind("LOGINNAME") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:Label ID="lbELoginName" runat="server" Text='<%# Bind("LOGINNAME") %>'></asp:Label> 
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFLoginName" runat="server" Text=""></asp:TextBox>     
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="UserName">
              <ItemTemplate>
                  <asp:Label ID="lbUserName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEUserName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:TextBox> 
              </EditItemTemplate>      
              <FooterTemplate>
                  <asp:TextBox ID="txtFUserName" runat="server" Text=""></asp:TextBox>          
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="LoginPWD">
              <ItemTemplate>
                  <asp:Label ID="lbLoginpwd" runat="server" Text='<%#Bind("LOGINPSW") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtELoginpwd" runat="server" Text='<%#Bind("LOGINPSW") %>'></asp:TextBox> 
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFLoginpwd" runat="server"></asp:TextBox>      
              </FooterTemplate>
         </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="MenuRight">
              <ItemTemplate>
                  <asp:Label ID="lbMenuRight" runat="server" Text='<%#Bind("MENURIGHT") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtEMenuRight" runat="server" Text='<%#Bind("MENURIGHT") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFMenuRight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="DataRight">
              <ItemTemplate>
                  <asp:Label ID="lbDataRight" runat="server" Text='<%#Bind("DATARIGHT") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtEDataRight" runat="server" Text='<%#Bind("DATARIGHT") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFDataRight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Dept">
              <ItemTemplate>
                  <asp:Label ID="lbDept" runat="server" Text='<%#Bind("DEPT") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtEDept" runat="server" Text='<%#Bind("DEPT") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFDept" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="ComputerName">
              <ItemTemplate>
                  <asp:Label ID="lbComputerName" runat="server" Text='<%#Bind("COMPUTERNAME") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEComputerName" runat="server" Text='<%#Bind("COMPUTERNAME") %>'></asp:TextBox> 
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFComputerName" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="FactoryArea">
              <ItemTemplate>
                  <asp:Label ID="lbFactoryArea" runat="server" Text='<%#Bind("FACTORY_AREA") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEFactoryArea" runat="server" Text='<%#Bind("FACTORY_AREA") %>'></asp:Label> 
              </EditItemTemplate>                            
              <FooterTemplate>
                  <asp:TextBox ID="txtFFactoryArea" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="EffectiveFrom">
              <ItemTemplate>
                  <asp:Label ID="lbEffectiveFrom" runat="server" Text='<%#Bind("EFFECTIVE_FROM") %>'></asp:Label>
              </ItemTemplate>        
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="EffectiveTo">
              <ItemTemplate>
                  <asp:Label ID="lbEffectiveTo" runat="server" Text='<%#Bind("EFFECTIVE_TO") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtEEffectiveTo" runat="server" Text='<%#Bind("EFFECTIVE_TO") %>'></asp:TextBox> 
              </EditItemTemplate>         
           </asp:TemplateColumn>    
           <asp:EditCommandColumn HeaderText="Edit" CancelText="取消" EditText="編輯" UpdateText="更新" >
               <ItemStyle ForeColor="Blue" />
           </asp:EditCommandColumn>
           <asp:TemplateColumn>
               <HeaderTemplate>
                      <asp:Label ID="lbldel" runat="server" text="Delete"></asp:Label>
                  </HeaderTemplate>
                  <ItemTemplate>
                      <asp:LinkButton  ID="btnDelete" runat="server" Text="刪除" CommandName="ItemDelete" ForeColor="red"/>
                      <%--<asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>   --%>    
                  </ItemTemplate>
            </asp:TemplateColumn>
        </Columns><%--
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="DataGridFixedHeader"
			BackColor="SteelBlue"></HeaderStyle>--%>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			BackColor="SteelBlue"></HeaderStyle>
</asp:datagrid>