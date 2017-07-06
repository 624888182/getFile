<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmFirstLineRate" CodeFile="WFrmFirstLineRate.ascx.cs" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<uc1:ModelTitle runat="server" ID="modeltitle1"></uc1:ModelTitle>
<table class="DataGridFont" id="Table1" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <asp:Label ID="lblStartDate" runat="server" Width="100px">Date From</asp:Label></td>
        <td style="width: 198px">
            <uc2:Calendar1 ID="tbStartDate" runat="server" />
            <br>
            <asp:Label ID="Label28" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        <td style="height: 23px; width: 101px;">
            <asp:Label ID="lblEndDate" runat="server" Width="100px">Date To</asp:Label></td>
        <td style="height: 23px">
            <uc2:Calendar1 ID="tbEndDate" runat="server" />
            <br>
            <asp:Label ID="Label29" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        <td valign="bottom" rowspan="2">
            <font face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </font>
            <asp:Button ID="btnQuery" Width="80px" Text="Query" runat="server" OnClick="btnQuery_Click">
            </asp:Button></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblModel" runat="server" Width="100px"> Model</asp:Label></td>
        <td width="155">
            <asp:DropDownList ID="ddlModel" runat="server" Width="155px">
            </asp:DropDownList></td>
        <td colspan="2">
            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" CssClass="DataGridFont"
                OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Line Rate</asp:ListItem>
                <asp:ListItem>First Line Rate</asp:ListItem>
            </asp:RadioButtonList><asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="DataGridFont"
                RepeatDirection="Horizontal" Visible="False">
                <asp:ListItem Value="SMT" Selected="True">SMT</asp:ListItem>
                <asp:ListItem Value="ASSEMBLY">ASSEMBLY</asp:ListItem>
            </asp:RadioButtonList></td>
    </tr>
</table>
<hr />
<asp:Panel runat="server" ID="Panel2">
<table>
    <tr>
        <td valign="top">
            <asp:DataGrid ID="dgFirstLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px"
                Font-Names="Verdana" ShowFooter="True" CssClass="DataGridFont" BackColor="White">
                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
                <ItemStyle Font-Bold="True" Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk">
                </ItemStyle>
                <HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px"
                    ForeColor="White" BackColor="#006699"></HeaderStyle>
                <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                <Columns>
                    <asp:BoundColumn DataField="LINEID" HeaderText="Line ID">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PASSQTY" HeaderText="PASS QTY">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FAILQTY" HeaderText="FAIL QTY">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TOTALQTY" HeaderText="TOTAL QTY">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="YIELD" HeaderText="YIELD(%)" DataFormatString="{0:N2}%">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
                    Mode="NumericPages"></PagerStyle>
            </asp:DataGrid></td>
        <td valign="top">
            <font face="新細明體">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <chart:WebChartViewer ID="wcvFirstLineRate" runat="server"></chart:WebChartViewer>
            </font>
        </td>
    </tr>
</table>
</asp:Panel>

<asp:Panel ID="Panel1" runat="server">
    <table>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSMTLineRate" runat="server" Font-Bold="True" Font-Size="X-Large">SMT Line Yield</asp:Label></td>
        </tr>
        <tr>
            <td valign="top">
                <asp:DataGrid ID="dgSMTLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px"
                    Font-Names="Verdana" ShowFooter="True" CssClass="DataGridFont" BackColor="White" OnPageIndexChanged="dgSMTLineRate_PageIndexChanged">
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
                    <ItemStyle Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk"></ItemStyle>
                    <HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px"
                        ForeColor="White" BackColor="#006699"></HeaderStyle>
                    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                    <Columns>
                        <asp:BoundColumn DataField="LINEID" HeaderText="Line ID">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PASSQTY" HeaderText="PASS QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FAILQTY" HeaderText="FAIL QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALQTY" HeaderText="TOTAL QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="YIELD" HeaderText="YIELD(%)" DataFormatString="{0:N2}%">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
                        Mode="NumericPages"></PagerStyle>
                </asp:DataGrid></td>
            <td valign="top">
                <chart:WebChartViewer ID="wcvSMTLineRate" runat="server"></chart:WebChartViewer></td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblAssyLineRate" runat="server" Font-Bold="True" Font-Size="X-Large">Assembly Line Yield</asp:Label></td>
        </tr>
        <tr>
            <td valign="top">
                <asp:DataGrid ID="dgAssyLineRate" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="20" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px"
                    Font-Names="Verdana" ShowFooter="True" CssClass="DataGridFont" BackColor="White" OnPageIndexChanged="dgAssyLineRate_PageIndexChanged">
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
                    <ItemStyle Height="30px" ForeColor="#000066" Width="100px" BackColor="Cornsilk"></ItemStyle>
                    <HeaderStyle Font-Size="Small" Font-Bold="True" HorizontalAlign="Center" Height="20px"
                        ForeColor="White" BackColor="#006699"></HeaderStyle>
                    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                    <Columns>
                        <asp:BoundColumn DataField="LINEID" HeaderText="Line ID">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PASSQTY" HeaderText="PASS QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FAILQTY" HeaderText="FAIL QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALQTY" HeaderText="TOTAL QTY">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="YIELD" HeaderText="YIELD(%)" DataFormatString="{0:N2}%">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="#000066" BackColor="White"
                        Mode="NumericPages"></PagerStyle>
                </asp:DataGrid></td>
            <td valign="top">
                <chart:WebChartViewer ID="wcvAssyLineRate" runat="server"></chart:WebChartViewer></td>
        </tr>
    </table>
</asp:Panel>
