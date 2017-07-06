<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmITRouteSet.ascx.cs" Inherits="Boundary_wfrmITRouteSet" %>
<fieldset>
    <legend>SFC重定义测试站点名管理控制台</legend>
    <br />
                機種: 
    <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
        Width="188px">
    </asp:DropDownList><br />
    <table>
        <tr valign="top">
            <td>
            </td>
            <td style="width: 256px">
                <asp:ListBox ID="ListBoxTableName" runat="server" Height="228px" Width="295px" AutoPostBack="True" OnSelectedIndexChanged="ListBoxTableName_SelectedIndexChanged"></asp:ListBox></td>
            <td align="left">
                <asp:DropDownList ID="DropDownListStation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListStation_SelectedIndexChanged"
                    Width="188px">
                </asp:DropDownList><br />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="增加測試站" /><br />
                <span style="font-size: 10pt">
                    <br />
                    站點名稱：</span><asp:TextBox ID="TextBoxStation" runat="server" Width="119px"></asp:TextBox><br />
                <br />
                <span style="font-size: 10pt">站點描述：</span><asp:TextBox ID="TextBoxStationDesc" runat="server"
                    Width="119px"></asp:TextBox><br />
                <br />
                <span style="font-size: 10pt"></span>
                <br />
            </td>
            <td>
                <br />
                <br />
                <br />
                <br />
                <asp:Button ID="ButtonSelect" runat="server" OnClick="ButtonSelect_Click" Text=">>"
                    Width="58px" /><br />
                <br />
                <asp:Button ID="ButtonUnselect" runat="server" OnClick="ButtonUnselect_Click" Text="<<"
                    Width="58px" /><br />
                <br />
            </td>
            <td>
                &nbsp;<asp:ListBox ID="ListBoxRouter" runat="server" Height="228px" SelectionMode="Multiple"
                    Width="295px"></asp:ListBox></td>
            <td style="width: 63px">
                <br />
                <br />
                <br />
                <asp:Button ID="ButtonUP" runat="server" OnClick="ButtonUP_Click" Text="UP" Width="75px" /><br />
                <br />
                <asp:Button ID="ButtonDOWN" runat="server" OnClick="ButtonDOWN_Click" Text="DOWN"
                    Width="76px" />
                <br />
                <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="保存路由"
                    Width="76px" /><br />
            </td>
            <td style="width: 80px">
            </td>
            <td>
                维护说明:</td>
        </tr>
    </table>
    1. PROCEDURE ROUTE_CODE_GET_MODEL(MODEL OUT RETURNDATASET);<br />
    2. PROCEDURE ROUTE_CODE_GET_TABLENAME(MODEL IN VARCHAR2,TABLE_NAME OUT RETURNDATASET
    );<br />
    3. PROCEDURE ROUTE_CODE_GET_STATION_NAME(TABLE_NAME IN VARCHAR2,STATION_NAME OUT
    RETURNDATASET);<br />
    4.SAVE
    <br />
    Insert into ROUTE_CODE_STATION_DESCRIPTION(MODEL, STATION_ID, DESCRIPTION, TESTSTATION,
    USER_NAME,CREATE_DATE) Values('LPI', 'B2', 'BASEBANDTEST', 'Y', 'F2832614',TO_DATE('04/03/2009
    10:08:12', 'MM/DD/YYYY HH24:MI:SS'))
</fieldset>
