<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacAlunoTransf.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3399_Relatorios.RelacAlunoTransf" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liUnidade,.liPeriodo,.liTpTransf
        {
            margin-top: 5px;
            width: 300px;            
        }       
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .ddlTpTransf { width: 100px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" Class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>  
        <li class="liTpTransf">
            <label Class="lblObrigatorio">
                Tipo Transferência</label>               
            <asp:DropDownList ID="ddlTpTransf" CssClass="ddlTpTransf" runat="server" ToolTip="Selecione o Tipo de Transferência">
             <asp:ListItem Selected="True" Value="I">Entre Turmas</asp:ListItem>
             <asp:ListItem Value="U">Entre Unidades</asp:ListItem>
             <asp:ListItem Value="E">Externa</asp:ListItem>
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTpTransf" Text="*" 
                ErrorMessage="Campo Tipo Transferência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
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