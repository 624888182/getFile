<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepRoleDistribute.aspx.cs" Inherits="DepRoleDistribute" StylesheetTheme="SkinFile"  EnableEventValidation="false"  meta:resourcekey="PageResource1"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>部門權限表</title>
    
    
</head>
<body>
    <form id="formBu" runat="server" method="post">    
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="PageHeaderLeft">
                    &nbsp;部門權限表</td>
                <td class="PageHeaderRight" ></td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0"  class="FindTable">
                        <tr>
                            <td  class="TdFindRight" >
                                <asp:Label ID="lbDeptRoleGpCode" runat="server" SkinID="TextLabel" Width="87px" meta:resourcekey="lbBuIDResource1">權限代碼 : </asp:Label></td>
                            <td class="TdFindLeft" >
                                <asp:TextBox ID="tbDeptRoleGpCode" runat="server" CssClass="inputcss" MaxLength="100" SkinID="NormalTextBox" meta:resourcekey="tbDeptRoleGpCodeResource1"></asp:TextBox></td>
                            <td  class="TdFindRight" >
                                <asp:Label ID="lbDeptRoleGpName" runat="server" SkinID="TextLabel" Width="84px" meta:resourcekey="lbBuNameResource1">權限名稱: </asp:Label></td>
                            <td class="TdFindLeft" >
                                <asp:TextBox ID="tbDeptRoleGpName" runat="server" CssClass="inputcss" MaxLength="100" SkinID="NormalTextBox" meta:resourcekey="tbDeptRoleGpNameResource1"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TdGrid" colspan="2"   >
                    </td>
            </tr>
            <tr>
                <td colspan="2" class="TdPadding" > 
                                 <asp:Button id="btnReset" Visible="false" runat="server" CssClass="MainButton" OnClick="btnReset_ServerClick"
                                     Text="重置"  meta:resourcekey="btnResetResource1" />
                                <asp:Button ID="btnSearch" runat="server" CssClass="MainButton" OnClick="btnFind_Click"
                                    Text="查詢" meta:resourcekey="btnSearchResource1" />
                    <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="MainButton" meta:resourcekey="btnAddResource1" OnClick="btnAdd_Click" />
                                <asp:Button id="btnOut" runat="server" CssClass="MainButton" OnClick="btnOut_ServerClick"
                                     Text="返回" meta:resourcekey="btnOutResource1" />                           
                 </td>
            </tr>            
            <tr>
                <td class="TdGrid" colspan="2"  >
                    </td>
            </tr>
        </table>
                        <asp:GridView ID="GridViewList" runat="server" Width="100%" AutoGenerateColumns="False"   
                        AllowPaging="True" PageSize="15" OnRowCreated="GridViewList_RowCreated" OnRowDataBound="GridViewList_RowDataBound" 
                        BorderStyle="Double" BorderWidth="0px" CssClass="GridView" 
                        meta:resourcekey="GridViewListResource1" OnRowDeleting="GridViewList_RowDeleting" DataSourceID="ODSourceOper" >

                            <Columns>
                                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbItemHead" runat="server" Height="100%" Text="項次" Width="100%" meta:resourcekey="lbItemHeadResource1"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
			                              <asp:Label ID="lbItem" runat="server" Text="項次" Width="100%" meta:resourcekey="lbItemResource1"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbDeptRoleGpCode" runat="server" Height="100%" Text="權限代碼" Width="100%" meta:resourcekey="lbDeptRoleGpCodeResource2"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>		
                                    <a href="#" onmouseover="this.style.cursor='hand';" > 								
                                        <asp:Label ID="lbDeptRoleGpCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeptRoleGpCode") %>' Width="100%" meta:resourcekey="lbDeptRoleGpCodeResource3"></asp:Label>
								</a>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbDeptRoleGpName" runat="server" Height="100%" Text="權限名稱" Width="100%" meta:resourcekey="lbDeptRoleGpNameResource2"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    
                                        <asp:Label ID="lbDeptRoleGpName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeptRoleGpName") %>' Width="100%" meta:resourcekey="lbDeptRoleGpNameResource3"></asp:Label>
									
									</ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbDivNo" runat="server" Height="100%" Text="權限列表" Width="100%" meta:resourcekey="lbDivNoResource2"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    
                                        <asp:Label ID="lbDivNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DivNo") %>' Width="100%" meta:resourcekey="lbDivNoResource3"></asp:Label>
									
									</ItemTemplate>
                                </asp:TemplateField>                                                                     
                                <asp:TemplateField HeaderText="修改" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" meta:resourcekey="imgbtnEditResource1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="刪除" ShowHeader="False" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"  Text="
                                        &lt;img  src=&quot;../App_Themes/SkinFile/images/delete.gif&quot; /&gt;" EnableTheming="True" BorderWidth="0px" BorderStyle="None" meta:resourcekey="lbtnDeleteResource1"></asp:LinkButton>
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
        &nbsp;<asp:ObjectDataSource ID="ODSourceOper" runat="server" ConvertNullToDBNull="True"
            DataObjectTypeName="FIH.Security.db.DeptRoleGpInfo" DeleteMethod="Delete" EnablePaging="True"
            OnSelecting="ODSourceOper_Selecting" SelectCountMethod="findCount" SelectMethod="find"
            TypeName="FIH.Security.db.DeptRoleGp">
            <SelectParameters>
                <asp:Parameter Name="deptRoleGp" Type="Object" />
                <asp:ControlParameter ControlID="GridViewList" Name="startRowIndex" PropertyName="PageIndex"
                    Type="Int32" />
                <asp:ControlParameter ControlID="GridViewList" Name="maximumRows" PropertyName="PageSize"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>

