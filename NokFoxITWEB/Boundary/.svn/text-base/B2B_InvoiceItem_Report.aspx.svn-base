<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_InvoiceItem_Report.aspx.cs" Inherits="Boundary_B2B_InvoiceItem_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>B2B_InvoiceItem Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell Invoice Item & Processing Status Report</asp:Label>
            </td>
            <td valign="middle" width="50%" style="height: 47px">
                <table height="100%">
                    <tr>                       
                        
                        <td  align="right" style="width: 100%; height: 10px;">
                            <asp:imagebutton id="btnClose" runat="server" ImageUrl="..\Images\close.gif"  AlternateText="Close" OnClick="btnClose_Click" ></asp:imagebutton>
                        </td>
                        <td align="left" style="height: 10px">
                            <asp:Label ID="lblClose" runat="server" >Close</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<table id="tb1" class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" align="center" width="95%">
    <tr>
        <td> 
        
            <asp:Label ID="lblItem" runat="server" ForeColor="blue" Font-Size="20px" Font-Bold="true" Text="Invoice Item"></asp:Label>
            <asp:GridView ID="gvInvoiceItem" runat="server" Font-Size="10px" AutoGenerateColumns="false" 
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvInvoiceItem_RowCreated" OnRowDataBound="gvInvoiceItem_RowDataBound">
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:BoundField DataField="SENDID" HeaderText="SenderID"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="ReceiverID"></asp:BoundField>
                    <asp:BoundField DataField="INVCNM" HeaderText="InvoiceNO"></asp:BoundField>
                    <asp:BoundField DataField="SEQNUM" HeaderText="SeqNO"></asp:BoundField>
                    <asp:BoundField DataField="PACKLT" HeaderText="PackLT"></asp:BoundField>
                    <asp:BoundField DataField="CLIMAT" HeaderText="CliMat"></asp:BoundField>
                    <asp:BoundField DataField="ITMQTY" HeaderText="ItmQty"></asp:BoundField>
                    <asp:BoundField DataField="UPRICE" HeaderText="UPrice"></asp:BoundField>
                    <asp:BoundField DataField="TPRICE" HeaderText="TPrice"></asp:BoundField>
                    <asp:BoundField DataField="ITEMCATEGORY" HeaderText="ItemCategory"></asp:BoundField>
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="ProcessDate"></asp:BoundField>                                     
                </Columns>
                <RowStyle HorizontalAlign="Center" BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>  
            
            <asp:Label ID="lblprocessstatus" runat="server" ForeColor="blue" Font-Size="20px" Font-Bold="true" Text="Invoice Processing Status"></asp:Label>  
            <asp:GridView ID="gvStatus" runat="server"  CssClass="DataGridFont" Font-Size="10px"  AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none"
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvStatus_RowCreated" OnRowDataBound="gvStatus_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex1" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="MESGID" HeaderText="MESG ID"></asp:BoundField>
                    <asp:BoundField DataField="SENDID" HeaderText="Sender ID"></asp:BoundField>
                    <asp:BoundField DataField="MSGTYP" HeaderText="Msg. Type"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="Receiver ID"></asp:BoundField>
                    <asp:BoundField DataField="EDI_INV" HeaderText="INVOICE#"></asp:BoundField>
                    <asp:BoundField DataField="EDI_INV_LINE" HeaderText="Inv. Line"></asp:BoundField> 
                    <asp:BoundField DataField="TRADING_PARTNER_ID" HeaderText="Trading Partner ID"></asp:BoundField>       
                    <asp:BoundField DataField="VENDOR" HeaderText="Vendor Code"></asp:BoundField>
                    <asp:BoundField DataField="VEN_LOC" HeaderText="Vend. Location code"></asp:BoundField>
                    <asp:BoundField DataField="NAME" HeaderText="Vendor Name"></asp:BoundField>
                    <asp:BoundField DataField="VEN_INV" HeaderText="Vendor Invoice#"></asp:BoundField>
                    <asp:BoundField DataField="VEN_INV_DATE" HeaderText="Vendor Invoice Date"></asp:BoundField>
                    <asp:BoundField DataField="INV_AMT" HeaderText="Invoice Amt"></asp:BoundField>
                    <asp:BoundField DataField="EDI_PO" HeaderText="Dell BP/O"></asp:BoundField>
                    <asp:BoundField DataField="EDI_TXN_DATE" HeaderText="EDI TXN Date"></asp:BoundField>
                    <asp:BoundField DataField="SHIP_QTY" HeaderText="Shipped QTY"></asp:BoundField>
                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price"></asp:BoundField>
                    <asp:BoundField DataField="EXT_AMT" HeaderText="Ext. Amt"></asp:BoundField>
                    <asp:BoundField DataField="ITEM" HeaderText="Item Code"></asp:BoundField>
                    <asp:BoundField DataField="PACKLIST" HeaderText="PackList#"></asp:BoundField>
                    <asp:BoundField DataField="ACCEPT" HeaderText="Invoice Status"></asp:BoundField>
                    <asp:BoundField DataField="REASON" HeaderText="Reason"></asp:BoundField>                    
                    <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>                             
                </Columns>
                <RowStyle   BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />   
            </asp:GridView>          
                     
        </td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
