<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PlanoAulaPorSerieTurma.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3199_Relatorios.PlanoAulaPorSerieTurma" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 505px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }
        .liClear { clear: both; margin-top:5px; }                                   
        .liPeriodo { margin-top:5px; margin-left: 10px; }
        .liTpAtividade
        {
        	clear:none;
        	margin-left:10px;
        	margin-top:5px;
        }
        .liModalidade, .liSerie, .liTurma
        {
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liProfResp
        {
        	clear:both;
        	margin-top:5px;
        } 
        .liMateria
        {
        	margin-left: 5px;
        	margin-top: 5px;        	
        }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }
        .ddlTpAtividade { width:105px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>    
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="lblUnidade" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>                                 
        <li class="liClear">
            <label class="lblObrigatorio" title="Ano">
                Ano</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged"
                ToolTip="Selecione o Ano">           
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAnoRefer" Text="*" 
                ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
        </li>                      
        <li class="liModalidade">
        <label class="lblObrigatorio" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" Width="140" CssClass="campoModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
        </asp:DropDownList>    
        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>            
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="campoSerieCurso" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                   
        <li class="liTurma">
            <label id="lblTurma" class="lblObrigatorio" title="Turma"  for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="campoTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>         
        <li class="liProfResp">
            <label id="lblProfResp" class="lblObrigatorio" title="Professor"  for="ddlProfResp">
                Professor</label>
            <asp:DropDownList ID="ddlProfResp" ToolTip="Selecione um Professor" 
                CssClass="ddlNomePessoa" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlProfResp_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlProfResp" runat="server" CssClass="validatorField"
                ControlToValidate="ddlProfResp" Text="*" 
                ErrorMessage="Campo Professor é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>          
        <li class="liMateria">
        <label class="lblObrigatorio" title="Matéria" for="ddlMateria">
            Matéria</label>
        <asp:DropDownList ID="ddlMateria" ToolTip="Selecione uma Matéria" CssClass="campoMateria" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
            ControlToValidate="ddlMateria" Text="*" 
            ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liClear">
            <label class="lblObrigatorio" title="Tipo de Atividade">
                Tipo de Atividade</label>               
            <asp:DropDownList ID="ddlTpAtividade" ToolTip="Selecione um Tipo de Atividade" CssClass="ddlTpAtividade" runat="server">           
                <asp:ListItem Value="T" Selected="True">Todas</asp:ListItem>
                <asp:ListItem Value="S">Homologadas</asp:ListItem>
                <asp:ListItem Value="N">Não Homologadas</asp:ListItem>
            </asp:DropDownList>            
            <asp:RequiredFieldValidator ID="rfvddlTpAtividade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTpAtividade" Text="*" 
                ErrorMessage="Campo Tipo de Atividade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período de Pesquisa</label>
                                                    
            <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial do Período" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
            ControlToValidate="txtDataPeriodoIni" Text="*" 
            ErrorMessage="Campo Data Período Início é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                
                
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final do Período" CssClass="campoData" runat="server"></asp:TextBox>  
            
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
