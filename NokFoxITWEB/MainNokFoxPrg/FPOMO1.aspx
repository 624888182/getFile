<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPOMO1.aspx.cs" Inherits="MainBBRYPrg_FPOMO1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO CONFIRM</title>
    <style type="text/css">
        .style1
        {
            width: 100px;
            text-align: center;
        }
        .style2
        {
            width: 400px;
        }
        .style3
        {
            width: 100px;
        }
        .style6
        {
            width: 100px;
            text-align: center;
            height: 10px;
        }
        .style7
        {
            width: 400px;
            height: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 777px">
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style1">PO:</td>
    <td class="style2">
        <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="style1">BeginDate:</td>
    <td class="style2">
        <asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="style1">EndDate:</td>
    <td class="style2">
        <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="style1">Confirmation:</td>
    <td class="style2">
        <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#D38F10" 
                Width="100px">
                <asp:ListItem Selected="True">ALL</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td class="style6"></td>
    <td class="style7">
        <asp:Button ID="Button1" runat="server" Text="Select" BackColor="#FFCC66" 
            onclick="Button1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
            onclick="Button2_Click"/>
        </td>
    </tr>
    <tr>
    <td class="style3"></td>
    <td>
        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="Label" 
            Width="396px" BackColor="Blue" Height="20px" 
            style="text-align: center; font-weight: 700"></asp:Label>
        </td>
    </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="2" CellSpacing="1" CssClass="style1" Height="104px" 
                    AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowdatabound="GridView1_RowDataBound" 
                HorizontalAlign="Center">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="Smaller" />
                    <Columns>
                        <asp:TemplateField HeaderText="NO">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ITEMID">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ITEMID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POCNT">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POCNT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DELIVERYSTARTDT">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DELIVERYSTARTDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHEDULELINEID">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SCHEDULELINEID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCHEDULEQUANTITY">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SCHEDULEQUANTITY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QTY/DELIVERYSTARTDT">
                            <ItemTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    ShowHeader="False" BorderColor="#009900" BorderStyle="Ridge" 
                                    HorizontalAlign="Center">
                                    <RowStyle BorderColor="#009933" BorderStyle="Ridge" />
                                    <Columns>
                                        <asp:BoundField DataField="QTY" />
                                        <asp:BoundField DataField="DELIVERYSTARTDT" />
                                    </Columns>
                                </asp:GridView>
                                <%--<asp:DataList ID="DataList1" runat="server">
                                </asp:DataList>--%>
                                <%--<asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"QTY") %>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONFIRMFLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="USERCONFIRMFLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"USERCONFIRMFLAG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UPLOAD_SAP">
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UPLOAD_SAP") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SAP_LOG">
                            <ItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SAP_LOG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserConfirm">
                            <ItemTemplate>
                                <asp:Button ID="Button3" runat="server" Text="UserConfirm" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button3_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="VerifySO">
                            <ItemTemplate>
                                <asp:Button ID="Button7" runat="server" Text="VerifySO" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button7_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreateSO">
                            <ItemTemplate>
                                <asp:Button ID="Button6" runat="server" Text="CreateSO" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button6_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Clear">
                            <ItemTemplate>
                                <asp:Button ID="Button4" runat="server" Text="Clear" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button4_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClearSendFlag">
                            <ItemTemplate>
                                <asp:Button ID="Button5" runat="server" Text="ClearSendFlag" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button5_Click" CausesValidation="False" />
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
    </div>
    </form>
</body>
</html>
