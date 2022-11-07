<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8240_RegisAgendaAvaliacao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin-top: 30px;
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
        .chk label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style=" clear:both;  width:500px"  >
            <label>
                Tipo de Pesquisa</label>
            <asp:DropDownList runat="server" ID="ddlTipoPesquisa" OnSelectedIndexChanged="ddlTipoPesquisa_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Todos" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Consulta de Avaliação" Value="C"></asp:ListItem>
                <asp:ListItem Text="Lista de Espera" Value="L"></asp:ListItem>
                <asp:ListItem Text="Procedimentos" Value="P"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
      
            <label>
                Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="190px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Paciente</label>
            <asp:TextBox runat="server" ID="txtNomePaciente" Width="220px"></asp:TextBox>
        </li>
        <li>
            <label>Indicativo de Profissionais</label>
            <asp:CheckBox style="margin-left:-5px" runat="server" ID="chkPsico" Text="PSI" ToolTip="Psicólogo(a)" CssClass="chk" />
            <asp:CheckBox style="margin-left: 5px" runat="server" ID="chkFono" Text="FON" ToolTip="Fonoaudiólogo(a)" CssClass="chk" />
            <asp:CheckBox style="margin-left: 2px" runat="server" ID="chkTeraOcup" Text="TOC" ToolTip="Terapeuta Ocupacional"
                CssClass="chk" />
            <asp:CheckBox runat="server" ID="chkFisio" Text="FIS" ToolTip="Fisioterapeuta" CssClass="chk" />
            <asp:CheckBox runat="server" ID="chkPscicPeda" Text="PEG" ToolTip="Psicopedagogo(a)"
                CssClass="chk" />
        </li>
        <li>
            <asp:CheckBox style="margin-left:-5px" runat="server" ID="chkMed" Text="MED" ToolTip="Médico " CssClass="chk" />
            <asp:CheckBox runat="server" ID="chkOdonto" Text="ODO" ToolTip="Odontólogo" CssClass="chk" />
            <asp:CheckBox runat="server" ID="chkOutro" Text="OUT" ToolTip="Outros" CssClass="chk" />
        </li>
        <li style="clear: both">
            <asp:Label runat="server" ID="Label4">Período de data de Nascimento</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="txtDataNascInicio" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label5"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtDatanascFim" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
        <li runat="server" id="liPeriCon">
            <label>Período de data de Consulta</label>
            <asp:TextBox runat="server" class="campoData" ID="IniPeriCon" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeriCon" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
        <li runat="server" id="liPeriCad">
            <label>Período de data de Cadastro</label>
            <asp:TextBox runat="server" class="campoData" ID="IniPeriCad" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label3"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeriCad" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label>
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Em Aberto"></asp:ListItem>
                <asp:ListItem Value="E" Text="Encaminhado"></asp:ListItem>
                <asp:ListItem Value="C" Text="Cancelado"></asp:ListItem>
                <asp:ListItem Value="R" Text="Realizado"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
