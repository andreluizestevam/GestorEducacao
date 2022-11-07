<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelHistoTarefAgendada.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1319_Relatorios.RelHistoTarefAgendada" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liPeriodo, .liFuncionarios, .liUnidade, .liTipo, .liPrioridade {margin-top: 5px;width: 300px;}        
        .liFuncionarios{clear: both;}                       
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .ddlTipo{width:85px;}
        .ddlPrioridade { width: 72px; }
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
            <label class="lblObrigatorio">Tipo</label>                    
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo"
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
        <li class="liPrioridade">
            <label for="ddlPrioridade">Prioridade</label>
                <asp:DropDownList ID="ddlPrioridade" CssClass="ddlPrioridade" runat="server" ToolTip="Selecione a Prioridade">            
                </asp:DropDownList>
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

