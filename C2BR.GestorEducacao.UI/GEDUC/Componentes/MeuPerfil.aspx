<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeuPerfil.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.MeuPerfil" %>
<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Serviços - Meu Perfil</title>
    <link href="../Library/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="../Library/CSS/intern.css" rel="stylesheet" type="text/css" />
    <script src="../Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <style type="text/css">        
        #divMeuPerfilContent {width: 522px;}
        .ulDados{width: 520px;}
        .liClear{clear: both;}
        .liGeral{clear: both;}
        .liPhoto{margin-top: -115px;margin-left: 427px;}
        .fldPhoto{height: 105px;}
        .liTop{margin-top: 10px;}
        .G1Clear{margin-top: -5px !important;clear: both;}
        .G1{margin-top: -5px;margin-left: 10px;}
        .G2Clear{clear: both;}
        .G2{margin-left: 10px;}
        .G3Clear{margin-top: 10px;clear: both;}
        .G3{margin-top: 10px;margin-left: 30px;}
        .liHora{margin-left: 20px;}
        .ddlEnviarSMS{width: 45px;}
        .formAuxText1{padding-right: 2px;padding-left: 2px;padding-top: 12px;}
        .cbDiaAcesso{border: none; margin-top: -2px;}
        .cbDiaAcesso tr td label{margin-left: -7px;display: inline-table;font-size: 9px;}
        .liSms{margin-left: 202px;}
        .G4Clear{clear: both;margin-top: -32px;}
        .G4{margin-top: -32px;margin-left: 75px;}
        .G5{margin-top: -32px;margin-left: 20px !important;}
        .AcessosPermitidos{margin-top: -1px;clear:both;width:362px;}
        .G6{margin-left: 15px;}
        .G7Clear{clear: both;margin-top: 0px;}
        .G7{margin-top: 0px;margin-left: 10px;}
        #divMeuPerfilContainer #divRodape
        {
            margin-top: 10px;
            float: right;
        }
        #divMeuPerfilContainer #imgLogoGestor
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtMP
        {
            float: left;
            margin-top: 10px;
            width: 205px;
            color: #DF6B0D;
            font-weight: bold;
            clear:both;
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
        .vsAlterarSenhaMP { margin-top: -27px; margin-bottom:2px; height: 25px; overflow-y:auto;}
        .successMessageMP 
        { 
            background: #F1FFEF url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;                 
            border: 1px solid #D2DFD1;
            font-size: 15px;
            font-weight: bold;
            padding: 35px 10px 10px;
            text-align: center;
            width: 488px;
            float: left;
            margin-top: 10px;
            margin-bottom: 22px;
        }
        #divAlterarSenhaContent  input[type='password']
        {
            width: 105px;
        }
        #divAlterarSenhaContent  
        {
            float:left;
            width:510px;
            margin-top:5px;
            border-top: 2px solid #8B8989;
        }
        #ulAlterarSenhaForm
        {
        	margin-top: 5px;
        }
        #ulAlterarSenhaForm li
        {
        	float: left;
        }
        #btnSalvarMP
        {
        	margin-top: 38px;
        	margin-left: 45px;
        }
        #lblCabAtuSenha
        {
        	font-size: 1.1em;
        	color: #009ACD;        	
        }
        .withSeparatorServMP
        {
            margin-left: 35px;
            margin-bottom: 2px;
            float: left;
            width: 95px !important;
            height:30px !important;
        }
        #ulMeusLinks li a
        {
            font-size: 0.9em;
            margin-left: 4px;
            color: #EE7600;         
        }
        #ulMeusLinks li a:hover
        {
            text-decoration: underline;
        }
        #ulMeusLinks li img
        {
            margin-top: 0px;
            margin-bottom: 3px;
            width: 22px;
            height: 22px;
            margin-right: 4px;
        }
        #ulMeusLinks {margin-top: 5px; }
        #divMeusLinks {border-top: 2px solid #8B8989; margin-top: 10px; width:505px; padding-top: 5px; padding-left: 5px;}
        #divAlterarSenhaFormButtons {margin-bottom: 30px;}
        #ulMeusLinks span {position:absolute; width: 30px; margin-left: 2px;}
        #ddlTpUsu {width:85px;}
        .chkLocais input { float: left; }
        .chkLocais label { display: inline !important; margin-left:-4px;}
        .liAP1 { width:118px; margin-right:-1px !important;}
        #ulAPPrincipal li { margin-bottom: -3px; }
        #liServs 
        {
            width: 150px;
            float: left;
            margin-top: 4px;
            margin-left:-2px;	
        }
        #ulServs { margin-left: -1px; padding-top:4px; }
        #ulServs li { margin-bottom: 3px; margin-left:-3px; width: 100%; }
        #ulAPCabIntern li { width: 17px; }
        .cbDiaAcesso input[type="checkbox"] { width: 20px !important; }   
        .ddlUsuaCaixa { width: 45px; } 
    </style>
</head>
<body>
    <div id="divMeuPerfilContainer">
        <form id="frmMeuPerfil" runat="server">
        <div id="divMeuPerfilContent" runat="server">
            <ul id="ulDados" class="ulDados">
            <li class="liTop">
                <li class="G1Clear">
                    <label for="txtUnidade" title="Unidade/Escola">
                        Unidade de Origem</label>
                    <asp:DropDownList ID="ddlUnidade" runat="server" Enabled="False" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade/Escola">
                    </asp:DropDownList>
                </li>
                <li class="G1" style="margin-left: 15px;">
                    <label for="txtColaborador">
                        Login de Acesso</label>
                    <asp:TextBox ID="txtLogin" runat="server" Width="100px" MaxLength="20" ToolTip="Login de Acesso">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin"
                        ErrorMessage="Login deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>     
                <li class="G1" style="margin-left: 15px;">
                    <label for="ddlTpUsu">
                        Tipo de Usuário</label>
                    <asp:DropDownList ID="ddlTpUsu" runat="server" Enabled="False" ToolTip="Selecione o Tipo de Usuário">
                        <asp:ListItem Text="Funcionário" Value="F" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Responsável" Value="R"></asp:ListItem>
                    </asp:DropDownList>
                </li>   
                <li class="G1" style="margin-left: 15px;margin-right: 0px;">
                    <label for="ddlTpUsu">
                        Usr Caixa</label>
                    <asp:DropDownList ID="ddlUsuaCaixa" runat="server" Enabled="False" CssClass="ddlUsuaCaixa"
                        ToolTip="Selecione se Usuário Caixa" >
                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </li>         
                <li class="G3Clear">
                    <label for="txtColaborador">
                        Nome completo do Usuário</label>
                    <asp:DropDownList ID="ddlCol" runat="server" Enabled="False" CssClass="campoNomePessoa" ToolTip="Selecione o Colaborador">
                    </asp:DropDownList>
                </li>
                <li class="G3">
                    <label for="txtColaborador">
                        Apelido</label>
                    <asp:TextBox ID="txtApelido" runat="server" Enabled="false" Width="100px" ToolTip="Apelido do Colaborador">
                    </asp:TextBox>
                </li>
                <li class="G2Clear">
                    <label for="txtDepartamento">
                        Localização</label>
                    <asp:TextBox ID="txtDepartamento" runat="server" Enabled="false" CssClass="txtDptoCurso" ToolTip="Departamento do Colaborador">
                    </asp:TextBox>
                </li>
                <li style="margin-left: 33px;">
                    <label for="txtFuncao">
                        Atividade</label>
                    <asp:TextBox ID="txtFuncao" runat="server" Enabled="false" Width="120px" ToolTip="Função do Colaborador">
                    </asp:TextBox>
                </li>
                <li style="clear: both;">
                    <label for="txtEmail">
                        Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtEmail" Enabled="false" ToolTip="E-Mail do Colaborador">
                    </asp:TextBox>
                </li>
                <li style="margin-left: 37px;">
                    <label for="txtCelular">
                        Celular</label>
                    <asp:TextBox ID="txtCelular" runat="server" Enabled="false" CssClass="campoTelefone" ToolTip="Celular do Colaborador">
                    </asp:TextBox>
                </li>
                <li>
                    <label for="txtTelefone">
                        Telefone</label>
                    <asp:TextBox ID="txtTelefone" runat="server" Enabled="false" CssClass="campoTelefone" ToolTip="Telefone do Colaborador">
                    </asp:TextBox>
                </li> 
                <li id="liPhoto" class="liPhoto">
                    <fieldset id="fldPhoto" class="fldPhoto">
                        <asp:Image ID="imgColMP" runat="server" ToolTip="Imagem do Colaborador" />
                    </fieldset>
                </li> 
                <li class="AcessosPermitidos">
                    <fieldset>
                        <legend>&nbsp;Acessos Permitidos&nbsp;</legend>
                        <ul id="ulAPPrincipal">
                            <li style="margin-bottom: 4px;color: black; font-weight: bold;margin-right:0 !important;">
                                <ul id="ulAPCabIntern">
                                    <li style="width:114px;text-align: center">
                                        <label>LOCAIS</label>    
                                    </li>
                                    <li>
                                        <label>SEG</label>    
                                    </li>
                                    <li>  
                                        <label>TER</label>    
                                    </li>
                                    <li style="margin-left:-2px;">
                                        <label>QUA</label>    
                                    </li>
                                    <li style="margin-left:2px;">
                                        <label>QUI</label>    
                                    </li>
                                    <li>
                                        <label>SEX</label>    
                                    </li>
                                    <li>
                                        <label>SAB</label>    
                                    </li>
                                    <li>
                                        <label>DOM</label>    
                                    </li>
                                    <li style="width: 75px; text-align: center; margin-left: 5px;">
                                        <label>HORÁRIO</label>    
                                    </li>
                                </ul>
                            </li>
                            <li style="margin-right: 0px;">
                                <ul>
                                    <li class="liAP1">
                                        <asp:CheckBox CssClass="chkLocais" runat="server" TextAlign="Right" Enabled="false" Text="Portal Educação" ID="chkGestorEducacao" Checked="true" />                                        
                                    </li>
                                    <li>
                                        <asp:CheckBoxList Enabled="False" ID="cbDiaAcesso" runat="server" RepeatColumns="7" CssClass="cbDiaAcesso" ToolTip="Marque a opção desejada"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="" Value="SG" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="" Value="TR" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QR" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QN" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SX" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                            <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="txtHoraAcessoI" Enabled="false" runat="server" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                        <span>A</span>
                                        <asp:TextBox ID="txtHoraAcessoF" Enabled="false" runat="server" AutoPostBack="True" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li  style="margin-right: 0px;">
                                <ul>
                                    <li class="liAP1">
                                        <asp:CheckBox CssClass="chkLocais" runat="server" TextAlign="Right" Enabled="false" Text="Portal Aluno" ID="CheckBox1" />                                        
                                    </li>
                                    <li>
                                        <asp:CheckBoxList Enabled="False" ID="CheckBoxList1" runat="server" RepeatColumns="7" CssClass="cbDiaAcesso" ToolTip="Marque a opção desejada"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="" Value="SG"></asp:ListItem>
                                            <asp:ListItem Text="" Value="TR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QN"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SX"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                            <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                        <span>A</span>
                                        <asp:TextBox ID="TextBox2" runat="server" Enabled="false" AutoPostBack="True" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li  style="margin-right: 0px;">
                                <ul>
                                    <li class="liAP1">
                                        <asp:CheckBox CssClass="chkLocais" runat="server" TextAlign="Right" Enabled="false" Text="Portal Professor" ID="CheckBox2" />                                        
                                    </li>
                                    <li>
                                        <asp:CheckBoxList Enabled="False" ID="CheckBoxList2" runat="server" RepeatColumns="7" CssClass="cbDiaAcesso" ToolTip="Marque a opção desejada"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="" Value="SG"></asp:ListItem>
                                            <asp:ListItem Text="" Value="TR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QN"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SX"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                            <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="TextBox3" runat="server" Enabled="false" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                        <span>A</span>
                                        <asp:TextBox ID="TextBox4" runat="server" Enabled="false" AutoPostBack="True" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                            <li style="margin-right: 0px;">
                                <ul>
                                    <li class="liAP1">
                                        <asp:CheckBox CssClass="chkLocais" TextAlign="Right" runat="server" Enabled="false" Text="Portal Responsável" ID="CheckBox3" />                                        
                                    </li>
                                    <li>
                                        <asp:CheckBoxList Enabled="False" ID="CheckBoxList3" runat="server" RepeatColumns="7" CssClass="cbDiaAcesso" ToolTip="Marque a opção desejada"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="" Value="SG"></asp:ListItem>
                                            <asp:ListItem Text="" Value="TR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QR"></asp:ListItem>
                                            <asp:ListItem Text="" Value="QN"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SX"></asp:ListItem>
                                            <asp:ListItem Text="" Value="SB"></asp:ListItem>
                                            <asp:ListItem Text="" Value="DG"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="TextBox5" runat="server" Enabled="false" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Acesso Inicio"></asp:TextBox>
                                        <span>A</span>
                                        <asp:TextBox ID="TextBox6" runat="server" Enabled="false" AutoPostBack="True" CssClass="campoHora" Width="30px" ToolTip="Informe a Hora de Acesso Final"></asp:TextBox>
                                    </li>
                                </ul>
                            </li>
                        </ul>                                                
                    </fieldset>
                </li>
                <li id="liServs">
                    <fieldset style="height: 96px;">
                        <ul id="ulServs">                            
                            <li>
                                <asp:CheckBox CssClass="chkLocais" ID="chkBiometria" TextAlign="Right" runat="server" Enabled="false" Text="Faz Captura de Digitais"/>                                
                            </li>                          
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkLocais" ID="chkFreqManu" TextAlign="Right" runat="server" Enabled="False" Text="Faz Manutenção Frequência"/>                                                     
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkLocais" ID="chkBibliReserva" TextAlign="Right" runat="server" Enabled="False" Text="Faz Reserva em Biblioteca"/>                                
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkLocais" ID="chkAltLoginSMS" TextAlign="Right" runat="server" Enabled="False" Text="Avisar Alter. Login por SMS"/>                                
                            </li>
                            <li class="G2Clear">
                                <asp:CheckBox CssClass="chkLocais" ID="chkSerEnvioSMS" TextAlign="Right" runat="server" Enabled="False" Text="Utilizar Serviço de Msg SMS"/>                                
                            </li>
                        </ul>                                                
                    </fieldset>
                </li>
                <li class="G2Clear" style="margin-top: 5px;">
                    <label for="ddlClaUsu">
                        Categoria de Usuário</label>
                    <asp:DropDownList ID="ddlClaUsu" runat="server" Enabled="False" Width="93px" ToolTip="Selecione o Tipo de Usuário">
                        <asp:ListItem Text="Comum" Value="C" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Master" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Suporte" Value="S"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="G2" style="margin-top: 5px; margin-left:283px;">
                    <label for="ddlStatus">
                        Status</label>
                    <asp:DropDownList ID="ddlStatus" Enabled="False" runat="server" Width="60px" ToolTip="Selecione o Status">
                        <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="G2" style="margin-top: 5px;">
                    <label for="txtDtSituacao">
                        Data Status</label>
                    <asp:TextBox ID="txtDtSituacao" Enabled="False" runat="server" CssClass="campoData" ToolTip="Informe a Data de Situação"></asp:TextBox>
                </li>                                                
            </li>            
            </ul>
        </div>
        <div id="divAlterarSenhaContent" runat="server">
            <ul id="ulAlterarSenhaForm">
                <li style="margin-bottom: 3px;">
                    <label id="lblCabAtuSenha">Atualizar Senha</label>
                    <span style="margin-bottom: 5px;">Por favor, informe os dados nos campos abaixo para atualizar sua senha.</span>
                </li>
                <li style="clear:both;">
                    <label for="txtSenhaAtual">Senha Atual</label>
                    <asp:TextBox runat="server" ID="txtSenhaAtual" ValidationGroup="vgAlterSenha" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rvfTxtSenhaAtual" ValidationGroup="vgAlterSenha" ErrorMessage="*** Por Favor preencha o campo Senha Atual ***" ControlToValidate="txtSenhaAtual" runat="server" Display="None" />
                </li>
                <li style="margin-left: 35px;">
                    <label for="txtNovaSenha">Nova Senha</label>
                    <asp:TextBox runat="server" ID="txtNovaSenha" ValidationGroup="vgAlterSenha" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvTxtNovaSenha" ValidationGroup="vgAlterSenha" ErrorMessage="*** Por Favor preencha o campo Nova Senha ***" ControlToValidate="txtNovaSenha" runat="server" Display="None" />
                </li>
                <li style="margin-left: 5px;">
                    <label for="txtConfirmaNovaSenha">Confirmar Nova Senha</label>
                    <asp:TextBox runat="server" ID="txtConfirmaNovaSenha" TextMode="Password" ValidationGroup="vgAlterSenha" />
                    <asp:RequiredFieldValidator ID="rfvtxtConfirmaNovaSenha" ErrorMessage="*** Por Favor preencha o confirmar Nova Senha ***" ValidationGroup="vgAlterSenha" ControlToValidate="txtConfirmaNovaSenha" runat="server" Display="None" />
                    <asp:CompareValidator ID="cvNovaSenha" ControlToValidate="txtConfirmaNovaSenha" ControlToCompare="txtNovaSenha" runat="server" ValidationGroup="vgAlterSenha" ErrorMessage="Os campos Nova Senha e Confirmar Nova Senha não coincidem." Display="None"></asp:CompareValidator>
                </li>                
            </ul>    
            <div id="divAlterarSenhaFormButtons">
                <asp:Button Text="Alterar Senha" runat="server" ID="btnSalvarMP" onclick="btnSalvar_Click" ValidationGroup="vgAlterSenha" />                
            </div>        
            <asp:ValidationSummary ID="vsAlterarSenha" runat="server" CssClass="vsAlterarSenhaMP" ValidationGroup="vgAlterSenha" />
                        
        </div>
        <div class="successMessageMP" id="divSucessMessage" runat="server">
            <asp:Label ID="lblMensagem" runat="server" Visible="false" />
        </div>
        <div id="divMeusLinks" style="clear:both;">
            <span>Por favor, Clique em um dos links abaixo para mais informações sobre o Meu Perfil.</span>
            <ul id="ulMeusLinks">
                <li id="liMeusAcessos" class="withSeparatorServMP" style="margin-left:5px !important;" title="Clique para visualizar seus acessos.">
                    <asp:LinkButton ID="btnMeusAcessos" runat="server" Style="margin: 0 auto;" OnClick="btnMeusAcessos_Click" ToolTip="Meus Acessos">
                        <img src='/Library/IMG/Gestor_Admin.png' alt="Icone MEUS ACESSOS" title="Disponibiliza a visualização dos acessos do usuário." />
                        <asp:Label runat="server" ID="lblBtnMeusAcessos" Text="Meus ACESSOS"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMinhasMsgs" class="withSeparatorServMP" title="Clique para visualizar suas mensagens de SMS.">
                    <asp:LinkButton ID="btnMinhasMsgs" runat="server" Style="margin: 0 auto;" OnClick="btnMinhasMsgs_Click" ToolTip="Minhas Msgs">
                        <img src='/Library/IMG/Gestor_ServicosEnvioMsgSMS.png' alt="Icone MINHAS MSGS" title="Disponibiliza a visualização das mensagens do usuário." />
                        <asp:Label runat="server" ID="lblMinhasMsgs" Text="Minhas MENSAGENS"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMeusAtalhos" class="withSeparatorServMP" title="Clique para visualizar os seus atalhos.">
                    <asp:LinkButton ID="btnMeusAtalhos" runat="server" Style="margin: 0 auto;" OnClick="btnMeusAtalhos_Click" ToolTip="Meus Atalhos">
                        <img src='/Library/IMG/Gestor_AIM.png' alt="Icone MEUS ATALHOS" Style="padding-left:2px;margin-right: 2px;" title="Disponibiliza a visualização dos atalhos do usuário." />
                        <asp:Label runat="server" ID="lblMeusAtalhos" Text="Meus ATALHOS"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMinhasInfor" class="withSeparatorServMP" title="Clique para visualizar as suas informações.">
                    <asp:LinkButton ID="btnMinhasInformacoes" runat="server" Style="margin: 0 auto;" OnClick="btnMinhasInformacoes_Click" ToolTip="Meus Atalhos">
                        <img src='/Library/IMG/Gestor_ServicosMeuPerfil.png' style="margin-top: 2px;margin-right: 5px;" alt="Icone MINHAS INFORMAÇÕES" title="Disponibiliza a visualização das informações do usuário." />
                        <asp:Label runat="server" ID="Label1" Text="Minhas INFORMAÇÕES"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMeusContat" class="withSeparatorServMP" style="margin-top: 3px; margin-left:5px !important;" title="Clique para visualizar os seus contatos.">
                    <asp:LinkButton ID="btnMeusContat" runat="server" Style="margin: 0 auto;" OnClick="btnMeusContat_Click" ToolTip="Meus Contatos">
                        <img src='/Library/IMG/Gestor_Beneficios.png' alt="Icone MEUS CONTATOS" title="Disponibiliza a visualização dos contatos do usuário." />
                        <asp:Label runat="server" ID="Label2" Text="Meus CONTATOS"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMinhasAvalia" class="withSeparatorServMP" style="margin-top: 3px;" title="Clique para visualizar as suas avaliações.">
                    <asp:LinkButton ID="btnMinhasAvalia" runat="server" Style="margin: 0 auto;" OnClick="btnMeusAtalhos_Click" ToolTip="Minhas Avaliações">
                        <img src='/Library/IMG/Gestor_ServicosAgendaContatos.png' alt="Icone MINHAS AVALIAÇÕES" title="Disponibiliza a visualização das avaliações do usuário." />
                        <asp:Label runat="server" ID="Label3" Text="Minhas AVALIAÇÕES"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li id="liMinhaBiblioteca" class="withSeparatorServMP" style="margin-top: 3px;" title="Clique para visualizar a sua biblioteca.">
                    <asp:LinkButton ID="btnMinhaBiblioteca" runat="server" Style="margin: 0 auto;" OnClick="btnMinhaBiblioteca_Click" ToolTip="Minha Biblioteca">
                        <img src='/Library/IMG/Gestor_Academico.jpg' alt="Icone MINHA BIBLIOTECA" title="Disponibiliza a visualização da biblioteca do usuário." />
                        <asp:Label runat="server" ID="Label4" Text="Minha BIBLIOTECA"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>        
        <div id="divHelpTxtMP">
            <p id="pAcesso" class="pAcesso">
                Resultado do perfil do usuário logado.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>

    <script type="text/javascript">
        // Sobrescreve o metodo do asp.Net de PostBack
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;

                if (eventTarget == 'btnMeusAcessos' || eventTarget == 'btnMinhasMsgs' || eventTarget == 'btnMeusAtalhos' || eventTarget == 'btnMinhaBiblioteca'
                    || eventTarget == 'btnMinhasInformacoes' || eventTarget == 'btnMeusContat' || eventTarget == 'btnMinhasAvalia') {

                    var options = {
                        target: '#divLoadShowMeuPerfil', // target element(s) to be updated with server response 
                        url: '/Componentes/MeuPerfil.aspx'
                        //beforeSubmit: showRequest,  // pre-submit callback 
                        //success: showResponse,  // post-submit callback 
                        //error: requestError
                        // other available options: 
                        //url:       url         // override for form's 'action' attribute 
                        //type:      type        // 'get' or 'post', override for form's 'method' attribute 
                        //dataType:  null        // 'xml', 'script', or 'json' (expected server response type) 
                        //clearForm: true        // clear all form fields after successful submit 
                        //resetForm: true        // reset the form after successful submit 

                        // $.ajax options can be used here too, for example: 
                        //timeout:   3000 
                    };

                    $(theForm).ajaxSubmit(options);
                }
                else {
                    theForm.submit();
                }
            }
        }

        jQuery(function ($) {
            $(".campoHora").mask("99:99");
        });
        jQuery(function ($) {
            $(".campoCep").mask("?99999-999");
        });
        jQuery(function ($) {
            $(".campoTelefone").mask("?(99) 9999-9999");
        });

        $(document).ready(function () {
            $('#divMeuPerfilContainer #frmMeuPerfil').ajaxForm({ target: '#divLoadShowMeuPerfil', url: '/Componentes/MeuPerfil.aspx' });
        });
    </script>
</body>
</html>
