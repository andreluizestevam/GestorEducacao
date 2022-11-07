<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3618_RegistroOcorrenciaSaudeAluno.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 530px; }
        .ulDados li input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 9px;
            margin-right: 10px;
        }        
        .liClear { clear: both; }
        #liDataEntrega { margin-bottom: 4px; }
        #liDiasLicenca { margin-bottom: 0; }
        
        /*--> CSS DADOS */
        .txtHospital { width: 225px; }
        .txtCrm, .txtDiasLicenca { width: 40px; }
        .ddlCodigoDoenca { width: 55px; }
        .txtDoenca { width: 120px; }
        .txtObservacao
        {
            width: 300px;
            height: 47px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade do Colaborador">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="true"
                Enabled="false" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade do Aluno">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlUnidade"
                ErrorMessage="Unidade é requerida" Display="Dynamic" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlAluno" class="lblObrigatorio" title="Nome do Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" runat="server" CssClass="campoNomePessoa" Enabled="false"
                ToolTip="Selecione o Nome do Aluno">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtMedico" class="lblObrigatorio" title="Nome do Médico Responsável pelo Atendimento">
                Nome do Médico</label>
            <asp:TextBox ID="txtMedico" runat="server" MaxLength="60" CssClass="campoNomePessoa" ToolTip="Informe o Nome do Médico Responsável pelo Atendimento"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMedico"
                ErrorMessage="Nome do Médico é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtCrm" class="lblObrigatorio" title="Código do Médico no Conselho Regional de Medicina">
                CRM</label>
            <asp:TextBox ID="txtCrm" runat="server" CssClass="txtCrm" MaxLength="10" ToolTip="Informe o Código do Médico no Conselho Regional de Medicina"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCrm"
                ErrorMessage="CRM do Médico é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtHospital" title="Nome do Hospital">
                Local de Emissão</label>
            <asp:TextBox ID="txtHospital" runat="server" CssClass="txtHospital" ToolTip="Informe o Nome do Hospital" MaxLength="100"></asp:TextBox>
        </li>
        <li>
            <label for="ddlCodigoDoenca" class="lblObrigatorio" title="Código da Doença">
                CID Doença</label>
            <asp:DropDownList ID="ddlCodigoDoenca" Width="220px" runat="server" CssClass="ddlCodigoDoenca"
               ToolTip="Selecione o Código da Doença"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCodigoDoenca"
                ErrorMessage="Código da Doença é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDoenca" title="Nome da Doença">
                Complemento</label>
            <asp:TextBox ID="txtDoenca" runat="server" CssClass="txtDoenca" MaxLength="100" ToolTip="Informe o Nome da Doença"></asp:TextBox>            
        </li>
        <li>
            <label for="txtDataConsulta" title="Data da Consulta">
                Data Consulta</label>
            <asp:TextBox ID="txtDataConsulta" runat="server" CssClass="campoData" ToolTip="Informe a Data Consulta"></asp:TextBox>
        </li>
        <li id="liDiasLicenca">
            <label for="txtDiasLicenca" title="Número de dias de Licença">
                Qt Dias</label>
            <asp:TextBox ID="txtDiasLicenca" runat="server" CssClass="txtDiasLicenca campoNumerico"
                ToolTip="Informe o número de dias de Licença"></asp:TextBox>
        </li>
        <li>
            <label for="txtObservacao" title="Observações">
                Observação</label>
            <asp:TextBox ID="txtObservacao" runat="server" CssClass="txtObservacao" TextMode="MultiLine"
                onkeyup="javascript:MaxLength(this, 100);" ToolTip="Informe as observações"></asp:TextBox>
        </li>
        <li id="liDataEntrega">
            <label for="txtDataEntrega" title="Data da Entrega do Atestado">
                Data Entrega</label>
            <asp:TextBox ID="txtDataEntrega" runat="server" CssClass="campoData" ToolTip="Informe a Data da Entrega do Atestado"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDataCadastro" title="Data de Cadastro">
                Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" runat="server" CssClass="campoData" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label for="ddlResponsavel" class="lblObrigatorio" title="Colaborador Responsável pelo Cadastro">
                Colaborador Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="campoNomePessoa" Enabled="false">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlResponsavel"
                ErrorMessage="Colaborador Responsável é requerido" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtDiasLicenca").mask("?999");
        });
    </script>

</asp:Content>
