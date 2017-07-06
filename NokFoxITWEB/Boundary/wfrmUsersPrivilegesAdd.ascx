<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmUsersPrivilegesAdd.ascx.cs" Inherits="Boundary_wfrmUsersPrivilegesAdd" %>
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
		Visible="false" OnCancelCommand="dgUser_CancelCommand" OnEditCommand="dgUser_EditCommand" OnItemCommand="dgUser_ItemCommand" 
		OnPageIndexChanged="dgUser_PageIndexChanged" OnItemDataBound="dgUser_ItemDataBound" OnUpdateCommand="dgUser_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="UserName"> 
              <ItemTemplate>
                  <asp:Label ID="lbUserName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:Label ID="lblEUserName" runat="server" Text='<%# Bind("USERNAME") %>'></asp:Label> 
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFUserName" runat="server" Text=""></asp:TextBox>     
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Password">
              <ItemTemplate>
                  <asp:Label ID="lbPWD" runat="server" Text='<%# Bind("PASSWORD") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEPWD" runat="server" Text='<%# Bind("PASSWORD") %>'></asp:TextBox> 
              </EditItemTemplate>      
              <FooterTemplate>
                  <asp:TextBox ID="txtFPWD" runat="server" Text=""></asp:TextBox>          
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="DDATE">
              <ItemTemplate>
                  <asp:Label ID="lbDdate" runat="server" Text='<%#Bind("DDATE") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:Label ID="lblEDdate" runat="server" Text='<%#Bind("DDATE") %>'></asp:Label> 
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:Label ID="lbFDdate" runat="server"></asp:Label>      
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Remark">
              <ItemTemplate>
                  <asp:Label ID="lbRemark" runat="server" Text='<%#Bind("REMARK") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtRemark" runat="server" Text=""></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFRemark" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Dept">
              <ItemTemplate>
                  <asp:Label ID="lbDept" runat="server" Text='<%#Bind("DEPT") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtDept" runat="server" Text=""></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFDept" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Groupid">
              <ItemTemplate>
                  <asp:Label ID="lbGroupid" runat="server" Text='<%#Bind("GROUPID") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtGroupid" runat="server" Text=""></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFGroupid" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Module">
              <ItemTemplate>
                  <asp:Label ID="lbModule" runat="server" Text='<%#Bind("MODULE") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEModule" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFModule" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Name">
              <ItemTemplate>
                  <asp:Label ID="lbName" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtEName" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>                            
              <FooterTemplate>
                  <asp:TextBox ID="txtFName" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="U_DEL">
              <ItemTemplate>
                  <asp:Label ID="lbUdel" runat="server" Text='<%#Bind("U_DEL") %>'></asp:Label>
              </ItemTemplate>               
              <EditItemTemplate>
                  <asp:TextBox ID="txtEUdel" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>            
              <FooterTemplate>
                  <asp:TextBox ID="txtFUdel" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="U_Update">
              <ItemTemplate>
                  <asp:Label ID="lbUupdate" runat="server" Text='<%#Bind("U_UPDATE") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtEUupdate" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFUupdate" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>          
           <asp:TemplateColumn HeaderText="U_ALL_P">
              <ItemTemplate>
                  <asp:Label ID="lbUallp" runat="server" Text='<%#Bind("U_ALL_P") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtEUallp" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFUallp" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Department">
              <ItemTemplate>
                  <asp:Label ID="lbDepartment" runat="server" Text='<%#Bind("DEPARTMENT") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtEDepartment" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFDepartment" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Menu_Part">
              <ItemTemplate>
                  <asp:Label ID="lbMenupart" runat="server" Text='<%#Bind("MENU_PART") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtEMenupart" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFMenupart" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
          </asp:TemplateColumn>          
          <asp:TemplateColumn HeaderText="CnName">
              <ItemTemplate>
                  <asp:Label ID="lbCnName" runat="server" Text='<%#Bind("CNNAME") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:TextBox ID="txtECnName" runat="server" Text=""></asp:TextBox> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFCnName" runat="server" Text=""></asp:TextBox>    
              </FooterTemplate>
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