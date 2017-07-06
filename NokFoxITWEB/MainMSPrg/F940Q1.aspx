<%@ Page Language="C#" AutoEventWireup="true" CodeFile="F940Q1.aspx.cs" Inherits="MainMSPrg_F940Q1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>940 Query</title>    
    <style type="text/css">
        .auto-style1 {
            width: 121px;
        }
        .auto-style2 {
            width: 114px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <center><h1><b><font color ="#0099ff" style="font-family: 標楷體">940&nbsp;Query</font></b></h1></center>
    </div>
    <div>
        <fieldset>
            <legend>940 Query</legend>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" class="input-table">
                <tr>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>DeliveryDate from：</b></font>
                    </td>
                    <td align="left" style="width: 130px">
                    <asp:TextBox ID="txtBeginDate" runat="server" BackColor="White" 
                    ForeColor="Black" onclick="showCalendar();"
                    onkeypress="javascript:event.returnValue=false;" Width="125px"></asp:TextBox>
                    </td>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>DeliveryDate end：</b></font></td>
                    <td align="left" style="width: 130px">
                    <asp:TextBox ID="txtendDate" runat="server" BackColor="White" 
                    ForeColor="Black" onclick="showCalendar();"
                    onkeypress="javascript:event.returnValue=false;" Width="125px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 120px">
                        &nbsp;</td>
                    <td align="left" style="width: 130px">
                        &nbsp;</td>
                    <td align="right" style="width: 120px">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>DN NUM：</b></font>
                    </td>
                    <td>
                    <asp:TextBox ID = "dnNum" runat ="server" Width="125px"></asp:TextBox> 
                    </td>
                    <td align="Plant" class="auto-style2">
                    <font color ="#0066ff" ><b>Plant：</b></font></td>
                    <td>
                    <asp:TextBox ID = "plant" runat ="server" Width="125px"></asp:TextBox> 
                    </td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="height: 28px" valign="bottom">
                    <td align="center" colspan="2">
                        <asp:Button ID="Button1" runat="server" BackColor="#FFCC00" BorderColor="White" ForeColor="#3366FF" style="font-weight: 700" Text="Query" Width="70px" OnClick="Button1_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" BackColor="#FFCC00" BorderColor="White" ForeColor="#3366FF" style="font-weight: 700" Text="Reset" Width="70px" OnClick="Button2_Click" />
                    </td>
                    <td align="left" colspan="6">
                    
                        <asp:Button ID="Button6" runat="server" BackColor="#FFCC00" BorderColor="White" ForeColor="#3366FF" style="font-weight: 700" Text="GetSAP" Width="70px" OnClick="Button6_Click" />
                    &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    
        <div id="Div_Header" runat="server" style="overflow: auto; height: 340px; width: 100%">
            <fieldset>
                <legend>940Header</legend>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Width="700px"></asp:Label>    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                    CellPadding="4" DataKeyNames="w0502,deliverRefer,orderedqty"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"     
                    PageSize="7" AllowPaging="True" Width="100px">
                    <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="Save">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" Text="Save" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button3_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Create">
                    <ItemTemplate>
                        <asp:Button ID="Button5" runat="server" Text="Send" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button5_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reset">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" Text="Reset" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button7_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="Button8" runat="server" Text="Delete" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button8_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DN_Number">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDnID" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text='<%#DataBinder.Eval(Container.DataItem,"w0502") %>' OnClick="lbtnDnID_Click">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="PickingDate">
                    <ItemTemplate>
                        <asp:TextBox ID = "shippingdate" runat ="server" onclick = "showCalendar();" 
                             AutoPostBack="True" Text='<%#DataBinder.Eval(Container.DataItem,"shippingdate") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DeliveryDate">
                    <ItemTemplate>
                        <asp:TextBox ID = "requestdate" runat ="server" onclick = "showCalendar();" 
                             AutoPostBack="True" Text='<%#DataBinder.Eval(Container.DataItem,"requestdate") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contractname">
                    <ItemTemplate>
                        <asp:TextBox ID = "contractname" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"contractname") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ContractEmail">
                    <ItemTemplate>
                        <asp:TextBox ID = "contractEmail" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"contractEmail") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ContractPhone">
                    <ItemTemplate>
                        <asp:TextBox ID = "contractPhone" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"contractPhone") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Routing">
                    <ItemTemplate>
                        <asp:TextBox ID = "routing" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"routing") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="Incom Term">
                    <ItemTemplate>
                        <asp:TextBox ID = "FOBpoint" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"FOBpoint") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="IndustryCode">
                    <ItemTemplate>
                        <asp:TextBox ID = "industrycode" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"industrycode") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>   
               <%-- <asp:BoundField DataField="shippingdate" HeaderText="ShippingDate" ReadOnly="True" />
                <asp:BoundField DataField="contractname" HeaderText="Contractname" />  
                <asp:BoundField DataField="contractEmail" HeaderText="ContractEmail" ReadOnly="True" />
                <asp:BoundField DataField="contractPhone" HeaderText="ContractPhone" /> 
                <asp:BoundField DataField="routing" HeaderText="Routing" />  
                <asp:BoundField DataField="FOBpoint" HeaderText="Incom Term" ReadOnly="True" />
                <asp:BoundField DataField="industrycode" HeaderText="IndustryCode" />  --%>   
                <asp:BoundField DataField="shipfrom" HeaderText="Shipfrom" ReadOnly="True" />
                <asp:BoundField DataField="salesorg" HeaderText="Salesorg" />
                <asp:BoundField DataField="deliverRefer" HeaderText="PO" />
                <asp:BoundField DataField="venderterm" HeaderText="PaymentTerm" />
                <asp:BoundField DataField="customerSO" HeaderText="SalesOrder" ReadOnly="True" />                             
                <asp:BoundField DataField="orderedqty" HeaderText="Request Qautity" ReadOnly="True" />                
                <asp:BoundField DataField="carton_number_qty" HeaderText="Carton_Number_QTY" ReadOnly="True" />
                <asp:BoundField DataField="pallete_mumber_qty" HeaderText="Pallete_Number_QTY" ReadOnly="True" />
                <asp:BoundField DataField="isa13" HeaderText="Isa13" ReadOnly="True" />
                <asp:BoundField DataField="gs06" HeaderText="Gs06" ReadOnly="True" />
                <asp:BoundField DataField="ST02" HeaderText="ST02" ReadOnly="True" />
                <asp:BoundField DataField="Create_flag" HeaderText="Create_flag" ReadOnly="True" />
                <asp:BoundField DataField="send_flag" HeaderText="Send_flag" ReadOnly="True" />
                <asp:BoundField DataField="send_time" HeaderText="Send_time" ReadOnly="True" />
                <asp:BoundField DataField="ackflag" HeaderText="Ackflag" ReadOnly="True" />
                <asp:BoundField DataField="ack_time" HeaderText="Ack_time" ReadOnly="True" />
                <asp:BoundField DataField="state" HeaderText="State" />
                <asp:BoundField DataField="CreationDT" HeaderText="CreationDT" ReadOnly="True" />                
                <asp:BoundField DataField="w0501" HeaderText="w0501" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
                    <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />--%>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        The current page:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        Pp/Total:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        Page
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                            Enabled='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>FirstPage</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>The Previous</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Next</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Last</asp:LinkButton>
                        Turn to
                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />Page
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                            CommandName="Page" Text="GO" />
                    </PagerTemplate>
                </asp:GridView>
    </fieldset>
        </div>
        <div id="Div_Detail" runat="server" 
            style="overflow: auto; height: 200px; width: 100%">
            <fieldset>
                <legend>940Detail</legend>
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Width="200px"></asp:Label>    
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="w0502,Selleritemno"
                    OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                    PageSize="3" AllowPaging="True" Width="100%" Height="160px" >
                    <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="Save">
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" Text="Save" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button4_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="w0502" HeaderText="DN_Number" ReadOnly="True" />
                <asp:BoundField DataField="Selleritemno" HeaderText="DN ITEM" ReadOnly="True" />
                <asp:BoundField DataField="LX01" HeaderText="LX01" />
                <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:TextBox ID = "qty" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"qty") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Weight">
                    <ItemTemplate>
                        <asp:TextBox ID = "lineitemweight" runat ="server" Text='<%#DataBinder.Eval(Container.DataItem,"lineitemweight") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MGpartnum" HeaderText="MSFT PN" />
                <asp:BoundField DataField="buyeritemno" HeaderText="BuyerItemNO" ReadOnly="True" />
                <asp:BoundField DataField="plantno" HeaderText="PlantNO" ReadOnly="True" />   
                <asp:BoundField DataField="origiancountry" HeaderText="Country" ReadOnly="True" />
                <asp:BoundField DataField="cartonqty" HeaderText="CartonQTY" ReadOnly="True" />
                <asp:BoundField DataField="palletqty" HeaderText="PalletQTY" ReadOnly="True" />
                <asp:BoundField DataField="isa13" HeaderText="Isa13" ReadOnly="True" />
                <asp:BoundField DataField="gs06" HeaderText="Gs06" ReadOnly="True" />
                <asp:BoundField DataField="ST02" HeaderText="ST02" ReadOnly="True" />
                <asp:BoundField DataField="CreationDT" HeaderText="CreationDT" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
                    <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />--%>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        The current page:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        Pp/Total:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        Page
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                            Enabled='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>FirstPage</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>The Previous</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Next</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Last</asp:LinkButton>
                        Turn to
                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />Page
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                            CommandName="Page" Text="GO" />
                    </PagerTemplate>
                </asp:GridView>
    </fieldset>
        </div>
        <div id="Div_Address" runat="server" 
            style="overflow: auto; height: 227px; width: 100%">
            <fieldset>
                <legend>940Address</legend>
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Label" Width="200px"></asp:Label>    
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                    OnPageIndexChanging="GridView3_PageIndexChanging" OnRowDataBound="GridView3_RowDataBound"
                    PageSize="3" AllowPaging="True" Width="100%" >
                    <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="w0502" HeaderText="DN_Number" ReadOnly="True" />
                <asp:BoundField DataField="addresstype" HeaderText="AddressType" ReadOnly="True" />
                <asp:BoundField DataField="addresstocode" HeaderText="AddresstoCode" />
                <asp:BoundField DataField="addresstoname" HeaderText="AddresstoName" />
                <asp:BoundField DataField="addressinfodesc" HeaderText="AddressInfoDesc" />
                <asp:BoundField DataField="cityname" HeaderText="CityName" ReadOnly="True" />
                <asp:BoundField DataField="statecode" HeaderText="StateCode" ReadOnly="True" />
                <asp:BoundField DataField="postcode" HeaderText="PostCode" ReadOnly="True" />
                <asp:BoundField DataField="countrycode" HeaderText="CountryCode" ReadOnly="True" />
                <asp:BoundField DataField="isa13" HeaderText="Isa13" ReadOnly="True" />
                <asp:BoundField DataField="gs06" HeaderText="Gs06" ReadOnly="True" />
                <asp:BoundField DataField="ST02" HeaderText="ST02" ReadOnly="True" />
                <asp:BoundField DataField="CreationDT" HeaderText="CreationDT" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
                    <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />--%>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        The current page:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        Pp/Total:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        Page
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                            Enabled='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>FirstPage</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>The Previous</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Next</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Last</asp:LinkButton>
                        Turn to
                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />Page
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                            CommandName="Page" Text="GO" />
                    </PagerTemplate>
                </asp:GridView>
    </fieldset>
        </div>
        </div>
    </form>
</body>
</html>
