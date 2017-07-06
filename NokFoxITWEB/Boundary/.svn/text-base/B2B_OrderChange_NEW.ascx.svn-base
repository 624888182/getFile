<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_OrderChange_NEW.ascx.cs" Inherits="Boundary_B2B_OrderChange_NEW" %>
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
            <asp:Button id="btnExport" runat="server" Width="100px" Text="Export" Visible="false" OnClick="btnExport_Click"/>
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
        <td><asp:label id="Label7" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">Message ID:</asp:label></td>
        <td>
            <asp:TextBox ID="txtMesgid" runat="server"></asp:TextBox>
        </td>
        <td><asp:label id="Label8" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">PO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtPoNo" runat="server" ></asp:TextBox>          		                
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label3" runat="server" Width="100px">REQ_Change:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlReq_change" runat="server" Width="60px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="C" ></asp:ListItem>	    
        </asp:dropdownlist></td>
        <td><asp:label id="Label4" runat="server" Width="100px">Upload Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlUploadFlag" runat="server" Width="60px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="P" ></asp:ListItem>
            <asp:ListItem Text="Y" ></asp:ListItem>
            <asp:ListItem Text="E" ></asp:ListItem>
            <asp:ListItem Text="H"></asp:ListItem>		    
        </asp:dropdownlist></td>
    </tr>
    <tr>
        <td><asp:label id="Label1" runat="server" Width="100px">Change Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlChangeFlag" runat="server" Width="60px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Success" ></asp:ListItem>	   
            <asp:ListItem Text="Failure" ></asp:ListItem>	 
        </asp:dropdownlist></td>
        <td><asp:label id="Label2" runat="server" Width="100px">ACK Send Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlAckSendFlag" runat="server" Width="50px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N" ></asp:ListItem>
            <asp:ListItem Text="Y" ></asp:ListItem>
            <asp:ListItem Text="E" ></asp:ListItem>	    
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
<asp:GridView ID="gvOrderChange" runat="server" AutoGenerateColumns="false" 
      BackColor="White" UserID="Any"  BorderStyle="none"   Font-Names="Verdana"  Font-Size="10px"
      GridLines="Both" PagerSettings-Visible="false" AllowPaging="false" OnRowCreated="gvOrderChange_RowCreated" OnRowDataBound="gvOrderChange_RowDataBound">
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
    <RowStyle BackColor="#f1f8f1"/>
    <HeaderStyle  HorizontalAlign="Center"/>
    <AlternatingRowStyle BackColor="White" />        
</asp:GridView>
<asp:Panel ID="panel3" runat="server" Visible="false" >
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
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