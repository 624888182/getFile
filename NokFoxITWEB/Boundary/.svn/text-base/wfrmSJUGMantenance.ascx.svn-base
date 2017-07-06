<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmSJUGMantenance.ascx.cs" Inherits="Boundary_wfrmSJUGMantenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50"></td>
		<td Width="100px"> <asp:label id="lblPart" runat="server" ></asp:label></td>
		<td style="WIDTH: 150px">
            <asp:TextBox ID="txtPart" runat="server"></asp:TextBox>
        </td>     
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click" ></asp:button></td> 	   
	</tr> 
</table>
<hr> 
<asp:Panel ID="panel1" runat="server" >
    <asp:datagrid id="dgsjug" runat="server" Font-Size="10px"  PageSize="30"
		BackColor="White"  BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		Visible="false" OnEditCommand="dgsjug_EditCommand" OnItemCommand="dgsjug_ItemCommand" 
		OnItemDataBound="dgsjug_ItemDataBound" OnPageIndexChanged="dgsjug_PageIndexChanged" 
		OnUpdateCommand="dgsjug_UpdateCommand" OnCancelCommand="dgsjug_CancelCommand">
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
          <asp:TemplateColumn HeaderText="S_PartNo" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbSPartNo" runat="server" Text='<%# Bind("SPART") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtESPartNo" runat="server" Text='<%# Bind("SPART") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFSPartNo" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="A_PartNo" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbAPartNo" runat="server" Text='<%# Bind("APART") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEAPartNo" runat="server" Text='<%# Bind("APART") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFAPartNo" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>          
          <asp:TemplateColumn HeaderText="P_PartNo" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbPPartNo" runat="server" Text='<%# Bind("PPART") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEPPartNo" runat="server" Text='<%# Bind("PPART") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFPPartNo" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>          
          <asp:TemplateColumn HeaderText="SJUG" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbSJUG" runat="server" Text='<%# Bind("SJUG") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtESJUG" runat="server" Text='<%# Bind("SJUG") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFSJUG" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Color" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbColor" runat="server" Text='<%# Bind("COLOR") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEColor" runat="server" Text='<%# Bind("COLOR") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFColor" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Customer" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbCustomer" runat="server" Text='<%# Bind("CUSTOMER") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtECustomer" runat="server" Text='<%# Bind("CUSTOMER") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFCustomer" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="PID SufFix" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbPidsuffix" runat="server" Text='<%# Bind("ASSGIN") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEPidsuffix" runat="server" Text='<%# Bind("ASSGIN") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFPidsuffix" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>  
          <asp:TemplateColumn HeaderText="Sw_Version"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSw" runat="server" Text='<%#Bind("SW_VERSION") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtESw" runat="server" Text='<%#Bind("SW_VERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFSw" runat="server" ></asp:TextBox>   
              </FooterTemplate>
          </asp:TemplateColumn> 
          <asp:TemplateColumn HeaderText="Flex_Version"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbFlex" runat="server" Text='<%#Bind("FLEX_VERSION") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtEFlex" runat="server" Text='<%#Bind("FLEX_VERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFFlex" runat="server" ></asp:TextBox>   
              </FooterTemplate>
          </asp:TemplateColumn> 
          <asp:TemplateColumn HeaderText="SequencesID"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSequencesID" runat="server" Text='<%#Bind("SEQUENCES_ID") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtESequencesID" runat="server" Text='<%#Bind("SEQUENCES_ID") %>'></asp:TextBox>
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFSequencesID" runat="server" ></asp:TextBox>   
              </FooterTemplate>
          </asp:TemplateColumn> 
          <asp:EditCommandColumn HeaderText="Edit"   ButtonType="LinkButton" CancelText="取消" EditText="編輯" UpdateText="更新"  ItemStyle-ForeColor="blue" />
          <asp:TemplateColumn Visible="false">
               <HeaderTemplate>
                      <asp:Label ID="lbldel" runat="server" text="Delete"></asp:Label>
                  </HeaderTemplate>
                  <ItemTemplate>
                      <asp:LinkButton  ID="btnDelete" runat="server" Text="刪除" CommandName="ItemDelete" ForeColor="red"/> 
                  </ItemTemplate>
           </asp:TemplateColumn>
        </Columns>
	    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#cccc99"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<EditItemStyle BackColor="#99cccc" />
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
    </asp:datagrid></asp:Panel> 