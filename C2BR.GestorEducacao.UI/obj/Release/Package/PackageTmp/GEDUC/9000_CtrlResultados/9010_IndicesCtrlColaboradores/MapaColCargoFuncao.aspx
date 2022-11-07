<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="MapaColCargoFuncao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9010_IndicesCtrlColaboradores.MapaColCargoFuncao" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">      
    .ulDados { width: 935px; margin-top: 20px !important;}
    .ulDados input{ margin-bottom: 0;}       
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both; }
    .liFuncao { clear:none !important; display:inline !important;margin-left:10px; }    
    .liBtnAnaRecPag
    {
    	background-color: #FFFFE0;
        border: 1px solid #D2DFD1;     
        padding: 2px 3px 1px 3px;
        margin-left: 435px;
        margin-bottom: 15px !important;
    }
    .liTotais{ width:25px;clear:none !important;text-align:right;margin-right: 0px !important; }
    
    /*--> CSS DADOS */
    .ddlFuncao { width: 200px; }
    .imgliLnk { width: 15px; height: 13px; }    
    .divGrid { height: 308px; overflow-y:auto; width: 570px; border: 1px solid #D2DFD1;}
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
        <span ID="lblMsgGenric" style="margin-top: -3px;">Escolha nos campos abaixo sua preferência de pesquisa e clique no botão de PESQUISAR.</span>
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
        <li style="margin-left:165px;">
            <label class="lblObrigatorio" title="Tipo de Unidade">
                Tipo de Unidade</label>
            <asp:DropDownList ID="ddlTpUnidade" ToolTip="Selecione o Tipo de Unidade" CssClass="ddlTpUnidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTpUnidade_SelectedIndexChanged">
            </asp:DropDownList>     
        </li>
        <li class="liFuncao">
            <label id="Label5" class="lblObrigatorio" title="Unidade Escolar">
                Unidade Escolar</label>
            <asp:DropDownList ID="ddlUnidadeEscolar" ToolTip="Selecione a Unidade Escolar" 
                CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>    
        </li>      
        <li class="liFuncao">
            <label id="Label1" class="lblObrigatorio" title="Cargo/Função">
                Cargo/Função</label>
            <asp:DropDownList ID="ddlFuncao" ToolTip="Selecione o Cargo/Função" CssClass="ddlFuncao" runat="server">
            </asp:DropDownList>     
        </li>    
        <li clientidmode="Static" runat="server" title="Clique para Gerar Grid" class="liBtnAnaRecPag">            
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnPesquisar_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Pesquisar" />
                <asp:Label runat="server" ID="Label3" Text="Pesquisar"></asp:Label>            
            </asp:LinkButton>
        </li>
        <li style="margin-left: 9px;" id="liGridMCCF" runat="server" clientidmode="Static" visible="false"> 
        <ul>
            <li style="margin-left: 225px;"><label style="text-transform:uppercase;">---------------------------- SITUAÇÃO FUNCIONAL ----------------------------</label></li>
            <li>
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResulParam" CssClass="grdBusca" Width="550px" runat="server" AutoGenerateColumns="False">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="NO_FUN" HeaderText="Função">
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ATI" HeaderText="ATI">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ATE" HeaderText="ATE">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="FCE" HeaderText="FCE">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FES" HeaderText="FES">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LFR" HeaderText="LFR">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LME" HeaderText="LME">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="LMA" HeaderText="LMA">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SUS" HeaderText="SUS">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRE" HeaderText="TRE">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="FER" HeaderText="FER">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL">
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="width:227px;margin-right: 0px;visibility:hidden;">
                <label>Total</label>
            </li> 
            <li class="liTotais" style="visibility:hidden;">
                <asp:Label ID="lblTotATI" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:28px;visibility:hidden;">
                <asp:Label ID="lblTotATE" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:26px;visibility:hidden;">
                <asp:Label ID="lblTotFCE" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:28px;visibility:hidden;">
                <asp:Label ID="lblTotFES" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:28px;visibility:hidden;">
                <asp:Label ID="lblTotLFR" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:29px;visibility:hidden;">
                <asp:Label ID="lblTotLME" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:29px;visibility:hidden;">
                <asp:Label ID="lblTotLMA" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:28px;visibility:hidden;"> 
                <asp:Label ID="lblTotSUS" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:27px;visibility:hidden;">
                <asp:Label ID="lblTotTRE" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:28px;visibility:hidden;">
                <asp:Label ID="lblTotFER" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:45px;visibility:hidden;">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </li>
         </ul>
         </li>
         <li style="clear:none; margin-left: 10px; margin-right: 0px;" id="liLegMCCF" runat="server" clientidmode="Static" visible="false">
            <asp:chart id="Grafico1" runat="server" Width="330px" Height="360px" 
                Palette="BrightPastel" imagetype="Png" BorderlineDashStyle="Solid" 
                BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" 
                backcolor="#D3DFF0" BorderColor="26, 59, 105" RightToLeft="Yes">
                <legends>
                    <asp:Legend Name="Legend1" IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                </legends>
                <borderskin skinstyle="Emboss"></borderskin>
                <series>
                    <asp:Series Name="Default" IsXValueIndexed="True" BorderColor="180, 26, 59, 105"
                         Legend="Legend1" IsValueShownAsLabel="True" IsVisibleInLegend="False" 
                        YValueType="Double" CustomProperties="PointWidth=0.5" 
                        Font="Microsoft Sans Serif, 8.25pt" XValueType="String" 
                        YValuesPerPoint="2">
                     </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <area3dstyle Rotation="10" Inclination="15" IsRightAngleAxes="False" 
                            wallwidth="0" IsClustered="False"></area3dstyle>
                        <axisy linecolor="64, 64, 64, 64">
                            <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                            <majorgrid linecolor="64, 64, 64, 64" />
                        </axisy>
                        <axisx linecolor="64, 64, 64, 64" LogarithmBase="2" Interval="1">
                            <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                            <majorgrid linecolor="64, 64, 64, 64" />
                        </axisx>
                    </asp:ChartArea>
                </chartareas>
             </asp:chart>
        </li>         
</ul>
<script type="text/javascript">
</script>
</asp:Content>