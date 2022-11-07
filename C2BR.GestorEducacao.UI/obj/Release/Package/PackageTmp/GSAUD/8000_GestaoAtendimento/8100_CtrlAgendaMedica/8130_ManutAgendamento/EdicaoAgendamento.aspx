<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="EdicaoAgendamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8130_ManutAgendamento.EdicaoAgendamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .campoHora
        {
             font-weight:bold;          
            display:inline;
            width: 30px;
        }
        .ulDados
        {
            width: 990px;
        }
        input
        {
            height: 13px !important;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .liTituloGrid
        {
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
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
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: -5px; margin-left: 160px; float: right;">
                    <div id="divBotoes">
                        <ul style="width: 1000px;">
                            <li style="margin: 10px 0 4px 0;"><a class="lnkPesPaci" href="#">
                                <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
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
                                <label>Profissional</label>
                                <asp:DropDownList ID="drpProfissional" Width="160px" runat="server"
                                    OnSelectedIndexChanged="drpProfissional_OnSelectedIndexChanged" AutoPostBack="true" />
                            </li>
                            <li style="margin-left: 8px">
                                <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado" class="lblObrigatorio">
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
                        </ul>
                    </div>
                </li>
            </ul>
        </li>
        <li class="liSeparador"></li>
        <li>
            <div id="tabResConsultas" runat="server" clientidmode="Static">
                <ul id="ul10">
                    <li style="margin-top: 10px !important; width: 1000px;">
                        <ul>
                            <li style="width: 980px; margin: 0 0 0 7px !important; clear: both;">
                                <ul>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                        background-color: #bde5ae; text-align: center; font-weight: bold; margin-bottom: 0px">
                                        <div style="float: left; margin-left: 10px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                AGENDA DE HORÁRIOS</label>
                                        </div>
                                        <div style="float: right; margin-right: 10px; margin-top: 3px;">
                                            <label id="lblMsgErro" style="font-size: 13px; color: #B22222; font-weight: bold;
                                                display: none;">
                                                Usuário sem permissão para agendamento de mais um paciente no mesmo horário.</label>
                                        </div>
                                    </li>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 0px;
                                        background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 8px">
                                        <div style="float: left; margin-left: 5px; margin-top: 4px;">
                                            <ul>
                                                <li>
                                                    <asp:Label ID="Label1" ToolTip="Classificação Agendamento" runat="server">Class</asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlClassFunci" OnSelectedIndexChanged="ddlClassFunci_OnSelectedIndexChanged"
                                                        AutoPostBack="true" Width="85px" ToolTip="Classificação Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label2" ToolTip="Tipo de Agendamento" runat="server">Tipo</asp:Label>
                                                    <asp:DropDownList runat="server" Width="59px" ID="ddlTipoAg" OnSelectedIndexChanged="ddlTipoAg_OnSelectedIndexChanged"
                                                        AutoPostBack="true" ToolTip="Tipo de Agendamento">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label3" ToolTip="Tipo de Contratação" runat="server">Cont</asp:Label>
                                                    <asp:DropDownList runat="server" Width="65px" ID="ddlOpers" OnSelectedIndexChanged="ddlOpers_OnSelectedIndexChanged" ToolTip="Tipo de Contratação"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label4" ToolTip="Plano" runat="server">Plano</asp:Label>
                                                    <asp:DropDownList runat="server" Width="65px" ID="drpPlano" OnSelectedIndexChanged="drpPlano_OnSelectedIndexChanged" ToolTip="Tipo de Contratação"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </li>
                                            </ul>
                                        </div>
                                        <div style="float: right; margin-top: 4px;">
                                            <ul>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkDom" Text="Do" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Domingos" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSeg" Text="Se" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Segundas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkTer" Text="Te" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Terças"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkQua" Text="Qa" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quartas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkQui" Text="Qi" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Quintas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSex" Text="Sx" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo as Sextas"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:CheckBox runat="server" ID="chkSab" Text="Sb" CssClass="chk" ToolTip="Marque caso deseje ver as agendas incluindo os Sábados"
                                                        Checked="true" />
                                                </li>
                                                <li>
                                                    <asp:TextBox ID="txtDtIniResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtDtFimResCons" runat="server" CssClass="campoData">
                                                    </asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início" CssClass="campoHora">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término" CssClass="campoHora">
                                                    </asp:TextBox>
                                                </li>
                                                <li style="margin-top: 1px; margin-left: -1px;">
                                                    <asp:ImageButton ID="imgPesqGridAgenda" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                        OnClick="imgPesqGridAgenda_OnClick" />
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 1px">
                                        <div id="divAgenda" class="divGridData" style="height: 320px; width: 980px; border: 1px solid #ccc;">
                                            <input type="hidden" id="divAgenda_posicao" name="divAgenda_posicao" />
                                            <asp:HiddenField runat="server" ID="hidCoConsul" />
                                            <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" AutoGenerateColumns="false"
                                                Style="width: 100%;" OnRowDataBound="grdHorario_OnRowDataBound">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="5px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkMarcaTodosItens" CssClass="chk" Style="" runat="server" OnCheckedChanged="ChkTodos_OnCheckedChanged"
                                                                AutoPostBack="true"></asp:CheckBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckSelectHr" CssClass="chk" runat="server" OnCheckedChanged="ckSelectHr_OnCheckedChanged"
                                                                AutoPostBack="true" />
                                                            <asp:HiddenField runat="server" ID="hidCoAgenda" Value='<%# Eval("CO_AGEND") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                                            <asp:HiddenField runat="server" ID="hidTpCons" Value='<%# Eval("TP_CONSUL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoCol" Value='<%# Eval("CO_COL") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoDepto" Value='<%# Eval("CO_DEPTO") %>' />
                                                            <asp:HiddenField runat="server" ID="hidCoEmp" Value='<%# Eval("CO_EMP") %>' />
                                                            <asp:HiddenField runat="server" ID="hidDataHora" Value='<%# Eval("hora") %>' />
                                                            <asp:HiddenField runat="server" ID="hidData" Value='<%# Eval("dt") %>' />
                                                            <asp:HiddenField runat="server" ID="hidHora" Value='<%# Eval("hr") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="CLASS FUNCIONAL">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidTpAgend" Value='<%# Eval("CO_TP_AGEND") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlTipoAgendam" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TIPO">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidTpConsul" Value='<%# Eval("CO_TP_CONSUL") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlTipo" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CONTRATAÇÃO">
                                                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdOper" Value='<%# Eval("CO_OPER") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlOperAgend" OnSelectedIndexChanged="ddlOperAgend_OnSelectedIndexChanged"
                                                                AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PLANO">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdPlan" Value='<%# Eval("CO_PLAN") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlPlanoAgend" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PROCEDIMENTO">
                                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField runat="server" ID="hidIdProced" Value='<%# Eval("ID_PROC") %>' />
                                                            <asp:DropDownList runat="server" ID="ddlProcedAgend" OnSelectedIndexChanged="ddlProcedAgend_OnSelectedIndexChanged"
                                                                AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px;">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VALOR">
                                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtValorAgend" Text='<%# Eval("VL_CONSUL") %>' CssClass="campoMoeda" Width="100%" Style="margin-left: -4px; margin-bottom:0px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CO_SITU_VALID" HeaderText="status">
                                                        <ItemStyle Width="40px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoHora").mask("99:99");
        });
    </script>
</asp:Content>
