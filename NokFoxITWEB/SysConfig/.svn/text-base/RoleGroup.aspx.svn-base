<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleGroup.aspx.cs" Inherits="SysConfig_RoleGroup"　StylesheetTheme="SkinFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>


</head>
<body>
    <form id="form2" runat="server"> 
          <table border="0" align="center" cellpadding="0" cellspacing="0" width="50%" style="text-align:center; " id="TABLE6" onclick="return TABLE1_onclick()" class="FounctionTable">
            <tr>
                <td class="PageHeaderLeft" align="left" >
                    <asp:Label ID="lbHead" runat="server" >權限分配</asp:Label>
                    </td>
                 <td class="PageHeaderRight" style="width: 100px">
                     
                    </td>
            </tr>
            
          
       
            <tr>
                <td class="founction" colspan="2" style="height: 50px">
         
                    <table id="Table7" style="HEIGHT: 10px;"  cellspacing="0"  cellpadding ="0" width="100%" border="0">
							
							<tr>
								<td  colspan="2" style="HEIGHT: 5px;"  align="left">
                                
                                    <asp:Label ID="Label5" runat="server" SkinID=lblMain>權限組  : </asp:Label>
                                    <asp:DropDownList ID="DdlRole" runat="server" AutoPostBack="True"  Width="141px" OnSelectedIndexChanged="DdlRole_SelectedIndexChanged">
                                    </asp:DropDownList></td>
							</tr>
							<tr>
								<td colspan="2" style="HEIGHT: 5px;" ></td>
							</tr>
							<tr>
								<td class="ButtonEareaLeft" align="left" colspan="2" >
									<asp:button id="btnCommit" runat="server" CssClass="MainButton"  Text="保存" OnClick="btnCommit_Click"></asp:button>
									<asp:button id="btnExit" runat="server" CssClass="MainButton"  Text="退出" CausesValidation="False" OnClick="btnExit_Click"></asp:button>
									</td>
							</tr>
							<tr>
								<td colspan="2" style="height: 5px" align="left">&nbsp;<asp:Label ID="lbMessage" runat="server"></asp:Label></td>
							</tr>
						</table>
						<table class="styleBodyTableInTable" id="Table8" cellspacing="0" cellpadding="2" width="100%"
							align="center" border="0">
							<tr  valign="top">
								<td>
									<table id="Table9" cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr><td style="width:100%" align="left" valign="top"></td></tr>
										<tr><td style="width:100%" align="left" valign="top">
                                            <asp:TreeView ID="TreeView1" runat="server" Height="100%" Width="100%">
                                            </asp:TreeView>
                                        </td></tr>
									</table>
								</td>
							</tr>
						</table>
						<table id="Table10"  cellspacing="0"  cellpadding="0" width="100%" border="0">
							<tr>
								<td colspan="2"></td>
							</tr>
						</table>
                </td>
            </tr>
        </table>
    </form>
</body>




</html>