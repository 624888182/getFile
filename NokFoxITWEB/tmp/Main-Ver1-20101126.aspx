<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main-Ver1-20101126.aspx.cs" Inherits="Main"
    StylesheetTheme="SkinFile" %>

<%@ Register Assembly="C1.Web.Command.2" Namespace="C1.Web.Command" TagPrefix="c1c" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SSI系统測試環境</title>
    <style type="text/css">
     .navPoint 
      { FONT-SIZE: 10px; CURSOR: hand; COLOR: Blue; FONT-FAMILY: Webdings }
    </style>

       <script language="javascript" type="text/javascript">

           function switchTopicBar() {
               if (document.getElementById("TopicBar").style.display == "none") {
                   document.getElementById("TopicBar").style.display = "";
                   document.getElementById("switchPoint").innerText = 3;
               }
               else {
                   document.getElementById("TopicBar").style.display = "none";
                   document.getElementById("switchPoint").innerText = 4;
               }
           }

            
    </script>

</head>
<body>
    <form id="fmMain" runat="server">
        <div>
            <table style="height: 100%">
                <tr>
                    <td colspan="3" class="header">
                        <div id="logo">
                        </div>
                        <div class="banner">
                            &nbsp;&nbsp;
                        <asp:TextBox ID="TextBox3" runat="server" Height="20px" Width="235px"></asp:TextBox>
&nbsp;&nbsp;
                        <asp:Button ID="Button3" runat="server" Height="19px" onclick="Button3_Click" 
                            Text="進入新網站" Width="80px" />
                        &nbsp;<asp:Button ID="Button4" runat="server" Height="19px" onclick="Button4_Click" 
                            Text="相片簿" Width="50px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="nav">
                        <div id="time"><font style="height:100%"></font>
                            <script language="JavaScript" type="text/javascript">
                                var enabled = 0; today = new Date();
                                var day; var date;
                                if(today.getDay()==0) day = "星期日"
                                if(today.getDay()==1) day = "星期一"
                                if(today.getDay()==2) day = "星期二"
                                if(today.getDay()==3) day = "星期三"
                                if(today.getDay()==4) day = "星期四"
                                if(today.getDay()==5) day = "星期五"
                                if(today.getDay()==6) day = "星期六"
                                document.fgColor = "000000";
                                date ="" + (today.getYear()) + "/" + (today.getMonth() + 1 ) + "/" + today.getDate() + " " +" ";
                                
                                document.write("<FONT COLOR=000000>" + date +'['+day+']'+"</FONT>");
                            </script>
                        </div>
                        <div class="welcome" style="color: red">
                            <asp:Label ID="lblUserName" runat="server" Text="Admin ," SkinID="lblWelcome"></asp:Label>Welcome!</div>
                        <div class="banner02">
                            <div id="home">
                                <ul>
                                    <li><a href="Main.aspx">首頁</a> | </li>
                                    <li><a>幫助</a> | </li>
                                    <li>
                                        <asp:LinkButton ID="btnLogout" runat="server" Height="7px"  >退出</asp:LinkButton>|</li>
                                </ul>
                            </div>
                        </div>
                    &nbsp; UserName:
                        <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="79px"></asp:TextBox>
&nbsp;&nbsp;&nbsp; Password:&nbsp;
                        <asp:TextBox ID="TextBox2" runat="server" Height="20px" Width="110px" 
                            ontextchanged="TextBox2_TextChanged" AutoPostBack="True"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Height="19px" onclick="Button1_Click" 
                            Text="進入" Width="35px" />
                    &nbsp;<asp:Button ID="Button2" runat="server" Height="19px" onclick="Button2_Click" 
                            Text="修改" Width="35px" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td id="TopicBar" class="leftMenu" style="width: 10%; height: 100%;" valign="top">
                        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                            <tr>
                                <td style="width: 100%; height: 668px;" valign="top">
                                    <asp:TreeView ID="tvManuTree" runat="server" CollapseImageToolTip="Collapse{0}" ExpandImageToolTip="Expand{0}"
                                        Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="12px"
                                        Height="100%" ShowLines="True" Target="_blank" Width="100%">
                                        <SelectedNodeStyle BackColor="#c0c0c0" BorderStyle="Solid" BorderColor="#c0c0c0" ImageUrl="App_Themes/SkinFile/images/add.gif"/>
                                    </asp:TreeView>
                                </td>
                            </tr>
                            <tr>
                                <td class="copyRight">
                                    CopyRight &copy; 2010<br />
                                    SSI Develop</td>
                            </tr>
                        </table>
                    </td>
                    <td id="SwitchBar" onclick="switchTopicBar()" style="vertical-align: middle; width: 1px;
                        height: 116%">
                        <span id="switchPoint" class="navPoint" style="font-size: 8px">3</span>
                    </td>
                    <td class="rightMenu" style="height: 116%; width: 100%">
                        <iframe id="Frame" frameborder="0" height="100%" name="MainFrame" scrolling="auto"
                            src="MainDesktop.aspx"  style="height: 100%; text-align: center; width: 100%;"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
