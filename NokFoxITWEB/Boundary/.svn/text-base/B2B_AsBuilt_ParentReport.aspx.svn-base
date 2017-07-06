<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_AsBuilt_ParentReport.aspx.cs" Inherits="Boundary_B2B_AsBuilt_ParentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>B2B_AsBuilt_ParentReport</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1" align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td style="height: 47px">
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell AsBuilt Parent Report</asp:Label>
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
        <td style="width: 838px"> 
            <asp:GridView ID="gvAsbuiltParent" runat="server" Font-Size="10px"  AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" Width="95%"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCommand="gvAsbuiltParent_RowCommand" OnRowCreated="gvAsbuiltParent_RowCreated" OnRowDataBound="gvAsbuiltParent_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Batch NO" >
                         <ItemTemplate> 
                             <asp:Label ID="lblBatchno" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.BATCHNO") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sender ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblSendid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.SENDERID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Receiver ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblReceid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.RECEIVERID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                       
                    <asp:TemplateField HeaderText="Document ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblDocumentid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.DOCUMENTID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                                                 
                    <asp:TemplateField HeaderText="Parent PPID" >
                         <ItemTemplate> 
                             <asp:LinkButton ID="lbtnParentPPID" runat="server" CommandName="ParentPPID" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.PARENTPPID") %>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARENTREV" HeaderText="Parentrev"></asp:BoundField>
                    <asp:BoundField DataField="SERVICETAG" HeaderText="Service Tag"></asp:BoundField>
                    <asp:BoundField DataField="ORDERNUMBER" HeaderText="Order Number"></asp:BoundField>
                    <asp:BoundField DataField="CONFIGID" HeaderText="Config ID"></asp:BoundField>
                    <asp:BoundField DataField="TIEGROUP" HeaderText="Tie Group"></asp:BoundField>
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
