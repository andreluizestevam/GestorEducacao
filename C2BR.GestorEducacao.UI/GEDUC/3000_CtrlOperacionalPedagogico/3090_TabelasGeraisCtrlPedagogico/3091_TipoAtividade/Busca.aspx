<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3090_TabelasGeraisCtrlPedagogico.F3091_TipoAtividade.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtDescricao" title="Nome">
                Nome</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Pesquise pelo Nome" runat="server" MaxLength="60"
                CssClass="campoDescricao"></asp:TextBox>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione a classificação do tipo de atividade, se será por Nota ou por Conceito">
                Classificação</label>
            <asp:DropDownList ID="ddlClass" runat="server" Width="65px">
                <asp:ListItem Selected="True" Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Nota" Value="N"></asp:ListItem>
                <asp:ListItem Text="Conceito" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione se o tipo de atividade lançará nota ou não">
                Lan&ccedil;a nota?</label>
            <asp:DropDownList ID="ddlLancaNota" runat="server" Width="60px" ToolTip="Selecione se o tipo de atividade lançará nota ou não">
                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" title="Digite a sigla do Tipo de atividade">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="5" Width="30px" ToolTip="Digite a sigla do Tipo de Atividade" CssClass="txtSigla">
            </asp:TextBox>
        </li>
        <li>
            <label title="Selecione se o tipo de ensino da atividade">
                Tipo de Ensino</label>
            <asp:DropDownList ID="drpTipoEnsino" runat="server" Width="55px" ToolTip="Selecione se o tipo de ensino da atividade">
                <asp:ListItem Selected="True" Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Presencial" Value="P"></asp:ListItem>
                <asp:ListItem Text="Ensino Remoto" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione se o tipo de atividade está ativo ou não">
                Situa&ccedil;&atilde;o</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" Width="55px" ToolTip="Selecione se o tipo de atividade está ativo ou não">
                <asp:ListItem Selected="True" Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
