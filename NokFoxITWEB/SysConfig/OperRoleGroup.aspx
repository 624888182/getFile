<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperRoleGroup.aspx.cs" Inherits="SysConfig_OperateOper" StylesheetTheme="SkinFile" EnableEventValidation="false"  meta:resourcekey="PageResource1"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    
</head>
<body>
    <form id="formOperate" runat="server" method="post">    
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID=lblTitle meta:resourcekey="lbHeadResource1" Text="操作權限組管理"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0"  style="width: 624px;height:50px">
                        <tr>
                            <td class="TdRight" style="width: 150px; height: 50px">
                                <asp:Label  ID="lblOperCode"  runat="server"  SkinID=lblMain meta:resourcekey="lbOperNameResource1" Text="操作權限組代碼 : "></asp:Label></td>
                            <td style="height: 50px">
                                <asp:TextBox ID="txtOperRoleGpCode" runat="server" CssClass="inputcss" MaxLength="100" SkinID=txtMain meta:resourcekey="tbOperCodeResource1"></asp:TextBox></td>
                            <td class="TdRight" style="width: 150px; height: 50px" align="right">
                                <asp:Label ID="lblOperName" runat="server" SkinID=lblMain meta:resourcekey="lbOperName1Resource1" Text="操作權限組名稱 : "></asp:Label></td>
                            <td style="height: 50px; width: 156px;">
                                <asp:TextBox ID="txtOperRoleGpName" runat="server" CssClass="inputcss" MaxLength="100" SkinID=txtMain meta:resourcekey="tbOperNameResource1"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea"> 
                    <asp:Button ID="btnReset" Visible="false" runat="server" SkinID=mainButton meta:resourcekey="btnResetResource1"
                        OnClick="btnReset_Click" Text="重設" AccessKey="T" ToolTip="ALT+T" />
                                <asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                                    Text="查詢" meta:resourcekey="btnFindResource1" />
                                <asp:Button ID="btnAdd" runat="server" SkinID=mainButton  Text="新增" AccessKey="A" ToolTip="ALT+A"  />
                    <asp:Button ID="btnOut" runat="server" SkinID=mainButton meta:resourcekey="btnOutResource1"
                        OnClick="btnOut_Click" Text="退出" AccessKey="E" ToolTip="ALT+E" /></td>
            </tr>            
            <tr>
                <td class="TdGrid" colspan="2" >
                        <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False"   AllowPaging="True" PageSize="5"   BorderStyle="Double" BorderWidth="0px" DataSourceID="dsOper" OnRowDeleting="gvList_RowDeleting" meta:resourcekey="gvListResource1" OnRowCreated="gvList_RowCreated" OnRowDataBound="gvList_RowDataBound" SkinID="gvMain"   >

                            <Columns>
                              <asp:TemplateField HeaderText="刪除" ShowHeader="False"  >
                                    <ItemTemplate>
                               <asp:LinkButton ID="lbtnDelete" runat="server"  CommandName="Delete"  Text="&lt;img   src=&quot;../App_Themes/SkinFile/images/delete.gif&quot;     border=&quot;0&quot;&gt;"  ToolTip="删除?" ></asp:LinkButton>
                                 </ItemTemplate>
                                  <HeaderStyle Width="30px" />
                                </asp:TemplateField>      
                                   <asp:TemplateField HeaderText="修改" >
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif"   />
                                    </ItemTemplate>
                                       <HeaderStyle Width="30px" />
                                </asp:TemplateField>

                                <asp:TemplateField Visible="False"  >
                                    <HeaderTemplate >
                                        <asp:Label ID="lblItemHead" runat="server" Height="100%" Text="序號" Width="100%"  Visible="false" ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
			                              <asp:Label ID="lblItem" runat="server" Text="item" Width="100%"   Visible="false"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOpeRoleGroupCodeHead" runat="server" Height="100%" Text="操作權限組代碼" Width="100%" meta:resourcekey="lblOperationCodeHeadResource1"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>				
                                     <a href="#" onmouseover="this.style.cursor='hand';" > 						
                                        <asp:Label ID="lblOpeRoleGroupCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OperRoleGpCode") %>' Width="100%" meta:resourcekey="lblOperationCodeIDResource1"></asp:Label>
								  </a> 
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOperRoleGpNameHead" runat="server"  Height="100%" Text="操作權限組名稱" Width="100%" meta:resourcekey="lblOperationNameHeadResource1"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        
                                          <asp:Label ID="lblOperRoleGpName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OperRoleGpName") %>' Width="100%" meta:resourcekey="lblOperationNameResource1"></asp:Label>										
									                                          
									</ItemTemplate>
                                </asp:TemplateField>                                             
                            </Columns>
                            <HeaderStyle  CssClass="HeaderStyle"/>
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle CssClass="LeftPagerStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                            <EditRowStyle BorderStyle="None" />          
                        </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="dsOper" runat="server" ConvertNullToDBNull="True"
            EnablePaging="True" OnSelecting="dsOper_Selecting" SelectCountMethod="findCount"
            SelectMethod="find" TypeName="FIH.Security.db.OperRoleGp" DataObjectTypeName="FIH.Security.db.OperRoleGpInfo" DeleteMethod="Delete">
            <SelectParameters>
                <asp:Parameter Name="operRoleGp" Type="Object" />
                <asp:ControlParameter ControlID="gvList" Name="startRowIndex" PropertyName="PageIndex"
                    Type="Int32" />
                <asp:ControlParameter ControlID="gvList" Name="maximumRows" PropertyName="PageSize"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>

