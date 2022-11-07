<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3109_OcorrenciasPaciente.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 260px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label title="Paciente" class="lblObrigatorio">Paciente</label>
            <asp:DropDownList ID="drpPaciente" Visible="false" runat="server" Width="230px" />
            <asp:TextBox ID="txtNomePacPesq" Width="230px" ValidationGroup="pesqPac" style="margin-bottom:0px;" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo paciente é requerido" ControlToValidate="drpPaciente"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 18px; margin-left: 3px;">
            <asp:ImageButton ID="imgbPesqPacNome" ValidationGroup="pesqPac" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Tipo da Ocorrência">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Informe o tipo da ocorrência" runat="server" Width="98px" />
            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ControlToValidate="ddlTipo" CssClass="validatorField"
                ErrorMessage="Tipo é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Título da Ocorrência">Título da Ocorrência</label>
            <asp:TextBox ID="txtTitulo" runat="server" CssClass="txtNomeAgencia"
                ToolTip="Informe o Título da Ocorrência" Width="160px" MaxLength="80" style="margin-bottom:0px;">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" CssClass="validatorField"
                ErrorMessage="Título da Ocorrência é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;">
            <label title="Ocorrência">Ocorrência</label>
            <asp:TextBox ID="txtOcorrencia" runat="server" Columns="50" Rows="4" TextMode="MultiLine" ToolTip="Informe o fato ocorrido">
            </asp:TextBox>
        </li>
        <li style="clear: both;">
            <label title="Ação Tomada">Ação Tomada</label>
            <asp:TextBox ID="txtAcao" runat="server" Columns="50" Rows="4" TextMode="MultiLine" ToolTip="Informe, caso haja, a ação tomada para a ocorrido">
            </asp:TextBox>
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Situação da Conta">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao"
                ToolTip="Selecione a Situação da Conta">
                <asp:ListItem Value="A" Selected="True">Ativa</asp:ListItem>
                <asp:ListItem Value="I">Inativa</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
