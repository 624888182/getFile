<%@ Page Language="C#" enableViewStateMac="false"  AutoEventWireup="true" CodeFile="MainNokFox.aspx.cs" Inherits="Main_MainNokFox"
    StylesheetTheme="SkinFile" %>

<%@ Register Assembly="C1.Web.Command.2" Namespace="C1.Web.Command" TagPrefix="c1c" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nokia SCM Syetem</title>
    <style type="text/css">
     .navPoint 
      { FONT-SIZE: 10px; CURSOR: hand; COLOR: Blue; FONT-FAMILY: Webdings }
        .style1
        {
            height: 116%;
            width: 78px;
        }
        .style2
        {
            background-color: #F3F7F9;
            border: solid 1px #69B2E4;
            height: 116%;
            width: 83%;
        }
        .style3
        {
            height: 34px;
        }
       
        .style7
        {
            height: 28px;
        }
        .style8
        {
            height: 34px;
            width: 674px;
            font-weight: 700;
        }
        .style9
        {
            width: 674px;
        }
        .style10
        {
            height: 48px;
            width: 674px;
            font-weight: 700;
        }
        .style11
        {
            height: 48px;
        }
        .style12
        {
            height: 24px;
            width: 674px;
            font-weight: 700;
        }
        .style13
        {
            height: 24px;
        }
        .style14
        {
            height: 44px;
            width: 674px;
            font-weight: 700;
        }
        .style15
        {
            height: 44px;
        }
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
            <table style="height: 111%">
                <tr>
                    <td colspan="3" class="header">
                        <div id="logo">
                        </div>
                        <div class="banner">
                            &nbsp;&nbsp;
                        <asp:TextBox ID="TextBox3" runat="server" Height="20px" Width="235px"></asp:TextBox>
&nbsp;&nbsp;
                        <asp:Button ID="Button3" runat="server" Height="19px" onclick="Button3_Click" 
                            Text="GoToNewSite" Width="85px" />
                        &nbsp;&nbsp;<asp:Button ID="Button4" runat="server" Height="19px" onclick="Button4_Click" 
                            Text="Authentication" Width="101px" BackColor="White" BorderColor="#FF99FF" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="nav">
                        <div class="welcome" style="color: red">
                            <asp:Label ID="lblUserName" runat="server" Text="Admin ," ForeColor="Black"></asp:Label>&nbsp; Welcome!</div>
                        <div class="banner02">
                            <div id="home">
                                <ul>
                                    <li><a href="MainMS.aspx">Home</a> | </li>
                                   
                                    <li>
                                        <asp:LinkButton ID="LinkButton2" runat="server" onclick="btnLogout_Click" 
                                            Visible="False">Logout</asp:LinkButton>
                                        |</li>
                                  
                                    <li>
                                  <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                                            Visible="False">ChangePassword</asp:LinkButton>
                                    </li>
                                  
                                </ul>
                            </div>
                        </div>
                    &nbsp; UserName:
                        <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="79px"></asp:TextBox>
