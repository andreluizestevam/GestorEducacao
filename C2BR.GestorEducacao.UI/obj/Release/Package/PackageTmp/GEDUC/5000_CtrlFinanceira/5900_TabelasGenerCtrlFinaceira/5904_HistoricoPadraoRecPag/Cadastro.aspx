<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5904_HistoricoPadraoRecPag.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 240px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .txtDescricao { width: 200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o" class="lblObrigatorio labelPixel">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o" runat="server" CssClass="txtDescricao"
                MaxLength="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField" Display="Dynamic"
                runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descri��o deve ter no m�ximo 50 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descri��o deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTipo" title="Tipo" class="lblObrigatorio labelPixel">
                Tipo</label>
            <asp:DropDownList ID="txtTipo" ToolTip="Selecione o Tipo" runat="server">
                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Cr�dito" Value="C"></asp:ListItem>
                <asp:ListItem Text="D�bito" Value="D"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server"
                ControlToValidate="txtTipo" ErrorMessage="Tipo deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
