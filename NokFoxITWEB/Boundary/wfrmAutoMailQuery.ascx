<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmAutoMailQuery.ascx.cs" Inherits="Boundary_wfrmAutoMailQuery" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
<asp:Button ID="Button1" runat="server" Height="23px" OnClick="Button1_Click" Text="顯示數據"
    Width="83px" />
<asp:Button ID="Button2" runat="server" Height="22px" OnClick="Button2_Click" Text="獲取DN"
    Width="74px" />
<asp:Label ID="Label1" runat="server" Text="總計：0條" Width="94px"></asp:Label>
<asp:GridView ID="GridView1" runat="server" Height="43px" Width="699px">
</asp:GridView> 
