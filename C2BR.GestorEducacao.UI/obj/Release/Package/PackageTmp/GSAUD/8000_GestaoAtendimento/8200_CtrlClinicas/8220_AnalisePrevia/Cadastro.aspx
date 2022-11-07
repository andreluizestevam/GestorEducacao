<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8220_AnalisePrevia.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1120px;
        }
        .ulPer label
        {
            text-align: left;
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
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <ul class="divEncamMedicoGeral">
                <li style="clear: both; margin-left: 15px;">
                    <ul>
                        <li class="liTituloGrid" style="width: 974px; height: 20px !important; margin-right: 0px;
                            background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        PROCEDIMENTOS SOLICITADOS</label>
                                </li>
                                <li style="margin-left: 23px; float: right; margin-top: 2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                                            <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="lblGrpProc">
                                                Grupo Proc
                                            </asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlGrupoProc" Width="140px" OnSelectedIndexChanged="ddlGrupoProc_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="lblSubGrpProc">
                                                Subgrupo Proc</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="140px">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="Label3">
                                                Situação
                                            </asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlSituacao">
                                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="Em Análise" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="Encaminhado" Value="E"></asp:ListItem>
                                                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqProcedimentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqProcedimentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <div style="width: 972px; height: 218px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:HiddenField runat="server" ID="hidIdItem" />
                                <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação em Aberto<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidItemSolic" Value='<%# Eval("ID_ITEM_SOLIC") %>' runat="server" />
                                                <asp:HiddenField ID="hidCoColObAnali" Value='<%# Eval("CO_COL_OBJET_ANALI") %>' runat="server" />
                                                <asp:CheckBox ID="chkselectSolic" runat="server" OnCheckedChanged="chkselectSolic_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PACIENTE">
                                            <ItemStyle Width="220px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfoPaciente" ImageUrl="/Library/IMG/Gestor_ImgInformacao.png"
                                                    ToolTip="Visualizar maiores informações sobre o(a) Paciente" Style="margin: 0 0 0 0 !important;"
                                                    OnClick="imgInfoPaciente_OnClick" />
                                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IDADE" HeaderText="ID">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <%-- <asp:BoundField DataField="siglaDef" HeaderText="DEF">
                                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="PLANO">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfoPlano" ImageUrl="/Library/IMG/Gestor_ImgInformacao.png"
                                                    ToolTip="Visualizar maiores informações sobre o Plano" Style="margin: 0 0 0 0 !important;"
                                                    OnClick="imgInfoPlano_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QTDE_V" HeaderText="QAP">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NU_REGIS" HeaderText="Nº CONTROLE">
                                            <ItemStyle HorizontalAlign="Center" Width="64px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                                            <ItemStyle HorizontalAlign="Left" Width="260px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OBSERVAÇÃO">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtObservacao" Style="margin: 2px 0px 2px 0px !important;
                                                    width: 70px; height: 26px;" TextMode="MultiLine" Text='<%# Eval("OBSERVACAO") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="STATUS" HeaderText="STATUS">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="text-align: center; margin: 10px 0 0 15px">
                    <ul>
                        <li class="liTituloGrid" style="width: 868px; height: 20px !important; margin-right: 0px;
                            background-color: #FFEC8B; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        PROFISSIONAIS DE SAÚDE</label>
                                </li>
                                <li style="margin-left: 23px; float: right; margin-top:2px;">
                                    <ul class="ulPer">
                                        <li>
                                            <asp:Label runat="server" ID="Label5">
                                                Classif Profissional</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlClassProfi" Width="120px">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="Label6">
                                                Nome</asp:Label>
                                            <asp:TextBox runat="server" ID="txtNome" Width="220px"></asp:TextBox>
                                        </li>
                                        <li style="margin: 0px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqProfis" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqProfis_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <asp:HiddenField runat="server" ID="hidCoCol" />
                            <asp:HiddenField runat="server" ID="hidCoEmpCol" />
                            <div style="width: 866px; height: 151px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:GridView ID="grdProfissionais" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default; height: 30px !important;" AutoGenerateColumns="false" AllowPaging="false"
                                    GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhumm Profissional nesses parâmetros<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                                <asp:HiddenField ID="hidCoEmp" Value='<%# Eval("CO_EMP") %>' runat="server" />
                                                <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NO_COL_V" HeaderText="PROFISSIONAL">
                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SX" HeaderText="SX">
                                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ID" HeaderText="ID">
                                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NU_TEL_V" HeaderText="TELEFONE">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_ESPEC" HeaderText="ESPEC">
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLASS_PROFI" HeaderText="CLASS PROFI">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="QPE" HeaderText="QPE">
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TP_CONTR" HeaderText="TP CONTR">
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_TPCAL" HeaderText="TP PGTO">
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VL_SALAR_COL" HeaderText="R$ BASE">
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li style="margin-top:20px;">
                            <fieldset title="Opções para salvar status da análise prévia" style="padding: 8px;">
                                <legend>AÇÃO</legend>
                                <ul>
                                    <li id="li17" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                        margin-bottom: 2px; clear: none !important; width: 70px">
                                        <asp:LinkButton ID="lnkEmAnalise" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkEmAnalise_OnClick">
                                            <asp:Label runat="server" ID="Label32" Text="EM ANÁLISE" Style="margin-left: 4px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li id="li1" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                        clear: both !important; width: 70px">
                                        <asp:LinkButton ID="lnkEncaminhar" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkEncaminhar_OnClick">
                                            <asp:Label runat="server" ID="Label1" Text="ENCAMINHAR" Style="margin-left: 4px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                    <li id="li2" runat="server" class="liBtnAddA" style="margin-left: 0px !important;
                                        clear: both !important; width: 70px; margin-top: 18px;">
                                        <asp:LinkButton ID="lnkCancelar" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkCancelar_OnClick">
                                            <asp:Label runat="server" ID="Label2" Text="CANCELAR" Style="margin-left: 4px;"></asp:Label>
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowInfosPaciente" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowInfosPlano" style="display: none; height: 305px !important;" />
        </li>
    </ul>
    <script type="text/javascript">

        function AbreModalInfoPaciente() {
            $('#divLoadShowInfosPaciente').dialog({ autoopen: false, modal: true, width: 300, height: 150, resizable: false, title: "INFORMAÇÕES DO(A) PACIENTE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalInfoPlano() {
            $('#divLoadShowInfosPlano').dialog({ autoopen: false, modal: true, width: 300, height: 150, resizable: false, title: "INFORMAÇÕES DO PLANO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
    </script>
</asp:Content>
