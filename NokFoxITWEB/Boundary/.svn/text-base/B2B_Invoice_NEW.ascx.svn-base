<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_Invoice_NEW.ascx.cs" Inherits="Boundary_B2B_Invoice_NEW" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td width="50" rowSpan="9"><FONT face="新細明體"></FONT></td>
        <td style="HEIGHT: 18px" ><asp:label id="lblFromDate" runat="server" ToolTip="格式:(yyyy/MM/dd HH:mm)" >From Date:</asp:label></td>
        <td style="WIDTH: 198px; HEIGHT: 18px">
            <uc2:Calendar1 ID="tbStartDate" runat="server"  />
        </td>
        <td style="HEIGHT: 18px"><asp:label id="lbEndDate" runat="server" ToolTip="格式:(yyyy/MM/dd HH:mm)" >To Date:</asp:label></td>
        <td style="HEIGHT: 18px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
        <td  align="center" rowspan="9"> 
            <asp:button id="btnSearch" runat="server" Width="100px" Text="Query" OnClick="btnSearch_Click"></asp:button>
            <br />
            <br />
            <asp:Button id="btnExport" runat="server" Width="100px" Text="Export" OnClick="btnExport_Click1" Visible="false"/>
        </td>
    </tr>
    <tr>
        <td></td>	            
        <td>
            <asp:label id="lblgeshi1" runat="server" Visible="False" ForeColor="Red"></asp:label>
        </td>
        <td></td>
        <td>
            <asp:label id="lblgeshi2" runat="server" Visible="False" ForeColor="Red"></asp:label>
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label6" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">IdocNo:</asp:label></td>
        <td>
            <asp:TextBox ID="txtIdocNo" runat="server"></asp:TextBox>		
        </td>
        <td><asp:label id="Label7" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">Message ID:</asp:label></td>
        <td>
            <asp:TextBox ID="txtMesgid" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label8" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">PO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtPoNo" runat="server" ></asp:TextBox>          		                
        </td>
        <td ><asp:label id="Label11" runat="server" ToolTip="(以逗號區分,例如:000000480,000000481)">LoadID:</asp:label></td>
        <td>
            <asp:TextBox ID="txtLoadid" runat="server" ></asp:TextBox>         
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label12" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">Invoice No:</asp:label></td>
        <td>
            <asp:TextBox ID="txtInvoiceNo" runat="server" ></asp:TextBox>   
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label13" runat="server" Width="100px">SendFlag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlSendFlag" runat="server" Width="80px">
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="E" ></asp:ListItem>	    
            <asp:ListItem Text="H" ></asp:ListItem>	    
            <asp:ListItem Text="N" ></asp:ListItem>	    
            <asp:ListItem Text="P" ></asp:ListItem>	    
            <asp:ListItem Text="Y" ></asp:ListItem>	    
        </asp:dropdownlist></td>
    </tr>	
    <tr> 
        <td><asp:label id="Label15" runat="server" Width="100px">Order By:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlOrder" runat="server" Width="60px"  >
            <asp:ListItem Text="Desc" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Asc" ></asp:ListItem>	    
        </asp:dropdownlist></td>
        <td><asp:label id="Label16" runat="server" Width="100px">Page Rows:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlPageRows" runat="server" Width="50px"  >
            <asp:ListItem Text="20" Selected="True"></asp:ListItem>
            <asp:ListItem Text="50" ></asp:ListItem>
            <asp:ListItem Text="100" ></asp:ListItem>
            <asp:ListItem Text="200" ></asp:ListItem>
            <asp:ListItem Text="500"></asp:ListItem>		    
        </asp:dropdownlist></td>
    </tr>
