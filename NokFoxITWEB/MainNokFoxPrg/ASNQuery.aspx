<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASNQuery.aspx.cs" Inherits="MainMSPrg_ASNQuery" %>

<!DOCTYPE html>
    <script type="text/javascript" src="../Jscript/DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DN Query</title>
    
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
    <center><h1><b><font color ="#0099ff" style="font-family: 標楷體">ASN&nbsp;Query</font></b></h1></center>
    </div>
    <div>
        <fieldset>
            <legend>ASN Query</legend>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" class="input-table">
                <tr>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>PickDate from：</b></font>
                    </td>
                    <td align="left" style="width: 130px">
<asp:TextBox ID="txtBeginDate" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textFrom'),dateFmt:'yyyy-MM-dd '})" onkeypress="javascript:event.returnValue=true;" Width="200px"></asp:TextBox>
                    </td>
                    <td align="left" class="auto-style1">
                    <font color ="#0066ff" ><b>PickDate end：</b></font></td>
                    <td align="left" style="width: 130px">
<asp:TextBox ID="txtendDate" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textEnd'),dateFmt:'yyyy-MM-dd'})" onkeypress="javascript:event.returnValue=false;" Width="196px" ></asp:TextBox>
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
                    <asp:TextBox ID = "txtDNNum" runat ="server"  Width="125px"></asp:TextBox> 
                    </td>
                    <td align="Plant" class="auto-style2">
                    <font color ="#0066ff" ><b>Plant：</b></font></td>
                    <td>
                    <asp:TextBox ID = "txtPlant" runat ="server"  Width="125px"></asp:TextBox> 
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
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    
        <div id="Div_Master" runat="server" style="overflow: auto; height: 310px; width: 100%">
            <fieldset>
                <legend>DeliveryNumberMaster</legend>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Width="700px"></asp:Label>    
        <asp:GridView ID="GridView1" runat="server" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataKeyNames="DNID"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                    PageSize="7" AllowPaging="True" Width="100%" EnableModelValidation="True" AutoGenerateColumns="False">
                    <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="DNID">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDnID" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            Text='<%#DataBinder.Eval(Container.DataItem,"DNID") %>' OnClick="lbtnDnID_Click">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="IssueDT" HeaderText="Pick Date" />
                <asp:BoundField DataField="ArrivalDT" HeaderText="DeliveryDate" />
                <asp:BoundField DataField="CONFIRMFLAG" HeaderText="CONFIRMFLAG" />
                <asp:BoundField DataField="SendFlag" HeaderText="SEND3B2FLAG" />
                <asp:BoundField DataField="SendTime" HeaderText="SEND3B2Time" />
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
        <div id="Div_Detail" runat="server" style="overflow: auto; height: 296px; width: 100%">
            <fieldset>
                <legend>DeliveryNumberDetail</legend>
        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Width="200px"></asp:Label>    
        <asp:GridView ID="GridView2" runat="server" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                    PageSize="7" AllowPaging="True" Width="100%" EnableModelValidation="True">
                    <RowStyle BackColor="White" ForeColor="#333333" />
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
