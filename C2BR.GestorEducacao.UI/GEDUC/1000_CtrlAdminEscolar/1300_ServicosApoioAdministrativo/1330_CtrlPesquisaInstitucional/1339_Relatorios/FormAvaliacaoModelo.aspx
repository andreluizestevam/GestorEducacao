<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FormAvaliacaoModelo.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1339_Relatorios.FormAvaliacaoModelo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        .liTipoAvaliacao, .liUnidade, .liMateria
        {
            margin-top: 5px;
            clear:both;
            width: 200px;
        } 
        .liPublicoAlvo
        {
        	margin-top: 5px;
        	margin-left: 5px;
        	width: 115px;        	
        }
        .liModalidade
        {
        	width:140px;
        	clear: both;
            margin-top: 5px;
        }      
        .liSerie
        {
        	width:110px;
        	margin-top: 5px;
        	margin-left: 5px;
        }   
        .liTurma
        {
        	margin-top: 5px;
        	margin-left: 5px;
        	width:125px;        
        }
        .liTipoAvaliacao { clear: both; }       
        .liNumPesquisa { margin-top:5px; } 
        .liIdent
        {
        	margin-top:5px;
        	margin-left:5px;
        	width:45px;
        }
        .ddlIdent { width: 45px; }
        .ddlTipoAvaliacao { width: 200px; }  
        .ddlPublicoAlvo { width:115px; }                
        .ddlNumPesquisa { width: 100px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
        <li id="liNumPesquisa" class="liNumPesquisa" runat="server">
            <label class="lblObrigatorio" title="N° Pesquisa">
                Nº Pesquisa</label>
            <asp:DropDownList ID="ddlNumPesquisa" CssClass="ddlNumPesquisa" runat="server" AutoPostBack="true"
                ToolTip="Selecione o N° da Pesquisa" 
                onselectedindexchanged="ddlNumPesquisa_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlNumPesquisa" runat="server" CssClass="validatorField"
            ControlToValidate="ddlNumPesquisa" Text="*" 
            ErrorMessage="Campo Nº Pesquisa é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>           
        </li>        
        <li class="liUnidade">
            <label for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" Enabled="false"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liTipoAvaliacao" class="liTipoAvaliacao" runat="server">
            <label for="txtTipoAvaliacao" title="Tipo de Avaliação">
                Tipo Avaliação</label>                    
            <asp:DropDownList ID="ddlTipoAvaliacao" CssClass="ddlTipoAvaliacao" runat="server" Enabled="false"
                ToolTip="Selecione o Tipo de Avaliação">
            </asp:DropDownList>
        </li>        
        <li id="liPublicoAlvo" class ="liPublicoAlvo" runat="server">
            <label for="ddlPublicoAlvo" title="Público Alvo">Público Alvo</label>
            <asp:DropDownList ID="ddlPublicoAlvo" runat="server" CssClass="ddlPublicoAlvo" ToolTip="Selecione o Público Alvo"  Enabled="false">
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="F">Funcionário</asp:ListItem>
            <asp:ListItem Value="P">Professor</asp:ListItem>
            <asp:ListItem Value="R">Responsável</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>        
        <li id="liIdent" class="liIdent" runat="server">
            <label title="Identificada">
                Identificada</label>                    
            <asp:DropDownList ID="ddlIdent" CssClass="ddlIdent" runat="server"  Enabled="false"
                ToolTip="Informe se a Pesquisa é Identificada">
            <asp:ListItem Selected="True" Value="S">Sim</asp:ListItem>
            <asp:ListItem Value="N">Não</asp:ListItem>
            </asp:DropDownList>            
        </li>        
        <li id="liModalidade" class="liModalidade" runat="server">
        <label for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" Enabled="false" ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        </li>        
        <li id="liSerie" class="liSerie" runat="server">
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" Enabled="false" ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
        </li>
        <li id="liTurma" class="liTurma" runat="server">
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" Enabled="false" ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>
        <li id="liMateria" class="liMateria" runat="server">
            <label for="ddlMateria" title="Matéria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server"  Enabled="false"
                ToolTip="Selecione a Matéria">
            </asp:DropDownList>
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel>     
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">    
</asp:Content>
