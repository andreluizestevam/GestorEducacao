<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DiarioClasse.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3499_Relatorios.DiarioClasse" %>

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
        .liAnoRefer, .liTurma, .liTrimestre
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
        .liReferencia
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
                    <label id="Label2" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
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
                <li id="liTurma" runat="server" class="liTurma" style="clear: none">
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
                    <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server" ToolTip="Selecione a Matéria"
                        OnSelectedIndexChanged="ddlMateria_OnSelectedIndexChanged" AutoPostBack="true">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="updPanel1" UpdateMode="Always">
            <ContentTemplate>
                <%--<li class="liTrimestre">
                    <asp:HiddenField ID="hidDtIniTrim" runat="server" />
                    <asp:HiddenField ID="hidDtFimTrim" runat="server" />
                    <label class="lblObrigatorio" for="ddlTrimestre" title="Selecione o trimestre de referência">
                        Referência</label>
                    <asp:DropDownList ID="ddlTrimestre" CssClass="ddlTrimestre" runat="server" ToolTip="Selecione o trimestre de Referência"
                        CausesValidation="true" OnSelectedIndexChanged="ddlTrimestre_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="T1" Selected="True">1° Trimestre</asp:ListItem>
                        <asp:ListItem Value="T2">2° Trimestre</asp:ListItem>
                        <asp:ListItem Value="T3">3° Trimestre</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTrimestre" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTrimestre" Text="*" ErrorMessage="Campo Referência é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>--%>
                <li class="ddlReferencia" style="margin-top: 5px;">
                    <asp:HiddenField ID="hidDtIniRef" runat="server" />
                    <asp:HiddenField ID="hidDtFimRef" runat="server" />
                    <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                        Referência</label>
                    <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                        runat="server" CausesValidation="true" OnSelectedIndexChanged="ddlReferencia_SelectedIndexChanged" AutoPostBack="true">   
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
                    ErrorMessage="A Referência em que a frequência será lançada deve ser informado." SetFocusOnError="true" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liTipo" style="width: 110px; margin-left: 0px;">
                    <label class="lblObrigatorio" for="ddlMesReferencia" title="Mês de Referência">
                        Mês de Referência</label>
                    <asp:DropDownList ID="ddlMesReferencia" CssClass="ddlMesReferencia" runat="server"
                        Width="100px" ToolTip="Selecione o Mês de Referência">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="rfvddlMesReferencia" runat="server" CssClass="validatorField"
                     ControlToValidate="ddlMesReferencia" Text="*" ErrorMessage="O Campo Mês Referência é Requerido"
                     SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                </li>
                <li class="liTipo" style="width: 50px;">
                    <label class="lblObrigatorio" title="Layout">
                        Layout</label>
                    <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Layout" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"   AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="A">Ambos</asp:ListItem>
                        <asp:ListItem Value="F">Frente</asp:ListItem>
                        <asp:ListItem Value="V">Verso</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin: 6px 0 0 12px; width: 120px">
                    <asp:CheckBox runat="server" ID="chkVerDetSim" class="chk" 
                        Text="Verso Detalhado" />
                    <asp:CheckBox runat="server" ID="chkImpAssinSegPag" Checked="false" class="chk" Text="Assinaturas frente"
                        ToolTip="Marque caso queira que os campos que receberão as assinaturas sejam impressos na parte da frente(frenquências)." />
                </li>
                <li style="clear: both; margin-top: 8px;">
                    <asp:Label runat="server" ID="lblErro" Visible="false" Style="color: Red; font-size: 9px">**O Trimestre selecionado não possui data de Início/Final no cadastro de Unidade.**</asp:Label>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
