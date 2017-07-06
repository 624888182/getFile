<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmRouteSet.ascx.cs" Inherits="Boundary_wfrmRouteSet" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<script language="javascript" type="text/javascript">
<!--

function Iframe_onblur() {

}

// -->
</script>

<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
<fieldset>
    <legend>A,P路由查詢</legend>
    <table border="0" width="100%">
        <tr>
            <label>當用戶選擇機種時,自動將此機種下主板的31料號，賦給31文本框，主板的sequence_id均為1,SFC.CMCS_SFC_PCBA_BARCODE_CTL,
            選中主板料號代出默認通用A,P路由代碼,共用路由,考慮到同一機種主板料號可能會變更,但路由不變,新加料號不需要重建路由</label>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="height: 14px">
                            <hr width="100" />
                        </td>
                          查 詢
                        <td style="height: 14px">
                            <hr width="900" />
                        </td>
                    </tr>
                </table>
                <table border="0"  height="50" width="100%">
                    <tr>
                        <td >
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 機種: &nbsp;&nbsp;<asp:DropDownList
                                ID="ddlModel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                                Width="118px">
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp;有效路由: &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownListRoute"
                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRoute_SelectedIndexChanged"
                                Width="118px">
                                <asp:ListItem Value="S">SMT路由</asp:ListItem>
                                <asp:ListItem Value="T">SMT_TEST路由</asp:ListItem>
                                <asp:ListItem Value="A">Assembly路由</asp:ListItem>
                                <asp:ListItem Value="P">PACK路由</asp:ListItem>
                            </asp:DropDownList><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 31料號: &nbsp;&nbsp;<asp:DropDownList ID="DropDownListSPart"
                                runat="server" Width="117px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSPart_SelectedIndexChanged">
                            </asp:DropDownList><br />
                            <br />
                            &nbsp; &nbsp; &nbsp; 路由Code:&nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="119px">
                            </asp:DropDownList>
                            &nbsp;
                            &nbsp;<font color="red">*</font> &nbsp;默認(默認路由不允許更改,如需更新,必須由各級主管會簽)
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <hr width="100" />
                            顯示區</td>
                        <td width="50">
                            </td>
                        <td>
                            <hr width="900" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="width: 991px">
                            RouteCode1 路由表:<asp:Label ID="Label1" runat="server" BackColor="Transparent" Font-Bold="True"
                                ForeColor="Blue" Text="Label" Width="847px"></asp:Label></td>
                        <td>
                            </td>
                    </tr>
                  
                     
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
        </tr>
    </table>

    
           
   <table>
        <tr>
            <td>
                <a href="CreateRouter.aspx" target="Main">創建路由</a></td>
            <td>
                <a href="AlterRouter.aspx" target="Main">修改路由</a></td>
            <td>
                <a href="DeleteRouter.aspx" target="Main">刪除路由</a></td>
            <td>
                <a href="RouterToOrder.aspx" target="Main">綁定工單</a>
            </td>
        </tr>
    </table>
    <table style="height: 24px">
        <tr>
            <td style="width: 83px">
                功能操作區</td>
            <td style="width: 3px">
                </td>
            <td>
                <hr width="870" />
            </td>
        </tr>
        <tr>
 
        </tr>
    </table>  
          <iframe name=Main  frameborder=0  scrolling =auto src="CreateRouter.aspx" id="Iframe" style="width: 1024px; height: 350px" language="javascript" onblur="return Iframe_onblur()" ></iframe>
</fieldset>