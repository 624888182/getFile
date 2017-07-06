<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_BLANKETPOquery.ascx.cs" Inherits="Boundary_B2B_BLANKETPOquery" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td width="50" rowSpan="9"><FONT face="新細明體"></FONT></td>
        <td style="HEIGHT: 18px"  width="100px"><asp:label id="lblBlanketPO" runat="server" Text="Blanket PO"></asp:label></td> 
        <td>
            <asp:TextBox ID="txtBlanketPO" runat="server"></asp:TextBox>
        </td> 
        <td width="50" ><FONT face="新細明體"></FONT></td>
        <td  align="center" rowspan="9"> 
            <asp:button id="btnSearch" runat="server" Width="100px" Text="Query" OnClick="btnSearch_Click"></asp:button> 
        </td>
    </tr>     
</table>
<hr /> 
<table   cellSpacing="0" cellPadding="0" border="0">
    <tr>
        <td width="50" rowSpan="9"><FONT face="新細明體"></FONT></td>
        <td>
            <asp:GridView ID="gvBlanketPO" runat="server"  Font-Size="10px"  AutoGenerateColumns="False"
                  BackColor="White" Font-Names="Verdana" BorderStyle="none" 
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvBlanketPO_RowCreated" OnRowDataBound="gvBlanketPO_RowDataBound"> 
                  <Columns>
                      <asp:TemplateField HeaderText="Index" > 
                         <ItemTemplate>   
                             <asp:Label ID="lblID" runat="server"></asp:Label>
                         </ItemTemplate> 
                      </asp:TemplateField> 
                    <asp:BoundField DataField="BLANKETPO" HeaderText="BlanketPO"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="FIELD1" HeaderText="Field1"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="FIELD2" HeaderText="Field2"  ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="FIELD3" HeaderText="Field3"  ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="LASTEDITBY" HeaderText="LastEditBy"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="LastEditDt"  ReadOnly="True"></asp:BoundField>  
                    <asp:BoundField DataField="REGION" HeaderText="Region"  ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="EDIID" HeaderText="EDI ID"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="GLOVIATPID" HeaderText="Gloviat PID"  ReadOnly="True"></asp:BoundField>
                  </Columns>
                <RowStyle BackColor="#f1f8f1" Height="50px" Width="80px" HorizontalAlign="center"/>
                <HeaderStyle  HorizontalAlign="Center" Height="30px" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>
        </td>
    </tr>     
</table>