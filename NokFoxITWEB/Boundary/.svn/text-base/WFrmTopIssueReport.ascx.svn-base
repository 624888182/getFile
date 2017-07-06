<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmTopIssueReport.ascx.cs" Inherits="Boundary_WFrmTopIssueReport" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="6"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" ></asp:dropdownlist></td>
		<td style="HEIGHT: 18px"><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlStation" runat="server" Width="155px">
		</asp:dropdownlist></td>
		<td rowSpan="6" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" OnClick="btnQuery_Click" ></asp:button>
			 <br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel" OnClick="btnExportExcel_Click" ></asp:button>
			 
			</td>
	     
	</tr>
	<tr>
		<td><asp:label id="lblTop" runat="server" Width="100px"> Top</asp:label></td>
		<td><asp:dropdownlist id="ddlTop" runat="server" Width="155px">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="TOP1">1</asp:ListItem>
				<asp:ListItem Value="TOP2">2</asp:ListItem>
				<asp:ListItem Value="TOP3">3</asp:ListItem>
				<asp:ListItem Value="TOP4">4</asp:ListItem>
				<asp:ListItem Value="TOP5" Selected="True">5</asp:ListItem>
				<asp:ListItem Value="TOP6">6</asp:ListItem>
				<asp:ListItem Value="TOP7">7</asp:ListItem>
				<asp:ListItem Value="TOP8">8</asp:ListItem>
				<asp:ListItem Value="TOP9">9</asp:ListItem>
				<asp:ListItem Value="TOP10">10</asp:ListItem>
				<asp:ListItem Value="TOPALL">ALL</asp:ListItem>
			</asp:dropdownlist>
				 
	   </td>
	</tr>
	<tr>
		<td><asp:label id="lblStartDate" runat="server" Width="100px">Date From</asp:label></td>
		<td style="WIDTH: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
			<asp:label id="Label28" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
		<td style="HEIGHT: 23px"><asp:label id="lblEndDate" runat="server" Width="100px">Date To</asp:label></td>
		<td style="HEIGHT: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
			<asp:label id="Label29" runat="server" Visible="False" ForeColor="Red"></asp:label></td>
	</tr>
 	 
</table>
<table>
<tr>
 	 <td>溫馨提示:FailQty之和不一定等於AllFailQty是因為有些FailuresDescription為空</td>
 	 </tr>
</table>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px"><%--PageSize="25"--%>
	<asp:datagrid id="dgData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="False" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3"  ShowFooter="True" onitemdatabound="FailData_ItemDataBound">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>        
	</asp:datagrid>
	<%--<asp:GridView ID="dgData" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%"
            SkinID="gvMain" OnRowDataBound="FailData_ItemDataBound" DataKeyNames="FailuresDescription" OnRowCommand="FailData_RowCommand">
            <Columns>
               <asp:BoundField DataField="TestStation" HeaderText="TestStation" />
               <asp:BoundField DataField="FailuresDescription" HeaderText="FailuresDescription" />
               <asp:BoundField DataField="InputQty" HeaderText="InputQty" />
               <asp:BoundField DataField="RetestYieldloss(%)" HeaderText="RetestYieldloss(%)" />
                <asp:TemplateField HeaderText="RetestQty">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="RetestQty" Text='<%# Bind("RetestQty") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PID" HeaderText="PID" />
                 
               </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="PagerStyleLeft" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <EditRowStyle BorderStyle="None" />
        </asp:GridView>--%>
</div>