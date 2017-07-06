<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="FPORPT.aspx.cs" Inherits="MainBBRYPrg_Reports_FPORPT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>
    <script type="text/javascript">
        function ChangeDataFrom() {
            switch (document.getElementById("<%=dpDataFrom.ClientID %>").value) {
                case "0":
                    document.getElementById("<%=lblTimeType.ClientID %>").innerHTML = "POCreateDate:";
                    break;
                case "1":
                    document.getElementById("<%=lblTimeType.ClientID %>").innerHTML = "POConfirmDate:";
                    break;
                case "2":
                    document.getElementById("<%=lblTimeType.ClientID %>").innerHTML = "ShipDate:";
                    break;
            }
        }
    </script>
    <style type="text/css">
   
    </style>
</head>
<body style="margin-top: 0; margin-left: 0;">
    <form id="form1" runat="server">
    <div style="height: 50px; background-color: #FF9933; text-align: left; font-weight: bold;
        font-size: xx-large;">
        BBRY B2B Web
    </div>
    <div style="height: 5px; background-color: #99ccff; margin-top: 1px; font-size: 1px;">
    </div>
    <div style="margin: 10px; margin-left: 10px;">
        ProductFamily:<asp:TextBox runat="server" ID="txtFamily" Width="100"></asp:TextBox>
        DataFrom:<asp:DropDownList runat="server" ID="dpDataFrom" onChange="ChangeDataFrom()">
            <asp:ListItem Value="0" Text="POCreateDate"></asp:ListItem>
            <asp:ListItem Value="1" Text="POConfirmDate"></asp:ListItem>
            <asp:ListItem Value="2" Text="ShipDate"></asp:ListItem>
        </asp:DropDownList>
        <asp:Label runat="server" ID="lblTimeType" Text="POCreateDate:"></asp:Label>
        <asp:TextBox runat="server" ID="txtFrom" Width="80" onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtTo" Width="80" onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;"></asp:TextBox>
        POID:
        <asp:DropDownList runat="server" ID="dpConditions1">
            <asp:ListItem Value="&gt;=" Text="&gt;="></asp:ListItem>
            <asp:ListItem Value="&gt;" Text="&gt;"></asp:ListItem>
            <asp:ListItem Value="=" Selected="True" Text="="></asp:ListItem>
            <asp:ListItem Value="&lt;" Text="&lt;"></asp:ListItem>
            <asp:ListItem Value="&lt;=" Text="&lt;="></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox runat="server" ID="txtPOIDFrom" Width="60"></asp:TextBox>
        <asp:DropDownList runat="server" ID="dpConditions2">
            <asp:ListItem Value="&gt;=" Text="&gt;="></asp:ListItem>
            <asp:ListItem Value="&gt;" Text="&gt;"></asp:ListItem>
            <asp:ListItem Value="=" Selected="True" Text="="></asp:ListItem>
            <asp:ListItem Value="&lt;" Text="&lt;"></asp:ListItem>
            <asp:ListItem Value="&lt;=" Text="&lt;="></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox runat="server" ID="txtPOIDTo" Width="60"></asp:TextBox>
        <asp:Button runat="server" ID="btnQuery" Text="Query" OnClick="btnQuery_Click" />
        <asp:Button runat="server" ID="btnExport" Text="Export" 
            onclick="btnExport_Click" />
    </div>
    <div style="width: 100%; overflow: scroll;">
        <asp:GridView ID="GridView1"  Width="1400"  runat="server" AutoGenerateColumns="False" 
            ForeColor="#333333" GridLines="Both" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound"
            OnPageIndexChanging="GridView1_PageIndexChanging" EnableModelValidation="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Product Family" DataField="PRODUCTCATEGORYID"></asp:BoundField>
                <asp:BoundField HeaderText="PRD#" DataField="PRODUCTBUYERID">
                <HeaderStyle Width="100" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PRD_Description" DataField="DESCRIPTION">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Order#" DataField="OriginID"></asp:BoundField>
                <asp:BoundField HeaderText="Order_ line//item#" DataField="OriginItemID"></asp:BoundField>
                <asp:BoundField HeaderText="BlackBerry _PO#" DataField="POID"></asp:BoundField>
                <asp:BoundField HeaderText="BlackBerry _PO_item#" DataField="ITEMID"></asp:BoundField>
                <asp:BoundField HeaderText="Customer_ Request_Date" DataField="DELIVERYSTARTDT" DataFormatString="{0:d}">
                </asp:BoundField>
                <asp:BoundField HeaderText="Total_ Order_Qty" DataField="SCHEDULEQUANTITY"></asp:BoundField>
                <asp:BoundField HeaderText="confirm qty">
                    <ItemStyle VerticalAlign="Top"  HorizontalAlign="Right"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Foxconn_ Commit_Date">
                    <ItemStyle VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Shipped_Qty">
                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="Ship_Date">
                    <ItemStyle VerticalAlign="Top" Width="100" />
                </asp:BoundField>

                       <asp:BoundField HeaderText="DNID">
                    <ItemStyle VerticalAlign="Top" />
                </asp:BoundField>
                       <asp:BoundField HeaderText="IssueDT" DataFormatString="{0:d}">
                    <ItemStyle VerticalAlign="Top" Width="100" />
                </asp:BoundField>


                <asp:BoundField HeaderText="Open_Qty">
                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right"/>
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="left" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
