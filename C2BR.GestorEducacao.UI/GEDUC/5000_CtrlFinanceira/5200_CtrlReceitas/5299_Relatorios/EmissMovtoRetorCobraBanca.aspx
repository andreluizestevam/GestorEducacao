<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="EmissMovtoRetorCobraBanca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.EmissMovtoRetorCobraBanca"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 240px;
        }
        .liPeriodo
        {
            margin-top: 5px;
        }
        .liClear { clear: both; }
        .lblDivData
        {
            margin: 0 5px;
            margin-top: 0px;
            height: 27px;
        }
        .ddlBanco, .ddlAgencia {width:160px;}
        .ddlConta {text-align:right;width:68px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <li>
                    <label for="ddlBanco" title="Banco">Banco</label>
                    <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                        ToolTip="Selecione o Banco" 
                        onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li class="liClear" style="margin-top: 10px;">
                    <label for="ddlAgencia" class="lblObrigatorio" title="Agência">Agência</label>
                    <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                        ToolTip="Selecione a Agência" 
                        onselectedindexchanged="ddlAgencia_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 10px;">
                    <label for="ddlConta" title="Conta">Conta</label>
                    <asp:DropDownList ID="ddlConta" runat="server" CssClass="ddlConta"
                        ToolTip="Selecione a Conta">
                    </asp:DropDownList>
                </li>                
        </ContentTemplate>
        </asp:UpdatePanel>
        <li  style="margin-top: 10px;">
            <label for="txtPeriodo" title="Período de Vencimento">
                Período de Vencimento</label>
                                                    
            <asp:TextBox ID="txtDtVenctoIni" ToolTip="Informe a Data Inicial do Vencimento" CssClass="campoData" runat="server"></asp:TextBox>
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDtVenctoFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final do Vencimento"></asp:TextBox>                                                                                                                 

            <asp:CompareValidator id="CompareValidator21" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDtVenctoFim"
                ControlToCompare="txtDtVenctoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
        </li>         
        <li class="liPeriodo">
            <label for="txtPeriodo" title="Período de Crédito">
                Período de Crédito</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                runat="server"></asp:TextBox>

            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>

            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial de Crédito.">
            </asp:CompareValidator>
        </li>
        <li class="liClear">
            <label title="Status"> Status</label>               
            <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento" runat="server" CssClass="ddlStaDocumento" >   
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>        
                <asp:ListItem Value="P">Pendente Baixa</asp:ListItem>
                <asp:ListItem Value="B">Baixado</asp:ListItem>                 
                <asp:ListItem Value="I">Inconsistente</asp:ListItem>
                <asp:ListItem Value="D">Mov Duplicado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
                <asp:ListItem Value="Q">Já Pago</asp:ListItem>
            </asp:DropDownList>       
        </li>
        <li class="liAluno" style="margin-left: 5px;">
            <label title="Ordenado Por">
                Ordenado Por</label>
            <asp:DropDownList ID="ddlOrdenRelat" ToolTip="Selecione a Ordenação do Relatório" Width="130px" runat="server">
                <asp:ListItem Text="Data de Vencimento" Value="V"></asp:ListItem>
                <asp:ListItem Text="Data de Crédito" Value="C"></asp:ListItem>
                <asp:ListItem Text="Banco, Agência e Conta" Value="B"></asp:ListItem>
                <asp:ListItem Text="Nosso Número" Value="N"></asp:ListItem>
                <asp:ListItem Text="Status" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>       
    </ul>
<script type="text/javascript">
    $(document).ready(function () {
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
