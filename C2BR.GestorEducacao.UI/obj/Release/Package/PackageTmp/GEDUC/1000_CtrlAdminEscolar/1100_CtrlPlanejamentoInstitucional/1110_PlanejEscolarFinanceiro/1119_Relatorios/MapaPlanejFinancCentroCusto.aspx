<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPlanejFinancCentroCusto.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1119_Relatorios.MapaPlanejFinancCentroCusto" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados { width: 230px; }
        .liUnidade, .liAnoBase,.liDepartamento, .liTipo
        {
            margin-top: 5px;
            width: 230px;
        }
        .liUnidade { clear: both; }
        .liVisualizacao, .liTipo
        {
            width: 140px;            
        }        
        .liVisualizacao { margin-top:-5px; }
        .lblDivAno
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }       
        .ddlVisualizacao,.ddlTipo { width:80px; }
        .ddlDepartamento { width:225px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liDepartamento">
            <label class="lblObrigatorio" title="Departamento">
                Departamento</label>
            <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" runat="server"
                ToolTip="Selecione o Departamento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDepartamento" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Departamento é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>       
        </ContentTemplate>
        </asp:UpdatePanel> 
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase" title="Ano Base">
                Ano Base</label>       
            <asp:TextBox ID="txtAnoBaseIni" CssClass="txtAno" runat="server"
                ToolTip="Informe o Ano Inicial">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtAnoBaseIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBaseIni" Text="*" 
            ErrorMessage="Campo Ano Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <label id="lblDivAno" class="lblDivAno" >à</label>
            <asp:TextBox ID="txtAnoBaseFim" CssClass="txtAno" runat="server"
                ToolTip="Informe o Ano Final">
            </asp:TextBox>
            
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
            <label class="lblObrigatorio" for="txtVisualizacao" title="Visualização">
                Visualização</label>
            <asp:DropDownList ID="ddlVisualizacao" CssClass="ddlVisualizacao" runat="server"
                ToolTip="Selecione o Tipo de Visualização">
                <asp:ListItem Value="C">Receitas</asp:ListItem>
                <asp:ListItem Value="D">Despesas</asp:ListItem>
            </asp:DropDownList>                                       
        </li>    
        <li class="liTipo">
            <label class="lblObrigatorio" for="txtTipo" title="Tipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server"
                ToolTip="Selecione o Tipo">
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
