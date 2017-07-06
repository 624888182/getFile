<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmLackPartAlert.ascx.cs" Inherits="Boundary_wfrmLackPartAlert" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<%--每10s刷新一次--%>
<META   HTTP-EQUIV="REFRESH" CONTENT="10" URL="/SFCQuery/Boundary/wfrmLackPartAlert.ascx" > 
<%--<MARQUEE DIRECTION=up BEHAVIOR=scroll SCROLLAMOUNT=10 SCROLLDELAY=200>
这是一个滚动字幕。
</MARQUEE>--%>
<%--<meta http-equiv="Page-Exit" content="revealTrans(duration=1.0, transition=23)">--%>
<asp:Panel id="Panel2" runat="server" Width="100%" >
    <table width="90%" class="DataGridFont" cellSpacing="0" cellPadding="0"  align="center" border="0">
        <tr height="50">          
            <td width="14%" align="center">
                <font color="white" size="20"><b><asp:Label ID="lb1Lack" runat="server" ></asp:Label></b></font>
            </td>                   
        </tr>       
        <tr>
            <td width="100%" colspan="9">
                    <table border="1px" cellSpacing="0" cellPadding="0" width="100%" height="100%" bordercolor="black">
                        <tr>  
                            <td>
                                <asp:datagrid id="dgLack" runat="server"  BorderStyle="None" BorderWidth="1px"
	                                BackColor="White" BorderColor="#CCCCCC" Font-Names="Verdana" CssClass="DataGridFont"  Width="100%">
	                                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
	                                <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
	                                <ItemStyle HorizontalAlign="Center" ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
	                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
	                                <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
	                                <%--<Columns>
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
	                                </Columns>--%>
                                </asp:datagrid>
                            </td>
                        </tr>
                   </table>
            </td>
        </tr> 
    </table>
</asp:Panel>