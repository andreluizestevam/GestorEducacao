<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3013_CadastramentoCoordDepto.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 343px; }  
        .ulDados li label{ margin-bottom: 1px; } 
        
        /*--> CSS LIs */ 
        .ulDados li{ margin-left: 5px; }
        .liClear { clear: both; }  
        
        /*--> CSS DADOS */        
        .ddlDepartamento{ margin-bottom: 10px;}
        .txtDescricao{ width: 265px;}
        .txtSigla{ width: 55px; text-transform: uppercase;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlDepartamento" class="lblObrigatorio" title="Departamento">Departamento</label>
            <asp:DropDownList ID="ddlDepartamento" class="ddlDepartamento campoDptoCurso" runat="server" Enabled="false"
                ToolTip="Selecione o Departamento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"  ControlToValidate="ddlDepartamento" ErrorMessage="Departamento deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Coordenação">Coordenação</label>
            <asp:TextBox ID="txtDescricao" runat="server" class="txtDescricao" MaxLength="40"
                ToolTip="Informe a Coordenação">
            </asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Coordenação deve ter no máximo 40 caracteres" Text="*" ValidationExpression="^(.|\s){1,40}$"></asp:RegularExpressionValidator>          
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="txtDescricao" 
                ErrorMessage="Coordenação deve ser informada">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" class="txtSigla" MaxLength="6"
                ToolTip="Informe a Sigla">
            </asp:TextBox>
            <asp:RegularExpressionValidator ID="revSigla" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ter 6 caracteres" Text="*" ValidationExpression="^(.|\s){1,6}$"></asp:RegularExpressionValidator>          
            <asp:RequiredFieldValidator ID="rfvSigla" runat="server" CssClass="validatorField"  ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>