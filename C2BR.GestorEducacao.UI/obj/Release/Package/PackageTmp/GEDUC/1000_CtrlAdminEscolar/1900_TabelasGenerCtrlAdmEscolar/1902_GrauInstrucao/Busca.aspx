<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1902_GrauInstrucao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .labelPixel { margin-bottom:1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCO_SIGLA_INST" class="labelPixel" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtCO_SIGLA_INST" class="campoSigla" runat="server" MaxLength="3"
                ToolTip="Informe a Sigla"></asp:TextBox>
        </li>
        <li>
            <label for="txtNO_FUN" class="labelPixel" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtNO_FUN" runat="server" CssClass="campoDescricao" MaxLength="40"
                ToolTip="Informe a Descrição"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
