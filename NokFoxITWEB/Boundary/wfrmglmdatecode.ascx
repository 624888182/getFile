<%@ Control Language="c#" Inherits="SFCQuery.Boundary.WFrmGLMDateCode" CodeFile="WFrmGLMDateCode.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" align="center" border="0">
	<tr>
		<td><asp:label id="lblProductID" runat="server" Width="265px" ToolTip="來料DataCode查詢，請輸入Product ID or IMEI or Picasso"> Product ID or IMEI or PICASSO</asp:label></td>
		<td>
			<P><asp:textbox id="tbProductID" runat="server" ToolTip="來料DataCode查詢，請輸入Product ID or IMEI or Picasso"></asp:textbox><br>
				<asp:label id="Label4" runat="server" ForeColor="Red" BackColor="White" Visible="False"></asp:label></P>
		</td>
		<td align="right" width="100"><asp:button id="btnQuery" Runat="server" Text="Query" onclick="btnQuery_Click"></asp:button></td>
	</tr>
</table>
<hr>
 <asp:Panel runat="server" ID="Panel1">
    <table align="center">
	    <tr>
		    <td>
			    <asp:datagrid id="dgDateCode" runat="server" BackColor="White" PageSize="20" BorderStyle="None"
				    BorderWidth="1px" BorderColor="#CCCCCC" Font-Size="10px" Font-Names="Verdana" ShowFooter="True"
				    CssClass="DataGridFont" AutoGenerateColumns="False">
				    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				    <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				    <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				    <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				    <Columns>
					    <asp:TemplateColumn HeaderText="Part NO">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate> 
							    <asp:LinkButton id="lbtn1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNo") %>' CausesValidation="false" CommandName="PartNo">
							    </asp:LinkButton> 
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartNo") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn HeaderText="Vendor Name">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VENDOR_NAME") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VENDOR_NAME") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn HeaderText="Date Code">						    
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:LinkButton id="LinkButton1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateCode") %>' CausesValidation="false" CommandName="DateCode">
							    </asp:LinkButton> 
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox2"   runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RcvNo") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn> 
					    <%--Rcv No--%>
					    <asp:TemplateColumn HeaderText="Rcv No">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:LinkButton id="LinkButton2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RcvNo") %>' CausesValidation="false" CommandName="RcvNo">
							    </asp:LinkButton> 
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox3"   runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RcvNo") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn> 
					    <%--Rcv No--%>	
					    <asp:TemplateColumn HeaderText="DISKNO">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISKNO") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISKNO") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>
					    <asp:TemplateColumn HeaderText="InputMaterialDate">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SLDate") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SLDate") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>	
					    <asp:TemplateColumn HeaderText="Line">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Line") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Line") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>	
					    <asp:TemplateColumn HeaderText="Track">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Track") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Track") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>		
					    <asp:TemplateColumn HeaderText="Employee">
						    <HeaderStyle Wrap="False"></HeaderStyle>
						    <ItemStyle Wrap="False"></ItemStyle>
						    <ItemTemplate>
							    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Employee") %>'>
							    </asp:Label>
						    </ItemTemplate>
						    <EditItemTemplate>
							    <asp:TextBox ID="TextBox8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Employee") %>'>
							    </asp:TextBox>
						    </EditItemTemplate>
					    </asp:TemplateColumn>			         
				    </Columns>
				    <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
			    </asp:datagrid>
		    </td>
	    </tr>
    </table>
