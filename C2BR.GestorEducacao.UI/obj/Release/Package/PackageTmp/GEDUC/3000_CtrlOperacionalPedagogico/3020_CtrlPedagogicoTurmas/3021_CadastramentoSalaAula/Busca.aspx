<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3021_CadastramentoSalaAula.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTipoSala{width:90px;}
        .ddlUnidade {width:210px;}
        .txtDescSala {width:125px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade">
                Unidade de Ensino</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" 
                ToolTip="Selecione a Unidade Escolar">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTipoSala">Tipo de Sala</label>
            <asp:DropDownList ID="ddlTipoSala" CssClass="ddlTipoSala" runat="server" ToolTip="Selecione o Tipo de Sala" Width="131px">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aula</asp:ListItem>
                <asp:ListItem Value="L">Laboratório</asp:ListItem>
                <asp:ListItem Value="E">Estudo</asp:ListItem>
                <asp:ListItem Value="M">Monitoria</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both;">
            <label for="txtDescSala">Descrição</label>
            <asp:TextBox ID="txtDescSala" CssClass="txtDescSala" runat="server" MaxLength="60"></asp:TextBox>            
        </li>
    </ul>
</asp:Content>