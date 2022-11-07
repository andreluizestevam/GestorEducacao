<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1207_CadastramentoTipoBeneficioColaborador.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlColaborador{ width: 220px;}                
        .ddlTpBeneficio{width: 120px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlUnidade"  title="Selecione a Unidade" >Unidade</label>
        <asp:DropDownList ID="ddlUnidade"  CssClass="ddlUnidadeEscolar" runat="server" 
          AutoPostBack="true" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">                                                             
        </asp:DropDownList>        
    </li>
    <li>
        <label for="ddlColaborador"  title="Colaborador">Colaborador</label>
        <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="ddlColaborador"
            ToolTip="Selecione o Colaborador"></asp:DropDownList>    
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="ddlTpBeneficio" title="Tipo de Beneficio">Tipo de Beneficio</label>
        <asp:DropDownList ID="ddlTpBeneficio" runat="server" CssClass="ddlTpBeneficio"
            ToolTip="Selecione o Tipo do Beneficio"></asp:DropDownList>
    </li>    
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>