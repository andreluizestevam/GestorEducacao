<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="HistoAvaliaAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.HistoAvaliaAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 380px; }  
        .liUnidade,.liAluno
        {
            margin-top: 5px;
            width: 300px;            
        }       
        .liAnoRefer, .liSerie, .liTpAvaliacao
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
        .liTurma, .liMateria
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }    
        .ddlTpAvaliacao { width:130px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>            
        </li>                      
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li class="liTurma">
            <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>           
        <li class="liTpAvaliacao">
            <label class="lblObrigatorio" title="Tipo de Avaliação">
                Tipo de Avaliação</label>               
            <asp:DropDownList ID="ddlTpAvaliacao" ToolTip="Selecione um Tipo de Avaliação" CssClass="ddlTpAvaliacao" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Text="Aula Normal" Value="ANO"></asp:ListItem>
                <asp:ListItem Text="Aula Extra" Value="AEX"></asp:ListItem>
                <asp:ListItem Text="Aula Reforço" Value="ARE"></asp:ListItem>
                <asp:ListItem Text="Aula de Recuperação" Value="ARC"></asp:ListItem>
                <asp:ListItem Text="Teste" Value="TES"></asp:ListItem>
                <asp:ListItem Text="Prova" Value="PRO"></asp:ListItem>
                <asp:ListItem Text="Trabalho" Value="TRA"></asp:ListItem>
                <asp:ListItem Text="Atividade em Grupo" Value="AGR"></asp:ListItem>
                <asp:ListItem Text="Atividade Externa" Value="ATE"></asp:ListItem>
                <asp:ListItem Text="Atividade Interna" Value="ATI"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="OUT"></asp:ListItem>
            </asp:DropDownList>
       </li>        
        <li id="liMateria" class="liMateria" runat="server">
            <label class="lblObrigatorio" for="ddlMateria" title="Matéria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" ToolTip="Selecione uma Matéria" CssClass="ddlMateria" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
                ControlToValidate="ddlMateria" Text="*" 
                ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>         
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>               
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAlunos" Text="*" 
                ErrorMessage="Campo Aluno é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                 
        </li>  
        </ContentTemplate>
        </asp:UpdatePanel>                                    
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>

