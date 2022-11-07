<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1203_RegistroCursoFormacaoColaborador.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlColaborador" class="lblObrigatorio">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" class="campoNomePessoa" runat="server" ToolTip="Selecione o Colaborador"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvColaborador" runat="server" CssClass="validatorField"
            ControlToValidate="ddlColaborador" Text="*" 
            ErrorMessage="Colaborador é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
