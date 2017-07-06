<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmNuberWarning.ascx.cs" Inherits="Boundary_WFrmNuberWarning" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
	<td colspan="2"  style="width: 30%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" OnClick="btnQuery_Click"  ></asp:button>
		</td>
		<td colspan="2" style="width: 30%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnSetMail" runat="server" Width="100px" Text="SetMail" OnClick="btnSetMail_Click" ></asp:button>
		</td>
		<td colspan="2" style="width: 30%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnSetWarningLine" runat="server" Width="100px" Text="SetWarningLine" OnClick="btnSetWarningLine_Click"  ></asp:button>
		</td>
	</tr>
 
	<tr>
	    <td colspan="2" style="width: 30%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			 <asp:Label ID="lblMail" runat="server" Text="Mail Name:"></asp:Label>
		</td>
		<td colspan="4" style="width: 60%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:TextBox  ID="txtMail" runat="server" Width="250px"></asp:TextBox>
			<asp:Label ID="lblMailEx" runat="server" Text="/FIH/FOXCONN@FOXCONN"></asp:Label>
		</td>
	</tr>
		<tr>
		<td colspan="6" style="width: 40%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			 <asp:ListBox ID="lstMails" runat="server" Height="200px" Width="80%"></asp:ListBox>
		</td>
		</tr>
		<tr>
		<td colspan="2" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			 <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100px" OnClick="btnAdd_Click" />
		</td>
		<td colspan="2" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			 <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="100px" OnClick="btnRemove_Click" />
		</td>
		<td colspan="2" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			 <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" Visible="False" />
		</td>
		</tr>
</table>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px"><%--PageSize="25"--%>
	<asp:datagrid id="dgData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="False" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3"  ShowFooter="True" onitemdatabound="FailData_ItemDataBound">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>        
	</asp:datagrid>
	
	
    
    <asp:datagrid id="dgBottomLine" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="left"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White"  BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		OnCancelCommand="dgBottomLine_CancelCommand" OnEditCommand="dgBottomLine_EditCommand" OnItemCommand="dgBottomLine_ItemCommand" 
		OnItemDataBound="dgBottomLine_ItemDataBound" OnPageIndexChanged="dgBottomLine_PageIndexChanged" OnUpdateCommand="dgBottomLine_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="Model"> 
              <ItemTemplate>
                  <asp:Label ID="lbModel" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:Label ID="lbModel" runat="server" Text='<%# Bind("MODEL") %>'></asp:Label> 
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFModel" runat="server" Text=""></asp:TextBox>     
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Type">
              <ItemTemplate>
                  <asp:Label ID="lbType" runat="server" Text='<%# Bind("TYPE") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="lbType" runat="server" Text='<%# Bind("TYPE") %>'></asp:TextBox> 
              </EditItemTemplate>      
              <FooterTemplate>
                  <asp:TextBox ID="txtFType" runat="server" Text=""></asp:TextBox>          
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="BottomLine">
              <ItemTemplate>
                  <asp:Label ID="lbBottomLine" runat="server" Text='<%#Bind("BOTTOM_LINE") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtEBottomLine" runat="server" Text='<%#Bind("BOTTOM_LINE") %>'></asp:TextBox> 
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFBottomLine" runat="server"></asp:TextBox>      
              </FooterTemplate>
         </asp:TemplateColumn>          
         <%--<asp:TemplateColumn HeaderText="MenuRight">
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
           </asp:TemplateColumn>--%>    
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
</div>
