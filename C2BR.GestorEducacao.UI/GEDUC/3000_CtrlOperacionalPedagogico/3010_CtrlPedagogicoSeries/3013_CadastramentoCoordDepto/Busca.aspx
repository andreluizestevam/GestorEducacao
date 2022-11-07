<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3013_CadastramentoCoordDepto.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome{ width: 265px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlDepartamento" title="Departamento">Departamento</label>
        <asp:DropDownList ID="ddlDepartamento" class="ddlDepartamento campoDptoCurso" runat="server"
            ToolTip="Selecione o Departamento">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtNome" title="Coordenação">Coordenação</label>
        <asp:TextBox ID="txtNome" class="txtNome" runat="server" MaxLength="40"
            ToolTip="Informe o nome da Coordenação">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
