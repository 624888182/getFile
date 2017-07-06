<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_PO_ItemReport.aspx.cs" Inherits="Boundary_B2B_PO_ItemReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_PO_ItemReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell PO Item Report</asp:Label>
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
            <asp:GridView ID="gvPOItem" runat="server" Font-Size="10px" AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvPOItem_RowCreated" OnRowDataBound="gvPOItem_RowDataBound" OnRowCommand="gvPOItem_RowCommand" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="sendID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblSendif" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.SENDID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Receiver ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblReceid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.RECEID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>     
                    
                    <asp:TemplateField HeaderText="mesgID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblMesgid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.MESGID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="ordnum" >
                         <ItemTemplate>   
                             <asp:Label ID="lblOrdnum" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.ORDNUM") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                           
                    <asp:TemplateField HeaderText="Item Num" >
                         <ItemTemplate> 
                             <asp:LinkButton ID="lbtnItemnum" runat="server" CommandName="ItemNum" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ITMNUM") %>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="CLIMAT" HeaderText="climat"></asp:BoundField>
                    <asp:BoundField DataField="MATDES" HeaderText="matdes"></asp:BoundField>
                    <asp:BoundField DataField="ORDQTY" HeaderText="ordqty"></asp:BoundField>
                    <asp:BoundField DataField="UPRICE" HeaderText="uprice"></asp:BoundField>
                    <asp:BoundField DataField="ITEMCATEGORY" HeaderText="itemcategory"></asp:BoundField>
                    <asp:BoundField DataField="HIGHLEVEL" HeaderText="highlevel"></asp:BoundField>
                    <asp:BoundField DataField="SOITMNUM" HeaderText="soitemnum"></asp:BoundField>
                    <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"></asp:BoundField>  
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>                                     
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
