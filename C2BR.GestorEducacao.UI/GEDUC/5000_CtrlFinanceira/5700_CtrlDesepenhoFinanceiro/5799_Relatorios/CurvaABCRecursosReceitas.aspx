<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="CurvaABCRecursosReceitas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5700_CtrlDesepenhoFinanceiro.F5799_Relatorios.CurvaABCRecursosReceitas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 275px; }
        .liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 275px;            
        }        
        .liSituacao { margin-top: 5px; }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }      
        .ddlSituacao { width:75px; }
        .ddlTpCliente { width:55px; }
        .ddlAgrupador { width:200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <li class="liSituacao">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione uma Situação" CssClass="ddlSituacao" runat="server">        
            <asp:ListItem Selected="True" Value="A">Em aberto</asp:ListItem>
            <asp:ListItem Value="C">Cancelado</asp:ListItem>
            <asp:ListItem Value="Q">Quitado</asp:ListItem>
            </asp:DropDownList>            
        </li>       
        <li class="liSituacao" style="margin-left: 5px;">
            <label class="lblObrigatorio" title="Tipo de Cliente">
                Tipo</label>               
            <asp:DropDownList ID="ddlTpCliente" ToolTip="Selecione um Tipo de Cliente" CssClass="ddlTpCliente" runat="server">        
            <asp:ListItem Selected="True" Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="O">Cliente</asp:ListItem>
            </asp:DropDownList>            
        </li>                                                                        
        <li id="liPeriodo" class="liPeriodo" runat="server">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData" runat="server"></asp:TextBox>  
            
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
        <li style="clear: both;">
            <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
        </li>                                  
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
