<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RegistraSolicitacaoServicos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
     /*--> CSS DADOS */
    .txtSolicitacao {width: 255px;} 
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlModalidade">Grupo</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" AutoPostBack="true" ToolTip="Selecione um Grupo"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" class="liSerie">Subgrupo</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="campoSerieCurso" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" 
                AutoPostBack="true"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma">Nível</label>
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="campoTurma" AutoPostBack="True"></asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label for="txtSolicitacao">N° Solicitação</label>
            <asp:TextBox ID="txtSolicitacao" runat="server" CssClass="txtSolicitacao"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
