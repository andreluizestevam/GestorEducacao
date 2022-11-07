<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3040_CtrlHistoricoEscolar._3042_AlteraGradeAluno.Busca" %>
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
            <label class="lblObrigatorio" for="txtAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                ToolTip="Selecione o Ano" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
         </li>                
        <li>
            <label for="ddlModalidade" title="Modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade em que o aluno está matrículado atualmente"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Informe a modalidade ou selecione todos." 
                ControlToValidate="ddlModalidade">*</asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerie" title="Série/Curso">Série/Curso</label>
            <asp:DropDownList ID="ddlSerie" CssClass="campoModalidade" runat="server" AutoPostBack="true" 
                
                ToolTip="Selecione a Série/Curso em que o aluno está matrículado atualmente" 
                onselectedindexchanged="ddlSerie_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Informe a série/curso ou selecione todos" 
                ControlToValidate="ddlSerie">*</asp:RequiredFieldValidator>
        </li>
         <li>
            <label  for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" 
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
       </li> 
        <li>
            <label for="ddlAluno" title="Aluno">Aluno</label>
            <asp:DropDownList ID="ddlAluno" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Aluno"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlAluno" 
                ErrorMessage="Informe o aluno ou escolha a opção todos.">*</asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
