<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DB_Sort.aspx.cs" Inherits="MainMotPrg_DB_Sort" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        	width: 500px;
        }
        .style3
        {
            width: 85px;
        }
        .style4
        {
            width: 296px;
        }
        /*绝对定位 + z-index */ 
.progressbar_3{ 
    background-color:#eee; 
    color:#222; 
    height:16px; 
    width:150px; 
    border:1px solid #bbb; 
    text-align:center; 
    position:relative; 
} 
.progressbar_3 .bar { 
    background-color:#6CAF00; 
    height:16px; 
    width:0; 
    position:absolute; 
    left:0; 
    top:0; 
    z-index:10; 
} 
.progressbar_3 .text { 
    height:16px; 
    position:absolute; 
    left:0; 
    top:0; 
    width:100%; 
    line-height:16px; 
     
    z-index:100; 
} 
    </style>
   <%-- <script type="text/javascript">
        var c = 0; 
        var t;
        function do_click() {
            document.getElementById("aaa").innerText = "正在执行后台操作 ... ...";
            document.getElementById("ww").style.display = "block";

            //document.getElementById("text").innerText = c + "%";
//            document.getElementById("text").innerText = c + "分鐘";
//            document.getElementById("bar1").style.width = c + "%";
//            c = c + 1;
//            // c = "<%=Xxx()%>";
//            //t = setTimeout("do_click()", 1000);
//  
//    
//            t = setTimeout("do_click()", 60000);
//            if (c == 150) {
//                clearTimeout(t)
//                
//            }
            
        }
        setTimeout("location.href='DB_Sort.aspx'", 1800000);
    </script>--%>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="Create_Date:"></asp:Label>
                </td>
                <td class="style4">
                    <asp:DropDownList ID="DDLYear" runat="server">
                    </asp:DropDownList>
                &nbsp;年 
                    <asp:DropDownList ID="DDLMM" runat="server">
                    </asp:DropDownList>
&nbsp;月</td>
                <td>
                    
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    <asp:Button ID="Button1" runat="server" Text="Create_Data" 
                        onclick="Button1_Click" />
               <%-- &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Check215" 
                        onclick="Button2_Click" />
                        --%>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" colspan="8">
                    <asp:Label ID="LbMassage" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnY" runat="server" Text="是" onclick="BtnY_Click" 
                        Visible="False" />
                    &nbsp;
                    <asp:Button ID="BtnN" runat="server" Text="否" onclick="BtnN_Click" 
                        Visible="False" />
                        <label id="Lbs" runat="server" style="color: #FF0000"></label>
                </td>
            </tr>
        </table>
    
       
    
    </div>

    
  

    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                 
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div id="ww" runat="server" style="DISPLAY: none; LEFT: 10px; WIDTH: 450px; POSITION: absolute; TOP: 150px; HEIGHT: 150px"> 
                <table height="100%" width="100%" bgColor="#66CCFF" border="1"> 
                    <tr> 
                        <td vAlign="middle" align="center">
                            <img id="jz" src="~/Images/loading.gif" runat="server" />
                            <label id="aaa" runat="server"></label>
                        </td> 
                    </tr> 
                    <tr> 
                        <td vAlign="middle" align="center">
                            <div class="progressbar_3"> 
                                <div class="text" ><label id="text" runat="server" ></label></div> 
                                <div class="bar" id="bar1" runat="server" ></div> 
                            </div>  
                        </td>
                    </tr> 
                </table> 
    </div> 
            
            
                <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="1000">
                </asp:Timer>
            </ContentTemplate>
            </asp:UpdatePanel>   
    </p>



    </form>
</body>
</html>
