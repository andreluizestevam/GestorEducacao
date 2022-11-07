﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DeclaracaoDeQuitacaoFinanceria.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes.DeclaracaoDeQuitacaoFinanceria" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .liUnidade, .liAno { margin-top: 5px; }                                     
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie
        {
        	clear: both;
        	margin-top: 5px;        	
        }            
        .liAluno { margin-top:5px; }   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
                    <label title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
         </li>
      
        <li class="liAno">
            <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                ToolTip="Selecione o Ano" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAno" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li class="liModalidade">
        <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" Width="150px">
        </asp:DropDownList>
        <asp:RequiredFieldValidator runat="server" ID="rfvModa" ControlToValidate="ddlModalidade"></asp:RequiredFieldValidator>
       </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlSerieCurso"></asp:RequiredFieldValidator>
         </li>  
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" 
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" Width="140px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlTurma"></asp:RequiredFieldValidator>
       </li>       
        <li class="liAluno">
            <label title="Aluno" class="lblObrigatorio">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                  
        </li>      
        </ContentTemplate>
        </asp:UpdatePanel>                               
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
