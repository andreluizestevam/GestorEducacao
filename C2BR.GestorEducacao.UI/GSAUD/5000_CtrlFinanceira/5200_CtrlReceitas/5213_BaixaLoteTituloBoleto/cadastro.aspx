<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5200_CtrlReceitas._5213_BaixaLoteTituloBoleto.cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
        }
        .check label
        {
            display: inline;
        }
        .check input
        {
            margin-left: -5px;
        }
        .conteudoGeral
        {
            width: 100%;
            text-align: center;
        }
        .conteudoGeral ul
        {
            width: 900px;
        }
        .conteudoGeral ul li
        {
            margin-left: 10px;
            margin-bottom: 15px;
            text-align: left;
        }
        .conteudoGeral ul .novaLinha
        {
            clear: both;
        }
        .conteudoGeral ul li .divGrid
        {
            width: 900px;
            height: 380px;
            overflow-y: scroll;
            border: 10x solid #CCCCCC;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 520px;
            margin-top: 8px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
        .larguraCombo
        {
            width: 130px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="conteudoGeral" class="conteudoGeral">
                <ul id="ulDados" class="ulDados">
                <li>
                <label for="ddlUnidade">
                            Unidade</label>
                        <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="larguraCombo" 
                        AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
                        </asp:DropDownList>
                </li>
                    <li>
                        <label for="ddlAgrupador">
                            Agrupador</label>
                        <asp:DropDownList ID="ddlAgrupador" runat="server" CssClass="larguraCombo" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlAgrupador_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlResponsavel">
                            Responsável</label>
                        <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="larguraCombo" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlResponsavel_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label runat="server" ID="lblNomeAluPac" CssClass="lblObrigatorio">
                            Paciente</asp:Label> <br />
                        <asp:DropDownList ID="ddlAluno" runat="server" CssClass="larguraCombo" AutoPostBack="True">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="txtPeriodo" title="Período">
                            Período</label>
                        <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                            runat="server"></asp:TextBox>
                        <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
                        <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                            runat="server"></asp:TextBox>
                    </li>
                    <li class="liBtnAdd">
                        <asp:LinkButton ID="btnGerar" runat="server" class="btnLabel" Width="50px" OnClick="btnGerar_Click">PESQUISAR</asp:LinkButton>
                    </li>
                    <li id="liGrid" class="novaLinha" runat="server">
                        <div id="divGrid" class="divGrid" runat="server">
                            <asp:GridView ID="grdTitulos" runat="server" AutoGenerateColumns="False" CssClass="grdBusca"
                                Width="880px" EmptyDataText="NENHUM TÍTULO ENCONTRADO.">
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbMarcar" runat="server" AutoPostBack="True" Checked='<%# bind("marcar") %>'
                                                CssClass="checkboxLabel" OnCheckedChanged="cbMarcar_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nº DOCUMENTO" DataField="numDocumento" />
                                    <asp:BoundField HeaderText="PA" DataField="numPa" />
                                    <asp:BoundField DataField="dataVencimento" HeaderText="DT VENC">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="agrupador" HeaderText="AGRUPADOR" />
                                    <asp:BoundField HeaderText="R$ VALOR" DataField="valor" DataFormatString="{0:n2}" />
                                    <asp:TemplateField HeaderText="MULTA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbMulta" runat="server" AutoPostBack="True" CssClass="campoMoeda"
                                                Enabled='<%# bind("marcar") %>' OnTextChanged="tbMulta_TextChanged" Text='<%# bind("multa") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JUROS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbJuros" runat="server" AutoPostBack="True" CssClass="campoMoeda"
                                                Enabled='<%# bind("marcar") %>' OnTextChanged="tbJuros_TextChanged" Text='<%# bind("juros") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESC EXT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbDesc" runat="server" AutoPostBack="True" CssClass="campoMoeda"
                                                Enabled='<%# bind("marcar") %>' OnTextChanged="tbDesc_TextChanged" Text='<%# bind("desc") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OUTROS(+)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbOutros" runat="server" AutoPostBack="True" CssClass="campoMoeda"
                                                Enabled='<%# bind("marcar") %>' OnTextChanged="tbOutros_TextChanged" Text='<%# bind("outros") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="R$ TOTAL" DataField="total" DataFormatString="{0:n2}" />
                                    <asp:TemplateField HeaderText="DATA RECEB">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbData" runat="server" CssClass="campoData" Enabled='<%# bind("marcar") %>'
                                                Text='<%# bind("data") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIPO RECEB">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTipos" runat="server" AutoPostBack="True" DataSource='<%# bind("comboTipo") %>'
                                                DataTextField="Text" DataValueField="Value" 
                                                Enabled='<%# bind("marcar") %>' 
                                                SelectedValue='<%# bind("tipo") %>' 
                                                onselectedindexchanged="ddlTipos_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                <RowStyle CssClass="rowStyle" />
                            </asp:GridView>
                        </div>
                    </li>
                    </li> </li>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $("input.campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        });
    </script>
</asp:Content>
