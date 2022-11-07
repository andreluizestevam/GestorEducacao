<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2101_SolicitacaoItensSecretaria.DocumentosServicos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlTipoDoc">
                Tipo de Documento</label>
           <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="campoTipoDoc" Width="90px" AutoPostBack="true">
               <asp:ListItem Value="T">Todos</asp:ListItem>
               <asp:ListItem Value="AT">Ata</asp:ListItem>
               <asp:ListItem Value="CE">Certidão</asp:ListItem>
               <asp:ListItem Value="CO">Contrato</asp:ListItem>
               <asp:ListItem Value="CC">Certificado</asp:ListItem>
               <asp:ListItem Value="DE">Declaração</asp:ListItem>
               <asp:ListItem Value="DA">Documentos Administrativos</asp:ListItem>
               <asp:ListItem Value="DT">Documentos Técnicos</asp:ListItem>
               <asp:ListItem Value="HI">Histórico</asp:ListItem>
               <asp:ListItem Value="RE">Recibo</asp:ListItem>
               <asp:ListItem Value="OT">Outros</asp:ListItem>
            </asp:DropDownList> 
        </li>
        <li>
            <label for="txtNomeDoc" class="liSerie">
                Nome do Documento</label>
            <asp:TextBox ID="txtNomeDoc" runat="server" CssClass="campoNomeDoc" Width="250px" AutoPostBack="true">
            </asp:TextBox>
        </li>
        <li>
           <label for="txtSiglaDoc" class="liSerie">
                Sigla do Documento</label>
            <asp:TextBox ID="txtSiglaDoc" runat="server" CssClass="campoSiglaDoc" Width="90px" AutoPostBack="true">
            </asp:TextBox>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label for="ddlStatus">Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlSolicitacoes" AutoPostBack="True">
            <asp:ListItem Value="T">Todos</asp:ListItem>
               <asp:ListItem Value="A">Ativos</asp:ListItem>
               <asp:ListItem Value="I">Inativos</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
