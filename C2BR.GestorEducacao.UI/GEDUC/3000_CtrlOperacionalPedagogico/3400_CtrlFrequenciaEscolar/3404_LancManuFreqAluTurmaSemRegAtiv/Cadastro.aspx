<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3400_CtrlFrequenciaEscolar._3404_LancManuFreqAluTurmaSemRegAtiv.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Library/JS/gestoreducacao.js"></script>
    <style type="text/css">
        .ulDados
        {
            width: 975px;
        }
        .chk label
        {
            display:inline;
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
            margin-left: 25px;
        }
        
        /*--> CSS DADOS */
        .divGridAluno
        {
            height: 300px;
            width: 380px;
            overflow-y: auto;
            <%--visibility:hidden;--%>
        }
        .divGridData
        {
            height: 330px;
            width: 300px;
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
        <%--<li class="liAno">
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="campoAno" runat="server"
                AutoPostBack="true" onselectedindexchanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAno" CssClass="validatorField"
             ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>--%>
        <li class="liData">
            <label for="txtData" class="lblObrigatorio" title="Data da Frequência">
                Data Frequência</label>
            <asp:TextBox ID="txtData" CssClass="campoData" runat="server" ToolTip="Informe a Data"
                OnTextChanged="txtData_OnTextChanged" AutoPostBack="true"></asp:TextBox>
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
        <li class="liModalidade">
            <label for="ddlTempo" title="Tempo de Aula" class="lblObrigatorio">
                Tempo</label>
            <asp:DropDownList ID="ddlTempo" Style="width: 90px !important;" OnSelectedIndexChanged="ddlTempo_SelectedIndexChanged"
                ToolTip="Selecione o Tempo de Aula" Width="80px" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <%--<li class="liData">
            <label for="txtPeriodoDe" class="lblObrigatorio" title="Período de Frequência">Frequência</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>--%>
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
        <li style="margin: 15px 0 0 -10px !Important;">
            <%-- <li>
                    <asp:CheckBox CssClass="chkAtualizaHist" ID="chkAtualizaHist" Checked="true" runat="server"
                        ToolTip="Marque para atualizar o histórico do aluno" />
                    <asp:Label runat="server" style="margin-left:-5px">
                        Atualiza Histórico</asp:Label>
                </li>--%>
            <asp:CheckBox ID="chkRepeteFreq" CssClass="chkRepeteFreq" Checked="true" runat="server"
                OnCheckedChanged="chkRepeteFreq_OnCheckedChanged" AutoPostBack="true" />
            <asp:Label runat="server" Style="margin-left: -5px">
                        Repete Frequência</asp:Label>
        </li>
        <li style="margin: 2px 0 0 -10px !Important;">
            <asp:CheckBox runat="server" CssClass="chk" ID="chkLancFreqHomol" ToolTip="Marque caso deseje que a frequência seja lançada já homologada" Text="Lança Freq. Homol." />
        </li>
        <li class="liBloco" style="margin-top: 0px !important;">
            <ul>
                <li style="margin-right: 2px; margin-top: 0px !important;">
                    <ul>
                        <li class="liGrid2" style="width: 300px; margin-right: 0px; margin-left: 75px;">ATIVIDADES
                            DO DIA</li>
                        <li class="liClear liGrideData">
                            <div id="divGrid" runat="server" class="divGridData" style="margin-left: 75px;">
                                <asp:GridView ID="grdAtividades" CssClass="grdBusca" Width="300px" runat="server"
                                    AutoGenerateColumns="False" OnRowDataBound="grdAtividades_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CHK">
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoAtiv" Value='<%# Eval("coAtiv") %>' runat="server" />
                                                <asp:HiddenField ID="hidCoTemp" Value='<%# Eval("coTempo")%>' runat="server" />
                                                <asp:CheckBox ID="ckSelect" AutoPostBack="true" Checked='<%# Eval("chkSel") %>' OnCheckedChanged="ckSelect_CheckedChange"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="deTema" HeaderText="TEMA">
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tempo" HeaderText="TEMPO">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="margin-right: 0px; margin-top: 0px !important;">
                    <ul>
                        <li class="liGrid2" style="width: 480px; margin-right: 0px; margin-bottom: -10px;">REGISTRO
                            DE FREQUÊNCIA - RELAÇÃO DE ALUNOS</li>
                        <li class="liClear liGrideAluno">
                            <div id="divGridAluno" class="divGridAluno" style="width: 479px; height: 362px;"
                                runat="server">
                                <asp:GridView ID="grdBusca" CssClass="grdBusca" Width="462px" runat="server" AutoGenerateColumns="False"
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
                                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                                            <ItemStyle Width="252px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Frequência">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:HiddenField ID="hdCoFlagFreq" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                                <asp:DropDownList ID="ddlFreq" CssClass="ddlFreq" runat="server">
                                                    <asp:ListItem Text="Presença" Value="S"></asp:ListItem>
                                                    <asp:ListItem Text="Falta" Value="N"></asp:ListItem>
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

        //        $(".chkRepeteFreq").change(function () {
        //            if ($('.chkRepeteFreq').selected()) {
        //                var el = document.getElementById('grdAtividades').getElementsByTagName("CheckBox");
        //                for (var i = 0; i < el.length; i++) {
        //                    if (el[i].type == "CheckBox") {
        //                        alert("Acessou o segundo if!");
        //                        el[i].checked = true;
        //                    }
        //                }
        //            }
        //        });
    </script>
</asp:Content>
