<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJTraceMenu.aspx.cs" Inherits="Traceability_TJTraceMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .style1
        {
            height: 32px;
        }
        .style2
        {
            height: 46px;
        }
        .style3
        {
            width: 232px;
        }
        .style5
        {
            height: 32px;
            width: 232px;
        }
        .style6
        {
            height: 46px;
            text-align: left;
        }
        .style7
        {
            height: 32px;
            text-align: left;
        }
        .style8
        {
            text-align: left;
        }
        .style9
        {
            width: 232px;
            text-align: left;
        }
    </style>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="style9">
                Traceability</td>
            <td class="style8">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;小板(PID)查詢 </td>
            <td class="style8">
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/App_Themes/SkinFile/images/query.gif" 
                    PostBackUrl="~/Traceability/Trace_PID.aspx" />
            </td>
            <td class="style8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style7">
                大板貼片信息查詢</td>
            <td class="style6">
                <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/App_Themes/SkinFile/images/query.gif" 
                    PostBackUrl="~/Traceability/Trace_BigSN.aspx" />
            </td>
            <td class="style2">
                </td>
        </tr>
        <tr>
            <td class="style3">
                <span id="Label1">上料表信息查詢</span></td>
            <td>
                <asp:ImageButton ID="ImageButton3" runat="server" 
                    ImageUrl="~/App_Themes/SkinFile/images/query.gif" 
                    PostBackUrl="~/Traceability/Trace_DiskNo.aspx" />
            </td>
            <td>
                </td>
        </tr>
        <tr>
            <td class="style5">
                具體來料信息查詢 </td>
            <td class="style1">
                <asp:ImageButton ID="ImageButton4" runat="server" 
                    ImageUrl="~/App_Themes/SkinFile/images/query.gif" 
                    PostBackUrl="~/Traceability/Trace_Support.aspx" />
            </td>
            <td class="style1">
                </td>
        </tr>
    </table>
</form>
</body>
</html>
