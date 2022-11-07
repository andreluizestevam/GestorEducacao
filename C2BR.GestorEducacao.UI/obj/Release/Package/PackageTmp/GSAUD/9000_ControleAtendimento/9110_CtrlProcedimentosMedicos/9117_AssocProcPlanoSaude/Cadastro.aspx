<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9117_AssocProcPlanoSaude.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados{width: 560px;}
    
    /*--> CSS LIs */
    .liBarraTituloTPA { background-color: #EEEEEE;margin-top:5px; margin-bottom: 2px; padding: 5px; text-align: center; width: 692px; height:10px; clear:both; margin-left: -90px;}
    
    /*--> CSS DADOS */
    .divTreeViewFuncTPA{clear:both; height: 335px;width:700px;overflow:auto;border:1px solid #CCCCCC;margin-left: -90px;}
    .divCheckOperacaoesTPA{height: 335px;width:86px; border:1px solid #CCCCCC; margin-left:505px; margin-top:-337px;}
    .lbldivCheckOperacaoesTPA{ font-size:11px; font-weight:bold; margin-bottom:10px; margin-top:10px; text-align:center;}
    .chkOperacoesTPA{margin-left: 7px;}
    .lblchkOperacoesTPA{margin-left: 30px; margin-bottom:10px;margin-top:-14px;}
    
</style>
<script type="text/javascript" src="../../../../Library/JS/TreeView.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
         <li style="margin:-5px 0 10px 150px !important;">
            <label for="ddlUnidadeTPA" class="lblObrigatorio" title="Unidade/Escola">
                Plano </label>
            <asp:DropDownList ID="ddlPlano" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="True" ToolTip="Selecione o Plano" Enabled="false" Width="270px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPlano"
                ErrorMessage="Unidade/Escola deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liBarraTituloTPA">
            <label>MARQUE PARA HABILITAR A ASSOCIAÇÃO DO(S) PROCEDIMENTO(S) À UNIDADE</label>
        </li>        
        <div id="divTreeViewFuncTPA" class="divTreeViewFuncTPA" runat="server">
            <asp:TreeView ID="TreeViewFuncTPA" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeViewFuncTPA_SelectedNodeChanged" ToolTip="Marque um Item e clique na Funcionalidades para carregar as permissões">
            </asp:TreeView>
        </div>
    </ul>
</asp:Content>
