<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipPlan_showETA.aspx.cs" Inherits="UploadData_ShipPlan_showETA" StyleSheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
            width: 651px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Button ID="Button3" runat="server" oncommand="Button3_Command" 
            Text="Test Excel" Width="154px" onclick="Button3_Click" Visible="False" />
        &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="下載 EXCEL" Visible="False" />
        
       
        <br />
                <table style="width: 78%; height: 31px;">
                    <tr>
                        <td class="style3">
                            DocumentID</td>
                        <td class="style1">
                            <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" 
            Width="171px" AutoPostBack="True" 
            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="CustomerSite"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:DropDownList 
            ID="DropDownList3" runat="server" Height="16px" Width="161px" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList3_SelectedIndexChanged">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style2">
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"> </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label1" runat="server" Text="FOXCONN SITE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="170px" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem>LH</asp:ListItem>
                                <asp:ListItem>BJ</asp:ListItem>
                                <asp:ListItem>LF</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            CustomerPN</td>
                        <td class="style2">
                            <asp:TextBox ID="TextBox1" runat="server" Width="163px"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:Button ID="Button5" runat="server" 
                        onclick="Button5_Click" Text="..." Width="32px" />
                        </td>
                        <td class="style2">
                            <asp:Button ID="Button4" runat="server" ForeColor="#003300" Height="22px" 
            oncommand="Button4_Command" SkinID="mainButton" Text="查 詢" Width="80px" onclick="Button4_Click" 
                                 />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            &nbsp;</td>
                        <td class="style1">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="style2">
                            <asp:Panel ID="Panel1" runat="server" Height="263px">
                                <asp:ListBox ID="ListBox1" runat="server" 
                             AutoPostBack="True" Height="254px" 
                               Width="170px" onselectedindexchanged="ListBox1_SelectedIndexChanged1"></asp:ListBox>
                            </asp:Panel>
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style2">
                            &nbsp;</td>
                    </tr>
                </table>
    
    </div>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
                    Font-Bold="False" Height="44px" onrowdatabound="GridView1_RowDataBound" 
                    Width="536px">
                    <RowStyle BackColor="#EFF3FC" />
                    <Columns>
                        <asp:BoundField DataField="AccountNum" HeaderText="Num" ReadOnly="True" />
                        <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="DocumentID" HeaderText="DocumentID" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="CustomerSite" HeaderText="CustomerSite" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="FoxconnSite" HeaderText="FoxconnSite" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="CustomerPN" HeaderText="CustomerPN" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="LeadTime" HeaderText="LeadTime" ReadOnly="True" />
                        <asp:BoundField DataField="SPDate_8Bytes" HeaderText="SPDate_8Bytes" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="ETD_SPQty" HeaderText="ETD_SPQty" ReadOnly="True" />
                        <asp:BoundField DataField="Org_Spilt_SPQty" HeaderText="Org_Spilt_SPQty" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="Leadtime_SPQty" HeaderText="Leadtime_SPQty" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="Leadtime_GIT_SPQty" HeaderText="Leadtime_GIT_SPQty" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="TodayGIT_SPQty" HeaderText="TodayGIT_SPQty" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="SumSPQty" HeaderText="SumSPQty" ReadOnly="True" />
                        <asp:BoundField DataField="SumC3" HeaderText="SumC3" ReadOnly="True" />
                        <asp:BoundField DataField="EVWeekOfDay" HeaderText="EVWeekOfDay" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="SWWeekDay" HeaderText="SWWeekDay" ReadOnly="True" />
                        <asp:BoundField ControlStyle-Width="88" DataField="SPweek" 
                            HeaderText="SPweek        " ReadOnly="True">
                            <ControlStyle Width="88px" />
                            <HeaderStyle Width="88px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Bold="False" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
    </form>
</body>
</html>
