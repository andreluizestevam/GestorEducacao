<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8271_AgendaDePacienteAPlantonista.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
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
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
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
        .divPaciPreAtend
        {
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 180px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -10px;
        }
        .divGeral
        {
            border-top: 1px solid #CCCCCC;
            width: 1120;
            height: 270px;
            padding-top: 6px;
            margin-top: 6px;
        }
        .divDadosPaciResp
        {
            border-right: 1px solid #CCCCCC;
            float: left;
            width: 600px;
            height: 264px;
            clear: both;
        }
        .DivResp
        {
            float: left;
            width: 600px;
            height: 207px;
        }
        .divEncamMedicoGeral
        {
            width: 370px;
            height: 50px;
            float: right;
        }
        .dvDadosInternacao
        {
            width: 370px;
            height: 300px;
            float: right;
        }
        .divEncamMedico
        {
            border: 1px solid #CCCCCC;
            width: 367px;
            height: 119px;
            float: right;
            overflow-y: scroll;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        
        .lblsub
        {
            color: #436EEE;
            font-size: 11px;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
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
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display: inline;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .divClassPri
        {
        }
        .divClassRed
        {
            background-color: Red;
        }
        .divClassOrange
        {
            background-color: Orange;
        }
        .divClassYellow
        {
            background-color: Yellow;
        }
        .divClassGreen
        {
            background-color: Green;
        }
        .divClassBlue
        {
            background-color: Blue;
        }
        .lisobe
        {
            margin-top: -9px !important;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        .btnRel
        {
            width: 61px;
            height: 15px !important;
            color: #000000;
            border-radius: 5%;
            background-color: #dff1ff;
            cursor: pointer;
            margin-top: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <li style="margin-top: 2px;">
            <ul>
                <li style="margin: 0 6px 0 27px;">
                    <label style="margin-bottom: 1px;" title="Selecione a unidade onde o plantonista está alocado">
                        Unidade de Plantão:</label>
                    <asp:DropDownList ID="ddlUnidPlant" runat="server" Width="200px" ToolTip="Selecione a unidade onde o plantonista está alocado"
                        OnSelectedIndexChanged="ddlUnidPlant_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <label style="margin-bottom: 1px;" title="Selecione o local/departamento em que o agendamento será realizado">
                                Local de Plantão:</label>
                            <asp:DropDownList ID="ddlLocalPlant" runat="server" Width="200px" ToolTip="Selecione o local/departamento em que o agendamento será realizado">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlUnidPlant" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </li>
                <li>
                    <label style="margin-bottom: 1px;" title="Selecione a especialidade do plantonista">
                        Especialidade:</label>
                    <asp:DropDownList ID="ddlEspecPlant" runat="server" Width="246px" ToolTip="Selecione a especialidade do plantonista">
                    </asp:DropDownList>
                </li>
                <li>
                    <label style="margin-bottom: 1px;" title="Selecione o período em wue o plantonista esteja atuado">
                        Período:</label>
                    <asp:TextBox runat="server" class="campoData" ID="txtDataIni" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                    <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                    <asp:TextBox runat="server" class="campoData" ID="txtDataFim" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                </li>
                <li style="margin-top: 15px; margin-left: 5px;">
                    <asp:ImageButton ID="imgPesqGrid" OnClick="imgPesqGrid_OnClick" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                </li>
                <li>
                    <asp:Button Font-Bold="true" runat="server" CssClass="btnRel" ID="btnRelatorio" OnClick="btnRelatorio_Click"
                        Text="Guia Atend." ToolTip="Gerar guia para impressão da escala de atendimento do profissional" />
                </li>
            </ul>
        </li>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: -5px; background-color: #DFF1FF; text-align: center; font-weight: bold;
            margin-bottom: 11px">
            <ul>
                <li style="margin-left: 383px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        PROFISSIONAIS PLANTONISTAS</label>
                </li>
            </ul>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel0" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgPesqGrid" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="divPaciPreAtend">
                        <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 963px;"
                            AutoGenerateColumns="false" ToolTip="Grid dos Profissionais de Saúde Plantonistas"
                            OnRowDataBound="grdProfi_RowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum Profissional Plantonista encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidPlantao" runat="server" Value='<%# Eval("CO_PLANT") %>' />
                                        <asp:HiddenField ID="hidCol" runat="server" Value='<%# Eval("CO_COL") %>' />
                                        <asp:HiddenField ID="hidEmp" runat="server" Value='<%# Eval("CO_EMP") %>' />
                                        <asp:HiddenField ID="hidcoEspec" runat="server" Value='<%# Eval("CO_ESPEC") %>' />
                                        <asp:HiddenField ID="hidcoDept" runat="server" Value='<%# Eval("CO_DEPT") %>' />
                                        <asp:CheckBox ID="ckSelect" OnCheckedChanged="ckSelect_CheckedChange" runat="server"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_COL" HeaderText="PROFISSIONAL">
                                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_TEL" HeaderText="TEL">
                                    <ItemStyle Width="30px" HorizontalAlign="Left" CssClass="campoTelefone" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPEC">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_EMP" HeaderText="UNID">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_INI_PLANTAO" HeaderText="DT INI">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DT_FIM_PLANTAO" HeaderText="DT FIM">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Local">
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlLocalGrid" OnSelectedIndexChanged="ddlLocalGrid_SelectedIndexChanged"
                                            AutoPostBack="true" ToolTip="Local onde o atendimento aos paciente poderá ocorrer"
                                            Width="190px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: 10px; background-color: #DFF1FF; text-align: center; font-weight: bold;
            margin-bottom: 11px">
            <ul>
                <li style="margin-left: 416px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        GRADE DE PACIENTES</label>
                </li>
            </ul>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="divPaciPreAtend">
                        <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 15px;" AllowPaging="false" GridLines="Vertical" AutoGenerateColumns="false"
                            ToolTip="Grid de requerimento de internação em aberto (Clique no checkbox ou em qualquer local da linha para selecionar)"
                            AutoGenerateSelectButton="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum requerimento de internação em aberto<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidORIGEM" Value='<%# Eval("ORIGEM") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdProfAgendInter" Value='<%# Eval("ID_AGEND_PROF_MEDIC") %>' />
                                        <asp:HiddenField ID="hidIdAtendInter" runat="server" Value='<%# Eval("ID_AGEND_INTER") %>' />
                                        <asp:HiddenField ID="hidIdAtendAgend" runat="server" Value='<%# Eval("CO_ALU") %>' />
                                        <asp:CheckBox ID="chkselect" runat="server" Checked='<%# Eval("Check") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LOCAL" HeaderText="LOCAL" />
                                <asp:BoundField DataField="PACIENTE" HeaderText="PACIENTE">
                                    <ItemStyle Width="220px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SEXO" HeaderText="SEXO" />
                                <asp:BoundField DataField="IDADE" HeaderText="IDADE" />
                                <asp:BoundField DataField="MED_ORIGEM" HeaderText="MÉDICO ORIGEM" />
                                <asp:TemplateField HeaderText="DATA PREVISTA">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" class="campoData" ID="txtDataAtendimento" Style="margin: 0"
                                            Text='<%# Eval("DT_ATEND") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HORA PREVISTA">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtHoraAtendimento" Width="50px" class="campoHora"
                                            Style="margin: 0" Text='<%# Eval("HR_ATEND") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OBESERVAÇÃO">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtObsPaciente" MaxLength="200" Style="margin: 0"
                                            onkeydown="checkTextAreaMaxLength(this,event,'200');" Text='<%# Eval("OBSERVACAO") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoCepF").mask("99999-999");
            $('#txtNuNisPaci').mask('9999999999999999');
        });


        //fixa a quantidade de inetiros e limita o textbox apenas a inteiros também
        function fixedlength(txt, keyEvent, maxlength) {
            if (txt.value.length > maxlength) {
                txt.value = txt.value.substr(0, maxlength);
            }
            else if (txt.value.length < maxlength || txt.value.length == maxlength) {
                txt.value = txt.value.replace(/[^\d]+/g, '');
                return true;
            }
            else
                return false;
        }

        //Determina a quantidade de caracteres do textbox multiline
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }       

    </script>
</asp:Content>
