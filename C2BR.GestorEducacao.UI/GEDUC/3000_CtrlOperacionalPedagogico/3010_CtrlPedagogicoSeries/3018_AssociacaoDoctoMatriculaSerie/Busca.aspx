<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3018_AssociacaoDoctoMatriculaSerie.Busca" Title="Untitled Page" %>
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
        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true" onselectedindexchanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                CssClass="validatorField" ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="ddlSerieCurso" title="Série/Curso">Série/Curso</label>
        <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
            ToolTip="Selecione a Série/Curso">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                CssClass="validatorField" ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
