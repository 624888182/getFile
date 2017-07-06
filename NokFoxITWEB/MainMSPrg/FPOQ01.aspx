 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPOQ01.aspx.cs" Inherits="LabelMapUpLoad_FPOQ01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO-Print</title>
    <link href="FPOQ01.css" rel="stylesheet" type="text/css" />
    <style media="print" type="text/css">
        .noPrint
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function POPrint() {
            document.getElementById("div_inputBox").style.display = "none";
        }
    </script>
</head>
<body onload="POPrint()">
    <object id="WebBrowser" height="0" width="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2">
    </object>
    <form id="form1" runat="server">
    <div class="noPrint">
        <div id="searchBox">
            <div id="div_inputBox" class="div1">
                POID：<asp:TextBox ID="txtKey" Text="" runat="server"></asp:TextBox>
                POCNT：<asp:TextBox runat="server" Width="50" ID="txtPOCNT" onkeyup="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}"
                    onafterpaste="if(this.value.length==1){this.value=this.value.replace(/[^1-9]/g,'')}else{this.value=this.value.replace(/\D/g,'')}"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" Text="query" OnClick="btnQuery_Click" />
            </div>
            <div class="div2">
                <input type="button" value="Print" onclick="javascript:document.all.WebBrowser.ExecWB(6,1)" />
                <input type="button" value="Page Set" onclick="javascript:document.all.WebBrowser.ExecWB(8,1)" />
                <input type="button" value="Print Preview" onclick="javascript:document.all.WebBrowser.ExecWB(7,1)" />
            </div>
        </div>
        <div class="clear">
        </div>
        <hr />
    </div>
    <div id="mainBox">
        <div id="headerBox">
            <div id="block1">
                <span class="blockSpan">BlackBerry Limited<br />
                    2200 University Avenue East<br />
                    Waterloo ON N2K 0A7<br />
                    Canada Phone:(+1)519-888-7465 Fax:(+)519-888-7884<br />
                    Web:<br />
                    www.blackberry.com</span>
            </div>
            <div id="block2">
                <div class="div1">
                    <span class="fontSpan">PO Number</span>
                    <asp:Label runat="server" ID="lblPoNum" Text=""></asp:Label>
                </div>
                <div class="div2">
                    <span class="fontSpan">PO Date</span>
                    <asp:Label runat="server" ID="lblPoDate" Text=""></asp:Label>
                </div>
                <div class="div3">
                    Page 1 of 1
                </div>
                <div class="clear">
                </div>
                <div class="div4">
                    <span class="fontSpan">Change #</span>
                    <asp:Label runat="server" ID="lblChange" Text=""></asp:Label>
                </div>
                <div class="div5">
                    <span class="fontSpan">Revised Date</span>
                    <asp:Label runat="server" ID="lblRevisedDate" Text=""></asp:Label>
                </div>
                <div class="clear">
                </div>
                <div class="div6">
                    <span class="fontSpan">Payments Terms</span>
                    <asp:Label runat="server" ID="lblTerms" Text=""></asp:Label>
                </div>
                <div class="div7">
                    <span class="fontSpan">Crrency</span>
                    <asp:Label runat="server" ID="lblCurrency" Text=""></asp:Label>
                </div>
                <div class="clear">
                </div>
                <div class="div8">
                    <span class="fontSpan">Suppliter No.</span>
                    <asp:Label runat="server" ID="lblSuppliter" Text=""></asp:Label>
                </div>
                <div class="div9">
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
            <div id="block3">
                <div class="div1">
                    <span class="fontSpan">Suppliter:</span>
                </div>
                <div class="div2">
                    <span class="positionSpan">FIH Hong Kong Ltd<br />
                        8F Peninsula Tower 538 Castle Peak<br />
                        518109 HONG KONG<br />
                        CHINA<br />
                        Contact: Frankie Yang -62657<br />
                        Tel:755-2770-8000-24 </span>
                </div>
                <div class="clear">
                </div>
                <div class="div3">
                    <span class="fontSpan">Ship To:</span>
                </div>
                <div class="div4">
                    <asp:Label runat="server" ID="lblGivenName"></asp:Label><br />
                    <asp:Label runat="server" ID="lblCareOfName"></asp:Label><br />
                    <asp:Label runat="server" ID="lblStreetName"></asp:Label><br />
                    <asp:Label runat="server" ID="lblCityPost"></asp:Label><br />
                    <asp:Label runat="server" ID="lblCityName"></asp:Label>
                </div>
            </div>
            <div id="block4">
                <div class="div1">
                    <span class="fontSpan">Sold To:</span>
                </div>
                <div class="div2">
                    <asp:Label runat="server" ID="lblGivenName_sold"></asp:Label><br />
                    <asp:Label runat="server" ID="lblStreetName_sold"></asp:Label><br />
                    <asp:Label runat="server" ID="lblCityPost_sold"></asp:Label><br />
                    <asp:Label runat="server" ID="lblAeriId"></asp:Label>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div id="middleBox">
            <div class="content">
                <asp:Table runat="server" ID="tbData" CssClass="tbData" CellPadding="0" CellSpacing="0">
                    <asp:TableHeaderRow CssClass="headerRow">
                        <asp:TableHeaderCell CssClass="tableCell_center" Text="Item" Width="80"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="Part No/Rev" Width="160"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="Description<br/> HS Code" Width="160"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="Dock Date Quantity<br/>YYYY/MM/DD" Width="160" HorizontalAlign="Left"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="UM" CssClass="tableCell_center" Width="100"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="Net PriceNet" Width="100"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text="Amount" CssClass="tableCell_center" Width="100"></asp:TableHeaderCell>
                        <asp:TableHeaderCell Text=""></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
            <div class="div1">
                THIS PURCHASE ORDER ("PO") CONSISTS OF: (A) THIS PAGE, (B) CODES OR FLY SHEETS REFERENCED
                BELOW (IF ANY); (C) BUYER'S PRINTED PROVISIONS AS ATTACHED (IF ANY); AND (D) EITHER
                (1) THE TERMS AND CONDITIONS OF THE NEGOTIATED SUPPLY AGREEMENT AS EXECUTED BY DULY
                AUTHORIZED SIGNATORIES OF BOTH PARTIES ("NEGOTIATED SUPPLY AGREEMENT"); OR (2) IN
                THE ABSENCE OF A NEGOTIATED SUPPLY AGREEMENT, BUYER'S STANDARD TERMS AND CONDITIONS
                ("Ts AND Cs") -- A COPY OF WHICH HAS BEEN PROVIDED TO SUPPLIER, IS ATTACHED HERETO,
                OR MAY BE OBTAINED UPON REQUEST.<br />
                SUPPLIER'S ACCEPTANCE OF BUYER'S ORDER EITHER BY SIGNING AND RETURNING AN ACKNOWLEDGEMENT
                OR BY DELIVERING IN WHOLE OR IN PART THE PRODUCTS AND SERVICES SET OUT IN THE ORDER,
                WILL BE DEEMED TO BE AN ACCEPTANCE OF THE PO IN ITS ENTIRETY. IF ANY ACCEPTANCE
                OR OTHER COMMUNICATIONS OF ANY KIND CONTAIN ANY ADDITIONAL TERMS OR CONFLICT WITH
                ANY TERMS AND CONDITIONS OF THE PO, THE PO SHALL GOVERN UNLESS SUPPLIER NOTIFIES
                BUYER'S LEGAL DEPARTMENT BY E- MAILING LEGAL@BLACKBERRY.COM THAT IT IS REJECTING
                THE PO ("NOTICE OF REJECTION") AND AFTER SUCH NOTICE OF REJECTION A DULY AUTHORIZED
                SIGNATORY OF BUYER AND THE SUPPLIER ENTER INTO AN AMENDMENT TO THE PO ("AMENDMENT").
                IN THE ABSENCE OF SUCH AMENDMENT EVEN IF SUPPLIER ISSUES A NOTICE OF REJECTION,
                IF SUPPLIER DELIVERS PRODUCTS OR SERVICES TO BUYER, SUCH PRODUCTS OR SERVICES SHALL
                BE DEEMED TO BE DELIVERED PURSUANT TO THE TERMS OF THE PO.
            </div>
        </div>
    </div>
    </form>
</body>
</html>
