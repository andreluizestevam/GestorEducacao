<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacTarefIncons.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1319_Relatorios.RelacTarefIncons" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liFuncionarios, .liUnidade, .liTipo {margin-top: 5px;width: 300px;}        
        .liFuncionarios{clear: both;}                       
        .ddlTipo{width:85px;}
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
        <li class="liTipo">
            <label class="lblObrigatorio" >Tipo</label>                    
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" 
                AutoPostBack="true" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="E">Emissor</asp:ListItem>
            <asp:ListItem Value="R">Responsavel</asp:ListItem>
            </asp:DropDownList>
        </li>        
        <li id="liFuncionarios" class="liFuncionarios" runat="server">
            <label for="txtFuncionarios">
                Solicitante</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Solicitante">
            </asp:DropDownList>
        </li>    
        </ContentTemplate>
        </asp:UpdatePanel>                  
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
