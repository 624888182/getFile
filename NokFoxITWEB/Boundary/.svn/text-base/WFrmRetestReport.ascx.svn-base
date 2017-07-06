<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmRetestReport.ascx.cs" Inherits="Boundary_WFrmRetestReport" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellpadding="0" border="0">
	<tr>
		<td width="50" rowSpan="6"><FONT face="新細明體"></FONT></td>
		<td style="HEIGHT: 18px"><asp:label id="lblModel" runat="server" Width="100px">Model</asp:label></td>
		<td style="WIDTH: 198px; HEIGHT: 18px"><asp:dropdownlist id="ddlModel" runat="server" Width="155px" AutoPostBack="true" ></asp:dropdownlist></td>
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
		<td rowspan="3" style="width: 149px" align="center">  
		    			 
			<asp:button id="btnQuery" runat="server" Width="100px" Text="Query" OnClick="btnQuery_Click" ></asp:button>
			 <br/>			 
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="btnExportExcel" runat="server" Width="100px" text="Export To Excel" OnClick="btnExportExcel_Click" ></asp:button>
			  			</td>
	    <td rowspan="3">
	    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	         <asp:ListBox ID="listPID" runat="server" Height="150px" Width="150px"></asp:ListBox>
	    </td>
			 
	</tr>
	<tr>
		<%--<td><asp:label id="lblStation" runat="server" Width="100px">Station</asp:label></td>
		<td><asp:dropdownlist id="ddlStation" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist></td>--%>
		<td><asp:label id="lblRepair" runat="server" Width="100px"> Repair</asp:label></td>
		<td><asp:dropdownlist id="ddlRepair" runat="server" Width="155px">
				<asp:ListItem Value="0" Selected="True">Without Repair</asp:ListItem>
				<%--<asp:ListItem Value="1">With Repair</asp:ListItem>--%>
			</asp:dropdownlist></td>
	    <td style="HEIGHT: 23px"><asp:label id="LabelType" runat="server" Width="100px">Type</asp:label></td>
		<td style="HEIGHT: 23px"><asp:radiobuttonlist id="rblQueryType" runat="server" Width="150px" RepeatDirection="Horizontal" RepeatColumns="4"
				CssClass="DataGridFont">
				<asp:ListItem Value="Total Data" Selected="True">Final Pass</asp:ListItem>
				<asp:ListItem Value="Final Data">Final Fail</asp:ListItem>
			</asp:radiobuttonlist>
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
 	 <tr>
 	 <td colspan="4">
 	 溫馨提示:FailuresDescription可能會出現0的情況,這表示沒有相關的的失敗描述!
 	 </td>
 	 <td colspan="2">
 	 <asp:button id="btnExportDetailedExcel" runat="server" Width="100px" Text="Detail To Excel" OnClick="btnExportDetailedExcel_Click" ></asp:button>

 	 </td>
 	 </tr>
</table>
<div class="DIVScrolling" id="divsize" style="WIDTH: 100%; HEIGHT: 500px"><%--PageSize="25"--%>
	<%--<asp:datagrid id="dgData" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="False" Font-Names="Verdana"
		BorderColor="#CCCCCC" CellPadding="3"  ShowFooter="True" onitemdatabound="FailData_ItemDataBound">
		<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" 
			BackColor="SteelBlue" CssClass="DataGridFixedHeader"></HeaderStyle>
		<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>        
	</asp:datagrid>--%>
	<asp:GridView ID="dgData" runat="server" AllowPaging="False" AutoGenerateColumns="False" Width="100%"
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
        </asp:GridView>
</div>