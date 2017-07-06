<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmPIDRangesMaintenance.ascx.cs" Inherits="Boundary_wfrmPIDRangesMaintenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50"></td>
		<td Width="100px"> <asp:label id="lblModel" runat="server" ></asp:label></td>
		<td style="WIDTH: 150px">
            <asp:dropdownlist id="ddlModel" runat="server" Width="100px" ></asp:dropdownlist>
        </td>        
		<td Width="100px"> <asp:label id="lblType" runat="server" ></asp:label></td>
		<td style="WIDTH: 150px">
            <asp:DropDownList ID="ddlType" runat="server" Width="100px"> 
                <asp:ListItem Text="" Selected="true"></asp:ListItem>
                <asp:ListItem Text="BlueTooth"></asp:ListItem>
                <asp:ListItem Text="Imei"></asp:ListItem>
                <asp:ListItem Text="MSN"></asp:ListItem>
                <asp:ListItem Text="Picasso"></asp:ListItem>            
            </asp:DropDownList>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click" ></asp:button></td> 	
    </tr> 
</table>
<hr>
<asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
<asp:datagrid id="dgLabelRange" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="30"
		BackColor="White"  BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		Visible="false" OnCancelCommand="dgLabelRange_CancelCommand"  OnEditCommand="dgLabelRange_EditCommand" 
		OnItemCommand="dgLabelRange_ItemCommand" OnItemDataBound="dgLabelRange_ItemDataBound" 
		OnPageIndexChanged="dgLabelRange_PageIndexChanged" OnUpdateCommand="dgLabelRange_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="Model" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbModel" runat="server" Text='<%# Bind("PROJECT_CODE") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:Label ID="lblEModel" runat="server" Text='<%# Bind("PROJECT_CODE") %>'></asp:Label> 
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:Label ID="lbFModel" runat="server" ></asp:Label>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Type" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbType" runat="server" Text='<%# Bind("NUMBER_CATEGORY") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:Label ID="lblEType" runat="server" Text='<%# Bind("NUMBER_CATEGORY") %>'></asp:Label> 
              </EditItemTemplate>      
              <FooterTemplate>
                  <asp:Label ID="lbFType" runat="server"></asp:Label>          
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Region"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbRegion" runat="server" Text='<%#Bind("ORGANIZATION_ID") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:Label ID="lblERegion" runat="server" Text='<%# Bind("ORGANIZATION_ID") %>'></asp:Label> 
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:Label ID="lbFRegion" runat="server" Text="" ></asp:Label>      
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Prefix"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPrefix" runat="server" Text='<%#Bind("PREFIX_CODE") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtPrefix" runat="server" Text='<%# Bind("PREFIX_CODE") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFPrefix" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="First"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbFirst" runat="server" Text='<%#Bind("FIRST_NUMBER") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtFirst" runat="server" Text='<%# Bind("FIRST_NUMBER") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFFirst" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Last"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLast" runat="server" Text='<%#Bind("LAST_NUMBER") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtLast" runat="server" Text='<%# Bind("LAST_NUMBER") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFLast" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Create"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCreate" runat="server" Text='<%#Bind("CREATED_BY") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:Label ID="lblECreate" runat="server" Text='<%# Bind("CREATED_BY") %>'></asp:Label> 
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:Label ID="lbFCreate" runat="server" Text='<%#Bind("CREATED_BY") %>'></asp:Label>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="CreateTime"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCreateTime" runat="server" Text='<%#Bind("CREATION_DATE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lblECreateTime" runat="server" Text='<%# Bind("CREATION_DATE") %>'></asp:Label> 
              </EditItemTemplate>                            
              <FooterTemplate>
                  <asp:Label ID="lbFCreateTime" runat="server" Text='<%#Bind("CREATION_DATE") %>'></asp:Label>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="LastUpdate"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLastUpdate" runat="server" Text='<%#Bind("LAST_UPDATED_BY") %>'></asp:Label>
              </ItemTemplate>               
              <EditItemTemplate>
                  <asp:Label ID="lblELastUpdate" runat="server" Text='<%# Bind("LAST_UPDATED_BY") %>'></asp:Label> 
              </EditItemTemplate>            
              <FooterTemplate>
                  <asp:Label ID="lbFLastUpdate" runat="server" Text='<%#Bind("LAST_UPDATED_BY") %>'></asp:Label>    
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="LastUpdateTime"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLastUpdateTime" runat="server" Text='<%#Bind("LAST_UPDATE_DATE") %>'></asp:Label>
              </ItemTemplate>                         
              <EditItemTemplate>
                  <asp:Label ID="lblELastUpdateTime" runat="server" Text='<%# Bind("LAST_UPDATE_DATE") %>'></asp:Label> 
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:Label ID="lbFLastUpdateTime" runat="server" Text='<%#Bind("LAST_UPDATE_DATE") %>'></asp:Label>    
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
		<%--<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="DataGridFixedHeader"
			BackColor="SteelBlue"></HeaderStyle>--%>
	    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
</asp:datagrid>