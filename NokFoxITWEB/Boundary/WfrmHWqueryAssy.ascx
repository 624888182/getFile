<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WfrmHWqueryAssy.ascx.cs" Inherits="Boundary_WfrmHWqueryAssy" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<uc1:modeltitle id="ModelTitle" runat="server"></uc1:modeltitle>
<table>
     <tr>
         <td style="width: 784px">
             <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0" width="90%" align="left">	   
                 <%--<tr> 
                    <td align="left" style="width: 115px; height: 23px"><asp:label id="lblModel" runat="server" Width="103px"></asp:label></td>
                    <td align="left" style="height: 23px; width: 146px;">
                        <asp:dropdownlist id="ddlModel" width="150px"  runat="server" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" ></asp:dropdownlist></td>
                    <td align="right" width="200px" style="height: 23px">
                        </td> 
                 </tr>--%><%--
                 <tr>
                    <td height="10px" style="width: 115px"></td>
                    <td><asp:Label ID="lbInfo" runat="server" ></asp:Label></td>
                    <td></td>
                 </tr>--%>
                 <tr> 
                     <td align="left" style="width: 90px; height: 24px;"><asp:label id="lblProductID" runat="server" Width="104px">Main PID</asp:label></td>
                     <td align="left" style="width: 130px; height: 24px;">
                        <asp:textbox ID="txtMainID" width="150px"  runat="server" /></td>
                    <td align="right" width="200px" style="height: 24px">
                        </td>
                 </tr>
                 <tr> 
                     <td align="left" style="width: 90px"><asp:label id="Label1" runat="server" Width="103px">Secondary PID</asp:label></td>
                     <td align="left" style="width: 130px">
                        <asp:textbox ID="txtSecondaryID" width="150px"  runat="server" /></td>
                    <td align="right" width="200px">
                        </td>
                 </tr>
                 <tr> 
                     <td align="left" style="width: 90px; height: 24px;"><asp:label id="Label3" runat="server" Width="100px">Third PID</asp:label></td>
                     <td align="left" style="width: 130px; height: 24px;">
                        <asp:textbox ID="txtThirdID" width="150px"  runat="server" /></td>
                    <td align="right" width="200px" style="height: 24px">
                        <asp:button id="btnquery" runat="server" width="100px" Text="Query" OnClick="btnquery_Click" ></asp:button></td>
                 </tr>
            </table>
        </td>
     </tr>
</table>
<hr />

<table>
     <tr>
         <td>
             <table>
	            <tr>
		            <td vAlign="top">
                        <table cellSpacing="0" cellPadding="0" border="0" width=80%  >
                            <tr> 
                                <td width="100%"><asp:Label ID="lbInfo" runat="server" ></asp:Label></td> 
                            </tr>  
	                        <tr align=left width="100%">
                               <td width="100%" valign="middle" >  
	                                <asp:DataGrid ID="dgHWByPID" runat="server" CssClass="DataGridFont" BorderStyle="None" BorderWidth="1px"
							                    BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True" AutoGenerateColumns="true" AllowPaging="True" PageSize="20" >
                                        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                                        <ItemStyle BackColor="Cornsilk" ForeColor="#000066" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <%--<Columns>
                                            <asp:TemplateColumn HeaderText="PRODUCT ID" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1"  runat="server" CausesValidation="false" CommandName="PRODUCTID" Text='<%# DataBinder.Eval(Container, "DataItem.PRODUCTID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="SORDER" HeaderText="SORDER "></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MODEL" HeaderText="MODEL"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SPART" HeaderText="SPART"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BOM_VER" HeaderText="BOM_VER"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LOWER_PN" HeaderText="LOWER_PN"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DAUGHTER_PN" HeaderText="DAUGHTER_PN"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="UP_PN" HeaderText="UP_PN"></asp:BoundColumn>
				                        </Columns>--%>
                                  </asp:DataGrid>  
	                            </td>
	                        </tr>	
                        </table>
		            </td>
		            <%--<td vAlign="top" align="center">
			            <table>
				            <tr>
					            <td align="center"><FONT><asp:label id="Label2" runat="server" Visible="False" CssClass="DataGridFont">Process Information</asp:label></FONT></td>
				            </tr>
				            <tr>
					            <td align="center"><asp:datagrid id="dgProduct" runat="server" Width="272px" CssClass="DataGridFont" AutoGenerateColumns="False"
						            BorderStyle="None" BorderWidth="1px" BackColor="White" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana"
						            CellPadding="3" ShowFooter="True" PageSize="30">
						            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
						            <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
						            <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
						            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
						            <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
						            <Columns>
							            <asp:BoundColumn DataField="FCDate" HeaderText="Creation Date">
								            <HeaderStyle Wrap="False"></HeaderStyle>
								            <ItemStyle Wrap="False"></ItemStyle>
							            </asp:BoundColumn>
							            <asp:BoundColumn DataField="FStationID" HeaderText="Station ID">
								            <HeaderStyle Wrap="False"></HeaderStyle>
								            <ItemStyle Wrap="False"></ItemStyle>
							            </asp:BoundColumn>
							            <asp:BoundColumn DataField="FStateID" HeaderText="State ID">
								            <HeaderStyle Wrap="False"></HeaderStyle>
							            </asp:BoundColumn>
							            <asp:BoundColumn DataField="FLine" HeaderText="Line">
								            <HeaderStyle Wrap="False"></HeaderStyle>
							            </asp:BoundColumn>
							            <asp:BoundColumn DataField="EMP_ID" HeaderText="Employee ID">
								            <HeaderStyle Wrap="False"></HeaderStyle>
							            </asp:BoundColumn>
							            <asp:BoundColumn DataField="ERROR_CODE" HeaderText="ERROR CODE">
								            <HeaderStyle Wrap="False"></HeaderStyle>
							            </asp:BoundColumn>
						            </Columns>
						            <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						            </asp:datagrid></td>
				            </tr>	
			            </table>
		            </td>--%>
	            </tr>
            </table>   
         </td>
     </tr>     
</table>
