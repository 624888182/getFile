<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmShipOrderMaintenance.ascx.cs" Inherits="Boundary_wfrmShipOrderMaintenance" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" rowSpan="2"></td>
		<td Width="100px"> <asp:label id="lblShoporder" runat="server" text="Shop Order"></asp:label></td>
		<td style="WIDTH: 60px">
            <asp:TextBox runat="server" ID="txtShoporder"></asp:TextBox>
        </td>
        <td Width="150px"  align="right"  valign="bottom"> 
			<asp:button id="btnQuery" runat="server" Width="80px" OnClick="btnQuery_Click"  ></asp:button></td> 	
    </tr> 
</table>
<hr>
   <asp:Label ID="lbcount" runat="server" ForeColor="red"></asp:Label> 
   <asp:datagrid id="dgShop" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="30"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" 
		OnCancelCommand="dgShop_CancelCommand" OnEditCommand="dgShop_EditCommand" 
	    OnPageIndexChanged="dgShop_PageIndexChanged" OnUpdateCommand="dgShop_UpdateCommand" >
		<Columns>  
          <asp:TemplateColumn HeaderText="ALLOWABLE_XCVR_MODEL" ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lballowxcvrmodel" runat="server" Text='<%# Bind("ALLOWABLE_XCVR_MODEL") %>' ></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEallowxcvrmodel" runat="server" Text='<%# Bind("ALLOWABLE_XCVR_MODEL") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEallowxcvrmodel" runat="server" Text='<%# Bind("ALLOWABLE_XCVR_MODEL") %>'></asp:TextBox>
              </EditItemTemplate>   
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="ADDON_ID" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbaddonid" runat="server" Text='<%# Bind("ADDON_ID") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEaddonid" runat="server" Text='<%# Bind("ADDON_ID") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEaddonid" runat="server" Text='<%# Bind("ADDON_ID") %>'></asp:TextBox>
              </EditItemTemplate>        
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="CONFIG_ID" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbconfigid" runat="server" Text='<%#Bind("CONFIG_ID") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>              
                  <asp:Label ID="lbEconfigid" runat="server" Text='<%# Bind("CONFIG_ID") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEconfigid" runat="server" Text='<%# Bind("CONFIG_ID") %>'></asp:TextBox>
              </EditItemTemplate>         
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="DEAKTIVATE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdeaktivate" runat="server" Text='<%#Bind("DEAKTIVATE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEdeaktivate" runat="server" Text='<%# Bind("DEAKTIVATE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEdeaktivate" runat="server" Text='<%# Bind("DEAKTIVATE") %>'></asp:TextBox>
              </EditItemTemplate>       
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="FAMILY"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfamily" runat="server" Text='<%#Bind("FAMILY") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfamily" runat="server" Text='<%# Bind("FAMILY") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfamily" runat="server" Text='<%# Bind("FAMILY") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="MULTIUP" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbmultiup" runat="server" Text='<%#Bind("MULTIUP") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEmultiup" runat="server" Text='<%# Bind("MULTIUP") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEmultiup" runat="server" Text='<%# Bind("MULTIUP") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>   
         <asp:TemplateColumn HeaderText="NEXTTEST_VERSION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbnexttestversion" runat="server" Text='<%#Bind("NEXTTEST_VERSION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEnexttestversion" runat="server" Text='<%# Bind("NEXTTEST_VERSION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEnexttestversion" runat="server" Text='<%# Bind("NEXTTEST_VERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="RECIPE_DELETE_CHECK" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbrecipedeletecheck" runat="server" Text='<%#Bind("RECIPE_DELETE_CHECK") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbErecipedeletecheck" runat="server" Text='<%# Bind("RECIPE_DELETE_CHECK") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtErecipedeletecheck" runat="server" Text='<%# Bind("RECIPE_DELETE_CHECK") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="REL_VERSION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbrelversion" runat="server" Text='<%#Bind("REL_VERSION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbErelversion" runat="server" Text='<%# Bind("REL_VERSION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtErelversion" runat="server" Text='<%# Bind("REL_VERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="PROD_UNIT" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbprodunit" runat="server" Text='<%#Bind("PROD_UNIT") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEprodunit" runat="server" Text='<%# Bind("PROD_UNIT") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEprodunit" runat="server" Text='<%# Bind("PROD_UNIT") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="LV_ULMA_TYPE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblvulmatype" runat="server" Text='<%#Bind("LV_ULMA_TYPE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbElvulmatype" runat="server" Text='<%# Bind("LV_ULMA_TYPE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtElvulmatype" runat="server" Text='<%# Bind("LV_ULMA_TYPE") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FLASHFILE_NAME" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbflashfilename" runat="server" Text='<%#Bind("FLASHFILE_NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEflashfilename" runat="server" Text='<%# Bind("FLASHFILE_NAME") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEflashfilename" runat="server" Text='<%# Bind("FLASHFILE_NAME") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FLASHFILE_PATH" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbflashfilepath" runat="server" Text='<%#Bind("FLASHFILE_PATH") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEflashfilepath" runat="server" Text='<%# Bind("FLASHFILE_PATH") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEflashfilepath" runat="server" Text='<%# Bind("FLASHFILE_PATH") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FLEXFILE_NAME" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbflexfilename" runat="server" Text='<%#Bind("FLEXFILE_NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEflexfilename" runat="server" Text='<%# Bind("FLEXFILE_NAME") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEflexfilename" runat="server" Text='<%# Bind("FLEXFILE_NAME") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FLEXFILE_PATH" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbflexfilepath" runat="server" Text='<%#Bind("FLEXFILE_PATH") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEflexfilepath" runat="server" Text='<%# Bind("FLEXFILE_PATH") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEflexfilepath" runat="server" Text='<%# Bind("FLEXFILE_PATH") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FLEXFILE_VERSION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbflexfileversion" runat="server" Text='<%#Bind("FLEXFILE_VERSION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEflexfileversion" runat="server" Text='<%# Bind("FLEXFILE_VERSION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEflexfileversion" runat="server" Text='<%# Bind("FLEXFILE_VERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="LANGUAGE_PACK" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lblanguagepack" runat="server" Text='<%#Bind("LANGUAGE_PACK") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbElanguagepack" runat="server" Text='<%# Bind("LANGUAGE_PACK") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtElanguagepack" runat="server" Text='<%# Bind("LANGUAGE_PACK") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="ORDER_NUMBER" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbordernumber" runat="server" Text='<%#Bind("ORDER_NUMBER") %>'></asp:Label>
              </ItemTemplate>
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="PRODUCT_FAMILY" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbproductfamily" runat="server" Text='<%#Bind("PRODUCT_FAMILY") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEproductfamily" runat="server" Text='<%# Bind("PRODUCT_FAMILY") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEproductfamily" runat="server" Text='<%# Bind("PRODUCT_FAMILY") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="PROTOCOL" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbprotocol" runat="server" Text='<%#Bind("PROTOCOL") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEprotocol" runat="server" Text='<%# Bind("PROTOCOL") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEprotocol" runat="server" Text='<%# Bind("PROTOCOL") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="QUANTITY" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbquantity" runat="server" Text='<%#Bind("QUANTITY") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEquantity" runat="server" Text='<%# Bind("QUANTITY") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEquantity" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>   
         <asp:TemplateColumn HeaderText="QUANTITY_PROCESSED" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbquantityprocessed" runat="server" Text='<%#Bind("QUANTITY_PROCESSED") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEquantityprocessed" runat="server" Text='<%# Bind("QUANTITY_PROCESSED") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEquantityprocessed" runat="server" Text='<%# Bind("QUANTITY_PROCESSED") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="RECIPE_NAME" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbrecipename" runat="server" Text='<%#Bind("RECIPE_NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbErecipename" runat="server" Text='<%# Bind("RECIPE_NAME") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtErecipename" runat="server" Text='<%# Bind("RECIPE_NAME") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="RECIPE_REVISION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbreciperevision" runat="server" Text='<%#Bind("RECIPE_REVISION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEreciperevision" runat="server" Text='<%# Bind("RECIPE_REVISION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEreciperevision" runat="server" Text='<%# Bind("RECIPE_REVISION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="SITE_CODE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbsitecode" runat="server" Text='<%#Bind("SITE_CODE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEsitecode" runat="server" Text='<%# Bind("SITE_CODE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEsitecode" runat="server" Text='<%# Bind("SITE_CODE") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="SWVERSION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbswversion" runat="server" Text='<%#Bind("SWVERSION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEswversion" runat="server" Text='<%# Bind("SWVERSION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEswversion" runat="server" Text='<%# Bind("SWVERSION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="XCVR_MODEL" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbxcvrmodel" runat="server" Text='<%#Bind("XCVR_MODEL") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbExcvrmodel" runat="server" Text='<%# Bind("XCVR_MODEL") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtExcvrmodel" runat="server" Text='<%# Bind("XCVR_MODEL") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="SALES_MODEL" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbsalesmodel" runat="server" Text='<%#Bind("SALES_MODEL") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEsalesmodel" runat="server" Text='<%# Bind("SALES_MODEL") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEsalesmodel" runat="server" Text='<%# Bind("SALES_MODEL") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="SALES_MODEL_REVISION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbsalesmodelrevision" runat="server" Text='<%#Bind("SALES_MODEL_REVISION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEsalesmodelrevision" runat="server" Text='<%# Bind("SALES_MODEL_REVISION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEsalesmodelrevision" runat="server" Text='<%# Bind("SALES_MODEL_REVISION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TOPTION" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtoption" runat="server" Text='<%#Bind("TOPTION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtoption" runat="server" Text='<%# Bind("TOPTION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtoption" runat="server" Text='<%# Bind("TOPTION") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TA_HEADER" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtaheader" runat="server" Text='<%#Bind("TA_HEADER") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtaheader" runat="server" Text='<%# Bind("TA_HEADER") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtaheader" runat="server" Text='<%# Bind("TA_HEADER") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory1" runat="server" Text='<%#Bind("CATEGORY1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory1" runat="server" Text='<%# Bind("CATEGORY1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory1" runat="server" Text='<%# Bind("CATEGORY1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype1" runat="server" Text='<%#Bind("TYPE1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype1" runat="server" Text='<%# Bind("TYPE1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype1" runat="server" Text='<%# Bind("TYPE1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath1" runat="server" Text='<%#Bind("FILE_PATH1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath1" runat="server" Text='<%# Bind("FILE_PATH1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath1" runat="server" Text='<%# Bind("FILE_PATH1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename1" runat="server" Text='<%#Bind("FILE_NAME1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename1" runat="server" Text='<%# Bind("FILE_NAME1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename1" runat="server" Text='<%# Bind("FILE_NAME1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>  
         <asp:TemplateColumn HeaderText="VERSION1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion1" runat="server" Text='<%#Bind("VERSION1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion1" runat="server" Text='<%# Bind("VERSION1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion1" runat="server" Text='<%# Bind("VERSION1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT1" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment1" runat="server" Text='<%#Bind("COMMENT1") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment1" runat="server" Text='<%# Bind("COMMENT1") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment1" runat="server" Text='<%# Bind("COMMENT1") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory2" runat="server" Text='<%#Bind("CATEGORY2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory2" runat="server" Text='<%# Bind("CATEGORY2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory2" runat="server" Text='<%# Bind("CATEGORY2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype2" runat="server" Text='<%#Bind("TYPE2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype2" runat="server" Text='<%# Bind("TYPE2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype2" runat="server" Text='<%# Bind("TYPE2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath2" runat="server" Text='<%#Bind("FILE_PATH2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath2" runat="server" Text='<%# Bind("FILE_PATH2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath2" runat="server" Text='<%# Bind("FILE_PATH2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename2" runat="server" Text='<%#Bind("FILE_NAME2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename2" runat="server" Text='<%# Bind("FILE_NAME2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename2" runat="server" Text='<%# Bind("FILE_NAME2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion2" runat="server" Text='<%#Bind("VERSION2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion2" runat="server" Text='<%# Bind("VERSION2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion2" runat="server" Text='<%# Bind("VERSION2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT2" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment2" runat="server" Text='<%#Bind("COMMENT2") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment2" runat="server" Text='<%# Bind("COMMENT2") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment2" runat="server" Text='<%# Bind("COMMENT2") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="IMPORT_DATE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbimportdate" runat="server" Text='<%#Bind("IMPORT_DATE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEimportdate" runat="server" Text='<%# Bind("IMPORT_DATE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEimportdate" runat="server" Text='<%# Bind("IMPORT_DATE") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory3" runat="server" Text='<%#Bind("CATEGORY3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory3" runat="server" Text='<%# Bind("CATEGORY3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory3" runat="server" Text='<%# Bind("CATEGORY3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype3" runat="server" Text='<%#Bind("TYPE3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype3" runat="server" Text='<%# Bind("TYPE3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype3" runat="server" Text='<%# Bind("TYPE3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath3" runat="server" Text='<%#Bind("FILE_PATH3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath3" runat="server" Text='<%# Bind("FILE_PATH3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath3" runat="server" Text='<%# Bind("FILE_PATH3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion3" runat="server" Text='<%#Bind("VERSION3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion3" runat="server" Text='<%# Bind("VERSION3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion3" runat="server" Text='<%# Bind("VERSION3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment3" runat="server" Text='<%#Bind("COMMENT3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment3" runat="server" Text='<%# Bind("COMMENT3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment3" runat="server" Text='<%# Bind("COMMENT3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME3" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename3" runat="server" Text='<%#Bind("FILE_NAME3") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename3" runat="server" Text='<%# Bind("FILE_NAME3") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename3" runat="server" Text='<%# Bind("FILE_NAME3") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="AKEY1_TYPE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbakey1type" runat="server" Text='<%#Bind("AKEY1_TYPE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEakey1type" runat="server" Text='<%# Bind("AKEY1_TYPE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEakey1type" runat="server" Text='<%# Bind("AKEY1_TYPE") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="AKEY2_TYPE" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbakey2type" runat="server" Text='<%#Bind("AKEY2_TYPE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEakey2type" runat="server" Text='<%# Bind("AKEY2_TYPE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEakey2type" runat="server" Text='<%# Bind("AKEY2_TYPE") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory4" runat="server" Text='<%#Bind("CATEGORY4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory4" runat="server" Text='<%# Bind("CATEGORY4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory4" runat="server" Text='<%# Bind("CATEGORY4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype4" runat="server" Text='<%#Bind("TYPE4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype4" runat="server" Text='<%# Bind("TYPE4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype4" runat="server" Text='<%# Bind("TYPE4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename4" runat="server" Text='<%#Bind("FILE_NAME4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename4" runat="server" Text='<%# Bind("FILE_NAME4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename4" runat="server" Text='<%# Bind("FILE_NAME4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath4" runat="server" Text='<%#Bind("FILE_PATH4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath4" runat="server" Text='<%# Bind("FILE_PATH4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath4" runat="server" Text='<%# Bind("FILE_PATH4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion4" runat="server" Text='<%#Bind("VERSION4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion4" runat="server" Text='<%# Bind("VERSION4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion4" runat="server" Text='<%# Bind("VERSION4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT4" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment4" runat="server" Text='<%#Bind("COMMENT4") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment4" runat="server" Text='<%# Bind("COMMENT4") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment4" runat="server" Text='<%# Bind("COMMENT4") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory5" runat="server" Text='<%#Bind("CATEGORY5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory5" runat="server" Text='<%# Bind("CATEGORY5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory5" runat="server" Text='<%# Bind("CATEGORY5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype5" runat="server" Text='<%#Bind("TYPE5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype5" runat="server" Text='<%# Bind("TYPE5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype5" runat="server" Text='<%# Bind("TYPE5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename5" runat="server" Text='<%#Bind("FILE_NAME5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename5" runat="server" Text='<%# Bind("FILE_NAME5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename5" runat="server" Text='<%# Bind("FILE_NAME5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath5" runat="server" Text='<%#Bind("FILE_PATH5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath5" runat="server" Text='<%# Bind("FILE_PATH5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath5" runat="server" Text='<%# Bind("FILE_PATH5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion5" runat="server" Text='<%#Bind("VERSION5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion5" runat="server" Text='<%# Bind("VERSION5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion5" runat="server" Text='<%# Bind("VERSION5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT5" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment5" runat="server" Text='<%#Bind("COMMENT5") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment5" runat="server" Text='<%# Bind("COMMENT5") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment5" runat="server" Text='<%# Bind("COMMENT5") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory6" runat="server" Text='<%#Bind("CATEGORY6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory6" runat="server" Text='<%# Bind("CATEGORY6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory6" runat="server" Text='<%# Bind("CATEGORY6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype6" runat="server" Text='<%#Bind("TYPE6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype6" runat="server" Text='<%# Bind("TYPE6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype6" runat="server" Text='<%# Bind("TYPE6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename6" runat="server" Text='<%#Bind("FILE_NAME6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename6" runat="server" Text='<%# Bind("FILE_NAME6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename6" runat="server" Text='<%# Bind("FILE_NAME6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath6" runat="server" Text='<%#Bind("FILE_PATH6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbFILE_PATH6" runat="server" Text='<%# Bind("FILE_PATH6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtFILE_PATH6" runat="server" Text='<%# Bind("FILE_PATH6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion6" runat="server" Text='<%#Bind("VERSION6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion6" runat="server" Text='<%# Bind("VERSION6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion6" runat="server" Text='<%# Bind("VERSION6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT6" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment6" runat="server" Text='<%#Bind("COMMENT6") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment6" runat="server" Text='<%# Bind("COMMENT6") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment6" runat="server" Text='<%# Bind("COMMENT6") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory7" runat="server" Text='<%#Bind("CATEGORY7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory7" runat="server" Text='<%# Bind("CATEGORY7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory7" runat="server" Text='<%# Bind("CATEGORY7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype7" runat="server" Text='<%#Bind("TYPE7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype7" runat="server" Text='<%# Bind("TYPE7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype7" runat="server" Text='<%# Bind("TYPE7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename7" runat="server" Text='<%#Bind("FILE_NAME7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename7" runat="server" Text='<%# Bind("FILE_NAME7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename7" runat="server" Text='<%# Bind("FILE_NAME7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath7" runat="server" Text='<%#Bind("FILE_PATH7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath7" runat="server" Text='<%# Bind("FILE_PATH7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath7" runat="server" Text='<%# Bind("FILE_PATH7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion7" runat="server" Text='<%#Bind("VERSION7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion7" runat="server" Text='<%# Bind("VERSION7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion7" runat="server" Text='<%# Bind("VERSION7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT7" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment7" runat="server" Text='<%#Bind("COMMENT7") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment7" runat="server" Text='<%# Bind("COMMENT7") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment7" runat="server" Text='<%# Bind("COMMENT7") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory8" runat="server" Text='<%#Bind("CATEGORY8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory8" runat="server" Text='<%# Bind("CATEGORY8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory8" runat="server" Text='<%# Bind("CATEGORY8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype8" runat="server" Text='<%#Bind("TYPE8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype8" runat="server" Text='<%# Bind("TYPE8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype8" runat="server" Text='<%# Bind("TYPE8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename8" runat="server" Text='<%#Bind("FILE_NAME8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename8" runat="server" Text='<%# Bind("FILE_NAME8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename8" runat="server" Text='<%# Bind("FILE_NAME8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath8" runat="server" Text='<%#Bind("FILE_PATH8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath8" runat="server" Text='<%# Bind("FILE_PATH8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath8" runat="server" Text='<%# Bind("FILE_PATH8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion8" runat="server" Text='<%#Bind("VERSION8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion8" runat="server" Text='<%# Bind("VERSION8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion8" runat="server" Text='<%# Bind("VERSION8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT8" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment8" runat="server" Text='<%#Bind("COMMENT8") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment8" runat="server" Text='<%# Bind("COMMENT8") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment8" runat="server" Text='<%# Bind("COMMENT8") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory9" runat="server" Text='<%#Bind("CATEGORY9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory9" runat="server" Text='<%# Bind("CATEGORY9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory9" runat="server" Text='<%# Bind("CATEGORY9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype9" runat="server" Text='<%#Bind("TYPE9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype9" runat="server" Text='<%# Bind("TYPE9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype9" runat="server" Text='<%# Bind("TYPE9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename9" runat="server" Text='<%#Bind("FILE_NAME9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename9" runat="server" Text='<%# Bind("FILE_NAME9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename9" runat="server" Text='<%# Bind("FILE_NAME9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>  
         <asp:TemplateColumn HeaderText="FILE_PATH9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath9" runat="server" Text='<%#Bind("FILE_PATH9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath9" runat="server" Text='<%# Bind("FILE_PATH9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath9" runat="server" Text='<%# Bind("FILE_PATH9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion9" runat="server" Text='<%#Bind("VERSION9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion9" runat="server" Text='<%# Bind("VERSION9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion9" runat="server" Text='<%# Bind("VERSION9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT9" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment9" runat="server" Text='<%#Bind("COMMENT9") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment9" runat="server" Text='<%# Bind("COMMENT9") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment9" runat="server" Text='<%# Bind("COMMENT9") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory10" runat="server" Text='<%#Bind("CATEGORY10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory10" runat="server" Text='<%# Bind("CATEGORY10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory10" runat="server" Text='<%# Bind("CATEGORY10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype10" runat="server" Text='<%#Bind("TYPE10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype10" runat="server" Text='<%# Bind("TYPE10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype10" runat="server" Text='<%# Bind("TYPE10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename10" runat="server" Text='<%#Bind("FILE_NAME10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename10" runat="server" Text='<%# Bind("FILE_NAME10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename10" runat="server" Text='<%# Bind("FILE_NAME10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath10" runat="server" Text='<%#Bind("FILE_PATH10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath10" runat="server" Text='<%# Bind("FILE_PATH10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath10" runat="server" Text='<%# Bind("FILE_PATH10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion10" runat="server" Text='<%#Bind("VERSION10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion10" runat="server" Text='<%# Bind("VERSION10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion10" runat="server" Text='<%# Bind("VERSION10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT10" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment10" runat="server" Text='<%#Bind("COMMENT10") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment10" runat="server" Text='<%# Bind("COMMENT10") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment10" runat="server" Text='<%# Bind("COMMENT10") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory11" runat="server" Text='<%#Bind("CATEGORY11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory11" runat="server" Text='<%# Bind("CATEGORY11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory11" runat="server" Text='<%# Bind("CATEGORY11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype11" runat="server" Text='<%#Bind("TYPE11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype11" runat="server" Text='<%# Bind("TYPE11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype11" runat="server" Text='<%# Bind("TYPE11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename11" runat="server" Text='<%#Bind("FILE_NAME11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename11" runat="server" Text='<%# Bind("FILE_NAME11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename11" runat="server" Text='<%# Bind("FILE_NAME11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath11" runat="server" Text='<%#Bind("FILE_PATH11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilepath11" runat="server" Text='<%# Bind("FILE_PATH11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath11" runat="server" Text='<%# Bind("FILE_PATH11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion11" runat="server" Text='<%#Bind("VERSION11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion11" runat="server" Text='<%# Bind("VERSION11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion11" runat="server" Text='<%# Bind("VERSION11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT11" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment11" runat="server" Text='<%#Bind("COMMENT11") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment11" runat="server" Text='<%# Bind("COMMENT11") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment11" runat="server" Text='<%# Bind("COMMENT11") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="CATEGORY12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcategory12" runat="server" Text='<%#Bind("CATEGORY12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcategory12" runat="server" Text='<%# Bind("CATEGORY12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcategory12" runat="server" Text='<%# Bind("CATEGORY12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="TYPE12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbtype12" runat="server" Text='<%#Bind("TYPE12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEtype12" runat="server" Text='<%# Bind("TYPE12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEtype12" runat="server" Text='<%# Bind("TYPE12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_NAME12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilename12" runat="server" Text='<%#Bind("FILE_NAME12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfilename12" runat="server" Text='<%# Bind("FILE_NAME12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilename12" runat="server" Text='<%# Bind("FILE_NAME12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="FILE_PATH12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfilepath12" runat="server" Text='<%#Bind("FILE_PATH12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEEfilepath12" runat="server" Text='<%# Bind("FILE_PATH12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEfilepath12" runat="server" Text='<%# Bind("FILE_PATH12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="VERSION12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbversion12" runat="server" Text='<%#Bind("VERSION12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEversion12" runat="server" Text='<%# Bind("VERSION12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEversion12" runat="server" Text='<%# Bind("VERSION12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn> 
         <asp:TemplateColumn HeaderText="COMMENT12" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcomment12" runat="server" Text='<%#Bind("COMMENT12") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcomment12" runat="server" Text='<%# Bind("COMMENT12") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcomment12" runat="server" Text='<%# Bind("COMMENT12") %>'></asp:TextBox>
              </EditItemTemplate> 
         </asp:TemplateColumn>  
         <asp:EditCommandColumn HeaderText="Edit"  ButtonType="LinkButton" CancelText="取消" EditText="編輯" UpdateText="更新"  ItemStyle-ForeColor="blue" />      
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