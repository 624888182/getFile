<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSCMPODetailNew.aspx.cs" Inherits="MainBBRYPrg_BBSCMPODetailNew " %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>
<%@ Register Src="~/WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css"
    title="wood" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="PODetail.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 0px;
            background-color: #FF9900;
        }
        .style1 td
        {
            background-color: #fff;
            height: 30px;
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
        .gvHeader
        {
            background-color: #F7DFB5;
            color: #8C4510;
            height: 40px;
        }
    </style>
</head>
<body bgcolor="#ffffff" style="overflow: scroll; width: 100%;">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        8<table cellpadding="0" cellspacing="1" class="style1">
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
                    <asp:Label ID="Label4" runat="server"></asp:Label>
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
                    <asp:Label ID="Label8" runat="server"></asp:Label>
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
                    <asp:Label ID="Label12" runat="server"></asp:Label>
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
                    <asp:Label ID="Label16" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label17" runat="server" Text="AMOUNT:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label18" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="Label19" runat="server" Text="BASEQTY:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label21" runat="server" Text="PRODUCTRECIPIENTPARTYID:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label22" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="Label23" runat="server" Text="DESCRIPTION:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label24" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label25" runat="server" Text="POCONFIRMATION:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label26" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="Label27" runat="server" Text="DELIVERYNOTIFICATION:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label28" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label29" runat="server" Text="INVOICEREQUEST:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label30" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="Label31" runat="server" Text="SCHEDULELINEID:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label32" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label33" runat="server" Text="DELIVERYSTARTDT:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="Label34" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    <asp:Label ID="Label35" runat="server" Text="SCHEDULEQUANTITY:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label36" runat="server"></asp:Label>
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
                    POID
                </td>
                <td class="style5">
                    <asp:Label ID="Label69" runat="server"></asp:Label>
                </td>
                <td class="style7">
                    POCNT&nbsp;
                </td>
                <td>
                    <asp:Label ID="Label72" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label53a" runat="server" Text="CostAmount: "></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label55a" runat="server"></asp:Label>
                </td>
                <td class="style9">
                    <asp:Label ID="Label56a" runat="server" Text="PO_CRAETE_UF3:"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label57a" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="style9">
                    <asp:Label ID="Label37a" runat="server" Text="Carrier ID: "></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label38a" runat="server"></asp:Label>
                </td>
                <td class="style9">
                    <asp:Label ID="Label39a" runat="server" Text="Carrier Name:"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label40a" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style9">
                    <asp:Label ID="Label37b" runat="server" Text="VO.RE: "></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="Label38b" runat="server"></asp:Label>
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="White" colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style5">
                    &nbsp;
                </td>
                &nbsp;
                <td align="right">
                    <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="#0066FF"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow: scroll; width: 100%;">
        <table cellpadding="2">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" HeaderStyle-CssClass="gvHeader" FooterStyle-CssClass="gvFooter"
                        DataKeyNames="ID,ITEMID" OnRowEditing="GridView_Edit" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div style="width: 50px; text-align: center;">
                                        <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Select" CommandName="Edit"
                                            ImageUrl="~/App_Themes/SkinFile/images/design.gif" />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="25px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CONFIRM_ADD_FLAG" HeaderText="*" />
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="ITEMID" HeaderText="ITEMID" />
                            <asp:BoundField DataField="IncoTermsCode" HeaderText="IncoTermsCode" />
                            <asp:BoundField DataField="IncoTermsName" HeaderText="IncoTermsName" />
                            <asp:BoundField DataField="OriginID" HeaderText="OriginID" />
                            <asp:BoundField DataField="OriginItemID" HeaderText="OriginItemID" />
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
                            <asp:BoundField DataField="POCONFIRMATION" HeaderText="POCONFIRMATION" />
                            <asp:BoundField DataField="DELIVERYNOTIFICATION" HeaderText="DELIVERYNOTIFICATION" />
                            <asp:BoundField DataField="INVOICEREQUEST" HeaderText="INVOICEREQUEST" />
                            <asp:BoundField DataField="SCHEDULELINEID" HeaderText="SCHEDULELINEID" />
                            <asp:BoundField DataField="DELIVERYSTARTDT" HeaderText="DELIVERYSTARTDT" />
                            <asp:BoundField DataField="ZoneCode" HeaderText="ZoneCode" />
                        </Columns>
                        <HeaderStyle Font-Bold="False" Font-Size="Small" />
                    </asp:GridView>
                    <table border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
                        text-align: center; height: 179px;">
                        <tr style="background-color: #FFFF00; height: 30px;">
                            <td style="width: 50px;">
                            </td>
                            <td>
                                ITEM
                            </td>
                            <td style="background-color: #FFFF00" class="style15">
                                SCHEDULELINEID
                            </td>
                            <td style="width: 125px">
                                QTY
                            </td>
                            <td style="width: 125px">
                                ACCEPTSTATUSCODE
                            </td>
                            <td>
                                DELIVERYSTARTDT
                            </td>
                            <td>
                                DELIVERYENDDT
                            </td>
                            <td style="width: 100px;">
                                Demand
                            </td>
                            <td style="width: 100px;">
                                Confirm&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox2" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                                <uc2:Calendar1 ID="Calendar11" runat="server"/>
                            </td>
                            <td>
                                <uc2:Calendar1 ID="Calendar12" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label70" runat="server" Text="Label" ForeColor="#FF3300"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label71" runat="server" Text="Label71"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                2
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox4" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/SkinFile/images/sendsign.gif"
                                    OnClick="ImageButton1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                3
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox6" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                        <tr>
                            <td>
                                4
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox8" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                        <tr>
                            <td>
                                5
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox21" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox10" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                        <tr>
                            <td>
                                6
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox22" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox12" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                        <tr>
                            <td>
                                7
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox23" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox14" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
                        <tr>
                            <td>
                                8
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox24" runat="server" Width="77px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="TextBox16" Enabled="false" onkeyup="onkeyupReg(this)" onafterpaste="onafterpasteReg(this)"
                                    runat="server" Width="100px"></asp:TextBox>
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
    </div>
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
