<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_Label_NEW.ascx.cs" Inherits="Boundary_B2B_Label_NEW" %>
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
            <asp:button id="btnSearch" runat="server" Width="100px" Text="Query" OnClick="btnSearch_Click" ></asp:button>
            <br />
            <asp:Button id="btnExport" runat="server" Width="100px" Text="Export" OnClick="btnExport_Click" Visible="false" />
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
        <td><asp:label id="Label8" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">PO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtPoNo" runat="server" ></asp:TextBox>          		                
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label15" runat="server">Order By:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlOrder" runat="server" Width="60px"  >
            <asp:ListItem Text="Desc" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Asc" ></asp:ListItem>	    
        </asp:dropdownlist></td>
        <td><asp:label id="Label16" runat="server">Page Rows:</asp:label></td>
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
<table id="tb1" class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" align="center" width="95%">
    <tr>
        <td>
            <asp:Label ID="lblShip" runat="server" Font-Size="15px" Text="Ship Label " Visible="false"></asp:Label>  
            <asp:GridView ID="gvShipLabel" runat="server" Font-Size="10px"  AutoGenerateColumns="False"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none"                  
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCommand="gvShipLabel_RowCommand" OnRowCreated="gvShipLabel_RowCreated" OnRowDataBound="gvShipLabel_RowDataBound" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Message ID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblMesgid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESGID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="MsgType" >
                         <ItemTemplate>   
                             <asp:Label ID="lblMsgType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESGTYP") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="SenderID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblSendid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SENDID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="ReceiverID" >
                         <ItemTemplate>   
                             <asp:Label ID="lblReceid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RECEID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>                           
                    <asp:TemplateField HeaderText="OrdNO" >
                         <ItemTemplate> 
                             <asp:LinkButton ID="lbtnOrdnum" runat="server" CommandName="OrdNum" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ORDNUM") %>'></asp:LinkButton>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="TNINTERNALID" HeaderText="tninternalid"></asp:BoundField>
                    <%--<asp:BoundField DataField="BATCHID" HeaderText="batchid"></asp:BoundField>
                    <asp:BoundField DataField="BATCHMESGID" HeaderText="batchmesgid"></asp:BoundField>--%>
                    <asp:BoundField DataField="LABEL_TEMPLATE" HeaderText="label_template"></asp:BoundField>
                    <asp:BoundField DataField="CARRIER_CODE" HeaderText="carrier_code"></asp:BoundField>
                    <asp:TemplateField HeaderText="buid" >
                         <ItemTemplate>   
                             <asp:Label ID="lblBuid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BUID") %>'></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:BoundField DataField="MCID" HeaderText="mcid"></asp:BoundField>
                    <asp:BoundField DataField="CSONUM" HeaderText="csonum"></asp:BoundField> 
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
                    <asp:BoundField DataField="TOTAL_BOXES" HeaderText="total_boxes"></asp:BoundField>
                    <asp:BoundField DataField="TOTAL_SYSTEMS" HeaderText="total_systems"></asp:BoundField>                    
                    <asp:BoundField DataField="RECYCLE_LOGO" HeaderText="recycle_logo"></asp:BoundField>
                    <asp:BoundField DataField="SPECIAL_INSTRUCTIONS" HeaderText="special_instructions"></asp:BoundField>                    
                    <asp:BoundField DataField="BELT_NUMBER" HeaderText="belt_number"></asp:BoundField>
                    <asp:BoundField DataField="POSTAL_CODE" HeaderText="postal_code"></asp:BoundField>
                    <asp:BoundField DataField="SERVICE_LEVEL_DESC" HeaderText="service_level_desc"></asp:BoundField>
                    <asp:BoundField DataField="SERVICE_LEVEL_ICON" HeaderText="service_level_icon"></asp:BoundField>
                    <asp:BoundField DataField="ROUTING_CODE" HeaderText="routing_code"></asp:BoundField>
                    <asp:BoundField DataField="LICENSE_PLATE" HeaderText="license_plate"></asp:BoundField>
                    <asp:BoundField DataField="DEPORT_CODE1" HeaderText="deport_code1"></asp:BoundField>
                    <asp:BoundField DataField="DEPORT_CODE2" HeaderText="deport_code1"></asp:BoundField>                    
                    <asp:BoundField DataField="PRIMARY_SORT" HeaderText="primary_sort"></asp:BoundField>
                    <asp:BoundField DataField="SECONDARY_SORT" HeaderText="secondary_sort"></asp:BoundField>
                    <asp:BoundField DataField="EXPRESS_ROUTING_CODE" HeaderText="express_routing_code"></asp:BoundField>
                    <asp:BoundField DataField="PROCESSFLAG" HeaderText="processflag"></asp:BoundField>
                    <asp:BoundField DataField="PROCESSDATE" HeaderText="processdate"></asp:BoundField>
                    <asp:BoundField DataField="ERRORMSG" HeaderText="errormsg"></asp:BoundField>
                    <asp:BoundField DataField="REGION" HeaderText="region"></asp:BoundField>
                    <asp:BoundField DataField="EXPRESS_ROUTING_CODE_BC" HeaderText="express_routing_code_bc"></asp:BoundField>                  
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Lasteditdt"></asp:BoundField>                          
                </Columns>
                <RowStyle BackColor="#f1f8f1" Wrap="true"/>
                <HeaderStyle  HorizontalAlign="Center"/>
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>

            <asp:Panel ID="panel2" runat="server" Height="10px" ></asp:Panel>
            <asp:Label ID="lblTio" runat="server" Font-Size="15px" Text="Tio Label " Visible="false"></asp:Label>  
            <asp:GridView ID="gvTioLabel" runat="server" Font-Size="10px" AutoGenerateColumns="false"
                  BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="none"
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvTioLabel_RowCreated" OnRowDataBound="gvTioLabel_RowDataBound">
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <ItemTemplate>   
                             <asp:Label ID="lblIndex1" runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MESGID" HeaderText="Message ID"></asp:BoundField> 
                    <asp:BoundField DataField="MESGTYP" HeaderText="MsgType"></asp:BoundField>
                    <asp:BoundField DataField="SENDID" HeaderText="SenderID"></asp:BoundField>
                    <asp:BoundField DataField="RECEID" HeaderText="ReceiverID"></asp:BoundField>
                    <asp:BoundField DataField="ORDNUM" HeaderText="OrdNO"></asp:BoundField>
                    <asp:BoundField DataField="LABEL_TEMPLATE" HeaderText="label_template"></asp:BoundField>
                    <asp:BoundField DataField="ADDL_BOXING" HeaderText="addl_boxing"></asp:BoundField>
                    <asp:BoundField DataField="SHIP_CODE" HeaderText="ship_code"></asp:BoundField>
                    <asp:BoundField DataField="PACK_REF" HeaderText="pack_ref"></asp:BoundField>                    
                    <asp:BoundField DataField="DEST_COUNTRY" HeaderText="dest_country"></asp:BoundField>
                    <asp:BoundField DataField="REGION" HeaderText="region"></asp:BoundField>     
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="lasteditdate"></asp:BoundField>         
                </Columns>
                <RowStyle BackColor="#f1f8f1" Wrap="false"/>
                <HeaderStyle  HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />   
            </asp:GridView>
        </td>
    </tr>
</table>