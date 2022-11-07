<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3615_ExclusaoAlunoSemMovtoDados.Cadastro" %>
<%@ Register src="~/Library/Componentes/ControleImagem.ascx" tagname="ControleImagem" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">  
        .ulDados{ width: 910px; margin-left: 20px;}        
        .ulDados li input{ margin-bottom: 0;}
        .ulDados li fieldset ul li{ margin-top: 0;}        

        /*--> CSS FIELDSETs */
        fieldset
        {
            padding: 0px  0px 5px 5px;
            margin: 0;
        }
        .fldInfoPessoal{ width: 200px; }
        .fldNacionalidade{ width: 300px; }                
        .fldFiliacao{ width: 220px; } 
        .fldSituacao{  height: 76px; border: none;}
        .fldEndereco{ border: none; margin: 0;}
        .fldDadosMatricula{ margin-top: 3px; margin-left: 11px;}
        .fldFiliacao ul li{margin-top: 2px;}
        .fldAlunosEspeciais ul li{ margin-bottom: 4px;}
        .fldDoctosDiversos ul li{ margin-top: 4px;}
        .fldIdentidade ul li{ margin-top: 4px;}
        .fldTituloEleitor ul li{ margin-top: 4px;}
        .fldEndereco ul li{ margin-top: 4px;}
        .fldCertidao ul li{ margin-top: 4px;}
        .fldBolsista ul li{ margin-top: 4px;}
        .fldSituacao{ height: 78px;}
        .fldInfoPessoal, .fldFiliacao{height: 104px;}        
        .fldAlunosEspeciais, .fldProgramasSociais{ height: 108px;}
        .fldAlunosEspeciais, .fldInfoPessoal, .fldProgramasSociais, .fldFiliacao{ margin-right: 6px; padding-right: 4px;}
        .fldDoctosDiversos, .fldCertidao, .fldTituloEleitor, .fldBolsista, .fldIdentidade{ height: 78px; margin-right: 2px; padding-right: 1px;}

        /*--> CSS ULs */
        .ulInfoPessoal1{ border-right: dashed 1px #DDDDDD;}
        .ulInfoPessoal1, .ulInfoPessoal2{ float: left; margin-top: 5px;}
        .ulInfoPessoal1 li, .ulInfoPessoal2 li{ clear: both; margin-bottom: 5px;}
        .ulInfoPessoal1{ padding-right: 5px;}
        .ulInfoPessoal2{ margin-left: 10px;}

        /*--> CSS LIs */  
        .ulDados li{ margin-top: 0px;}
        .liAlunosEspeciais{ margin-top: 0; }
        .liBolsa { margin-left: 7px;}
        .liClear { clear: both; }
        .liDataNascimento, .liEmail, .liSexo { margin-left: 10px;}
        .liNacionalidade { margin-left: 12px;}
        .liNis{ margin-left: 6px;}
        .liPeriodoAte{ margin-top: 17px !important;}
        .liPeriodoDe { margin-left: 7px; margin-right: 0px; }
        .liPhoto{ height: 100px; margin-top: 6px; margin-left: -3px;} 
        .liProgramasSociais{ margin-top: 0; }
        .liSituacaoAluno { margin-top: 4px; margin-left: 7px; padding-left: 12px; border-left: dashed 1px #CCCCCC; height: 100px;}
        .liTitEndereco { padding: 9px 0px 0px 0px; clear:both; margin-top: 3px;}  
        .liSituacao{ margin-top: 15px;}
        .liDataCadastro { margin-top: 15px; clear: both; }

        /*--> CSS DADOS */
        .formAuxText1
        {
            padding-left: 5px;
            padding-top: 12px;
        }
        .divInstEspecializada
        { 
        	height: 48px; 
        	width: 144px;
        	overflow-y: scroll;
        	border: solid 1px #CCCCCC;
        }
        .divProgramasSociais
        { 
        	height: 84px; 
        	width: 120px;
        	overflow-y: scroll;
        	border: solid 1px #CCCCCC;
        	margin-top: 10px;
        }
        .divInstEspecializada table tr td label, #divProgramasSociais table tr td label
        {
        	display: inline;
        	margin-left: 0px;
        }
        .divInstEspecializada table, .divProgramasSociais table { border: none; }
        .rblBolsista{ margin-top: 10px; border: none; }
        .rblBolsista tr td label
        { 
        	display: inline; 
        	margin-left: 2px;
        }                
        .txtLogradouro { width: 130px;}
        .ddlCidade { width: 177px;}
        .ddlBairro { width: 150px;}
        .txtTurma { width: 31px;}
        .txtSerie, .txtFolha { width: 24px;}
        .txtModalidade { width: 71px;}
        .txtUnidade { width: 39px;}
        .txtMatricula { width: 69px;}
        .txtComplemento{ width: 128px;}
        .txtResponsavel{ width: 143px;}
        .txtGrauParentesco{ width: 60px;}
        .txtEmail{ width: 206px;}
        .txtCartaoSaude{ width: 78px;}        
        .txtNire, .txtNis{ width: 46px;}
        .ddlSexo, .ddlRendaFamiliar{ width: 80px;}        
        .ddlNacionalidade, .txtNacionalidade, .ddlEtnia, .ddlDeficiencia, .txtDeficiencia, .ddlPasseEscolar, .ddlTransporteEscolar, .txtRg, .txtNumeroTitulo  { width: 70px;}
        .txtNaturalidade{ width: 95px; }
        .ddlEstadoCivil{ width: 90px; }      
        .txtObservacoes{ width: 419px; height: 37px; margin-top: 2px; }   
        .ddlTipoCertidao{ width: 79px; }
        .txtNumeroCertidao{ width: 37px; }
        .txtLivro{ width: 25px; }
        { width: 24px; }
        .txtCartorio{ width: 185px; } 
        .txtOrgaoEmissor{ width: 55px; }
        .txtSecao, .txtZona, .txtNumero, .txtDesconto { width: 40px; }  
        .ddlBolsa{ width: 121px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtNire" class="lblObrigatorio" title="Número do NIRE">N° NIRE</label>
            <asp:TextBox ID="txtNire" CssClass="txtNire" runat="server" ToolTip="Informe o Número do NIRE"></asp:TextBox>
            <asp:RangeValidator ID="rvNire" runat="server" ControlToValidate="txtNire" CssClass="validatorField"
                ErrorMessage="NIRE deve estar entre 0 e 1000000" Text="*" Type="Integer"
                MaximumValue="1000000" MinimumValue="0"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="rfvNire" runat="server" ControlToValidate="txtNire" ErrorMessage="NIRE deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liNis"> 
            <label for="txtNis" title="Número do NIS">N° NIS</label>
            <asp:TextBox ID="txtNis" CssClass="txtNis" runat="server" ToolTip="Informe o Número do NIS" MaxLength="11"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNis"
                ErrorMessage="NIS deve ser numérico de 11 dígitos" Text="*" Type="Double"
                MaximumValue="99999999999" MinimumValue="0"></asp:RangeValidator>
        </li>
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Nome do Aluno">Nome</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="campoNomePessoa" ToolTip="Informe o Nome do Aluno" MaxLength="80"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liSexo">
            <label for="ddlSexo" class="lblObrigatorio" title="Sexo do Aluno">Sexo</label>
            <asp:DropDownList ID="ddlSexo" CssClass="ddlSexo" runat="server" ToolTip="Informe o Sexo do Aluno">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Feminino</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSexo" ErrorMessage="Sexo deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataNascimento">
            <label for="txtDataNascimento" class="lblObrigatorio" title="Data de Nascimento">Data Nascimento</label>
            <asp:TextBox ID="txtDataNascimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liNacionalidade">
            <fieldset class="fldNacionalidade">
                <ul>
                    <li>  
                        <label for="ddlNacionalidade" class="lblObrigatorio" title="Nacionalidade do Aluno">Nacionalidade</label>
                        <asp:DropDownList ID="ddlNacionalidade" CssClass="ddlNacionalidade" runat="server" ToolTip="Informe a Nacionalidade do Aluno" AutoPostBack="true" 
                            >
                            <asp:ListItem Value="B">Brasileiro</asp:ListItem>
                            <asp:ListItem Value="E">Estrangeiro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNacionalidade" ErrorMessage="Nacionalidade deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                     
                    </li>
                    <li>
                        <label for="txtNacionalidade" title="País de Nacionalidade do Aluno Estrangeiro">País</label>
                        <asp:TextBox ID="txtNacionalidade" CssClass="txtNacionalidade" runat="server" ToolTip="Informe o País de Nacionalidade do Aluno" Enabled="false" MaxLength="40"></asp:TextBox>                        
                    </li>
                    <li>
                        <label for="txtNaturalidade" title="Cidade de Naturalidade do Aluno">Naturalidade</label>
                        <asp:TextBox ID="txtNaturalidade" CssClass="txtNaturalidade" runat="server" ToolTip="Informe a Cidade de Naturalidade do Aluno" MaxLength="40"></asp:TextBox>                        
                    </li>
                    <li>
                        <label for="ddlUfNacionalidade" title="UF de Nacionalidade do Aluno">UF</label>
                        <asp:DropDownList ID="ddlUfNacionalidade" CssClass="campoUf" runat="server" ToolTip="Informe a UF de Nacionalidade"></asp:DropDownList>                            
                    </li>
                </ul>
            </fieldset>
        </li>
        <li>
            <fieldset class="fldInfoPessoal">
                <ul class="ulInfoPessoal1">
                    <li>
                        <label for="ddlEstadoCivil" class="lblObrigatorio" title="Estado Civil do Aluno">Estado Civil</label>
                        <asp:DropDownList ID="ddlEstadoCivil" CssClass="ddlEstadoCivil" runat="server" ToolTip="Informe o Estado Civil do Aluno">
                            <asp:ListItem Value="O">Solteiro</asp:ListItem>
                            <asp:ListItem Value="C">Casado</asp:ListItem>
                            <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                            <asp:ListItem Value="D">Divorciado</asp:ListItem>
                            <asp:ListItem Value="V">Viúvo</asp:ListItem>
                            <asp:ListItem Value="P">Companheiro</asp:ListItem>
                            <asp:ListItem Value="U">União Estável</asp:ListItem>
                        </asp:DropDownList>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEstadoCivil" ErrorMessage="Estado civil deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                       
                    </li> 
                    <li>
                        <label for="ddlEtnia" class="lblObrigatorio" title="Etnia do Aluno">Etnia</label>
                        <asp:DropDownList ID="ddlEtnia" CssClass="ddlEtnia" runat="server" ToolTip="Informe a Etnia do Aluno">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="B">Branca</asp:ListItem>
                            <asp:ListItem Value="N">Negra</asp:ListItem>
                            <asp:ListItem Value="A">Amarela</asp:ListItem>
                            <asp:ListItem Value="P">Parda</asp:ListItem>
                            <asp:ListItem Value="I">Indígena</asp:ListItem>
                        </asp:DropDownList>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlEtnia" ErrorMessage="Etnia deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                        
                    </li>
                    <li>
                        <label for="ddlRendaFamiliar" class="lblObrigatorio" title="Renda Familiar do Aluno">Renda Familiar</label>
                        <asp:DropDownList ID="ddlRendaFamiliar" CssClass="ddlRendaFamiliar" runat="server" ToolTip="Informe a Renda Familiar do Aluno">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                            <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                            <asp:ListItem Value="3">+5 SM</asp:ListItem>
                            <asp:ListItem Value="4">Sem Renda</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRendaFamiliar" ErrorMessage="Renda familiar deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                </ul>
                <ul class="ulInfoPessoal2">
                    <li>
                        <label for="ddlPasseEscolar" title="Precisa de Passe Escolar?">Passe Escolar</label>
                        <asp:DropDownList ID="ddlPasseEscolar" CssClass="ddlPasseEscolar" runat="server" ToolTip="Informe se o Aluno Precisa de Passe Escolar">
                            <asp:ListItem Value="True">Sim</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">Não</asp:ListItem>
                        </asp:DropDownList>                          
                    </li>
                    <li>
                        <label for="ddlTransporteEscolar" title="Participa do Transporte Escolar?">Transporte Escolar</label>
                        <asp:DropDownList ID="ddlTransporteEscolar" CssClass="ddlTransporteEscolar" runat="server" ToolTip="Informe se o Aluno participará do Transporte Escolar">
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                        </asp:DropDownList>                          
                    </li>
                </ul>
            </fieldset>
        </li>       
        <li class="liAlunosEspeciais">
            <fieldset class="fldAlunosEspeciais">
                <legend>Alunos Especiais</legend>
                <ul>    
                    <li>
                        <label for="ddlDeficiencia" title="Deficiência">Deficiência</label>
                        <asp:DropDownList ID="ddlDeficiencia" CssClass="ddlDeficiencia" runat="server" ToolTip="Selecione a Deficiência do Aluno" AutoPostBack="true" >
                            <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                            <asp:ListItem Value="A">Auditiva</asp:ListItem>   
                            <asp:ListItem Value="V">Visual</asp:ListItem>
                            <asp:ListItem Value="F">Física</asp:ListItem>
                            <asp:ListItem Value="M">Mental</asp:ListItem>
                            <asp:ListItem Value="P">Múltiplas</asp:ListItem>
                            <asp:ListItem Value="O">Outras</asp:ListItem>                 
                        </asp:DropDownList> 
                        <asp:TextBox ID="txtDeficiencia" CssClass="txtDeficiencia" ToolTip="Informe a Deficiência" Enabled="false" runat="server" MaxLength="50"></asp:TextBox>                         
                    </li>
                    <li class="liClear">                    
                        <label for="cblInstEspecializada" title="Instituições de Apoio das quais o Aluno participa">Instituições de Apoio</label>
                        <div class="divInstEspecializada">
                            <asp:CheckBoxList ID="cblInstEspecializada" CssClass="cblInstEspecializada" ToolTip="Marque as Instituições de Apoio das quais o Aluno participa" runat="server" Enabled="False">
                            </asp:CheckBoxList>          
                        </div>
                    </li>
                </ul>
            </fieldset>
        </li>       
        <li class="liProgramasSociais">
            <fieldset class="fldProgramasSociais">
                <legend title="Programas Sociais dos quais o Aluno participa">Programas Sociais</legend>
                <ul>
                    <li>
                        <div class="divProgramasSociais">
                            <asp:CheckBoxList ID="cblProgramasSociais" CssClass="cblProgramasSociais" ToolTip="Marque os Programas Sociais dos quais o Aluno participa" runat="server">
                            </asp:CheckBoxList>          
                        </div>
                    </li>
                </ul>
            </fieldset>
        </li>   
        <li>
            <fieldset class="fldFiliacao">
                <legend>Filiação/Responsável</legend>
                <ul>
                    <li>
                        <label for="txtNomeMae" class="lblObrigatorio" title="Nome da Mãe">Mãe</label>
                        <asp:TextBox ID="txtNomeMae" CssClass="campoNomePessoa" ToolTip="Informe o Nome da Mãe" runat="server" MaxLength="60"></asp:TextBox>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNomeMae" ErrorMessage="Nome da mãe deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                           
                    </li>
                    <li>
                        <label for="txtNomePai" title="Nome do Pai">Pai</label>
                        <asp:TextBox ID="txtNomePai" CssClass="campoNomePessoa" ToolTip="Informe o Nome do Pai" runat="server" MaxLength="60"></asp:TextBox>                        
                    </li>
                    <li>
                        <label for="txtResponsavel" title="Nome do Responsável pelo Aluno">Responsável</label>
                        <asp:TextBox ID="txtResponsavel" CssClass="txtResponsavel" ToolTip="Informe o Nome do Responsável pelo Aluno" Enabled="false" runat="server"></asp:TextBox>                              
                    </li>
                    <li>
                        <label for="txtGrauParentesco" title="Grau de Parentesco do Responsável">Parentesco</label>
                        <asp:TextBox ID="txtGrauParentesco" CssClass="txtGrauParentesco" ToolTip="Informe o Grau de Parentesco do Responsável" Enabled="false" runat="server"></asp:TextBox>                              
                    </li>
                </ul>
            </fieldset>
        </li>   
        <li class="liPhoto">
            <uc1:ControleImagem ID="upImagem" runat="server" />
        </li> 
        <li class="liClear">
            <fieldset class="fldDoctosDiversos">
                <legend>Doctos Diversos</legend>
                <ul>
                    <li class="liClear">  
                        <label for="txtCpf" title="CPF do Aluno">CPF</label>
                        <asp:TextBox ID="txtCpf" ToolTip="Informe o CPF do Aluno" runat="server" CssClass="campoCpf"></asp:TextBox>                         
                    </li>
                    <li class="liClear">
                        <label for="txtCartaoSaude" title="Número do Cartão Saúde">N° Cartão Saúde</label>
                        <asp:TextBox ID="txtCartaoSaude" ToolTip="Informe o Número do Cartão Saúde" runat="server" CssClass="txtCartaoSaude"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li>
            <fieldset class="fldIdentidade">
                <legend>Identidade</legend>
                <ul>                        
                    <li>      
                        <label for="txtRg" title="Número da Identidade">Identidade</label>
                        <asp:TextBox ID="txtRg" CssClass="txtRg" ToolTip="Informe o Número da Identidade" runat="server" MaxLength="20"></asp:TextBox>  
                    </li>                         
                    <li>      
                        <label for="txtOrgaoEmissor" title="Órgão Emissor da Identidade">Órg. Emissor</label>
                        <asp:TextBox ID="txtOrgaoEmissor" CssClass="txtOrgaoEmissor" ToolTip="Informe o Órgão Emissor da Identidade" runat="server" MaxLength="12"></asp:TextBox>  
                    </li>                         
                    <li class="liClear">      
                        <label for="txtDataEmissaoRg" title="Data de Emissão da Identidade">Data Emissão</label>
                        <asp:TextBox ID="txtDataEmissaoRg" CssClass="campoData" ToolTip="Informe a Data de Emissão da Identidade" runat="server" ></asp:TextBox> 
                    </li>                       
                    <li>      
                        <label for="ddlUfRg" title="UF do Órgão Emissor">UF</label>
                        <asp:DropDownList ID="ddlUfRg" CssClass="campoUf" ToolTip="Informe a UF do Órgão Emissor" runat="server"></asp:DropDownList>  
                    </li>  
                </ul>
            </fieldset>
        </li> 
        <li>
            <fieldset class="fldTituloEleitor">
                <legend>Título Eleitor</legend>
                <ul>
                    <li>
                        <label for="txtNumeroTitulo" title="Número do Título de Eleitor">Número</label>
                        <asp:TextBox ID="txtNumeroTitulo" CssClass="txtNumeroTitulo" ToolTip="Informe o Número do Título de Eleitor" runat="server" MaxLength="15"></asp:TextBox> 
                    </li>
                    <li>
                        <label for="txtZona" title="Zona do Título de Eleitor">Zona</label>
                        <asp:TextBox ID="txtZona" CssClass="txtZona" ToolTip="Informe a Zona do Título de Eleitor" runat="server" MaxLength="10"></asp:TextBox>
                    </li>
                    <li class="liClear">
                        <label for="txtSecao" title="Seção do Título de Eleitor">Seção</label>
                        <asp:TextBox ID="txtSecao" CssClass="txtSecao" ToolTip="Informe a Seção do Título de Eleitor" runat="server" MaxLength="10"></asp:TextBox> 
                    </li>
                    <li>
                        <label for="ddlUfTitulo" title="UF do Título de Eleitor">UF</label>
                        <asp:DropDownList ID="ddlUfTitulo" CssClass="campoUf" ToolTip="Informe a UF do Título de Eleitor" runat="server"></asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li>
            <fieldset class="fldCertidao">
                <legend>Certidão</legend>
                <ul>
                    <li>
                        <label for="ddlTipoCertidao" class="lblObrigatorio" title="Tipo de Certidão">Tipo</label>
                        <asp:DropDownList ID="ddlTipoCertidao" CssClass="ddlTipoCertidao" ToolTip="Selecione o Tipo de Certidão" runat="server">
                            <asp:ListItem Value="N">Nascimento</asp:ListItem>
                            <asp:ListItem Value="C">Casamento</asp:ListItem>
                        </asp:DropDownList>    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTipoCertidao" ErrorMessage="Tipo de certidão deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                  
                    </li>
                    <li>                                     
                        <label for="txtNumeroCertidao" class="lblObrigatorio" title="Número da Certidão">Número</label>
                        <asp:TextBox ID="txtNumeroCertidao" CssClass="txtNumeroCertidao" ToolTip="Informe o Número da Certidão" runat="server" MaxLength="10"></asp:TextBox>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNumeroCertidao" ErrorMessage="Número da Certidão deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                  
                    </li>
                    <li>   
                        <label for="txtLivro" class="lblObrigatorio" title="Livro da Certidão">Livro</label>
                        <asp:TextBox ID="txtLivro" CssClass="txtLivro" ToolTip="Informe o Livro da Certidão" runat="server" MaxLength="10"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLivro" ErrorMessage="Livro da certidão deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                     
                    </li>
                    <li>  
                        <label for="txtFolha" class="lblObrigatorio" title="Folha da Certidão">Folha</label>
                        <asp:TextBox ID="txtFolha" CssClass="txtFolha" ToolTip="Informe a Folha da Certidão" runat="server" MaxLength="8"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFolha" ErrorMessage="Folha da certidão deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                      
                    </li>
                    <li class="liClear">    
                        <label for="txtCartorio" class="lblObrigatorio" title="Cartório da Certidão">Cartório</label>
                        <asp:TextBox ID="txtCartorio" CssClass="txtCartorio" ToolTip="Informe o Cartório da Certidão" runat="server" MaxLength="80"></asp:TextBox>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCartorio" ErrorMessage="Cartório deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>                                                                  
                    </li>
                </ul>
            </fieldset>                        
        </li>    
        <li>
            <fieldset class="fldBolsista">
                <legend>Aluno Bolsista</legend>
                <ul>
                    <li>                            
                        <asp:RadioButtonList ID="rblBolsista" ToolTip="Informe se o Aluno é Bolsista" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="rblBolsista" >
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                            <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li class="liBolsa">
                        <label for="ddlBolsa" title="Tipo de Bolsa">Bolsa</label>
                        <asp:DropDownList ID="ddlBolsa" CssClass="ddlBolsa" ToolTip="Selecione o Tipo de Bolsa" runat="server" Enabled="False"></asp:DropDownList>
                    </li>
                    <li class="liClear">
                        <label for="txtDesconto" title="Porcentagem de Desconto">% Desc.</label>
                        <asp:TextBox ID="txtDesconto" CssClass="txtDesconto campoMoeda" ToolTip="Informe a Porcentagem de Desconto" runat="server" Enabled="False" ></asp:TextBox>
                    </li>
                    <li class="liPeriodoDe">
                        <label for="txtPeriodoDe" title="Período de Duração da Bolsa">Período</label>
                        <asp:TextBox ID="txtPeriodoDe" ToolTip="Informe a Data de Início da Bolsa" runat="server" CssClass="campoData" Enabled="False"></asp:TextBox>
                    </li>
                    <li class="formAuxText1"><span>à</span> 
                    </li>
                    <li class="liPeriodoAte">
                        <asp:TextBox ID="txtPeriodoAte" ToolTip="Informe a Data de Término da Bolsa" runat="server" CssClass="campoData" Enabled="False"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li class="liTitEndereco">
            <img src="../../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
        </li>
        <li>
            <fieldset class="fldEndereco">
                <ul>
                    <li>
                        <label for="txtLogradouro" class="lblObrigatorio" title="Logradouro da Residência do Aluno">Logradouro</label>
                        <asp:TextBox ID="txtLogradouro" CssClass="txtLogradouro" ToolTip="Informe o Logradouro da Residência do Aluno" runat="server" MaxLength="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtLogradouro" ErrorMessage="Logradouro deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtNumero" title="Número da Residência do Aluno">Número</label>
                        <asp:TextBox ID="txtNumero" CssClass="txtNumero" ToolTip="Informe o Número da Residência do Aluno" runat="server" MaxLength="5"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtComplemento" title="Complemento">Complemento</label>
                        <asp:TextBox ID="txtComplemento" CssClass="txtComplemento" ToolTip="Informe o Complemento" runat="server" MaxLength="40"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtCep" class="lblObrigatorio" title="CEP">CEP</label>
                        <asp:TextBox ID="txtCep" CssClass="campoCep" ToolTip="Informe o CEP" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCep" ErrorMessage="CEP deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="ddlCidade" class="lblObrigatorio" title="Cidade">Cidade</label>
                        <asp:DropDownList ID="ddlCidade" ToolTip="Informe a Cidade" runat="server" CssClass="ddlCidade" AutoPostBack="true" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlCidade" ErrorMessage="Cidade deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlBairro" class="lblObrigatorio" title="Bairro">Bairro</label>
                        <asp:DropDownList ID="ddlBairro" CssClass="ddlBairro" ToolTip="Informe o Bairro" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlBairro" ErrorMessage="Bairro deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlUf" class="lblObrigatorio" title="UF">UF</label>
                        <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf" ToolTip="Informe a UF" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlUf" ErrorMessage="UF deve ser informado" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtTelResidencial" title="Telefone Residencial">Tel. Residencial</label>
                        <asp:TextBox ID="txtTelResidencial" CssClass="campoTelefone" ToolTip="Informe o Telefone Residencial do Aluno" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <label for="txtTelCelular" title="Telefone Celular">Tel. Celular</label>
                        <asp:TextBox ID="txtTelCelular" CssClass="campoTelefone" ToolTip="Informe o Telefone Celular do Aluno" runat="server"></asp:TextBox>
                    </li>
                    <li class="liEmail">
                        <label for="txtEmail" title="E-mail">E-mail</label>
                        <asp:TextBox ID="txtEmail" CssClass="txtEmail" ToolTip="Informe o E-mail do Aluno" runat="server" MaxLength="50"></asp:TextBox>
                    </li>
                </ul>
            </fieldset>
         </li> 
        <li class="liSituacaoAluno">
            <fieldset class="fldSituacao">
                <ul>
                    <li>
                        <label for="txtObservacoes" title="Observações sobre o Aluno">Observações</label>
                        <asp:TextBox ID="txtObservacoes" CssClass="txtObservacoes" ToolTip="Informe as Observações sobre o Aluno" runat="server" TextMode="MultiLine" onkeyup="javascript:MaxLength(this, 500);"></asp:TextBox>
                    </li>
                    <li class="liDataCadastro">
                        <label for="txtDataCadastro" class="lblObrigatorio" title="Data de Cadastro do Aluno">Data Cadastro</label>
                        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" Enabled="False" ToolTip="Informe a Data de Cadastro do Aluno" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDataCadastro" ErrorMessage="Data de Cadastro deve ser informada" Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liSituacao">
                        <label for="ddlSituacao" class="lblObrigatorio" title="Situação do Aluno">Situação</label>
                        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" ToolTip="Informe a Situação do Aluno" runat="server">
                            <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                            <asp:ListItem Value="I">Inativo</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <fieldset class="fldDadosMatricula">
                            <legend>Dados Matrícula</legend>
                            <ul>
                                <li>
                                    <label for="txtUnidade" title="Unidade/Escola na qual o Aluno está Matriculado">Unidade</label>
                                    <asp:TextBox ID="txtUnidade" CssClass="txtUnidade" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtMatricula" title="Número de Matrícula do Aluno">Matrícula</label>
                                    <asp:TextBox ID="txtMatricula" CssClass="txtMatricula" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtModalidade" title="Modalidade no qual o Aluno está Matriculado">Modalidade</label>
                                    <asp:TextBox ID="txtModalidade" CssClass="txtModalidade" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtSerie" title="Série na qual o Aluno está Matriculado">Série</label>
                                    <asp:TextBox ID="txtSerie" CssClass="txtSerie" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtTurma" title="Turma na qual o Aluno está Matriculado">Turma</label>
                                    <asp:TextBox ID="txtTurma" CssClass="txtTurma" runat="server" Enabled="false"></asp:TextBox>
                                </li>
                            </ul>
                        </fieldset>
                    </li> 
                </ul>
            </fieldset>
        </li> 
               
    </ul>
    <script type="text/javascript">
        jQuery(function($){
           $(".txtNis").mask("?99999999999");
           $(".txtCartaoSaude").mask("?9999999999999999");
           $(".txtNire").mask("?999999");
           $(".txtNumero").mask("?99999");                      
           $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });           
           $(".campoCep").mask("99999-999");
           $(".campoTelefone").mask("(99)9999-9999");
           $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>
      
    
