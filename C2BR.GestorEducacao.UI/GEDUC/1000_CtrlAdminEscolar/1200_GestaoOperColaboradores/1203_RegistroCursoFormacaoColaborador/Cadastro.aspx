<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1203_RegistroCursoFormacaoColaborador.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 450px; }
        
        /*--> CSS LIs */
        #liColaborador{ margin-bottom: 10px;}
        #liMesAnoInicio{ margin-right: 10px; }
        #liMesAnoFim{margin-right: 10px;}        
        #liCargaHoraria{margin-left: 10px;}
        #liSituacao{ clear:both; margin-right: 10px;}
        #liCursoPrincipal{ margin-top: 13px;}
        .liClear{ clear: both;}
        .liCidade {clear:both; margin-top:-5px;}  
        .liUF {margin-top:-5px;} 
                
        /*--> CSS DADOS */
        #liCursoPrincipal label{ display: inline;}
        .txtCargaHoraria{ width: 32px;}
        .ddlGrauInstrucao{ width: 150px;}
        .ddlEspecializacao{ width: 200px;}
        .txtMesAnoInicio{width: 45px;}
        .txtMesAnoFim{width: 45px;}
        .txtNomeInstituicao{width: 255px;}
        .txtNumeroDiploma{width: 62px;}
        .txtCidade{width: 255px;}
        .txtPontos { width: 60px; text-align: right; }
        .ddlInstrutor{width:280px}     
            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul class="ulDados">
        <li id="liColaborador">
            <label for="ddlColaborador" class="lblObrigatorio">Colaborador</label>
            <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o colaborador"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlColaborador" ErrorMessage="Colaborador é requerido"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liClear">
            <label for="ddlGrauInstrucao" class="lblObrigatorio">Categoria</label>
            <asp:DropDownList ID="ddlGrauInstrucao" runat="server" CssClass="ddlGrauInstrucao" AutoPostBack="true" Enabled="false"
                onselectedindexchanged="ddlGrauInstrucao_SelectedIndexChanged" ToolTip="Selecione o grau de instrução"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlGrauInstrucao" ErrorMessage="Grau de Instrução é requerido"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liEspecializacao">
            <label for="ddlEspecializacao" class="lblObrigatorio">Curso</label>
            <asp:DropDownList ID="ddlEspecializacao" AutoPostBack="true" onselectedindexchanged="ddlEspecializacao_SelectedIndexChanged" runat="server" CssClass="ddlEspecializacao" Enabled="false" ToolTip="Selecione o Curso"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlEspecializacao" ErrorMessage="Especialização é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtPontos" title="Pontuação para Promoção">Pts.</label>
            <asp:TextBox ID="txtPontos" runat="server" Enabled="false" CssClass="txtCargaHoraria"  ToolTip="Pontuação para Promoção"></asp:TextBox>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li id="liCargaHoraria">
            <label for="txtCargaHoraria" class="lblObrigatorio" title="Carga Horária do Curso">CH</label>
            <asp:TextBox ID="txtCargaHoraria" runat="server" CssClass="txtCargaHoraria campoNumerico" ToolTip="Informe a Carga Horária do Curso"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCargaHoraria" ErrorMessage="Carga Horária é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liSituacao">
            <label for="ddlSituacao" class="lblObrigatorio">Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao" ToolTip="Selecione a situação do Curso"
                AutoPostBack="true" onselectedindexchanged="ddlSituacao_SelectedIndexChanged">
                <asp:ListItem Value="C">Cursando</asp:ListItem>
                <asp:ListItem Value="F">Concluído</asp:ListItem>
                <asp:ListItem Value="T">Trancado</asp:ListItem>
                <asp:ListItem Value="A">Abandono</asp:ListItem>
                <asp:ListItem Value="R">Reprovado</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação do Curso é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liMesAnoInicio">
            <label for="txtMesAnoInicio" class="lblObrigatorio" title="Mês/Ano de Início do Curso">Início</label>
            <asp:TextBox ID="txtMesAnoInicio" runat="server" CssClass="txtMesAnoInicio" ToolTip="Informe o Mês/Ano de Início do Curso"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMesAnoInicio" ErrorMessage="Mês/Ano de Início é requerido"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
             <asp:CustomValidator ControlToValidate="txtMesAnoInicio" ID="cvMesAnoInicio" runat="server"
                ErrorMessage="Mês/Ano de Início não pode ser maior que Mês/Ano de Término" Display="None"
                CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvMesAnoInicio_ServerValidate">
            </asp:CustomValidator>
        </li>
        <li id="liMesAnoFim">
            <label for="txtMesAnoFim" title="Mês/Ano de Término do Curso">Término</label>
            <asp:TextBox ID="txtMesAnoFim" runat="server" CssClass="txtMesAnoFim" Enabled="false" ToolTip="Informe o Mês/Ano de Término do Curso"></asp:TextBox>
             <asp:CustomValidator ControlToValidate="txtMesAnoFim" ID="cvMesAnoFim" runat="server"
                ErrorMessage="Mês/Ano de Término não pode ser menor que Mês/Ano de Início" Display="None"
                CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvMesAnoTermino_ServerValidate">
            </asp:CustomValidator>
        </li>
        <li id="liNumeroDiploma">
            <label for="txtNumeroDiploma">N° Diploma</label>
            <asp:TextBox ID="txtNumeroDiploma" runat="server" CssClass="txtNumeroDiploma" Enabled="false" ToolTip="Informe o número do Diploma"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNomeInstituicao" class="lblObrigatorio">Instituição</label>
            <asp:TextBox ID="txtNomeInstituicao" runat="server" CssClass="txtNomeInstituicao" MaxLength="65" ToolTip="Informe o nome da instituição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeInstituicao" ErrorMessage="Instituição é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li id="liSigla">
            <label for="txtSigla" class="lblObrigatorio">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" CssClass="campoSigla" MaxLength="12" ToolTip="Informe a sigla da Instituição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla da Instituição é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liCidade">
            <label for="txtCidade" class="lblObrigatorio">Cidade</label>
            <asp:TextBox ID="txtCidade" runat="server" CssClass="txtCidade" MaxLength="60" ToolTip="Informe o nome da Cidade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCidade" ErrorMessage="Cidade é requerida"
             Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liUF">
            <label for="txtUf">UF</label>
            <asp:TextBox ID="txtUf" runat="server" CssClass="campoUf" MaxLength="2" ToolTip="Sigla do Estado"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtOrgaoRegulamentador" title="Órgão Regulamentador">Órgão</label>
            <asp:TextBox ID="txtOrgaoRegulamentador" runat="server" CssClass="txtOrgaoRegulamentador" MaxLength="20" ToolTip="Informe o Órgão Regulamentador"></asp:TextBox>
        </li>
        <li id="liRegistro">
            <label for="txtRegistro" title="Registro no Órgão Regulamentador">Registro</label>
            <asp:TextBox ID="txtRegistro" runat="server" CssClass="txtRegistro" MaxLength="20" ToolTip="Informe o Registro no Órgão Regulamentador"></asp:TextBox>
        </li>
        <li id="liDataCadastro">
            <label for="txtDataCadastro" title="Data de Cadastro">Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" runat="server" CssClass="campoData" Enabled="false" ToolTip="Data de Cadastro"></asp:TextBox>
        </li>
        <li id="liCursoPrincipal">
            <asp:CheckBox ID="chkCursoPrincipal" Text="Curso Principal" runat="server" CssClass="chkCursoPrincipal" ToolTip="Curso principal"></asp:CheckBox>
        </li>        
        <li class="liClear">
            <label for="ddlInstrutor" title="Instrutor">Instrutor</label>
            <asp:TextBox ID="txtNomInstrutor" runat="server" CssClass="ddlInstrutor" MaxLength="80" ToolTip="Nome do Instrutor"></asp:TextBox>
        </li> 
    </ul>
    
<script type="text/javascript">
    jQuery(function($) {
        $(".txtCargaHoraria").mask("?99999");
        $(".txtNumeroDiploma").mask("?999999999");
        $(".txtMesAnoFim").mask("99/9999");
        $(".txtMesAnoInicio").mask("99/9999");
    });
</script>
</asp:Content>