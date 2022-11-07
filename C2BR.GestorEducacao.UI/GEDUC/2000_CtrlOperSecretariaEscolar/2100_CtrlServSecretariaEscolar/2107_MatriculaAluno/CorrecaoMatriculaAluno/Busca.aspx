<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.CorrecaoMatriculaAluno.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li class="liAno">
            <label for="ddlAno">Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano corrente">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtNire" title="Digite o Nire do Aluno">Nire</label>
            <asp:TextBox ID="txtNire" runat="server" CssClass="txtNire" MaxLength="9" Width="60" ToolTip="Digite o Nire do Aluno"></asp:TextBox>
        </li>
        <li>
            <label for="ddlModalidade" title="Selecione a modalidade">Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a modalidade" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" runat="server" width="120px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerie" title="Selecione a série">Série</label>
            <asp:DropDownList ID="ddlSerie" ToolTip="Selecione a sárie" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged" runat="server" width="70px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma" title="Selecione a turma">Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a turma" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" runat="server" width="70px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAluno" title="Selecione o aluno">Nome</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o aluno" runat="server" width="250px">
            </asp:DropDownList>
        </li>
    </ul>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtNire").mask("?999999999");
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
