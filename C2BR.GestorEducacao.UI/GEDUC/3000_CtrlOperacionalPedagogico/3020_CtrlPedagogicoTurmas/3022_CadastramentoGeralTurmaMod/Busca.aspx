<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" 
CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3022_CadastramentoGeralTurmaMod.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlModalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" ToolTip="Informe a Modalidade"></asp:DropDownList>
        </li>
        <li>
            <label for="txtNO_TURMA">Turma</label>
            <asp:TextBox ID="txtNO_TURMA" runat="server" MaxLength="40" CssClass="txtTurma" ToolTip="Informe a Turma"></asp:TextBox>
        </li>
    </ul>
</asp:Content>