<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SCMPO_Change_HEADER.aspx.cs"
    Inherits="BBSCMPO_Change_HEADER" StylesheetTheme="SkinFile" %>

<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>
<%@ Register Src="BBRY_Header.ascx" TagName="BBRY_Header" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="POPrint.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 934px;
        }
        .style3
        {
            width: 64px;
        }
        .style4
        {
            width: 629px;
        }
        .style5
        {
            width: 352px;
        }
        #Select2
        {
            width: 77px;
        }
        #Select1
        {
            width: 98px;
        }
        #Downlist2
        {
            width: 71px;
        }
        .style6
        {
            width: 824px;
            height: 43px;
            font-weight: bold;
            font-size: x-large;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SetBGC() {

            var style;

            var obj = document.getElementByIdx("lv_roleList").getElementsByTagName_r("tr");

            for (var i = 0; i < obj.length; i++) {

                obj[i].onclick = function () {

                    style = this.style.background; //记录当前样式（背景）

                    for (var j = 0; j < obj.length; j++) {

                        obj[j].style.background = ""; //将所有行的样式清空
                    }

                    this.style.background = style; //还原当前行样式

                    this.style.background = this.style.background == "#ff0000" ? "" : "#ff0000"; //调整将当前行样式
                }
            }
        }

        if (window.attachEvent)
          //  window.attachEvent("onload", SetBGC);

    </script>
</head>
<body class="homePageBg">
    <form id="form1" runat="server">
    <!--  <uc1:HeaderBar ID="Header1" runat="server"></uc1:HeaderBar>   -->
    <!--<uc2:BBRY_Header ID="BBRY_Header1" runat="server" />-->
    
    <table>
    <tr>
    <td class="style6">PO Change Create</td>
    </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td class="style2">
                <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#FF9933" Height="21px"
                    Width="137px">
                    <asp:ListItem>POID</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="211px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="BtnGO" runat="server" BackColor="#CC6600" BorderColor="#FF9933" Font-Bold="True"
                    ForeColor="White" Text="GO" OnClick="BtnGO_Click" />
            </td>
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Text="ShipFrom_DUNS:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" BackColor="#FF9900" Width="134px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Receive_Date:"></asp:Label>
                <asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" ForeColor="Black"
                    onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="(Beginning)"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" ForeColor="Black"
                    onclick="showCalendar();" onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="(Ending)"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td class="style4">
                &nbsp;&nbsp;
                <select id="Downlist1" name="D3" onchange="ChangeValue()" style="background-color: #FFCC66"
                    runat="server">
                    <option selected="selected" value="Upload_SAP">Upload_SAP</option>
                    <option value="Price_Flag">Price_Flag</option>
                </select>&nbsp;
                <select id="Downlist2" name="D4" style="background-color: #FFCC66" runat="server">
                    <option selected="selected" value="ALL">ALL</option>
                    <option value="Y">Y</option>
                    <option value="N">N</option>
                    <option value="E">E</option>
                    <option value="M">M</option>
                </select>&nbsp;
                <asp:CheckBox ID="CheckBox1" runat="server" Text="(Confirm All?)" />
                &nbsp;
                <asp:DropDownList ID="DropDownList5" runat="server" BackColor="#FF9900" Width="145px">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="btnsap" runat="server" BackColor="#CC6600" BorderColor="#FF9933"
                    OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')"
                    Style="height: 26px" Font-Bold="True" ForeColor="White" Text="SAP" OnClick="btnsap_Click" />
                &nbsp;<asp:Button ID="btnsbi" runat="server" BackColor="#CC6600" OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')"
                    Style="height: 26px" BorderColor="#FF9933" Font-Bold="True" ForeColor="White"
                    Text="SBI" OnClick="btnsbi_Click" />
                &nbsp;<asp:Button ID="btnissue" runat="server" BackColor="#CC6600" OnClientClick="return confirm('You will filter the issue SBI price? Are you sure to execute the process?')"
                    Style="height: 26px" BorderColor="#FF9933" Font-Bold="True" ForeColor="White"
                    Text="Issue" OnClick="btnissue_Click" />
                &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
            </td>
            <td align="right" class="style5">
                <asp:Label ID="Label6" runat="server" Text="Add SAP_Price:"></asp:Label>
                <asp:CheckBox ID="CheckBox2" runat="server" Text="(Add All?)" />
                <asp:TextBox ID="TextBox4" runat="server" Width="117px"></asp:TextBox>
                <asp:Button ID="btnadd" runat="server" BackColor="#CC6600" OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')"
                    Style="height: 26px" BorderColor="#FF9933" Font-Bold="True" ForeColor="White"
                    Text="Add" OnClick="btnadd_Click" />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Button" />
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" BorderColor="#FF3300" />
            </td>
            <td align="right">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="datagrid gridWidth"
                    Height="114px" Width="1600px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowDataBound="GridView1_RowDataBound" PageSize="20" OnRowCreated="GridView1_RowCreated"
                    DataKeyNames="ID,ITEMID" SkinID="gvMain" OnRowDeleting="Gridview_RowDeleting">
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
                                <asp:Label ID="Label92a" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POCNT") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="CONFIRM_ADD_FLAG">
                            <ItemTemplate>
                                <asp:Label ID="Label93" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CONFIRM_ADD_FLAG") %>'></asp:Label>
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
                                <%--<asp:DataList ID="DataList1" runat="server">
                                </asp:DataList>--%>
                                <%--<asp:Label ID="Label7" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"QTY") %>'></asp:Label>--%>
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
                        <asp:TemplateField HeaderText="UPLOAD_SAP">
                            <ItemTemplate>
                                <asp:Label ID="Label19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UPLOAD_SAP") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="Label91" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clear">
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
                                <!--
                                <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="Detail" Text=''></asp:LinkButton>
                                      -->
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
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
