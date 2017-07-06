<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FDISP1.aspx.cs" Inherits="MainMSPrg_FDISP1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>

           
            <table border="0" class="input-table">
                <tr>
                    <td style="width: 90px; text-align: right">BeginTime:
                    </td>
                    <td style="width: 90px; text-align: left">                      
                         <asp:TextBox ID="textBegTime" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right">EndTime:
                    </td>
                    <td style="width: 90px; text-align: left">                     

                         <asp:TextBox ID="textEndTime" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="120px"></asp:TextBox>

                    </td>

                   
                </tr>
                <tr>
                    <td style="width: 90px; text-align: right">PO:
                    </td>
                    <td style="width: 90px; text-align: left">
                        <asp:TextBox ID="textPO" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right">DN:
                    </td>
                    <td style="width: 90px; text-align: left">
                        <asp:TextBox runat="server" ID="textDN" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td colspan="2">
                        <asp:Button runat="server" ID="btnSelect" Text="Select" OnClick="btnSelect_Click" Height="29px" Width="90px"></asp:Button>
                    </td>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnReset" Text="Reset" Height="29px" Width="65px" OnClick="btnReset_Click"></asp:Button>
                    </td>
                </tr>
            </table>
                 </center>
        </div>
        <div>
            <br />
            <center>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                AutoGenerateColumns="False" PageSize="15" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="PO">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"deliverRefer") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POITEM">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"buyeritemno") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                     
                    <asp:TemplateField HeaderText="DN">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"w0502") %>'></asp:Label>
                        </ItemTemplate>                        
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="DNITEM">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Selleritemno") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"qty") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATE">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"creationdt") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MATERIALNO">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MGpartnum") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3A4_ID">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"uf10") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="940_ID">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="204_ID">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"uf9") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3B2_ID">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"uf8") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                    <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />--%>
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Center" />
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
                </center>
        </div>
    </form>
</body>
</html>
