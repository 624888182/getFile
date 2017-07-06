<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmFPYquery.ascx.cs" Inherits="Boundary_WFrmFPYquery" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>	
<script type="text/javascript" language="javascript">
function go()
{
	document.forms[0].message.style.display="block";
}
</script>
<asp:Panel id="panel1" runat="server" width="100%" Height="120px">
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr width="100%">
	    <td>
	        <table>
	             <tr>
	                <td width="50" ></td>
		            <td ><asp:label id="lblWO" runat="server" Width="100px">Work Order</asp:label></td>
		            <td>
                        <asp:textbox ID="txtWO" runat="server" />
                    </td>
		            <td><asp:label id="lblModel" runat="server" Width="100px">Model Name</asp:label></td>
		            <td >
                        <asp:textbox ID="txtModel"  runat="server" ReadOnly=true />
                    </td>
		            <td ><FONT face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			            </FONT>
			        <asp:button id="btnQuery" runat="server" Width="80px" Text="Query" onclick="btnQuery_Click"></asp:button></td>
			        <td style="width: 11px" ><FONT face="新細明體"></FONT></td> 
	             </tr>
	        </table>
            &nbsp;
        <hr/>
	    </td>
        <td >
            <IMG id="message" style="DISPLAY: none" alt="" src="../Images/Message.JPG">	        
	    </td>
	</tr>
</table> &nbsp;</asp:Panel>
<table cellSpacing="0" cellPadding="0" border="0" width=80% align="center" >
    <tr align=left width="100%">
       <td width="100%" valign="middle"> 
           <asp:DataGrid ID="dgFPY" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CssClass="DataGridFont"
                Font-Names="Verdana">
                <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <AlternatingItemStyle BackColor="WhiteSmoke" />
                <ItemStyle BackColor="Cornsilk" ForeColor="#000066" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Station Code">
                        <ItemStyle BackColor="#006699" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_code") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_code") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Input" HeaderText="Input"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FirstPass" HeaderText="First Pass"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="FPY Rate">
                        <ItemStyle ForeColor="red" />
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FPYRate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
         </asp:DataGrid>
     </td>
	 </tr>	
</table>
