<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.AlteraStatusMatricula.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 420px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liAno
        {
            clear: both;
            margin-top: 7px;
        }
        .liEspaco
        {
            margin-left: 10px;
            margin-top: 7px;
        }
        .liReativa
        {
            clear: both;
            margin-top: 15px;
            margin-left: 160px;
        }
        .liFunc
        {
            margin-top: 10px;
            margin-left:20px;
        }
        .liData
        {
            clear: both;
            margin-top: 10px;
        }
        
        /*--> CSS DADOS */
        .ddlStatus { width: 70px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li id="liAluno">
            <label for="txtAluno" title="Aluno" class="lblObrigatorio">
                Aluno</label>
            <asp:TextBox ID="txtAluno" Enabled="false" ToolTip="Aluno" CssClass="campoNomePessoa"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdAluno" runat="server" />
        </li>
        <li class="liAno">
            <label for="txtAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:TextBox ID="txtAno" Enabled="false" ToolTip="Ano" CssClass="campoAno" runat="server"></asp:TextBox>
        </li>
        <li class="liEspaco">
            <label for="txtModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:TextBox ID="txtModalidade" Enabled="false" ToolTip="Modalidade" CssClass="campoModalidade"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdCodMod" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtS�rie" title="S�rie/Curso" class="lblObrigatorio">
                S�rie/Curso</label>
            <asp:TextBox ID="txtS�rie" Enabled="false" ToolTip="S�rie/Curso" CssClass="campoSerieCurso"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdSerie" runat="server" />
        </li>
        <li class="liEspaco">
            <label for="txtTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:TextBox ID="txtTurma" Enabled="false" ToolTip="Turma" CssClass="campoTurma"
                runat="server"></asp:TextBox>
            <asp:HiddenField ID="hdTurma" runat="server" />
        </li>
        <li class="liReativa">
            <label for="ddlStatus" title="Status da Matr�cula" class="lblObrigatorio">
                Status Matr�cula</label>
            <asp:DropDownList ID="ddlStatus" CssClass="ddlStatus" ToolTip="Selecione o Status da Matr�cula" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Selecionar" Value=""></asp:ListItem>
                <asp:ListItem Text="Ativar" Value="A"></asp:ListItem>
                <asp:ListItem Text="Cancelar" Value="C"></asp:ListItem>
                <asp:ListItem Text="Renova��o" Value="R"></asp:ListItem>
                <asp:ListItem Text="Trancar" Value="T"></asp:ListItem>
            </asp:DropDownList>
        </li>
          <li class="liData">
            <label for="txtOBS" title="Observa��o">
               Observa��o</label>
            <asp:TextBox ID="txtOBS" ToolTip="Observa��o" TextMode="MultiLine"
            
                runat="server" Height="65px" Width="411px"></asp:TextBox>
        </li>
        <li class="liData">
            <label for="txtDataC" title="Data de Altera��o">
                Data Altera��o</label>
            <asp:TextBox ID="txtDataC" Enabled="false" ToolTip="Data de Altera��o" CssClass="campoData"
                runat="server"></asp:TextBox>
        </li>
        <li class="liFunc">
            <label for="txtFuncionario" title="Funcion�rio Logado" >
                Funcion�rio</label>
            <asp:TextBox ID="txtFuncionario" Enabled="false" ToolTip="Funcion�rio Logado" CssClass="campoNomePessoa"
                runat="server"></asp:TextBox>
        </li>
    </ul>

    <script type="text/javascript">
      
    </script>

</asp:Content>
