<%@ Page Language="C#" AutoEventWireup="true" CodeFile="F204Q1.aspx.cs" Inherits="MainMSPrg_F204Q1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>204 Query</title>
    
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
    <center><h1><b><font color ="#0099ff" style="font-family: 標楷體">204&nbsp;Query</font></b></h1></center>
    </div>
    <div>
        <fieldset>
            <legend>204 Query</legend>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" class="input-table">
                <tr>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>PickDate from：</b></font>
                    </td>
                    <td align="left" style="width: 130px">
                    <asp:TextBox ID="txtBeginDate" runat="server" BackColor="White" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="125px"></asp:TextBox>
                    </td>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>PickDate end：</b></font></td>
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
                    <asp:TextBox ID = "dnNum" runat ="server"  Width="125px"></asp:TextBox> 
                    </td>
                    <td align="Plant" class="auto-style2">
                    <font color ="#0066ff" ><b>Plant：</b></font></td>
                    <td>
                    <asp:TextBox ID = "plant" runat ="server"  Width="125px"></asp:TextBox> 
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
                        <asp:Button ID="Button5" runat="server" BackColor="#FFCC00" BorderColor="White" ForeColor="#3366FF" style="font-weight: 700" Text="KeyData" Width="70px" OnClick="Button5_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button6" runat="server" BackColor="#FFCC00" BorderColor="White" ForeColor="#3366FF" style="font-weight: 700" Text="Download" Width="70px" OnClick="Button6_Click" />
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    
        <div id="Div_Header" runat="server" style="overflow: auto; height: 360px; width: 100%">
            <fieldset>
                <legend>204Header</legend>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Width="700px"></asp:Label>    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="L1101,RCOUNT"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                    PageSize="7" AllowPaging="True" Width="100">
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
                <asp:TemplateField HeaderText="Confirm">
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" Text="Confirm" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button4_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" Text="Delete" BackColor="#FFCC00"
                            BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# Container.DataItemIndex %>'
                            onclick="Button7_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LoadID">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnBOL" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text='<%#DataBinder.Eval(Container.DataItem,"L1101") %>' OnClick="lbtnBOL_Click">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="PickDate">
                    <ItemTemplate>
                        <asp:TextBox ID = "pickdate" runat ="server" onclick = "showCalendar();" 
                             AutoPostBack="True" 
                            Text='<%#DataBinder.Eval(Container.DataItem,"pickdate") %>' Height="20px" 
                            Width="70px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DeliveryDate">
                    <ItemTemplate>
                        <asp:TextBox ID = "deliverydate" runat ="server" onclick = "showCalendar();" 
                             AutoPostBack="True" 
                            Text='<%#DataBinder.Eval(Container.DataItem,"deliverydate") %>' Height="20px" 
                            Width="70px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CarrierID">
                    <ItemTemplate>
                        <asp:TextBox ID = "B202" runat ="server" 
                            Text='<%#DataBinder.Eval(Container.DataItem,"B202") %>' Width="50px" 
                            Height="20px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="RFB204" HeaderText="204ID" ReadOnly="True" />
                <asp:BoundField DataField="MS304" HeaderText="Transportation Method"/>
                <asp:BoundField DataField="CONFIRMFLAG" HeaderText="CONFIRMFLAG" ReadOnly="True" />
                <asp:BoundField DataField="SEND3B2FLAG" HeaderText="SEND3B2FLAG" />
                <asp:BoundField DataField="SEND3B2Time" HeaderText="SEND3B2Time" />
                <asp:BoundField DataField="SEND3B2Log" HeaderText="SEND3B2Log" />
                <asp:BoundField DataField="RCOUNT" HeaderText="RCOUNT" />
                <asp:BoundField DataField="ackflag" HeaderText="ackflag" />
                <asp:BoundField DataField="ack_time" HeaderText="ack_time" />
                <%--<asp:BoundField DataField="ST01" HeaderText="ST01" ReadOnly="True" />                             
                <asp:BoundField DataField="B206" HeaderText="B206" ReadOnly="True" />                
                <asp:BoundField DataField="B2A01" HeaderText="B2A01" ReadOnly="True" />
                <asp:BoundField DataField="MS301" HeaderText="MS301" ReadOnly="True" />
                <asp:BoundField DataField="MS302" HeaderText="MS302" ReadOnly="True" />
                <asp:BoundField DataField="MS304" HeaderText="MS304" ReadOnly="True" />
                <asp:BoundField DataField="MS305" HeaderText="MS305" ReadOnly="True" />
                <asp:BoundField DataField="N101" HeaderText="N101" ReadOnly="True" />
                <asp:BoundField DataField="N102" HeaderText="N102" ReadOnly="True" />--%>
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
            style="overflow: auto; height: 280px; width: 100%">
            <fieldset>
                <legend>204Detail</legend>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Width="200px"></asp:Label>
                <asp:GridView ID="GridView2" runat="server" 
                        AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                    OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                    PageSize="7" AllowPaging="True" Width="100%" Height="160px" >
                    <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="B204" HeaderText="LoadID" ReadOnly="True" />
                <%--<asp:BoundField DataField="S501" HeaderText="S501" ReadOnly="True" />
                <asp:BoundField DataField="G6201" HeaderText="G6201" />--%>
                <asp:BoundField DataField="OID01" HeaderText="Delivery number" />
                <asp:BoundField DataField="OID02" HeaderText="MSFT PO" ReadOnly="True" />
                <asp:BoundField DataField="OID03" HeaderText="Material" ReadOnly="True" />   
                <asp:BoundField DataField="OID04" HeaderText="Unit" ReadOnly="True" />
                <asp:BoundField DataField="OID05" HeaderText="Quantity" ReadOnly="True" />
                <asp:BoundField DataField="OID06" HeaderText="WeightUnit" ReadOnly="True" />
                <asp:BoundField DataField="OID07" HeaderText="Weight" ReadOnly="True" />
                <asp:BoundField DataField="LAD01" HeaderText="PackingUnit" ReadOnly="True" />
                <asp:BoundField DataField="LAD02" HeaderText="LoadingQuantity" ReadOnly="True" />
                <asp:BoundField DataField="LAD05" HeaderText="WeightUnit" ReadOnly="True" />
                <asp:BoundField DataField="LAD06" HeaderText="Weight" ReadOnly="True" />
                <asp:BoundField DataField="RCOUNT" HeaderText="RCOUNT" ReadOnly="True" />
                <%--<asp:BoundField DataField="LAD07" HeaderText="LAD07" ReadOnly="True" />
                <asp:BoundField DataField="LAD08" HeaderText="LAD08" ReadOnly="True" />
                <asp:BoundField DataField="LAD09" HeaderText="LAD09" ReadOnly="True" />
                <asp:BoundField DataField="LAD010" HeaderText="LAD010" ReadOnly="True" />
                <asp:BoundField DataField="LAD011" HeaderText="LAD011" ReadOnly="True" />
                <asp:BoundField DataField="LAD012" HeaderText="LAD012" ReadOnly="True" />
                <asp:BoundField DataField="LAD013" HeaderText="LAD013" ReadOnly="True" />--%>
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
                </legend>           
    </fieldset>
        </div>
        </div>
    </form>
</body>
</html>
