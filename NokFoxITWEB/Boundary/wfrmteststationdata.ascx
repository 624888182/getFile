<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmTestStationData" CodeFile="WFrmTestStationData.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td width="50" rowSpan="6"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"  ></asp:dropdownlist></td>
		<td style="HEIGHT: 18px"><asp:label id="lblLine" runat="server" Width="100px">Line</asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlLine" runat="server" Width="155px">
				<asp:ListItem Selected="True"></asp:ListItem>
				<asp:ListItem Value="LINE1">LINE1</asp:ListItem>
				<asp:ListItem Value="LINE2">LINE2</asp:ListItem>
				<asp:ListItem Value="LINE3">LINE3</asp:ListItem>
				<asp:ListItem Value="LINE4">LINE4</asp:ListItem>
				<asp:ListItem Value="LINE5">LINE5</asp:ListItem>
				<asp:ListItem Value="LINE6">LINE6</asp:ListItem>
				<asp:ListItem Value="LINE7">LINE7</asp:ListItem>
				<asp:ListItem Value="LINE8">LINE8</asp:ListItem>
				<asp:ListItem Value="LINE9">LINE9</asp:ListItem>
				<asp:ListItem Value="LINE10">LINE10</asp:ListItem>
				<asp:ListItem Value="LINE11">LINE11</asp:ListItem>
				<asp:ListItem Value="LINE12">LINE12</asp:ListItem>
				<asp:ListItem Value="LINE13">LINE13</asp:ListItem>
				<asp:ListItem Value="LINE14">LINE14</asp:ListItem>
				<asp:ListItem Value="LINE15">LINE15</asp:ListItem>
				<asp:ListItem Value="LINE16">LINE16</asp:ListItem>
				<asp:ListItem Value="LINE17">LINE17</asp:ListItem>
				<asp:ListItem Value="LINE18">LINE18</asp:ListItem>
				<asp:ListItem Value="LINE19">LINE19</asp:ListItem>
				<asp:ListItem Value="LINE20">LINE20</asp:ListItem>
			</asp:dropdownlist></td>
		<td rowSpan="6" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" onclick="btnQuery_Click"></asp:button>
			 <br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel"  onclick="btnExportExcel_Click"></asp:button>
			 
			</td>
			<td rowSpan="6" style="width: 149px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnFailQuery" runat="server" Width="100px" Text="Fail Query" OnClick="btnFailQuery_Click" ></asp:button>
			 <br>
			<br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnFailToExcel" runat="server" Width="100px" text="Fail To Excel" OnClick="btnFailToExcel_Click" ></asp:button>
			 
			</td>
	</tr>
	<tr>
		<td><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td><asp:dropdownlist id="ddlStation" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist></td>
		<td><asp:label id="lblRepair" runat="server" Width="100px"> Repair</asp:label></td>
		<td><asp:dropdownlist id="ddlRepair" runat="server" Width="155px">
				<asp:ListItem Value="0" Selected="True">Without Repair</asp:ListItem>
				<asp:ListItem Value="1">With Repair</asp:ListItem>
			</asp:dropdownlist></td>
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
	<tr>
		<td colSpan="4"><asp:radiobuttonlist id="rblQueryType" runat="server" Width="568px" RepeatDirection="Horizontal" RepeatColumns="4"
				CssClass="DataGridFont">
				<asp:ListItem Value="Total Data" Selected="True">Total Data</asp:ListItem>
				<asp:ListItem Value="Final Data">Final Data</asp:ListItem>
				<asp:ListItem Value="First Data">First Data</asp:ListItem>
				<asp:ListItem Value="Retest Fail Data">Retest Fail Data</asp:ListItem>
				<asp:ListItem Value="All Fail Data">All Fail Data</asp:ListItem>
				<asp:ListItem Value="First Fail Data">First Fail Data</asp:ListItem>
				<asp:ListItem Value="Final Fail Information">Final Fail Information</asp:ListItem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="lblWO" runat="server" Width="100px">WorkOrder</asp:label></td>
		<td><asp:textbox id="tbWO" runat="server"></asp:textbox></td>
		<td><asp:label id="lblProductID" runat="server" Width="100px">Product ID</asp:label></td>
		<td><asp:textbox id="tbProductID" runat="server"></asp:textbox></td>
	</tr>
	<TR>
		<td style="height: 24px"><asp:label id="lblIMEI" runat="server" Width="100px">IMEI/Picasso</asp:label></td>
		<td style="WIDTH: 198px; height: 24px;"><asp:textbox id="tbIMEI" runat="server"></asp:textbox></td>
		<td style="height: 24px"><FONT face="新細明體"><asp:label id="lblPCName" runat="server" Width="100px">PC Name</asp:label></FONT></td>
		<td style="height: 24px"><asp:textbox id="tbPCName" runat="server"></asp:textbox></td>
	</TR>
</table>
<hr>
<asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>
<br>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px">
	<asp:datagrid id="dgTestStationData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3" PageSize="25" ShowFooter="True" onitemdatabound="FailData_ItemDataBound" >
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
		 
        
	</asp:datagrid>
	<asp:GridView ID="gvList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%"
            SkinID="gvMain" OnRowDataBound="gvList_RowDataBound" DataKeyNames="ERROR_MSG" OnPageIndexChanging="gvList_PageIndexChanging">
            <Columns>
               <asp:BoundField DataField="ERROR_MSG" HeaderText="ERROR_MSG" />
                <asp:TemplateField HeaderText="Fail Qty">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="Detail" Text='<%# Bind("FailQty") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="FailLoss%" HeaderText="FailLoss%" />
               </Columns>
            <HeaderStyle CssClass="HeaderStyle" />
            <FooterStyle CssClass="FooterStyle" />
            <PagerStyle CssClass="PagerStyleLeft" />
            <RowStyle CssClass="RowStyle" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
            <EditRowStyle BorderStyle="None" />
        </asp:GridView>
        <br />
        <asp:textbox id="txtSqlFail" runat="server" Visible="False"></asp:textbox>
</div>
