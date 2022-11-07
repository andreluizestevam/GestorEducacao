<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="BalancPlanoContas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro._1119_Relatorios.BalancPlanoContas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 220px;
        }
        .ulDados li
        {
            display: inline-block;
            margin: 10px 10px 0 0;
        }
        .liTipo
        {
            width: 90px;
        }
        .liGrupo
        {
            width: 100px;
        }
        .liSubGrupo
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" Width="220px" runat="server"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li>
                    <label for="ddlUnidContrato" title="Unidade de Contrato" class="lblObrigatorio labelPixel">
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidContrato" ToolTip="Selecione a Unidade de Contrato"
                        Width="220px" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUnidContrato"
                        ErrorMessage="Unidade de Contrato deve ser informado" Display="None">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liTipo">
                    <label for="ddlTipo" title="Tipo" class="lblObrigatorio labelPixel">
                        Tipo</label>
                    <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" Width="110px" runat="server"
                        OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipo"
                        ErrorMessage="Tipo deve ser informado" Display="None">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liSubGrupo">
                    <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">
                        Grupo</label>
                    <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="160px" runat="server"
                        OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrupo"
                        ErrorMessage="Grupo deve ser informado" Display="None">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liSubGrupo">
                    <label for="ddlSubgrupo" class="lblObrigatorio labelPixel" title="Subgrupo">
                        Subgrupo</label>
                    <asp:DropDownList ID="ddlSubgrupo" Width="160px" runat="server" ToolTip="Selecione o Subgrupo"
                        onselectedindexchanged="ddlSubGrupo_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubgrupo"
                        ErrorMessage="Subgrupo deve ser informado" Display="None">
                    </asp:RequiredFieldValidator>
                </li>   
                <li>
                    <label for="ddlSubgrupo2" class="lblObrigatorio labelPixel" title="Subgrupo 2">Subgrupo 2</label>
                    <asp:DropDownList ID="ddlSubgrupo2" Width="220px" runat="server" ToolTip="Selecione o Subgrupo 2">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubgrupo2" 
                        ErrorMessage="Subgrupo 2 deve ser informado" Display="None">
                    </asp:RequiredFieldValidator>
                </li>            
                
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liPeriodo">
                    <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                        Período</label>
                    <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                        ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
                    <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                        runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                        ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                        Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                    </asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                        ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>               
                </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
