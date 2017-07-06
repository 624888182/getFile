<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_ASN_ItemReport.aspx.cs" Inherits="Boundary_B2B_ASN_ItemReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>B2B_ASN_ItemReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell ASN Item Report</asp:Label>
            </td>
            <td valign="middle" width="50%" style="height: 47px">
                <table height="100%">
                    <tr>  
                        <td  align="right" style="width: 100%; height: 10px;">
                            <asp:imagebutton id="btnClose" runat="server" ImageUrl="..\Images\close.gif"  AlternateText="Close" OnClick="btnClose_Click"></asp:imagebutton>
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
            <asp:GridView ID="gvASNItem" runat="server" Font-Size="10px" AutoGenerateColumns="false" 
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCommand="gvASNItem_RowCommand" OnRowCreated="gvASNItem_RowCreated" OnRowDataBound="gvASNItem_RowDataBound">
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="BatchNO" >
                         <ItemTemplate>   
                             <asp:Label ID="lblBatchno" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.BATCHNO") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="LoadID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblLoadid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.LOADID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Pallet_ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblPalletid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.PALLET_ID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemNO" >
                         <ItemTemplate> 
                             <asp:LinkButton ID="lbtnItemno" runat="server" CommandName="ItemNo" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ITMNUM") %>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="ORDNUM" HeaderText="OrdNO"></asp:BoundField>
                    <asp:BoundField DataField="CSONUM" HeaderText="CsoNO"></asp:BoundField>
                    <asp:BoundField DataField="SERNUM" HeaderText="SerNO"></asp:BoundField>
                    <asp:BoundField DataField="MSPPID" HeaderText="MspPID"></asp:BoundField>
                    <asp:BoundField DataField="TIEGROUP" HeaderText="TieGroup"></asp:BoundField>
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="ProcessDate"></asp:BoundField>                                     
                </Columns>
                <RowStyle BackColor="#f1f8f1"/>
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
