<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperateOper.aspx.cs" Inherits="SysConfig_OperateOper" StylesheetTheme="SkinFile" EnableEventValidation="false"      %>

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
                    <asp:Label ID="lblHead" runat="server" SkinID=lblTitle   Text="操作功能管理"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0"   >
                        <tr>
                            <td class="TdRight"  >
                                <asp:Label  ID="lblOperCode"  runat="server"  SkinID=lblMain   Text="操作功能代碼 : "></asp:Label></td>
                            <td  >
                                <asp:TextBox ID="txtOperCode" runat="server" CssClass="inputcss" MaxLength="100" SkinID=txtMain  ></asp:TextBox></td>
                            <td class="TdRight"  >
                                <asp:Label ID="lblOperName" runat="server" SkinID=lblMain   Text="操作功能名稱 : "></asp:Label></td>
                            <td  >
                                <asp:TextBox ID="txtOperName" runat="server" CssClass="inputcss" MaxLength="100" SkinID=txtMain  ></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea"> 
                    <asp:Button ID="btnReset" runat="server" Visible="false" SkinID=mainButton
                        OnClick="btnReset_Click" Text="重設" AccessKey="T" ToolTip="ALT+T" />
                                <asp:Button ID="btnSearch" runat="server" SkinID=mainButton OnClick="btnFind_Click"
                                    Text="查詢"   />
                                <asp:Button ID="btnAdd" runat="server"  SkinID=mainButton  Text="新增" AccessKey="A" ToolTip="ALT+A"   />
                    <asp:Button ID="btnOut" runat="server" SkinID=mainButton 
                        OnClick="btnOut_Click" Text="退出" AccessKey="E" ToolTip="ALT+E" /></td>
            </tr>            
            <tr>
                <td class="TdGrid" colspan="2" >
                    &nbsp;<asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridView" DataSourceID="dsOper" OnRowCreated="gvList_RowCreated" Width="100%"
            OnRowDataBound="gvList_RowDataBound" OnRowDeleting="gvList_RowDeleting"
            PageSize="15" SkinID="gvMain">
            <Columns>
                <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Text="&lt;img   src=&quot;../App_Themes/SkinFile/images/delete.gif&quot;     border=&quot;0&quot;&gt;"
                            ToolTip="删除?"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <HeaderTemplate>
                        <asp:Label ID="lblItemHead" runat="server" Height="100%" Text="序號" Visible="false"
                            Width="100%"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text="item" Visible="false" Width="100%"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField  >
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOperationCodeHead" runat="server" Height="100%" Text="操作功能代碼" Width="100%"  ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>		
                                       <a href="#" onmouseover="this.style.cursor='hand';" > 								
                                        <asp:Label ID="lblOperationCodeID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OperCode") %>' Width="100%"  ></asp:Label>
                                           </a>   
									</ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField  >
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOperationNameHead" runat="server"  Height="100%" Text="操作功能名稱" Width="100%"  ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      
                                          <asp:Label ID="lblOperationName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OperName") %>' Width="100%"  ></asp:Label>										
									                                       
									</ItemTemplate>
                                </asp:TemplateField>  
                                       
            </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="LeftPagerStyle" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="dsOper" runat="server" ConvertNullToDBNull="True"
            EnablePaging="True" OnSelecting="dsOper_Selecting" SelectCountMethod="findCount"
            SelectMethod="find" TypeName="FIH.Security.db.Operate" DataObjectTypeName="FIH.Security.db.OperateInfo" DeleteMethod="Delete">
            <SelectParameters>
                <asp:Parameter Name="operate" Type="Object" />
                <asp:ControlParameter ControlID="gvList" Name="startRowIndex" PropertyName="PageIndex"
                    Type="Int32" />
                <asp:ControlParameter ControlID="gvList" Name="maximumRows" PropertyName="PageSize"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;
    </form>
</body>
</html>

