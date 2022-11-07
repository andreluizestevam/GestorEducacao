<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0200_ConfiguracaoModulos._0204_CadastroBoletim.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        /* ul principal do formulário */
        .ulDados{width: 340px;}        
        .ulDados input{ margin-bottom: 0;}
        .ulDados li{ margin-bottom: 10px;}        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>  
            <label class="lblObrigatorio" title="Informa o nome do Boletim">Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o nome do Boletim" Width="160" runat="server" CssClass="txtNome" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtNome" runat="server" ControlToValidate="txtNome"
                    ErrorMessage="Nome do boletim deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Informa a Classe do Boletim">Classe</label>
            <asp:TextBox ID="txtClasse" ToolTip="Informe a Classe do Boletim" Width="160" runat="server" CssClass="txtClasse" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClasse"
                    ErrorMessage="Classe do boletim deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Informa a Descrição do Boletim">Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição do Boletim" Width="340px" runat="server" CssClass="txtDescricao" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDescricao" runat="server" ControlToValidate="txtDescricao"
                    ErrorMessage="Descricao do boletim deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Selecione o status do Boletim" >Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" ToolTip="Selecione o status da funcionalidade">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlStatus" runat="server" ControlToValidate="ddlStatus"
                    ErrorMessage="O Status deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
