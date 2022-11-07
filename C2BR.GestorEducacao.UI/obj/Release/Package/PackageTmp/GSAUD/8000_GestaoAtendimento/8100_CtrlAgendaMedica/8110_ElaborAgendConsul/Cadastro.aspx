<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8110_ElaborAgendConsul.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .campoHora
        {
            width: 30px;
        }
        input
        {
            height: 13px;
        }
        .ulDados
        {
            width: 800px;
            margin-left: 285px !important;
        }
        
        .ulDados li
        {
            clear: none;
            margin-left: 5px;
            margin-top: 5px;
        }
        .ulDadosMod li
        {
            clear: none;
            margin-left: 5px;
            margin-top: 5px;
        }
        .chk label
        {
            margin-top: -50;
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
        .ddlLocalAgend
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField ID="hidCoCol" Value="0" runat="server" />
    <ul class="ulDados">
        <li>
            <ul>
                <li style="margin-top: 5px; clear: both; margin-left: -195px;">
                    <label for="ddlUnid" title="Selecione a unidade do médico">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnid" OnSelectedIndexChanged="ddlUnid_SelectedIndexChanged"
                        AutoPostBack="true" Width="230px" runat="server" ToolTip="Selecione a unidade do médico">
                        <asp:ListItem Value="0">Todas</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 5px;">
                    <label for="ddlDepto" title="Selecione o local onde será criada a agenda do profissional" class="lblObrigatorio">
                        Local de Agendamento</label>
                    <asp:DropDownList ID="ddlDepto" runat="server" CssClass="ddlLocalAgend" ToolTip="Selecione o Local do médico">
                    </asp:DropDownList>
                </li>
                <li style="margin: 3px 0 0px 2px;">
                    <label style="color: Blue;" title="Filtrar a grid por um profissional específico">
                        Nome do Profissional</label>
                    <asp:TextBox runat="server" ID="txtProSolicitado" Style="margin: 0; width: 221px;"></asp:TextBox>
                    <asp:ImageButton ID="imgPesProfSolicitado" ValidationGroup="pesqPac" runat="server"
                        ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgPesProfSolicitado_OnClick" />
                    <asp:DropDownList ID="drpProSolicitado" Width="221px" OnSelectedIndexChanged="drpProSolicitado_OnSelectedIndexChanged"
                        runat="server" Visible="false" AutoPostBack="true" />
                    <asp:ImageButton ID="imgVoltarPesProfSOlicitado" ValidationGroup="pesqPac" Width="16px"
                        Height="16px" Style="margin: 0 0 -4px 0px;" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                        OnClick="imgVoltarPesProfSOlicitado_OnClick" Visible="false" runat="server" />
                </li>
                <li style="float: right;">
                    <label title="Selecione qual a classificação funcional que deseja ser apresentada na grid abaixo"
                        style="color: Blue;" class="lblObrigatorio">
                        Classificação Funcional</label>
                    <asp:DropDownList runat="server" ID="ddlClassFunc" ToolTip="Filtre os profissionais de saúde pela Classificação Funcional"
                        Width="120px" OnSelectedIndexChanged="ddlClassFunc_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 17px; clear: both; margin-left: -196px;">
                    <div id="divGrdProfi" runat="server" class="divGridData" style="height: 290px; width: 810px;
                        overflow-y: scroll !important; border: 1px solid #ccc;">
                        <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 25px;" AutoGenerateColumns="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                        <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChanged" AutoPostBack="true"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Nome do profissional">
                                            NOME</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtNoCol" Text='<%# Eval("NO_COL") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="230px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Agenda do profissional">
                                            AG</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAgend" ImageUrl="/Library/IMG/PGS_CentralRegulacao_Icone_EmissaoGuia.png"
                                            ToolTip="Visualizar informações dos agendamentos já cadastrados para o profissional"
                                            runat="server" Style="width: 16px; height: 19px;" OnClick="imgInfosAgend_OnClick" />
                                        <asp:ImageButton ID="imgImprimirAgend" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                                            ToolTip="Gerar relatório de informações dos agendamentos já cadastrados para o profissional"
                                            runat="server" Style="width: 16px; height: 19px;" OnClick="imgImprimirAgend_OnClick" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Classificação funcional relacionada ao cadastro do profissional">
                                            CLASSIFICAÇÃO</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtClassifi" Text='<%# Eval("NO_CLASS_PROFI") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="95px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Atual situação em que o profissional se encontra">
                                            Situação</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidSitu" Value='<%# Eval("CO_SITU") %>' runat="server" />
                                        <asp:Label ID="txtSitu" Text='<%# Eval("SITU") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Especialidade relacionada ao cadastro do profissional">
                                            ESPECIALIDADE</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtEspec" Text='<%# Eval("DE_ESP") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Unidade em que o profissional foi cadastrado">
                                            UNIDADE</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnid" Text='<%# Eval("NO_EMP") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Local de trabalho existente no cadastro do profissional">
                                            LOCAL</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtSiglaDepto" Text='<%# Eval("SIGLA_DEPTO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label title="Telefone de contato cadastrado do profissional">
                                            TELEFONE</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtTel" Text='<%# Eval("TEL") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="margin-top: 0px; clear: both; margin-left: -200px;">
            <ul>
                <li>
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Período dentro do qual será feita a elaboração da agenda" class="lblObrigatorio">
                                Período</label>
                        </li>
                        <li style="clear: both">
                            <label title="">
                                Início</label>
                            <asp:TextBox ID="txtDtIni" runat="server" ToolTip="Informe a data de início da agenda"
                                CssClass="campoData">
                            </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="">
                                Fim</label>
                            <asp:TextBox ID="txtDtFim" runat="server" ToolTip="Informe a data de término da agenda"
                                CssClass="campoData">
                            </asp:TextBox></li>
                        <li>
                            <label title="Infome o intervalo entre os horários">
                                Tempo</label>
                            <asp:TextBox ID="txtHrInterv" runat="server" ToolTip="Informe o intervalo entre os horários"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Horário dentro do qual será feita a elaboração da agenda" class="lblObrigatorio">
                                Horário</label>
                        </li>
                        <li style="clear: both">
                            <label title="Informe o horário da agenda">
                                Início</label>
                            <%--  <label title="Data Início">
                        Início</label>--%>
                            <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="Data Fim">
                                Fim</label>
                            <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Intervalo de descanso dentro do qual não será feita a elaboração da agenda">
                                Intervalo</label>
                        </li>
                        <li style="clear: both">
                            <label title="Informe o Intervalo">
                                Início</label>
                            <%--  <label title="Data Início">
                        Início</label>--%>
                            <asp:TextBox ID="txtIntervaloInicio" runat="server" ToolTip="Informe a hora de início da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                        <li style="margin-top: 18px;">até</li>
                        <li>
                            <label title="Data Fim">
                                Fim</label>
                            <asp:TextBox ID="txtIntervaloFim" runat="server" ToolTip="Informe a hora de término da agenda"
                                CssClass="campoHora">
                            </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 14px;">
                    <ul>
                        <li class="lblsub" style="margin-bottom: -5px">
                            <label title="Dias da semana dentro dos quais será feita a elaboração da agenda">
                                Dias Semana</label>
                        </li>
                        <li style="clear: both">
                            <label title="">
                                Dom</label>
                            <asp:CheckBox runat="server" ID="chkDom" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  os Domingos" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Seg</label>
                            <asp:CheckBox runat="server" ID="chkSeg" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para as Segundas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Ter</label>
                            <asp:CheckBox runat="server" ID="chkTer" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Terças"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Qua</label>
                            <asp:CheckBox runat="server" ID="chkQua" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Quartas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Qui</label>
                            <asp:CheckBox runat="server" ID="chkQui" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Quintas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Sex</label>
                            <asp:CheckBox runat="server" ID="chkSex" Text="" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  as Sextas"
                                Checked="true" />
                        </li>
                        <li style="margin-left: -2px;">
                            <label title="">
                                Sab</label>
                            <asp:CheckBox runat="server" ID="chkSab" CssClass="chk" ToolTip="Marque caso queira  realizar agenda para  os Sábados" />
                        </li>
                        <li style="margin: 17px 0 0 -2px;">
                            <asp:CheckBox runat="server" ID="chkConsiderarFeriados" CssClass="chk" ToolTip="Marque caso queira  Considerar os feriados"
                                Text="Considerar Feriados" />
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="margin: 0 0 0 -196px; clear: both">
            <asp:CheckBox runat="server" ID="chkAgendaMulti" CssClass="chk" ToolTip="Gerar agenda para registros existentes"
                Text="Desconsiderar Horários Existentes" />
        </li>
        <li>
            <div id="divProfAgend" style="display: none; height: 438px !important;">
                <ul class="ulDadosMod" style="width: auto; margin-top: 0px !important; float: left;">
                    <li class="" style="margin-bottom: -2px">
                        <asp:Label Style="font-size: 12px;" ID="noProfiMod" Text="" runat="server"></asp:Label>
                    </li>
                    <li class="lblsub" style="margin-bottom: -4px">
                        <label title="Período dentro do qual existem agendamentos cadastrados">
                            Período</label>
                    </li>
                    <li style="clear: both">
                        <asp:TextBox ID="dtIniMod" runat="server" ToolTip="Informe a data de início da agenda"
                            CssClass="campoData">
                        </asp:TextBox>
                    </li>
                    <li style="margin-top: -23px; margin-left: 90px;">até</li>
                    <li style="margin-top: -15px; margin-left: 117px;">
                        <asp:TextBox ID="dtFimMod" runat="server" ToolTip="Informe a data de término da agenda"
                            CssClass="campoData">
                        </asp:TextBox>
                    </li>
                    <li style="margin-top: -25px; margin-left: 200px;">
                        <asp:ImageButton ID="imgPesqAgendMod" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgPesqAgendMod_OnClick" />
                    </li>
                    <li style="margin-top: 17px; clear: both; margin-left: 4px;">
                        <div id="div1" runat="server" class="divGridData" style="height: 360px; width: 600px;
                            overflow-y: scroll !important; border: 1px solid #ccc;">
                            <asp:HiddenField ID="hidCoColMod" Value="0" runat="server" />
                            <asp:GridView ID="grdAgendMod" CssClass="grdBusca" runat="server" Style="width: 100%;
                                height: 25px;" AutoGenerateColumns="false">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Data do agendamento">
                                                DATA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtDtAgendMod" Text='<%# Eval("DE_DT_AGEND") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Hora em que ocorreu o agendamento">
                                                HORA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtHrAgendMod" Text='<%# Eval("HR_AGEND") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    
                                     <HeaderStyle Width="40%" />
                                        <HeaderTemplate>
                                        
                                            <label title="Paciente referente do horário agendado">
                                                PACIENTE</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       
                                            <asp:Label ID="txtPaciAgendMod" Text='<%# Eval("NOME") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Número da pasta de controle do paciente se houver">
                                                PASTA</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPastaAgenMod" Text='<%# Eval("DESC_PASTA") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                    <HeaderStyle Width="45%" />
                                        <HeaderTemplate>
                                            <label title="Local do agendamento">
                                                LOCAL</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtLocalAgendMod" Text='<%# Eval("LOCAL") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Tipo do agendamento">
                                                TIPO</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtTipoAgendMod" Text='<%# Eval("TIPO") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label title="Situação do agendamento">
                                                SITUAÇÃO</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtSitAgendMod" Text='<%# Eval("DE_SITU") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divPeriodoAgend" style="display: none; height: 98px !important;">
                <ul>
                    <li class="lblsub" style="margin-bottom: -4px">
                        <asp:Label runat="server" ID="lblProfImp" Style="color: #000000"></asp:Label>
                        <asp:HiddenField runat="server" ID="hidCoColImp" />
                        <ul>
                            <li style="width: 180px;">
                                <label title="Período dentro do qual existem agendamentos cadastrados" style="margin: 7px 0 5px 0;
                                    color: #000000">
                                    Período</label>
                                <asp:TextBox ID="txtDtIniImp" runat="server" ToolTip="Informe a data de início da agenda"
                                    CssClass="campoData">
                                </asp:TextBox><span style="margin: 0 0 0 4px;color: #000000">até</span> 
                                <asp:TextBox ID="txtDtFimImp" runat="server" ToolTip="Informe a data de término da agenda"
                                    CssClass="campoData">
                                </asp:TextBox>
                            </li>
                            <li style="width: 20px;margin:-26px 0 0 193px;">
                                <asp:ImageButton ID="ImprimirAgend" ImageUrl="/BarrasFerramentas/Icones/Imprimir.png"
                                    ToolTip="Gerar relatório de informações dos agendamentos já cadastrados para o profissional"
                                    runat="server" Style="width: 16px; height: 19px;" OnClick="ImprimirAgend_OnClick" />
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(function () {
            $(".campoHora").mask("99:99");
        });

        function AbreModalInfosAgend() {
            $('#divProfAgend').dialog({ autoopen: false, modal: true, left: 221, top: 22, width: 635, height: 470, resizable: false, title: "AGENDAMENTOS CADASTRADOS DO PROFISSIONAL",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalImprimirAgend() {
            $('#divPeriodoAgend').dialog({ autoopen: false, modal: true, left: 221, top: 22, width: 275, height: 100, resizable: false, title: "INFORME O PERÍODO DOS AGENDAMENTOS",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
    </script>
</asp:Content>
