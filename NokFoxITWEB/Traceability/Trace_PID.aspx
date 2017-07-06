<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trace_PID.aspx.cs" Inherits="Temp_Trace_BigSN" Theme="" StyleSheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style17
        {
        }
        
td { font-size:12px;
            }
	
        .style19
        {
            font-size: medium;
        }
        .style22
        {
            font-size: medium;
        }
        </style>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="小板(PID)查詢 "></asp:Label>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;
        <table  border="1" bordercolor="#CCCCFF" cellspacing="0" cellpadding="0"  
                      
            
        style="border-collapse: collapse;  text-align: left; width: 1131px; height: 63px;">
            <tr>
                <td class="style17" colspan="2">
                    <strong>&nbsp;</strong><span class="style19">條 碼 </span>
                    <strong>: </strong>
                    <asp:TextBox ID="TextBox1" runat="server" Height="23px"></asp:TextBox>
&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="22px" 
                        ImageUrl="~/App_Themes/SkinFile/images/search.gif" onclick="ImageButton1_Click" 
                        Width="69px" />
                &nbsp;<asp:LinkButton ID="LinkButton13" runat="server" 
                        PostBackUrl="~/Traceability/TJTraceMenu.aspx">BACK</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;主板條碼:&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;投入時間:&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;當前工令:&nbsp;
                    <asp:LinkButton ID="LinkButton3" runat="server"></asp:LinkButton>
                </td>
                <td>
                    <!-- 
			<input type="text" name="FoxconnPlant" value =""/>
			-->
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;鴻海料號:&nbsp;
                    <asp:LinkButton ID="LinkButton4" runat="server"></asp:LinkButton>
                </td>
                <td>
                    <strong></strong></td>
            </tr>
            <tr>
                <td class="style19">
                    <strong>&nbsp;</strong>大板條碼:&nbsp;
                    <asp:LinkButton ID="LinkButton5" runat="server"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp; 機種名稱:&nbsp;
                    <asp:LinkButton ID="LinkButton6" runat="server"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp; 工單數量:&nbsp;
                    <asp:LinkButton ID="LinkButton7" runat="server"></asp:LinkButton>
                </td>
                <td>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="style22" colspan="2">
                    <strong>&nbsp;</strong><asp:Label ID="Label2" runat="server"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" 
                        Width="822px" SkinID="gvMain" AllowPaging="True" ShowFooter="True">
<Columns>
 <asp:BoundField DataField="rownum" HeaderText="編號" />
 <asp:BoundField DataField="in_line_time" HeaderText="入站時間" />
 <asp:BoundField DataField="line_name" HeaderText="線別" />   
 <asp:BoundField DataField="station_name" HeaderText="站名" />  
 <asp:BoundField DataField="emp_no" HeaderText="作業員" />   

 




 </Columns>
                    </asp:GridView>
                </td>
            </tr>
            
            </table>
    
    </form>
</body>
</html>
