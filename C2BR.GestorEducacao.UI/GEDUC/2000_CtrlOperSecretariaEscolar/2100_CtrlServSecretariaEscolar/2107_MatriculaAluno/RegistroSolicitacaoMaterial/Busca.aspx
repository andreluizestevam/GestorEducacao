<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroSolicitacaoMaterial.Busca"
    Title="Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" CssClass="campoAno" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" runat="server" CssClass="campoModalidade" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série" class="lblObrigatorio">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" 
                ToolTip="Selecione a Série" AutoPostBack="true"
                onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série deve ser informada"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

 <script type="text/javascript">
</script>

</asp:Content>
