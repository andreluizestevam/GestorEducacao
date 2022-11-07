<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MovimEstoque.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6250_LanctoEntradaSaidaItensEstoque.F6259_Relatorios.MovimEstoque" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }                            
        .liUnidade,.liGrupo,.liSubGrupo,.liTpProduto,.liPeriodo
        {
        	clear:both;
            margin-top: 5px;
            width: 250px;            
        }            
        .ddlGrupo, .ddlSubGrupo { width:200px; }
        .ddlTpProduto { width:150px; }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" ToolTip="Selecione a Unidade/Escola" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liGrupo">
            <label class="lblObrigatorio" title="Grupo">
                Grupo</label>               
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" CssClass="ddlGrupo" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlGrupo_SelectedIndexChanged">           
            </asp:DropDownList>     
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlGrupo" Text="*" 
            ErrorMessage="Campo Grupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>       
        </li>                     
        <li class="liSubGrupo">
            <label class="lblObrigatorio" title="Sub-Grupo">
                Sub-Grupo</label>
            <asp:DropDownList ID="ddlSubGrupo" ToolTip="Selecione o Sub-Grupo" CssClass="ddlSubGrupo" runat="server">
            </asp:DropDownList>               
            <asp:RequiredFieldValidator ID="rfvddlSubGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSubGrupo" Text="*" 
            ErrorMessage="Campo Sub-Grupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>        
        <li class="liTpProduto">
            <label class="lblObrigatorio" title="Tipo Produto">
                Tipo Produto</label>
            <asp:DropDownList ID="ddlTpProduto" ToolTip="Selecione o Tipo Produto" CssClass="ddlTpProduto" runat="server">
            </asp:DropDownList>       
            <asp:RequiredFieldValidator ID="rfvddlTpProduto" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpProduto" Text="*" 
            ErrorMessage="Campo Tipo Produto é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>       
        </li>      
        </ContentTemplate>
        </asp:UpdatePanel>
               
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe o Início do Período" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData"  ToolTip="Informe o Término do Período" runat="server"></asp:TextBox>     
            
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>