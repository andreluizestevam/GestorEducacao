<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._7000_ControleOperRH._7950_CtrlCadastralParceiros._7951_CadastroParceiros.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<%@ Register Src="~/Library/Componentes/FormularioEndereco.ascx" TagName="FormularioEndereco"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 880px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        input[type="text"]
        {
            font-size: 10px !important;
            font-family: Arial !important;
        }
        select
        {
            margin-bottom: 0;
            font-family: Arial !important;
            border: 1px solid #BBBBBB !important;
            font-size: 0.9em !important;
            height: 15px !important;
        }
        
        /*--> CSS LIs */
        .liUnidade
        {
        }
        .liUnidade li, .liEndereco li
        {
            margin-bottom: 5px;
        }
        .liPessoaContato
        {
            margin-left: -21px;
        }
        .liPessoaContato li
        {
            margin-top: 5px;
        }
        .liObservacao
        {
            clear: both;
            margin-right: 20px;
            margin-top: 5px;
        }
        .liObservacao li
        {
            margin-top: 5px;
        }
        .liSituacao li
        {
            margin-top: 5px;
        }
        .liTitEndereco
        {
            margin-top: 20px;
            border-left: solid 1px #dddddd;
            padding-left: 10px;
            margin-left: 0px;
        }
        .liNome
        {
            margin-bottom: 7px !important;
        }
        .liRazaoSocial
        {
            clear: both;
        }
        .liSigla
        {
            margin-right: 67px;
        }
        .liEmail
        {
            clear: both;
            margin-right: 15px;
        }
        .liClear
        {
            clear: both;
        }
        .liTelefone2
        {
            margin-right: 15px;
        }
        .liTelCelularContato
        {
            margin-right: 50px;
        }
        .liDtCadastro
        {
            margin-right: 30px;
        }
        .liEndereco
        {
            margin-top: 3px;
        }
        
        /*--> CSS DADOS*/
        .fldPessoaContato, .fldObservacao, .fldSituacao
        {
            padding: 5px;
        }
        .txtNomeContato
        {
            width: 187px;
        }
        .txtCargoContato
        {
            width: 150px;
        }
        .txtNome, .txtRazaoSocial
        {
            width: 292px;
        }
        .txtSigla
        {
            width: 34px;
        }
        .txtCNPJ
        {
            width: 100px;
        }
        .txtCPF
        {
            width: 100px;
        }
        .txtObservacao
        {
            width: 452px;
            height: 12px;
        }
        .liResponsavel
        {
            width: 471px;
        }
        .txtEmail
        {
            width: 180px;
        }
        .txtWebSite
        {
            width: 150px;
        }
        .txtEmailContato
        {
            width: 151px;
        }
        .ddlTipo
        {
            width: 90px;
        }
        .ddlTipoCategoria
        {
            width: 180px;
        }
        
        /*--> CSS FORMULÁRIO ENDEREÇO */
        #ctl00_content_formularioEndereco_liNumero
        {
            clear: both;
        }
        #ctl00_content_formularioEndereco_liCidade
        {
            clear: both;
        }
        #ctl00_content_formularioEndereco_liPesquisarCep
        {
            margin-top: 11px;
            margin-left: -3px;
        }
        #ctl00_content_formularioEndereco_liCorreios
        {
            margin-top: 12px;
            margin-left: -3px;
            margin-right: 15px;
        }
        #ctl00_content_formularioEndereco_btnCorreios
        {
            width: 15px;
            height: 15px;
        }
        #ctl00_content_formularioEndereco_txtLogradouro
        {
            width: 222px;
        }
        #ctl00_content_formularioEndereco_txtComplemento
        {
            width: 95px;
        }
        #ctl00_content_formularioEndereco_ddlBairro
        {
            width: 170px;
        }
        #ctl00_content_formularioEndereco_ddlCidade
        {
            width: 195px;
        }
        #ctl00_content_formularioEndereco_txtCEP
        {
            width: 54px;
        }
        #ctl00_content_formularioEndereco_txtNumero
        {
            width: 30px;
        }
        .txtNomeFantasia
        {
            width: 113px;
        }
        .txtSkype
        {
            width: 136px;
        }
        .propNegocio
        {
            width: 401PX;
            height: 245px;
            background-color: #FFFFE0;
            font-size: 12px;
        }
        .liEmailIndicacao
        {
            clear: both;
        }
        .txtEmailIndicacao
        {
            width: 187px;
        }
        .txtSkypeIndicacao
        {
            width: 177px;
        }
        .txtNomeIndicacao
        {
            width: 188px;
        }
        .liLeft
        {
            width: 465px;
        }
        .liRight
        {
            width: 366px;
        }
        .PropostaNegocio
        {
            margin-top: 5px;
        }
        .ddlProspeccao
        {
            width: 100%;
        }
        .Inscricoes
        {
            width: 122px;
        }
    </style>
    <script type="text/javascript">
        function SetCpfCnpj(control) {
            $(".txtCNPJ").unmask();

            if (control.selectedIndex == 0) {
                $(".txtCNPJ").mask("99.999.999/9999-99");
                $(".txtCNPJ").attr('title', 'Informe o CNPJ');
                $("#lblCNPJ").attr('innerHTML', 'CNPJ');
                $("#lblCNPJ").attr('title', 'CNPJ');
                $("#lblRazaoSocial").attr('innerHTML', 'Razão Social');
                $("#lblRazaoSocial").attr('title', 'Razão Social');
                $(".txtRazaoSocial").attr('title', 'Informe a Razão Social');
                $("#lblNome").attr('innerHTML', 'Nome Fantasia');
                $("#lblNome").attr('title', 'Nome Fantasia');
                $(".txtNome").attr('title', 'Informe o Nome Fantasia');
                $(".liInscEstadual").attr('style', 'display: block;');
            }
            else if (control.selectedIndex == 1) {
                $(".txtCNPJ").mask("999.999.999-99");
                $(".txtCNPJ").attr('title', 'Informe o CPF');
                $("#lblCNPJ").attr('innerHTML', 'CPF');
                $("#lblCNPJ").attr('title', 'CPF');
                $("#lblRazaoSocial").attr('innerHTML', 'Nome');
                $("#lblRazaoSocial").attr('title', 'Nome');
                $(".txtRazaoSocial").attr('title', 'Informe o Nome');
                $("#lblNome").attr('innerHTML', 'Apelido');
                $("#lblNome").attr('title', 'Apelido');
                $(".txtNome").attr('title', 'Informe o Apelido');
                $(".liInscEstadual").attr('style', 'display: none;');
            }
            else {
                $(".txtCNPJ").mask("?99999999999999");
                $(".txtCNPJ").attr('title', 'Informe o Código');
                $("#lblCNPJ").attr('innerHTML', 'Código');
                $("#lblCNPJ").attr('title', 'Código');
                $("#lblRazaoSocial").attr('innerHTML', 'Nome');
                $("#lblRazaoSocial").attr('title', 'Nome');
                $(".txtRazaoSocial").attr('title', 'Informe o Nome');
                $("#lblNome").attr('innerHTML', 'Apelido');
                $("#lblNome").attr('title', 'Apelido');
                $(".txtNome").attr('title', 'Informe o Apelido');
                $(".liInscEstadual").attr('style', 'display: none;');
            }
        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <ul class="liLeft">
                <li class="liUnidade">
                    <ul>
                        <li>
                            <label for="ddlTipo" title="Tipo de Participante" class="lblObrigatorio">
                                Tipo</label>
                            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo de Participante" CssClass="ddlTipo"
                                runat="server" onchange="SetCpfCnpj(this);">
                                <asp:ListItem Value="J" Selected="True">Pessoa Jurídica</asp:ListItem>
                                <asp:ListItem Value="F">Pessoa Física</asp:ListItem>
                                <asp:ListItem Value="O">Outros</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label id="lblCNPJ" for="txtCNPJ" class="lblObrigatorio" title="CNPJ">
                                CNPJ</label>
                            <asp:TextBox ID="txtCNPJ" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server"
                                MaxLength="14"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCNPJ"
                                ErrorMessage="CNPJ/CPF/Código deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li id="liInscEstadual" class="liInscEstadual">
                            <label for="txtInscEstadual" title="Inscrição Estadual">
                                Insc. Estadual</label>
                            <asp:TextBox ID="txtInscEstadual" CssClass="Inscricoes" ToolTip="Informe a Inscrição Estadual"
                                MaxLength="20" runat="server"></asp:TextBox>
                        </li>
                        <li id="liInscMunicipal" class="liInscEstadual">
                            <label for="txtInscMunicipal" title="Inscrição Municipal">
                                Insc. Municipal</label>
                            <asp:TextBox ID="txtInscMunicipal" CssClass="Inscricoes" ToolTip="Informe a Inscrição Municipal"
                                MaxLength="20" runat="server"></asp:TextBox>
                        </li>
                        <li class="liRazaoSocial">
                            <label id="lblRazaoSocial" for="txtRazaoSocial" class="lblObrigatorio" title="Razão Social">
                                Raz&atilde;o Social</label>
                            <asp:TextBox ID="txtRazaoSocial" ToolTip="Informe a Razão Social" CssClass="txtRazaoSocial"
                                MaxLength="120" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRazaoSocial"
                                ErrorMessage="Razão Social/Nome deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liSigla">
                            <label for="txtSigla" class="lblObrigatorio" title="Sigla">
                                Sigla</label>
                            <asp:TextBox ID="txtSigla" ToolTip="Informe a sigla da razão social" CssClass="txtSigla"
                                MaxLength="12" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSigla"
                                ErrorMessage="Sigla deve ser informada" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liNome">
                            <label id="lblNome" for="txtNome" class="lblObrigatorio" title="Nome">
                                Nome Fantasia</label>
                            <asp:TextBox ID="txtNome" ToolTip="Informe o nome fantasia da razão  social" CssClass="txtNomeFantasia"
                                MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNome"
                                ErrorMessage="Nome Fantasia/Apelido deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                    </ul>
                    <ul>
                        <li class="liClear">
                            <label for="txtTelefone" title="Telefone">
                                Telefone (1)</label>
                            <asp:TextBox ID="txtTelefone" ToolTip="Informe o número do Telefone" CssClass="campoTelefone"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liTelefone2">
                            <label for="txtTelefone2" title="Telefone 2">
                                Telefone (2)</label>
                            <asp:TextBox ID="txtTelefone2" ToolTip="Informe o número do Telefone" CssClass="campoTelefone"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liWhatsapp">
                            <label for="txtWhatsapp" title="txtWhatsApp">
                                WhatsApp</label>
                            <asp:TextBox ID="txtWhatsapp" ToolTip="Informe o número do WhatsApp" CssClass="campoTelefone"
                                runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtFax" title="Fax">
                                Fax</label>
                            <asp:TextBox ID="txtFax" ToolTip="Informe o Número do Fax" CssClass="campoTelefone"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liSkype">
                            <label for="txtSkype" title="Skype">
                                Skype</label>
                            <asp:TextBox ID="txtSkype" ToolTip="Informe o contato do Skype" CssClass="txtSkype"
                                MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEmail">
                            <label for="txtEmail" title="E-mail">
                                E-mail</label>
                            <asp:TextBox ID="txtEmail" ToolTip="Informe o E-mail" CssClass="txtEmail" MaxLength="160"
                                runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtWebSite" title="Web Site">
                                Web Site</label>
                            <asp:TextBox ID="txtWebSite" ToolTip="Informe o Web Site" CssClass="txtWebSite" MaxLength="60"
                                runat="server"></asp:TextBox>
                        </li>
                        <li class="liEmailIndicacao">
                            <label>
                                Observação</label>
                            <asp:TextBox ID="txtObservacao" runat="server" ToolTip="Informe a Observação" CssClass="txtObservacao"
                                MaxLength="200" onkeyDown="checkTextAreaMaxLength(this,event,'200');" TextMode="MultiLine"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liObservacao liResponsavel">
                    <label style="color: #0000CD;">
                        RESPONSÁVEL DO PARCEIRO</label>
                    <ul>
                        <li>
                            <label for="txtNomeRespParceiro" title="Nome do Contato">
                                Responsável</label>
                            <asp:TextBox ID="txtNomeRespParceiro" ToolTip="Informe o Nome do Responsável" CssClass="txtNomeContato"
                                MaxLength="100" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label id="Label1" for="txtCPFRespParceiro" class="" title="CPF">
                                CPF</label>
                            <asp:TextBox ID="txtCPFRespParceiro" ToolTip="Informe o CPF" CssClass="txtCPF" runat="server"
                                MaxLength="14"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtCargoRespParceiro" title="Função/Cargo do Responsável">
                                Função/Cargo</label>
                            <asp:TextBox ID="txtCargoRespParceiro" ToolTip="Informe a Função/Cargo do Responsável"
                                CssClass="txtCargoContato" MaxLength="40" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="txtTelRespParceiro" title="Telefone do Responsável">
                                Telefone
                            </label>
                            <asp:TextBox ID="txtTelRespParceiro" ToolTip="Informe o Telefone do Responsável"
                                CssClass="campoTelefone" runat="server"></asp:TextBox>
                        </li>
                        <li class="liWhatsappResp">
                            <label for="txtWhatsappResp" title="txtWhatsAppResp">
                                WhatsApp</label>
                            <asp:TextBox ID="txtWhatsappResp" ToolTip="Informe o número do WhatsApp" CssClass="campoTelefone"
                                runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtEmailResp" title="E-mail do Responsável">
                                E-mail</label>
                            <asp:TextBox ID="txtEmailResp" ToolTip="Informe o E-mail do Responsável" CssClass="txtEmailContato"
                                MaxLength="160" runat="server"></asp:TextBox>
                        </li>
                        <li class="liSkypeResp">
                            <label for="txtSkypeResp" title="Skype">
                                Skype</label>
                            <asp:TextBox ID="txtSkypeResp" ToolTip="Informe o contato do Skype" CssClass="txtSkype"
                                MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                        <li class=".txtObservacao">
                            <label>
                                Observa&ccedil;&atilde;o
                            </label>
                            <asp:TextBox ID="txtObsRespParceiro" runat="server" ToolTip="Informe a Observação"
                                CssClass="txtObservacao" MaxLength="200" onkeyDown="checkTextAreaMaxLength(this,event,'200');"
                                TextMode="MultiLine"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liObservacao">
                    <label style="color: #0000CD;">
                        INDICAÇÃO DO PARCEIRO</label>
                    <ul>
                        <li>
                            <asp:CheckBox runat="server" ID="chcFuncionario" ToolTip="Quem indicou é funcionário da empresa?"
                                Text="Funcionário" AutoPostBack="true" OnCheckedChanged="chcFuncionario_CheckedChanged" />
                        </li>
                        <li id="liNomeIndicacao" runat="server">
                            <label for="txtNomeRespParceiro" title="Nome do Contato">
                                Nome</label>
                            <asp:TextBox ID="txtNomeIndicacao" ToolTip="Informe o nome de quem indicou este parceiro"
                                CssClass="" MaxLength="100" Width="237px" runat="server"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="ddlNomeIndicacao" Visible="false" Width="237px">
                            </asp:DropDownList>
                        </li>
                        <li class="">
                            <label for="txtTelIndicacao" title="Telefone de quem indicou o parceiro">
                                Telefone
                            </label>
                            <asp:TextBox ID="txtTelIndicacao" ToolTip="Informe o Telefone de quem indicou o parceiro"
                                CssClass="campoTelefone" runat="server"></asp:TextBox>
                        </li>
                        <li class="liWhatsappIndicacao">
                            <label for="txtWhatsappIndicacao" title="WhatsApp de quem indicou o parceiro">
                                WhatsApp</label>
                            <asp:TextBox ID="txtWhatsappIndicacao" ToolTip="Informe o número do WhatsApp de quem indicou o parceiro"
                                CssClass="campoTelefone" runat="server"></asp:TextBox>
                        </li>
                        <li class="liEmailIndicacao">
                            <label for="txtEmailIndicacao" title="E-mail de quem indicou o parceiro">
                                E-mail</label>
                            <asp:TextBox ID="txtEmailIndicacao" ToolTip="Informe o E-mail de quem indicou o parceiro"
                                CssClass="txtEmailIndicacao" MaxLength="160" runat="server"></asp:TextBox>
                        </li>
                        <li class="liSkypeIndicacao">
                            <label for="txtSkypeIndicacao" title="Skype">
                                Skype</label>
                            <asp:TextBox ID="txtSkypeIndicacao" ToolTip="Informe o contato do Skype de quem indicou o parceiro"
                                CssClass="txtSkypeIndicacao" MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtDtIndicacao" title="Data da indicação">
                                Data Indicação</label>
                            <asp:TextBox ID="txtDtIndicacao" Enabled="False" ToolTip="Informe a data da indicação"
                                CssClass="campoData" runat="server"></asp:TextBox>
                        </li>
                        <li class=".txtObservacao">
                            <label>
                                Observa&ccedil;&atilde;o
                            </label>
                            <asp:TextBox ID="txtObsIndicacao" runat="server" ToolTip="Informe a Observação" CssClass="txtObservacao"
                                MaxLength="200" onkeyDown="checkTextAreaMaxLength(this,event,'200');" TextMode="MultiLine"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liSituacao liObservacao">
                    <label style="color: #0000CD;">
                        SITUAÇÃO</label>
                    <ul>
                        <li class="liDtCadastro">
                            <label for="txtDtCadastro" title="Data Cadastro">
                                Data Cadastro</label>
                            <asp:TextBox ID="txtDtCadastro" ToolTip="Informe a Data de Cadastro" CssClass="campoData"
                                Enabled="false" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDtStatus"
                                ErrorMessage="Data Cadastro deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlStatus" class="lblObrigatorio" title="Status">
                                Status</label>ddlStatus
                            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o Status" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
                                <asp:ListItem Value="A">Ativo</asp:ListItem>
                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlStatus"
                                ErrorMessage="Status deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="txtDtStatus" title="Data Status">
                                Data Status</label>
                            <asp:TextBox ID="txtDtStatus" Enabled="False" ToolTip="Informe a Data Status" CssClass="campoData"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDtStatus"
                                ErrorMessage="Data Status deve ser informado" Display="None"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidatorDataAtual" runat="server" CssClass="validatorField"
                                ForeColor="Red" ControlToValidate="txtDtStatus" Type="Date" Operator="LessThanEqual"
                                ErrorMessage="Data Status não pode ser maior que Data Atual.">
                            </asp:CompareValidator>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li class="liRight">
            <ul>
                <li class="liTitEndereco">
                    <img id="imgEnd" class="imgEnd" src="../../../../../Library/IMG/Gestor_ImgEndereco.png"
                        alt="endereço" />
                </li>
                <li class="liEndereco">
                    <uc2:FormularioEndereco ID="formularioEndereco" runat="server" />
                </li>
                <li style="margin-top: 14px">
                    <label style="color: #0000CD; margin-bottom: 6PX;">
                        <strong>PROPOSTA DE NEGÓCIO</strong></label>
                </li>
                <li>
                    <ul style="display: flex;">
                        <li class="liClear" style="width: 172px;">
                            <label>
                                Área de Prospecção <span style="color:#FF0000">*</span></label>
                            <asp:DropDownList runat="server" ID="ddlAreaProspeccao" CssClass="ddlProspeccao">
                                <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                                <asp:ListItem Value="EDU">Educação</asp:ListItem>
                                <asp:ListItem Value="SAU">Saúde</asp:ListItem>
                                <asp:ListItem Value="ENE">Energia</asp:ListItem>
                                <asp:ListItem Value="ESP">Esporte</asp:ListItem>
                                <asp:ListItem Value="SEG">Seguros</asp:ListItem>
                                <asp:ListItem Value="BIA">Bio-Agro</asp:ListItem>
                                <asp:ListItem Value="CON">Consórcios</asp:ListItem>
                                <asp:ListItem Value="COM">Commodites</asp:ListItem>
                                <asp:ListItem Value="INF">Infra-estrutura</asp:ListItem>
                                <asp:ListItem Value="MAM">Meio Ambiente</asp:ListItem>
                                <asp:ListItem Value="COI">Construção e Incorporação</asp:ListItem>
                                <asp:ListItem Value="ASM">Assest Management</asp:ListItem>
                                <asp:ListItem Value="GAI">Gestão de Ativos Imobiliários</asp:ListItem>
                                <asp:ListItem Value="TEC">Tecnologia</asp:ListItem>
                                <asp:ListItem Value="PAG">Meio de Pagamentos</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="liAnexo" class="liAnexo">
                            <label>
                                Anexo</label>
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="btnAnexo" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </li>
                <li class="PropostaNegocio ">
                    <label>
                        Descrição Resumida da Proposta de Negócios</label>
                    <asp:TextBox ID="propNegocio" runat="server" ToolTip="Informe de forma resumida a proposta de negócio apresentada pelo Parceiro." CssClass="propNegocio"
                        MaxLength="5000" onkeyDown="checkTextAreaMaxLength(this,event,'1000');" TextMode="MultiLine"></asp:TextBox>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtCEP").mask("99999-999");
        });
        $(document).ready(function () {
            SetCpfCnpj(document.getElementById('ctl00_content_ddlTipo'));
            $(".campoTelefone").mask("(99) 9999-9999");
            $(".txtCEP").mask("99999-999");
            $(".txtCPF").mask("999.999.999-99");
        });
    </script>
</asp:Content>
