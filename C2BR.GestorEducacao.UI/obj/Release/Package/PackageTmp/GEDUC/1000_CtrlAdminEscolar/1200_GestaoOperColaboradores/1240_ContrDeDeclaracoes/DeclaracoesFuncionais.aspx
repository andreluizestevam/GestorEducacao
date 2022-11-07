<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DeclaracoesFuncionais.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1200_GestaoOperColaboradores._1240_ContrDeDeclaracoes.DeclaracoesFuncionais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liFuncionarios,.liUnidade,.liTipoCol{margin-top: 5px;width: 200px;}        
        .liAnoBase {margin-top: 5px;}
        .liFuncionarios{clear: both;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>

        <li class="liTipoCol">
            <label class="lblObrigatorio" for="ddlTipoColaborador">
                Tipo do Colaborador</label>
            <asp:DropDownList ID="ddlTipoColaborador" CssClass="ddlTipoCol" runat="server" 
                ToolTip="Selecione o Tipo do Colaborador" AutoPostBack="True" 
                onselectedindexchanged="ddlTipoColaborador_SelectedIndexChanged" Width="120px" />
        </li>

        <li class="liFuncionarios">
            <label class="lblObrigatorio" for="txtFuncionarios">
                Funcionários</label>
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Funcionário">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" CssClass="validatorField"
            ControlToValidate="ddlFuncionarios" Text="*" 
            ErrorMessage="Campo Funcionário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>

        <li class="liFuncionarios">
            <label class="lblObrigatorio" for="drpTipoDeclaracao">
                Tipo de Declaração</label>
            <asp:DropDownList ID="drpTipoDeclaracao" CssClass="ddlTipoCol" runat="server" 
                ToolTip="Selecione o Tipo de Declaração" Width="150px">
                <asp:ListItem Value="AEACE">Declaração Aviso de Encerramento Antecipado do Contrato de Experiência</asp:ListItem>
                <asp:ListItem Value="AENCTE">Declaração Aviso de Encerramento Normal do Contrato de Trabaho em Experiência</asp:ListItem>
                <asp:ListItem Value="DAF">Declaração Aviso de Férias</asp:ListItem>
                <asp:ListItem Value="ATCE">Declaração Aviso de Término do Contrato de Experiência</asp:ListItem>
                <asp:ListItem Value="APDEJC">Declaração Aviso Prévio para Dispensa do Empregado Justa Causa</asp:ListItem>
                <asp:ListItem Value="CAI">Declaração Concessão de Adicional de Insalubridade</asp:ListItem>
                <asp:ListItem Value="AFA">Declaração de Aviso de Férias Antecipada</asp:ListItem>
                <asp:ListItem Value="DCAP">Declaração de Dispensa do Cumprimento do Aviso Prévio</asp:ListItem>
                <asp:ListItem Value="NAC">Declaração de não Acumulação de Cargo</asp:ListItem>
                <asp:ListItem Value="DND">Declaração de não Demissão</asp:ListItem>
                <asp:ListItem Value="DNPB">Declaração de não Possuir Bens</asp:ListItem>
                <asp:ListItem Value="DNPRPM">Declaração de não Possui Renda Para Menores</asp:ListItem>
                <asp:ListItem Value="DNTSPEFP">Declaração de não ter Sofrido Penalidades no Exercício da Função Pública</asp:ListItem>
                <asp:ListItem Value="DEMP">Declaração Empregador</asp:ListItem>
                <asp:ListItem Value="DEIR">Declaração Entrega do Imposto de Renda</asp:ListItem>
                <asp:ListItem Value="DPE">Declaração Pedido de Exoneração</asp:ListItem>
                <asp:ListItem Value="DNPR">Declaração que não Possui Renda</asp:ListItem>
                <asp:ListItem Value="RDV">Requerimento de Direitos e Vantagens - RDV</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
