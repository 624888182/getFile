<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipPlan_showETD_SP_Upload.aspx.cs"
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
            <asp:BoundField DataField="DocumentID" HeaderText="DocumentID" ReadOnly="True"  />
            <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" ReadOnly="True" />
            <asp:BoundField DataField="CustomerSite" HeaderText="CustomerSite" ReadOnly="True" />
            <asp:BoundField DataField="FoxconnSite" HeaderText="FoxconnSite" ReadOnly="True" />
            <asp:BoundField DataField="CustomerPN" HeaderText="CustomerPN" ReadOnly="True" />
            <asp:BoundField DataField="FoxconnPN" HeaderText="FoxconnPN" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"  />
            <asp:BoundField DataField="PNProject" HeaderText="PNProject" ReadOnly="True" />
            <asp:BoundField DataField="Dom_Exp" HeaderText="Dom_Exp" ReadOnly="True" />
            <asp:BoundField DataField="SPDate" HeaderText="SPDate" ReadOnly="True" />
            <asp:BoundField DataField="SPQty" HeaderText="SPQty" ReadOnly="True" />
            <asp:BoundField DataField="Account" HeaderText="Account" ReadOnly="True" />
            <asp:BoundField DataField="CurrentDate" HeaderText="CurrentDate" ReadOnly="True"  />
            <asp:BoundField DataField="UpdateFlag" HeaderText="UpdateFlag" ReadOnly="True" />
            <asp:BoundField DataField="Forecast_IntervalCode" HeaderText="Forecast_IntervalCode" ReadOnly="True" />            
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
