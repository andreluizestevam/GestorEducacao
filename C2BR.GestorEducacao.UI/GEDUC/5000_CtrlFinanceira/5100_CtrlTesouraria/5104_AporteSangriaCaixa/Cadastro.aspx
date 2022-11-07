<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5104_AporteSangriaCaixa.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 250px; margin: 30px auto auto !important; }
    .ulDados input{ margin-bottom: 0;}
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}       
    .liDataMovimento {clear:none !important; display:inline !important;}   
    .liGridForPag { margin-top: 10px; }
    
    /*--> CSS DADOS */ 
    .ddlNomeCaixa { width: 160px;}
    .txtSenhaApoSan { width: 70px; margin-left: 5px; }
    .ddlFuncCaixa { width: 235px; }
    .txtOperCaixa { width: 232px; } 
    #divBarraPadraoContent{display:none;}
    .ddlTpMov { width: 60px; }    
    .txtValorFP { width: 60px; text-align: right; }
    .divgrdNegociacao { overflow-y: scroll; height: 180px; border: 1px solid #CCCCCC; width: 245px;}
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-62px; border: 1px solid #CCC; overflow: auto; width: 230x; padding: 3px 7px 3px 0; }
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
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnAporte_Click">
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
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulDados" class="ulDados">        
        <li>
            <label class="lblObrigatorio" title="Nome do Caixa">
                Caixa</label>
            <asp:DropDownList ID="ddlNomeCaixa" ToolTip="Selecione o Nome do Caixa" AutoPostBack="true" OnSelectedIndexChanged="ddlNomeCaixa_SelectedIndexChanged"
                CssClass="ddlNomeCaixa" runat="server">
            </asp:DropDownList>    
            <asp:HiddenField ID="hdCodCola" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="ddlNomeCaixa" ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Nome do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>   
        <li style="clear:none; margin-left: 10px;">
            <label class="lblObrigatorio" title="Tipo de Movimento">
                Tipo</label>
            <asp:DropDownList ID="ddlTpMov" ToolTip="Selecione o Tipo de Movimento do Caixa" CssClass="ddlTpMov" runat="server">
                <asp:ListItem Selected="true" Value="A">Aporte</asp:ListItem>
                <asp:ListItem Value="S">Sangria</asp:ListItem>
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ControlToValidate="ddlTpMov" ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Tipo de Movimento do Caixa deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>  
        <li style="margin-top: 10px;">
            <label id="Label3" class="lblObrigatorio" title="Usuário Responsável">
                Usuário Responsável</label>
            <asp:DropDownList ID="ddlFuncCaixa" ToolTip="Selecione o Usuário Responsável" 
                CssClass="ddlFuncCaixa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ControlToValidate="ddlFuncCaixa" ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="Usuário Responsável pelo Aporte deve ser informado" Display="None"></asp:RequiredFieldValidator>  
        </li>               
        <li class="liDataMovimento" style="margin-bottom: 7px;margin-left: 40px;">
            <span title="Senha de Autenticação" class="lblObrigatorio">
                Senha de Autenticação:</span>
            <asp:TextBox ID="txtSenhaApoSan" Enabled="false" TextMode="Password" ToolTip="Senha do Funcionário" CssClass="txtSenhaApoSan" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtSenhaApoSan" ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Senha deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>     
        <li class="liGridForPag">
            <div class="divgrdNegociacao">
            <asp:GridView runat="server" ID="grdFormPag" CssClass="grdBusca" AutoGenerateColumns="False"
                DataKeyNames="CO_TIPO_REC" Width="220px">
                <RowStyle CssClass="rowStyle" />
                <HeaderStyle CssClass="th" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <Columns>
                    <asp:BoundField DataField="DE_SIG_RECEB" HeaderText="TP">
                        <ItemStyle Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Descrição">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Valor R$">
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtValorFP" CssClass="txtValorFP" AutoPostBack="true" runat="server" OnTextChanged="txtValorFP_TextChanged" />
                            <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
        </li> 
        <li class="liDataMovimento" style="margin-bottom: 7px;margin-left: 150px;">
            <span title="Total">
                Total:</span>
            <asp:TextBox ID="txtTotalRec" Enabled="false" CssClass="txtValorFP" runat="server"></asp:TextBox>
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel>
        <li style="margin-top: 10px;">
            <label title="Operador do Caixa">
                Operador do Caixa:</label>
            <asp:TextBox ID="txtOperCaixa" Enabled="false" runat="server" CssClass="txtOperCaixa" ToolTip="Responsável Pela Abertura do Caixa"></asp:TextBox> 
        </li>
        <li class="liDataMovimento" style="margin-bottom: 7px;margin-left: 40px;">
            <span title="Senha de Autenticação" class="lblObrigatorio">
                Senha de Autenticação:</span>
            <asp:TextBox ID="txtSenhaOpeCai" TextMode="Password" ToolTip="Senha do Operador do Caixa" CssClass="txtSenhaApoSan" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtSenhaOpeCai" ID="RequiredFieldValidator6" runat="server" 
            ErrorMessage="Senha deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li> 
</ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".txtValorFP").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });

    $(document).ready(function () {
        $(".txtValorFP").maskMoney({ symbol: "", decimal: ",", thousands: "." });
    });
</script>
</asp:Content>