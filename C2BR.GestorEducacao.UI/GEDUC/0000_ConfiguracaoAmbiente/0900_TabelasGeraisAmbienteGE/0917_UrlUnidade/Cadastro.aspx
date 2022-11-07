<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0917_UrlUnidade.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 240px;
            
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
            margin-right: 10px;
            width: 219px;
        }
        .liClear
        {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados" >
        <li>
             <label title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar"
                ToolTip="Selecione a Unidade" Enabled="False">
            </asp:DropDownList>
        </li>
        <li>
           <label>
                URL</label>
            <asp:TextBox ID="txtUrl" MaxLength="120" CssClass="campoNomePessoa" runat="server" 
                ToolTip="Informe  a Url" Width="198px"></asp:TextBox>
        </li>
        
    </ul>
</asp:Content>
