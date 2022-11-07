<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0504_CadastroGrafico.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 300px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}              
        .liClear{ clear: both;}
        
        /*--> CSS Dados */
        .txtQueryGrafi { width: 275px; height: 200px; }
        .txtTitulGrafi { width: 275px; }
        .ddlModulo { width: 278px; }
        .ddlStatus, .ddlTipoGrafi { width: 60px; }
        .ddlClassiGrafi { width: 85px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label title="Módulo">
                Módulo</label>
            <asp:DropDownList ID="ddlModulo" ToolTip="Selecione o Módulo" CssClass="ddlModulo" runat="server" AutoPostBack="true" onselectedindexchanged="ddlModulo_SelectedIndexChanged">                  
            </asp:DropDownList>
        </li>
        <li>
            <label title="Grupo de Informações">
                Grupo de Informações</label>
            <asp:DropDownList ID="ddlGrupoInfor" ToolTip="Selecione o Grupo de Informação" CssClass="ddlModulo" runat="server">                              
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtTitulGrafi" title="Título do Gráfico" class="lblObrigatorio">Título do Gráfico</label>
            <asp:TextBox ID="txtTitulGrafi" ToolTip="Informe o Título do Gráfico" runat="server" MaxLength="80" CssClass="txtTitulGrafi"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtTitulGrafi"
                ErrorMessage="Título do Gráfico deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtQueryGrafi" title="Query do Gráfico" class="lblObrigatorio">Query</label>
            <asp:TextBox ID="txtQueryGrafi" ToolTip="Informe a Descrição" runat="server" TextMode="MultiLine" CssClass="txtQueryGrafi"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="txtQueryGrafi"
                ErrorMessage="Query do Gráfico deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipoGrafi" title="Tipo do Gráfico" class="lblObrigatorio">Tipo</label>
            <asp:DropDownList ID="ddlTipoGrafi" ToolTip="Selecione o Tipo do Gráfico" runat="server" CssClass="ddlTipoGrafi">
                <asp:ListItem Text="Coluna" Value="C"></asp:ListItem>
                <asp:ListItem Text="Pirâmide" Value="P"></asp:ListItem>
                <asp:ListItem Text="Pizza" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField"  runat="server" ControlToValidate="ddlTipoGrafi"
                ErrorMessage="Tipo do Gráfico deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlClassiGrafi" title="Classificação do Gráfico" class="lblObrigatorio">Classificação</label>
            <asp:DropDownList ID="ddlClassiGrafi" AutoPostBack="true" onselectedindexchanged="ddlClassiGrafi_SelectedIndexChanged" 
            ToolTip="Selecione a Classicação do Gráfico" runat="server" CssClass="ddlClassiGrafi">
                <asp:ListItem Text="Restrito" Value="R"></asp:ListItem>
                <asp:ListItem Text="Geral" Selected="true" Value="G"></asp:ListItem>
                <asp:ListItem Text="Principal" Value="P"></asp:ListItem>
                <asp:ListItem Text="Gerencial" Value="X"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="validatorField"  runat="server" ControlToValidate="ddlClassiGrafi"
                ErrorMessage="Classicação do Gráfico deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlStatus" title="Status" class="lblObrigatorio">Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"  runat="server" ControlToValidate="ddlStatus"
                ErrorMessage="Status deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
