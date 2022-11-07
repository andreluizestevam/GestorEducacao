<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_RegistroConsulMedMod2._8123_ReplicacaoAgendaProfSimp.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
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
        .lblTitInf
        {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 1.0em;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
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
        .lachk label
        {
            display: inline;
            margin-left: -3px;
        }
        .pAcesso
        {
            font-size: 1.1em;
            color: #4169E1;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
            width:10px;
        }
        .pFechar
        {
            font-size: 0.9em;
            color: #FF6347;
            margin-top: 2px;
        }
        #divListarResponsaveisContent
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
        }
        .liFoto
        {
            float: left !important;
            margin-right: 0 !important;
        }
        .fldFotoAluno
        {
            border: none;
            width: 80px;
            height: 100px;
        }
        .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
        .chkLocais label
        {
            margin-left: -3px;
            display: inline !important;
        }
        .DivResp
        {
            float: left;
            width: 500px;
            height: 207px;
        }
        .chk label
        {
            display: inline;
        }
        .ulDadosResp li
        {
            margin-top: -2px;
            margin-left: 5px;
        }
        .ulDadosPaciente li
        {
            margin-left: 0px;
        }
        .ulDadosResp
        {
            float: left;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 108px;
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
        
        .divValidationSummary
        {
            cursor: move;
            padding: 10px;
            display: none;
            width: 210px;
            position: absolute;
            top: 35px;
            left: 0;
        }
        .divValidationSummary .divButtons
        {
            text-align: right;
        }
        .divValidationSummary .vsValidation
        {
            margin-left: 10px;
        }
        .divValidationSummary #divValidationSummaryContent
        {
            margin-bottom: 10px;
        }
        .divValidationSummary li
        {
            color: #666666;
            line-height: 11px;
            border-bottom: 1px solid #CFCFCF;
            list-style-type: circle;
            padding-bottom: 2px;
        }
          .divEsquePgto
        {
            border-right: 1px solid #CCCCCC;
            margin-top:15px;
            margin-right:20px !Important;
            width: 165px;
            height: 180px;
            float: left;
        }
        .divDirePgto
        {
            <%--border: 1px solid #CCCCCC;--%>;
            margin-top:15px;
            width: 550px;
            height: 180px;
            float: right;
        }
        .divGrdChequePgto
        {
            border: 1px solid #CCCCCC;
            width: 720px;
            height: 106px;
            overflow-y: scroll;
        }
        .lblchkPgto
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-5px;
        }
        .ulReceb li 
        {
            margin-top:-6px;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px;
        }
        .chkDestaque label
        {
             font-weight:bold;
            color: #FFA07A;
            margin-left:-4px;
            display:inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidMultiAgend" />
    <asp:HiddenField runat="server" ID="hidEspelhoAgenda" />
    <ul id="ulDados" class="ulDados">
        <li style="margin-right: 0px;">
            <ul id="ulDadosMatricula">
                <li style="margin-top: -5px; margin-left: 370px; float: right;">
                    <div id="divBotoes">
                        <ul style="width: 1000px;">
                            <li style="margin-left: 8px">
                                <label title="Nome do profissional selecionado" class="lblObrigatorio">
                                    Profissional</label>
                                <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Profissional para o qual a consulta será marcada"
                                    Width="230px" OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="pesquisa" runat="server" ID="rfvNomeUsu" CssClass="validatorField"
                                    ErrorMessage="O paciente é obrigatório" ControlToValidate="ddlNomeUsu" />
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
                    <li style="margin-top: 10px !important; margin-left:100px; width: 800px;">
                        <ul>
                            <li style="width: 780px; float: left; margin-left: 6px;">
                                <ul>
                                    <li class="liTituloGrid" style="width: 100%; height: 24px !important; margin-right: 40px !important;
                                        background-color: #E0EEEE; text-align: center; font-weight: bold; margin-bottom: 5px">
                                        <div style="float: left; margin-left: 10px !important;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                                HISTÓRICO DE AGENDAMENTOS DO PROFISSIONAL</label>
                                        </div>
                                        <div style="float: right; margin-top: 5px">
                                            <ul>
                                                <li>
                                                    <asp:TextBox ID="txtDtIniHistoUsuar" runat="server" Width="55" Enabled="false">
                                                    </asp:TextBox>
                                                    &nbsp até &nbsp
                                                    <asp:TextBox ID="txtDtFimHistoUsuar" runat="server" Width="55" Enabled="false">
                                                    </asp:TextBox>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                    <li style="margin-top: 5px">
                                        <div id="divHistoricoAgenda" class="divGridData" style="height: 350px; width: 780px;
                                            overflow-y: scroll !important; border: 1px solid #ccc;">
                                            <input type="hidden" id="divHistoricoAgenda_posicao" name="divHistoricoAgenda_posicao" />
                                            <asp:GridView ID="grdHistorPaciente" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                                AutoGenerateColumns="false" ToolTip="Grade de histórico de agendamentos do(a) Paciente selecionado">
                                                <RowStyle CssClass="rowStyle" />
                                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                <EmptyDataTemplate>
                                                    Nenhum registro encontrado.<br />
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CK">
                                                        <ItemStyle Width="8px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hidCoAgenda" Value='<%# Eval("CO_AGEND") %>' runat="server" />
                                                            <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' runat="server" />
                                                            <asp:HiddenField ID="hidHora" Value='<%# Eval("HR") %>' runat="server" />
                                                            <asp:HiddenField ID="hidDia" Value='<%# Eval("DIA") %>' runat="server" />
                                                            <asp:CheckBox ID="ckSlctHora" runat="server" Checked="true" style="margin-right:-5px; margin-left:-5px;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NO_PAC" HeaderText="PACIENTE">
                                                        <ItemStyle Width="220px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="DATA/HORA">
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:label runat="server" Text='<%# Eval("DT_HORAR") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="OPER" HeaderText="OPERADORA">
                                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STATUS_V" HeaderText="STATUS">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
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
        function mostraDadosAgenda() {
            document.getElementById("DadosAgenda").style.display = "block";
        }
        function mostraErroPermi() {
            $("#lblMsgErro").fadeIn();

            setInterval(function () {
                $("#lblMsgErro").fadeOut("slow");
            }, 10000);
        }

        window.onload = function () {
            MaintainScrollProfi();
            MaintainScrollAgenda();
            MaintainScrollHistorico();
        }

        function MaintainScrollProfi() {
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

        function MaintainScrollAgenda() {
            var div = document.getElementById("divAgenda");
            var div_position = document.getElementById("divAgenda_posicao");
            var position = parseInt('<%= Request.Form["divAgenda_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

        function MaintainScrollHistorico() {
            var div = document.getElementById("divHistoricoAgenda");
            var div_position = document.getElementById("divHistoricoAgenda_posicao");
            var position = parseInt('<%= Request.Form["divHistoricoAgenda_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }


        $(function () {
            $(".campoHora").mask("99:99");
        });

        $(document).ready(function () {
            carregaCss();
        });

        //Inserida função apra abertura de nova janela popup com a url do relatório que apresenta as guias
        function customOpen(url) {
            var w = window.open(url);
            w.focus();
        }

        function carregaCss() {
            $(".campoCpf").unmask();
            $(".campoCpf").mask("999.999.999-99");
            $(".campoHora").unmask();
            $(".campoHora").mask("99:99");
        }
    </script>
</asp:Content>
