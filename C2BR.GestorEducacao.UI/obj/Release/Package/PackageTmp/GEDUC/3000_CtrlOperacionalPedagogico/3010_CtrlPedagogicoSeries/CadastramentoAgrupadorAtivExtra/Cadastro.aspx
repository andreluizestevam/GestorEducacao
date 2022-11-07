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
            <label for="txtNome" class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe a Descrição do Agrupador" runat="server" MaxLength="30" CssClass="txtNome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNome"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="txtCodRef" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtCodRef" ToolTip="Informe a Sigla do Agrupador" runat="server" MaxLength="12" CssClass="txtCodRef"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDtSituacao" class="lblObrigatorio">Data de Situação</label>
            <asp:TextBox ID="txtDtSituacao" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data da Situação"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtSituacao"
                ErrorMessage="Data da Situação é requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="ddlSituacao"  class="lblObrigatorio" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server" CssClass="ddlSituacao">
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
