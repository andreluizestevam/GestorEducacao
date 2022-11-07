<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformacoesEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.InformacoesEscola" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informações da Unidade Atual</title>
    <link href="../Library/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="../Library/CSS/intern.css" rel="stylesheet" type="text/css" />
    <script src="../Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="../Library/JS/jquery.maskMoney.0.2.js" type="text/javascript"></script>
    <style type="text/css">        
        #divMeuPerfilContent {width: 875px;}
        .ulDados {margin-top:0px !important; width: 870px;}

        /*--> CSS LIs*/
        .liUnidadeInEs { border-right: 1px solid #F0F0F0; margin-top: 10px; padding-right: 6px; width: 374px;}
        .liUnidade2 { margin-top: 10px; }
        .liClassificacao { float: right !important; }
        .liMatriz { float: right !important; }
        .liNucleo { margin-left: 15px; }
        .liRazaoSocial { clear: both; }
        .liTipoUnidade { clear: both; }
        .liCNPJ { float: right !important; }
        .liINEP { clear: both; }
        .liInscMunicipal { clear: both; }
        .liInscEstadual { clear: both; }
        .liControle { clear: both; margin-top: 20px; }
        .liAta { margin: 20px 0 0 11px; }
        .liContato { margin: 20px 0 0 11px; width: 328px;}
        .liWebSite { float: right !important; }
        .liDtDecreto { clear: both; }
        .liSituacao { margin: 20px 0 0 11px; }
        .liComplemento { clear: both; }
        .liCidade { clear: both; }
        .liDtCadastro { margin-left: 5px; }
        .liDtStatus { margin-left: 5px; }
        .liObservacao { clear: both; margin: -60px 0 0 143px; } 
        .liFax { clear: both; }
        .liMEC{ margin-top:1px !important;}
        .liNumDecreto{ margin-top:1px !important;}
        .liTelefone{ margin-top:1px !important;}
        .liEmail{ margin-top:1px !important;}    
        .liUnidadeInEs li { margin-top: -7px; }
        .liUnidade2 li { margin-top: -7px; }
        .liControle li { margin-top: -7px; }
        .liAta li { margin-top: -7px; }
        .liContato li { margin-top: -7px; } 
    
        /*--> CSS DADOS */
        .fldFoto { border: none; }
        .fldsUnidade { padding: 5px 5px 0 9px; }
        .fldSituacao { padding: 5px 5px 0 9px; }
        .lblNomeUnidade { font-weight: bold; text-transform: uppercase; }
        .imgEnd { margin: 14px 0 0 5px; }    
        .txtNome { width: 210px; }
        .txtRazaoSocial { width: 210px; }
        .txtSigla { width: 38px; }
        .txtCNPJ { width: 98px; }
        .campoTelefone { width: 74px; }
        .txtLogradouro { width: 225px; }
        .txtComplemento { width: 95px; }
        .ddlBairro { width: 170px; }
        .ddlCidade { width: 195px; }
        .txtCEP { width: 55px; }
        .txtObservacao { width: 404px; height: 44px; }
        .txtEmail { width: 143px; }
        .txtWebSite { width: 150px; }
        .tipoValor { width: 50px; }
        .campoNumerico{ width: 30px;}
        .titulo
        {
            font-size: 1.1em;
            font-weight: bold;
        }
        .ddlTipoControle{width: 70px;}
        .ddlPeriodicidade{ width: 70px;}    
        #divInformacoesEscolaContainer #divRodapeIE
        {
            margin-top: 10px;
            float: right;
        }
        #divInformacoesEscolaContainer #imgLogoGestorIE
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtIE
        {
            float: left;
            margin-top: 10px;
            width: 225px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFechar
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 2px;
        }
        #imgUnid
        {
        	height: 80px;
        	width: 130px;
        }
        .ddlNucleoIE { width: 75px; }
        .ddlTipoUnidadeIE { width: 150px; }
        .ddlClassificacaoIE { width: 100px; }
        
    </style>
</head>
<body>
    <div id="divInformacoesEscolaContainer">
        <form id="frmInformacoesEscola" runat="server">
        <div id="divInformacoesEscolaContent" runat="server">
            <ul id="ulDados1" class="ulDados">
            <li><ul>
                <li class="liFoto">                    
                    <label title="Foto Unidade">Foto Unidade</label>
                    <asp:Image ID="imgUnid" runat="server" ToolTip="Imagem da Unidade Ativa" />
                </li>
            </ul></li>
            
            <li class="liUnidadeInEs"><ul>
                <li id="liNome" class="liNome">
                    <label for="txtNome" title="Nome">Nome</label>
                    <asp:TextBox ID="txtNome" Enabled="False" 
                        ToolTip="Informe o Nome da Unidade"
                        CssClass="txtNome" MaxLength="80" runat="server"></asp:TextBox>                    
                </li>
                
                <li id="liSigla" class="liSigla">
                    <label for="txtSigla" title="Sigla">Sigla</label>
                    <asp:TextBox ID="txtSigla" Enabled="False"
                        ToolTip="Informe a Sigla"
                        CssClass="txtSigla" MaxLength="5" runat="server"></asp:TextBox>                    
                </li>
            
                <li id="liCNPJ" class="liCNPJ">
                    <label for="txtCNPJ" title="CNPJ">CNPJ</label>
                    <asp:TextBox ID="txtCNPJ" Enabled="False"
                        ToolTip="Informe o CNPJ"
                        CssClass="txtCNPJ" runat="server"></asp:TextBox>                    
                </li>
                
                <li id="liRazaoSocial" class="liRazaoSocial">
                    <label for="txtRazaoSocial" title="Razão Social">Raz&atilde;o Social</label>
                    <asp:TextBox ID="txtRazaoSocial" Enabled="False"
                        ToolTip="Informe a Razão Social"
                        CssClass="txtRazaoSocial" MaxLength="80" runat="server"></asp:TextBox>                    
                </li>
                
                <li id="liNucleo" class="liNucleo">
                    <label for="ddlNucleo" title="Núcleo">N&uacute;cleo</label>
                    <asp:DropDownList ID="ddlNucleo" CssClass="ddlNucleoIE" Enabled="False" ToolTip="Selecione o Núcleo" runat="server">
                    </asp:DropDownList>
                </li>
                
                <li id="liMatriz" class="liMatriz">
                    <label for="ddlMatriz" title="Matriz">Matriz?</label>
                    <asp:DropDownList ID="ddlMatriz" Enabled="False" ToolTip="Informe se a Unidade é Matriz" runat="server">
                        <asp:ListItem Value="N">N&atilde;o</asp:ListItem>
                        <asp:ListItem Value="S">Sim</asp:ListItem>
                    </asp:DropDownList>
                </li>
                
                <li id="liTipoUnidade" class="liTipoUnidade">
                    <label for="ddlTipoUnidade" title="Tipo Unidade">Tipo Unidade</label>
                    <asp:DropDownList ID="ddlTipoUnidade" CssClass="ddlTipoUnidadeIE" Enabled="False"
                        ToolTip="Selecione o Tipo Unidade" runat="server">
                    </asp:DropDownList>                    
                </li>
                
                <li id="liClassificacao" class="liClassificacao">
                    <label for="ddlClassificacao" title="Classificação">Classifica&ccedil;&atilde;o</label>
                    <asp:DropDownList ID="ddlClassificacao" CssClass="ddlClassificacaoIE" Enabled="False" runat="server" ToolTip="Selecione a Classificação">
                    </asp:DropDownList>                    
                </li>
            </ul></li>
            
            <li id="liTitEndereco" class="liTitEndereco">
                <img id="imgEnd" class="imgEnd" src="../../../../Library/IMG/Gestor_ImgEndereco.png" alt="endereço" />
            </li>
            
            <li class="liUnidade2"><ul>
                <li id="liCEP" class="liCEP">
                    <label for="txtCEP" title="CEP">CEP</label>
                    <asp:TextBox ID="txtCEP" Enabled="False"
                        ToolTip="Informe o CEP"
                        CssClass="txtCEP" runat="server"></asp:TextBox>
                </li>
                <li id="liLogradouro" class="liLogradouro">
                    <label for="txtLogradouro" title="Endereço">Endere&ccedil;o</label>
                    <asp:TextBox ID="txtLogradouro" Enabled="False"
                        ToolTip="Informe o Endereço"
                        CssClass="txtLogradouro" MaxLength="60" runat="server"></asp:TextBox>                    
                </li>
                <li id="liComplemento" class="liComplemento">
                    <label for="txtComplemento" title="Complemento">Complemento</label>
                    <asp:TextBox ID="txtComplemento" Enabled="False"
                        ToolTip="Informe o Complemento"
                        CssClass="txtComplemento" MaxLength="30" runat="server"></asp:TextBox>
                </li>
                <li id="liBairro" class="liBairro">
                    <label for="ddlBairro" title="Bairro">Bairro</label>
                    <asp:DropDownList ID="ddlBairro" Enabled="False"
                        ToolTip="Selecione o Bairro"
                        CssClass="ddlBairro" runat="server">
                    </asp:DropDownList>                    
                </li>
                <li id="liCidade" class="liCidade">
                    <label for="ddlCidade" title="Cidade">Cidade</label>
                    <asp:DropDownList ID="ddlCidade" Enabled="False"
                        ToolTip="Selecione a Cidade"
                        CssClass="ddlCidade" runat="server">
                    </asp:DropDownList>                    
                </li>
                <li id="liUF" class="liUF">
                    <label for="ddlUF" title="UF">UF</label>
                    <asp:DropDownList ID="ddlUF" Enabled="False"
                        ToolTip="Selecione a UF"
                        CssClass="ddlUF" runat="server">
                    </asp:DropDownList>                    
                </li>
            </ul></li>
                
            <li id="liControle" class="liControle">
            <fieldset id="fldControle" class="fldsUnidade">
            <legend>N° Controle</legend>
            <ul>
                <li id="liMEC" class="liMEC">
                    <label for="txtMEC" title="Número do MEC">MEC N°</label>
                    <asp:TextBox ID="txtMEC" Enabled="False"
                        ToolTip="Informe o Número do MEC"
                        CssClass="txtNumControle" runat="server"></asp:TextBox>                    
                </li>
                <li id="liInscMunicipal" class="liInscMunicipal">
                    <label for="txtInscMunicipal" title="Inscrição Municipal">Insc. Municipal</label>
                    <asp:TextBox ID="txtInscMunicipal" Enabled="False"
                        ToolTip="Informe a Inscrição Municipal"
                        CssClass="txtNumControle" runat="server"></asp:TextBox>
                </li>
                <li id="liINEP" class="liINEP">
                    <label for="txtINEP" title="Número do INEP">INEP N°</label>
                    <asp:TextBox ID="txtINEP" Enabled="False"
                        ToolTip="Informe o Número do INEP"
                        CssClass="txtNumControle" runat="server"></asp:TextBox>                    
                </li>
                <li id="liInscEstadual" class="liInscEstadual">
                    <label for="txtInscEstadual" title="Inscrição Estadual">Insc. Estadual</label>
                    <asp:TextBox ID="txtInscEstadual" Enabled="False"
                        ToolTip="Informe a Inscrição Estadual"
                        CssClass="txtNumControle" runat="server"></asp:TextBox>
                </li>
            </ul>
            </fieldset>
            </li>
                
            <li id="liAta" class="liAta">
            <fieldset id="fldAta" class="fldsUnidade">
            <legend>Ata</legend>
            <ul>
                <li id="liNumDecreto" class="liNumDecreto">
                    <label for="txtNumDecreto" title="Número do Decreto">N° Decreto</label>
                    <asp:TextBox ID="txtNumDecreto" Enabled="False"
                        ToolTip="Informe o Número do Decreto"
                        CssClass="txtNumControle" runat="server"></asp:TextBox>
                </li>
                <li id="liDtDecreto" class="liDtDecreto">
                    <label for="txtDtDecreto" title="Data do Decreto">Data</label>
                    <asp:TextBox ID="txtDtDecreto" Enabled="False"
                        ToolTip="Informe a Data do Decreto"
                        CssClass="campoData" runat="server"></asp:TextBox>
                </li>
            </ul>
            </fieldset>
            </li>
                
            <li id="liContato" class="liContato">
            <fieldset id="fldContato" class="fldsUnidade">
            <legend>Informa&ccedil;&otilde;es de Contato</legend>
            <ul>
                <li id="liTelefone" class="liTelefone">
                    <label for="txtTelefone" title="Telefone">Telefone</label>
                    <asp:TextBox ID="txtTelefone" Enabled="False"
                        ToolTip="Informe o Número do Telefone"
                        CssClass="campoTelefone" runat="server"></asp:TextBox>
                </li>
                
                <li id="liTelefone2" class="liTelefone">
                    <label for="txtTelefone2" title="Telefone 2">Telefone 2</label>
                    <asp:TextBox ID="txtTelefone2" Enabled="False"
                        ToolTip="Informe o Número do Telefone"
                        CssClass="campoTelefone" runat="server"></asp:TextBox>
                </li>
                
                <li id="liEmail" class="liEmail">
                    <label for="txtEmail" title="E-mail">E-mail</label>
                    <asp:TextBox ID="txtEmail" Enabled="False"
                        ToolTip="Informe o E-mail"
                        CssClass="txtEmail" MaxLength="60" runat="server"></asp:TextBox>
                </li>

                <li id="liFax" class="liFax">
                    <label for="txtFax" title="Fax">Fax</label>
                    <asp:TextBox ID="txtFax" Enabled="False"
                        ToolTip="Informe o Número do Fax"
                        CssClass="campoTelefone" runat="server"></asp:TextBox>
                </li>
                
                <li id="liWebSite" class="liWebSite">
                    <label for="txtWebSite" title="Web Site">Web Site</label>
                    <asp:TextBox ID="txtWebSite" Enabled="False"
                        ToolTip="Informe o Web Site"
                        CssClass="txtWebSite" MaxLength="60" runat="server"></asp:TextBox>
                </li>
            </ul>
            </fieldset>
            </li>
            
            <li id="liSituacao" class="liSituacao">
            <fieldset id="fldSituacao" class="fldSituacao">
            <legend>Situa&ccedil;&atilde;o</legend>
            <ul>
                <li id="liStatus" class="liStatus">
                    <label for="ddlStatus" title="Status">Status</label>
                    <asp:DropDownList ID="ddlStatus" Enabled="False" ToolTip="Selecione o Status" runat="server">
                        <asp:ListItem Value="A">Ativo</asp:ListItem>
                        <asp:ListItem Value="I">Inativo</asp:ListItem>
                    </asp:DropDownList>                    
                </li>
                
                <li id="liDtStatus" class="liDtStatus">
                    <label for="txtDtStatus" title="Data Status">Data Status</label>
                    <asp:TextBox ID="txtDtStatus" Enabled="false" 
                        ToolTip="Informe a Data Status"
                        CssClass="campoData" runat="server"></asp:TextBox>
                </li>
                
                <li id="liDtCadastro" class="liDtCadastro">
                    <label for="txtDtCadastro" title="Data Cadastro">Data Cadastro</label>
                    <asp:TextBox ID="txtDtCadastro"
                        ToolTip="Informe a Data de Cadastro"
                        CssClass="campoData" Enabled="false" runat="server"></asp:TextBox>
                </li>
            </ul>
            </fieldset>
            </li>
            
            <li id="liObservacao" class="liObservacao">
                <label for="txtObservacao" title="Observação">Observa&ccedil;&atilde;o</label>
                <asp:TextBox ID="txtObservacao" runat="server" Enabled="False"
                     ToolTip="Informe a Observação"
                     CssClass="txtObservacao" onkeyup="javascript:MaxLength(this, 150);"
                     TextMode="MultiLine"></asp:TextBox>
            </li>
        </ul>
        </div>
        <div id="divHelpTxtIE">
            <p id="pAcesso" class="pAcesso">
                Resultado da Informação da Unidade Atual.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeIE">
            <img id="imgLogoGestorIE" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>

    <script type="text/javascript">       
        jQuery(function ($) {
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoMoeda2").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoNumerico").mask("?99");
            $(".txtQtdItensAcervo").mask("?999999");
            $(".txtQtdDiasReserva").mask("?999999");
            $(".txtCNPJ").mask("99.999.999/9999-99");
            $(".txtCEP").mask("99999-999");
            $('.txtNumControle').mask("?9999999999999999");
            $('.txtConvenio').mask("?9999999");
            $(".campoTelefone").mask("?(99) 9999-9999");
        });

        $(document).ready(function () {
            $('#divInformacoesEscolaContainer #frmInformacoesEscola').ajaxForm({ target: '#divLoadShowInformacoesEscola', url: '/Componentes/InformacoesEscola.aspx' });
        });
    </script>
</body>
</html>
