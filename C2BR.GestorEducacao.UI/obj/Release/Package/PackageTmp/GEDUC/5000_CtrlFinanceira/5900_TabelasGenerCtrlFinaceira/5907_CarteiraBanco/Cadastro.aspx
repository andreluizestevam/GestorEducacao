<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5907_CarteiraBanco.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados {width: 286px;}
        
        /*--> CSS LIs */
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
        .ddlBanco {margin-bottom:10px;width:160px;}
        .txtCodigoCarteira {width: 35px;}
        .txtDescricao {width: 290px;}
        
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
            <label for="txtCodigoCarteira" class="lblObrigatorio" title="Código da Carteira">Código</label>
            <asp:TextBox ID="txtCodigoCarteira" runat="server" CssClass="txtCodigoCarteira"
                ToolTip="Informe o Código da Carteira"
                MaxLength="6">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCodigoCarteira" CssClass="validatorField"
                ErrorMessage="Código da Carteira é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição da Carteira">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao"
                ToolTip="Informe a Descrição da Carteira"
                MaxLength="150"
                onkeyup="javascript:MaxLength(this, 150);">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao" CssClass="validatorField"
                ErrorMessage="Descrição da Carteira é requerida">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
    </script>
</asp:Content>
