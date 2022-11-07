<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="CustoFinanFuncio.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7200_CtrlCustoFinanColab._7299_Relatorios.CustoFinanFuncio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
        }
        /* Usado para definir o formulário ao centro */
        .liOpcoesRelatorio, .liDescOpRelatorio
        {
            margin-top: 5px;
            width: 246px;
        }
        .liCategoria
        {
            margin-top: 5px;
        }
        .liSexo, .liDeficiencia
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .ddlOpcoesRelatorio
        {
            width: 140px;
        }
        .ddlDescOpRelatorio
        {
            width: 220px;
            margin-top: 1px;
            display: block;
        }
        .ddlSexo, .ddlDeficiencia
        {
            width: 75px;
        }
        .ddlCategoria
        {
            width: 85px;
        }
        .lblDescOpRelatorio
        {
            margin-bottom: 1px;
            display: inline;
        }
        .lblCategoria, .lblSexo, .lblDeficiencia
        {
            width: 50px;
            display: block;
            margin-bottom: 1px;
        }
        .lblObrig
        {
            color: Red;
            width: 50px;
            display: inline;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liOpcoesRelatorio">
                    <label id="Label1" class="lblObrigatorio">
                        Opções de Relatório</label>
                    <asp:DropDownList ID="ddlOpcoesRelatorio" CssClass="ddlOpcoesRelatorio" ToolTip="Selecione a Opção de Relatório"
                        runat="server" OnSelectedIndexChanged="ddlOpcoesRelatorio_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Value="U">Por Unidade</asp:ListItem>
                        <asp:ListItem Value="D">Por Departamento</asp:ListItem>
                        <asp:ListItem Value="F">Por Função</asp:ListItem>
                        <asp:ListItem Value="C">Por Tipo de Contrato</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="dfvddlOpcoesRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlOpcoesRelatorio" Text="*" ErrorMessage="Campo Opções de Relatório é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liDescOpRelatorio">
                    <asp:Label ID="lblDescOpRelatorio" CssClass="lblDescOpRelatorio" runat="server">
                Unidade</asp:Label><label class="lblObrig">*</label>
                    <asp:DropDownList ID="ddlDescOpRelatorio" CssClass="ddlDescOpRelatorio" runat="server"
                        ToolTip="Selecione a Descrição">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlDescOpRelatorio" Text="*" ErrorMessage="Campo Descrição é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liDescOpRelatorio">
                    <label class="lblObrigatorio" for="txtUnidade">
                        Unidade Lotação/Contrato</label>
                    <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                        ToolTip="Selecione a Unidade">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liCategoria">
            <label id="lblCategoria" class="lblObrigatorio" for="lblDeficiencia">
                Categoria</label>
            <asp:DropDownList ID="ddlCategoria" ToolTip="Selecione a Categoria Funcional do Funcionário"
                CssClass="ddlCategFuncColab" runat="server">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="N">Funcionário</asp:ListItem>
                <asp:ListItem Value="S">Profissional Saúde</asp:ListItem>
                <asp:ListItem Value="P">Prestador Serviços</asp:ListItem>
                <asp:ListItem Value="E">Estagiário(a)</asp:ListItem>
                <asp:ListItem Value="O">Outro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liSexo">
            <label id="lblSexo" class="lblObrigatorio">
                Sexo</label>
            <asp:DropDownList ID="ddlSexo" CssClass="ddlSexo" runat="server" ToolTip="Selecione o Sexo">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liDeficiencia">
            <label id="lblDeficiencia" class="lblObrigatorio">
                Deficiência</label>
            <asp:DropDownList ID="ddlDeficiencia" CssClass="ddlDeficiencia" runat="server" ToolTip="Selecione a Deficiência">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                <asp:ListItem Value="V">Visual</asp:ListItem>
                <asp:ListItem Value="F">Físico</asp:ListItem>
                <asp:ListItem Value="M">Mental</asp:ListItem>
                <asp:ListItem Value="I">Múltiplas</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
