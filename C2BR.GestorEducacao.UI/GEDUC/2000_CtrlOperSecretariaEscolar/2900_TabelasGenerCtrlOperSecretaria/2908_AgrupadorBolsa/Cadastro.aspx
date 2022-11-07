<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2908_AgrupadorBolsa.Cadastro" %>

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
            <label for="txtNome" class="lblObrigatorio" title="Nome">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Agrupador" runat="server" MaxLength="40" CssClass="txtNome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNome"
                ErrorMessage="Nome deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="txtCodRef" title="Código de Referência">
                Cod REF</label>
            <asp:TextBox ID="txtCodRef" ToolTip="Informe o Código de Referência do Agrupador" runat="server" MaxLength="20" CssClass="txtCodRef"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDescricao" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição do Agrupador" runat="server" MaxLength="200" CssClass="txtDescricao"></asp:TextBox>            
        </li>                        
        <li class="liClear">
            <label for="txtDtCadas" class="lblObrigatorio">Data de Cadastro</label>
            <asp:TextBox ID="txtDtCadas" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data de Cadastro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDtCadas"
                ErrorMessage="Data de Cadastro é requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
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
