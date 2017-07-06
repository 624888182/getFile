<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_AsBuilt_NEW.ascx.cs" Inherits="Boundary_B2B_AsBuilt_NEW" %>
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
            <asp:Button id="btnExport" runat="server" Width="100px" Text="Export" OnClick="btnExport_Click" Visible="false"/>
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
        <td><asp:label id="Label6" runat="server" ToolTip="(以逗號區分,例如:000000480,000000481)">PO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtPoNo" runat="server"></asp:TextBox>		
        </td>
        <td><asp:label id="Label7" runat="server" ToolTip="(以逗號區分,例如:000000480,000000481)">Batch NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtBatcNo" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label8" runat="server" ToolTip="(以逗號區分,例如:000000480,000000481)">Document ID:</asp:label></td>
        <td>
            <asp:TextBox ID="txtMesgID" runat="server" ></asp:TextBox>          		                
        </td>
        <td><asp:label id="Label91" runat="server">Ship Dest:</asp:label></td>
        <td>
            <asp:TextBox ID="txtShipDest" runat="server" ></asp:TextBox>          		                
        </td>         
    </tr>
    <tr> 
        <td style="HEIGHT: 18px" ><asp:label id="Label9" runat="server" >Send Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlSendFlag" runat="server" Width="80px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N-New" ></asp:ListItem>
            <asp:ListItem Text="Y-Sent" ></asp:ListItem>
            <asp:ListItem Text="E-Error" ></asp:ListItem>
            <asp:ListItem Text="H-Hold"></asp:ListItem>		    
        </asp:dropdownlist></td>
        <td style="HEIGHT: 18px"><asp:label id="Label14" runat="server" >Validate Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlValidateFlag" runat="server" Width="80px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N-New" ></asp:ListItem>
            <asp:ListItem Text="Y-Sent" ></asp:ListItem>
            <asp:ListItem Text="E-Error" ></asp:ListItem>
            <asp:ListItem Text="H-Hold"></asp:ListItem>		    
        </asp:dropdownlist></td>
    </tr>	
    
    <tr> 
        <td style="HEIGHT: 18px" ><asp:label id="Label17" runat="server" >ACK Status:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlACKStatus" runat="server" Width="80px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Y" ></asp:ListItem>
            <asp:ListItem Text="N" ></asp:ListItem>
            <asp:ListItem Text="R" ></asp:ListItem> 	    
        </asp:dropdownlist></td>         
    </tr>
    
    <tr> 
        <td style="HEIGHT: 18px"><asp:label id="Label15" runat="server" >Order By:</asp:label></td>
        <td >
        <asp:dropdownlist id="ddlOrder" runat="server" Width="60px"  >
            <asp:ListItem Text="Desc" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Asc" ></asp:ListItem>	    
        </asp:dropdownlist></td>
        <td style="HEIGHT: 18px" ><asp:label id="Label16" runat="server">Page Rows:</asp:label></td>
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
<hr>
<asp:GridView ID="gvAsBuilt" runat="server"  Font-Size="10px"  AutoGenerateColumns="false" 
      BackColor="White" UserID="Any" Font-Names="Verdana" BorderStyle="none"
      GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCommand="gvAsBuilt_RowCommand" OnRowCreated="gvAsBuilt_RowCreated" OnRowDataBound="gvAsBuilt_RowDataBound" >
   <Columns>   
        <asp:TemplateField HeaderText="Index" >
             <ItemTemplate>   
                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
             </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="Batch NO" >
             <ItemTemplate> 
                 <asp:Label ID="lblBatchno" runat="server" Width="64px" Text='<%# DataBinder.Eval(Container, "DataItem.BATCHNO") %>'></asp:Label>
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
                 <%--<asp:Label ID="lblDocumentid" runat="server" Width="64px"  Text='<%# DataBinder.Eval(Container, "DataItem.DOCUMENTID") %>'></asp:Label>--%>
                 <asp:LinkButton ID="lbtnDocumentid" runat="server" CommandName="Documentid" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.DOCUMENTID") %>'></asp:LinkButton>
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
        <asp:BoundField DataField="ACKSTATUS" HeaderText="Ack Status"></asp:BoundField>
        <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"></asp:BoundField>                 
        <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"></asp:BoundField>                          
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