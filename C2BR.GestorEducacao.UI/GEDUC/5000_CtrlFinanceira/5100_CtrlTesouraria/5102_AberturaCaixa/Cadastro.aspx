<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5102_AberturaCaixa.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 270px;margin-top: 30px !important;}
    .ulDados input{ margin-bottom: 0;}       
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}
    .liDataMovimento,.liHoraAberCai {clear:none !important; display:inline !important;}    
    .liValor { clear: none !important; margin-left: 5px; display:inline !important; }
    .liParamAbeCai { margin-top: 10px; }
    
    /*--> CSS DADOS */
    .ddlNomeCaixa { width: 160px; }
    .ddlFuncCaixa { width: 210px; }    
    .chkLocais label { display: inline !important; margin-left:-4px;}
    .txtMoney { width: 77px; text-align:right; }    
    #divBarraPadraoContent{display:none;}
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
                <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
            </li>
            <li id="btnNovo">
                <img title="Abre o formulario para Criar um Novo Registro."
                        alt="Icone de Criar Novo Registro." 
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulGravar">
            <li>
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnAberCaixa_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir">
                <img title="Exclui o Registro atual selecionado."
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar">
                <a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                            alt="Icone de Cancelar Operacao Atual." 
                            src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a>
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." 
                        src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
            </li>
            <li id="liImprimir">
                <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." 
                        alt="Icone de Impressao." 
                        src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
            </li>
        </ul>
    </div>
</div>
<ul id="ulDados" class="ulDados">        
        <li>
            <label id="Label1" class="lblObrigatorio" title="Nome do Caixa">
                Nome Caixa</label>
            <asp:DropDownList ID="ddlNomeCaixa" ToolTip="Selecione o Nome do Caixa" 
                CssClass="ddlNomeCaixa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ControlToValidate="ddlNomeCaixa" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Nome do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>
        <li class="liDataMovimento" style="margin-left: 15px;">
            <label for="txtDataMovimento" class="lblObrigatorio" title="Data do Movimento">Data Movimento</label>
            <asp:TextBox ID="txtDataMovimento" ToolTip="Informe a Data de Movimentação" CssClass="campoData" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorDataAtual" runat="server" CssClass="validatorField"
                                ForeColor="Red" ControlToValidate="txtDataMovimento" Type="Date" Operator="LessThanEqual"
                                ErrorMessage="Data Movimento não pode ser maior que Data Atual.">
                            </asp:CompareValidator>
            <asp:RequiredFieldValidator ControlToValidate="txtDataMovimento" ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Data de Movimentação deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 10px;">
            <label id="Label3" class="lblObrigatorio" title="Funcionário do Caixa">
                Funcionário do Caixa</label>
            <asp:DropDownList ID="ddlFuncCaixa" ToolTip="Selecione o Funcionário do Caixa" 
                CssClass="ddlFuncCaixa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ControlToValidate="ddlFuncCaixa" ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="Funcionário do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>
        <li class="liParamAbeCai">
            <fieldset class="fldFiliaResp">
                <legend style="margin-left: 10px;">Parâmetro de Abertura do Caixa</legend>
                <ul style="width:245px;padding-left: 5px;">
                    <li style="margin-left: 5px;">
                        <label for="txtDataAbertura" class="lblObrigatorio" title="Data de Abertura do Caixa">Data</label>
                        <asp:TextBox ID="txtDataAbertura" Enabled="false" ToolTip="Informe a Data de Abertura do Caixa" CssClass="campoData" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtDataAbertura" ID="RFV1" runat="server" 
                            ErrorMessage="Data de Abertura do Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>

                    <li class="liHoraAberCai" style="margin-left: 5px;">
                        <label id="Label4" class="lblObrigatorio" title="Hora de Abertura do Caixa">
                            Hora</label>
                        <asp:TextBox ID="txtHoraAberCai" Enabled="false" runat="server" Width="30px" CssClass="campoHora" ToolTip="Informe a Hora de Abertura do Caixa"></asp:TextBox>       
                        <asp:RequiredFieldValidator ControlToValidate="txtHoraAberCai" ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="Hora de Abertura do Caixa deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    </li>
        
                    <li class="liValor" style="margin-left: 15px;">
                         <label id="lblValor" runat="server" title="Informe o Valor de Abertura">Valor de Abertura</label>
                        <asp:TextBox ID="txtValor" 
                            ToolTip="Informe o Valor de Abertura do Caixa"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                    </li>  
                    
                    <li>
                        <asp:CheckBox CssClass="chkLocais" ID="chkPerDescto" OnCheckedChanged="chkPerDescto_CheckedChanged" TextAlign="Right" AutoPostBack="true" runat="server" Text="Permite Desconto"/>                                
                    </li>
                    <li class="liValor" style="margin-left: 15px;">
                        <span>%</span>
                        <asp:TextBox ID="txtPorcDescto" style="width:45px;" Enabled="false"
                            ToolTip="Informe a Porcentagem do Desconto"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <asp:CheckBox CssClass="chkLocais" ID="chkAboMulta" AutoPostBack="true" OnCheckedChanged="chkAboMulta_CheckedChanged" TextAlign="Right" runat="server" Text="Abona Multa"/>                                
                    </li>
                    <li class="liValor" style="margin-left: 36px;">
                        <span>%</span>
                        <asp:TextBox ID="txtPorcAboMulta" style="width:45px;" Enabled="false"
                            ToolTip="Informe a Porcentagem de Abono da Multa"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <asp:CheckBox CssClass="chkLocais" AutoPostBack="true" ID="chkAboCorrecao" OnCheckedChanged="chkAboCorrecao_CheckedChanged" TextAlign="Right" runat="server" Text="Abona Correção"/>                                
                    </li>    
                    <li class="liValor" style="margin-left: 18px;">
                        <span>%</span>
                        <asp:TextBox ID="txtPorcAboCorrecao" style="width:45px;" Enabled="false"
                            ToolTip="Informe a Porcentagem de Abono da Correção"
                            CssClass="txtMoney" runat="server"></asp:TextBox>
                    </li>                                                              
                </ul>
            </fieldset>
        </li>     
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtMoney").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>