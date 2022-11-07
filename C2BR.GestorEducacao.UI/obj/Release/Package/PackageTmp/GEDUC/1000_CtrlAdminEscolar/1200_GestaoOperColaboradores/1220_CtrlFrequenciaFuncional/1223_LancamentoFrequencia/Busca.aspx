<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1223_LancamentoFrequencia.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
        .liPeriodo, .liFuncionarios, .liUnidade{margin-top: 5px;width: 300px;}        
        .liFuncionarios{clear: both;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li class="liUnidade">
            <label for="ddlUnidade">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="true "
                OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>
        <li class="liFuncionarios">
            <label for="ddlColaborador">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o Funcionário ou Professor desejado">
            </asp:DropDownList>
        </li>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período da Consulta</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" ToolTip="Data Início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Data Fim"></asp:TextBox>    
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
             
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                                                                                                 
        </li>
    </ul>
</asp:Content>
