<%@ Page Language="C#"  MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="ManutCaixa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5100_CtrlTesouraria._5110_ManutMovFinCaixa.Cadastro" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    .ulDados { width: 950px; margin-top: 0px !important; }
    .ulDados input{ margin-bottom: 0;} 
    .ulDados label {clear:none !important;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 5px; margin-right: 5px;clear:both; } 
    .liDataMovimento, .liDocumento, .liDtVencto, .liGridForPag {clear:none !important; display:inline !important;}    
    .liGrdContratos
    {
        width: 940px;
        clear:both;
        height: 250px;
    }
    .liValSis { width: 100%; margin-top: 4px; }  
    .liInfCad { background-color: #E0FFFF; margin-top: 3px; }
    .liInfTitulo { background-color:#FFEFDB; margin-bottom: 7px !important; }
    .liValores { border-bottom: 1px solid #CCC; height: 165px; }
    .liCadCheque { margin-left: 5px; }    
    .liClear { clear: both; }
    
    /*--> CSS DADOS */
    .chkLocais { margin-left: -5px; }
    .chkLocais label { display: inline !important; margin-left:-4px;}
    .txtMoney { width: 60px; text-align:right; float: right; margin-top: -3px; }
    #divBarraPadraoContent{display:none;}    
    .divGrdContratos
    {
        margin-top:10px;
    	height: 145px;
    	border: 1px solid #CCCCCC;
    	overflow-y: scroll;
    	width: 940px;
    }
    .lblResultados { margin-left: 0px; font-weight:bold; }
    .lblResultadosSis { margin-left: 5px; font-weight:bold; float: right;}      
    .txtValorFP { text-align:right; width:40px; }
    .txtQtdeFP { width: 13px; text-align: right; }
    .fldFiliaResp { border: 0px;}    
    .imgCadCheque { height: 13px; width: 13px; }        
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-37px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }     
    /*.grdBusca .emptyDataRowStyle td { padding: 10px 355px !important; }       */
    .noprint{display:none;}
    .divgrdNegociacao {  height: 200px; overflow-x: hidden; }
    .th{/*position:relative;*/
        background-color:#999999 !important;}            
    .divTelaExportacaoCarregando
        {
            left: 50%;
            margin-top: 32px;
            position: relative;
            top: 10px;
            display: none;
        }
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>

<asp:Content ID="Content3"   ContentPlaceHolderID="content" runat="server">   

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
                <asp:LinkButton ID="btnSaveMovimento" runat="server" OnClick="btnSaveMovimento_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                </asp:LinkButton>
            </li>
        </ul>
        <ul id="ulExcluirCancelar" style="width: 39px;">
            <li id="btnExcluir" >
                <img title="Exclui o Registro atual selecionado."
                        alt="Icone de Excluir o Registro." 
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
            </li>
            <li id="btnCancelar" >
                <a  href='<%= Request.Url.AbsoluteUri %>' >
                    <img  title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                            alt="Icone de Cancelar Operacao Atual." 
                            src="../../../../BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
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
            <li style="clear:both;float:left;margin-left:0px;">
                    <label title="Responsável do Título">Usuário Responsável</label>
                    <asp:DropDownList Width="205px" ID="ddlNomeCaixa" ToolTip="Selecione o Usuário Responsável" CssClass="ddlNomeCaixa" runat="server" >
                    </asp:DropDownList>    
                    <asp:RequiredFieldValidator ID="rfvddlNomeCaixa" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlNomeCaixa" Text="*" 
                        ErrorMessage="Campo Usuário Responsável é requerido" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>  
             </li>    
             <li style="clear:none;margin-left:3px;padding-top:0px;">
                    <label title="Senha de Autenticação">Senha Autenticação:</label>
                    <asp:TextBox ID="txtSenha" Height="13px"  style="margin-left: 0px;margin-bottom:0px" Enabled="true" TextMode="Password"
                    ToolTip="Senha do Funcionário"
                    CssClass="txtSenha" runat="server" Width="91px" ></asp:TextBox>

                     
        
            </li>
            <li style="clear:none;margin-left:3px;height:20px;padding-top:13px;">
                    <span style="margin-bottom: 0px">
                    <asp:Button ID="Autenticar" runat="server" Height="15px" Text="Autenticar" 
                        onclick="Autenticar_Click" />
                    </span>
                   
            </li>
             <li class="liDataMovimento" style="width:90px;float:left;clear:none;margin-left:5px;margin-right:0px"  >
                    <label title="Data do Movimento">Data Movimento:</label>
                     <asp:DropDownList Width="80px" ID="ddlDtMovto" ToolTip="Nome Caixa" 
                    AutoPostBack="true" CssClass="ddlDataMovimento" runat="server" OnSelectedIndexChanged="ddlDataMovimento_SelectedIndexChanged" />
            </li>         
            <li class="liNomeCaixa" style="clear:none;margin-left:3px" >
                    <label title="Data do Movimento">Nome Caixa:</label>
                    <asp:DropDownList ID="ddlCaixa" ToolTip="Nome Caixa" 
                    AutoPostBack="true" CssClass="ddlNomeCaixa" runat="server" OnSelectedIndexChanged="ddlCaixa_SelectedIndexChanged"
                    Width="130px">
                    </asp:DropDownList>    
             </li>  

             <li class="liFuncionarioCaixa" style="float:left;clear:none;margin-left:3px">
                    <label title="Data do Movimento">Funcionário do Caixa:</label>
                    <asp:DropDownList ID="ddlFuncCaixa" ToolTip="Funcionario Caixa" 
                    AutoPostBack="true" CssClass="ddlFuncionarioCaixa" runat="server" OnSelectedIndexChanged="ddlFuncionarioCaixa_SelectedIndexChanged"
                    Width="205px">
                    </asp:DropDownList>    
             </li>  

             <li class="liTipoMovimento"  style="float:left;clear:none;margin-left:3px">
                    <label title="Tipo do Movimento">Tipo Movimento:</label>
                    <asp:DropDownList ID="ddlTpMovimento" ToolTip="Tipo Movimento" 
                    AutoPostBack="true" CssClass="ddlTipoMovimento" runat="server" OnSelectedIndexChanged="ddlTipoMovimento_SelectedIndexChanged"
                    Width="100px">
                        <asp:ListItem Value="T" Text="Todos"></asp:ListItem>
                        <asp:ListItem Value="C" Text="Recebimentos"></asp:ListItem>
                        <asp:ListItem Value="D" Text="Pagamentos"></asp:ListItem>
                    </asp:DropDownList>    
             </li> 
             
             <asp:HiddenField ID="hdfCoSeqSelec" runat="server" />
             <li class="liGrdContratos" style="width:870px; height: 160px;margin-top:-15px">
              <div id="divTelaExportacaoCarregando" class="divTelaExportacaoCarregando" runat="server">
                    <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
               </div> 
             <div class="divGrdContratos" runat="server" id="divGrdContratos"> 
                <asp:GridView runat="server" ID="grdContratos" CellPadding='2' CssClass="grdBusca" 
                    OnRowDataBound="grdContratos_RowDataBound"
                    AutoGenerateColumns="False"  HeaderStyle-CssClass="th"
                    OnSelectedIndexChanged="grdContratos_SelectedIndexChanged" Width="862px" >
                   
                    <HeaderStyle CssClass="th" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />                                        
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="th">
                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" AutoPostBack="true"
                                    oncheckedchanged="ckSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Sequencial"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th"  HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="black" HeaderText="SEQ">
<HeaderStyle CssClass="th" Font-Bold="True" ForeColor="Black"></HeaderStyle>

                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tipo"  HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black"   HeaderText="TIPO">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-Width="350px" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderStyle-Wrap="false" DataField="Responsavel" HeaderText="RESPONSÁVEL">
<HeaderStyle Wrap="False" Font-Bold="True" ForeColor="Black"></HeaderStyle>

                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoDocto" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" ItemStyle-Width="250px" HeaderText="TIPO DOCTO">
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>

                            <ItemStyle  />
                        </asp:BoundField>
                        <asp:BoundField DataField="Documento" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="Nº DOCTO">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Parcela" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="black" HeaderText="PA">
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>

                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="DataDocto" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" DataFormatString="{0:d}"  HeaderText="DT DOC">
                            <ItemStyle  />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DataVencto" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black"  DataFormatString="{0:d}"  HeaderText="DT VEN">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Valor" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="R$ DOCTO">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ValorPago" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="R$ PAGTO">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="HistoDocumento" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="HISTÓRICO DOCUMENTO">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Caixa" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black"  ItemStyle-Width="150px"  HeaderText="CAIXA MOVTO">
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>

                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField DataField="FuncCaixa" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="FUNC CX">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CoCaixa" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="th" HeaderStyle-ForeColor="black" HeaderText="CO CX" Visible="True">
                            <ItemStyle />
<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                   
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle"  />
                    <EmptyDataTemplate>                    
                        Nenhum Título em Aberto foi Encontrado<br />
                    </EmptyDataTemplate>
                    <HeaderStyle 
                        BackColor="#6B696B" Font-Bold="True" ForeColor="White" 
                    />
                    <PagerStyle CssClass="grdFooter" />
                    <RowStyle CssClass="rowStyle" />
                </asp:GridView>
            </div>                 
        </li> 
        <li style="clear: both;margin-top:-4px">
            <ul style="width:270px;">
                <li style="background-color: #5F9EA0; width: 270px; text-align: center; height:20px ; font-size: small;">
                      <asp:Label ID="Label1" runat="server" Text="VALORES INFORMADOS" 
                          Font-Bold="false" Font-Size="9" ForeColor="White"></asp:Label>
                </li>
                <li >
                    <ul class="ulDados2" style="margin-left:80px;" >
                        <li style="color: #C0C0C0;">DADOS ATUAIS</li>
                        <li style="color: #C0C0C0; clear: none;margin-left:0px">NOVO CONTEUDO</li>
                    </ul>
                </li>
                <li style="clear: both;margin-top:-5px">
                    <ul class="ulDados3" >
                        <li style="height:5px;width:75px;"><label>Valor Título</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtVlTitulo" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP"  Enabled="false" style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtVlTituloDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                   <ul class="ulDados3" >
                        <li style="height:5px;width:75px;"><label>Valor Multa</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtMultaDA" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP" AutoPostBack="true" OnTextChanged="txtMultaDANC_TextChanged" style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtMultaDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                     <ul class="ulDados3" >
                        <li style="height:5px;width:75px; clear: both;"><label>Valor Juros</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtJurosDA" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP" AutoPostBack="true" OnTextChanged="txtJurosDANC_TextChanged" style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtJurosDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                     <ul class="ulDados3" >
                        <li style="height:5px;width:75px; clear: both;"><label>Valor Adicional</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtAdiciDA" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP" AutoPostBack="true" OnTextChanged="txtAdiciDANC_TextChanged" style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtAdiciDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                     <ul class="ulDados3" >
                        <li style="height:5px;width:75px; clear: both;"><label>Valor Desconto</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtDesctoDA" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP" AutoPostBack="true" OnTextChanged="txtDesctoDANC_TextChanged" style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtDesctoDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                     <ul class="ulDados3" >
                        <li style="height:5px;width:75px ;clear: both;"><label>Valor Pago</label></li>
                        <li style="clear: none; width: 60px;"><asp:TextBox Enabled="false" style="margin-bottom:0px;text-align:right;" ID="txtPagoDA" runat="server"  Width="80px" ></asp:TextBox></li>
                        <li style="clear: none;" ><asp:TextBox CssClass="txtValorFP"  style="margin-bottom:0px;margin-left:25px;text-align:right;" ID="txtPagoDANC" runat="server"  Width="80px" ></asp:TextBox></li>
                    </ul>
                </li>
              </ul>
        </li>
        <li style="clear: none;margin-left: 0px;height:217px;margin-top:-4px">
            <ul style="width:650px;margin-left:15px">
                <li style="background-color: #5F9EA0; width: 650px; text-align: center; height:20px ; font-size: small;">
                        <asp:Label ID="Label7" runat="server" Text="FORMA DE RECEBIMENTO OU PAGAMENTO DO TÍTULO" 
                            Font-Bold="false"  Font-Size="9"  ForeColor="White"></asp:Label>
                </li>
                <li style="clear: both;width:650px;padding:0px;margin:0px;height:200px;margin-top:-5px" >
                   <ul class="ulDados3"  style="width:650px;">
                        <li class="liGridForPag"  style="margin-top: -22px; width:650px; height:200px ;margin-right: 0; clear: both;">
                            <div class="divgrdNegociacao" >
                            <asp:GridView runat="server" ID="grdFormPag"  Height="200px" CssClass="grdBusca" AutoGenerateColumns="False"
                                DataKeyNames="CO_TIPO_REC" Width="650px">
                                <RowStyle CssClass="rowStyle" />
                                <HeaderStyle 
                                   BackColor="Red"
                                 />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="DE_RECEBIMENTO" HeaderText="Descrição">
                                        <ItemStyle HorizontalAlign="Left" Width="130px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Qtde">
                                        <ItemStyle Width="14px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtdeFP" Text='<%# bind("NU_QTDE") %>' Enabled="false" CssClass="txtQtdeFP" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor R$">
                                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValorFP" Text='<%# bind("VR_RECEBIDO") %>' Enabled="false" CssClass="txtValorFP" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="140px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObservacao" Text='<%# bind("DE_OBS") %>' MaxLength="255" Enabled="false" style="width:140px; margin-right: 0 !important;" runat="server" />
                                            <asp:HiddenField ID="hdCO_TIPO_REC" runat="server" Value='<%# bind("CO_TIPO_REC") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtde">
                                        <ItemStyle Width="14px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtdeNC" Text='<%# bind("NU_QTDE") %>' CssClass="txtQtdeFP" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor R$">
                                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValorNC" Text='<%# bind("VR_RECEBIDO") %>' CssClass="txtValorFP" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="140px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObservacaoNC" Text='<%# bind("DE_OBS") %>' MaxLength="255" style="width:140px; margin-right: 0 !important;" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                        </li>
                    </ul>                    
                </li>
            </ul>
        </li>
        <li style="clear:none">
            <ul>
                <li style="color: #FF8B3D;">
                    ATENÇÃO!<br />
                    1-Todas as alterações de conteúdos são registradas em LOG.<br />
                    2-Alterando a situação do registro de movimento de caixa para 
                        CANCELADO <br /> o titulo sai do  movimento e volta  a condição inicial (EM ABERTO) no financeiro.
                </li>
                <li style="clear:none;margin-right:0px;margin-left: 30px; width: 260px;text-align:right ;">
                   <ul>
                        <li style="background-color: #5F9EA0; width: 260px;text-align: center; height:15px ; font-size: small;">
                            <asp:Label ID="Label2" runat="server"  Text="SITUAÇÃO DO REGISTRO DE CAIXA " 
                                Font-Bold="True"  Font-Size="8"  ForeColor="yellow"></asp:Label>
                        </li>
                        <li style="height:10px" >
                            <ul class="ulDados2" style="margin-left:0px" >
                                <li style="color: #C0C0C0;margin-left:0px;width:100px" >CONDIÇÃO ATUAL</li>
                                <li style="color: #C0C0C0; clear: none;margin-left:35px" clear="none">NOVA CONDIÇÃO</li>
                             </ul>
                        </li>
                        <li style="margin-top:-2px;height:10px"">
                            <ul class="ulDados2"  >
                                <li style="color: #C0C0C0;width:100px"" ><asp:TextBox  Enabled="false" ID="txtCondAtual" runat="server"  Width="110px" ></asp:TextBox></li>
                                <li style="color: #C0C0C0; clear: none;margin-left:35px" clear="none;"> <asp:DropDownList ID="ddlNovaCond"  ToolTip="Selecione a nova condição" 
                                    CssClass="ddlNomeCaixa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTxtCondAtual_SelectedIndexChanged" Width="110px">
                                        
                                    </asp:DropDownList> </li>
                             </ul>
                            
                        </li>
                    </ul>
                </li>
            </ul>  
       </li>   
       <li style="clear:none">
            <ul>
                <li style="background-color: #5F9EA0; width: 270px;text-align: center; height:15px ; font-size: small;">
                            <asp:Label ID="Label3" runat="server"  Text="MOTIVO" 
                                Font-Bold="True"  Font-Size="8"  ForeColor="yellow"></asp:Label>
                </li>
                <li>
                    <asp:TextBox ID="txtMotivo" runat="server" MaxLength="50" Width="260px"></asp:TextBox>
                </li>
            </ul>
       </li>                 
        
      </ul>       
      <script type="text/javascript">

          /*function loading() {
              $("#divTelaExportacaoCarregando").css("display:block");
          }*/
          
          $(".ddlDataMovimento").change(function (event) {
              $(".divTelaExportacaoCarregando").attr("style", "display: block;");
              $(".divGrdContratos").attr("style", "display: none;");
          });
          $(".ddlTipoMovimento").change(function (event) {
              $(".divTelaExportacaoCarregando").attr("style", "display: block;");
              $(".divGrdContratos").attr("style", "display: none;");
          });
          $(".ddlFuncionarioCaixa").change(function (event) {
              $(".divTelaExportacaoCarregando").attr("style", "display: block;");
              $(".divGrdContratos").attr("style", "display: none;");
          });
      </script>


</asp:Content>







