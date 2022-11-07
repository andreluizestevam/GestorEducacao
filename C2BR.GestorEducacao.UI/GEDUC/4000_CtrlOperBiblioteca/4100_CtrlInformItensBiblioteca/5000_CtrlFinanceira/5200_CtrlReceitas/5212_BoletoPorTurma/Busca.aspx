<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5212_BoletoPorTurma.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome { width: 210px; }
        .ddlTipo { width: 65px; }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlAno" title="Ano">
            Ano</label>
        <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
            ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="txtUnidade" title="Unidade/Escola">
            Unidade/Escola</label>
        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlUnidadeContrato" title="Selecione a Unidade de Contrato">
        Unidade de Contrato</label>
        <asp:DropDownList ID="ddlUnidadeContrato" ToolTip="Selecione a Unidade de Contrato" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="ddlUnidadeContrato_OnSelectedIndexChanged">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlModalidade" title="Selecione a moalidade">
        Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a modalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" >
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlSerie" title="Selecione a série">
        S&eacute;rie</label>
        <asp:DropDownList ID="ddlSerie" ToolTip="Selecione a série" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="ddlSerie_OnSelectedIndexChanged">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlTurma" title="Selecione a turma">
        Turma</label>
        <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a turma" runat="server">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlTipo" title="Tipo da Fonte de Receita">Tipo</label>
        <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" CssClass="ddlTipo" runat="server">
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="O">Não Aluno</asp:ListItem>
        </asp:DropDownList>
    </li>
    <!--li>
        <label for="ddlTipoTaxaBoleto" title="Tipo de Taxa do Boleto">Tipo</label>
        <asp:DropDownList ID="ddlTipoTaxaBoleto" runat="server" CssClass="ddlTipoTaxaBoleto"
            ToolTip="Selecione o Tipo de Taxa do Boleto" AutoPostBack="true" onselectedindexchanged="ddlTipoTaxaBoleto_SelectedIndexChanged">
            <asp:ListItem Value="" Selected="true">Selecione</asp:ListItem>
            <asp:ListItem Value="M">Matricula</asp:ListItem>
            <asp:ListItem Value="R">Renovação</asp:ListItem>
            <asp:ListItem Value="E">Mensalidade</asp:ListItem>
            <asp:ListItem Value="A">Atividades Extras</asp:ListItem>
            <asp:ListItem Value="B">Biblioteca</asp:ListItem>
            <asp:ListItem Value="S">Serv. de Secretaria</asp:ListItem>
            <asp:ListItem Value="D">Serv. Diversos</asp:ListItem>
            <asp:ListItem Value="N">Negociação</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlBoleto" title="Selecione o boleto">
        Boleto</label>
        <asp:DropDownList ID="ddlBoleto" ToolTip="Selecione o boleto" runat="server">
        </asp:DropDownList>
    </li-->
</ul>
</asp:Content>