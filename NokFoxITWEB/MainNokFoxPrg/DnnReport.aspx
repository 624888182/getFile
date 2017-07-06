<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DnnReport.aspx.cs" Inherits="MainMSPrg_DnnReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script type="text/javascript" src="../Jscript/DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

    <title></title>
        <style type="text/css">
            .auto-style1 {
                width: 480px;
            }
        </style>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <table align="center";style="border-collapse:collapse;" border="1" style="width: 491px" >
    <tr>
    <td class="style2">PickDate From :</td>
    <td class="style3">
        <asp:TextBox ID="textFrom" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textFrom'),dateFmt:'yyyyMMdd '})" onkeypress="javascript:event.returnValue=false;" Width="200px"  AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td class="style2">PickDate End :</td>
    <td class="style3">
            <asp:TextBox ID="textEnd" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textEnd'),dateFmt:'yyyyMMdd'})" onkeypress="javascript:event.returnValue=false;" Width="196px"  AutoPostBack="True"></asp:TextBox>
                    </td>
    </tr>
   <tr>
    <td class="style2">HMD DNN :</td>
    <td class="style3">
        <asp:TextBox ID="textDnn" runat="server"  
                    ForeColor="Black"
                   Width="200px"></asp:TextBox>
                    </td>
    </tr>

       <tr>
    <td class="style2">Shipping Ponit :</td>
    <td class="style3">
        <asp:TextBox ID="textShipping" runat="server"  
                    ForeColor="Black"
                     Width="200px"></asp:TextBox>
                    </td>
    </tr>
 
    </table>
         <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="auto-style1">
        <asp:Button ID="Button1" runat="server" Text="Query" BackColor="#FFCC66" 
            Width="84px" Height="22px" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
             Width="84px" Height="22px" OnClick="Button2_Click"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--        <asp:Button ID="Button3" runat="server" Text="KeyData" BackColor="#FFCC66" 
            Height="22px"/>--%>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

<%--        <asp:Button ID="ButtonExcel" runat="server" Text="ExcelDownload" 
                            OnClick="ButtonExcel_Click" Width="170px" />--%>
        </td>
    </tr>
    </table>


        <asp:Label ID="Label19" runat="server" Text="Display Header"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" AutoGenerateColumns="False"
                BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                Width="100px" DataKeyNames="" 
                EnableModelValidation="True" AllowPaging="True" PageSize="15">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <Columns> 
                    <asp:TemplateField HeaderText="Modify">
                        <ItemTemplate>
                            <asp:Button ID="btnModify" runat="server" Text="Modify" BackColor="#FFCC00" OnClick="Modify_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" BackColor="#FFCC00" OnClick="Confirm_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>


                    <asp:TemplateField HeaderText="Nokia DNN">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Label3" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"DNID") %>' OnClick="lbtDnn_Click">LinkButton</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                    <%--<asp:BoundField DataField="DNID" HeaderText="Nokia DNN" />--%>
                    <asp:BoundField DataField="IssueDT" HeaderText="PickDate" />
                    <asp:BoundField DataField="ArrivalDT" HeaderText="DeliveryDate" />             
                    <asp:BoundField DataField="UF1" HeaderText="Foxconn Dn" />
                    <asp:BoundField DataField="IncotermsCode" HeaderText="IncotermsCode" />
                    <asp:BoundField DataField="ShipmentModeCode" HeaderText="ShipmentModeCode" />
                    <asp:BoundField DataField="CONFIRMFLAG" HeaderText="CONFIRMFLAG" />          
                    <asp:BoundField DataField="UF1" HeaderText="SEND3B2FLAG" />
                    <asp:BoundField DataField="UF1" HeaderText="SEND3B2Time" />
                    <asp:BoundField DataField="UF1" HeaderText="SEND3B2Log" />
                    <asp:BoundField DataField="UF1" HeaderText="RCOUNT" />
                    <asp:BoundField DataField="UF1" HeaderText="ackflag" />
                    <asp:BoundField DataField="UF1" HeaderText="ack_time" />

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <%-- <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />    Text='<%#DataBinder.Eval(Container.DataItem,"routing") %>'--%>
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
            </asp:GridView>


        <div id="divGridView3" runat="server">
            &nbsp;</div>



        <div id="div1" runat="server" align="left">
        <fieldset>
                <legend>DNNDetail</legend>
                <asp:Label ID="Label2" runat="server" ForeColor ="#ff0000"></asp:Label>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView4_PageIndexChanging" OnRowDataBound="GridView4_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>

                        <asp:TemplateField HeaderText="Save">
                        <ItemTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#FFCC00" OnClick="Save_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>

                                <asp:BoundField DataField="DNID" HeaderText="DNNID" />
                                <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                <asp:TemplateField HeaderText="PO">
                                    <ItemTemplate>
                                         <asp:TextBox ID="textpoid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"poid") %>'
                                Width="80px" Height="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="POItem">
                                    <ItemTemplate>
                                         <asp:TextBox ID="textpoitem" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"poitemid") %>'
                                 Width="80px" Height="20px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductRecipientID" HeaderText="Customer PN" />
                                <asp:BoundField DataField="PNDES" HeaderText="description" />
                                <asp:BoundField DataField="Total_QTY" HeaderText="QTY" />
                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                <asp:BoundField DataField="UF1" HeaderText="FOX PN" />
                                <asp:BoundField DataField="UF1" HeaderText="Customer sales order" />
                                <asp:BoundField DataField="CUORDERITEM" HeaderText="Customer sales orde item" />
                                <asp:BoundField DataField="CUDNN" HeaderText="Customer delivery Number" />
                                <asp:BoundField DataField="CUDNNITEM" HeaderText="Customer delivery item" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
            </fieldset>
            </div>

        <div id="div2" runat="server" align="left">
            <fieldset>
                <legend>DNNSoldToAddress</legend>
                <asp:Label ID="lblDetail" runat="server"></asp:Label>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                                 <asp:BoundField DataField="NumberDefaultIndicator" HeaderText="Company code" />
