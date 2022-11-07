<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="LogAtividUsuario.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0599_Relatorios.LogAtividUsuario" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 340px; } /* Usado para definir o formulário ao centro */ 
        .liFuncionalidade { margin-top: 15px; }
        .liUsuario, .liAcao { margin-top: 5px; }              
        .liUnidade
        {
        	margin-top: 5px;
        	clear: both;
        }             
        .liUsuario
        {
        	display:inline;
        	margin-left: 5px;
        }                
        .liPeriodo
        {
        	margin-top: -28px;
        	margin-left: 160px;
        }
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .ddlFuncionalidade { width: 330px; }
        .ddlAcao { width:75px; }
        .ddlUnidade { width:60px; }
   </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">           
        <li class="liFuncionalidade">
            <label class="lblObrigatorio" title="Funcionalidade">
                Funcionalidade</label>               
            <asp:DropDownList ID="ddlFuncionalidade" ToolTip="Selecione a Funcionalidade" CssClass="ddlFuncionalidade" runat="server">           
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlFuncionalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlFuncionalidade" Text="*" 
            ErrorMessage="Campo Funcionalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label for="ddlUnidade" class="lblObrigatorio labelPixel" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Informe a Unidade/Escola" CssClass="ddlUnidade"
                runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Unidade/Escola deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liUsuario">
            <label id="Label1" class="lblObrigatorio" title="Usuario">
                Usuario</label>
            <asp:DropDownList ID="ddlUsuario" CssClass="ddlNomePessoa" ToolTip="Selecione o Usuário" runat="server">
            </asp:DropDownList>  
            <asp:RequiredFieldValidator ID="rfvddlUsuario" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUsuario" Text="*" 
            ErrorMessage="Campo Usuário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liAcao">
            <label class="lblObrigatorio" title="Ação">
                Ação</label>               
            <asp:DropDownList ID="ddlAcao" ToolTip="Selecione a Ação" CssClass="ddlAcao" runat="server">           
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="G">Gravação</asp:ListItem>
                <asp:ListItem Value="E">Alteração</asp:ListItem>
                <asp:ListItem Value="D">Exclusão</asp:ListItem>
                <asp:ListItem Value="P">Consulta</asp:ListItem>
                <asp:ListItem Value="R">Relatório</asp:ListItem>
                <asp:ListItem Value="X">Sem Ação</asp:ListItem>
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAcao" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAcao" Text="*" 
            ErrorMessage="Campo Ação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>
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
