<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_Label_MainReport.aspx.cs" Inherits="Boundary_B2B_Label_MainReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_Label_MainReport</title>
    <style>
    <!--
   .test{border:1px solid;border-collapse:collapse;empty-cells:show;}
   .test td{border:1px solid;height:10px;<b>empty-cells:show;</b>}
    -->
   </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell Label Main Report</asp:Label>
            </td>
            <td valign="middle" width="50%" style="height: 47px">
                <table height="100%">
                    <tr>
                        <td  align="right" style="width: 100%; height: 10px;">
                            <asp:imagebutton id="btnSingle" runat="server" ImageUrl="..\Images\Reporte.gif" AlternateText="Report current page to excel" OnClick="btnSingle_Click"  ></asp:imagebutton>
                        </td>
                        <td align="left" style="height: 10px">
                            <asp:Label ID="lblSingle" runat="server" >Single</asp:Label>
                        </td>
                        
                        <td  align="right" style="width: 100%; height: 10px;">
                            <asp:imagebutton id="btnAll" runat="server" ImageUrl="..\Images\Reporte.gif"  AlternateText="Report all pages to excel" OnClick="btnAll_Click"  ></asp:imagebutton>
                        </td>
                        <td align="left" style="height: 10px">
                            <asp:Label ID="lblAll" runat="server" >All</asp:Label>
                        </td>
                        
                        <td  align="right" style="width: 100%; height: 10px;">
                            <asp:imagebutton id="btnClose" runat="server" ImageUrl="..\Images\close.gif" OnClick="btnClose_Click" AlternateText="Close"  ></asp:imagebutton>
                        </td>
                        <td align="left" style="height: 10px">
                            <asp:Label ID="lblClose" runat="server" >Close</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="test" cellSpacing="0" cellPadding="0" border="1"  align="center" width="95%">
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblFromDate" runat="server" >From Date:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblFromDate1" runat="server" Text=" " ></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblToDate" runat="server" >To Date:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblToDate1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblPoNo" runat="server" >PoNo:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblPoNo1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblUploadFlag" runat="server" ></asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblUploadFlag1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
    </table>
