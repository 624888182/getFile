<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false"  CodeFile="B2B_PO_MainReport.aspx.cs" Inherits="Boundary_B2B_PO_MainReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PO Main Report</title>    
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
    <table cellSpacing="0" cellPadding="0" border="0"  width="95%" >
        <tr>
            <td rowspan="2" style="width: 30px"></td>
            <td>
                <table cellSpacing="0" cellPadding="0" border="1"  align="left" width="90%" bordercolor="#66cc99">
                    <tr width="50%">
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell PO Main Report</asp:Label>
                        </td>
                        <td height="10px" valign="middle" width="50%">
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
            </td>
        </tr>
        <tr>
            <td>
                <table cellSpacing="0" cellPadding="0" border="1"  align="left" width="90%"  class="test">
                    <tr >
                        <td width="20%" align="center"  valign="middle" style="height: 10px" >
                            <asp:Label ID="lblFromDate" runat="server" >From Date:</asp:Label>
                        </td>
                        <td align="center" valign="middle" width="30%" style="height: 10px" >
                            <asp:Label ID="lblFromDate1" runat="server" Text=" " ></asp:Label>
                        </td>
                        <td width="20%" align="center"  valign="middle" style="height: 10px">
                            <asp:Label ID="lblToDate" runat="server" >To Date:</asp:Label>
                        </td>
                        <td align="center" valign="middle" width="30%" style="height: 10px" >
                            <asp:Label ID="lblToDate1" runat="server" Text=" "></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblMesgID" runat="server" >Message ID:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%" >
                            <asp:Label ID="lblMesgID1" runat="server" Text=" "></asp:Label>
                        </td>
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblPoNo" runat="server" >PoNo:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%" >
                            <asp:Label ID="lblPoNo1" runat="server" Text=" "></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="center"  valign="middle" style="height: 10px" >
                            <asp:Label ID="lblUploadFlag" runat="server" >Upload Flag:</asp:Label>
                        </td>
                        <td align="center" valign="middle" width="30%" style="height: 10px" >
                            <asp:Label ID="lblUploadFlag1" runat="server" Text=" "></asp:Label>
                        </td>
                        <td width="20%" align="center"  valign="middle" style="height: 10px">
                            <asp:Label ID="lblSoNo" runat="server" >SoNo:</asp:Label>
                        </td>
                        <td align="center" valign="middle" width="30%" style="height: 10px">
                            <asp:Label ID="lblSoNo1" runat="server" Text=" "></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblIdocNo" runat="server" >Idoc No:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%">
                            <asp:Label ID="lblIdocNo1" runat="server" Text=" "></asp:Label>
                        </td>
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblSFCFlag" runat="server" >In SFC Flag:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%">
                            <asp:Label ID="lblSFCFlag1" runat="server" Text=" "></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblPlantCode" runat="server" >Plant Code:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%">
                            <asp:Label ID="lblPlantCode1" runat="server" Text=" "></asp:Label>
                        </td>
                        <td width="20%" align="center"  valign="middle">
                            <asp:Label ID="lblAckFlag" runat="server" >ACK Flag:</asp:Label>
                        </td>
                        <td height="10px" align="center" valign="middle" width="30%">
                            <asp:Label ID="lblAckFlag1" runat="server" Text=" "></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellSpacing="0" cellPadding="0" border="0" width="95%" style="height: 115px">
            <tr>
                <td style="width: 30px"></td>
                <td>
                    <asp:Panel ID="panel1" runat="server" Height="10px"></asp:Panel>
                        <asp:GridView ID="gvPO" runat="server"  CssClass="DataGridFont" Font-Size="10px" AutoGenerateColumns="false" 
                               BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="None" PagerSettings-Visible="false" OnRowCreated="gvPO_RowCreated" OnRowDataBound="gvPO_RowDataBound" Height="67px" OnRowCommand="gvPO_RowCommand"  >
                           <Columns>   
                                <asp:TemplateField HeaderText="Index" >
                                     <ItemTemplate>   
                                         <asp:Label ID="lblIndex" runat="server" Width="64px"></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Message ID" >
                                     <ItemTemplate>   
                                         <asp:Label ID="lblMesgid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.MESGID") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:BoundField DataField="MSGTYP" HeaderText="Message Type"></asp:BoundField>
                                <asp:TemplateField HeaderText="Sender ID" >
                                     <ItemTemplate>   
                                         <asp:Label ID="lblSendid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.SENDID") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Receiver ID" >
                                     <ItemTemplate>   
                                         <asp:Label ID="lblReceid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.RECEID") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>                              
                                <asp:TemplateField HeaderText="PONO" >
                                     <ItemTemplate> 
                                         <asp:LinkButton ID="lbtnPoNo" runat="server" CommandName="PONO" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ORDNUM") %>'></asp:LinkButton>
                                     </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:BoundField DataField="ORDFLAG" HeaderText="Order Flag"></asp:BoundField>
                                <asp:BoundField DataField="ORDDAT" HeaderText="PO Date"></asp:BoundField>
                                <asp:BoundField DataField="DLYDAT" HeaderText="Delivery Date"></asp:BoundField>
                                <asp:BoundField DataField="ORD_PRIORITY" HeaderText="Ord_Priority"></asp:BoundField>
                                <asp:BoundField DataField="SHPMOD" HeaderText="Ship Method"></asp:BoundField>
                                <asp:BoundField DataField="PARTSH" HeaderText="Partsh"></asp:BoundField>
                                <asp:BoundField DataField="SCACID" HeaderText="SCAC ID"></asp:BoundField>
                                <asp:BoundField DataField="MCID" HeaderText="Mcid"></asp:BoundField>  
                                <asp:BoundField DataField="CUSNAM" HeaderText="Customer Name"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD1" HeaderText="Customer Address1"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD2" HeaderText="Customer Address2"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD3" HeaderText="Customer Address3"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD4" HeaderText="Customer Address4"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD5" HeaderText="Customer Address5"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD6" HeaderText="Customer Address6"></asp:BoundField>
                                <asp:BoundField DataField="CUSAD7" HeaderText="Customer Address7"></asp:BoundField>  
                                <asp:BoundField DataField="CUSCOO" HeaderText="Cuscoo"></asp:BoundField>
                                <asp:BoundField DataField="CUSPHO" HeaderText="Cuspho"></asp:BoundField>
                                <asp:BoundField DataField="CSONUM" HeaderText="Csonum"></asp:BoundField>
                                <asp:BoundField DataField="CUSNUM" HeaderText="Cusnum"></asp:BoundField>
                                <asp:BoundField DataField="CUSTPONUM" HeaderText="Custponum"></asp:BoundField>
                                <asp:BoundField DataField="CTOMOD" HeaderText="Ctomod"></asp:BoundField>
                                <asp:BoundField DataField="SOTYPE" HeaderText="SO Type"></asp:BoundField>
                                <asp:BoundField DataField="PLANTCODE" HeaderText="PlantCode"></asp:BoundField> 
                                <asp:BoundField DataField="SALESORG" HeaderText="Salesorg"></asp:BoundField>
                                <asp:BoundField DataField="SALESCHANNEL" HeaderText="Saleschannel"></asp:BoundField>
                                <asp:BoundField DataField="SHIPPOINT" HeaderText="Shippoint"></asp:BoundField>
                                <asp:BoundField DataField="SHIPCONDITION" HeaderText="Shipcondition"></asp:BoundField>
                                <asp:BoundField DataField="SALESDIVISION" HeaderText="Salesdivision"></asp:BoundField>
                                <asp:BoundField DataField="PARTNOTYPE" HeaderText="Partnotype"></asp:BoundField>
                                <asp:BoundField DataField="PRIORITY" HeaderText="Priority"></asp:BoundField>
                                <asp:BoundField DataField="ROUTE" HeaderText="Route"></asp:BoundField> 
                                <asp:BoundField DataField="STORAGELOCATION" HeaderText="Storage Location"></asp:BoundField>
                                <asp:BoundField DataField="SHIPTOPARTNER" HeaderText="ShipToPartner"></asp:BoundField>
                                <asp:BoundField DataField="SOLDTOPARTNER" HeaderText="SoldToPartner"></asp:BoundField>
                                <asp:BoundField DataField="INSFCFLAG" HeaderText="In SFC Flag"></asp:BoundField>
                                <asp:BoundField DataField="CREATEDUMMYBOMFLAG" HeaderText="Create Dummy Bom Flag"></asp:BoundField>
                                <asp:BoundField DataField="BOMIDOCNO" HeaderText="Bom Idoc NO"></asp:BoundField>
                                <asp:BoundField DataField="BOOKTIME" HeaderText="Book Time"></asp:BoundField>
                                <asp:BoundField DataField="UPLOADFLAG" HeaderText="Upload Flag"></asp:BoundField>
                                <asp:BoundField DataField="SONO" HeaderText="SO NO"></asp:BoundField> 
                                <asp:BoundField DataField="SODATE" HeaderText="SO Date"></asp:BoundField>
                                <asp:BoundField DataField="IDOCNO" HeaderText="Idoc No"></asp:BoundField>
                                <asp:BoundField DataField="ERRORMSG" HeaderText="Error Message"></asp:BoundField> 
                                <asp:BoundField DataField="acksendflag" HeaderText="Ack Send Flag"></asp:BoundField>
                                <asp:BoundField DataField="acksenddate" HeaderText="Ack Send Date"></asp:BoundField>
                                <asp:BoundField DataField="ackerrormsg" HeaderText="Ack Error Message"></asp:BoundField>                               
                                <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"></asp:BoundField>
                                <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>
                            </Columns>
                            <RowStyle BackColor="#F1F8F1"/>
                            <HeaderStyle  HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />        
                            <PagerSettings Visible="False" />   
                        </asp:GridView>
                </td>
           </tr>
            <tr>
                <td style="height: 27px; width: 30px;"></td>
                <td style="height: 27px">
                    <asp:Panel ID="panel2" runat="server">
                        &nbsp;&nbsp;
                </asp:Panel>
                    &nbsp;
                </td>
            </tr>
    </table>
    <table cellSpacing="0" cellPadding="0" border="0" width="95%" >
            <tr>
            <td width="30" rowspan="2"></td>
                <td>
                    <asp:Panel ID="panel3" runat="server">
                        <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"  align="left" width="95%">
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
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblNoData" Visible="false" Font-Bold="true" Font-Size="20px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
