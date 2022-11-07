<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="HistoricoFinanceiro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios.HistoricoFinanceiro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 380px">
        <li style="clear: both; margin-top:5px;">
            <asp:Label runat="server" ID="lblUnidade">Unidade</asp:Label><br />
            <asp:DropDownList runat="server" Width="230px" ID="ddlUnidade" OnSelectedIndexChanged="dllUnidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both; margin-top:5px;">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
            Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" Width="230px" Visible="false">
            </asp:DropDownList>
            <asp:TextBox style="margin-bottom:-10px; height:13px;" ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            
        </li>
        <li style="margin-top: 16px; margin-left: 0px;">
            <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
        </li>
        <li style="clear: both; margin-top:5px;">
            <label>Tipo</label>
            <asp:DropDownList ID="drpTipoRecebimento" Width="100px" runat="server">
                <asp:ListItem Value="" Text="Todos" />
                <asp:ListItem Value="C" Text="Consulta" />
                <asp:ListItem Value="P" Text="Procedimentos" />
                <asp:ListItem Value="S" Text="Serviços" />
                <asp:ListItem Value="E" Text="Exames" />
                <asp:ListItem Value="O" Text="Outros" />
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label>Contratação</label>
            <asp:DropDownList runat="server" ID="drpContratacao" OnSelectedIndexChanged="drpContratacao_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label>Plano</label>
            <asp:DropDownList runat="server" ID="drpPlano">
                <asp:ListItem Value="0" Text="Todos" Selected="True" />
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label>Cortesia</label>
            <asp:DropDownList ID="drpCortesia" Width="100px" runat="server">
                <asp:ListItem Value="" Text="Todos" />
                <asp:ListItem Value="S" Text="Sim" />
                <asp:ListItem Value="N" Text="Não" />
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label>Nota Fiscal</label>
            <asp:DropDownList ID="drpNotaFiscal" Width="100px" runat="server">
                <asp:ListItem Value="" Text="Todos" />
                <asp:ListItem Value="S" Text="Sim" />
                <asp:ListItem Value="N" Text="Não" />
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
