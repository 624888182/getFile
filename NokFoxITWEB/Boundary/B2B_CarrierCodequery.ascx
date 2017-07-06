<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_CarrierCodequery.ascx.cs" Inherits="Boundary_B2B_CarrierCodequery" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 

<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td width="50" rowSpan="9"><FONT face="新細明體"></FONT></td>
        <td><asp:label id="lblRegion" runat="server" >Region:</asp:label></td>
        <td width="200px">
            <asp:TextBox ID="txtRegion" runat="server" ></asp:TextBox>          		                
        </td>
        <td ><asp:label id="lblCarrierCode" runat="server" >Carrier Code:</asp:label></td>
        <td>
            <asp:TextBox ID="txtCarrierCode" runat="server" ></asp:TextBox>         
        </td>
        <td  align="center" rowspan="9" width="200px"> 
            <asp:button id="btnSearch" runat="server" Width="100px" Text="Query" OnClick="btnSearch_Click"></asp:button>
            <br />
            <br />
            <asp:Button id="btnExport" runat="server" Width="100px" Text="Export" OnClick="btnExport_Click" Visible="false" />
        </td>
    </tr> 
    <tr> 
        <td><asp:label id="lblMCID" runat="server" >MCID:</asp:label></td>
        <td><asp:dropdownlist id="ddlMCID" runat="server" Width="80px"></asp:dropdownlist></td>
        <td><asp:label id="lblShipMode" runat="server" Width="100px">Ship Mode:</asp:label></td>
        <td>
        <asp:dropdownlist id="ddlShipMode" runat="server" Width="80px"  >
            <asp:ListItem Text="ALL" Selected="True"></asp:ListItem>
            <asp:ListItem Text="A-Air" ></asp:ListItem>
            <asp:ListItem Text="G-Ground" ></asp:ListItem>
            <asp:ListItem Text="S-Sea" ></asp:ListItem> 	    
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
                <asp:GridView ID="gvCarrierCode" runat="server"  Font-Size="10px" AutoGenerateColumns="false" 
                       BackColor="White" UserID="Any"  BorderWidth="1px" Font-Names="Verdana" BorderStyle="None" PagerSettings-Visible="false" 
                       Font-Strikeout="False" OnRowCreated="gvCarrierCode_RowCreated" OnRowDataBound="gvCarrierCode_RowDataBound">
                   <Columns>   
                        <asp:TemplateField HeaderText="Index" >
                             <ItemTemplate>   
                                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="ORIGIN" HeaderText="Origin" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="REGION" HeaderText="Region" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="MCID" HeaderText="MCID" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SHIP_MODE" HeaderText="Ship Mode" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="SCACID" HeaderText="Carrier Code" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="UPDATED_DATE" HeaderText="Updated Date" ReadOnly="True"></asp:BoundField>  
                        <asp:BoundField DataField="UPDATED_BY" HeaderText="Updated By" ReadOnly="True"></asp:BoundField>   
                    </Columns>
                    <RowStyle BackColor="#F1F8F1" Height="30px" Width="80px" HorizontalAlign="center"/>
                    <HeaderStyle  HorizontalAlign="Center" Height="30px"/>
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
                            <td style="width: 30px"></td>
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
