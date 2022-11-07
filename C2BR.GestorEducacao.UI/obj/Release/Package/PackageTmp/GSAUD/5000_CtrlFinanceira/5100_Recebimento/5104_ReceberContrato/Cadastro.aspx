<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5104_ReceberContrato.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .fixedHeader
        {
            position:absolute;
            font-weight:bold;
        }
        
        input[type="text"]
        {
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            margin-bottom: 4px;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        .liBtnEmt
        {
            background-color: #04B486;
            border: 1px solid #D2DFD1;
            margin-top: 9px;
            padding: 0px 8px 1px 8px;
            float:right;
            height: 11px;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        input[type='text']
        {
            margin-bottom: 4px;
        }
        label
        {
            margin-bottom: 1px;
        }
        .ulDados
        {
            width: 1003px;
            margin-top: -15px !important;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display:inline;
            margin-left:-4px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="width: 780px; height:30px; margin-top: 0px !important;">
        <li>
            <asp:HiddenField ID="hidAgendReceb" runat="server" />
            <asp:HiddenField ID="hidAgendAvalReceb" runat="server" />
            <asp:HiddenField ID="hidIdRecebimento" runat="server" />
            <label title="Paciente" class="lblObrigatorio">Paciente</label>
            <asp:DropDownList ID="drpPacienteReceb" Visible="false" OnSelectedIndexChanged="drpPacienteReceb_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="230px" />
            <asp:TextBox ID="txtNomePacPesq" Width="230px" ValidationGroup="pesqPac" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPacienteReceb" CssClass="validatorField"
                ErrorMessage="O campo data é requerido" ControlToValidate="drpPacienteReceb"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 11px; margin-left: -4px;">
            <asp:ImageButton ID="imgbPesqPacNome" ValidationGroup="pesqPac" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
        </li>
        <li>
            <label title="Responsável">Responsável</label>
            <asp:TextBox runat="server" ID="txtResponsavelReceb" Width="165px" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label title="Número do RAP">Nº Documento</label>
            <asp:TextBox ID="txtRAP" Width="87px" Enabled="false" runat="server" />
        </li>
        <li>
            <label title="Valor Total" class="lblObrigatorio">Valor Total</label>
            <asp:TextBox runat="server" CssClass="campoDin" ID="txtVlrReceb" Width="50px" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvVlrReceb" CssClass="validatorField" ClientIDMode="Static"
                ErrorMessage="O campo data é requerido" ControlToValidate="txtVlrReceb"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <!--Forma Pagamento-->
    <ul class="ulDados" style="width: 955px; height:100px; margin-top: 5px !important;">
        <li  style="width: 100%; height: 19px !important; background-color: #9AFF9A; text-align: center; margin-bottom:15px;">
            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">FORMA DE RECEBIMENTO</label>
        </li>
        <li style="clear:both; padding-right:28px; padding-left:0px; border-right: 1px solid #CCCCCC;">
            <label title="Dinheiro" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkDinheiro" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Dinheiro
            </label>
            <asp:TextBox runat="server" CssClass="campoDin" ID="txtDinheiro" Width="50px" ToolTip="Valor Total em Dinheiro" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:23px; border-right: 1px solid #CCCCCC;">
            <label title="Cheque" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkCheque" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Cheque
            </label>
            <asp:TextBox runat="server" ID="txtCheque" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cheque" ClientIDMode="Static" /> /
            <asp:TextBox runat="server" ID="txtParcelasCheque" MaxLength="3" Width="20px" ToolTip="Quantidade de Cheques" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:23px; border-right: 1px solid #CCCCCC;">
            <label title="Cartão de Débito" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkDebito" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Débito
            </label>
            <asp:TextBox runat="server" ID="txtDebito" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cartão de Débito" ClientIDMode="Static" /> /
            <asp:TextBox runat="server" ID="txtParcelasDebito" MaxLength="3" Width="20px" ToolTip="Quantidade de Cartões" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:23px; border-right: 1px solid #CCCCCC;">
            <label title="Cartão de Crédito" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkCredito" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Crédito
            </label>
            <asp:TextBox runat="server" ID="txtCredito" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Cartão de Crédito" ClientIDMode="Static" /> /
            <asp:TextBox runat="server" ID="txtParcelasCredito" MaxLength="3" Width="20px" ToolTip="Quantidade de Parcelas" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:23px; border-right: 1px solid #CCCCCC;">
            <label title="Transferência" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkTransferencia" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Transfer
            </label>
            <asp:TextBox runat="server" ID="txtTransferencia" CssClass="campoDin" Width="50px" ToolTip="Valor Total Transferido" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:23px; border-right: 1px solid #CCCCCC;">
            <label title="Deposíto" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkDeposito" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Deposíto
            </label>
            <asp:TextBox runat="server" ID="txtDeposito" CssClass="campoDin" Width="50px" ToolTip="Valor Total Depositado" ClientIDMode="Static" />
        </li>
        <li style="padding-right:28px; padding-left:28px; border-right: 1px solid #CCCCCC;">
            <label title="Boleto" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkBoleto" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Boleto Bancário
            </label>
            <asp:TextBox runat="server" ID="txtBoleto" CssClass="campoDin" Width="50px" ToolTip="Valor Total em Boleto Bancário" ClientIDMode="Static" /> /
            <asp:TextBox runat="server" ID="txtParcelasBoleto" MaxLength="3" Width="20px" ToolTip="Quantidade de Boletos" ClientIDMode="Static" />
        </li>
        <li style="padding-left:25px;">
            <label title="Outros" style="margin-bottom:2px;">
                <asp:CheckBox ID="chkOutros" style="margin:0 -5px 0 -5px;" runat="server" ClientIDMode="Static" />Outros
            </label>
            <asp:TextBox runat="server" ID="txtOutros" CssClass="campoDin" Width="50px" ToolTip="Valor Recebido em Outras Formas" ClientIDMode="Static" />
        </li>
        <li style="clear:both; margin-top: 10px;">
            Observações <asp:TextBox ID="txtObsReceb" Width="795" runat="server" />
        </li>
        <li class="liBtnEmt">
            <asp:LinkButton ID="lnkbEmitirRecibo" runat="server" Text="EMITIR RECIBO" ForeColor="White" OnClick="lnkbEmitirRecibo_Click" ToolTip="Imprimir o recibo do recebimento">
            </asp:LinkButton>
        </li>
    </ul>
    <!--Pagamento Debito-->
    <ul class="ulDados" style="clear:both; width: 955px; height:100px; margin-top: 10px !important;">
        <li style="background-color: #ADD8E6; width: 955px; margin-bottom:3px; text-align: center;">
            <label style="color:Black;">DETALHES DO RECEBIMENTO DE CARTÕES E CHEQUES</label>
        </li>
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px;
                    background-color: #EEEEE0; text-align: center; float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                            CARTÃO DE DÉBITO</label>
                </li>
                <li id="li12" runat="server" title="Clique para adicionar um cartão de débito"
                    class="liBtnAddA" style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cartão de Débito" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCartaoDebito" runat="server" OnClick="lnkAddCartaoDebito_OnClick" OnClientClick="if($('#chkDebito').attr('checked')){return true;}else{alert('Selecione a opção débito e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCartaoDebito" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical" ClientIDMode="Static"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cartão associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlBcoDebito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agência">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtAgenDebito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conta">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumContaDebito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="105px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumCartaoDebito" CssClass="campoNumCart" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitulDebito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Débito">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVlrDebito" CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº Autorização">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumAutoriDebito" MaxLength="50" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCartaoDebito" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cartão" OnClick="imgExcCartaoDebito_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <!--Pagamento Credito-->
    <ul class="ulDados" style="width: 955px; height:85px; margin-top: 10px !important;">
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px;
                    background-color: #EEEEE0; text-align: center; float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                            CARTÃO DE CRÉDITO</label>
                </li>
                <li id="li11" runat="server" title="Clique para adicionar um cartão de crédito"
                    class="liBtnAddA" style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cartão de Crédito" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCartaoCredito" runat="server" OnClick="lnkAddCartaoCredito_OnClick" OnClientClick="if($('#chkCredito').attr('checked')){return true;}else{alert('Selecione a opção crédito e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 60px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCartaoCredito" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical" ClientIDMode="Static"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cartão associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Bandeira">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlBandCredito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="105px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumCartaoCredito" CssClass="campoNumCart" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitulCredito" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento">
                            <ItemStyle HorizontalAlign="Center" Width="65px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVencimentoCredito" CssClass="campoVenc" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Crédito">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVlrCredito" CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº Autorização">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumAutoriCredito" MaxLength="50" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCartaoCredito" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cartão" OnClick="imgExcCartaoCredito_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <!--Pagamento Cheque-->
    <ul class="ulDados" style="width: 955px; height:113px; margin-top: 10px !important;">
        <li>
            <ul style="width: 955px;">
                <li style="height: 20px !important; width: 885px;
                    background-color: #EEEEE0; text-align: center; float: left;">
                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                            CHEQUE</label>
                </li>
                <li id="li13" runat="server" title="Clique para adicionar um cheque"
                    class="liBtnAddA" style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Cheque" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                        height="15px" width="15px" />
                    <asp:LinkButton ID="lnkAddCheque" runat="server" OnClick="lnkAddCheque_OnClick" OnClientClick="if($('#chkCheque').attr('checked')){return true;}else{alert('Selecione a opção cheque e informe o valor'); return false;}">Adicionar</asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div style="width: 953px; height: 81px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdCheque" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical" ClientIDMode="Static"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum cheque associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Banco">
                            <ItemStyle HorizontalAlign="Center" Width="75px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlBcoCheque" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agência">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtAgenCheque" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conta">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumContaCheque" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Número">
                            <ItemStyle HorizontalAlign="Center" Width="95px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNumCheque" MaxLength="30" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CPF">
                            <ItemStyle HorizontalAlign="Center" Width="83px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtCPFCheque" class="campoCpf" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Titular">
                            <ItemStyle HorizontalAlign="Center" Width="165px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitulCheque" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Cheque">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVlrCheque" CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento">
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtVencimentoCheque" class="campoData" Style="margin-left: -4px; margin-bottom:0px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EX">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgExcCheque" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                    ToolTip="Excluir Cheque" OnClick="imgExcCheque_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <!--Titulos-->
    <div id="divEncaminAtend" style="display: none; height: 100px !important; width: 270px">
        <asp:HiddenField ID="hidTitulo" runat="server" />

    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".nuQtde").mask("9?99");
            $(".campoVenc").mask("99/99");
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoNumCart").mask("9999.9999.9999.9999");

            if (!($("#chkDinheiro").attr("checked"))) {
                $("#txtDinheiro").enable(false);
            }
            if (!($("#chkCheque").attr("checked"))) {
                $("#txtCheque").enable(false);
                $("#txtParcelasCheque").enable(false);
            }
            if (!($("#chkDebito").attr("checked"))) {
                $("#txtDebito").enable(false);
                $("#txtParcelasDebito").enable(false);
            }
            if (!($("#chkCredito").attr("checked"))) {
                $("#txtCredito").enable(false);
                $("#txtParcelasCredito").enable(false);
            }
            if (!($("#chkTransferencia").attr("checked"))) {
                $("#txtTransferencia").enable(false);
            }
            if (!($("#chkDeposito").attr("checked"))) {
                $("#txtDeposito").enable(false);
            }
            if (!($("#chkBoleto").attr("checked"))) {
                $("#txtBoleto").enable(false);
                $("#txtParcelasBoleto").enable(false);
            }
            if (!($("#chkOutros").attr("checked"))) {
                $("#txtOutros").enable(false);
            }

            $("#chkDinheiro").click(function () {
                if ($("#chkDinheiro").attr("checked")) {
                    $("#txtDinheiro").enable(true);
                }
                else {
                    $("#txtDinheiro").enable(false);
                    $("#txtDinheiro").val("");
                }
            });

            $("#chkCheque").click(function () {
                if ($("#chkCheque").attr("checked")) {
                    $("#txtCheque").enable(true);
                    $("#txtParcelasCheque").enable(true);
                    $("#txtParcelasCheque").val("1");
                }
                else {
                    $("#txtCheque").enable(false);
                    $("#txtParcelasCheque").enable(false);
                    $("#txtCheque").val("");
                    $("#txtParcelasCheque").val("");
                }
            });

            $("#chkDebito").click(function () {
                if ($("#chkDebito").attr("checked")) {
                    $("#txtDebito").enable(true);
                    $("#txtParcelasDebito").enable(true);
                    $("#txtParcelasDebito").val("1");
                }
                else {
                    $("#txtDebito").enable(false);
                    $("#txtParcelasDebito").enable(false);
                    $("#txtDebito").val("");
                    $("#txtParcelasDebito").val("");
                }
            });

            $("#chkCredito").click(function () {
                if ($("#chkCredito").attr("checked")) {
                    $("#txtCredito").enable(true);
                    $("#txtParcelasCredito").enable(true);
                    $("#txtParcelasCredito").val("1");
                }
                else {
                    $("#txtCredito").enable(false);
                    $("#txtParcelasCredito").enable(false);
                    $("#txtCredito").val("");
                    $("#txtParcelasCredito").val("");
                }
            });

            $("#chkTransferencia").click(function () {
                if ($("#chkTransferencia").attr("checked")) {
                    $("#txtTransferencia").enable(true);
                }
                else {
                    $("#txtTransferencia").enable(false);
                    $("#txtTransferencia").val("");
                }
            });

            $("#chkDeposito").click(function () {
                if ($("#chkDeposito").attr("checked")) {
                    $("#txtDeposito").enable(true);
                }
                else {
                    $("#txtDeposito").enable(false);
                    $("#txtDeposito").val("");
                }
            });

            $("#chkBoleto").click(function () {
                if ($("#chkBoleto").attr("checked")) {
                    $("#txtBoleto").enable(true);
                    $("#txtParcelasBoleto").enable(true);
                    $("#txtParcelasBoleto").val("1");
                }
                else {
                    $("#txtBoleto").enable(false);
                    $("#txtParcelasBoleto").enable(false);
                    $("#txtBoleto").val("");
                    $("#txtParcelasBoleto").val("");
                }
            });

            $("#chkOutros").click(function () {
                if ($("#chkOutros").attr("checked")) {
                    $("#txtOutros").enable(true);
                }
                else {
                    $("#txtOutros").enable(false);
                    $("#txtOutros").val("");
                }
            });
        });
    </script>
</asp:Content>
