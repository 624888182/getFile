<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmErrorCodeMaintenance.ascx.cs" Inherits="Boundary_wfrmErrorCodeMaintenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lblerrorcode" runat="server" text="Error Code"></asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txterrorcode"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click"  ></asp:button></td> 	
    </tr> 
</table>
<hr> 
   <asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
   <asp:datagrid id="dgError" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="30"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnItemCommand="dgError_ItemCommand" OnItemDataBound="dgError_ItemDataBound">
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
          <asp:TemplateColumn HeaderText="DEFECT CODE" ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lbdefectcode" runat="server" Text='<%# Bind("DEFECT_CODE") %>' ></asp:Label>
              </ItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFdefectcode" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'  ></asp:Label>
              </ItemTemplate>               
              <FooterTemplate>
                  <asp:TextBox ID="txtFdescription" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="USED" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbused" runat="server" Text='<%#Bind("USED") %>'></asp:Label>
              </ItemTemplate>               
              <FooterTemplate>   
                  <asp:DropDownList ID="ddlFused" runat="server" Width="60px">
                      <asp:ListItem Text="S" Value="S" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="A" Value="A"></asp:ListItem>
                      <asp:ListItem Text="P" Value="P"></asp:ListItem>
                  </asp:DropDownList>         
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="FACTORY AREA" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfactoryarea" runat="server" Text='<%#Bind("FACTORY_AREA") %>'></asp:Label>
              </ItemTemplate>             
              <FooterTemplate>
                  <asp:TextBox ID="txtFfactoryarea" runat="server" Text=""></asp:TextBox>          
              </FooterTemplate>
          </asp:TemplateColumn>
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
		<FooterStyle ForeColor="#000066" BackColor="White" HorizontalAlign="Center"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
</asp:datagrid>