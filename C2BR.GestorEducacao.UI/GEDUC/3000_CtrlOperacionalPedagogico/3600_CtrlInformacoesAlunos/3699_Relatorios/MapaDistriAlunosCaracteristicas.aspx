<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MapaDistriAlunosCaracteristicas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.MapaDistriAlunosCaracteristicas" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 340px;            
        }                
        .liAnoRefer, .liTipo
        {
        	clear:both;
        	margin-top:5px;
        }            
        .liModalidade
        {        	
        	width:140px;
        	margin-top: 5px;
        	margin-left:10px;
        }
        .liSerie
        {
        	margin-top: 5px; 
        	margin-left:10px;       	
        }      
        .ddlTipo { width:55px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>   
        <li class="liTipo">
            <label id="lblTipo" title="Tipo de Pesquisa" class="lblObrigatorio" 
                for="rblTipo">
                Tipo de Pesquisa</label>
                <asp:RadioButtonList ID="rblTipo" runat="server" 
                onselectedindexchanged="rblTipo_SelectedIndexChanged" 
                ControlToValidate="rblTipo" CssClass="checkboxLabel" AutoPostBack="True" 
                RepeatDirection="Horizontal" BorderStyle="None">
                </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Escolha um tipo de filtro" ControlToValidate="rblTipo">*</asp:RequiredFieldValidator>
            
        </li>
        <li class="liTipo">
            <asp:Label ID="lblParametroNome" runat="server" Text="Label" Visible="false"></asp:Label>
        </li>
        <li id="liAnoRefer" runat="server" class="liAnoRefer" visible="false">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>               
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAnoRefer_SelectedIndexChanged">           
            </asp:DropDownList>      
            <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAnoRefer" Text="*" 
            ErrorMessage="Campo Ano Referência é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>      
        </li>                      
        <li id="liModalidade" runat="server" class="liModalidade" visible="false">
            <label class="lblObrigatorio" title="Modalidade" for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" 
                ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li id="liSerie" runat="server" class="liSerie" visible="false">
            <label class="lblObrigatorio" title="Série/Curso" for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" 
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liEstado" runat="server" class="liSerie" visible="false">
            <label class="lblObrigatorio" title="Estado" for="ddlEstado">
                Estado</label>
            <asp:DropDownList ID="ddlEstado" ToolTip="Selecione um estado" 
                CssClass="ddlSerieCurso" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlEstado_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField"
                ControlToValidate="ddlEstado" Text="*" 
                ErrorMessage="Campo estado é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li> 
        <li id="liCidade" runat="server" class="liUnidade" visible="false">
            <label class="lblObrigatorio" title="Série/Curso" for="ddlCidade">
                Cidade</label>
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma cidade" 
                CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validatorField"
                ControlToValidate="ddlCidade" Text="*" 
                ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>     
        </ContentTemplate>
        </asp:UpdatePanel>                                     
    </ul>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
        <ProgressTemplate>
            Processando...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>