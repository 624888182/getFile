<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPODL1.aspx.cs" Inherits="MainBBRYPrg_FPODL1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Excel Download</title>
    <style type="text/css">
        .style1
        {
            width: 550px;
            font-weight: bold;
            text-align: center;
        }
        .style2
        {
            width: 200px;
            text-align: center;
        }
        .style3
        {
            width: 344px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 280px">
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style1">PO Excel Download</td>
    </tr>
    </table>
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style2">POID:</td>
    <td class="style3">
        <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">BeginDate:</td>
    <td class="style3">
        <asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">EndDate:</td>
    <td class="style3">
        <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">PRODUCTCATEGORYID:</td>
    <td class="style3">
        <asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">Customers P/N Code:</td>
    <td class="style3">
        <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">Incoterm:</td>
    <td class="style3">
        <asp:TextBox ID="TextBox4" runat="server" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    </table>
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style1">
        <asp:Button ID="Button1" runat="server" Text="Select" BackColor="#FFCC66" 
            onclick="Button1_Click" Width="84px" Height="22px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
            onclick="Button2_Click" Width="84px" Height="22px"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Download" BackColor="#FFCC66" 
            onclick="Button3_Click" Height="22px"/>
        </td>
    </tr>
    </table>
    <table align="center">
    <tr>
    <td class="style1">
        <asp:Label ID="Label0" runat="server" style="color: #FF0000" Text="Label" 
            Width="300px"></asp:Label>
        </td>
    </tr>
    </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="2" CellSpacing="1" CssClass="style1" Height="104px" 
                    AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowdatabound="GridView1_RowDataBound" 
                HorizontalAlign="Center" PageSize="20">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="Smaller" />
                    <Columns>
                        <asp:TemplateField HeaderText="Create_Time">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRODUCTCATEGORYID">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PRODUCTCATEGORYID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="POID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item_ID">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ITEMID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OriginID">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OriginID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FIH_P/N_Code">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FIH_P/N_Code") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customers_P/N_Code">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"INTERNALID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Currency">
                            <ItemTemplate>
                                <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Currency") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CostAmount">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CostAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CostCurrencyCode">
                            <ItemTemplate>
                                <asp:Label ID="Label23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CostCurrencyCode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order_Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SCHEDULEQUANTITY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DELIVERYSTARTDT">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DELIVERYSTARTDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country_Origin">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Country_Origin") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Country_Code">
                            <ItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CountryCode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ship_to_Address">
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Ship_to_Address") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ship_to_Code">
                            <ItemTemplate>
                                <asp:Label ID="Label22" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Ship_to_Code") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Incoterm">
                            <ItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Incoterm") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carrier_ID">
                            <ItemTemplate>
                                <asp:Label ID="Label19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Carrier_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CarrierName">
                            <ItemTemplate>
                                <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CarrierName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CCI_Sold_To">
                            <ItemTemplate>
                                <asp:Label ID="Label24" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CCI_Sold_To") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CCI_Header">
                            <ItemTemplate>
                                <asp:Label ID="Label25" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CCI_Header") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONFIRMFLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POCNT">
                            <ItemTemplate>
                                <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POCNT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" 
                        Font-Size="Smaller" />
                    <HeaderStyle BackColor="#CC6600" BorderColor="Black" Font-Bold="True" 
                        ForeColor="White" Font-Size="Smaller" />
                    <AlternatingRowStyle Font-Size="Smaller" />
                </asp:GridView>
    </form>
</body>
</html>
