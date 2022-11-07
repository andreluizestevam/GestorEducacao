<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1230_CrtlMovimentacaoFuncional.F1232_InstituicaoTransferencia.Cadastro" %>
<%@ Register src="~/Library/Componentes/ControleImagem.ascx" tagname="ControleImagem" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">  
    .divFormData { width: 620px; margin: auto; }
    legend { padding: 0 3px 0 3px; }
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
    fieldset { border-width: 0px; }
    fieldset legend { font-size: 12px; padding: 0 0; }

    /*--> CSS LIs */
    .liUnidade { border-right: 1px solid #F0F0F0; height: 85px; margin-top: 25px; padding-right: 6px; width: 230px;}
    .liUnidade2 { margin-top: 25px; }
    .liContato { margin-top: 10px; width: 270px;}
    .liUnidade li, .liUnidade2 li, .liContato li { margin-top: -7px; }
    .liClear { clear: both; }
    .liTelefone { margin-top:1px !important;}
     
    /*--> CSS DADOS */
    .fldsContato { padding: 5px 5px 0 0px; }
    .imgEnd { margin: 28px 0 0 5px; }    
    .txtNome { width: 220px; }
    .txtCNPJ { width: 100px; }
    .txtTelefone { width: 76px; }
    .txtLogradouro { width: 222px; }
    .txtComplemento { width: 95px; }
    .ddlBairro { width: 170px; }
    .ddlCidade { width: 195px; }
    .txtCEP { width: 54px; }
    .txtSigla { width: 45px; }
    .txtNumero { width: 40px; }
    
</style>
<script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulDados" class="ulDados">
    <li class="liUnidade">
        <ul>
            <li style="clear: both; margin-top: 10px !important;"></li>        
            <li class="liClear">
                <label for="txtNome" class="lblObrigatorio" title="Nome">Nome Fantasia</label>
                <asp:TextBox ID="txtNome" 
                    ToolTip="Informe o Nome da Instituição"
                    CssClass="txtNome" MaxLength="80" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNome" 
                    ErrorMessage="Nome deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>  
            <li class="liClear">
                <label for="txtSigla" class="lblObrigatorio" title="Nome">Sigla</label>
                <asp:TextBox ID="txtSigla" 
                    ToolTip="Informe a Sigla da Instituição"
                    CssClass="txtSigla" MaxLength="5" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla" 
                    ErrorMessage="Nome deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>         
            <li>
                <label for="txtCNPJ" class="lblObrigatorio" title="CNPJ">CNPJ</label>
                <asp:TextBox ID="txtCNPJ" 
                    ToolTip="Informe o CNPJ"
                    CssClass="txtCNPJ" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCNPJ" 
                    ErrorMessage="CNPJ deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>  
        </ul>
    </li>
    
    <li id="liTitEndereco" class="liTitEndereco">
        <img id="imgEnd" class="imgEnd" src="../../../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
    </li>
    
    <li class="liUnidade2">
        <ul>            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <li>
                <label for="txtCEP" title="CEP">CEP</label>
                <asp:TextBox ID="txtCEP" class="lblObrigatorio" 
                    ToolTip="Informe o CEP"
                    CssClass="txtCEP" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCEP" 
                    ErrorMessage="CEP deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtLogradouro" title="Endereço">Endere&ccedil;o</label>
                <asp:TextBox ID="txtLogradouro" 
                    ToolTip="Informe o Endereço"
                    CssClass="txtLogradouro" MaxLength="60" runat="server"></asp:TextBox>
            </li>
            <li>
                <label for="txtNumero" title="Número">Número</label>
                <asp:TextBox ID="txtNumero" 
                    ToolTip="Informe o Número"
                    CssClass="txtNumero" runat="server"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtComplemento" title="Complemento">Complemento</label>
                <asp:TextBox ID="txtComplemento" 
                    ToolTip="Informe o Complemento"
                    CssClass="txtComplemento" MaxLength="30" runat="server"></asp:TextBox>
            </li>
            <li>
                <label for="ddlBairro" class="lblObrigatorio" title="Bairro">Bairro</label>
                <asp:DropDownList ID="ddlBairro" 
                    ToolTip="Selecione o Bairro"
                    CssClass="ddlBairro" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBairro" 
                    ErrorMessage="Bairro deve ser informado" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="ddlCidade" class="lblObrigatorio" title="Cidade">Cidade</label>
                <asp:DropDownList ID="ddlCidade" 
                    ToolTip="Selecione a Cidade"
                    CssClass="ddlCidade" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" 
                    AutoPostBack="true" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCidade" 
                    ErrorMessage="Cidade deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="ddlUF" class="lblObrigatorio" title="UF">UF</label>
                <asp:DropDownList ID="ddlUF" 
                    ToolTip="Selecione a UF"
                    CssClass="ddlUF" OnSelectedIndexChanged="ddlUF_SelectedIndexChanged"
                    AutoPostBack="true" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUF" 
                    ErrorMessage="UF deve ser informada" Display="None">
                </asp:RequiredFieldValidator>
            </li>
            </ContentTemplate>
            </asp:UpdatePanel>
        </ul>
    </li>
        
    <li class="liContato liClear">
    <fieldset class="fldsContato">
    <legend>Informa&ccedil;&otilde;es de Contato</legend>
    <ul>
        <li class="liTelefone">
            <label for="txtTelefone" title="Telefone">Telefone</label>
            <asp:TextBox ID="txtTelefone" 
                ToolTip="Informe o Número do Telefone"
                CssClass="txtTelefone" runat="server"></asp:TextBox>
        </li>        
        <li class="liTelefone">
            <label for="txtTelefone2" title="Telefone 2">Telefone 2</label>
            <asp:TextBox ID="txtTelefone2" 
                ToolTip="Informe o Número do Telefone"
                CssClass="txtTelefone" runat="server"></asp:TextBox>
        </li>     
        <li class="liTelefone">
            <label for="txtFax" title="Fax">Fax</label>
            <asp:TextBox ID="txtFax" 
                ToolTip="Informe o Número do Fax"
                CssClass="txtTelefone" runat="server"></asp:TextBox>
        </li>       
    </ul>
    </fieldset>
    </li>
</ul>
<script type="text/javascript">
    $(document).ready(function() {
        $(".txtCNPJ").mask("99.999.999/9999-99");
        $(".txtTelefone").mask("(99) 9999-9999");
        $(".txtCEP").mask("99999-999");
        $(".txtNumero").mask("?99999");        
    });
</script>
</asp:Content>