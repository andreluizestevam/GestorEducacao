<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacAlunoPorEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.RelacAlunoPorEscola" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        .liUnidade,.liAnoBase,.liTipo
        {
            margin-top: 5px;
            width: 250px;
        }
         .liUnidade1
         {
             margin-bottom:-3px;
         }
           
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <%--<li>
        <label title="Ordernar lista de alunos por nire ou nome">Ano Letivo</label>
            <asp:DropDownList ID="ddlAno"  runat="server">
            </asp:DropDownList>
        </li> --%>
        <li class="liUnidade1">
            <label title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:TextBox>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator5" runat="server"
                ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator> 
        </li>        
        
        <li class="liTipo">
            <label  title="Tipo">
               Tipo</label>
            <asp:DropDownList ID="ddlTipoAluno"  runat="server">
            </asp:DropDownList>
            
        </li>   
        
        <li class="liTipo">
        <label title="Ordernar lista de alunos por nire ou nome">Ordenar por:</label>
            <asp:DropDownList ID="ddlOrdem"  runat="server">
            </asp:DropDownList>
        </li>   
        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">    
    <script type="text/javascript">
    jQuery(function ($) {
        $(".txtAno").mask("9999");
    });
    </script>
</asp:Content>