<asp:Panel ID="panel1" runat="server" Height="10px" ></asp:Panel>
<table id="tb1" class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" align="center" width="95%">
    <tr>
        <td>
            <asp:Label ID="lblShip" runat="server" Font-Size="15px" Text="Ship Label "></asp:Label>  
            <asp:GridView ID="gvShipLabel" runat="server" Font-Size="10px"  AutoGenerateColumns="False"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvShipLabel_RowCreated" OnRowDataBound="gvShipLabel_RowDataBound" OnRowCommand="gvShipLabel_RowCommand" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Message ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblMesgid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.MESGID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="MsgType" >
                         <ItemTemplate>   
                             <asp:Label ID="lblMsgType" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.MESGTYP") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="SenderID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblSendid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.SENDID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="ReceiverID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblReceid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.RECEID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                           
                    <asp:TemplateField HeaderText="OrdNO" >
                         <ItemTemplate> 
                             <asp:LinkButton ID="lbtnOrdnum" runat="server" CommandName="OrdNum" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ORDNUM") %>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="TNINTERNALID" HeaderText="tninternalid"></asp:BoundField>
                    <asp:BoundField DataField="BATCHID" HeaderText="batchid"></asp:BoundField>
                    <asp:BoundField DataField="BATCHMESGID" HeaderText="batchmesgid"></asp:BoundField>
                    <asp:BoundField DataField="LABEL_TEMPLATE" HeaderText="label_template"></asp:BoundField>
                    <asp:BoundField DataField="CARRIER_CODE" HeaderText="carrier_code"></asp:BoundField>
                    <asp:TemplateField HeaderText="buid" >
                         <ItemTemplate>   
                             <asp:Label ID="lblBuid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.BUID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="MCID" HeaderText="mcid"></asp:BoundField>
                    <asp:BoundField DataField="CSONUM" HeaderText="csonum"></asp:BoundField> 
                    <asp:BoundField DataField="CUSNAM" HeaderText="cusnam"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD1" HeaderText="cusad1"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD2" HeaderText="cusad2"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD3" HeaderText="cusad3"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD4" HeaderText="cusad4"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD5" HeaderText="cusad5"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD6" HeaderText="cusad6"></asp:BoundField>
                    <asp:BoundField DataField="CUSAD7" HeaderText="cusad7"></asp:BoundField>                    
                    <asp:BoundField DataField="CUSCOO" HeaderText="cuscoo"></asp:BoundField>
                    <asp:BoundField DataField="CUSPHO" HeaderText="cuspho"></asp:BoundField>                    
                    <asp:BoundField DataField="TOTAL_BOXES" HeaderText="total_boxes"></asp:BoundField>
                    <asp:BoundField DataField="TOTAL_SYSTEMS" HeaderText="total_systems"></asp:BoundField>                    
                    <asp:BoundField DataField="RECYCLE_LOGO" HeaderText="recycle_logo"></asp:BoundField>
                    <asp:BoundField DataField="SPECIAL_INSTRUCTIONS" HeaderText="special_instructions"></asp:BoundField>                    
                    <asp:BoundField DataField="BELT_NUMBER" HeaderText="belt_number"></asp:BoundField>
                    <asp:BoundField DataField="POSTAL_CODE" HeaderText="postal_code"></asp:BoundField>
                    <asp:BoundField DataField="SERVICE_LEVEL_DESC" HeaderText="service_level_desc"></asp:BoundField>
                    <asp:BoundField DataField="SERVICE_LEVEL_ICON" HeaderText="service_level_icon"></asp:BoundField>
                    <asp:BoundField DataField="ROUTING_CODE" HeaderText="routing_code"></asp:BoundField>
                    <asp:BoundField DataField="LICENSE_PLATE" HeaderText="license_plate"></asp:BoundField>
                    <asp:BoundField DataField="DEPORT_CODE1" HeaderText="deport_code1"></asp:BoundField>
                    <asp:BoundField DataField="DEPORT_CODE2" HeaderText="deport_code1"></asp:BoundField>                    
                    <asp:BoundField DataField="PRIMARY_SORT" HeaderText="primary_sort"></asp:BoundField>
                    <asp:BoundField DataField="SECONDARY_SORT" HeaderText="secondary_sort"></asp:BoundField>
                    <asp:BoundField DataField="EXPRESS_ROUTING_CODE" HeaderText="express_routing_code"></asp:BoundField>
                    <asp:BoundField DataField="PROCESSFLAG" HeaderText="processflag"></asp:BoundField>
                    <asp:BoundField DataField="PROCESSDATE" HeaderText="processdate"></asp:BoundField>
                    <asp:BoundField DataField="ERRORMSG" HeaderText="errormsg"></asp:BoundField>
                    <asp:BoundField DataField="REGION" HeaderText="region"></asp:BoundField>
                    <asp:BoundField DataField="EXPRESS_ROUTING_CODE_BC" HeaderText="express_routing_code_bc"></asp:BoundField>                  
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Lasteditdt"></asp:BoundField>                          
                </Columns>
                <RowStyle BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>

            <asp:Panel ID="panel2" runat="server" Height="10px" ></asp:Panel>
            <asp:Label ID="lblTio" runat="server" Font-Size="15px" Text="Tio Label "></asp:Label>  
            <asp:GridView ID="gvTioLabel" runat="server"  CssClass="DataGridFont" Font-Size="10px" AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none"
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvTioLabel_RowCreated" OnRowDataBound="gvTioLabel_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex1" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MESGID" HeaderText="Message ID"></asp:BoundField> 
                    <asp:BoundField DataField="MESGTYP" HeaderText="MsgType"></asp:BoundField>
                    <asp:BoundField DataField="SENDID" HeaderText="SenderID"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="ReceiverID"></asp:BoundField>
                    <asp:BoundField DataField="ORDNUM" HeaderText="OrdNO"></asp:BoundField>
                    <asp:BoundField DataField="LABEL_TEMPLATE" HeaderText="label_template"></asp:BoundField>
                    <asp:BoundField DataField="ADDL_BOXING" HeaderText="addl_boxing"></asp:BoundField>
                    <asp:BoundField DataField="SHIP_CODE" HeaderText="ship_code"></asp:BoundField>
                    <asp:BoundField DataField="PACK_REF" HeaderText="pack_ref"></asp:BoundField>                    
                    <asp:BoundField DataField="DEST_COUNTRY" HeaderText="dest_country"></asp:BoundField>
                    <asp:BoundField DataField="REGION" HeaderText="region"></asp:BoundField>     
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdate"></asp:BoundField>         
                </Columns>
                <RowStyle BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />   
            </asp:GridView>
        </td>
    </tr>
</table>

<%--<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"  align="center" width="95%" visible="false">
    <tr>
        <td align="center">
            <asp:imagebutton id="btnFirst" AlternateText="First" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-first.gif"  CommandName="Page" CommandArgument="First" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:imagebutton id="btnPrevious" AlternateText="Previous" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-previous.gif"  CommandName="Page" CommandArgument="Previous" OnClick="PagebtnClick"> </asp:imagebutton>
            <asp:imagebutton id="btnNext" AlternateText="Next" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-next.gif"  CommandName="Page" CommandArgument="Next" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:imagebutton id="btnLast" AlternateText="Last" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-last.gif"   CommandName="Page" CommandArgument="Last" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:Label ID="lblNow" runat="server" ></asp:Label> 
            <asp:Label ID="lblTotal" runat="server" ></asp:Label>                 
            <asp:TextBox ID="txtNo" runat="server" Width="80px"></asp:TextBox>
            頁<asp:imagebutton id="btnGo" AlternateText="Go" runat="server" ImageUrl="..\Images\go.gif" CausesValidation="false" CommandArgument="-1" CommandName="Page" OnClick="PagebtnClick"></asp:imagebutton>

        </td>
    </tr>
</table>--%>
    </div>
    </form>
</body>
</html>
