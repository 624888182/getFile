<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPOI01.aspx.cs" Inherits="MainBBRYPrg_FPOI01" %>

<%--<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>--%>

<script language="javascript" type="text/javascript" src="../Jscript/Calendar.js"></script>

<%--<%@ Register Src="BBRY_Header.ascx" TagName="BBRY_Header" TagPrefix="uc2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Change</title>

    <script src="POPrint.js" type="text/javascript"></script>

    <style type="text/css">
        .gvHeader
        {
            background-color: #99ccff;
            height: 40px;
            color: #000;
        }
        .btnStyle
        {
            background-color: #CC6600;
            color: #fff;
            font-weight: bold;
            cursor: pointer;
            border: none;
        }
    </style>
</head>
<body style="margin-top: 0; margin-left: 0; overflow:scroll;">
    <form id="form1" runat="server">
    <div style="width: 110%; height: 50px; background-color: #FF9933; text-align: left;
        font-weight: bold; font-size: xx-large;">
        BBRY B2B Web
    </div>
    <div style="width: 110%; height: 5px; background-color: #99ccff; margin-top: 1px;
        font-size: 1px;">
    </div>
    <div style="width: 100%; margin-top: 10px;">
        <div style="float: left; margin-left: 20px;">
            POID:
            <asp:TextBox ID="TextBox1" runat="server" Width="100"></asp:TextBox></div>
        <div style="float: left; margin-left: 10px;">
            Receive_Date:<asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" ForeColor="Black"
                onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>(Beginning)
        </div>
        <div style="float: left;">
            <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" ForeColor="Black"
                onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>(Ending)</div>
        <div style="float: left; margin-left: 10px;">
            <asp:Button ID="BtnGO" runat="server" BackColor="#CC6600" BorderColor="#FF9933" Font-Bold="True"
                ForeColor="White" Text="GO" OnClick="BtnGO_Click" /></div>
        <div style="float: right; margin-right: 30px;">
            <asp:Label runat="server" ID="lblTotal" Text=""></asp:Label></div>
    </div>
    <div style="clear: both;">
    
        <asp:Label ID="Label94" runat="server" 
            style="text-align: center; color: #FF0000; font-weight: 700" Text="Label" 
            Width="350px"></asp:Label>
    
    </div>
    <div style="width: 100%; overflow: scroll; margin-top: 10px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="datagrid gridWidth"
            Height="114px" Width="1600px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDataBound="GridView1_RowDataBound" PageSize="20" OnRowCreated="GridView1_RowCreated"
            DataKeyNames="ID,POID,ITEMID,POCNT,DATAFROM" SkinID="gvMain" OnRowDeleting="Gridview_RowDeleting"
            HeaderStyle-CssClass="gvHeader" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="25px" />
                    <ItemTemplate>
                        &nbsp;<asp:ImageButton ID="ibtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/SkinFile/images/arrow.gif" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox3" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NO">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="POID">
                    <ItemTemplate>
                        <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ITEMID">
                    <ItemTemplate>
                        <asp:Label ID="Label92" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ITEMID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="POCONT">
                    <ItemTemplate>
                        <asp:Label ID="lblPOCNT" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POCNT") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CREATIONDT">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="INTERNALID">
                    <ItemTemplate>
                        <asp:Label ID="Label10a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"INTERNALID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CostAmount">
                    <ItemTemplate>
                        <asp:Label ID="Label10b" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CostAmount") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY/DELIVERYSTARTDT">
                    <ItemTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                            BorderColor="#009900" BorderStyle="Ridge" HorizontalAlign="Center">
                            <RowStyle BorderColor="#009933" BorderStyle="Ridge" />
                            <Columns>
                                <asp:BoundField DataField="QTY" />
                                <asp:BoundField DataField="DELIVERYSTARTDT" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CONFIRM_ADD_FLAG">
                    <ItemTemplate>
                        <asp:Label ID="Label93" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRM_ADD_FLAG") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CONFIRMFLAG">
                    <ItemTemplate>
                        <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRMFLAG") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SENDFLAG ">
                    <ItemTemplate>
                        <asp:Label ID="Label23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SENDFLAG") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UPLOAD_SAP">
                    <ItemTemplate>
                        <asp:Label ID="Label19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UPLOAD_SAP") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SAP_LOG">
                    <ItemTemplate>
                        <asp:Label ID="Label119" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SAP_LOG") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ProductRecipientID">
                    <ItemTemplate>
                        <asp:Label ID="Label11a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ProductRecipientID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IncoTermsCode">
                    <ItemTemplate>
                        <asp:Label ID="Label11b" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IncoTermsCode") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IncoTermsName">
                    <ItemTemplate>
                        <asp:Label ID="Label11c" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"IncoTermsName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OriginID">
                    <ItemTemplate>
                        <asp:Label ID="Label11d" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OriginID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TESTDATAINDICATOR">
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TESTDATAINDICATOR") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SENDERID">
                    <ItemTemplate>
                        <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SENDERID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BUYERPOSTDT">
                    <ItemTemplate>
                        <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BUYERPOSTDT") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TRANSMISSIONINDICATOR">
                    <ItemTemplate>
                        <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TRANSMISSIONINDICATOR") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SELLERPARTYID">
                    <ItemTemplate>
                        <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SELLERPARTYID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CLASSIFICATIONCODE">
                    <ItemTemplate>
                        <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CLASSIFICATIONCODE") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TRANSFERLOCATIONNAME">
                    <ItemTemplate>
                        <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TRANSFERLOCATIONNAME") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label91" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="hid" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHidDataFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"dataFrom") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="hid" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHidSCHEDULEQUANTITY" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SCHEDULEQUANTITY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UserConfirm">
                    <ItemTemplate>
                        <asp:Button ID="btnUserConfirm" runat="server" Text="UserConfirm" CssClass="btnStyle"
                            CommandArgument='<%# Container.DataItemIndex %>' OnClick="btnUserConfirm_Click"
                            CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClearQty">
                    <ItemTemplate>
                        <asp:Button ID="btnClearQty" runat="server" Text="ClearQty" CssClass="btnStyle" CommandArgument='<%# Container.DataItemIndex %>'
                            OnClick="btnClearQty_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ClearSendFlag">
                    <ItemTemplate>
                        <asp:Button ID="btnClearSendFlag" runat="server" Text="ClearSendFlag" CssClass="btnStyle"
                            CommandArgument='<%# Container.DataItemIndex %>' OnClick="btnClearSendFlag_Click"
                            CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Clear">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" Text="Clear" BackColor="#CC6600" BorderColor="#FF9933"
                            Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                            OnClick="Button_Clear" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Confirm">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" Text="Confirm" BackColor="#CC6600" BorderColor="#FF9933"
                            Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                            OnClick="Button_Confirm" CausesValidation="False" />
                                          </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                --%>
                <asp:TemplateField HeaderText="VerifySO">
                            <ItemTemplate>
                                <asp:Button ID="Button7" runat="server" Text="VerifySO" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button7_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreateSO">
                            <ItemTemplate>
                                <asp:Button ID="Button6" runat="server" Text="CreateSO" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button6_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                <asp:TemplateField HeaderText="Print">
                    <ItemTemplate>
                        <input id="btnPrint" type="button" onclick='return POPrint(<%#DataBinder.Eval(Container.DataItem,"POID") %>,<%#DataBinder.Eval(Container.DataItem,"POCNT") %>);'
                            value="POPrint" style="background-color: #CC6600; font-weight: bold; color: #fff;
                            width: 60px; height: 21px; border: 1px solid #ff9933; cursor: pointer;" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle />
            <SelectedRowStyle Font-Bold="True" />
            <HeaderStyle Font-Bold="False" />
            <PagerStyle CssClass="LeftPagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
