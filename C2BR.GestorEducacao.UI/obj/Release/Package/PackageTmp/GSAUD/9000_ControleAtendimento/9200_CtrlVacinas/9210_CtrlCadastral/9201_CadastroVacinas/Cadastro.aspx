<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9210_CtrlCadastral._9201_CadastroVacinas.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
            margin: 50px 0 0 347px !important;
        }
        .ulDados li
        {
            margin-left:5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .lblTop
        {
            font-size: 9px;
            color: #436EEE;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoSitua" />
    <div style="width:100px; margin: 50px 0 -165px 247px !important;">
        <fieldset style="border:0 none;">
            <uc1:ControleImagem ID="upImageVacina" runat="server" />
        </fieldset>
    </div>
    <ul class="ulDados">
        <li>
            <label title="Nome da Vacina" class="lblObrigatorio">Nome</label>
            <asp:TextBox runat="server" ID="txtNome" Width="220px" MaxLength="80" ToolTip="Nome da Vacina"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvNome" ControlToValidate="txtNome" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Sigla da Vacina" class="lblObrigatorio">Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="59px" MaxLength="12" ToolTip="Sigla da Vacina"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvSigla" ControlToValidate="txtSigla" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label title="Descrição da Vacina" class="lblObrigatorio">Descrição</label>
            <asp:TextBox runat="server" ID="txtDescricao" Width="50px" ToolTip="Descrição da Vacina"
                TextMode="MultiLine" Style="width: 300px; height: 64px;"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvDescricao" ControlToValidate="txtDescricao" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <ul class="ulDados" style="margin: 170px 0 0 247px !important;">
        <li style="clear:both; margin-bottom:5px;">
            <label class="lblTop">DADOS TÉCNICOS</label>
        </li>
        <li style="clear:both;">
            <label title="Código do Laboratório">Código Laboratório</label>
            <asp:TextBox ID="txtCodigoLaboratorio" runat="server" Width="85px" ToolTip="Código do Laboratório" Enabled="false" />
        </li>
        <li>
            <label title="Laboratório Fornecedor da Vacina" class="lblObrigatorio">Laboratório</label>
            <asp:DropDownList runat="server" ID="drpLaboratorio" Width="140px" ToolTip="Laboratório Fornecedor da Vacina" 
                OnSelectedIndexChanged="drpLaboratorio_SelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator runat="server" ID="rfvLaboratorio" ControlToValidate="drpLaboratorio" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:13px;">
            <label title="Origem Fabricação" class="lblObrigatorio">Fabricação</label>
            <asp:DropDownList runat="server" ID="drpFabricacao" Width="70px" ToolTip="Origem Fabricação">
                <asp:ListItem Value="" Text="Selecione" Selected="True" />
                <asp:ListItem Value="I" Text="Importada" />
                <asp:ListItem Value="N" Text="Nacional" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvFabricacao" ControlToValidate="drpFabricacao" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:7px;">
            <label title="País de Procedência" class="lblObrigatorio">Procedência</label>
            <asp:DropDownList runat="server" ID="drpProcedencia" Width="80px" ToolTip="País de Procedência" />
            <asp:RequiredFieldValidator runat="server" ID="rfvProcedencia" ControlToValidate="drpProcedencia" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label title="Código de Barras">Código Barras - Laboratório</label>
            <asp:TextBox ID="txtCodigoBarrasLaboratorio" Width="180px" runat="server" ToolTip="Código de Barras" />
        </li>
        <li style="margin-left:77px;">
            <label title="Tipo de Embalagem" class="lblObrigatorio">Tipo Embalagem</label>
            <asp:DropDownList runat="server" ID="drpTipoEmbalagem" Width="80px" ToolTip="Tipo de Embalagem" />
            <asp:RequiredFieldValidator runat="server" ID="rfvTipoEmbalagem" ControlToValidate="drpTipoEmbalagem" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:-2px;">
            <label title="Quantidade de Doses" class="lblObrigatorio">Qtd. Doses</label>
            <asp:TextBox ID="txtQtDoses" Width="40px" runat="server" ToolTip="Quantidade de Doses" />
            <asp:RequiredFieldValidator runat="server" ID="rfvQtDoses" ControlToValidate="txtQtDoses" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label title="Código de Barras">Código Barras - Estoque/Inventário</label>
            <asp:TextBox ID="txtCodigoBarrasEstoque" Width="180px" runat="server" ToolTip="Código de Barras" />
        </li>
        <li style="margin-left:77px;">
            <label title="Unidade do Item" class="lblObrigatorio">Unidade Item</label>
            <asp:DropDownList runat="server" ID="drpUnidadeItem" Width="80px" ToolTip="Unidade do Item" />
            <asp:RequiredFieldValidator runat="server" ID="rfvUnidade" ControlToValidate="drpUnidadeItem" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left:-2px;">
            <label title="Capacidade em ML" class="lblObrigatorio">Capacidade em ML</label>
            <asp:TextBox ID="txtCapacidade" Width="65px" runat="server" ToolTip="Capacidade em ML" />
            <asp:RequiredFieldValidator runat="server" ID="rfvCapacidade" ControlToValidate="txtCapacidade" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both;">
            <label title="Código de Barras">Código Barras - Armazenagem</label>
            <asp:TextBox ID="txtCodigoBarrasArmazenagem" Width="180px" runat="server" ToolTip="Código de Barras" />
        </li>
        <li style="margin-left:77px;">
            <label title="Código ANVISA">Código ANVISA</label>
            <asp:TextBox ID="txtCodigoANVISA" Width="65px" runat="server" ToolTip="Código ANVISA" />
        </li>
        <li style="margin-left:20px;">
            <label title="Código MS/SUS">Código MS/SUS</label>
            <asp:TextBox ID="txtCodigoMSSUS" Width="65px" runat="server" ToolTip="Código MS/SUS" />
        </li>
        <li style="clear:both; margin-bottom:5px;">
            <label class="lblTop">DADOS DE ARMAZENAGEM</label>
        </li>
        <li style="clear:both;">
            <label title="Tipo de Armazenagem" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList runat="server" ID="drpTipoArmazenagem" Width="60px" ToolTip="Tipo de Armazenagem" />
            <asp:RequiredFieldValidator runat="server" ID="rfvTipoArmazenagem" ControlToValidate="drpTipoArmazenagem" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Quantidade de Doses Armazenagem">Qtd. Doses</label>
            <asp:TextBox ID="txtQtdDosesArmazenagem" Width="40px" runat="server" ToolTip="Quantidade de Doses Armazenagem" />
        </li>
        <li>
            <label title="Unidade de Armazenagem" class="lblObrigatorio">Unidade</label>
            <asp:DropDownList runat="server" ID="drpUnidadeArmazenagem" Width="80px" ToolTip="Unidade de Armazenagem" />
            <asp:RequiredFieldValidator runat="server" ID="rfvUnidadeArmazenagem" ControlToValidate="drpUnidadeArmazenagem" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Empilhamento Máximo">Empilhamento Máximo</label>
            <asp:TextBox ID="txtEmpilhamento" Width="60px" ToolTip="Empilhamento Máximo" runat="server" />
        </li>
        <li style="clear:both;">
            <label title="Temperatura Média">Temperatura Média</label>
            <asp:TextBox ID="txtTemperaturaMedia" Width="60px" ToolTip="Temperatura Média" runat="server" />
        </li>
        <li>
            <label title="Observações">Observações</label>
            <asp:TextBox ID="txtObservacoes" Width="200px" ToolTip="Observações" runat="server" />
        </li>
    </ul>
    <ul class="ulDados" style="margin: -52px -390px 0 0 !important; float:right;">
        <li style="clear:both; margin-bottom:5px;">
            <label class="lblTop">DADOS DA APLICAÇÃO</label>
        </li>
        <li style="clear:both;">
            <label title="Via de Aplicação" class="lblObrigatorio">Via de Aplicação</label>
            <asp:DropDownList runat="server" ID="drpViaAplicacao" Width="90px" ToolTip="Via de Aplicação" 
                OnSelectedIndexChanged="drpViaAplicacao_SelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator runat="server" ID="rfvViaAplicacao" ControlToValidate="drpViaAplicacao" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both; margin-top:6px;">
            <label title="Local de Aplicação" class="lblObrigatorio">Local de Aplicação</label>
            <asp:DropDownList runat="server" ID="drpLocalAplicacao" Width="90px" ToolTip="Local de Aplicação" />
            <asp:RequiredFieldValidator runat="server" ID="rfvLocalAplicacao" ControlToValidate="drpLocalAplicacao" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <ul class="ulDados" style="margin: 170px 0 0 247px !important;">
        <li style="clear:both;">
            <label title="Situação da Vacina" class="lblObrigatorio">Situação</label>
            <asp:DropDownList runat="server" ID="drpSituacao" Width="60px" ToolTip="Situação da Vacina">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvSituacao" ControlToValidate="drpSituacao" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Data de Cadastro da Vacina">Data Cadastro</label>
            <asp:TextBox runat="server" ID="txtDtCadas" CssClass="campoData" ToolTip="Data de Cadastro da Vacina" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label title="Data de Cadastro da Vacina">Data Situação</label>
            <asp:TextBox runat="server" ID="txtDtSitua" CssClass="campoData" ToolTip="Data de Situação da Vacina" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin-left:105px">
            <label title="Quantidade de doses para aplicação / Intervalo entre cada aplicação">Qtd. Doses / Intervalo</label>
            <asp:DropDownList runat="server" ID="drpQtdDosesAplicacao" Width="35px" ToolTip="Quantidade de doses para aplicação">
                <asp:ListItem Value="1" Text="1" Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                <asp:ListItem Value="9" Text="9+"></asp:ListItem>
            </asp:DropDownList>  / 
            <asp:TextBox ID="txtIntervaloAplicacao" Width="40px" ToolTip="Intervalo entre cada aplicação" runat="server" />
        </li>
    </ul>
</asp:Content>
