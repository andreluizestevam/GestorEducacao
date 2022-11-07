<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="ExclusaoItemAgendamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8130_ManutAgendamento.ExclusaoItemAgendamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .ulDados
        {
            width: 900px;
        }
        .ulDados li
        {
            margin-left: 5px;
        }
        .liClear
        {
            clear: both;
        }
        .campoDin
        {
            width: 60px;
            text-align: right;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        /*--> CSS DADOS */
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 10px;
            margin-top: 11px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 0px 1px 9px !important;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            width: 890px;
            overflow-y: scroll;
            margin-top: 10px;
            height: 290px;
            margin-left: 0px;
            border: 1px solid #CCC;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .liResumo
        {
            clear: both;
            margin-top: 10px;
            margin-left: 200px;
        }
        .check label
        {
            display: inline;
        }
        .check input
        {
            margin-left: -5px;
        }
        .campoHora
        {
            width: 30px;
        }
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        .ulDados li
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div style="height: 600px;">
        <ul id="ulDados" class="ulDados">
            <li style="margin-top: 0px; clear: both;">
                <ul>
                    <li> 
                        <ul>
                            <li class="lblsub" style="margin-bottom: -5px">
                                <label title="Período dentro do qual será feita a elaboração da agenda">
                                    Período</label>
                            </li>
                            <li style="clear: both">
                                <label title="">
                                    Início</label>
                                <asp:TextBox ID="txtDataPeriodoIni" runat="server" ToolTip="Informe a data de início da agenda"
                                    CssClass="campoData"> </asp:TextBox>
                            </li>
                            <li style="margin-top: 18px;">até</li>
                            <li>
                                <label title="">
                                    Fim</label>
                                <asp:TextBox ID="txtDataPeriodoFim" runat="server" ToolTip="Informe a data de término da agenda"
                                    CssClass="campoData"> </asp:TextBox></li>
                        </ul>
                    </li>
                    <li style="margin-left: 14px;">
                        <ul>
                            <li class="lblsub" style="margin-bottom: -5px">
                                <label title="Horário dentro do qual será feita a elaboração da agenda">
                                    Horário</label>
                            </li>
                            <li style="clear: both">
                                <label title="Informe o horário da agenda">
                                    Início</label>
                                <asp:TextBox ID="txtHrIni" runat="server" ToolTip="Informe a hora de início da agenda"
                                    CssClass="campoHora"> </asp:TextBox>
                            </li>
                            <li style="margin-top: 18px;">até</li>
                            <li>
                                <label title="Data Fim">
                                    Fim</label>
                                <asp:TextBox ID="txtHrFim" runat="server" ToolTip="Informe a hora de término da agenda"
                                    CssClass="campoHora"> </asp:TextBox>
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
                        </ul>
                    </li>
                    <li>
                        <ul>
                            <li class="lblsub" style="margin-bottom: -5px">
                                <label>Ação</label>
                            </li>
                            <li style="clear: both">
                                <label class="lblObrigatorio">Tipo da Ação</label>
                                <asp:DropDownList ID="drpTipoAcao" runat="server">
                                    <asp:ListItem Value="" Text="Selecione" />
                                    <asp:ListItem Value="E" Text="Excluir" />
                                    <asp:ListItem Value="C" Text="Cancelar" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvTipoAcao" CssClass="validatorField"
                                    ErrorMessage="O campo tipo ação é obrigatório" ControlToValidate="drpTipoAcao" Display="Dynamic" />
                            </li>
                        </ul>
                    </li>
                    <li>
                        <ul>
                            <li class="lblsub"> 
                            </li>
                            <li style="clear: both">
                                <label>Observação da Ação</label>
                                <asp:TextBox ID="txtObservacoes" Rows="1" Width="215px" TextMode="MultiLine" runat="server" />
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
            <li style="margin-left: 5px; clear:both">
                <label for="txtNomeEmp" title="Unidade">
                    Unidade</label>
                <asp:DropDownList runat="server" ID="ddlUnidade" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                    AutoPostBack="true" Width="240px">
                </asp:DropDownList>
            </li>
            <li style="margin-left: 5px;">
                <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">
                    Paciente</label>
                <asp:DropDownList ID="ddlAluno" runat="server" ToolTip="Paciente para o qual a consulta será marcada"
                    Width="192px" style="height:13px;" Visible="false">
                </asp:DropDownList>
                <asp:TextBox ID="txtNomePacPesq" style="height:13px;" width="190px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            </li>
            <li style="margin-top: 16px; margin-left:0px;">
                <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
             OnClick="imgbPesqPacNome_OnClick" />
                <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
            </li>
            <li style="">
                <label>
                    Class Funcional</label>
                <asp:DropDownList runat="server" ID="ddlClassFuncional" Width="126px" OnSelectedIndexChanged="ddlClassFuncional_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li>
                <label>
                    Profissional</label>
                <asp:DropDownList runat="server" ID="ddlProfissional" Width="200px">
                </asp:DropDownList>
            </li>
            <li id="Li1" runat="server" title="Processar arqui retorno" class="liBtnAdd" style=" margin-left:344px; margin-top:-17px">
                <asp:LinkButton ID="btnGerar" runat="server" ValidationGroup="pesquisa" OnClick="btnPesquisar_Click"
                    Width="64">PESQUISAR</asp:LinkButton>
            </li>
            <li class="liResumo" id="liResumo" runat="server" visible="false" style="margin-left: 246px;">
                <span style="font-size: 1.4em; font-weight: bold;">*** ITENS DE AGENDA EM ABERTO E MOVIMENTADOS ***</span>
            </li>
            <li class="liClear">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResumo" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum Agendamento em Aberto<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                <ItemStyle Width="10px" CssClass="grdLinhaCenter" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="chkMarcaTodosItens" CssClass="verificaItem" OnCheckedChanged="chkMarcaTodosItens_OnCheckedChanged"
                                        AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# bind("ID_AGEND_HORAR") %>' />
                                    <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# bind("CO_ALU") %>' />
                                    <asp:CheckBox ID="chkSelect" CssClass="verificaItem" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="hora" HeaderText="DATA E HORA">
                                <ItemStyle Width="125px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PACIENTE" HeaderText="PACIENTE">
                                <ItemStyle Width="285px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_CLASS_FUNC" HeaderText="Class Funcional">
                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CO_MATRICULA" HeaderText="Matrícula">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO_COL" HeaderText="Profissional">
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField DataField="CO_SITUA_AGEND_HORAR" HeaderText="Situação">
                                <ItemStyle Width="70px" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
        </ul>
    </div>
    <div id="divSituacaoPaciente" style="display: none; height: 150px !important; width: 420px">
        <asp:HiddenField ID="hidCoAluAlterar" runat="server" />
        <ul>
            <li style="margin-bottom: 10px;">
                O paciente <asp:Label ID="lblPaciente" Font-Bold="true" runat="server" /><br />
                se encontra na situação de ATENDIMENTO!<br />
                Deseja encaminhar o paciente para outra situação?
            </li>
            <li style="clear:both;">
                <label title="Situação do Paciente">
                    Situação</label>
                <asp:DropDownList ID="drpSituacao" ToolTip="Informe a Situação do Paciente" runat="server" Width="98px">
                    <asp:ListItem Value="V">Em Análise</asp:ListItem>
                    <asp:ListItem Value="E" Selected="True">Alta (Normal)</asp:ListItem>
                    <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                    <asp:ListItem Value="I">Inativo</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li class="liBtnConfirm" style="margin-left:155px; width: 30px">
                <asp:LinkButton ID="lnkbAlterSim" OnClick="lnkbAlterSim_OnClick"
                    runat="server" ToolTip="Alterar a situação atual do paciente e continuar o procedimento">
                    <label style="margin-left:5px; color:White;">SIM</label>
                </asp:LinkButton>
            </li>
            <li class="liBtnConfirm" style="margin:-21px 0 0 205px; width: 30px;">
                <asp:LinkButton ID="lnkbAlterNao" OnClick="lnkbAlterNao_OnClick"
                    runat="server" ToolTip="Continuar o procedimento sem alterar a situação atual do paciente">
                    <label style="margin-left:5px; color:White;">NÃO</label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $(".campoHora").mask("99:99");
            });
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });


            $('.verificaItem').delegate('input:checkbox', 'change', function (e) {
                var ddlTipo = $('#ctl00_content_drpTipoAcao').val();

                if (!ddlTipo) {
                    alert("Você deve selecionar um Tipo de Ação antes da escolha de qualquer item da agenda.");
                    $('.verificaItem input:checkbox').attr('checked', false);

                } else if (ddlTipo == "E") {

                    if (!confirm("ATENÇÃO!!! Você tem certeza que deseja excluir o(s) item(ns) selecionado(s)? ... Ao confirmar o(s) registro(s) selelcionado(s) será(ão) excluido(s) definitivamente dos agendamentos, ficando as datas/horários livres (em aberto) não havendo como recuperar.")) {
                        $(this).attr('checked', false);
                    }
                } else if (ddlTipo == "C") {
                    if (!confirm("ATENÇÃO!!! Você tem certeza que deseja cancelar o(s) item(ns) selecionado(s)? ... Ao confirmar o(s) registro(s) selelcionado(s) o(s) cancelamento(s) só poderá(ão) ser revertidos em tela específica.")) {
                        $(this).attr('checked', false);
                    }
                }
            });
        });

        function AbreModalSituacaoPaciente() {
            $('#divSituacaoPaciente').dialog({ autoopen: false, modal: true, width: 420, height: 150, resizable: false, title: "ALTERAÇÃO DE SITUAÇÃO DO PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
    </script>
</asp:Content>
