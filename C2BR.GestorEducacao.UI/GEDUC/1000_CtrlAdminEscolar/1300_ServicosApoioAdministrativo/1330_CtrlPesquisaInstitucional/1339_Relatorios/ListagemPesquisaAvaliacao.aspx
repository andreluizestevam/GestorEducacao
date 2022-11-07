<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ListagemPesquisaAvaliacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1339_Relatorios.ListagemPesquisaAvaliacao" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 400px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 250px;            
        }      
        .liModalidade
        {
        	width:140px;
        	clear: both;
            margin-top: 5px;
        }
        .liAno
        {
        	margin-top:5px;
        	margin-left: 5px;
        }
        .liSerie
        {
        	width:100px;
        	margin-top: 5px;
        	margin-left: 5px;
        }      
        .liTurma
        {
        	margin-top: 5px;
        	width:125px;
        }
        .liMateria
        {
        	margin-top: 5px;
        	margin-left: 5px;
        	width:200px;
        }         
        .liBimestre { margin-top: 5px; }
        .ddlSerieCurso { clear: both; }        
        .ddlBimestre { width:85px; }
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
            <asp:DropDownList ID="ddlUnidade" AutoPostBack="true" 
                CssClass="ddlUnidadeEscolar" runat="server" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged"
                ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator Enabled="false" ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>  
        <li class="liModalidade">
        <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
            Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlModalidade" Text="*" 
            ErrorMessage="Campo Modalidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAno">
            <label class="lblObrigatorio" for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlAno_SelectedIndexChanged"
                ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
            ControlToValidate="ddlAno" Text="*" 
            ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                ToolTip="Selecione a Série/Curso">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSerieCurso" Text="*" 
            ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTurma" Text="*" 
            ErrorMessage="Campo Turma é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li id="liMateria" class="liMateria" runat="server">
            <label class="lblObrigatorio" for="ddlMateria" title="Matéria">
                Matéria</label>
            <asp:DropDownList ID="ddlMateria" CssClass="ddlMateria" runat="server"
                ToolTip="Selecione a Matéria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" CssClass="validatorField"
            ControlToValidate="ddlMateria" Text="*" 
            ErrorMessage="Campo Matéria é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>         
        </ContentTemplate>
        </asp:UpdatePanel>   
        <li class="liBimestre">
            <label class="lblObrigatorio" for="ddlBimestre" title="Bimestre">
                Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server"
                ToolTip="Selecione o Bimestre">
                <asp:ListItem Value="1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="4">4º Bimestre</asp:ListItem>
            </asp:DropDownList>
        </li>                        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
