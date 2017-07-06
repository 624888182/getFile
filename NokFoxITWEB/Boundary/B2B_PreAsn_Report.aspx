<%@ Page Language="C#" AutoEventWireup="true" CodeFile="B2B_PreAsn_Report.aspx.cs" Inherits="Boundary_B2B_PreAsn_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>B2B_PREASN Report</title>
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
                <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell PreAsn Report</asp:Label>
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
            <td width="20%" align="center"  valign="middle" style="height: 21px">
                <asp:Label ID="lblPoNo" runat="server" >PONO:</asp:Label>
            </td>
            <td align="center" valign="middle" width="30%" style="height: 21px">
                <asp:Label ID="lblPoNo1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle" style="height: 21px">
                <asp:Label ID="lblBatchNo" runat="server" >BatchNo:</asp:Label>
            </td>
            <td align="center" valign="middle" width="30%" style="height: 21px" >
                <asp:Label ID="lblBatchNo1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblMesgID" runat="server" >MesgID:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblMesgID1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblLoadID" runat="server" >LoadID:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblLoadID1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblSendFlag" runat="server" >SendFlag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblSendFlag1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblACKFlag" runat="server" >ACKFlag:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%" >
                <asp:Label ID="lblACKFlag1" runat="server" Text=" "></asp:Label>
            </td>
        </tr>
        <tr >
            <td width="20%" align="center"  valign="middle">
                <asp:Label ID="lblPalletID" runat="server" >PalletID:</asp:Label>
            </td>
            <td height="10px" align="center" valign="middle" width="30%">
                <asp:Label ID="lblPalletID1" runat="server" Text=" "></asp:Label>
            </td>
            <td width="20%" align="center"  valign="middle"></td>
            <td height="10px" align="center" valign="middle" width="30%" ></td>
        </tr>
    </table>
<asp:Panel ID="panel1" runat="server" Height="10px"></asp:Panel>		      
<asp:GridView ID="gvPreAsn" runat="server" Font-Size="10px"  AutoGenerateColumns="false"
      BackColor="White" UserID="Any"  Font-Names="Verdana" BorderStyle="none"
      GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvPreAsn_RowCreated" OnRowDataBound="gvPreAsn_RowDataBound" OnRowCommand="gvPreAsn_RowCommand" >
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
        <asp:BoundField DataField="MESGID" HeaderText="MesgID"></asp:BoundField>
        <asp:BoundField DataField="MSGTYP" HeaderText="MsgType"></asp:BoundField>
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
                 <asp:LinkButton ID="lbtnLoadid" runat="server" CommandName="LoadID" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.LOADID") %>'></asp:LinkButton>
             </ItemTemplate>
        </asp:TemplateField> 
        <asp:BoundField DataField="LOADQT" HeaderText="LoadQty"></asp:BoundField>
        <asp:BoundField DataField="CUSNUM" HeaderText="CusNum"></asp:BoundField>
        <asp:BoundField DataField="SCACID" HeaderText="ScacID"></asp:BoundField>
        <asp:BoundField DataField="TRKMAS" HeaderText="TrkMas"></asp:BoundField>
        <asp:BoundField DataField="SHIP_MODE" HeaderText="Ship_Mode"></asp:BoundField>
        <asp:BoundField DataField="SHPDAT" HeaderText="Ship Date"></asp:BoundField>
        <asp:BoundField DataField="PALNUM" HeaderText="PalNum"></asp:BoundField>
        <asp:BoundField DataField="PALTOT" HeaderText="PalTot"></asp:BoundField>  
        <asp:BoundField DataField="PALLET_ID" HeaderText="Pallet_ID"></asp:BoundField>
        <asp:BoundField DataField="MANFID" HeaderText="Manfid"></asp:BoundField>
        <asp:BoundField DataField="MCID" HeaderText="Mcid"></asp:BoundField>
        <asp:BoundField DataField="MFG_LOC" HeaderText="Mfg_loc"></asp:BoundField>
        <asp:BoundField DataField="PLT_GROSS_WT" HeaderText="Plt_gross_wt"></asp:BoundField>
        <asp:BoundField DataField="PLT_VOL_WT" HeaderText="Plt_vol_wt"></asp:BoundField>
        <asp:BoundField DataField="PLT_LENGTH" HeaderText="Plt_length"></asp:BoundField>
        <asp:BoundField DataField="PLT_WIDTH" HeaderText="Plt_width"></asp:BoundField>  
        <asp:BoundField DataField="PLT_HEIGHT" HeaderText="Plt_height"></asp:BoundField>
        <asp:BoundField DataField="VALIDATEFLAG" HeaderText="ValidateFlag"></asp:BoundField>
        <asp:BoundField DataField="ERRORMSG" HeaderText="ErrorMsg"></asp:BoundField>
        <asp:BoundField DataField="BOOKTIME" HeaderText="BookTime"></asp:BoundField>
        <asp:BoundField DataField="SENDFLAG" HeaderText="SendFlag"></asp:BoundField>
        <asp:BoundField DataField="SENDDATE" HeaderText="SendDate"></asp:BoundField>
        <asp:BoundField DataField="ACKSTATUS" HeaderText="ACK Status"></asp:BoundField>
        <asp:BoundField DataField="ACKFLAG" HeaderText="ACK Flag"></asp:BoundField> 
        <asp:BoundField DataField="preasnackreceivedt" HeaderText="ACK Receive Date"></asp:BoundField>
        <asp:BoundField DataField="processdate" HeaderText="Process Date"></asp:BoundField>
    </Columns>
    <RowStyle HorizontalAlign="Center" BackColor="#f1f8f1"/>
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
