<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5911_LocalCobranca.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 660px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liDados
        {
            clear: both;
            width: 350px;
        }
        .liEndereco { width: 300px; }
        .liTelefones
        {
            clear: both;
            width: 260px;
        }
        .liCampoSituacao { margin-left: 10px; }
        .liSituacao { width: 200px; }
        .liEspaco { margin-top:-5px !important; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liDados">
            <fieldset id="fldDados">
                <legend>Dados</legend>
                <ul id="ulDadosEmpresa">
                    <li class="liClear">
                        <label for="txtNomeFantasia" class="lblObrigatorio">
                            Nome Fantasia</label>
                        <asp:TextBox ID="txtNomeFantasia" CssClass="txtDescricao" runat="server" MaxLength="80"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvNomeFantasia" runat="server"
                            ControlToValidate="txtNomeFantasia" ErrorMessage="Nome Fantasia deve ser informado"
                            Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtRazaoSocial" class="lblObrigatorio">
                            Razão Social</label>
                        <asp:TextBox ID="txtRazaoSocial" CssClass="txtRazaoSocial" runat="server" MaxLength="80"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvRazaoSocial" runat="server"
                            ControlToValidate="txtRazaoSocial" ErrorMessage="Razão Social deve ser informada"
                            Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li id="liTipoEmpresa" class="liClear">
                        <label for="ddlTipoEmpresa">
                            Categoria da Empresa</label>
                        <asp:DropDownList ID="ddlTipoEmpresa" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="txtCpfCnpj">
                            CPF/CNPJ</label>
                        <asp:TextBox ID="txtCpfCnpj" runat="server" MaxLength="14"></asp:TextBox>
                        <asp:CustomValidator ControlToValidate="txtCpfCnpj" ID="cvCpfCnpj" runat="server"
                            ErrorMessage="CPF/CNPJ inválido" Text="*" Display="None" CssClass="validatorField"
                            OnServerValidate="cvCpfCnpj_ServerValidate" EnableClientScript="false"></asp:CustomValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtInscricaoEstadual">
                            Inscrição Estadual</label>
                        <asp:TextBox ID="txtInscricaoEstadual" runat="server" MaxLength="20"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtInscricaoMunicipal">
                            Inscrição Municipal</label>
                        <asp:TextBox ID="txtInscricaoMunicipal" runat="server" MaxLength="20"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liEndereco">
            <fieldset id="fldEndereco">
                <legend>Endereço</legend>
                <ul id="ulEndereco">
                    <li class="liClear">
                        <label for="txtEndereco">
                            Endereço</label>
                        <asp:TextBox ID="txtEndereco" CssClass="txtLogradouro" runat="server" MaxLength="80"></asp:TextBox>
                    </li>
                    <li class="liClear">
                        <label for="txtComplemento">
                            Complemento</label>
                        <asp:TextBox ID="txtComplemento" CssClass="txtComplemento" runat="server" MaxLength="30"></asp:TextBox>
                    </li>
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <li class="liClear">
                        <label for="ddlCidade">
                            Cidade</label>
                        <asp:DropDownList ID="ddlCidade" runat="server" CssClass="campoCidade" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liClear">
                        <label for="ddlBairro">
                            Bairro</label>
                        <asp:DropDownList ID="ddlBairro" CssClass="campoBairro" runat="server">
                        </asp:DropDownList>
                    </li>
                    <li class="liClear">
                        <label for="ddlUf" class="lblObrigatorio">
                            UF</label>
                        <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <li>
                        <label for="txtCep" class="lblObrigatorio">
                            CEP</label>
                        <asp:TextBox ID="txtCep" runat="server" CssClass="campoCep" MaxLength="8"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvCep" runat="server"
                            ControlToValidate="txtCep" ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liTelefones">
            <fieldset id="fldTelefones">
                <legend>Contato</legend>
                <ul id="ulTelefones">
                    <li>
                        <label for="txtTelefone1">
                            Telefone 1</label>
                        <asp:TextBox ID="txtTelefone1" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtTelefone2">
                            Telefone 2</label>
                        <asp:TextBox ID="txtTelefone2" CssClass="campoTelefone" runat="server" MaxLength="20"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtFax">
                            Fax</label>
                        <asp:TextBox ID="txtFax" runat="server" CssClass="campoTelefone" MaxLength="20"></asp:TextBox>
                    </li>
                    <li id="liHomePage" class="liClear,liEspaco">
                        <label for="txtHomePage">
                            Home-Page</label>
                        <asp:TextBox ID="txtHomePage" runat="server" CssClass="txtEmail" MaxLength="60"></asp:TextBox>
                    </li>
                    <li class="liClear">
                        <label for="txtEmail, liEspaco">
                            E-mail</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail" MaxLength="160"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li id="liObservacao">
            <fieldset id="fldObservacao">
                <legend>Observação</legend>
                <asp:TextBox ID="txtObservacao" TextMode="MultiLine" runat="server" Height="100px"
                    Width="170px"></asp:TextBox>
            </fieldset>
        </li>
        <li class="liSituacao">
            <fieldset id="fldSituacaoLocal">
                <legend>Situação</legend>
                <ul id="ulSituacao">
                    <li class="liClear">
                        <label for="txtDataInclusao" class="lblObrigatorio">
                            Data da Inclusão</label>
                        <asp:TextBox ID="txtDataInclusao" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvDataInclusao" runat="server"
                            ControlToValidate="txtDataInclusao" ErrorMessage="Data da Inclusão deve ser informada"
                            Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="rblSituacao">
                            Situação</label>
                        <asp:DropDownList ID="rblSituacao" runat="server">
                            <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liCampoSituacao">
                        <label for="txtDataSituacao" class="lblObrigatorio">
                            Data da Situação</label>
                        <asp:TextBox ID="txtDataSituacao" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="validatorField" ID="rfvDataSituacao" runat="server"
                            ControlToValidate="txtDataSituacao" ErrorMessage="Data da Situação deve ser informada"
                            Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                </ul>
            </fieldset>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
            $("#txtCpfCnpj").mask("99.999.999/9999-99");
            $(".txtCep").mask("99999-999");
            $(".campoTelefone").mask("(99)9999-9999");
        });
    </script>

</asp:Content>
