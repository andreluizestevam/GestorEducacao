<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3113_LancamentoAtividadeLetivaRealiz.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 990px;
            padding-left: 10px;
        }
        
        /*--> CSS LIs */
        .liEspaco
        {
            margin-top: 10px;
        }
        .liLeft1
        {
            margin-left: 10px;
        }
        .liLeft3
        {
            margin-left: 7px;
        }
        .liLeft4
        {
            margin-left: 585px;
        }
        .liClear
        {
            clear: both;
        }
        .liGrid
        {
            margin-top: 10px;
            clear: both;
            margin-left: -5px;
        }
        .liGrid2
        {
            background-color: #EEEEEE;
            margin-top: -50px;
            height: 15px;
            width: 575px;
            text-align: center;
            padding: 5 0 5 0;
            margin-left: -5px;
        }
        .liGrid3
        {
            background-color: #EEEEEE;
            margin-top: -50px;
            margin-left: 10px;
            height: 15px;
            width: 395px;
            text-align: center;
            padding: 5 0 5 0;
            margin-right: 0px;
        }
        .lidi
        {
            margin-top: 50px;
        }
        .liDisciplina
        {
            display: inline;
            margin-left: 0px;
        }
        .liDatas
        {
            width: 150px;
        }
        /*--> CSS DADOS */
        .txtHora
        {
            width: 30px;
            text-align: center;
            margin-bottom: 0px !important;
            padding-bottom: 0px !important;
        }
        .campoData
        {
            margin-bottom: 0px !important;
            padding-bottom: 0px !important;
        }
        .divGrid
        {
            height: 296px;
            width: 575px;
            overflow-y: scroll;
            margin-top: -40px;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        .divGridTempos
        {
            height: 100px;
            width: 385px;
            overflow-y: scroll;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        .divDtMesmaLinha
        {
            float: left;
            display: inline;
            margin-left: 5px;
        }
        .liGridTempo
        {
            width: 385px !important;
            text-align: center;
        }
        .grdBuscaTempo
        {
            width: 365px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .chkAtivNaoPlanej
        {
            margin-left: -5px;
        }
        .ddlTipoAtiv
        {
            width: 90px !important;
        }
        .ddlTipoTempo
        {
            width: 80px !important;
        }
        .txtFormaAvaliAtiv
        {
            width: 100px;
        }
        .ddlAvaliaAtiv
        {
            width: 40px;
        }
        .chkAtivNaoPlanej label
        {
            display: inline;
        }
        .campoMateria
        {
            width: 160px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <asp:HiddenField runat="server" ID="hidTpAulaRequired" />
            <asp:HiddenField runat="server" ID="hidItemJaSalvo" />
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="campoAno" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno"
                CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft1">
            <label for="ddlProfessor" class="lblObrigatorio" title="Professor">
                Professor</label>
            <asp:DropDownList ID="ddlProfessor" ToolTip="Selecione o Professor" CssClass="campoNomePessoa"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProfessor_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProfessor"
                CssClass="validatorField" ErrorMessage="Professor deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft1">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft1">
            <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liLeft1">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTurma"
                CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <%--<li>
            <label for="ddlBimestre" title="Selecione o Bimestre em que a frequência será lançada"
                class="lblObrigatorio">
                Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" ToolTip="Selecione o Bimestre em que a frequência será lançada"
                runat="server">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="B1" Text="Bimestre 1"></asp:ListItem>
                <asp:ListItem Value="B2" Text="Bimestre 2"></asp:ListItem>
                <asp:ListItem Value="B3" Text="Bimestre 3"></asp:ListItem>
                <asp:ListItem Value="B4" Text="Bimestre 4"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBimestre" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="O Bimestre em que a atividade será lançada deve ser informado."
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>--%>
         <li runat="server">
                    <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                        Referência</label>
                    <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                        runat="server">   
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
                        ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
        <li class="liLeft1">
            <label for="ddlMes" class="lblObrigatorio" title="Mês">
                Mês</label>
            <asp:DropDownList ID="ddlMes" Width="90px" ToolTip="Selecione o Mês" CssClass="campoAno"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="lidi liLeft4 liClear">
            <ul>
                <li style="margin-top: 20px !important;" class="liEspaco">
                    <asp:CheckBox ID="chkAtivNaoPlanej" CssClass="chkAtivNaoPlanej" runat="server" ToolTip="Atividade Não-Planejada"
                        Text="Atividade Não Planejada"></asp:CheckBox>
                </li>
                <li class="liDisciplina liEspaco">
                    <label for="ddlDisciplina" class="lblObrigatorio" title="Disciplina">
                        Disciplina</label>
                    <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDisciplina"
                        CssClass="validatorField" ErrorMessage="Disciplina deve ser informada" Text="*"
                        Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="txtDataRe" class="lblObrigatorio" title="Data de Realização">
                        Data Realizada</label>
                    <asp:TextBox ID="txtDataRe" ToolTip="Informe a Data de Realização da Atividade" runat="server"
                        CssClass="campoData"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataRe"
                        CssClass="validatorField" ErrorMessage="Data de Realização deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </li>
        <li class="liGrid2">ATIVIDADES ESCOLARES</li>
        <li class="liGrid">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="558px" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdAlunos_RowDataBound">
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
                                <asp:CheckBox ID="ckSelect" runat="server" Enabled='<%# bind("FLA_HOMOLOG") %>' AutoPostBack="True"
                                    OnCheckedChanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DT_PREV_PLA" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PREV">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Atividade">
                            <ItemStyle Width="320px" />
                            <ItemTemplate>
                                <asp:Label ID="HiddenField1" runat="server" Text='<%# bind("DE_TEMA_AULA") %>' />
                                <asp:HiddenField ID="hdPlaAula" runat="server" Value='<%# bind("CO_PLA_AULA") %>' />
                                <asp:HiddenField ID="hdNumeroTempo" runat="server" Value='<%# bind("NU_TEMP_PLA") %>' />
                                <asp:HiddenField ID="hdInicioTempo" runat="server" Value='<%# bind("HR_INI_AULA_PLA") %>' />
                                <asp:HiddenField ID="hdTerminoTempo" runat="server" Value='<%# bind("HR_FIM_AULA_PLA") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DES_TIPO_ATIV" HeaderText="Tipo">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FL_PLANEJ_ATIV" HeaderText="PLANO">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FL_AVALIA_ATIV" HeaderText="NOTA?">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Situação">
                            <ItemStyle Width="120px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSituacao" runat="server" Text='<%# bind("SITUACAO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li class="liGrid3">INFORMAÇÕES</li>
        <li class="liEspaco liLeft1">
            <label for="ddlTipoAtiv" class="lblObrigatorio" title="Tipo de Atividade">
                Tipo</label>
            <asp:DropDownList ID="ddlTipoAtiv" OnSelectedIndexChanged="ddlTipoAtiv_SelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione o Tipo de Atividade" CssClass="ddlTipoAtiv"
                runat="server" Width="80px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlTipoAtiv"
                CssClass="validatorField" ErrorMessage="Tipo de Atividade deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco" style="margin-right: 0px;">
            <asp:Label ID="Label1" runat="server" Text="Tempo de aula" AssociatedControlID="ddlTempos"
                CssClass="lblObrigatorio"></asp:Label>
            <asp:DropDownList ID="ddlTempos" runat="server" OnSelectedIndexChanged="ddlTempos_SelectedIndexChanged"
                AutoPostBack="True" Width="80px" CssClass="ddlTipoTempo">
            </asp:DropDownList>
<%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlTempos"
                ErrorMessage="Informe o tipo de registro">*</asp:RequiredFieldValidator>--%>
        </li>
        <li class="liEspaco" style="margin-right: 0px;">
            <asp:Label ID="lblTurno" runat="server" Text="Turno" AssociatedControlID="ddlTurno"></asp:Label>
            <asp:DropDownList ID="ddlTurno" runat="server" AutoPostBack="true" Width="50px" CssClass="ddlTurno"
                OnSelectedIndexChanged="ddlTurno_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="M">Matutino</asp:ListItem>
                <asp:ListItem Value="V">Vespertino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liEspaco liDatas">
            <div runat="server" id="divDtInicio" visible="True" class="divDtMesmaLinha">
                <label for="txtInicio" class="lblObrigatorio" title="Horário de Inicio">
                    Inicio</label>
                <asp:TextBox ID="txtInicio" ToolTip="Informe a Hora de Inicio" runat="server" CssClass="txtHora"
                    AutoPostBack="True" OnTextChanged="txtInicio_TextChanged1"></asp:TextBox>
            </div>
            <div runat="server" id="divDtTermino" visible="True" class="divDtMesmaLinha">
                <label title="Horário de Término" class="lblObrigatorio" for="txtTermino">
                    Término</label>
                <asp:TextBox ID="txtTermino" ToolTip="Informe a Hora de Término" runat="server" CssClass="txtHora"
                    AutoPostBack="True" OnTextChanged="txtTermino_TextChanged1"></asp:TextBox>
            </div>
            <div runat="server" id="divDtDuracao" visible="True" class="divDtMesmaLinha">
                <label title="Horário de Término" for="txtDuracao">
                    Duração</label>
                <asp:TextBox ID="txtDuracao" ToolTip="Duração total" runat="server" CssClass="txtHora"
                    ReadOnly="True"></asp:TextBox>
            </div>
        </li>
        <li class="liEspaco liLeft1">
            <div runat="server" visible="True" id="divGridTempos" class="divGridTempos">
                <asp:GridView ID="grdTempos" runat="server" AutoGenerateColumns="False" CssClass="grdBusca grdBuscaTempo">
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="MARCAR">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbMarcar" runat="server" Checked="<%# bind('marcarTempo') %>" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="nomeTempo" HeaderText="TEMPO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="INÍCIO">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInicio" runat="server" CssClass="txtHora" Text='<%# bind("inicioTempo") %>'
                                    OnTextChanged="txtInicio_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TÉRMINO">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTermino" runat="server" CssClass="txtHora" Text='<%# bind("terminoTempo") %>'
                                    OnTextChanged="txtTermino_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="duracaoTempo" HeaderText="DURAÇÃO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <RowStyle CssClass="rowStyle" Height="5px" />
                </asp:GridView>
            </div>
        </li>
        <li class="liEspaco liLeft1">
            <label for="ddlAvaliaAtiv" title="Com nota?">
                Com nota?</label>
            <asp:DropDownList ID="ddlAvaliaAtiv" ToolTip="Selecione se Atividade terá nota" CssClass="ddlAvaliaAtiv"
                runat="server">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liEspaco liLeft1">
            <label for="txtPlanoAula" class="lblObrigatorio" title="Tema da Aula">
                Tema da Aula</label>
            <asp:TextBox ID="txtPlanoAula" MaxLength="256" ToolTip="Tema da Aula" runat="server"
                CssClass="campoNomePessoa"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtPlanoAula"
                CssClass="validatorField" ErrorMessage="Tema da Aula deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco liLeft1">
            <label for="txtFormaAvaliAtiv" title="Forma de Avaliação da Atividade">
                Forma de Avaliação</label>
            <asp:TextBox ID="txtFormaAvaliAtiv" MaxLength="15" ToolTip="Forma de Avaliação da Atividade"
                runat="server" CssClass="txtFormaAvaliAtiv"></asp:TextBox>
        </li>
        <li class="liLeft1">
            <label for="txtResumo" class="lblObrigatorio" title="Resumo das Atividades">
                Resumo das Atividades</label>
            <asp:TextBox ID="txtResumo" ToolTip="Informe Resumo das Atividades" onkeyup="javascript:MaxLength(this, 2000);"
                runat="server" TextMode="MultiLine" Width="390px" Height="60px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtResumo"
                CssClass="validatorField" ErrorMessage="Resumo deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco liLeft1">
            <label for="txtDataCad" title="Data de Cadastro">
                Data Cadastro</label>
            <asp:TextBox ID="txtDataCad" Enabled="false" ToolTip="Data de Cadastro" runat="server"
                CssClass="campoData"></asp:TextBox>
            <asp:HiddenField ID="HiddenField2" runat="server" />
        </li>
        <li class="liEspaco liLeft1">
            <label for="ddlCocol" title="Funcionário">
                Funcionário</label>
            <asp:DropDownList ID="ddlCocol" Enabled="false" ToolTip="Funcionário " CssClass="campoNomePessoa"
                runat="server">
            </asp:DropDownList>
        </li>
        <li class="liEspaco liLeft1">
            <label id="Label2" for="ddlProfessorReal">
                Realizado por:</label>
            <asp:DropDownList ID="ddlProfessorReal" ToolTip="Selecione o Professor como responsável"
                CssClass="campoNomePessoa" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li id="li2" runat="server" class="liEspaco liLeft1" title="Clique para Registrar a frequência."
            style="background-color: #F1FFEF; border: 1px solid #D2DFD1; margin-left: 210px;
            margin-top: 20px; padding: 2px 3px 1px 3px;">
            <asp:LinkButton OnClientClick="if (!confirm('Deseja salvar o registro?')) return false;"
                ID="btnRegFreq" CssClass="btnRegFreq" runat="server" OnClick="btnRegFreq_Click">REGISTRAR FREQUÊNCIA</asp:LinkButton>
        </li>
        <li></li>
    </ul>
    <asp:HiddenField ID="hdCodPlanAula" runat="server" />
    <asp:HiddenField ID="hdNumeroTempo" runat="server" />
    <asp:HiddenField ID="hdInicioTempo" runat="server" />
    <asp:HiddenField ID="hdTerminoTempo" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
//            if ($(".grdBusca tbody tr").length == 1) {
//                setTimeout("$('.emptyDataRowStyle').hide('slow')", 2500);
            //            }
            $(".txtHora").mask("?99:99");
        });
    </script>
</asp:Content>
