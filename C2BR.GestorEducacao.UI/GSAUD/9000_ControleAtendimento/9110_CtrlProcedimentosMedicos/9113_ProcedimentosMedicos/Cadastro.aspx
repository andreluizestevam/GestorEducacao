<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9113_ProcedimentosMedicos.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin: 12px 0 0 300px !important;
            width: 490px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px !important;
        }
        .chk label
        {
            display: inline;
        }
        .campoDinheiro
        {
            text-align: right;
            width: 55px;
        }
        .liBtnAddA
        {
            /*background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
            height: 15px;*/
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
            margin-top: 5px; /*background-color: #5858FA;*/
            margin-right: 5px;
            float: right;
        }
        .liBtnAddA span
        {
            font-weight: bold;
            margin-left: 4px;
            margin-right: 4px;
            color: #666666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidCoSitua" />
    <ul class="ulDados">
        <li>
            <label title="Nome do Procedimento de Saúde" class="lblObrigatorio">
                Nome Procedimento</label>
            <asp:TextBox runat="server" ID="txtNoProcedimento" Width="361px" MaxLength="100"
                ToolTip="Nome do Procedimento de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtNoProcedimento"
                ErrorMessage="Nome do Procedimento Saúde é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Código do Procedimento de Saúde" class="lblObrigatorio">
                Código Pro.</label>
            <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Código do Procedimento Médico"
                Width="50px" CssClass="campoCodigo"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtCodProcMedic"
                ErrorMessage="Código do Procedimento Médico é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-top: -3px;">
            <label title="Nome do Procedimento de Saúde" class="lblObrigatorio">
                Nome Reduzido</label>
            <asp:TextBox runat="server" ID="txtNomeRedu" Width="205px" MaxLength="50" ToolTip="Nome Reduzido do Procedimento de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtNomeRedu"
                ErrorMessage="Nome Reduzido do Procedimento Saúde é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -3px;">
            <label title="Complemento do Procedimento de Saúde">
                Complemento</label>
            <asp:TextBox runat="server" ID="txtComplemento" Width="205px" MaxLength="50" ToolTip="Complemento do Procedimento de Saúde"></asp:TextBox>
        </li>
        <li style="clear: both; margin-top: -3px;">
            <label title="Grupo de Procedimentos de Saúde" class="lblObrigatorio">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="140px" ToolTip="Grupo de Procedimentos de Saúde"
                OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlGrupo"
                ErrorMessage="Nome do Grupo é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: -3px;">
            <label title="SubGrupo de Procedimentos de Saúde" class="lblObrigatorio">
                SubGrupo</label>
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="130px" ToolTip="SubGrupo de Procedimentos de Saúde">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlSubGrupo"
                ErrorMessage="Nome do SubGrupo é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 305px; margin-top: -28px">
            <label title="Classificação" class="">
                Classificações
            </label>
            <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstClassificacao" Height="60px"
                Width="130px" ToolTip="Lista das Classificação"></asp:ListBox>
            <%--            <asp:DropDownList runat="server" ID="ddlClassificacao" Width="130px" ToolTip="Classificação">
            </asp:DropDownList>--%>
        </li>
        <%--        <li style="clear: both; margin-bottom: -4px">
            <label>
                Cod</label>
            <asp:TextBox runat="server" ID="txtCodOper" Enabled="false" Width="40px" ToolTip="Código da Operadora"></asp:TextBox>
        </li>--%>
        <li style="margin-bottom: 4px; margin-top: -30px">
            <label title="Pesquise pelo Operadora de Planos de Saúde">
                Contratação</label>
            <asp:DropDownList runat="server" ID="ddlOper" Width="140px" ToolTip="Pesquise pelo Operadora de Planos de Saúde"
                OnSelectedIndexChanged="ddlOper_ddlOperOnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="margin-bottom: 4px; margin-left: 13px !important; margin-top: -30px">
            <label title="Tipo do Procedimento" class="lblObrigatorio">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoProcedimento" Width="130px" ToolTip="Tipo do Procedimento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlTipoProcedimento"
                ErrorMessage="Nome do Procedimento é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <fieldset style="padding-right: 5px;">
                <legend>Valores</legend>
                <ul>
                    <li style="clear: both">
                        <label>
                            Data Valores</label>
                        <asp:TextBox runat="server" ID="txtDtValores" Enabled="false" Width="53px"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor de Custo do procedimento">
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDinheiro" ToolTip="Valor de Custo atual do procedimento"
                            Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor base do procedimento de saúde para cálculo">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" Enabled="false" CssClass="campoDinheiro"
                            ToolTip="Valor base do procedimento de saúde para cálculo" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor Base da Operadora para o procedimento">
                            R$ Restitu</label>
                        <asp:TextBox runat="server" ID="txtVlRestitu" Enabled="false" CssClass="campoDinheiro"
                            ToolTip="Valor de Restituição atual do procedimento" ClientIDMode="Static"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li style="margin: 18px 0 0 35px;">
            <ul style="margin-top:-18px;">
                <li>
                    <label title="Quantidade de Seção Autorizadas Pelo plano de Saúde">
                        QSA</label>
                    <asp:TextBox runat="server" Width="40px" ID="txtQtSecaoAutorizada" ToolTip="Quantidade de Seção Autorizadas pelo plano de Saúde"></asp:TextBox>
                </li>
                <li>
                    <label title="Quantidade de Auxiliares para o procedimento">
                        QAUX</label>
                    <asp:TextBox runat="server" ID="txtQTAux" Width="40px" ToolTip="Quantidade de Auxiliares para o procedimento"
                        CssClass="campoDinheiro"></asp:TextBox>
                </li>
                <li>
                    <label title="Quantidade de Anestesistas para o procedimento">
                        QANE</label>
                    <asp:TextBox runat="server" ID="txtQTAnest" Width="40px" ToolTip="Quantidade de Anestesistas para o procedimento"
                        CssClass="campoDinheiro"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li style="margin-left:40px; margin-top:-6px;">
            <label title="Procedimentos">Procedimentos</label>
            <asp:DropDownList runat="server" ID="ddlSigTap" style="width:135px;"></asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Observação do Procedimento de Saúde">
                Observação</label>
            <asp:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Width="427px"
                Height="60px" ToolTip="Observação do Procedimento de Saúde"></asp:TextBox>
        </li>
        <li>
            <label>
                Agrupador</label>
            <asp:DropDownList runat="server" ID="ddlAgrupadora" Width="180px" ToolTip="Selecione caso o procedimento em questão tenha relação com outro">
            </asp:DropDownList>
        </li>
        <li>
            <label for="lblTipo" title="Protocolo">
                Protocolo</label>
            <asp:DropDownList ID="ddlProtocolo" Width="120px" ToolTip="Escolha o protocolo do procedimento"
                runat="server" class="ddlProtocolo">
            </asp:DropDownList>
        </li>
        <li style="margin-left: 14px;">
            <label title="Situação de cadastro do Procedimento de Saúde">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação de cadastro do Procedimento de Saúde">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlSituacao"
                ErrorMessage="Situação é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;" class="liBtnAddA">
            <asp:LinkButton ID="lnkBtnAdiocionarKit" Enabled="true" runat="server" OnClick="lnkBtnAdiocionarKit_OnClick">
                <asp:Label runat="server" Text="ADICIONAR KIT DE ITENS"  Style="margin-left: 4px;"></asp:Label>
            </asp:LinkButton>
        </li>
        <li style="margin-left: -1px; clear: both;">
            <label style="margin: 0px 0px 5px 6px;">
                Procedimento para uso:
            </label>
            <asp:CheckBox runat="server" ID="chkProfissionalSaude" Text="Profissional de Saúde"
                CssClass="chk" ToolTip="Selecione caso o procedimento em questão seja para uso do profissional de saúde" />
        </li>
        <li style="clear: both; margin-left: -1px;">
            <asp:CheckBox runat="server" ID="chkTecnico" Text="Técnico" CssClass="chk" ToolTip="Selecione caso o procedimento em questão seja para uso do técnico" />
        </li>
        <li style="clear: both; margin-left: -1px;">
            <asp:CheckBox runat="server" ID="chkUsoExterno" Text="Habilitado para uso sem atendimento interno (consulta)"
                CssClass="chk" ToolTip="O procedimento aparecerá para funcionalidades que tenham atendimento externo" />
        </li>
        <li style="margin-left: -1px; clear: both;">
            <asp:CheckBox runat="server" ID="chkRequerAuto" Text="Requer Autorização" CssClass="chk"
                ToolTip="Selecione caso o procedimento em questão precise de aprovação na Central de Regulação" />
        </li>
    </ul>
    <div id="divKitItens" style="display: none; height: 430px !important; width: 800px">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <ul class="ulDados" style="margin: 5px 0 0 -10px !important; width: 100%;">
                    <li>
                        <ul>
                            <li>
                                <label>
                                    Tipo de item</label>
                                <asp:DropDownList ID="ddlTipoItem" Width="210px" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlTipoItem_OnSelectedIntexChanged" />
                            </li>
                            <li>
                                <label>
                                    Grupo de itens</label>
                                <asp:DropDownList ID="ddlGrupoItens" Width="210px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlGrupoItens_OnSelectedIntexChanged" />
                            </li>
                            <li>
                                <label>
                                    Subgrupo de itens</label>
                                <asp:DropDownList ID="ddlSubGrupoItens" Width="280px" runat="server" AutoPostBack="true" />
                            </li>
                            <li style="margin-top: 7px;">
                                <asp:ImageButton ID="imgbPesqItens" OnClick="imgbPesqItens_OnClick" runat="server" style="margin: 10px 0 0 10px;"
                                    ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" />
                            </li>
                        </ul>
                    </li>
                    <li>
                        <div style="margin-left: 5px; width: 755px; height: 160px; border: 1px solid #CCC;
                            overflow-y: scroll">
                            <asp:GridView ID="grdPesqEstoque" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum item encontrado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="CK">
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidIdPesqEstoque" Value='<%# Eval("ID_ITEM_ESTOQUE") %>' runat="server" />
                                            <asp:HiddenField ID="hidPesqUnidadePadrao" Value='<%# Eval("CO_UNID_ITEM") %>' runat="server" />
                                            <asp:CheckBox runat="server" ID="chkItensEstoque" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NOME_ITEM" HeaderText="NOME">
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UNIDADE_PADRAO" HeaderText="UNID. PADRÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="UNID. DE USO">
                                        <ItemStyle Width="180px" />
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlUnidadeUso" Width="80px" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD BAS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtdeBase" Width="30px" CssClass="qtdeItem" Style="margin-bottom: 0px;"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD MIN">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtdeMinima" Width="30px" CssClass="qtdeItem" Style="margin-bottom: 0px;"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTD MAX">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtdeMaxima" Width="30px" CssClass="qtdeItem" Style="margin-bottom: 0px;"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBRIG">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlObrigatorio" Width="50px" Style="margin-bottom: 0px;" runat="server">
                                                <asp:ListItem Value="false" Selected="True">Não</asp:ListItem>
                                                <asp:ListItem Value="true">Sim</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObs" Width="160px" Style="margin-bottom: 0px;" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                    <li style="clear: both; margin: 17px 0px;">
                        <ul>
                            <li title="Clique para adicionar o medicamento" class="liBtnAddA" style="clear: both;
                                margin-top: -4px; margin-left: 350px !important; width: 75px;">
                                <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Medicamento" src="/Library/IMG/Gestor_SaudeEscolar.png"
                                    height="15px" width="15px" />
                                <asp:LinkButton ID="lnkAddItem" runat="server" ValidationGroup="AddMedic" OnClick="lnkAddItem_OnClick">ADICIONAR</asp:LinkButton>
                            </li>
                        </ul>
                    </li>
                    <li style="margin-left: 15px !important; clear: both; float: left;">
                        <ul>
                            <li style="clear: both; margin: -7px 0 0 -5px;">
                                <div style="width: 755px; height: 140px; border: 1px solid #CCC; overflow-y: auto">
                                    <asp:GridView ID="grdItensEstoque" CssClass="grdBusca" runat="server" Style="width: 100%;
                                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                        ShowHeaderWhenEmpty="false">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum item associado<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="NOME_ITEM" HeaderText="NOME">
                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="UNIDADE_PADRAO" HeaderText="UNIDADE PADRÃO">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="UNIDADE_USO" HeaderText="UNID. DE USO">
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTDE_BASE" HeaderText="QTD BAS">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTDE_MINIMA" HeaderText="QTD MIN">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QTDE_MAX" HeaderText="QTD MAX">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DE_OBRIGATORIO" HeaderText="OBRIG">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                              <asp:BoundField DataField="DE_OBSER" HeaderText="OBS">
                                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="*">
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="imgExcItem" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                        ToolTip="Excluir Medicamento" OnClick="imgExcItem_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                    <asp:HiddenField ID="hidIdItemEstoque" Value='<%# Eval("ID_ITEM_ESTOQUE") %>' runat="server" />
                                                    <%--<asp:HiddenField ID="B" Value='<%# Eval("CO_UNID_ITEM") %>' runat="server" />--%>
                                                    <asp:HiddenField ID="hidCodUnidadeUso" Value='<%# Eval("COD_UNIDADE_USO") %>' runat="server" />
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
    </div>
    <script type="text/javascript">

        function AbrirModalKitItens() {
            $('#divKitItens').dialog({ autoopen: false, modal: true, width: 777, height: 470, resizable: false, title: "ASSOCIAÇÃO DE ITENS DE ESTOQUE (FARMÁCIA) A PROCEDIMENTOS DE SAÚDE",
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }



        jQuery(function ($) {

            $(".campoCodigo");
            $(".campoDinheiro").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".qtdeItem").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //Se ao deixar o campo, o mesmo estiver vazio, insere 0
            $("#txtVlCusto").blur(function () {
                if ($("#txtVlCusto").val() == "")
                    $("#txtVlCusto").val("0,00");
            });
            $("#txtVlBase").blur(function () {
                if ($("#txtVlBase").val() == "")
                    $("#txtVlBase").val("0,00");
            });
            $("#txtVlRestitu").blur(function () {
                if ($("#txtVlRestitu").val() == "")
                    $("#txtVlRestitu").val("0,00");
            });
        });
    </script>
</asp:Content>
