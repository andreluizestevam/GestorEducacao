<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.CadastramentoAgrupadorAtivExtra.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 305px; }
        .ulDados li input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li { margin-bottom: 10px; }
        .liClear { clear: both; }
        
        /*--> CSS dados */
        .txtCodRef { width: 123px; }
        .check label{ display: inline; margin-left: -5px; }
        .check input{ margin-left: -5px; } 
        .txtDescricao { width: 295px; }
        .ddlTipo { width: 70px; }
        .txtNome { width: 160px; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Descri��o">
                Descri��o</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe a Descri��o do Agrupador" runat="server" MaxLength="30" CssClass="txtNome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNome"
                ErrorMessage="Descri��o deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="txtCodRef" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtCodRef" ToolTip="Informe a Sigla do Agrupador" runat="server" MaxLength="12" CssClass="txtCodRef"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDtSituacao" class="lblObrigatorio">Data de Situa��o</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data da Situa��o"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtSituacao"
                ErrorMessage="Data da Situa��o � requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="ddlSituacao"  class="lblObrigatorio" title="Situa��o">
                Situa��o</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situa��o" runat="server" CssClass="ddlSituacao">
                <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
