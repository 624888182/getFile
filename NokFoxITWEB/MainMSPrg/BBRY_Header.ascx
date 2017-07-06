<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BBRY_Header.ascx.cs" Inherits="MainNokPrg_IHub_Header" %>

<table width="100%" cellspacing="0">
    <tr bgcolor="#FF9933"           
            style="padding: 0px; margin: 0px; border-color: #FF9933; border-width: 0px;">
            <td style="border-color: #FF9933; border-width: 0px; font-size: xx-large; font-family: 'Times New Roman', Times, serif;" 
                align="center">BBRY B2B Web
            </td>
        </tr>
    <tr align="right" valign="middle" bgcolor="#FF9933"                        
            style="padding: 0px; margin: 0px; border-color: #FF9933; border-width: 0px;">
            <td valign="middle" style="border-color: #FF9933; border-width: 0px;">
                <asp:Button ID="btnLogout" runat="server" Text="Logout:" 
                onclick="btnLogout_Click" BorderColor="Black" BorderWidth="2px" 
                    Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" 
                    ForeColor="#333399" />
                &nbsp;
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
                    Font-Names="Times New Roman" ForeColor="#000099"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
            </td>
            
        </tr>
    <tr>
        <td>
            <asp:Menu ID="Menu1" runat="server" 
                 DynamicHorizontalOffset="2" Font-Names="Arial monospaced for SAP" 
                Font-Size="Medium" ForeColor="Black" Orientation="Horizontal" 
                StaticEnableDefaultPopOutImage="False" StaticSubMenuIndent="10px" 
                BackColor="#3399FF" Width="100%">
                <StaticSelectedStyle BackColor="Gray" BorderStyle="Solid" BorderColor="Gray" Font-Bold="True" />
                <DynamicHoverStyle BackColor="Blue" ForeColor="White" />
                <StaticHoverStyle BackColor="#666666" ForeColor="White" />
                <StaticMenuItemStyle HorizontalPadding="15px" VerticalPadding="4px" Font-Bold="True" />               
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" BackColor="#3399ff" />          
                <items>
                    <asp:menuitem Text ="PO Manage" Value ="PO Manage" NavigateUrl="BBSCMPOHEADER.aspx" Selected="True">
                    </asp:menuitem>
                    <asp:menuitem Text ="PO Confirm" Value ="PO Confirm" NavigateUrl="Nokiaihub4B2.aspx">
                    </asp:menuitem>
                    <asp:menuitem Text ="PO Notes" Value ="PO Notes" NavigateUrl="">
                    </asp:menuitem>
                    <asp:menuitem Text ="Pending Confirm" Value ="Pending Confirm" NavigateUrl="">
                    </asp:menuitem>
                    <asp:menuitem Text ="3C7" Value ="3C7" NavigateUrl="Nokiaihub3c7.aspx">
                    </asp:menuitem>
                    <asp:menuitem Text ="Item" Value ="Item" NavigateUrl="">
                    </asp:menuitem>
                    <asp:menuitem Text ="ForecastRef" Value ="ForecastRef" NavigateUrl="">
                    </asp:menuitem>
                    <asp:menuitem Text ="SAP3B2" Value ="ForecastRef" NavigateUrl="">
                    </asp:menuitem>
                    <asp:menuitem Text ="3B2FromExcel_null" Value ="ForecastRef" NavigateUrl="">
                    </asp:menuitem>
                </items>                
            </asp:Menu>            
        </td>
    </tr>
</table>