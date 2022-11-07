<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoTituloAlunosRecursReceitas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.RelacaoTituloAlunosRecursReceitas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 330px; } 
        .liUnidade,.liVencimento,.liNumDoc
        {
            margin-top: 5px;
            width: 300px;            
        }         
        .liPeriodo
        {
        	margin-top:0px;
        	width: 300px;
        }
        .liAnoRefer { margin-top:5px; }            
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie, .liStaDocumento 
        {
        	clear: both;
        	margin-top: 5px;        	
        }      
        .liTurma
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }      
        .liAluno
        {
        	clear:both;
        	margin-top: 0px;
        }     
        .ddlStaDocumento { width: 120px; } 
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }  
        .txtNumDoc { width: 80px; }
        .ddlAgrupador { width:200px; }
        .chkLocais label { display: inline !important; margin-left:-4px;}
        
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
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>       
        <li class="liUnidade">
            <label id="Label3" class="lblObrigatorio" runat="server" title="Unidade de Contrato"> 
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade de Contrato">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidContrato" Text="*" 
            ErrorMessage="Campo Unidade de Contrato é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liAnoRefer">
            <label title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>    
        </li>
        <li class="liSerie">
            <label style="clear:both;" title="Pesquisa Por">Pesquisa Por</label>
            <asp:CheckBox CssClass="chkLocais" ID="chkPorAluno" 
                TextAlign="Right" AutoPostBack="true" runat="server" Text="Por Aluno" 
                oncheckedchanged="chkPorAluno_CheckedChanged1"/>
            <asp:CheckBox CssClass="chkLocais" ID="chkPorResponsavel"  TextAlign="Right" 
                AutoPostBack="true" runat="server" Text="Por Responsável" 
                oncheckedchanged="chkPorResponsavel_CheckedChanged1"/>
        </li>
        
        
        <li class="liNumDoc">
            <label class="label" title="Nº do Documento">
                Nº do Documento</label>
            <asp:TextBox ID="txtNumDoc" CssClass="txtNumDoc" ToolTip="Informe o Nº do Documento" runat="server"></asp:TextBox>                
        </li>
        <li class="liAluno" id="liAluno" runat="server" visible="false">
            <label title="Aluno" for="ddlAluno" >
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" runat="server" ToolTip="Selecione o Aluno" CssClass="ddlNomePessoa" >
            </asp:DropDownList>                   
        </li> 
        <li class="liAluno" id="liResponsavel" runat="server" visible="false">
            <label id="lblResponsavel" for="ddlAluno" title="Responsável">
                Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Responsável">
            </asp:DropDownList>
        </li>
        <li class="liModalidade" id="liModalidade" runat="server">
            <label for="ddlModalidade" title="Modalidade"  runat="server">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" ToolTip="Selecione a Modalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>   
        </li>
        <li class="liSerie"  id="liSerie" runat="server" >
            <label for="ddlSerieCurso" title="Série/Curso" runat="server">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" ToolTip="Selecione a Série/Curso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li class="liTurma" id="liTurma" runat="server">
            <label id="liTurma" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>     
        </ContentTemplate>
        </asp:UpdatePanel>
                            
        <li class="liStaDocumento">
            <label title="Documento(s)">
                Documento(s)</label>               
                <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento" AutoPostBack="true" OnSelectedIndexChanged="ddlStaDocumento_SelectedIndexChanged" 
                runat="server" CssClass="ddlStaDocumento" >   
            </asp:DropDownList>     
            
            <asp:CheckBox CssClass="chkLocais" ID="chkIncluiCancel" TextAlign="Right" runat="server" Text="Incluir Cancelados"/>       
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
            <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
        </li>                                     
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
