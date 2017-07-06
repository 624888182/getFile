<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WFrmFunctionType.aspx.cs" Inherits="Boundary_WFrmFunctionType" %>

<%@ Register Assembly="WebDataGrid" Namespace="System.Web.UI.WebControls" TagPrefix="cwc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>Function Name</title>
    <base target="_self">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="80px" />
        <cwc:WebDataGrid ID="dgFunctionName" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BorderColor="MediumSlateBlue" BorderStyle="Solid"
            BorderWidth="1px" CellPadding="4" CssClass="FontDataGrid" CurrentStatus="" DisablePaging="False"
            DisableSetButton="False" DisableSort="False" Font-Names="Verdana" Font-Size="10px"
            OnCancelCommand="dgFunctionName_CancelCommand" OnDeleteCommand="dgFunctionName_DeleteCommand"
            OnEditCommand="dgFunctionName_EditCommand" OnItemCommand="dgFunctionName_ItemCommand"
            OnItemCreated="dgFunctionName_ItemCreated" OnItemDataBound="dgFunctionName_ItemDataBound"
            OnUpdateCommand="dgFunctionName_UpdateCommand" PageSize="20" SaveSettings="False"
            SetShowFooter="True" ShowFooter="True" SummaryText="" sUserLanguage="en-us" UserID="Any">
            <FooterStyle BackColor="Lavender" />
            <Columns>
                <asp:TemplateColumn HeaderText="Edit">
                    <itemtemplate>
<asp:LinkButton id="LinkButton1" runat="server" Text="編輯" __designer:wfdid="w41" CausesValidation="false" CommandName="Edit"></asp:LinkButton> 
</itemtemplate>
                    <edititemtemplate>
<asp:LinkButton id="LinkButton2" runat="server" Text="更新" __designer:wfdid="w42" CommandName="Update"></asp:LinkButton>&nbsp;<asp:LinkButton id="LinkButton3" runat="server" Text="取消" __designer:wfdid="w43" CausesValidation="false" CommandName="Cancel"></asp:LinkButton> 
</edititemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Delete">
                    <itemtemplate>
<asp:LinkButton id="LinkButton4" runat="server" Text="刪除" CausesValidation="false" CommandName="Delete" __designer:wfdid="w1"></asp:LinkButton> 
</itemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Function ID" Visible="False">
                    <itemtemplate>
<asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_ID") %>'></asp:Label>
</itemtemplate>
                    <edititemtemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_ID") %>'></asp:TextBox>
</edititemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Function Name" SortExpression="Function_Name">
                    <itemtemplate>
<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_Name") %>'></asp:Label>
</itemtemplate>
                    <edititemtemplate>
<asp:TextBox id="tbFunctionName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Function_Name") %>'></asp:TextBox>
</edititemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FUNCTION DESC" SortExpression="FUNCTION_DESC">
                    <itemtemplate>
<asp:Label id="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FUNCTION_DESC") %>'></asp:Label>
</itemtemplate>
                    <edititemtemplate>
<asp:TextBox id="tbFunctionDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FUNCTION_DESC") %>'></asp:TextBox>
</edititemtemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle BackColor="Plum" HorizontalAlign="Right" Mode="NumericPages" />
            <HeaderStyle BackColor="CornflowerBlue" Font-Bold="True" ForeColor="Black" />
            <ItemStyle BackColor="Cornsilk" />
            <AlternatingItemStyle BackColor="WhiteSmoke" />
        </cwc:WebDataGrid></div>
    </form>
</body>
</html>
