<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmRosPNMantenance.ascx.cs" Inherits="Boundary_wfrmRosPNMantenance" %>
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
	    <td Width="150"  align="right"  valign="bottom"> 
	       <asp:button id="btnSA" runat="server" Width="80px" text="查詢認證記錄" OnClick="btnSA_Click" Visible="false" ></asp:button></td> 	
        	
    </tr> 
</table>
<hr> 
<asp:Panel ID="panel1" runat="server" >
    <asp:Panel ID="panel2" runat="server" BorderColor="Gray" BorderStyle="Double" Visible="false" Width="300px">
            <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
                <tr>
                    <td colspan="3">
                        <asp:Label runat="server" ID="lb1" Text="條碼生成選項" ForeColor="red" Height="20px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ckb1" Text="不產生條碼" runat="server" TextAlign="Right" Checked="false" />                        
                    </td>
                    <td>
                        <asp:CheckBox ID="ckb2" Text="IMEI" runat="server" TextAlign="Right" Checked="false" />   
                    </td>
                    <td>
                        <asp:CheckBox ID="ckb3" Text="Picasso/SN" runat="server" TextAlign="Right" Checked="false" />   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ckb4" Text="重工工單" runat="server" TextAlign="Right" Checked="false" />   
                    </td>
                    <td>
                        <asp:CheckBox ID="ckb5" Text="MSN" runat="server" TextAlign="Right" Checked="false" />   
                    </td>
                    <td>
                        <asp:CheckBox ID="ckb6" Text="BT Address" runat="server" TextAlign="Right" Checked="false" />   
                    </td>                    
                </tr>
            </table>  
    </asp:Panel>
    <asp:datagrid id="dgPart" runat="server" Font-Size="10px"  PageSize="30"
		BackColor="White"  BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		Visible="false" OnCancelCommand="dgPart_CancelCommand" OnEditCommand="dgPart_EditCommand" 
		OnItemCommand="dgPart_ItemCommand" OnItemDataBound="dgPart_ItemDataBound" OnPageIndexChanged="dgPart_PageIndexChanged" OnUpdateCommand="dgPart_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="Part No" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbPartNo" runat="server" Text='<%# Bind("PPART") %>'></asp:Label>
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:TextBox ID="txtEPartNo" runat="server" Text='<%# Bind("PPART") %>'></asp:TextBox>
              </EditItemTemplate>  
              <FooterTemplate>
                  <asp:TextBox ID="txtFPartNo" runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Model_Type" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbModel_Type" runat="server" Text='<%# Bind("TYPE_CODE") %>'></asp:Label> 
              </ItemTemplate> 
              <EditItemTemplate>
                  <asp:DropDownList ID="ddlEModel_Type" runat="server"></asp:DropDownList>
              </EditItemTemplate>      
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFModel_Type" runat="server"></asp:DropDownList>     
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Customer_Item"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCust_Item" runat="server" Text='<%#Bind("PHONE_MODEL") %>'></asp:Label>
              </ItemTemplate>              
              <EditItemTemplate>
                  <asp:TextBox ID="txtECust_Item" runat="server" Text='<%#Bind("PHONE_MODEL") %>'></asp:TextBox>
              </EditItemTemplate> 
              <FooterTemplate>
                  <asp:TextBox ID="txtFCust_Item" runat="server" ></asp:TextBox>   
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="SA_NO"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSaNo" runat="server" Text='<%#Bind("SA_NO") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtSaNo" runat="server" Text='<%# Bind("SA_NO") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFSaNo" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Back_Num"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbBackNum" runat="server" Text='<%#Bind("BACK_NUM") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtBackNum" runat="server" Text='<%# Bind("BACK_NUM") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFBackNum" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="MFG_Country"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMfgCountry" runat="server" Text='<%#Bind("MFG_COUNTRY") %>'></asp:Label>
              </ItemTemplate>  
              <EditItemTemplate>
                  <asp:TextBox ID="txtMfgCountry" runat="server" Text='<%# Bind("MFG_COUNTRY") %>'></asp:TextBox>
              </EditItemTemplate>           
              <FooterTemplate>
                  <asp:TextBox ID="txtFMfgCountry" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="T_OPTI"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbTOpti" runat="server" Text='<%#Bind("T_OPTI") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtETOpti" runat="server" Text='<%#Bind("T_OPTI") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFTOpti"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>          
          <asp:TemplateColumn HeaderText="HW_Ver"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbHw" runat="server" Text='<%#Bind("HW_VER") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEHw" runat="server" Text='<%#Bind("HW_VER") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFHw"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="SW_Ver"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSw" runat="server" Text='<%#Bind("SW_VER") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtESw" runat="server" Text='<%#Bind("SW_VER") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFSw"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Picasso_DEF"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPicassoDef" runat="server" Text='<%#Bind("PICASSO_DEF") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEPicassoDef" runat="server" Text='<%#Bind("PICASSO_DEF") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFPicassoDef"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Qty/Carton"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbQty" runat="server" Text='<%#Bind("QTY") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEQty" runat="server" Text='<%#Bind("QTY") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFQty"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="MRP"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMrp" runat="server" Text='<%#Bind("MRP") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEMrp" runat="server" Text='<%#Bind("MRP") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFMrp"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>          
          <asp:TemplateColumn HeaderText="EAN"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbEan" runat="server" Text='<%#Bind("EAN") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEEan" runat="server" Text='<%#Bind("EAN") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFEan"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Market_Name"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMarketName" runat="server" Text='<%#Bind("MARKET_NAME") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEMarketName" runat="server" Text='<%#Bind("MARKET_NAME") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFMarketName"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="COLOR"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbColor" runat="server" Text='<%#Bind("COLOR") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEColor" runat="server" Text='<%#Bind("COLOR") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFColor"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="COMMODITY"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCommodity" runat="server" Text='<%#Bind("COMMODITY") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtECommodity" runat="server" Text='<%#Bind("COMMODITY") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFCommodity"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="LABEL_TYPE"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbLabelType" runat="server" Text='<%#Bind("LABEL_TYPE") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:DropDownList ID="ddlELabelType" runat="server"></asp:DropDownList>
              </EditItemTemplate>         
              <FooterTemplate>
                   <asp:DropDownList ID="ddlFLabelType" runat="server"></asp:DropDownList>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="R-DL_PATH"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbRdlPath" runat="server" Text='<%#Bind("RDL_PATH") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtERdlPath" runat="server" Text='<%#Bind("RDL_PATH") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFRdlPath"  runat="server"></asp:TextBox>
              </FooterTemplate> 
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="CONTENTS"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbContent" runat="server" Text='<%#Bind("CONTENT") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEContent" runat="server" Text='<%#Bind("CONTENT") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFContent"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="E2PREFFILE"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbE2pReffile" runat="server" Text='<%#Bind("E2PREFFILE") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtEE2pReffile" runat="server" Text='<%#Bind("E2PREFFILE") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFE2pReffile"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="CHECKBOX"  ItemStyle-HorizontalAlign="Center" Visible="False">
              <ItemTemplate>
                  <asp:Label ID="lbCheckbox" runat="server" Text='<%#Bind("CHECKBOX") %>'></asp:Label>
              </ItemTemplate>      
          </asp:TemplateColumn>          
          <asp:BoundColumn ReadOnly="true" DataField="UPDATED_BY" HeaderText="Last_updated Name"></asp:BoundColumn>
          <asp:BoundColumn ReadOnly="true" DataField="LAST_UPDATED" HeaderText="Last_updated Date"></asp:BoundColumn>
          <asp:EditCommandColumn HeaderText="Edit"   ButtonType="LinkButton" CancelText="取消" EditText="編輯" UpdateText="更新"  ItemStyle-ForeColor="blue" />
          <asp:TemplateColumn>
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
    </asp:datagrid>
    <asp:Panel ID="panel3" runat="server">
    <br /> 
    <asp:datagrid id="dgsa" runat="server" Font-Size="10px" BackColor="White"  BorderStyle="None" BorderWidth="1px"  Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		Visible="false" OnItemCommand="dgsa_ItemCommand" OnItemDataBound="dgsa_ItemDataBound" >
		<Columns>  
		   <asp:TemplateColumn> 
              <HeaderTemplate>
                  <asp:LinkButton ID="lkbAddItem1" runat="server" CommandName="AddItem">新增</asp:LinkButton>
              </HeaderTemplate>
              <ItemTemplate>
                  <asp:Label ID="lblID1" runat="server" ></asp:Label>         
              </ItemTemplate>
               <FooterTemplate>
                  <asp:LinkButton  ID="btnCommit" runat="server" Text="確定" CommandName="ItemSure"/>
                  <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>
              </FooterTemplate> 
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Part_No" ItemStyle-HorizontalAlign="Center"> 
              <ItemTemplate>
                  <asp:Label ID="lbPartNo" runat="server" Text='<%# Bind("PPART") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:Label ID="lbFPartNo" runat="server"></asp:Label> 
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Customer_Item" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCustomerItem" runat="server" Text='<%# Bind("PHONE_MODEL") %>'></asp:Label> 
              </ItemTemplate>       
              <FooterTemplate> 
                  <asp:TextBox ID="txtFCustomerItem"  runat="server" ></asp:TextBox>   
              </FooterTemplate>
          </asp:TemplateColumn>               
          <asp:TemplateColumn HeaderText="HW_Ver"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbHw" runat="server" Text='<%#Bind("HW_VER") %>'></asp:Label>
              </ItemTemplate>                 
              <FooterTemplate>
                  <asp:TextBox ID="txtFHw"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="SW_Ver"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSw" runat="server" Text='<%#Bind("SW_VER") %>'></asp:Label>
              </ItemTemplate>                        
              <EditItemTemplate>
                  <asp:TextBox ID="txtESw" runat="server" Text='<%#Bind("SW_VER") %>'></asp:TextBox>
              </EditItemTemplate>         
              <FooterTemplate>
                  <asp:TextBox ID="txtFSw"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Market_Name"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMarketName" runat="server" Text='<%#Bind("MAKET_NAME") %>'></asp:Label>
              </ItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFMarketName"  runat="server"></asp:TextBox>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="SA_Date"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSadate" runat="server" Text='<%#Bind("SA_DATE") %>'></asp:Label>
              </ItemTemplate>             
              <FooterTemplate> 
                  <asp:Label ID="lbFSadate"  runat="server"></asp:Label>
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="SA_Pass"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSapass" runat="server" Text='<%#Bind("SA_FLAG") %>'></asp:Label>
              </ItemTemplate>             
              <FooterTemplate> 
                  <asp:Label ID="lbFSapass"  runat="server"></asp:Label>
              </FooterTemplate>
          </asp:TemplateColumn>         
          <asp:TemplateColumn HeaderText="QTY"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbQty" runat="server" Text='<%#Bind("QTY") %>'></asp:Label>
              </ItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFQty"  runat="server"></asp:TextBox>
              </FooterTemplate> 
          </asp:TemplateColumn>
        </Columns>
	    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#cccc99"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<EditItemStyle BackColor="#99cccc" />
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="SteelBlue"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>  
    </asp:datagrid>     
    </asp:Panel>
</asp:Panel>