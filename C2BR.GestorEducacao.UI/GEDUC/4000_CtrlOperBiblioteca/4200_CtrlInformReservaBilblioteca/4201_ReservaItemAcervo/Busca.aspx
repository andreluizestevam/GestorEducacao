<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4200_CtrlInformReservaBilblioteca.F4201_ReservaItemAcervo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTipoUsuario { width: 76px; }
        .ddlUnidade, .ddlUsuario { width: 270px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlUnidadeUsuario" title="Unidade do Usuário">Unidade do Usuário</label>
            <asp:DropDownList ID="ddlUnidadeUsuario"
                ToolTip="Selecione a Unidade do Usuário" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" 
                onselectedindexchanged="ddlUnidadeUsuario_SelectedIndexChanged">
            </asp:DropDownList>
        </li>                      
        <li>
            <asp:HiddenField runat="server" ID="hfSelectedRow" EnableViewState="false" Value="-1" />
            <label for="ddlTipoUsuario" title="Tipo de Usuário">Tipo de Usuário</label>
            <asp:DropDownList ID="ddlTipoUsuario" CssClass="ddlTipoUsuario" runat="server" AutoPostBack="true" 
                ToolTip="Selecione o Tipo de Usuário" 
                onselectedindexchanged="ddlTipoUsuario_SelectedIndexChanged">
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="F">Colaborador</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
            </asp:DropDownList>
        </li>        
        <li>
            <label for="ddlUsuario" title="Usuário">Usuário</label>
            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="ddlUsuario" 
                ToolTip="Selecione o Usuário">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script type="text/javascript">
</script>
</asp:Content>