<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPlanejFinancContaContabil.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1119_Relatorios.MapaPlanejFinancContaContabil" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados { width: 230px; }
        .liUnidade, .liAnoBase, .liTipo
        {
            margin-top: 5px;
            width: 200px;
        }
        .liUnidade { clear: both; }
        .liVisualizacao,.liTipo { width: 140px; }        
        .liVisualizacao { margin-top:-5px; }
        .lblDivAno
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }       
        .ddlVisualizacao,.ddlTipo { width:80px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Seleciona a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase">
                Ano Base</label>       
            <asp:TextBox ID="txtAnoBaseIni" CssClass="txtAno" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtAnoBaseIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBaseIni" Text="*" 
            ErrorMessage="Campo Ano Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <label id="lblDivAno" class="lblDivAno" >à</label>
            <asp:TextBox ID="txtAnoBaseFim" CssClass="txtAno" runat="server"></asp:TextBox>
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtAnoBaseFim"
                ControlToCompare="txtAnoBaseIni"
                Type="Integer"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Ano Final não pode ser menor que Ano Inicial." >
            </asp:CompareValidator >
            
            <asp:RequiredFieldValidator ID="rfvtxtAnoBaseFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBaseFim" Text="*" 
            ErrorMessage="Campo Ano Final é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                    
        </li>  
        <li class="liVisualizacao">
            <label class="lblObrigatorio" for="txtVisualizacao">
                Visualização</label>
            <asp:DropDownList ID="ddlVisualizacao" CssClass="ddlVisualizacao" runat="server">
                <asp:ListItem Value="C">Receitas</asp:ListItem>
                <asp:ListItem Value="D">Despesas</asp:ListItem>
            </asp:DropDownList>                                       
        </li>    
        <li class="liTipo">
            <label class="lblObrigatorio" for="txtTipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server">
                <asp:ListItem Value="A">Analítico</asp:ListItem>
                <asp:ListItem Value="D">Diferença</asp:ListItem>
            </asp:DropDownList>                   
        </li>       
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        jQuery(function($){
           $(".txtAno").mask("9999");           
        });
    </script>
</asp:Content>
