<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0201_CadastraModulos.Cadastro"
    Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        .ulDados{width: 345px;}        
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs*/
        .ulDados li{ margin-bottom: 10px;}
        .liClear{ clear: both;}
               
        /*--> CSS DADOS */
        .txtNomeModCMF{ width: 290px; }
        .txtNomeItemMenuModCMF{ width: 330px;}
        .ddlModuloPaiCMF{ width: 330px;}
        .txtDescricaoModCMF{ width: 330px;}
        .txtOrdemMenuModCMF{ width: 40px;}
        .ddlTipoItemSubmenuModCMF{ width: 50px;}
        .txtIconeModCMF{ width: 237px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label>Código</label>
            <asp:TextBox CssClass="campoCodigo" ID="txtCodigoModCMF" runat="server" MaxLength="4" Enabled="False" ToolTip="Código do Módulo"></asp:TextBox>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Nome">Nome</label>
            <asp:TextBox ID="txtNomeModCMF" ToolTip="Informe o Nome" runat="server" CssClass="txtNomeModCMF" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeModCMF"
                    ErrorMessage="Nome do módulo deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">  
            <label class="lblObrigatorio" title="Título">Título</label>
            <asp:TextBox CssClass="txtNomeItemMenuModCMF" ToolTip="Informe o Título" ID="txtNomeItemMenuModCMF" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeItemMenuModCMF"
                    ErrorMessage="O Título do módulo deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">  
            <label title="Pai">Pai</label>
            <asp:DropDownList ID="ddlModuloPaiCMF" ToolTip="Selecione o Pai" runat="server" CssClass="ddlModuloPaiCMF"></asp:DropDownList>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtDescricaoModCMF" runat="server" ToolTip="Informe a Descrição" CssClass="txtDescricaoModCMF" MaxLength="150"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescricaoModCMF"
                    ErrorMessage="A Descrição do módulo deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">  
            <label class="lblObrigatorio" title="Ordem no Menu">Ordem no Menu</label>
            <asp:TextBox ID="txtOrdemMenuModCMF" ToolTip="Informe a Ordem no Menu" runat="server" CssClass="txtOrdemMenuModCMF" MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOrdemMenuModCMF"
                    ErrorMessage="A Ordem do menu deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Item do Submenu">Item do Submenu</label>
            <asp:DropDownList ID="ddlTipoItemSubmenuModCMF" ToolTip="Selecione o Item do Submenu" runat="server" CssClass="ddlTipoItemSubmenuModCMF">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="ATM" Value="ATM"></asp:ListItem>
                <asp:ListItem Text="LST" Value="LST"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTipoItemSubmenuModCMF"
                    ErrorMessage="O Item do SubMenu deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatusModCMF" ToolTip="Selecione o Status" runat="server" CssClass="ddlStatusModCMF">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStatusModCMF"
                    ErrorMessage="O Status deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">  
            <label title="Ícone">Ícone</label>
            <asp:TextBox ID="txtIconeModCMF" ToolTip="Informe o Ícone" runat="server" CssClass="txtIconeModCMF" MaxLength="60"></asp:TextBox> 
        </li>
    </ul>
</asp:Content>