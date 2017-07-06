<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmLHSNImport.ascx.cs" Inherits="Boundary_wfrmLHSNImport" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" >
    <tr width="100%">
        <td width="50px"></td>
        <td style="width: 200px"> <asp:Label ID="lbPorder" runat="server"  ></asp:Label>
        </td>
        <td><asp:textbox id="tbPorder" runat="server"></asp:textbox></td>        
        <td style="width: 100px">
        </td>
    </tr> 
    <tr width="100%">
        <td width="50px"></td>
        <td style="width: 200px"> <asp:Label ID="lbSN" runat="server"  ></asp:Label>
        </td>
        <td style="width: 100px">
            <asp:FileUpload ID="FileUpload" runat="server" Width="250px" /></td>
        <td style="width: 100px">
        </td>
    </tr>
    
    <tr width="100%">
        <td width="50px"></td>
        <td style="width: 200px"></td>
        <td style="width: 100px">
            <asp:Button ID="Button1" runat="server" width="86px"  Text="提交" OnClick="btnSubmit_Click" /></td>
        <td style="width: 100px">
        </td>
    </tr>
    
</table>
<hr />
<table align="center" width="90%">
    <tr width="100%" align="left">
        <td > <asp:Label ID="lbalert" runat="server"  Visible="false" Font-Size="15px"></asp:Label>
        </td>
    </tr> 
    <tr width="100%" align="left">
        <td width="200px">
        <asp:ListBox ID="lbalter1" runat="server"  Width="100%" ></asp:ListBox>
        </td>
    </tr>
    
</table>

