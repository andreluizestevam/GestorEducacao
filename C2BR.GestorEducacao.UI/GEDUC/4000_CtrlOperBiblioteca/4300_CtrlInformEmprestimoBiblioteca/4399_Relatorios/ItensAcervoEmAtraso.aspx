<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ItensAcervoEmAtraso.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4399_Relatorios.ItensAcervoEmAtraso" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        .liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 230px;            
        }              
        .liAreaInteresse
        {
        	clear:both;
        	margin-top:-5px;
        }                          
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }      
        .ddlAreaInteresse { width:150px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <li class="liPeriodo">
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
        <li class="liAreaInteresse">
            <label class="lblObrigatorio" title="Área de Interesse">
                Área Interesse</label>               
            <asp:DropDownList ID="ddlAreaInteresse" ToolTip="Informe uma Área de Interesse" CssClass="ddlAreaInteresse" runat="server">           
            </asp:DropDownList>            
        </li>                                                                             
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>