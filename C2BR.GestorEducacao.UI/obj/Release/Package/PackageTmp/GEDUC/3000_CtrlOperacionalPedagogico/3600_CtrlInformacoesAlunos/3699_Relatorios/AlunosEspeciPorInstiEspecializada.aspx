<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AlunosEspeciPorInstiEspecializada.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.AlunosEspeciPorInstiEspecializada" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 380px; }
        .liUnidade
        {
            margin-top: 5px;
            clear:both;
            width: 380px;
        }        
        .liOpcoesRelatorio
        {
            margin-top: 5px;
            width: 300px;
        }
        .lblOpcoesRelatorio { margin-bottom:1px; }
        .ddlOpcoesRelatorio { width:140px; }
        .ddlInstituicaoEspecializada { width:150px; }
        .liAnoRefer, .liModalidade, .liTurma, .liInstituicaoEspecializada
        {
        	clear:both;
        	margin-top:5px;
        }        
        .liSerie
        {
        	margin-top:5px;
        	margin-left:5px;
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
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>        
        <li class="liOpcoesRelatorio">   
            <label id="lblOpcoesRelatorio" title="Tipo de Relatório" class="lblOpcoesRelatorio" runat="server">
                Tipo Relatório</label>
            <asp:DropDownList ID="ddlOpcoesRelatorio" ToolTip="Selecione um Tipo de Relatório" CssClass="ddlOpcoesRelatorio" runat="server">
            <asp:ListItem Value="0" Selected="True">Por Série</asp:ListItem>                
            <asp:ListItem Value="1">Por Instituição</asp:ListItem>                
            </asp:DropDownList>                                                            
        </li>        
        <li id="liAnoRefer" runat="server" class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAnoRefer" Text="*" 
                ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                       
        <li id="liModalidade" runat="server" class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        <li id="liTurma" class="liTurma" runat="server">
            <label class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        </ContentTemplate>
        </asp:UpdatePanel>

        <li id="liInstituicaoEspecializada" class="liInstituicaoEspecializada" runat="server">
            <label class="lblObrigatorio" title="Instituição Especializada">
                Instituição Especializada</label>
            <asp:DropDownList ID="ddlInstituicaoEspecializada" ToolTip="Selecione uma Instituição Especializada" CssClass="ddlInstituicaoEspecializada" runat="server">
            </asp:DropDownList>
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>