<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoClienteOrdenaDespesa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5499_Relatorios.RelacaoClienteOrdenaDespesa" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        .liUnidade,.liTpDocumento,.liCNPJ,.liCPF
        {
        	clear:both;
            margin-top: 5px;
            width: 250px;            
        }                                   
        .liNomeCliente
        {
        	margin-top:-5px;
        	width: 250px;
        } 
        .ddlTpDocumento { width:55px; }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label title="Unidade/Escola" id="Label1" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liTpDocumento">
            <label class="label" title="Tipo de Documento">
                Tipo Documento</label>               
            <asp:DropDownList ID="ddlTpDocumento" ToolTip="Selecione um Tipo de Documento" CssClass="ddlTpDocumento" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlTpDocumento_SelectedIndexChanged">   
            <asp:ListItem Selected="True" Value="0">CNPJ</asp:ListItem>        
            <asp:ListItem Value="1">CPF</asp:ListItem>
            </asp:DropDownList>            
        </li>                     
        <li id="liCNPJ" class="liCNPJ" runat="server">
            <label class="label" title="CNPJ">
                CNPJ</label>
            <asp:TextBox ID="txtCNPJ" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server"></asp:TextBox>                
        </li>        
        <li id="liCPF" class="liCPF" runat="server">
            <label title="CPF" class="label">
                CPF</label>
            <asp:TextBox ID="txtCPF" ToolTip="Informe o CPF" CssClass="txtCPF" runat="server"></asp:TextBox>                
        </li>        
        <li class="liNomeCliente">
            <label class="label" title="Nome do Cliente">
                Nome Cliente</label>
            <asp:TextBox ID="txtNomeCliente" ToolTip="Informe o Nome do Cliente" CssClass="txtNomePessoa" runat="server"></asp:TextBox>
        </li>      
        </ContentTemplate>
        </asp:UpdatePanel>                                        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtCPF").mask("999.999.999-99");
            $(".txtCNPJ").mask("99.999.999/9999-99");
        });

        jQuery(function($){
           $(".txtCPF").mask("999.999.999-99");   
           $(".txtCNPJ").mask("99.999.999/9999-99");                  
        });               
    </script>
</asp:Content>
