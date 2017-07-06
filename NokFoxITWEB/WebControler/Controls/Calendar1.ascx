<%@ Control Language="c#" Inherits="Calendar1" CodeFile="Calendar1.ascx.cs" %>
<table>
    <tr>
        <td>
            <asp:TextBox ID="DateText" runat="server" ></asp:TextBox>
        </td>
        <td>
            <asp:PlaceHolder runat="server" ID="CommandPlace" Visible="False">
                <div id="DIV1" runat="server">
                </div>
            </asp:PlaceHolder>
        </td>
        <td>
            <asp:PlaceHolder runat="server" ID="FlatPlace" Visible="false">
                <div id="calendar_container" runat="server">
                </div>
            </asp:PlaceHolder>
        </td>
        <td>
            <input type="hidden" id="HiddenText" runat="server" /></td>
    </tr>
</table>
