<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PautaChamadaSerieTurma.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3499_Relatorios.PautaChamadaSerieTurma" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 370px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;            
        }                                   
        .liTipo
        {
        	margin-top:5px;
        	margin-left: 5px;
        	width:70px;        	
        }
        .liAnoRefer, .liMesReferencia, .liTurma
        {
        	clear:both;
        	margin-top:5px;
        }       
        .liModalidade
        {
        	width:140px;
        	margin-top: 5px;
        	margin-left: 5px;
        }
        .liSerie
        {
        	margin-top: 5px;        	
        	margin-left: 5px;
        }              
        .ddlTipo { width:60px; }
        .ddlMesReferencia { width: 95px; }
        .liMateria
        {
        	margin-left: 5px;
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
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                ToolTip="Selecione a Unidade/Escola"
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano">
                Ano</label>               
            <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" 
                ToolTip="Selecione o Ano"
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>         
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAnoRefer" Text="*" 
                ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>   
        </li>                 
        <li id="liModalidade" runat="server" class="liModalidade">
            <label class="lblObrigatorio" runat="server" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true" Width="140"
                ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                    ControlToValidate="ddlModalidade" Text="*" 
                    ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>             
        </li>
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" title="Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" 
                ToolTip="Selecione a Curso"
                AutoPostBack="True" onselectedindexchanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                   
        <li id="liTurma" runat="server" class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" 
                ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>           
        <li id="liMateria" class="liMateria" runat="server">
            <label class="lblObrigatorio" for="ddlMateria" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server"
                ToolTip="Selecione a Disciplina">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
                ControlToValidate="ddlMateria" Text="*" 
                ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>        
        </ContentTemplate>
        </asp:UpdatePanel>         
        <li class="liMesReferencia">
            <label class="lblObrigatorio" for="txtMesReferencia" title="Mês de Referência">
                Mês de Referência</label>               
            <asp:DropDownList ID="ddlMesReferencia" CssClass="ddlMesReferencia" runat="server"
                ToolTip="Selecione o Mês de Referência">
                <asp:ListItem Value="0">Sem Referência</asp:ListItem>
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>                    
            </asp:DropDownList>
       </li>        
       <li class="liTipo">
            <label class="lblObrigatorio" title="Layout">
                Layout</label>               
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server"
                ToolTip="Selecione o Tipo de Layout">
            <asp:ListItem Selected="True" Value="F">Frente</asp:ListItem>
            <asp:ListItem Value="V">Verso</asp:ListItem>
            </asp:DropDownList>            
        </li>                                                             
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
