<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacaoFuncionariosParamet.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.RelacaoFuncionariosParamet"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 420px;
            margin-left:380px;
        }
        /* Usado para definir o formulário ao centro */
        .liUnidade
        {
            margin-top: 5px;
            width: 250px;
            clear: both;
        }
        .liCategoria
        {
            clear: both;
            margin-top: 5px;
            width: 85px;
        }
        .liFuncao, .liGrauInstrucao
        {
            clear: both;
            margin-top: 5px;
            width: 200px;
        }
        .liDeficiencia, .liSexo
        {
            clear: both;
            margin-top: 5px;
            width: 75px;
        }
        .liUF
        {
            clear: both;
            margin-top: 5px;
            width: 45px;
        }
        .liCidade
        {
            margin-top: 5px;
            width: 220px;
        }
        .liBairro
        {
            margin-top: 5px;
            width: 180px;
        }
        .ddlFuncao, .ddlGrauInstrucao
        {
            width: 210px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio">
                Unidade Cadastro</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade" Width="250px">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade Lotação/Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                ToolTip="Selecione a Unidade de contrato" Width="250px">
            </asp:DropDownList>
        </li>
        <li style="margin-top:5px; clear:both">
            <label class="lblObrigatorio">
                Categoria</label>
            <asp:DropDownList ID="ddlCategoria" CssClass="ddlCategoriaFuncional" runat="server"
                ToolTip="Selecione a Categoria do Colaborador" Width="110px">
            </asp:DropDownList>
        </li>
        <%-- Este controle só é apresentado quando a unidade logada for do tipo SAÚDE --%>
        <li id="liClassFunc" runat="server" style="margin-top:5px;">
            <label title="Selecione a Classificação Funcional">
                Classif. Funcional</label>
            <asp:DropDownList ID="ddlClassFunc" Width="90px" runat="server" ToolTip="Selecione a Classificação Funcional">
            </asp:DropDownList>
        </li>
        <li class="liFuncao">
            <label class="lblObrigatorio">
                Função</label>
            <asp:DropDownList ID="ddlFuncao" CssClass="ddlFuncao" runat="server" ToolTip="Selecione a Função do Colaborador">
            </asp:DropDownList>
        </li>
        <li style="margin-top:5px; clear:both">
            <label class="lblObrigatorio">
                Grau de Instrução</label>
            <asp:DropDownList ID="ddlGrauInstrucao" CssClass="ddlGrauInstrucao" runat="server"
                ToolTip="Selecione o Grau de Instrução Desejado" Width="120px">
            </asp:DropDownList>
        </li>
   <li style="margin-top:5px;">
            <label class="lblObrigatorio">
                Deficiência</label>
            <asp:DropDownList ID="ddlDeficiencia" CssClass="ddlDeficiencia" runat="server" ToolTip="Selecione a Deficiência do Dolaborador">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                <asp:ListItem Value="V">Visual</asp:ListItem>
                <asp:ListItem Value="F">Físico</asp:ListItem>
                <asp:ListItem Value="M">Mental</asp:ListItem>
                <asp:ListItem Value="I">Múltiplas</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top:5px;">
            <label class="lblObrigatorio">
                Sexo</label>
            <asp:DropDownList ID="ddlSexo" CssClass="ddlSexo" runat="server" ToolTip="Selecione o Sexo do Colaborador">
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUF">
                    <label class="lblObrigatorio">
                        UF</label>
                    <asp:DropDownList ID="ddlUF" CssClass="ddlUF" runat="server" ToolTip="Selecione o Estado Desejado"
                        OnSelectedIndexChanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li style="margin-top:5px;">
                    <label class="lblObrigatorio">
                        Cidade</label>
                    <asp:DropDownList ID="ddlCidade" CssClass="ddlCidade" runat="server" ToolTip="Selecione a Cidade Desejada"
                        OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li class="liBairro">
                    <label class="lblObrigatorio">
                        Bairro</label>
                    <asp:DropDownList ID="ddlBairro" CssClass="ddlBairro" runat="server" ToolTip="Selecione o Bairro Desejado"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
