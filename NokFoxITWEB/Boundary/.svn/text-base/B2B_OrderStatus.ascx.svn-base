<%@ Control Language="C#" AutoEventWireup="true" CodeFile="B2B_OrderStatus.ascx.cs" Inherits="Boundary_B2B_OrderStatus" %>
<%@ Register TagPrefix="cwc" NameSpace="System.Web.UI.WebControls" Assembly="WebDataGrid"%>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx"%> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<meta name="vs_showGrid" content="False">
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"  align="center" width="95%">
    <tr height="10px" >
        <td height="10px" valign="middle">
            <table height="100%">
                <tr>
                    <td  align="right" style="width: 100%; height: 10px;">
                        <asp:imagebutton id="btnSearch" runat="server" AlternateText="Search" ImageUrl="..\Images\search.gif" OnClick="btnSearch_Click" ></asp:imagebutton>
                    </td>
                    <td align="left" style="height: 10px">
                        <asp:Label ID="lblSearch" runat="server" >Search</asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" >
        <asp:Panel runat="server" ID="panel2" Width="100%"  > 
            <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1"  align="center" width="100%">
                <tr>
		            <td align="right" width="30%"><asp:label id="lblFromDate" runat="server" >From Date:</asp:label></td>
		            <td Width="70%">
		                <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
		                    <tr>
		                        <td>
		                        <uc2:Calendar1 ID="tbStartDate" runat="server" />
		                        </td>
		                        <td>
		                        <asp:Label ID="lblgeshi1" runat="server"  Text="(yyyy/MM/dd HH:mm)  "></asp:Label>
		                        </td>
		                        <td>
		                        <asp:label id="Label1" runat="server" Visible="False" ForeColor="Red"></asp:label>
		                        </td>
		                    </tr>
		                </table>  
			        </td>
	            </tr>
	            <tr>
		            <td style="HEIGHT: 23px" align="right" Width="30%"><asp:label id="lbEndDate" runat="server" >To Date:</asp:label></td>
		            <td Width="70%">
		                <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
		                    <tr>
		                        <td style="height: 51px">
		                        <uc2:Calendar1 ID="tbEndDate" runat="server" />
		                        </td>
		                        <td style="height: 51px">
		                        <asp:Label ID="lblgeshi2" runat="server"  Text="(yyyy/MM/dd HH:mm)  "></asp:Label>
		                        </td>
		                        <td style="height: 51px">
		                        <asp:label id="Label2" runat="server" Visible="False" ForeColor="Red"></asp:label>
		                        </td>
		                    </tr>
		                </table> 
			        </td>
	            </tr>
	            <tr>
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label7" runat="server" Width="100px">Message ID:</asp:label></td>
		            <td Width="70%">
		                 <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
		                    <tr>
		                        <td style="height: 84px">
		                            <asp:TextBox ID="txtMesgID" runat="server" TextMode="MultiLine" Width="200px" Height="80px"></asp:TextBox>	                        
		                        </td>
		                        <td style="height: 84px">
		                           <asp:Label ID="lblgeshi4" runat="server"  Text="(以逗號區分,例如:000000480,000000481)  "></asp:Label> 
		                        </td>
		                    </tr>
		                </table> 
		                
		            </td>
	            </tr>
	            <tr>
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label12" runat="server" Width="100px">Idoc NO:</asp:label></td>
		            <td Width="70%">
		                <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
		                    <tr>
		                        <td>
		                            <asp:TextBox ID="txtIdocNo" runat="server" TextMode="MultiLine" Width="200px" Height="80px"></asp:TextBox>                  
		                        </td>
		                        <td>
		                           <asp:Label ID="lblgeshi7" runat="server"  Text="(以逗號區分,例如:000000480,000000481)  " ></asp:Label> 
		                        </td>
		                    </tr>
		                </table> 
		                
		            </td>
	            </tr>           
	            <tr>
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label8" runat="server" Width="100px">PO NO:</asp:label></td>
		            <td Width="70%">
		                 <table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0">
		                    <tr>
		                        <td>
		                            <asp:TextBox ID="txtPoNo" runat="server" TextMode="MultiLine" Width="200px" Height="80px"></asp:TextBox>                       
		                        </td>
		                        <td>
		                           <asp:Label ID="lblgeshi5" runat="server"  Text="(以逗號區分,例如:000000480,000000481)  "></asp:Label> 
		                        </td>
		                    </tr>
		                </table> 
		                
		            </td>
	            </tr>
	            <tr> 
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label9" runat="server" Width="100px">Status:</asp:label></td>
		            <td Width="70%">
		            <asp:dropdownlist id="ddlStatus" runat="server" Width="80px"  >
		                <asp:ListItem Text="All" Selected="True"></asp:ListItem>
		                <asp:ListItem Text="V" ></asp:ListItem>
		                <asp:ListItem Text="BS" ></asp:ListItem>
		                <asp:ListItem Text="SS" ></asp:ListItem>	    
		            </asp:dropdownlist></td>
	            </tr>	            
	            <tr> 
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label13" runat="server" Width="100px">SendFlag:</asp:label></td>
		            <td Width="70%">
		            <asp:dropdownlist id="ddlSendFlag" runat="server" Width="80px"  >
		                <asp:ListItem Text="All" Selected="True"></asp:ListItem>
		                <asp:ListItem Text="N-New" ></asp:ListItem>
		                <asp:ListItem Text="Y-Sent" ></asp:ListItem>
		                <asp:ListItem Text="E-Error" ></asp:ListItem>
		                <asp:ListItem Text="H-Hold"></asp:ListItem>		    
		            </asp:dropdownlist></td>
	            </tr>
	            <tr> 
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label15" runat="server" Width="100px">Order By:</asp:label></td>
		            <td Width="70%">
		            <asp:dropdownlist id="ddlOrder" runat="server" Width="60px"  >
		                <asp:ListItem Text="Desc" Selected="True"></asp:ListItem>
		                <asp:ListItem Text="Asc" ></asp:ListItem>	    
		            </asp:dropdownlist></td>
	            </tr>	
	            <tr> 
		            <td style="HEIGHT: 18px" align="right" Width="30%"><asp:label id="Label16" runat="server" Width="100px">Page Rows:</asp:label></td>
		            <td Width="70%">
		            <asp:dropdownlist id="ddlPageRows" runat="server" Width="50px"  >
		                <asp:ListItem Text="1" Selected="True"></asp:ListItem>
		                <asp:ListItem Text="50" ></asp:ListItem>
		                <asp:ListItem Text="100" ></asp:ListItem>
		                <asp:ListItem Text="200" ></asp:ListItem>
		                <asp:ListItem Text="500"></asp:ListItem>		    
		            </asp:dropdownlist></td>
	            </tr>
            </table>
            </asp:Panel>
        </td>
    </tr>
</table>

