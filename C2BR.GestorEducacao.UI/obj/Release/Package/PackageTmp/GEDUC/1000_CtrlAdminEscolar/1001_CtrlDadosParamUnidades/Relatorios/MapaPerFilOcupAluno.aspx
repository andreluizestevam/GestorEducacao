<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPerFilOcupAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.MapaPerFilOcupAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 225px; } /* Usado para definir o formulário ao centro */ 
        .liUnidade
        {
            margin-top: 5px;
            width: 225px;            
        }          
        .liAnoRefer { margin-top:5px; }        
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
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" CssClass="ddlAno" runat="server">           
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>