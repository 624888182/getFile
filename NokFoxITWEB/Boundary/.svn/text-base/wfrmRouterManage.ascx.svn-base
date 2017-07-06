<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wfrmRouterManage.ascx.cs" Inherits="Boundary_wfrmRouterManage" %>
<%@ Register TagPrefix="cwc" Namespace="System.Web.UI.WebControls" Assembly="WebDataGrid" %>
<%@ Register TagPrefix="uc1" TagName="ModelTitle" Src="../WebControler/ModelTitle.ascx" %> 
<%@ Register Src="../WebControler/Controls/Calendar1.ascx" TagName="Calendar1" TagPrefix="uc2" %>
<uc1:modeltitle id="ModelTitle1" runat="server"></uc1:modeltitle> 
 <script language="javascript" type="text/javascript">  
 function Mouse_Click()  
 {  
 /*text1 = document.getElementById("textBox1");  
 text2 = document.getElementById("textBox2");  
 text3 = document.getElementById("textBox3");  
 text4 = document.getElementById("textBox4");  
  
 if(text1.value == "" || text2.value == "" || text3.value == "" || text4.value == "")  
 {  */
 //alert("请填写完整"); 
 
 }  
 </script>
 
 <script type="text/javascript">
        //把事件放在onload里，因为我不知道JS如果直接写到这儿是不是会等页面加载完才执行
        //颜色值推荐使用Hex，如 #f00 或 #ff0000
        window.onload = function(){
            GridViewColor("<%=GridView1.ClientID%>","#fff","#eee","#6df","#fd6");
        }
       
        //参数依次为（后两个如果指定为空值，则不会发生相应的事件）：
        //GridView ID, 正常行背景色,交替行背景色,鼠标指向行背景色,鼠标点击后背景色
        function GridViewColor(GridViewId, NormalColor, AlterColor, HoverColor, SelectColor)
        {
            //获取所有要控制的行
            var AllRows = document.getElementById(GridViewId).getElementsByTagName("tr");
           
            //设置每一行的背景色和事件，循环从1开始而非0，可以避开表头那一行
            for(var i=1; i<AllRows.length; i++)
            {
                //设定本行默认的背景色
                AllRows[i].style.background = i%2==0?NormalColor:AlterColor;
               
                //如果指定了鼠标指向的背景色，则添加onmouseover/onmouseout事件
                //处于选中状态的行发生这两个事件时不改变颜色
                if(HoverColor != ""){
                    AllRows[i].onmouseover = function(){if(!this.selected)this.style.background = HoverColor;}
                    if(i%2 == 0){
                        AllRows[i].onmouseout = function(){if(!this.selected)this.style.background = NormalColor;}
                    }
                    else{
                        AllRows[i].onmouseout = function(){if(!this.selected)this.style.background = AlterColor;}
                    }
                }

                //如果指定了鼠标点击的背景色，则添加onclick事件
                //在事件响应中修改被点击行的选中状态
                if(SelectColor != "")
                {
                    AllRows[i].onclick = function()
                    {
                        this.style.background = this.style.background==SelectColor?HoverColor:SelectColor;
                        this.selected = !this.selected;
                        
                    }
                }
            }
        }
    </script>

 
