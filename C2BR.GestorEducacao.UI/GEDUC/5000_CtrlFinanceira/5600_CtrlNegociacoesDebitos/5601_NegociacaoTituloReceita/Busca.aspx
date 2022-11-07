<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5600_CtrlNegociacoesDebitos.F5601_NegociacaoTituloReceita.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */              
        .direita { text-align: right;}
        .ddlTipoFonte {width:80px;}  
        .ddlNomeFonte {width:226px;}  
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label id="Label1" title="Unidade/Escola" runat="server">
            Unidade/Escola</label>
        <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" 
            CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
        </asp:DropDownList>        
    </li>          
    <li>
        <label for="ddlTipoFonte" class="lblObrigatorio" title="Tipo da Fonte de Receita">TFR</label>
        <asp:DropDownList ID="ddlTipoFonte" CssClass="ddlTipoFonte" runat="server" ToolTip="Selecione o Tipo da Fonte" 
            AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFonte_SelectedIndexChanged">
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="R" Selected="True">Responsável</asp:ListItem>
            <asp:ListItem Value="O">Outro</asp:ListItem>
        </asp:DropDownList>
    </li>    
    <li id="liNomeFonte" runat="server">
        <label for="ddlNomeFonte" class="lblObrigatorio" title="Nome da Fonte">Nome</label>
        <asp:DropDownList ID="ddlNomeFonte" CssClass="ddlNomeFonte" runat="server" ToolTip="Selecione o Nome da Fonte" 
            OnSelectedIndexChanged="ddlNomeFonte_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:RequiredFieldValidator ControlToValidate="ddlNomeFonte" ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Nome deve ser informado" Display="None"></asp:RequiredFieldValidator>
    </li>
    <li id="liCodigoFonte" runat="server">
        <label for="txtCodigoFonte" title="Código da Fonte">Código</label>
        <asp:TextBox ID="txtCodigoFonte" CssClass="txtCodigoFonte campoNumerico" runat="server" Enabled="false"></asp:TextBox>
    </li>    
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label title="Unidade de Contrato" runat="server">
            Unidade de Contrato</label>
        <asp:DropDownList ID="ddlUnidadeContrato" ToolTip="Selecione a Unidade de Contrato" 
            CssClass="ddlUnidadeEscolar" runat="server">
        </asp:DropDownList>        
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>