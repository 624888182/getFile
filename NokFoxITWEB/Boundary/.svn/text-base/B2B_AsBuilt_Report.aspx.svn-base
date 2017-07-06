<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_AsBuilt_Report.aspx.cs" Inherits="Boundary_B2B_AsBuilt_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_AsBuilt Report</title>
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
    <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1"  align="center" width="95%" bordercolor="#66cc99">
        <tr width="50%">
            <td>
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell AsBuilt Report</asp:Label>
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
                <asp:Label ID="lblPoNo" runat="server" >PONO:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblPoNo1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblBatchNo" runat="server" >Batch No:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblBatchNo1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblMsgID" runat="server" >Message ID:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblMsgID1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblSendFlag" runat="server" >Send Flag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblSendFlag1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblValidateFlag" runat="server" >Validate Flag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblValidateFlag1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle"></td>
            <td height="10px" align="center" valign="middle" width="30%" ></td>
        </tr>        
    </table>
<asp:Panel ID="panel1" runat="server" Height="10px"></asp:Panel>		      
<asp:GridView ID="gvAsBuilt" runat="server"  Font-Size="10px"  AutoGenerateColumns="false" 
      BackColor="White" UserID="Any" Font-Names="Verdana" BorderStyle="none"
      GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowDataBound="gvAsBuilt_RowDataBound" OnRowCreated="gvAsBuilt_RowCreated" OnRowCommand="gvAsBuilt_RowCommand">
   <Columns>   
        <asp:TemplateField HeaderText="Index" >
             <ItemTemplate>   
                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
             </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="Batch NO" >
             <ItemTemplate> 
                 <asp:LinkButton ID="lbtnBatchno" runat="server" CommandName="BatchNo" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.BATCHNO") %>'></asp:LinkButton>
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
        <asp:BoundField DataField="VENDORNAME" HeaderText="Vendor Name"></asp:BoundField>
        <asp:BoundField DataField="MSGTYPE" HeaderText="Message Type"></asp:BoundField>
        <asp:BoundField DataField="SHIPDEST" HeaderText="Shipdest"></asp:BoundField>
        <asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
        <asp:BoundField DataField="TRANSMITTALDATE" HeaderText="Transmittaldate"></asp:BoundField>
        <asp:BoundField DataField="BOOKTIME" HeaderText="Book Time"></asp:BoundField>
        <asp:BoundField DataField="SENDFLAG" HeaderText="Send Flag"></asp:BoundField>  
        <asp:BoundField DataField="SENDDATE" HeaderText="SendDate"></asp:BoundField>
        <asp:BoundField DataField="VALIDATEFLAG" HeaderText="Validate Flag"></asp:BoundField>
        <asp:BoundField DataField="ERRORMSG" HeaderText="Error Msg"></asp:BoundField>
        <asp:BoundField DataField="ACKFLAG" HeaderText="Ack Flag"></asp:BoundField>
        <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"></asp:BoundField>                 
        <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>                          
    </Columns>
    <RowStyle BackColor="#f1f8f1"/>
    <HeaderStyle  HorizontalAlign="Center"/>
    <AlternatingRowStyle BackColor="White" />        
    <PagerSettings Visible="False" />      
</asp:GridView>
<asp:Panel ID="panel3" runat="server">
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"  align="center" width="95%">
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
    </div>
    </form>
</body>
</html>
