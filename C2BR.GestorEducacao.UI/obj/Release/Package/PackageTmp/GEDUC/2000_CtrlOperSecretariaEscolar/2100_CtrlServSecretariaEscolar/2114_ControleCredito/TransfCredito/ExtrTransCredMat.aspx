<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ExtrTransCredMat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito.ExtrTransCredMat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
        .ulDados 
        {
             width: 275px;
            margin-top: 30px;
        }
        
        .ulDados li
        {
            margin-top: 6px;
            margin-left: 40px;
            margin-right: 0px !important;
        }
        
        .liboth
        {
            clear:both;
        }
        
        .ddlMat
        {
            width:140px;
            clear:both;
        }
        .liUnidade,.liTipoRelatorio
        {
            margin-top: 5px;
            width: 240px;
        }
        
        .ddlModalidade
        {
            width:120px;
            clear:both;
        }
        
        .ddlSerie
        {
            width:60px;
            clear:both; 
        }
        
        .ddlTurma
        {
             width:100px;
            clear:both;
        }
        
        .ddlAluno
        {
            width:205px;
            clear:both;
        }
        
        .ddlAno
        {
            width:26px;
            clear:both;
        }
        
        .chkLocais { margin-left: 5px; }
        .chkLocais label { display: inline !important; margin-left:-4px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
                    <li class="liboth">
            <asp:Label runat="server" ID="lblAnoRef" CssClass="lblObrigatorio">Ano Referência</asp:Label>
            <asp:TextBox runat="server" ID="txtAnoRef" ToolTip="Informe o Ano de Referência" class="ddlAno"></asp:TextBox>
            <asp:requiredfieldvalidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnoRef" ErrorMessage="O Ano Referência deve ser Informado"
            Display="None"></asp:requiredfieldvalidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblTipoPesquisa">Pesquisar por</asp:Label><br /><br />
            <asp:CheckBox CssClass="chkLocais" ID="chkAluno" Checked="true" OnCheckedChanged="chkAluno_OnCheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Aluno" />
            <asp:CheckBox CssClass="chkLocais" ID="chkCPF" runat="server" OnCheckedChanged="chkCPF_OnCheckedChanged" TextAlign="Right" AutoPostBack="true" Text="CPF" /><br />
        </li>


    <li class="liboth">
            <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label><br />
            <asp:DropDownList ID="ddlModalidade" class="ddlModalidade" runat="server" ToolTip="Selecione a Modalidade" 
            OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade" ErrorMessage="Modalidade deve ser informada"
                Display="None"></asp:RequiredFieldValidator>
        </li>

        <li class="liboth">
            <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label><br />
            <asp:DropDownList ID="ddlSerieCurso" class="ddlSerie" runat="server" ToolTip="Selecione a Série/Curso"
             OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso" ErrorMessage="Série/Curso deve ser informada"
                Display="None"></asp:RequiredFieldValidator>
        </li>

        <li class="liboth">
            <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label><br />
            <asp:DropDownList ID="ddlTurma" class="ddlTurma" runat="server" ToolTip="Selecione a Turma" 
            OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma" ErrorMessage="Turma deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>

        <li class="liboth">
            <asp:Label runat="server" ID="lblAluno" class="lblObrigatorio">Aluno</asp:Label>
            <asp:DropDownList ID="ddlAluno" class="ddlAluno" runat="server" ToolTip="Selecione o Aluno"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAluno" ErrorMessage="O Aluno deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>

        <li class="liboth">
            <asp:Label runat="server" ID="lblCPF">CPF</asp:Label><br />
            <asp:TextBox runat="server" ID="txtCPF" ToolTip="Informe o CPF" CssClass="campoCpf"></asp:TextBox>
        </li>
    </ul>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">

    <script type="text/javascript">

    $(document).ready(function () {
    $(".campoCpf").mask("999.999.999-99");
});

    </script>
</asp:Content>
