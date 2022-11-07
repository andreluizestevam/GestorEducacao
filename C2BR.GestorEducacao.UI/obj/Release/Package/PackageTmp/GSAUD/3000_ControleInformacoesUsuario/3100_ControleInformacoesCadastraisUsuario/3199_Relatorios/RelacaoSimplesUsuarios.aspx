<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" 
CodeBehind="RelacaoSimplesUsuarios.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios.FichaCadasResponUsuario" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
        .ulDados { width: 300px; }
        .liUnidade,.liCidade,.liBairro
        {
            margin-top: 5px;
            width: 250px;
        }        
        .liUnidade
        {
            clear: both;
        }
        .liGrauInstrucao
        {
            clear:both;
        	margin-top: 5px;
            width: 200px;	
        }
        .liUF
        {
        	clear:both;
        	margin-top: 5px;
        	width:45px;
        }
        .liCidade
        {
        	margin-top: 5px;
        	margin-left: 5px;        	
        	width:220px;
        }
        .liBairro
        {
        	margin-top: 5px;        	        	
        	width:180px;
        	clear:both;
        }
        .liModalidade
        {
        	clear:both;
        	margin-top:5px;
        	width:140px;
        }
        .liSerie
        {  	
        	margin-top:5px;
        	margin-left: 5px;
        }  
        .liTurma, .liAnoRefer, .liGrauParentesco
        {
        	clear: both;
        	margin-top: 5px;
        }   
        .ddlGrauInstrucao { width: 210px; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
        </li>         
        <li class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liCidade">
            <label class="lblObrigatorio" title="Cidade">
                Cidade</label>               
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade" runat="server" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liBairro">
            <label class="lblObrigatorio" title="Bairro">
                Bairro</label>               
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>        
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano">
                Ano</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>            
        </li>        
        <%--<li id="liModalidade" runat="server" class="liModalidade">
            <label class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" Width="140"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>                
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlModalidade" Text="*" 
                    ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                   
        <li id="liTurma" runat="server" class="liTurma">
            <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>--%>      
        </ContentTemplate>
        </asp:UpdatePanel>
           
        <li class="liGrauInstrucao">
            <label class="lblObrigatorio" title="Grau de Instrução">
                Grau de Instrução</label>               
            <asp:DropDownList ID="ddlGrauInstrucao" ToolTip="Selecione um Grau de Instrução" CssClass="ddlGrauInstrucao" runat="server">
            </asp:DropDownList>
        </li>        
        <li class="liGrauParentesco">
            <label class="lblObrigatorio" title="Grau de Parentesco">
                Grau de Parentesco</label>               
            <asp:DropDownList ID="ddlGrauParentesco" ToolTip="Selecione um Grau de Parentesco" CssClass="ddlGrauParentesco" runat="server">
            </asp:DropDownList>
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
