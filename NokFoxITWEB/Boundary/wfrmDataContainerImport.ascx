<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmDataContainerImport.ascx.cs" Inherits="Boundary_wfrmDataContainerImport" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<br />
&nbsp;<asp:FileUpload ID="tbPath" runat="server" Width="394px" />
<asp:Button ID="Button1" runat="server" Height="22px" OnClick="Button1_Click" Text="開如導入" /> 