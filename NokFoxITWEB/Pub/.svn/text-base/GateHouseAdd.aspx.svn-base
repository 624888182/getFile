<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GateHouseAdd.aspx.cs" Inherits="Pub_GateHouseAdd" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="formUser" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImgAdd" style="width: 899px">
                    <asp:Label ID="lblHead" runat="server" SkinID="lblTitleAdd">門崗資料</asp:Label>
                </td>
            </tr>
            <tr>
                <td class="buttonArea" colspan="1" height="20" style="width: 899px">
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
                <td class="founction" colspan="1" style="width: 899px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="TextTable">
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblGateHouseCode" runat="server" SkinID=lblRed>門崗編號:</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtGateHouseCode" runat="server" MaxLength="20" SkinID="txtMain" Enabled="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblDescription" runat="server" SkinID=lblRed>描述:</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="50" SkinID="txtMain" Enabled="False" Width="377px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TdRight" style="height: 22px">
                                <asp:Label ID="lblMemo" runat="server" SkinID=lblMain>備註:</asp:Label></td>
                            <td class="Tdleft" style="height: 22px">
                                <asp:TextBox ID="txtMemo" runat="server" MaxLength="50" SkinID="txtMain" Enabled="False" Width="377px" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>                        
                        
                        <tr visible="false">
                            <td style="height: 22px">
                            </td>
                            <td style="height: 22px" visible="false">
                                <asp:TextBox ID="txtInitiateId" runat="server" Width="26px" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtInitiateDate" runat="server" Width="26px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
