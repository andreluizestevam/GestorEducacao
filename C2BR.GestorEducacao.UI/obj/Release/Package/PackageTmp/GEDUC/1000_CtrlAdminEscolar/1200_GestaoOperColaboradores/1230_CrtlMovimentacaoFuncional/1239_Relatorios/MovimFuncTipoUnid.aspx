<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MovimFuncTipoUnid.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1239_Relatorios.MovimFuncTipoUnid" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; } /* Usado para definir o formulário ao centro */
        .liUsuario,.liPeriodo
        {
            margin-top: 5px;   
            clear:both;     
        }              
        .liUnidade { margin-top: 5px; }             
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .ddlTpMovim { width: 120px; }
   </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">   
        <li class="liUnidade">
            <label for="ddlUnidade" class="lblObrigatorio labelPixel" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Informe a Unidade/Escola" CssClass="ddlUnidadeEscolar"
                runat="server" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Unidade/Escola deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li id="liUsuario" runat="server" class="liUsuario">
            <label id="Label1" class="lblObrigatorio" title="Tipo de Movimentação">
                Tipo de Movimentação</label>
            <asp:DropDownList ID="ddlTpMovim" CssClass="ddlTpMovim" ToolTip="Selecione o Tipo de Movimentação" runat="server">
            <asp:ListItem Value="T" Selected="true">Todos</asp:ListItem>
            <asp:ListItem Value="MI">Movimentação Interna</asp:ListItem>
            <asp:ListItem Value="ME">Movimentação Externa</asp:ListItem>
            <asp:ListItem Value="TE">Transferência Externa</asp:ListItem>
            </asp:DropDownList>  
            <asp:RequiredFieldValidator ID="rfvddlTpMovim" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpMovim" Text="*" 
            ErrorMessage="Campo Tipo de Movimentação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
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
