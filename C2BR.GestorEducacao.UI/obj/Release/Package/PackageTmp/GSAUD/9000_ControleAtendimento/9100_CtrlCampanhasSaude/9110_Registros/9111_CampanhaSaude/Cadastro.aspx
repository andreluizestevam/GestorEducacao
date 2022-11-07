<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9111_CampanhaSaude.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
            margin-left: 133px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-top:-6px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .campoHora
        {
            width: 28px;
        }
        .campoTelefone
        {
            width: 55px;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
        }
        input
        {
            height: 13px;
        }
        .chk label
        {
            display: inline;
            margin-left: -3px;
        }
        .lisobe
        {
            margin-top:-20px !important;
        }
        .fldFotoColab
        {
            margin-left: 0px;
            border: none;
            width: 90px;
            height: 130px;
        }
        .liFotoColab
        {
            width: 200px;
            float: left !important;
            margin-right: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoSitua" />
    <ul class="ulDados">
        <li style="margin-bottom:5px;">
            <label class="lblTitu">
                Dados Campanha</label>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Nome da Campanha de Saúde">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeCampanha" Width="290px" MaxLength="100" ToolTip="Nome da Campanha de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtNomeCampanha" ControlToValidate="txtNomeCampanha"
                ErrorMessage="Nome da Campanha é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -4px;">
            <label class="lblObrigatorio" title="Sigla da Campanha de Saúde">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" MaxLength="6" Width="45px" ToolTip="Sigla da Campanha de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtSigla"
                ErrorMessage="A Sigla da Campanha é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 41px">
            <label class="lblObrigatorio" title="Tipo da Campanha de Saúde">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                Width="124px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlTipoCamp"
                ErrorMessage="O Tipo da Campanha é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Classificação da Campanha de Saúde">
                Classificação</label>
            <asp:DropDownList runat="server" ID="ddlClassCamp" Width="110px" ToolTip="Classificação da Campanha de Saúde">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlClassCamp"
                ErrorMessage="A Classificação da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Competência da Campanha de Saúde">
                Competência</label>
            <asp:DropDownList runat="server" ID="ddlCompetencia" Width="80px" ToolTip="Competência da Campanha de Saúde">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlCompetencia"
                ErrorMessage="A Competência é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label>
                Slogan da Campanha</label>
            <asp:TextBox runat="server" ID="txtSlogan" Width="430px"></asp:TextBox>
        </li>
        <li style="margin-left: 76px;">
            <label class="lblObrigatorio" title="Data de Início da Campanha de Saúde">
                Data/Hora Início</label>
            <asp:TextBox runat="server" ID="txtDataInicio" CssClass="campoData" ToolTip="Data de Início da Campanha de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDataInicio"
                ErrorMessage="A Data de Início da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin: 8px 0 0 -4px;">
            <asp:TextBox runat="server" ID="txtHrInicio" ToolTip="Hora de Início da Campanha de Saúde"
                CssClass="campoHora"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtHrInicio"
                ErrorMessage="A Hora de Início da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" title="Data de Término da Campanha de Saúde">
                Data/Hora Término</label>
            <asp:TextBox runat="server" ID="txtDataTermino" CssClass="campoData" ToolTip="Data de Término da Campanha de Saúde"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtDataTermino"
                ErrorMessage="A Data de Término da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin: 8px 0 0 -12px;">
            <asp:TextBox runat="server" ID="txtHrTermino" ToolTip="Hora de Término da Campanha de Saúde"
                CssClass="campoHora"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtHrTermino"
                ErrorMessage="A Hora de Término da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label title="Responsável pela Campanha de Saúde">
                Responsável</label>
            <asp:CheckBox runat="server" ID="chkRespEhFunc" Text="É Funcionario(a)" CssClass="chk"
                OnCheckedChanged="chkRespEhFunc_OnCheckedChanged" AutoPostBack="true" Checked="true"
                Style="margin-left: -5px;" />
            <asp:DropDownList runat="server" ID="ddlRespCamp" Width="202px" ToolTip="Responsável pela Campanha de Saúde"
                Style="margin-left: 2px;">
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtNoResp" ToolTip="Nome do Responsável pela Campanha de Saúde"
                MaxLength="100" Width="202px" Visible="false"></asp:TextBox>
        </li>
        <li style="margin-left: -1px;">
            <label title="Número do Telefone Fixo do Responsável pela Campanha de Saúde">
                Telefone</label>
            <asp:TextBox runat="server" ID="txtTelResResp" CssClass="campoTelefone" ToolTip="Número do Telefone Fixo do Responsável pela Campanha de Saúde"></asp:TextBox>
        </li>
        <li>
            <label title="Número do Celular do(a) Responsável pela Campanha de Saúde">
                Celular</label>
            <asp:TextBox runat="server" ID="txtTelCelResp" CssClass="campoTelefone" ToolTip="Número do Celular do(a) Responsável pela Campanha de Saúde"></asp:TextBox>
        </li>
        <li>
            <label title="Número do aplicativo WhatsApp do(a) Responsável pela Campanha de Saúde">
                Nº WhatsApp</label>
            <asp:TextBox runat="server" ID="txtNuWhatsResp" CssClass="campoTelefone" ToolTip="Número do aplicativo WhatsApp do(a) Responsável pela Campanha de Saúde"></asp:TextBox>
        </li>
        <li>
            <label title="Email do(a) Responsável pela Campanha de Saúde">
                Email</label>
            <asp:TextBox runat="server" ID="txtEmailResp" Width="195px" ToolTip="Email do(a) Responsável pela Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="margin:5px 0 5px 5px; clear:both">
            <label class="lblTitu" title="Informações do Local onde a Campanha de Saúde acontecerá">
                Local da Campanha</label>
        </li>
        <li style="clear: both">
            <label>
                Unidade da Campanha</label>
            <asp:CheckBox runat="server" ID="chkEhUnidadeCadastrada" Text="É Unidade Cadastrada"
                CssClass="chk" OnCheckedChanged="chkEhUnidadeCadastrada_OnCheckedChanged" AutoPostBack="true"
                Checked="true" Style="margin-left: -5px;" />
            <asp:DropDownList runat="server" ID="ddlUnidCampa" Width="233px" ToolTip="Nome da Unidade da Campanha de Saúde">
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtNomeLocal" Width="232px" ToolTip="Local onde a Campanha será realizada"
                Visible="false"></asp:TextBox>
        </li>
        <li style="margin-left: 17px;">
            <label title="Telefone fixo do local da Campanha de Saúde">
                Telefone</label>
            <asp:TextBox runat="server" ID="txtTeleLocal" CssClass="campoTelefone" ToolTip="Telefone fixo do local da Campanha de Saúde"></asp:TextBox>
        </li>
        <li>
            <label title="Telefone celular do local da Campanha">
                Celular</label>
            <asp:TextBox runat="server" ID="txtCeluLocal" CssClass="campoTelefone" ToolTip="Telefone celular do local da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="margin-left: 17px;">
            <label title="Email do local da Campanha de Saúde">
                Email</label>
            <asp:TextBox runat="server" ID="txtEmailLocal" Width="195px" ToolTip="Email do local da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="CEP do Local da Campanha de Saúde">
                CEP</label>
            <asp:TextBox runat="server" ID="txtCEPLocal" Width="52px" CssClass="campoCEP" ToolTip="CEP do Local da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="margin-left:12px;">
            <label title="UF do Local da Campanha de Saúde">
                UF</label>
            <asp:DropDownList runat="server" ID="ddlUFLocal" Width="40px" OnSelectedIndexChanged="ddlUFLocal_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="UF do Local da Campanha de Saúde">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Cidade do Local da Campanha de Saúde">
                Cidade</label>
            <asp:DropDownList runat="server" ID="ddlCidadeLocal" Width="160px" ToolTip="Cidade do Local da Campanha de Saúde"
            OnSelectedIndexChanged="ddlCidadeLocal_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="margin-left:12px;">
            <label title="Bairro do Local da Campanha de Saúde">
                Bairro</label>
            <asp:DropDownList runat="server" ID="ddlBairroLocal" Width="163px" ToolTip="Bairro do Local da Campanha de Saúde">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Endereço do Local da Campanha de Saúde">
                Endereço</label>
            <asp:TextBox runat="server" ID="txtEndeLocal" Width="280px" ToolTip="Endereço do Local da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="margin:5px 0 5px 5px; clear:both">
            <label class="lblTitu" title="Informações complementares da Campanha de Saúde">
                Informações Complementares</label>
        </li>
        <li style="clear: both">
            <label title="Descrição da Campanha de Saúde">
                Descrição</label>
            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" Height="100px" Width="250px"
                ToolTip="Descrição da Campanha de Saúde"></asp:TextBox>
        </li>
        <li>
            <label title="Observação sobre a Campanha de Saúde">
                Observação</label>
            <asp:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Height="100px"
                Width="250px" ToolTip="Observação sobre a Campanha de Saúde"></asp:TextBox>
        </li>
        <li class="liFotoColab" style="margin-left:42px;">
        <label>Imagem da Campanha</label>
            <fieldset class="fldFotoColab" style="width: 160px">
                <uc1:ControleImagem ID="upImagemProdu" runat="server" style="margin-left: 20px" />
            </fieldset>
        </li>
        <li style="clear: both" class="lisobe">
            <label title="Link externo para o HotSite da Campanha de Saúde">
                Link Externo - HotSite da Campanha</label>
            <asp:TextBox runat="server" ID="txtLinkHomePage" Width="280px" ToolTip="Link externo para o HotSite da Campanha de Saúde"></asp:TextBox>
        </li>
        <li class="lisobe">
            <label title="Link externo para a mídia de divulgação da Campanha de Saúde">
                Link Externo - Mídia de Divulgação da Campanha</label>
            <asp:TextBox runat="server" ID="txtLinkMidia" Width="280px" ToolTip="Link externo para a mídia de divulgação da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="margin-left: 94px;" class="lisobe">
            <label class="lblObrigatorio" title="Situação da Campanha de Saúde">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" ToolTip="Situação da Campanha de Saúde"
                Width="80px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlSituacao"
                ErrorMessage="A Situação da Campanha de Saúde é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoHora").mask("99:99");
            $(".campoTelefone").mask("(99)9999-9999");
            $(".campoCEP").mask("99999-999");
        });
    </script>
</asp:Content>
