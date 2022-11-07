<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DemonAlunosPorEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.DemonAlunosPorEscola" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }        
        .liAnoRefer { margin-top:5px; }
        .liSituMatricula
        {
        	clear:both;
        	margin-top:5px;
        }  
        .ddlSituMatricula { width:75px; }
   </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">   
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" CssClass="ddlAno" runat="server">           
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
        <li class="liSituMatricula">
            <label class="lblObrigatorio" title="Tipo de Ensino">
                Tipo de Ensino</label>               
            <asp:DropDownList ID="ddlTipoEnsino" ToolTip="Selecione o Tipo do Ensino" CssClass="ddlTipoEnsino" runat="server">           
                <asp:ListItem Value="I" Selected="True">Ensino Infantil</asp:ListItem>
                <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
                <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSituMatricula" Text="*" 
            ErrorMessage="Campo Situação da Matrícula é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
        <li class="liSituMatricula">
            <label class="lblObrigatorio" title="Situação da Matrícula">
                Situação da Matrícula</label>               
            <asp:DropDownList ID="ddlSituMatricula" ToolTip="Selecione a Situação da Matrícula" CssClass="ddlSituMatricula" runat="server">           
                <asp:ListItem Selected="True" Value="O">Todas</asp:ListItem>
                <asp:ListItem Value="A">Em aberto</asp:ListItem>
                <asp:ListItem Value="T">Transferido</asp:ListItem>
                <asp:ListItem Value="F">Finalizado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlSituMatricula" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSituMatricula" Text="*" 
            ErrorMessage="Campo Situação da Matrícula é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
