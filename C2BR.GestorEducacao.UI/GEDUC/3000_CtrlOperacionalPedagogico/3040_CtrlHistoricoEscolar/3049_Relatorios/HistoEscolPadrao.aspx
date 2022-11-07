<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="HistoEscolPadrao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3049_Relatorios.HistoEscolPadrao"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
        }
        .liUnidade, .liAluno, .liModalidade
        {
            margin-top: 5px;
            width: 270px;
        }
        .txtInforComplementares
        {
            width: 265px;
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <ul id="ulDados" class="ulDados">
                <li class="liUnidade">
                    <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True"
                        ToolTip="Selecione a Unidade/Escola">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade" title="Classificação">
                        Classificação</label>
                    <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" ToolTip="Selecione a Classificação"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Classificação é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-top: 5px;">
                    <label>
                        Modalidade</label>
                    <asp:DropDownList runat="server" ID="ddlModal" ToolTip="Selecione a modalidade para filtrar os alunos matriculados"
                        OnSelectedIndexChanged="ddlModal_OnSelectedIndexChanged" AutoPostBack="true"
                        Width="150px">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label>
                        Série</label>
                    <asp:DropDownList runat="server" ID="ddlSerie" ToolTip="Selecione a Série para filtrar os alunos matriculados"
                        OnSelectedIndexChanged="ddlSerie_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label>
                        Turma</label>
                    <asp:DropDownList runat="server" ID="ddlTurma" ToolTip="Selecione a Turma para filtrar os alunos matriculados"
                        OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true"
                        Width="140px">
                    </asp:DropDownList>
                </li>
                <li class="liAluno">
                    <label class="lblObrigatorio" title="Aluno">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Aluno">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAluno" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAlunos" Text="*" ErrorMessage="Campo Aluno é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAluno">
                    <label title="Informações Complementares">
                        Informações Complementares</label>
                    <asp:TextBox ID="txtInforComplementares" CssClass="txtInforComplementares" runat="server"
                        ToolTip="Informações Complementares" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                </li>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Navegacao/Icones/carregando.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
