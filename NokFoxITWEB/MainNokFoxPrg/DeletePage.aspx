<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeletePage.aspx.cs" Inherits="MainMSPrg_DeletePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<script>
    window.returnValue = "1"//你把yes换为空，上个页面就不会刷新，不为空就会刷新。
</script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Reason</title>
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
     <base target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  align="center";style="border-collapse:collapse;" border="1">
    <tr>
    <td>
        <div style="width:100%; text-align:center;">
            <asp:Label ID="Label1" runat="server" Text="Label" Width="200px" Height="22px"></asp:Label>
        </div> 
    </td>
    </tr>
    <tr>
    <td align="justify">
        <div style="width:100%; " class="style1">
            <b>Reason:</b> <asp:TextBox ID="TextBox1" runat="server" Width="150px" 
                Height="55px"></asp:TextBox>
        </div> 
    </td>        
    </tr>
    <tr>
        <td>
            <div style="width:100%; text-align:center;">
            <asp:Label ID="Label2" runat="server" Width="200px" Height="22px" ForeColor="#FF3300"></asp:Label>
        </div> 
        </td>
    </tr>
    <tr>
    <td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" 
            Height="22px" /> 
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Button2_Click" 
        Height="22px" />
    </td>
    </tr>
    </table>        
    </div>
    </form>
</body>
</html>
