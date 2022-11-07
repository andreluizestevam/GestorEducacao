<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaEstatisEvasaoEscolar.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9020_IndicesCtrlPedagogico.F9029_Relatorios.MapaEstatisEvasaoEscolar" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 385px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 270px;            
        }                                   
        .liAnoReferIni, .liTipo
        {        	
        	margin-top:5px;
        	clear:both;
        }
        .liAnoReferFim
        {        	
        	margin-top:5px;
        	margin-left:10px;
        }               
        .liModalidade
        {        	
        	clear:both;
        	width:140px;
        	margin-top: 5px;
        }
        .liSerie, .liMateria
        {
        	margin-top: 5px;        	
        	margin-left: 5px; 
        }       
        .ddlTipo { width:130px; }
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
        <li class="liAnoReferIni">
            <label class="lblObrigatorio" title="Ano Inicial">
                Ano Inicial</label>               
            <asp:DropDownList ID="ddlAnoReferIni" CssClass="ddlAno" runat="server"
                ToolTip="Selecione o Ano Inicial">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlAnoReferIni" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoReferIni" Text="*" 
            ErrorMessage="Campo Ano Inicial é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>        
        <li class="liAnoReferFim">
            <label class="lblObrigatorio" title="Ano Final">
                Ano Final</label>               
            <asp:DropDownList ID="ddlAnoReferFim" CssClass="ddlAno" runat="server"
                ToolTip="Selecione o Ano Final">
            </asp:DropDownList>   
            <asp:RequiredFieldValidator ID="rfvddlAnoReferFim" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoReferFim" Text="*" 
            ErrorMessage="Campo Ano Final é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>     
        <li class="liTipo">
            <label class="lblObrigatorio" title="Tipo de Relatório">
                Tipo de Relatório</label>               
            <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server"
                ToolTip="Selecione o Tipo de Relatório" 
                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="S">Por Séries</asp:ListItem>  
            <asp:ListItem Value="M">Por Matéria (Séries)</asp:ListItem>                     
            <asp:ListItem Value="G">Por Matéria (Global)</asp:ListItem>
            </asp:DropDownList>            
        </li>
        <li class="liModalidade">
            <label Class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" Width="140" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li id="liSerie" runat="server" class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                
        <li id="liMateria" runat="server" class="liMateria">
            <label id="lblMateria" class="lblObrigatorio" title="Matéria">
                Matéria</Label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server"
                ToolTip="Selecione Matéria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
                ControlToValidate="ddlMateria" Text="*" 
                ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel>                                         
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
