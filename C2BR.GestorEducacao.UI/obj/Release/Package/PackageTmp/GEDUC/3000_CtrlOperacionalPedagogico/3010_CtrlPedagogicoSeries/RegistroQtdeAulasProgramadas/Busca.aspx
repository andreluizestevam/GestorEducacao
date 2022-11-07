<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.RegistroQtdeAulasProgramadas.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlAno" ToolTip="Selecione o Ano">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true"
                ToolTip="Selecione a Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
