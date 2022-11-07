<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1122_PlanejDisVagaModSerTurnAnoLet.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTurma { width: 110px;}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlAno" title="Ano">Ano</label>
        <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano">
        </asp:DropDownList>
    </li>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true" onselectedindexchanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlSerieCurso" title="Série">Série/Curso</label>
        <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
            ToolTip="Selecione a Série/Curso" AutoPostBack="true"
          onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
        </asp:DropDownList>        
    </li>
    <li>
        <label for="ddlTurma" title="Turma">Turma</label>
        <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma"
            ToolTip="Selecione a Turma">
        </asp:DropDownList>        
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
