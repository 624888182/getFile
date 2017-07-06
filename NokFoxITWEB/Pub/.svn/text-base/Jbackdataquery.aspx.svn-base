<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Jbackdataquery.aspx.cs" Inherits="Pub_Jbackdataquery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body bgcolor="#ff9900">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" 
                        ForeColor="Red" Text="備份檢查"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <asp:Label ID="year" runat="server" Text="年份："></asp:Label>
                    <asp:DropDownList ID="yearchiose" runat="server">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="motch" runat="server" Text="月份："></asp:Label>
                    <asp:DropDownList ID="motchchiose" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="40%">
                    <asp:Label ID="DB" runat="server" Text="備份數據庫："></asp:Label>
                    <asp:DropDownList ID="dbchiose" runat="server">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    StartRec:
                    <asp:TextBox ID="TextBox4" runat="server" Width="54px"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:Button ID="querybutton" runat="server" BorderStyle="Inset" 
                        onclick="querybutton_Click" Text="記錄備份" />
                </td>
                <td>
                    <asp:Button ID="qurey_button" runat="server" BorderStyle="Inset" 
                        onclick="qurey_button_Click" Text="查詢數據" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; StartDate
                    <asp:TextBox ID="TextBox1" runat="server" Width="105px"></asp:TextBox>
&nbsp; EndDate&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server" Width="105px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;DataCnt
                    <asp:TextBox ID="TextBox3" runat="server" Width="64px"></asp:TextBox>
                    &nbsp;Confirm :
                    <asp:DropDownList ID="dbchiose0" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                &nbsp;StopRec:
                    <asp:TextBox ID="TextBox5" runat="server" Width="54px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:Button ID="Button1" runat="server" BorderStyle="Inset" 
                        onclick="Button1_Click" Text="說明" Width="169px" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="LabelMessage" runat="server" Font-Bold="True" 
                        Font-Size="X-Large" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    
                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" 
                            Font-Bold="True" Font-Size="Large" GroupName="Group1" 
                            oncheckedchanged="RadioButton1_CheckedChanged" Text="已備份" Checked="True" />
                        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" 
                            Font-Bold="True" Font-Size="Large" GroupName="Group1" 
                            oncheckedchanged="RadioButton2_CheckedChanged" Text="未備份" />
                    
                </td>
            </tr>
        </table>
    
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
            BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" 
            Width="100%" onpageindexchanging="GridView1_PageIndexChanging">
            <PagerSettings Visible="False" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                <asp:BoundField DataField="PID" HeaderText="PID" />
                <asp:BoundField DataField="Cartion" HeaderText="Cartion" />
                <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery_Date" />
                <asp:BoundField DataField="Delete_Date" HeaderText="Delete_Date" />
                <asp:BoundField DataField="Flag1" HeaderText="Flag1" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div>
    <asp:Panel ID="Panel2" runat="server">
    <asp:ImageButton ID="btnFirst" runat="server" AlternateText="First" CausesValidation="false"
                            CommandArgument="First" CommandName="Page" ImageUrl="..\Images\arrow-first.gif"
                            OnClick="PagebtnClick" />
                        <asp:ImageButton ID="btnPrevious" runat="server" AlternateText="Previous" CausesValidation="false"
                            CommandArgument="Previous" CommandName="Page" ImageUrl="..\Images\arrow-previous.gif"
                            OnClick="PagebtnClick" />
                        <asp:ImageButton ID="btnNext" runat="server" AlternateText="Next" CausesValidation="false"
                            CommandArgument="Next" CommandName="Page" ImageUrl="..\Images\arrow-next.gif"
                            Width="17px" OnClick="PagebtnClick" />
                        <asp:ImageButton ID="btnLast" runat="server" AlternateText="Last" CausesValidation="false"
                            CommandArgument="Last" CommandName="Page" ImageUrl="..\Images\arrow-last.gif"
                            OnClick="PagebtnClick" />
                        <asp:Label ID="lblNow" runat="server"></asp:Label>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        <asp:TextBox ID="txtNo" runat="server" Width="80px"></asp:TextBox>
                        頁<asp:ImageButton ID="btnGo" runat="server" AlternateText="Go" CausesValidation="false"
                            CommandArgument="-1" CommandName="Page" ImageUrl="..\Images\go.gif" Style="height: 15px"
                            OnClick="PagebtnClick" />
                            </asp:Panel>
    </div>
    <asp:Panel ID="Panel1" runat="server">
    
    <div>
        <asp:Label ID="Label6" runat="server" Text="FLAG第一欄位表示此數據進行過更新（因時間變更）"></asp:Label>
        
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="FLAG第二欄位為表：SHP.CMCS_SFC_IMEINUM"></asp:Label>
    
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="FLAG第三欄位為表：SFC.MES_ASSY_PID_JOIN"></asp:Label>
    
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="FLAG第四欄位為表：SFC.MES_ASSY_HISTORY"></asp:Label>
    
    </div>
    <div>
        <asp:Label ID="Label5" runat="server" 
            Text="FLAG第五欄位為表：SFC.R_WIP_TRACKING_T_PID"></asp:Label>
    
    </div>
    <div>
    <asp:Label ID="Label7" runat="server" 
            Text="IMEI和PID的抓取：日期->DN(sap.cmcs_sfc_packing_lines_all)->CARTON( SHP.CMCS_SFC_SHIPPING_DATA)->IMEI和PID"></asp:Label>
    </div>
    <div>
        <table style="width: 100%;" align = "center">
            <tr>
                <td align="center" width="30%">
                    211是否存在數據</td>
                <td align="center" width="30%">
                    &nbsp; 108或者215是否存在數據</td>
                <td align="center">
                    &nbsp; 標示位</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp; Y</td>
                <td align="center">
                    &nbsp; N</td>
                <td align="center">
                    &nbsp; E</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp; N</td>
                <td align="center">
                    &nbsp; Y</td>
                <td align="center">
                    &nbsp; 1</td>
            </tr>
             <tr>
                <td align="center">
                    &nbsp; Y</td>
                <td align="center">
                    &nbsp; Y</td>
                <td align="center">
                    &nbsp; 1</td>
            </tr>
             <tr>
                <td align="center">
                    &nbsp; N</td>
                <td align="center">
                    &nbsp; N</td>
                <td align="center">
                    &nbsp; 0</td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    </form>
</body>
</html>
