<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmSFCConfiguration.ascx.cs"
    Inherits="Boundary_WFrmSFCConfiguration" %>
<%@ Register Assembly="WebDataGrid" Namespace="System.Web.UI.WebControls" TagPrefix="cwc" %>
<%@ Register Src="../WebControler/modeltitle.ascx" TagName="modeltitle" TagPrefix="uc1" %>
<uc1:modeltitle ID="Modeltitle1" runat="server" />
<table class="DataGridFont">
    <tr>
        <td style="height: 26px">
            <asp:Label ID="lblModel" runat="server" Text="Model" Width="100px"></asp:Label></td>
        <td style="width: 198px; height: 26px;">
            <asp:DropDownList ID="ddlModel" runat="server" Width="155px">
            </asp:DropDownList></td>
        <td style="height: 26px">
            <asp:Label ID="lblStationID" runat="server" Width="100px">Station ID</asp:Label></td>
        <td style="width: 198px; height: 26px;">
            <asp:DropDownList ID="ddlStationID" runat="server" Width="155px">
            </asp:DropDownList></td>
        <td style="width: 119px; height: 26px;">
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Button ID="btnQuery" runat="server" Text="Query" Width="80px" OnClick="btnQuery_Click" /></td>
    </tr>
</table>
<hr />
<asp:Button ID="btnAdd" Text="Add" runat="server" Width="80px" OnClick="btnAdd_Click" />
<cwc:WebDataGrid ID="dgConfiguration" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    BorderColor="MediumSlateBlue" BorderStyle="Solid" BorderWidth="1px" CellPadding="4"
    CurrentStatus="" DisablePaging="False" DisableSetButton="False" DisableSort="False"
    Font-Names="Verdana" Font-Size="10px" SaveSettings="False" SetShowFooter="True"
    ShowFooter="True" SummaryText="" sUserLanguage="en-us" UserID="Any" AllowPaging="True" OnCancelCommand="dgConfiguration_CancelCommand" OnEditCommand="dgConfiguration_EditCommand" OnItemCommand="dgConfiguration_ItemCommand" OnItemCreated="dgConfiguration_ItemCreated" OnDeleteCommand="dgConfiguration_DeleteCommand" OnItemDataBound="dgConfiguration_ItemDataBound" OnUpdateCommand="dgConfiguration_UpdateCommand" CssClass="FontDataGrid" PageSize="20">
    <FooterStyle BackColor="Lavender" />
    <Columns>
        <asp:TemplateColumn HeaderText="Edit">
            <itemtemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="編輯" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
            </itemtemplate>
            <edititemtemplate>
                <asp:LinkButton ID="LinkButton2" runat="server" Text="更新" CommandName="Update"></asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButton3" runat="server" Text="取消" CausesValidation="false" CommandName="Cancel"></asp:LinkButton> 
            </edititemtemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Delete">
            <itemtemplate>
                <asp:LinkButton id="LinkButton4" runat="server" Text="刪除" CausesValidation="false" CommandName="Delete" __designer:wfdid="w1"></asp:LinkButton> 
            </itemtemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Model">
            <itemtemplate>
                <asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Model") %>' __designer:wfdid="w14"></asp:Label> 
            </itemtemplate>
            <edititemtemplate>
                <asp:DropDownList id="ddlModel1" runat="server" __designer:wfdid="w15"></asp:DropDownList> 
            </edititemtemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Station ID">
            <itemtemplate>
                <asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_ID") %>' __designer:wfdid="w25"></asp:Label> 
            </itemtemplate>
            <edititemtemplate>
                <asp:DropDownList id="ddlStationID1" runat="server" __designer:wfdid="w26"></asp:DropDownList> 
            </edititemtemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Column Name">
            <itemtemplate>
                <asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COLUMN_NAME") %>' __designer:wfdid="w7"></asp:Label> 
            </itemtemplate>
            <edititemtemplate>
                <asp:DropDownList id="ddlColumnName" runat="server" __designer:wfdid="w8"></asp:DropDownList> 
            </edititemtemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="COLUMN_Desc" HeaderText="Column Desc"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="Function Type">
            <itemtemplate>
<asp:Label id="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_ID") %>' __designer:wfdid="w1"></asp:Label> 
</itemtemplate>
            <edititemtemplate>
&nbsp;<asp:DropDownList id="ddlFunctionType" runat="server" __designer:wfdid="w2" AutoPostBack="True" OnSelectedIndexChanged="ddlFunctionType_SelectedIndexChanged"></asp:DropDownList>&nbsp;<asp:ImageButton id="ibtnFunctionType" onclick="ibtnFunctionType_Click" runat="server" __designer:wfdid="w3" ImageUrl="../Images/plus2.gif"></asp:ImageButton> 
</edititemtemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Function Value">
            <itemtemplate>
<asp:Label id="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_Value") %>' __designer:wfdid="w8"></asp:Label> 
</itemtemplate>
            <edititemtemplate>
&nbsp;<asp:DropDownList id="ddlFunction_Value" runat="server" __designer:wfdid="w9"></asp:DropDownList> <asp:ImageButton id="ibtnFunctionValue" onclick="ibtnFunctionType_Click" runat="server" __designer:wfdid="w10" ImageUrl="../Images/plus2.gif"></asp:ImageButton> <asp:TextBox id="tbFunction_Value" runat="server" __designer:wfdid="w11" Visible="False"></asp:TextBox> 
</edititemtemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="Creation_Date" HeaderText="Creation Date"></asp:BoundColumn>
        <asp:TemplateColumn HeaderText="ROWID" Visible="False">
            <itemtemplate>
<asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ROWID") %>'></asp:Label>
</itemtemplate>
            <edititemtemplate>
<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ROWID") %>'></asp:TextBox>
</edititemtemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle BackColor="Plum" HorizontalAlign="Right" Mode="NumericPages" />
    <HeaderStyle BackColor="CornflowerBlue" Font-Bold="True" ForeColor="Black" />
    <ItemStyle BackColor="Cornsilk" />
    <AlternatingItemStyle BackColor="WhiteSmoke" />
</cwc:WebDataGrid>&nbsp; 