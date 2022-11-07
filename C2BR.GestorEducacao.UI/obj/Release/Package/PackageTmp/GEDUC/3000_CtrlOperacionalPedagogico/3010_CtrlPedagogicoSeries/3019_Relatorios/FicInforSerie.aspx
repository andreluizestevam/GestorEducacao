<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FicInforSerie.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3019_Relatorios.FicInforSerie" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 320px; }
        .liUnidade,.liDepartamento,.liNivel,.liCoordenacao,.liModalidade,.liSerie,.liSituacao
        {
            margin-top: 5px;
            width: 315px;
        }        
        .liUnidade { clear: both; }      
        .ddlDepartamento,.ddlCoordenacao { width: 315px; } 
        .ddlSituacao { width:85px; }
        .ddlNivel { width:135px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged1"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator5" runat="server"
                ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator> 
        </li>
        <li class="liNivel">
            <label class="lblObrigatorio" title="Nível">
                Nível</label>               
            <asp:DropDownList ID="ddlNivel" CssClass="ddlNivel" runat="server"
                ToolTip="Selecione o Nível" AutoPostBack="True" onselectedindexchanged="ddlNivel_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="I">Ensino Infantil</asp:ListItem>
            <asp:ListItem Value="F">Ensino Fundamental</asp:ListItem>
            <asp:ListItem Value="M">Ensino Médio</asp:ListItem>
            <asp:ListItem Value="G">Graduação</asp:ListItem>
            <asp:ListItem Value="P">Pós-Graduação</asp:ListItem>
            <asp:ListItem Value="E">Mestrado</asp:ListItem>
            <asp:ListItem Value="D">Doutorado</asp:ListItem>
            <asp:ListItem Value="U">Pós-Doutorado</asp:ListItem>
            <asp:ListItem Value="S">Especialização</asp:ListItem>            
            <asp:ListItem Value="T">Técnico</asp:ListItem>
            <asp:ListItem Value="R">Preparatório</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>    
        <li class="liDepartamento">
            <label class="lblObrigatorio" title="Departamento">
                Departamento</label>                
            <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" 
                AutoPostBack="true" runat="server" 
                onselectedindexchanged="ddlDepartamento_SelectedIndexChanged"
                ToolTip="Selecione o Departamento">                  
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator4" runat="server"
                ControlToValidate="ddlDepartamento" ErrorMessage="Departamento deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator> 
        </li>
        <li class="liCoordenacao">
            <label class="lblObrigatorio" title="Coordenação">
                Coordenação</label>               
            <asp:DropDownList ID="ddlCoordenacao" CssClass="ddlCoordenacao" runat="server"
                ToolTip="Selecione a Coordenação" AutoPostBack="True" onselectedindexchanged="ddlCoordenacao_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="ddlCoordenacao" ErrorMessage="Coordenação deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator> 
        </li>
        <li class="liModalidade">
            <label class="lblObrigatorio" title="Modalidade">
                Modalidade</label>               
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" AutoPostBack="true" 
                runat="server" onselectedindexchanged="ddlModalidade_SelectedIndexChanged"
                    ToolTip="Selecione a Modalidade">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator2" runat="server"
                ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>       
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>               
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator class="validatorField" ID="RequiredFieldValidator3" runat="server"
                ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liSituacao">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server"
                ToolTip="Selecione a Situação">
            <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
            <asp:ListItem Value="S">Suspenso</asp:ListItem>
            <asp:ListItem Value="C">Cancelado</asp:ListItem>
            </asp:DropDownList>            
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">    
</asp:Content>
