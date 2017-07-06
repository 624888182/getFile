<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyMasterDetailAdd.aspx.cs"
    Inherits="App_EnterFactApplyMasterDetailAdd" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外來申請明細管理</title>

    <script language="javascript" src="../Js/Calendar.js"></script>

</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" id="TABLE1">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID=lblTitle>外來申請明細管理</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="2">
                    <asp:Button ID="btnModify" runat="server" SkinID="MainButton" Text="修改" OnClick="btnModify_Click">
                    </asp:Button>
                    <asp:Button ID="btnCommit" runat="server" SkinID="MainButton" Text="保存" OnClick="btnCommit_Click">
                    </asp:Button>
                    <asp:Button ID="btnExit" runat="server" SkinID="MainButton" Text="退出" OnClick="btnExit_Click">
                    </asp:Button></td>
            </tr>
            <tr>
                <td colspan="5" class="TdMessage">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td class="founction" colspan="4" style="width: 1126px">
                    <fieldset style="margin-left:1px">
                        <legend>申請單基本信息</legend>
                        <table border="0" cellpadding="0" cellspacing="0" class="TextTable" style="width: 100%;">
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblApplyCode" runat="server" SkinID="lblRed">單號:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="20" SkinID="txtMain" Enabled="False"
                                        Width="225px"></asp:TextBox>
                                </td>
                                <td class="TdRight" style="width: 152px">
                                    <asp:Label ID="lblItemNo" runat="server" SkinID="lblRed" Text="項次(自動產生):"></asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtItemNo" runat="server" SkinID="txtMain" ReadOnly="True" Enabled="False"
                                        Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblStaffName" runat="server" SkinID="lblRed">人員姓名:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtStaffName" runat="server" SkinID="txtMain" Width="224px"></asp:TextBox></td>
                                <td class="TdRight" style="width: 152px">
                                    <asp:Label ID="lblIDCardNo" runat="server" SkinID="lblMain">證件號碼:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtIDCardNo" runat="server" SkinID="txtMain" Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblCompany" runat="server" SkinID="lblRed">公司/單位:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtCompany" runat="server" SkinID="txtMain" Width="224px"></asp:TextBox></td>
                                <td class="TdRight" style="width: 152px">
                                    <asp:Label ID="lblTel" runat="server" SkinID="lblMain">聯係電話:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtTel" runat="server" SkinID="txtMain" Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblEnterFactReason" runat="server" SkinID="lblRed">入厰原因:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:DropDownList ID="ddlEnterFactReason" runat="server" Width="228px">
                                    </asp:DropDownList>
                                </td>
                                <td class="TdRight" style="width: 152px">
                                    <asp:Label ID="lblGateHous" runat="server" SkinID="lblRed">門崗:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:DropDownList ID="ddlGateHouse" runat="server" Width="304px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px; height: 22px">
                                    <asp:Label ID="lblReceptionDept" runat="server" SkinID="lblRed">接待部門:</asp:Label></td>
                                <td class="Tdleft" style="height: 22px">
                                    <asp:TextBox ID="txtReceptionDept" runat="server" SkinID="txtMain" Width="224px"></asp:TextBox>
                                </td>
                                <td class="TdRight" style="width: 152px; height: 22px">
                                    <asp:Label ID="lblReceptionStaff" runat="server" SkinID="lblRed">接待人員:</asp:Label></td>
                                <td class="Tdleft" style="height: 22px">
                                    <asp:TextBox ID="txtReceptionStaff" runat="server" SkinID="txtMain" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblReceptionTel" runat="server" SkinID="lblRed">接待人員電話:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtReceptionTel" runat="server" SkinID="txtMain" Width="223px"></asp:TextBox>
                                </td>
                                <td class="Tdleft" style="width: 152px">
                                    <asp:Label ID="lblCardStatusName" runat="server" SkinID="lblMain">狀態:</asp:Label>
                                </td>
                                <td class="TdRight">
                                    <asp:TextBox ID="txtCardStatusName" runat="server" SkinID="txtMain" Width="300px" BorderStyle="None" Enabled="False" ReadOnly="True"></asp:TextBox>
                                </td>                                                           
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblExpectedEnterDate" runat="server" SkinID="lblRed">預計入厰時間:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtExpectedEnterDate" runat="server" SkinID="txtMain"></asp:TextBox>
                                    <asp:DropDownList ID="ddlExpectedEnterTimeHour" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>時~
                                    <asp:DropDownList ID="ddlExpectedEnterTimeMinute" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList>分
                                </td>
                                <td class="TdRight" style="width: 152px">
                                    <asp:Label ID="lblExpectedLeaveDate" runat="server" SkinID="lblRed">預計出廠時間:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtExpectedLeaveDate" runat="server" SkinID="txtMain"></asp:TextBox>
                                    <asp:DropDownList ID="ddlExpectedLeaveTimeHour" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>~
                                    <asp:DropDownList ID="ddlExpectedLeaveTimeMinute" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TdRight" style="width: 180px">
                                    <asp:Label ID="lblTakeItems" runat="server" SkinID="lblMain">攜帶物品:</asp:Label></td>
                                <td class="Tdleft" colspan="3">
                                    <asp:TextBox ID="txtTakeItems" runat="server" SkinID="txtMain" Width="663px"></asp:TextBox>
                                </td>                              
                            </tr>
                            <tr>
                                <td class="Tdleft" style="width: 180px">
                                    <asp:Label ID="lblMemo" runat="server" SkinID="lblMain">備註:</asp:Label>
                                </td>                                    
                                <td class="TdRight" colspan="3">
                                    <asp:TextBox ID="txtMemo" runat="server" SkinID="txtMain" Width="665px" TextMode="MultiLine"></asp:TextBox>
                                </td>                  
                            </tr>                            
                            
                            <tr id="Tr1" visible="false" runat="server">
                                <td colspan="4" style="height: 22px">
                                
                                    <asp:TextBox ID="txtStatus" runat="server" SkinID="txtMain" Width="14px"></asp:TextBox>
                                    <asp:TextBox ID="txtCardStatus" runat="server" SkinID="txtMain" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtActualEnterDate" runat="server" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtActualEnterTime" runat="server" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtActualLeaveDate" runat="server" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtActualLeaveTime" runat="server" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtInitiateID" runat="server" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtInitiateDate" runat="server" Width="10px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-left:1px">
                        <legend>外來人員發卡信息</legend>
                        <table border="0" cellpadding="0" cellspacing="0" class="TextTable" style="width: 100%">
                            <tr>
                                <td class="TdRight" style="width: 181px; height: 22px">
                                    <asp:Label ID="lblCardNo" runat="server" SkinID=lblMain>卡號:</asp:Label></td>   
                                <td class="Tdleft" style="width: 382px; height: 22px">
                                    <asp:TextBox ID="txtCardNo" runat="server" SkinID=txtMain Width="223px"></asp:TextBox>
                                </td>
                                <td class="TdRight" width="100px" style="height: 22px">
                                    <asp:Label ID="lblRightDescription" runat="server" SkinID="lblMain">權限描述:</asp:Label></td>
                                <td class="Tdleft" style="height: 22px">
                                    <asp:TextBox ID="txtRightDescription" runat="server" SkinID="txtMain" Width="300px"></asp:TextBox>
                                </td>                          
                            </tr> 
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
