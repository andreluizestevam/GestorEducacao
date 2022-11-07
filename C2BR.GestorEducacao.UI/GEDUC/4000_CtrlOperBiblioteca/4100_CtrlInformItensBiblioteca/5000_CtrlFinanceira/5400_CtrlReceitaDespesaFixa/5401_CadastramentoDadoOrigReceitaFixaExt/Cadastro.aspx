<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5401_CadastramentoDadoOrigReceitaFixaExt.Cadastro" %>
<%@ Register src="~/Library/Componentes/ControleImagem.ascx" tagname="ControleImagem" tagprefix="uc1" %>
<%@ Register Src="~/Library/Componentes/FormularioEndereco.ascx" TagName="FormularioEndereco"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
    .ulDados { width: 805px;}
    .ulDados input{ margin-bottom: 0;}
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
        font-size:0.9em !important;
        height: 15px !important;
    }

    /*--> CSS LIs */
    .liUnidade { margin-top: 15px;}
    .liUnidade li, .liEndereco li { margin-bottom: 5px;}
    .liPessoaContato { margin-left: -21px;}
    .liPessoaContato li{ margin-top: 5px;}
    .liObservacao { clear: both; margin-right: 20px; }
    .liObservacao li{ margin-top: 5px;}
    .liSituacao li{ margin-top: 5px;}
    .liTitEndereco{ margin-top: 20px; border-left: solid 1px #dddddd; padding-left: 10px; margin-left: 0px;}
    .liNome{clear: both; margin-left: 108px; margin-bottom: 20px !important;}
    .liRazaoSocial{clear: both; margin-left: 108px; }
    .liSigla{margin-right: 67px; }
    .liEmail{clear: both; margin-right: 15px;}
    .liClear { clear: both; }
    .liTelefone2{ margin-right: 15px;}
    .liTelCelularContato{ margin-right: 50px;}
    .liDtCadastro{ margin-right: 30px;}
    .liEndereco { margin-top: 15px; }
    
    /*--> CSS DADOS*/
    .fldPessoaContato, .fldObservacao, .fldSituacao { padding: 5px;}
    .txtNomeContato{ width: 200px}
    .txtCargoContato{ width: 150px}
    .txtNome, .txtRazaoSocial { width: 210px; }
    .txtSigla { width: 34px; }
    .txtCNPJ { width: 100px; }
    .txtObservacao { width: 493px; height: 28px;}
    .txtEmail { width: 159px; }
    .txtWebSite { width: 150px; }
    .txtEmailContato{ width: 150px;}
    .ddlTipo { width: 90px; }
    
    /*--> CSS FORMULÁRIO ENDEREÇO */
    #ctl00_content_formularioEndereco_liNumero{clear: both;}
    #ctl00_content_formularioEndereco_liCidade{clear: both;}
    #ctl00_content_formularioEndereco_liPesquisarCep{ margin-top: 11px; margin-left: -3px;}
    #ctl00_content_formularioEndereco_liCorreios{ margin-top: 12px; margin-left: -3px; margin-right: 15px;}    
    #ctl00_content_formularioEndereco_btnCorreios{ width: 15px; height: 15px;}
    #ctl00_content_formularioEndereco_txtLogradouro { width: 222px; }
    #ctl00_content_formularioEndereco_txtComplemento { width: 95px; }
    #ctl00_content_formularioEndereco_ddlBairro { width: 170px; }
    #ctl00_content_formularioEndereco_ddlCidade { width: 195px; }
    #ctl00_content_formularioEndereco_txtCEP { width: 54px; }
    #ctl00_content_formularioEndereco_txtNumero{ width: 30px;}  
        
</style>

<script type="text/javascript">
    function SetCurrentSelectedTab(s) {
        $('.hiddenSelectedTab').val(s);
    }

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
            $("#liInscEstadual").attr('style', 'display: block;');
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
            $("#liInscEstadual").attr('style', 'display: none;');
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
            $("#liInscEstadual").attr('style', 'display: none;');
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
    <li class="liUnidade">
        <ul>
            <li>
                <label for="ddlTipoCategoria" title="Classificação da Fonte de Recurso" class="lblObrigatorio">Classificação</label>
                <asp:DropDownList ID="ddlTipoCategoria" ToolTip="Selecione a Classificação da Fonte de Recurso" CssClass="ddlTipoCategoria" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlTipoCategoria" 
                    ErrorMessage="Classificação deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlTipo" title="Tipo de Cliente">Tipo</label>
                <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo de Cliente" CssClass="ddlTipo" runat="server" onchange="SetCpfCnpj(this);">
                    <asp:ListItem Value="J">Pessoa Jurídica</asp:ListItem>
                    <asp:ListItem Value="F">Pessoa Física</asp:ListItem>                
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <label id="lblCNPJ" for="txtCNPJ" class="lblObrigatorio" title="CNPJ">CNPJ</label>
                <asp:TextBox ID="txtCNPJ" ToolTip="Informe o CNPJ" CssClass="txtCNPJ" runat="server" MaxLength="18"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCNPJ" ErrorMessage="CNPJ/CPF/Código deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li id="liInscEstadual">
                <label for="txtInscEstadual" title="Inscrição Estadual">Insc. Estadual</label>
                <asp:TextBox ID="txtInscEstadual" ToolTip="Informe a Inscrição Estadual" CssClass="txtNumControle" runat="server"></asp:TextBox>
            </li>
            <li class="liRazaoSocial">
                <label id="lblRazaoSocial" for="txtRazaoSocial" class="lblObrigatorio" title="Razão Social">Raz&atilde;o Social</label>
                <asp:TextBox ID="txtRazaoSocial" ToolTip="Informe a Razão Social" CssClass="txtRazaoSocial" MaxLength="80" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRazaoSocial" 
                    ErrorMessage="Razão Social/Nome deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liSigla">
                <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
                <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla da Unidade" CssClass="txtSigla" MaxLength="8" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSigla" 
                    ErrorMessage="Sigla deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liNome">
                <label id="lblNome" for="txtNome" class="lblObrigatorio" title="Nome">Nome Fantasia</label>
                <asp:TextBox ID="txtNome" ToolTip="Informe o Nome da Unidade" CssClass="txtNome" MaxLength="80" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNome" 
                    ErrorMessage="Nome Fantasia/Apelido deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
        </ul>
        <ul>
            <li class="liClear">
                <label for="txtTelefone" title="Telefone">Telefone (1)</label>
                <asp:TextBox ID="txtTelefone" ToolTip="Informe o Número do Telefone" CssClass="campoTelefone" runat="server"></asp:TextBox>
            </li>
            <li class="liTelefone2">
                <label for="txtTelefone2" title="Telefone 2">Telefone (2)</label>
                <asp:TextBox ID="txtTelefone2" ToolTip="Informe o Número do Telefone" CssClass="campoTelefone" runat="server"></asp:TextBox>
            </li>
            <li>
                <label for="txtFax" title="Fax">Fax</label>
                <asp:TextBox ID="txtFax" ToolTip="Informe o Número do Fax" CssClass="campoTelefone" runat="server"></asp:TextBox>
            </li>  
            <li class="liEmail">
                <label for="txtEmail" title="E-mail">E-mail</label>
                <asp:TextBox ID="txtEmail" ToolTip="Informe o E-mail" CssClass="txtEmail" MaxLength="60" runat="server"></asp:TextBox>
            </li>      
            <li>
                <label for="txtWebSite" title="Web Site">Web Site</label>
                <asp:TextBox ID="txtWebSite" ToolTip="Informe o Web Site" CssClass="txtWebSite" MaxLength="60" runat="server"></asp:TextBox>
            </li>
        </ul>
    </li>
    <li class="liTitEndereco">
        <img id="imgEnd" class="imgEnd" src="../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
    </li>    
    <li class="liEndereco">    
        <uc2:FormularioEndereco ID="formularioEndereco" runat="server" />
    </li>
    <li class="liPessoaContato">
        <fieldset class="fldPessoaContato">
            <legend>Pessoa para Contato</legend>
            <ul>
                <li>
                    <label for="txtNomeContato" class="lblObrigatorio" title="Nome do Contato">Contato</label>
                    <asp:TextBox ID="txtNomeContato" ToolTip="Informe o Nome do Contato" CssClass="txtNomeContato" MaxLength="60" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeContato" ErrorMessage="Nome do Contato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtCargoContato" class="lblObrigatorio" title="Função/Cargo do Contato">Função/Cargo</label>
                    <asp:TextBox ID="txtCargoContato" ToolTip="Informe a Função/Cargo do Contato" CssClass="txtCargoContato" MaxLength="60" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCargoContato" ErrorMessage="Função/Cargo do Contato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtTelFixoContato" class="lblObrigatorio" title="Telefone Fixo do Contato">Telefone Fixo</label>
                    <asp:TextBox ID="txtTelFixoContato" ToolTip="Informe o Telefone Fixo do Contato" CssClass="campoTelefone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelFixoContato" ErrorMessage="Telefone Fixo do Contato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liTelCelularContato">
                    <label for="txtTelCelularContato" class="lblObrigatorio" title="Telefone Celular do Contato">Telefone Celular</label>
                    <asp:TextBox ID="txtTelCelularContato" ToolTip="Informe o Telefone Celular do Contato" CssClass="campoTelefone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTelCelularContato" ErrorMessage="Telefone Celular do Contato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtEmailContato" class="lblObrigatorio" title="E-mail do Contato">E-mail</label>
                    <asp:TextBox ID="txtEmailContato" ToolTip="Informe o E-mail do Contato" CssClass="txtEmailContato" MaxLength="60" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmailContato" ErrorMessage="E-mail do Contato deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liObservacao">
        <fieldset class="fldObservacao">
            <legend>Observa&ccedil;&atilde;o</legend>
            <ul>
                <li>
                    <asp:TextBox ID="txtObservacao" runat="server" ToolTip="Informe a Observação" CssClass="txtObservacao" MaxLength="150"
                         TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtObservacao" ErrorMessage="Obsevação deve ter no máximo 150 caracteres" 
                        ValidationExpression="^(.|\s){1,150}$" CssClass="validatorField"></asp:RegularExpressionValidator>
                </li>
            </ul>
        </fieldset>
    </li>    
    <li class="liSituacao">
        <fieldset class="fldSituacao">
            <legend>Situa&ccedil;&atilde;o</legend>
            <ul>
                <li class="liDtCadastro">
                    <label for="txtDtCadastro" title="Data Cadastro">Data Cadastro</label>
                    <asp:TextBox ID="txtDtCadastro" ToolTip="Informe a Data de Cadastro" CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlStatus" class="lblObrigatorio" title="Status">Status</label>
                    <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o Status" runat="server">
                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStatus" ErrorMessage="Status deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="txtDtStatus" title="Data Status">Data Status</label>
                    <asp:TextBox ID="txtDtStatus" Enabled="False" ToolTip="Informe a Data Status" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:CompareValidator id="CompareValidatorDataAtual" runat="server" CssClass="validatorField"
                        ForeColor="Red"
                        ControlToValidate="txtDtStatus"                        
                        Type="Date"       
                        Operator="LessThanEqual"      
                        ErrorMessage="Data Status não pode ser maior que Data Atual." >
                    </asp:CompareValidator >
                </li>
            </ul>
        </fieldset>
    </li>
</ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".txtCEP").mask("99999-999");
    }); 
    $(document).ready(function() {
        SetCpfCnpj(document.getElementById('ctl00_content_ddlTipo'));
        $(".campoTelefone").mask("(99) 9999-9999");
        $(".txtCEP").mask("99999-999");
        $('.txtNumControle').mask("?9999999999999999");
    });
</script>
</asp:Content>