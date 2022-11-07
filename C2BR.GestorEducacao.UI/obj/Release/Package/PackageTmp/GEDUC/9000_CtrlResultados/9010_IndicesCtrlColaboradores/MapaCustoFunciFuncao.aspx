<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="MapaCustoFunciFuncao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9010_IndicesCtrlColaboradores.MapaCustoFunciFuncao" %>
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
    .ddlSituacao {width:125px;}
    .imgliLnk { width: 15px; height: 13px; }    
    .divGrid { height: 308px; overflow-y:auto; width: 620px; border: 1px solid #D2DFD1;}    
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
        <li style="margin-left: 228px;">
            <label class="lblObrigatorio" title="Tipo de Unidade">
                Tipo de Unidade</label>
            <asp:DropDownList ID="ddlTpUnidade" ToolTip="Selecione o Tipo de Unidade" AutoPostBack="true" OnSelectedIndexChanged="ddlTpUnidade_SelectedIndexChanged"
                CssClass="ddlTpUnidade" runat="server">
            </asp:DropDownList>     
        </li>
        <li class="liUnidadeEscolar">
            <label id="Label5" class="lblObrigatorio" title="Unidade Escolar">
                Unidade Escolar</label>
            <asp:DropDownList ID="ddlUnidadeEscolar" ToolTip="Selecione a Unidade Escolar" 
                CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>    
        </li>              
        <li class="liUnidadeEscolar">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">Situação Atual</label>
            <asp:DropDownList ID="ddlSituacao" 
                ToolTip="Selecione a Situação Atual do Funcionário"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="ATI">Atividade Interna</asp:ListItem>
                <asp:ListItem Value="ATE">Atividade Externa</asp:ListItem>
                <asp:ListItem Value="FCE">Cedido</asp:ListItem>
                <asp:ListItem Value="FES">Estagiário</asp:ListItem>
                <asp:ListItem Value="LFR">Licença Funcional</asp:ListItem>
                <asp:ListItem Value="LME">Licença Médica</asp:ListItem>
                <asp:ListItem Value="LMA">Licença Maternidade</asp:ListItem>
                <asp:ListItem Value="SUS">Suspenso</asp:ListItem>
                <asp:ListItem Value="TRE">Treinamento</asp:ListItem>
                <asp:ListItem Value="FER">Férias</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao" ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li> 
        <li clientidmode="Static" runat="server" title="Clique para Gerar Grid" class="liBtnAnaRecPag">            
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="btnPesquisar_Click">
                <img class="imgliLnk" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa" title="Pesquisar" />
                <asp:Label runat="server" ID="Label3" Text="Pesquisar"></asp:Label>            
            </asp:LinkButton>
        </li>
        <li style="margin-left: 9px;" id="liGridMCFF" runat="server" clientidmode="Static" visible="false">
        <ul>
            <li style="margin-left: 202px;"><label style="text-transform:uppercase;">----------------------------------------- TIPO CONTRATO -----------------------------------------</label></li>
            <li>
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdResulParam" CssClass="grdBusca" Width="600px" runat="server" AutoGenerateColumns="False">
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
                            <asp:BoundField DataField="COM" HeaderText="COM" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PJU" HeaderText="PJU" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="COO" HeaderText="COO" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EST" HeaderText="EST" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AUT" HeaderText="AUT" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EFE" HeaderText="EFE" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="CTR" HeaderText="CTR" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <li style="width:200px;margin-right: 0px;visibility:hidden;">
                <label>Total</label>
            </li> 
            <li class="liTotais" style="visibility:hidden;">
                <asp:Label ID="lblTotCOM" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:45px;visibility:hidden;">
                <asp:Label ID="lblTotPJU" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:47px;visibility:hidden;">
                <asp:Label ID="lblTotCOO" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:45px;visibility:hidden;">
                <asp:Label ID="lblTotEST" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:45px;visibility:hidden;">
                <asp:Label ID="lblTotAUT" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:55px;visibility:hidden;">
                <asp:Label ID="lblTotEFE" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:45px;visibility:hidden;">
                <asp:Label ID="lblTotCTR" runat="server"></asp:Label>
            </li>
            <li class="liTotais" style="width:65px;visibility:hidden;">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </li>
         </ul>
         </li>
         <li style="clear:none; margin-left: 20px; margin-right: 0px;" id="liLegMCFF" runat="server" clientidmode="Static" visible="false">
            <asp:chart id="Grafico1" runat="server" Width="310px" Height="360px" 
                Palette="BrightPastel" imagetype="Png" BorderlineDashStyle="Solid" 
                BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" 
                backcolor="#D3DFF0" BorderColor="26, 59, 105" RightToLeft="Yes">
                <legends>
                    <asp:Legend Name="Legend1" IsTextAutoFit="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
                </legends>
                <borderskin skinstyle="Emboss"></borderskin>
                <series>
                    <asp:Series Name="Default" IsXValueIndexed="True" BorderColor="180, 26, 59, 105"
                         Legend="Legend1" IsValueShownAsLabel="True" LabelAngle="45" IsVisibleInLegend="False" LabelFormat="F2"
                        YValueType="Double" CustomProperties="PointWidth=0.5" 
                        Font="Microsoft Sans Serif, 8.25pt" XValueType="String">
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