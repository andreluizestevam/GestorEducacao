﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AlunosPorSerieTurmaAssinaturaResponsavel.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.AlunosPorSerieTurmaAssinaturaResponsavel" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 280px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 280px;
        }        
        .liAnoRefer, .liModalidade, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }
        .liSerie
        {
        	margin-top:5px;
        	margin-left:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label title="Unidade">
                Unidade</label>
            <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:TextBox>
        </li> 
        <li class="liUnidade">
            <label title="Unidade/Contrato">
                Unidade/Contrato</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Contrato" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
        </li>        
        <li id="liAnoRefer" runat="server" class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAnoRefer" Text="*" 
                ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liModalidade" runat="server" class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione um Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li id="liTurma" class="liTurma" runat="server">
            <label class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        </ContentTemplate>
        </asp:UpdatePanel>                      
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>