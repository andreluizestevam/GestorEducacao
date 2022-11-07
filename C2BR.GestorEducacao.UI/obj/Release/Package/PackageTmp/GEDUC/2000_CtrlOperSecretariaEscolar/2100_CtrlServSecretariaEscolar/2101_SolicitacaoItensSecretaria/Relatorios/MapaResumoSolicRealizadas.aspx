<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaResumoSolicRealizadas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.Relatorios.MapaResumoSolicRealizadas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 235px; }
        .liUnidade, .liTipoMapaSolicitacao, .liAnoBase
        {
            margin-top: 5px;
            width: 235px;
        }        
        .ddlTipoMapaSolicitacao { width:170px; }
        .lblDivAno
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
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>                    
        <li class="liTipoMapaSolicitacao">
            <label class="lblObrigatorio" for="txtTipoMapaSolicitacao">
                Situação</label>               
            <asp:DropDownList ID="ddlTipoMapaSolicitacao" CssClass="ddlTipoMapaSolicitacao" 
                runat="server" 
                onselectedindexchanged="ddlTipoMapaSolicitacao_SelectedIndexChanged" 
                AutoPostBack="True">
                <asp:ListItem Selected="True" Value="A">Mapa Anual de Solicitações</asp:ListItem>           
                <asp:ListItem  Value="M">Mapa Mensal de Solicitações</asp:ListItem>                
            </asp:DropDownList>
        </li>                   
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase">
                Ano Base</label>       
            <asp:DropDownList ID="ddlAnoIni" CssClass="campoAno" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAnoIni" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoIni" Text="*" 
            ErrorMessage="Campo Ano Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label id="lblDivAno" class="lblDivAno" runat="server" >à</asp:label>
            <asp:DropDownList ID="ddlAnoFim" CssClass="campoAno" runat="server">    
            </asp:DropDownList>
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="ddlAnoIni"
                ControlToCompare="ddlAnoFim"
                Type="Integer"       
                Operator="LessThanEqual"      
                ErrorMessage="Ano Final não pode ser menor que Ano Inicial." >
            </asp:CompareValidator >           
        </li>    
        </ContentTemplate>
        </asp:UpdatePanel>            
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">    
</asp:Content>
