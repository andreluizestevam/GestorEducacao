<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3315_HomolAtividadeLetivaProfe.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .checkboxLabelTodos label
        {
            display: inline;
            margin-left: -2px;
            margin-top: 6px;
        }
        /* ESTILO ESTRUTURAL DA PÁGINA */
        /* DIV PRINCIPAL DA PÁGINA */
        #dados
        {
            margin-left: 7px;
            width: 990px;
            height: 453px;
        }
        
        /* DIV DOS FILTROS DA PÁGINA */
        #filtros
        {
            width: 984px;
            margin-left: 3px;
            height: 40px;
        }
        
        /* DIV DA GRID DE ATIVIDADES */
        #atividades
        {
            margin-left: 3px;
            margin-top: 2px;
            float: left;
            width: 490px;
            height: 405px;
        }
        
        /* DIV DA GRID DE FREQUÊNCIAS */
        #frequencias
        {
            margin-right: 3px;
            margin-top: 2px;
            float: right;
            width: 490px;
            height: 405px;
        }
        
        /* DIV DOS CAMPOS DO FORMULÁRIO */
        .divInput
        {
            float: left;
            margin-left: 10px;
            margin-top: 5px;
            margin-bottom: 0;
            position: relative;
            display: inline;
        }
        
        .divMarcar
        {
            float: left;
            margin-left: -5px;
            margin-top: 12px;
            margin-bottom: 0;
            position: relative;
            display: inline;
            height: 20px;
        }
        
        /* DIV PADRÃO PARA GRIDS */
        .divGrid
        {
            height: 130px;
            width: 920px;
            overflow-y: scroll;
            border-bottom: solid #EEEEEE 1px;
            border-top: solid #EEEEEE 1px;
            border-left: solid #EEEEEE 1px;
        }
        
        .divTitulo
        {
            background-color: #EEEEEE;
            height: 15px;
            width: 520px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        /* FIM DO ESTILO ESTRUTURAL DA PÁGINA */
        
        /* ESTILO DE CAMPOS NUMÉRICOS */
        .mascaradecimal
        {
            width: 40px;
            text-align: right;
        }
        .ulDados
        {
            width: 975px;
        }
        
        .liAux
        {
            margin-right: 5px;
            clear: none !important;
            display: inline;
            margin-left: -20px;
        }
        .chkLa label
        {
            display: inline;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
    <!--                                             INICIO DA PÁGINA (DIV - "DADOS")                                                                                                 -->
    <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
    <div id="dados">
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <!--                                             INICIO DOS FILTROS UTILIZADOS NA PÁGINA (DIV - "FILTROS")                                                                        -->
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <div id="filtros">
            <ul id="ulDados" class="ulDados">
                <li style="margin-left: 40px">
                    <label for="ddlAno" title="Selecione o ano de referência" class="lblObrigatorio">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" runat="server" ToolTip="Selecione o Ano de referência"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" CssClass="ddlAno">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="ddlAno"
                        CssClass="validatorField" ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="ddlModalidade" title="Selecione a modalidade" class="lblObrigatorio">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" runat="server" ToolTip="Selecione a modalidade"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                        CssClass="ddlModalidade">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" ControlToValidate="ddlModalidade"
                        CssClass="validatorField" ErrorMessage="Modalidade deve ser informado" Text="*"
                        Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="ddlSerie" title="Selecione a série" class="lblObrigatorio">
                        S&eacute;rie</label>
                    <asp:DropDownList ID="ddlSerie" Width="65px" runat="server" ToolTip="Selecione a série" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSerie" runat="server" ControlToValidate="ddlSerie"
                        CssClass="validatorField" ErrorMessage="Série deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="ddlTurma" title="Selecione a turma" class="lblObrigatorio">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" runat="server" ToolTip="Selecione a turma" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTurma" runat="server" ControlToValidate="ddlTurma"
                        CssClass="validatorField" ErrorMessage="Turma deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <%--<li style="margin-left: 10px;">
                    <label for="ddlTrimestre" title="Selecione o Trimestre em que a frequência será lançada"
                        class="lblObrigatorio">
                        Referência</label>
                    <asp:DropDownList ID="ddlTrimestre" ToolTip="Selecione o Trimestre em que a frequência será lançada"
                        OnSelectedIndexChanged="ddlTrimestre_OnSelectedIndexChanged" AutoPostBack="true"
                        runat="server">
                        <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="T1" Text="Trimestre 1"></asp:ListItem>
                        <asp:ListItem Value="T2" Text="Trimestre 2"></asp:ListItem>
                        <asp:ListItem Value="T3" Text="Trimestre 3"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTrimestre" runat="server" ControlToValidate="ddlTrimestre"
                        CssClass="validatorField" ErrorMessage="O Trimestre em que a frequência será lançada deve ser informado."
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>--%>
                <li style="margin-left: 10px;">
                    <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                        Referência</label>
                    <asp:DropDownList ID="ddlReferencia" Width="68px" ToolTip="Selecione a Referência em que a frequência será lançada"
                        runat="server" OnSelectedIndexChanged="ddlReferencia_OnSelectedIndexChanged" AutoPostBack="true">   
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
                    ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="txtDataFreq" class="lblObrigatorio" title="Período de Frequência">
                        Período da Atividade</label>
                    <asp:TextBox ID="txtDataFreq" CssClass="campoData" runat="server" ToolTip="Informe a data da frequência"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDataFreq"
                        CssClass="validatorField" ErrorMessage="Período de início deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liAux" style="margin-top: 12px;">
                    <label class="labelAux">
                        até</label>
                </li>
                <li style="margin-top: 12px; margin-left: 1px;">
                    <asp:TextBox ID="txtDataFreqAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataFreqAte"
                        CssClass="validatorField" ErrorMessage="Período de final deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="ddlMateria" title="Selecione a matéria" class="lblObrigatorio">
                        Mat&eacute;rias</label>
                    <asp:DropDownList ID="ddlMateria" runat="server" ToolTip="Selecione a matéria" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvMateria" runat="server" ControlToValidate="ddlMateria"
                        CssClass="validatorField" ErrorMessage="Matéria deve ser informado" Text="*"
                        Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px;">
                    <label for="ddlTempo" title="Selecione o tempo da frequência" class="lblObrigatorio">
                        Tempo</label>
                    <asp:DropDownList ID="ddlTempo" Width="135px" runat="server" ToolTip="Selecione o tempo da frequência"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTempo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTempo" runat="server" ControlToValidate="ddlTempo"
                        CssClass="validatorField" ErrorMessage="Tempo da frequência deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px; margin-top: 10px;">
                    <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                    <img title="Clique para gerar as Grides de Notas e Frequências."
                            alt="Icone de Pesquisa das Grides." 
                            src="/Library/IMG/Gestor_BtnPesquisa.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <br />
        </div>
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <!--                                             FINAL DOS FILTROS UTILIZADOS NA PÁGINA (DIV - "FILTROS")                                                                         -->
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <div>
            <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
            <!--                                             INICIO DA GRID DE APRESENTAÇÃO DAS ATIVIDADES (DIV - "ATIVIDADES")                                                               -->
            <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
            <div id="atividades" style="margin-left: 270px">
                <!-- GRID DE NOTAS -->
                <div class="divMarcar">
                    <ul style="margin-left: -227px;">
                        <li style="float: left;">
                            <div>
                                <asp:HiddenField ID="hidCoAtivSel" runat="server" Value="0" />
                                <asp:HiddenField ID="hidHomAtivSel" runat="server" Value="" />
                                <asp:CheckBox ID="cbTodos" runat="server" AutoPostBack="True" CssClass="checkboxLabelTodos"
                                    OnCheckedChanged="cbTodos_CheckedChanged" Text="Marcar todos os Registros para Homologação da(s) Atividade(s)" />
                            </div>
                        </li>
                        <li style="float: right;">
                            <div style="margin-bottom: -7px;">
                                <asp:CheckBox runat="server" ID="chkHomolFreq" Text="Homologa Frequência(s) da(s) Atividade(s) selecionada(s)"
                                    CssClass="chkLa" Checked="true" />
                            </div>
                        </li>
                    </ul>
                    <div class="divTitulo" style="width: 901px; margin-left: -222px;">
                        HOMOLOGAÇÃO DE ATIVIDADES
                    </div>
                    <div id="divGrid" runat="server" class="divGrid" style="width: 900px; height: 350px;
                        margin-left: -222px;">
                        <asp:GridView ID="grdAtividades" CssClass="grdBusca" Width="100%" runat="server"
                            AutoGenerateColumns="False" OnRowDataBound="grdAtividades_RowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CHK">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="True" OnCheckedChanged="ckSelect_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HOM">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckHomol" runat="server" Enabled="false" Checked='<%# bind("chkValid") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DATA">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDataAtiv" runat="server" Enabled="false" Width="60px" Style="text-align: center !important;"
                                            Text='<%# bind("DT_ATIV") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resumo da Atividade">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdCoAtv" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                                        <asp:HiddenField ID="hdFlHomol" runat="server" Value='<%# bind("FL_HOMOL_ATIV") %>' />
                                        <asp:TextBox ID="txtResumo" runat="server" Enabled="false" Width="732px" Height="26px"
                                            TextMode="multiline" Text='<%# bind("DE_RES_ATIV") %>'>
                                        </asp:TextBox>
                                        <asp:HiddenField ID="hdPlaAula" runat="server" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                                        <asp:HiddenField ID="hidCoMat" runat="server" Value='<%# bind("CO_MAT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
                <!--                                             FINAL DA GRID DE APRESENTAÇÃO DAS ATIVIDADES (DIV - "ATIVIDADES")                                                                -->
                <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
                <%--<!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
            <!--                                             INICIO DA GRID DE APRESENTAÇÃO DAS FREQUÊNCIAS (DIV - "FREQUENCIAS")                                                             -->
            <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
            <div id="frequencias">
                <!-- GRID DE FREQUÊNCIAS -->
                <div class="divMarcar">
                    <asp:HiddenField ID="hidCoAtivSel" runat="server" Value="0" />
                    <asp:HiddenField ID="hidHomAtivSel" runat="server" Value="" />
                    <asp:CheckBox ID="cbTodos" runat="server" AutoPostBack="True" CssClass="checkboxLabelTodos"
                        OnCheckedChanged="cbTodos_CheckedChanged" Text="Marcar todos" />
                </div>
                <div class="divTitulo" style="width: 480px; margin-top: 12px; margin-left: 10px;">
                    HOMOLOGAÇÃO DE FREQUÊNCIAS
                </div>
                <div id="divFreq" runat="server" class="divGrid" style="width: 479px; height: 350px;
                    margin-left: 10px;">
                    <asp:GridView ID="grdFreq" CssClass="grdBusca" Width="462px" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="grdFreq_RowDataBound">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="CHK">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckSelect" Enabled='<%# bind("chk") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NIRE" HeaderText="NIRE">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                                <ItemStyle Width="252px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="FREQU">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                    <asp:HiddenField ID="hdIdFrequ" runat="server" Value='<%# bind("ID_FREQU_ALUNO") %>' />
                                    <asp:HiddenField ID="hdCoFlagFreq" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                    <asp:HiddenField ID="hdFlHomolA" runat="server" Value='<%# bind("FL_HOMOL_FREQU") %>' />
                                    <asp:DropDownList ID="ddlFreq" Enabled='<%# bind("chk") %>' CssClass="ddlFreq" runat="server">
                                        <asp:ListItem Text="Presença" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Falta" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
            <!--                                             FINAL DA GRID DE APRESENTAÇÃO DAS FREQUÊNCIAS (DIV - "FREQUENCIAS")                                                              -->
            <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->--%>
            </div>
        </div>
        <div id="divConfirmExclusão" style="display: none; height: 100px !important;">
            <ul>
                <li style="margin-bottom: 10px;">
                    <label>
                        Selecione o que deseja excluir:</label>
                </li>
                <li style="clear: both">
                    <asp:CheckBox runat="server" ID="chkExcluiAtividades" Text="Atividade Selecionada"
                        ToolTip="Marque caso deseje excluir a atividade selecionada" CssClass="checkboxLabelTodos" />
                </li>
                <li style="clear: both">
                    <asp:CheckBox runat="server" ID="chkExcluiFrequencias" Text="Frequência(s) Correspondente(s)"
                        ToolTip="Marque caso deseje excluir a(s) frequência(s) selecionada(s)" CssClass="checkboxLabelTodos" />
                </li>
                <li runat="server" id="liBtnGrdFinanMater" class="liBtnGrdFinan" style="clear: both;
                    margin: 15px 0 0 25px; width: 110px">
                    <asp:LinkButton ID="lnkExcluItens" OnClick="lnkExcluItens_OnClick" ValidationGroup="vgMontaGridMensa"
                        runat="server" Style="margin: 0 auto;" ToolTip="Confirmar exclusão dos itens">
                        <asp:Label runat="server" ID="Label166" ForeColor="GhostWhite" Text="CONFIRMAR EXCLUSÃO"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <!--                                             FINAL DA PÁGINA (DIV - "DADOS")                                                                                                  -->
        <!---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->
        <script type="text/javascript">

            function AbreModal() {
                $('#divConfirmExclusão').dialog({ autoopen: false, modal: true, width: 200, height: 80, resizable: false, title: "ITENS PARA EXCLUSÃO",
                    //                open: function () { $('#divLoadRegisOcorr').show(); }
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            }

        </script>
</asp:Content>
