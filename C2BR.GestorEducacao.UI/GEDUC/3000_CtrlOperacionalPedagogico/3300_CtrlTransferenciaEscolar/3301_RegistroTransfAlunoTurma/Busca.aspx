<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3301_RegistroTransfAlunoTurma.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtAnoDrop{width: 60px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlAno">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="txtAnoDrop" 
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Informe o Ano" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="ddlModalidade" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" ToolTip="Selecione a Série/Curso">
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
