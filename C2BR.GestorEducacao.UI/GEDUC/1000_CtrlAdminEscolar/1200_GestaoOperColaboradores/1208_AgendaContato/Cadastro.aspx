<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1208_AgendaContato.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 312px; }
        
        /*--> CSS LIs */
        .liColaborador, .liTipoOcorrencia, .liDataCadastro { clear: both; margin-top: 10px; }        
        .liClear { clear: both; }
        .liEspaco { margin-left: 5px; }
        
        /*--> CSS DADOS */
        .ddlUnidade, .ddlContato { width: 261px; }
        .txtNomeContato, .txtEmailContato { width: 258px; }
        .ddlTipoContato { width: 80px; }
        .ddlSexoContato { width:70px; } 
        .txtTelResidContato { width:78px; }    
        .txtApeliContato { width: 95px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlTipoContato" class="lblObrigatorio" title="Tipo de Contato">Tipo de Contato</label>
            <asp:DropDownList ID="ddlTipoContato" ToolTip="Selecione o Tipo de Contato" CssClass="ddlTipoContato" runat="server"
                AutoPostBack="true" onselectedindexchanged="ddlTipoContato_SelectedIndexChanged">
                <asp:ListItem Text="Funcionário" Value="F"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
                <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
                <asp:ListItem Text="Responsável" Value="R"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
        </li> 
        <li class="liClear" style="margin-top: 10px;">
            <label for="ddlUnidade" title="Unidade">Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li class="liClear" style="margin-top: 10px;">
            <label for="ddlContato" title="Contato">Contato</label>
            <asp:DropDownList ID="ddlContato" ToolTip="Selecione o Contato" CssClass="ddlContato" runat="server" AutoPostBack="true"
            onselectedindexchanged="ddlContato_SelectedIndexChanged">
            </asp:DropDownList>
        </li>   
        <li class="liClear" style="margin-top: 10px;">
            <label for="txtNomeContato" class="lblObrigatorio" title="Contato">Nome do Contato</label>
            <asp:TextBox ID="txtNomeContato" ToolTip="Informe o Nome do Contato" MaxLength="80" CssClass="txtNomeContato" runat="server"></asp:TextBox>
        </li>                     
        <li class="liClear">
            <label for="txtApeliContato" title="Apelido">Apelido</label>
            <asp:TextBox ID="txtApeliContato" MaxLength="20" ToolTip="Informe o Apelido do Contato" CssClass="txtApeliContato" runat="server"></asp:TextBox>
        </li>        
        <li class="liEspaco">
            <label for="txtDataNascto"  class="lblObrigatorio" title="Data de Nascimento do Contato">Data de Nascto</label>
            <asp:TextBox ID="txtDataNascto" ToolTip="Data de Nascimento do Contato" Enabled="false" CssClass="campoData" runat="server"></asp:TextBox>
        </li>   
        <li class="liEspaco">
            <label for="ddlSexoContato" class="lblObrigatorio" title="Sexo do Contato">Sexo</label>
            <asp:DropDownList ID="ddlSexoContato" CssClass="ddlSexoContato" runat="server"
                ToolTip="Selecione o Sexo do Funcionário">
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList>
        </li>       
        <li class="liClear">
            <label for="txtTelResidContato" title="Telefone Residencial">Tel. Residencial</label>
            <asp:TextBox ID="txtTelResidContato" 
                ToolTip="Informe o Telefone Residencial do Contato"
                CssClass="txtTelResidContato" runat="server"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label for="txtTelCelulContato" title="Telefone Celular">Tel. Celular</label>
            <asp:TextBox ID="txtTelCelulContato" 
                ToolTip="Informe o Telefone Celular do Contato"
                CssClass="txtTelResidContato" runat="server"></asp:TextBox>
        </li>   
        <li class="liEspaco">
            <label for="txtTelComerContato" title="Telefone Celular">Tel. Comercial</label>
            <asp:TextBox ID="txtTelComerContato" 
                ToolTip="Informe o Telefone Comercial do Contato"
                CssClass="txtTelResidContato" runat="server"></asp:TextBox>
        </li>    
        <li class="liClear">
            <label for="txtEmailContato" title="E-mail">E-mail</label>
            <asp:TextBox ID="txtEmailContato" 
                ToolTip="Informe o E-mail do Contato"
                CssClass="txtEmailContato" runat="server" MaxLength="100"></asp:TextBox>
        </li>  
    </ul>

<script type="text/javascript">
    $(document).ready(function () {
        $(".txtTelResidContato").mask("(99) 9999-9999");
    });
</script>
</asp:Content>
