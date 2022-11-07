﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoDespesasFixas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5499_Relatorios.RelacaoDespesasFixas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 275px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;                        
        }                                   
        .liNomeCliente
        {
        	width:300px;
        	margin-top:-5px; 	
        }
        .liTpDocumento, .liStaDocumento
        {
        	margin-top:-5px;
        	clear:both;
        }
        .liCNPJ, .liCPF
        {
        	margin-left:5px;
        	margin-top: -5px;
        }
        .liNumDoc
        {        	
        	clear:both;
        	margin-top:5px;
        }        
        .liPeriodo
        {
        	margin-left:5px;
        	margin-top:5px;
        }           
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }      
        .ddlStaDocumento { width: 85px; }  
        .txtNumDoc { width: 80px; }
        .ddlTpDocumento { width:55px; }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecine a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <li class="liNumDoc">
            <label class="label" title="Contrato">
                Contrato</label>
            <asp:TextBox ID="txtNumDoc" ToolTip="Informe o Contrato" CssClass="txtNumDoc" runat="server"></asp:TextBox>                
        </li>        
        <li class="liPeriodo">
            <label class="lblObrigatorio" title="Período" for="txtPeriodo">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial do Período" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final do Período" CssClass="campoData" runat="server"></asp:TextBox>  
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
               
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                                                                                                 
        </li>            
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>     
        <li class="liTpDocumento">
            <label class="lblObrigatorio" title="Tipo do Documento">
                Tipo Documento</label>               
            <asp:DropDownList ID="ddlTpDocumento" ToolTip="Selecione o Tipo do Documento" CssClass="ddlTpDocumento" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlTpDocumento_SelectedIndexChanged">   
            <asp:ListItem Selected="True" Value="0">CNPJ</asp:ListItem>        
            <asp:ListItem Value="1">CPF</asp:ListItem>
            </asp:DropDownList>            
        </li>                     
        <li id="liCNPJ" class="liCNPJ" runat="server">
            <label class="label" title="CNPJ">
                CNPJ</label>
            <asp:TextBox ID="txtCNPJ" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server"></asp:TextBox>                
        </li>        
        <li id="liCPF" class="liCPF" runat="server">
            <label class="label" title="CPF">
                CPF</label>
            <asp:TextBox ID="txtCPF" ToolTip="Informe o CPF" CssClass="txtCPF" runat="server"></asp:TextBox>                
        </li>        
        <li class="liNomeCliente">
            <label class="label" title="Nome do Fornecedor">
                Nome Fornecedor</label>
            <asp:TextBox ID="txtNomeCliente" ToolTip="Informe o Nome do Fornecedor" CssClass="txtNomePessoa" runat="server"></asp:TextBox>
        </li>       
        </ContentTemplate>
        </asp:UpdatePanel>                                       
        <li class="liStaDocumento">
            <label class="lblObrigatorio" title="Documento(s)">
                Documento(s)</label>               
            <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o(s) Documento(s)" CssClass="ddlStaDocumento" runat="server">   
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>        
            <asp:ListItem Value="A">Em aberto(s)</asp:ListItem>
            <asp:ListItem Value="Q">Quitado(s)</asp:ListItem>
            <asp:ListItem Value="C">Cancelado(s)</asp:ListItem>
            </asp:DropDownList>            
        </li>                                                   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtCPF").mask("999.999.999-99");
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });

        jQuery(function($){
           $(".txtCPF").mask("999.999.999-99");   
           $(".txtCNPJ").mask("99.999.999/9999-99");                       
        });               
    </script>
</asp:Content>