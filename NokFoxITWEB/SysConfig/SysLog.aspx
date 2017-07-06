<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysLog.aspx.cs" Inherits="SysConfig_SysLog2" StylesheetTheme="SkinFile"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script language="javascript"  src="../App_Themes/SkinFile/popcalendar.js" type="text/javascript"></script>
</head>
<body>
    <form id="formOperate" runat="server" method="post">    
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="PageHeaderLeft">
                    &nbsp;操作歷史記錄管理</td>
                <td class="PageHeaderRight" ></td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table border="0" cellpadding="0" cellspacing="0"  style="width: 100%;height:50px">
                      <tr>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    </td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                    </td>
                                <td style="width: 100px; height: 21px;" class="TD">
                                </td>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    </td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                  </td>
                                <td style="height: 21px;" class="TD">
                                </td>
                        </tr>
                        <tr>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    <asp:Label ID="lbUserID" runat="server"  Width="100%" SkinID="TextLabel">用戶ID : </asp:Label></td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                    <asp:TextBox ID="tbUserID" runat="server" CssClass="inputcss" MaxLength="100" Width="200px"></asp:TextBox></td>
                                <td style="width: 100px; height: 21px;" class="TD">
                                </td>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    <asp:Label ID="lbModuleName" runat="server"  Width="100%" SkinID="TextLabel">模組名稱 : </asp:Label></td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                    <asp:DropDownList ID="ddlModule" runat="server" Width="205px">
                                    </asp:DropDownList></td>
                                <td style="height: 21px;" class="TD">
                                </td>
                            </tr> 
                            <tr>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    <asp:Label ID="lbOperType" runat="server"  Width="100%" SkinID="TextLabel">操作類型 : </asp:Label></td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                    <asp:DropDownList ID="ddlOperType" runat="server" Width="205px">
                                    </asp:DropDownList></td>
                                <td style="width: 100px; height: 21px;" class="TD">
                                </td>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    <asp:Label ID="lbOperTime" runat="server"  Width="100%" SkinID="TextLabel">操作時間 :</asp:Label></td>
                                <td class="TdLeft" colspan="2" style="height: 21px">
                                    <asp:TextBox ID="tbOperTimeStart" runat="server" MaxLength="10" Width="80px" ReadOnly="True"></asp:TextBox><img
                                        id="imgEmpFoldDate" runat="server" align="middle" alt="Start Time"   height="18"
                                        onclick="popUpCalendar(this,tbOperTimeStart,'yyyy-mm-dd')" onmouseover="javascript:this.style.cursor='hand'; "
                                        src="../App_Themes/SkinFile/images/finddate.gif"/>
                                    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server"  Width="20px">到 :</asp:Label><asp:TextBox ID="tbOperTimeEnd" runat="server" MaxLength="10" Width="80px" ReadOnly="True"></asp:TextBox>
                                    <img
                                        id="Img1" runat="server" align="middle" alt="End Time"  height="18"
                                        onclick="popUpCalendar(this,tbOperTimeEnd,'yyyy-mm-dd')" onmouseover="javascript:this.style.cursor='hand'; "
                                        src="../App_Themes/SkinFile/images/finddate.gif" /></td>
                            </tr>
                            <tr>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    </td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                    </td>
                                <td style="width: 100px; height: 21px;" class="TD">
                                </td>
                                <td style="width: 150px; height: 21px;" class="TD" align="right">
                                    </td>
                                <td style="width: 200px; height: 21px;" class="TdLeft">
                                  </td>
                                <td style="height: 21px;" class="TD">
                                </td>
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
                               
                                <input id="btnToExcel" runat="server" class="MainButton" type="button" value="導出Excel" />
                                <input id="btnOut" runat="server" class="MainButton" onserverclick="btnOut_ServerClick"
                                    type="button" value="退出" accesskey="E" />                           
                 </td>
            </tr>  
                      
            <tr>
                <td class="TdGrid" colspan="2" >
                        <asp:GridView CssClass="GridView" ID="GridViewList" runat="server" AutoGenerateColumns="False"  CellPadding="4" BorderWidth="1px" AllowPaging="True" PageSize="15"  OnPageIndexChanging="GridViewList_PageIndexChanging" OnRowDataBound="GridViewList_RowDataBound" OnRowCreated="GridViewList_RowCreated"   >
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbItemHead" runat="server" Height="100%" Text="NO." Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
			                              <asp:Label ID="lbItem" runat="server" Text="item" Width="100%"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField Visible="False">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbLogIdHead" runat="server" Height="100%" Text="記錄ID" Width="100%" ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>										
                                        <asp:Label ID="lbLogId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LogId")%>' Width="100%"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbUserIDHead" runat="server" Height="100%" Text="用戶ID" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>										
                                        <asp:Label ID="lbUserID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID")%>' Width="100%"></asp:Label>
									</ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbUserNameHead" runat="server" Height="100%" Text="用戶名稱" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "UserName")%>
									</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbOperTypeHead" runat="server" Height="100%" Text="操作類型" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "OperType")%>
									</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbModuleNameHead" runat="server" Height="100%" Text="模組名稱" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "ModuleName")%>
									</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lbOperTimeHead" runat="server" Height="100%" Text="操作時間" Width="100%"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem, "OperTime","{0:g}")%>
									</ItemTemplate>
                                </asp:TemplateField>
                                                                                         
                            </Columns>
                           <HeaderStyle  CssClass="HeaderStyle"/>
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                        </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
