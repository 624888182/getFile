<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FDNQ01.aspx.cs" Inherits="MainBBRYPrg_FDNQ01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script language = "javascript" src ="../Jscript/Calendar.js"   ></script>
    <script language =javascript >
        function selectAll(spanChk)
        {
            
            var ob = spanChk;
            var xState = ob.checked;
            var elem = ob.form.elements;
            for(i=0;i<elem.length;i++)
            {
                if(elem[i].type=="checkbox" && elem[i].id!=ob.id)
                {
                    if(elem[i].checked!=xState)
                        elem[i].click();
                }
            }

        }
        function showD()
        {
            if(confirm('你确认要删除吗？'))
            {
                document.getElementById('Hidden1').value = '1';
            }
        }
        function clickOk()
        {
            if(event.keyCode == 13)
                document.getElementById('ButtonOk').click();
        }
        
        function getTime()
        {
            document.getElementById('Button2').click();
        }
    </script>
    <style type = "text/css">
        td
        {
        	border-color : Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center><h1><b><font color ="#0099ff" style="font-family: 標楷體">DN&nbsp;Confirmation</font></b></h1></center>
    </div>
    <div>
        <table width = "100%" border = 2px cellspacing = 1px>
            <tr>
                <td width = 100px>
                    <font color ="#0066ff" ><b>起始時間：</b></font>
                </td>
                <td width = "120px">
                    <asp:TextBox ID = "BegTime" runat = "server" onkeyprass = "javascript:event.returnValue = false;" onclick = "showCalendar();"></asp:TextBox>
                </td>
                <td width = 100px>
                    <font color ="#0066ff" ><b>結束時間：</b></font>
                </td>
                <td width = 120px>
                    <asp:TextBox ID = "EndTime" runat = "server" onkeyprass = "javascript:event.returnValue = false;" onclick = "showCalendar();"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                    <%--<asp:Button ID="Button2" runat="server" Text="Button" style ="display:none" 
                        onclick="Button2_Click" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <font color ="#0066ff" ><b>DN NUM:</b></font>
                </td>
                <td>
                    <asp:TextBox ID = "dnNum" runat =server onkeydown = "javascript:clickOk();"></asp:TextBox> 
                </td>
                <td colspan = 2>
                    &nbsp;
                </td>
                <td>
                        <asp:ImageButton ID="ButtonOk" runat="server" ImageUrl="~/Images/search.gif" 
                            onclick="ButtonOk_Click" />
                        <input id="Hidden1" type="hidden" runat =server />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <br />
    </div>
    <div style =" overflow:scroll; width:100%"  id = "gv1" runat ="server">
        <font><b>選擇完日期后請按回車確定</b></font>
        <asp:GridView ID="GridView1" runat="server" Width = "112%" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
            BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            GridLines="Horizontal" ondatabound="GridView1_DataBound" 
             >
            <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
                <%--<asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID = "ChooseAll" runat =server Text="全选/取消" ToolTip="按一次全选，再按一次取消全选" onclick = "selectAll(this);"  />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID = "Choose" runat =server /> 
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="ArrivalDT">
                    <ItemTemplate>
                        <asp:TextBox ID="ArrivalDTText" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WayBillID">
                    <ItemTemplate>
                        <asp:TextBox ID="WayBillText" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="DNID" HeaderText="DNID" ReadOnly="True" />
                <%--<asp:BoundField DataField="ArrivalDT" HeaderText="ArrivalDT" />
                <asp:BoundField DataField="WayBillID" HeaderText="WayBillID" />
                <asp:BoundField DataField="ProductRecipientID" 
                    HeaderText="ProductRecipientID" />--%>
                    <asp:TemplateField HeaderText="IssueDT">
                    <ItemTemplate>
                        <asp:TextBox ID = "issueDt" runat =server onclick = "showCalendar();" 
                             AutoPostBack="True" 
                            ontextchanged="issueDt_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ARRIVALDT">
                    <ItemTemplate>
                        <asp:TextBox ID = "arrivalDt" runat =server onclick = "showCalendar();" 
                             AutoPostBack="True" 
                            ontextchanged="arrivalDt_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BOL">
                    <ItemTemplate>
                        <asp:TextBox ID = "wayBillId" runat =server></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SCAC">
                    <ItemTemplate>
                        <asp:TextBox ID = "productrecipientId" runat =server></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SendFlag" HeaderText="SendFalg" ReadOnly="True" />
                <asp:BoundField DataField="CONFIRMFLAG" HeaderText="CONFIRMFLAG" />
                <%--<asp:BoundField DataField="SFCFlag" HeaderText="SFCFlag" />
                <asp:TemplateField HeaderText="SFCLog">
                            <ItemTemplate>
                                <asp:Label ID="Label4a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Delivery_Notification_MT_UF10") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ButtonDet" runat="server" Text="詳細資料" BorderStyle="Groove" CommandArgument="<%# Container.DataItemIndex%>"
                            onclick="ButtonDet_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ButtonConf" runat="server" Text="CONFIRM" BorderStyle="Groove" 
                            CommandArgument="<%# Container.DataItemIndex%>" onclick="ButtonConf_Click"
                             />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ButtonClear" runat="server" Text="清除" 
                            CommandArgument="<%# Container.DataItemIndex%>" 
                            onclick="ButtonClear_Click" onclientclick="showD();" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="ButtonUp" runat="server" Text="更新" 
                            CommandArgument="<%# Container.DataItemIndex%>" onclick="ButtonUp_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField ShowEditButton="True" CancelText="取消" EditText="編輯" 
                    UpdateText="更新" />--%>
                <asp:BoundField DataField="CreationDT" HeaderText="CreationDT" 
                    ReadOnly="True" />
                <asp:BoundField DataField="SendTime" HeaderText="SendTime" ReadOnly="True" />
                <asp:BoundField DataField="GrossUnitCode" HeaderText="GrossUnitCode" 
                    ReadOnly="True" />
                <asp:BoundField DataField="GrossWeight" HeaderText="GrossWeight" 
                    ReadOnly="True" />
                <asp:BoundField DataField="NetUnitCode" HeaderText="NetUnitCode" 
                    ReadOnly="True" />
                <asp:BoundField DataField="NetWeight" HeaderText="NetWeight" ReadOnly="True" />
                <asp:BoundField DataField="BuyerPartyInternalID" 
                    HeaderText="BuyerPartyInternalID" ReadOnly="True" />
                <asp:BoundField DataField="BuyerPartyVendorID" 
                    HeaderText="BuyerPartyVendorID" ReadOnly="True" />
                <asp:BoundField DataField="VendorPartyVendorID" 
                    HeaderText="VendorPartyVendorID" ReadOnly="True" />
                <asp:BoundField DataField="ItemQTY" HeaderText="ItemQTY" ReadOnly="True" />
                <asp:BoundField DataField="HUQTY" HeaderText="HUQTY" ReadOnly="True" />
                <asp:BoundField DataField="BoxQTY" HeaderText="BoxQTY" ReadOnly="True" />
                <asp:BoundField DataField="SerialIDQTY" HeaderText="QTY" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div style ="overflow:scroll; width:100%; height: 1271px;" id = "gv2" 
        visible =false runat =server  >
       <asp:Button ID = "Button1" Text = "返回" runat =server onclick="Button1_Click" />
        <br/>
        <br/>
        <asp:Label ID="Label1" runat="server" Text="1.Delivery_Map_DN:Count" 
            Width="174px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="" Width="40px"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" BackColor="White"   
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            GridLines="Horizontal" AllowPaging="True" 
            onpageindexchanging="GridView2_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br/>
        <br/>
        <asp:Label ID="Label3" runat="server" Text="2.Delivery_Notification_DT:Count" 
            Width="210px"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="" Width="40px"></asp:Label>
        <asp:GridView ID="GridView3" runat="server" BackColor="White"   
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            GridLines="Horizontal" AllowPaging="True" 
            onpageindexchanging="GridView3_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br/>
        <br/>
        <asp:Label ID="Label5" runat="server" Text="3.Delivery_Notification_HU:Count" 
            Width="210px"></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="" Width="40px"></asp:Label>
        <asp:GridView ID="GridView4" runat="server" BackColor="White"   
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            GridLines="Horizontal" AllowPaging="True" 
            onpageindexchanging="GridView4_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#333333" />
            <Columns>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    
    </form>
</body>
</html>
