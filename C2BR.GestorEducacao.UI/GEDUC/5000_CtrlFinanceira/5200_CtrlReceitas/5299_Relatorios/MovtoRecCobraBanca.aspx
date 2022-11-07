<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MovtoRecCobraBanca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios.MovtoRecCobraBanca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados
        {
            width: 240px;
        }
        .liPeriodo
        {
            margin-top: 5px;
        }
        .liClear { clear: both; }
        .lblDivData
        {
            margin: 0 5px;
            margin-top: 0px;
            height: 27px;
        }
        .ddlBanco, .ddlAgencia {clear: both;width:160px;}
        .ddlConta {clear: both;text-align:right;width:68px;}
        .ddlStaDocumento{clear: both;text-align:right;width:160px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <li>
                    <label for="ddlBanco" title="Banco">Banco</label>
                    <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                        ToolTip="Selecione o Banco" 
                        onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li class="liClear" style="margin-top: 10px;">
                    <label for="ddlAgencia" class="lblObrigatorio" title="Agência">Agência</label>
                    <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                        ToolTip="Selecione a Agência" 
                        onselectedindexchanged="ddlAgencia_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label for="ddlConta" title="Conta">Conta</label>
                    <asp:DropDownList ID="ddlConta" runat="server" CssClass="ddlConta"
                        ToolTip="Selecione a Conta" 
                        onselectedindexchanged="ddlConta_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label for="ddlContrato" title="Conta">Contrato</label>
                    <asp:DropDownList ID="ddlContrato" runat="server" CssClass="ddlConta"
                        ToolTip="Selecione um Contrato" 
                        onselectedindexchanged="ddlContrato_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </li>   
                <li class="liClear">
            <label title="Status" for="ddlStaDocumento"> Status</label>               
            <asp:DropDownList ID="ddlStaDocumento" 
                ToolTip="Selecione o Status do Documento" runat="server" 
                CssClass="ddlStaDocumento" AutoPostBack="True" >   
            </asp:DropDownList>       
        </li>             
        </ContentTemplate>
        </asp:UpdatePanel>
        <li  class="liClear">
            <label for="txtPeriodo" title="Período de Crédito">
                Período de Cr&eacute;dito</label>
                                                    
            <asp:TextBox ID="txtDtVenctoIni" ToolTip="Informe a Data Inicial do Crédito" CssClass="campoData" runat="server"></asp:TextBox>
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDtVenctoFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final do Crédito"></asp:TextBox>                                                                                                                 

            <asp:CompareValidator id="CompareValidator21" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDtVenctoFim"
                ControlToCompare="txtDtVenctoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
        </li>    
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
