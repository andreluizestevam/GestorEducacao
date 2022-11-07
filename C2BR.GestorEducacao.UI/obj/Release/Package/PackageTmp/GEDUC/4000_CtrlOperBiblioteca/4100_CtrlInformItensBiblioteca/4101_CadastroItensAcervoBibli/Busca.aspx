<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4101_CadastroItensAcervoBibli.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
        /*--> CSS DADOS */    
        .ddlAreaConhecimento, .ddlClassificacao { width: 160px;}
        .ddlObra, .ddlUnidadeEscolar { width: 260px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlUnidade" title="Unidade/Escola">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" runat="server" CssClass="ddlUnidadeEscolar"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlAreaConhecimento" title="Área do Conhecimento">Área do Conhecimento</label>
            <asp:DropDownList ID="ddlAreaConhecimento" 
                ToolTip="Selecione uma Área do Conhecimento" runat="server"
                CssClass="ddlAreaConhecimento" AutoPostBack="True"
                onselectedindexchanged="ddlAreaConhecimento_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlClassificacao" title="Classificação">Classificação</label>
            <asp:DropDownList ID="ddlClassificacao" ToolTip="Selecione uma Classificação" runat="server" CssClass="ddlClassificacao" AutoPostBack="True"
                onselectedindexchanged="ddlClassificacao_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlObra" title="Nome da Obra">Nome da Obra</label>
            <asp:DropDownList ID="ddlObra" ToolTip="Selecione uma Obra" runat="server" CssClass="ddlObra"></asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script type="text/javascript">
    </script>
</asp:Content>
