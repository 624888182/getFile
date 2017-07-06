<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NLVDailyIncoming.aspx.cs" Inherits="UploadData_DailyIncoming" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Original Of NLV Daily Incoming</title>
    <link href="../CSS/Upload.css" type="text/css" rel="stylesheet" />
    <link href="../CSS/StyleSheet.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../javascript/Calendar.js"></script>
    <style type="text/css">
        .style1
        {
            width: 416px;
        }
        .style2
        {
            width: 576px;
        }
        .style3
        {
            width: 81px;
        }
        .style4
        {
            height: 25px;
            width: 81px;
        }
        .style5
        {
            width: 576px;
            height: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <center><div style="text-align: center">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="title">
            GSCMD &amp; Syncro ETDToETA, CurrNextDosMPQ</div>
        <br />
    <hr />
        <br />
        <table style="width:500px">
            <%--<tr>
                <td style="width: 150px; text-align: right;">
                    請選擇幣種：</td>
                <td style="text-align: left" >
                <asp:DropDownList ID="ddlCurrency" runat="server">
                            <asp:ListItem />
                            <asp:ListItem Value="RMB" Text="RMB" />
                            <asp:ListItem Value="USD" Text="USD" />
                            <asp:ListItem Value="KUSD" Text="KUSD" />
                            <asp:ListItem Value="新臺幣" Text="新臺幣" />
                            <asp:ListItem Value="新臺幣千元" Text="新臺幣千元" />
                        </asp:DropDownList></td>
            </tr>        --%>    
            <tr>
                <td style="text-align: right" class="style3">
                                        日期：</td>
                <td style="text-align: left" class="style1">
                        <asp:TextBox ID="textBox1" onclick="showCalendar();" runat="server" 
                            Width="73px"></asp:TextBox>&nbsp;<asp:Button ID="btnUpload5" runat="server" 
                            Text="參數設定" OnClick="btnUpload5_Click" Height="25px" Width="61px" />&nbsp;
                        <asp:TextBox ID="textBox6" runat="server" 
                            Width="19px"></asp:TextBox>&nbsp;<asp:TextBox ID="textBox7" 
                            runat="server" Width="19px"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnUpload6" runat="server" Text="執行設定" OnClick="btnUpload6_Click" 
                            Height="25px" Width="65px" />
                        <asp:Button ID="btnUpload3" runat="server" Text="測試ETAD" 
                            OnClick="btnUpload3_Click" Height="25px" Width="75px" />
                    </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style3">
                    初始：</td>
                <td style="text-align: left" class="style1">
                        <asp:FileUpload ID="FileUpload1" runat="server" Height="25px" />
                        <asp:Button ID="btnUpload" runat="server" Text="導  入" OnClick="btnUpload_Click" Height="25px" Width="84px" />
                        <asp:Button ID="btnUpload7" runat="server" Text="測試Ship" 
                            OnClick="btnUpload7_Click" Height="25px" Width="75px" />
                    </td>
            </tr>
<tr>
            <td class="style4"></td>
            </tr>
            <tr>
                <td style="text-align: right" class="style3">
                    營收：</td>
                <td style="text-align: left" class="style1">
                        <asp:FileUpload ID="FileUpload2" runat="server" Height="25px" />
                        <asp:Button ID="btnUpload2" runat="server" Text="導  入" 
                            OnClick="btnUpload2_Click" Height="25px" Width="62px" />
                        </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="textBox8" onclick="showCalendar();" runat="server" 
                            Width="16px"></asp:TextBox>
                    </td>
            </tr>
         <tr>
                    <td style="height: 100px" colspan="2">
                    &nbsp;
                        <asp:TextBox ID="textBox2" onclick="showCalendar();" runat="server" 
                            Width="24px"></asp:TextBox>
                        <asp:Button ID="btnUpload4" runat="server" Text="測試 ReqProcETDToETAGSCMD" 
                            OnClick="btnUpload3_Click" Height="30px" Width="86px" 
                            style="margin-left: 0px" />
                        <asp:TextBox ID="textBox3" onclick="showCalendar();" runat="server" 
                            Height="21px" Width="79px"></asp:TextBox>
                        <asp:TextBox ID="textBox4" onclick="showCalendar();" runat="server" 
                            Width="96px"></asp:TextBox>
                        <asp:TextBox ID="textBox5" onclick="showCalendar();" runat="server" 
                            Width="61px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px" class="table1">
                <tr>
                    <td rowspan="2" style="width: 130px">
                        操作提示: &nbsp; &nbsp;&nbsp;</td>
                    <td style="text-align: left; " class="style5">
                        參數設定:
                        <br />
                        1. 第 1 位置 D 表清 3 資料庫 MemoryETDToETA,&nbsp; E 表清 ReqProc<br />
                        2. 第 1 位置 A 表 回寫&nbsp; Syncro 其他到 Test_syncro                         <br />
                        3. 第 1 位置 F 表清 MemoryCurrNextDosMPQ </td>
                </tr>
                <tr>
                    <td style="text-align: left; " class="style2">
                        4. 第 1 位置 G 表清 TestDBFShipPlan&nbsp; 第 2 位置 Y 表回寫 <br />
                        5.&nbsp;第 1 位置 H 表回寫 Download CurrDos AarraytransDay 到資料庫對帳</td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 130px">
                        上傳的Excel模板: &nbsp; &nbsp;&nbsp;
                    </td>
                    <td style="text-align: left; " class="style2">
                        <a href="../ExcelTemplate/NLV/NLV每日營收進度表初始報表.xls" target="_blank">NLV每日營收進度表初始報表.xls</a>
                        &nbsp; &nbsp;Upgadre V12 <a href="../ExcelTemplate/NLV/NLV每日營收進度表最終表.xls">
                        20100427 </a>&nbsp;</td>
                </tr>
            </table>
    </div>
        </center>
    </form>
</body>
</html>
