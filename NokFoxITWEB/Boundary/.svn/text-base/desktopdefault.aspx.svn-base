<%@ Page language="c#" Inherits="Boundary.Web.APS.DesktopDefault"  EnableEventValidation = "false" AutoEventWireup="true"  CodeFile="DesktopDefault.aspx.cs" %>

<%@ Register Src="../WebControler/sitefooter.ascx" TagName="sitefooter" TagPrefix="uc2" %>
<%@ Register TagPrefix="cc1" Namespace="EeekSoft.Web" Assembly="EeekSoft.Web.PopupWin" %>
<%@ Register TagPrefix="uc1" TagName="HeadBar" Src="../WebControler/HeadBar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>SFCQuery System</title> 
		<script type="text/javascript" language="JavaScript">
			var currentpos,timer; 
			function initialize() 
			
			{ 
				timer=setInterval("scrollwindow()",20);
			} 
			function sc()
			{
				clearInterval(timer); 
			}
			function scrollwindow() 
			{ 
				currentpos=document.body.scrollTop; 
				window.scroll(0,++currentpos); 
				if (currentpos != document.body.scrollTop) 
				sc();
			}
			
 
			document.onmousedown=sc
			//document.ondblclick=initialize
		</script>
		<script type="text/javascript" language="JavaScript">
			function window.onscroll()
			{
				document.all['hidPos'].value = document.body.scrollTop;
			}
			function document.onkeydown()
			{
				var e = event.srcElement;
				if ((event.keyCode==13) && (((e.tagName=="INPUT") && (e.type=="text")) || (e.tagName=="SELECT")))
				{
					event.keyCode = 9;				
				}
			}			
		</script>
		<script type="text/javascript" language="javascript">
		<!--
		    //按回車時用於將焦點傳給SignIn按鈕
			function focusSignInButton()
			{
				if (event.keyCode == 13)
				{
					var c = document.all["btnSignIn"];
					if (c != null)
					{
						c.focus();
					}
				}
			}
		//-->
		</script>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <%--每10s刷新一次--%>
        <%--<META   HTTP-EQUIV="REFRESH"   CONTENT="10;   URL=WFrmLineStatusMonitor.ascx"> --%>
		<link href="../Images/aps_logo.gif" rel="start"/>
		<link href="../WebControler/OMS.css" type="text/css" rel="stylesheet"/>
		<!--日期選擇器的樣式-->
		<link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css" title="wood" />
		<style type="text/css">
		.DataGridFixedHeader { POSITION: relative; ; TOP: expression(this.offsetParent.scrollTop); Font-Bold: True; HorizontalAlign: Left; ForeColor: White; BackColor: #006699 }
		</style>
		<%--<link rel="stylesheet" type="text/css" media="all" href="../WebControler/skins/aqua/theme.css"
			title="Aqua">--%>
		
	</head>
	<body bottomMargin="0" bgProperties="fixed" leftMargin="0" background="../images/1170744771831.jpg"
		id="MainBody" runat="server" topMargin="0" rightMargin="0">
		<form method="post" runat="server">
			<input type="hidden" name="__SCROLLPOS"> <INPUT id="hidPos" type="hidden" name="hidPos" runat="server">
			<uc1:headbar id="HeadBar1" runat="server">
			
			</uc1:headbar>	
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="bottom" style="width: 14px; height: 19px">&nbsp;</TD>
					<TD id="ContentPane" width="*" runat="server" style="height: 19px"><FONT face="新細明體"></FONT></TD>
				</TR>
			</TABLE>
			<script type="text/javascript" language="javascript">
				var cookieString = new String(document.cookie);
				var cookieHeader = "myVisible=";
				var beginPosition = cookieString.indexOf(cookieHeader);
				if (beginPosition != -1)
				{
					if (cookieString.substring(beginPosition+cookieHeader.length, beginPosition+cookieHeader.length+1) == 5)
					{
						switchPoint.innerText=5;
						switchPoint.title="Close Header";
						document.all["TableHead1"].style.display="";
					}
					else
					{
						switchPoint.innerText=6
						switchPoint.title="Open Header";
						document.all["TableHead1"].style.display="none";
					}
				}
				else
				{
					switchPoint.title="Close Header";
					switchPoint.innerText=5;
					document.all["TableHead1"].style.display="";
					document.cookie="myVisible=5";
				}
				if (document.all['hidPos'].value != "")
					document.body.scrollTop = document.all('hidPos').value;
			</script>

            
		</form>
	</body>
</html>
