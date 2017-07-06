<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmMeasureStandard.ascx.cs" Inherits="Boundary_wfrmMeasureStandard" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lblModel" runat="server" ></asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtModel"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click"  ></asp:button></td> 	
    </tr> 
</table>

<hr>
  <asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
  
  <%--<asp:GridView ID="gvMeasure" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
      BackColor="White" userid="any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
      CellPadding="3" BorderColor="#CCCCCC" PageSize="15"  ShowFooter="False"  AllowSorting="True" 
      OnPageIndexChanging="gvMeasure_PageIndexChanging"  
      AutoGenerateColumns="False" OnSorting="gvMeasure_Sorting" OnRowCommand="gvMeasure_RowCommand" OnRowCreated="gvMeasure_RowCreated" OnRowDeleted="gvMeasure_RowDeleted" OnRowDeleting="gvMeasure_RowDeleting" >
     <%-- <FooterStyle ForeColor="#000066" BackColor="White" />
      <Columns> 
          <asp:TemplateField>
              <HeaderTemplate>
                  <asp:LinkButton ID="lkbAddItem" runat="server" CommandName="AddItem">新增</asp:LinkButton>
              </HeaderTemplate>
              <ItemTemplate>
                  <asp:Label ID="ID" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>         
              </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Model" SortExpression="Model"> 
              <ItemTemplate>
                  <asp:Label ID="lbModelID" runat="server" Text='<%# Bind("Model_ID") %>' ></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtModelID" runat="server" Text='<%# Bind("Model_ID") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFModelID" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Country" SortExpression="Country" >
              <ItemTemplate>
                  <asp:Label ID="lbCountry" runat="server" Text='<%# Bind("country_name") %>'  ></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtCountry" runat="server" Text='<%# Bind("country_name") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFCountry" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Max_Weight" SortExpression="Max_Weight">
              <EditItemTemplate>
                  <asp:TextBox ID="txtMaxWeight" runat="server" Text='<%# Bind("Max_Weight") %>'></asp:TextBox>
              </EditItemTemplate>
              <ItemTemplate>
                  <asp:Label ID="lbMaxWeight" runat="server" Text='<%#Bind("Max_Weight") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFMaxWeight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateField>
          
         <asp:TemplateField HeaderText="Min_Weight" SortExpression="Min_Weight">
              <EditItemTemplate>
                  <asp:TextBox ID="txtMinWeight" runat="server" Text='<%# Bind("Min_Weight") %>'></asp:TextBox>
              </EditItemTemplate>
              <ItemTemplate>
                  <asp:Label ID="lbMinWeight" runat="server" Text='<%#Bind("Min_Weight") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFMinWeight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Carton_Qty" SortExpression="Carton_Qty">
              <EditItemTemplate>
                  <asp:TextBox ID="txtCartonQty" runat="server" Text='<%# Bind("Carton_Qty") %>'></asp:TextBox>
              </EditItemTemplate>
              <ItemTemplate>
                  <asp:Label ID="lbCartonQty" runat="server" Text='<%#Bind("Carton_Qty") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFCartonQty" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="area" SortExpression="area">
          <EditItemTemplate>
              <asp:TextBox ID="txtArea" runat="server" Text='<%# Bind("area") %>'></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Label ID="lbArea" runat="server" Text='<%#Bind("area") %>'></asp:Label>
          </ItemTemplate>
          <FooterTemplate>
              <asp:TextBox ID="txtFArea" runat="server" Text=""></asp:TextBox>             
          </FooterTemplate>
      </asp:TemplateField> 
       <asp:CommandField HeaderText="编辑" ShowEditButton="True" />
       <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
      <asp:TemplateField >
          <EditItemTemplate>
              <asp:LinkButton ID="LinkButton1" runat="server" Text="更新" CommandName="ItemUpdate" CommandArgument=' <%# Container.DisplayIndex %>' /> 
              <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>  
          </EditItemTemplate>
          <ItemTemplate>
              <asp:LinkButton ID="LinkButton1" runat="server" Text="編輯" CommandName="ItemEdit" />
              <asp:LinkButton ID="LinkButton2" runat="server" Text="刪除" />
          </ItemTemplate>
          <FooterTemplate>
              <asp:LinkButton  ID="btnCommit" runat="server" Text="確定" CommandName="ItemSure"/>
              <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>
          </FooterTemplate>
      </asp:TemplateField> 
      
      </Columns> 
      <RowStyle BackColor="Cornsilk" ForeColor="#000066" />
      <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />              
      <PagerStyle  HorizontalAlign="Left"  />
      <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="DataGridFixedHeader" />
      <AlternatingRowStyle BackColor="WhiteSmoke" />
  </asp:GridView>--%>
   
  <asp:datagrid id="dgMeasure" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="30"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False"
		OnCancelCommand="dgMeasure_CancelCommand" OnUpdateCommand="dgMeasure_UpdateCommand" OnEditCommand="dgMeasure_EditCommand"
		OnItemDataBound="dgMeasure_ItemDataBound" OnPageIndexChanged="dgMeasure_PageIndexChanged" OnItemCommand="dgMeasure_ItemCommand">
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
          <asp:TemplateColumn HeaderText="Model" SortExpression="Model" ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lbModelID" runat="server" Text='<%# Bind("Model_ID") %>' ></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFModelID" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Country" SortExpression="Country"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCountry" runat="server" Text='<%# Bind("country_name") %>'  ></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFCountry" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Max_Weight" SortExpression="Max_Weight" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMaxWeight" runat="server" Text='<%#Bind("Max_Weight") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtMaxWeight" runat="server" Text='<%# Bind("Max_Weight") %>'></asp:TextBox>
              </EditItemTemplate>
              
              <FooterTemplate>
                  <asp:TextBox ID="txtFMaxWeight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Min_Weight" SortExpression="Min_Weight" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMinWeight" runat="server" Text='<%#Bind("Min_Weight") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtMinWeight" runat="server" Text='<%# Bind("Min_Weight") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFMinWeight" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Carton_Qty" SortExpression="Carton_Qty" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCartonQty" runat="server" Text='<%#Bind("Carton_Qty") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtCartonQty" runat="server" Text='<%# Bind("Carton_Qty") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFCartonQty" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="area" SortExpression="area" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbArea" runat="server" Text='<%#Bind("area") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtArea" runat="server" Text='<%# Bind("area") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFArea" runat="server" Text=""></asp:TextBox>             
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
