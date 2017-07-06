<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyMasterAdd.aspx.cs"
    Inherits="App_EnterFactApplyMasterAdd" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>外來申請主儅</title>
    <script language=javascript>
        function ShowBU()
        {
            var chkIsBuMgrConrim = document.getElementById('chkIsBuMgrConrim');
            var trBU = document.getElementById('trBU');
            if(chkIsBuMgrConrim.checked)
            {
                trBU.style.display = 'block';
            }else
            {
                trBU.style.display = 'none';
            }
        }
    
    </script>
</head> 
<body>
    <form id="formDetail" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" Text="外來申請主儅" SkinID="lblTitle"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    <asp:Button ID="btnModify" runat="server" SkinID="MainButton" OnClick="btnModify_Click"
                        Text="修改" Visible="False" AccessKey="U" ToolTip="ALT+U" />
                    <asp:Button ID="btnCommit" runat="server" SkinID="MainButton" OnClick="btnCommit_Click"
                        Text="保存" AccessKey="S" ToolTip="ALT+S" />
                    <asp:Button ID="btnDelete" runat="server" SkinID="MainButton" OnClick="btnDelete_Click"
                        Text="刪除" AccessKey="D" ToolTip="ALT+D" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="TdMessage" style="width: 853px">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="width:100%">
                    <table border="0" cellpadding="0" cellspacing="0" class="function" style="width: 100%;">
                        <tr>
                            <td class="TdRight" style="width: 115px">
                                <asp:Label ID="lblApplyCode" runat="server" Text="單號(自動產生):" SkinID="lblRed"></asp:Label></td>
                            <td class="TdLeft">
                                <asp:TextBox ID="txtApplyCode" SkinID="txtMain" runat="server" ReadOnly="True" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="width: 115px">
                                <asp:Label ID="lblTel" runat="server" Text="電話:" SkinID="lblMain"></asp:Label></td>
                            <td class="TdLeft">
                                <asp:TextBox ID="txtTel" SkinID="txtMain" runat="server" Width="500px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="width: 115px">
                                <asp:Label ID="lblIsBuMgrConrim" runat="server" Text="需處級核准:" SkinID="lblMain"></asp:Label></td>
                            <td class="TdLeft">
                                <asp:CheckBox ID="chkIsBuMgrConrim" runat="server" onClick="ShowBU()" /></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="width: 115px">
                                <asp:Label ID="lblDivision" runat="server" Text="廠部簽核主管:" SkinID="lblMain"></asp:Label></td>
                            <td class="TdLeft">
                                <asp:DropDownList ID="ddlDivision" runat="server">
                                </asp:DropDownList></td>                        
                        </tr>
                        <tr id="trBU">
                            <td class="TdRight" style="width: 115px">
                                <asp:Label ID="lblBU" runat="server" Text="處級簽核主管:" SkinID="lblMain"></asp:Label></td>
                            <td class="TdLeft">
                                <asp:DropDownList ID="ddlBU" runat="server">
                                </asp:DropDownList></td>                        
                        </tr>                        
                        <tr>
                            <td class="TdRight" style="height: 40px; width: 115px;">
                                <asp:Label ID="lblMemo" runat="server" Text="備註:" SkinID="lblMain"></asp:Label></td>
                            <td class="TdLeft" colspan="5" style="height: 40px">
                                <asp:TextBox ID="txtMemo" SkinID="txtMain" runat="server" TextMode="MultiLine" Height="74px"
                                    Width="502px"></asp:TextBox></td>
                        </tr>
                        <tr visible="false" runat="server">
                            <td colspan="2">
                                <asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtRejectReason" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtApplyDate" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtApplyID" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtApplyDepartment" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtDivisionMgrId" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtDivisionConfirmDate" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtBUMgrId" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtBUConfirmDate" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtInitiateId" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtInitiateDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea" style="width: 853px">
                    <asp:Button ID="btnRefresh" runat="server" SkinID="mainButton" Text="刷新" OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnAddDetail" runat="server" SkinID="mainButton" Text="新增明細" OnClick="btnAddDetail_Click" />&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="gvListDetail" runat="server" AllowPaging="True" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvListDetail_RowDataBound"  DataKeyNames="ApplyCode,ItemNo"
            AutoGenerateColumns="False" OnRowCreated="gvListDetail_RowCreated" OnRowCommand="gvListDetail_RowCommand" OnRowDeleting="gvListDetail_RowDeleting" OnRowEditing="gvListDetail_RowEditing">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/Delete.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ApplyCode" HeaderText="案號" ReadOnly="True" Visible="False" />
                <asp:TemplateField HeaderText="項次">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnItemNo" CommandName="Detail" runat="server" Text='<%# Bind("ItemNo") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle ForeColor="Blue" />
                </asp:TemplateField>
                <asp:BoundField DataField="StaffName" HeaderText="人員姓名" />
                <asp:BoundField DataField="IDCardNo" HeaderText="證件號碼" />
                <asp:BoundField DataField="ReasonName" HeaderText="入廠原因" />
                <asp:BoundField DataField="GateHouseName" HeaderText="門崗" />
                <asp:BoundField DataField="Company" HeaderText="公司/單位" />
                <asp:BoundField DataField="Tel" HeaderText="電話" />
                <asp:BoundField DataField="Memo" HeaderText="備註" />
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="LeftPagerStyle" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        </asp:GridView>
        <script language=javascript>
            ShowBU();
        </script>
    </form>
</body>
</html>
