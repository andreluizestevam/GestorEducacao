<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.EntregaDiploma.Cadastro"
    Title="Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 420px; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-left: 10px;
            margin-top: 5px;
        }
        .liClear { clear: both; }
        .liddlTipoUsuRece
        {
            margin-top: 5px !important;
            clear: both;
        }
        .liAlunoRe { margin-top: 5px !important; }
        .liRG
        {
            margin-left: 120px !important;
            margin-top: -5px !important;
        }
        .liFone { margin-top: -5px !important; }
        
        /*--> CSS DADOS */
        .txtNire { margin-bottom:0px !important; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label for="ddlModalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" ToolTip="Informe o Modalidade"
                AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:HiddenField ID="hdCod" runat ="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" ToolTip="Informe a Série/Curso"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNire" class="lblObrigatorio">
                NIRE</label>
            <asp:TextBox ID="txtNire" CssClass="txtNire" Enabled="false" Width="70px" runat="server" ToolTip="Informe o NIRE"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtNire"
                ErrorMessage="Campo NIRE é requerido" Text="*" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAluno" class="lblObrigatorio">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Aluno"
                AutoPostBack="True" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlTipoUsu">
                Usuário Solicitante</label>
            <asp:DropDownList ID="ddlTipoUsu" Enabled="false" CssClass="campoData" runat="server"
                ToolTip="Informe o Tipo de Usuário" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoUsu_SelectedIndexChanged">
                <asp:ListItem Value="A" Text="Aluno" Selected="True"></asp:ListItem>
                <asp:ListItem Value="R" Text="Responsável"></asp:ListItem>
                <asp:ListItem Value="O" Text="Outros"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li id="liAlunoSol" runat="server">
            <label for="ddlAluno" class="lblObrigatorio">
                Aluno Solicitante</label>
            <asp:DropDownList ID="ddlAlunoSolicitante" Enabled="false" CssClass="campoNomePessoa"
                runat="server" ToolTip="Informe o Aluno" AutoPostBack="True" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAlunoSolicitante"
                ErrorMessage="Aluno de Entrega deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liResponsavel" runat="server" visible="false">
            <label for="ddlResponsavel" class="lblObrigatorio">
                Responsável Solicitante</label>
            <asp:DropDownList ID="ddlResponsavel" Enabled="false" CssClass="campoNomePessoa"
                runat="server" ToolTip="Informe o Responsável">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlResponsavel"
                ErrorMessage="Responsável Solicitante deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroNomeSol" runat="server" visible="false" class="liAlunoRe">
            <label for="txtNomeS" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox ID="txtNomeS" Enabled="false" CssClass="campoNomePessoa" runat="server"
                ToolTip="Informe o Nome da Pessoa que Solicitou o Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNomeS"
                ErrorMessage="Nome do Responsável pela Solicitação deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroRGSol" runat="server" visible="false" class="liRG">
            <label for="txtRGS" class="lblObrigatorio">
                RG</label>
            <asp:TextBox ID="txtRGS" Enabled="false" CssClass="campoNumerico" runat="server"
                ToolTip="Informe o RG do Responsável pela Solicitação do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRGS"
                ErrorMessage="RG do Responsável pela Solicitação deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroFoneSol" runat="server" visible="false" class="liFone">
            <label for="txtTelefoneS" class="lblObrigatorio">
                Telefone</label>
            <asp:TextBox ID="txtTelefoneS" Enabled="false" CssClass="campoTelefone" runat="server"
                ToolTip="Informe o Telefone do Responsável pela Solicitação do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTelefoneS"
                ErrorMessage="Telefone do Responsável pela Solicitação deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liddlTipoUsuRece">
            <label for="ddlTipoUsu">
                Usuário Recebimento</label>
            <asp:DropDownList ID="ddlTipoUsuRece" CssClass="campoData" runat="server" ToolTip="Informe o Tipo de Usuário"
                AutoPostBack="True" OnSelectedIndexChanged="ddlTipoUsuRec_SelectedIndexChanged">
                <asp:ListItem Value="A" Text="Aluno" Selected="True"></asp:ListItem>
                <asp:ListItem Value="R" Text="Responsável"></asp:ListItem>
                <asp:ListItem Value="O" Text="Outros"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li id="liAlunoRe" runat="server" class="liAlunoRe">
            <label for="ddlAluno" class="lblObrigatorio">
                Aluno Recebimento</label>
            <asp:DropDownList ID="ddlAuloRecebimento" CssClass="campoNomePessoa" runat="server"
                ToolTip="Informe o Aluno" AutoPostBack="True" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAuloRecebimento"
                ErrorMessage="Aluno de Recebimento deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liResSol" runat="server" visible="false" class="liAlunoRe">
            <label for="ddlResponsavel" class="lblObrigatorio">
                Responsável Recebimento</label>
            <asp:DropDownList ID="ddlRespRec" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Responsável">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlRespRec"
                ErrorMessage="Responsável pelo Recebimento deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutrosNome" runat="server" visible="false" class="liAlunoRe">
            <label for="txtNomeOutro" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox ID="txtNomeOutro" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Nome da Pessoa que Receberá o Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNomeOutro"
                ErrorMessage="Nome do Responsável pelo Recebimento deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutrosRG" runat="server" visible="false" class="liRG">
            <label for="txtRGOutro" class="lblObrigatorio">
                RG</label>
            <asp:TextBox ID="txtRGOutro"  MaxLength="12" CssClass="campoNumerico" runat="server" ToolTip="Informe o RG do Responsável pelo Recebimento do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRGOutro"
                ErrorMessage="RG do Responsável pelo Recebimento deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutrosTelefone" runat="server" visible="false" class="liFone">
            <label for="txtTelefone" class="lblObrigatorio">
                Telefone</label>
            <asp:TextBox ID="txtTelefone" MaxLength="10" CssClass="campoTelefone" runat="server" ToolTip="Informe o Telefone do Responsável pelo Recebimento do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTelefone"
                ErrorMessage="Telefone do Responsável pelo Recebimento deve ser informado" Text="*"
                Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtCadastro" class="lblObrigatorio">
                Data Entrega</label>
            <asp:TextBox ID="txtCadastro" Enabled="false" CssClass="campoData" runat="server"
                ToolTip="Informe a Data de Cadastro"></asp:TextBox>
        </li>
        <li>
            <label for="ddlColaborador">
                Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" Enabled="false" CssClass="campoNomePessoa"
                runat="server" ToolTip="Informe o Colaborador">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlColaborador"
                ErrorMessage="Colaborador deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>

    <script type="text/javascript">
        $(document).ready(function() {
        $(".txtAnoSemestreConclusao").mask("?9999/99");
        $(".campoTelefone").mask("?(99) 9999-9999");
        });
    </script>

</asp:Content>
