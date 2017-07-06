<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI204aspx.aspx.cs" Inherits="MainMSPrg_EDI204aspx" %>

<!DOCTYPE html>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

    <style type="text/css">
        .auto-style1
        {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <h1>
                <b><font color="#cc6600" style="font-family: 標楷體">204&nbsp;Key&nbsp;Data</font></b></h1>
        </center>
    </div>
    <div>
        <center>
            <table>
                <tr>
                    <td class="auto-style1" style="text-align: right">
                        Insert:
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="Button1" runat="server" Text="Insert" OnClick="Button1_Click" Width="100px" />
                    </td>
                    <td class="auto-style1" style="text-align: right">
                        Reset:
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="Button2_Click" Width="101px" />
                    </td>
                    <td class="auto-style1" style="text-align: right">
                        Return:
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="Button3" runat="server" Text="Return" OnClick="Button3_Click" Width="101px" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    <div id="Div" runat="server" style="width: 100%">
        <center>
            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="" Width="200px"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" AutoGenerateColumns="False"
                BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                OnRowDataBound="GridView1_RowDataBound" Visible="true" PageSize="20" AllowPaging="True"
                Width="100">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox3_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NO">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="204 ID">
                        <ItemTemplate>
                            <asp:TextBox ID="textLoadID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"204 ID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Picking Date">
                        <ItemTemplate>
                            <asp:TextBox ID="textPickingDate" runat="server" onclick="showCalendar();" AutoPostBack="True"
                                Text='<%#DataBinder.Eval(Container.DataItem,"Picking Date") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Delivery Date">
                        <ItemTemplate>
                            <asp:TextBox ID="textDeliveryDate" runat="server" onclick="showCalendar();" AutoPostBack="True"
                                Text='<%#DataBinder.Eval(Container.DataItem,"Delivery Date") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Load ID">
                        <ItemTemplate>
                            <asp:TextBox ID="textLoadID1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Load ID") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivery Number">
                        <ItemTemplate>
                            <asp:TextBox ID="textDeliveryNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Delivery Number") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />    Text='<%#DataBinder.Eval(Container.DataItem,"routing") %>'--%>
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
        </center>
    </div>
    <div>
    </div>
    </form>
</body>
</html>
