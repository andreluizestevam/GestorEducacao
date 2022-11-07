<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaResultadoAvaliacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1339_Relatorios.MapaResultadoAvaliacao" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; } /* Usado para definir o formulário ao centro */
        .liTipoAvaliacao,.liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 250px;            
        }      
        .liGrupoAvaliacao
        {
        	margin-top: 5px;
        	width:150px;
        }
        .liModalidade
        {
        	clear: both;
            margin-top: 5px;
        	width:140px;
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
        	width: 125px;
        }
        .liMateria
        {
        	margin-top: 5px;
        	width:226px;
        }  
        .liFuncionarios
        {
        	margin-top: 5px;
        	clear:both;
        	width:260px;
        }
        .liTipoAvaliacao { clear: both; }       
        .ddlTipoAvaliacao { width: 250px; }      
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }
        .ddlNumPesq
        {
        	width:40px;
        	clear: both;
            margin-top: 5px;
        }        
        .label { margin-bottom:1px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrupoAvaliacao">
            <label class="lblObrigatorio" for="txtGrupoAvaliacao">
                Grupo Avaliação</label>               
            <asp:DropDownList ID="ddlGrupoAvaliacao" CssClass="ddlGrupoAvaliacao" runat="server">
                <asp:ListItem Selected="True" Value="C">Curso</asp:ListItem>
                <asp:ListItem Value="D">Disciplina</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="C">Colaboradores</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>    
        <li class="liTipoAvaliacao">
            <label class="lblObrigatorio" for="txtTipoAvaliacao">
                Tipo Avaliação</label>                    
            <asp:DropDownList ID="ddlTipoAvaliacao" CssClass="ddlTipoAvaliacao" AutoPostBack="true"
                runat="server" onselectedindexchanged="ddlTipoAvaliacao_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTipoAvaliacao" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTipoAvaliacao" Text="*" 
            ErrorMessage="Campo Tipo Avaliação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liNumPesq" class="liNumPesq">
            <label class="lblObrigatorio" for="ddlNumPesq">
                Pesq Nº</label>                    
            <asp:DropDownList ID="ddlNumPesq" CssClass="ddlNumPesq" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlNumPesq" runat="server" CssClass="validatorField"
            ControlToValidate="ddlNumPesq" Text="*" 
            ErrorMessage="Campo Pesq Nº é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
            <label class="label" for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>        
        </li>
        <li class="liSerie">
            <label class="label" for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>        
        </li>
        <li class="liTurma">
            <label class="label" for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>        
        </li>
        <li id="liMateria" class="liMateria" runat="server">
            <label class="label" for="ddlMateria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server">
            </asp:DropDownList>        
        </li>
    
        <li class="liFuncionarios">
            <label class="label" for="txtFuncionarios">
                Funcionários</label>                    
            <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>        
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"></asp:TextBox> 
            
            <asp:CompareValidator id="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red"
                ControlToValidate="txtDataPeriodoFim"
                ControlToCompare="txtDataPeriodoIni"
                Type="Date"       
                Operator="GreaterThanEqual"      
                ErrorMessage="Data Final não pode ser menor que Data Inicial." >
            </asp:CompareValidator >
                
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoFim" Text="*" 
            ErrorMessage="Campo Data Período Fim é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                                                                                           
        </li>            
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
