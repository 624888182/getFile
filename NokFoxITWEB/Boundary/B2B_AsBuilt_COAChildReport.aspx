<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_AsBuilt_COAChildReport.aspx.cs" Inherits="Boundary_B2B_AsBuilt_COAChildReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_AsBuilt_COA & ChildReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell AsBuilt COA & Child Report</asp:Label>
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
            <asp:Label ID="lblAsbuiltCOA" runat="server" ForeColor="blue" Font-Size="20px" Text="Asbuilt COA " Font-Bold="true"></asp:Label>  
            <asp:GridView ID="gvAsbuiltCOA" runat="server" Font-Size="10px"  AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvAsbuiltCOA_RowCreated" OnRowDataBound="gvAsbuiltCOA_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>                         
                    </asp:TemplateField>  
                    <asp:BoundField DataField="BATCHNO" HeaderText="batchno"></asp:BoundField>
                    <asp:BoundField DataField="SENDERID" HeaderText="senderid"></asp:BoundField>
                    <asp:BoundField DataField="RECEIVERID" HeaderText="receiverid"></asp:BoundField>
                    <asp:BoundField DataField="DOCUMENTID" HeaderText="documentid"></asp:BoundField>
                    <asp:BoundField DataField="PARENTPPID" HeaderText="parentppid"></asp:BoundField>
                    <asp:BoundField DataField="COAPART" HeaderText="coapart"></asp:BoundField> 
                    <asp:BoundField DataField="COASTRING" HeaderText="coastring"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITBY" HeaderText="lasteditby"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdt"></asp:BoundField>   
                </Columns>
                <RowStyle BackColor="#f1f8f1"/>
                <HeaderStyle  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>
            <asp:Label ID="lblAsbuiltChild" runat="server" ForeColor="blue" Font-Size="20px" Font-Bold="true" Text="Asbuilt Child"></asp:Label>  
            <asp:GridView ID="gvTioLabel" runat="server"  CssClass="DataGridFont" Font-Size="10px"  AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none"
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvTioLabel_RowCreated" OnRowDataBound="gvTioLabel_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex1" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="BATCHNO" HeaderText="batchno"></asp:BoundField>
                    <asp:BoundField DataField="SENDERID" HeaderText="senderid"></asp:BoundField>
                    <asp:BoundField DataField="RECEIVERID" HeaderText="receiverid"></asp:BoundField>
                    <asp:BoundField DataField="DOCUMENTID" HeaderText="documentid"></asp:BoundField>
                    <asp:BoundField DataField="PARENTPPID" HeaderText="parentppid"></asp:BoundField>
                    <asp:BoundField DataField="CHILDPPID" HeaderText="childppid"></asp:BoundField> 
                    <asp:BoundField DataField="CHILDREV" HeaderText="childrev"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITBY" HeaderText="lasteditby"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdt"></asp:BoundField>                             
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
