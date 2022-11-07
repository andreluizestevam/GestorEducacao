<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8220_AnalisePrevia._8221_AnalisePreviaCompl.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados
        {
            width: 1120px;
        }
        .campoHora
        {
            font-weight: bold;
            display: inline;
            width: 30px;
        }
        .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        .ulPer label
        {
            text-align: left;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px !important;
        }
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        .chk label
        {
            display: inline;
            margin-left: -6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidIdAvaliacao" />
    <ul class="ulDados">
        <li>
            <ul class="divEncamMedicoGeral">
                <li style="clear: both; margin-left: 3px;">
                    <ul>
                        <li class="liTituloGrid" style="width: 442px; height: 20px !important; margin-right: 0px;
                            background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        AGENDAMENTO</label>
                                    <asp:HiddenField runat="server" ID="hidCoAlu" />
                                </li>
                                <li style="margin-left: 18px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="IniPeriAG" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeriAG"></asp:RequiredFieldValidator>
                                            <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="FimPeriAG" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeriAG"></asp:RequiredFieldValidator><br />
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="Label3">
                                                Situação
                                            </asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlSituacao">
                                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Em Análise" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="Encaminhado" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqAgendamentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <div style="width: 440px; height: 95px; border: 1px solid #CCC; overflow-y: scroll" id="divPacien">
                            <input type="hidden" id="divPacien_posicao" name="divPacien_posicao" />
                                <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    OnRowDataBound="grdPacientes_OnRowDataBound" ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação em Aberto<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidIdAgendaAval" Value='<%# Eval("ID_AGEND_AVALI") %>' runat="server" />
                                                <asp:CheckBox ID="chkSelectPaciente" runat="server" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                                    AutoPostBack="true" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PACIENTE">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NU_REGIS" HeaderText="Nº REGISTRO">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="INF">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfAgenda" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                                    ToolTip="Apresenta informações do agendamento/recepção de avaliação" Style="width: 13px !important;
                                                    height: 13px !important; margin: 0 0 0 0 !important" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl="/Library/IMG/PGS_PacienteNaoChegou.ico"
                                                    ToolTip="Status do agendamento de avaliação" Style="width: 16px !important; height: 16px !important;
                                                    margin: 0 0 0 0 !important" OnClick="imgSituacao_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: -5px;">
                    <ul>
                        <li class="liTituloGrid" style="width: 232px; height: 20px !important; margin-right: 0px;
                            background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        INFORMAÇÕES/DIAGNÓSTICO</label>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both">
                            <asp:TextBox runat="server" ID="txtNecessidade" TextMode="MultiLine" Width="230px"
                                Height="95px" ReadOnly="true"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: -6px; margin-top: -2px;">
                    <ul>
                        <li style="clear: both">
                            <div style="width: 280px; height: 124px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                <asp:GridView ID="grdItensRecep" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação para esta recepção<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OBS">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfoObser" ImageUrl="/Library/IMG/Gestor_Serviços.png"
                                                    ToolTip="Informações do Plano de Saúde" OnClick="imgInfoObser_OnClick" Style="margin: 0 0 0 0 !important;
                                                    width: 16px !important; height: 16px !important;" />
                                                <asp:TextBox runat="server" ID="txtObservacao" Style="margin: 2px 0px 2px 0px !important;
                                                    width: 70px; height: 26px;" TextMode="MultiLine" Visible="false" Text='<%# Eval("OBSERVACAO") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="text-align: center; margin: 6px 0 0 3px">
                    <ul>
                        <li class="liTituloGrid" style="width: 975px; height: 20px !important; margin-right: 0px;
                            background-color: #FFEC8B; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: none">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        AVALIAÇÃO PRÉVIA DO PACIENTE E ENCAMINHAMENTO</label>
                                </li>
                                <li style="margin-left: 23px; float: right; margin-top: 2px;"></li>
                            </ul>
                        </li>
                        <li style="width: 975px">
                            <ul>
                                <li style="width: 435px; margin-left: 0px;">
                                    <ul>
                                        <li>
                                            <ul style="width: 435px">
                                                <li class="liTituloGrid" style="width: 370px; height: 20px !important; margin-left: -5px;
                                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                                    <ul>
                                                        <li style="margin: 0 0 0 10px; float: left">
                                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                                QUESTIONÁRIOS AVALIATIVOS</label>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li id="li3" runat="server" title="Clique para Adicionar as Questões da Avaliação"
                                                    class="liBtnAddA" style="float: right; margin: -25px -2px 3px 5px; width: 60px">
                                                    <img alt="" class="imgAdd" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                                        height="15px" width="15px" />
                                                    <asp:LinkButton ID="btnAddForm" runat="server" class="btnLabel" OnClick="btnAddForm_OnClick">Adicionar</asp:LinkButton>
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="clear: both; margin: -7px 0 0 0;">
                                            <div style="width: 440px; height: 100px; border: 1px solid #CCC; overflow-y: scroll">
                                                <asp:GridView ID="grdQuestionario" CssClass="grdBusca" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="true">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhum Questionário associado<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="QUESTIONÁRIO">
                                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hidNuAval" Value='<%# Eval("QUESTIONARIO") %>' />
                                                                <asp:DropDownList runat="server" ID="ddlQuest" Width="385px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li style="margin-left: 7px;">
                                    <ul>
                                        <li class="liTituloGrid" style="width: 302px; height: 20px !important; background-color: #EEEEE0;
                                            text-align: center; font-weight: bold; float: left">
                                            <ul>
                                                <li style="margin: 0 0 0 10px; float: left">
                                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                        CONSIDERAÇÕES AVALIADOR</label>
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="clear: both; margin-top: -2px;">
                                            <asp:TextBox runat="server" ID="txtConsidAvaliador" Style="width: 300px; height: 100px !important;"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                                <li style="width: 200px; margin-left: -0px;">
                                    <ul>
                                        <li>
                                            <ul style="width: 204px">
                                                <li class="liTituloGrid" style="width: 139px; height: 20px !important; margin-left: -5px;
                                                    background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                                                    <ul>
                                                        <li style="margin: 0 0 0 10px; float: left">
                                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                                CID's</label>
                                                        </li>
                                                    </ul>
                                                </li>
                                                <li id="li4" runat="server" title="Clique para Adicionar as Questões da Avaliação"
                                                    class="liBtnAddA" style="float: right; margin: -25px -2px 3px 5px; width: 60px">
                                                    <img alt="" class="imgAdd" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                                        height="15px" width="15px" />
                                                    <asp:LinkButton ID="lnkAdicionarCID" runat="server" class="btnLabel" OnClick="lnkAdicionarCID_OnClick">Adicionar</asp:LinkButton>
                                                </li>
                                            </ul>
                                        </li>
                                        <li style="clear: both; margin: -7px 0 0 0;">
                                            <div style="width: 209px; height: 100px; border: 1px solid #CCC; overflow-y: scroll">
                                                <asp:GridView ID="grdCids" CssClass="grdBusca" runat="server" Style="width: 100%;
                                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                                    ShowHeaderWhenEmpty="true">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhum CID<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CID">
                                                            <ItemStyle Width="170px" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hidIdCID" Value='<%# Eval("CID") %>' />
                                                                <asp:DropDownList runat="server" ID="ddlCID" Width="160px">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EX">
                                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                                    ToolTip="Excluir CID" OnClick="imgExcCID_OnClick" />
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
                </li>
                <li style="width: 975px; clear: both; margin-left: 8px; margin-top: -2px;">
                    <ul style="float: left; width: 600px">
                        <li>
                            <ul style="width: 670px">
                                <li class="liTituloGrid" style="width: 580px; height: 20px !important; margin-left: -5px;
                                    background-color: #fa8072; text-align: center; font-weight: bold; float: left">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 4px;">
                                                DEFINIÇÃO DE PROFISSIONAIS, PROCEDIMENTOS E AGENDA PRÉVIA</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="float: right; margin-top: -22px;">
                                    <img alt="" class="imgAdd" title="Adicionar definição" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                        height="15px" width="15px" />
                                    <asp:LinkButton runat="server" ID="btnMaisDefinicoes" Style="margin-left: 1px;" ToolTip="Adicionar novo Item de Avaliação"
                                        Enabled="false" OnClick="btnMaisDefinicoes_OnClick"><span style="color:#FF7050">INSERIR MAIS</span></asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin: -7px 0 0 0;">
                            <div style="width: 670px; height: 113px; border: 1px solid #CCC; overflow-y: scroll" id="divProfi">
                            <input type="hidden" id="divProfi_posicao" name="divProfi_posicao" />
                                <asp:GridView ID="grdProfissionais" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default; height: 30px !important;" AutoGenerateColumns="false" AllowPaging="false"
                                    GridLines="Vertical" ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum Item para esta Avaliação<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdItemAval" Value='<%# Eval("IDITEMAVAL") %>' />
                                                <asp:CheckBox ID="chkselectDef" runat="server" Enabled="false" OnCheckedChanged="chkselectDef_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CLASS FUNC.">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidCoClassP" Value='<%# Eval("CLASSFUNC") %>' />
                                                <asp:DropDownList runat="server" ID="ddlClassFuncional" OnSelectedIndexChanged="ddlClassFuncional_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Style="margin: 0 0 0 0; width: 70px;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROFISSIO">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("PROFI") %>' />
                                                <asp:DropDownList runat="server" ID="ddlProfissional" Style="margin: 0 0 0 0; width: 70px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkProcedFatur" ToolTip="Marque caso o procedimento seja para faturamento"
                                                    Style="margin: 0 0 0 0 !important;" Checked="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkProcedAtend" ToolTip="Marque caso o procedimento seja para atendimento"
                                                    Style="margin: 0 0 0 0 !important;" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="+INF">
                                            <ItemStyle Width="14px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgINF" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                                    ToolTip="Informações" Style="margin: 0 0 0 0; width: 15px !important; height: 14px !important;"
                                                    OnClick="imgINF_OnClick" />
                                                <asp:HiddenField runat="server" ID="hidIdOper" Value='<%# Eval("OPER") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdPlan" Value='<%# Eval("PLAN") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROCED">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidIdProc" Value='<%# Eval("PROCED") %>' />
                                                <asp:DropDownList runat="server" ID="ddlProced" Style="margin: 0 0 0 0;">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QSP">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidQSP" Value='<%# Eval("QSP") %>' />
                                                <asp:TextBox runat="server" ID="txtQSP" Width="20px" Style="margin: 0 0 0 0;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INÍCIO">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidDtInicio" Value='<%# Eval("INICIO") %>' />
                                                <asp:TextBox runat="server" ID="txtDataInicio" CssClass="campoData" Style="margin: 0 0 0 0;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TÉRMINO">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hidDtFinal" Value='<%# Eval("TERMINO") %>' />
                                                <asp:TextBox runat="server" ID="txtDataTermino" CssClass="campoData" Style="margin: 0 0 0 0;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EX">
                                            <ItemStyle Width="12px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgExcDef" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                    ToolTip="Exclui a definição em questão" OnClick="imgExcDef_OnClick" Style="margin: 0 0 0 0;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRV">
                                            <ItemStyle Width="12px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgGravar" ImageUrl="/Library/IMG/Gestor_IcoSalvar.png"
                                                    ToolTip="Grava a definição preenchida" OnClick="imgGravar_OnClick" Style="margin: 0 0 0 0;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                    <ul style="float: right; margin-right: -10px;">
                        <li>
                            <asp:HiddenField runat="server" ID="hidCoCol" />
                            <asp:HiddenField runat="server" ID="hidDtInicio" />
                            <asp:HiddenField runat="server" ID="hidDtFinal" />
                            <ul style="width: 290px">
                                <li class="liTituloGrid" style="width: 290px; height: 40px !important; margin-left: -5px;
                                    background-color: #c1ffc1; text-align: center; font-weight: bold; float: left">
                                    <ul style="float: left">
                                        <li style="margin: 8px 0 0 8px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 4px;">
                                                AGENDA</label>
                                        </li>
                                    </ul>
                                    <ul style="float: right; margin-top: 9px;">
                                        <li style="margin-left: -10px !important;">
                                            <label>
                                                D</label>
                                            <asp:CheckBox runat="server" ID="chkDom" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Domingos" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                S</label>
                                            <asp:CheckBox runat="server" ID="chkSeg" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Segundas"
                                                Checked="true" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                T</label>
                                            <asp:CheckBox runat="server" ID="chkTer" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Terças"
                                                Checked="true" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                Q</label>
                                            <asp:CheckBox runat="server" ID="chkQua" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quartas"
                                                Checked="true" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                Q</label>
                                            <asp:CheckBox runat="server" ID="chkQui" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quintas"
                                                Checked="true" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                S</label>
                                            <asp:CheckBox runat="server" ID="chkSex" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Sextas"
                                                Checked="true" />
                                        </li>
                                        <li style="margin-left: -11px;">
                                            <label>
                                                S</label>
                                            <asp:CheckBox runat="server" ID="chkSab" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Sábados" />
                                        </li>
                                        <li style="margin: -2px 0 0 -5px;">
                                            <label>
                                                Intervalo</label>
                                            <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início" CssClass="campoHora">
                                            </asp:TextBox>
                                            à
                                            <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término" CssClass="campoHora">
                                            </asp:TextBox>
                                        </li>
                                        <li style="margin:-7px 0 0 3px;">
                                            <asp:ImageButton ID="imgbAgenda" Width="17" Height="17" ImageUrl="~/Library/IMG/Gestor_ServicosAgendaContatos.png" runat="server"
                                                OnClick="imgbAgenda_OnClick" ToolTip="Ir para a funcionalidade de agendamento completo" OnClientClick="return confirm('Ao ir para a funcionalidade de agendamento completo o trabalho que não foi salvo será perdido, tem certeza que deseja continuar?')" />
                                        </li>
                                        <li style="margin-top: 13px; margin-left: -14px;">
                                            <asp:ImageButton ID="imgPesqAgenda" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                OnClick="imgPesqAgenda_OnClick" ToolTip="Pesquisar horários do profissional indicado" />
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                                    <div style="width: 288px; height: 93px; border: 1px solid #CCC; overflow-y: scroll"
                                        id="divAgendaAt">
                                        <input type="hidden" id="divAgendaAt_posicao" name="divAgendaAt_posicao" />
                                        <asp:GridView ID="grdAgenda" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                            ShowHeaderWhenEmpty="true" OnRowDataBound="grdAgenda_OnRowDataBound">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhuma Agenda nesses parâmetros<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="CK">
                                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGEND_HORAR") %>' />
                                                        <asp:CheckBox runat="server" ID="chkSelectAgend" Enabled='<%# Eval("CHK_HABILITA") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DTHR" HeaderText="DATA E HORA">
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_ALU_V" HeaderText="NOME DO PACIENTE">
                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="margin-top: 20px; display: none">
                    <fieldset title="Opções para salvar status da análise prévia" style="padding: 8px;">
                        <legend>AÇÃO</legend>
                        <ul>
                            <li id="li17" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                margin-bottom: 2px; clear: none !important; width: 70px">
                                <asp:LinkButton ID="lnkEmAnalise" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkEmAnalise_OnClick">
                                    <asp:Label runat="server" ID="Label32" Text="EM ANÁLISE" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li1" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                clear: both !important; width: 70px">
                                <asp:LinkButton ID="lnkEncaminhar" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkEncaminhar_OnClick">
                                    <asp:Label runat="server" ID="Label1" Text="ENCAMINHAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                            <li id="li2" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                clear: both !important; width: 70px; margin-top: 18px;">
                                <asp:LinkButton ID="lnkCancelar" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkCancelar_OnClick">
                                    <asp:Label runat="server" ID="Label2" Text="CANCELAR" Style="margin-left: 4px;"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </fieldset>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowInfosPaciente" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowInfosPlano" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciMODLOG" Enabled="false" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Nº Registro</label>
                        <asp:TextBox runat="server" ID="txtNuRegistroMODLOG" Enabled="false" Width="100px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Sexo</label>
                        <asp:TextBox runat="server" ID="txtSexoMODLOG" Enabled="false" Width="10px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadeMODLOG" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Questionário associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Data_V" HeaderText="DATA">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PROFI" HeaderText="RESPONSÁVEL">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                        <ItemStyle HorizontalAlign="Left" Width="65px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                margin: 0 0 0 0 !important" title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowInfoPlano" style="display: none; height: 96px !important;">
                <asp:HiddenField runat="server" ID="hidIndexGridRefer" />
                <ul class="ulDados" style="width: 300px; margin-top: 0px !important">
                    <li>
                        <label>
                            Operadora</label>
                        <asp:DropDownList runat="server" ID="ddlOperadora" Width="230px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both">
                        <label>
                            Plano</label>
                        <asp:DropDownList runat="server" ID="ddlPlano" Width="190px">
                        </asp:DropDownList>
                    </li>
                    <li id="liConfPlan" runat="server" class="liBtnAddA" style="margin-top: 0px !important;
                        clear: both !important; height: 15px;" runat="server">
                        <asp:LinkButton ID="lnkConfirmarPlano" runat="server" ValidationGroup="atuEndAlu"
                            OnClick="lnkConfirmarPlano_OnClick" ToolTip="Armazena as informações na solicitação em questão">
                            <asp:Label runat="server" ID="Label7" Text="CONFIRMAR" Style="margin-left: 4px;"></asp:Label>
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowObservacao" style="display: none; height: 120px !important;">
                <asp:HiddenField runat="server" ID="HiddenField2" />
                <ul class="ulDados" style="width: 400px; margin-top: 0px !important">
                    <li>
                        <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="width: 400px;
                            height: 130px"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">

        window.onload = function () {
            mantemScrollPac();
            mantemScrollAgenda();
            mantemScrollProfi();
        }

        function mantemScrollPac() {
            var div = document.getElementById("divPacien");
            var div_position = document.getElementById("divPacien_posicao");
            var position = parseInt('<%= Request.Form["divPacien_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function mantemScrollProfi() {
            var div = document.getElementById("divProfi");
            var div_position = document.getElementById("divProfi_posicao");
            var position = parseInt('<%= Request.Form["divProfi_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function mantemScrollAgenda() {
            var div = document.getElementById("divAgendaAt");
            var div_position = document.getElementById("divAgendaAt_posicao");
            var position = parseInt('<%= Request.Form["divAgendaAt_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function AbreModalObservacao() {
            $('#divLoadShowObservacao').dialog({ autoopen: false, modal: true, width: 444, height: 140, resizable: false, title: "OBSERVAÇÃO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfoPlano() {
            $('#divLoadShowInfoPlano').dialog({ autoopen: false, modal: true, width: 480, height: 100, resizable: false, title: "INFORMAÇÕES DO PLANO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE AVALIAÇÃO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        //Inserida função para apresentação de mensagens de inconsistências
        function customOpen(msg) {
            alert(msg);
        }

        $(document).ready(function () {
            $(".nuGuia").mask("?999999999999999");
            $(".campoHora").unmask();
            $(".campoHora").mask("99:99");
        });
    </script>
</asp:Content>
