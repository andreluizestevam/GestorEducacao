<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ListaCargoFunciColab.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1239_Relatorios.ListaCargoFunciColab" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liUsuario,.liPeriodo, .liUnidade { margin-top: 5px; }       
        .liUsuario { display:inline; }         
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
   </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label for="ddlUnidade" class="lblObrigatorio labelPixel" title="Unidade/Escola">
                Unidade Origem</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Informe a Unidade/Escola" CssClass="ddlUnidadeEscolar"
                runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Unidade/Escola Origem deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li id="liUsuario" runat="server" class="liUsuario">
            <label id="Label1" Class="lblObrigatorio" title="Funcionário">
                Funcionário</label>
            <asp:DropDownList ID="ddlUsuario" CssClass="ddlNomePessoa" ToolTip="Selecione o Funcionário" runat="server">
            </asp:DropDownList>  
            <asp:RequiredFieldValidator ID="rfvddlUsuario" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUsuario" Text="*" 
            ErrorMessage="Campo Funcionário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período</label>                
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" ToolTip="Data Início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Data Fim"></asp:TextBox>    
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                                                                                                
        </li> 
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
