<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" 
CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2105_ReservaVagas.ReservaVagasAlunos.Busca" 
Title="Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Nome">Nome</label>
        <asp:TextBox ID="txtNome" runat="server" MaxLength="80" CssClass="campoNomePessoa"
            ToolTip="Informe o Nome"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipoMatricula" title="Tipo de Matrícula">Tipo Matrícula</label>
        <asp:DropDownList ID="ddlTipoMatricula" runat="server"
            ToolTip="Selecione o Tipo de Matrícula">
            <asp:ListItem Value="">Todos</asp:ListItem>
            <asp:ListItem Value="R">Rematrícula</asp:ListItem>
            <asp:ListItem Value="N">Nova Matrícula</asp:ListItem>
        </asp:DropDownList>
    </li>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlModalidade" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" 
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlSerieCurso" title="Série/Curso">Série/Curso</label>
        <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso"
            ToolTip="Selecione a Série/Curso"></asp:DropDownList>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">  
    </script>
</asp:Content>
