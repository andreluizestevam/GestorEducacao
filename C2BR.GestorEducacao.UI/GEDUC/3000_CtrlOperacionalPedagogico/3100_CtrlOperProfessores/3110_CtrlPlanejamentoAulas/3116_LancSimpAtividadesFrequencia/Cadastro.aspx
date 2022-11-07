<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3116_LancSimpAtividadesFrequencia.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1090px;
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
            margin-top: 30px;
            height: 15px;
            width: 975px;
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
        .divTitulo
        {
            background-color: #EEEEEE;
            height: 15px;
            width: 520px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
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
    <asp:HiddenField runat="server" ID="CO_ATIV_PROF_TURMA" />
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 68px;">
            <asp:HiddenField runat="server" ID="hidTpAulaRequired" />
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
            <label for="ddlSerieCurso" title="Série" class="lblObrigatorio">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série" CssClass="campoSerieCurso"
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
        <%--<li class="liLeft1">
            <label for="ddlBimestre" title="Selecione o Bimestre em que a frequência será lançada"
                class="lblObrigatorio">
                Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" ToolTip="Selecione o Bimestre em que a frequência será lançada"
                runat="server" OnSelectedIndexChanged="ddlBimestre_OnSelectedIndexChanged" AutoPostBack="true">
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
        <li class="liLeft1">
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada"
                class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server" OnSelectedIndexChanged="ddlReferencia_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia"
                CssClass="validatorField" ErrorMessage="A Referência em que a frequência será lançada deve ser informado."
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: 5px;">
            <!-- 30px  -->
            <%-- <ul style="float: left;" runat="server">
                <div class="divTitulo" style="width: 17px; margin-left: -532px;">
                    HOMOLOGAÇÃO DE ATIVIDADES
                </div>
              
                    <!----------------------------------------------------------------------------------------------------------------------------------------------->
                    <div id="divGrid" runat="server" class="divGrid" style="width: 530px; height: 320px;
                        margin-left: -222px;">
                        asdcsad

                    </div>
                
                <!----------------------------------------------------------------------------------------------------------------------------------------------->
            </ul>--%>
            <ul style="float: left;" runat="server">
                <%-- <div class="divTitulo" style="width: 17px; margin-left: -532px;">
                </div>--%>
                <!----------------------------------------------------------------------------------------------------------------------------------------------->
                <li class="liTituloGrid" style="width: 532px; height: 21px !important; margin-right: 0px;
                    margin-top: 10px; background-color: #EEEEEE; text-align: center; font-weight: bold;
                    margin-bottom: 5px">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                        PLANEJAMENTO DE AULAS</label>
                </li>
                <li style="clear: both; margin-top: 5px;" runat="server" id="li1" visible="false">
                    <div style="border: 1px solid #CCCCCC; width: 530px; height: 150px; overflow-y: scroll;">
                        <asp:GridView ID="grdAtividades" CssClass="grdBusca" Width="513px" runat="server"
                            AutoGenerateColumns="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckAtividades_OnCheckedChanged"
                                            AutoPostBack="true" />
                                        <asp:HiddenField runat="server" ID="hdCoMatAtividade" Value='<%# bind("CO_MAT") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdPlanejamento" />
                                        <asp:HiddenField runat="server" ID="hdCoAtividadeProfTurma" Value='<%# bind("CO_ATIV_PROF_TUR") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DT_ATIV" HeaderText="DATA">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DE_TEMA" HeaderText="TEMA">
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DE_RES_ATIV" HeaderText="Resumo da Atividade">
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FL_HOMOL_ATIV" HeaderText="HOMOL">
                                    <ItemStyle Width="10px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <%--<div id="divGrid" runat="server" class="divGrid" style="width: 530px; height: 320px;
                    margin-left: -222px;">
                    asdcsad
                </div>--%>
                <!----------------------------------------------------------------------------------------------------------------------------------------------->
                <%--  <li class="liTituloGrid" style="width: 532px; height: 17px !important; margin-right: 0px;
                    margin-top: 40px; background-color: #AFEEEE; text-align: center; font-weight: bold;
                    margin-bottom: 5px">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                        ATIVIDADES ESCOLARES</label>
                </li>--%>
                <li style="clear: both; margin-top: 5px">
                    <ul id="ulAtividade2" visible="false" runat="server">
                        <li style="clear: both">
                            <label for="txtDataRe" class="lblObrigatorio" title="Data de Realização">
                                Data Realizada</label>
                            <asp:TextBox ID="txtDataRe" ToolTip="Informe a Data de Realização da Atividade" runat="server"
                                CssClass="campoData"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataRe"
                                CssClass="validatorField" ErrorMessage="Data de Realização deve ser informado"
                                Text="*" Display="Static"></asp:RequiredFieldValidator>
                        </li>
                        <li style="margin-left: 10px">
                            <label for="ddlAvaliaAtiv" title="Com nota?">
                                Com nota?</label>
                            <asp:DropDownList ID="ddlAvaliaAtiv" ToolTip="Selecione se Atividade terá nota" Width="47px"
                                runat="server">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 20px; margin-top: 5px; background-color: #AFEEEE; text-align: center;
                            font-weight: bold; width: 370px; margin-bottom: 5px; height: 21px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                REGISTRO DA ATIVIDADE</label>
                        </li>
                </li>
                <li style="clear: both; margin-top: 5px;" runat="server" id="liDisc" visible="false">
                    <div style="border: 1px solid #CCCCCC; width: 530px; height: 170px; overflow-y: scroll;">
                        <asp:GridView ID="grdDisciplinas" CssClass="grdBusca" Width="100%" runat="server"
                            AutoGenerateColumns="False" ToolTip="Grid de Matérias dentro dos parâmetros selecionados"
                            AutoGenerateSelectButton="false" DataKeyNames="CO_MAT">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField Visible="false" DataField="CO_MAT" HeaderText="Cod." SortExpression="CO_MAT"
                                    HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                    <HeaderStyle CssClass="noprint"></HeaderStyle>
                                    <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="20px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckSelect_OnCheckedChanged"
                                            AutoPostBack="true" />
                                        <asp:HiddenField runat="server" ID="hdCoMat" Value='<%# bind("CO_MAT") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdPlanejamento" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_MATERIA" HeaderText="MATÉRIA">
                                    <ItemStyle Width="170px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="TIPO">
                                    <ItemStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTipoAtiv" ToolTip="Selecione o Tipo de Atividade" runat="server"
                                            Width="70px" Enabled="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TEMPO">
                                    <ItemStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTempo" Height="60px"
                                            Width="90px" Enabled="false" ToolTip="Lista de tempos de Aula (Para selecionar mais de um, basta pressionar CTRL e Clicar no tempo desejado)">
                                        </asp:ListBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TEMA / DESCRIÇÃO">
                                    <ItemStyle Width="130px" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTemaAula" Width="130px" ToolTip="Tema da Aula para a Matéria em questão"
                                            ClientIDMode="Static" TextMode="MultiLine" Height="15px" Enabled="false" placeholder="Tema da Atividade"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtDescAtividade" TextMode="MultiLine" Style="margin-top: 5px;"
                                            Height="36px" Width="130px" ToolTip="Descrição da atividade realizada para a Matéria em questão"
                                            placeholder="Descrição da Atividade" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
    </ul>
    <ul style="margin-left: 14px; float: right" runat="server">
        <li>
            <ul>
                <li class="liTituloGrid" style="width: 432px; height: 21px !important; margin-right: 0px;
                    margin-top: 10px; background-color: #FFF5EE; text-align: center; font-weight: bold;
                    margin-bottom: 5px">
                    <ul>
                        <li style="float: left; margin-left: 8px;">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                REGISTRO DE FREQUÊNCIA</label>
                        </li>                      
                        <li style="margin-top: 3px; float: right">
                            <asp:CheckBox runat="server" ID="chkMandaSMSResp" Text="Enviar SMS informativo" CssClass="checkboxLabel"
                                ToolTip="Quando selecionado, envia um SMS para o responsável do(a) Aluno(a) que estiver recebendo falta" />
                        </li>
                         <li style="margin-top: 3px; float: right">
                            <asp:CheckBox runat="server" ID="chkHomologar" Text=" Frequência Homologada" CssClass="checkboxLabel"
                                ToolTip="Quando selecionado, homologa as frequências lançadas no sistema" />
                        </li>
                    </ul>
                </li>
                <%--   <li class="liTituloGrid" style="width: 432px; height: 17px !important; margin-right: 0px;
                    margin-top: 10px; background-color: #FFF5EE; text-align: center; font-weight: bold;
                    margin-bottom: 5px">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; text-align:left; ">
                        REGISTRO DE FREQUÊNCIA</label>
                    <asp:CheckBox style=" margin-right:-130px; margin-top:3px;" runat="server" ID="CheckBox1" Text="Enviar SMS informativo para Responsável de aluno ausente"
                        CssClass="checkboxLabel" ToolTip="Envia um SMS para o responsável do(a) Aluno(a) que estiver recebendo falta" />
                </li>--%>
                <li style="clear: both; margin-top: 5px;">
                    <ul id="ulAlunos2" visible="false" runat="server">
                        <li class="liClear liGrideAluno">
                            <div id="divGridAluno" class="divGridAluno" style="width: 430px; height: 362px; overflow-y: scroll;
                                border: 1px solid #CCC" runat="server">
                                <asp:GridView ID="grdBusca" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                                    AutoGenerateSelectButton="false" DataKeyNames="CO_ALU">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="CO_ALU" HeaderText="Cod." SortExpression="CO_ALU"
                                            HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                            <HeaderStyle CssClass="noprint"></HeaderStyle>
                                            <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NIRE" HeaderText="NIRE">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                                            <ItemStyle Width="222px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="FALTA">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdCoAluno" runat="server" Value='<%# bind("CO_ALU") %>' />
                                                <asp:HiddenField ID="hdCoFlagFreq" runat="server" Value='<%# bind("CO_FLAG_FREQ_ALUNO") %>' />
                                                <asp:HiddenField ID="hdIdFreq" runat="server" Value='<%# bind("ID_FREQU_ALUNO") %>' />
                                                <asp:CheckBox ID="chkTevePresenca" runat="server" Checked='<%# bind("chk") %>' />
                                            </ItemTemplate>
                                            <%--  <ItemTemplate>
                                                <asp:CheckBox ID="chkTevePresenca" Enabled='<%# bind("chk") %>' runat="server"/>
                                            </ItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
    </li> </ul>
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
