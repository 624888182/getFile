<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmHWVersionMantenance.ascx.cs" Inherits="Boundary_wfrmHWVersionMantenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
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
   <asp:datagrid id="dgHWver" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="30"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnCancelCommand="dgHWver_CancelCommand" 
		OnEditCommand="dgHWver_EditCommand" OnItemCommand="dgHWver_ItemCommand" OnItemDataBound="dgHWver_ItemDataBound" 
		OnPageIndexChanged="dgHWver_PageIndexChanged" OnUpdateCommand="dgHWver_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="Model" ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lbModel" runat="server" Text='<%# Bind("Model") %>' ></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEModel" runat="server" Text='<%# Bind("Model") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtModel" runat="server" Text='<%# Bind("Model") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFModel" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="HW Ver" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbHW" runat="server" Text='<%# Bind("HW_REV") %>'  ></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtHW" runat="server" Text='<%# Bind("HW_REV") %>'></asp:TextBox>
              </EditItemTemplate>               
              <FooterTemplate>
                  <asp:TextBox ID="txtFHW" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Up PN" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbUp" runat="server" Text='<%#Bind("UP_PN") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>              
                  <asp:Label ID="lbEUp" runat="server" Text='<%# Bind("UP_PN") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtUp" runat="server" Text='<%# Bind("UP_PN") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFUp" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Low PN" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLow" runat="server" Text='<%#Bind("LOWER_PN") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbELow" runat="server" Text='<%# Bind("LOWER_PN") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtLow" runat="server" Text='<%# Bind("LOWER_PN") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFLow" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Daughter PN"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbDaughter" runat="server" Text='<%#Bind("DAUGHTER_PN") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEDaughter" runat="server" Text='<%# Bind("DAUGHTER_PN") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtDaughter" runat="server" Text='<%# Bind("DAUGHTER_PN") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFDaughter" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="Last Updated By" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLastBY" runat="server" Text='<%#Bind("LAST_UPDATED_BY") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtLastBY" runat="server" Text='<%# Bind("LAST_UPDATED_BY") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFLastBY" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="Last Update Date" SortExpression="area" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLastDT" runat="server" Text='<%#Bind("LAST_UPDATE_DATE") %>'></asp:Label>
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