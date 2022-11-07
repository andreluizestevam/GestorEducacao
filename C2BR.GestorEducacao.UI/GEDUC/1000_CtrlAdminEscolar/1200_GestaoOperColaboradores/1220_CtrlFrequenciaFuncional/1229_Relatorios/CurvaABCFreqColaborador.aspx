<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="CurvaABCFreqColaborador.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1229_Relatorios.CurvaABCFreqColaborador" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liOpcoesRelatorio, .liPeriodo, .liTipoCol{margin-top: 5px;width: 300px;}   
        .liDescOpRelatorio{margin-top: 5px;}
        .ddlDescOpRelatorio{width: 220px;margin-top: 1px;display:block;}        
        .lblDescOpRelatorio{display:inline;}
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .lblOpcoesRelatorio{margin-bottom:1px;}
        .ddlOpcoesRelatorio{width:140px;}
        .lblObrig{color:Red; width:50px;display:inline; margin-left:0px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liOpcoesRelatorio">   
            <label id="lblOpcoesRelatorio" class="lblObrigatorio" runat="server">
                Tipo Relatório</label>
            <asp:DropDownList ID="ddlOpcoesRelatorio" CssClass="ddlOpcoesRelatorio" runat="server" ToolTip="Selecione o Tipo de Relatório"
                        onselectedindexchanged="ddlOpcoesRelatorio_SelectedIndexChanged" 
                        AutoPostBack="True" >
            <asp:ListItem Value="U" Selected="True">Por Unidade/Escola</asp:ListItem>                
            <asp:ListItem Value="F">Por Função</asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                ControlToValidate="ddlOpcoesRelatorio" Text="*" 
                ErrorMessage="Campo Tipo de Relatório é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>    
        <li class="liDescOpRelatorio">
            <asp:Label id="lblDescOpRelatorio" CssClass="lblDescOpRelatorio" runat="server">
                Unidade/Escola</asp:Label><label class="lblObrig">*</label>
            <asp:DropDownList ID="ddlDescOpRelatorio" CssClass="ddlDescOpRelatorio" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlDescOpRelatorio" Text="*" 
                ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li> 
        <%--<li class="liTipoCol">
            <label class="lblObrigatorio" for="ddlTipoColaborador">
                Tipo do Colaborador</label>
            <asp:DropDownList ID="ddlTipoColaborador" CssClass="ddlTipoCol" runat="server" 
                ToolTip="Selecione o Tipo do Colaborador" AutoPostBack="True" Width="120px">
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="N">Funcionários</asp:ListItem>
                <asp:ListItem Value="S">Professor</asp:ListItem>
            </asp:DropDownList>
        </li>  --%>
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

