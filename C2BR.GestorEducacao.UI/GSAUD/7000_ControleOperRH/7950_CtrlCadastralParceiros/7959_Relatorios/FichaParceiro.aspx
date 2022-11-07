<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="FichaParceiro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7950_CtrlCadastralParceiros._7959_Relatorios.FichaParceiro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 260px;
        }
        .liAnoBase, .liFuncionarios, .liUnidade, .liTipoCol
        {
            margin-top: 5px;
            width: 200px;
        }
        .liFuncionarios
        {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liFuncionarios">
                    <label class="lblObrigatorio" for="txtFuncionarios">
                        Parceiros</label>
                    <asp:DropDownList ID="ddlParceiro" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Parceiro">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlParceiro" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlParceiro" Text="*" ErrorMessage="Campo Funcionário é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