</asp:Panel>
 <asp:Panel runat="server" ID="Panel2">
     <table align="center" width="60%" > 
        <tr>
           <td>
               <table align="left" width="100%" >
                   <tr >
                      <td>
                         <font><b><asp:Label id="lb1" Visible=false Text="Date Code :" runat="server"></asp:Label></b></font><asp:Label ID="lb11" Visible=false runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td>
                         <font><b><asp:Label id="lb2" Visible=false Text="Rcv No :" runat="server"></asp:Label></b></font><asp:Label ID="lb22" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td>
                         <font><b><asp:Label id="lb3" Text="Part No :" runat="server"></asp:Label></b></font><asp:Label ID="lb33" runat="server"></asp:Label>
                      </td>
                      <td>
                         <font><b><asp:Label id="lb5" Text="Line :" runat="server"></asp:Label></b></font><asp:Label ID="lb55"  runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td>
                         <font><b><asp:Label id="lb4" Text="Input Material Date :" runat="server"></asp:Label></b></font><asp:Label ID="lb44" runat="server"></asp:Label>
                      </td>
                      <td>
                         <font><b><asp:Label id="lb6" Text="Track :" runat="server"></asp:Label></b></font><asp:Label ID="lb66"  runat="server"></asp:Label>
                      </td>
                   </tr> 
                   <tr>
                      <td>
                         <font><b><asp:Label id="lb7" Text="Employee :" runat="server"></asp:Label></b></font><asp:Label ID="lb77"  runat="server"></asp:Label>
                      </td>
                      <td > 
                         <font><asp:Label id="Label04" runat="server"></asp:Label></font>
                      </td>
                   </tr>
                   <tr> 
                      <td > 
                         <font><asp:Label id="lbl9" runat="server" Visible="false"></asp:Label></font>
                      </td>
                   </tr>
               </table>
            </td>
        </tr>   
		<tr>
		<td vAlign="top" align="center" width="100%">
		<%--<cwc:webdatagrid id="dgDateCodeDetail" runat="server" Width="272px" CssClass="DataGridFont" UserID="Any"
				ShowFooter="True" CellPadding="3" DisablePaging="False" sUserLanguage="en-us" AllowSorting="True" BorderColor="#CCCCCC" DisableSetButton="False"
				Font-Names="Verdana" Font-Size="10px" AllowPaging="True" PageSize="20" DisableSort="False" SaveSettings="False" BackColor="White" BorderWidth="1px"
				BorderStyle="None"  SetShowFooter="True" AutoGenerateColumns="False">
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<Columns>	
					<asp:BoundColumn DataField="PRODUCTID" HeaderText="PRODUCTID"></asp:BoundColumn>
					<asp:BoundColumn DataField="IMEI" HeaderText="IMEI"></asp:BoundColumn>
					<asp:BoundColumn DataField="SORDER" HeaderText="SORDER"></asp:BoundColumn>
					<asp:BoundColumn DataField="SPART" HeaderText="SPART"></asp:BoundColumn>
					<asp:BoundColumn DataField="INPUTDATE" HeaderText="INPUT DATE"></asp:BoundColumn>
				</Columns>
			</cwc:webdatagrid>--%>
		     <asp:datagrid id="dgDateCodeDetail" runat="server" Width="100%" CssClass="DataGridFont" Font-Size="10px"
		        BackColor="White" UserID="Any" BorderStyle="None" BorderWidth="1px" AllowPaging="True" OnPageIndexChanged="dgDateCodeDetail_PageIndexChanged"   Font-Names="Verdana"
		        BorderColor="#CCCCCC" CellPadding="3" PageSize="20" ShowFooter="True">
		        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
		        <AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
		        <ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
		        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
			        BackColor="SteelBlue"></HeaderStyle>
		        <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
		        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
	        </asp:datagrid>
		</td>		
	</tr> 
    <tr> 
      <td align="right" >
          <asp:button id="btnExportExcel" runat="server" Width="100px" Height="30px" Text="Export To Excel" onclick="btnExportExcel_Click"></asp:button> 
           <asp:button id="btnExportExcelPartNo" runat="server" Width="100px" Height="30px" Text="Export To Excel" OnClick="btnExportExcelPartNo_Click" Visible="false"></asp:button> 
      
      </td>
    </tr>
	</table>
 </asp:Panel>
