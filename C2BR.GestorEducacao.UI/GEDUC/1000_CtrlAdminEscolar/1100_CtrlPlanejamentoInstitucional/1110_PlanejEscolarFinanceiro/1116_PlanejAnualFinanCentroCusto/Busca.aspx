<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1116_PlanejAnualFinanCentroCusto.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlAno"
                ToolTip="Selecione o Ano">
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlDepartamento" title="Departamento">
                Departamento</label>
            <asp:DropDownList ID="ddlDepto" runat="server" Width="215px" 
                AutoPostBack="True" onselectedindexchanged="ddlDepto_SelectedIndexChanged"
                ToolTip="Selecione o Departamento">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlCCusto" title="Centro de Custo">
                Centro Custo</label>
            <asp:DropDownList ID="ddlCCusto" runat="server" Width="215px"
                ToolTip="Selecione o Centro de Custo">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTipo" title="Tipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" Width="90px" AutoPostBack="True" 
                onselectedindexchanged="ddlTipo_SelectedIndexChanged"
                ToolTip="Selecione o Tipo">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlGrupoCentroCusto" title="Subgrupo da Conta Contábil">
                SubGrupo Conta Contábil</label>
            <asp:DropDownList ID="ddlSubGrupoConta" runat="server" Width="215px" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlSubGrupoConta_SelectedIndexChanged"
                ToolTip="Selecione o Subgrupo da Conta Contábil">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlCtaContabil" title="Conta Contábil">
               Conta Contábil</label>
            <asp:DropDownList ID="ddlCtaContabil" runat="server" Width="215px"
                ToolTip="Selecione a Conta Contábil">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
