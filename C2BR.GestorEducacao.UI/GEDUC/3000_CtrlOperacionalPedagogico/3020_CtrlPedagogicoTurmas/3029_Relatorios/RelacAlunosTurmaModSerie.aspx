<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacAlunosTurmaModSerie.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3029_Relatorios.RelacAlunosTurmaModSerie" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade,.liSituacao
        {
            margin-top: 5px;
            width: 250px;            
        }      
        .liModalidade
        {
        	margin-left: 5px;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie, .liAno { margin-top: 5px; }      
        .liTurma
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }     
        .ddlSituacao { width:80px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liAno">
        <label class="lblObrigatorio" for="txtUnidade">
            Ano</label>
        <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
            AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAno" Text="*" 
            ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>    
        <li class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>     
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liSituacao">
            <label class="lblObrigatorio">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
            <asp:ListItem Selected="True" Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="C">Cancelado</asp:ListItem>
            <asp:ListItem Value="T">Trancado</asp:ListItem>
            <asp:ListItem Value="F">Finalizado</asp:ListItem>
            <asp:ListItem Value="X">Transferido</asp:ListItem>
            <asp:ListItem Value="E">Evadido</asp:ListItem>
            <asp:ListItem Value="P">Pendente</asp:ListItem>
            <asp:ListItem Value="U">Todos</asp:ListItem>            
            </asp:DropDownList>
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
