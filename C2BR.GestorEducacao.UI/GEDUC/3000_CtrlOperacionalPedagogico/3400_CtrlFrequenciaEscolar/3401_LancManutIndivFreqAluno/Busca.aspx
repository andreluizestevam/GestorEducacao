<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3400_CtrlFrequenciaEscolar.F3401_LancManutIndivFreqAluno.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">       
        /*--> CSS LIs */                 
        .liDivPeriodo{ clear:none !important;display:inline !important;margin-right:0px;margin-left:-15px;margin-top:15px;}
        .liPeriodoAte { clear:none !important;display:inline !important; margin-top:12px;margin-left:5px;}
        
        /*--> CSS DADOS */
        .ddlAluno{ width: 212px;}      
        .txtPeriodoDe, .txtPeriodoAte { width: 60px;}      
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label title="Ano da matrícula do(a) Aluno(a)">Ano</label>
        <asp:DropDownList runat="server" ID="ddlAno" Width="70px" ToolTip="Ano da matrícula do(a) Aluno(a)" OnSelectedIndexChanged="ddlAno_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">Modalidade</label>
        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
            ToolTip="Selecione a Modalidade">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade" CssClass="validatorField"
         ErrorMessage="Modalidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="ddlSerieCurso" class="lblObrigatorio" title="Curso">Série/Curso</label>
        <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
            ToolTip="Selecione a Série/Curso"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSerieCurso" CssClass="validatorField"
            ErrorMessage="Série/Curso deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="ddlTurma" class="lblObrigatorio" title="Turma">Turma</label>
        <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" 
            AutoPostBack="true" onselectedindexchanged="ddlTurma_SelectedIndexChanged"
            ToolTip="Selecione a Turma"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTurma"  CssClass="validatorField" ErrorMessage="Turma deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="ddlAluno" class="lblObrigatorio" title="Aluno">Aluno</label>
        <asp:DropDownList ID="ddlAluno" CssClass="ddlAluno ddlNomePessoa" runat="server"
            ToolTip="Selecione o Aluno"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlTurma"  CssClass="validatorField" ErrorMessage="Aluno deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="txtPeriodoDe" class="lblObrigatorio" title="Período de Frequência">Período</label>
        <asp:TextBox ID="txtPeriodoDe" Width="60px" CssClass="campoData" runat="server" MaxLength="10"
            ToolTip="Período Inicial"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="validatorField" runat="server" ControlToValidate="txtPeriodoDe"
           ErrorMessage="Data inicial do período deve ser informada" Text="*"
            Display="None"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator CssClass="validatorField" ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPeriodoDe" Display="None"
            ErrorMessage="Data deve ter no máximo 8 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
    </li>
    <li class="liDivPeriodo">
        <label>a</label>
    </li>
    <li class="liPeriodoAte">
        <label for="txtPeriodoAte"></label>
        <asp:TextBox ID="txtPeriodoAte" Width="60px" CssClass="campoData" runat="server" MaxLength="10"
            ToolTip="Período Final"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField" runat="server" ControlToValidate="txtPeriodoAte"
           ErrorMessage="Data final do período deve ser informada" Text="*"
            Display="None"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator CssClass="validatorField" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPeriodoAte" Display="None"
            ErrorMessage="Data deve ter no máximo 8 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">           
</asp:Content>
