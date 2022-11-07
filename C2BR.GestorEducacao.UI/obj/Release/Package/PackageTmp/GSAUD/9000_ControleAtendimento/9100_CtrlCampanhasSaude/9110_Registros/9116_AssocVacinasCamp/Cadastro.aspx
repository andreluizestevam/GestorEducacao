<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9116_AssocVacinasCamp.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
            margin-top: 40px !important;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
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
            margin: 3px 0 1px 0;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .grdBuscaLeft th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display:inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <li style="margin-left: 0px !important; margin-top: -13px !important; width: 543px;
            height: auto; border-right: 1px solid #CCCCCC">
            <asp:UpdatePanel ID="updInfos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hidQtdAssoc" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <ul class="camp">
                        <li class="liTituloGrid" style="width: 98%; height: 20px !important; margin-right: 0px;
                            margin-top: -0px; background-color: #AFEEEE; text-align: center; font-weight: bold;
                            margin-bottom: 5px">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                CAMPANHA DE SAÚDE</label>
                        </li>
                        <li>
                            <label style="" title="Informações para pesquisa por Campanhas de Saúde" class="lblTitu">
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
                        <li>
                            <label title="Seleciona a situação para pesquisa por plantões já agendados">
                                Situação</label>
                            <asp:DropDownList runat="server" ID="ddlSituCampSaude" Width="80px" ToolTip="Seleciona a situação para pesquisa por plantões já agendados">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 10px; margin-left: 0px;">
                            <asp:ImageButton ID="imgPesqCampV" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                OnClick="imgPesqCampV_OnClick" ToolTip="Pesquise as Campanhas de Saúde de acordo com os filtros informados" />
                        </li>
                        <%--<asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <li style="margin-top: 10px !important">
                            <div id="div3" runat="server" class="divGridData" style="height: 323px; width: 528px;
                                border: 1px solid #ccc;">
                                <asp:HiddenField runat="server" ID="hidIdCampa" />
                                <asp:GridView ID="grdCampSaude" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Campanhas de Saúde"
                                    DataKeyNames="ID_CAMPAN" OnSelectedIndexChanged="grdCampSaude_SelectedIndexChanged"
                                    AutoGenerateSelectButton="false" OnRowDataBound="grdCampSaude_RowDataBound">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma Campanha de Saúde encontrada.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="ID_CAMPAN" HeaderText="Cod." SortExpression="CO_EMP"
                                            HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                            <HeaderStyle CssClass="noprint"></HeaderStyle>
                                            <ItemStyle Width="20px" />
                                        </asp:BoundField>
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
                                        <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li style="width: 420px; float: right;">
            <ul class="ulDadosUsu">
                <li>
                    <ul>
                        <asp:UpdatePanel ID="updVacinasDisponiveis" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <li class="liTituloGrid" style="width: 99%; height: 20px !important; margin-right: 0px;
                                    margin-left: 0px !important; margin-bottom: 5px; background-color: #d2ffc2; text-align: center;
                                    font-weight: bold; margin-top: -13px;">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        VACINAS DISPONÍVEIS</label>
                                </li>
                                <li style="clear: both">
                                    <label style="margin-bottom: 2px;" title="Informações para pesquisa por Campanhas de Saúde"
                                        class="lblTitu">
                                        Pesquisa</label>
                                </li>
                                <li style="clear: both">
                                    <label title="Filtre pela Sigla da Vacina">
                                        Sigla</label>
                                    <asp:TextBox runat="server" ID="txtSiglaVacina" ToolTip="Filtre pela Sigla da Vacina"
                                        Width="70px" MaxLength="12"></asp:TextBox>
                                </li>
                                <li>
                                    <label title="Filtre pelo Nome da Vacina">
                                        Nome</label>
                                    <asp:TextBox runat="server" ID="txtNomeVacina" ToolTip="Filtre pelo Nome da Vacina"
                                        Width="200px" MaxLength="80"></asp:TextBox>
                                </li>
                                <li style="margin:12px 6px 0 -4px;">
                                    <asp:CheckBox runat="server" ID="chkSomenteDisponiveis" Text="Apenas Disponíveis" CssClass="chk" ToolTip="Marque caso deseje que apenas as Vacinas disponíveis para associação fiquem visíveis" OnCheckedChanged="chkSomenteDisponiveis_OnCheckedChanged" AutoPostBack="true"/>
                                </li>
                                <li style="margin-top: 10px; margin-left: 0px;">
                                    <asp:ImageButton ID="imgPesqVacinas" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        OnClick="imgPesqVacinas_OnClick" ToolTip="Pesquise as Vacinas de acordo com os filtros informados" />
                                </li>
                                <li style="margin-top: 1px !important">
                                    <div id="div1" runat="server" class="divGridData" style="height: 140px; width: 415px;
                                        border: 1px solid #ccc;">
                                        <asp:GridView ID="grdVacinasDisponiveis" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Campanhas de Saúde"
                                            DataKeyNames="ID_VACINA" OnSelectedIndexChanged="grdVacinasDisponiveis_SelectedIndexChanged"
                                            AutoGenerateSelectButton="false" OnRowDataBound="grdVacinasDisponiveis_RowDataBound">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhuma Vacina encontrada.<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField Visible="false" DataField="ID_VACINA" HeaderText="Cod." SortExpression="CO_EMP"
                                                    HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                    <HeaderStyle CssClass="noprint"></HeaderStyle>
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="CK">
                                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="chkMarcaTodasVacinas" OnCheckedChanged="chkMarcaTodosItensPendentes_OnCheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidIdVacina" Value='<%# Eval("ID_VACINA") %>' runat="server" />
                                                        <asp:CheckBox ID="chkSelectVacin" runat="server" OnCheckedChanged="chkSelectVacin_OnCheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CO_SIGLA_VACINA" HeaderText="SIGLA">
                                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NM_VACINA" HeaderText="NOME">
                                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                    <asp:UpdatePanel ID="updVacinasAssociadas" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <ul>
                                <li style="width: 420px; float: right; clear: both; margin: 29px 0 0 0px;">
                                    <ul class="ulDadosUsu">
                                        <li class="liTituloGrid" style="width: 99.5%; height: 20px !important; margin-right: 0px;
                                            margin-left: 0px !important; margin-bottom: -5px; background-color: #F5DEB3;
                                            text-align: center; font-weight: bold; margin-top: -13px;">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                VACINAS ASSOCIADAS À CAMPANHA</label>
                                        </li>
                                        <li style="margin-top: 10px !important">
                                            <div id="div2" runat="server" class="divGridData" style="height: 140px; width: 415px;
                                                border: 1px solid #ccc;">
                                                <asp:GridView ID="grdVacinasAssociadas" CssClass="grdBusca" runat="server" Style="width: 100%;
                                                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Vacinas associadas à Campanhas de Saúde"
                                                    DataKeyNames="ID_VACINA">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                    <EmptyDataTemplate>
                                                        Nenhuma Vacina Associada à Campanha Selecionada<br />
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="CO_SIGLA_VACINA" HeaderText="SIGLA">
                                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NM_VACINA" HeaderText="NOME">
                                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="EXCLUIR">
                                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkExc" OnClientClick="if (!confirm('Deseja realmente Excluir a Ass ociação?')) return false;"
                                                                    runat="server" Style="margin: 0 auto;" ToolTip="Excluir Associação da Vacina na Grid"
                                                                    OnClick="lnkExc_OnClick">
                                                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                                                        title="Excluir Registro" />
                                                                    <asp:HiddenField ID="hidIdAssoc" Value='<%# Eval("ID_VACIN_CAMPSAUDE") %>' runat="server" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>
