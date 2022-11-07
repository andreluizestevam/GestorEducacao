<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2903_TipoBolsaEscolar.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .ulDados li input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li { margin-bottom: 10px; }
        .liClear { clear: both; }
        
        /*--> CSS dados */
        .txtValorDescto { width: 70px; }
        .check label{ display: inline; margin-left: -5px; }
        .check input{ margin-left: -5px; } 
        .txtDescricao { width: 295px; }
        .ddlTipo { width: 70px; }
        .ddlGrupoBolsa, .txtNome { width: 150px; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li class="liClear">
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo" >
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" runat="server" CssClass="ddlTipo">
                <asp:ListItem Value="B" Selected="True">Bolsa</asp:ListItem>
                <asp:ListItem Value="C">Conv�nio</asp:ListItem>
            </asp:DropDownList>
        </li>     
        <li style="margin-left: 5px;">
            <label for="ddlGrupoBolsa" title="Agrupador da Bolsa/Conv�nio" >
                Agrupador</label>
            <asp:DropDownList ID="ddlGrupoBolsa" ToolTip="Selecione o Agrupador da Bolsa/Conv�nio" runat="server" CssClass="ddlGrupoBolsa">
            </asp:DropDownList>
        </li> 
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Nome">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Agrupador da Bolsa/Conv�nio de Estudo" runat="server" MaxLength="20" CssClass="txtNome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNome"
                ErrorMessage="Nome deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
            <label for="txtValorDescto" title="Valor Desconto">Valor</label>
            <asp:TextBox ID="txtValorDescto" CssClass="txtValorDescto campoMoeda" runat="server" ToolTip="Informe o Valor do Desconto"></asp:TextBox>
            <asp:CheckBox ID="chkValorDesctoPercentual" CssClass="check" runat="server" Text="%" ToolTip="Marque se o valor for percentual"/>
        </li>
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o da Bolsa/Conv�nio de Estudo" runat="server" MaxLength="200" CssClass="txtDescricao"></asp:TextBox>            
        </li>    
        <li class="liClear">
            <label for="txtDtInicioBolsa" title="Data de In�cio da Bolsa">Data In�cio</label>
            <asp:TextBox ID="txtDtInicioBolsa" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data de In�cio da Bolsa"></asp:TextBox>
        </li>      
        <li style="margin-left: 5px;">
            <label for="txtDtFinalBolsa" title="Data de In�cio da Bolsa">Data Fim</label>
            <asp:TextBox ID="txtDtFinalBolsa" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data de Fim da Bolsa"></asp:TextBox>
        </li>              
        <li class="liClear">
            <label for="txtDtCadas" class="lblObrigatorio">Data de Cadastro</label>
            <asp:TextBox ID="txtDtCadas" Enabled="False" CssClass="campoData" runat="server" MaxLength="8" ToolTip="Data de Cadastro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDtCadas"
                ErrorMessage="Data de Cadastro � requerida" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 5px;">
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
