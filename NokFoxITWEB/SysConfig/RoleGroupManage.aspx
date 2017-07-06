<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleGroupManage.aspx.cs" Inherits="SysConfig_RoleGroupManage2" StylesheetTheme="SkinFile"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="formOperate" runat="server" method="post">    
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="PageHeaderLeft">
                    &nbsp;操作功能管理</td>
                <td class="PageHeaderRight" ></td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0"  style="width: 624px;height:50px">
                        <tr>
                            <td class="TdRight" style="width: 150px; height: 50px">
                                <asp:Label  ID="lbOperName"  runat="server"  SkinID="TextLabel">操作功能代碼 : </asp:Label></td>
                            <td style="height: 50px">
                                <asp:TextBox ID="tbRoleCode" runat="server" CssClass="inputcss" MaxLength="100" Width="200px"></asp:TextBox></td>
                            <td class="TdRight" style="width: 150px; height: 50px" align="right">
                                <asp:Label ID="lbOperName1" runat="server" SkinID="TextLabel">操作功能名稱 : </asp:Label></td>
                            <td style="height: 50px; width: 156px;">
                                <asp:TextBox ID="tbRoleName" runat="server" CssClass="inputcss" MaxLength="100" Width="200px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="TdPadding"> 
                                 <input id="btnReset" runat="server" class="MainButton" onserverclick="btnReset_ServerClick"
                                    type="button" value="重設" accesskey="T" />
                                <asp:Button ID="btnFind" runat="server" CssClass="MainButton" OnClick="btnFind_Click"
                                    Text="查詢" />
                                <asp:Button ID="btnAdd" runat="server" CssClass="MainButton" OnClick="btnAdd_Click" Text="新增" AccessKey="N" ToolTip="ALT+N" />
                                <input id="btnToExcel" runat="server" class="MainButton" type="button" value="導出Excel" />
                                <input id="btnOut" runat="server" class="MainButton" onserverclick="btnOut_ServerClick"
                                    type="button" value="退出" accesskey="E" />                           
                 </td>
            </tr>            
            <tr>
                <td class="TdGrid" colspan="2" >
                        <asp:GridView ID="GridViewList" runat="server" Width="100%" AutoGenerateColumns="False"   AllowPaging="True" PageSize="15" OnRowCreated="GridViewList_RowCreated" OnRowDataBound="GridViewList_RowDataBound" OnPageIndexChanging="GridViewList_PageIndexChanging" BorderStyle="Double" BorderWidth="0px" CssClass="GridView"   >

                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbItemHead" runat="server" Height="100%" Text="NO." Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
			                              <asp:Label ID="lbItem" runat="server" Text="項次" Width="100%"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbOperationCodeHead" runat="server" Height="100%" Text="操作功能代碼" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>										
                                        <asp:Label ID="lbRoleCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RoleCode")%>' Width="100%"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbOperationNameHead" runat="server"  Height="100%" Text="操作功能名稱" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
										<%# DataBinder.Eval(Container, "DataItem.RoleName")%>
									</ItemTemplate>
                                </asp:TemplateField>                                
                                                                                         
                            </Columns>
                            <HeaderStyle  CssClass="HeaderStyle"/>
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <EditRowStyle BorderStyle="None" />
                            
                                                
                        </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
