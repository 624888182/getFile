<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmSetupSorder.ascx.cs" Inherits="Boundary_wfrmSetupSorder" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 

<table  cellSpacing="0" cellPadding="0" border="0" >
    <tr>
        <td width="50" style="height: 120px"></td>
        <td style="width: 180px; height:120px;" > 
            <asp:Panel ID="Panel1" runat="server" BorderColor="Gray" BorderStyle="Double" > 
            <asp:Label ID="lb1" Text="PCB板順序" runat="server" ForeColor="Black"></asp:Label>
            <asp:RadioButtonList ID="rbl1" runat="server" TextAlign="Right" Height="120px" Width="150px" CellPadding="0" CellSpacing="0">
                <asp:ListItem Text="主板" Selected="True"></asp:ListItem>
                <asp:ListItem Text="第一塊副板"></asp:ListItem>
                <asp:ListItem Text="第二塊副板"></asp:ListItem>
            </asp:RadioButtonList>           
            </asp:Panel>            
             <table>
                <tr>
                    <td>
                    <asp:CheckBox ID="ckbDUO" runat="server" Text="DUO,DUA,SLI試產" Checked="false"  />  
                    </td>
                </tr>
            </table>             
        </td>
        <td style="height: 120px; width: 350px;" valign="bottom" >
            <table  cellSpacing="0" cellPadding="0" border="0" height="80%" width="100%" >
                <tr height="15px"><td height="25px"></td></tr>
                <tr>
                    <td><asp:Label ID="lbWO" runat="server" Text="工單"></asp:Label></td>
                    <td style="width: 167px"><asp:TextBox ID ="txtWO" runat="server"></asp:TextBox></td>
                     <td align="center"><asp:Button ID="btnWOInfo" Text="帶入工單資料" runat="server" OnClick="btnWOInfo_Click" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lbPart" runat="server" Text="料號"></asp:Label></td>
                    <td><asp:TextBox ID ="txtPart" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lbVer" runat="server" Text="版本"></asp:Label></td>
                    <td style="width: 167px"><asp:TextBox ID ="txtVer" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lbQty" runat="server" Text="數量"></asp:Label></td>
                    <td style="width: 167px"><asp:TextBox ID ="txtQty" runat="server">
                    </asp:TextBox>                    
                    </td>
                </tr>
                <tr>
                 <td></td>
                    <td style="width: 167px">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ErrorMessage="BOM版本僅能輸入三位數字"
                        ValidationExpression="[0-9]" ControlToValidate="txtQty"></asp:RegularExpressionValidator> 
                    </td>
               </tr>
            </table>
        </td>       
    </tr> 
    <tr height="5">
       <td height="5px"> </td>
    </tr>
    <tr>
        <td width="50" style="height: 150px"></td>
        <td style="height: 150px; width: 180px;">
            <asp:Panel ID="Panel2" runat="server" Width="100%" Height="80%" BorderColor="Gray" BorderStyle="Double" >
            <table  cellSpacing="0" cellPadding="0" border="0" >
                <tr>
                    <td style="width: 62px; height: 50px;" valign="top">
                        <asp:CheckBox ID="ckb1" Text="試產" runat="server" TextAlign="Right" Checked="false" Width="56px"/>                        
                    </td>
                    <td style="width: 150px; height: 50px;">
                        <table>
                            <tr>
                                <td width="5"> </td>
                                <td>
                                <asp:Panel ID="Panel3" runat="server" BorderColor="Gray" BorderStyle="Double">
                                    <asp:Label ID="lbchoose" runat="server" Text="請選擇"></asp:Label>
                                    <asp:RadioButtonList ID="rbl2" runat="server"  >
                                        <asp:ListItem Text="1.LPR" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="2.FPR" ></asp:ListItem>
                                        <asp:ListItem Text="3.EPR" ></asp:ListItem>
                                        <asp:ListItem Text="4.EXR"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 62px" >
                        <asp:CheckBox ID="ckb2" runat="server" Text="次數" Checked="false" TextAlign="Right"/>             
                    </td>
                    <td style="width: 150px">
                        <asp:TextBox ID="txtTime" runat="server" Width="100"></asp:TextBox>
                    </td>
                </tr>
            </table>  
            </asp:Panel>
            <table>
                <tr>
                    <td>
                    <asp:CheckBox ID="ckbElse" runat="server" Text="其他幾種試產" Checked="false" />  
                    </td>
                </tr>
            </table>   
            </td>
        <td width="50%" style="height: 150px">
            <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" width="100%" height="95%" >                
                <tr valign="top" height="100%" >
                    <td width="80" align="center" valign="top"><asp:Button ID="btnCreateWO" Text="創建工單" runat="server" Width="80%" OnClick="btnCreateWO_Click" /></td>
                    <td width="80" align="center" valign="top"><asp:Button ID="btnFind" Text="查找" runat="server" Width="80%" OnClick="btnFind_Click" /></td>
                    <td width="80" align="center" valign="top"></td>
                </tr>
            </table>
        </td>              
    </tr>   
</table>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px" align="center" >
    輸入Sorder:
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="ButtonFind" runat="server" Text="查找" Width="69px" OnClick="ButtonFind_Click" />
    <asp:Button ID="ButtonDelete" runat="server"  Text="刪除" Width="65px" OnClick="ButtonDelete_Click" /><br />
    <asp:DataGrid ID="dgSorder" runat="server" BackColor="White" BorderColor="#3366CC"
        BorderWidth="1px" CellPadding="4" Width="925px" BorderStyle="None" Height="1px" OnItemDataBound="dgSorder_ItemDataBound">
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <SelectedItemStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
        <Columns>
            <asp:TemplateColumn>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <ItemStyle BackColor="White" ForeColor="#003399" />
    </asp:DataGrid>
    &nbsp;</div>