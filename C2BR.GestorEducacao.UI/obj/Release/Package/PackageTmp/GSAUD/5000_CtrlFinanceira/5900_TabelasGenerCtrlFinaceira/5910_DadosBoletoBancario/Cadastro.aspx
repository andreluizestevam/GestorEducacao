<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5910_DadosBoletoBancario.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 415px;
        }
        *
        {
            margin-bottom: 0px !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px !important;
        }
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .ddlBanco
        {
            width: 160px;
        }
        .ddlAgencia
        {
            width: 155px;
        }
        .ddlConta
        {
            text-align: right;
            width: 68px;
        }
        .ddlCarteira
        {
            width: 290px;
        }
        .txtInstrucoes
        {
            width: 397px;
        }
        .txtCodigoCedente
        {
            text-align: right;
            width: 75px;
        }
        .txtNumeroConvenio
        {
            text-align: right;
            width: 56px;
        }
        .ddlTipoTaxaBoleto
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlBanco" class="lblObrigatorio" title="Banco">
                Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco" ToolTip="Selecione o Banco"
                OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBanco" CssClass="validatorField"
                ErrorMessage="Banco é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlAgencia" class="lblObrigatorio" title="Agência">
                Agência</label>
            <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia" ToolTip="Selecione a Agência"
                OnSelectedIndexChanged="ddlAgencia_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAgencia" CssClass="validatorField"
                ErrorMessage="Agência é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlConta" class="lblObrigatorio" title="Conta">
                Conta</label>
            <asp:DropDownList ID="ddlConta" runat="server" CssClass="ddlConta" ToolTip="Selecione a Conta">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlConta" CssClass="validatorField"
                ErrorMessage="Conta é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlCarteira" class="lblObrigatorio" title="Carteira">
                Carteira</label>
            <asp:DropDownList ID="ddlCarteira" runat="server" CssClass="ddlCarteira" ToolTip="Selecione a Carteira">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCarteira" CssClass="validatorField"
                ErrorMessage="Carteira é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipoTaxaBoleto" class="lblObrigatorio" title="Tipo de Taxa do Boleto">
                Tipo</label>
            <asp:DropDownList ID="ddlTipoTaxaBoleto" runat="server" CssClass="ddlTipoTaxaBoleto"
                ToolTip="Selecione o Tipo de Taxa do Boleto">
                <asp:ListItem Value="C">Consulta</asp:ListItem>
                <asp:ListItem Value="P">Procedimentos</asp:ListItem>
                <asp:ListItem Value="S">Serviços</asp:ListItem>
                <asp:ListItem Value="E">Exames</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoTaxaBoleto"
                CssClass="validatorField" ErrorMessage="Tipo de Taxa do Boleto é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtCodigoCedente" class="lblObrigatorio" title="Código do Cedente">
                Código Cedente</label>
            <asp:TextBox ID="txtCodigoCedente" runat="server" CssClass="txtCodigoCedente" ToolTip="Informe o Código do Cedente"
                MaxLength="10">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigoCedente" CssClass="validatorField"
                ErrorMessage="Cedente é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNumeroConvenio" class="lblObrigatorio" title="Número do Convênio">
                N° Convênio</label>
            <asp:TextBox ID="txtNumeroConvenio" runat="server" CssClass="txtNumeroConvenio" ToolTip="Informe o Número do Convênio"
                MaxLength="7">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumeroConvenio"
                CssClass="validatorField" ErrorMessage="Número do Convênio é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtInstrucoesL1" title="Instruções impressas na primeira linha do Boleto">
                Instruções - Linha 1</label>
            <asp:TextBox ID="txtInstrucoesL1" runat="server" MaxLength="120" CssClass="txtInstrucoes"
                ToolTip="Informe as Instruções da Linha 1">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtInstrucoesL2" title="Instruções impressas na segunda linha do Boleto">
                Instruções - Linha 2</label>
            <asp:TextBox ID="txtInstrucoesL2" runat="server" MaxLength="120" CssClass="txtInstrucoes"
                ToolTip="Informe as Instruções da Linha 2">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtInstrucoesL3" title="Instruções impressas na terceira linha do Boleto">
                Instruções - Linha 3</label>
            <asp:TextBox ID="txtInstrucoesL3" runat="server" MaxLength="120" CssClass="txtInstrucoes"
                ToolTip="Informe as Instruções da Linha 3">
            </asp:TextBox>
        </li>
        <li style="clear: none"><span style="float: left">
            <asp:CheckBox ID="chkTxBolCli" AutoPostBack="True" OnCheckedChanged="chkTxBolCliChanged_CheckedChanged"
                runat="server"></asp:CheckBox></span>
            <label style="width: 170px">
                Taxa de Boleto ao Cliente</label>
        </li>
        <li>
            <label style="float: left">
                R$</label>
            <span style="float: left">
                <asp:TextBox ID="txtVlBolCLi" runat="server" Width="60px" MaxLength="5" CssClass="txtInstrucoes campoMoeda"
                    ToolTip="Informe o Valor de Taxa do Boleto ao Cliente"></asp:TextBox>
            </span></li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {

            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
