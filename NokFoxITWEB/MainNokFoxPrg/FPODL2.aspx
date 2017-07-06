<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPODL2.aspx.cs" Inherits="MainBBRYPrg_FPODL2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Excel Download</title>
    <style type="text/css">
        .style1
        {
            width: 550px;
            font-weight: bold;
            text-align: center;
        }
        .style2
        {
            width: 200px;
            text-align: center;
        }
        .style3
        {
            width: 344px;
        }
        .auto-style1 {
            width: 550px;
            font-weight: bold;
            text-align: center;
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div>
    <center><h1><b><font color ="#cc6600" style="font-family: 標楷體">3A4&nbsp;Query</font></b></h1></center>
    </div>
    <div style="height: 180px">
   
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style2">MSFT_PO:</td>
    <td class="style3">
        <asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">BeginDate:</td>
    <td class="style3">
        <asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                    </td>
    </tr>
   <tr>
    <td class="style2">EndDate:</td>
    <td class="style3">
        <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="200px"></asp:TextBox>
                    </td>
    </tr>
 
    </table>
    <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="style1">
        <asp:Button ID="Button1" runat="server" Text="Select" BackColor="#FFCC66" 
            onclick="Button1_Click" Width="84px" Height="22px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
            onclick="Button2_Click" Width="84px" Height="22px"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Download" BackColor="#FFCC66" 
            onclick="Button3_Click" Height="22px"/>
        </td>
    </tr>
    </table>
    <table align="center">
    <tr>
    <td class="style1">
        <asp:Label ID="Label0" runat="server" style="color: #FF0000" Text="Label" 
            Width="300px"></asp:Label>
        </td>
    </tr>
    </table>
    </div>
        <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
    <div id="GridView11" runat="server" style="width: 100%; overflow:scroll; margin-top: 10px;">      
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="datagrid gridWidth"
                Height="114px" Width="1600px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowDataBound="GridView1_RowDataBound" PageSize="20"   OnRowDeleting="Gridview_RowDeleting"
                HeaderStyle-CssClass="gvHeader" EnableModelValidation="True" HeaderStyle-Wrap="False">
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="Smaller" />
                <Columns>      
                     <asp:TemplateField>
                    <HeaderStyle Width="25px" />
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/arrow.gif" />
                    </ItemTemplate>
                </asp:TemplateField>     
                     <asp:TemplateField HeaderText="ID" Visible="False">
                        <ItemTemplate>
                             <asp:Label ID="Labelid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>        
                    <asp:TemplateField HeaderText="NO">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MSFT DUNS">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT DUNS") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="MSFT PO">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MSFT PO ITEM">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT PO ITEM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MSFT P/N">
                        <ItemTemplate>
                            <asp:Label ID="lblPOCNT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MSFT P/N") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CREATIONDT">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO Price Base Unit">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PO Price Base Unit") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText=" Oreder QTY">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Oreder QTY") %>'></asp:Label>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    
                    
                      <asp:TemplateField HeaderText="MRP_Price">
                        <ItemTemplate>
                            <asp:Label ID="Label8a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MRP_Price") %>'></asp:Label>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="MRP_PricingUnit">
                        <ItemTemplate>
                            <asp:Label ID="Label8b" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MRP_PricingUnit") %>'></asp:Label>

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Pick Up Date">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Pick Up Date") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ShipToAdress">
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShipToAdress") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField> 
                    <%-- <asp:TemplateField HeaderText="Payment Term">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Payment Term") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField> --%>
                     <asp:TemplateField HeaderText="Incoterm">
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Incoterm") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="Payment">
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Payment") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="ShipTo DUNS Code">
                        <ItemTemplate>
                            <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShipTo DUNS Code") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ShipToCode">
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShipToCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Request Delivery Date">
                        <ItemTemplate>
                            <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Request Delivery Date") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Storage Location">
                        <ItemTemplate>
                            <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Storage Location") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="PurchaseGroup">
                        <ItemTemplate>
                            <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PurchaseGroup") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DateTimeStamp">
                        <ItemTemplate>
                            <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"DateTimeStamp") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="CONFIRMFLAG">
                        <ItemTemplate>
                            <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                     
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />


                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" 
                        Font-Size="Smaller" />
                    <HeaderStyle BackColor="#CC6600" BorderColor="Black" Font-Bold="True" 
                        ForeColor="White" Font-Size="Smaller" />
                 <PagerTemplate>
                        The current page:
                        <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                        Pp/Total:
                        <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                        Page
                        <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                            Enabled='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>FirstPage</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                            CommandName="Page" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>The Previous</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Next</asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>The Last</asp:LinkButton>
                        Turn to
                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />Page
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                            CommandName="Page" Text="GO" />
                    </PagerTemplate>
                    <AlternatingRowStyle Font-Size="Smaller" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
