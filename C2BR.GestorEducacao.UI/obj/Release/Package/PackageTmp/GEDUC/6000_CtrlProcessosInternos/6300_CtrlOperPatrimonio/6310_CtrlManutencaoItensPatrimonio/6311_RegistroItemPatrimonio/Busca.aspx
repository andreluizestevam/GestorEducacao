<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6310_CtrlManutencaoItensPatrimonio.F6311_RegistroItemPatrimonio.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="txtCodPatrimonio"  title="Código do patrimônio">Cód. Patrimônio</label>
            <asp:TextBox ID="txtCodPatrimonio" CssClass="txtCodPatrimonio" runat="server" 
                ToolTip="Informe o código do patrimônio." MaxLength="15" Width="85px"/>
        </li>
        <li>
            <label for="ddlTipoPatrimonio" title="Tipo do patrimônio">Tipo do patrimônio</label>
            <asp:DropDownList ID="ddlTipoPatrimonio" CssClass="ddlTipoPatrimonio" 
                runat="server" ToolTip="Infome o tipo do patrimônio" 
                AutoPostBack="True" Width="140px" 
              onselectedindexchanged="ddlTipoPatrimonio_SelectedIndexChanged" >              
            </asp:DropDownList>
        
        </li>
        <li>
            <label for="ddlGrupo" title="Grupo do patrimônio">
                Grupo</label>
            <asp:DropDownList ID="ddlGrupo" CssClass="ddlGrupo" runat="server"
                ToolTip="Infome a grupo do patrimônio" Width="150px" 
              AutoPostBack="true" onselectedindexchanged="ddlGrupo_SelectedIndexChanged" />        
        </li>
        <li>
            <label for="ddlSubGrupo"  title="SubGrupo do patrimônio">
                SubGrupo</label>
            <asp:DropDownList ID="ddlSubGrupo" CssClass="ddlSubGrupo" runat="server"
                ToolTip="Infome o subgrupo do patrimônio" Width="150px" />        
        </li>
        <li>
            <label for="ddlUnidadePatrimonio" title="Unidade">Unidade</label>
            <asp:DropDownList ID="ddlUnidadePatrimonio" CssClass="ddlUnidadePatrimonio" 
                runat="server" ToolTip="Infome a unidade do patrimônio." AutoPostBack="True" 
                onselectedindexchanged="ddlUnidadePatrimonio_SelectedIndexChanged" />
                <asp:RequiredFieldValidator ID="rfvUnidadePatrimonio" runat="server" ControlToValidate="ddlUnidadePatrimonio"
                        ErrorMessage="A Unidade deve ser selecionada." Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlDeptoAtual" title="Departamento atual">Departamento</label>
            <asp:DropDownList ID="ddlDeptoAtual" CssClass="ddlDeptoAtual" runat="server" 
                ToolTip="Infome o departamento atual do patrimônio" AutoPostBack="True" Width="80px" >
                <asp:ListItem Selected="True" Value="-1">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel> 
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