<%--                                 <asp:BoundField DataField="Address_UF1" HeaderText="company" />--%>
                                <asp:BoundField DataField="GivenName" HeaderText="GivenName" />
                                <asp:BoundField DataField="CityName" HeaderText="CityName" />
                                <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" />
                                <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" />
                                <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" />
                                
                                <asp:BoundField DataField="StreetName" HeaderText="StreetName" />
                                <asp:BoundField DataField="CareOfName" HeaderText="CareOfName" />
                                <asp:BoundField DataField="LanguageCode" HeaderText="LanguageCode" />
                                <asp:BoundField DataField="AreaID" HeaderText="AreaID" />
                                
                                <asp:BoundField DataField="ID" HeaderText="SPointcode" />
                                
                                <asp:BoundField DataField="ID" HeaderText="contactName" />
                                <asp:BoundField DataField="ID" HeaderText="FAXnumber" />
                                <asp:BoundField DataField="ID" HeaderText="telnumber" />
                                <asp:BoundField DataField="URIDefaultIndicator" HeaderText="URIDefaultIndicator" />
                                <asp:BoundField DataField="URIUsageDenialIndicator" HeaderText="URIUsageDenialIndicator" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
            </fieldset>
         </div>


          <div id="div3" runat="server" align="left">
          <fieldset>
                <legend>DNNShipToAddress </legend>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView3_PageIndexChanging" OnRowDataBound="GridView3_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                                <asp:BoundField DataField="NumberDefaultIndicator" HeaderText="Company code" />
                                 <asp:BoundField DataField="company" HeaderText="company" />
                                <asp:BoundField DataField="GivenName" HeaderText="GivenName" />
                                <asp:BoundField DataField="CityName" HeaderText="CityName" />
                                <asp:BoundField DataField="CountryCode" HeaderText="CountryCode" />
                                <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" />
                                <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" />
                                
                                <asp:BoundField DataField="StreetName" HeaderText="StreetName" />
                                <asp:BoundField DataField="CareOfName" HeaderText="CareOfName" />
                                <asp:BoundField DataField="LanguageCode" HeaderText="LanguageCode" />
                                <asp:BoundField DataField="AreaID" HeaderText="AreaID" />
                                
                                <asp:BoundField DataField="ID" HeaderText="SPointcode" />
                                
                                <asp:BoundField DataField="ID" HeaderText="contactName" />
                                <asp:BoundField DataField="ID" HeaderText="FAXnumber" />
                                <asp:BoundField DataField="ID" HeaderText="telnumber" />
                                <asp:BoundField DataField="URIDefaultIndicator" HeaderText="URIDefaultIndicator" />
                                <asp:BoundField DataField="URIUsageDenialIndicator" HeaderText="URIUsageDenialIndicator" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
            </fieldset>
           
        </div>
    </form>
</body>
</html>
