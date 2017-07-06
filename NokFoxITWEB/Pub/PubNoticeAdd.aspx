<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubNoticeAdd.aspx.cs" Inherits="Pub_GateHouseAdd"
    StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd">公告管理</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="1" height="20">
                    <asp:Button ID="btnCommit" runat="server" Text="保存" OnClick="btnCommit_Click" AccessKey="S"
                        ToolTip="ALT+S" SkinID="mainButton"></asp:Button>
                    <asp:Button ID="btnEdit" runat="server" Text="修改" AccessKey="M" ToolTip="ALT+M" SkinID="mainButton"
                        OnClick="btnEdit_Click"></asp:Button>
                    <asp:Button ID="btnExit" runat="server" Text="退出" CausesValidation="False" OnClick="btnExit_Click"
                        SkinID="mainButton"></asp:Button></td>
            </tr>
            <tr>
                <td colspan="4" class="TdMessage" style="height: 12px">
                    <asp:Label ID="lblMessage" runat="server" SkinID="lblMessage"></asp:Label></td>
            </tr>
            <tr>
                <td class="founction" colspan="1">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="TextTable">
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblTitle" runat="server" SkinID="lblRed">主旨:</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtTitle" runat="server" SkinID="txtMain" Width="600px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblMeno" runat="server" SkinID="lblMain">描述:</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtMemo" runat="server" SkinID="txtMain" Width="600px" TextMode="MultiLine"
                                    Rows="5"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 22px" runat=server visible=false>
                                <asp:TextBox ID="txtNoticeCode" runat="server" Width="33px"></asp:TextBox>
                                <asp:TextBox ID="txtCreateUser" runat="server" Width="33px"></asp:TextBox>
                                <asp:TextBox ID="txtCreateDate" runat="server" Width="33px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px" runat=server id = "FileDetail1">
                                <asp:Label ID="lblAttach" runat="server" Text="附件:" SkinID="lblMain"></asp:Label>
                            </td>
                            <td>
                                <table width="600px" runat=server id="FileDetail2">
                                    <tr>
                                        <td>
                                            <asp:DataList ID="DataList1" runat="server" DataKeyField="AttachID" OnItemCommand="DataList1_ItemCommand" OnItemCreated="DataList1_ItemCreated">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnDelete" CommandName="Delete" runat="server" AlternateText="刪除" ImageUrl="~/App_Themes/SkinFile/images/DeleteFile.gif" />&nbsp;
                                                    <asp:ImageButton ID="ibtnDownload" CommandName="Download" runat="server" AlternateText="下載" ImageUrl="~/App_Themes/SkinFile/images/DownloadFile.gif" />&nbsp;
                                                    <a target=_blank href='<%# this.LinkFile %>'><%# Eval("OriginalFileName") %></a>
                                                    <%--<asp:LinkButton ID="lbtnTitle" runat="server" Text = '<%# Eval("OriginalFileName") %>'></asp:LinkButton>--%>
                                                </ItemTemplate>                                              
                                            </asp:DataList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fldFile" runat="server" Width="80%" />
                                            <asp:Button ID="btnUpload" runat="server" Text="上傳" OnClick="btnUpload_Click" Height="21px" Width="34px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;
    </form>
</body>
</html>
