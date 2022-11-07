<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8272_Classificacoes.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
            margin: 30px 0 0 390px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoSitua" />
    <ul class="ulDados">
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Nome da Classificação de Prontuários">
                Classificação</label>
            <asp:TextBox runat="server" ID="txtNomeClassificacao" Width="220px" MaxLength="200" ToolTip="Nome da Classificação de Prontuários Médicos"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvTxtNomeClassificacao" ControlToValidate="txtNomeClassificacao" Display="Dynamic"
                ErrorMessage="Nome é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Sigla da Classificação de Prontuários">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSiglaClassificacao" Width="85px" MaxLength="15" ToolTip="Sigla da Classificação de Prontuários Médicos"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvTxtSiglaClassificacao" ControlToValidate="txtSiglaClassificacao" Display="Dynamic"
                ErrorMessage="Sigla é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;">
            <label title="Descrição da Classificação de Prontuários">
                Descrição</label>
            <asp:TextBox runat="server" ID="txtDescricao" Width="220px" Rows="3" TextMode="MultiLine" MaxLength="500" ToolTip="Descrição da Classificação de Prontuários Médicos"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <label title="Situação da Classificação de Prontuários Médicos" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação da Classificação de Prontuários Médicos">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvSituacao" ControlToValidate="ddlSituacao"
                ErrorMessage="A Situação da Classificação é Requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