&nbsp;&nbsp;&nbsp; Password:&nbsp;
                        <asp:TextBox ID="TextBox2" runat="server" Height="20px" Width="87px" 
                            ontextchanged="TextBox2_TextChanged" 
                            TextMode="Password"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Height="19px" onclick="Button1_Click" 
                            Text="Login" Width="60px" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Height="19px" onclick="Button2_Click" 
                            Text="Modi" Width="35px" Visible="False" />
                    &nbsp;&nbsp; SecPS:&nbsp;
                        <asp:TextBox ID="TextBox5" runat="server" Height="20px" Width="87px" 
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td id="TopicBar" class="leftMenu" style="width: 20%; height: 100%;" valign="top">
                        <table cellpadding="0" cellspacing="0" style="width: 36%; height: 75%;">
                            <tr>
                                <td style="width: 100%; height: 668px;" valign="top">
                        <%--<div id="time">
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
                            <asp:Button ID="Button5" runat="server" Height="20px" onclick="Button5_Click" 
                            Text="20110409" Width="73px" BackColor="White" />
                        &nbsp;<br />
                            <asp:Button ID="Button8" runat="server" Height="20px" onclick="Button8_Click" 
                            Text="PC Test" Width="73px" BackColor="White" />
                        &nbsp;<asp:Button ID="Button10" runat="server" Height="20px" onclick="Button4_Click" 
                            Text="Picture" Width="73px" BackColor="White" />
                        &nbsp;<br />
                            <asp:Button ID="Button11" runat="server" Height="20px" onclick="Button4_Click" 
                            Text="Picture" Width="73px" BackColor="White" />
                            &nbsp;<asp:Button ID="Button12" runat="server" Height="20px" onclick="Button4_Click" 
                            Text="Picture" Width="73px" BackColor="White" />
                        &nbsp;
                            <asp:Button ID="Button13" runat="server" Height="20px" onclick="Button4_Click" 
                            Text="Picture" Width="73px" BackColor="White" />
                            &nbsp;<asp:Button ID="Button14" runat="server" Height="20px" onclick="Button4_Click" 
                            Text="Picture" Width="73px" BackColor="White" />
                        &nbsp;
                    
                            c
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                <ContentTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="145px" 
                                ImageUrl="~/Picture/PictS1.jpg" Width="149px" />
                            <br />
                            <asp:Button ID="Button15" runat="server" Text="Screen Desp" Width="160px" 
                                BackColor="White" />
                            <br />
                            <asp:Button ID="Button16" runat="server" Text="Screen Desp" Width="160px" 
                                BackColor="White" />
                            <br />
                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="143px" 
                                ImageUrl="~/Picture/PictS1.jpg" Width="154px" /> 
                                        <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="1800000">
                                    </asp:Timer>
                                    <br />
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                                &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="copyRight">
                                    CopyRight &copy; 2010<br />
                                    SSI Develop</td>
                            </tr>
                        </table>
                    </td>
                    <td id="SwitchBar" onclick="switchTopicBar()" style="vertical-align: middle; " 
                        class="style1">
                        <span id="switchPoint" class="navPoint" style="font-size: 8px">3</span>
                    </td>
                    <td class="style2">
                        
                        <table style="width: 100%; height: 703px;">
                            <tr>
                                <td class="style6">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    MS SCM, SFC&nbsp; IT Management System&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button17" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="System Auto" Width="117px" onclick="Button17_Click" />
                                </td>
                                <td class="style7">
                                </td>
                                <td class="style7">
                                </td>
                            </tr>
                            <tr>
                                <td class="style8">
