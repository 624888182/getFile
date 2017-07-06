<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPwd03.aspx.cs" Inherits="MainSetPwd03" %>



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
    <p style="font-family:Tahoma;font-size:large;color:Blue">轉換 Password To Txt&nbsp;&nbsp; 
        Date :&nbsp;
                            <asp:TextBox ID="TextBox11" runat="server" Width="93px"></asp:TextBox>
            </p>
    <div id="login_div" style="margin-top:20px;width:320px;padding:5px;background-color:#F0F8FF;text-align:center;font-size:10pt;border:solid 1px #6495ED;height: 99px;">
            <table cellpadding="0" cellspacing="0" style="width: 190%">
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
                    <asp:Button ID="Button1" runat="server" Text="取的密碼" OnClick="Button1_Click" 
                        Width="64px" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="返回" onclick="Button2_Click" 
                        Width="38px" />
                    &nbsp;
                    <asp:Button ID="Button5" runat="server" Text="轉換密碼 Pass" BackColor="#FF0066" 
                        Width="97px" onclick="Button5_Click" />
                    &nbsp;&nbsp;<asp:Button ID="Button10" runat="server" Text="轉換密碼 DBF" BackColor="#FF0066" 
                        Width="95px" onclick="Button10_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button6" runat="server" Text="讀取WebReadParam.txt" BackColor="#FF0066" 
                        Width="150px" onclick="Button6_Click" />
                    &nbsp;<asp:Button ID="Button7" runat="server" Text="Test密碼LinK" BackColor="#FF0066" 
                        Width="83px" onclick="Button7_Click" />
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 系統管理者：&nbsp;&nbsp;&nbsp;                             <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 系統管理密碼：<asp:TextBox 
                        ID="TextBox5" runat="server" TextMode="Password" Height="20px" 
                        style="margin-bottom: 0px" Width="122px"></asp:TextBox>
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 加密碼 :&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox 
                        ID="TextBox10" runat="server" 
                Width="125px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; ( Best Short then Password ) ( Need 系統管理者)(1ken)</td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    ORGCODE:&nbsp;&nbsp;                     <asp:TextBox ID="TextBox7" 
                runat="server" Width="489px"></asp:TextBox>
        &nbsp;</td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    ENCODE:&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox8" runat="server" 
                Width="496px"></asp:TextBox>
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    DECODE:&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox9" runat="server" 
                Width="497px"></asp:TextBox>
                    </td></tr>
              
                <tr><td colspan='2' style="border:0px;text-align:center;height:35px">
                    <asp:Button ID="Button8" runat="server" Text="Set Sec-Pass" OnClick="Button8_Click" 
                        Width="90px" />&nbsp;
                    <asp:Button ID="Button9" runat="server" Text="Get Daily Sec-Pass" OnClick="Button9_Click" 
                        Width="136px" />&nbsp;:
                            <asp:TextBox ID="TextBox12" runat="server" Width="83px"></asp:TextBox>
                        &nbsp;&nbsp;
                            <asp:TextBox ID="TextBox13" runat="server" Width="105px" Height="22px"></asp:TextBox>
                        </td></tr>
              
            </table>
     </div>
     </center>
        <center>
            &nbsp;</center>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
