<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaCadIndivProfessor.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.FichaCadIndivProfessor" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .liAnoBase, .liFuncionarios,.liUnidade
        {
            margin-top: 5px;
            width: 200px;
        }        
        .liFuncionarios { clear: both; }     
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
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase">
                Ano Base</label>
            <asp:DropDownList ID="ddlAnoBase" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano Base">
            </asp:DropDownList>
        </li>        
        <li class="liFuncionarios">
            <label class="lblObrigatorio" for="txtFuncionarios">
                Funcionários</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Funcionário">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" CssClass="validatorField"
            ControlToValidate="ddlFuncionarios" Text="*" 
            ErrorMessage="Campo Funcionário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>     
        </ContentTemplate>
        </asp:UpdatePanel>   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
