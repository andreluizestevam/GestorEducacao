<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DiarioClasseBimestral.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3499_Relatorios.DiarioClasseBimestral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 500px;
            margin-left: 300px;
        }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;
        }
        .liTipo
        {
            margin-top: 5px;
            margin-left: 5px;
            width: 70px;
        }
        .liAnoRefer, .liTurma, .liBimestre
        {
            clear: both;
            margin-top: 5px;
        }
        .liModalidade
        {
            width: 140px;
            margin-top: 5px;
            margin-left: 5px;
        }
        .liSerie
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .ddlTipo
        {
            width: 60px;
        }
        .liBimestre
        {
            width: 75px;
        }
        .ddlMesReferencia
        {
            width: 75px;
            margin-top: 0px;
        }
        .liMateria
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .chk label
        {
            display: inline;
            margin-left: -2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label id="Label2" class="lblObrigatorio" runat="server" title="Unidade">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAnoRefer">
                    <label class="lblObrigatorio" title="Ano">
                        Ano</label>
                    <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAnoRefer" Text="*" ErrorMessage="Campo Ano Referência é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liModalidade" runat="server" class="liModalidade">
                    <label id="Label1" class="lblObrigatorio" runat="server" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                        Width="140" ToolTip="Selecione a Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liSerie" runat="server" class="liSerie">
                    <label class="lblObrigatorio" title="Série/Curso">
                        Série</label>
                    <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" ToolTip="Selecione a Curso"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liTurma" runat="server" style="margin: 5px 0 0 5px">
                    <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                        AutoPostBack="true" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liSerie" style="clear: both; margin-left: 0px;">
                    <label>
                        Emissão Por</label>
                    <asp:DropDownList runat="server" ID="ddlEmissao" Width="110px" ToolTip="Selecione a opção de Emissão. Consolidador por Professor(a) ou Disciplina."
                        OnSelectedIndexChanged="ddlEmissao_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="D" Text="Disciplina"></asp:ListItem>
                        <asp:ListItem Value="P" Text="Professor(a)"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="liMateria" class="liMateria" runat="server">
                    <label class="lblObrigatorio" for="ddlMateria" title="Matéria">
                        Matéria</label>
                    <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server" ToolTip="Selecione a Matéria">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlMateria" Text="*" ErrorMessage="Campo Matéria é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liProfessor" runat="server" class="liMateria" visible="false" style="margin-top: 5px;">
                    <label>
                        Professor(a)</label>
                    <asp:DropDownList runat="server" ID="ddlProfessor" CssClass="ddlMateria" ToolTip="Selecione o(a) Professor(a)">
                    </asp:DropDownList>
                </li>
                <li class="liBimestre">
                    <asp:HiddenField ID="hidDtIniBim" runat="server" />
                    <asp:HiddenField ID="hidDtFimBim" runat="server" />
                    <label class="lblObrigatorio" for="ddlBimestre" title="Selecione o bimestre de referência">
                        Bimestre</label>
                    <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server" ToolTip="Selecione o bimestre de Referência"
                        CausesValidation="true" OnSelectedIndexChanged="ddlBimestre_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="B1" Selected="True">1° Bimestre</asp:ListItem>
                        <asp:ListItem Value="B2">2° Bimestre</asp:ListItem>
                        <asp:ListItem Value="B3">3° Bimestre</asp:ListItem>
                        <asp:ListItem Value="B4">4° Bimestre</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlBimestre" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlBimestre" Text="*" ErrorMessage="Campo Bimestre é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liTipo">
                    <label class="lblObrigatorio" title="Layout">
                        Layout</label>
                    <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Layout" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"   AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="A">Ambos</asp:ListItem>
                        <asp:ListItem Value="F">Frente</asp:ListItem>
                        <asp:ListItem Value="V">Verso</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-top:10px;">
                    <ul>
                        <li>
                            <asp:CheckBox runat="server" ID="chkVerDetSim" Text="Verso Detalhado"
                                CssClass="chk" 
                                ToolTip="Quando selecionado, imprime o verso(atividades) com maiores detalhes" />
                        </li>
                        <li style="margin: 2px 0 0 0px; clear: both">
                            <asp:CheckBox runat="server" ID="chkImprimeMedias" Checked="true" Text="Imprime Médias"
                                ToolTip="Quando selecionado, imprime as médias na lateral frontal" CssClass="chk" />
                        </li>
                    </ul>
                </li>
                <li style="margin-top:10px;">
                    <ul>
                        <li>
                            <asp:CheckBox runat="server" ID="chkPresenP" Text="Imprime Presença com *" CssClass="chk"
                                ToolTip="Quando selecionado, imprime no diário a presença com '*'." />
                        </li>
                        <li style="margin: 2px 0 0 0px; clear: both">
                            <asp:CheckBox runat="server" ID="chkImpAssinSegPag" Checked="false" class="chk" Text="Assinaturas frente"
                                ToolTip="Marque caso queira que os campos que receberão as assinaturas sejam impressos na parte da frente(frenquências)." />
                        </li>
                    </ul>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