</table>
<hr />
<asp:GridView ID="gvInvoice" runat="server" Font-Size="10px"
      BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none" AutoGenerateColumns="false"
      GridLines="Both" PagerSettings-Visible="false" AllowPaging="false" OnRowCommand="gvInvoice_RowCommand" OnRowCreated="gvInvoice_RowCreated" OnRowDataBound="gvInvoice_RowDataBound" >
    <Columns>   
        <asp:TemplateField HeaderText="Index" >
             <ItemTemplate>   
                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
             </ItemTemplate>
        </asp:TemplateField> 
        <asp:BoundField DataField="IDOCNO" HeaderText="IdocNO"></asp:BoundField>
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
        <asp:BoundField DataField="TRDPID" HeaderText="TrdPid"></asp:BoundField>
        <asp:BoundField DataField="CUSNUM" HeaderText="CusNum"></asp:BoundField>                                  
        <asp:TemplateField HeaderText="InvoiceNO" >
             <ItemTemplate> 
                 <asp:LinkButton ID="lbtnInvoiceno" runat="server" CommandName="InvoiceNo" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.INVCNM") %>'></asp:LinkButton>
             </ItemTemplate>
        </asp:TemplateField> 
        <asp:BoundField DataField="LOADID" HeaderText="LoadID"></asp:BoundField>
        <asp:BoundField DataField="INVDAT" HeaderText="InvDat"></asp:BoundField>
        <asp:BoundField DataField="CURECY" HeaderText="CureCy"></asp:BoundField>
        <asp:BoundField DataField="INVAMT" HeaderText="InvAmt"></asp:BoundField>
        <asp:BoundField DataField="VATTAX" HeaderText="VatTax"></asp:BoundField>
        <asp:BoundField DataField="ORDNUM" HeaderText="OrdNum"></asp:BoundField>
        <asp:BoundField DataField="SONO" HeaderText="SoNO"></asp:BoundField>
        <asp:BoundField DataField="BOOKTIME" HeaderText="BookTime"></asp:BoundField>  
        <asp:BoundField DataField="SENDFLAG" HeaderText="SendFlag"></asp:BoundField>
        <asp:BoundField DataField="SENDDATE" HeaderText="SendDate"></asp:BoundField>
        <asp:BoundField DataField="ERRORMSG" HeaderText="ErrorMsg"></asp:BoundField> 
        
<%--        <asp:BoundField DataField="ACKFLAG" HeaderText="AckFlag"></asp:BoundField>  
        <asp:BoundField DataField="ACKDATE" HeaderText="AckDate"></asp:BoundField>
        <asp:BoundField DataField="ACKSTATUS" HeaderText="AckStatus"></asp:BoundField>
        <asp:BoundField DataField="ACKMSG" HeaderText="AckMsg"></asp:BoundField> --%>
        
        <asp:BoundField DataField="LASTEDITDT" HeaderText="ProcessDate"></asp:BoundField>            
    </Columns>
    <RowStyle BackColor="#f1f8f1"/>
    <HeaderStyle  HorizontalAlign="Center"/>
    <AlternatingRowStyle BackColor="White" />        
    <PagerSettings Visible="False" />    
</asp:GridView>
<asp:Panel ID="panel3" runat="server" Visible="false">
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td>
            <asp:imagebutton id="btnFirst" AlternateText="First" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-first.gif"  CommandName="Page" CommandArgument="First" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:imagebutton id="btnPrevious" AlternateText="Previous" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-previous.gif"  CommandName="Page" CommandArgument="Previous" OnClick="PagebtnClick"> </asp:imagebutton>
            <asp:imagebutton id="btnNext" AlternateText="Next" runat="server" 
                CausesValidation="false" ImageUrl="..\Images\arrow-next.gif"  
                CommandName="Page" CommandArgument="Next" OnClick="PagebtnClick" Width="17px"></asp:imagebutton>
            <asp:imagebutton id="btnLast" AlternateText="Last" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-last.gif"   CommandName="Page" CommandArgument="Last" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:Label ID="lblNow" runat="server" ></asp:Label> 
            <asp:Label ID="lblTotal" runat="server" ></asp:Label>                 
            <asp:TextBox ID="txtNo" runat="server" Width="80px"></asp:TextBox>
            頁<asp:imagebutton id="btnGo" AlternateText="Go" runat="server" 
                ImageUrl="..\Images\go.gif" CausesValidation="false" CommandArgument="-1" 
                CommandName="Page" OnClick="PagebtnClick" style="height: 15px"></asp:imagebutton>

        </td>
    </tr>
</table>
</asp:Panel>