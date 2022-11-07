<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.SolicitacaoDiploma.Cadastro"
    Title="Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 700px; }
        
        /*--> CSS LIs */
        .ulDados li { margin-left: 10px; }
        .liClear { clear: both; }
        .liObservacao 
        {
        	clear: both;
        	margin-bottom:10px !important;
        }
        .liDataCadastro { margin-left:120px; }
        .liDataPrev { margin-left:110px; }
        
        /*--> CSS DADOS */
        .txtObservacao { width: 630px; }
        .campoTipoUsu{ width:100px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label for="ddlModalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" ToolTip="Informe a Modalidade" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlSerieCurso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" ToolTip="Informe a Série/Curso" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNire" class="lblObrigatorio">
                NIRE</label>
            <asp:TextBox ID="txtNire" Enabled="false" Width="70px" runat="server" ToolTip="Informe o NIRE"></asp:TextBox>
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
                Tipo Usuário</label>
            <asp:DropDownList ID="ddlTipoUsu" CssClass="campoTipoUsu" runat="server" ToolTip="Informe o Tipo de Usuário"
                AutoPostBack="True" 
                OnSelectedIndexChanged="ddlTipoUsu_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li id="liAlunoSol" runat="server">
            <label for="ddlAluno" class="lblObrigatorio">
                Aluno Solicitante</label>
            <asp:DropDownList ID="ddlAlunoSolicitante" CssClass="campoNomePessoa" runat="server"
                ToolTip="Informe o Aluno" AutoPostBack="True" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAlunoSolicitante"
                ErrorMessage="Aluno Solicitante deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liResponsavel" runat="server" visible="false">
            <label for="ddlResponsavel" class="lblObrigatorio">
                Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Responsável">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlResponsavel"
                ErrorMessage="Responsável deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroNomeSol" runat="server" visible="false" class="liAlunoRe">
            <label for="txtNomeS" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox ID="txtNomeS" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Nome da Pessoa que Solicitou o Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNomeS"
                ErrorMessage="Nome do Responsável pela Solicitação deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroRGSol" runat="server" visible="false" class="liRG">
            <label for="txtRGS" class="lblObrigatorio">
                RG</label>
            <asp:TextBox ID="txtRGS"   CssClass="campoNumerico" runat="server" ToolTip="Informe o RG do Responsável pela Solicitação do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRGS"
                ErrorMessage="RG do Responsável pela Solicitação deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liOutroFoneSol" runat="server" visible="false">
            <label for="txtTelefoneS" class="lblObrigatorio">
                Telefone</label>
            <asp:TextBox ID="txtTelefoneS"  CssClass="campoTelefone" runat="server" ToolTip="Informe o Telefone do Responsável pela Solicitação do Documento">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTelefoneS"
                ErrorMessage="Telefone do Responsável pela Solicitação deve ser informado" Text="*" Display="None"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataPrev">
            <label for="txtDataPrev"  class="lblObrigatorio">
                Data Previsão</label>
            <asp:TextBox ID="txtDataPrev" CssClass="campoData" runat="server" ToolTip="Informe a Data de Previsão"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataPrev"
                ErrorMessage="Data de Previsão deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
          <li>
            <label for="txtDataSol"  class="lblObrigatorio">
                Data Solicitação</label>
            <asp:TextBox ID="txtDataSol" Enabled="false" CssClass="campoData" runat="server"
                ToolTip="Informe a Data de Solicitação"></asp:TextBox>
        </li>
        <li class="liObservacao">
            <label for="txtObservacao">
                Observação</label>
            <asp:TextBox ID="txtObservacao" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 200);"
                CssClass="txtObservacao" runat="server" ToolTip="Observação"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDataStatus"  class="lblObrigatorio">
                Data Status</label>
            <asp:TextBox ID="txtDataStatus" Enabled="false" CssClass="campoData" runat="server" ToolTip="Informe a Data de Status"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDataStatus"
                ErrorMessage="Data de Status deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlStatus"  class="lblObrigatorio">
                Status</label>
            <asp:DropDownList ID="ddlStatus" CssClass="campoData" runat="server" ToolTip="Informe a Data de Status">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="C" Text="Cancelado"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liDataCadastro">
            <label for="txtCadastro" class="lblObrigatorio">
                Data Cadastro</label>
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
