<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GateHouse.aspx.cs" Inherits="Pub_GateHouse" StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>門崗資料</title>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="titleImg">
                    <asp:Label ID="lblHead" SkinID="lblTitle" runat="server" Text="門崗資料"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="FindTable">
                        <tr>
                            <td class="TdFindRight">
                                <asp:Label ID="lblGetHouseCode" runat="server" SkinID="lblMain" Width="87px">門崗編號:</asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtGetHouseCode" runat="server" SkinID="txtMain" MaxLength="100"></asp:TextBox></td>
                            <td class="TdFindRight">
                                <asp:Label ID="lblDescription" runat="server" SkinID="lblMain" Width="84px" meta:resourcekey="lblDescriptionResource1">描述:</asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="inputcss" MaxLength="100"
                                    SkinID="txtMain" meta:resourcekey="txtDescriptionResource1"></asp:TextBox></td>
                            <td class="TdFindRight">
                                <asp:Label ID="lblMemo" runat="server" SkinID=lblMain Width="84px">備註:</asp:Label></td>
                            <td class="TdFindLeft">
                                <asp:TextBox ID="txtMemo" runat="server" MaxLength="100"
                                    SkinID="txtMain"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="buttonArea">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" SkinID="MainButton" OnClick="btnFind_Click"
                        Text="查詢" />
                    <asp:Button ID="btnAdd" runat="server" Text="新增" SkinID="MainButton" OnClick="btnAdd_Click"
                        meta:resourcekey="btnAddResource1" AccessKey="N" ToolTip="ALT+N" />
                    <asp:Button ID="btnOut" runat="server" SkinID="MainButton" OnClick="btnOut_ServerClick"
                        Text="返回" meta:resourcekey="btnOutResource1" AccessKey="E" ToolTip="ALT+E" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="15" DataSourceID="dsGateHouse" SkinID="gvMain" DataKeyNames="GatehouseCode" OnRowDeleting="gvList_RowDeleting" OnRowDataBound="gvList_RowDataBound" >
            <FooterStyle CssClass="FooterStyle" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/App_Themes/SkinFile/images/delete.gif" CommandName="Delete" AlternateText="刪除" />
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/App_Themes/SkinFile/images/edit.gif" CommandName="Edit"  AlternateText="修改"/>
                    </ItemTemplate>
                    <HeaderStyle Width="25px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="門崗編號" SortExpression="GatehouseCode">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnGateHouseCode" runat="server" Text='<%# Bind("GateHouseCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="描述" SortExpression="Description" ReadOnly="True" />
                <asp:BoundField DataField="Memo" HeaderText="備註" SortExpression="Memo" ReadOnly="True" />
            </Columns>
            <RowStyle CssClass="RowStyle" />
            <EditRowStyle BorderStyle="None" />
            <PagerStyle CssClass="LeftPagerStyle" />
            <HeaderStyle CssClass="HeaderStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
        </asp:GridView>
        <asp:SqlDataSource ID="dsGateHouse" runat="server" ConnectionString="<%$ ConnectionStrings:ForeignStaffSqlServer %>"
            SelectCommand="SELECT pubGatehouse.* FROM pubGatehouse&#13;&#10;where (GateHouseCode like '%'+@GateHouseCode+'%' or @GateHouseCode is null or @GateHouseCode='' or @GateHouseCode ='''' )&#13;&#10;and (Description like '%'+@Description+'%' or @Description is null or @Description='' or @Description ='''' )&#13;&#10;and (Memo like '%'+@Memo+'%' or @Memo is null or @Memo='' or @Memo ='''' )" DeleteCommand="DELETE FROM pubGatehouse WHERE (GateHouseCode = @GateHouseCode)">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtGetHouseCode" ConvertEmptyStringToNull="False"
                    Name="GateHouseCode" PropertyName="Text" />
                <asp:ControlParameter ControlID="txtDescription" ConvertEmptyStringToNull="False"
                    Name="Description" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtMemo" ConvertEmptyStringToNull="False" Name="Memo"
                    PropertyName="Text" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="GateHouseCode" />
            </DeleteParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
