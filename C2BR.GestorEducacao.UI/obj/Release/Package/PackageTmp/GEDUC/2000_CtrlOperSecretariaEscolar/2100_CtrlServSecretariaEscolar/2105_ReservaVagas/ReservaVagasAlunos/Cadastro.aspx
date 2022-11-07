<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2105_ReservaVagas.ReservaVagasAlunos.Cadastro" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 820px; margin-left: 68px; margin:-7px auto auto !important}        
        .ulDados li input{ margin-bottom: 0;}
        .ulDados li fieldset ul li{ margin-top: 0;}     
        fieldset
        {
            padding: 0px  0px 5px 5px;
            margin: 0;
        }          

        /*--> CSS LIs */
        .ulDados li{ margin-top: 0px;}
        .liClear { clear: both; }
        .liCidade { clear: none;margin-left: 6px;}
        .liBairro { clear: none;margin-left: 7px;}
        .liClearNone { clear: none; }  
        .liBarraTitulo
        {
        	padding: 2px; 
        	text-align: left;        	        	
        	width: 400px;
        }
        .liBarraReserva
        {
        	background-color: #E5E5E5;
        	margin-bottom: 5px;
        	padding: 2px; 
        	text-align: center;
        	font-size: 11px;
        	font-weight:bold;
        	width: 100%;
        }        
        .liDadosResponsavel { display: inline; border: none; margin: 0; width:400px; }
        .liDadosCandidato { display: inline; border: none; margin: 0; width:400px; float:right !important; }        
        .liDadosReserva { width: 100%; }  
        .liSemestre { margin-left: 38px !important; }        
        .liTitEndereco {width:60px; } 
        .liDataNascimentoResponsavel{margin-top:2px !important;}
        .liEstadoCivil, .liIdentidade {display:inline;margin-top:2px !important;}
        .liPesquisarCep{ margin-top: 5px !important; margin-left: -3px;}
        .liCorreios{ margin-top: 11px !important; margin-left: -3px;}
        .liEmailResponsavel {clear:none;margin-left:7px;}
        .liSeparador {border-bottom: solid 3px #CCCCCC; width:100%;padding-bottom:5px;margin-bottom:5px;}
        .liSeparadorTit {border-bottom: solid 2px #000; width:100%;padding-bottom:0px;margin-bottom:5px;}
        .liSeparadorReserva {border-bottom: solid 2px #000; width:100%;padding-bottom:0px;margin-bottom:5px;}
        .liEmpresaResp {clear:none;margin-left:20px;}
        .liNumEmpresaResp {clear:none;margin-left:9px;}
        .liFuncaoResp {clear:both;margin-left:85px;}
        .liEmailEmpResp {clear:none;margin-left:5px;}
        .liDataNascimentoCandidato {clear:none;margin-left:10px;}
        .liCpfCandidato {clear:none;margin-left:20px;}
        .liNomeMaeAluno, .liNomePaiAluno {clear:none; margin-left: 103px;}
        
        /*--> CSS DADOS */
        .txtEmailRVL {width:180px !important;}
        .txtLogradouroRVL { width: 200px;}
        .ddlCidadeRVL{ width: 177px;}
        .ddlBairroRVL { width: 150px;}
        .txtComplementoRVL { width: 134px;}
        .txtNireRVL { width: 46px;}
        .ddlSexoRVL { width: 50px;}        
        .ddlNacionalidadeRVL, .ddlDeficienciaRVL { width: 70px;}
        .txtObservacoesRVL { width: 388px; height: 44px; margin-top: 0px;}   
        .txtObsReservaRVL { width: 200px; height: 42px; margin-top: 0px;}
        .txtNumeroRVL {width:40px;}       
        .txtFuncaoRespRVL {width:110px;} 
        .txtEmpresaRespRVL, .ddlCandidatoRVL {width:210px;}
        .ddlSemestreRVL {width:30px;}
        .ddlAnoRVL {width:45px;}
        .campoTelefone { width: 76px !important; }        
        .lblTitulo 
        {
        	font-weight:bold;
        	font-size: 11px;
        }           
        .lblTitEndereco {text-align:left;font-size:12px; font-family:Arial;}        
        .btnValidarCpf { background-color: #F1FFEF; border: 1px solid #D2DFD1; padding: 0 4px 1px 5px; }
        .ddlEstadoCivilRVL{ width: 105px;}
        .txtNumReservaRVL { width: 75px; }
        .ddlRendaFamiliarRVL{ width: 80px;}        
        .btnCorreiosRVL{ width: 15px; height: 15px;}        
        .txtNumRGRespRVL {width:65px;}
        .txtOrgaoRGRespRVL {width:50px;}       
        .txtQtdDepenRVL {width:30px;} 
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">                
        <li class="liDadosResponsavel">            
            <ul>
                <li class="liBarraTitulo"><label class="lblTitulo">Informações do Responsável</label></li>
                <li class="liSeparadorTit"></li>
                <li>
                    <label for="txtCpfResponsavelRVL" class="lblObrigatorio" title="CPF do Responsável">CPF</label>
                    <asp:TextBox ID="txtCpfResponsavelRVL" ToolTip="Informe o CPF do Responsável" runat="server" CssClass="campoCpf"></asp:TextBox>                     
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCpfResponsavelRVL"
                        ErrorMessage="CPF do Responsável deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                    
                    <asp:LinkButton ID="btnValidarCpfResponsavel" CssClass="btnValidarCpf" runat="server" ValidationGroup="buscaCpfRespo"
                        onclick="btnValidarCpfResponsavel_Click" CausesValidation="false">
                        Validar CPF
                    </asp:LinkButton>
                </li>
                <li style="clear: none; margin-left: 25px;">
                    <label for="txtNomeResponsavelRVL" class="lblObrigatorio" title="Nome do Responsável">Nome</label>
                    <asp:TextBox ID="txtNomeResponsavelRVL" runat="server"
                        CssClass="campoNomePessoa" ToolTip="Informe o Nome do Responsável" MaxLength="60"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNomeResponsavelRVL" 
                        ErrorMessage="Nome do Responsável deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liDataNascimentoResponsavel">
                    <label for="txtDataNascimentoResponsavelRVL" title="Data de Nascimento do Responsável">Data Nascimento</label>
                    <asp:TextBox ID="txtDataNascimentoResponsavelRVL" CssClass="campoData" runat="server" 
                        ToolTip="Informe a Data de Nascimento do Responsável" Enabled="false">
                    </asp:TextBox>
                </li>
                <li class="liEstadoCivil">
                    <label for="ddlEstadoCivilRVL" title="Estado Civil do Responsável">Estado Civil</label>
                    <asp:DropDownList ID="ddlEstadoCivilRVL" CssClass="ddlEstadoCivilRVL" runat="server" 
                        ToolTip="Informe o Estado Civil do Responsável">
                        <asp:ListItem Value="O">Solteiro</asp:ListItem>
                        <asp:ListItem Value="C">Casado</asp:ListItem>
                        <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                        <asp:ListItem Value="D">Divorciado</asp:ListItem>
                        <asp:ListItem Value="V">Viúvo</asp:ListItem>
                        <asp:ListItem Value="P">Companheiro</asp:ListItem>
                        <asp:ListItem Value="U">União Estável</asp:ListItem>
                    </asp:DropDownList>   
                </li> 
                <li class="liIdentidade">
                    <ul>
                        <li><label>Identidade (Nº / Órgão / Emissão)</label></li>
                        <li style="clear: both;">
                        <asp:TextBox ID="txtNumRGRespRVL" CssClass="txtNumRGRespRVL" runat="server" 
                            ToolTip="Informe o Nº do RG do Responsável" Enabled="false">
                        </asp:TextBox>
                        <asp:TextBox ID="txtOrgaoRGRespRVL" CssClass="txtOrgaoRGRespRVL" runat="server" 
                            ToolTip="Informe o Orgão do RG do Responsável" Enabled="false">
                        </asp:TextBox>
                        <asp:TextBox ID="txtDtEmissaoRGRespRVL" CssClass="campoData" runat="server" 
                            ToolTip="Informe a Data de Emissão do RG do Responsável" Enabled="false">
                        </asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="liClear">
                    <label title="Grau de Parentesco do Responsável">Parentesco</label>
                    <asp:DropDownList ID="ddlGrauParentescoRVL" runat="server"
                        ToolTip="Informe o Grau de Parentesco do Responsável">
                        <asp:ListItem Value="PM">Pai/Mãe</asp:ListItem>
                        <asp:ListItem Value="TI">Tio(a)</asp:ListItem>
                        <asp:ListItem Value="AV">Avô/Avó</asp:ListItem>
                        <asp:ListItem Value="PR">Primo(a)</asp:ListItem>
                        <asp:ListItem Value="CN">Cunhado(a)</asp:ListItem>
                        <asp:ListItem Value="TU">Tutor(a)</asp:ListItem>
                        <asp:ListItem Value="IR">Irmão(ã)</asp:ListItem>
                        <asp:ListItem Value="OU">Outros</asp:ListItem>
                    </asp:DropDownList>
                </li>               
                <li style="display:inline;margin-left:5px;">
                    <label for="ddlRendaFamiliarRVL" class="lblObrigatorio" title="Renda Familiar do Responsável">Renda Familiar</label>
                    <asp:DropDownList ID="ddlRendaFamiliarRVL" CssClass="ddlRendaFamiliarRVL" runat="server" 
                        ToolTip="Informe a Renda Familiar do Responsável">
                        <asp:ListItem Value="">Selecione</asp:ListItem>
                        <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                        <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                        <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                        <asp:ListItem Value="4">+10 SM</asp:ListItem>
                        <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                        <asp:ListItem Value="6">Não informada</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlRendaFamiliarRVL" ErrorMessage="Renda familiar deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
                <li style="display:inline; margin-left:5px;">
                    <label for="txtQtdDepenRVL" title="Quantidade de Dependentes do Responsável" class="lblObrigatorio">Qtd Dep</label>
                    <asp:TextBox ID="txtQtdDepenRVL" CssClass="txtQtdDepenRVL" runat="server" 
                        ToolTip="Informe a Quantidade de Dependentes do Responsável" Enabled="false">
                    </asp:TextBox>      
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtQtdDepenRVL" 
                        ErrorMessage="Quantidade de Dependentes deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>     
                </li>        
                <li class="liClear">
                    <label for="txtTelResidencialResponsavelRVL" title="Telefone Residencial do Responsavel">Tel. Residencial</label>
                    <asp:TextBox ID="txtTelResidencialResponsavelRVL" CssClass="campoTelefone" 
                        ToolTip="Informe o Telefone Residencial do Responsavel" runat="server">
                    </asp:TextBox>
                </li>
                <li style="margin-left:7px;">
                    <label for="txtTelCelularResponsavelRVL" title="Telefone Celular do Responsavel">Tel. Celular</label>
                    <asp:TextBox ID="txtTelCelularResponsavelRVL" CssClass="campoTelefone" 
                        ToolTip="Informe o Telefone Celular do Responsavel" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liEmailResponsavel">
                    <label for="txtEmailResponsavelRVL" title="E-mail do Responsavel">E-mail</label>
                    <asp:TextBox ID="txtEmailResponsavelRVL" style="width:205px;" 
                        ToolTip="Informe o E-mail do Responsavel" runat="server" MaxLength="60">
                    </asp:TextBox>
                </li>        
                <li class="liSeparador"></li>
                <li class="liTitEndereco">
                    <label class="lblTitEndereco">Endereço Residencial</label>
                </li>                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <li style="display:inline;margin-left:5px;">
                    <label for="txtCepResponsavelRVL" class="lblObrigatorio" title="CEP">CEP</label>
                    <asp:TextBox ID="txtCepResponsavelRVL" CssClass="campoCep" ToolTip="Informe o CEP do Responsável" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCepResponsavelRVL" 
                        ErrorMessage="CEP deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li id="liPesquisarCep" class="liPesquisarCep" runat="server">
                    <asp:ImageButton ID="btnPesquisarCep" CssClass="liPesquisarCep" runat="server" 
                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" onclick="btnPesquisarCep_Click"
                        CausesValidation="false"/>
                </li>
                <li id="liCorreios" class="liCorreios" runat="server">
                    <asp:ImageButton ID="btnCorreiosRVL" runat="server" ImageUrl="/Library/IMG/Gestor_Correios.gif" CssClass="btnCorreiosRVL" OnClientClick="javascript:window.open('http://www.buscacep.correios.com.br/servicos/dnec/index.do');" CausesValidation="false"/>
                </li>
                <li class="liClear">
                    <label for="ddlUfResponsavelRVL" class="lblObrigatorio" title="UF">UF</label>
                    <asp:DropDownList ID="ddlUfResponsavelRVL" runat="server" CssClass="campoUf" 
                        ToolTip="Informe a UF do Responsável" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlUfResponsavelRVL_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlUfResponsavelRVL" 
                        ErrorMessage="UF do Responsavel deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li> 
                <li class="liCidade">
                    <label for="ddlCidadeResponsavelRVL" class="lblObrigatorio" title="Cidade">Cidade</label>
                    <asp:DropDownList ID="ddlCidadeResponsavelRVL" runat="server"
                        ToolTip="Informe a Cidade do Responsável"
                        CssClass="ddlCidadeRVL" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlCidadeResponsavelRVL_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCidadeResponsavelRVL" 
                        ErrorMessage="Cidade do Responsavel deve ser informada" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li> 
                <li class="liBairro">
                    <label for="ddlBairroResponsavelRVL" class="lblObrigatorio" title="Bairro">Bairro</label>
                    <asp:DropDownList ID="ddlBairroResponsavelRVL" CssClass="ddlBairroRVL" 
                        ToolTip="Informe o Bairro do Responsável" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBairroResponsavelRVL" 
                        ErrorMessage="Bairro do Responsavel deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtLogradouroResponsavelRVL" class="lblObrigatorio" title="Logradouro da Residência do Responsável">Logradouro</label>
                    <asp:TextBox ID="txtLogradouroResponsavelRVL" CssClass="txtLogradouroRVL" ToolTip="Informe o Logradouro da Residência do Responsável" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogradouroResponsavelRVL" 
                        ErrorMessage="Logradouro deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>                
                <li class="liClearNone">
                    <label for="txtNumeroResponsavelRVL" title="Número da Residência do Responsável">Número</label>
                    <asp:TextBox ID="txtNumeroResponsavelRVL" CssClass="txtNumeroRVL" ToolTip="Informe o Número da Residência do Responsável" runat="server" MaxLength="5"></asp:TextBox>
                </li>
                <li class="liClearNone">
                    <label for="txtComplementoResponsavelRVL" title="Complemento">Complemento</label>
                    <asp:TextBox ID="txtComplementoResponsavelRVL" CssClass="txtComplementoRVL" ToolTip="Informe o Complemento da Residência do Responsável" runat="server" MaxLength="30"></asp:TextBox>
                </li>     
                </ContentTemplate>
                </asp:UpdatePanel>
                <li class="liSeparador"></li> 
                <li class="liTitEndereco">
                    <label class="lblTitEndereco" style="width:70px;">Informações do Trabalho</label>
                </li>   
                <li class="liEmpresaResp">
                    <label for="txtEmpresaRespRVL" title="Nome da Empresa de Trabalho do Responsável">Empresa</label>
                    <asp:TextBox ID="txtEmpresaRespRVL" CssClass="txtEmpresaRespRVL" Enabled="false" 
                        ToolTip="Informe o Nome da Empresa de Trabalho do Responsável" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liNumEmpresaResp">
                    <label for="txtNumEmpresaRespRVL" title="Telefone do Trabalho do Responsável">Tel. Empresa</label>
                    <asp:TextBox ID="txtNumEmpresaRespRVL" CssClass="campoTelefone" Enabled="false"
                        ToolTip="Informe o Telefone do Trabalho do Responsável" runat="server">
                    </asp:TextBox>
                </li>      
                <li class="liFuncaoResp">
                    <label for="txtFuncaoRespRVL" title="Nome da Função do Responsável">Função</label>
                    <asp:TextBox ID="txtFuncaoRespRVL" CssClass="txtFuncaoRespRVL" Enabled="false"
                        ToolTip="Informe a Função do Responsável" runat="server">
                    </asp:TextBox>
                </li>       
                <li class="liEmailEmpResp">
                    <label for="txtEmailEmpRespRVL" title="Email de Trabalho do Responsável">Email</label>
                    <asp:TextBox ID="txtEmailEmpRespRVL" CssClass="txtEmailRVL" Enabled="false"
                        ToolTip="Informe o Email de Trabalho do Responsável" runat="server">
                    </asp:TextBox>
                </li>                                                            
            </ul>
        </li>       
                
        <li id="liDadosCandidato" runat="server" class="liDadosCandidato">            
            <ul>
                <li class="liBarraTitulo"><label class="lblTitulo">Informações do Aluno Candidato</label></li>
                <li class="liSeparadorTit"></li>
                <li id="liNomeCandidato" runat="server">
                    <label for="txtNomeCandidatoRVL" class="lblObrigatorio" title="Nome do Candidato a Vaga">Nome</label>
                    <asp:TextBox ID="txtNomeCandidatoRVL" runat="server" CssClass="campoNomePessoa" 
                        ToolTip="Informe o Nome do Candidato a Vaga" MaxLength="60" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNomeCandidatoRVL" 
                        ErrorMessage="Nome do Candidato deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li id="liCandidato" runat="server" Visible="false">
                    <label for="ddlCandidatoRVL" title="Candidato">Nome</label>
                    <asp:DropDownList ID="ddlCandidatoRVL" CssClass="ddlCandidatoRVL" runat="server" AutoPostBack="true"
                        ToolTip="Selecione o Candidato"
                        onselectedindexchanged="ddlCandidatoRVL_SelectedIndexChanged"></asp:DropDownList>
                </li> 
                <li style="display:inline;">
                    <label for="chkNovoAlunoRVL" title="Novo Aluno?">Novo</label>
                    <asp:CheckBox ID="chkNovoAlunoRVL" runat="server" style="margin-left:-5px;" 
                        ToolTip="Novo Aluno" Enabled="false" AutoPostBack="True" 
                        oncheckedchanged="chkNovoAlunoRVL_CheckedChanged"></asp:CheckBox>
                </li>
                <li class="liDataNascimentoCandidato">
                    <label for="txtDataNascimentoCandidatoRVL" class="lblObrigatorio" title="Data de Nascimento do Cadidato">Data Nascimento</label>
                    <asp:TextBox ID="txtDataNascimentoCandidatoRVL" CssClass="campoData" runat="server"
                        ToolTip="Informe a Data de Nascimento do Candidato" Enabled="false">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDataNascimentoCandidatoRVL" 
                        ErrorMessage="Data de Nascimento deve ser informada" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClearNone">
                    <label for="ddlSexoCandidatoRVL" class="lblObrigatorio" title="Sexo do Candidato">Sexo</label>
                    <asp:DropDownList ID="ddlSexoCandidatoRVL" CssClass="ddlSexoRVL" runat="server" 
                        ToolTip="Informe o Sexo do Candidato" Enabled="false">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="M">Masc</asp:ListItem>
                        <asp:ListItem Value="F">Femin</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlSexoCandidatoRVL" 
                        ErrorMessage="Sexo deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtNireRVL" class="lblObrigatorio" title="Número do NIRE">N° NIRE</label>
                    <asp:TextBox ID="txtNireRVL" CssClass="txtNireRVL" runat="server" 
                        ToolTip="Informe o Número do NIRE" Enabled="false"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNireRVL" CssClass="validatorField"
                        ErrorMessage="NIRE deve estar entre 0 e 1000000" Type="Integer"
                        MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNireRVL" 
                        ErrorMessage="NIRE deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liCpfCandidato">  
                    <label for="txtCpfCandidatoRVL" title="CPF do Candidato">CPF</label>
                    <asp:TextBox ID="txtCpfCandidatoRVL" runat="server" 
                        ToolTip="Informe o CPF do Candidato" CssClass="campoCpf" Enabled="false"></asp:TextBox> 
                    <asp:CustomValidator ControlToValidate="txtCpfCandidatoRVL" runat="server" 
                        ErrorMessage="CPF inválido" Display="None" CssClass="validatorField" 
                        EnableClientScript="false" OnServerValidate="cvCPF_ServerValidate">
                    </asp:CustomValidator>
                </li>            
                <li style="display:inline; margin-left: 33px; margin-right: 0px;">
                    <ul>
                        <li><label>Identidade (Nº / Órgão / Emissão)</label></li>
                        <li style="clear: both;">
                        <asp:TextBox ID="txtNumRGAluRVL" CssClass="txtNumRGRespRVL" runat="server" 
                            ToolTip="Informe o Nº do RG do Aluno" Enabled="false">
                        </asp:TextBox>
                        <asp:TextBox ID="txtOrgaoRGAluRVL" CssClass="txtOrgaoRGRespRVL" runat="server" 
                            ToolTip="Informe o Orgão do RG do Aluno" Enabled="false">
                        </asp:TextBox>
                        <asp:TextBox ID="txtDtEmissRGAluRVL" CssClass="campoData" runat="server" 
                            ToolTip="Informe a Data de Emissão do RG do Aluno" Enabled="false">
                        </asp:TextBox>
                        </li>
                    </ul>
                </li>                                   
                <li class="liClear">  
                    <label for="ddlNacionalidadeCandidatoRVL" class="lblObrigatorio" title="Nacionalidade do Candidato">Nacionalidade</label>
                    <asp:DropDownList ID="ddlNacionalidadeCandidatoRVL" runat="server" 
                        CssClass="ddlNacionalidadeRVL" Enabled="false"
                        ToolTip="Informe a Nacionalidade do Aluno">
                        <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                        <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNacionalidadeCandidatoRVL" 
                        ErrorMessage="Nacionalidade deve ser informada" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>                     
                </li>        
                <li class="liNomeMaeAluno">
                    <label for="txtNomeMaeAlunoRVL" title="Nome da Mãe do Aluno Candidato">Nome Mãe</label>
                    <asp:TextBox ID="txtNomeMaeAlunoRVL" CssClass="campoNomePessoa" Enabled="false" ToolTip="Informe o Nome da Mãe do Aluno Candidato" runat="server" MaxLength="100"></asp:TextBox>
                </li>        
                <li class="liClear">
                    <label for="ddlDeficienciaCandidatoRVL" title="Deficiência">Deficiência</label>
                    <asp:DropDownList ID="ddlDeficienciaCandidatoRVL" runat="server"
                        CssClass="ddlDeficienciaRVL" Enabled="false"
                        ToolTip="Informe se o Candidato possui Deficiências">
                        <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                        <asp:ListItem Value="A">Auditiva</asp:ListItem>   
                        <asp:ListItem Value="V">Visual</asp:ListItem>
                        <asp:ListItem Value="F">Física</asp:ListItem>
                        <asp:ListItem Value="M">Mental</asp:ListItem>
                        <asp:ListItem Value="P">Múltiplas</asp:ListItem>
                        <asp:ListItem Value="O">Outras</asp:ListItem>                 
                    </asp:DropDownList>
                </li>  
                <li class="liNomePaiAluno">
                    <label for="txtNomePaiAlunoRVL" title="Nome do Pai do Aluno Candidato">Nome Pai</label>
                    <asp:TextBox ID="txtNomePaiAlunoRVL" CssClass="campoNomePessoa" Enabled="false" ToolTip="Informe o Nome do Pai do Aluno Candidato" runat="server" MaxLength="100"></asp:TextBox>
                </li>                  
                <li class="liSeparador"></li>
                <li class="liTitEndereco">
                    <label class="lblTitEndereco">Endereço Residencial</label>
                </li>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                <li style="display:inline;margin-left:5px;">
                    <label for="txtCepCandidatoRVL" class="lblObrigatorio" title="CEP">CEP</label>
                    <asp:TextBox ID="txtCepCandidatoRVL" CssClass="campoCep" ToolTip="Informe o CEP do Aluno Candidato" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCepCandidatoRVL" 
                        ErrorMessage="CEP deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>                
                <li class="liPesquisarCep" runat="server">
                    <asp:ImageButton ID="btnPesqCEPCandid" CssClass="liPesquisarCep" runat="server"
                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" onclick="btnPesqCEPCandid_Click"
                        CausesValidation="false"/>
                </li>
                <li class="liCorreios" runat="server">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/Library/IMG/Gestor_Correios.gif" CssClass="btnCorreiosRVL" OnClientClick="javascript:window.open('http://www.buscacep.correios.com.br/servicos/dnec/index.do');" CausesValidation="false"/>
                </li>
                <li style="display:inline;margin-top:11px;margin-left:5px;">                
                    <asp:CheckBox ID="chkEndAluRespRVL" runat="server" Enabled="false" 
                            style="margin-left:-5px;" ToolTip="Mesmo Endereço do Responável" 
                            AutoPostBack="True" oncheckedchanged="chkEndAluRespRVL_CheckedChanged"></asp:CheckBox>                
                    <span style="margin-left:-5px;">Mesmo endereço do responsável?</span>
                </li>
                <li class="liClear">
                    <label for="ddlUfCandidatoRVL" class="lblObrigatorio" title="UF">UF</label>
                    <asp:DropDownList ID="ddlUfCandidatoRVL" runat="server" CssClass="campoUf" 
                        ToolTip="Informe a UF do Aluno Candidato" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlUfCandidatoRVL_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlUfCandidatoRVL" 
                        ErrorMessage="UF do Aluno Candidato deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li> 
                <li class="liCidade">
                    <label for="ddlCidadeCandidatoRVL" class="lblObrigatorio" title="Cidade">Cidade</label>
                    <asp:DropDownList ID="ddlCidadeCandidatoRVL" runat="server"
                        ToolTip="Informe a Cidade do Aluno Candidato"
                        CssClass="ddlCidadeRVL" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlCidadeCandidatoRVL_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCidadeCandidatoRVL" 
                        ErrorMessage="Cidade do Aluno Candidato deve ser informada" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li> 
                <li class="liBairro">
                    <label for="ddlBairroCandidatoRVL" class="lblObrigatorio" title="Bairro">Bairro</label>
                    <asp:DropDownList ID="ddlBairroCandidatoRVL" CssClass="ddlBairroRVL" 
                        ToolTip="Informe o Bairro do Aluno Candidato" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlBairroCandidatoRVL" 
                        ErrorMessage="Bairro do Aluno Candidato deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>
                <li class="liClear">
                    <label for="txtLogradouroCandidatoRVL" class="lblObrigatorio" title="Logradouro da Residência do Aluno Candidato">Logradouro</label>
                    <asp:TextBox ID="txtLogradouroCandidatoRVL" CssClass="txtLogradouroRVL" ToolTip="Informe o Logradouro da Residência do Aluno Candidato" runat="server" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLogradouroCandidatoRVL" 
                        ErrorMessage="Logradouro deve ser informado" Display="None" CssClass="validatorField">
                    </asp:RequiredFieldValidator>
                </li>                
                <li class="liClearNone">
                    <label for="txtNumeroCandidatoRVL" title="Número da Residência do Aluno Candidato">Número</label>
                    <asp:TextBox ID="txtNumeroCandidatoRVL" CssClass="txtNumeroRVL" ToolTip="Informe o Número da Residência do Aluno Candidato" runat="server" MaxLength="5"></asp:TextBox>
                </li>
                <li class="liClearNone">
                    <label for="txtComplementoCandidatoRVL" title="Complemento">Complemento</label>
                    <asp:TextBox ID="txtComplementoCandidatoRVL" CssClass="txtComplementoRVL" ToolTip="Informe o Complemento da Residência do Aluno Candidato" runat="server" MaxLength="30"></asp:TextBox>
                </li>     
                </ContentTemplate>
                </asp:UpdatePanel>
                <li class="liSeparador"></li>
                <li id="liObsAlunoCandidato" class="liObsAlunoCandidato">
                    <label for="txtObsAlunoCandidatoRVL" title="Telefone Residencial do Candidato">Observações sobre o Candidato</label>
                    <asp:TextBox ID="txtObservacoesRVL" CssClass="txtObservacoesRVL" Enabled="false" ToolTip="Informe as Observações sobre o Aluno Candidato" runat="server" 
                    TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"></asp:TextBox>
                </li>
            </ul>
        </li>                       
        <li class="liDadosReserva">            
            <ul>                
                <li class="liBarraReserva" style="width:100% !important;margin-top:5px;"><span>DADOS DE RESERVA</span></li>
                <li style="clear:both;width:250px;">
                    <ul>
                        <li>
                            <label for="ddlUnidadeSugeridaRVL" class="lblObrigatorio" title="Unidade">Opção 1 - Unidade/Escola</label>
                            <asp:DropDownList ID="ddlUnidadeSugeridaRVL" runat="server" CssClass="campoUnidadeEscolar"
                                ToolTip="Selecione a Unidade/Escola" AutoPostBack="True" 
                                onselectedindexchanged="ddlUnidadeSugeridaRVL_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlUnidadeSugeridaRVL" 
                                ErrorMessage="Opção 1 - Unidade/Escola deve ser informado" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlUnidadeSugerida2RVL" title="Unidade">Opção 2 - Unidade/Escola</label>
                            <asp:DropDownList ID="ddlUnidadeSugerida2RVL" runat="server" CssClass="campoUnidadeEscolar"
                                ToolTip="Selecione a Opção 2 - Unidade/Escola" 
                                onselectedindexchanged="ddlUnidadeSugerida2RVL_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlUnidadeSugerida3RVL" title="Unidade">Opção 3 - Unidade/Escola</label>
                            <asp:DropDownList ID="ddlUnidadeSugerida3RVL" runat="server" CssClass="campoUnidadeEscolar"
                                ToolTip="Selecione a Opção 3 - Unidade/Escola">
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>
                <li style="clear:none;">
                    <ul>
                        <li>                    
                            <label for="ddlTipoReservaRVL" title="Tipo de Reserva de Matrícula">Tipo de Reserva</label>
                            <asp:DropDownList ID="ddlTipoReservaRVL" runat="server" style="width:90px;"
                                ToolTip="Selecione o Tipo de Reserva de Matrícula">
                                <asp:ListItem Value="N">Matrícula Nova</asp:ListItem>
                                <asp:ListItem Value="R">Rematrícula</asp:ListItem>
                            </asp:DropDownList>
                        </li>

                        <li class="liSemestre" style="clear:none;margin-left:10px;">
                            <label for="ddlSemestreRVL" class="lblObrigatorio" title="Semeestre/Ano">Semestre/Ano</label>
                            <asp:DropDownList ID="ddlSemestreRVL" runat="server" CssClass="ddlSemestreRVL"
                                ToolTip="Selecione o Semestre/Ano"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" class="validatorField" runat="server" ControlToValidate="ddlSemestreRVL"
                                ErrorMessage="Semestre deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                            
                            <asp:DropDownList ID="ddlAnoRVL" runat="server" CssClass="ddlAnoRVL"
                                ToolTip="Selecione o Ano"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" class="validatorField" runat="server" ControlToValidate="ddlAnoRVL"
                                ErrorMessage="Ano deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>               
                        <li style="clear:both;">
                            <label for="ddlModalidadeRVL" class="lblObrigatorio" title="Modalidade">Modalidade</label>
                            <asp:DropDownList ID="ddlModalidadeRVL" runat="server" CssClass="campoModalidade" 
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlModalidadeRVL_SelectedIndexChanged"
                                ToolTip="Selecione a Modalidade">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlModalidadeRVL" 
                                ErrorMessage="Modalidade deve ser informada" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="clear:both;">
                            <label for="ddlSerieCursoRVL" class="lblObrigatorio" title="Série/Curso">Série/Curso</label>
                            <asp:DropDownList ID="ddlSerieCursoRVL" runat="server" CssClass="ddlSerieCurso"
                                ToolTip="Selecione a Série/Curso"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSerieCursoRVL" 
                                ErrorMessage="Série/Curso deve ser informada" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li id="liTurno" class="liTurno" style="clear:none;">
                            <label for="ddlTurnoRVL" class="lblObrigatorio" title="Turno">Turno</label>
                            <asp:DropDownList ID="ddlTurnoRVL" CssClass="ddlTurno" runat="server"
                                ToolTip="Selecione o Turno">
                                <asp:ListItem Value="">Selecione</asp:ListItem>
                                <asp:ListItem Value="M">Matutino</asp:ListItem>
                                <asp:ListItem Value="V">Vespertino</asp:ListItem>
                                <asp:ListItem Value="N">Noturno</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="validatorField" ControlToValidate="ddlTurnoRVL"
                                ErrorMessage="Turno deve ser informado">
                            </asp:RequiredFieldValidator>
                        </li>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </li>
                <li style="clear:none;margin-left:20px;">
                    <ul>
                        <li style="clear:none;">
                            <label for="txtNumReservaRVL" title="Número da Reserva">N° Reserva</label>
                            <asp:TextBox ID="txtNumReservaRVL" runat="server" CssClass="txtNumReservaRVL"
                                ToolTip="Número da Reserva" Enabled="false">
                            </asp:TextBox>
                        </li>                                    
            
                        <li id="liDataCadastro" style="clear:both;">
                            <label for="txtDataCadastroRVL" class="lblObrigatorio" 
                                title="Data de Cadastro do Aluno">Data Cadastro</label>
                            <asp:TextBox ID="txtDataCadastroRVL" CssClass="campoData" Enabled="false"
                                ToolTip="Informe a Data de Cadastro do Aluno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataCadastroRVL" 
                                ErrorMessage="Data de Cadastro deve ser informada" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="clear:both;">
                            <label for="txtDataValidadeRVL" class="lblObrigatorio" 
                                title="Data de Validade da Reserva">Data Validade</label>
                            <asp:TextBox ID="txtDataValidadeRVL" CssClass="campoData" ToolTip="Informe a Data de Validade da Reserva" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataValidadeRVL"
                                ErrorMessage="Data de Validade deve ser informada" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>                                        
                    </ul>
                </li> 
                <li style="clear:none; margin-left:20px;">
                    <ul>
                        <li id="li9" class="liObsAlunoCandidato">
                            <label for="txtObsReservaRVL" title="Telefone Residencial do Candidato">Observações da Reserva</label>
                            <asp:TextBox ID="txtObsReservaRVL" CssClass="txtObsReservaRVL" ToolTip="Informe as Observações sobre a Reserva" runat="server" 
                            TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"></asp:TextBox>
                        </li>
                        <li style="clear:both;margin-left: 45px;margin-top:1px;">
                            <label for="ddlSituacaoRVL" class="lblObrigatorio" title="Situação da Reserva de Matrícula">Situação</label>
                            <asp:DropDownList ID="ddlSituacaoRVL" style="width:75px;"
                                ToolTip="Informe a Situação da Reserva de Matrícula" runat="server" 
                                AutoPostBack="true" onselectedindexchanged="ddlSituacaoRVL_SelectedIndexChanged">
                                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
                                <asp:ListItem Value="I">Inscrição</asp:ListItem>
                                <asp:ListItem Value="E">Efetivada</asp:ListItem>
                                <asp:ListItem Value="C">Cancelada</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlSituacaoRVL" 
                                ErrorMessage="Situação deve ser informada" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li id="liMotivoCancel" runat="server" visible="false" style="margin-left:5px;">
                            <label for="ddlMotivoCancelRVL" class="lblObrigatorio" title="Motivo do Cancelamento">Motivo Cancelamento</label>
                            <asp:DropDownList ID="ddlMotivoCancelRVL" runat="server"
                                ToolTip="Selecione o Motivo do Cancelamento">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left:5px;clear:none;margin-top:1px;">
                            <label for="txtDataStatusRVL" class="lblObrigatorio" title="Data de Status da Reserva">Data Status</label>
                            <asp:TextBox ID="txtDataStatusRVL" Enabled="false" CssClass="campoData" ToolTip="Informe a Data de Status da Reserva" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtDataStatusRVL"
                                ErrorMessage="Data de Status deve ser informada" Display="None" CssClass="validatorField">
                            </asp:RequiredFieldValidator>
                        </li>
                        
                    </ul>
                </li>    
                <li class="liClear" style="float: right; margin-top: 10px; padding-right: 7px;">
                    <asp:CheckBox ID="chkGeraCompro" runat="server" Enabled="false" 
                            style="margin-left:-5px;" ToolTip="Gera comprovante de reserva"></asp:CheckBox>                
                    <span style="margin-left:-5px;">Gera Comprovante?</span>
                </li>      
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".campoCep").mask("99999-999");
            $(".txtNumeroRVL").mask("?99999");
        }); 

        jQuery(function ($) {
            $(".txtNireRVL").mask("?999999");
            $(".txtNumeroRVL").mask("?99999");
            $(".txtQtdDepenRVL").mask("?99");
            $(".campoCep").mask("99999-999");
            $(".campoTelefone").mask("(99) 9999-9999");
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>