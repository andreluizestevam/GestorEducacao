<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ExtratSolicPorAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.Relatorios.ExtratSolicPorAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .liAlunos, .liUnidade, .liTipoSolicitacao, .liPeriodo, .liSituacao
        {
            margin-top: 5px;
            width: 260px;
        }        
        .liAlunos { clear: both; }    
        .ddlSituacao { width:85px; }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li class="liAlunos">
            <label class="lblObrigatorio" for="txtAlunos">
                Alunos</label>                    
            <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>
        </li>  
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <li class="liTipoSolicitacao">
            <label class="lblObrigatorio" for="txtTipoSolicitacao">
                Tipo de Solicitação</label>                    
            <asp:DropDownList ID="ddlTipoSolicitacao" CssClass="ddlTipoSolicitacao" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTipoSolicitacao" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTipoSolicitacao" Text="*" 
            ErrorMessage="Campo Tipo Avaliação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        
        <li class="liSituacao">
            <label class="lblObrigatorio" for="txtSituacao">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server">
                <asp:ListItem Selected="True" Value="S">Todas</asp:ListItem>
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                <asp:ListItem Value="T">Em Trâmite</asp:ListItem>
                <asp:ListItem Value="F">Finalizada</asp:ListItem>
                <asp:ListItem Value="E">Entregue</asp:ListItem>                
                <asp:ListItem Value="C">Cancelada</asp:ListItem>               
            </asp:DropDownList>
        </li>  
        
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"></asp:TextBox>     
            
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
              
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
