<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_PO_NEW.ascx.cs" Inherits="Boundary_B2B_PO_NEW" %>
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
        <td><asp:label id="Label6" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">CsoNum:</asp:label></td>
        <td>
            <asp:TextBox ID="txtCsoNum" runat="server"></asp:TextBox>		
        </td>
        <td><asp:label id="Label7" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">Message ID:</asp:label></td>
        <td>
            <asp:TextBox ID="txtMesgID" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label8" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">PO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtPoNo" runat="server" ></asp:TextBox>          		                
        </td>
        <td ><asp:label id="Label11" runat="server" ToolTip="(以逗號區分,例如:000000480,000000481)">SO NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtSoNo" runat="server" ></asp:TextBox>         
        </td>
    </tr>
    <tr>
        <td><asp:label id="Label12" runat="server"  ToolTip="(以逗號區分,例如:000000480,000000481)">Idoc NO:</asp:label></td>
        <td>
            <asp:TextBox ID="txtIdocNo" runat="server" ></asp:TextBox>   
        </td>
    </tr>
    <tr> 
        <td><asp:label id="Label5" runat="server" >PlantCode:</asp:label></td>
        <td><asp:dropdownlist id="ddlPlantCode" runat="server" Width="80px"></asp:dropdownlist></td>
        <td><asp:label id="Label9" runat="server" Width="100px">Upload Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlUploadFlag" runat="server" Width="80px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N-New" ></asp:ListItem>
            <asp:ListItem Text="Y-Sent" ></asp:ListItem>
            <asp:ListItem Text="E-Error" ></asp:ListItem>
            <asp:ListItem Text="H-Hold"></asp:ListItem>		    
        </asp:dropdownlist></td> 
    </tr>	
    <tr>
        <td><asp:label id="Label10" runat="server" Width="100px">CTOMOD:</asp:label></td>
        <td ><asp:dropdownlist id="ddlCtoMod" runat="server" Width="80px"></asp:dropdownlist></td>
        <td><asp:label id="Label13" runat="server" Width="100px">INSFC Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlSfcFlag" runat="server" Width="80px">
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N-New" ></asp:ListItem>
            <asp:ListItem Text="Y-Sent" ></asp:ListItem>
            <asp:ListItem Text="E-Error" ></asp:ListItem>
            <asp:ListItem Text="H-Hold"></asp:ListItem>		    
        </asp:dropdownlist></td>
    </tr>	
    <tr> 
        <td><asp:label id="Label14" runat="server" Width="100px">ACK Flag:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlAckFlag" runat="server" Width="80px"  >
            <asp:ListItem Text="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text="N-New" ></asp:ListItem>
            <asp:ListItem Text="Y-Sent" ></asp:ListItem>	    
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
<hr>
<table cellSpacing="0" cellPadding="0" border="0" >
    <tr>
        <td style="width: 30px"></td>
        <td>
            <asp:Panel ID="panel1" runat="server" Height="10px"></asp:Panel>
                <asp:GridView ID="gvPO" runat="server"  Font-Size="10px" AutoGenerateColumns="false" 
                       BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="None" PagerSettings-Visible="false" 
                       OnRowCommand="gvPO_RowCommand" OnRowCreated="gvPO_RowCreated" OnRowDataBound="gvPO_RowDataBound" OnRowCancelingEdit="gvPO_RowCancelingEdit" 
                       OnRowEditing="gvPO_RowEditing" OnRowUpdating="gvPO_RowUpdating"  Font-Strikeout="False">
                   <Columns>   
                        <asp:TemplateField HeaderText="Index" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Message ID" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblMesgid" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.MESGID") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="MSGTYP" HeaderText="Message Type" ReadOnly="True"></asp:BoundField>
                        <asp:TemplateField HeaderText="Sender ID" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblSendid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SENDID") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Receiver ID" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblReceid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RECEID") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>                              
                        <asp:TemplateField HeaderText="PONO" >
                             <ItemTemplate> 
                                 <asp:LinkButton ID="lbtnPoNo" runat="server" CommandName="PONO" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.ORDNUM") %>'></asp:LinkButton>
                             </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="ORDFLAG" HeaderText="Order Flag" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="ORDDAT" HeaderText="PO Date" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="DLYDAT" HeaderText="Delivery Date" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="ORD_PRIORITY" HeaderText="Ord_Priority" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SHPMOD" HeaderText="Ship Method" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="PARTSH" HeaderText="Partsh" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SCACID" HeaderText="SCAC ID" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="MCID" HeaderText="Mcid" ReadOnly="True"></asp:BoundField>  
                        <asp:BoundField DataField="FINAL_MCID" HeaderText="Final Mcid" ReadOnly="True"></asp:BoundField>                          
                        <asp:BoundField DataField="CUSNAM" HeaderText="Customer Name" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD1" HeaderText="Customer Address1" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD2" HeaderText="Customer Address2" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD3" HeaderText="Customer Address3" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD4" HeaderText="Customer Address4" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD5" HeaderText="Customer Address5" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD6" HeaderText="Customer Address6" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSAD7" HeaderText="Customer Address7" ReadOnly="True"></asp:BoundField>  
                        <asp:BoundField DataField="CUSCOO" HeaderText="Cuscoo" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSPHO" HeaderText="Cuspho" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CSONUM" HeaderText="Csonum" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSNUM" HeaderText="Cusnum" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CUSTPONUM" HeaderText="Custponum" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CTOMOD" HeaderText="Ctomod" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SOTYPE" HeaderText="SO Type" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="PLANTCODE" HeaderText="PlantCode" ReadOnly="True"></asp:BoundField> 
                        <asp:BoundField DataField="SALESORG" HeaderText="Salesorg" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SALESCHANNEL" HeaderText="Saleschannel" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SHIPPOINT" HeaderText="Shippoint" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SHIPCONDITION" HeaderText="Shipcondition" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SALESDIVISION" HeaderText="Salesdivision" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="PARTNOTYPE" HeaderText="Partnotype" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="PRIORITY" HeaderText="Priority" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="ROUTE" HeaderText="Route" ReadOnly="True"></asp:BoundField> 
                        <asp:BoundField DataField="STORAGELOCATION" HeaderText="Storage Location" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SHIPTOPARTNER" HeaderText="ShipToPartner" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SOLDTOPARTNER" HeaderText="SoldToPartner" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="INSFCFLAG" HeaderText="In SFC Flag" ReadOnly="True"></asp:BoundField>
                        <%--<asp:BoundField DataField="CREATEDUMMYBOMFLAG" HeaderText="Create Dummy Bom Flag" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="BOMIDOCNO" HeaderText="Bom Idoc NO" ReadOnly="True"></asp:BoundField>--%>
                        <asp:BoundField DataField="BOOKTIME" HeaderText="Book Time" ReadOnly="True"></asp:BoundField> 
                        <asp:TemplateField HeaderText="Upload Flag" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblUploadFlag" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UPLOADFLAG") %>'></asp:Label>  
                             </ItemTemplate>
                             <EditItemTemplate> 
                                 <asp:DropDownList ID="ddlUploadFlag" runat="server" DataTextField="UPLOADFLAG" DataValueField="UPLOADFLAG">
                                     <asp:ListItem Text="E" Selected="true"></asp:ListItem>
                                     <asp:ListItem Text="N"></asp:ListItem>
                                 </asp:DropDownList>  
                                 <asp:Label ID="lblUploadFlag" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.UPLOADFLAG") %>'></asp:Label>
                             </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True"  HeaderText="Edit"  /> 
                        <asp:BoundField DataField="SONO" HeaderText="SO NO"  ReadOnly="True"></asp:BoundField> 
                        <asp:BoundField DataField="SODATE" HeaderText="SO Date"  ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="IDOCNO" HeaderText="Idoc No"   ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="ERRORMSG" HeaderText="Error Message"  ReadOnly="True"></asp:BoundField> 
                        <asp:BoundField DataField="acksendflag" HeaderText="Ack Send Flag"  ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="acksenddate" HeaderText="Ack Send Date"  ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="ackerrormsg" HeaderText="Ack Error Message"  ReadOnly="True"></asp:BoundField> 
                        
                        <asp:BoundField DataField="reason" HeaderText="Reason"  ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="reason_desc" HeaderText="Reason Desc"  ReadOnly="True"></asp:BoundField> 
                                                      
                        <asp:BoundField DataField="LASTEDITBY" HeaderText="Last Edit By"  ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"  ReadOnly="True"></asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#F1F8F1"/>
                    <HeaderStyle  HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />        
                    <PagerSettings Visible="False" />   
                </asp:GridView>
        </td>
   </tr>
</table>
<table cellSpacing="0" cellPadding="0" border="0">
        <tr>
            <td>
                <asp:Panel ID="panel3" runat="server" Visible="false">
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
            </td>
        </tr>
    </table>

