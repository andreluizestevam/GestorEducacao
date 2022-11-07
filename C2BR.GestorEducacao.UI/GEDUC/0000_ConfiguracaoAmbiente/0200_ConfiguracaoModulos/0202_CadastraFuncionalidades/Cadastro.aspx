<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0202_CadastraFuncionalidades.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        .ulDados{width: 340px;}        
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px;}        
        #liNomModuloGerenFuncCF, #liDesGerenFuncCF, .liClear { clear: both; }
        .liOrdemMenuFuncCF { margin-right:16px; margin-left:10px; }
        #liBarraTituloFuncCF { background-color: #EEEEEE;margin-top:5px; margin-bottom: 2px; padding: 5px; text-align: center; width: 325px; height:10px; clear:both}        
        
        /*--> CSS DADOS */               
        .txtNomeFuncCFU { width: 290px; }
        .txtNomeItemMenuFuncCFU { width: 330px;}
        .ddlModuloPaiFuncCFU { width: 330px;}
        .txtDescricaoFuncCFU { width: 330px; }
        .txtNomUrlModuloFuncCFU {width:330px;}
        .txtOrdemMenuFuncCFU { width: 40px;}
        .ddlTipoIconeFuncCFU {width:60px; margin-right:10px;}
        .txtIconeFuncCFU { width: 240px;}
        .txtNomeGerencialFuncCFU { width:330px;}
        .txtDescGerenFuncCFU {width:330px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">  
            <label class="lblObrigatorio" title="Módulo Pai">Módulo Pai</label>
            <asp:DropDownList ID="ddlModuloPaiFuncCFU"  runat="server" CssClass="ddlModuloPaiFuncCFU" ToolTip="Módulo Pai da funcionalidade"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModuloPaiFuncCFU"
                    ErrorMessage="Módulo Pai deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Código">Código</label>
            <asp:TextBox CssClass="campoCodigo" ID="txtCodigoFuncCFU" runat="server" MaxLength="4" Enabled="False" ToolTip="Código da funcionalidade"></asp:TextBox>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Nome da Funcionalidade">Nome da Funcionalidade</label>
            <asp:TextBox ID="txtNomeFuncCFU" runat="server" CssClass="txtNomeFuncCFU" MaxLength="100" ToolTip="Nome da funcionalidade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeFuncCFU"
                    ErrorMessage="Nome da funcionalidade deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">  
            <label class="lblObrigatorio" title="Título da Funcionalidade">Título da Funcionalidade</label>
            <asp:TextBox CssClass="txtNomeItemMenuFuncCFU" ID="txtNomeItemMenuFuncCFU" runat="server" MaxLength="100" ToolTip="Título da funcionalidade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeItemMenuFuncCFU"
                    ErrorMessage="O Título da funcionalidade deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Descrição da Funcionalidade">Descrição da Funcionalidade</label>
            <asp:TextBox ID="txtDescricaoFuncCFU" runat="server" CssClass="txtDescricaoFuncCFU" MaxLength="150" ToolTip="Descrição da funcionalidade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescricaoFuncCFU"
                    ErrorMessage="A Descrição da funcionalidade deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" title="URL da Funcionalidade">  
            <label class="lblObrigatorio">URL da Funcionalidade</label>
            <asp:TextBox ID="txtNomUrlModuloFuncCFU" runat="server" CssClass="txtNomUrlModuloFuncCFU" MaxLength="255" ToolTip="Endereço da URL da funcionalidade"></asp:TextBox>
        </li>
        <li id="chkFlagInforGerenCFU" runat="server" >
            <asp:CheckBox ID="chkInforGerenFuncCFU" runat="server"  CssClass="chkFlagInforGerenFuncCFU" Text="Gerencial" ToolTip="Funcionalidade Gerencial"
                oncheckedchanged="chkInforGerenFuncCFU_CheckedChanged" TextAlign="Left" />    
        </li>
        <li class="liOrdemMenuFuncCFU">  
            <label class="lblObrigatorio" title="Ordem">Ordem</label>
            <asp:TextBox ID="txtOrdemMenuFuncCFU" runat="server" CssClass="txtOrdemMenuFuncCFU" ToolTip="Ordem no Menu"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOrdemMenuFuncCFU"
                    ErrorMessage="A Ordem do menu deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>  
            <label class="lblObrigatorio" title="Tipo Ícone">Tipo Ícone</label>
            <asp:DropDownList ID="ddlTipoIconeFuncCFU" runat="server" CssClass="ddlTipoIconeFuncCFU" ToolTip="Tipo do Ícone da funcionalidade">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Cadastro" Value="C"></asp:ListItem>
                <asp:ListItem Text="Relatório" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="lblObrigatorio" title="Status" >Status</label>
            <asp:DropDownList ID="ddlStatusFuncCFU" runat="server" CssClass="ddlStatusFuncCFU" ToolTip="Status da funcionalidade">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStatusFuncCFU"
                    ErrorMessage="O Status deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Nome do Ícone">Nome do Ícone</label>
            <asp:TextBox ID="txtIconeFuncCFU" runat="server" CssClass="txtIconeFuncCFU" MaxLength="60" ToolTip="Nome do ícone da funcionalidade"></asp:TextBox> 
        </li>
        <li id="liBarraTituloFuncCFU" runat="server">
            <label title="Informação Gerencial">INFORMAÇÃO GERENCIAL</label>
        </li>
        <li id="liNomModuloGerenFuncCFU" runat="server">  
            <label title="Nome Gerencial">Nome Gerencial</label>
            <asp:TextBox ID="txtNomeGerencialFuncCFU" runat="server" CssClass="txtNomeGerencialFuncCFU" MaxLength="255" ToolTip="Nome gerencial da funcionalidade"></asp:TextBox>
        </li>
        <li id="liDesGerenFuncCFU" runat="server">  
            <label title="Descrição Gerencial">Descrição Gerencial</label>
            <asp:TextBox ID="txtDescGerenFuncCFU" runat="server" CssClass="txtDescGerenFuncCFU" MaxLength="255" ToolTip="Descrião gerencial da funcionalidade"></asp:TextBox>
        </li>
        <li id="liTipoIconeGerenFuncCFU" runat="server">  
            <label title="Tipo Ícone">Tipo Ícone</label>
            <asp:DropDownList ID="ddlTipoIconeGerenFuncCFU" runat="server" CssClass="ddlTipoIconeGerenFuncCFU" ToolTip="Tipo de ícone da funcionalidade gerencial">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="Cadastro" Value="C"></asp:ListItem>
                <asp:ListItem Text="Relatório" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtOrdemMenuFuncCFU").mask("?99");
        });
    </script>
</asp:Content>
