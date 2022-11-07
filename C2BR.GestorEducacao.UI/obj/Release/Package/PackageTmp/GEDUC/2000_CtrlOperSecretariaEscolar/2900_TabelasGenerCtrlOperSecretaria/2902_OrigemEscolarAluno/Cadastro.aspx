<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2902_OrigemEscolarAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 245px; }       
        
        /*--> CSS LIs */
        .liClear { clear:both; } 
        
        /*--> CSS DADOS */
        .txtDescricao { width: 240px; }
        .txtSigla { width: 35px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">      
        <li>
            <label for="txtDescricao" title="Descrição da Origem do Aluno" class="lblObrigatorio">Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição da Origem do Aluno"  runat="server" MaxLength="60"></asp:TextBox>
           
            <asp:RegularExpressionValidator ID="revtxtDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ter no máximo 60 caracteres"
                ValidationExpression="^(.|\s){1,60}$" CssClass="validatorField">
            </asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="rfvTxtDE_ORIGEM" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada" 
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: -5px;">
            <label for="txtSigla" title="Sigla" class="lblObrigatorio">Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" MaxLength="4" CssClass="txtSigla"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtSIG_ORI_ALU" runat="server" ControlToValidate="txtSigla" 
                ErrorMessage="Sigla deve ter no máximo 4 caracteres" ValidationExpression="^(.|\s){1,4}$"
                CssClass="validatorField">
            </asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" 
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>