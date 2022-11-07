<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7140_RegistroPlantao.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 100%;
        }
        
        .ulDados li
        {
            margin-left: 5px;
        }
        
        .ulDados label
        {
            margin-bottom:1px;
        }
        .liPrima
        {
            clear: both;
            margin-left: 10px !important;
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
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidCoCol" runat="server" Value="0" />
    <asp:HiddenField ID="hidCoEmpCol" runat="server" Value="0" />
    <asp:HiddenField ID="hidCoEspCol" runat="server" />
    <asp:HiddenField ID="hidCoDepCol" runat="server" />
    <ul id="ul10" class="ulDados">
        <li style="margin-left: 40px !important; margin-top: 10px !important;">
            <ul>
                <li style="width: 448px;">
                    <asp:UpdatePanel ID="updProfi" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%-- Grid de profissionais --%>
                            <ul>
                                <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
                                    background-color: #ffff99; text-align: center; font-weight: bold; margin-bottom: 5px">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                        GRID DE PROFISSIONAIS PLANTONISTAS</label>
                                </li>
                                <li>
                                    <label for="ddlEspMedResCons" title="Selecione a especialidade para filtrar os Profissionais de Saúde">
                                        Especialidade Médica</label>
                                    <asp:DropDownList ID="ddlEspMedResCons" OnSelectedIndexChanged="ddlEspMedResCons_SelectedIndexChanged"
                                        AutoPostBack="true" Width="214px" runat="server" ToolTip="Selecione a especialidade para filtrar os Profissionais de Saúde">
                                    </asp:DropDownList>
                                </li>
                                <li class="liClear">
                                    <label for="ddlUnidResCons" title="Selecione a Unidade para filtrar os Profissionais de Saúde">
                                        Unidade</label>
                                    <asp:DropDownList ID="ddlUnidResCons" OnSelectedIndexChanged="ddlUnidResCons_SelectedIndexChanged"
                                        AutoPostBack="true" Width="214px" runat="server" ToolTip="Selecione a Unidade para filtrar os Profissionais de Saúde">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 10px !important;">
                                    <div id="divGrdProfi" runat="server" class="divGridData" style="height: 309px; width: 440px;
                                        border: 1px solid #ccc;">
                                        <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 423px;
                                            height: 20px;" AutoGenerateColumns="false" ToolTip="Grid dos Profissionais de Saúde Plantonistas">
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
                                                        <asp:HiddenField ID="hidCol" runat="server" Value='<%# Eval("CO_COL") %>' />
                                                        <asp:HiddenField ID="hidEmp" runat="server" Value='<%# Eval("CO_EMP") %>' />
                                                        <asp:HiddenField ID="hidcoEspec" runat="server" Value='<%# Eval("CO_ESPEC") %>' />
                                                        <asp:HiddenField ID="hidcoDept" runat="server" Value='<%# Eval("CO_DEPT") %>' />
                                                        <asp:CheckBox ID="ckSelect" AutoPostBack="true" OnCheckedChanged="ckSelect_CheckedChange"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NOME" HeaderText="MÉDICO(A)">
                                                    <ItemStyle Width="223px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DE_ESP" HeaderText="ESPEC">
                                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_EMP" HeaderText="UNID">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CHPLANT" HeaderText="CH">
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TMPDESCA" HeaderText="TD">
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- Grid de profissionais --%>
                </li>
                <li style="clear: none !important; width: 433px; margin-left: 15px !important;">
                    <%-- Grid de horário --%>
                    <asp:UpdatePanel ID="updAgend" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <ul>
                                <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
                                    background-color: #d2ffc2; text-align: center; font-weight: bold;">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        AGENDAMENTO DE PLANTÃO</label>
                                </li>
                                <li style="margin-top: 5px;">
                                    <label style="margin-bottom: 1px;" title="Selecione a unidade onde o plantão será realizado">
                                        Unidade de Plantão</label>
                                    <asp:DropDownList ID="ddlUnidPlant" runat="server" Width="200px" ToolTip="Selecione a unidade onde o plantão será realizado"
                                        OnSelectedIndexChanged="ddlUnidPlant_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 5px;">
                                    <label style="margin-bottom: 1px;" title="Selecione o tipo de plantão que será realizado">
                                        Tipo Plantão</label>
                                    <asp:DropDownList ID="ddlTipoPlant" runat="server" Width="209px" ToolTip="Selecione o tipo de plantão que será realizado"
                                        OnSelectedIndexChanged="ddlTipoPlant_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both; margin-top: 5px;">
                                    <label style="margin-bottom: 1px;" title="Selecione o local/departamento em que o plantão será realizado">
                                        Local de Plantão</label>
                                    <asp:DropDownList ID="ddlLocalPlant" runat="server" Width="200px" ToolTip="Selecione o local/departamento em que o plantão será realizado">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 5px;">
                                    <label title="Data que o Plantão será realizado">
                                        Data do Plantão</label>
                                    <asp:TextBox runat="server" ID="txtdtPl" CssClass="campoData" ToolTip="Data que o Plantão será realizado"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -4px;">
                                    <label style="margin-bottom: 1px;" title="Selecione a especialidade do plantão">
                                        Especialidade</label>
                                    <asp:DropDownList ID="ddlEspecPlant" runat="server" Width="246px" ToolTip="Selecione a especialidade do plantão">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 15px;">
                                    <label style="margin-bottom: 1px;" title="Período para pesquisa por plantões já agendados">
                                        Pesquisa</label>
                                    <asp:TextBox ID="txtDtIniResCons" Style="margin: 0px !important;" runat="server"
                                        CssClass="campoData" ToolTip="Data de início para pesquisa de plantões agendados">
                                    </asp:TextBox>
                                    <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" ID="txtDtFimResCons" CssClass="campoData" ToolTip="Data de término para pesquisa de plantões agendados"></asp:TextBox>
                                </li>
                                <li style="margin-top: 15px;">
                                    <label title="Seleciona a situação para pesquisa por plantões já agendados">
                                        Situação</label>
                                    <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Seleciona a situação para pesquisa por plantões já agendados">
                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Aberto" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Realizado" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Planejado" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 27px; margin-left: 0px;">
                                    <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        OnClick="imgCpfResp_OnClick" />
                                </li>
                                <li style="margin-top: 0px !important">
                                    <ul>
                                        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
                                            margin-left: 0px !important; margin-bottom: 5px; background-color: #EEEEE0; text-align: center;
                                            font-weight: bold;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                AGENDA DE PLANTÕES DO PROFISSIONAL</label>
                                        </li>
                                    </ul>
                                    <div id="div3" runat="server" class="divGridData" style="height: 173px; width: 433px;
                                        border: 1px solid #ccc;">
                                        <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" Style="width: 416px;
                                            height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de plantões agendados">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum Plantão agendado encontrado.<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="data" HeaderText="DATA">
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="coSiglaEmp" HeaderText="UNID">
                                                    <ItemStyle Width="45px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="coSiglaDepte" HeaderText="LOCAL">
                                                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="coSiglaEspec" HeaderText="ESPECIALIDADE">
                                                    <ItemStyle Width="88px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hrIni" HeaderText="INÍCIO">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="hrFim" HeaderText="TERM">
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="deSitua" HeaderText="SITUA">
                                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="RI">
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncon" runat="server" Visible='<%# Eval("Icon") %>'>*</asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <label>
                                        *RI - Registro de Inconsistência</label>
                                </li>
                            </ul>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- Grid de horário --%>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            carregaCss();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

        function carregaCss() {
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        }

    </script>
</asp:Content>
