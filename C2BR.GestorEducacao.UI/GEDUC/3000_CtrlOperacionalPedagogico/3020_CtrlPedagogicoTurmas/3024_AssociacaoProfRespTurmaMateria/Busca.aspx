<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3024_AssociacaoProfRespTurmaMateria.Busca"
    Title="Untitled Page" %>

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
                    <label for="ddlAno">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
                        OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAno"
                        ErrorMessage="Ano deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="ddlModalidade" class="lblObrigatorio">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" runat="server" ToolTip="Selecione a Modalidade"
                        CssClass="ddlModalidade" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlSerieCurso">
                        Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" runat="server" CssClass="ddlSerieCurso" AutoPostBack="true"
                        ToolTip="Selecione o Curso" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlTurma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" runat="server" CssClass="ddlTurma" ToolTip="Selecione a Turma">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlClassificacao">
                        Classificação</label>
                    <asp:DropDownList ID="ddlClassificacao" CssClass="ddlClassificacao" Width="135px"
                        runat="server" ToolTip="Classificação do Colaborador">
                        <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="C" Text="Coordenador de Turma"></asp:ListItem>
                        <asp:ListItem Value="P" Text="Professor Responsável"></asp:ListItem>
                        <asp:ListItem Value="A" Text="Professor Adjunto"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Monitor"></asp:ListItem>
                        <asp:ListItem Value="X" Text="Auxiliar"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
