<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="MapaDistrMedic.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._6000_CtrlProcessosInternos._6200_CtrlOperMateriais._6250_LanctoEntradaSaidaItensEstoque._6259_Relatorios.MapaDistrMedic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
        }
        .ulDados li
        {
            margin:5px 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnid" Width="180px">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
