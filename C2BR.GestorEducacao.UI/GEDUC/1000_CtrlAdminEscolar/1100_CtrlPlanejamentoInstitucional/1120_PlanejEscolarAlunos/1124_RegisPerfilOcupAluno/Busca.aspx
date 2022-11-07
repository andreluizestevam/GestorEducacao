<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1124_RegisPerfilOcupAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade { width: 200px; }
        .ddlano { width: 80px }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade"  title="Nome da Unidade" >
                Unidade</label>
            <asp:DropDownList id="ddlUnidade" runat="server" ToolTip="Selecione a Unidade" CssClass="ddlUnidade">
              </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAno" title="Ano de Referência" >
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="ddlano"> 
              </asp:DropDownList>
        </li>        
    </ul>
</asp:Content>
