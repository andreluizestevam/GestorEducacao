<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="GradeCurricSerie.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3019_Relatorios.GradeCurricSerie" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 225px; }
        .liUnidade,.liModalidade,.liSerie,.liAnoBase
        {
            margin-top: 5px;
            width: 225px;
        }        
        .liUnidade { clear: both; }      
        .lblDivAno
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator5" runat="server"
                ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator> 
        </li>        
        <li class="liModalidade">
            <label class="lblObrigatorio" title="Modalidade">
                Modalidade</label>               
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" AutoPostBack="true" 
                runat="server" onselectedindexchanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>       
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>               
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" 
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator3" runat="server"
                ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase" title="Ano Base">
                Ano Base</label>       
            <asp:textBox ID="txtAnoIni" CssClass="txtAno" runat="server"
                ToolTip="Informe o Ano Base Inicial">
            </asp:textBox>
            <asp:RequiredFieldValidator ID="rfvddlAnoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoIni" Text="*" 
            ErrorMessage="Campo Ano Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label id="lblDivAno" class="lblDivAno" runat="server" >à</asp:label>
            <asp:textBox ID="txtAnoFim" CssClass="txtAno" runat="server"
                ToolTip="Informe o Ano Base Final">
            </asp:textBox>           
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtAnoIni"
                ControlToCompare="txtAnoFim"
                Type="Integer"       
                Operator="LessThanEqual"      
                ErrorMessage="Ano Final não pode ser menor que Ano Inicial." >
            </asp:CompareValidator >
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoFim" Text="*" 
            ErrorMessage="Campo Ano Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
