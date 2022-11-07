<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPlanejMatricula.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1129_Relatorios.MapaPlanejMatricula" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 320px; }
        .liModalidade, .liAnoBase,.liUnidade, .liDepartamento
        {
            margin-top: 5px;
            width: 220px;
        } 
        .ddlDepartamento { width: 315px; }   
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
        <li class="liDepartamento">
            <label class="lblObrigatorio" for="txtDepatamento">
                Departamento</label>               
            <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDepartamento" runat="server" CssClass="validatorField"
            ControlToValidate="ddlDepartamento" Text="*" 
            ErrorMessage="Campo Departamento é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade">
                Modalidade</label>                                        
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server">                 
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="lblAnoBase">
                Ano Base</label>               
            <asp:TextBox ID="txtAnoBase" CssClass="txtAno" runat="server">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtAnoBase" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBase" Text="*" 
            ErrorMessage="Campo Ano Base é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
