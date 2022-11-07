<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="DeclaracaoComparecimentoInsef.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes.DeclaracaoComparecimentoInsef"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 260px;
        }
        .liUnidade, .liAno
        {
            margin-top: 5px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liAluno
        {
            margin-top: 5px;
        }
        .liData
        {
            margin-top: 5px;
        }
        .liPeriodo
        {
            margin-top: -5px;
            width: 140px;
        }
        .liDescricao
        {
            margin-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.campoData').mask('99/99/9999');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liAno">
                    <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAno" Text="*" ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liAluno">
                    <label title="Aluno" class="lblObrigatorio">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liData">
                    <label class="lblObrigatorio" title="Dia do comparecimento" for="txtDataTemp">
                        Dia do comparecimento
                    </label>
                    <asp:TextBox ID="txtDataTemp" CssClass="campoData" runat="server" ToolTip="Selecione a data do comparecimento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                        ControlToValidate="txtDataTemp" Text="*" ErrorMessage="Campo Data é requerido"
                        SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liPeriodo">
                    <label class="lblObrigratorio" title="Periodo do comparecimento" for="ddlPeriodo">
                        Periodo</label>
                    <asp:DropDownList ID="ddlPeriodo" CssClass="ddlTurno" runat="server" ToolTip="Selecione o periodo do comparecimento">
                        <asp:ListItem Value="M" Text="Matutino">Matutino</asp:ListItem>
                        <asp:ListItem Value="V" Text="Vespertino">Vespertino</asp:ListItem>
                        <asp:ListItem Value="N" Text="Noturno">Noturno</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlPeriodo" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlPeriodo" Text="*" ErrorMessage="Campo Periodo é requerido"
                        SetFocusOnError="true">                    
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liDescricao">
                    <label class="lblObrigatorio" title="Descrição" for="txtDescricao">
                        Descrição</label>
                    <asp:TextBox ID="txtDescricao" runat="server" Columns="48" Rows="3" TextMode="Multiline"
                        ToolTip="Digite uma descrição"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtDescricao" runat="server" CssClass="validatorField"
                        ControlToValidate="txtDescricao" Text="*" ErrorMessage="Campo Descrição é requerido"
                        SetFocusOnError="true">                    
                    </asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