<fieldset style="width: 970px">
    <legend>路由設置與查詢</legend>
    <table border="0" width="100%" id="TABLE1">
        <tr>
            <td>
                機種：<asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" Width="118px">
                </asp:DropDownList>
            </td>
            <td>
                料號：<asp:DropDownList
                    ID="DropDownListSPart" runat="server" AutoPostBack="True" 
                    Width="117px">
                </asp:DropDownList>
             </td>
             <td>
                工單：<asp:TextBox ID="TextBoxOrder" runat="server"></asp:TextBox></td>
             <td>
                線別：<asp:dropdownlist id="ddlLine" runat="server" Width="155px">
				<asp:ListItem></asp:ListItem>
				<asp:ListItem Value="LINE1">LINE1</asp:ListItem>
				<asp:ListItem Value="LINE2">LINE2</asp:ListItem>
				<asp:ListItem Value="LINE3">LINE3</asp:ListItem>
				<asp:ListItem Value="LINE4">LINE4</asp:ListItem>
				<asp:ListItem Value="LINE5">LINE5</asp:ListItem>
				<asp:ListItem Value="LINE6">LINE6</asp:ListItem>
				<asp:ListItem Value="LINE7">LINE7</asp:ListItem>
				<asp:ListItem Value="LINE8">LINE8</asp:ListItem>
				<asp:ListItem Value="LINE9">LINE9</asp:ListItem>
				<asp:ListItem Value="LINE10">LINE10</asp:ListItem>
				<asp:ListItem Value="LINE11">LINE11</asp:ListItem>
				<asp:ListItem Value="LINE12">LINE12</asp:ListItem>
				<asp:ListItem Value="LINE13">LINE13</asp:ListItem>
				<asp:ListItem Value="LINE14">LINE14</asp:ListItem>
				<asp:ListItem Value="LINE15">LINE15</asp:ListItem>
				<asp:ListItem Value="LINE16">LINE16</asp:ListItem>
				<asp:ListItem Value="LINE17">LINE17</asp:ListItem>
				<asp:ListItem Value="LINE18">LINE18</asp:ListItem>
				<asp:ListItem Value="LINE19">LINE19</asp:ListItem>
				<asp:ListItem Value="LINE20">LINE20</asp:ListItem>
				<asp:ListItem Value="LINE21">LINE21</asp:ListItem>
				<asp:ListItem Value="LINE22">LINE22</asp:ListItem>
				<asp:ListItem Value="LINE23">LINE23</asp:ListItem>
				<asp:ListItem Value="LINE24">LINE24</asp:ListItem>
				<asp:ListItem Value="LINE25">LINE25</asp:ListItem>
				<asp:ListItem Value="LINE26">LINE26</asp:ListItem>
				<asp:ListItem Value="LINE27">LINE27</asp:ListItem>
			</asp:dropdownlist></td>
          </tr>
          <tr>
             <td style="height: 71px">
                路由代碼：<asp:TextBox ID="TextBoxRouteID" runat="server" BackColor="#E0E0E0" Enabled="False"></asp:TextBox></td>
             <td style="height: 71px">
                <asp:label ID="label1"  Text=" 失效日：" runat="server" ></asp:label><uc2:calendar1 id="tbInvalidDate" runat="server"></uc2:calendar1>
             <td style="height: 71px">
                 <asp:label ID="label2"  Text=" 描述：" runat="server" ></asp:label><br />
                 <asp:TextBox ID="TextBoxDescription" runat="server" Height="46px" TextMode="MultiLine"
                     Width="201px"></asp:TextBox></td>
                     
               <td style="height: 71px">
                   &nbsp; &nbsp; &nbsp;
               <EMBED align=middle src="../Images/w_flash_3.swf"  type=application/x-shockwave-flash QUALITY="high" ALLOWSCRIPTACCESS="sameDomain" style="height: 80px; width: 182px;" wmode="transparent"></EMBED>

               </td>
         </tr>
         <tr>
            <td style="height: 26px">
            <asp:Label ID="LabelFlag" runat="server" Visible="False" Width="54px"></asp:Label>
                
            </td>
            <td style="height: 26px">
                &nbsp; &nbsp; &nbsp;<asp:Button ID="ButtonFind" runat="server" Text="查找路由" OnClick="ButtonFind_Click" />
                &nbsp;&nbsp; &nbsp;
                <asp:Button ID="ButtonCopy" runat="server" Text="復制路由" OnClick="ButtonCopy_Click" /></td>
                <td style="height: 26px">
                    &nbsp;</td>
                    
         </tr>
        </table>
      </fieldset>
<br />
<asp:Panel ID="PanelCopy" runat=server Width="934px" Visible=false Direction="LeftToRight">
    <fieldset style="width: 970px">
    <legend>路由復制功能</legend>
    <table border="0" width="100%" id="TABLE2">
        <tr>
            <td>
                機種：<asp:DropDownList ID="DropDownListModel" runat="server" AutoPostBack="True"
                     Width="118px" OnSelectedIndexChanged="DropDownListModel_SelectedIndexChanged" >
                </asp:DropDownList>
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                料號：<asp:DropDownList
                    ID="DropDownListPPart" runat="server" AutoPostBack="True" Width="117px" OnSelectedIndexChanged="DropDownListPPart_SelectedIndexChanged">
                </asp:DropDownList>
             </td>
             <td style="width: 228px">
                工單：<asp:DropDownList ID="TextBoxCopyOrder" runat="server" Width="131px"></asp:DropDownList>
                </td>
             <td>
                線別：<asp:dropdownlist id="DropdownlistLine" runat="server" Width="155px">
			</asp:dropdownlist></td>
          </tr>
          
         <tr>
            <td>
            <asp:Label ID="Label5" runat="server" Visible="False" Width="54px"></asp:Label>
                
            </td>
            <td>
                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="ButtonStartCopy" runat="server" Text="復制" Width="82px" OnClick="ButtonStartCopy_Click" />
                &nbsp; &nbsp; &nbsp;<asp:Button ID="ButtonCancelCopy" runat="server" Text="取消" Width="88px" OnClick="ButtonCancelCopy_Click" /></td>
             <td>
                 &nbsp;</td>
                    
         </tr>
        </table>
      </fieldset>
