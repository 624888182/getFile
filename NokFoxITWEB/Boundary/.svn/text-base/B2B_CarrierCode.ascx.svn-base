<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_CarrierCode.ascx.cs" Inherits="Boundary_B2B_CarrierCode" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 

<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td width="50" rowSpan="9"><FONT face="新細明體"></FONT></td>
        <td><asp:label id="lblRegion" runat="server" >Region:</asp:label></td>
        <td width="200px">
            <asp:TextBox ID="txtRegion" runat="server" ></asp:TextBox>          		                
        </td>
        <td ><asp:label id="lblCarrierCode" runat="server" >Carrier Code:</asp:label></td>
        <td>
            <asp:TextBox ID="txtCarrierCode" runat="server" ></asp:TextBox>         
        </td>
        <td  align="center" rowspan="9" width="200px"> 
            <asp:button id="btnSearch" runat="server" Width="100px" Text="Query" OnClick="btnSearch_Click"></asp:button>
        </td>
    </tr> 
    <tr> 
        <td><asp:label id="lblMCID" runat="server" >MCID:</asp:label></td>
        <td><asp:TextBox id="txtMCID" runat="server" Width="80px"></asp:TextBox></td>
        <%--<td><asp:dropdownlist id="ddlMCID" runat="server" Width="80px"></asp:dropdownlist></td>--%>
        <td><asp:label id="lblShipMode" runat="server" Width="100px">Ship Mode:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlShipMode" runat="server" Width="80px"  >
            <asp:ListItem Text="ALL" Selected="True"></asp:ListItem>
            <asp:ListItem Text="A-Air" ></asp:ListItem>
            <asp:ListItem Text="G-Ground" ></asp:ListItem>
            <asp:ListItem Text="S-Sea" ></asp:ListItem> 	    
        </asp:dropdownlist></td> 
</table>
<hr>
<table cellSpacing="0" cellPadding="0" border="0" >
    <tr>       
        <td width="50" ><FONT face="新細明體"></FONT></td>
        <td >
             <asp:GridView ID="gvCarrierCode" runat="server"  Font-Size="10px"  AutoGenerateColumns="false" 
                  BackColor="White" Font-Names="Verdana" BorderStyle="none" 
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCancelingEdit="gvCarrierCode_RowCancelingEdit" 
                  OnRowCommand="gvCarrierCode_RowCommand" OnRowCreated="gvCarrierCode_RowCreated" OnRowDataBound="gvCarrierCode_RowDataBound" 
                  OnRowEditing="gvCarrierCode_RowEditing" OnRowUpdating="gvCarrierCode_RowUpdating">
                  <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <HeaderTemplate>
                             <asp:LinkButton ID="lkbAddItem" runat="server" CommandName="AddItem">新增</asp:LinkButton>
                         </HeaderTemplate>
                         <ItemTemplate>   
                             <asp:Label ID="lblID" runat="server"></asp:Label>
                         </ItemTemplate>
                         <FooterTemplate>                            
                            <asp:LinkButton  ID="btnCommit" runat="server" Text="確定" CommandName="ItemSure"/>
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>
                         </FooterTemplate>
                    </asp:TemplateField>                      
                    <asp:TemplateField HeaderText="Origin">
                        <ItemTemplate>
                            <asp:Label ID="lblorigin" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORIGIN") %>'></asp:Label>
                        </ItemTemplate> 
                        <FooterTemplate>
                            <asp:Label ID="lblForigin"  runat="server" Text="FTY"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblregion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEregion" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:Label>  
                             <asp:TextBox ID="txtEregion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFregion" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MCID">
                        <ItemTemplate>
                            <asp:Label ID="lblMCID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEMCID" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.MCID") %>'></asp:Label>  
                             <asp:TextBox ID="txtEMCID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MCID") %>'></asp:TextBox>
                             <%--<asp:DropDownList  ID="ddlEMCID" runat="server"></asp:DropDownList>    --%>                    
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--<asp:DropDownList  ID="ddlFMCID" runat="server"></asp:DropDownList>--%>
                            <asp:TextBox ID="txtFMCID" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>           
                    <asp:TemplateField HeaderText="Ship Mode">
                        <ItemTemplate>
                            <asp:Label ID="lblShipMode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SHIP_MODE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEShipMode" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.SHIP_MODE") %>'></asp:Label>  
                             <%--<asp:TextBox ID="txtEShipMode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SHIP_MODE") %>'></asp:TextBox>  --%> 
                             <asp:DropDownList  ID="ddlEShipMode" runat="server">
                                <asp:ListItem Text="A-Air"></asp:ListItem>
                                <asp:ListItem Text="G-Ground" ></asp:ListItem>
                                <asp:ListItem Text="S-Sea" ></asp:ListItem> 	
                            </asp:DropDownList>                      
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList  ID="ddlFShipMode" runat="server">
                                <asp:ListItem Text="A-Air" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="G-Ground" ></asp:ListItem>
                                <asp:ListItem Text="S-Sea" ></asp:ListItem> 	
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtFShipMode" runat="server" Text=""></asp:TextBox>--%>
                        </FooterTemplate>
                    </asp:TemplateField>                     
                    <asp:TemplateField HeaderText="SCACID">
                        <ItemTemplate>
                            <asp:Label ID="lblSCACID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCACID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblSCACID" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.SCACID") %>'></asp:Label>  
                             <asp:TextBox ID="txtESCACID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCACID") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFSCACID" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="UPDATED_DATE" HeaderText="Updated Date"  ReadOnly="True"></asp:BoundField>                     
                    <asp:TemplateField HeaderText="Updated By">
                        <ItemTemplate>
                            <asp:Label ID="lblupdatedby" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UPDATED_BY") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEupdatedby" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.UPDATED_BY") %>'></asp:Label>  
                             <asp:TextBox ID="txtEupdatedby" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UPDATED_BY") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                    </asp:TemplateField>                     
                    <asp:CommandField ShowEditButton="True"  HeaderText="Edit"  /> 
                </Columns>
                <RowStyle BackColor="#f1f8f1" Height="30px" Width="80px" HorizontalAlign="center"/>
                <HeaderStyle  HorizontalAlign="Center" Height="30px" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>
        </td>
   </tr>
</table>