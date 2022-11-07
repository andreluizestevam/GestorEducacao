<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6322_TransferenciaInternaBens.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtAnoDrop { width: 60px; }
        .ddlUnidadaPatr { width: 250px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlUnidade">Unidade do Patrimônio </label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="ddlUnidadaPatr" ToolTip="Selecione uma Unidade de Origem"
                AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlDepartamento">
                Departamento do Patrimônio
            </label>
            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="ddlUnidadaPatr"
                ToolTip="Selecione um Departamento">
            </asp:DropDownList>
        </li>
         <li>
            <label for="ddlPatrimonio">
                Patrimônio
            </label>
            <asp:DropDownList ID="ddlPatrimonio" runat="server" CssClass="ddlUnidadaPatr"
                ToolTip="Selecione um Patrimônio">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
