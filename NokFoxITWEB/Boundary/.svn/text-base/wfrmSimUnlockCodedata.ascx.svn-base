<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmShippingInfo" CodeFile="wfrmSimUnlockCodedata.ascx.cs" %>
<uc1:ModelTitle ID="ModelTitle1" runat="Server"></uc1:ModelTitle>
<table class="DataGridFont" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td width="50" rowspan="4">
        </td>
        <td>
            <asp:Label ID="lblStartDate" runat="server" Width="100px" Visible="False">Ship Date From</asp:Label></td>
        <td style="width: 183px">
            <asp:TextBox ID="tbStartDate" runat="server" Visible="False"></asp:TextBox><asp:Button
                ID="btnDateFrom" runat="server" Width="25px" Text="..." Visible="False"></asp:Button></td>
        <td style="width: 106px">
            <asp:Label ID="lblEndDate" runat="server" Width="100px" Visible="False">Ship Date To</asp:Label></td>
        <td style="width: 248px">
            <asp:TextBox ID="tbEndDate" runat="server" Visible="False"></asp:TextBox><asp:Button
                ID="btnDateTo" runat="server" Width="25px" Text="..." Visible="False"></asp:Button></td>
        <td rowspan="4">
            <font face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </font>
            <asp:Button ID="btnQuery" runat="server" Width="100px" Text="Query" OnClick="btnQuery_Click">
            </asp:Button></td>
        <td rowspan="4">
            <font face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </font>
            <asp:Button ID="btnExportExcel" runat="server" Width="100px" Text="Export To Excel"
                OnClick="btnExportExcel_Click" Visible="False"></asp:Button>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 183px">
            <asp:Label ID="Label28" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
        <td style="width: 106px">
        </td>
        <td style="width: 248px">
            <asp:Label ID="Label29" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblInvoiceNO" runat="server" Width="100px">MODEL</asp:Label></td>
        <td style="width: 183px">
            <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="true" 
                Width="155px">
            </asp:DropDownList></td>
        <td style="width: 106px">
            <asp:Label ID="lblCartonID" runat="server" Width="100px">PID</asp:Label></td>
        <td style="width: 248px">
            <asp:TextBox ID="tbPID" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 24px">
            <font face="新細明體">
                <asp:Label ID="lblIMEI" runat="server" Width="100px">IMEI&Picasso</asp:Label></font></td>
        <td style="width: 183px; height: 24px;">
            <asp:TextBox ID="tbIMEI" runat="server"></asp:TextBox></td>
        <td colspan="2" style="height: 24px">
            <asp:CheckBox ID="FA6FA1" runat="server" Text="FA6FA1-RDL和SIMLOCK沒有合併數據查詢" />&nbsp;</td>
    </tr>
</table>
<hr>
<asp:Label ID="Label1" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<div class="DIVScrolling" id="divsize" style="width: 100%; height: 500px">
    <asp:GridView ID="dgProduct" runat="server" Font-Size="10px" AutoGenerateColumns="false"
        AllowPaging="false" BackColor="White" UserID="Any" BorderWidth="1px" Font-Names="Verdana"
        BorderStyle="None">
        <Columns>
            <asp:BoundField DataField="IMEI" HeaderText="IMEI" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="PID" HeaderText="PID" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="UNLOCKCODE" HeaderText="UNLOCKCODE" ReadOnly="True"></asp:BoundField>
            <asp:BoundField DataField="SIMLOCKCODE" HeaderText="SIMLOCKCODE" ReadOnly="True"></asp:BoundField> 
            <asp:BoundField DataField="TESTDATE" HeaderText="TESTDATE" ReadOnly="True"></asp:BoundField>               
        </Columns>
        <RowStyle BackColor="#F1F8F1" />
        <HeaderStyle HorizontalAlign="Center" Font-Size="15px" Font-Bold="true" />
        <AlternatingRowStyle BackColor="White" />
        <PagerSettings Visible="true" />
    </asp:GridView>
</div>
