<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5903_TipoRecebimentoFinanceiro.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtSigla { width: 50px; }
        .txtDescricao { width: 200px; }
        .ddlClasMovim { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">      
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o" class="lblObrigatorio">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o" runat="server" CssClass="txtDescricao" MaxLength="80"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descri��o deve ter no m�ximo 80 caracteres" Text="*" ValidationExpression="^(.|\s){1,80}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descri��o deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" CssClass="txtSigla" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter no m�ximo 3 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipo" title="Tipo" class="lblObrigatorio">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" runat="server" CssClass="campoDescricao" >
                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                <asp:ListItem Text="Cart�o de Cr�dito" Value="V"></asp:ListItem>
                <asp:ListItem Text="Cart�o de D�bito" Value="D"></asp:ListItem>
                <asp:ListItem Text="Cheque" Value="C"></asp:ListItem>
                <asp:ListItem Text="Especie" Value="E"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>           
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField" runat="server" ControlToValidate="ddlTipo"
                ErrorMessage="Tipo deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
         </li>
         <li class="liClear" style="margin-top: 10px;">
            <label for="ddlTipo" title="Tipo" class="lblObrigatorio">
                Classifica��o Movimenta��o</label>
            <asp:DropDownList ID="ddlClasMovim" ToolTip="Selecione o Tipo de Classifica��o do Movimento" runat="server" CssClass="ddlClasMovim" >                
                <asp:ListItem Text="Cr�dito" Value="C"></asp:ListItem>
                <asp:ListItem Text="D�bito" Value="D"></asp:ListItem>
                <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField" runat="server" ControlToValidate="ddlClasMovim"
                ErrorMessage="Tipo de Classifica��o do Movimento deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
         </li>
    </ul>
</asp:Content>
