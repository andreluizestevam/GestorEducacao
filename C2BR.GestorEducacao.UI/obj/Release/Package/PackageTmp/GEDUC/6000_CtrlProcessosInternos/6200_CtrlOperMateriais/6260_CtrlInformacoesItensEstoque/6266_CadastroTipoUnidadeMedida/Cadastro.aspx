<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6260_CtrlInformacoesItensEstoque.F6266_CadastroTipoUnidadeMedida.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .ddlUniCEA { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">       
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio labelPixel" title="Descrição da Unidade de Medida">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição da Unidade de Medida" runat="server" MaxLength="80"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
         <li class="liClear">
            <label for="txtSigla" title="Sigla da Unidade de Medida" class="lblObrigatorio labelPixel">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla da Unidade de Medida" runat="server" CssClass="txtSigla" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlCatUnidade" title="Categoria da Unidade de Medida" class="lblObrigatorio labelPixel">
                Categoria</label>
            <asp:DropDownList ID="ddlCatUnidade" CssClass="ddlUniCEA" runat="server" ToolTip="Informe a Categoria da Unidade de Medida">
                <asp:ListItem Value="T" Selected="True">Todas</asp:ListItem>
                <asp:ListItem Value="E">Estoque</asp:ListItem>
                <asp:ListItem Value="M">Merenda</asp:ListItem>
                <asp:ListItem Value="U">Uniforme</asp:ListItem>
                <asp:ListItem Value="S">Saúde</asp:ListItem>
                <asp:ListItem Value="R">Remédios/Medicamentos</asp:ListItem>
                <asp:ListItem Value="V">Vacinas</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCatUnidade"
                ErrorMessage="Categoria da Unidade de Medida deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
