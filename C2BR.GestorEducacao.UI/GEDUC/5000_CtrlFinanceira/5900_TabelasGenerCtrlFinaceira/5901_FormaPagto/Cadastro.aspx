<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5901_FormaPagto.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 255px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS DADOS */
        .txtDescricao { width: 200px; }
        .txtNumber { width: 60px; }
        .txtNumber1
        {
            width: 60px;
            text-align: right;
        }        
        .labelPixel { margin-bottom: 1px; }
        .ddlReceberValorEntrada { width: 45px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" dir="ltr" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" title="Descrição" class="lblObrigatorio labelPixel">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição" CssClass="txtDescricao" runat="server" MaxLength="40"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ter no máximo 40 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtParcela" title="Número de Parcelas">
                N° de</label>
            <label for="txtParcela" title="Número de Parcelas" style="margin-top: -4px" class="lblObrigatorio">
                Parcela</label>
            <asp:TextBox ID="txtNParcela" ToolTip="Informe o Número de Parcelas" runat="server" CssClass="txtNumber" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNParcela"
                ErrorMessage="Número de parcelas deve ter no máximo 4 caracteres" CssClass="validatorField"
                Text="*" ValidationExpression="^(.|\s){1,4}$" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNParcela"
                ErrorMessage="Número de parcelas deve ser informada" CssClass="validatorField"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="txtCorrecaoParcela" title="Porcentagem de Correção da Parcela">
                % Correção</label>
            <label for="txtCorrecaoParcela2" title="Porcentagem de Correção da Parcela" style="margin-top: -4px" class="lblObrigatorio">
                de Parcela</label>
            <asp:TextBox ID="txtCorrecaoParcela" ToolTip="Informe a Porcentagem de Correção da Parcela" runat="server" CssClass="txtNumber1" MaxLength="5"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="txtCorrecaoParcela" ErrorMessage="% Correção Parcela deve ter no máximo 5 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,5}$" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="txtCorrecaoParcela" ErrorMessage="% Correção Parcela deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="txtQtdDias" title="Quantidade de Dias">
                Qtde</label>
            <label for="txtQtdDias" title="Quantidade de Dias" style="margin-top: -4px" class="lblObrigatorio">
                de Dias</label>
            <asp:TextBox ID="txtQtdDias" ToolTip="Informe a Quantidade de Dias" runat="server" MaxLength="4" CssClass="txtNumber"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtQtdDias" ErrorMessage="Qtde Dias deve ter no máximo 4 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,4}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtQtdDias" ErrorMessage="Qtde Dias deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtReceberValorEntrada" title="Recebimento de Entrada">
                Recebimento</label>
            <label for="txtReceberValorEntrada2" title="Recebimento de Entrada" style="margin-top: -4px" class="lblObrigatorio">
                de Entrada</label>
            <asp:DropDownList ID="ddlReceberValorEntrada" CssClass="ddlReceberValorEntrada" ToolTip="Selecione se Houve o Recebimento de Entrada"  runat="server">
                <asp:ListItem Text="Sim" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Não" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumber1").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtNumber").mask("?9999");
        });
    </script>
</asp:Content>
