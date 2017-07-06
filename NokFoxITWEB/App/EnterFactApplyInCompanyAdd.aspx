<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyInCompanyAdd.aspx.cs" Inherits="App_EnterFactApplyInCompanyAdd" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>外來人員入廠</title>
    <script language=javascript>
    //window.opener.submit();
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" id="TABLE1">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd">外來人員入廠</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="2">
                    <asp:Button ID="btnConfirm" runat="server" SkinID="MainButton" Text="確定" OnClick="btnConfirm_Click">
                    </asp:Button>&nbsp;<asp:Button ID="btnExit" runat="server" SkinID="MainButton" Text="退出" OnClick="btnExit_Click">
                    </asp:Button></td>
            </tr>
            <tr>
                <td colspan="5" class="TdMessage">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td class="founction" colspan="4" style="width: 100%">
                    <fieldset style="margin-left:1px">
                        <legend>外來人員基本信息</legend>
                        <table border="0" cellpadding="0" cellspacing="0" class="TextTable" style="width: 100%;">
                            <tr>
                                <td class="TdRight" width="100px">
                                    <asp:Label ID="lblApplyCode" runat="server" SkinID="lblMain">單號:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtApplyCode" runat="server" MaxLength="20" SkinID="txtMain" Enabled="False"
                                        Width="200px" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="TdRight" width="100px">
                                    <asp:Label ID="lblItemNo" runat="server" SkinID="lblMain" Text="項次:"></asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtItemNo" runat="server" SkinID="txtMain" ReadOnly="True" Enabled="False"
                                        Width="300px" BorderStyle="Groove"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="TdRight">
                                    <asp:Label ID="lblStaffName" runat="server" SkinID="lblMain">姓名:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtStaffName" runat="server" SkinID="txtMain" Width="200px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                                <td class="TdRight">
                                    <asp:Label ID="lblCompany" runat="server" SkinID="lblMain">公司/單位:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtCompany" runat="server" SkinID="txtMain" Width="300px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>

                                <td class="TdRight">
                                    <asp:Label ID="lblTel" runat="server" SkinID="lblMain">聯係電話:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtTel" runat="server" SkinID="txtMain" Width="200px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                                <td class="TdRight">
                                    <asp:Label ID="lblEnterFactReason" runat="server" SkinID="lblMain">入厰原因:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtEnterFactReason" runat="server" Enabled="False" Width="300px" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>                                                                        
                            </tr>
                            <tr>

                                <td class="TdRight">
                                    <asp:Label ID="lblGateHouse" runat="server" SkinID="lblMain">門崗:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtGateHouse" runat="server" Enabled="False" Width="200px" BorderStyle="Groove" ReadOnly="True"></asp:TextBox></td>
                                 <td class="TdRight">
                                    <asp:Label ID="lblReceptionDept" runat="server" SkinID="lblMain">接待部門:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtReceptionDept" runat="server" SkinID="txtMain" Width="300px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>                                   
                            </tr>
                            <tr>

                                <td class="TdRight">
                                    <asp:Label ID="lblReceptionStaff" runat="server" SkinID="lblMain">接待人員:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtReceptionStaff" runat="server" SkinID="txtMain" Width="200px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="TdRight">
                                    <asp:Label ID="lblReceptionTel" runat="server" SkinID="lblMain">接待人員電話:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtReceptionTel" runat="server" SkinID="txtMain" Width="300px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td class="TdRight">
                                    <asp:Label ID="lblExpectedEnterDate" runat="server" SkinID="lblMain">預計入厰時間:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtExpectedEnterDate" runat="server" SkinID="txtMain" Enabled="False" Width="200px" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="TdRight">
                                    <asp:Label ID="lblExpectedLeaveDate" runat="server" SkinID="lblMain">預計出廠時間:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtExpectedLeaveDate" runat="server" SkinID="txtMain" Enabled="False" Width="300px" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Tdleft">
                                    <asp:Label ID="lblCardNo" runat="server" SkinID="lblMain">卡號:</asp:Label>
                                </td>                                    
                                <td class="TdRight" colspan="3">
                                    <asp:TextBox ID="txtCardNo" runat="server" SkinID="txtMain" Width="640px" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td>                                     
                            </tr>
                            <tr>
                                <td class="Tdleft">
                                    <asp:Label ID="lblMemo" runat="server" SkinID="lblMain">備註:</asp:Label>
                                </td>                                    
                                <td class="TdRight" colspan="3">
                                    <asp:TextBox ID="txtMemo" runat="server" SkinID="txtMain" Width="640px" TextMode="MultiLine" Enabled="False" BorderStyle="Groove" ReadOnly="True"></asp:TextBox>
                                </td> 
                            </tr>                          
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>外來人員資料更新</legend>
                            <table border="0" cellpadding="0" cellspacing="0" class="TextTable" style="width: 100%;">
                            <tr>
                                <td class="TdRight">
                                    <asp:Label ID="lblIDCardNo" runat="server" SkinID="lblMain">證件號碼:</asp:Label></td>
                                <td class="Tdleft">
                                    <asp:TextBox ID="txtIDCardNo" runat="server" SkinID="txtMain" Width="200px"></asp:TextBox></td>                            
                                <td class="TdRight">
                                    <asp:Label ID="lblTakeItems" runat="server" SkinID="lblMain">攜帶物品:</asp:Label></td>
                                <td class="Tdleft" colspan="3">
                                    <asp:TextBox ID="txtTakeItems" runat="server" SkinID="txtMain" Width="300px" TextMode="MultiLine"></asp:TextBox>
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
