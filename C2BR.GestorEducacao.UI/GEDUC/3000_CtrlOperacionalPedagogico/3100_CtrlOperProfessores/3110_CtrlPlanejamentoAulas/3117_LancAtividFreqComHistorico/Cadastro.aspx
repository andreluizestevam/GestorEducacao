<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs"
 Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3117_LancAtividFreqComHistorico.Cadastro"%>
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
        .lblNormal { color:Black; }
        .lblFaltas { color:Red; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField Value="" ID="hdCoAtivProfTur" runat="server" />
    <ul id="ulDados" class="ulDados">
        <li class="liData">
            <label for="txtData" class="lblObrigatorio" title="Data da Frequência">
                Data Frequência</label>
            <asp:TextBox ID="txtData" CssClass="campoData" runat="server" ToolTip="Informe a Data"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="tfv" ControlToValidate="txtData"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" Style="width: 130px !important;" ToolTip="Selecione a Modalidade"
                CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label for="ddlSerieCurso" title="Série" class="lblObrigatorio">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" Style="width: 80px !important;" ToolTip="Selecione a Série"
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
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server" OnSelectedIndexChanged="ddlReferencia_OnSelectedIndexChanged" AutoPostBack="true">   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <asp:Label ID="lblTurno" runat="server" Text="Turno" AssociatedControlID="ddlTurno"></asp:Label>
            <asp:DropDownList ID="ddlTurno" runat="server" AutoPostBack="true" Width="60px" CssClass="ddlTurno"
                OnSelectedIndexChanged="ddlTurno_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="M">Matutino</asp:ListItem>
                <asp:ListItem Value="V">Vespertino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liEspaco">
            <asp:Label ID="Label1" runat="server" Text="Tempo de aula" AssociatedControlID="ddlTempos"
                CssClass="lblObrigatorio"></asp:Label>
            <asp:DropDownList ID="ddlTempos" runat="server" OnSelectedIndexChanged="ddlTempos_SelectedIndexChanged"
                AutoPostBack="True" Width="80px" CssClass="ddlTipoTempo">
            </asp:DropDownList>
            <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlTempos"
                ErrorMessage="Informe o tipo de registro">*</asp:RequiredFieldValidator>--%>
        </li>
        <li class="liDisciplina">
            <label for="ddlDisciplina" title="Disciplina">
                Matéria</label>
            <asp:DropDownList ID="ddlDisciplina" Enabled="false" Style="width: 150px !important;"
                ToolTip="Selecione a Matéria" CssClass="campoMateria" runat="server" AutoPostBack="True"
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
        <li>
            <ul>
                <li class="liEspaco liLeft1;" style="float: left;">
                    <div runat="server" visible="True" id="divGridTempos" class="divGridTempos">
                        <asp:GridView ID="grdTempos" runat="server" AutoGenerateColumns="False" CssClass="grdBusca grdBuscaTempo">
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="MARCAR">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbMarcar" runat="server" Checked='<%# bind("marcarTempo") %>' />
                                        <asp:Label runat="server" ID="lblNomeTempo" Text='<%# bind("nomeTempo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="INÍCIO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInicio" runat="server" CssClass="txtHora" Text='<%# bind("inicioTempo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TÉRMINO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTermino" runat="server" CssClass="txtHora" Text='<%# bind("terminoTempo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                            <RowStyle CssClass="rowStyle" Height="5px" />
                        </asp:GridView>
                    </div>
                </li>
                <li runat="server" id="liGridAluno" style="margin-left: 180px; margin-top: 0px !important; float: right">
                    <ul>
                        <li class="liGrid2" style="width: 779px; margin-right: 0px; margin-bottom: -10px;">REGISTRO
                            DE FREQUÊNCIA - RELAÇÃO DE ALUNOS</li>
                        <li class="liClear liGrideAluno">
                            <div id="divGridAluno" class="divGridAluno" style="width: 779px; height: 362px;"
                                runat="server">
                                <asp:GridView ID="grdBusca" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="NIRE" HeaderText="NIRE">
                                            <ItemStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                                            <ItemStyle Width="252px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="FALTA">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:HiddenField ID="hdCoFlagFreq" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                                <asp:CheckBox runat="server" ID="chkFreq" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt1" Text='<%# bind("DT1") %>' CssClass='<%# bind("CLASSE_DT1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt2" Text='<%# bind("DT2") %>' CssClass='<%# bind("CLASSE_DT2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt3" Text='<%# bind("DT3") %>' CssClass='<%# bind("CLASSE_DT3") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt4" Text='<%# bind("DT4") %>' CssClass='<%# bind("CLASSE_DT4") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt5" Text='<%# bind("DT5") %>' CssClass='<%# bind("CLASSE_DT5") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt6" Text='<%# bind("DT6") %>' CssClass='<%# bind("CLASSE_DT6") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt7" Text='<%# bind("DT7") %>' CssClass='<%# bind("CLASSE_DT7") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt8" Text='<%# bind("DT8") %>' CssClass='<%# bind("CLASSE_DT8") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt9" Text='<%# bind("DT9") %>' CssClass='<%# bind("CLASSE_DT9") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="dt10" Text='<%# bind("DT10") %>' CssClass='<%# bind("CLASSE_DT10") %>'></asp:Label>
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
</asp:Content>
