<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JDeleteData.aspx.cs" Inherits="Pub_JDeleteData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 50px;
        }
        .style3
        {
            width: 650px;    
        }
        .style4
        {
            width:auto;
        }
        .style6
        {
            width:105px;
        }
        .style7
        {
            width: 105px;
        }
        .style8
        {
            width: 105px;
        }
        #div2
        {
            width: 521px;
        }
    </style>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td align="center" colspan="6" class = "style3" >
                    <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="已備份數據刪除"></asp:Label>
                    </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style6">
                    <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                </td>
                <td class="style8">
                    <asp:TextBox ID="TextBox1" runat="server" Width="105px"
                    onkeypress="javascript:event.returnValue=false;" onclick="showCalendar();"></asp:TextBox>
                </td>
                <td class="style6">
                &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="TextBox2" runat="server" Width="105px" 
                    onkeypress="javascript:event.returnValue=false;" onclick="showCalendar();"></asp:TextBox>
                </td>
                <td class="style7">
                    <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="style7">
                    <asp:TextBox ID="TextBox3" runat="server" Width="105px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style6">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Table1" />
                </td>
                <td class="style8">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Table2" />
                </td>
                <td class="style6">
                    &nbsp;
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Table3" />
                    </td>
                <td class="style7">
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="Table4" />
                </td>
                <td class="style7">
                    <asp:Button ID="btn2" runat="server" Text="Note" Width="62px" 
                        onclick="btn2_Click" />
                </td>
                <td class="style7">
                    <asp:Button ID="Delete" runat="server" Text="Delete" Width="62px" 
                        onclick="Delete_Click" />
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3" colspan="6">
                    <div id="div2" runat ="server">
                        &nbsp;Table1: SHP.CMCS_SFC_IMEINUM<br />
                        &nbsp;Table2: SFC.MES_ASSY_PID_JOIN<br />
                        &nbsp;Table3: SFC.MES_ASSY_HISTORY<br />
                        &nbsp;Table4: SFC.R_WIP_TRACKING_T_PID<br />
                        &nbsp;Date Format YYYY/MM/DD
                    </div>
                    &nbsp;<asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                    <br />
                    &nbsp;<asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                    <br />
                    &nbsp;<asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                    <br />
                    &nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                    
                    </td>
                <td class="style4">
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
   
    </form>
</body>
</html>
