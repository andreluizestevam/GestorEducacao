<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaEvoluMatricEfetiAnual.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.Relatorios.MapaEvoluMatricEfetiAnual" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 225px; }
        .liUnidade,.liAnoBase
        {
            margin-top: 5px;
            width: 225px;            
        }          
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: -5px;
        }
        .liSerie
        {
        	clear: both;
        	margin-top: 5px;        	
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
            <label id="Label1" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True"
            onselectedindexchanged="ddlUnidade_SelectedIndexChanged1" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="txtAnoBase">
                Ano Base</label>       
            <asp:TextBox ID="txtAnoBase" CssClass="txtAno" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtAnoBase" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBase" Text="*" 
            ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
        <li class="liModalidade">
        <label class="lblObrigatorio" for="ddlModalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" ToolTip="Selecione uma Modalidade"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        </ContentTemplate>
        </asp:UpdatePanel>         
    </ul>   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtAno").mask("9999");
        });

        jQuery(function($){
           $(".txtAno").mask("9999");     
        });
    </script>
</asp:Content>
