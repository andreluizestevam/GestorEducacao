<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3100_CtrlOperProfessores._3110_CtrlPlanejamentoAulas._3114_LancamentoSimplificadoAtividadeDiarioClasse.Cadastro" %>

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
        <li style="margin-left: 17px;">
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
            <label for="ddlTrimestre" title="Selecione o Trimestre em que a frequência será lançada"
                class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlTrimestre" ToolTip="Selecione o Trimestre em que a frequência será lançada"
                runat="server">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="B1" Text="Trimestre 1"></asp:ListItem>
                <asp:ListItem Value="B2" Text="Trimestre 2"></asp:ListItem>
                <asp:ListItem Value="B3" Text="Trimestre 3"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTrimestre" runat="server" ControlToValidate="ddlTrimestre"
                CssClass="validatorField" ErrorMessage="O Trimestre em que a atividade será lançada deve ser informada."
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>--%>
        <li>
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
        <li class="liGrid3" style="clear: both; margin-bottom: 5px;">ATIVIDADES ESCOLARES</li>
        <li class="liClear">
            <ul>
                <li>
                    <label for="txtDataRe" class="lblObrigatorio" title="Data de Realização">
                        Data Realizada</label>
                    <asp:TextBox ID="txtDataRe" ToolTip="Informe a Data de Realização da Atividade" runat="server"
                        CssClass="campoData"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataRe"
                        CssClass="validatorField" ErrorMessage="Data de Realização deve ser informado"
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 10px">
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
                <li style="margin-left: 10px">
                    <label for="ddlAvaliaAtiv" title="Com nota?">
                        Com nota?</label>
                    <asp:DropDownList ID="ddlAvaliaAtiv" ToolTip="Selecione se Atividade terá nota" CssClass="ddlAvaliaAtiv"
                        runat="server">
                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <li style="clear: both; margin-top: 5px;">
            <div style="border: 1px solid #CCCCCC; width: 973px; height: 250px; overflow-y: scroll;">
                <asp:GridView ID="grdDisciplinas" CssClass="grdBusca" Width="100%" runat="server"
                    AutoGenerateColumns="False" ToolTip="Grid de Matérias dentro dos parâmetros selecionados">
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
                                <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckSelect_OnCheckedChanged"
                                    AutoPostBack="true" />
                                <asp:HiddenField runat="server" ID="hdCoMat" Value='<%# bind("CO_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NO_MATERIA" HeaderText="MATÉRIA">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="TEMPO">
                            <ItemStyle Width="140px" />
                            <ItemTemplate>
                                <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstTempo" Height="60px"
                                    Width="150px" Enabled="false" ToolTip="Lista de tempos de Aula (Para selecionar mais de um, basta pressionar CTRL e Clicar no tempo desejado)">
                                </asp:ListBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TEMA DA AULA">
                            <ItemStyle Width="180px" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtTemaAula" TextMode="MultiLine" Enabled="false"
                                    Height="60px" Width="190px" ToolTip="Tema da Aula para a Matéria em questão"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DESCRIÇÃO DA ATIVIDADE">
                            <ItemStyle Width="320px" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtDescAtividade" Enabled="false" TextMode="MultiLine"
                                    Height="60px" Width="340px" ToolTip="Descrição da atividade realizada para a Matéria em questão"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
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
