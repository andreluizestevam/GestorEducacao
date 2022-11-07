<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5906_Agencia.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados {width: 254px;}
        
        /*--> CSS LIs */
        .liClear { clear: both; } 
        
        /*--> CSS DADOS */
        .ddlBanco {margin-bottom:10px;width:160px;}
        .telefone {width: 76px;}
        .agencia {text-align: right; width: 42px;}
        .txtDigitoAgencia {text-align: right; width: 20px;text-transform:uppercase;}
        .txtNomeAgencia {width: 140px;}
        .txtNomeGerenteAgencia {width: 160px;}
        .txtEmail {width: 160px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlBanco" class="lblObrigatorio" title="Banco">Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                ToolTip="Selecione o Banco">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBanco" CssClass="validatorField"
                ErrorMessage="Banco é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtCodigoAgencia" class="lblObrigatorio" title="Código da Agência">Agência/Dígito</label>
            <asp:TextBox ID="txtCodigoAgencia" runat="server" CssClass="agencia"
                ToolTip="Informe o Código da Agência"
                MaxLength="5">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigoAgencia" CssClass="validatorField"
                ErrorMessage="Agência é requerida">
            </asp:RequiredFieldValidator>
            <asp:TextBox ID="txtDigitoAgencia" runat="server" CssClass="txtDigitoAgencia"
                ToolTip="Informe o Dígito da Agência"
                MaxLength="1">
            </asp:TextBox>
            
        </li>
        <li>
            <label for="txtNomeAgencia" class="lblObrigatorio" title="Nome da Agência">Nome Agência</label>
            <asp:TextBox ID="txtNomeAgencia" runat="server" CssClass="txtNomeAgencia"
                ToolTip="Informe o Nome Agência"
                MaxLength="30">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNomeAgencia" CssClass="validatorField"
                ErrorMessage="Nome da Agência é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTelefoneAgencia" title="Telefone da Agência">Telefone Agência</label>
            <asp:TextBox ID="txtTelefoneAgencia" runat="server" CssClass="telefone"
                ToolTip="Informe o Telefone da Agência">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtFaxAgencia" title="Fax da Agência">Fax Agência</label>
            <asp:TextBox ID="txtFaxAgencia" runat="server" CssClass="telefone"
                ToolTip="Informe o Fax da Agência">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNomeGerenteAgencia" class="lblObrigatorio" title="Nome do Gerente da Agência">Nome Gerente</label>
            <asp:TextBox ID="txtNomeGerenteAgencia" runat="server" CssClass="txtNomeGerenteAgencia"
                ToolTip="Informe o Nome do Gerente da Agência"
                MaxLength="35">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNomeGerenteAgencia" CssClass="validatorField"
                ErrorMessage="Nome do Gerente requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtEmail" title="E-mail do Gerente">E-mail Gerente</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail"
                ToolTip="Informe o E-mail do Gerente"
                MaxLength="50">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtTelefoneGerenteAgencia" title="Telefone do Gerente da Agência">Telefone Gerente</label>
            <asp:TextBox ID="txtTelefoneGerenteAgencia" runat="server" CssClass="telefone"
                ToolTip="Informe o Telefone do Gerente da Agência">
            </asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".telefone").mask("(99) 9999-9999");
        });
    </script>
</asp:Content>
