<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacAluMatric.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3019_Relatorios.RelacAluMatric" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 250px;            
        }      
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie, .liSituacao
        {
        	margin-top: 5px;
        	margin-left: 5px;
        }      
        .liTurma,.liAno { margin-top: 5px; }   
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
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAno">
            <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged"
                ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
        <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>       
        <li class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liSituacao">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server"
                ToolTip="Selecione a Situação">
            <asp:ListItem Selected="True" Value="A">Matriculado</asp:ListItem>
            <asp:ListItem Value="C">Cancelado</asp:ListItem>
            <asp:ListItem Value="T">Trancado</asp:ListItem>
            <asp:ListItem Value="F">Finalizado</asp:ListItem>
            <asp:ListItem Value="X">Transferido</asp:ListItem>
            <asp:ListItem Value="E">Evadido</asp:ListItem>
            <asp:ListItem Value="P">Pendente</asp:ListItem>
            <asp:ListItem Value="0">Todos</asp:ListItem>            
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Situação">
                Ordenação</label>  
            <asp:DropDownList ID="drpOrdenacao" runat="server" Width="150px">
                <asp:ListItem Value="1" Text="Unidade, Série e Turma" />
                <asp:ListItem Value="2" Text="Identificação MEC" />
                <asp:ListItem Value="3" Text="Nome do Aluno" />
                <asp:ListItem Value="4" Text="Responsável" />
                <asp:ListItem Value="5" Text="Idade" />
                <asp:ListItem Value="6" Text="Situação" />
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
