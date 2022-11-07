<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1205_RegistroOcorrenciaColaborador.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade { width: 250px; }
        .ddlColaborador { width: 230px; }
        .ddlTipoOcorrencia { width: 80px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlUnidade" title="Unidade">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li>
            <label for="ddlColaborador" title="Colaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" ToolTip="Selecione o Colaborador" CssClass="ddlColaborador" runat="server">
            </asp:DropDownList>
        </li>      
        </ContentTemplate>
        </asp:UpdatePanel>  
        <li>
            <label for="ddlTipoOcorrencia" title="Tipo Ocorrência">Tipo de Ocorrência</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione o Tipo de Ocorrência" CssClass="ddlTipoOcorrencia" runat="server">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
