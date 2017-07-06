<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WFrmLineStatusMonitor.ascx.cs" Inherits="Boundary_WFrmLineStatusMonitor" %>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%--每10s刷新一次--%>
<META   HTTP-EQUIV="REFRESH"   CONTENT="10"  URL= "/SFCQuery/Boundary/WFrmLineStatusMonitor.ascx"> 
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<asp:Panel id="Panel2" runat="server" Width="100%" >
    <table width="90%" class="DataGridFont" cellSpacing="0" cellPadding="0"  align="center" border="0">
        <tr height="50" bgcolor="blue" >
            <td width="8%" bordercolor="blue"></td>             
            <td width="8%" bordercolor="blue"></td>          
            <td width="14%" align="center" bordercolor="blue">
                <font color="white" size="20"><b><asp:Label ID="lb1" runat="server" >Line</asp:Label></b></font></td>
            <td width="14%" align="center" bordercolor="blue">
                 <font color="white" size="20"><b><asp:Label ID="lbLine" runat="server"></asp:Label></b></font></td>  
            <td width="11%" bordercolor="blue"></td>          
            <td width="14%" align="center" bordercolor="blue">
                <font color="white" size="20"><b><asp:Label ID="lb2" Text="Model" runat="server"></asp:Label></b></font>
            </td>
            <td width="14%" align="center" bordercolor="blue">
                 <font color="white" size="20"><b><asp:Label ID="lbModel" runat="server"></asp:Label></b></font>
            </td>  
            <td width="8%" bordercolor="blue"></td>          
            <td width="9%" bordercolor="blue"></td>          
        </tr>
        <tr height="50" bgcolor="blue" >
            <td width="100%" colspan="9">
                <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="black">
                    <tr>                        
                        <td  colspan="3" align="center" width="33%">
                            <font color="white" size="10"><asp:Label ID="lb3" runat="server">Shift Target</asp:Label></font>              
                        </td>
                        <td  colspan="3" align="center" bordercolor="black" width="33%">
                            <font color="white" size="10"><asp:Label ID="lb4" runat="server" Text="Achieve Total"></asp:Label></font>
                        </td>
                        <td  colspan="3" align="center" bordercolor="black" width="34%">
                            <font color="white" size="10"><asp:Label ID="lb5" runat="server" Text="Achieve Rate"></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="80">        
            <td width="100%" colspan="9">
                <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="blue" bordercolorlight="black">
                    <tr> 
                        <td  colspan="3" align="center" bordercolor="black" width="33%" bgcolor="white">
                            <font color="black" size="30"><asp:Label ID="lbShiftTarget" runat="server" ></asp:Label></font>
                        </td>
                        <td  colspan="3" align="center" bordercolor="black" width="33%" bgcolor="white">
                            <font color="black" size="30"><asp:Label ID="lbAchieveTotal" runat="server" ></asp:Label></font>
                        </td>
                        <td  colspan="3" align="center" bordercolor="black" width="34%" bgcolor="white">
                            <font color="black" size="30"><asp:Label ID="lbAchieveRate" runat="server" ></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="50" bgcolor="blue"  bordercolor="black">
            <td width="100%" colspan="9">
                    <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="black">
                        <tr>  
                            <td  colspan="3" align="center" width="33%"  bordercolor="black">
                                <font color="white" size="20"><asp:Label ID="lb6" runat="server" Text="Leader"></asp:Label></font>
                            </td>
                            <td  colspan="6" align="center" width="67%"  bordercolor="black">
                                <font color="white" size="20"><asp:Label ID="lb7" runat="server" Text="Through Rate"></asp:Label></font>
                            </td>
                        </tr>
                   </table>
            </td>
        </tr>
        <tr bordercolordark="black" bordercolor="black">
            <td width="100%" colspan="9">
                    <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="black">
                        <tr>
                            <td  colspan="3" align="center" width="33%" bgcolor="white"  bordercolor="black">
                                <asp:Label ID="lbPhoto" runat="server" ></asp:Label>
                            </td> 
                            <td colspan="6" width="67%"  bordercolor="black">
                                <asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px"
	                                BackColor="White" BorderColor="#CCCCCC" Font-Names="Verdana" CssClass="DataGridFont"  Width="100%">
	                                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	                                <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	                                <ItemStyle HorizontalAlign="Right" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	                                <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	                                <Columns>
                                        <asp:TemplateColumn HeaderText="Station ID">
                                            <ItemStyle BackColor="#006699" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Station_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
		                                <asp:BoundColumn DataField="Input" HeaderText="Input" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="FirstPass" HeaderText="First Pass" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="FirstYield" HeaderText="First Yield" Visible="False"> 
		                                </asp:BoundColumn>
		                                <asp:BoundColumn DataField="FinalPass" HeaderText="Final Pass" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>                
		                                <asp:BoundColumn DataField="FinalFail" HeaderText="Final Fail" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="FinalYield" HeaderText="Trough Rate" ItemStyle-HorizontalAlign="Center">
			                                <ItemStyle ForeColor="Red"></ItemStyle>
		                                </asp:BoundColumn>
		                                <asp:BoundColumn DataField="FirstFail" HeaderText="First Fail" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="SecondFail" HeaderText="Second Fail" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="ThirdFail" HeaderText="Third Fail" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="ForthFail" HeaderText="Forth Fail" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="FifthFail" HeaderText="Fifth Fail" Visible="False"></asp:BoundColumn>
		                                <asp:BoundColumn DataField="FinalFail1" HeaderText="Final Fail1" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Retest Rate" Visible="false"> 
                                            <ItemStyle ForeColor = "red" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RetestRate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>                		
	                                </Columns>
                                </asp:datagrid>
                            </td>
                        </tr>
                   </table>
            </td>
        </tr>
        <tr bordercolor="black">
            <td width="100%" colspan="9" height="100%">
                <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="black">
                    <tr bordercolor="black" height="100%">
                        <td rowspan="4" colspan="3" width="33%"  bordercolor="black" height="100%">
                            <table width="100%" border="1px" cellSpacing="0" cellPadding="0" bgcolor="white" height="100%">
                                <tr height="50px"  bordercolor="black">
                                    <td valign="bottom">
                                        <font size="5px" color="black"><b>
                                        <asp:Label ID="lb8" runat="server" Text="應到："></asp:Label>
                                        <asp:Label ID="lbYingDao" runat="server" ></asp:Label>
                                        <asp:Label ID="lb9" runat="server" Text="人"></asp:Label></b></font>
                                    </td>
                                </tr>
                                <tr height="50px"  bordercolor="black">
                                    <td valign="bottom">
                                        <font size="5px" color="black"><b>
                                        <asp:Label ID="lb10" runat="server" Text="實到："></asp:Label>
                                        <asp:Label ID="lbShiDao" runat="server" ></asp:Label>
                                        <asp:Label ID="lb11" runat="server" Text="人"></asp:Label></b></font>
                                    </td>
                                </tr>
                                <tr height="50px"  bordercolor="black">
                                    <td valign="bottom">
                                        <font size="5px" color="black" ><b>
                                        <asp:Label ID="lb12" runat="server" Text="請假："></asp:Label>
                                        <asp:Label ID="lbQingJia" runat="server" ></asp:Label>
                                        <asp:Label ID="lb13" runat="server" Text="人"></asp:Label></b></font>
                                    </td>
                                </tr>
                                <tr height="50px"  bordercolor="black">
                                    <td valign="bottom">
                                        <font size="5px" color="black"><b>
                                        <asp:Label ID="lb14" runat="server" Text="曠工："></asp:Label>
                                        <asp:Label ID="lbKuangGong" runat="server" ></asp:Label>
                                        <asp:Label ID="lb15" runat="server" Text="人"></asp:Label></b></font>
                                    </td>
                                </tr>
                                <tr height="50px"  bordercolor="black">
                                    <td valign="bottom">
                                        <font size="5px" color="red"><b><asp:Label ID="lbshijian" runat="server">當前時間：</asp:Label><asp:Label ID="lbDateTime" runat="server" ></asp:Label></b></font>
                                    </td>
                                </tr>
                            </table>                
                        </td>
                        <td colspan="6" rowspan="4" align="center" width="67%"  bordercolor="black" height="100%">
                            <table width="100%">
	                            <tr width="100%">
		                            <td width="100%">
			                            <chart:WebChartViewer id="WebChartViewer1" runat="server" Visible="False" ></chart:WebChartViewer></td>
	                            </tr>
                            </table>
                        </td>   
                    </tr>
                </table>
            </td>
        </tr>  
    </table>
</asp:Panel>