<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="EvolucReceitaDespesa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.ControleAdministrativo.Planejamento.Relatorios.EvolucReceitaDespesa" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados { width: 300px; }
        .liUnidade{margin-top: 5px;width: 300px;}
        .liAnoRefer{margin-top: 5px; clear: both;}     
        .ddlAnoRefer{ width: 45px;}             
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li class="liAnoRefer">
            <label class="lblObrigatorio" for="txtUnidade">
                Ano de Referência</label>
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAnoRefer" runat="server" ToolTip="Selecione o Ano de Referência">            
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano de Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>       
        </ContentTemplate>
        </asp:UpdatePanel>             
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
