<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trace_DiskNo.aspx.cs" Inherits="Temp_Trace_BigSN" Theme="" 
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
        .O
	{color:black;
	font-size:149%;}
        .style26
        {
            width: 140px;
        }
        .style27
        {
            height: 15px;
            width: 140px;
        }
        .style28
        {
            width: 146px;
        }
        .style29
        {
            height: 15px;
            width: 146px;
        }
        .style30
        {
            color: black;
            font-size: 149%;
            text-align: center;
        }
        </style>
</head>
<body background="../index_bg.gif">
    <form id="form1" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="上料表信息查詢 "></asp:Label>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;
         <table  border="1" bordercolor="#CCCCFF" cellspacing="0" cellpadding="0"  
                      
            
        style="border-collapse: collaps&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table 
                        style="width:575px;">
                        <tr>
                            <td>
                                &nbsp;開始時間</td>
                            <td class="style26">
                                <uc2:Calendar1 ID="Startdate" 
                        runat="server" />
                            </td>
                            <td>
                                <div class="O" style="text-align:center;mso-line-spacing:
&quot;100 50 0&quot;;mso-margin-left-alt:216;mso-char-wrap:1;mso-kinsoku-overflow:1" v:shape="_x0000_s1026">
                                    <span>物 料</span></div>
                            </td>
                            <td class="style28">
                                <asp:TextBox ID="Txtpart" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style25">
                                &nbsp;
                                開始時間</td>
                            <td class="style27">
                                <uc2:Calendar1 ID="Enddate" 
                        runat="server" />
                            </td>
                            <td class="style25">
                                <p:colorscheme
 colors="#ffffff,#000000,#808080,#330066,#cccc00,#669999,#7e9ce8,#d8d8ec" 
                                    xmlns:p="urn:schemas-microsoft-com:office:powerpoint"/>

                                <div class="O" v:shape="_x0000_s1026">
                                    <span>線 別</span></div>
                            </td>
                            <td class="style29">
                                <asp:TextBox ID="TxtLine" runat="server"></asp:TextBox>
                                </td>
                            <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="22px" 
                        ImageUrl="~/App_Themes/SkinFile/images/search.gif" onclick="ImageButton1_Click" 
                        Width="74px" />
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton13" runat="server" 
                                    PostBackUrl="~/Traceability/TJTraceMenu.aspx">BACK</asp:LinkButton>
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
 <asp:BoundField DataField="LINE_NAME" HeaderText="LINE" />
 <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE" />   
 <asp:BoundField DataField="STATION_NUMBER" HeaderText="STATION" />  
 <asp:BoundField DataField="TRACK_NO" HeaderText="TRACKNO" />   


  <asp:BoundField DataField="MO_NUMBER" HeaderText="MO_NUMBER" />
 <asp:BoundField DataField="MODEL_NAME" HeaderText="MODEL" />
 <asp:BoundField DataField="BOM_NO" HeaderText="BOMNO" />   
 <asp:BoundField DataField="KEY_PART_NO" HeaderText="PARTNO" />  
 <asp:BoundField DataField="LOT_NO" HeaderText="LOT_NO" />   

  <asp:BoundField DataField="CREATE_TIME" HeaderText="CREATETIME" />
  
 <asp:BoundField DataField="QTY" HeaderText="QTY" />   
 <asp:BoundField DataField="EMP_NO" HeaderText="EMP_NO" />  
 <asp:BoundField DataField="IQC_SN" HeaderText="IQC_SN" />   








 
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
