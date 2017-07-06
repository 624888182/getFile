<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSCMPO_Change_Detail.aspx.cs" Inherits="BBSCMPODetail" %>
<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <%@ Register src="~/WebControler/Controls/Calendar1.ascx" tagname="Calendar1" tagprefix="uc2" %>

<link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css" title="wood" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 2px;
        }
        .style5
        {
            width: 385px;
        }
        .style7
        {
            width: 207px;
        }
        .style8
        {
        }
        .style9
        {
            width: 211px;
        }
        #form1
        {
            height: 885px;
        }
        .style10
        {
            width: 100%;
            border: 2px ;
        }
    </style>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    
    <!--
    <uc1:HeaderBar ID="Header1" runat="server"></uc1:HeaderBar>
    
    -->
    
    
    <table cellpadding="2" class="style1" bgcolor="#FF9900" border="3" 
        style="border-style: hidden; border-color: #FFFFFF" >
        <tr>
            <td class="style9" bgcolor="#FF9933">
                <asp:Label ID="Label1" runat="server" Text="ID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label3" runat="server" Text="ITEMID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label5" runat="server" Text="INVOICEVERIFYINDICATOR:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label6" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label7" runat="server" Text="INTERNALID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label9" runat="server" Text="STANDARDID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label10" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label11" runat="server" Text="PRODUCTBUYERID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label13" runat="server" Text="PRODUCTSELLERID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label14" runat="server"></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label15" runat="server" Text="PRODUCTCATEGORYID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label16" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label17" runat="server" Text="AMOUNT:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label18" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label19" runat="server" Text="BASEQTY:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label21" runat="server" Text="PRODUCTRECIPIENTPARTYID:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label22" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label23" runat="server" Text="DESCRIPTION:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label24" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label25" runat="server" Text="POCONFIRMATION:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label26" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label27" runat="server" Text="DELIVERYNOTIFICATION:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label28" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label29" runat="server" Text="INVOICEREQUEST:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label30" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label31" runat="server" Text="SCHEDULELINEID:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label32" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label33" runat="server" Text="DELIVERYSTARTDT:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label34" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label35" runat="server" Text="SCHEDULEQUANTITY:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label36" runat="server" ></asp:Label>
            </td>
        </tr>
        <!--
        <tr>
            <td class="style9">
                <asp:Label ID="Label37" runat="server" Text="PO_CREATE_DT_UF1:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label38" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label39" runat="server" Text="PO_CREATE_DT_UF2:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label40" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label41" runat="server" Text="PO_CREATE_DT_UF3:"></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label42" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label43" runat="server" Text="PO_CREATE_DT_UF4:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label44" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:Label ID="Label45" runat="server" Text="PO_CREATE_DT_UF5: "></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label46" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label47" runat="server" Text="PO_CREATE_DT_UF6: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label48" runat="server" ></asp:Label>
            </td>
        </tr>
        
                <tr>
            <td class="style9">
                <asp:Label ID="Label49" runat="server" Text="PO_CREATE_DT_UF7: "></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label50" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label51" runat="server" Text="PO_CREATE_DT_UF8: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label52" runat="server" ></asp:Label>
            </td>
        </tr>
        
        
                <tr>
            <td class="style9">
                <asp:Label ID="Label62" runat="server" Text="PO_CREATE_DT_UF9: "></asp:Label>
            </td>
            <td class="style5">
                <asp:Label ID="Label63" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                <asp:Label ID="Label64" runat="server" Text="PO_CREATE_DT_UF10: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label65" runat="server" ></asp:Label>
            </td>
        </tr>
        
        -->
        
        <tr>
            <td class="style9">
                POID</td>
            <td class="style5">
                <asp:Label ID="Label69" runat="server" ></asp:Label>
            </td>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" BackColor="#33CCFF" 
                    BorderColor="#FF9900" ForeColor="Black" Text="Back" 
                      Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td class="style8" bgcolor="White" colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
     &nbsp;</td>
            <td align="right">
                <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="#0066FF"></asp:Label>
            </td>
        </tr>
        </table>
    <table cellpadding="2" class="style10">
        <tr>
            <td>
                  <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID,ITEMID" OnRowEditing= "GridView_Edit">
                  
                <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="ibtnAdd" runat="server"   CommandName= "Edit" ImageUrl="~/App_Themes/SkinFile/images/design.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
             
                        
