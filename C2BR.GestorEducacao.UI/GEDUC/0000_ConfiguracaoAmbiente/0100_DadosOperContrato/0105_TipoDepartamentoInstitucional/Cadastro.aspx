<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0105_TipoDepartamentoInstitucional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 345px;
        }
        
        .clear
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlSitua{width:auto;}
        .ddlClass{width:auto;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtNome" title="Nome do Tipo de Departamento/Local" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o nome para o tipo de Departamento/Local"
                runat="server" CssClass="txtNome" MaxLength="50" Width="360px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNome"
                ErrorMessage="O nome deve ter no m�ximo 50 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNome"
                CssClass="validatorField" ErrorMessage="O nome deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="clear" style="margin-top:-15px;">
            <label for="txtDescricao" title="Descri��o do Tipo de Departamento/Local">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a descri��o do tipo de Departamento/Local"
                runat="server" TextMode="MultiLine" Width="360px" Rows="4" CssClass="txtDescricao" MaxLength="200"></asp:TextBox>
        </li>
        <li class="clear">
            <label for="ddlClass" title="Classifica��o do Departamento/Local" class="lblObrigatorio">
                Classifica��o</label>
            <asp:DropDownList CssClass="ddlClass" ID="ddlClass" runat="server" ToolTip="Selecione a classifica��o atual para o tipo de Departamento/Local">
                <asp:ListItem Value="" Text="Selecione" Selected="true"></asp:ListItem>
                <asp:ListItem Value="ACO" Text="Acomoda��o"></asp:ListItem>
                <asp:ListItem Value="ADM" Text="Administrativo"></asp:ListItem>
                <asp:ListItem Value="ATE" Text="Atendimento"></asp:ListItem>
                <asp:ListItem Value="FIN" Text="Financeiro"></asp:ListItem>
                <asp:ListItem Value="OPE" Text="Operacional"></asp:ListItem>
                <asp:ListItem Value="TEC" Text="T�cnico"></asp:ListItem>                              
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px;">
            <label for="ddlSitua" title="Situa��o do tipo de Departamento/Local" class="lblObrigatorio">
                Situa��o</label>
            <asp:DropDownList CssClass="ddlSitua" ID="ddlSitua" runat="server" ToolTip="Selecione a situa��o atual para o tipo de Departamento/Local">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
