<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.ReativacaoMatriculaFinalizada.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 420px; }
        
        /*--> CSS LIs */
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
        .liConf
        {
            margin-left: 80px;
            margin-top: -101px;
            background-color: #F1FFEF;
        }
        
        /*--> CSS DADOS */
        .rbConfirma label, .rbReativa label
        {
            display: inline;
        }
        .rbReativa { border-style: none; }
        .rbConfirma
        {
            margin-left: 56px;
            border-style: none;
        }        
        .divSuperior
        {
            border: solid 1px #CCCCCC;
            width: 250px;
            height: 90px;
            text-align: center;
            vertical-align: middle;
        }
        .dvMsg { margin-top: 5px; }
        .lblDeseja
        {
            clear: both;
            margin-top: 10px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
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
            <label for="txtSérie" title="Série/Curso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:TextBox ID="txtSérie" Enabled="false" ToolTip="Série" CssClass="campoSerieCurso"
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
        <li>
           <label for="txtMotRea" title="Motivo da Reabertura" class="lblObrigatorio">
                Motivo Reabertura</label></asp:TextBox>
            <asp:TextBox ID="txtMotRea" TextMode="MultiLine" Width="420px" onkeyup="javascript:MaxLength(this, 250);"
                Height="50px" ToolTip="Informe o Motivo da Reabertura" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMotRea"
                CssClass="validatorField" ErrorMessage="Motivo de Reabertura deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liReativa">
            <label for="rbReativa" title="Reativação" class="lblObrigatorio">
                Confirma Reativação?</label>
            <asp:RadioButtonList ID="rbReativa" CssClass="rbReativa" AutoPostBack="true" runat="server"
                RepeatDirection="Horizontal" Height="24px" Width="137px" OnSelectedIndexChanged="rbReativa_SelectedIndexChanged">
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:HiddenField ID="hdRematriculado" runat="server" />
        </li>
        <li id="liMsg" class="liConf">
            <div id="divSuperior" visible="false" runat="server" class="divSuperior">
                <div id="dvMsg" class="dvMsg" runat="server" style="height: 50px; width: 250px;">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br />
                    <br />
                    <asp:Label ID="lblDeseja" CssClass="lblDeseja" runat="server"></asp:Label>
                    <asp:RadioButtonList ID="rbConfirma" CssClass="rbConfirma" AutoPostBack="true" Visible="false"
                        runat="server" RepeatDirection="Horizontal" Height="24px" Width="137px" OnSelectedIndexChanged="rbConfirma_SelectedIndexChanged">
                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </li>
    </ul>
    <script type="text/javascript">      
    </script>
</asp:Content>
