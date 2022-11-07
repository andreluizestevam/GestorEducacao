<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoTituloFornecDespesas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5399_Relatorios.RelacaoTituloFornecDespesas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; } 
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }         
        .liPeriodo, .liNumDoc, .liVencimento
        {
        	margin-top:0px;
        	width: 300px;
        }        
        .liStaDocumento 
        {
        	clear: both;      	
        }      
        .liAluno
        {
        	clear:both;
        	margin-top:-5px;
        }     
        .ddlStaDocumento { width: 85px; } 
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }  
        .txtNumDoc { width: 80px; }
        .ddlAgrupador { width:200px; }
        .ddlFornecedor { width:270px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">        
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola"> 
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>           
        <li class="liNumDoc">
            <label class="label" title="Nº do Documento">
                Nº do Documento</label>
            <asp:TextBox ID="txtNumDoc" CssClass="txtNumDoc" ToolTip="Informe o Nº do Documento" runat="server"></asp:TextBox>                
        </li>
        <li class="liAluno">
            <label title="Fornecedor">
                Fornecedor</label>               
            <asp:DropDownList ID="ddlFornecedor" ToolTip="Selecione o Fornecedor" CssClass="ddlFornecedor" runat="server">
            </asp:DropDownList>                
        </li>                                            
        <li class="liStaDocumento">
            <label title="Documento(s)">
                Documento(s)</label>               
            <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento" CssClass="ddlStaDocumento" runat="server">   
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>        
            <asp:ListItem Value="A">Em aberto(s)</asp:ListItem>
            <asp:ListItem Value="Q">Quitado(s)</asp:ListItem>
            <asp:ListItem Value="C">Cancelado(s)</asp:ListItem>
            </asp:DropDownList>            
        </li>        
        <li class="liVencimento">
            <label for="txtPeriodo" class="lblObrigatorio" title="Período de Vencimento">
                Período de Vencimento</label>
                                                    
            <asp:TextBox ID="txtDtVenctoIni" ToolTip="Informe a Data Inicial do Vencimento" CssClass="campoData" runat="server"></asp:TextBox>                          
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDtVenctoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDtVenctoFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final do Vencimento"></asp:TextBox>                                                                                                                 

            <asp:CompareValidator id="CompareValidator2" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDtVenctoFim"
                ControlToCompare="txtDtVenctoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
              
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDtVenctoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>         
        <li class="liPeriodo">
            <label for="txtPeriodo" title="Período de Cadastro">
                Período de Cadastro</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial de Cadastro" runat="server"></asp:TextBox>                         
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final de Cadastro"></asp:TextBox>  
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >                                                                                                               
        </li>            
        <li style="clear: both;">
            <label for="ddlAgrupador" title="Agrupador de Despesa">Agrupador de Despesa</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Despesa"/>
        </li>                                     
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
