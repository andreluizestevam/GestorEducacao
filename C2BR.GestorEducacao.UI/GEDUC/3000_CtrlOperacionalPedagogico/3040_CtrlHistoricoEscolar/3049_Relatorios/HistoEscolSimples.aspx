﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="HistoEscolSimples.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3040_CtrlHistoricoEscolar.F3049_Relatorios.HistoEscolSimples" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade,.liAluno 
        { 
        	margin-top: 5px;
            width: 270px;            
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
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Aluno">
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="rfvddlAluno" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>    
        </ContentTemplate>
        </asp:UpdatePanel>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