&nbsp; 1. System Management&nbsp;&nbsp;:&nbsp; BB SCM SFC Web&nbsp;&nbsp;&nbsp;&nbsp;FoxSite :&nbsp;
                        <asp:TextBox ID="TextBox9" runat="server" Height="20px" Width="42px" 
                                        ontextchanged="TextBox8_TextChanged"></asp:TextBox>
                                &nbsp;&nbsp;LF/BJ/LH&nbsp;&nbsp; 
                                    <asp:Button ID="Button83" 
                                        runat="server" Height="19px" onclick="Button83_Click" 
                            Text="Confirn U" Width="65px" />
                                &nbsp; Run:
                        <asp:TextBox ID="TextBox12" runat="server" Height="20px" Width="42px" 
                                        ontextchanged="TextBox8_TextChanged"></asp:TextBox>
                                </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                </td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;2. ZZ&nbsp; SFC Link Tiptop&nbsp;&nbsp;
                                    <asp:Button ID="Button74" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="102px" onclick="Button74_Click" />
                                &nbsp;
                                    <asp:Button ID="Button44" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="139px" onclick="Button44_Click" />                                             
                                &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button73" runat="server" 
                                        BackColor="#99FF99" BorderColor="White" BorderStyle="Dotted" 
                                        ForeColor="#CC00FF" Height="20px" Text="OPEN" Width="80px" 
                                        onclick="Button73_Click" />
                                &nbsp; 
                                    <asp:Button ID="Button51" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="65px" onclick="Button51_Click" />
                                </td>
                                <td class="style3">
                                    &nbsp;&nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;&nbsp;&nbsp;&nbsp; INPUT &nbsp;<asp:TextBox ID="TextBox7" runat="server" Height="20px" Width="24px"></asp:TextBox>&nbsp; 1)&nbsp; Days (2) DN (3) WO(L6) (4) WO(L10) //&nbsp; Data :&nbsp;
                        <asp:TextBox ID="TextBox8" runat="server" Height="20px" Width="124px" 
                                        ontextchanged="TextBox8_TextChanged"></asp:TextBox>
                                </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10">
                                &nbsp;3. SCM System&nbsp;<asp:Button ID="Button26" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="940 Query" Width="90px" onclick="Button26_Click" Enabled="False" />
                                    &nbsp;<asp:Button ID="Button49" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="204 Query" Width="91px" onclick="Button49_Click" Enabled="False" />
                                    &nbsp;<asp:Button ID="Button27" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="3B2 Query" Width="90px" onclick="Button27_Click" Enabled="False" />
                                    &nbsp;<asp:Button ID="Button89" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        onclick="Button89_Click" Text="3A4 Query" Width="91px" Enabled="False" />
                                            &nbsp;<asp:Button ID="Button5" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        onclick="Button5_Click" Text="3A4 Cancel" Width="91px" Enabled="False" />
                                        &nbsp;</td>
                                <td class="style11">
                                    </td>
                                <td class="style11">
                                    </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                                                      &nbsp;4. SCM System&nbsp; <asp:Button ID="Button91" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        onclick="Button91_Click" Text="POTraceReport" Width="95px" Enabled="False" />
                                         &nbsp;
                                    <asp:Button ID="Button90" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        onclick="Button90_Click" Text="PO Integration" Width="91px" />
                                    &nbsp;<asp:Button ID="Button85" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="DNN Query" Width="87px" onclick="Button85_Click" Enabled="False" />
                                    &nbsp;<asp:Button ID="Button86" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="DN-Confirm" Width="80px" onclick="Button86_Click" />
                                    &nbsp;<asp:Button ID="Button52" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="85px" onclick="Button52_Click" />
                               
                                </td>
                                <td class="style11">
                                    &nbsp;</td>
                                <td class="style11">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                                &nbsp;5. SCM DN System&nbsp;&nbsp;DN :
                          <asp:TextBox ID="TextBox10" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; DBA :&nbsp;
                          <asp:TextBox ID="TextBox11" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                &nbsp;&nbsp; PO :&nbsp;
                          <asp:TextBox ID="TextBox13" runat="server" Height="20px" Width="75px"></asp:TextBox>
                                &nbsp;
                                    <asp:Button ID="Button57" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="31px" onclick="Button57_Click" />
                                    &nbsp;<asp:Button ID="Button75" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="23px" onclick="Button75_Click" />
                                    </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                        &nbsp;6. SCM WebSer&nbsp;:&nbsp; <asp:Button ID="Button62" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Recv" Width="80px" onclick="Button62_Click1" />
                                    &nbsp;<asp:Button ID="Button64" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Manage" Width="80px" onclick="Button64_Click" />
                                    &nbsp;<asp:Button ID="Button36" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Conf SO " Width="80px" onclick="Button36_Click" />
                                &nbsp;<asp:Button ID="Button92" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="3B2 Message" Width="80px" onclick="Button92_Click" />
                                &nbsp;<asp:Button ID="Button71" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="19px" 
                                        Text="DN up PO" Width="75px" onclick="Button71_Click" />
                                &nbsp;<asp:Button ID="Button93" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="Automatic" Width="60px" onclick="Button93_Click" />
                                    </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                                &nbsp;7. SCM WebSer&nbsp;:&nbsp;                                           <asp:Button ID="Button65" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Check" Width="80px" onclick="Button65_Click" />
                                &nbsp;<asp:Button ID="Button66" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Gen SO" Width="80px" onclick="Button66_Click" />
                                    &nbsp;<asp:Button ID="Button67" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="DN Fro Sap" Width="80px" onclick="Button67_Click" />
                                    &nbsp;<asp:Button ID="Button72" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="3B2ChkSFC" Width="80px" onclick="Button72_Click" />
                                    &nbsp;<asp:Button ID="Button87" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="3B2 S XML" Width="80px" onclick="Button87_Click" />
                                    </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style12">
                                  &nbsp; 8. OutSource&nbsp;&nbsp;&nbsp; :&nbsp;                                             
                                    <asp:Button ID="Button76" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="PO Query" Width="80px" onclick="Button76_Click" />
                                &nbsp;<asp:Button ID="Button77" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OutS 3B2" Width="80px" onclick="Button77_Click" />
                                    &nbsp;<asp:Button ID="Button78" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="3B2 Call Sap" Width="80px" onclick="Button78_Click" />
                                    &nbsp;<asp:Button ID="Button63" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="Sap Invoice" Width="80px" onclick="Button63_Click" />
                                    &nbsp;<asp:Button ID="Button88" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="OPEN" Width="80px" onclick="Button88_Click" />
                                    </td>
                                <td class="style13">
                                    &nbsp;</td>
                                <td class="style13">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style14">
                                    &nbsp;9. E-Kanban&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;<asp:Button ID="Button80" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="22px" 
                                        Text="OPEN" Width="80px" onclick="Button80_Click" />
                                    &nbsp;<asp:Button ID="Button81" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="22px" 
                                        Text="OPEN" Width="80px" onclick="Button81_Click" />
                                    &nbsp;<asp:Button ID="Button82" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="22px" 
                                        Text="OPEN" Width="80px" onclick="Button82_Click" />
                                    &nbsp;
                                      <asp:Button ID="Button79" 
                                        runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="22px" 
                                        Text="OPEN" Width="80px" onclick="Button79_Click" />
                                    </td>
                                <td class="style15">
                                    &nbsp;</td>
                                <td class="style15">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;Currency Status :&nbsp;<asp:TextBox ID="TextBox14" runat="server" 
                                        Height="20px" Width="522px"></asp:TextBox>
                                    &nbsp;</asp:TextBox>
                                    </asp:TextBox>
                                &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style9">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style9">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        
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
