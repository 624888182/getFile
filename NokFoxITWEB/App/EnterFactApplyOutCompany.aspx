<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyOutCompany.aspx.cs" Inherits="App_EnterFactApplyInCompany" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外來人員出廠</title>
    <script language=javascript>

    </script>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitle" Text="外來人員出廠"></asp:Label></td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable" style="width: 100%">
                        <tr>                               
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblCardNo" runat="server" SkinID="lblMain" >卡號:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="100" SkinID="txtMain" Width="200px"></asp:TextBox>  
                            </td>
                        </tr>
                        <tr>
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblStaffName" runat="server" SkinID="lblMain">姓名:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtStaffName" runat="server" MaxLength="100" SkinID="txtMain" Width="200px" BorderStyle="Groove" Enabled="False" ReadOnly="True"></asp:TextBox></td>                            
                        </tr>
                        <tr>
                            <td class="TdFindRight" style="width: 50px; height: 22px;">
                                <asp:Label ID="lblCompany" runat="server" SkinID="lblMain">公司:</asp:Label></td>
                            <td class="TdFindLeft" style="height: 22px">
                                <asp:TextBox ID="txtCompany" runat="server" MaxLength="100" SkinID="txtMain" Width="300px" BorderStyle="Groove" Enabled="False" ReadOnly="True"></asp:TextBox></td>                        
                         </tr>
                         <tr>
                            <td colspan="2" class="TdMessage">
                            <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
                         </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnOutCompany" runat="server" SkinID="MainButton"
                        Text="出廠" OnClick="btnOutCompany_Click" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
