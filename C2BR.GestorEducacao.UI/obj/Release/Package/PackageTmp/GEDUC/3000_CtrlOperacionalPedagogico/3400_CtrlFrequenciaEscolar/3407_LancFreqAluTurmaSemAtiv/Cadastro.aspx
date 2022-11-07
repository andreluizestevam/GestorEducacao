<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3407_LancFreqAluTurmaSemAtiv.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Library/JS/gestoreducacao.js"></script>
    <style type="text/css">
        .ulDados
        {
            width: 975px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        .ulDados table
        {
            border: none !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
            margin-top: 10px;
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
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        .liPeriodoAte
        {
            clear: none !important;
            display: inline;
            margin-left: 0px;
            margin-top: 13px;
        }
        .liAux
        {
            margin-right: 5px;
            clear: none !important;
            display: inline;
        }
        .liBloco
        {
            clear: both;
            width: 100%;
            padding-left: 20px;
            margin-top: 10px;
        }
        .liPesqAtiv
        {
            margin-top: 22px !important;
            margin-left: -5px !important;
        }
        .liGrideAluno
        {
            margin-right: 0px !important;
            margin-top: 10px;
        }
        .liGrideData
        {
            margin-right: 10px !important;
            margin-top: -11px !important;
        }
        .liModalidade
        {
            margin-left: 0px;
        }
        .liDisciplina
        {
            margin-left: 0px;
            clear: none;
        }
        .liGrid
        {
            margin-left: 290px;
            margin-top: 10px;
        }
        .liSerie
        {
            margin-left: 0px;
        }
        .liTurma
        {
            margin-left: 0px;
            clear: none !important;
        }
        .liData
        {
            margin-left: 27px;
        }
        
        /*--> CSS DADOS */
        .divGridAluno
        {
            height: 300px;
            width: 380px;
            overflow-y: auto;
        }
        .divGridData
        {
            height: 330px;
            width: 265px;
            overflow-y: auto;
        }
        .ddlFreq
        {
            text-align: left;
            width: 65px;
        }
        .divGrid
        {
            height: 130px;
            width: 520px;
            overflow-y: scroll;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .labelAux
        {
            margin-top: 16px;
        }
        .liPesqAtiv img
        {
            width: 14px;
            height: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField Value="" ID="hdCoAtivProfTur" runat="server" />
    <ul id="ulDados" class="ulDados">
        <li class="liData">
            <label for="txtData" class="lblObrigatorio" title="Data da Frequência">
                Data Frequência</label>
            <asp:TextBox ID="txtData" CssClass="campoData" runat="server" ToolTip="Informe a Data"></asp:TextBox>
        </li>
        <li class="liModalidade">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" Style="width: 115px !important;" ToolTip="Selecione a Modalidade"
                CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" Style="width: 80px !important;" ToolTip="Selecione a Série/Curso"
                CssClass="campoSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" Style="width: 90px !important;" ToolTip="Selecione a Turma"
                CssClass="campoTurma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <asp:Label ID="Label1" runat="server" Text="Tempo de aula" AssociatedControlID="ddlTempos"
                CssClass="lblObrigatorio"></asp:Label>
            <asp:DropDownList ID="ddlTempos" runat="server" OnSelectedIndexChanged="ddlTempos_SelectedIndexChanged"
                AutoPostBack="True" Width="126px" CssClass="ddlTipoTempo">
                <asp:ListItem Value="S" Text="Com Registro de Tempo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="N" Text="Tempo não Definido"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="lblTurno" runat="server" Text="Turno" AssociatedControlID="ddlTurno"></asp:Label>
            <asp:DropDownList ID="ddlTurno" runat="server" AutoPostBack="true" Width="70px" CssClass="ddlTurno"
                OnSelectedIndexChanged="ddlTurno_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="M">Matutino</asp:ListItem>
                <asp:ListItem Value="V">Vespertino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlBimestre" title="Selecione a Referência em que a frequência será lançada"
                class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlBimestre" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBimestre" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="A Referência em que a frequência será lançada deve ser informado."
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDisciplina">
            <label for="ddlDisciplina" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" Enabled="false" Style="width: 200px !important;"
                ToolTip="Selecione a Disciplina" CssClass="campoMateria" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <img title="Clique para gerar Gride de Atividades e Alunos."
                        alt="Icone de Pesquisa das Grides." 
                        src="/Library/IMG/Gestor_BtnPesquisa.png" />
            </asp:LinkButton>
        </li>

        <li style="text-align: left;margin-top: -6px;margin-left: 611px;" runat="server" id="lisms" visible="false">
        <label for="ddlDisciplina" title="Avisar o Responsável via SMS da Frequência?">Avisar o Responsável via SMS da Frequência?</label>
        <asp:DropDownList runat="server" ID="ddlenvioSMS" Width="200px">
        <asp:ListItem Text="Selecionar" Value="0"></asp:ListItem>
        <asp:ListItem Text="Falta" Value="1"></asp:ListItem>
        <asp:ListItem Text="Presença" Value="2"></asp:ListItem>
        <asp:ListItem Text="Falta e Presença" Value="3"></asp:ListItem>
        <asp:ListItem Text="Não Avisar" Value="4"></asp:ListItem>
        </asp:DropDownList>
        </li>
        <li class="liBloco" style="margin-top: 0px !important;">
            <ul>
                <li style="margin-right: 2px; margin-top: 0px !important; margin-left: 30px">
                    <ul>
                        <li class="liGrid2" style="width: 252px; margin-right: 0px; margin-left: 75px;">TEMPOS
                            DE AULA</li>
                        <li class="liClear liGrideData">
                            <div id="divGrid" runat="server" class="divGridData" style="margin-left: 75px;" visible="true">
                                <asp:GridView ID="grdTempos" runat="server" AutoGenerateColumns="False" CssClass="grdBusca grdBuscaTempo">
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="MARCAR">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbMarcar" runat="server" Checked='<%# bind("marcarTempo") %>' OnCheckedChanged="cbMarcar_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                                <asp:HiddenField runat="server" ID="hidCoTp" Value='<%# bind("nomeTempo") %>' />
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
                    </ul>
                </li>
                <li style="margin-right: 0px; margin-top: 0px !important; margin-left: 20px">
                    <ul>
                        <li class="liGrid2" style="width: 532px; margin-right: 0px; margin-bottom: -10px;">REGISTRO
                            DE FREQUÊNCIA - RELAÇÃO DE ALUNOS</li>
                        <li class="liClear liGrideAluno">
                            <div id="divGridAluno" class="divGridAluno" style="width: 532px; height: 362px;"
                                runat="server">
                                <asp:GridView ID="grdBusca" CssClass="grdBusca" Width="532px" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="grdAlunos_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="NIRE" HeaderText="NIRE">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno" >
                                            <ItemStyle Width="210px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Frequência" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                
                                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:HiddenField ID="hdCoFlagFreq" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                                <asp:DropDownList ID="ddlFreq" CssClass="ddlFreq" runat="server" AutoPostBack="true" onselectedindexchanged ="ddlFreq_SelectedIndexChanged">
                                                    <asp:ListItem Text="Presença" Value="S"></asp:ListItem>
                                                    <asp:ListItem Text="Falta" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Justificativa" ControlStyle-Width="150px">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdCoAluno2" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:HiddenField ID="hdCoFlagFreq2" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                                <asp:DropDownList ID="ddlJust" CssClass="ddlFreq" runat="server" AutoPostBack="true" Enabled="false" onselectedindexchanged ="ddlFreq_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
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