<asp:BoundField DataField="ID" HeaderText="ID" />
<asp:BoundField DataField="ITEMID" HeaderText="ITEMID" />
<asp:BoundField DataField="INVOICEVERIFYINDICATOR" HeaderText="INVOICEVERIFYINDICATOR" />
<asp:BoundField DataField="CostCurrencyCode" HeaderText="CostCurrencyCode" />
<asp:BoundField DataField="INTERNALID" HeaderText="INTERNALID" />
<asp:BoundField DataField="STANDARDID" HeaderText="STANDARDID" />
<asp:BoundField DataField="PRODUCTBUYERID" HeaderText="PRODUCTBUYERID" />
<asp:BoundField DataField="PRODUCTSELLERID" HeaderText="PRODUCTSELLERID" />
<asp:BoundField DataField="ProductRecipientID" HeaderText="ProductRecipientID" />
<asp:BoundField DataField="PRODUCTCATEGORYID" HeaderText="PRODUCTCATEGORYID" />
<asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" />
<asp:BoundField DataField="Currency" HeaderText="Currency" />
<asp:BoundField DataField="BASEQTY" HeaderText="BASEQTY" />
<asp:BoundField DataField="Unit" HeaderText="Unit" />
<asp:BoundField DataField="PRODUCTRECIPIENTPARTYID" HeaderText="PRODUCTRECIPIENTPARTYID" />
<asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
<asp:BoundField DataField="OriginID" HeaderText="OriginID" />
<asp:BoundField DataField="OriginItemID" HeaderText="OriginItemID" />
<asp:BoundField DataField="POCONFIRMATION" HeaderText="POCONFIRMATION" />
<asp:BoundField DataField="DELIVERYNOTIFICATION" HeaderText="DELIVERYNOTIFICATION" />
<asp:BoundField DataField="INVOICEREQUEST" HeaderText="INVOICEREQUEST" />
<asp:BoundField DataField="SCHEDULELINEID" HeaderText="SCHEDULELINEID" />
<asp:BoundField DataField="DELIVERYSTARTDT" HeaderText="DELIVERYSTARTDT" />
<asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" />



<asp:BoundField DataField="IncoTermsCode" HeaderText="IncoTermsCode" />
<asp:BoundField DataField="IncoTermsName" HeaderText="IncoTermsName" />

                         
                         
                         
                    </Columns>
                  
                      <HeaderStyle Font-Bold="False" Font-Size="Small" />
                  
                  </asp:GridView>
                    
                   
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#CC6600" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                             
                
                        <table border="1" bordercolor="Silver" cellspacing="0" cellpadding="0" 
 style="border-collapse: collapse;  text-align: center; width: 832px; height: 179px;">
                            <tr style="background-color: #FFFF00">
                            <td>  </td>
                                <td style="background-color: #FFFF00" class="style15">
                                    SCHEDULELINEID</td>
                                <td class="style16">
                                    QTY</td>
                                <td>
                                    ACCEPTSTATUSCODE</td>
                                <td>
                                    DELIVERYSTARTDT</td>
                                <td >
                                    DELIVERYENDDT</td>
                                    <td rowspan="2" bgcolor="White" align="center" 
                                    style="padding: 20px 10px 10px 10px"> 
                                        <asp:Label ID="Label70" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr> <td>1</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox2" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar11" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar12" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>2</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox4" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar1" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar2" runat="server" />
                                </td>
                                 <td>
                                     
                                     <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/App_Themes/SkinFile/images/sendsign.gif" 
                                            onclick="ImageButton1_Click" />
                                     
                                </td>
                                 
                            </tr>
                            <tr><td>3</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox6" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar3" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar4" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>4</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox8" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList4" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar5" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar6" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>5</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox10" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList5" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar7" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar8" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>6</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox12" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList6" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar9" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar10" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>7</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox14" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList7" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar13" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar14" runat="server" />
                                </td>
                                 
                            </tr>
                            <tr><td>8</td>
                                <td class="style15">
                                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                                </td>
                                <td class="style16">
                                    <asp:TextBox ID="TextBox16" runat="server" Width="134px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList8" runat="server" Height="20px" Width="85px">
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AJ</asp:ListItem>
                                        <asp:ListItem>RE</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar15" runat="server" />
                                </td>
                                <td>
                                    <uc2:Calendar1 ID="Calendar16" runat="server" />
                                </td>
                                 
                            </tr>
                             
                        </table>

                <asp:Label ID="Label68" runat="server" Text="PO Item's  Confirmation"></asp:Label>
                &nbsp;Dispaly.<br />
                
                
                                    </ContentTemplate>
                </td>
        </tr>
    </table>
    </form>
</body>
</html>
<!--
<asp:BoundField DataField="SCHEDULEQUANTITY" HeaderText="SCHEDULEQUANTITY" />
<asp:BoundField DataField="ScheduleLineUnit" HeaderText="ScheduleLineUnit" />
<asp:BoundField DataField="PO_CREATE_DT_UF1" HeaderText="PO_CREATE_DT_UF1" />
<asp:BoundField DataField="PO_CREATE_DT_UF2" HeaderText="PO_CREATE_DT_UF2" />
<asp:BoundField DataField="PO_CREATE_DT_UF3" HeaderText="PO_CREATE_DT_UF3" />
<asp:BoundField DataField="PO_CREATE_DT_UF4" HeaderText="PO_CREATE_DT_UF4" />
<asp:BoundField DataField="PO_CREATE_DT_UF5" HeaderText="PO_CREATE_DT_UF5" />
<asp:BoundField DataField="PO_CREATE_DT_UF6" HeaderText="PO_CREATE_DT_UF6" />
<asp:BoundField DataField="PO_CREATE_DT_UF7" HeaderText="PO_CREATE_DT_UF7" />
<asp:BoundField DataField="PO_CREATE_DT_UF8" HeaderText="PO_CREATE_DT_UF8" />
<asp:BoundField DataField="PO_CREATE_DT_UF9" HeaderText="PO_CREATE_DT_UF9" />
<asp:BoundField DataField="PO_CREATE_DT_UF10" HeaderText="PO_CREATE_DT_UF10" />

-->