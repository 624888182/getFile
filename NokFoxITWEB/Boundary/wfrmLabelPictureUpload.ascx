<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmLabelPictureUpload.ascx.cs" Inherits="Boundary_wfrmLabelPictureUpload" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
	    <td>圖片類型：</td>
		<td width="50" rowSpan="1" style="height: 23px">
            <span style="font-size: 9pt"></span>
            <asp:DropDownList ID="DropDownList2" runat="server" Width="168px">
            </asp:DropDownList></td>
		<td style="height: 23px"></td>
		<td>選擇圖片：</td>
		<td style="WIDTH: 220px; height: 23px;">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="247px" /></td>
		<td style="width: 32px; height: 23px;">
        </td>
		<td rowSpan="1" style="width: 174px; height: 23px;"><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server"
                Text="上偉" Width="76px" OnClick="Button1_Click" />&nbsp;
			</FONT>
		</td>
		
	</tr>
	<tr>
	
	</tr>
</table>
<asp:Label ID="Label1" runat="server" Width="934px"></asp:Label>
<hr>
<br />
<br />
<TABLE class="DataGridFont" id="tblEffiency" cellSpacing="1" cellPadding="1" align="center"
	border="1" runat="server">
	<tr>
	    <TD style="width: 762px; text-align: center;">
            瀏覽圖片：&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="168px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
</asp:DropDownList></TD>
	</tr>
	<TR>
		<TD style="width: 762px">
            <asp:Image ID="Image1" runat="server" Height="410px" Width="774px" ImageUrl="~/Images/bg12.jpg" /></TD>
	</TR>
</TABLE>