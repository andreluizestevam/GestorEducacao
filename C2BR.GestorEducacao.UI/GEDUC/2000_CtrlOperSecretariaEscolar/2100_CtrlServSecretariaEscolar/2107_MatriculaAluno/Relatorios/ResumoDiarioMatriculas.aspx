<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ResumoDiarioMatriculas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.Relatorios.ResumoDiarioMatriculas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade
        {
            width: 270px;            
        }        
        .liAnoRefer, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }
        .liMesReferencia, .liSerie
        {        	        
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liModalidade
        {
        	clear:both;
        	margin-top:5px;
        	width:140px;
        }          
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 5px; margin-top:18px;} 
        .liAux { margin-left: -15px !important; margin-right: 10px; margin-top:18px; clear:none !important; display:inline;}
        .liClear { clear: both; }    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:TextBox>
        </li> 
        <li class="liUnidade">
            <label title="Unidade/Contrato">
                Unidade/Contrato</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Contrato" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
        </li>                                            
        <li id="liModalidade" runat="server" class="liModalidade">
            <label title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>              
        </li>
        <li id="liSerie" runat="server" class="liSerie">
            <label title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série" CssClass="ddlSerieCurso" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>                   
        <li id="liTurma" runat="server" class="liTurma">
            <label id="lblTurma" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
        </li>           
        </ContentTemplate>
        </asp:UpdatePanel>    
        <li class="liClear" style="margin-top: 5px;">
            <label for="txtPeriodoDe" class="lblObrigatorio" title="Intervalo de Pesquisa">Intervalo de Pesquisa</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtPeriodoDe" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAux">
            <label class="labelAux">até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtPeriodoAte" ControlToCompare="txtPeriodoDe"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtPeriodoAte" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear" style="margin-top: -5px;">
            <label for="ddlAnoRefer" class="lblObrigatorio" title="Ano de Referência">Ano Referência</label>
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAnoRefer" runat="server" ToolTip="Selecione o Ano de Referência">
            </asp:DropDownList>
        </li>                                            
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>

