<%@ Control Language="c#" Inherits="WebControler.APS.HeadBar" CodeFile="HeadBar.ascx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="SolpartWebControls" Assembly="SolpartWebControls" %>
<STYLE>.navPoint { FONT-SIZE: 12pt; CURSOR: hand; COLOR: white; FONT-FAMILY: Webdings }
</STYLE>
<style type="text/css">.DataGridFixedHeader { POSITION: relative; ; TOP: expression(this.offsetParent.scrollTop) }
</style>
<script language="javascript">
function switchSysBar()
{
	if (switchPoint.innerText==5)
	{
		switchPoint.innerText=6
		switchPoint.title="Open Header";
		document.all["TableHead1"].style.display="none";
		document.cookie="myVisible=6";	
	}
	else
	{
		switchPoint.innerText=5;
		switchPoint.title="Close Header";
		document.all["TableHead1"].style.display="";
		document.cookie="myVisible=5";
	}
}
</script>
<TABLE id="TableHead1" height="80" cellSpacing="0" cellPadding="0" width="100%" background="../Images/main_left.gif"
	border="0">
	<TR>
		<TD width="42"><IMG style="HEIGHT: 33px" height="33" alt="" isMap src="../Images/FIH.ico" width="50"></TD>
		<TD align="left"><asp:label id="lblSiteName" tabIndex="-1" Font-Bold="True" runat="server" ForeColor="White"
				Font-Size="Medium"> SFCQuery</asp:label></TD>
		<!--<TD vAlign="middle" noWrap align="right" width="15%"><FONT face="·s²Ó©úÅé"></FONT></TD>-->
		<td vAlign="top" noWrap align="right" width="30%">
			<table border="0">
				<tr>
					<td style="HEIGHT: 20px"><asp:hyperlink id="hlWelCome" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt" Font-Names="Arial">WelCome !</asp:hyperlink>
						<DIV style="DISPLAY: inline; FONT-SIZE: 9pt; WIDTH: 10px; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 16px"
							align="center">|</DIV>
						<asp:hyperlink id="hlHome" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt" Font-Names="Arial">Home</asp:hyperlink>
						<DIV style="DISPLAY: inline; FONT-SIZE: 9pt; WIDTH: 10px; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 16px"
							align="center">|</DIV>
						<asp:hyperlink id="hlChangePass" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt"
							Font-Names="Arial" Visible="False">Change Password</asp:hyperlink>
						<DIV style="DISPLAY: inline; FONT-SIZE: 9pt; WIDTH: 10px; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 16px"
							align="center"></DIV>
						<asp:linkbutton id="hlDefaultPage" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt"
							Font-Names="Arial" Visible="False" CausesValidation="False" onclick="hlDefaultPage_Click">Default Page</asp:linkbutton>
						<DIV style="DISPLAY: inline; FONT-SIZE: 9pt; WIDTH: 10px; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 16px"
							align="center">|</DIV>
						<asp:hyperlink id="hlLogOut" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt" Font-Names="Arial">LogOut</asp:hyperlink>&nbsp;
						<DIV style="DISPLAY: inline; FONT-SIZE: 9pt; WIDTH: 10px; COLOR: red; FONT-FAMILY: Arial; HEIGHT: 16px"
							align="center">|</DIV>
						<asp:linkbutton id="lbtnLanguage" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt"
							Font-Names="Arial" CausesValidation="False" onclick="lbtnLanguage_Click">English</asp:linkbutton></td>
				</tr>
				<tr>
					<td>
						<IMG height="14" alt="" src="../images/home.gif" width="14" border="0">&nbsp;
						<asp:LinkButton id="lbHomePage" runat="server" Font-Size="9pt" Font-Names="Arial" ForeColor="White"></asp:LinkButton><br>
						<IMG height="13" alt="" src="../images/Favorites.gif" width="15" border="0">&nbsp;
						<asp:LinkButton id="lbFavorite" runat="server" Font-Size="9pt" Font-Names="Arial" ForeColor="White"></asp:LinkButton>
					</td>
				</tr>
			</table>
		</td>
	</TR>
</TABLE>
<TABLE id="TableHead2" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD tabIndex="-1" bgColor="#404040" style="height: 19px"><cc1:solpartmenu id="SolpartMenu" tabIndex="-1" runat="server" ForeColor="White" Font-Size="9pt"
				Font-Names="Arial" MenuEffects-MenuTransition="Fade" MenuItemHeight="21" ForceDownlevel="False" IconWidth="0" MenuEffects-MouseOutHideDelay="500"
				MouseOutHideDelay="1" MenuBarHeight="16" SelectedForeColor="White" ShadowColor="Blue" MenuEffects-MouseOverExpand="True" MenuEffects-MouseOverDisplay="Highlight"
				MenuEffects-Style="filter:progid:DXImageTransform.Microsoft.Shadow(color='DimGray', Direction=135, Strength=3) progid:DXImageTransform.Microsoft.Fade(Overlap=1.00) ;"
				SystemImagesPath="/" Moveable="True" MenuBorderWidth="0" BackColor="#66679A" HighlightColor="#FF8080" IconBackgroundColor="Blue"
				SelectedBorderColor="Blue" SelectedColor="SteelBlue" MenuAlignment="Left" Display="Horizontal"></cc1:solpartmenu></TD>
		<TD onclick="switchSysBar()" tabIndex="-1" width="12" bgColor="#404040" style="height: 19px"><SPAN class="navPoint" id="switchPoint" title="Close Header">5</SPAN></TD>
	</TR>
</TABLE>
