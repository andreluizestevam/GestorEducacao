<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3012_CadastramentoDeptoEnsino.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 240px; }
        .ulDados li label{ margin-bottom: 1px; }
        
        /*--> CSS LIs */
        .ulDados li{ margin-left: 5px; }        
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
        .txtSigla { width: 55px; text-transform: uppercase; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">   
        <li>
            <label for="txtDescricao" class="lblObrigatorio" title="Departamento">Departamento</label>
            <asp:TextBox ID="txtDescricao" class="campoDptoCurso" runat="server" MaxLength="60"
                ToolTip="Informe o Departamento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                CssClass="validatorField" ErrorMessage="Departamento deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" class="txtSigla" runat="server" MaxLength="6"
                ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSigla"
                CssClass="validatorField" ErrorMessage="Sigla deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
