<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterFactApplyMasterWorkFlow.aspx.cs"
    Inherits="App_EnterFactApplyMasterWorkFlow" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 280px; left: 10px; position: absolute;">
            <fieldset style="height: 80px;">
                <legend>�Ĥ@�B(��l���A)</legend>
                <table style="width: 100%; height: 50%">
                    <tr>
                        <td width="30%">
                            �渹
                        </td>
                        <td>
                            <asp:Label ID="lblApplyCode" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            �ӽФH
                        </td>
                        <td>
                            <asp:Label ID="lblApplyID" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="height: 80px">
                <legend>�ĤG�B(�t���f��)</legend>
                <table style="width: 100%; height: 50%">
                    <tr>
                        <td width="30%">
                            �f�֥D��
                        </td>
                        <td>
                            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
                        </td>
                            <td rowspan="2" width="20%">
                                <asp:RadioButtonList ID="rblDivision" runat="server" Enabled="False">
                                    <asp:ListItem>�q�L</asp:ListItem>
                                    <asp:ListItem>�ڵ�</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>                        
                    </tr>
                    <tr>
                        <td>
                            �f�֤��</td>
                        <td>
                            <asp:Label ID="lblDivisionDate" runat="server" Text=""></asp:Label></td>
                        <td>
                     </tr>
                </table>
            </fieldset>
            <fieldset style="height: 80px" runat=server id="fieldBU">
                <legend>�ĤT�B(�B�żf��)</legend>
                <table style="width: 100%; height: 50%">
                    <tr>
                        <td width="30%">
                            �f�֥D��
                        </td>
                        <td>
                            <asp:Label ID="lblBU" runat="server" Text=""></asp:Label>
                        </td>
                            <td rowspan="2" width="20%">
                                <asp:RadioButtonList ID="rblBU" runat="server" Enabled="False">
                                    <asp:ListItem>�q�L</asp:ListItem>
                                    <asp:ListItem>�ڵ�</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>                        
                    </tr>
                    <tr>
                        <td>
                            �f�֤��</td>
                        <td>
                            <asp:Label ID="lblBUDate" runat="server" Text=""></asp:Label></td>
                        <td>
                     </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
