<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="MapaEvoDesMediaEsc.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9020_IndicesCtrlPedagogico.MapaEvoDesMediaEsc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 985px;margin-top: 20px !important;}
    .ulDados input{ margin-bottom: 0;}   
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both;}   
    .liUnidadeEscolar {clear:none !important; display:inline !important;margin-left:10px;}    
    .liBtnAnaRecPag
    {
    	background-color: #FFFFE0;
        border: 1px solid #D2DFD1;     
        padding: 2px 3px 1px 3px;
        margin-left: 460px;
        margin-bottom: 15px !important;
    }
    .liTotais{ width:51px;clear:none !important;text-align:right;margin-right: 0px !important; }
    
    /*--> CSS DADOS */
    .ddlAnoRefer { width: 45px; }
    .imgliLnk { width: 15px; height: 13px; }    
    .divGrid { height: 220px; overflow-y:auto; overflow-x:auto; width: 636px; border: 1px solid #D2DFD1;}    
    #divBarraPadraoContent{display:none;}
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-30px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }  
    #helpMessages {display:none;}
    .helpMessages
    {
        margin-top: -5px;
        font-size: 11px !important;
    }
    
</style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="helpMessagesFC" class="helpMessages">
    <div id="divMensagGenerica" class="divMensagGenerica">
        <span id="lblMsgGenric" style="margin-top: -3px;">Escolha nos campos abaixo sua preferência de pesquisa e clique no botão de PESQUISAR.</span>
    </div>
    <div id="divMensagCamposObrig" class="divMensagGenerica" style="margin-top: 2px;">
        <span>Para voltar a tela inicial ou anterior utilize um dos botões da barra de comandos </span>
    </div>
</div>
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
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
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
        <li style="margin-left: 125px;">
            <label class="lblObrigatorio" title="Tipo de Unidade">
                Tipo de Unidade</label>
            <asp:DropDownList ID="ddlTpUnidade" ToolTip="Selecione o Tipo de Unidade" AutoPostBack="true" OnSelectedIndexChanged="ddlTpUnidade_SelectedIndexChanged"
                CssClass="ddlTpUnidade" runat="server">
            </asp:DropDownList>     
        </li>
        <li class="liUnidadeEscolar">
            <label class="lblObrigatorio" title="Unidade Escolar">
                Unidade Escolar</label>
            <asp:DropDownList ID="ddlUnidadeEscolar" ToolTip="Selecione a Unidade Escolar" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadeEscolar_SelectedIndexChanged"
                CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>    
        </li>                     
        <li class="liUnidadeEscolar">
            <label class="lblObrigatorio" title="Modalidade de Ensino">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade de Ensino" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                CssClass="ddlModalidade" runat="server">
            </asp:DropDownList>    
        </li>     
        <li class="liUnidadeEscolar">
            <label class="lblObrigatorio" title="Série">
                Série</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série" CssClass="ddlSerieCurso" runat="server">
            </asp:DropDownList>    
        </li>  
        <li class="liUnidadeEscolar">
            <label class="lblObrigatorio" title="Ano">
                Ano Letivo</label>
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione o Ano de Referência" 
                CssClass="ddlAnoRefer" runat="server">
            </asp:DropDownList>    
        </li> 
        <li clientidmode="Static" runat="server" title="Clique para Gerar Grid" class="liBtnAnaRecPag">            
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnPesquisar_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Pesquisar" />
                <asp:Label runat="server" ID="Label3" Text="Pesquisar"></asp:Label>            
            </asp:LinkButton>
        </li>
        <li style="margin-left: 9px;" id="liGridMCFF" runat="server" clientidmode="Static" visible="false">
        <ul>
            <li style="margin-left: 265px;"><label style="text-transform:uppercase;">------------------------------------------ SÉRIES ------------------------------------------</label></li>
            <li style="margin-left: 5px;">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResulParam" CssClass="grdBusca" Width="830px" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </li>
         </ul>
         </li>
         <li style="clear:none; margin-left: 10px; margin-right: 0px;" id="liLegMCFF" runat="server" clientidmode="Static" visible="false">
            <asp:CHART id="Chart1" runat="server" Palette="BrightPastel" BackColor="WhiteSmoke" Width="300px" Height="260px" BorderlineDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)">							
				<legends>
					<asp:Legend BackColor="Transparent" Alignment="Center" 
                        Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" Name="Default" 
                        LegendStyle="Column"></asp:Legend>
				</legends>
				<Titles>
                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Pie Chart" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                </Titles>
				<borderskin SkinStyle="Emboss"></borderskin>
				<series>
					<asp:Series Name="Default" IsValueShownAsLabel="True" ChartType="Pie" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240"></asp:Series>
				</series>
				<chartareas>
					<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BorderWidth="0">
						<area3dstyle Rotation="0" />
						<axisy LineColor="64, 64, 64, 64">
							<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
							<MajorGrid LineColor="64, 64, 64, 64" />
						</axisy>
						<axisx LineColor="64, 64, 64, 64">
							<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
							<MajorGrid LineColor="64, 64, 64, 64" />
						</axisx>
					</asp:ChartArea>
				</chartareas>
			  </asp:CHART>    
          </li>           
</ul>
<script type="text/javascript">
</script>
</asp:Content>