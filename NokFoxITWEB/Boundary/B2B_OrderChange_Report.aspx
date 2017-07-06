<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_OrderChange_Report.aspx.cs" Inherits="Boundary_B2B_OrderChange_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_OrderChange Report</title>
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
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell OrderChange Report</asp:Label>
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
                <asp:Label ID="lblMesgID" runat="server" >Message ID:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
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
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblReqChange" runat="server" >Req_Change:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblReqChange1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblUploadFlag" runat="server" >Upload Flag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblUploadFlag1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblChangeFlag" runat="server" >Change Flag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblChangeFlag1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblAckSendFlag" runat="server" >ACK Send Flag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblAckSendFlag1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
    </table>
    <table id="tb1" class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" align="center" width="95%">
    <tr>
        <td> 	      
<asp:GridView ID="gvOrderChange" runat="server" Font-Size="10px" AutoGenerateColumns="false" 
      BackColor="White" UserID="Any"  BorderStyle="none" Width="95%"  
      GridLines="Both" PagerSettings-Visible="false" AllowPaging="false" OnRowCreated="gvOrderChange_RowCreated" OnRowDataBound="gvOrderChange_RowDataBound" >
   <Columns>   
        <asp:TemplateField HeaderText="Index" >
             <ItemTemplate>   
                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
             </ItemTemplate>
        </asp:TemplateField>  
        <asp:BoundField DataField="MESGID" HeaderText="Message ID"></asp:BoundField>
        <asp:BoundField DataField="MSGTYP" HeaderText="Message Type"></asp:BoundField>
        <asp:BoundField DataField="SENDID" HeaderText="Sender ID"></asp:BoundField>
        <asp:BoundField DataField="RECEID" HeaderText="Receiver ID"></asp:BoundField>
        <asp:BoundField DataField="ORDNUM" HeaderText="PONO"></asp:BoundField>
        <asp:BoundField DataField="ORDDAT" HeaderText="PO Date"></asp:BoundField>
        <asp:BoundField DataField="REQ_DATE" HeaderText="Required Date"></asp:BoundField>  
        <asp:BoundField DataField="REQ_CHANGE" HeaderText="Req_change"></asp:BoundField>
        <asp:BoundField DataField="REQ_ACTION" HeaderText="Req_action"></asp:BoundField>
        <asp:BoundField DataField="ORD_PRIORITY" HeaderText="Ord_priority"></asp:BoundField>
        <asp:BoundField DataField="DLYDAT" HeaderText="Dlydat"></asp:BoundField>
        <asp:BoundField DataField="COMMENT_LINE1" HeaderText="comment_line1"></asp:BoundField>
        <asp:BoundField DataField="COMMENT_LINE2" HeaderText="comment_line2"></asp:BoundField>
        <asp:BoundField DataField="COMMENT_LINE3" HeaderText="comment_line3"></asp:BoundField>
        <asp:BoundField DataField="BOOKTIME" HeaderText="booktime"></asp:BoundField>
        <asp:BoundField DataField="CHANGEFLAG" HeaderText="changeflag"></asp:BoundField>
        <asp:BoundField DataField="CHANGEDATE" HeaderText="changedate"></asp:BoundField>
        <asp:BoundField DataField="UPLOADFLAG" HeaderText="uploadflag"></asp:BoundField>  
        <asp:BoundField DataField="ERRORMSG" HeaderText="errormsg"></asp:BoundField>
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
        <asp:BoundField DataField="LASTEDITBY" HeaderText="lasteditby"></asp:BoundField>                 
        <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdate"></asp:BoundField>         
        <asp:BoundField DataField="acksenddate" HeaderText="acksenddate"></asp:BoundField> 
        <asp:BoundField DataField="acksenddate" HeaderText="acksenddate"></asp:BoundField>
        <asp:BoundField DataField="ack_action" HeaderText="ack_action"></asp:BoundField>   
        <asp:BoundField DataField="ackreason" HeaderText="ackreason"></asp:BoundField>   
        <asp:BoundField DataField="ackreason_desc" HeaderText="ackreason_desc"></asp:BoundField> 
    </Columns>
    <RowStyle BackColor="#f1f8f1" Font-Size="15px"/>
    <HeaderStyle  HorizontalAlign="Center" Font-Bold="false" Height="15px"/>
    <AlternatingRowStyle BackColor="White" />        
</asp:GridView>
</td>
</tr>
</table>
<asp:Panel ID="panel2" runat="server" Visible="false" >
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
