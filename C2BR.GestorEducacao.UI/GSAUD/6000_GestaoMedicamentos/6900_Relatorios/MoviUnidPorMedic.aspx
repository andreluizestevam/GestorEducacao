<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MoviUnidPorMedic.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6900_Relatorios.MoviUnidPorMedic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados 
        {
             width: 275px;
            margin-top: 25px;
        }
        
        .ulDados li
        {
            margin-top: 6px;
            margin-left: 50px;
            margin-right: 0px !important;
        }
        
        .liboth
        {
            clear:both;
        }
        
        .ddltop
        {
            width:170px;
            clear:both;
        }
        .liUnidade,.liTipoRelatorio
        {
            margin-top: 5px;
            width: 200px;
        }
        .ddlTipo
        {
            width:132px;
            clear:both;
        }
        .ddlItem
        {
            width:150px;
        }
        .ddlGrItem
        {
            width:150px;
            clear:both;           
        }
        .ddlReg
        {
             width:90px;
            clear:both;         
        }
        .ddlArea
        {
            width:130px;
            clear:both;                     
        }
        .ddlSubArea
        {
            width:150px;
            clear:both;                     
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <ul class="ulDados">

    <li class="liboth">
        <label class="lblObrigatorio" for="txtTipoSolicitacao" title="Tipo de Relatório">Tipo de Relatório</label>
        <asp:DropDownList ID="ddlTipoRelatorio" class="ddlTipo" runat="server" AutoPostBack="True" 
        ToolTip="Selecione um Tipo de Relatório" onselectedindexchanged="ddlTipoRelatorio_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="U">Emissão Por Unidade</asp:ListItem>
            <asp:ListItem Value="M">Emissão Por Medicamento</asp:ListItem>
            </asp:DropDownList><br /><br/>
    </li>

        <li class="liboth">
        <asp:Label runat="server" ID="lblUnid">Unidade</asp:Label><br />
        <asp:DropDownList runat="server" class="ddltop" ID="ddlUnid" ToolTip="Selecine a Unidade"></asp:DropDownList><br /><br />
    </li>

    <li class="liboth">
        <asp:Label runat="server" id="lblInfoItem">Informações do Item</asp:Label>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblGrpItem1">Grupo do Item</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlGrpItem" class="ddlGrItem" ToolTip="Selecione o Grupo do Item"
         OnSelectedIndexChanged="ddlGrpItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblSubGrpItem1">Subgrupo</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlSubGrpItem" class="ddlItem" ToolTip="Selecione o Subgrupo do Item"
         OnSelectedIndexChanged="ddlSubGrpItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblItem1">Item</asp:Label> <br />
        <asp:DropDownList runat="server" ID="ddlItem" class="ddlItem" ToolTip="Selecione o Item"></asp:DropDownList><br /><br />
     </li>

     <li class="liboth">
        <asp:Label runat="server" id="lblLocal1">Localidade</asp:Label>
     </li>
     <li class="liboth">
        <asp:Label runat="server" ID="lblReg1">Região</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlReg" OnSelectedIndexChanged="ddlReg_SelectedIndexChanged" AutoPostBack="true" class="ddlReg" ToolTip="Selecione a Região"></asp:DropDownList>
     </li>

     <li class="liboth">
        <asp:Label runat="server" ID="lblArea1">Área</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlArea" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" AutoPostBack="true" class="ddlArea" ToolTip="Selecione a Área"></asp:DropDownList>
     </li>

     <li class="liboth">
        <asp:Label runat="server" ID="lblSubArea1">Subárea</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlSubArea" class="ddlSubArea" ToolTip="Selecione a Subárea"></asp:DropDownList><br /><br />
     </li>

          <li class="liboth">
        <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />

        <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvIniPeri" CssClass="validatorField" ErrorMessage="O campo data Inicial é requerido"
        ControlToValidate="IniPeri"></asp:RequiredFieldValidator>

        <asp:Label runat="server" ID="Label1" > &nbsp à &nbsp </asp:Label>

        <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvFimPeri" CssClass="validatorField" ErrorMessage="O campo data Final é requerido"
        ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
    </li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
