<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="POCancel.aspx.cs" Inherits="MainMSPrg_POCancel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .style1 {
            width: 550px;
            font-weight: bold;
            text-align: center;
        }

        .style2 {
            width: 200px;
            text-align: center;
        }

        .style3 {
            width: 344px;
        }

        .auto-style2 {
            width: 247px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divTable">
            <fieldset>
                <legend>3A4 Cancel</legend>
                <table align="center" style="border-collapse: collapse;" border="1" id="table1" runat="server">
                    <tr>
                        <td class="style2">MSFT_PO:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtPOID" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">BeginDate:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtBeginDate" runat="server"
                                ForeColor="Black" onclick="showCalendar();"
                                onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">EndDate:</td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtendDate" runat="server"
                                ForeColor="Black" onclick="showCalendar();"
                                onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="Button1" runat="server" Text="Select"
                                OnClick="Button1_Click" Width="84px" Height="22px" />
                        </td>
                        <td style="text-align: center" class="auto-style2">
                            <asp:Button ID="Button2" runat="server" Text="Reset"
                                OnClick="Button2_Click" Width="84px" Height="22px" />
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
                <legend>3A4 Header</legend>
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
                <div id="GridView11" runat="server" style="width: 100%; overflow: scroll; margin-top: 10px;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                            <asp:TemplateField HeaderText="Cancel PO">
                                <ItemTemplate>
                                    <asp:Button ID="btnClearPO" runat="server" Text="Cancel PO" CommandArgument='<%# Container.DataItemIndex %>'
                                        OnClick="btnClearPO_Click" CausesValidation="False" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Labelid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NO">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSFT DUNS">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT DUNS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSFT PO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Label3" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO") %>' OnClick="lbtPO_Click">LinkButton</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CREATIONDT">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Incoterm">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Incoterm") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Payment") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ShipToCode">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShipToCode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PurchaseGroup">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PurchaseGroup") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DateTimeStamp">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateTimeStamp") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CONFIRMFLAG">
                                <ItemTemplate>
                                    <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="940Count">
                                <ItemTemplate>
                                    <asp:Label ID="lbl940Count" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"940Count") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="940Flag">
                                <ItemTemplate>
                                    <asp:Label ID="lbl940Flag" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"940Flag") %>'></asp:Label>
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
        <div id="divGridView2" runat="server">
            <fieldset>
                <legend>3A4 Details </legend>
                <asp:Label ID="lblDetail" runat="server"></asp:Label>
                <div id="GridView22" runat="server" style="width: 100%; overflow: scroll; margin-top: 10px;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                            <asp:TemplateField HeaderText="MSFT PO">
                                <ItemTemplate>
                                    <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSFT PO ITEM">
                                <ItemTemplate>
                                    <asp:Label ID="Label23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO ITEM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MSFT P/N">
                                <ItemTemplate>
                                    <asp:Label ID="lblPOCNT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT P/N") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CREATIONDT">
                                <ItemTemplate>
                                    <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Label26" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Price Base Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label27" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PO Price Base Unit") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Oreder QTY">
                                <ItemTemplate>
                                    <asp:Label ID="Label28" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Oreder QTY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pick Up Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label29" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pick Up Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Term'">
                                <ItemTemplate>
                                    <asp:Label ID="Label30" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Payment Term") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Request Delivery Date">
                                <ItemTemplate>
                                    <asp:Label ID="Label31" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request Delivery Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Location">
                                <ItemTemplate>
                                    <asp:Label ID="Label32" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Storage Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CONFIRMFLAG">
                                <ItemTemplate>
                                    <asp:Label ID="Label33" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
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
    </form>
</body>
</html>
