<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactConfirmDetail.aspx.cs"
    Inherits="App_EnterFactConfirmDetail" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主管審核明細</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" Text="主管審核" SkinID="lblTitle"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea" style="height: 25px">
                    <asp:Button ID="btnConfirmY" runat="server" SkinID="MainButton" Text="通過" OnClick="btnConfirmY_Click" />
                    <asp:Button ID="btnConfirmN" runat="server" SkinID="MainButton" Text="拒絕" OnClick="btnConfirmN_Click" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" Text="返回" OnClick="btnOut_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="TdMessage">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="function" style="width: 100%;">
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblRejectReason" runat="server" SkinID="lblMain" Text="決絕原因(拒絕請填寫):"></asp:Label>
                            </td>
                            <td class="TdRight" colspan="3">
                                <asp:TextBox ID="txtRejectReason" runat="server" Width="302px" SkinID="txtMain" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain" Text="案號:"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtApplyCode" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="TdLeft">
                                <asp:Label ID="lblApplyDate" runat="server" SkinID="lblMain" Text="申請日期:"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtApplyDate" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblApplyName" runat="server" SkinID="lblMain" Text="申請人"></asp:Label></td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtApplyName" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox></td>
                            <td class="TdLeft">
                                <asp:Label ID="lblStatus" runat="server" SkinID="lblMain" Text="狀態"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtStatus" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblTel" runat="server" SkinID="lblMain" Text="電話"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtTel" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="TdLeft">
                                <asp:Label ID="lblIsBUMgrConfirm" runat="server" SkinID="lblMain" Text="需要處級部門審核"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtIsBUMgrConfirm" runat="server" Width="300px" SkinID="txtMain"
                                    Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblDeptName" runat="server" SkinID="lblMain" Text="部門"></asp:Label></td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtDeptName" runat="server" Width="302px" Enabled="false" SkinID="txtMain"
                                    TextMode="MultiLine"></asp:TextBox></td>
                            <td class="TdLeft">
                                <asp:Label ID="lblMemo" runat="server" SkinID="lblMain" Text="備註"></asp:Label>
                            </td>
                            <td class="TdRight" colspan="3">
                                <asp:TextBox ID="txtMemo" runat="server" Width="302px" Enabled="false" SkinID="txtMain"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>                                    
                        </tr>
                        <tr>
                            <td class="TdLeft">
                                <asp:Label ID="lblDivisionEmployee" runat="server" SkinID="lblMain" Text="廠(部)主管"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtDivisionEmployee" runat="server" Width="300px" SkinID="txtMain"
                                    Enabled="false"></asp:TextBox>
                            </td>
                            <td class="TdLeft">
                                <asp:Label ID="lblBUEmployee" runat="server" SkinID="lblMain" Text="處級主管"></asp:Label>
                            </td>
                            <td class="TdRight">
                                <asp:TextBox ID="txtBUEmployee" runat="server" Width="300px" SkinID="txtMain" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="function" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:FormView ID="FormView1" runat="server" AllowPaging="True" Caption="外來申請人員明細"
                                    Width="100%" DataSourceID="SqlDataSource1" AccessKey="8" CellPadding="4" ForeColor="#333333"
                                    CaptionAlign="Top" EnableTheming="True">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    次項:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ItemNoLabel" runat="server" Text='<%# Bind("ItemNo") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    姓名:</td>
                                                <td>
                                                    <asp:Label ID="StaffNameLabel" runat="server" Text='<%# Bind("StaffName") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    證件號碼:
                                                </td>
                                                <td>
                                                    <asp:Label ID="IDCardNoLabel" runat="server" Text='<%# Bind("IDCardNo") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    公司:
                                                </td>
                                                <td>
                                                    <asp:Label ID="CompanyLabel" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    電話:
                                                </td>
                                                <td>
                                                    <asp:Label ID="TelLabel" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    入廠原因:
                                                </td>
                                                <td>
                                                    <%# PubFunction.GetEnterReasonNameByID(DataBinder.Eval(Container, "DataItem.EnterFactReason").ToString())%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    門崗:
                                                </td>
                                                <td>
                                                    <%# PubFunction.GetGateHouseNameByID(DataBinder.Eval(Container, "DataItem.GateHouse").ToString())%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    接受部門:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ReceptionDeptLabel" runat="server" Text='<%# Bind("ReceptionDept") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    接受人員:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ReceptionStaffLabel" runat="server" Text='<%# Bind("ReceptionStaff") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    接受人員電話:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ReceptionTelLabel" runat="server" Text='<%# Bind("ReceptionTel") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    預計入廠日期:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ExpectedEnterDateLabel" runat="server" Text='<%# Bind("ExpectedEnterDate") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    預計入廠時間:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ExpectedEnterTimeLabel" runat="server" Text='<%# Bind("ExpectedEnterTime") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    預計離開日期:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ExpectedLeaveDateLabel" runat="server" Text='<%# Bind("ExpectedLeaveDate") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    預計離開時間:
                                                </td>
                                                <td>
                                                    <asp:Label ID="ExpectedLeaveTimeLabel" runat="server" Text='<%# Bind("ExpectedLeaveTime") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    權限描述:
                                                </td>
                                                <td>
                                                    <asp:Label ID="RightDescriptionLabel" runat="server" Text='<%# Bind("RightDescription") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    攜帶物品:
                                                </td>
                                                <td>
                                                    <asp:Label ID="TakeItemsLabel" runat="server" Text='<%# Bind("TakeItems") %>'></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    備註:
                                                </td>
                                                <td>
                                                    <asp:Label ID="MemoLabel" runat="server" Text='<%# Bind("Memo") %>'></asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    創建人:
                                                </td>
                                                <td>
                                                    <asp:Label ID="InitiateIdLabel" runat="server" Text='<%# Bind("InitiateId") %>'>
                                                    </asp:Label><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    創建日期:
                                                </td>
                                                <td>
                                                    <asp:Label ID="InitiateDateLabel" runat="server" Text='<%# Bind("InitiateDate") %>'>
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                </asp:FormView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ForeignStaffSqlServer %>"
            SelectCommand="SELECT [ItemNo], [StaffName], [IDCardNo], [Company], [Tel], [EnterFactReason], [GateHouse], [ReceptionDept], [ReceptionStaff], [ReceptionTel], [ExpectedEnterDate], [ExpectedEnterTime], [ExpectedLeaveDate], [ExpectedLeaveTime], [RightDescription], [CardNo], [TakeItems], [Memo], [InitiateId], [InitiateDate] FROM [appEnterFactApplyDetail] WHERE ([ApplyCode] = @ApplyCode)">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtApplyCode" Name="ApplyCode" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
