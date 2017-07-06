<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmE2PMantenance.ascx.cs" Inherits="Boundary_wfrmE2PMantenance" %>
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
    <asp:datagrid id="dgE2p" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px" 
        EditItemStyle-HorizontalAlign="Center"  EditItemStyle-VerticalAlign="Middle"  PageSize="15"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="true" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" ShowFooter="false"  AutoGenerateColumns="False" OnCancelCommand="dgE2p_CancelCommand" OnEditCommand="dgE2p_EditCommand" OnItemCommand="dgE2p_ItemCommand" OnItemDataBound="dgE2p_ItemDataBound" OnPageIndexChanged="dgE2p_PageIndexChanged" OnUpdateCommand="dgE2p_UpdateCommand">
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
          <asp:TemplateColumn HeaderText="Packing Line"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPackingline" runat="server" Text='<%#Bind("PACKING_LINE") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPackingline" runat="server" Text='<%# Bind("PACKING_LINE") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFPackingline" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>          
         <asp:TemplateColumn HeaderText="PID Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPidflag" runat="server" Text='<%#Bind("PID_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPidflag" runat="server" Text='<%# Bind("PID_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>              
              <FooterTemplate>
                  <asp:TextBox ID="txtFPidflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Imei Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbImeiflag" runat="server" Text='<%#Bind("IMEI_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtImeiflag" runat="server" Text='<%# Bind("IMEI_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFImeiflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate>
         </asp:TemplateColumn>
         <asp:TemplateColumn HeaderText="Picasso Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPicassoflag" runat="server" Text='<%#Bind("PICASSO_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPicassoflag" runat="server" Text='<%# Bind("PICASSO_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPicassoflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="ErrorMsg Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbErrormsgflag" runat="server" Text='<%#Bind("ERRORMSG_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtErrormsgflag" runat="server" Text='<%#Bind("ERRORMSG_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFErrormsgflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="ProcessTime Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbProcesstimeflag" runat="server" Text='<%#Bind("PROCESSTIME_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtProcesstimeflag" runat="server" Text='<%# Bind("PROCESSTIME_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFProcesstimeflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Repair Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbRepairflag" runat="server" Text='<%#Bind("REPAIR_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtRepairflag" runat="server" Text='<%# Bind("REPAIR_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFRepairflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="PrivilegePwd Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPrivilegepwdflag" runat="server" Text='<%#Bind("PRIVILEGEPWD_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPrivilegepwdflag" runat="server" Text='<%# Bind("PRIVILEGEPWD_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPrivilegepwdflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Nkey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbNkeyflag" runat="server" Text='<%#Bind("NKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtNkeyflag" runat="server" Text='<%# Bind("NKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFNkeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>          
        <asp:TemplateColumn HeaderText="Nskey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbNskeyflag" runat="server" Text='<%#Bind("NSKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtNskeyflag" runat="server" Text='<%# Bind("NSKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFNskeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="Spkey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSpkeyflag" runat="server" Text='<%#Bind("SPKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtSpkeyflag" runat="server" Text='<%# Bind("SPKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFSpkeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>        
        <asp:TemplateColumn HeaderText="Ckey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCkeyflag" runat="server" Text='<%#Bind("CKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtCkeyflag" runat="server" Text='<%# Bind("CKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFCkeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="Pkey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPkeyflag" runat="server" Text='<%#Bind("PKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPkeyflag" runat="server" Text='<%# Bind("PKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPkeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="Fskey Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbFskeyflag" runat="server" Text='<%#Bind("FSKEY_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtFskeyflag" runat="server" Text='<%# Bind("FSKEY_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFFskeyflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="RandomSeed Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbRandomseedflag" runat="server" Text='<%#Bind("RANDOMSEED_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtRandomseedflag" runat="server" Text='<%# Bind("RANDOMSEED_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFRandomseedflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>         
        <asp:TemplateColumn HeaderText="E2Preffile Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbE2preffileflag" runat="server" Text='<%#Bind("E2PREFFILE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtE2preffileflag" runat="server" Text='<%# Bind("E2PREFFILE_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFE2preffileflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="E2Pprofile Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbE2pprofileflag" runat="server" Text='<%#Bind("E2PPROFILE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtE2pprofileflag" runat="server" Text='<%# Bind("E2PPROFILE_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFE2pprofileflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="BtAddress Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbBtaddressflag" runat="server" Text='<%#Bind("BTADDRESS_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtBtaddressflag" runat="server" Text='<%# Bind("BTADDRESS_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFBtaddressflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="SugNumber Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbSugflag" runat="server" Text='<%#Bind("SUGNUMBER_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtSugflag" runat="server" Text='<%# Bind("SUGNUMBER_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFSugflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="ModuleProductID Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbModuleflag" runat="server" Text='<%#Bind("MODULEPRODUCTID_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtModuleflag" runat="server" Text='<%# Bind("MODULEPRODUCTID_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFModuleflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="PhoneVer Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPhoneverflag" runat="server" Text='<%#Bind("PHONEVER_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPhoneverflag" runat="server" Text='<%# Bind("PHONEVER_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPhoneverflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="OuterFid Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbOuterflag" runat="server" Text='<%#Bind("OUTERFID_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtOuterflag" runat="server" Text='<%# Bind("OUTERFID_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFOuterflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="InnerFid Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbInnerflag" runat="server" Text='<%#Bind("INNERFID_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtInnerflag" runat="server" Text='<%# Bind("INNERFID_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFInnerflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="PDErrorCode Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPderrorcodeflag" runat="server" Text='<%#Bind("PDERRORCODE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPderrorcodeflag" runat="server" Text='<%# Bind("PDERRORCODE_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPderrorcodeflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>      
        <asp:TemplateColumn HeaderText="PDErrorMsg Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbPderrormsgflag" runat="server" Text='<%#Bind("PDERRORMSG_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtPderrormsgflag" runat="server" Text='<%# Bind("PDERRORMSG_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFPderrormsgflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="MiscInfo Flag"   ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbMiscinfoflag" runat="server" Text='<%#Bind("MISCINFO_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtMiscinfoflag" runat="server" Text='<%# Bind("MISCINFO_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFMiscinfoflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>     
        <asp:TemplateColumn HeaderText="Disable Flag" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbDisableflag" runat="server" Text='<%#Bind("DISABLE_FLAG") %>'></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                  <asp:TextBox ID="txtDisableflag" runat="server" Text='<%# Bind("DISABLE_FLAG") %>'></asp:TextBox>
              </EditItemTemplate>
              <FooterTemplate>
                  <asp:TextBox ID="txtFDisableflag" runat="server" Text=""></asp:TextBox>             
              </FooterTemplate> 
        </asp:TemplateColumn>  
        <asp:TemplateColumn HeaderText="Create Date" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbCreatedate" runat="server" Text='<%#Bind("CREATE_DATE") %>'></asp:Label>
              </ItemTemplate>
        </asp:TemplateColumn> 
        <asp:TemplateColumn HeaderText="Emp Name" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="lbEmpname" runat="server" Text='<%#Bind("EMP_NAME") %>'></asp:Label>
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