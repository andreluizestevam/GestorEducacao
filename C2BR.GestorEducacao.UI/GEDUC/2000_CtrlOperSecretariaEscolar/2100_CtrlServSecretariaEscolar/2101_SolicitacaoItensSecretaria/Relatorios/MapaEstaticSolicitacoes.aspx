<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaEstaticSolicitacoes.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.Relatorios.MapaEstaticSolicitacoes" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 235px; }
        .liUnidade, .liAnoBase, .liTipo
        {
            margin-top: 5px;
            width: 235px;
        }
        .liSituacao
        {
            margin-top: -5px;
            width: 235px;	
        }          
        .ddlTipo { width:80px; }
        .ddlSituacao { width:85px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label Class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True">
            </asp:DropDownList>
        </li>                            
        
        <li class="liAnoBase">
            <label Class="lblObrigatorio" for="txtAnoBase">
                Ano Base</label>       
            <asp:TextBox ID="txtAnoBase" CssClass="txtAno" runat="server"></asp:TextBox>   
            <asp:RequiredFieldValidator ID="rfvtxtAnoBase" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBase" Text="*" 
            ErrorMessage="Campo Ano Base é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                           
        </li>  
        
        <li class="liSituacao">
            <label Class="lblObrigatorio" for="txtSituacao">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server">
                <asp:ListItem Selected="True" Value="S">Todas</asp:ListItem>
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>                
                <asp:ListItem Value="T">Em Trâmite</asp:ListItem>
                <asp:ListItem Value="F">Finalizada</asp:ListItem>                             
                <asp:ListItem Value="C">Cancelada</asp:ListItem>               
            </asp:DropDownList>
        </li> 
        
        <li class="liTipo">
            <label Class="lblObrigatorio" for="txtTipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server">
                <asp:ListItem Value="A">Analítico</asp:ListItem>
                <asp:ListItem Value="D">Diferença</asp:ListItem>
            </asp:DropDownList>                   
        </li>  
              
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        jQuery(function($){
           $(".txtAno").mask("9999");           
        });
    </script>
</asp:Content>
