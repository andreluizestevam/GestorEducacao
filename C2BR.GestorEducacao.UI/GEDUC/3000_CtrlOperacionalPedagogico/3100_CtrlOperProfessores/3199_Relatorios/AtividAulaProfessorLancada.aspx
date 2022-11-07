<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AtividAulaProfessorLancada.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3199_Relatorios.AtividAulaProfessorLancada" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px;
					margin-left:38%;
					margin-top:50px;
		}
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
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
        .liTurma, .liFuncionarios, .liAnoRefer
        {
        	clear: both;
        	margin-top: 5px;
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
            <label id="lblUnidade" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>                  
        <li class="liAnoRefer">
            <label id="lblAnoRefer" runat="server" class="lblObrigatorio" title="Ano">
                Ano</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged"
                ToolTip="Selecione o Ano">           
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAnoRefer" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
        </li>                        
        <li id="liModalidade" runat="server" class="liModalidade">
            <label id="lblModalidade" runat="server" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" Width="140px" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                ToolTip="Selecione a Modalidade">
            </asp:DropDownList>        
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlModalidade" Text="*" 
                    ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li id="liSerie" runat="server" class="liSerie">
            <label id="lblSerieCurso" runat="server" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                   
        <li id="liTurma" runat="server" class="liTurma">
            <label id="lblTurma" runat="server" class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server"
			    AutoPostBack="True" onselectedindexchanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>           
        <li class="liFuncionarios">
            <label class="lblObrigatorio" for="txtFuncionarios" title="Professor">
                Professor</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server"
                ToolTip="Selecione o Professor">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" CssClass="validatorField"
            ControlToValidate="ddlFuncionarios" Text="*" 
            ErrorMessage="Campo Professor é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>     
        </ContentTemplate>
        </asp:UpdatePanel>                                                            
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
