<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9112_ColaboCampanha.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
        }
        .ulDados li
        {
            <%--margin:5px 0 0 5px;--%>
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .campoTelefone
        {
            width: 60px;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
            margin-left: -5px;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
            margin-left: 0px;
        }
       .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .maskDin
        {
            width:50px;
            text-align:right;
        }
        .camp li
        {
            margin-left:5px;
            margin-top:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <ul class="ulDados">
        <li style="margin-left: 0px !important; float: left; width: 545px; border-right: 1px solid #CCCCCC">
            <ul class="camp">
                <li class="liTituloGrid" style="width: 97%; height: 20px !important; margin-right: 0px;
                    background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        CAMPANHA DE SAÚDE</label>
                </li>
                <li>
                    <label style="margin-bottom: 1px;" title="Informações para pesquisa por Campanhas de Saúde"
                        class="lblTitu">
                        Pesquisa</label>
                </li>
                <li style="clear: both">
                    <label title="Tipo da Campanha de Saúde">
                        Tipo</label>
                    <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                        Width="124px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Competência da Campanha de Saúde">
                        Competência</label>
                    <asp:DropDownList runat="server" ID="ddlCompetencia" Width="80px" ToolTip="Competência da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Classificação da Campanha de Saúde">
                        Classificação</label>
                    <asp:DropDownList runat="server" ID="ddlClassCamp" Width="94px" ToolTip="Classificação da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li style="clear: both">
                    <label>
                        Período</label>
                    <asp:TextBox ID="txtDataIniCamp" Style="margin: 0px !important;" runat="server" CssClass="campoData"
                        ToolTip="Data de início para pesquisa de Campanhas de Saúde">
                    </asp:TextBox>
                    <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
                    <asp:TextBox runat="server" ID="txtDataFimCamp" CssClass="campoData" ToolTip="Data de término para pesquisa de Campanhas de Saúde"></asp:TextBox>
                </li>
                <li>
                    <label title="Seleciona a situação para pesquisa por plantões já agendados">
                        Situação</label>
                    <asp:DropDownList runat="server" ID="ddlSituCampSaude" Width="80px" ToolTip="Seleciona a situação para pesquisa por plantões já agendados">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 16px; margin-left: 0px;">
                    <asp:ImageButton ID="imgPesq" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgPesq_OnClick" />
                </li>
                <%--<asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <li style="margin-top: 0px !important">
                            <div id="div3" runat="server" class="divGridData" style="height: 130px; width: 528px;
                                border: 1px solid #ccc;">
                                <asp:GridView ID="grdCampSaude" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Campanhas de Saúde">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma Campanha de Saúde encontrada.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoCampan" Value='<%# Eval("ID_CAMPAN") %>' runat="server" />
                                                <asp:CheckBox ID="chkSelectCamp" runat="server" OnCheckedChanged="chkSelectCamp_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dataValid" HeaderText="DATA">
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HORA" HeaderText="HORA">
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noCampa" HeaderText="NOME">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_Valid" HeaderText="TIPO">
                                            <ItemStyle Width="40px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="comp_Valid" HeaderText="COMP">
                                            <ItemStyle Width="40px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="classi_Valid" HeaderText="CLASS">
                                            <ItemStyle Width="40px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li style="margin-top: 14px !important">
                            <ul>
                                <li class="liTituloGrid" style="width: 99%; height: 20px !important; margin-right: 0px;
                                    margin-left: 0px !important; margin-bottom: 1px; background-color: #d2ffc2; text-align: center;
                                    font-weight: bold;">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        EQUIPE CAMPANHAS DE SAÚDE</label>
                                </li>
                            </ul>
                            <div id="div1" runat="server" class="divGridData" style="height: 130px; width: 528px;
                                border: 1px solid #ccc;">
                                <asp:GridView ID="grdEquipCamp" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid que apresenta a Equipe da Campanha de Saúde selecionada">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum Colaborador associado à Campanha selecionada.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoColabCampan" Value='<%# Eval("CO_COLAB_CAMPAN") %>' runat="server" />
                                                <asp:CheckBox ID="chkSelectEquipe" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="noColab" HeaderText="NOME">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CPF_Valid" HeaderText="CPF">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="noFuncao" HeaderText="FUNÇÃO">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nuTelef_Valid" HeaderText="TELEFONE">
                                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="situa_Valid" HeaderText="Situação">
                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </ul>
        </li>
        <li style="width: 420px; float: right;">
            <%--<asp:UpdatePanel ID="updColab" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <ul class="camp">
                        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
                            background-color: #EEEEE0; text-align: center; font-weight: bold; margin-bottom: 30px">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                COLABORADOR</label>
                        </li>
                        <li style="clear: both">
                            <label title="Selecione o Colaborador para associá-lo à Campanha de Saúde">
                                Colaborador</label>
                            <asp:DropDownList runat="server" ID="ddlColab" Width="235px" ToolTip="Selecione o Colaborador para associá-lo à Campanha de Saúde"
                                OnSelectedIndexChanged="ddlColab_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label title="Nome do Colaborador que fará parte da equipe na Campanha de Saúde">
                                Nome Responsável</label>
                            <asp:TextBox runat="server" ID="txtNoColab" ToolTip="Nome do Colaborador que será Associado à Campanha de Saúde"
                                MaxLength="100" Width="235px"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Função</label>
                            <asp:TextBox runat="server" ID="txtFuncao" Width="130px" MaxLength="50"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Identidade</label>
                            <asp:TextBox runat="server" ID="txtIdentidade" Width="90px" MaxLength="20"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                CPF</label>
                            <asp:TextBox runat="server" ID="txtCPF" Width="80px" CssClass="campoCPF"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Telefone</label>
                            <asp:TextBox runat="server" ID="txtNrTele" CssClass="campoTelefone"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Celular</label>
                            <asp:TextBox runat="server" ID="txtNrCelu" CssClass="campoTelefone"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Email</label>
                            <asp:TextBox runat="server" ID="txtEmail" Width="160px" MaxLength="100"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                R$ Diário</label>
                            <asp:TextBox runat="server" ID="txtValDiario" CssClass="maskDin" ToolTip="Valor que o(a) Colaborador(a) receberá por dia"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                R$ Gratificação</label>
                            <asp:TextBox runat="server" ID="txtValFinal" CssClass="maskDin" ToolTip="Gratificação que o(a) Colaborador(a) receberá"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Situação</label>
                            <asp:DropDownList runat="server" ID="ddlSituacao" Width="120px">
                            </asp:DropDownList>
                        </li>
                    </ul>
            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            CarregaCss();
        });

        function CarregaCss() {
            $(".campoHora").mask("99:99");
            $(".campoTelefone").mask("(99)9999-9999");
            $(".campoCPF").mask("999.999.999-99");
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
            $(".maskDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        }

//        var prm = Sys.WebForms.PageRequestManager.getInstance();
//        prm.add_endRequest(function () {
//            CarregaCss();
    </script>
</asp:Content>
