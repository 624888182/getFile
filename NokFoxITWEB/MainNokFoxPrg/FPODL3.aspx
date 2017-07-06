<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPODL3.aspx.cs" Inherits="MainMSPrg_FPODL3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .auto-style1 {
            width: 128px;
            border: 1px solid blue;
        }

        .auto-style2 {
            width: 220px;
            border: 1px solid blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <h1><b><font color="#0099ff" style="font-family: 標楷體">PO&nbsp;Trace&nbsp;Report</font></b></h1>
            </center>
        </div>
        <div id="divTable">
            <fieldset>
                <legend></legend>
                <table align="center" style="border-collapse: collapse; border: 1px solid blue" id="table1" runat="server">
                    <tr>
                        <td class="auto-style1">MSFT_PO:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPOID" runat="server" Width="92px"></asp:TextBox>~
                            <asp:TextBox ID="txtPOID1" runat="server" Width="92px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">BeginDate:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtBeginDate" runat="server"
                                ForeColor="Black" onclick="showCalendar();"
                                onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">EndDate:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtendDate" runat="server"
                                ForeColor="Black" onclick="showCalendar();"
                                onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" class="auto-style1">
                            <asp:Button ID="Button1" runat="server" Text="Select"
                                OnClick="Button1_Click" Width="84px" Height="22px" />
                        </td>
                        <td style="text-align: center" class="auto-style2">
                            <asp:Button ID="Button2" runat="server" Text="Reset"
                                OnClick="Button2_Click" Width="84px" Height="22px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="Button3" runat="server" Text="DownLoad"
                                OnClick="Button3_Click" Width="84px" Height="22px" />

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div id="divGridView1" runat="server">
            <fieldset>
                <legend>PO Trace Report</legend>
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
                <div id="GridView11" runat="server" style="width: 100%; overflow: scroll; margin-top: 10px;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                            <%--<asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Labelid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="NO">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--  3A4  --%>
                            <asp:TemplateField HeaderText="MSFT PO">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSFT PO ITEM">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO ITEM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="MSFT P/N">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT P/N") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CREATIONDT">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Price Base Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PO Price Base Unit") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Oreder QTY(Piece)">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BaseQty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pick Up Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pick Up Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ShipToAdress">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShipToAdress") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Request Delivery Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request Delivery Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Location">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Storage Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Balance Qty">
                                <ItemTemplate>
                                    <asp:Label ID="Label4a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Balance Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--  940  --%>
                            <asp:TemplateField HeaderText="Delivery Note No">
                                <ItemTemplate>
                                    <asp:Label ID="Label14a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"W0502") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery date">
                                <ItemTemplate>
                                    <asp:Label ID="Label14b" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"W0502") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivery Note Qty">
                                <ItemTemplate>
                                    <asp:Label ID="Label14c" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"W0502") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking request Status">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking request ACK">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--  204  --%>
                            <asp:TemplateField HeaderText="B204" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label16a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"B204") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LoadID">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CarrierID">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transportation Method">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--  3B2  --%>
                            <asp:TemplateField HeaderText="DNID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label19A" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ASN Send">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ASN Send ACK" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Send3B2Time">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />

                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

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
                        <AlternatingRowStyle Font-Size="Smaller" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
       <asp:gridview ID="GridView2"  runat="server" OnRowDataBound="GridView1_RowDataBound" Visible=false ></asp:gridview>
    </form>
</body>

</html>
