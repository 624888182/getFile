<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrm3DDataQuery.ascx.cs" Inherits="SFCQuery.Boundary.WFrmDataQuery" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<table style="width: 819px">
    <tr>
        
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
        </td>
		<td><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td>
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
        </td>
        <td rowSpan="4" style="width: 50px">
            <asp:Button ID="btnQuery" runat="server" Text="查詢" Height="27px" Width="50px" OnClick="btnQuery_Click" /></td>
        <td rowSpan="4" style="width: 50px">
            <asp:Button ID="btnToExcel" runat="server" OnClick="btnToExcel_Click" Text="導出Excel" />
            </td>
    </tr>
     <tr>
        <td style="width: 50px; height: 22px;">
        </td>
        <td style="width: 100px; height: 22px;">
            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="Label"></asp:Label></td>
        <td style="width: 71px; height: 22px;">
            </td>
        <td style="width: 100px; height: 22px;">
            <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="Label"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 50px">
            <asp:Label ID="Label1" runat="server" Text="Line"></asp:Label>
        </td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddlLine" runat="server" Width ="80px" DataTextField="Line" DataValueField="Line">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>S1</asp:ListItem>
                <asp:ListItem>S2</asp:ListItem>
                <asp:ListItem>S3</asp:ListItem>
                <asp:ListItem>S4</asp:ListItem>
                <asp:ListItem>S5</asp:ListItem>
                <asp:ListItem>S6</asp:ListItem>
                <asp:ListItem>S7</asp:ListItem>
                <asp:ListItem>S8</asp:ListItem>
                <asp:ListItem>S9</asp:ListItem>
                <asp:ListItem>S10</asp:ListItem>
            </asp:DropDownList></td>
        <td style="width: 71px">
            <asp:Label ID="Label2" runat="server" Text="Board"></asp:Label></td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddlBoard" runat="server" Width ="80px">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem Value="Image 1">Image1</asp:ListItem>
                <asp:ListItem Value="Image 2">Image2</asp:ListItem>
                <asp:ListItem Value="Image 3">Image3</asp:ListItem>
                <asp:ListItem Value="Image 4">Image4</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 50px; height: 22px;">
            <asp:Label ID="Label3" runat="server" Text="Feature"></asp:Label>
        </td>
        <td style="width: 100px; height: 22px;">
            <asp:TextBox ID="tbFeature" runat="server" Width="100px"></asp:TextBox></td>
        <td style="width: 71px; height: 22px;">
            <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label></td>
        <td style="width: 100px; height: 22px;">
            <asp:TextBox ID="tbLocation" runat="server" Width="100px"></asp:TextBox></td>
    </tr>
</table>
<HR>
<asp:GridView ID="GridView" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="GridView_PageIndexChanging" PageSize="20" Font-Size="Smaller" OnRowDataBound="GridView_RowDataBound"  Width = "3500px">
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <RowStyle ForeColor="#000066" HorizontalAlign =Center />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="blue" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=TYSFC;User ID=SFC;Password=SFC;Unicode=True"
    ProviderName="System.Data.OracleClient" SelectCommand="SELECT DISTINCT LINE FROM PANL_PRINT_ANALY">
</asp:SqlDataSource>
&nbsp; &nbsp;
