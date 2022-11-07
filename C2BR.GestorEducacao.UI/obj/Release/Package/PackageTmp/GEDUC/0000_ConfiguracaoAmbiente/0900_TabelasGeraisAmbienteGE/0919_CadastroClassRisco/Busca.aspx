<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0919_CadastroClassRisco.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ddlTipo, .ddlSituacao
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li style="margin-top: 50px;">
            <label title="Tipo Classificação de Risco">
                Tipo Clas. Risco</label>
            <asp:DropDownList ID="ddlTipoClassRisco" runat="server" ToolTip="Pesquise pelo tipo de classificação de risco">
                <asp:ListItem Text="Todos" Value="0" />
                <asp:ListItem Text="Australiano" Value="1" />
                <asp:ListItem Text="Canadense" Value="2" />
                <asp:ListItem Text="Manchester" Value="3" />
                <asp:ListItem Text="Americano" Value="4" />
                <asp:ListItem Text="Pediatria" Value="5" />
                <asp:ListItem Text="Obstetrícia" Value="6" />
                <asp:ListItem Text="Instituição" Value="99" />
            </asp:DropDownList>
        </li>
        <li>
            <label title="Situação da classificação de risco">
            Situação
            </label>
            <asp:DropDownList ID="ddlSitua" runat="server" ToolTip="Pesquise pela situação da classificação de risco">
                <asp:ListItem Text="Todos" Value="T" />
                <asp:ListItem Text="Ativo" Value="A"/>
                <asp:ListItem Text="Inativo" Value="I"/>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
