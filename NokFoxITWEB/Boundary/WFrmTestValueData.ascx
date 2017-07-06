<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmTestValueData.ascx.cs" Inherits="Boundary_WFrmTestValueData" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
	    <td style="width: 300px">
	    </td>
		<td >
		<asp:label id="lblModel" runat="server" Width="100px">PID:</asp:label>
		</td>
		<td style="width: 200px">
            <asp:TextBox ID="txtPID" runat="server"></asp:TextBox>
		</td>
		<td style="width: 149px">
            <asp:Button ID="btnQuery" runat="server" Text="¬d ¸ß" Width="84px" OnClick="btnQuery_Click" />
		</td>
		</tr>
		<tr>
		<td style="width: 300px">
	    </td>
		<td colSpan="2"><asp:radiobuttonlist id="rblQueryType" runat="server" Width="300px" RepeatDirection="Horizontal" RepeatColumns="4"
				CssClass="DataGridFont">
				<asp:ListItem Value="Total Data" Selected="True">Total Data</asp:ListItem>
				<asp:ListItem Value="Final Data">Fail Data</asp:ListItem>		 
			</asp:radiobuttonlist></td>
		<td style="width: 149px">
            <asp:Button ID="btnToExcel" runat="server" Text="¾É  ¥X" Width="84px" OnClick="btnToExcel_Click" />
		</td>
	    </tr>
	   <%-- <tr>
	        <td style="width: 300px">
	        </td>
	        <td colSpan="2"><asp:radiobuttonlist id="rblSort" runat="server" Width="300px" RepeatDirection="Horizontal" RepeatColumns="4"
				CssClass="DataGridFont">
				<asp:ListItem Value="Total Data" Selected="True">Asc</asp:ListItem>
				<asp:ListItem Value="Final Data">Desc</asp:ListItem>		 
			</asp:radiobuttonlist></td>
	    </tr>--%>
</table>
<asp:Label id="LabelTotal" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<asp:GridView ID="dgData" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%"
            SkinID="gvMain" OnRowDataBound="Data_ItemDataBound" DataKeyNames="PID"> <%--OnRowCommand="FailData_RowCommand"--%>
            <Columns>
               <asp:BoundField DataField="PID" HeaderText="PID" />
               <asp:BoundField DataField="Status" HeaderText="Status" />
               <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
               <asp:BoundField DataField="MinSpec" HeaderText="MinSpec" />
               <asp:BoundField DataField="MaxSpec" HeaderText="MaxSpec" />
               <asp:BoundField DataField="TestValue" HeaderText="TestValue" />
               <asp:BoundField DataField="Unit" HeaderText="Unit" />
               <asp:BoundField DataField="TestTime" HeaderText="TestTime" />
                <%--<asp:TemplateField HeaderText="RetestQty">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="RetestQty" Text='<%# Bind("RetestQty") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PID" HeaderText="PID" />
                 --%>
               </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="PagerStyleLeft" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <EditRowStyle BorderStyle="None" />
        </asp:GridView>
