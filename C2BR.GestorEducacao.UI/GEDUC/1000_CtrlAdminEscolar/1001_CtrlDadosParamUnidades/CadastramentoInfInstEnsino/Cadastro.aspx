<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfInstEnsino.Cadastro" ValidateRequest="false"%>
<%@ Register src="../../../../Library/Componentes/ControleImagem.ascx" tagname="ControleImagem" tagprefix="uc1" %>
<%@ Register assembly="DevExpress.Web.ASPxHtmlEditor.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHtmlEditor" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxSpellChecker.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSpellChecker" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">    
    .ulDados {margin-top:0px !important;}
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
    
    /*--> CSS LIs */
    .liInstituicaoIIE li { margin-top: -7px; }
    .liSituacaoIIE { margin-top: 5px; }
    .liClear { clear: both; }
    .liFotosIIE { margin-top: 3px; height: 80px; }
    .liInstituicaoIIE { margin: 7px 0 0 5px; }
    .liContatoIIE { margin-top: 10px; padding-left: 10px; }
    .liUnidade2IIE { clear: both; }
    .liUnidade2IIE li { margin-top: -7px; }
    .liDadosControleIIE li { margin-top: 5px; }
    .liSituacaoIIE li { margin-top: -7px; }
    .liHorarioFuncionamentoIIE { clear: both; }
    .liHorarioFuncionamentoIIE li { margin-top: -5px; }
    .liTipoControleFuncIIE { margin-bottom: 10px; margin-top: 6px !important; margin-left: 12px; }
    .liTipoControleTpEnsinoIIE { margin-left: 79px; margin-top:0px !important; }
    .liClearIIE { clear: both; padding-top: 2px; width: 125px; }
    .liClear2IIE { clear: both; padding-top: 2px; width: 202px; }
    .liClear3IIE { clear: both; padding-top: 2px; width: 72px; }
    #liTabelaLimiteApontamentoIIE {clear: both; margin-top: -5px;}
    .liControleMetodologia label, .liRecuperacao label, .liMetodEnsino label, .liContrAval label, .liMediaRecuperacao label, #liDependencia label, .liLimiteMedia label { display: inline;}
    .liAprovDireta label, #liConselho label, .liPerioAval label, .liAprovGeral label, #liQtdMateriasRecuperacao label, #liQtdMateriasDependencia label, #liQtdMaxMaterias label { display: inline;}
    .liPedagMatric { background-color: #F9F9FF; padding: 4px 0 4px 0px; color: #006699; text-align:center; width: 220px; }
    .liControleMetodologia { width: 220px; margin-top: 7px; clear: both; }
    .liMetodEnsino {width: 220px; clear: both; margin-top: 3px; }
    .liEquivNotaConce { background-color: #FFE8C4; color:#797979; padding: 4px 0 4px 0px; text-align:center; width: 220px; clear: both; margin-top: 10px; }
    .liNotaConceito { clear: both; margin-top: 2px; }
    .liContrAval{ clear: both; margin-top: 7px; }
    .liAprovDireta, .liAprovGeral { clear: both; width: 190px; }
    .liMediaRecuperacao { clear: both; margin-left: 7px; margin-left: 25px; }
    .liMediaRecuperacao input, .liMediaRecuperacao select{ float: right; margin-left: 3px; }
    .liTopData { margin-top: 4px; }
    .liDataReserva { width: 120px; padding: 4px 0 4px 0px; clear: both;  }
    .liPerioAval{ margin-top: 4px; clear: both; }
    .liLimiteMedia,  .liLimiteMedia { margin-left: 7px;}
    #liQtdMateriasRecuperacao, #liQtdMateriasDependencia, #liQtdMaxMaterias { margin-left: 2px; margin-right: 0px; }
    .liHorarioTp1 { background-color: #F9F9FF; width: 80px; padding: 2px 0 2px 10px; clear: both; margin-top: 11px;  }
    .liHorarioTp { background-color: #F9F9FF; width: 80px; padding: 2px 0 2px 10px; clear: both; }
    .liPesqCEP { margin-top: 7px !important; margin-left: -3px; }   
    .liUnidade4 { margin-top: 10px; border-right: 1px solid #CCCCCC; padding-right: 10px; padding-left: 10px; height: 104px; }
    .liUnidade5 { margin-top: 20px; border-right: 1px solid #CCCCCC; padding-right: 10px; padding-left: 10px; clear: both; }
    .liUnidade6 { margin-top: 20px; padding-left: 10px; }
    .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:12px !important;} 
    .liAux { margin-left: -17px !important; margin-right: 5px; clear:none !important; display:inline; margin-top: 0px !important;}
    .liEspaco { clear: none !important; margin-left: 15px !important; margin-top: 0px !important; }
    
    /*--> CSS DADOS */
    #tabDadosCadas { height: 282px; padding: 10px 0 0 10px; width: 800px; }
    #tabQuemSomos, #tabNossaHisto, #tabPropoPedag, #tabFrequFunci, #tabBibliEscol, #tabPedagMatric, #tabMensaSMS, #tabContabil, #tabSecreEscol { height: 282px; padding: 6px 0 0 30px; width: 780px; }              
    .fldsInstituicaoIIE { padding: 5px 5px 0 9px; }    
    .imgEndIIE { margin: 14px 0 0 5px; }    
    .fldSituacaoIIE { padding: 5px 5px 0 9px; }
    .txtNomeIIE { width: 210px; }
    .txtTelefoneIIE { width: 78px; }
    .txtLogradouroIIE { width: 225px; }
    .txtComplementoIIE { width: 164px; }
    .campoNumericoEndIIE { width: 30px; }
    .ddlBairroIIE { width: 170px; }
    .ddlCidadeIIE { width: 195px; }
    .txtCEPIIE { width: 55px; }
    .txtEmailIIE, .txtWebSiteIIE { width: 248px; }
    .spaceIIE { clear: both; height: 15px; }    
    .fldDadosControleIIE { padding: 2px 5px 10px 9px; }
    .fldHorarioFuncionamentoIIE { padding: 7px 5px 0 9px; width: 190px; }    
    .txtNumControleIIE { width: 86px; }
    #tbLimiteApontamentoIIE{ border: none; margin-bottom: 7px;}
    #tbLimiteApontamentoIIE tr td{ padding: 1px;}
    #tbLimiteApontamentoIIE th label{ vertical-align: middle; margin-left: 5px;}
    #tbLimiteApontamentoIIE label{ vertical-align: middle;}
    #tbLimiteApontamentoIIE input{ margin-bottom: 0 !important; margin-left: 5px;}
    .txtHorLimiteIIE, .txtHorarioFuncManha, .txtHorarioFuncTarde, .txtHorarioFuncNoite { width: 30px; }
    .campoMoeda{ width: 30px; text-align:right;}
    .rblInforCadastro label { display: inline; font-size: 1.1em; }
    .rblInforCadastro { border-width: 0px; }    
    .rblInforControle label { display: inline; font-size: 1.1em; }
    .rblInforControle { border-width: 0px; } 
    .rblInforCadastro tr, .rblInforControle tr { height: 18px; }
    .divTabs 
    { 
    	height: 400px; 
    	padding: 0.2em;
        position: relative;
    }
    .txtInstitEnsino, .txtNomeFantaIIE { width: 240px; }
    .txtInstitEnsino { width: 300px; text-transform: uppercase; }
    .txtSiglaIUE { width: 85px; }
    .txtCNPJIUE { width: 100px; text-align: right; }
    .ddlAlterPrazoEntreSolic { width: 40px; }
    .txtQtdeDiasEntreSolic { margin-bottom: 0px !important; text-align: right; width: 18px; }
    .txtNumIniciSolicAuto { width: 55px; }
    .txtCabecalhoRelatorio { width: 330px; }
    .ddlMetodEnsino, .ddlFormaAvali { width: 102px; }
    .ddlPerioAval { width: 110px; }
    .txtDescrConce { width: 95px; padding-left: 3px; }
    .txtSiglaConce { width: 25px; padding-left: 3px; }
    .txtNotaIni, .txtNotaFim { width: 30px; text-align: right; }
    .chkRecuperEscol label { color: #336699; }
    .campoNumerico{ width: 30px;}
    .txtInscEstadualIIE { width: 100px; }
    .ddlCtrlHoraExtra { width: 70px; }
    .ddlTipoAplic { width: 40px; }
    #ControleImagem .liControleImagemComp .fakefile { width: 120px !important; }
    .ddlSitucaoIIE { width: 105px; }
    .btnPesqCEP { width: 13px; }
    .ddlUFIIE { width: 40px; }
    .ddlNucleoIUE { width: 115px; }
    .txtNumDocto { width: 90px; }
    .txtLatitude, .txtLongitude { width: 120px; }
    .rblInforControle label { display: inline; }
    .rblInforControle { border-width: 0px; }
    .txtObservacaoIUE { width: 200px; height: 63px; }
    .ddlSiglaUnidSecreEscol { width: 60px; }
    .ddlNomeSecreEscol { width: 250px; }
    .ddlQtdeSecretario { width: 35px; }
    .liNotaConceito input { margin-bottom: 2px; }
    .ddlTipoCtrlDescr, .txtTipoCtrlDescr { width: 110px; }
    .ddlPermiMultiFrequ { width: 45px; }
    .txtQtdeMaxSMS { width: 35px; text-align: right; }
    .txtCPFRespon { width: 82px; }
    .labelAux { margin-top: 16px; }
    .txtNomeRespon { width: 247px; }
    .txtNumContr { width: 100px; }
    
</style>
<script type="text/javascript">  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul style="width:1000px;margin-top: 15px;">
    <li style="float:left;width:160px;margin-top: 15px; padding-left: 15px; height: 380px; margin-right: -2px;">
        <ul>
            <li style="background-color: #4682B4; padding: 3px 0 3px 0; text-align: center;"> <span style="font-size:1.1em; font-family: arial; font-weight: bold; color: white;">MENU OPÇÕES</span> </li>
            <li style="background-color: #B0C4DE; padding: 3px 0 3px 2px; text-align: center; margin-bottom: 2px;"> <span style="text-transform:uppercase; font-size:1.1em;">Informações Cadastrais</span> </li>
            <li style="background-color: #F5FFFF; height: 114px; padding-top: 10px;">
                <asp:RadioButtonList ID="rblInforCadastro" ClientIDMode="Static" CssClass="rblInforCadastro" runat="server" RepeatDirection="Vertical" Width="190px">
                    <asp:ListItem Text="Dados Cadastrais" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Quem somos" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Nossa História" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Proposta Pedagógica" Value="4"></asp:ListItem>                    
                </asp:RadioButtonList>
            </li>
            <li style="background-color: #B0C4DE; padding: 3px 0 3px 2px; text-align: center; margin-bottom: 2px;"> <span style="text-transform:uppercase;font-size:1.1em;">Informações de Controle</span> </li>
            <li style="background-color: #F5FFFF; height: 235px; padding-top: 10px;">
                <asp:RadioButtonList ID="rblInforControle" ClientIDMode="Static" CssClass="rblInforControle" runat="server" RepeatDirection="Vertical" Width="190px">
                    <asp:ListItem Text="Frequência Funcional" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Pedagógico / Matrículas" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Secretaria Escolar" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Biblioteca Escolar" Value="8"></asp:ListItem>                    
                    <asp:ListItem Text="Contábil" Value="9"></asp:ListItem>                          
                    <asp:ListItem Text="Mensagens SMS" Value="10"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:button id="btnPostBack" runat="server" style="display: none"></asp:button>
            </li>            
        </ul>        
    </li>
    <li style="float:left; width:820px;">
        <div class="divTabs">            
            <div id="tabDadosCadas" clientidmode="Static" class="tabDadosCadas" runat="server">
                <ul class="ulDados">
                <li class="liFotosIIE">
                    <ul>
                    <li class="liFotoIIE">
                        <uc1:ControleImagem ID="imgOrgaoIIE" ImagemLargura="130" ImagemAltura="70" runat="server" />
                    </li>
                    </ul>
                </li>
        
                <li class="liInstituicaoIIE">
                    <ul>                                    
                        <li class="liClear">
                            <label for="txtNomeIIE" title="Nome Instituição" class="lblObrigatorio">Instituição</label>
                            <asp:TextBox ID="txtNomeIIE" ToolTip="Informe o Nome da Instituição" CssClass="txtInstitEnsino" MaxLength="80" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeIIE" 
                                ErrorMessage="Nome deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                
                        <li style="margin-left: 35px;">
                            <label for="txtSiglaIIE" title="Código de Identificação" class="lblObrigatorio">Cód. Identificação</label>
                            <asp:TextBox ID="txtSiglaIIE" ToolTip="Informe o Código de Identificação" CssClass="txtSiglaIUE" MaxLength="15" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiglaIIE" 
                                ErrorMessage="Código de Identificação deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                             
                        
                        <li class="liClear">
                            <label for="txtNomeFantaIIE" class="lblObrigatorio" title="Nome">Nome de Fantasia (Apelido)</label>
                            <asp:TextBox ID="txtNomeFantaIIE" style="margin-bottom: 0;"
                                ToolTip="Informe o Nome Fantasia da Instituição"
                                CssClass="txtNomeFantaIIE" MaxLength="80" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNomeFantaIIE" 
                                ErrorMessage="Nome Fantasia deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>

                        <li style="margin-left: 7px;">
                            <label for="ddlNucleoIUE" title="Núcleo">N&uacute;cleo / Regional</label>
                            <asp:DropDownList ID="ddlNucleoIUE" CssClass="ddlNucleoIUE" ToolTip="Selecione o Núcleo" runat="server">
                            </asp:DropDownList>
                        </li>      
                        
                        <li style="margin-left: 7px;">
                            <label for="txtNISIIE" title="Número do NIS">N° NIS</label>
                            <asp:TextBox ID="txtNISIIE" style="margin-bottom: 0px;"
                                ToolTip="Informe o Número do NIS"
                                CssClass="txtNumControle" runat="server"></asp:TextBox>
                        </li> 
                        
                        <li class="liClear" style="margin-top: 2px !important;">
                            <label for="txtCNPJIUE" title="CNPJ" class="lblObrigatorio">CNPJ</label>
                            <asp:TextBox ID="txtCNPJIUE" ToolTip="Informe o CNPJ" CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCNPJIUE" 
                                ErrorMessage="CNPJ deve ser informado" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>
                        
                        <li style="margin-left: 10px; margin-top: 2px !important;">
                            <label for="txtInscEstadualIIE" title="Número Inscrição Estadual">Nº Inscrição Estadual</label>
                            <asp:TextBox ID="txtInscEstadualIIE" MaxLength="20"
                                ToolTip="Informe a Inscrição Estadual"
                                CssClass="txtInscEstadualIIE" runat="server"></asp:TextBox>
                        </li>

                        <li style="margin-top: 2px !important; margin-left: 10px;">
                            <label for="txtNumContr" title="Número do Contrato">N° Contrato</label>
                            <asp:TextBox ID="txtNumContr" style="margin-bottom: 0px;"
                                ToolTip="Informe o Número do Contrato" MaxLength="20"
                                CssClass="txtNumContr" runat="server"></asp:TextBox>
                        </li>  
                        <li class="liEspaco">
                            <label for="txtDtIniContr" title="Período de Contrato">Período de Contrato</label>
                            <asp:TextBox ID="txtDtIniContr" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
                        </li>
                        <li class="liAux">
                            <label class="labelAux">até</label>
                        </li>
                        <li class="liPeriodoAte">
                            <asp:TextBox ID="txtDtFimContr" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
                        </li>                          
                    </ul>
                </li>    
                
                <li class="liClear" style="margin-top: 10px;">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Informações do Responsável</span></li>
                        <li class="liClear">
                            <label for="txtNomeRespon" title="Nome Responsável">Nome</label>
                            <asp:TextBox ID="txtNomeRespon" ToolTip="Informe o Nome do Responsável" CssClass="txtNomeRespon" MaxLength="80" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtCPFRespon" title="CPF do Responsável">CPF</label>
                            <asp:TextBox ID="txtCPFRespon" ToolTip="Informe o CPF do Responsável" CssClass="txtCPFRespon" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtDtNasctoRespon" title="Data de Nascimento do Responsável">Data Nascto</label>
                            <asp:TextBox ID="txtDtNasctoRespon" ToolTip="Informe a Data de Nascimento do Responsável" CssClass="campoData" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtTelFixoRespo" title="Telefone Fixo">Telefone Fixo</label>
                            <asp:TextBox ID="txtTelFixoRespo" ToolTip="Informe o número do Telefone Fixo" CssClass="txtTelefoneIIE" runat="server"></asp:TextBox>
                        </li>                
                        <li>
                            <label for="txtTelCeluRespo" title="Telefone Celular">Telefone Celular</label>
                            <asp:TextBox ID="txtTelCeluRespo" ToolTip="Informe o número do Telefone Celular" CssClass="txtTelefoneIIE" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtEmailRespo" title="E-mail Responsável">E-mail</label>
                            <asp:TextBox ID="txtEmailRespo" ToolTip="Informe o E-mail do Responsável" style="width: 180px;" MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </li>            
        
                <li class="liClear" style="border-right: 1px solid #CCCCCC; margin-top: 10px;">
                    <ul>                
                        <li style="margin-bottom: 9px; margin-top: 0px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Informações de Endereço</span></li>
                        <li class="liUnidade2IIE">
                            <ul>                        
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                <li>
                                    <label for="txtCEPIIE" title="CEP">CEP</label>
                                    <asp:TextBox ID="txtCEPIIE" ToolTip="Informe o CEP" CssClass="txtCEPIIE" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCEPIIE" 
                                        ErrorMessage="CEP deve ser informado" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li class="liPesqCEP">
                                    <asp:ImageButton ID="btnPesqCEP" runat="server" onclick="btnPesqCEP_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" class="btnPesqCEP"
                                        CausesValidation="false"/>
                                </li>
                                <li id="liLogradouroIIE" class="liLogradouroIIE">
                                    <label for="txtLogradouroIIE" title="Descrição do Endereço" class="lblObrigatorio">Descrição do Endere&ccedil;o</label>
                                    <asp:TextBox ID="txtLogradouroIIE" ToolTip="Informe a Descrição do Endereço" CssClass="txtLogradouroIIE" MaxLength="60" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLogradouroIIE" 
                                        ErrorMessage="Descrição do Endereço deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="txtNumeroIIE" title="Número">Número</label>
                                    <asp:TextBox ID="txtNumeroIIE" ToolTip="Informe o Número" CssClass="campoNumericoEndIIE" runat="server" MaxLength="6"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtComplementoIIE" title="Complemento">Complemento</label>
                                    <asp:TextBox ID="txtComplementoIIE" ToolTip="Informe o Complemento" CssClass="txtComplementoIIE" MaxLength="30" runat="server"></asp:TextBox>
                                </li>
                                <li id="liBairroIIE" class="liBairroIIE">
                                    <label for="ddlBairroIIE" class="lblObrigatorio" title="Bairro">Bairro</label>
                                    <asp:DropDownList ID="ddlBairroIIE" ToolTip="Selecione o Bairro" CssClass="ddlBairroIIE" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlBairroIIE" 
                                        ErrorMessage="Bairro deve ser informado" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li class="liClear">
                                    <label for="ddlCidadeIIE" class="lblObrigatorio" title="Cidade">Cidade</label>
                                    <asp:DropDownList ID="ddlCidadeIIE" ToolTip="Selecione a Cidade" CssClass="ddlCidadeIIE" OnSelectedIndexChanged="ddlCidadeIIE_SelectedIndexChanged" 
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCidadeIIE" 
                                        ErrorMessage="Cidade deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <li id="liUFIIE" class="liUFIIE">
                                    <label for="ddlUFIIE" class="lblObrigatorio" title="UF">UF</label>
                                    <asp:DropDownList ID="ddlUFIIE" ToolTip="Selecione a UF" CssClass="ddlUFIIE" OnSelectedIndexChanged="ddlUFIIE_SelectedIndexChanged"
                                        AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlUFIIE" 
                                        ErrorMessage="UF deve ser informada" Display="None">
                                    </asp:RequiredFieldValidator>
                                </li>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </ul>
                        </li>
                    </ul>
                </li>

                <li class="liUnidade4">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Georeferenciamento</span></li>
                        <li class="liClear">
                            <label for="txtLatitude" title="Latitude">Latitude</label>
                            <asp:TextBox ID="txtLatitude" 
                                ToolTip="Informe a Latitude" MaxLength="20"
                                CssClass="txtLatitude" runat="server"></asp:TextBox>
                        </li>                
                        <li class="liClear" style="margin-top: -7px;">
                            <label for="txtLongitude" title="Longitude">Longitude</label>
                            <asp:TextBox ID="txtLongitude" MaxLength="20"
                                ToolTip="Informe a Longitude"
                                CssClass="txtLongitude" runat="server"></asp:TextBox>
                        </li> 
                    </ul>
                </li>

                <li class="liContatoIIE">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Informações de Contato</span></li>                        
                        <li class="liClear">
                            <label for="txtTelefoneIIE" title="Telefone">Telefone</label>
                            <asp:TextBox ID="txtTelefoneIIE" ToolTip="Informe o número do Telefone" CssClass="txtTelefoneIIE" runat="server"></asp:TextBox>
                        </li>                
                        <li>
                            <label for="txtTelefone2IIE" title="Telefone 2">Telefone 2</label>
                            <asp:TextBox ID="txtTelefone2IIE" ToolTip="Informe o número do Telefone" CssClass="txtTelefoneIIE" runat="server"></asp:TextBox>
                        </li>        
                        <li>
                            <label for="txtFaxIIE" title="Fax">Fax</label>
                            <asp:TextBox ID="txtFaxIIE" ToolTip="Informe o número do Fax" CssClass="txtTelefoneIIE" runat="server"></asp:TextBox>
                        </li>           
                        <li class="liClear" style="margin-top: -7px;">
                            <label for="txtEmailIIE" title="E-mail">E-mail</label>
                            <asp:TextBox ID="txtEmailIIE" ToolTip="Informe o E-mail" CssClass="txtEmailIIE" MaxLength="60" runat="server"></asp:TextBox>
                        </li>                                     
                        <li class="liClear" style="margin-top: -7px;">
                            <label for="txtWebSiteIIE" title="Web Site">Web Site</label>
                            <asp:TextBox ID="txtWebSiteIIE" ToolTip="Informe o Web Site" CssClass="txtWebSiteIIE" MaxLength="60" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </li>                

                <li class="liUnidade5" style="padding-left: 0px; padding-right: 5px;">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Horário de Atividades</span></li>
                        <li class="liClear" style="border-right: 1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC; margin-right: 0px;">
                            <ul>
                                <li><span title="Turno 1">Turno 1</span></li>
                                <li class="liClear">                        
                                    <asp:TextBox ID="txtHoraIniTurno1" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Inicial do Turno 1"
                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                </li>
                                <li><span> às </span></li>
                                <li>
                                    <asp:TextBox ID="txtHoraFimTurno1" style="margin-bottom: 5px;"
                                    ToolTip="Informe o Horário Final do Turno 1"
                                    CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>    
                        <li style="border-bottom: 1px solid #CCCCCC; padding-left: 5px;">
                            <ul>
                                <li><span title="Turno 2">Turno 2</span></li>
                                <li class="liClear">
                                    <asp:TextBox ID="txtHoraIniTurno2" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Inical do Turno 2"
                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                </li>
                                <li><span> às </span></li>
                                <li>
                                    <asp:TextBox ID="txtHoraFimTurno2" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Final do Turno 2"
                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li class="liClear" style="border-right: 1px solid #CCCCCC;">
                            <ul>
                                <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                <li class="liClear">
                                    <asp:TextBox ID="txtHoraIniTurno3" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Inicial do Turno 3"
                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                </li>
                                <li><span> às </span></li>
                                <li>
                                    <asp:TextBox ID="txtHoraFimTurno3" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Final do Turno 3"
                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <ul>
                                <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                <li class="liClear">
                                    <asp:TextBox ID="txtHoraIniTurno4" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Inicial do Turno 4"
                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                </li>
                                <li><span> às </span></li>
                                <li>
                                    <asp:TextBox ID="txtHoraFimTurno4" style="margin-bottom: 5px;"
                                        ToolTip="Informe o Horário Final do Turno 4"
                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>

                <li class="liUnidade5" style="clear: none; padding-right: 5px;">
                    <ul>
                        <li style="margin-bottom: 4px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Observações</span></li>
                        <li class="liClear">
                        <ul>
                            <li>
                                <asp:TextBox ID="txtObservacaoIIE" runat="server"
                                        ToolTip="Informe a Observação" style="overflow-y: hidden;"
                                        CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 150);"
                                        TextMode="MultiLine"></asp:TextBox>
                            </li>
                        </ul>
                        </li>
                    </ul>
                </li>

                <li class="liUnidade5" style="clear: none;">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Constituição e Cadastro</span></li>
                        <li class="liClear">
                            <label for="txtDataConstituicao" title="Data Cadastro">Data de Constituição</label>
                            <asp:TextBox ID="txtDataConstituicao" 
                                ToolTip="Informe a Data de Constituição"
                                CssClass="campoData" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: 0px;">
                            <label for="txtNumDocto" title="Longitude">Nº do Documento</label>
                            <asp:TextBox ID="txtNumDocto" MaxLength="20"
                                ToolTip="Informe o Número do Documento"
                                CssClass="txtNumDocto" runat="server"></asp:TextBox>
                        </li> 
                        <li class="liClear" style="margin-top: -4px;">
                            <label for="txtDtCadastroIIE" title="Data Cadastro">Data de Cadastro</label>
                            <asp:TextBox ID="txtDtCadastroIIE" style="margin-bottom: 5px;"
                                ToolTip="Informe a Data de Cadastro"
                                CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: -4px;">
                            <label for="ddlUsoLogo" title="Uso da Logomarca">Uso da Logomarca</label>
                            <asp:DropDownList ID="ddlUsoLogo" CssClass="ddlSitucaoIIE" ToolTip="Selecione o Uso da Logomarca" runat="server">
                                <asp:ListItem Value="I">Por Instituição</asp:ListItem>
                                <asp:ListItem Value="U">Por Unidade</asp:ListItem>                                
                            </asp:DropDownList>
                        </li>
                    </ul>
                </li>

                <li class="liUnidade6">
                    <ul>
                        <li style="margin-bottom: 2px;"><span style="text-transform:uppercase;color:#87CEFA;font-size:1.0em;">Situação</span></li>  
                        <li class="liClear">
                            <label for="txtDtStatusIIE" title="Data Situação">Data da Situação</label>
                            <asp:TextBox ID="txtDtStatusIIE" Enabled="false" 
                                ToolTip="Informe a Data Situação"
                                CssClass="campoData" runat="server"></asp:TextBox>
                        </li>               
                        <li class="liClear" style="margin-top: -4px;">
                            <label for="ddlSitucaoIIE" class="lblObrigatorio" title="Status">Situação da Unidade</label>
                            <asp:DropDownList ID="ddlSitucaoIIE" CssClass="ddlSitucaoIIE" ToolTip="Selecione a Situação" runat="server">
                                <asp:ListItem Value="A">Ativo</asp:ListItem>
                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSitucaoIIE" 
                                ErrorMessage="Situação da Unidade deve ser informada" Display="None">
                            </asp:RequiredFieldValidator>
                        </li>                                
                    </ul>
                </li>                 
            </ul>
            </div>
            <div id="tabQuemSomos" class="tabQuemSomos" style="display: none;">
                <ul id="ulDados2" class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoQS" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoQS" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>  
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoQS" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoQS" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJQS" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJQS" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Quem somos?</span></li>                   

                    <li class="liClear" style="margin-top: 2px; margin-left: 322px; margin-bottom: 10px;">
                        <label for="ddlTipoCtrlDescr" style="float:left;" title="Tipo de Controle do Quem Somos">Por</label>
                        <asp:DropDownList ID="ddlTipoCtrlDescr" ToolTip="Selecione o Tipo de Controle do Quem Somos" style="margin-left: 7px;"
                            CssClass="ddlTipoCtrlDescr" runat="server">
                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                        </asp:DropDownList>
                    </li> 

                    <li class="liClear" style="margin-bottom: 5px;"><span style="color:#87CEFA; font-size:1.1em;">Utilize o editor de texto abaixo para descrever sobre a Unidade Escolar, sua missão,
                    características, equipes administrativas e de profissionais de educação, etc...</span></li>

                    <li> 
                        <dx:ASPxHtmlEditor ID="txtQuemSomos" Height="300px" Width="745px" runat="server" Theme="Office2010Blue"
                        ClientInstanceName="txtQuemSomos">
                            <Settings AllowHtmlView="False" AllowPreview="False" />
                            <SettingsResize MinHeight="300" MinWidth="745" />
                        </dx:ASPxHtmlEditor>
                    </li>                                           
                </ul>
            </div>
            <div id="tabNossaHisto" class="tabNossaHisto" style="display: none;">
                <ul id="ul5" class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoNH" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoNH" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>   
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoNH" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoNH" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJNH" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJNH" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Nossa história?</span></li>

                    <li class="liClear" style="margin-top: 2px; margin-left: 322px;">
                        <label for="txtTipoCtrlNossaHisto" style="float:left;" title="Tipo de Controle do Nossa História">Por</label>
                        <asp:TextBox ID="txtTipoCtrlNossaHisto" Enabled="false" ClientIDMode="Static" style="margin-left: 7px; padding-left: 3px;"
                            ToolTip="Tipo de Controle do Nossa História"
                            CssClass="txtTipoCtrlDescr" runat="server"></asp:TextBox>
                    </li>

                    <li class="liClear" style="margin-bottom: 5px;"><span style="color:#87CEFA; font-size:1.1em;">Utilize o editor de texto abaixo para descrever sobre a história da Unidade Escolar,
                    desde sua criação contemplando fases e fatos importantes a repeito da mesma, dentre outras informações...</span></li>

                    <li> 
                        <dx:ASPxHtmlEditor ID="txtNossaHisto" Height="300px" Width="745px" runat="server" Theme="Office2010Blue"
                        ClientInstanceName="txtNossaHisto">
                            <Settings AllowHtmlView="False" AllowPreview="False" />
                            <SettingsResize MinHeight="300" MinWidth="745" />
                        </dx:ASPxHtmlEditor>
                    </li>                       
                </ul>
            </div>
            <div id="tabPropoPedag" class="tabPropoPedag" style="display: none;">
                <ul id="ul6" class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoPP" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoPP" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>   
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoNH" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoPP" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJNH" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJPP" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Proposta pedagógica?</span></li>

                    <li class="liClear" style="margin-top: 2px; margin-left: 322px;">
                        <label for="txtTipoCtrlPropoPedag" style="float:left;" title="Tipo de Controle do Proposta Pedagógica">Por</label>
                        <asp:TextBox ID="txtTipoCtrlPropoPedag" Enabled="false" ClientIDMode="Static" style="margin-left: 7px; padding-left: 3px;"
                            ToolTip="Tipo de Controle do Proposta Pedagógica"
                            CssClass="txtTipoCtrlDescr" runat="server"></asp:TextBox>
                    </li>

                    <li class="liClear" style="margin-bottom: 18px;text-align:center;margin-left: 30px;"><span style="color:#87CEFA; font-size:1.1em;">Utilize o editor de texto abaixo para descrever sobre a Proposta Pedagógica da
                    Instituição de Ensino e/ou Unidade Escolar.</span></li>

                    <li> 
                        <dx:ASPxHtmlEditor ID="txtPropoPedag" Height="300px" Width="745px" runat="server" Theme="Office2010Blue"
                        ClientInstanceName="txtPropoPedag">
                            <Settings AllowHtmlView="False" AllowPreview="False" />
                            <SettingsResize MinHeight="300" MinWidth="745" />
                        </dx:ASPxHtmlEditor>
                    </li>                       
                </ul>
            </div>
            <div id="tabFrequFunci" class="tabFrequFunci" style="display: none;">
                <ul id="ul9" class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoFF" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoFF" ClientIDMode="Static" ToolTip="Instituição de Ensino" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li> 
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoFF" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoFF" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJFF" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJFF" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#3366FF; font-size:1.3em;font-weight:bold;">Controle de Frequência Funcional</span></li>            

                    <li class="liClear" style="margin-top: 2px; margin-left: 325px;">
                        <label for="txtTipoCtrlFrequ" style="float:left;" title="Tipo de Controle de Frequência">Por</label>                        
                        <asp:TextBox ID="txtTipoCtrlFrequ" Enabled="false" ClientIDMode="Static" style="margin-left: 7px; padding-left: 3px;"
                            ToolTip="Tipo de Controle de Frequência"
                            CssClass="ddlTipoControleFuncIIE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="margin-top: 2px; margin-left: 170px;">
                        <label style="color: #BEBEBE;">Os Horários são cadastrados em funcionalidade específica - Cadastramento de Horário Funcional</label>               
                    </li>

                    <li style="margin-left: 109px;">            
                        <ul>
                            <li class="liClear" style="margin-top: 11px;width: 565px; text-align: center; background-color:#F9F9FF;padding: 2px 0;"><span style="font-size:1.3em;text-transform:uppercase;font-weight: bold;">Quadro de Horários Funcionais</span></li>
                            <li style="margin-top: 5px;text-align: center; background-color:#F9F9FF;padding: 2px 0; clear:both; margin-left: 143px;width: 74px;margin-bottom: 2px;"><span style="font-size:1.3em;text-transform:uppercase;">1º Turno</span></li>
                            <li style="margin-top: 5px;margin-left: 10px;text-align: center; background-color:#DDFFDD;padding: 2px 0;margin-bottom: 2px;width: 74px;"><span style="font-size:1.3em;text-transform:uppercase;">Intervalo</span></li>
                            <li style="margin-top: 5px;text-align: center; background-color:#F9F9FF;padding: 2px 0; margin-left: 10px;width: 70px;margin-bottom: 2px;width: 74px;"><span style="font-size:1.3em;text-transform:uppercase;">2º Turno</span></li>
                            <li style="margin-top: 5px;text-align: center; background-color:#FFEFDF;padding: 2px 0; margin-left: 50px;margin-bottom: 2px;width: 74px;"><span style="font-size:1.3em;text-transform:uppercase;">Extra</span></li>

                            <li class="liHorarioTp1"><span title="Horário TP 1" style="font-size: 1.2em;">Horário TP 1</span></li>
                            <li style="margin-left: 10px;">
                                <label>Limite</label>
                                <asp:TextBox ID="txtLimiteEntHTP1" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 2px;">
                                <label>Entrada</label>
                                <asp:TextBox ID="txtTurno1EntHTP1" Enabled="false" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <label>Saída</label>
                                <asp:TextBox ID="txtTurno1SaiHTP1" Enabled="false" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <label>Entrada</label>
                                <asp:TextBox ID="txtInterEntHTP1" Enabled="false" ToolTip="Horário entrada do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <label>Saída</label>
                                <asp:TextBox ID="txtInterSaiHTP1" Enabled="false" ToolTip="Horário saída intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <label>Entrada</label>
                                <asp:TextBox ID="txtTurno2EntHTP1" Enabled="false" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <label>Saída</label>
                                <asp:TextBox ID="txtTurno2SaiHTP1" Enabled="false" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <label>Limite</label>
                                <asp:TextBox ID="txtLimiteSaiHTP1" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <label>Entrada</label>
                                <asp:TextBox ID="txtExtraEntHTP1" Enabled="false" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <label>Saída</label>
                                <asp:TextBox ID="txtExtraSaiHTP1" Enabled="false" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <label>Limite</label>
                                <asp:TextBox ID="txtLimiteExtraSaiHTP1" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>

                            <li class="liHorarioTp" style="margin-top: -1px;"><span title="Horário TP 2" style="font-size: 1.2em;">Horário TP 2</span></li>
                            <li style="margin-left: 10px;">
                                <asp:TextBox ID="txtLimiteEntHTP2" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 2px;">
                                <asp:TextBox ID="txtTurno1EntHTP2" Enabled="false" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno1SaiHTP2" Enabled="false" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtInterEntHTP2" Enabled="false" ToolTip="Horário entrada intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtInterSaiHTP2" Enabled="false" ToolTip="Horário saída intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtTurno2EntHTP2" Enabled="false" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno2SaiHTP2" Enabled="false" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteSaiHTP2" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtExtraEntHTP2" Enabled="false" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtExtraSaiHTP2" Enabled="false" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteExtraSaiHTP2" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>

                            <li class="liHorarioTp"><span title="Horário TP 3" style="font-size: 1.2em;">Horário TP 3</span></li>
                            <li style="margin-left: 10px;">
                                <asp:TextBox ID="txtLimiteEntHTP3" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 2px;">
                                <asp:TextBox ID="txtTurno1EntHTP3" Enabled="false" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno1SaiHTP3" Enabled="false" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtInterEntHTP3" Enabled="false" ToolTip="Horário entrada do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtInterSaiHTP3" Enabled="false" ToolTip="Horário saída do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtTurno2EntHTP3" Enabled="false" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno2SaiHTP3" Enabled="false" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteSaiHTP3" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtExtraEntHTP3" Enabled="false" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtExtraSaiHTP3" Enabled="false" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteExtraSaiHTP3" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>

                            <li class="liHorarioTp"><span title="Horário TP 4" style="font-size: 1.2em;">Horário TP 4</span></li>
                            <li style="margin-left: 10px;">
                                <asp:TextBox ID="txtLimiteEntHTP4" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 2px;">
                                <asp:TextBox ID="txtTurno1EntHTP4" Enabled="false" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno1SaiHTP4" Enabled="false" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtInterEntHTP4" Enabled="false" ToolTip="Horário entrada do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtInterSaiHTP4" Enabled="false" ToolTip="Horário saída do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtTurno2EntHTP4" Enabled="false" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno2SaiHTP4" Enabled="false" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteSaiHTP4" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtExtraEntHTP4" Enabled="false" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtExtraSaiHTP4" Enabled="false" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteExtraSaiHTP4" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>

                            <li class="liHorarioTp"><span title="Horário TP 5" style="font-size: 1.2em;">Horário TP 5</span></li>
                            <li style="margin-left: 10px;">
                                <asp:TextBox ID="txtLimiteEntHTP5" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite entrada" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 2px;">
                                <asp:TextBox ID="txtTurno1EntHTP5" Enabled="false" ToolTip="Horário entrada do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno1SaiHTP5" Enabled="false" ToolTip="Horário saída do turno 1" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtInterEntHTP5" Enabled="false" ToolTip="Horário entrada do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtInterSaiHTP5" Enabled="false" ToolTip="Horário saída do intervalo" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtTurno2EntHTP5" Enabled="false" ToolTip="Horário entrada do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtTurno2SaiHTP5" Enabled="false" ToolTip="Horário saída do turno 2" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteSaiHTP5" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 12px;">
                                <asp:TextBox ID="txtExtraEntHTP5" Enabled="false" ToolTip="Horário entrada extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtExtraSaiHTP5" Enabled="false" ToolTip="Horário saída extra" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>
                            <li style="margin-left: 3px;">
                                <asp:TextBox ID="txtLimiteExtraSaiHTP5" Enabled="false" style="background-color:#FFFFB7; color: #FF0000;" ToolTip="Horário limite saída" CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                            </li>

                            <li class="liClear" style="margin-top: 10px;">
                                <label for="txtCtrlHoraExtra" title="Controle de Hora Extra" style="float: left;">Controle de Hora Extra</label>
                                <asp:TextBox ID="txtCtrlHoraExtra" Enabled="false" style="margin-left: 5px; padding-left: 3px;"
                                ToolTip="Controle de Hora Extra" Text="Com Controle"
                                CssClass="ddlCtrlHoraExtra" runat="server"></asp:TextBox>
                            </li> 

                            <li style="margin-top: 10px; margin-left: 10px;">
                                <label for="txtCtrlHoraExtra" title="Permite Multifrequência?" style="float: left;">Permite Multifrequência?</label>
                                <asp:DropDownList ID="ddlPermiMultiFrequ" ToolTip="Permite Multifrequência?" style="margin-left: 5px;"
                                    CssClass="ddlPermiMultiFrequ" runat="server">
                                    <asp:ListItem Value="S" Selected="true">Sim</asp:ListItem>
                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div id="tabPedagMatric" class="tabPedagMatric" style="display: none;">
                <ul id="ul8" class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoPM" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoPM" ClientIDMode="Static" ToolTip="Instituição de Ensino" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li> 
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoPM" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoPM" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJQS" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJPM" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="margin-bottom: 27px;width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Controle Pedagógico / Matrículas</span></li>

                    <li>
                        <ul>
                            <li>
                                <ul>
                                    <li class="liPedagMatric"><span style="font-size: 1.0em;text-transform:uppercase;font-weight: bold;">Parâmetros de Metodologia</span></li>                
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                    <li class="liControleMetodologia">
                                        <label title="Controle de Metodologia">Controle de Metodologia</label>
                                        <asp:DropDownList ID="ddlTipoCtrleMetod" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle de Biblioteca" style="width: 102px;"
                                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrleMetod_SelectedIndexChanged">
                                            <asp:ListItem Value="I" Selected="true">Por Instituição</asp:ListItem>
                                            <asp:ListItem Value="U">Por Unidade</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liMetodEnsino">
                                        <label title="Metodologia de Ensino">Metodologia de Ensino</label>
                                        <asp:DropDownList ID="ddlMetodEnsino" style="margin-left: 9px;" ToolTip="Informe a Metodologia de Ensino<" CssClass="ddlMetodEnsino" runat="server">
                                            <asp:ListItem Value="S">Seriada</asp:ListItem>
                                            <asp:ListItem Value="P">Progressiva</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                    <ContentTemplate>
                                    <li class="liMetodEnsino">
                                        <label title="Formato de Avaliação">Formato de Avaliação</label>                  
                                        <asp:DropDownList ID="ddlFormaAvali" style="margin-left: 13px;" ToolTip="Informe a Forma de Avaliação" CssClass="ddlFormaAvali" runat="server"
                                        OnSelectedIndexChanged="ddlFormaAvali_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="N">Nota</asp:ListItem>
                                            <asp:ListItem Value="C">Conceito</asp:ListItem>
                                        </asp:DropDownList>                                  
                                    </li>                                    
                                    <li class="liEquivNotaConce"><span style="font-size: 1.0em;text-transform:uppercase;font-weight: bold;">Equivalência de Nota/Conceito</span></li>
                                    <li style="background-color:#FFFFCC; width: 220px; clear: both;"><span style="font-size: 1.0em; margin-left: 3px;">Conceito (Nome e Sigla)</span><span style="font-size: 1.0em; margin-left: 22px;">Intervalo de Notas</span></li>
                                    <li class="liNotaConceito" style="margin-top: 4px; ">
                                        <asp:TextBox ID="txtDescrConce1" ToolTip="Descrição do Conceito" CssClass="txtDescrConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSiglaConce1" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtNotaIni1" ToolTip="Nota Inicial" CssClass="txtNotaIni" style="margin-left: 7px;" runat="server" Enabled="false"></asp:TextBox>
                                        <span style="font-size: 1.0em;">a</span>
                                        <asp:TextBox ID="txtNotaFim1" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li class="liNotaConceito">
                                        <asp:TextBox ID="txtDescrConce2" ToolTip="Descrição do Conceito" CssClass="txtDescrConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSiglaConce2" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtNotaIni2" ToolTip="Nota Inicial" CssClass="txtNotaIni" style="margin-left: 7px;" runat="server" Enabled="false"></asp:TextBox>
                                        <span style="font-size: 1.0em;">a</span>
                                        <asp:TextBox ID="txtNotaFim2" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li class="liNotaConceito">
                                        <asp:TextBox ID="txtDescrConce3" ToolTip="Descrição do Conceito" CssClass="txtDescrConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSiglaConce3" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtNotaIni3" ToolTip="Nota Inicial" CssClass="txtNotaIni" style="margin-left: 7px;" runat="server" Enabled="false"></asp:TextBox>
                                        <span style="font-size: 1.0em;">a</span>
                                        <asp:TextBox ID="txtNotaFim3" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li class="liNotaConceito">
                                        <asp:TextBox ID="txtDescrConce4" ToolTip="Descrição do Conceito" CssClass="txtDescrConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSiglaConce4" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtNotaIni4" ToolTip="Nota Inicial" CssClass="txtNotaIni" style="margin-left: 7px;" runat="server" Enabled="false"></asp:TextBox>
                                        <span style="font-size: 1.0em;">a</span>
                                        <asp:TextBox ID="txtNotaFim4" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li class="liNotaConceito">
                                        <asp:TextBox ID="txtDescrConce5" ToolTip="Descrição do Conceito" CssClass="txtDescrConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtSiglaConce5" ToolTip="Sigla do Conceito" CssClass="txtSiglaConce" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBox ID="txtNotaIni5" ToolTip="Nota Inicial" CssClass="txtNotaIni" style="margin-left: 7px;" runat="server" Enabled="false"></asp:TextBox>
                                        <span style="font-size: 1.0em;">a</span>
                                        <asp:TextBox ID="txtNotaFim5" ToolTip="Nota Final" CssClass="txtNotaFim" runat="server" Enabled="false"></asp:TextBox>
                                    </li>    
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>                          
                                </ul>
                            </li>
                            <li style="padding-left: 10px; padding-right: 5px; border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC;">
                                <ul>
                                    <li class="liPedagMatric" style="width: 190px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Parâmetros de Avaliação</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                    <li class="liContrAval">
                                        <label title="Controle por">Controle por</label>
                                        <asp:DropDownList ID="ddlTipoCtrleAval" AutoPostBack="true" style="margin-left: 5px; width: 110px;" ToolTip="Selecione o Tipo de Controle de Biblioteca"
                                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrleAval_SelectedIndexChanged">
                                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liPerioAval">
                                        <label for="ddlPerioAval" title="Periodicidade da Avaliação">Periodicidade</label>      
                                        <asp:DropDownList ID="ddlPerioAval" ToolTip="Informe a Periodicidade da Avaliação" style="margin-left: 3px;" CssClass="ddlPerioAval" runat="server">
                                            <asp:ListItem Value="M">Mensal</asp:ListItem>
                                            <asp:ListItem Value="B">Bimestral</asp:ListItem>
                                            <asp:ListItem Value="T">Trimestral</asp:ListItem>
                                            <asp:ListItem Value="S">Semestral</asp:ListItem>
                                            <asp:ListItem Value="A">Anual</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liEquivNotaConce" style="width: 190px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Controle de Médias</span></li>
                                    <li style="background-color:#FFFFCC; clear: both;width: 190px;"><span style="font-size: 1.0em; margin-left: 3px;">Descrição</span><span style="font-size: 1.0em;margin-left:107px;">Média</span></li>
                                    <li class="liAprovGeral" style="margin-top: 5px;">
                                        <label title="Média de Aprovação Geral">Aprovação Geral</label>
                                        <asp:TextBox ID="txtMediaAprovGeral" ToolTip="Informe a Média de Aprovação Geral" CssClass="campoMoeda" style="margin-left: 73px; margin-bottom: 2px;" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="liAprovDireta">
                                        <label title="Média para Aprovação Direta">Aprovação Direta</label>
                                        <asp:TextBox ID="txtMediaAprovDireta" ToolTip="Informe a Média para Aprovação Direta" CssClass="campoMoeda" style="margin-left: 70px; margin-bottom: 2px;" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="liAprovDireta">
                                        <label title="Média Prova Final">Prova Final</label>
                                        <asp:TextBox ID="txtMediaProvaFinal" ToolTip="Informe a Média Prova Final" CssClass="campoMoeda" style="margin-left: 98px;" runat="server"></asp:TextBox>
                                    </li>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                    <li class="liEquivNotaConce" style="width: 190px; margin-top: 0px; margin-bottom: 3px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Controle de Aprovação</span></li>                
                                    <li class="liRecuperacao liClear">
                                        <asp:CheckBox ID="chkRecuperEscol" CssClass="chkRecuperEscol" ToolTip="Informe se poderá haver Recuperação" runat="server" 
                                            Text="Recuperação Escolar" OnCheckedChanged="chkRecuperEscol_CheckedChanged" AutoPostBack="true"/>
                                    </li>
                                    <li class="liMediaRecuperacao">
                                        <label title="Média de Aprovação na Recuperação" style="float: left;">Média</label>
                                        <asp:TextBox ID="txtMediaRecuperEscol" ToolTip="Informe a Média de Aprovação na Recuperação" Enabled="false" style="margin-bottom: 2px;" CssClass="campoMoeda" runat="server"></asp:TextBox>
                                    </li>
                                    <li id="liQtdMateriasRecuperacao">
                                        <label for="txtQtdeMaterRecuper" title="Quantidade Máxima de Matérias na Recuperação">Máx. Matérias</label>
                                        <asp:TextBox ID="txtQtdeMaterRecuper" ToolTip="Informe a Quantidade Máxima de Matérias na Recuperação" CssClass="campoNumerico" runat="server" style="margin-bottom: 2px;" Enabled="false"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                    <li id="liDependencia" class="liClear">
                                        <asp:CheckBox ID="chkDepenEscol" CssClass="chkRecuperEscol" ToolTip="Informe se poderá haver Dependência" runat="server" 
                                            Text="Dependência Escolar" OnCheckedChanged="chkDepenEscol_CheckedChanged" AutoPostBack="true"/>
                                    </li>
                                    <li class="liMediaRecuperacao">
                                        <label title="Média de Dependência Escolar" style="float: left;">Média</label>
                                        <asp:TextBox ID="txtMediaDepenEscol" ToolTip="Informe a Média de Dependência Escolar" CssClass="campoMoeda" runat="server" style="margin-bottom: 2px;" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li id="liQtdMateriasDependencia">
                                        <label for="txtQtdMaterDepenEscol" title="Quantidade Máxima de Matérias na Dependência">Máx. Matérias</label>
                                        <asp:TextBox ID="txtQtdMaterDepenEscol" ToolTip="informe a Quantidade Máxima de Matérias na Dependência" CssClass="campoNumerico" style="margin-bottom: 2px;" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                    <li id="liConselho" class="liClear">
                                        <asp:CheckBox ID="chkConselho" CssClass="chkRecuperEscol" ToolTip="Informe se poderá haver Conselho de Classe" runat="server" 
                                            Text="Conselho de Classe" OnCheckedChanged="chkConselho_CheckedChanged" AutoPostBack="true"/>
                                    </li>
                                    <li class="liLimiteMedia liClear" style="margin-left: 25px;">
                                        <label for="txtLimitMediaConseEscol" title="Limite Inferior de Média para Conselho de Classe">Média</label>
                                        <asp:TextBox ID="txtLimitMediaConseEscol" ToolTip="Informe o Limite Inferior de Média para Conselho de Classe" CssClass="campoMoeda" runat="server" Enabled="false"></asp:TextBox>
                                    </li>
                                    <li id="liQtdMaxMaterias">
                                        <label for="txtQtdMaxMaterConse" title="Quantidade Máxima de Matérias para Conselho de Classe">Máx. Matérias</label>
                                        <asp:TextBox ID="txtQtdMaxMaterConse" ToolTip="Informe a Quantidade Máxima de Matérias para Conselho de Classe" CssClass="campoNumerico" runat="server" Enabled="false"></asp:TextBox>
                                    </li>                
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>
                
                            <li style="width: 305px;margin-left: 5px;">
                                <ul>
                                    <li class="liPedagMatric" style="text-align:left; padding-left: 4px; width: 295px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Datas de Controle</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                    <ContentTemplate>
                                    <li class="liContrAval">
                                        <label for="ddlTipoCtrleDatas" title="Controle por">Controle por</label>
                                        <asp:DropDownList ID="ddlTipoCtrleDatas" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle de Biblioteca" style="width: 110px; margin-left: 64px;"
                                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrleDatas_SelectedIndexChanged">
                                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liDataReserva" style="margin-top: 4px;"><span title="Data de Reserva de Matrícula" style="font-size: 1.0em;">Reserva de Vagas</span></li>
                                    <li class="liTopData" style="margin-top: 8px;">
                                        <asp:TextBox ID="txtReservaMatriculaDtInicioIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Reserva de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator31" ControlToValidate="txtReservaMatriculaDtInicioIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Reserva de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataReservaMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData" style="margin-top: 8px;"><span> até </span></li>
                                    <li class="liTopData" style="margin-top: 8px;">
                                        <asp:TextBox ID="txtReservaMatriculaDtFimIUE"  style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Reserva de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator39" ControlToValidate="txtReservaMatriculaDtFimIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Reserva de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataReservaMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>

                                    <li class="liDataReserva"><span title="Data de Renovação de Matrículas" style="font-size: 1.0em;">Renovação de Matrículas</span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtRematriculaInicioIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Renovação de Matrículas"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtRematriculaInicioIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Renovação de Matrículas" 
                                            Display="None" CssClass="validatorField"
                                            EnableClientScript="false" OnServerValidate="cvDataRematricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtRematriculaFimIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Rematrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator38" ControlToValidate="txtRematriculaFimIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Renovação de Matrículas" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataRematricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>            

                                    <li class="liDataReserva"><span title="Data de Efetivação de Matrícula" style="font-size: 1.0em;">Matrículas Novas</span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtMatriculaInicioIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informa a Data Incial de Efetivação de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator3" ControlToValidate="txtMatriculaInicioIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtMatriculaFimIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informa a Data Final de Efetivação de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator7" ControlToValidate="txtMatriculaFimIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>

                                    <li class="liDataReserva"><span title="Data de Remanejamento" style="font-size: 1.0em;">Remanejamento de Alunos</span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtDataRemanAlunoIni" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Renovação de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator8" ControlToValidate="txtDataRemanAlunoIni" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Remanejamento de Alunos" 
                                            Display="None" CssClass="validatorField"
                                            EnableClientScript="false" OnServerValidate="cvDataRemanAluno_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtDataRemanAlunoFim" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Rematrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator23" ControlToValidate="txtDataRemanAlunoFim" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Remanejamento de Alunos" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataRemanAluno_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>

                                    <li class="liDataReserva"><span title="Data de Transferências Internas" style="font-size: 1.0em;">Transferências Internas</span></li>                    
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtDtInicioTransInter" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Transferências Internas"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator24" ControlToValidate="txtDtInicioTransInter" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Transferências Internas" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataTransInter_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtDtFimTransInter" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Transferências Internas"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator25" ControlToValidate="txtDtFimTransInter" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Transferências Internas" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataTransInter_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>                               
                    
                                    <li class="liDataReserva"><span title="Data de Trancamento de Matrícula" style="font-size: 1.0em;">Trancamento de Matrículas</span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtTrancamentoMatriculaInicioIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Trancamento de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator26" ControlToValidate="txtTrancamentoMatriculaInicioIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Trancamento de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataTrancamentoMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtTrancamentoMatriculaFimIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Trancamento de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator27" ControlToValidate="txtTrancamentoMatriculaFimIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Trancamento de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataTrancamentoMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                    
                                    <li class="liDataReserva"><span title="Data de Alteração de Matrícula" style="font-size: 1.0em;">Altera&ccedil;&atilde;o de Matrículas</span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtAlteracaoMatriculaInicioIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Inicial de Alteração de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator28" ControlToValidate="txtAlteracaoMatriculaInicioIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Alteração de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataAlteracaoMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    <li class="liTopData"><span> até </span></li>
                                    <li class="liTopData">
                                        <asp:TextBox ID="txtAlteracaoMatriculaFimIUE" style="margin-bottom: 2px;"
                                            ToolTip="Informe a Data Final de Alteração de Matrícula"
                                            CssClass="campoData" runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="CustomValidator29" ControlToValidate="txtAlteracaoMatriculaFimIUE" runat="server" 
                                            ErrorMessage="É necessário informar a Data de Início e Fim de Alteração de Matrícula" 
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvDataAlteracaoMatricula_ServerValidate">
                                        </asp:CustomValidator>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>                 
                            </li> 

                            <li style="width: 305px;margin-left: 5px; margin-top: 10px;">
                                <ul>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    <li class="liPedagMatric" style="text-align:left; padding-left: 4px; width: 295px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Controle Operacional</span></li>
                                    <li class="liContrAval">
                                        <label for="txtTipoContrOpera" title="Controle por">Controle por</label>
                                        <asp:DropDownList ID="ddlTipoControleTpEnsinoIIE" style="margin-left: 72px; width: 110px;"
                                            ToolTip="Selecione o Tipo de Controle - Tipo de Ensino" AutoPostBack="true"
                                            CssClass="ddlTipoControleTpEnsinoIIE" runat="server" OnSelectedIndexChanged="ddlTipoControleTpEnsinoIIE_SelectedIndexChanged">
                                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                
                                    <li class="liMetodEnsino" style="width:260px; margin-top: 5px;">
                                        <label title="Gerar N° de Rede (NIRE) Automático">Gerar N° de Rede (NIRE) Autom&aacute;tico</label>
                                        <asp:DropDownList ID="ddlGerarNisIIE" style="margin-left: 33px;"
                                            ToolTip="Informe se o NIRE será gerado automaticamente"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N">N&atilde;o</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                    
                                    <li class="liMetodEnsino" style="width:260px;">
                                        <label title="Gerar N° de Matrícula de Aluno Automático">Gerar N° de Matr&iacute;cula de Aluno Autom&aacute;tico</label>
                                        <asp:DropDownList ID="ddlGerarMatriculaIIE" style="margin-left: 5px;"
                                            ToolTip="Informe se o N° de Matrícula de Aluno será gerado automaticamente"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N">N&atilde;o</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>
                        </ul>    
                    </li>                    
                </ul>
            </div>
            <div id="tabSecreEscol" class="tabSecreEscol" clientidmode="Static" style="display: none;" runat="server">
                <ul class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoSE" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoSE" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>   
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoSE" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoSE" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJSE" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJSE" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Controle Secretaria Escolar</span></li>            
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">                     
                    <ContentTemplate>
                    <li class="liClear" style="margin-top: 2px; margin-left: 323px; margin-bottom: 10px;">
                        <label for="ddlTipoCtrlSecreEscol" style="float:left;" title="Tipo de Secretaria Escolar">Por</label>
                        <asp:DropDownList ID="ddlTipoCtrlSecreEscol" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle de Biblioteca" style="margin-left: 7px; width: 110px;"
                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrlSecreEscol_SelectedIndexChanged">
                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                        </asp:DropDownList>
                    </li> 

                    <li class="liClear">
                        <ul>
                            <li style="border-right: 1px solid #CCCCCC;width: 370px;height: 295px;">
                                <ul>
                                    <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; height: 105px;">  
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Usuários de Secretaria</span></li>                                                   
                                            <li class="liRecuperacao liClear" style="margin-top: 7px; margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarFunc" ToolTip="Informe se existirá usuário Funcionário" runat="server" 
                                                    Text="Funcionários"/>
                                            </li>   
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarProf" ToolTip="Informe se existirá usuário Professor" runat="server" 
                                                    Text="Professores"/>
                                            </li>   
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarAluno" ToolTip="Informe se existirá usuário Aluno" runat="server" 
                                                 OnCheckedChanged="chkUsuarAluno_CheckedChanged" AutoPostBack="true" Text="Alunos"/>
                                            </li>      
                                            <li style="margin-left: 4px;">
                                                <label for="txtIdadeMinimAlunoSecreEscol" style="float:left;" title="Idade Mínima para Aluno">Idade Mínima</label>
                                                <asp:TextBox ID="txtIdadeMinimAlunoSecreEscol" style="margin-left:7px;"
                                                    ToolTip="Informe a Idade Mínima do aluno" Enabled="false"
                                                    CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                            </li>   
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarPaisRespo" ToolTip="Informe se existirá usuário Pais/Responsáveis" runat="server" 
                                                    Text="Pais/Responsáveis"/>
                                            </li>   
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarOutro" ToolTip="Informe se existirá usuário Outros" runat="server" 
                                                    Text="Outros"/>
                                            </li>                      
                                        </ul>  
                                    </li>
                                    <li style="padding-left: 5px;">
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Horário de Atividades</span></li>
                                            <li class="liClear" style="margin-top:4px;">
                                            <ul>
                                                <li><span title="Turno 1">Turno 1</span></li>
                                                <li class="liClear">                        
                                                    <asp:TextBox ID="txtHorarIniT1SecreEscol" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Inicial do Turno 1"
                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT1SecreEscol" style="margin-bottom: 3px;"
                                                    ToolTip="Informe o Horário Final do Turno 1"
                                                    CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>    
                                            <li style="margin-top:4px;">
                                            <ul>
                                                <li><span title="Turno 2">Turno 2</span></li>
                                                <li class="liClear">
                                                    <asp:TextBox ID="txtHorarIniT2SecreEscol" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Inical do Turno 2"
                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT2SecreEscol" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Final do Turno 2"
                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>
                                            <li class="liClear">
                                            <ul>
                                                <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                                <li class="liClear">
                                                    <asp:TextBox ID="txtHorarIniT3SecreEscol" style="margin-bottom: 5px;"
                                                        ToolTip="Informe o Horário Inicial do Turno 3"
                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT3SecreEscol" style="margin-bottom: 5px;"
                                                        ToolTip="Informe o Horário Final do Turno 3"
                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>
                                            <li>
                                            <ul>
                                                <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                                <li class="liClear">
                                                    <asp:TextBox ID="txtHorarIniT4SecreEscol" style="margin-bottom: 5px;"
                                                        ToolTip="Informe o Horário Inicial do Turno 4"
                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT4SecreEscol" style="margin-bottom: 5px;"
                                                        ToolTip="Informe o Horário Final do Turno 4"
                                                        CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>
                                        </ul>
                                    </li>   
                                    <li class="liClear" style="margin-top: 10px;width: 360px;">  
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Secretário(a) Escolar</span></li>                                                                                      
                                            <asp:UpdatePanel ID="UpdatePanel48" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liClear" style="margin-top: 5px;">
                                                <label for="ddlSiglaUnidSecreEscol1" title="Unidade" style="color:#006699;">Unidade</label>
                                                <asp:DropDownList ID="ddlSiglaUnidSecreEscol1" AutoPostBack="true" runat="server"
                                                    ToolTip="Selecione a Sigla da Unidade Escolar"
                                                    CssClass="ddlSiglaUnidSecreEscol" OnSelectedIndexChanged="ddlSiglaUnidSecreEscol1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>  
                                            <li style="margin-left: 5px; margin-top: 5px;">
                                                <label for="ddlNomeSecreEscol1" title="Nome Secretário Escolar" style="color:#006699;">Nome Secretário Escolar</label>
                                                <asp:DropDownList ID="ddlNomeSecreEscol1" runat="server"
                                                    ToolTip="Selecione o Nome Secretário Escolar" CssClass="ddlNomeSecreEscol" />
                                            </li>
                                            <li style="margin-right: 0px; margin-top: 5px;">
                                                <label for="ddlClassifSecre1" title="Classificação do Secretário" style="color:#006699;">Classif.</label>
                                                <asp:DropDownList ID="ddlClassifSecre1" runat="server" ToolTip="Selecione a classificação de secretário" CssClass="ddlQtdeSecretario">
                                                    <asp:ListItem Selected="True" Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Value="2">2º</asp:ListItem>
                                                    <asp:ListItem Value="3">3º</asp:ListItem>
                                                </asp:DropDownList>                                        
                                            </li>
                                            <li class="liClear" style="margin-top: 5px;">
                                                <asp:DropDownList ID="ddlSiglaUnidSecreEscol2" AutoPostBack="true" runat="server"
                                                    ToolTip="Selecione a Sigla da Unidade Escolar"
                                                    CssClass="ddlSiglaUnidSecreEscol" OnSelectedIndexChanged="ddlSiglaUnidSecreEscol2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li> 
                                            <li style="margin-left: 5px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlNomeSecreEscol2" runat="server"
                                                    ToolTip="Selecione o Nome Secretário Escolar" CssClass="ddlNomeSecreEscol" />                                    
                                            </li>
                                            <li style="margin-right: 0px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlClassifSecre2" runat="server" ToolTip="Selecione a classificação de secretário" CssClass="ddlQtdeSecretario">
                                                    <asp:ListItem Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">2º</asp:ListItem>
                                                    <asp:ListItem Value="3">3º</asp:ListItem>
                                                </asp:DropDownList>                                        
                                            </li>
                                            <li class="liClear" style="margin-top: 5px;">
                                                <asp:DropDownList ID="ddlSiglaUnidSecreEscol3" AutoPostBack="true" runat="server"
                                                    ToolTip="Selecione a Sigla da Unidade Escolar"
                                                    CssClass="ddlSiglaUnidSecreEscol" OnSelectedIndexChanged="ddlSiglaUnidSecreEscol3_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li> 
                                            <li style="margin-left: 5px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlNomeSecreEscol3" runat="server"
                                                    ToolTip="Selecione o Nome Secretário Escolar" CssClass="ddlNomeSecreEscol" />                                        
                                            </li>
                                            <li style="margin-right: 0px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlClassifSecre3" runat="server" ToolTip="Selecione a classificação de secretário" CssClass="ddlQtdeSecretario">
                                                    <asp:ListItem Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Value="2">2º</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="3">3º</asp:ListItem>
                                                </asp:DropDownList>                                        
                                            </li>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ul>
                                    </li>                                                            
                                </ul>
                            </li>
                            <li style="padding-left: 5px;">
                                <ul>
                                    <li style="width:374px;">
                                        <ul>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liPedagMatric" style="width: 375px;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Serviços de Secretaria Escolar</span></li>                
                                            <li class="liControleMetodologia" style="width: 190px;">
                                                <label title="Gera nº solicitação automático?">Gera nº solicitação automático?</label>
                                                <asp:DropDownList ID="ddlGeraNumSolicAuto" onselectedindexchanged="ddlGeraNumSolicAuto_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Informe se gera nº de solicitação automático"
                                                    CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            <li style="margin-top: 7px;">
                                                <label for="txtNumIniciSolicAuto" style="float:left;" title="Número Inicial de Solicitação Automática">Nº Inicial</label>
                                                <asp:TextBox ID="txtNumIniciSolicAuto" Enabled="false" style="margin-left:8px;margin-bottom: 0;text-align: right;"
                                                    ToolTip="Informe o Número Inicial de Solicitação Automática"
                                                    CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                            </li> 
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liControleMetodologia" style="width: 190px;margin-top: 5px;">
                                                <label title="Controle de prazo de entrega?">Controle de prazo de entrega?</label>
                                                <asp:DropDownList ID="ddlContrPrazEntre" style="margin-left: 3px;" onselectedindexchanged="ddlContrPrazEntre_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Informe se possui controle de prazo de entrega"
                                                    CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>

                                            <li style="margin-top: 5px;">
                                                <label for="txtQtdeDiasEntreSolic" style="float:left;" title="Quantidade de dias para entrega">Qtde dias</label>
                                                <asp:TextBox ID="txtQtdeDiasEntreSolic" style="margin-left:7px;"
                                                    ToolTip="Informe a Quantidade de dias para entrega" Enabled="false"
                                                    CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                            </li>
                    
                                            <li style="margin-top: 5px;margin-left:13px;">
                                                <label for="ddlAlterPrazoEntreSolic" style="float:left;" title="Pode ser alterado o prazo de entrega?">Altera?</label>
                                                <asp:DropDownList ID="ddlAlterPrazoEntreSolic" style="margin-left:7px;"
                                                    ToolTip="Informe se o Prazo de Entrega pode ser alterado" Enabled="false"
                                                    CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <asp:UpdatePanel ID="UpdatePanel35" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liControleMetodologia" style="width: 190px;margin-top: 5px;">
                                                <label title="Controle de prazo de entrega?">Apresenta valores de Serviços?</label>
                                                <asp:DropDownList ID="ddlFlagApresValorServi" style="" onselectedindexchanged="ddlFlagApresValorServi_SelectedIndexChanged" AutoPostBack="true"
                                                 ToolTip="Informe se possui controle de prazo de entrega" CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>

                                            <li style="margin-top: 5px;">
                                                <label for="ddlAbonaValorServiSolic" style="float:left;" title="Pode ser abonado o valor de serviço?">Abona?</label>
                                                <asp:DropDownList ID="ddlAbonaValorServiSolic" style="margin-left:7px;"
                                                    ToolTip="Informe se o valor de serviço pode ser abonado" Enabled="false"
                                                    CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                    
                                            <li style="margin-top: 5px;">
                                                <label for="ddlApresValorServiSolic" style="float:left;" title="Pode ser alterado o valor de serviço?">Altera?</label>
                                                <asp:DropDownList ID="ddlApresValorServiSolic" style="margin-left:7px;"
                                                    ToolTip="Informe se o valor de serviço pode ser alterado" Enabled="false"
                                                    CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>                                            
                                            <li class="liClear" style="margin-top: 5px;">
                                                <label for="ddlFlagIncluContaReceb" style="float:left;" title="Inclui taxas no contas a receber?">Inclui taxas no CAR?</label>
                                                <asp:DropDownList ID="ddlFlagIncluContaReceb" style="margin-left: 50px;"
                                                    ToolTip="Informe se inclui no contas a receber" runat="server"
                                                    CssClass="ddlAlterPrazoEntreSolic" AutoPostBack="true" onselectedindexchanged="ddlFlagIncluContaReceb_SelectedIndexChanged">
                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                </asp:DropDownList>
                                            </li>
                                        </ul>
                                    </li>
                                    <li class="liClear" style="margin-top: 15px; width:374px;">
                                        <ul>                                                                        
                                            <li class="liPedagMatric" style="width: 100%;background-color: #FFFFCC;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Boleto Bancário</span></li>
                                            <li class="liClear">
                                                <ul>
                                                    <li class="liClear">
                                                        <ul>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">                     
                                                            <ContentTemplate>
                                                            <li class="liClear" style="margin-top: 7px;">
                                                                <label for="ddlFlagGerarBoletoIUE" style="float:left;" title="Gera Boleto Bancário?">Gera Boleto Bancário?</label>
                                                                <asp:DropDownList ID="ddlGeraBoletoServiSecre" style="margin-left: 21px;" onselectedindexchanged="ddlGeraBoletoServiSecre_SelectedIndexChanged" AutoPostBack="true"
                                                                    ToolTip="Informe se serão gerados Boletos Bancários" runat="server"
                                                                    CssClass="ddlAlterPrazoEntreSolic">
                                                                    <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </li>                                                
                                                            <li class="liClear" style="margin-top: 5px;">
                                                                <label for="ddlTipoBoletoServiSecre" style="float:left;" title="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)">Tipo Boleto</label>
                                                                <asp:DropDownList ID="ddlTipoBoletoServiSecre" Enabled="false" ToolTip="Selecione o tipo do boleto bancário (Modelo 1 - Carnê; Modelo 2 - Com recibo do sacado; Modelo 3 - Com recibo do sacado e comprovante de entrega; Modelo 4 - Com recibo do sacado e valor do desconto nas instruções)" style="margin-left:7px;" runat="server">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                    <asp:ListItem Value="N">Modelo 1</asp:ListItem> 
                                                                    <asp:ListItem Value="C">Modelo 2</asp:ListItem> 
                                                                    <asp:ListItem Value="S">Modelo 3</asp:ListItem> 
                                                                    <asp:ListItem Value="M">Modelo 4</asp:ListItem> 
                                                                </asp:DropDownList>
                                                            </li>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ul>
                                                    </li>
                                                    <li style="margin-top: 7px; margin-left: 5px;margin-right: 0;">
                                                        <ul>
                                                            <li class="liClear" style="">
                                                                <label for="txtVlJurosSecre" style="float:left;" title="Juros Diário">Juros Diário</label>
                                                                <asp:TextBox ID="txtVlJurosSecre" CssClass="txtVlJurosSecre" runat="server" MaxLength="5" style="width: 30px; margin-left: 5px;margin-bottom:0;"
                                                                    ToolTip="Informe o Valor do Juros"></asp:TextBox>
                                                            </li>
                    
                                                            <li>
                                                                <label for="ddlFlagTipoJurosSecre" style="float:left;" title="Aplicação">Aplicação</label>
                                                                <asp:DropDownList ID="ddlFlagTipoJurosSecre" CssClass="ddlTipoAplic" runat="server"
                                                                    ToolTip="Informe o Tipo do Valor de Juros" style="margin-left: 3px;">
                                                                    <asp:ListItem Value="P">%</asp:ListItem>
                                                                    <asp:ListItem Value="V">R$</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </li>
                                                            <li class="liClear" style="margin-top: 5px;">
                                                                <label for="txtVlMultaSecre" style="float:left; width: 51px;" title="Multa">Multa</label>
                                                                <asp:TextBox ID="txtVlMultaSecre" CssClass="campoMoeda" runat="server" style="width: 30px; margin-left: 6px;"
                                                                    ToolTip="Informe o Valor da Multa"></asp:TextBox>
                                                            </li>
                    
                                                            <li style="margin-top: 5px;">
                                                                <label for="ddlFlagTipoMultaSecre" style="float:left;" title="Aplicação">Aplicação</label>
                                                                <asp:DropDownList ID="ddlFlagTipoMultaSecre" CssClass="ddlTipoAplic" runat="server"
                                                                    ToolTip="Informe o Tipo do Valor da Multa" style="margin-left: 3px;">
                                                                    <asp:ListItem Value="P">%</asp:ListItem>
                                                                    <asp:ListItem Value="V">R$</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </li>                                                            
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </li>                                                                        
                                        </ul>
                                    </li>    
                                    <%-- <li class="liClear" style="margin-top: 10px; width: 374px;">  
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Linhas de Relatório</span></li>                                                                                                                          
                                            <li class="liClear" style="margin-top: 5px;">
                                                <asp:TextBox ID="TextBox19" Enabled="false" Text="1" style="margin-bottom: 0;text-align: center; width: 15px;"
                                                    CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                            </li>  
                                            
                                            <li style="margin-top: 5px;">
                                                <asp:TextBox ID="txtCabec1Relatorio" style="margin-bottom: 0;"
                                                    ToolTip="Informe o texto que será apresentado na Linha 1 do Cabeçalho do Relatório"
                                                CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>
                                            </li>
                                            <li class="liClear" style="margin-top: 3px;">
                                                <asp:TextBox ID="TextBox21" Enabled="false" Text="2" style="margin-bottom: 0;text-align: center; width: 15px;"
                                                runat="server"></asp:TextBox>
                                            </li> 
                                            <li style="margin-top: 3px;">
                                                <asp:TextBox ID="txtCabec2Relatorio" style="margin-bottom: 0;"
                                                    ToolTip="Informe o texto que será apresentado na Linha 2 do Cabeçalho do Relatório"
                                                CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                
                                            </li>
                                            <li class="liClear" style="margin-top: 3px;">
                                                <asp:TextBox ID="TextBox23" Enabled="false" Text="3" style="margin-bottom: 0;text-align: center; width: 15px;"
                                                runat="server"></asp:TextBox>
                                            </li> 
                                            <li style="margin-top: 3px;">
                                                <asp:TextBox ID="txtCabec3Relatorio" style="margin-bottom: 0;"
                                                    ToolTip="Informe o texto que será apresentado na Linha 3 do Cabeçalho do Relatório"
                                                CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                       
                                            </li>
                                            <li class="liClear" style="margin-top: 3px;">
                                                <asp:TextBox ID="TextBox25" Enabled="false" style="margin-bottom: 0;text-align: center; width: 15px;"
                                                Text="4" runat="server"></asp:TextBox>
                                            </li> 
                                            <li style="margin-top: 3px;">
                                                <asp:TextBox ID="txtCabec4Relatorio" style="margin-bottom: 0;"
                                                    ToolTip="Informe o texto que será apresentado na Linha 4 do Cabeçalho do Relatório"
                                                CssClass="txtCabecalhoRelatorio" MaxLength="255" runat="server"></asp:TextBox>                                       
                                            </li>
                                            
                                        </ul>
                                    </li>   --%>                              
                                </ul>
                            </li>                    
                        </ul>
                    </li>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
            <div id="tabBibliEscol" class="tabBibliEscol" style="display: none;">
                <ul class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoBE" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoBE" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>   
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoBE" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoBE" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJBE" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJBE" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Controle Biblioteca Escolar</span></li>            
                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">                     
                    <ContentTemplate>
                    <li class="liClear" style="margin-top: 2px; margin-left: 324px; margin-bottom: 10px;">
                        <label for="ddlTipoCtrlBibliEscol" style="float:left;" title="Tipo de Biblioteca Escolar">Por</label>
                        <asp:DropDownList ID="ddlTipoCtrlBibliEscol" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle de Biblioteca" style="margin-left: 7px; width: 110px;"
                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrlBibliEscol_SelectedIndexChanged">
                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                        </asp:DropDownList>
                    </li> 

                    <li class="liClear">
                        <ul>
                            <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;height: 195px;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Parametrização Geral</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liControleMetodologia" style="width: 170px;">
                                        <label title="Permite reserva de livros?">Permite reserva?</label>
                                        <asp:DropDownList ID="ddlFlagReserBibli" onselectedindexchanged="ddlFlagReserBibli_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se permite reserva de livros" style="margin-left: 49px;"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 7px;">
                                        <label for="txtQtdeItensReser" style="float:left;" title="Quantidade de itens para reserva">Qtde de itens</label>
                                        <asp:TextBox ID="txtQtdeItensReser" style="margin-left:11px;"
                                            ToolTip="Informe a quantidade de itens para reserva" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 7px;">
                                        <label for="txtQtdeMaxDiasReser" style="float:left;" title="Quantidade máxima de dias para reserva">Máx. de dias</label>
                                        <asp:TextBox ID="txtQtdeMaxDiasReser" style="margin-left:10px;"
                                            ToolTip="Informe a quantidade máxima de dias para reserva" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>                            
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">                     
                                    <ContentTemplate>                            
                                    <li class="liControleMetodologia" style="width: 170px;margin-top: 5px;">
                                        <label title="Permite empréstimo de livros?">Permite empréstimo?</label>
                                        <asp:DropDownList ID="ddlFlagEmpreBibli" onselectedindexchanged="ddlFlagEmpreBibli_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se permite empréstimo de livros" style="margin-left: 31px;"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtQtdeItensEmpre" style="float:left;" title="Quantidade de itens para empréstimo">Qtde de itens</label>
                                        <asp:TextBox ID="txtQtdeItensEmpre" style="margin-left:11px;"
                                            ToolTip="Informe a quantidade de itens para empréstimo" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtQtdeMaxDiasEmpre" style="float:left;" title="Quantidade máxima de dias para empréstimo">Máx. de dias</label>
                                        <asp:TextBox ID="txtQtdeMaxDiasEmpre" style="margin-left:10px;"
                                            ToolTip="Informe a quantidade máxima de dias para empréstimo" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>                            
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel38" runat="server">                     
                                    <ContentTemplate>                
                                    <li class="liControleMetodologia" style="width: 170px;margin-top: 5px;">
                                        <label title="Gera nº empréstimo automático?">Gera nº emp. automático?</label>
                                        <asp:DropDownList ID="ddlGeraNumEmpreAuto" onselectedindexchanged="ddlGeraNumEmpreAuto_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se gera nº de empréstimo automático" style="margin-left: 6px;"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtNumIniciEmpreAuto" style="float:left;" title="Número Inicial de Empréstimo Automática">Nº Inicial</label>
                                        <asp:TextBox ID="txtNumIniciEmpreAuto" Enabled="false" style="margin-left:29px;margin-bottom: 0;text-align: right;"
                                            ToolTip="Informe o Número Inicial de Empréstimo Automático"
                                            CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">                     
                                    <ContentTemplate>                            
                                    <li class="liControleMetodologia" style="width: 170px;margin-top: 5px;">
                                        <label title="Cobra taxa de empréstimo?">Cobra taxa de empréstimo?</label>
                                        <asp:DropDownList ID="ddlFlagTaxaEmpre" onselectedindexchanged="ddlFlagTaxaEmpre_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se cobra taxa de empréstimo" style="margin-left: 1px;"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtDiasBonusTaxaEmpre" style="float:left;" title="Quantidade de dias bônus para taxa de empréstimo">Dias de bônus</label>
                                        <asp:TextBox ID="txtDiasBonusTaxaEmpre" style="margin-left:7px;"
                                            ToolTip="Informe a quantidade de dias bônus para taxa de empréstimo" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtValorTaxaEmpre" style="float:left;" title="Valor da taxa do empréstimo">Valor diário</label>
                                        <asp:TextBox ID="txtValorTaxaEmpre" style="margin-left:7px; margin-bottom: 0; width: 25px;"
                                            ToolTip="Informe o valor da taxa do empréstimo" Enabled="false"
                                            CssClass="campoMoeda" MaxLength="4" runat="server"></asp:TextBox>
                                    </li>                            
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">                     
                                    <ContentTemplate>                            
                                    <li class="liControleMetodologia" style="width: 170px;margin-top: 5px;">
                                        <label title="Multa de atraso devolução?">Multa de atraso devolução?</label>
                                        <asp:DropDownList ID="ddlFlagMultaAtraso" onselectedindexchanged="ddlFlagMultaAtraso_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se cobra taxa de empréstimo"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtDiasBonusMultaEmpre" style="float:left;" title="Quantidade de dias bônus para multa de empréstimo">Dias de bônus</label>
                                        <asp:TextBox ID="txtDiasBonusMultaEmpre" style="margin-left:7px;"
                                            ToolTip="Informe a quantidade de dias bônus para multa de empréstimo" Enabled="false"
                                            CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtValorTaxaEmpre" style="float:left;" title="Valor da multa do empréstimo">Valor diário</label>
                                        <asp:TextBox ID="txtValorMultaEmpre" style="margin-left:7px; margin-bottom: 0; width: 25px;"
                                            ToolTip="Informe o valor da multa do empréstimo" Enabled="false"
                                            CssClass="campoMoeda" MaxLength="4" runat="server"></asp:TextBox>
                                    </li>                            
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">                     
                                    <ContentTemplate>                
                                    <li class="liControleMetodologia" style="width: 170px;margin-top: 5px;">
                                        <label title="Gera nº item automático?">Gera nº item automático?</label>
                                        <asp:DropDownList ID="ddlGeraNumItemAuto" onselectedindexchanged="ddlGeraNumItemAuto_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Informe se gera nº de item automático" style="margin-left: 10px;"
                                            CssClass="ddlAlterPrazoEntreSolic" runat="server">
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">Não</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: 5px;">
                                        <label for="txtNumIniciItemAuto" style="float:left;" title="Número Inicial de Item Automática">Nº Inicial</label>
                                        <asp:TextBox ID="txtNumIniciItemAuto" Enabled="false" style="margin-left:29px;margin-bottom: 0;text-align: right;"
                                            ToolTip="Informe o Número Inicial de Solicitação Automática"
                                            CssClass="txtNumIniciSolicAuto" runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <li class="liClear" style="margin-top: 5px;">
                                        <label for="ddlNumISBNObrig" style="float:left;" title="Nº de ISBN é obrigatório?">Nº de ISBN é obrigatório?</label>
                                        <asp:DropDownList ID="ddlNumISBNObrig" style="margin-left: 8px;"
                                            ToolTip="Informe se nº de ISBN é obrigatório" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>

                                    <li class="liClear" style="margin-top: 5px;">
                                        <label for="ddlFlarIncluContaRecebBibli" style="float:left;" title="Inclui taxas no contas a receber?">Inclui taxas no CAR?</label>
                                        <asp:DropDownList ID="ddlFlarIncluContaRecebBibli" style="margin-left: 33px;"
                                            ToolTip="Informe se inclui no contas a receber" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </li>
                            <li style="width: 380px;padding-left: 5px;margin-right: 0;">
                                <ul>
                                    <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; height: 105px;">  
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Usuários de Biblioteca</span></li>                                                   
                                            <li class="liRecuperacao liClear" style="margin-top: 6px; margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarFuncBibli" ToolTip="Informe se existirá usuário Funcionário" runat="server" 
                                                    Text="Funcionários"/>
                                            </li>   
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarProfBibli" ToolTip="Informe se existirá usuário Professor" runat="server" 
                                                    Text="Professores"/>
                                            </li>   
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarAlunoBibli" ToolTip="Informe se existirá usuário Aluno" runat="server" 
                                                 OnCheckedChanged="chkUsuarAlunoBibli_CheckedChanged" AutoPostBack="true"    Text="Alunos"/>
                                            </li>      
                                            <li style="margin-left: 4px;">
                                                <label for="txtIdadeMinimAlunoBibli" style="float:left;" title="Idade Mínima para Aluno">Idade Mínima</label>
                                                <asp:TextBox ID="txtIdadeMinimAlunoBibli" style="margin-left:7px;"
                                                    ToolTip="Informe a Idade Mínima do aluno" Enabled="false"
                                                    CssClass="txtQtdeDiasEntreSolic" runat="server"></asp:TextBox>
                                            </li>   
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarRespBibli" ToolTip="Informe se existirá usuário Pais/Responsáveis" runat="server" 
                                                    Text="Pais/Responsáveis"/>
                                            </li>   
                                            <li class="liRecuperacao liClear" style="margin-left: -5px;">
                                                <asp:CheckBox ID="chkUsuarOutroBibli" ToolTip="Informe se existirá usuário Outros" runat="server" 
                                                    Text="Outros"/>
                                            </li>                      
                                        </ul>  
                                    </li>
                                    <li style="padding-left: 5px;">
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Horário de Atividades</span></li>                                          
                                            <li class="liClear" style="margin-top:4px;">
                                            <ul>
                                                <li><span title="Turno 1">Turno 1</span></li>
                                                <li class="liClear">                        
                                                    <asp:TextBox ID="txtHorarIniT1Bibli" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Inicial do Turno 1"
                                                        CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT1Bibli" style="margin-bottom: 3px;"
                                                    ToolTip="Informe o Horário Final do Turno 1"
                                                    CssClass="txtHorarioFuncManha" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>    
                                            <li style="margin-top:4px;">
                                            <ul>
                                                <li><span title="Turno 2">Turno 2</span></li>
                                                <li class="liClear">
                                                    <asp:TextBox ID="txtHorarIniT2Bibli" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Inical do Turno 2"
                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                </li>
                                                <li><span> às </span></li>
                                                <li>
                                                    <asp:TextBox ID="txtHorarFimT2Bibli" style="margin-bottom: 3px;"
                                                        ToolTip="Informe o Horário Final do Turno 2"
                                                        CssClass="txtHorarioFuncTarde" runat="server"></asp:TextBox>
                                                </li>
                                            </ul>
                                            </li>
                                            <li class="liClear">
                                                <ul>
                                                    <li class="liClear" title="Turno 3"><span>Turno 3</span></li>
                                                    <li class="liClear">
                                                        <asp:TextBox ID="txtHorarIniT3Bibli" style="margin-bottom: 3px;"
                                                            ToolTip="Informe o Horário Inicial do Turno 3"
                                                            CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                    </li>
                                                    <li><span> às </span></li>
                                                    <li>
                                                        <asp:TextBox ID="txtHorarFimT3Bibli" style="margin-bottom: 3px;"
                                                            ToolTip="Informe o Horário Final do Turno 3"
                                                            CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>
                                                <ul>
                                                    <li class="liClear" title="Turno 4"><span>Turno 4</span></li>
                                                    <li class="liClear">
                                                        <asp:TextBox ID="txtHorarIniT4Bibli" style="margin-bottom: 3px;"
                                                            ToolTip="Informe o Horário Inicial do Turno 4"
                                                            CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                    </li>
                                                    <li><span> às </span></li>
                                                    <li>
                                                        <asp:TextBox ID="txtHorarFimT4Bibli" style="margin-bottom: 3px;"
                                                            ToolTip="Informe o Horário Final do Turno 4"
                                                            CssClass="txtHorarioFuncNoite" runat="server"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                    
                                    <li class="liClear" style="margin-top: 7px;width: 360px;">  
                                        <ul>
                                            <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Bibliotecário(a)</span></li>                                                                                      
                                            <asp:UpdatePanel ID="UpdatePanel49" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liClear" style="margin-top: 5px;">
                                                <label for="ddlSiglaUnidBibliEscol" title="Unidade" style="color:#006699;">Unidade</label>
                                                <asp:DropDownList ID="ddlSiglaUnidBibliEscol1" AutoPostBack="true" runat="server"
                                                    ToolTip="Selecione a Sigla da Unidade Escolar"
                                                    CssClass="ddlSiglaUnidSecreEscol" OnSelectedIndexChanged="ddlSiglaUnidBibliEscol1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>  
                                            <li style="margin-left: 5px; margin-top: 5px;">
                                                <label for="ddlNomeBibliEscol" title="Nome Bibliotecário Escolar" style="color:#006699;">Nome Bibliotecário Escolar</label>
                                                <asp:DropDownList ID="ddlNomeBibliEscol1" runat="server"
                                                    ToolTip="Selecione o Nome Bibliotecário(a)" CssClass="ddlNomeSecreEscol" />
                                            </li>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <li style="margin-right: 0px; margin-top: 5px;">
                                                <label for="ddlClassifBibli1" title="Classificação do Bibliotecário" style="color:#006699;">Classif.</label>
                                                <asp:DropDownList ID="ddlClassifBibli1" runat="server" ToolTip="Selecione a classificação de bibliotecário" CssClass="ddlQtdeSecretario">
                                                    <asp:ListItem Selected="True" Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Value="2">2º</asp:ListItem>
                                                </asp:DropDownList>                                        
                                            </li>
                                            <asp:UpdatePanel ID="UpdatePanel50" runat="server">                     
                                            <ContentTemplate>
                                            <li class="liClear" style="margin-top: 5px;">
                                                <asp:DropDownList ID="ddlSiglaUnidBibliEscol2" AutoPostBack="true" runat="server"
                                                    ToolTip="Selecione a Sigla da Unidade Escolar"
                                                    CssClass="ddlSiglaUnidSecreEscol" OnSelectedIndexChanged="ddlSiglaUnidBibliEscol2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </li>  
                                            <li style="margin-left: 5px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlNomeBibliEscol2" runat="server"
                                                    ToolTip="Selecione o Nome Bibliotecário(a)" CssClass="ddlNomeSecreEscol" />
                                            </li>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <li style="margin-right: 0px; margin-top: 5px;">
                                                <asp:DropDownList ID="ddlClassifBibli2" runat="server" ToolTip="Selecione a classificação de bibliotecário" CssClass="ddlQtdeSecretario">
                                                    <asp:ListItem Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">2º</asp:ListItem>
                                                </asp:DropDownList>                                        
                                            </li>
                                        </ul>
                                    </li>                                                                       
                                </ul>
                            </li>
                        </ul>
                    </li>
                    </ContentTemplate>
                    </asp:UpdatePanel> 
                </ul>
            </div>
            <div id="tabContabil" class="tabContabil" style="display: none;">
                <ul class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoCO" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoCO" ClientIDMode="Static" ToolTip="Instituição de Ensino" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>   
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoCO" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoCO" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJCO" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJCO" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Controle Contábil</span></li>            
                    <asp:UpdatePanel ID="UpdatePanel34" runat="server">                     
                    <ContentTemplate>
                    <li class="liClear" style="margin-top: 2px; margin-left: 324px; margin-bottom: 10px;">
                        <label for="ddlTipoCtrleContaContab" style="float:left;" title="Tipo de Controle Contábil">Por</label>
                        <asp:DropDownList ID="ddlTipoCtrleContaContab" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle Contábil" style="margin-left: 7px; width: 110px;"
                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoCtrleContaContab_SelectedIndexChanged">
                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                        </asp:DropDownList> 
                    </li> 

                    <li class="liClear">
                        <ul>
                            <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Taxas e Mensalidades Escolares</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 5px;">
                                        <label style="color:#006699;" title="Item">ITEM</label>
                                        <label style="float:left;" title="Taxa Serviço Secretaria">Tx Serviço</label>
                                    </li> 
                                    <li style="margin-left: 5px;width: 205px;margin-top: 5px;">
                                        <label style="color:#006699;" title="CONTA CONTÁBIL (Tipo/Grupo/SubGrupo/Conta)">CONTA CONTÁBIL (Tp/Grp/SGrp/SGrp2/Cta)</label>
                                        <asp:TextBox ID="TextBox11" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxServSecre" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServSecre_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator17" ControlToValidate="ddlGrupoTxServSecre" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil da Tx Serv. Secretaria"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxServSecre_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxServSecre" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServSecre_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2TxServSecre" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxServSecre_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxServSecre" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServSecre_SelectedIndexChanged">
                                        </asp:DropDownList>                                
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 5px;">
                                        <label style="color:#006699;" title="CENTRO DE CUSTO">CENTRO DE CUSTO</label>
                                        <asp:DropDownList ID="ddlCentroCustoTxServSecre" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left;margin-top: -5px;" title="Taxa Serviço Secretaria">Secretaria</label>
                                        <asp:TextBox ID="txtCtaContabTxServSecre" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 14px;"
                                        runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px;">
                                        <label style="float:left;" title="Taxa Serviço Biblioteca">Tx Serviço</label>
                                    </li> 
                                    <li style="margin-left: 5px;width: 205px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox12" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxServBibli" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServBibli_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator16" ControlToValidate="ddlGrupoTxServBibli" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Biblioteca"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxServBibli_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxServBibli" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServBibli_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2TxServBibli" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxServBibli_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxServBibli" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServBibli_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoTxServBibli" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left;margin-top: -5px;" title="Taxa Serviço Biblioteca">Biblioteca</label>
                                        <asp:TextBox ID="txtCtaContabTxServBibli" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 16px;"
                                            ToolTip="Taxas de Serviços de Biblioteca Escolar" runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px;">
                                        <label style="float:left;" title="Tx Negociação de Débito">Tx Negoc.</label>
                                    </li> 
                                    <li style="margin-left: 7px;width: 205px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox13" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxMatri" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxMatri_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator15" ControlToValidate="ddlGrupoTxMatri" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Negociação de Débito"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxMatri_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxMatri" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxMatri_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2TxMatri" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2TxMatri_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxMatri" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxMatri_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoTxMatri" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left;margin-top: -5px;" title="Negociação de Débito">De Débito</label>
                                        <asp:TextBox ID="txtCtaContabTxMatri" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 13px;"
                                            ToolTip="Taxas Contratuais de Negociação de Débito" runat="server"></asp:TextBox>
                                    </li>                                    
                                    </ContentTemplate>
                                    </asp:UpdatePanel>      
                            
                                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px;">
                                        <label style="float:left;" title="Atividades Extras">Atividades</label>
                                    </li> 
                                    <li style="margin-left: 7px;width: 205px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox15" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoAtiviExtra" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoAtiviExtra_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator19" ControlToValidate="ddlGrupoAtiviExtra" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Atividade Extra"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilAtividaExtra_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoAtiviExtra" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoAtiviExtra_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2AtiviExtra" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2AtiviExtra_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilAtiviExtra" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilAtiviExtra_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoAtiviExtra" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left;margin-top: -5px;" title="Atividades Extras">Extras</label>
                                        <asp:TextBox ID="txtCtaContabAtiviExtra" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 31px;"
                                            ToolTip="Mensalidades de Atividades Extra Classe" runat="server"></asp:TextBox>
                                    </li>                                    
                                    </ContentTemplate>
                                    </asp:UpdatePanel>  
                                    
                                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px; margin-right: 7px;">
                                        <label style="float:left;" title="Conta Contábil de Caixa">Conta Caixa</label>
                                    </li> 
                                    <li style="margin-left: -1px;width: 205px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox14" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoContaCaixa" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoContaCaixa_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator18" ControlToValidate="ddlGrupoContaCaixa" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Caixa"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilCaixa_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoContaCaixa" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoContaCaixa_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2ContaCaixa" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2ContaCaixa_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilContaCaixa" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilContaCaixa_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoContaCaixa" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;margin-left: 58px;">
                                        <asp:TextBox ID="txtCtaContabCaixa" Enabled="false" style="width: 250px;margin-bottom: 0;"
                                            ToolTip="Conta Contábil de Caixa" runat="server"></asp:TextBox>
                                    </li>                                    
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 

                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px; margin-right: 3px;">
                                        <label style="float:left;" title="Conta Contábil de Banco">Conta Banco</label>
                                    </li> 
                                    <li style="margin-left: -1px;width: 205px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox1" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoContaBanco" runat="server" Width="35px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoContaBanco_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator2" ControlToValidate="ddlGrupoContaBanco" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Banco"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilBanco_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoContaBanco" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoContaBanco_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlSubGrupo2ContaBanco" runat="server" Width="40px"
                                            ToolTip="Selecione o Subgrupo 2"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupo2ContaBanco_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilContaBanco" ToolTip="Selecione a Conta Contábil" runat="server" Width="45px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilContaBanco_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoContaBanco" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;margin-left: 58px;">
                                        <asp:TextBox ID="txtCtaContabBanco" Enabled="false" style="width: 250px;margin-bottom: 0;"
                                            ToolTip="Conta Contábil de Banco" runat="server"></asp:TextBox>
                                    </li>                                    
                                    </ContentTemplate>
                                    </asp:UpdatePanel> 
                                </ul>
                            </li>
                            <%--
                            <li style="padding-left: 5px;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Gestão de Matrículas</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 5px;">
                                        <label style="color:#006699;" title="Item">ITEM</label>
                                        <label style="float:left;" title="Taxa Matrícula Nova">Tx Matrícula</label>
                                    </li> 
                                    <li style="margin-left: 8px;width: 182px;margin-top: 5px;">
                                        <label style="color:#006699;" title="CONTA CONTÁBIL (Tipo/Grupo/SubGrupo/Conta)">CONTA CONTÁBIL (Tp/Grp/SGrp/Cta)</label>
                                        <asp:TextBox ID="TextBox16" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxMatricNova" runat="server" Width="40px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxMatricNova_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator20" ControlToValidate="ddlGrupoTxMatricNova" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Matrícula Nova"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxMatricNova_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxMatricNova" runat="server" Width="45px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxMatricNova_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxMatricNova" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilAtiviExtra_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 5px;">
                                        <label style="color:#006699;" title="CENTRO DE CUSTO">CENTRO DE CUSTO</label>
                                        <asp:DropDownList ID="ddlCentroCustoTxMatricNova" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left; margin-top: -5px;" title="Taxa Matrícula Nova">Nova</label>
                                        <asp:TextBox ID="txtCtaContabTxMatricNova" Enabled="false" style="width: 250px;margin-bottom: 0;margin-left: 43px;"
                                            ToolTip="Taxas de Serviços de Secretaria" runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px;">
                                        <label style="float:left;" title="Tx Renovação">Tx Renovação</label>
                                    </li> 
                                    <li style="margin-left: -2px;width: 182px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox17" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxRenov" runat="server" Width="40px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxRenov_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator21" ControlToValidate="ddlGrupoTxRenov" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Renovação de Matrícula"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxRenov_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxRenov" runat="server" Width="45px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxRenov_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxRenov" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxRenov_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoTxRenov" Width="93px" ToolTip="Selecione o Centro de Custo" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;margin-left: 67px;">
                                        <asp:TextBox ID="txtCtaContabTxRenov" Enabled="false" style="width: 250px;margin-bottom: 0;"
                                            ToolTip="Outras taxas de serviços de Secretaria" runat="server"></asp:TextBox>
                                    </li> 
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liClear" style="margin-top: 10px;">
                                        <label style="float:left;" title="Taxa Serviço Secretaria">Tx Serviço</label>
                                    </li> 
                                    <li style="margin-left: 14px;width: 182px;margin-top: 10px;">
                                        <asp:TextBox ID="TextBox18" Enabled="false" style="width: 10px; margin-bottom: 0;"
                                            ToolTip="Crédito" runat="server">C</asp:TextBox>

                                        <asp:DropDownList ID="ddlGrupoTxServSecreMatri" runat="server" Width="40px"
                                            ToolTip="Selecione o Grupo de Receita"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoTxServSecreMatri_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator22" ControlToValidate="ddlGrupoTxServSecreMatri" runat="server" 
                                            ErrorMessage="É necessário informar todos os campo referentes a Conta Contábil de Serviço de Secretaria da Gestão de Matrícula"
                                            Display="None" CssClass="validatorField" 
                                            EnableClientScript="false" OnServerValidate="cvContaContabilTxServSecreMatri_ServerValidate">
                                        </asp:CustomValidator>

                                        <asp:DropDownList ID="ddlSubGrupoTxServSecreMatri" runat="server" Width="45px"
                                            ToolTip="Selecione o Subgrupo"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubGrupoTxServSecreMatri_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlContaContabilTxServSecreMatri" ToolTip="Selecione a Conta Contábil" runat="server" Width="49px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlContaContabilTxServSecreMatri_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li> 
                                    <li style="margin-left: 5px;margin-top: 10px;">
                                        <asp:DropDownList ID="ddlCentroCustoTxServSecreMatri" runat="server" Width="93px" ToolTip="Selecione o Centro de Custo">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear" style="margin-top: 2px;">
                                        <label style="float:left; margin-top: -5px;" title="Taxa Serviço Secretaria">Secretaria</label>
                                        <asp:TextBox ID="txtCtaContabTxServSecreMatri" Enabled="false" style="width:250px;margin-bottom: 0;margin-left: 23px;"
                                            ToolTip="Taxas de Serviços de Secretaria" runat="server"></asp:TextBox>
                                    </li>  
                                    </ContentTemplate>
                                    </asp:UpdatePanel>              
                                </ul>
                            </li>--%>                                        
                        </ul>
                    </li>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
            <div id="tabMensaSMS" class="tabMensaSMS" style="display: none;">
                <ul class="ulDados">
                    <li class="liClear" style="margin-left: 130px;">
                        <label for="txtInstituicaoMS" title="Instituição de Ensino">Instituição de Ensino</label>
                        <asp:TextBox ID="txtInstituicaoMS" ToolTip="Instituição de Ensino" ClientIDMode="Static" Enabled="false" BackColor="#FFFFE1" CssClass="txtInstitEnsino" runat="server"></asp:TextBox>
                    </li>  
                    
                    <li class="liCodIdentificacao">
                        <label for="txtCodIdenticacaoMS" title="Código de Identificação">Cód. Identificação</label>
                        <asp:TextBox ID="txtCodIdenticacaoMS" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o Código de Identificação"
                            CssClass="txtSiglaIUE" MaxLength="5" runat="server"></asp:TextBox>
                    </li>

                    <li style="margin-left: 10px;">
                        <label for="txtCNPJMS" title="CNPJ">Nº CNPJ</label>
                        <asp:TextBox ID="txtCNPJMS" Enabled="false" ClientIDMode="Static"
                            ToolTip="Informe o CNPJ"
                            CssClass="txtCNPJIUE" runat="server"></asp:TextBox>
                    </li> 

                    <li class="liClear" style="width: 100%; text-align: center;"><span style="color:#6495ED; font-size:1.3em;font-weight:bold;">Controle de Mensagens SMS</span></li>            
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">                     
                    <ContentTemplate>
                    <li class="liClear" style="margin-top: 2px; margin-left: 324px; margin-bottom: 10px;">
                        <label for="ddlTipoControleMensaSMS" style="float:left;" title="Tipo de Biblioteca Escolar">Por</label>
                        <asp:DropDownList ID="ddlTipoControleMensaSMS" AutoPostBack="true" ToolTip="Selecione o Tipo de Controle de Mensagens SMS" style="margin-left: 7px; width: 110px;"
                            CssClass="ddlTipoControleFuncIIE" runat="server" OnSelectedIndexChanged="ddlTipoControleMensaSMS_SelectedIndexChanged">
                            <asp:ListItem Value="I">Instituição de Ensino</asp:ListItem>
                            <asp:ListItem Value="U">Unidade Escolar</asp:ListItem>
                        </asp:DropDownList>               
                    </li>

                    <li class="liClear">
                        <ul>
                            <li style="padding-right: 10px; border-right: 1px solid #CCCCCC;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Secretaria Escolar</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSSolicSecreEscol" ToolTip="Informe se enviará SMS para uma solicitação da Secretaria Escolar" runat="server" 
                                            OnCheckedChanged="chkSMSSolicSecreEscol_CheckedChanged" AutoPostBack="true"  Text="Solicitação"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSSolicEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSSolicEnvAuto" style="margin-left: 3px;"
                                            ToolTip="Informe se o envio é automático" runat="server" Enabled="false"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N" Selected="true">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSSolic" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSSolic" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de solicitação da Secretaria Escolar" Enabled="false" CssClass="txtObservacaoIUE"
                                                onkeyup="javascript:MaxLength(this, 100);" TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSEntreSecreEscol" ToolTip="Informe se enviará SMS para uma entrega da Secretaria Escolar" runat="server" 
                                            OnCheckedChanged="chkSMSEntreSecreEscol_CheckedChanged" AutoPostBack="true"    Text="Entrega"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSEntreEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSEntreEnvAuto" style="margin-left: 3px;"
                                            ToolTip="Informe se o envio é automático" runat="server" Enabled="false"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSEntre" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSEntre" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de entrega da Secretaria Escolar" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSOutroSecreEscol" ToolTip="Informe se enviará SMS para outras ocorrências da Secretaria Escolar" runat="server" 
                                            OnCheckedChanged="chkSMSOutroSecreEscol_CheckedChanged" AutoPostBack="true"    Text="Outros"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSOutroEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSOutroEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSOutro" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSOutro" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de outras ocorrências da Secretaria Escolar"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);" Enabled="false"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>
                            <li style="padding-left: 5px;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Gestão de Matrículas</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSReserVagas" ToolTip="Informe se enviará SMS para uma reserva de vagas" runat="server" 
                                            OnCheckedChanged="chkSMSReserVagas_CheckedChanged" AutoPostBack="true"    Text="Reserva Vagas"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSReserVagasEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSReserVagasEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSReserVagas" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSReserVagas" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de reserva de vagas" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSRenovMatri" ToolTip="Informe se enviará SMS para uma renovação de matrícula" runat="server" 
                                            OnCheckedChanged="chkSMSRenovMatri_CheckedChanged" AutoPostBack="true"    Text="Renovação"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSRenovMatriEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSRenovMatriEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSRenovMatri" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSRenovMatri" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de renovação de matrícula" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSMatriNova" ToolTip="Informe se enviará SMS para matrícula nova" runat="server" 
                                            OnCheckedChanged="chkSMSMatriNova_CheckedChanged" AutoPostBack="true"    Text="Matrícula Nova"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSMatriNovaEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSMatriNovaEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSMatriNova" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSMatriNova" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de matrícula nova" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>
                            <li style="padding-right: 10px; border-right: 1px solid #CCCCCC; margin-top: 10px;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Biblioteca Escolar</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSReserBibli" ToolTip="Informe se enviará SMS para uma reserva de biblioteca" runat="server" 
                                            OnCheckedChanged="chkSMSReserBibli_CheckedChanged" AutoPostBack="true"    Text="Reserva"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSReserBibliEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSReserBibliEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSReserBibli" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSReserBibli" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de reserva de biblioteca" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSEmpreBibli" ToolTip="Informe se enviará SMS para um empréstimo de biblioteca" runat="server" 
                                            OnCheckedChanged="chkSMSEmpreBibli_CheckedChanged" AutoPostBack="true"    Text="Empréstimo"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSEmpreBibliEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSEmpreBibliEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSEmpreBibli" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSEmpreBibli" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de empréstimo de biblioteca" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSDiverBibli" ToolTip="Informe se enviará SMS para diversas operações da biblioteca" runat="server" 
                                            OnCheckedChanged="chkSMSDiverBibli_CheckedChanged" AutoPostBack="true"    Text="Diversas"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSDiverBibliEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSDiverBibliEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSDiverBibli" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSDiverBibli" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS para diversas operações da biblioteca" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>
                            <li style="padding-left: 5px; margin-top: 10px;">
                                <ul>                            
                                    <li class="liPedagMatric" style="width: 100%;"><span style="font-size: 1.0em;text-transform:uppercase;font-weight:bold;">Controles Diversos</span></li>
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkSMSFaltaAluno" ToolTip="Informe se enviará SMS para uma falta do aluno" runat="server" 
                                            OnCheckedChanged="chkSMSFaltaAluno_CheckedChanged" AutoPostBack="true"    Text="Falta de Aluno"/>
                                    </li> 
                                    <li class="liClear" style="margin-left: 15px; margin-top: 2px;">
                                        <label for="ddlFlagSMSFaltaAlunoEnvAuto" style="float:left;" title="Envio automático">Env.Aut.:</label>
                                        <asp:DropDownList ID="ddlFlagSMSFaltaAlunoEnvAuto" style="margin-left: 3px;" Enabled="false"
                                            ToolTip="Informe se o envio é automático" runat="server"
                                            CssClass="ddlAlterPrazoEntreSolic">
                                            <asp:ListItem Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="margin-top: -13px; margin-left: -10px;">
                                        <label for="txtMsgSMSFaltaAluno" style="float:left;" title="Mensagem">Msg</label>
                                        <asp:TextBox ID="txtMsgSMSFaltaAluno" runat="server" style="margin-left: 3px; height: 28px; width: 240px; overflow-y: hidden;"
                                                ToolTip="Informe a Mensagem SMS de reserva de vagas" Enabled="false"
                                                CssClass="txtObservacaoIUE" onkeyup="javascript:MaxLength(this, 100);"
                                                TextMode="MultiLine"></asp:TextBox>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">                     
                                    <ContentTemplate>
                                    <li class="liRecuperacao liClear" style="margin-left: -5px; margin-top: 4px;">
                                        <asp:CheckBox ID="chkEnvioSMS" ToolTip="Informe se usuários enviarão SMS" runat="server" 
                                            OnCheckedChanged="chkEnvioSMS_CheckedChanged" AutoPostBack="true" Text="Msg SMS"/>
                                        <label style="margin-left:13px;" title="Quantidade máxima de SMS Mês">Qtd Máx</label>                                        
                                        <label style="margin-left:78px;" title="Quantidade máxima de SMS Mês">Qtd Máx</label>
                                        <label style="margin-left:55px;" title="Quantidade máxima de SMS Mês">Qtd Máx</label>
                                    </li> 
                                    <li class="liClear" style="margin-left: 20px; margin-top: 7px;">
                                        <ul>
                                            <li>
                                                <label style="float:left;" title="Quantidade máxima de SMS Mês - Funcionário">Funcionário</label>                                        
                                                <asp:TextBox ID="txtQtdMaxFunci" runat="server" style="margin-left: 3px;"
                                                ToolTip="Informe a quantidade máxima de SMS no mês para funcionário" Enabled="false"
                                                CssClass="txtQtdeMaxSMS"></asp:TextBox>
                                            </li>
                                            <li style="margin-left: 20px;">
                                                <label style="float:left;" title="Quantidade máxima de SMS Mês - Responsável">Responsável</label>                                        
                                                <asp:TextBox ID="txtQtdMaxRespo" runat="server" style="margin-left: 3px;"
                                                ToolTip="Informe a quantidade máxima de SMS no mês para responsável" Enabled="false"
                                                CssClass="txtQtdeMaxSMS"></asp:TextBox>
                                            </li>
                                            <li style="margin-left: 20px;">
                                                <label style="float:left;" title="Quantidade máxima de SMS Mês - Outros">Outros</label>                                        
                                                <asp:TextBox ID="txtQtdMaxOutros" runat="server" style="margin-left: 3px;"
                                                ToolTip="Informe a quantidade máxima de SMS no mês para outros" Enabled="false"
                                                CssClass="txtQtdeMaxSMS"></asp:TextBox>
                                            </li>
                                        </ul>
                                    </li>
                                    <li class="liClear" style="margin-left: 20px; margin-top: 2px;">
                                        <ul>
                                            <li>
                                                <label style="float:left;" title="Quantidade máxima de SMS Mês - Professor">Professor</label>                                        
                                                <asp:TextBox ID="txtQtdMaxProfe" runat="server" style="margin-left: 15px;"
                                                ToolTip="Informe a quantidade máxima de SMS no mês para professor" Enabled="false"
                                                CssClass="txtQtdeMaxSMS"></asp:TextBox>
                                            </li>
                                            <li style="margin-left: 20px;">
                                                <label style="float:left;" title="Quantidade máxima de SMS Mês - Aluno">Aluno</label>                                        
                                                <asp:TextBox ID="txtQtdMaxAluno" runat="server" style="margin-left: 31px;"
                                                ToolTip="Informe a quantidade máxima de SMS no mês para aluno" Enabled="false"
                                                CssClass="txtQtdeMaxSMS"></asp:TextBox>
                                            </li>
                                        </ul>
                                    </li>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ul>
                            </li>

                        </ul>
                    </li>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </ul>
            </div>
        </div>
    </li>
</ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".campoNumericoEndIIE").mask("?9999");
        $(".txtCEPIIE").mask("99999-999");
        $(".txtHorLimiteIIE").mask("99:99");
        $('.txtHorarioFuncManha').mask("99:99");
        $('.txtHorarioFuncTarde').mask("99:99");
        $('.txtHorarioFuncNoite').mask("99:99");
        $('.txtQtdeDiasEntreSolic').mask("?99");
        $('.txtNumIniciSolicAuto').mask("?9999999");
        $(".campoNumerico").mask("?99");
        $("input.campoData").datepicker();
        $(".campoData").mask("99/99/9999");
        $(".txtQtdeMaxSMS").mask("?999");
        $(".txtCPFRespon").mask("999.999.999-99");
        $(".campoHora").mask('99:99:99');  
    });

    $(document).ready(function () {
        $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $('.txtQtdeDiasEntreSolic').mask("?99");
        $(".campoNumerico").mask("?99");
        $(".campoNumericoEndIIE").mask("?9999");
        $(".txtCNPJIUE").mask("99.999.999/9999-99");
        $(".txtTelefoneIIE").mask("(99) 9999-9999");
        $(".txtCEPIIE").mask("99999-999");
        $(".txtHorLimiteIIE").mask("99:99");
        $('.txtHorarioFuncManha').mask("99:99");
        $('.txtHorarioFuncTarde').mask("99:99");
        $('.txtHorarioFuncNoite').mask("99:99");
        $('.txtNumControleIIE').mask("?9999999999999999");
        $('.txtNumIniciSolicAuto').mask("?9999999");
        $(".txtQtdeMaxSMS").mask("?999");
        $(".txtCPFRespon").mask("999.999.999-99");

        $(".ddlTipoCtrlDescr").click(function () {

            if ($(".ddlTipoCtrlDescr").val() == "I") {
                $("#txtTipoCtrlNossaHisto").val("Instituição de Ensino");
                $("#txtTipoCtrlPropoPedag").val("Instituição de Ensino");
            }
            else {
                $("#txtTipoCtrlNossaHisto").val("Unidade Escolar");
                $("#txtTipoCtrlPropoPedag").val("Unidade Escolar");
            }

        });

        function preencheInformacoesUnidade() {
            $("#txtInstituicaoQS").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoNH").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoPP").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoFF").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoPM").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoSE").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoBE").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoMS").val($(".txtInstitEnsino").val());
            $("#txtInstituicaoCO").val($(".txtInstitEnsino").val());

            $("#txtCodIdenticacaoQS").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoNH").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoPP").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoFF").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoPM").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoSE").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoBE").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoMS").val($(".txtSiglaIUE").val());
            $("#txtCodIdenticacaoCO").val($(".txtSiglaIUE").val());

            $("#txtCNPJQS").val($(".txtCNPJIUE").val());
            $("#txtCNPJNH").val($(".txtCNPJIUE").val());
            $("#txtCNPJPP").val($(".txtCNPJIUE").val());
            $("#txtCNPJFF").val($(".txtCNPJIUE").val());
            $("#txtCNPJPM").val($(".txtCNPJIUE").val());
            $("#txtCNPJSE").val($(".txtCNPJIUE").val());
            $("#txtCNPJBE").val($(".txtCNPJIUE").val());
            $("#txtCNPJMS").val($(".txtCNPJIUE").val());
            $("#txtCNPJCO").val($(".txtCNPJIUE").val());
        }

        $(".rblInforCadastro").click(function () {
            preencheInformacoesUnidade();
            $(".rblInforControle :checked").removeAttr('checked');

            if ($(":checked").val() == "1" || $(":checked").val() == "N" || $(":checked").val() == "on" || $(":checked").val() == "") {
                $(".tabDadosCadas").show();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $("#rblInforCadastro_0").attr("checked", "checked");
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "visible");           
            }
            if ($(":checked").val() == "2") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").show();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
                txtQuemSomos.AdjustControl();
            }
            if ($(":checked").val() == "3") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").show();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
                txtNossaHisto.AdjustControl();
            }
            if ($(":checked").val() == "4") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").show();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
                txtPropoPedag.AdjustControl();
            }
        });

        $(".rblInforControle").click(function () {
            preencheInformacoesUnidade();
            $(".rblInforCadastro :checked").removeAttr('checked');

            if ($(":checked").val() == "5" || $(":checked").val() == "N" || $(":checked").val() == "on" || $(":checked").val() == "") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").show();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $("#rblInforControle_0").attr("checked", "checked");
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
            else if ($(":checked").val() == "6") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").show();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
            else if ($(":checked").val() == "7") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").show();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
            else if ($(":checked").val() == "8") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").show();
                $(".tabMensaSMS").hide();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
            else if ($(":checked").val() == "9") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").hide();
                $(".tabContabil").show();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
            else if ($(":checked").val() == "10") {
                $(".tabDadosCadas").hide();
                $(".tabQuemSomos").hide();
                $(".tabNossaHisto").hide();
                $(".tabPropoPedag").hide();
                $(".tabPedagMatric").hide();
                $(".tabFrequFunci").hide();
                $(".tabSecreEscol").hide();
                $(".tabBibliEscol").hide();
                $(".tabMensaSMS").show();
                $(".tabContabil").hide();
                $(".divMensagCamposObrig").css("visibility", "collapse");
            }
        });
    });
</script>
</asp:Content>