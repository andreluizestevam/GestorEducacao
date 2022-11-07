<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1223_LancamentoFrequencia.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .ulDados li input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 9px;
            margin-right: 10px;
        }        
        .liClear { clear: both; }
        #liDataEntrega { margin-bottom: 4px; }
        #liDiasLicenca { margin-bottom: 0; }
        
        /*--> CSS DADOS */
        .ddlTipoFreq { width: 100px; }
        .txtMotivo { width: 185px; }
        .txtHora { width: 30px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade do Colaborador">Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="true"
                Enabled="false" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade do Colaborador">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Unidade é requerida" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlColaborador" class="lblObrigatorio" title="Nome do Colaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="campoNomePessoa" Enabled="false"
                ToolTip="Selecione o Nome do Colaborador">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlColaborador"
                ErrorMessage="Colaborador é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDataFrequ" title="Data da Frequência">Data</label>
            <asp:TextBox ID="txtDataFrequ" runat="server" CssClass="campoData"
                ToolTip="Informe a Data da Frequência"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataFrequ"
                CssClass="validatorField"
                ErrorMessage="Data da Frequência deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtHora" title="Hora da Frequencia">Hora</label>
            <asp:TextBox ID="txtHora" runat="server" CssClass="txtHora" 
                ToolTip="Informe a Hora da Freqüência"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlTipoFreq" class="lblObrigatorio" title="Tipo da Freqüência">Tp Freq</label>
            <asp:DropDownList ID="ddlTipoFreq" runat="server" 
                ToolTip="Informe o Tipo da Freqüência" AutoPostBack="true" 
                onselectedindexchanged="ddlTipoFreq_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="E">Entrada</asp:ListItem>
                <asp:ListItem Value="S">Saída</asp:ListItem>
                <asp:ListItem Value="F">Falta</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlTipoFreq"
                ErrorMessage="Tipo da Freqüência é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-right: 0px;">
            <label for="txtMotivo" title="Motivo da Falta">Motivo</label>
            <asp:TextBox ID="txtMotivo" runat="server" CssClass="txtMotivo" MaxLength="50" 
                ToolTip="Informe o Motivo de Falta" Enabled="false"></asp:TextBox>
        </li>        
        <li class="liClear">
            <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" runat="server" CssClass="campoData" Enabled="false"></asp:TextBox>
        </li>
    </ul>
<script type="text/javascript">
    $(document).ready(function () {
        $('.txtHora').mask('99:99');
    });
</script>
</asp:Content>
