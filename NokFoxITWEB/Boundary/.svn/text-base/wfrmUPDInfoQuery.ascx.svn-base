<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmUPDInfoQuery.ascx.cs" Inherits="Boundary_wfrmUPDInfoQuery" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc3" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 

<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
	<tr height="15">
		<td width="50" style="height: 15px"></td>
		<td Width="100px" style="height: 15px"> <asp:label id="Label1" runat="server" Text="Invoice NO" ></asp:label></td>
		<td style="WIDTH: 150px; height: 15px;">
            <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox> 
        </td>   
        <td Width="100px" style="height: 15px"> <asp:label id="Label3" runat="server" Text="Carton NO" ></asp:label></td>
		<td style="WIDTH: 150px; height: 15px;">
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                  Width="150px"></asp:DropDownList>
        </td>  
        <td Width="150px"  align="right"  valign="bottom" style="height: 15px"> 
			<asp:button id="Button1" runat="server" Width="80px" OnClick="Button1_Click" ></asp:button></td> 	   
	</tr>  
</table>   
<hr>    
    <asp:Label ID="lbl1" runat="server">UPD_DATALOAD_DETAIL_T信息</asp:Label>
    <asp:GridView ID="GridView1" runat="server"  BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView> 
<br />  
    <asp:Label ID="lbl2" runat="server">UPD_INVOICE_HAND_TEMP信息</asp:Label>
    <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView> 
<br />  
    <asp:Label ID="lbl3" runat="server">CMCS_SFC_PACKING_LINES_ALL信息</asp:Label>
    <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView>  
<br />  
<asp:Label ID="lbl4" runat="server">SHIP_CARTON_MAP信息</asp:Label>
<div style="height:200px; overflow:auto;" id="div4" runat="server"> 
    <asp:GridView ID="GridView4" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="DataGridFixedHeader"/>
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView> 
    </div>
<br /> 
<asp:Label ID="lbl5" runat="server">SHIPPING DATA中每箱的數量信息</asp:Label>
<div style="height:200px; overflow:auto;" id="div5" runat="server"> 
    <asp:GridView ID="GridView5" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="DataGridFixedHeader"/>
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView> 
</div>
<br /> 
<asp:Label ID="lbl6" runat="server">SHIPPING_DATA信息</asp:Label>
<div style="height:200px; overflow:auto;" id="div6">  
    <asp:GridView ID="GridView6" runat="server" BackColor="White" BorderColor="#CCCCCC"  Font-Size="10px"  
         Font-Names="Verdana" CssClass="DataGridFont"  AllowPaging="false"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="true">
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <PagerStyle  HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#cccc99" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="SteelBlue" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="DataGridFixedHeader"/>
        <RowStyle ForeColor="#000066" BackColor="Cornsilk" Wrap="false"/>
        <AlternatingRowStyle BackColor="WhiteSmoke" />
    </asp:GridView>  
</div>
