<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="wfrmDellPOMainReport.ascx.cs" Inherits="Boundary_wfrmDellPOMainReport" %>

<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1"  align="center" width="95%" bordercolor="#66cc99">
    <tr width="50%">
        <td>
            <asp:Label ID="lblTitle" runat="server" Font-Bold=true Font-Size="20px" >Dell PO Main Report</asp:Label>
        </td>
        <td height="10px" valign="middle" width="50%">
            <table height="100%">
                <tr>
                    <td  align="right" style="width: 100%; height: 10px;">
                        <asp:imagebutton id="btnSingle" runat="server" ImageUrl="..\Images\Reporte.gif" AlternateText="Report current page to excel" OnClick="btnSingle_Click"  ></asp:imagebutton>
                    </td>
                    <td align="left" style="height: 10px">
                        <asp:Label ID="lblSingle" runat="server" >Single</asp:Label>
                    </td>
                    
                    <td  align="right" style="width: 100%; height: 10px;">
                        <asp:imagebutton id="btnAll" runat="server" ImageUrl="..\Images\Reporte.gif"  AlternateText="Report all pages to excel" OnClick="btnAll_Click"  ></asp:imagebutton>
                    </td>
                    <td align="left" style="height: 10px">
                        <asp:Label ID="lblAll" runat="server" >All</asp:Label>
                    </td>
                    
                    <td  align="right" style="width: 100%; height: 10px;">
                        <asp:imagebutton id="btnClose" runat="server" ImageUrl="..\Images\close.gif" OnClick="btnClose_Click" AlternateText="Close"  ></asp:imagebutton>
                    </td>
                    <td align="left" style="height: 10px">
                        <asp:Label ID="lblClose" runat="server" >Close</asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="1"  align="center" width="95%">
    <tr >
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblFromDate" runat="server" >From Date:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%">
            <asp:Label ID="lblFromDate1" runat="server" Text=" " ></asp:Label>
        </td>
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblToDate" runat="server" >To Date:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%" >
            <asp:Label ID="lblToDate1" runat="server" Text=" "></asp:Label>
        </td>
    </tr>
    <tr >
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblMesgID" runat="server" >Message ID:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%">
            <asp:Label ID="lblMesgID1" runat="server" Text=" "></asp:Label>
        </td>
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblPoNo" runat="server" >PoNo:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%" >
            <asp:Label ID="lblPoNo1" runat="server" Text=" "></asp:Label>
        </td>
    </tr>
    <tr >
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblUploadFlag" runat="server" >Upload Flag:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%">
            <asp:Label ID="lblUploadFlag1" runat="server" Text=" "></asp:Label>
        </td>
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblSoNo" runat="server" >SoNo:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%" >
            <asp:Label ID="lblSoNo1" runat="server" Text=" "></asp:Label>
        </td>
    </tr>
    <tr >
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblIdocNo" runat="server" >Idoc No:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%">
            <asp:Label ID="lblIdocNo1" runat="server" Text=" "></asp:Label>
        </td>
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblSFCFlag" runat="server" >In SFC Flag:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%" >
            <asp:Label ID="lblSFCFlag1" runat="server" Text=" "></asp:Label>
        </td>
    </tr>
    <tr >
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblPlantCode" runat="server" >Plant Code:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%">
            <asp:Label ID="lblPlantCode1" runat="server" Text=" "></asp:Label>
        </td>
        <td width="20%" align="center"  valign="middle">
            <asp:Label ID="lblAckFlag" runat="server" >ACK Flag:</asp:Label>
        </td>
        <td height="10px" align="center" valign="middle" width="30%" >
            <asp:Label ID="lblAckFlag1" runat="server" Text=" "></asp:Label>
        </td>
    </tr>
</table>
<asp:Panel ID="panel1" runat="server" Height="10px"></asp:Panel>
<asp:Panel ID="panel2" runat="server" > 
<asp:GridView ID="gvPO" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" PagerSettings-Visible="false" OnRowCreated="gvPO_RowCreated" AllowPaging="false" OnRowDataBound="gvPO_RowDataBound">
   <Columns>   
        <asp:TemplateField HeaderText="Index" >
             <ItemTemplate>   
                 <asp:Label ID="lblIndex" runat="server"></asp:Label>
             </ItemTemplate>
        </asp:TemplateField>        
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />     
</asp:GridView>
<table class="DataGridFont" cellSpacing="0" cellPadding="0" border="0"  align="center" width="95%">
    <tr>
        <td align="center">
            <asp:imagebutton id="btnFirst" AlternateText="First" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-first.gif"  CommandName="Page" CommandArgument="First" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:imagebutton id="btnPrevious" AlternateText="Previous" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-previous.gif"  CommandName="Page" CommandArgument="Previous" OnClick="PagebtnClick"> </asp:imagebutton>
            <asp:imagebutton id="btnNext" AlternateText="Next" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-next.gif"  CommandName="Page" CommandArgument="Next" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:imagebutton id="btnLast" AlternateText="Last" runat="server" CausesValidation="false" ImageUrl="..\Images\arrow-last.gif"   CommandName="Page" CommandArgument="Last" OnClick="PagebtnClick"></asp:imagebutton>
            <asp:Label ID="lblNow" runat="server" ></asp:Label> 
            <asp:Label ID="lblTotal" runat="server" ></asp:Label>                 
            <asp:TextBox ID="txtNo" runat="server" Width="80px"></asp:TextBox>
            頁<asp:imagebutton id="btnGo" AlternateText="Go" runat="server" ImageUrl="..\Images\go.gif" CausesValidation="false" CommandArgument="-1" CommandName="Page" OnClick="PagebtnClick"></asp:imagebutton>

        </td>
    </tr>
</table>
</asp:Panel>
