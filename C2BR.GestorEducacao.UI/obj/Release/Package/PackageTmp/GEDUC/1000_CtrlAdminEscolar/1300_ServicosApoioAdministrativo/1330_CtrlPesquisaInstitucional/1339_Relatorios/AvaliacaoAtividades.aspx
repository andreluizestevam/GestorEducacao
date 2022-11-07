<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AvaliacaoAtividades.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1339_Relatorios.AvaliacaoAtividades" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        .liTipoAvaliacao,.liUnidade,.liPeriodo
        {
            margin-top: 5px;
            width: 250px;            
        }      
        .liGrupoAvaliacao
        {
        	clear:both;
        	margin-top: 5px;
        	width:100px;
        }
        .liModalidade
        {
        	clear: both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie, .liTurma
        {
        	margin-top: 5px;
        	margin-left: 5px;
        }      
        .liMateria { margin-top: 5px; }  
        .liTipoAvaliacao { clear: both; }       
        .ddlTipoAvaliacao { width: 250px; }     
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
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
        <li>
            <label>Ano</label>
            <asp:DropDownList runat="server" ID="ddlAno"></asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrupoAvaliacao" title="Grupo Avaliação">
            <label class="lblObrigatorio" for="txtGrupoAvaliacao">
                Grupo Avaliação</label>               
            <asp:DropDownList ID="ddlGrupoAvaliacao" CssClass="ddlGrupoAvaliacao" runat="server"
                ToolTip="Selecione o Grupo Avaliação">
                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                <asp:ListItem Value="C">Curso</asp:ListItem>
                <asp:ListItem Value="D">Disciplina</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="C">Colaboradores</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>    
        <li class="liTipoAvaliacao">
            <label class="lblObrigatorio" for="txtTipoAvaliacao" title="Tipo de Avaliação">
                Tipo Avaliação</label>                    
            <asp:DropDownList ID="ddlTipoAvaliacao" CssClass="ddlTipoAvaliacao" runat="server"
                ToolTip="Selecione o Tipo de Avaliação">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTipoAvaliacao" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTipoAvaliacao" Text="*" 
            ErrorMessage="Campo Tipo Avaliação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liModalidade">
        <label class="label" for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        </li>
        <li class="liSerie">
            <label class="label" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
        </li>
        <li class="liTurma">
            <label class="label" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
        </li>
        <li id="liMateria" class="liMateria" runat="server">
            <label class="label" for="ddlMateria" title="Matéria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server"
                ToolTip="Selecione a Matéria">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" 
                ToolTip="Informe o Período Inicial">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server"
                ToolTip="Informe o Período Final">
            </asp:TextBox>    
            
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
