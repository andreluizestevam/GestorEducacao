<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaPerfiDesempAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.MapaPerfiDesempAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 380px; } /* Usado para definir o formulário ao centro */   
        .liUnidade,.liAluno
        {
            margin-top: 5px;
            width: 300px;            
        }                                   
        .liAnoRefer
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
        	clear: both;
        	margin-top: 5px;        	
        }      
        .liTurma
        {
        	margin-left: 5px;
        	margin-top: 5px;
        } 
        .liMateria     
        {
        	margin-top:5px;        	
        }        
        .label   
        {
            margin-bottom:1px;	
            display:block;
        }   
        .txtNota 
        {
        	width: 20px;
        }
        .liFxNota
        {
        	margin-left: 5px;
        	margin-top: 5px;
        }
        .lblDivData{display:inline;margin: 0 5px;margin-top: 0px;}
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
        <li id="liMateria" class="liMateria" runat="server">
        <label class="lblObrigatorio" for="ddlMateria" title="Matéria">
            Matéria</label>
        <asp:DropDownList ID="ddlMateria" ToolTip="Selecione uma Matéria" CssClass="ddlMateria" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
            ControlToValidate="ddlMateria" Text="*" 
            ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liFxNota">
            <label>
                Faixa de Notas</label>               
            <asp:TextBox ID="txtNotaMin" CssClass="txtNota" runat="server" ToolTip="Informe a Nota Mínima">
            </asp:TextBox>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> até </asp:Label>
            <asp:TextBox ID="txtNotaMax" CssClass="txtNota" runat="server" ToolTip="Informe a Nota Máxima">
            </asp:TextBox>
        </li>                                  
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
    jQuery(function ($) {
        $(".txtNota").mask("?999");
    });
    </script>
</asp:Content>

