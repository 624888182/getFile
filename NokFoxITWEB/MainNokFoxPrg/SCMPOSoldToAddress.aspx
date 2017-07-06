<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCMPOSoldToAddress.aspx.cs" Inherits="MainBBRYPrg_SCMPOSoldToAddress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POSoldToAddress</title>
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
        .style4
    {
        width: 499px;
        font-weight: bold;
    }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="height: 777px; font-size: large;">
    <table align="center";style="border-collapse:collapse;" border="1" 
            style="width: 517px">
    <tr>
    <td class="style4">POSoldToAddress</td>
    </tr>
    </table>
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
    <td class="style1"></td>
    <td class="style2">
        <asp:Button ID="Button1" runat="server" Text="Select" BackColor="#FFCC66" 
            onclick="Button1_Click" Height="21px" Width="50px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
            onclick="Button2_Click" Height="20px"/></td>
    </tr>
    <tr>
    <td class="style1"></td>
    <td>
        <asp:Label ID="Label0" runat="server" ForeColor="Red" Text="Label" 
            Width="300px" BackColor="Blue" Height="20px" 
            style="text-align: center; font-weight: 700"></asp:Label>
        </td>
    </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" 
                    BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="2" CellSpacing="1" CssClass="style1" Height="104px" 
                    AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowdatabound="GridView1_RowDataBound" 
                HorizontalAlign="Center">
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="Smaller" />
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
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


