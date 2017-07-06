<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSCMPO_ITEM_Detail.aspx.cs" Inherits="BBSCMPO_ITEM_Detail" StyleSheetTheme="" %>
<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>


  <%@ Register src="~/WebControler/Controls/Calendar1.ascx" tagname="Calendar1" tagprefix="uc2" %>

<link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css" title="wood" />

        <form id="form1" runat="server">
    
       


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 2px;
        }
        .style3
        {
        }
        .style5
        {
            width: 385px;
        }
        .style7
        {
            width: 207px;
        }
        .style9
        {
            width: 211px;
        }
        #form1
        {
            height: 720px;
        }
        .style10
        {
            width: 100%;
            border: 2px ;
        }
        .style15
        {
            width: 151px;
        }
        .style16
        {
            width: 265px;
        }
    </style>
</head>
<body bgcolor="#ffffff">
    
    &nbsp;&nbsp;
    <asp:Label ID="Label66" runat="server" Text="PO Item Information"></asp:Label>
    
    <table cellpadding="2" class="style1" bgcolor="#FF9900" border="3" 
        style="border-style: hidden; border-color: #FFFFFF; height: 284px;" >
        <tr>
            <td class="style9" bgcolor="#FF9933">
                <asp:Label ID="Label1" runat="server" Text="ID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label3" runat="server" Text="POID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label5" runat="server" Text="ITEMID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label6" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label7" runat="server" Text="INVOICEVERIFYINDICATOR:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label9" runat="server" Text="CostCurrencyCode:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label10" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label11" runat="server" Text="INTERNALID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label13" runat="server" Text="STANDARDID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label14" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label15" runat="server" Text="PRODUCTBUYERID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label16" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label17" runat="server" Text="PRODUCTSELLERID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label18" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label19" runat="server" Text="ProductRecipientID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label21" runat="server" Text="PRODUCTCATEGORYID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label22" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label23" runat="server" Text="AMOUNT:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label24" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label25" runat="server" Text="Currency:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label26" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label27" runat="server" Text="BASEQTY:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label28" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label29" runat="server" Text="Unit:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label30" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label31" runat="server" Text="PRODUCTRECIPIENTPARTYID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label32" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label33" runat="server" Text="DESCRIPTION:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label34" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label35" runat="server" Text="IncoTermsCode:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label36" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label37" runat="server" Text="IncoTermsName:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label38" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label39" runat="server" Text="OriginID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label40" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label41" runat="server" Text="OriginItemID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label42" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label43" runat="server" Text="POCONFIRMATION:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label44" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label45" runat="server" Text="DELIVERYNOTIFICATION: "></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label46" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label47" runat="server" Text="INVOICEREQUEST: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label48" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label49" runat="server" Text="SCHEDULELINEID: "></asp:Label>
            </td>
            <td  >
                <asp:Label ID="Label62" runat="server" ></asp:Label>
            </td>
         
            <td class="style9">
                <asp:Label ID="Label50" runat="server" Text="DELIVERYSTARTDT:"></asp:Label>
            </td>
            <td class="style3"  >
                <asp:Label ID="Label63" runat="server"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label51" runat="server" Text="ZoneCode: "></asp:Label>
            </td>
            <td class="style3"  >
                <asp:Label ID="Label64" runat="server"  ></asp:Label>
            </td>
        
            <td class="style9">
                <asp:Label ID="Label52" runat="server" Text="CONFIRMFLAG:"></asp:Label>
            </td>
            <td class="style3" >
                <asp:Label ID="Label65" runat="server"  ></asp:Label>
            </td>
        </tr>
         
         
        </table>
    <table cellpadding="2" class="style10">
    
    
   
        <tr>
            <td>
                <asp:Label ID="Label67" runat="server" Text="PO Item's  Confirmation"></asp:Label>
                &nbsp;Add.<br />

                
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" CellSpacing="2" Width="1189px" 
                    onrowdatabound="GridView1_RowDataBound">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <Columns>
                    
                    
                                              
                     

                    
                        <asp:TemplateField HeaderText="NO">
                            <ItemTemplate>
                                <asp:Label ID="Label54" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="SCHEDULELINEID">
                            <ItemTemplate>
                                <asp:Label ID="Label58" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"SCHEDULELINEID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTY">
                            <ItemTemplate>
                                <asp:Label ID="Label59" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"QTY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CREATIONDT">
                            <ItemTemplate>
                                <asp:Label ID="Label60" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ACCEPTSTATUSCODE">
                            <ItemTemplate>
                                <asp:Label ID="Label61" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"ACCEPTSTATUSCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                            
     
 
     
  
 
  
    
                    
                        
                                                <asp:TemplateField HeaderText="DELIVERYSTARTDT">
                            <ItemTemplate>
                                <asp:Label ID="Label68" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"DELIVERYSTARTDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                                                <asp:TemplateField HeaderText="DELIVERYENDDT">
                            <ItemTemplate>
                                <asp:Label ID="Label69" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"DELIVERYENDDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        
                                                <asp:TemplateField HeaderText="CONFIRMFLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label70" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                                                                
                                                                               <asp:TemplateField HeaderText="SENDFLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label71" runat="server" 
                                    Text='<%#DataBinder.Eval(Container.DataItem,"SENDFLAG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      
                      
     
   
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#CC6600" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                
                
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
