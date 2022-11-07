<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6260_CtrlInformacoesItensEstoque.F6264_CadastroTipoMovtoItemEstoque.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .ddlTipoMovimento { width:60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio labelPixel" title="Descrição do Tipo de Movimento">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição do Tipo de Movimento"
                runat="server" MaxLength="80"></asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDescricao" title="Tipo de Movimento" class="lblObrigatorio" >
                Tipo Movimento</label>
            <asp:DropDownList ID="ddlTipoMovimento" CssClass="ddlTipoMovimento" runat="server" ToolTip="Selecione o Tipo de Movimento">
                <asp:ListItem Text="Entrada" Value="E" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Saida" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