</asp:Panel>
       <fieldset style="width: 973px">
       <legend>路由設置與操作</legend>
  
        <table>
         <tr> 
                <td style="width: 485px">
                    &nbsp;<fieldset>
                    <legend><span style="color: #ff6666">路由主項設置與操作</span></legend>
                    <asp:Button ID="ButtonAddRow" runat="server" Text="增加行" OnClick="ButtonAddRow_Click" Width="75px" /><br />
                    <asp:Panel runat=server ID="Panel1" ScrollBars=Vertical Height="340px">
                        <asp:GridView ID="GridView1" runat="server"  CssClass="DataGridFont" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="478px" OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="LabelID" runat="server" Width="50px"></asp:Label>
                                </ItemTemplate>
                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <img src="../Images/zip.gif" style="height: 15px" />&nbsp;
                                    <asp:DropDownList ID="DropDownListName" runat="server"  DataSourceID="SqlDataSource1" DataTextField="STATION_NAME" DataValueField="STATION_ID" OnSelectedIndexChanged="DropDownListName_SelectedIndexChanged" AutoPostBack="True" Width="139px" >
                                    </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString1 %>"
                                        ProviderName="<%$ ConnectionStrings:ConnectionString1.ProviderName %>" SelectCommand='SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM "SFC_PRODUCTION_STATIONS"  ORDER BY "STATION_NAME"'>
                                    </asp:SqlDataSource>
                                    &nbsp; &nbsp;
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disable">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxDisable" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="ButtonFile" Text="顯示依存性" style="display:none"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Click me">
                                <ItemTemplate>
                                     <asp:Label ID="LabelSelect" runat="server" ></asp:Label>
                                     <asp:Image ID="passShow" runat="server" ImageUrl="~/Images/cursor.jpg" Visible=false Height="14px" Width="25px" /> 

                                 </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <RowStyle BackColor="White" ForeColor="#330099" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099"/>
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"   CssClass="DataGridFixedHeader" />
                    </asp:GridView>
                    </asp:Panel>
                    </fieldset>
                </td>
                <td style="width: 430px">
                <br />
                   <fieldset>
                    <legend><span style="color: #ff6666">路由依存項設置與操作</span></legend>
                    <asp:Button ID="ButtonReRowAdd" runat="server" Text="增加行"  Width="75px" OnClick="ButtonReRowAdd_Click" />
                    <asp:Panel runat=server ID="Panel2" ScrollBars=Vertical Height="340px">
                    
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="426px" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView2_RowDataBound">
                         <Columns>
                             <asp:TemplateField HeaderText="ID" Visible="False">
                                 <ItemTemplate>
                                     <asp:Label ID="LabelID" runat="server" Text='<%# Bind("Station_Name") %>' Width="110px"></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Flow">
                                <ItemTemplate>
                                    <img src="../Images/zip.gif" style="height: 15px" />
                                    <asp:DropDownList ID="DropDownListReName" runat="server" DataSourceID="SqlDataSource1"
                                        DataTextField="STATION_NAME" DataValueField="STATION_ID" Width="128px">
                                    </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString1 %>"
                                        ProviderName="<%$ ConnectionStrings:ConnectionString1.ProviderName %>" SelectCommand='SELECT STATION_CODE STATION_ID, STATION_DESC STATION_NAME FROM "SFC_PRODUCTION_STATIONS"  ORDER BY "STATION_NAME"'>
                                    </asp:SqlDataSource>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListReType" runat="server" Width="112px">
                                        <asp:ListItem> </asp:ListItem>
                                        <asp:ListItem>FROM</asp:ListItem>
                                        <asp:ListItem>TO</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disable">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxReDisable" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"  CssClass="DataGridFixedHeader" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>
                     </asp:Panel>
                     </fieldset></td>
          </tr>
        </table>
        <table>
            <tr>
              <td style="width: 437px">
              
              </td>
              <td style="width: 271px">
                  <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="保存路由" />
                  &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="刪除路由" OnClick="ButtonDelete_Click1" />&nbsp;
              </td>
            </tr>
        </table>
 </fieldset>


