<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPwd.aspx.cs" Inherits="SetPwd" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>登錄</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <p style="font-family:Tahoma;font-size:large;color:Blue">密碼設定</p>
    <div id="login_div" style="margin-top:20px;width:320px;padding:5px;background-color:#F0F8FF;text-align:center;font-size:10pt;border:solid 1px #6495ED;height: 99px;">
            <table cellpadding="0" cellspacing="0" style="width: 104%">
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">工   號：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                    <asp:TextBox ID="user_id" runat="server" Width="120px"></asp:TextBox></td></tr>
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">密   碼：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                    <asp:TextBox ID="user_pwd" runat="server" Width="120px" TextMode="Password"></asp:TextBox></td></tr>
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">
                    資訊系統：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td></tr>
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">
                    第2 密碼 ：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                            <asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                        </td></tr>
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">
                    新密碼 ：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
                        </td></tr>
                <tr><td style="border:0px;height:35px;text-align:right;border-bottom:1px solid activeborder; width: 131px;">
                    新密碼：</td><td style="border:0px;border-bottom:1px solid activeborder; width: 6px;">
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox>
                        </td></tr>
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click" 
                        Width="38px" />&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Text="修改" Width="38px" 
                        onclick="Button4_Click" />&nbsp;&nbsp;<asp:Button 
                        ID="Button3" runat="server" Text="刪除" onclick="Button3_Click" 
                        Width="35px" />&nbsp;&nbsp;<asp:Button ID="ok" runat="server" Text="查尋" 
                        onclick="ok_Click" Width="39px" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="返回" onclick="Button2_Click" />
                    &nbsp;
                    <asp:Button ID="Button5" runat="server" Text="返回" BackColor="#FF0066" 
                        Width="83px" />
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 系統管理者：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 系統管理密碼：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox5" runat="server" TextMode="Password"></asp:TextBox>
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    1. The Password will been transfer for protect after save....</td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    2. Password will convert to&nbsp; CheckSumNum for check only&nbsp;
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    3. ID need setup system seq and UserID for Index key.......&nbsp;
                    </td></tr>
              
            </table>
     </div>
     </center>
        <center>
            &nbsp;</center>
    </form>
</body>
</html>
