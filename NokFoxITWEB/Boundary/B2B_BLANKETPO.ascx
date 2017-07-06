<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_BLANKETPO.ascx.cs" Inherits="Boundary_B2B_BLANKETPO" %>
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
            <asp:GridView ID="gvBlanketPO" runat="server"  Font-Size="10px"  AutoGenerateColumns="false" 
                  BackColor="White" Font-Names="Verdana" BorderStyle="none" 
                  GridLines="Both" PagerSettings-Visible="false"  AllowPaging="false" OnRowCreated="gvBlanketPO_RowCreated" 
                  OnRowCancelingEdit="gvBlanketPO_RowCancelingEdit"   
                  OnRowEditing="gvBlanketPO_RowEditing" OnRowUpdating="gvBlanketPO_RowUpdating" OnRowDataBound="gvBlanketPO_RowDataBound" OnRowCommand="gvBlanketPO_RowCommand" >
               <Columns>   
                    <asp:TemplateField HeaderText="Index" >
                         <HeaderTemplate>
                             <asp:LinkButton ID="lkbAddItem" runat="server" CommandName="AddItem">新增</asp:LinkButton>
                         </HeaderTemplate>
                         <ItemTemplate>   
                             <asp:Label ID="lblID" runat="server"></asp:Label>
                         </ItemTemplate>
                         <FooterTemplate>                            
                            <asp:LinkButton  ID="btnCommit" runat="server" Text="確定" CommandName="ItemSure"/>
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="取消" CommandName="ItemCancel"/>
                         </FooterTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Blanket PO" >
                         <ItemTemplate>  
                             <asp:Label ID="lblBlanketPO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLANKETPO") %>'></asp:Label>
                            
                         </ItemTemplate>
                         <EditItemTemplate> 
                             <asp:Label ID="lblEBlanketPO" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.BLANKETPO") %>'></asp:Label>  
                             <asp:TextBox ID="txtEBlanketPO" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLANKETPO") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <FooterTemplate>
                             <asp:TextBox ID="txtFblanketpo" runat="server" Text=""></asp:TextBox>
                         </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FIELD1" HeaderText="Field1"  ReadOnly="True"></asp:BoundField> 
                    <asp:BoundField DataField="FIELD2" HeaderText="Field2"  ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="FIELD3" HeaderText="Field3"  ReadOnly="True"></asp:BoundField>
                    <asp:TemplateField HeaderText="Last Edit By">
                        <ItemTemplate>
                            <asp:Label ID="lbllasteditby" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTEDITBY") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblElasteditby" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.LASTEDITBY") %>'></asp:Label>  
                             <asp:TextBox ID="txtElasteditby" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTEDITBY") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFlasteditby" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LASTEDITDT" HeaderText="Last Edit Date"  ReadOnly="True"></asp:BoundField>
                    <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblregion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEregion" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:Label>  
                             <asp:TextBox ID="txtEregion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REGION") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFregion" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDI ID">
                        <ItemTemplate>
                            <asp:Label ID="lblediid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EDIID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEediid" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.EDIID") %>'></asp:Label>  
                             <asp:TextBox ID="txtEediid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EDIID") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFediid" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gloviat PID">
                        <ItemTemplate>
                            <asp:Label ID="lblgloviatpid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLOVIATPID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Label ID="lblEgloviatpid" runat="server" Visible="false"  Text='<%# DataBinder.Eval(Container, "DataItem.GLOVIATPID") %>'></asp:Label>  
                             <asp:TextBox ID="txtEgloviatpid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLOVIATPID") %>'></asp:TextBox>                         
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFgloviatpid" runat="server" Text=""></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True"  HeaderText="Edit"  /> 
                </Columns>
                <RowStyle BackColor="#f1f8f1" Height="50px" Width="80px" HorizontalAlign="center"/>
                <HeaderStyle  HorizontalAlign="Center" Height="30px" />
                <AlternatingRowStyle BackColor="White" />        
                <PagerSettings Visible="False" />      
            </asp:GridView>
        </td>
    </tr>     
</table>
