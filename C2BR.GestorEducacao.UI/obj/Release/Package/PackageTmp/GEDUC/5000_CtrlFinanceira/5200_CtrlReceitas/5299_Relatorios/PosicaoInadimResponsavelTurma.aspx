<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PosicaoInadimResponsavelTurma.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.PosicaoInadimResponsavelTurma" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 275px; }
        .liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 275px;            
        }                                   
        .liAnoRefer, .liTurma, .liResponsavel, .liTpInadimplencia 
        {
        	clear:both;
        	margin-top:5px;
        }            
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie
        {        	
        	margin-top: 5px;        	
        	margin-left: 5px;
        }      
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }      
        .ddlTpInadimplencia { width: 230px; }  
        .ddlAgrupador { width:200px; }
        
        .liPeriodo { width: 75px; }
        .liPeriodoAte, .liAux { margin-top: 18px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>    
        <li class="liUnidade">
            <label id="Label2" title="Unidade de Contrato" class="lblObrigatorio" runat="server">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" ToolTip="Selecione a Unidade de Contrato" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="rfvUnidade" runat="server" 
                ControlToValidate="ddlUnidadeContrato" 
                ErrorMessage="Campo Unidade de Contrato é requerido">*</asp:RequiredFieldValidator>
        </li>       
        <li class="liTpInadimplencia">
            <label class="lblObrigatorio" title="Tipo de Inadimplência">
                Tipo de Inadimplência</label>               
            <asp:DropDownList ID="ddlTpInadimplencia" ToolTip="Selecione um Tipo de Inadimplência" CssClass="ddlTpInadimplencia" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlTpInadimplencia_SelectedIndexChanged">        
            <asp:ListItem Selected="True" Value="F">Ficha de Inadimplência</asp:ListItem>
            <asp:ListItem Value="R">Inadimplência por Responsável - Resumida</asp:ListItem>
            <asp:ListItem Value="RD">Inadimplência por Responsável - Detalhada</asp:ListItem>
            <asp:ListItem Value="S">Inadimplência por Curso/Turma</asp:ListItem>
            </asp:DropDownList>            
            <asp:RequiredFieldValidator ID="rfvTipo" runat="server" 
                ControlToValidate="ddlTpInadimplencia" ErrorMessage="Informe o tipo">*</asp:RequiredFieldValidator>
        </li>             
        <li id="liResponsavel" class="liResponsavel" runat="server">
            <label class="lblObrigatorio" title="Responsável">
                Responsável</label>               
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione um Responsável" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                     
            <asp:RequiredFieldValidator ID="rfvResp" runat="server" 
                ControlToValidate="ddlResponsavel" ErrorMessage="Campo Responsável é requerido">*</asp:RequiredFieldValidator>
        </li>                           
        <li id="liModalidade" class="liModalidade"  runat="server">
            <label title="Modalidade" for="ddlModalidade">
                Modalidade</label>
            
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" 
                ErrorMessage="Informe a modalidade" ControlToValidate="ddlModalidade">*</asp:RequiredFieldValidator>       
        </li>
        <li id="liSerie" class="liSerie"  runat="server">
        <label title="Curso" for="ddlSerieCurso">Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSerie" runat="server" 
                ControlToValidate="ddlSerieCurso" ErrorMessage="Informe o curso">*</asp:RequiredFieldValidator>
        </li>        
        <li id="liTurma" class="liTurma"  runat="server" style="clear:both; margin-top:-10px;">
            <label id="lblTurma" title="Turma" for="ddlTurma">
                Turma</label>
            
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTurma" runat="server" 
                ErrorMessage=" Informe a turma" ControlToValidate="ddlTurma">*</asp:RequiredFieldValidator>
        </li>   
        <li class="liAnoRefer">
            <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
            <asp:RequiredFieldValidator ID="rfvAgrupador" runat="server" 
                ControlToValidate="ddlAgrupador" ErrorMessage="Informe o agrupador">*</asp:RequiredFieldValidator>
        </li>
        <li class="liPeriodo">
            <label for="txtPeriodoDe" class="lblObrigatorio" title="Informe o período de vencimento utilizado na consulta do relatório">Período</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtPeriodoDe" runat="server" CssClass="validatorField"
            ControlToValidate="txtPeriodoDe" Text="*" 
            ErrorMessage="Campo Data de início do período deve ser informado" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtPeriodoAte" runat="server" CssClass="validatorField"
            ControlToValidate="txtPeriodoDe" Text="*" 
            ErrorMessage="Campo Data final do período deve ser informado" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
