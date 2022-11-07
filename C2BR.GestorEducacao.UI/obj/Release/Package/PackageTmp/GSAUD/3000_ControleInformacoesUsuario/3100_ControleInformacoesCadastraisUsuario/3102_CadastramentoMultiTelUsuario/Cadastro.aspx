<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3101_CadastramentoMultiTelUsuario.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .ulDados { width: 250px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liSituacao { clear: both; margin-top: -5px !important; }
        
        /*--> CSS DADOS */
        .ddlTipoTelefone { width: 100px; }
        .ddlSituacao { width:  75px; }                
        .labelPixel { margin-top: 5px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
        <li class="liClear">
            <label for="ddlAluno" class="lblObrigatorio labelPixel" title="Aluno">Usuário de Saúde</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Usuário" CssClass="campoNomePessoa" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAluno" 
                ErrorMessage="Aluno deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipoTelefone" class="lblObrigatorio labelPixel" title="Tipo de Telefone">Tipo de Telefone</label>
            <asp:DropDownList ID="ddlTipoTelefone" ToolTip="Selecione o Aluno" CssClass="ddlTipoTelefone" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoTelefone" 
                ErrorMessage="Tipo de Telefone deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTelefone" class="lblObrigatorio labelPixel" title="Telefone">Telefone</label>
            <asp:TextBox ID="txtTelefone" CssClass="campoTelefone" ToolTip="Informe o Telefone" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelefone" 
                ErrorMessage="Telefone deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liSituacao">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação">
                Situação
            </label>
            <asp:DropDownList ID="ddlSituacao" class="selectedRowStyle"  CssClass="ddlSituacao"
                runat="server" ToolTip="Informe a Situação">                
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoTelefone").mask("(99)9999-9999");
        });
    </script>
</asp:Content>
