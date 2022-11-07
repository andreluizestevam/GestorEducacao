<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FolhadePonto.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1229_Relatorios.FolhadePonto" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liFuncionarios,.liUnidade,.liTipoCol{margin-top: 5px;width: 200px;}        
        .liAnoBase {margin-top: 5px;}
        .liFuncionarios{clear: both;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>               

        <li class="liTipoCol">
            <label class="lblObrigatorio" for="ddlTipoColaborador">
                Tipo do Colaborador</label>
            <asp:DropDownList ID="ddlTipoColaborador" CssClass="ddlTipoCol" runat="server" 
                ToolTip="Selecione o Tipo do Colaborador" AutoPostBack="True" 
                onselectedindexchanged="ddlTipoColaborador_SelectedIndexChanged" Width="120px">
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="N">Funcionário</asp:ListItem>
                <asp:ListItem Value="S">Professor</asp:ListItem>
            </asp:DropDownList>
        </li>

        <li class="liFuncionarios">
            <label class="lblObrigatorio" for="txtFuncionarios">
                Funcionários</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Funcionário">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" CssClass="validatorField"
            ControlToValidate="ddlFuncionarios" Text="*" 
            ErrorMessage="Campo Funcionário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liAnoBase" style="clear: both;">
            <label class="lblObrigatorio" for="lblAnoBase">
                Ano Base</label>               
            <asp:TextBox ID="txtAnoBase" CssClass="txtAno" runat="server" ToolTip="Informe o Ano Base">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtAno" runat="server" CssClass="validatorField"
            ControlToValidate="txtAnoBase" Text="*" 
            ErrorMessage="Campo Ano Base é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
        <li class="liAnoBase">
            <label class="lblObrigatorio" for="ddlTipoColaborador" title="Mês de Referência">
                Mês Referência</label>
            <asp:DropDownList ID="ddlMesRefer" CssClass="ddlTipoCol" runat="server" 
                ToolTip="Selecione o Mês d Referência" Width="80px">
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        jQuery(function($){
           $(".txtAno").mask("9999");           
        });
    </script>
</asp:Content>
