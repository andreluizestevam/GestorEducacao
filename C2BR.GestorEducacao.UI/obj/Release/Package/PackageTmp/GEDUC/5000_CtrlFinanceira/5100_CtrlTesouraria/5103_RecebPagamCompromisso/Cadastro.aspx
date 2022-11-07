<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5103_RecebPagamCompromisso.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 360px; margin: 30px auto auto !important;}
    .ulDados input{ margin-bottom: 0;}
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}
    .liDataMovimento {clear:none !important; display:inline !important;}  
    .liParamAbeCai{background-color:#EEE9BF; margin-top: 5px;}  
    .liNomeCaixa { margin-top: 10px; }
    .liSiglaCaixa { clear:none !important; margin-left: 29px; margin-top: 10px; }
    
    /*--> CSS DADOS */
    .ddlNomeCaixa { width: 160px; margin-left: 5px;}
    .txtSenha { width: 70px; }
    .txtMoney { width: 77px; text-align:right; }
    #divBarraPadraoContent{display:none;}    
    .txtDataMovimento,.txtDataAbertura{width:55px;}
    .fldFiliaResp {border: 0px;}
    .txtSiglaCaixa { width: 70px; }    
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-60px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }  
          
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="div1" class="bar" > 
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
        <ul id="ulNavegacao" style="width: 39px;">
            <li id="btnVoltarPainel">
                <a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." 
                            alt="Icone Voltar ao Painel Inicial." 
                            src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a>
            </li>
            <li id="btnVoltar">
                <a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior."
                            alt="Icone Voltar a Pagina Anterior." 
                            src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a>
            </li>
        </ul>
        <ul id="ulEditarNovo" style="width: 39px;">
            <li id="btnEditar">
                <img title="" alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
            </li>
            <li id="btnNovo">
                <img title=""
                        alt="Icone de Criar Novo Registro." 
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulGravar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnAbreCaixa_Click">
                    <img title="Direciona para tela de movimentação de caixa."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <img title=""
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar">
                <a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title=""
                            alt="Icone de Cancelar Operacao Atual." 
                            src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a>
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                <img title=""
                        alt="Icone de Pesquisa." 
                        src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
            </li>
            <li id="liImprimir">
                <img title="" 
                        alt="Icone de Impressao." 
                        src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
            </li>
        </ul>
    </div>
</div>
<ul id="ulDados" class="ulDados">        
        <li class="liNomeCaixa">
            <span class="lblObrigatorio" title="Nome do Caixa">
                Caixa</span>
            <asp:DropDownList ID="ddlNomeCaixa" ToolTip="Selecione o Nome do Caixa" AutoPostBack="true" OnSelectedIndexChanged="ddlNomeCaixa_SelectedIndexChanged"
                CssClass="ddlNomeCaixa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ControlToValidate="ddlNomeCaixa" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Nome do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>      
        <li class="liSiglaCaixa">
            <span title="Sigla do Caixa">
                Sigla Caixa</span>
            <asp:TextBox ID="txtSiglaCaixa" Enabled="false" ToolTip="Sigla do Caixa" CssClass="txtSiglaCaixa" runat="server"></asp:TextBox>
        </li>                  
        <li class="liParamAbeCai">
            <fieldset class="fldFiliaResp">
                <ul class="ulParaAbeCai" style="width:350px;padding-left:5px;">
                    <li style="width: 101%; background-color:#FFFACD;text-align:center;text-transform:uppercase;margin-left: -5px;"><span title="Data do Movimento">Parâmetros de Abertura de Caixa</span></li>
                    <li class="liDataMovimento">
                        <span title="Data do Movimento">Data Movimento:</span>
                        <asp:TextBox ID="txtDataMovimento" Enabled="false" style="margin-left: 51px;" ToolTip="Data de Movimentação" CssClass="txtDataMovimento" runat="server"></asp:TextBox>
                    </li>
                    <li>
                         <span title="Valor de Abertura">Valor de Abertura - R$:</span>
                        <asp:TextBox ID="txtValor" Enabled="false" style="margin-left: 22px;"
                            ToolTip="Informe o Valor de Abertura do Caixa"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <span title="Data/Hora de Abertura do Caixa">Data e Hora de Abertura:</span>
                        <asp:TextBox ID="txtDataAbertura" style="margin-left: 14px;" Enabled="false" ToolTip="Data de Abertura do Caixa" CssClass="txtDataAbertura" runat="server"></asp:TextBox>
                        <span>-</span>
                        <asp:TextBox ID="txtHoraAberCai" Enabled="false" runat="server" Width="30px" CssClass="campoHora" ToolTip="Hora de Abertura do Caixa"></asp:TextBox>
                    </li>
                    <li>
                        <span title="Responsável Pela Abertura">
                            Responsável Pelo Caixa:</span>
                        <asp:TextBox ID="txtRespAbert" style="margin-left: 20px;" Enabled="false" runat="server" CssClass="campoNomePessoa" ToolTip="Responsável Pela Abertura do Caixa"></asp:TextBox> 
                    </li>                                                                                         
                    <li>       
                        <span title="Percentual limite Desconto">
                            Porcentual limite Desconto:</span>                 
                        <asp:TextBox ID="txtPorcDescto" style="width:45px;margin-left: 6px;" Enabled="false"
                            ToolTip="Porcentagem do Desconto" 
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                        <span>%</span>
                    </li>
                    <li>
                        <span title="Percentual limite Multa">
                            Porcentual limite Multa:</span>
                        <asp:TextBox ID="txtPorcAboMulta" style="width:45px;margin-left: 24px;" Enabled="false"
                            ToolTip="Porcentagem de Abono da Multa"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                        <span>%</span>
                    </li>
                    <li>
                        <span title="Percentual limite Desconto">
                            Porcentual limite Juros:</span>
                        <asp:TextBox ID="txtPorcAboCorrecao" style="width:45px;margin-left: 24px;" Enabled="false"
                            ToolTip="Porcentagem de Abono de Juros"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                        <span>%</span>
                    </li>                                                                                  
                </ul>
            </fieldset>
        </li>   
        <li class="liDataMovimento" style="margin-top: 15px;margin-bottom: 7px;margin-left: 5px;">
            <span title="Senha de Autenticação" class="lblObrigatorio">
                Senha de Autenticação:</span>
            <asp:TextBox ID="txtSenha" style="width:70px;margin-left: 5px;" Enabled="false" TextMode="Password"
                ToolTip="Senha do Funcionário"
                CssClass="txtSenha" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtSenha" ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Senha deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtMoney").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>