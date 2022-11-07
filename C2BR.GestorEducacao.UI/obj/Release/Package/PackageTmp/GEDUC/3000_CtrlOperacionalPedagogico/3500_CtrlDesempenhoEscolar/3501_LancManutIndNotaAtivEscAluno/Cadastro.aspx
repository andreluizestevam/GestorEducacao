<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3501_LancManutIndNotaAtivEscAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 600px;
            height: 15px;
        }
        .ulDados input[type="text"]
        {
            margin-bottom: 0 !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liGrid
        {
            margin-top: -10px;
            clear: both;
        }
        .liGrid2
        {
            background-color: #EEEEEE;
            height: 15px;
            width: 555px;
            text-align: center;
            padding: 5 0 5 0;
        }
        
        /*--> CSS DADOS */
        .ddlUnidade
        {
            width: 200px;
        }
        .ddlTipo
        {
            width: 120px;
        }
        .txtNomeAtiv
        {
            width: 335px;
        }
        .mascaradecimal
        {
            width: 50px;
            text-align: right;
        }
        .ddlAluno
        {
            width: 375px;
        }
        .ddlMateria
        {
            width: 160px;
        }
        .campoModalidade
        {
            width: 160px !important;
        }
        .campoSerieCurso
        {
            width: 50px !important;
        }
        .campoTurma
        {
            width: 145px !important;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .ddlJusti
        {
            width: 45px;
        }
        .ddlStatus
        {
            width: 60px;
        }
        .divGrid
        {
            height: 130px;
            width: 555px;
            overflow-y: scroll;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        .txtJustificativa
        {
            width: 380px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" title="Selecione a Unidade" class="lblObrigatorio">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" AutoPostBack='true'
                OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlBimestre" title="Bimestre">
                Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server" ToolTip="Selecione o Bimestre">
                <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                <asp:ListItem Value="B1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="B2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="B3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="B4">4º Bimestre</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBimestre" runat="server" class="validatorField"
                ControlToValidate="ddlBimestre" ErrorMessage="Bimestre deve ser selecionado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSemestre" class="lblObrigatorio" title="Semeestre/Ano">
                Semestre/Ano</label>
            <asp:DropDownList ID="ddlSemestre" runat="server" Width="35px" ToolTip="Selecione o Semestre">
                <asp:ListItem Value="1" Text="01"></asp:ListItem>
                <asp:ListItem Value="2" Text="02"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSemestre" class="validatorField" runat="server"
                ControlToValidate="ddlSemestre" ErrorMessage="Semestre deve ser selecionado"
                Display="None"> </asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlAno" runat="server" Style="width: 45px;" AutoPostBack="true"
                ToolTip="Selecione o Ano" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" class="validatorField" runat="server"
                ControlToValidate="ddlAno" ErrorMessage="Ano deve ser selecionado" Display="None"> </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" class="validatorField"
                ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser selecionada"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" class="validatorField" runat="server"
                ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser selecionado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                ToolTip="Selecione a Turma" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" class="validatorField" runat="server"
                ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser selecionado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlMateria" class="lblObrigatorio" title="Matéria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" runat="server" CssClass="ddlMateria" AutoPostBack="true"
                OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged" ToolTip="Selecione a Matéria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" class="validatorField" runat="server"
                ControlToValidate="ddlMateria" ErrorMessage="Matéria deve ser selecionado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrid2">ATIVIDADES ESCOLARES</li>
        <li class="liGrid">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdAtividades" CssClass="grdBusca" Width="538px" runat="server"
                    AutoGenerateColumns="False" OnRowDataBound="grdAtividades_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="True" OnCheckedChanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DT_ATIV_REAL" DataFormatString="{0:d}" HeaderText="Dt Ativ">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Atividade">
                            <ItemStyle Width="320px" />
                            <ItemTemplate>
                                <asp:Label ID="HiddenField1" runat="server" Text='<%# bind("DE_TEMA_AULA") %>' />
                                <asp:HiddenField ID="hdPlaAula" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DES_TIPO_ATIV" HeaderText="Tipo">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FL_PLANEJ_ATIV" HeaderText="Planej">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li>
            <label for="ddlAluno" class="lblObrigatorio" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" runat="server" CssClass="ddlAluno" ToolTip="Selecione o Aluno">
            </asp:DropDownList>
            <asp:HiddenField ID="hdCodPlanAula" runat="server" />
            <asp:RequiredFieldValidator ID="rfvddlAluno" class="validatorField" runat="server"
                ControlToValidate="ddlAluno" ErrorMessage="Aluno deve ser selecionado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <br class="liClear" />
        <li>
            <label for="ddlTipoAtiv" class="lblObrigatorio" title="Tipo de Atividade">
                Tipo</label>
            <asp:DropDownList ID="ddlTipoAtiv" ToolTip="Selecione o Tipo de Atividade" CssClass="ddlTipoAtiv"
                OnSelectedIndexChanged="ddlTipoAtiv_OnSelectedIndexChanged" AutoPostBack="true"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlTipoAtiv"
                CssClass="validatorField" ErrorMessage="Tipo de Atividade deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataAtiv" title="Data da Atividade" class="lblObrigatorio">
                Data da Atividade</label>
            <asp:TextBox ID="txtDataAtiv" CssClass="campoData" runat="server" ToolTip="Selecione a Data da Atividade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataAtiv" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataAtiv" Text="*" ErrorMessage="Campo Data é requerido"
                SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNomeAtiv" class="lblObrigatorio" title="Nome da Atividade">
                Nome da Atividade</label>
            <asp:TextBox ID="txtNomeAtiv" runat="server" MaxLength="256" ToolTip="Digite o Nome da Atividade"
                CssClass="txtNomeAtiv"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtNomeAtiv" runat="server" CssClass="validatorField"
                ControlToValidate="txtNomeAtiv" Text="*" ErrorMessage="Campo Nome da Atividade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNota" class="lblObrigatorio" title="Nota da Atividade">
                Nota</label>
            <asp:TextBox ID="txtNota" runat="server" ToolTip="Digite o Nome da Atividade" CssClass="mascaradecimal"
                MaxLength="9"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtNota" runat="server" CssClass="validatorField"
                ControlToValidate="txtNota" Text="*" ErrorMessage="Campo Nota da Atividade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlJusti" title="Nota Avaliada">
                Avaliada</label>
            <asp:DropDownList ID="ddlAvaliNota" runat="server" CssClass="ddlJusti" ToolTip="Informe se a nota do Aluno foi avaliada">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlJusti" title="Justificado">
                Justificado</label>
            <asp:DropDownList ID="ddlJusti" runat="server" CssClass="ddlJusti" ToolTip="Informe se a nota do Aluno foi justificada"
                AutoPostBack="true" OnSelectedIndexChanged="ddlJusti_SelectedIndexChanged">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liJustificativa">
            <label for="txtJustificativa" title="Justificativa da Nota">
                Justificativa de Nota</label>
            <asp:TextBox ID="txtJustificativa" runat="server" CssClass="txtJustificativa" MaxLength="200"
                Enabled="false" ToolTip="Informe a Justificativa de Falta"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlStatus" title="Status da Nota">
                Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" ToolTip="Informe o Status da Nota do Aluno">
                <asp:ListItem Text="Ativa" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlBimestre" class="lblObrigatorio" title="Classificação">
                Classificação</label>
            <asp:DropDownList ID="ddlClassi" CssClass="ddlClassi" runat="server" ToolTip="Selecione a Classificação">
                <asp:ListItem Value="N1">Nota 1</asp:ListItem>
                <asp:ListItem Value="N2">Nota 2</asp:ListItem>
                <asp:ListItem Value="N3">Simulado</asp:ListItem>
                <asp:ListItem Value="S1">Extra</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClassi"
                CssClass="validatorField" ErrorMessage="Classificação deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco liLeft1">
            <label for="txtDataCad" title="Data de Cadastro">
                Data Cadastro</label>
            <asp:TextBox ID="txtDataCad" Enabled="false" ToolTip="Data de Cadastro" runat="server"
                CssClass="campoData"></asp:TextBox>
            <asp:HiddenField ID="HiddenField2" runat="server" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($(".grdBusca tbody tr").length == 1) {
                setTimeout("$('.emptyDataRowStyle').fadeOut('slow', SetInputFocus)", 2500);
            }
        });       
    </script>
</asp:Content>
