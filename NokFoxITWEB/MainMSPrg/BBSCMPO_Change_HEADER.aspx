<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSCMPO_Change_HEADER.aspx.cs" Inherits="BBSCMPOHEADER"  StyleSheetTheme="SkinFile" %>
<%@ Register TagPrefix="uc1" TagName="HeaderBar" Src="BBRY_Header.ascx" %>
 
<%@ Register src="BBRY_Header.ascx" tagname="BBRY_Header" tagprefix="uc2" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
    <style type="text/css">
        .style1
        {
            width: 100%;

        }
        .style2
        {
            width: 934px;
        }
        .style3
        {
            width: 64px;
        }
        .style4
        {
            width: 629px;
        }
        .style5
        {
            width: 352px;
        }
        #Select2
        {
            width: 77px;
        }
        #Select1
        {
            width: 98px;
        }
        #Downlist2
        {
            width: 71px;
        }
    </style>
    
    
    
 




    
    
    
    
</head>
<body  class="homePageBg">
    <form id="form1" runat="server">
    
 <!--  <uc1:HeaderBar ID="Header1" runat="server"></uc1:HeaderBar>   -->
    <uc2:BBRY_Header ID="BBRY_Header1" runat="server" />
    <table cellpadding="2" class="style1">
        <tr>
            <td class="style2">
                <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#FF9933" 
                    Height="21px" Width="137px">
                    <asp:ListItem>Document_ID</asp:ListItem>
                </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="211px"></asp:TextBox>
&nbsp;&nbsp;
                <asp:Button ID="BtnGO" runat="server" BackColor="#CC6600" BorderColor="#FF9933" 
                    Font-Bold="True" ForeColor="White" Text="GO" onclick="BtnGO_Click" />
            </td>
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Text="ShipFrom_DUNS:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" BackColor="#FF9900" 
                    Width="134px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Receive_Date:"></asp:Label>
                <asp:TextBox ID="txtBeginDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="(Beginning)"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtendDate" runat="server" BackColor="#FFCC66" 
                    ForeColor="Black" onclick="showCalendar();" 
                    onkeypress="javascript:event.returnValue=false;" Width="100px"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="(Ending)"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td class="style4">
&nbsp;&nbsp;
                
                    <select id="Downlist1" name="D3" onchange="ChangeValue()" style="background-color:#FFCC66" runat="server">
                        <option selected="selected" value="Upload_SAP">Upload_SAP</option>
                        <option value="Price_Flag">Price_Flag</option>
                    </select>&nbsp; <select id="Downlist2" name="D4"  style="background-color:#FFCC66" runat="server">
                <option selected="selected" value="ALL">ALL</option>
                <option  value="Y">Y</option>
                <option  value="N">N</option>
                <option  value="E">E</option>
                <option  value="M">M</option>
                </select>&nbsp; <asp:CheckBox ID="CheckBox1" runat="server" Text="(Confirm All?)" />
                &nbsp;
                <asp:DropDownList ID="DropDownList5" runat="server" BackColor="#FF9900" 
                    Width="145px" >
                </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnsap" runat="server" BackColor="#CC6600" BorderColor="#FF9933" 
                    OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')" style="height: 26px"
                    Font-Bold="True" ForeColor="White" Text="SAP" onclick="btnsap_Click" />
                &nbsp;<asp:Button ID="btnsbi" runat="server" BackColor="#CC6600" 
                 OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')" style="height: 26px"
                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" Text="SBI" 
                    onclick="btnsbi_Click" />
                &nbsp;<asp:Button ID="btnissue" runat="server" BackColor="#CC6600" 
                 OnClientClick="return confirm('You will filter the issue SBI price? Are you sure to execute the process?')" style="height: 26px"
                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" Text="Issue" 
                    onclick="btnissue_Click" />
                
&nbsp;
                &nbsp;&nbsp;&nbsp;
                &nbsp;
                </td>
            <td align="right" class="style5">
                <asp:Label ID="Label6" runat="server" Text="Add SAP_Price:"></asp:Label>
                <asp:CheckBox ID="CheckBox2" runat="server" Text="(Add All?)" />
                <asp:TextBox ID="TextBox4" runat="server" Width="117px"></asp:TextBox>
                <asp:Button ID="btnadd" runat="server" BackColor="#CC6600" 
                OnClientClick="return confirm('Please attention your operation is invalid for the data that have been confirmed! Are you sure to execute the process?')" style="height: 26px"
                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" Text="Add" 
                    onclick="btnadd_Click" />
                <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Button" />
                <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="Button" />
            </td>
            <td align="right">
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Open</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table cellpadding="2" class="style1">
        <tr>
            <td>
             

            
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    Height="114px"  Width="100%"
                    AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowdatabound="GridView1_RowDataBound" PageSize="20" 
                    onrowcreated="GridView1_RowCreated" DataKeyNames="ID" SkinID="gvMain">
                    
                   
                    
                    <Columns>
                        <asp:TemplateField >
                        <HeaderTemplate>
                              <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" 
                                  oncheckedchanged="CheckBox4_CheckedChanged" />
                        </HeaderTemplate>
                                
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox3" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NO">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CREATIONDT">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CREATIONDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TESTDATAINDICATOR">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TESTDATAINDICATOR") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SENDERID">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SENDERID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POID">
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"POID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BUYERPOSTDT">
                            <ItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BUYERPOSTDT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TRANSMISSIONINDICATOR">
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TRANSMISSIONINDICATOR") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SELLERPARTYID">
                            <ItemTemplate>
                                <asp:Label ID="Label16" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SELLERPARTYID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLASSIFICATIONCODE">
                            <ItemTemplate>
                                <asp:Label ID="Label17" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CLASSIFICATIONCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TRANSFERLOCATIONNAME">
                            <ItemTemplate>
                                <asp:Label ID="Label18" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TRANSFERLOCATIONNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UPLOAD_SAP">
                            <ItemTemplate>
                                <asp:Label ID="Label19" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UPLOAD_SAP") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SAP_LOG">
                            <ItemTemplate>
                                <asp:Label ID="Label20" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SAP_LOG") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO_CREATE_MT_UF1 ">
                            <ItemTemplate>
                                <asp:Label ID="Label21" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PO_CREATE_MT_UF1") %>'></asp:Label>
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="SENDFLAG ">
                            <ItemTemplate>
                                <asp:Label ID="Label23" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SENDFLAG") %>'></asp:Label>
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" Text="Detail" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button1_Click" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        
                        <asp:TemplateField HeaderText="Clear">
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" Text="Clear" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button_Clear" CausesValidation="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        
                        
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                               
                             
                                <asp:Button ID="Button3" runat="server" Text="Confirm" BackColor="#CC6600"
                                    BorderColor="#FF9933" Font-Bold="True" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>'
                                    onclick="Button_Confirm" CausesValidation="False" />
                                    
   <!--
                                <asp:LinkButton ID="lbtnTitle" runat="server" CommandName="Detail" Text=''></asp:LinkButton>
                                      -->   
                                         
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"    />
                        </asp:TemplateField>
                        
                        
                        
                    </Columns>
                    <PagerStyle />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle Font-Bold="False" />
                     <PagerStyle CssClass="LeftPagerStyle" />
                </asp:GridView>
                
              
            </td>
        </tr>
    </table>
    
 
     
     
     
    </form>
    </body>
</html>
