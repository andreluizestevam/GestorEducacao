<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0912_CadastroCEP.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 370px; }        
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}  
        
        /*--> CSS Dados */
        .txtEndereco {width:250px;}
        .txtCoordenada {width:68px;}
        .tipoCoordenada {width:55px;}
        .tipoLogradouro {width:96px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCep" title="CEP" class="lblObrigatorio">CEP</label>
            <asp:TextBox ID="txtCep" CssClass="campoCep" ToolTip="Informe o CEP" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtCep"
                ErrorMessage="CEP deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label for="ddlTipoLogradouro" title="Tipo de Logradouro" class="lblObrigatorio">Tipo Logradouro</label>
            <asp:DropDownList ID="ddlTipoLogradouro"
                ToolTip="Selecione o Tipo de Logradouro" runat="server" CssClass="tipoLogradouro"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="ddlTipoLogradouro"
                ErrorMessage="Tipo de Logradouro deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtEndereco" title="Endereço" class="lblObrigatorio">Endereço</label>
            <asp:TextBox ID="txtEndereco" ToolTip="Informe o Endereço" runat="server" MaxLength="60" CssClass="txtEndereco">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField" runat="server" ControlToValidate="txtEndereco"
                ErrorMessage="Endereço deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label for="ddlUf" title="UF" class="lblObrigatorio">UF</label>
            <asp:DropDownList ID="ddlUf" ToolTip="Selecione uma Cidade"
                runat="server" CssClass="campoUf" onselectedindexchanged="ddlUf_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField" runat="server" ControlToValidate="ddlUf"
                ErrorMessage="UF deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li id="liCidade">
            <label for="ddlCidade" title="Cidade" class="lblObrigatorio">Cidade</label>
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" 
                runat="server" CssClass="campoCidade" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"  runat="server" ControlToValidate="ddlCidade"
                ErrorMessage="Cidade deve ser informada"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlBairro" title="Bairro" class="lblObrigatorio">Bairro</label>
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione o Bairro" runat="server" 
                CssClass="campoBairro">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField" runat="server" ControlToValidate="ddlBairro"
                ErrorMessage="Bairro deve ser informado"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label for="txtLatitude" title="Latitude">Latitude</label>
            <asp:TextBox ID="txtLatitude" ToolTip="Informe a Latitude" runat="server"
                CssClass="txtCoordenada"></asp:TextBox>
        </li>
        <li>
            <label for="ddlTipoLatitude" title="Tipo Latitude">Tipo Latitude</label>
            <asp:DropDownList ID="ddlTipoLatitude" ToolTip="Selecione o Tipo de Latitude" runat="server"
                CssClass="tipoCoordenada">
                <asp:ListItem Value="N">Norte</asp:ListItem>
                <asp:ListItem Value="S">Sul</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear:both;">
            <label for="txtLongitude" title="Longitude">Longitude</label>
            <asp:TextBox ID="txtLongitude" ToolTip="Informe a Longitude" runat="server"
                CssClass="txtCoordenada">
            </asp:TextBox>
        </li>
        <li>
            <label for="ddlTipoLongitude" title="Tipo Longitude">Tipo Longitude</label>
            <asp:DropDownList ID="ddlTipoLongitude" ToolTip="Selecione o Tipo de Longitude" runat="server"
                CssClass="tipoCoordenada">
                <asp:ListItem Value="O">Oeste</asp:ListItem>
                <asp:ListItem Value="L">Leste</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoCep").mask("99999-999");
            $(".txtCoordenada").maskMoney({ symbol: "", decimal: ",", thousands: "" });
        });
    </script>
</asp:Content>
