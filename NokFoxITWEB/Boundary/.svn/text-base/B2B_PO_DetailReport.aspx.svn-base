<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_PO_DetailReport.aspx.cs" Inherits="Boundary_B2B_PO_DetailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_PO_DetailReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell PO Item Detail Report</asp:Label>
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
            <asp:GridView ID="gvPODetail" runat="server" Font-Size="10px" AutoGenerateColumns="false" 
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvPODetail_RowCreated" OnRowDataBound="gvPODetail_RowDataBound"  >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SENDID" HeaderText="SendID"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="Receiver ID"></asp:BoundField>
                    <asp:BoundField DataField="MESGID" HeaderText="Message ID"></asp:BoundField>
                    <asp:BoundField DataField="ORDNUM" HeaderText="PONO"></asp:BoundField>
                    <asp:BoundField DataField="ITMNUM" HeaderText="Item Number"></asp:BoundField>
                    <asp:BoundField DataField="SUBITM" HeaderText="Subitem"></asp:BoundField>
                    <asp:BoundField DataField="CLIMAT" HeaderText="Climat"></asp:BoundField>
                    <asp:BoundField DataField="MATDES" HeaderText="Matdes"></asp:BoundField>                     
                    <asp:BoundField DataField="ORDQTY" HeaderText="Order Qty"></asp:BoundField>
                    <asp:BoundField DataField="ITEMCATEGORY" HeaderText="Itemcategory"></asp:BoundField>
                    <asp:BoundField DataField="HIGHLEVEL" HeaderText="High Level"></asp:BoundField>
                    <asp:BoundField DataField="SOITMNUM" HeaderText="SO Item Number"></asp:BoundField>
                    <asp:BoundField DataField="PART_CODE" HeaderText="Part Code"></asp:BoundField>                     
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>                                                     
                </Columns>
                <RowStyle HorizontalAlign="Center" BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center"/>
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
