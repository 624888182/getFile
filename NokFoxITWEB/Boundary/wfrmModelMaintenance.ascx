<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmModelMaintenance.ascx.cs" Inherits="Boundary_wfrmModelMaintenance" %>
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
    <asp:datagrid id="dgModel" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnCancelCommand="dgModel_CancelCommand" OnEditCommand="dgModel_EditCommand" OnItemCommand="dgModel_ItemCommand" OnItemDataBound="dgModel_ItemDataBound" OnPageIndexChanged="dgModel_PageIndexChanged" OnUpdateCommand="dgModel_UpdateCommand"  >
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
          <asp:TemplateColumn HeaderText="ID"  ItemStyle-HorizontalAlign="Center" > 
              <ItemTemplate>
                  <asp:Label ID="lbID" runat="server" Text='<%# Bind("ID") %>' ></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFID" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Model" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbModel" runat="server" Text='<%# Bind("MODEL") %>'  ></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFModel" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Description"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdescription" runat="server" Text='<%#Bind("DESCRIPTION") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEdescription" runat="server" Text='<%#Bind("DESCRIPTION") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEdescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFdescription" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="Customer Type" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcustomertype" runat="server" Text='<%#Bind("CUSTOMER_TYPE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcustomertype" runat="server" Text='<%#Bind("CUSTOMER_TYPE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcustomertype" runat="server" Text='<%# Bind("CUSTOMER_TYPE") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFcustomertype" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="New Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbnewflag" runat="server" Text='<%#Bind("NEW_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEnewflag" runat="server" Text='<%#Bind("NEW_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEnewflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFnewflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList>          
              </FooterTemplate>
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="Gift Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbgiftflag" runat="server" Text='<%#Bind("GIFT_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEgiftflag" runat="server" Text='<%#Bind("GIFT_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEgiftflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFgiftflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                  </asp:DropDownList> 
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Sku Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbskuflag" runat="server" Text='<%#Bind("SKU_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEskuflag" runat="server" Text='<%#Bind("SKU_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEskuflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFskuflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Carton Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcartonflag" runat="server" Text='<%#Bind("CARTON_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcartonflag" runat="server" Text='<%#Bind("CARTON_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEcartonflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFcartonflag" runat="server">
                      <asp:ListItem Text="Y" Value="Y" ></asp:ListItem>
                      <asp:ListItem Text="N" Value="N" Selected="True"></asp:ListItem>
                  </asp:DropDownList>         
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="DB User" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdbuser" runat="server" Text='<%#Bind("DB_USER") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEdbuser" runat="server" Text='<%#Bind("DB_USER") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEdbuser" runat="server" Text='<%# Bind("DB_USER") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFdbuser" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Abled" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbabled" runat="server" Text='<%#Bind("ABLED") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEabled" runat="server" Text='<%#Bind("ABLED") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEabled" runat="server">
                      <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFabled" runat="server">
                      <asp:ListItem Text="Y" Value="Y" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="N" Value="N"></asp:ListItem>
                  </asp:DropDownList>              
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Mass" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbmass" runat="server" Text='<%#Bind("MASS") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEmass" runat="server" Text='<%#Bind("MASS") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEmass" runat="server" Text='<%# Bind("MASS") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFmass" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>          
        <asp:TemplateColumn HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcustomername" runat="server" Text='<%#Bind("CUSTOMER_NAME") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcustomername" runat="server" Text='<%#Bind("CUSTOMER_NAME") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEcustomername" runat="server" Text='<%# Bind("CUSTOMER_NAME") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFcustomername" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="Alert Value" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbalertvalue" runat="server" Text='<%#Bind("ALERT_VALUE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEalertvalue" runat="server" Text='<%#Bind("ALERT_VALUE") %>' Visible="false"></asp:Label>
                  <asp:TextBox ID="txtEalertvalue" runat="server" Text='<%# Bind("ALERT_VALUE") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFalertvalue" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>        
        <asp:TemplateColumn HeaderText="E2P Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbe2pflag" runat="server" Text='<%#Bind("E2P_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEe2pflag" runat="server" Text='<%#Bind("E2P_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEe2pflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFe2pflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="FQC Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbfqcflag" runat="server" Text='<%#Bind("FQC_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEfqcflag" runat="server" Text='<%#Bind("FQC_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEfqcflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFfqcflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="MSN Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbmsnflag" runat="server" Text='<%#Bind("MSN_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEmsnflag" runat="server" Text='<%#Bind("MSN_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEmsnflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFmsnflag" runat="server">
                      <asp:ListItem Text="1" Value="1"  Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="PID Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbpidflag" runat="server" Text='<%#Bind("PID_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEpidflag" runat="server" Text='<%#Bind("PID_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEpidflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFpidflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="PCBA Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbpcbaflag" runat="server" Text='<%#Bind("PCBA_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEpcbaflag" runat="server" Text='<%#Bind("PCBA_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEpcbaflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFpcbaflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>           
              </FooterTemplate> 
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="OQC Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lboqcflag" runat="server" Text='<%#Bind("OQC_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEoqcflag" runat="server" Text='<%#Bind("OQC_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEoqcflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFoqcflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="OOB Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lboobflag" runat="server" Text='<%#Bind("OOB_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEoobflag" runat="server" Text='<%#Bind("OOB_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEoobflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFoobflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="CDMA Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbcdmaflag" runat="server" Text='<%#Bind("CDMA_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEcdmaflag" runat="server" Text='<%#Bind("CDMA_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEcdmaflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFcdmaflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="IMEI Link Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbimeilinkflag" runat="server" Text='<%#Bind("IMEI_LINK_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEimeilinkflag" runat="server" Text='<%#Bind("IMEI_LINK_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEimeilinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFimeilinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>           
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="ThirdID Link Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbthirdidlinkflag" runat="server" Text='<%#Bind("THIRDID_LINK_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEthirdidlinkflag" runat="server" Text='<%#Bind("THIRDID_LINK_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEthirdidlinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFthirdidlinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="PORDER Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbporderflag" runat="server" Text='<%#Bind("PORDER_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEporderflag" runat="server" Text='<%#Bind("PORDER_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEporderflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFporderflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="GLM Link Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbglmlinkflag" runat="server" Text='<%#Bind("GLM_LINK_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEglmlinkflag" runat="server" Text='<%#Bind("GLM_LINK_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEglmlinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFglmlinkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Ship Country Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbshipcountryflag" runat="server" Text='<%#Bind("SHIPCOUNTRY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEshipcountryflag" runat="server" Text='<%#Bind("SHIPCOUNTRY_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEshipcountryflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFshipcountryflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>      
        <asp:TemplateColumn HeaderText="Rework Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbreworkflag" runat="server" Text='<%#Bind("REWORK_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEreworkflag" runat="server" Text='<%#Bind("REWORK_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEreworkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFreworkflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>          
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="Data Type Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbdatatypeflag" runat="server" Text='<%#Bind("DATA_TYPE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEdatatypeflag" runat="server" Text='<%#Bind("DATA_TYPE_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEdatatypeflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFdatatypeflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="IMEI Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbimeiflag" runat="server" Text='<%#Bind("IMEI_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEimeiflag" runat="server" Text='<%#Bind("IMEI_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEimeiflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFimeiflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Picasso Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbpicassoflag" runat="server" Text='<%#Bind("PICASSO_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEpicassoflag" runat="server" Text='<%#Bind("PICASSO_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEpicassoflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFpicassoflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Ship Type Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbshiptypeflag" runat="server" Text='<%#Bind("SHIP_TYPE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEshiptypeflag" runat="server" Text='<%#Bind("SHIP_TYPE_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEshiptypeflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFshiptypeflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                  </asp:DropDownList>            
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Weight Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbweightflag" runat="server" Text='<%#Bind("WEIGHT_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:Label ID="lbEweightflag" runat="server" Text='<%#Bind("WEIGHT_FLAG") %>' Visible="false"></asp:Label>
                  <asp:DropDownList ID="ddlEweightflag" runat="server">
                      <asp:ListItem Text="1" Value="1" ></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList> 
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:DropDownList ID="ddlFweightflag" runat="server">
                      <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="0" Value="0"></asp:ListItem>
                  </asp:DropDownList>            
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