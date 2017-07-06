<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_Label_DetailReport.aspx.cs" Inherits="Boundary_B2B_Label_DetailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_Label_DetailReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell Label Detail Report</asp:Label>
            </td>
            <td valign="middle" width="50%" style="height: 47px">
                <table height="100%">
                    <tr>                       
                        
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
<table id="tb1" class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" align="center" width="95%">
    <tr>
        <td> 
            <asp:GridView ID="gvLabelDetail" runat="server" Font-Size="10px" AutoGenerateColumns="false" 
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvLabelDetail_RowCreated" OnRowDataBound="gvLabelDetail_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                     
                    <asp:BoundField DataField="MESGID" HeaderText="Message ID"></asp:BoundField> 
                    <asp:BoundField DataField="MESGTYP" HeaderText="MsgType"></asp:BoundField>
                    <asp:BoundField DataField="SENDID" HeaderText="SenderID"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="ReceiverID"></asp:BoundField>
                    <asp:BoundField DataField="ORDNUM" HeaderText="OrdNO"></asp:BoundField>
                    <asp:BoundField DataField="BUID" HeaderText="buid"></asp:BoundField>
                    <asp:BoundField DataField="CSONUM" HeaderText="csonum"></asp:BoundField>
                    <asp:BoundField DataField="BOX_NUM" HeaderText="box_num"></asp:BoundField>
                    <asp:BoundField DataField="TIEGROUP" HeaderText="tiegroup"></asp:BoundField>                    
                    <asp:BoundField DataField="SSIC_NUMBER" HeaderText="ssic_number"></asp:BoundField>
                    <asp:BoundField DataField="TRACKING_NUMBER" HeaderText="tracking_number"></asp:BoundField>                    
                    <asp:BoundField DataField="SHIPMENT_CODE" HeaderText="shipment_code"></asp:BoundField>
                    <asp:BoundField DataField="CONSIGNMENT_NUMBER" HeaderText="consignment_number"></asp:BoundField>
                    <asp:BoundField DataField="PARCEL_NUMBER" HeaderText="parcle_number"></asp:BoundField>                    
                    <asp:BoundField DataField="BOX_BARCODE" HeaderText="box_barcode"></asp:BoundField>
                    <asp:BoundField DataField="REGION" HeaderText="region"></asp:BoundField>                         
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdate"></asp:BoundField>  
                </Columns>
                <RowStyle BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>           
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNoData" Font-Size="20px" Font-Bold="true" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
