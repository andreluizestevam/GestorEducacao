<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoChequeEmitidoInstituicao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5819_Relatorios.RelacaoChequeEmitidoInstituicao" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 275px; }
        .liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 275px;            
        }                                   
        .liAnoRefer, .liTurma, .liResponsavel, .liTpInadimplencia 
        {
        	clear:both;
        	margin-top:5px;
        }         
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }      
        .ddlTpInadimplencia { width: 165px; }  
        .ddlAgrupador { width:200px; }
        
        .liPeriodoAte, .liAux { margin-top: 18px; }
        .liSituacao { clear: both; margin-top: -5px; }
        .ddlTpCliente { width: 75px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>    
        <li class="liTpInadimplencia">
            <label class="lblObrigatorio" title="Tipo de Cliente">
                Tipo de Cliente</label>               
            <asp:DropDownList ID="ddlTpCliente" ToolTip="Selecione um Tipo de Cliente" CssClass="ddlTpCliente" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlTpCliente_SelectedIndexChanged">        
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
            <asp:ListItem Value="C">Cliente</asp:ListItem>
            <asp:ListItem Value="R">Responsável</asp:ListItem>
            </asp:DropDownList>            
        </li>             
        <li id="liResponsavel" class="liResponsavel" runat="server">
            <label title="Nome">
                Nome</label>               
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione um Nome" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                    
        </li>         
        <li id="liPeriodo" class="liPeriodo">
            <label class="lblObrigatorio" title="Período" for="txtPeriodo">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData" runat="server"></asp:TextBox>  
            
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
        <li class="liSituacao">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" Width="75px" runat="server">        
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                <asp:ListItem Value="Q">Baixado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
            </asp:DropDownList>            
        </li> 
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
