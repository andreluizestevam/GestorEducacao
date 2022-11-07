<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DeclaracaoParcelasEmAberto.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes.DeclaracaoParcelasEmAberto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados { width: 300px; }
        .liUnidade,.liAluno
        {
            margin-top: 5px;
            width: 300px;            
        }             
        .liObservacao 
        {
            margin-top: 5px;
            width: 300px;
        }
        .txtObservacao 
        {
            width: 300px;
            height: 100px;
        }
        .liAnoRefer { margin-top:5px; }            
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie
        {
        	clear: both;
        	margin-top: 5px;        	
        }      
        .liTurma
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }    
        .ddlCategoria { width: 110px; }
        .ddlDisciplina { width: 190px; }
        .liPeriodo, .liNumDoc, .liVencimento
        {
        	margin-top: 10px;
        	width: 300px;
        } 
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1"
                ToolTip="Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liSerie">
            <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie" id="liResponsavel" runat="server">
            <label id="ddlLblResponsavel" class="lblObrigatorio" for="ddlResponsavel" title="Responsável">
                Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" CssClass="ddlTurma" runat="server" 
                ToolTip="Selecione o Responsável" 
                onselectedindexchanged="ddlResponsavel_SelectedIndexChanged1" 
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                ControlToValidate="ddlResponsavel" Text="*" 
                ErrorMessage="Campo Responsnável é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>

        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Aluno">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAlunos" Text="*" 
                ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                 
        </li>
        <li class="liVencimento">
            <label for="txtPeriodo" class="lblObrigatorio" title="Período">
                Período</label>
                                                    
            <asp:TextBox ID="txtDtInicio" ToolTip="Informe a Data Inicial do Periodo" CssClass="campoData" runat="server"></asp:TextBox>                          
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDtInicio" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDtFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final do Periodo"></asp:TextBox>                                                                                                                 

            <asp:CompareValidator id="CompareValidator2" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDtFim"
                ControlToCompare="txtDtInicio"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
              
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDtFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>    
                                   
    </ul>
    <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("input.campoData").datepicker();
                $("input.campoData").mask("99/99/9999");
            });
</script> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
