<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trace_Support.aspx.cs" Inherits="Temp_Trace_BigSN" Theme="" 
StyleSheetTheme="SkinFile" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
 <link rel="stylesheet" type="text/css" media="all" href="../WebControler/themes/wood.css" title="wood" />



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style17
        {
        }
        
td { font-size:12px;
            }
	
        .style19
        {
            font-size: medium;
        }
        .style22
        {
            font-size: medium;
        }
        .style23
        {
            width: 317px;
        }
        .style24
        {
            font-size: medium;
            width: 317px;
        }
        .style25
        {
            height: 15px;
        }
        </style>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="具體來料信息查詢 "></asp:Label>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;
         <table  border="1" bordercolor="#CCCCFF" cellspacing="0" cellpadding="0"  
                      
            
        style="border-collapse: collapse;  text-align: left; width: 1131px; height: 63px;">
            <tr>
                <td class="style23">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table style="width:150%;">
                        <tr>
                            <td>
                                &nbsp;DISKNO</td>
                            <td>
                    <asp:TextBox ID="TextBox1" runat="server" Height="23px"></asp:TextBox>
                            </td>
                            <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="22px" 
                        ImageUrl="~/App_Themes/SkinFile/images/search.gif" onclick="ImageButton1_Click" 
                        Width="69px" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style25">
                                &nbsp;</td>
                            <td class="style25">
                                &nbsp;</td>
                            <td class="style25">
                                <asp:LinkButton ID="LinkButton13" runat="server" 
                                    PostBackUrl="~/Traceability/TJTraceMenu.aspx">BACK</asp:LinkButton>
                            </td>
                            <td class="style25">
                                </td>
                        </tr>
                        </table>
                </td>
                <td class="style17">
                    &nbsp;</td>
                <td class="style17">
                    &nbsp;</td>
                <td class="style17">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style23">
                    &nbsp;</td>
                <td class="style17">
                    &nbsp;</td>
                <td class="style17">
                    &nbsp;</td>
                <td class="style17">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style19" colspan="4">
                    
                    &nbsp;&nbsp;&nbsp;<asp:GridView ID="GridView1" runat="server" 
                        Width="1097px" SkinID="gvMain" AllowPaging="True" ShowFooter="True">
<Columns>
 
 <asp:BoundField DataField="rownum" HeaderText="rownum" />
 <asp:BoundField DataField="iqcpo_NO" HeaderText="LINE" />
 <asp:BoundField DataField="key_part_no" HeaderText="MACHINE" />   
 <asp:BoundField DataField="qty" HeaderText="STATION" />  
 <asp:BoundField DataField="creator" HeaderText="TRACKNO" />   


  <asp:BoundField DataField="create_time" HeaderText="MO_NUMBER" />
 <asp:BoundField DataField="Lot_no" HeaderText="MODEL" />
 <asp:BoundField DataField="d_c" HeaderText="BOMNO" />   
 <asp:BoundField DataField="supply" HeaderText="PARTNO" />  
 <asp:BoundField DataField="LOT_NO" HeaderText="LOT_NO" />   

  <asp:BoundField DataField="disk_sn" HeaderText="CREATETIME" />
  
 <asp:BoundField DataField="checkin_type" HeaderText="QTY" />   
 <asp:BoundField DataField="supply_pn" HeaderText="EMP_NO" />  
 <asp:BoundField DataField="update_time" HeaderText="IQC_SN" />   
  <asp:BoundField DataField="wh_emp" HeaderText="EMP_NO" />  
 <asp:BoundField DataField="wh_time" HeaderText="IQC_SN" />  


  


 
 </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style24">
                    <strong>&nbsp;</strong></td>
                <td class="style22">
                    &nbsp;</td>
                <td class="style22">
                    &nbsp;</td>
                <td class="style22">
                    &nbsp;</td>
            </tr>
            
            </table>
    
    </form>
</body>
</html>
