<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipPlan_showPreETA.aspx.cs"
    Inherits="UploadData_ShipPlan_showETA" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipPlan_showPreETA</title>
    <style type="text/css">
        .style1
        {
            width: 273px;
        }
        .style2
        {
            width: 329px;
        }
        .style3
        {
            width: 202px;
        }
        .style4
        {
            width: 202px;
            height: 23px;
        }
        .style5
        {
            width: 273px;
            height: 23px;
        }
        .style6
        {
            height: 23px;
            width: 210px;
        }
        .style7
        {
            width: 329px;
            height: 23px;
        }
        .style8
        {
            width: 210px;
             height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Button ID="btnTestExcel" runat="server" OnCommand="btnTestExcel_Command" Text="Test Excel"
            Width="154px" OnClick="btnTestExcel_Click" Visible="False" />
        &nbsp;<asp:Button ID="btnDownloadExcel" runat="server" OnClick="btnDownloadExcel_Click"
            Text="下載 EXCEL" Visible="False" />
        <br />
        <table style="width: 78%; height: 31px;">
            <tr>
                <td class="style4">
                    <asp:Label ID="lblDocumentID" runat="server" Text="DocumentID"></asp:Label>
                </td>
                <td class="style5">
                    <asp:DropDownList ID="ddlDocumentID" runat="server" Height="16px" Width="171px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDocumentID_SelectedIndexChanged">                     
                    </asp:DropDownList>
                </td>
                <td class="style6">
                    <asp:Label ID="lblCustomerSite" runat="server" Text="CustomerSite"></asp:Label>
                </td>
                <td class="style7">
                    <asp:DropDownList ID="ddlCustomerSite" runat="server" Height="16px" Width="161px"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerSite_SelectedIndexChanged">                    
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td class="style7">
                </td>
                <td class="style7">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"> </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblFoxconnSite" runat="server" Text="FOXCONN SITE"></asp:Label>
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlFoxconnSite" runat="server" Height="16px" Width="170px">         
                        
                    </asp:DropDownList>
                </td>
                <td class="style8">
                    <asp:Label ID="lblCustomerPN" runat="server" Text="CustomerPN"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="txtCustomerPN" runat="server" Width="163px"></asp:TextBox>
                </td>
                <td class="style2">
                    <asp:Button ID="btnCustomerPN" runat="server" OnClick="btnCustomerPN_Click" Text="..."
                        Width="32px" />
                </td>
                <td class="style2">
                    <asp:Button ID="btnSearch" runat="server" ForeColor="#003300" Height="22px" OnCommand="btnSearch_Command"
                        SkinID="mainButton" Text="查 詢" Width="80px" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style1">
                    &nbsp;
                </td>
                <td class="style8">
                    &nbsp;
                </td>
                <td class="style2">
                    <asp:Panel ID="Panel1" runat="server" Height="263px">
                        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" Height="254px" Width="170px"
                            OnSelectedIndexChanged="ListBox1_SelectedIndexChanged1"></asp:ListBox>
                    </asp:Panel>
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Font-Bold="False" AutoGenerateColumns="false"
        Height="44px" OnRowDataBound="GridView1_RowDataBound" Width="536px">
        <RowStyle BackColor="#EFF3FC" />
        <Columns>
            <asp:BoundField DataField="AccountNum" HeaderText="Num" ReadOnly="True" />
            <asp:BoundField DataField="SPDate" HeaderText="SPDate" ReadOnly="True" />
            <asp:BoundField DataField="SPDate_8Bytes" HeaderText="SPDate_8Bytes" ReadOnly="True" />
            <asp:BoundField DataField="Org_Spilt_SPQty" HeaderText="Org_Spilt_SPQty" ReadOnly="True" />
            <asp:BoundField DataField="Leadtime_SPQty" HeaderText="Leadtime_SPQty" ReadOnly="True" />
            <asp:BoundField DataField="Leadtime_GIT_SPQty" HeaderText="Leadtime_GIT_SPQty" ReadOnly="True" />
            <asp:BoundField DataField="TodayGIT_SPQty" HeaderText="TodayGIT_SPQty" ReadOnly="True" />
            <asp:BoundField DataField="SumSPQty" HeaderText="SumSPQty" ReadOnly="True" />
            <asp:BoundField DataField="SumC3" HeaderText="SumC3" ReadOnly="True" />
            <asp:BoundField DataField="EVWeekOfDay" HeaderText="EVWeekOfDay" ReadOnly="True" />
            <asp:BoundField DataField="SWWeekDay" HeaderText="SWWeekDay" ReadOnly="True" />
            <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" ReadOnly="True" />
            <asp:BoundField DataField="SPweek" HeaderText="SPweek" ReadOnly="True" />
            <asp:BoundField DataField="DocumentID" HeaderText="DocumentID" ReadOnly="True" />
            <asp:BoundField DataField="ETD_SPQty" HeaderText="ETD_SPQty" ReadOnly="True" />
            <asp:BoundField DataField="CustomerSite" HeaderText="CustomerSite" ReadOnly="True" />
            <asp:BoundField DataField="FoxconnSite" HeaderText="FoxconnSite" ReadOnly="True" />
            <asp:BoundField DataField="CustomerPN" HeaderText="CustomerPN" ReadOnly="True" />
            <asp:BoundField DataField="DVCode" HeaderText="DVCode" ReadOnly="True" />
            <asp:BoundField DataField="FirstFlag" HeaderText="FirstFlag" ReadOnly="True" />
            <asp:BoundField DataField="Current_Dos" HeaderText="Current_Dos" ReadOnly="True" />
            <asp:BoundField DataField="Next_Dos" HeaderText="Next_Dos" ReadOnly="True" />
            <asp:BoundField DataField="GIT_Dos" HeaderText="GIT_Dos" ReadOnly="True" />
            <asp:BoundField DataField="MPQ" HeaderText="MPQ" ReadOnly="True" />
            <asp:BoundField DataField="LeadTime" HeaderText="LeadTime" ReadOnly="True" />
            <asp:BoundField DataField="CurrNextDosQty" HeaderText="CurrNextDosQty" ReadOnly="True" />
            <asp:BoundField DataField="MPQQty" HeaderText="MPQQty" ReadOnly="True" />
            <asp:BoundField DataField="DownToTQty" HeaderText="DownToTQty" ReadOnly="True" />
            <asp:BoundField DataField="DVType" HeaderText="DVType" ReadOnly="True" />
            <asp:BoundField DataField="Nokia1Document_ID" HeaderText="Nokia1Document_ID" ReadOnly="True" />
            <asp:BoundField DataField="Nokia2Document_ID" HeaderText="Nokia2Document_ID" ReadOnly="True" />
            <asp:BoundField DataField="CustomerDum" HeaderText="CustomerDum" ReadOnly="True" />
            <asp:BoundField DataField="OrgGITQty" HeaderText="OrgGITQty" ReadOnly="True" />
            <asp:BoundField DataField="GITDocument_ID" HeaderText="GITDocument_ID" ReadOnly="True" />
      <%--      <asp:BoundField ControlStyle-Width="88" DataField="SPweek" HeaderText="SPweek        "
                ReadOnly="True">
                <ControlStyle Width="88px" />
                <HeaderStyle Width="88px" />
            </asp:BoundField>--%>
        </Columns>
        <HeaderStyle Font-Bold="False" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
