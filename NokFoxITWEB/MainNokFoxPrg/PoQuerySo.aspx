﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoQuerySo.aspx.cs" Inherits="MainNokFoxPrg_PoQuerySo" %>

<!DOCTYPE html>
        <script type="text/javascript" src="../Jscript/DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 484px;
        }
    </style>
</head>
<body>

     <script type="text/javascript">
         function test() {
             var buttonList = document.getElementById("buttonList").value;
             var buttonArray = buttonList.split(',');
             for (var i = 0; i < buttonArray.length - 1; i++) {
                 var buttonID = buttonArray[i];
                 var b = document.getElementById(buttonID);
                 b.disabled = true;
             }
             var a = document.getElementById("Button1");
             a.disabled = true;
             var c = document.getElementById("Button2");
             c.disabled = true;
             var d = document.getElementById("Button3");
             d.disabled = true;
         }
     </script>



    <form id="form1" runat="server">
    <div>
    <table align="center";style="border-collapse:collapse;" border="1" style="width: 491px" >
    <tr>
    <td class="style2">Receive date From :</td>
    <td class="style3">
        <asp:TextBox ID="textFrom" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textFrom'),dateFmt:'yyyy-MM-dd '})" onkeypress="javascript:event.returnValue=false;" Width="200px"  AutoPostBack="True"></asp:TextBox>
        &nbsp;<asp:TextBox ID="textVendID" runat="server"  ForeColor="Black" 
            onClick="WdatePicker({el:$dp.$('textFrom'),dateFmt:'yyyy-MM-dd '})" 
            onkeypress="javascript:event.returnValue=false;" Width="2px"  
            AutoPostBack="True" Visible="False"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="style2">Receive date End :</td>
    <td class="style3">
            <asp:TextBox ID="textEnd" runat="server"  ForeColor="Black" onClick="WdatePicker({el:$dp.$('textEnd'),dateFmt:'yyyy-MM-dd'})" onkeypress="javascript:event.returnValue=false;" Width="196px"  AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtConn" runat="server" Visible="False" Width="16px"></asp:TextBox>
                    </td>
    </tr>
   <tr>
    <td class="style2">HMD PO :</td>
    <td class="style3">
        <asp:TextBox ID="textpo" runat="server"  
                    ForeColor="Black"
                   Width="200px"></asp:TextBox>
                    <asp:TextBox ID="txtt3" runat="server" Visible="False" Width="16px"></asp:TextBox>
                    </td>
    </tr>

     <%--  <tr>
    <td class="style2">Loaction Code :</td>
    <td class="style3">
        <asp:TextBox ID="textCode" runat="server"  
                    ForeColor="Black"
                     Width="200px"></asp:TextBox>
                    </td>
    </tr>--%>

    <tr>
    <td class="style2">Customer PN:</td>
    <td class="style3">
        <asp:TextBox ID="sptext" runat="server"  
                    ForeColor="Black"
                     Width="200px"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2">Create DN :</td>
    <td class="style3">
        <asp:DropDownList ID="DropDownListdn" runat="server">
            <asp:ListItem>ALL</asp:ListItem>
            <asp:ListItem>YES</asp:ListItem>
            <asp:ListItem>NO</asp:ListItem>
        </asp:DropDownList>
    </td>
    </tr>
    </table>
        <table align="center";style="border-collapse:collapse;" border="1" >
    <tr>
    <td class="auto-style1">
        <asp:Button ID="Button1" runat="server" Text="Query" BackColor="#FFCC66" 
            Width="84px" Height="22px" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" BackColor="#FFCC66" 
             Width="84px" Height="22px" OnClick="Button2_Click"/>

          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <%--        <asp:Button ID="ButtonExcel" runat="server" Text="ExcelDownload" 
                            OnClick="ButtonExcel_Click" Width="170px" />--%>
         
        <asp:Button ID="Button3" runat="server" Text="Download" BackColor="#FFCC66" 
            onclick="Download_Click" Height="21px" Width="90px"/>

        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Batch Create SO/DN" BackColor="#FFCC66" 
            onclick="CreateallPO_Click" Height="21px" Width="135px"   OnClientClick="test();  "/>
         
        </td>
        
    </tr>
    </table>
 <div id="div1" runat="server" align="left">
        <fieldset>
                <legend>poheader</legend>
                            <asp:Label ID="sostatus" runat="server" ForeColor ="#ff0000"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" BackColor="White" AutoGenerateColumns="False"
                BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                Width="100px" DataKeyNames="" 
                EnableModelValidation="True" AllowPaging="True" PageSize="7">
                <RowStyle BackColor="White" ForeColor="#333333" />
                <Columns> 
                    <%--<asp:TemplateField HeaderText="Modify">
                        <ItemTemplate>
                            <asp:Button ID="btnModify" runat="server" Text="Modify" BackColor="#FFCC00" OnClick="Modify_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm"  OnClick="Confirm_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" Enabled = "false" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>


                    <asp:TemplateField HeaderText="Nokia PO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Label3" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        Text='<%#DataBinder.Eval(Container.DataItem,"POID") %>' OnClick="lbtPO_Click">LinkButton</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                    <%--<asp:BoundField DataField="DNID" HeaderText="Nokia DNN" />--%>
                    <asp:BoundField DataField="USERCONFIRMFLAG" HeaderText="CONFIRMFLAG" />
                    <asp:BoundField DataField="TRANSMISSIONINDICATOR" HeaderText="order type" />             
                    <asp:BoundField DataField="po_create_mt_UF5" HeaderText="incoterms" />
                    <asp:BoundField DataField="po_create_mt_UF4" HeaderText="Payment term" />
                    <asp:BoundField DataField="SellerPartyID" HeaderText="Location code" />
                    <asp:BoundField DataField="SOID" HeaderText="SO" />
                    <asp:BoundField DataField="SAP_LOG" HeaderText="sap log" />
                    <asp:BoundField DataField="CONFIRMFLAG" HeaderText="ShipmentModeCode" />          
                    <asp:BoundField DataField="PO_Create_MT_UF9" HeaderText="SEND3B2FLAG" />
                    <asp:BoundField DataField="PO_Create_MT_UF10" HeaderText="SEND3B2Time" />
                    <%--<asp:BoundField DataField="PO_CREATE_MT_UF8" HeaderText="SEND3B2Log" />
                    <asp:BoundField DataField="PO_CREATE_MT_UF8" HeaderText="RCOUNT" />
                    <asp:BoundField DataField="PO_CREATE_MT_UF8" HeaderText="ackflag" />
                    <asp:BoundField DataField="PO_CREATE_MT_UF8" HeaderText="ack_time" />--%>



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
               </fieldset>
        </div>

         <div id="div2" runat="server" align="left">
        <fieldset>
                <legend>podetail</legend>
                <asp:Label ID="Label2" runat="server" ForeColor ="#ff0000"></asp:Label>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView4_PageIndexChanging" OnRowDataBound="GridView4_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>

                       <%-- <asp:TemplateField HeaderText="Save">
                        <ItemTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#FFCC00" OnClick="Save_Click"
                                BorderColor="White" Font-Bold="True" ForeColor="#3366FF" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'

                                CausesValidation="False" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                       </asp:TemplateField>--%>

                                <asp:BoundField DataField="poid" HeaderText="poid" />
                                <asp:BoundField DataField="ItemID" HeaderText="poitemid" />
                                <asp:BoundField DataField="SOID" HeaderText="SO" />
                                <asp:BoundField DataField="DNID" HeaderText="DN" />
                                <asp:BoundField DataField="InternalID" HeaderText="Customer PN" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="description" />
                                <asp:BoundField DataField="BASEQTY" HeaderText="QTY" />
                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                <asp:BoundField DataField="DeliveryStartDT" HeaderText="pick up date" />
                                <asp:BoundField DataField="PO_Create_DT_UF1" HeaderText="delivery date" />
                                <asp:BoundField DataField="CostAmount" HeaderText="unit price" />
                                <asp:BoundField DataField="CostCurrencyCode" HeaderText="Currency" />
                                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                                <asp:BoundField DataField="F6" HeaderText="ackflag" />
                                <asp:BoundField DataField="FWCODE" HeaderText="Carrier" />
                                <asp:BoundField DataField="OriginID" HeaderText="TNS PO" />
                                <asp:BoundField DataField="OriginalSOID" HeaderText="TNS SO" />
                                <asp:BoundField DataField="Orincoterms" HeaderText="Terms" />
                                <asp:BoundField DataField="Orincoterms2" HeaderText="term description" />

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
                <legend>POshipToAddress</legend>
                <asp:Label ID="lblDetail" runat="server"></asp:Label>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDataBound="GridView2_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                               <asp:BoundField DataField="NumberDefaultIndicator" HeaderText="Company code" />
                               <asp:BoundField DataField="Address_UF2" HeaderText="Name" />
                               <asp:BoundField DataField="Address_UF3" HeaderText="Address Line 1" />
                               <asp:BoundField DataField="Address_UF4" HeaderText="Address Line 2" />
                               <asp:BoundField DataField="CareOfName" HeaderText="FloorOriginal" /> 
                               <asp:BoundField DataField="StreetName" HeaderText="HouseNumber" /> 
                               <asp:BoundField DataField="GivenName" HeaderText="Street" />  
                               <asp:BoundField DataField="Address_UF5" HeaderText="Address Line 4" />
                               <asp:BoundField DataField="Address_UF6" HeaderText="Address Line 5" />
                               <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" />
                               <asp:BoundField DataField="CityName" HeaderText="CityName" />
                               <asp:BoundField DataField="Address_UF1" HeaderText="State" />   
                               <asp:BoundField DataField="CountryCode" HeaderText="Country" />  
                               <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" />  


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

          <div id="div4" runat="server" align="left">
            <fieldset>
                <legend>POSoldToAddress</legend>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView3_PageIndexChanging" OnRowDataBound="GridView3_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                               <asp:BoundField DataField="NumberDefaultIndicator" HeaderText="Company code" />
                               <asp:BoundField DataField="Address_UF2" HeaderText="Name" />
                               <asp:BoundField DataField="Address_UF3" HeaderText="Address Line 1" />
                               <asp:BoundField DataField="Address_UF4" HeaderText="Address Line 2" />
                               <asp:BoundField DataField="CareOfName" HeaderText="FloorOriginal" /> 
                               <asp:BoundField DataField="StreetName" HeaderText="HouseNumber" /> 
                               <asp:BoundField DataField="GivenName" HeaderText="Street" />  
                               <asp:BoundField DataField="Address_UF5" HeaderText="Address Line 4" />
                               <asp:BoundField DataField="Address_UF6" HeaderText="Address Line 5" />
                               <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" />
                               <asp:BoundField DataField="CityName" HeaderText="CityName" />
                               <asp:BoundField DataField="Address_UF1" HeaderText="State" />   
                               <asp:BoundField DataField="CountryCode" HeaderText="Country" /> 
                               <asp:BoundField DataField="RegionCode" HeaderText="RegionCode" />    
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


        <div id="div5" runat="server" align="left">
            <fieldset>
                <legend>Letter of Credit</legend>
                <asp:Label ID="Label4" runat="server"></asp:Label>
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        OnPageIndexChanging="GridView5_PageIndexChanging" OnRowDataBound="GridView5_RowDataBound"
                        PageSize="7" AllowPaging="True" Width="100px">
                        <RowStyle BackColor="White" ForeColor="#333333" Font-Size="Smaller" />
                        <Columns>
                                <asp:BoundField DataField="ContextText" HeaderText="Letter of Credit code" />
                                
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
         <asp:HiddenField ID="buttonList" runat="server" />

    </form>
</body>
</html>

