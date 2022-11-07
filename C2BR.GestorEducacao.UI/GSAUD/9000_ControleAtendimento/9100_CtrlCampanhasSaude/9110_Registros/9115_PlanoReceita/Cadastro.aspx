<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9115_PlanoReceita.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 950px;
            margin: 30px 0 0 60px !important;
        }
        .ulDados li
        {
            margin: 0 0 10px 10px;
        }
        .input
        {
            height:13px;
        }
        .campoValor
        {
            width: 95px;
            text-align: right;
            font-weight: bold;
            font-size: 13px;
        }
         .campoValorDefault
        {
            width: 60px;
            text-align: right;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li>
                    <label title="Unidade da Campanha de Saúde">
                        Unidade</label>
                    <asp:DropDownList runat="server" ID="ddlUnidade" Width="260px" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                        AutoPostBack="true" ToolTip="Unidade da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Campanha de Saúde para a qual será(ão) feito(s) o(s) lançamento(s)"
                        class="lblObrigatorio">
                        Campanha</label>
                    <asp:DropDownList runat="server" ID="ddlCampanha" Width="290px" OnSelectedIndexChanged="ddlCampanha_OnSelectedIndexChanged"
                        AutoPostBack="true" ToolTip="Campanha de Saúde para a qual será(ão) feito(s) o(s) lançamento(s)">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="clear: both; float: left; width: 200px; margin-left: 0px;">
            <ul>
                <asp:UpdatePanel ID="updTipoLanc" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <li>
                            <label title="Tipo do Lançamento(Receita/Despesa)" class="lblObrigatorio">
                                Tipo de Lançamento</label>
                            <asp:DropDownList runat="server" ID="ddlTipoLancamento" Width="120px" OnSelectedIndexChanged="ddlTipoLancamento_OnSelectedIndexChanged"
                                AutoPostBack="true" ToolTip="Tipo do Lançamento(Receita/Despesa)">
                                <asp:ListItem Value="C" Text="Receita"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Despesa"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both">
                            <label title="Histórico do Tipo de Lançamento" class="lblObrigatorio">
                                Histórico</label>
                            <asp:DropDownList runat="server" ID="ddlHistorico" Width="170px" ToolTip="Histórico do Tipo de Lançamento">
                            </asp:DropDownList>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li style="clear: both">
                    <label>
                        Data Lncto</label>
                    <asp:TextBox runat="server" ID="txtDtLanct" CssClass="campoData"></asp:TextBox>
                </li>
                <li>
                    <label title="Valor da Movimentação de Receita/Despesa" class="lblObrigatorio">
                        Valor R$</label>
                    <asp:TextBox runat="server" ID="txtValor" CssClass="campoValorDefault" ToolTip="Valor da Movimentação de Receita/Despesa">
                    </asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -9px;">
                    <label>
                        Nº Docto</label>
                    <asp:TextBox runat="server" ID="txtNuDocto" Width="160px" MaxLength="40"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -9px;">
                    <label>
                        Nº Contrato</label>
                    <asp:TextBox runat="server" ID="txtNuContrato" Width="120px" MaxLength="40"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -9px;">
                    <label>
                        Data Docto</label>
                    <asp:TextBox runat="server" ID="txtDtDocto" CssClass="campoData"></asp:TextBox>
                </li>
                <asp:UpdatePanel ID="updCNPJ" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <li style="clear: both; margin-top: -10px;">
                            <label>
                                Tipo Pessoa</label>
                            <asp:DropDownList runat="server" ID="ddlTipoPessoa" Width="60px" ToolTip="Tipo de Pessoa(Física ou Jurídica) para filtro dos fornecedores"
                                OnSelectedIndexChanged="ddlTipoPessoa_OnSelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Jurídica" Value="J"></asp:ListItem>
                                <asp:ListItem Text="Física" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: -10px;" runat="server" id="liCNPJ">
                            <label title="Pesquise pelo CNPJ do Fornecedor">
                                CNPJ</label>
                            <asp:TextBox runat="server" ID="txtCPNJ" Width="97px" CssClass="campoCNPJ" ToolTip="Pesquise pelo CNPJ do Fornecedor"></asp:TextBox>
                        </li>
                        <li style="margin-top: -10px;" runat="server" id="liCPF" visible="false">
                            <label title="Pesquise pelo CPF do Fornecedor">
                                CPF</label>
                            <asp:TextBox runat="server" ID="txtCPF" Width="70px" CssClass="campoCPF" ToolTip="Pesquise pelo CPF do Fornecedor"></asp:TextBox>
                        </li>
                        <li style="margin-top: 0px; margin-left: 3px;">
                            <asp:ImageButton ID="imgPesq" OnClick="imgPesq_OnClick" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                ToolTip="Pesquisa por um determinado Fornecedor com o CNPJ correspondente ao informado" />
                        </li>
                        <li style="margin-top: -6px; clear: both">
                            <label title="Fornecedor origem da Receita/Despesa" class="lblObrigatorio">
                                Nome Fornecedor</label>
                            <asp:DropDownList runat="server" ID="ddlFornecedor" Width="185px" ToolTip="Fornecedor origem da Receita/Despesa">
                            </asp:DropDownList>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
        </li>
        <li>
            <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Always">
                <ContentTemplate>
                <div style="width: 662px; text-align: center; height: 17px; background-color: #B0E0E6;
                        margin: 0 0 3px 0px;">
                        <div style="float: none;">
                            <asp:Label ID="Label1" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                                vertical-align: middle; margin-left: 4px !important;">
                                    GRADE DE LANÇAMENTO DE RECEITAS / DESPESAS DE CAMPANHAS DE SAÚDE</asp:Label>
                        </div>
                    </div>
                    <div id="div3" runat="server" class="divGridData" style="height: 315px; width: 660px;
                        border: 1px solid #ccc; overflow-y: scroll">
                        <asp:GridView ID="grdCampSaude" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 20px;" AutoGenerateColumns="false" ToolTip="Apresenta o planejamento orçamentário da Campanha de Saúde selecionada">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro financeiro para a Campanha de Saúde selecionada.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="0px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidTipo" Value='<%# Eval("TIPO") %>' runat="server" />
                                        <asp:HiddenField ID="hidValor" Value='<%# Eval("VALOR") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DATA_V" HeaderText="LANCTO">
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATA_DOCTO_V" HeaderText="DT DOCTO">
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_V" HeaderText="TIPO">
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_HISTORICO" HeaderText="HISTÓRICO">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ORIGEM" HeaderText="ORIGEM">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_DOC_V" HeaderText="Nº DOCTO">
                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblValorPositivo" Text='<%# Eval("VALOR_V") %>' Style="color: Blue"
                                            Visible='<%# Eval("SW_VALOR_POSITIVO") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblValorNegativo" Text='<%# Eval("VALOR_V") %>' Style="color: Red"
                                            Visible='<%# Eval("SW_VALOR_NEGATIVO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <asp:UpdatePanel ID="updSaldo" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <li style="margin: -5px 0 0 759px !important;">
                    <label title="Saldo total da Campanha Selecionada" style="font-weight:bold">
                        Saldo R$</label>
                    <asp:TextBox runat="server" ID="txtSaldo" ReadOnly="true" CssClass="campoValor" ToolTip="Saldo total da Campanha Selecionada"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtSaldoP" ReadOnly="true" CssClass="campoValor"
                        ToolTip="Saldo total da 
Campanha Selecionada" Visible="false" Style="background-color: #87CEEB"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtSaldoN" ReadOnly="true" CssClass="campoValor"
                        ToolTip="Saldo total da Campanha Selecionada" Visible="false" Style="background-color: #FF7F50"></asp:TextBox>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            CarregaCss();
            $(".campoValorDefault").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoCNPJ").mask("99.999.999/9999-99");
            $(".campoCPF").mask("999.999.999-99");
        });

        function CarregaCss() {
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
            $(".campoCPF").mask("999.999.999-99");
            $(".campoCNPJ").mask("99.999.999/9999-99");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            CarregaCss();
        });
    </script>
</asp:Content>
