<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" EnableEventValidation="false"
CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4500_CtrlInformUsuariosBiblioteca.F4501_ManutencaoUsuarioBiblioteca.Cadastro" %>
<%@ Register src="~/Library/Componentes/ControleImagem.ascx" tagname="ControleImagem" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 955px; }
    input[type='text'] { margin-bottom: 4px; }
    select { margin-bottom: 4px; }    
    label { margin-bottom: 1px; }
    .fieldset {width: 800px;margin: 5px 0 0 5px;padding-bottom: 5px;}
        
    /*--> CSS LIs */
    .liTipo { margin-right: 10px !important;}
    .liUnidade { margin-right: 53px !important;}        
    .liPhoto { float: left !important; margin-top: 0px; }  
    .liCTPS, .liIdentidade, .liTituloEleitor { margin-top:14px; margin-right: 35px !important; }
    .liCNH { margin-top:14px;  }
    .liInfoDiversas { margin-right: 20px !important; }    
    .liDados1 { border-right:1px solid #F0F0F0; padding:0 10px 0 2px; }
    .liClear { clear:both; }
    .liApelido, .liGrauInstrucao { margin-left: 7px; }
    .liSexo { margin-left: 9px; }    
    .liTitEndereco { padding: 9px 0 0 7px; }    
    .liDados2 { width: 386px; }
    .liNumero { margin-left:2px; }
    .liComplemento, .liBairro { float: right !important; }
    .liCep { margin-left: 5px; }
    .liTelCelular { float: right !important; margin-left: 5px; }
    .liTelResidencial, .liEmailPessoal { float: right !important; margin-left: 9px; }  
    .liValidadeCnh { margin-left: 6px; }    
    .liDtAdmissao { margin-left: 20px; }    
    .fldInfoDiversas { margin-top: 13px; padding: 0 4px 5px 8px; width: 185px; }
    .fldDadosAux { margin-top: 13px; margin-left: 114px !important; padding: 0 4px 5px 8px; }
    .fldDadosProfissionais { width: 442px; margin-top: 13px; padding: 0 4px 5px 8px; }    
    .fldPhoto {margin-left: -3px;width: 64px; height: 85px; border: 1px solid #DDDDDD !important;}
    .fldCtps { padding-left:11px; width: 120px;}
    .fldIdentidade { padding-left:11px; width: 220px;}
    .fldTituloEleitor { padding-left:9px; width: 157px;}
    .fldCNH { padding-left:10px; width: 236px;}    
    
    /*--> CSS DADOS */
    .ddlNome { width: 210px;}
    .txtNumeroControle{width:55px;}
    .txtApelido, .txtTelResidencial, .txtTelCelular, .txtTelefoneComercial {width:78px;}
    .ddlSexo{width:75px;}
    .ddlDeficiencia{width:70px;}
    .ddlGrauInstrucao, .ddlDepartamento {width:100px;}
    .ddlCursoFormacao, .ddlFuncao {width:185px;}
    .ddlEstadoCivil, .txtEmail {width:120px;}
    .txtCPF { width: 82px; }
    .txtIdentidade{width:104px;}
    .txtOrgEmissor{width:65px;}
    .txtDtEmissao, .txtValidadeCnh, .txtDtAdmissao, .txtDtSituacao, .txtDtNasc, .txtEmailPessoal {width:60px;}
    .txtNumeroTitulo, .txtComplemento {width:95px;}
    .txtZona, .txtSecao, .txtSerieCtps, .txtNumero {width:40px;}
    .txtNumeroCtps{width:58px;}
    .txtVia, .txtRamalComercial {width:30px;}
    .txtLogradouro{width:225px;}
    .ddlCidade{width:195px;}
    .ddlBairro{ width:170px; }
    .txtCep{width:56px;}
    .txtNomePaiMae{width:180px;}
    #ControleImagem .liControleImagemComp .fakefile { width: 65px !important; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulDados" class="ulDados">
    <li class="liPhoto">
        <fieldset class="fldPhoto">        
            <uc1:ControleImagem ID="upImagem" runat="server" />        
        </fieldset>
    </li>
    <li class="liDados1">
        <ul>
            <li class="liTipo">
                <label for="ddlTipo" title="Tipo de Usuário">Tipo de Usuário</label>
                <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Usuário" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="A">Aluno</asp:ListItem>
                    <asp:ListItem Value="P">Professor</asp:ListItem>
                    <asp:ListItem Value="F">Funcionário</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipo" ErrorMessage="Tipo deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li id="liUnidade" runat="server" class="liUnidade">
                <label for="ddlUnidade" title="Unidade/Escola">Unidade/Escola</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="campoUnidade" runat="server" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li>
                <label for="txtNumeroControle" title="Número de Controle da Biblioteca">N° Ctrl</label>
                <asp:TextBox ID="txtNumeroControle" ToolTip="Informe a Matrícula do Funcionário" CssClass="txtNumeroControle" runat="server"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtNome" class="lblObrigatorio" title="Nome">Nome</label>
                <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Usuário" CssClass="campoNomePessoa" runat="server" MaxLength="60"></asp:TextBox>
                <asp:DropDownList ID="ddlNome" ToolTip="Informe o Nome do Usuário" CssClass="ddlNome" runat="server"></asp:DropDownList>
            </li>
            <li class="liApelido">
                <label for="txtApelido" title="Apelido">Apelido</label>
                <asp:TextBox ID="txtApelido" 
                    ToolTip="Informe o Apelido do Funcionário"    
                    CssClass="txtApelido" runat="server" MaxLength="15">
                </asp:TextBox>
            </li>
            <li class="liSexo">
                <label for="ddlSexo" title="Sexo">Sexo</label>
                <asp:DropDownList ID="ddlSexo" CssClass="ddlSexo" runat="server" ToolTip="Selecione o Sexo do Usuário">
                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="txtDtNasc" class="lblObrigatorio" title="Data de Nascimento">Data Nascto</label>
                <asp:TextBox ID="txtDtNasc" 
                    ToolTip="Informe a Data de Nascimento do Funcionário"
                    CssClass="txtDtNasc campoData" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revDtNasc" ControlToValidate="txtDtNasc" runat="server" ErrorMessage="Data de Nascimento inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$" Display="None"></asp:RegularExpressionValidator>
            </li>
            <li>
                <label for="ddlEstadoCivil" title="Estado Civil">Estado Civil</label>
                <asp:DropDownList ID="ddlEstadoCivil" 
                    ToolTip="Selecione o Estado Civil do Funcionário"
                    CssClass="ddlEstadoCivil" runat="server">
                    <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                    <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                    <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                    <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                    <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                    <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                    <asp:ListItem Value="U">União Estável</asp:ListItem>
                    <asp:ListItem Value="O">Outro</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <label for="ddlDeficiencia" title="Deficiência">Deficiência</label>
                <asp:DropDownList ID="ddlDeficiencia" 
                    ToolTip="Informe se o Funcionário possui Deficiências"
                    CssClass="ddlDeficiencia" runat="server">
                    <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                    <asp:ListItem Value="A">Auditivo</asp:ListItem>
                    <asp:ListItem Value="V">Visual</asp:ListItem>
                    <asp:ListItem Value="F">Físico</asp:ListItem>
                    <asp:ListItem Value="M">Mental</asp:ListItem>
                    <asp:ListItem Value="I">Múltiplas</asp:ListItem>
                    <asp:ListItem Value="O">Outros</asp:ListItem>
                </asp:DropDownList>
            </li> 
            <li class="liGrauInstrucao">
                <label for="ddlGrauInstrucao" title="Grau de Instrução">Grau de Instrução</label>
                <asp:DropDownList ID="ddlGrauInstrucao" 
                    ToolTip="Selecione o Grau de Instrução do Funcionário"
                    CssClass="ddlGrauInstrucao" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlGrauInstrucao_SelectedIndexChanged"></asp:DropDownList>
            </li>
        </ul>
    </li>
    <li class="liTitEndereco">
        <img src="../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
    </li>
    <li class="liDados2">
        <ul>
            <li>
                <label for="txtLogradouro" class="lblObrigatorio" title="Logradouro">Logradouro</label>
                <asp:TextBox ID="txtLogradouro" ToolTip="Informe o Logradouro" CssClass="txtLogradouro" runat="server" MaxLength="40"></asp:TextBox>
            </li>
            <li class="liNumero">
                <label for="txtNumero" title="Número">Número</label>
                <asp:TextBox ID="txtNumero" ToolTip="Informe o Número da residência" CssClass="txtNumero" runat="server" MaxLength="5"></asp:TextBox>
            </li>
            <li class="liComplemento">
                <label for="txtComplemento" title="Complemento">Complemento</label>
                <asp:TextBox ID="txtComplemento" ToolTip="Informe o Complemento" CssClass="txtComplemento" runat="server" MaxLength="40"></asp:TextBox>
            </li>            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <li>
                <label for="ddlCidade" class="lblObrigatorio" title="Cidade">Cidade</label>
                <asp:DropDownList ID="ddlCidade" ToolTip="Selecione a Cidade" CssClass="ddlCidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged"></asp:DropDownList>
            </li>
            <li class="liBairro">
                <label for="ddlBairro" class="lblObrigatorio" title="Bairro">Bairro</label>
                <asp:DropDownList ID="ddlBairro" ToolTip="Selecione o Bairro" CssClass="ddlBairro" runat="server"></asp:DropDownList>
            </li>
            <li>
                <label for="ddlUf" class="lblObrigatorio" title="UF">UF</label>
                <asp:DropDownList ID="ddlUf" ToolTip="Selecione o Estado" CssClass="ddlUF" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged"></asp:DropDownList>
            </li>
            </ContentTemplate>
            </asp:UpdatePanel>
            <li class="liCep">
                <label for="txtCep" class="lblObrigatorio" title="CEP">CEP</label>
                <asp:TextBox ID="txtCep" ToolTip="Informe o CEP" CssClass="txtCep" runat="server"></asp:TextBox>
            </li>
            <li class="liTelResidencial">
                <label for="txtTelResidencial" title="Telefone Residencial">Tel. Residencial</label>
                <asp:TextBox ID="txtTelResidencial" ToolTip="Informe o Telefone Residencial do Funcionário" CssClass="txtTelResidencial" runat="server"></asp:TextBox>
            </li>
            <li class="liTelCelular">
                <label for="txtTelCelular" title="Telefone Celular">Tel. Celular</label>
                <asp:TextBox ID="txtTelCelular" ToolTip="Informe o Telefone Celular do Funcionário" CssClass="txtTelCelular" runat="server"></asp:TextBox>
            </li>
            <li class="liEmailPessoal">
                <label for="txtEmailPessoal" title="E-mail">E-mail</label>
                <asp:TextBox ID="txtEmailPessoal" ToolTip="Informe o E-mail do Usuário" CssClass="txtEmailPessoal" runat="server" MaxLength="80"></asp:TextBox>
            </li>
        </ul>
    </li>
    <li class="liClear liCTPS">
        <fieldset class="fldCtps">
            <legend>CTPS</legend>
            <ul>
                <li>
                    <label for="txtNumeroCtps" title="Número da Carteira de Trabalho">Número</label>
                    <asp:TextBox ID="txtNumeroCtps" ToolTip="Informe o Número da Carteira de Trabalho" CssClass="txtNumeroCtps" runat="server" MaxLength="9"></asp:TextBox>
                </li>
                <li>
                    <label for="txtSerieCtps" title="Número de Série da Carteira de Trabalho">Série</label>
                    <asp:TextBox ID="txtSerieCtps" ToolTip="Informe o Número de Série da Carteira de Trabalho" CssClass="txtSerieCtps" runat="server" MaxLength="6"></asp:TextBox>
                </li>
                <li>
                    <label for="txtVia" title="Via">Via</label>
                    <asp:TextBox ID="txtVia" ToolTip="Informe a Via da Carteira de Trabalho" CssClass="txtVia" runat="server" MaxLength="2"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlCtpsUF" title="UF">UF</label>
                    <asp:DropDownList ID="ddlCtpsUF" ToolTip="Informe a UF de origem da Carteira de Trabalho" CssClass="ddlUF" runat="server"></asp:DropDownList>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liIdentidade">
        <fieldset class="fldIdentidade">
            <legend>Identidade - CPF</legend>
            <ul>
                <li>
                    <label for="txtIdentidade" class="lblObrigatorio" title="Número de Identidade">Número</label>
                    <asp:TextBox ID="txtIdentidade" ToolTip="Informe o Número da Carteira de Identidade" CssClass="txtIdentidade" runat="server" MaxLength="20"></asp:TextBox>
                </li>
                <li>
                    <label for="txtDtEmissao" class="lblObrigatorio" title="Data de Emissão da Carteira de Identidade">Data Emissão</label>
                    <asp:TextBox ID="txtDtEmissao" ToolTip="Informe a Data de Emissão da Carteira de Identidade" CssClass="txtDtEmissao campoData" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDtEmissao" runat="server" ErrorMessage="Data de Emissão inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$" Display="None"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <label for="txtOrgEmissor" class="lblObrigatorio" title="Órgão Emissor da Carteira de Identidade">Orgão Emissor</label>
                    <asp:TextBox ID="txtOrgEmissor" ToolTip="Informe o Órgão Emissor da Carteira de Identidade" CssClass="txtOrgEmissor" runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlIdentidadeUF" class="lblObrigatorio" title="UF do Órgão Emissor">UF</label>
                    <asp:DropDownList ID="ddlIdentidadeUF" ToolTip="Informe a UF do Órgão Emissor" CssClass="ddlUF" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <label for="txtCPF" class="lblObrigatorio" title="CPF">CPF</label>
                    <asp:TextBox ID="txtCPF" ToolTip="Informe o CPF do Funcionário" CssClass="txtCPF" runat="server"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liTituloEleitor">
        <fieldset class="fldTituloEleitor">
            <legend>Título de Eleitor</legend>
            <ul>
                <li>
                    <label for="txtNumeroTitulo" title="Número do Título de Eleitor">Número</label>
                    <asp:TextBox ID="txtNumeroTitulo" ToolTip="Informe o Número do Título de Eleitor" CssClass="txtNumeroTitulo" runat="server" MaxLength="15"></asp:TextBox>
                </li>
                <li>
                    <label for="txtZona" title="Zona Eleitoral">Zona</label>
                    <asp:TextBox ID="txtZona" ToolTip="Informe a Zona Eleitoral" CssClass="txtZona" runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li>
                    <label for="txtSecao" title="Seção Eleitoral">Seção</label>
                    <asp:TextBox ID="txtSecao" ToolTip="Informe a Seção Eleitoral" CssClass="txtSecao" runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li>
                    <label for="ddlUfTitulo" title="UF">UF</label>
                    <asp:DropDownList ID="ddlUfTitulo" ToolTip="Informe a UF do Título de Eleitor" CssClass="ddlUF" runat="server"></asp:DropDownList>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liCNH">
        <fieldset class="fldCNH">
            <legend>CNH</legend>
            <ul>
                <li>
                    <label for="txtRegCnh" title="N° de Regitro da CNH">N° Registro</label>
                    <asp:TextBox ID="txtRegCnh" ToolTip="Informe o N° de Regitro da CNH" CssClass="txtRegCnh" runat="server" MaxLength="12"></asp:TextBox>
                </li>
                <li class="liDocCnh">
                    <label for="txtDocCnh" title="N° do Documento da CNH">N° Documento</label>
                    <asp:TextBox ID="txtDocCnh" style="margin-right: 0px;" ToolTip="Informe o N° do Documento da CNH" CssClass="txtDocCnh" runat="server" MaxLength="10"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtCatCnh" title="Categoria da CNH">Categoria</label>
                    <asp:TextBox ID="txtCatCnh" ToolTip="Informe a Categoria da CNH" CssClass="txtCatCnh" runat="server" MaxLength="2"></asp:TextBox>
                </li>
                <li class="liValidadeCnh">
                    <label for="txtValidadeCnh" title="Data de Validade da CNH">Validade</label>
                    <asp:TextBox ID="txtValidadeCnh" ToolTip="Informe a Data de Validade da CNH" CssClass="txtValidadeCnh campoData" runat="server" MaxLength="30"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
    </li>
    <li class="liClear liInfoDiversas">
        <fieldset class="fldInfoDiversas">
            <legend>Info. Diversas</legend>
            <ul>
                <li>
                    <label for="txtNomeMae" class="lblObrigatorio" title="Nome da Mãe">Nome da Mãe</label>
                    <asp:TextBox ID="txtNomeMae" ToolTip="Informe o Nome da Mãe" CssClass="txtNomePaiMae" runat="server" MaxLength="60"></asp:TextBox>
                </li>
                <li class="liClear">
                    <label for="txtNomePai" title="Nome do Pai">Nome do Pai</label>
                    <asp:TextBox ID="txtNomePai" ToolTip="Informe o Nome do Pai" CssClass="txtNomePaiMae" runat="server" MaxLength="60"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
    </li>
    <li id="liDadosProfissionais" class="liDadosProfissionais">
        <fieldset class="fldDadosProfissionais">
            <legend>Dados Profissionais</legend>
            <ul>
                <li>
                    <label for="txtDepartamento" title="Departamento">Departamento</label>
                    <asp:TextBox ID="txtDepartamento" ToolTip="Informe o Departamento do Funcionário" CssClass="ddlDepartamento" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="txtFuncao" title="Função">Função</label>
                    <asp:TextBox ID="txtFuncao" ToolTip="Informe a Função do Funcionário" CssClass="ddlFuncao" runat="server"></asp:TextBox>
                </li>
                <li class="liDtAdmissao">
                    <label for="txtDtAdmissao" title="Data de Admissão">Data Admissão</label>
                    <asp:TextBox ID="txtDtAdmissao" ToolTip="Informe a Data de Admissão" CssClass="txtDtAdmissao campoData" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDtAdmissao" runat="server" ErrorMessage="Data de Admissão inválida" ValidationExpression="^((0[1-9]|[1-9]|[12]\d)\/(0[1-9]|1[0-2]|[1-9])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$" Display="None"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <label for="ddlCursoFormacao" title="Curso de Formação">Curso de Formação</label>
                    <asp:DropDownList ID="ddlCursoFormacao" 
                        ToolTip="Selecione o Curso de Formação do Funcionário"
                        CssClass="ddlCursoFormacao" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <label for="txtEmail" title="E-mail">E-mail</label>
                    <asp:TextBox ID="txtEmail" ToolTip="Informe o E-mail do Funcionário" CssClass="txtEmail" runat="server" MaxLength="255"></asp:TextBox>
                </li>
                <li>
                    <label for="txtTelefoneComercial" title="Telefone Comercial">Telefone</label>
                    <asp:TextBox ID="txtTelefoneComercial" ToolTip="Informe o Telefone Comercial do Funcionário" CssClass="txtTelefoneComercial" runat="server"></asp:TextBox>
                </li>
                <li>
                    <label for="txtRamalComercial" title="Ramal do Telefone Comercial">Ramal</label>
                    <asp:TextBox ID="txtRamalComercial" MaxLength="5" ToolTip="Informe o Ramal do Telefone Comercial do Funcionário" CssClass="txtRamalComercial" runat="server"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
    </li>
    <li id="liDadosAux" class="liDadosAux">
        <fieldset class="fldDadosAux">
            <legend>Situação</legend>
            <ul>
                <li>
                    <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação</label>
                    <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação Atual do Usuário" CssClass="ddlSituacao" runat="server">
                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liClear">
                    <label for="txtDtSituacao" class="lblObrigatorio" title="Data da Situação">Data Situação</label>
                    <asp:TextBox ID="txtDtSituacao" Enabled="False" CssClass="txtDtSituacao campoData" runat="server"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
    </li>
</ul>

<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".txtCep").mask("99999-999");
    }); 

    $(document).ready(function() {
        var datePickerOptions = { showOn: 'button',
            buttonImage: '../../../../Library/IMG/Gestor_IcoCalendario.gif',
            buttonImageOnly: true,
            inline: true,
            duration: 'fast',
            yearRange: '-110:+110'
        };

        $(".txtCep").mask("99999-999");
        $(".txtNumero").mask("?999999");
        $(".txtTelResidencial").mask("(99) 9999-9999");
        $(".txtTelCelular").mask("(99) 9999-9999");
        $(".txtTelefoneComercial").mask("(99) 9999-9999");        
        $(".txtCPF").mask("999.999.999-99");
        $(".txtNumeroCtps").mask("?999999999");
        $(".txtSerieCtps").mask("?999999");
        $(".txtVia").mask("?99");
        $(".txtIdentidade").mask("?99999999999999999999");
        $(".txtRegCnh").mask("?999999999999");
        $(".txtDocCnh").mask("?9999999999");
    });
</script>
</asp:Content>