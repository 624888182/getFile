<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmDownLoadShippingData.ascx.cs" Inherits="SFCQuery.Boundary.wfrmDownLoadShippingData" %>
<%@ Register Src="../WebControler/modeltitle.ascx" TagName="modeltitle" TagPrefix="uc1" %>
<uc1:modeltitle ID="Modeltitle1" runat="server" />
<table class="DataGridFont">
    <tr> 
		<td width="50" ><FONT face="新細明體"></FONT></td>
        <td><asp:Label ID="lblCartonNo" runat=server Text="CartonNo" Width="80px" CssClass="Font"></asp:Label>
        </td> 
        <td>
            <asp:TextBox ID="tbCartonNo" runat="server" TextMode="MultiLine" Width="300px" Height="100px" ToolTip="以逗號區分,例如:P8863003_11-1,P8863003_11-2"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Button ID="btnDownLoad" runat="server" Text="DownLoad" OnClick="btnDownLoad_Click" CssClass="DataGridFont" Width="90px" /></td>
    </tr>
</table>
<hr />