<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WfrmCheckDiskNumber.ascx.cs"   Inherits="BOUNDARY_WfrmCheckDiskNumber" %>
<%@ Register Src="../WEBCONTROLER/modeltitle.ascx" TagName="modeltitle" TagPrefix="uc1" %>
<uc1:modeltitle id="ModelTitle2" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center" border="0">
	<tr>
		<td><asp:label id="lblDiskNo" runat="server" Width="150px">PID or IMEI or Apart</asp:label></td>
		<td>
			<asp:textbox id="txtDiskno" runat="server" Width="150px"></asp:textbox> 
		</td>
		<td align="right" width="120px"> 
		    <asp:button id="btnQuery" Text="Query" Runat="server" OnClick="btnQuery_Click" Width="80px"></asp:button></td>
	</tr>
</table>
<hr/>  
<table>
    <tr>
        <td align="left">
           <asp:Label id="Label4" runat="server" CssClass="DataGridFont"></asp:Label>          
        </td>
    </tr>
    <tr>
    <td>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
             Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" 
             BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="false" OnPageIndexChanged="GridView1_PageIndexChanged" 
             OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" >
             <Columns>
                <asp:BoundField DataField="APART" HeaderText="A Part" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="PID" HeaderText="PID" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="KEYPART" HeaderText="Key Part" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="Vendor" HeaderText="Vendor" ReadOnly="True"></asp:BoundField>
                <asp:TemplateField HeaderText="DateCode" >
                     <ItemTemplate> 
                         <asp:LinkButton ID="lbtnDateCode" runat="server" CommandName="DateCode" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.DATECODE") %>'></asp:LinkButton>
                     </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LotCode" >
                     <ItemTemplate> 
                         <asp:LinkButton ID="lbtnLotCode" runat="server" CommandName="LotCode" CausesValidation="false"  Text='<%# DataBinder.Eval(Container, "DataItem.LOTCODE") %>'></asp:LinkButton>
                     </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QTY" HeaderText="Qty" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="DISKNO" HeaderText="DiskNo" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="WO" HeaderText="WO" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="INPUTMATERIALDATE" HeaderText="InputMaterialDate" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="LINE" HeaderText="Line" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="EMPNO" HeaderText="EmpNo" ReadOnly="True"></asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066"/>
            <PagerStyle  HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
            <%--<HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="DataGridFixedHeader"/>--%>
            
            <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
            <AlternatingRowStyle BackColor="WhiteSmoke" />        
        </asp:GridView>  
        </td>
    </tr>
</table>     
