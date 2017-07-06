<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" CodeFile="WFrmCPK.ascx.cs" Inherits="SFCQuery.Boundary.WFrmCPK" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<!-- 
<script language="javascript" type="text/javascript">
 function showNext(sid,obj)
        {
          if(sid==null || sid=="" || sid.length<1)return;
          var slt =document.getElementById(obj);
          var v = WFrmCPK.getTestStation(sid).value; // 类的名称
          if (v != null){     
          if(v != null && typeof(v) == "object"&&v.Tables!=null)
                    {
                        slt.length = 0;
                        slt.options.add(new Option("请选择",0));
                        //加了个“请选择”主要为了触发onchange事件
                        for(var i=0; i<v.Tables[0].Rows.length; i++)
　　　　                {
　　　　                    var txt = v.Tables[0].Rows[i].TXT; //这个地方需要注意区分大小写
　　　　　　                var vol = v.Tables[0].Rows[i].VOL; //跟dataset表的列名称要一致
　　　　　　                slt.options.add(new Option(txt,vol));
　　　　                }
                    }
           }   
           return;
        }
</script>
-->
<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="100%" align="center"
	border="0">
	<tr>
		<td colSpan="8" height="20">
			<hr>
		</td>
	</tr>
	<tr>
		<TD style="HEIGHT: 10px" align="right" width="10%"><FONT face="宋体"><asp:label id="lblModel" runat="server"></asp:label></FONT></TD>
		<td style="HEIGHT: 10px" width="10%"><asp:dropdownlist id="DropDownList1" runat="server" Width="100%" AutoPostBack="True"></asp:dropdownlist></td>
		<td style="HEIGHT: 10px" align="right" width="10%"><asp:label id="lblTStation" runat="server"></asp:label></td>
		<td style="HEIGHT: 10px"><asp:dropdownlist id="DropDownList2" runat="server" Width="80%" AutoPostBack="True"></asp:dropdownlist></td>
		<td style="HEIGHT: 10px" align="right" width="10%"><asp:label id="lblContent" runat="server"></asp:label></td>
		<td style="HEIGHT: 10px" colSpan="2"><asp:dropdownlist id="DropDownList3" runat="server" Width="70%" AutoPostBack="True"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td colSpan="7">
			<hr>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 17px" width="20%" colSpan="2"><FONT face="宋体"><asp:label id="lblcountname" runat="server" Width="100%"></asp:label></FONT></td>
		<td style="HEIGHT: 17px" align="right" width="10%"><asp:label id="lblWO" Runat="server"></asp:label></td>
		<td style="HEIGHT: 17px"><asp:textbox id="txtORDER" Width="80%" Runat="server"></asp:textbox></td>
		<td style="HEIGHT: 17px" align="right" width="10%"><asp:label id="lblLine" Runat="server"></asp:label></td>
		<td style="HEIGHT: 17px" width="20%"><asp:dropdownlist id="dpdLINE" runat="server" Width="80%"></asp:dropdownlist></td>
		<td style="HEIGHT: 17px" width="20%"><FONT face="宋体"></FONT></td>
	</tr>
	<tr>
		<td style="HEIGHT: 41px" width="20%" colSpan="2"><FONT face="宋体"><asp:label id="lblacount" runat="server" Width="100%"></asp:label></FONT></td>
		<td style="HEIGHT: 41px" align="right" width="10%"><asp:label id="lblEmp" Runat="server"></asp:label></td>
		<td style="HEIGHT: 41px"><asp:textbox id="txtEMPLOYEE" Width="80%" Runat="server"></asp:textbox></td>
		<td style="HEIGHT: 41px" align="right" width="10%"><asp:label id="lblTestPC" Runat="server"></asp:label></td>
		<td style="HEIGHT: 41px" width="20%"><asp:dropdownlist id="dpdCOMPUTER" runat="server" Width="80%" AutoPostBack="False"></asp:dropdownlist></td>
		<td style="HEIGHT: 41px" width="20%"><FONT face="宋体"></FONT></td>
	</tr>
	<tr>
		<td width="20%" colSpan="2"><FONT face="宋体"></FONT></td>
		<td align="right" width="10%"><asp:label id="lblSTARTTIME" Runat="server"></asp:label></td>
		<td id="startDate">
            &nbsp;<uc2:Calendar1 ID="txtSTARTTIME" runat="server" />
		</td>
		<td align="right" width="10%"><asp:label id="lblENDTIME" Runat="server"></asp:label></td>
		<td id="endDate" width="20%">
            &nbsp;<uc2:Calendar1 ID="txtEndDate" runat="server" />
		</td>
		<td align="center" width="20%"><asp:button id="btnQUERY" Width="80px" Runat="server"></asp:button><FONT face="宋体">&nbsp;
			</FONT>
			<asp:button id="btnOUTPUT" Width="80px" Runat="server"></asp:button></td>
	</tr>
</table>
<HR>
<TABLE width="100%" align="center">
	<tr>
		<td vAlign="top">
			<table class="DataGridFont" cellSpacing="0" cellPadding="0" width="80%" align="center">
				<tr>
					<td align="center"><FONT face="宋体"></FONT></td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<div id="showtext"></div>
						<asp:datagrid id="dgcpk1" runat="server" Width="1086px" BackColor="White" BorderWidth="1px" BorderColor="#CCCCCC"
							Font-Size="X-Small" Font-Names="Verdana" CssClass="DataGridFont" ShowFooter="True" BorderStyle="None"
							PageSize="20">
							<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle ForeColor="#000066" BackColor="Cornsilk"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#006699"></HeaderStyle>
							<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
