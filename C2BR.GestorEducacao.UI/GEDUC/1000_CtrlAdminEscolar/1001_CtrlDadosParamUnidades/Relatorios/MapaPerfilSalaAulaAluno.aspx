<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPerfilSalaAulaAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.MapaPerfilSalaAulaAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 310px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 225px;            
        }       
        .liTipoSala
        {
        	display:inline;
        	margin-left: 5px;
        	margin-top: 5px;
        }
        .ddlTpSala { width: 70px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liTipoSala">
            <label class="lblObrigatorio" title="Tipo de Sala">
                Tipo de Sala</label>               
            <asp:DropDownList ID="ddlTpSala" ToolTip="Selecione o Tipo de Sala" CssClass="ddlTpSala" runat="server">    
                <asp:ListItem Selected="true" Value="T">Todos</asp:ListItem>       
                <asp:ListItem Value="A">Aula</asp:ListItem>       
                <asp:ListItem Value="L">Laboratório</asp:ListItem>       
                <asp:ListItem Value="E">Estudo</asp:ListItem>       
                <asp:ListItem Value="M">Monitoria</asp:ListItem>       
                <asp:ListItem Value="O">Outros</asp:ListItem>       
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlTpSala" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpSala" Text="*" 
            ErrorMessage="Campo Tipo de Sala é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>