<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9301_CadastroExamesEsternos.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 990px;
        }
        input
        {
            height: 13px !important;
        }
        .successMessageSMS
        {
            background: url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;
        }
        .liBtnFinAten
        {
            border: 1px solid #D2DFD1;
            float: right !Important;
            margin-top:5px;
            padding: 2px 8px 1px 8px;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px;
        }
        .sitPacPadrao
        {
            color:Blue;
            margin-left:1px;
        }
        .sitPacAnalise
        {
            font-weight:bold;
            color:Black;
            margin-left:1px;
        }
        .sitPacAlta
        {
            font-weight:bold;
            color:Red;
            margin-left:1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="margin-top: -5px; margin-left:40px;">
            <div id="divBotoes">
                <ul style="width: 1000px;">
                    <li style="margin: 10px 0 4px 0;"><a class="lnkPesPaci" href="#">
                        <img class="imgPesPac" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                            style="width: 17px; height: 17px;" /></a> </li>
                    <li style="margin-left: 5px;">
                        <label>
                            Nº PRONTUÁRIO</label>
                        <asp:CheckBox runat="server" ID="chkPesqNire" Style="margin-left: -5px" Checked="true"
                            OnCheckedChanged="chkPesqNire_OnCheckedChanged" AutoPostBack="true" />
                        <asp:TextBox runat="server" ID="txtNirePaci" CssClass="txtNireAluno" Width="50px"
                            Style="margin-left: -7px" Enabled="true"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            CPF</label>
                        <asp:CheckBox runat="server" ID="chkPesqCpf" Style="margin-left: -5px" OnCheckedChanged="chkPesqCpf_OnCheckedChanged"
                            AutoPostBack="true" />
                        <asp:TextBox runat="server" ID="txtCPFPaci" Width="75px" CssClass="campoCpf" Style="margin-left: -7px"
                            Enabled="false"></asp:TextBox>
                    </li>
                    <li style="margin-top: 12px; margin-left: -4px;">
                        <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgCpfPac_OnClick" />
                    </li>
                    <li style="margin-left: 10px;">
                        <label>Unidade</label>
                        <asp:DropDownList ID="ddlLocalPaciente" Width="170px" runat="server"
                            OnSelectedIndexChanged="ddlLocalPaciente_OnSelectedIndexChanged" AutoPostBack="true" />
                    </li>
                    <li style="margin-left: 10px;">
                        <label>Profissional</label>
                        <asp:DropDownList ID="drpProfissional" Width="160px" runat="server"
                            OnSelectedIndexChanged="drpProfissional_OnSelectedIndexChanged" AutoPostBack="true" />
                    </li>
                    <li style="margin-left: 8px">
                        <label title="Nome do usuário selecionado" class="lblObrigatorio">
                            Paciente</label>
                        <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Paciente para o qual a consulta será marcada"
                            Width="230px" Visible="false" OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
                    </li>
                    <li style="margin-top: 11px; margin-left: -4px;">
                        <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                            OnClick="imgbPesqPacNome_OnClick" />
                        <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                            OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
                    </li>
                    <li style="margin-left:-205px;">
                        <asp:Label ID="lblSitPaciente" Text=" - " runat="server" />
                    </li>
                    <li style="margin-top: 13px;">
                        <asp:CheckBox runat="server" ID="chkEnviaSms" Text="Enviar SMS" CssClass="chk" Enabled="false"
                            ToolTip="Selecione para enviar um SMS com informações do agendamento ao Paciente automaticamente" />
                    </li>
                </ul>
            </div>
        </li>
        <li style="width: 981px; float: left; margin-left: 6px; margin-top: 6px;">
            <ul>
                <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                    background-color: #E0EEEE; text-align: center; font-weight: bold; margin-bottom: -0px">
                    <div style="float: left; margin-left: 10px !important;">
                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                            HISTÓRICO DE ATENDIMENTOS</label>
                    </div>
                    <div style="float: right; margin-top: 5px">
                        <ul>
                            <li>
                                <asp:Label Text="Unidade Cadastro" runat="server" />
                                <asp:DropDownList ID="ddlUnidHist" Width="170px" runat="server" />
                            </li>
                            <li style="margin-left: 14px;">
                                <asp:TextBox ID="txtDtIniHist" runat="server" CssClass="campoData">
                                </asp:TextBox>
                                à
                                <asp:TextBox ID="txtDtFimHist" runat="server" CssClass="campoData">
                                </asp:TextBox>
                            </li>
                            <li style="margin-top: 0px; margin-left: 0px;">
                                <asp:ImageButton ID="imgPesqHist" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                    OnClick="imgPesqHist_OnClick" />
                            </li>
                        </ul>
                    </div>
                </li>
                <li style="margin-top: 5px">
                    <div id="divHistorPac" class="divGridData" style="height: 168px; width: 979px;
                        overflow-y: scroll !important; border: 1px solid #ccc;">
                        <asp:GridView ID="grdHistorPaciente" CssClass="grdBusca" runat="server" Style="width: 100%; overflow-y: scroll !important;"
                            AutoGenerateColumns="false" ToolTip="Grade de histórico de atendimentos do(a) Paciente selecionado"
                            ShowHeaderWhenEmpty="true">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="DT_HORAR" HeaderText="DATA/HORA">
                                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UNID" HeaderText="UNID">
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GRUPO" HeaderText="GRUPO">
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SUBGRUPO" HeaderText="SUBGRUPO">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTRAT" HeaderText="CONTRATAÇÃO">
                                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CORTESIA" HeaderText="CORT">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QTD" HeaderText="QTD">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_PROC" HeaderText="R$ UNIT">
                                    <ItemStyle Width="30px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_TOTAL" HeaderText="R$ TOTAL">
                                    <ItemStyle Width="30px" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SOLICITANTE" HeaderText="SOLICITANTE">
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="STATUS" HeaderText="STATUS">
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ORIGEM">
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblOrigem" ToolTip='<%# Eval("ORIGEM_TOOLTIP") %>' Text='<%# Eval("ORIGEM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CAN">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdExame" Value='<%# Eval("ID_EXAME") %>' />
                                        <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                            ToolTip="Cancelar item de planejamento" OnClientClick="return confirm('Tem certeza de que deseja CANCELAR o atendimento ?')"
                                            OnClick="imgExc_OnClick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="width: 980px; margin: 10px 0 0 7px !important; clear: both;">
            <ul>
                <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                    background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 0px">
                    <div style="float: left; margin: 0 0 0 10px;">
                        <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                            PROCEDIMENTOS</label>
                    </div>
                    <ul style="margin-top: 6px;">
                        <li style="margin-left: 90px;">
                            <asp:Label Text="Contratação" runat="server" />
                            <asp:DropDownList runat="server" ID="ddlContratacao" Width="130" />
                        </li>
                        <li style="margin-left: 3px;">
                            <asp:Label Text="Grupo" runat="server" />
                            <asp:DropDownList runat="server" ID="ddlGrupo" Width="130" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
                        </li>
                        <li style="margin-left: 3px;">
                            <asp:Label Text="Subgrupo" runat="server" />
                            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="130" />
                        </li>
                        <li style="margin-top: 0px;">
                            <asp:ImageButton ID="imgbPesqProced" runat="server" OnClick="imgbPesqProced_OnClick" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                        </li>
                    </ul>
                </li>
                <li style="margin-top: 3px">
                    <div id="divItens" class="divGridData" style="height: 165px; width: 978px; border: 1px solid #ccc; overflow-y: scroll !important;">
                        <asp:GridView ID="grdProcedimentos" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                            Style="width: 100%;" >
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkTodosItens" runat="server"
                                            AutoPostBack="true" OnCheckedChanged="chkTodosItens_OnCheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkProced" runat="server"
                                            AutoPostBack="true" OnCheckedChanged="chkProced_OnCheckedChanged" />
                                        <asp:HiddenField runat="server" ID="hidIdProc" Value='<%# Eval("ID_PROC_MEDI_PROCE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NM_PROC_MEDI" HeaderText="PROCEDIMENTO">
                                    <ItemStyle Width="260px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CORTES">
                                    <ItemStyle Width="12px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlCortesia" Width="100%" Style="margin: 0 0 0 -4px !important;" Enabled="false">
                                            <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="Não" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTD">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtQtd" Width="100%" Style="margin: 0 0 0 -4px !important; text-align:right;" 
                                            Text="1" CssClass="nuQtde" Enabled="false" OnTextChanged="txtQtd_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R$ UNIT">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVlUnit" Width="100%" Text='<%# Eval("VL_PROCED") %>'
                                            Style="margin: 0 0 0 -4px !important; text-align:right;" CssClass="campoDin" Enabled="false" OnTextChanged="txtVlUnit_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R$ TOTAL">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVlTotal" Width="100%" Text='<%# Eval("VL_PROCED") %>'
                                            Style="margin: 0 0 0 -4px !important; text-align:right;" CssClass="campoDin" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REQUIS">
                                    <ItemStyle Width="12px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlRequis" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                            Enabled="false" OnSelectedIndexChanged="ddlRequis_OnSelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="Não" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SOLICITANTE">
                                    <ItemStyle Width="160px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtSolicitante" Width="100%" Style="margin: 0 0 0 -4px !important;"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REGISTRO (TP/Nº)">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlSiglaEntid" Width="50px" CssClass="ddlSiglaEntd"
                                            ToolTip="Sigla da Entidade Profissional do(a) Colaborador(a)" Enabled="false" Style="margin: 0 0 0 -2px !important;">
                                            <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="CRM" Text="CRM"></asp:ListItem>
                                            <asp:ListItem Value="CRO" Text="CRO"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox runat="server" ID="txtNumEntid" Width="45px" Style="margin: 0 0 0 2px !important;"
                                            Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divOcorrencia").show();
            $(".nuQtde").mask("?99");
            $(".txtNireAluno").mask("?999999999");
            $(".campoCpf").mask("999.999.999-99");
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".lnkPesPaci").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });
        });
    </script>
</asp:Content>
