<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DeclaracoesPreMatricula.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2113_PreMatriculaAluno.DeclaracoesPreMatricula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados { width: 300px; }
        .liUnidade
        {
             margin-top: 10px;
             margin-bottom:-7px;
        }        
        .liUnidade, .liAno { margin-top: 5px; }                                     
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
        	width:300px;    	
        }
        
         .liDecl
        {
        	clear: both;
        	margin-top: 5px;    
        	width:300px;    	
        }
        
        .liDecl label
        {
            display:inline;    
        }                        
        .liAluno { margin-top:5px; }   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <li>
                <label>Ano</label>
                <asp:DropDownList runat="server" ID="ddlAno" ToolTip="Selecione o Ano"></asp:DropDownList>
            </li>
                <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
         </li>
         <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>

         </li>  
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" 
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
          </li>    
          <%--<li class="liAluno">
            <label title="Aluno" class="lblObrigatorio" for="ddlResponsavel">
                Responsável</label>               
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione um Aluno" 
                  CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                  
        </li>--%>      
        <li class="liAluno">
            <label title="Aluno" class="lblObrigatorio">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" 
                CssClass="ddlNomePessoa" runat="server" 
                onselectedindexchanged="ddlAlunos_SelectedIndexChanged">
            </asp:DropDownList>                  
        </li>    
        <li class="liDecl">
            <label title="Declarações" class="lblObrigatorio">
                Modelos de Declarações:</label><br />
            <asp:RadioButtonList ID="rbDeclaracoes" runat="server" AutoPostBack="True" style="display:inline !Important;">
            </asp:RadioButtonList>
        </li> 
            <%--<li class="liSerie">
                
        
        </li>  --%>
            </ContentTemplate>
            
        </asp:UpdatePanel>
        
        <li class="liSerie">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
            AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
            Processando...
            </ProgressTemplate>
        </asp:UpdateProgress></li>
    </ul>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
