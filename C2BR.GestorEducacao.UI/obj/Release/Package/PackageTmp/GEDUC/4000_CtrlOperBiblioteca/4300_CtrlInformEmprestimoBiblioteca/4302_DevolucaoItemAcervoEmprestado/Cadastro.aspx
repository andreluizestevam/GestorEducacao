<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4302_DevolucaoItemAcervoEmprestado.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados{width: 781px; margin-top:15px;}
    
    /*--> CSS LIs */
    .litxtTotalMulta, .liTotalIsencao, .liDtBaixa {margin-top:15px;margin-left:5px;}
    .liRespBaixa {margin-top:15px;}
    .liValorPago {margin-top:15px;margin-left:5px;margin-right:10px;}
    .lichkBoleto {margin-top:13px;margin-left:25px;}
    .liTipoUsuario {margin-right:10px;margin-left:115px;}
    .liUnidadeUsuario { margin-right:10px;}
    .liBarraTitulo { background-color: #EEEEEE;margin-bottom: 2px; padding: 5px; text-align: center; width: 713px; height:10px; clear:both}
    .liClear { clear: both; }
    
    /*--> CSS DADOS */
    .ddlTipoUsuario { width: 76px; }
    .ddlUnidade { width: 210px; margin-top:13px; margin-left:-100px;}
    .ddlUsuario { width: 210px; }
    .chkBoleto {margin-left:-5px !important;}
    .ddlBoleto {width:220px;}
    .txtResponsavel {width:179px;}
    .txtTotalMulta {width:60px; text-align: right;}
    .txtTotalIsencao {width:72px; text-align: right;}
    .txtValorPago {width:60px; text-align: right;}
    .txtPagEmprestimo, .txtPagEntrega, .txtMultaPag {width:40px; text-align: right;}   
    .divGrid{height: 150px;width: 745px;overflow-y: scroll;margin-top: 10px;border-bottom: solid #EEEEEE 1px;border-top: solid #EEEEEE 1px;border-left: solid #EEEEEE 1px;}
    
    #divBarraPadraoContent{display:none;}
    #divBarraBaixaEmpre { position:absolute; margin-left: 770px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraBaixaEmpre ul { display: inline; float: left; margin-left: 10px; }
    #divBarraBaixaEmpre ul li { display: inline; margin-left: -2px; }
    #divBarraBaixaEmpre ul li img { width: 19px; height: 19px; }
             
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraEmpreBibli { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<div id="div1" class="bar" > 
        <div id="divBarraBaixaEmpre" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnBaixaEmpre_Click">
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
            <li  id="Li1">
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
    <li class="liTipoUsuario">
        <label for="ddlTipoUsuario" class="lblObrigatorio" title="Tipo de Usuário">Tipo de Usuário</label>
        <asp:DropDownList ID="ddlTipoUsuario" CssClass="ddlTipoUsuario" runat="server" AutoPostBack="true" 
            ToolTip="Selecione o Tipo de Usuário" 
            onselectedindexchanged="ddlTipoUsuario_SelectedIndexChanged">
            <asp:ListItem Value="">Selecione</asp:ListItem>
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="P">Professor</asp:ListItem>
            <asp:ListItem Value="F">Funcionário</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
            runat="server" ControlToValidate="ddlTipoUsuario" 
            ErrorMessage="Tipo de Usuário deve ser informado"> </asp:RequiredFieldValidator>
    </li>
    <li>
        <asp:Label id="lblUnidadeUsuario" for="ddlUnidadeUsuario" class="lblObrigatorio lblUnidadeUsuario" title="Unidade do Usuário" runat="server">Unidade do Usuário</asp:Label>
    </li>
    <li class="liUnidadeUsuario">
        <asp:DropDownList ID="ddlUnidadeUsuario"
            ToolTip="Selecione a Unidade do Usuário" AutoPostBack="true"
            CssClass="ddlUnidade" runat="server" 
            onselectedindexchanged="ddlUnidadeUsuario_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField" ControlToValidate="ddlUnidadeUsuario" 
            ErrorMessage="Unidade do Usuário deve ser informada"> </asp:RequiredFieldValidator>
    </li>
    <li class="liUsuario">
        <label for="ddlUsuario" class="lblObrigatorio" title="Usuário">Usuário</label>
        <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="ddlUsuario" 
            ToolTip="Selecione o Usuário" AutoPostBack="true"
            onselectedindexchanged="ddlUsuario_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
            runat="server" ControlToValidate="ddlUsuario" 
            ErrorMessage="Usuário deve ser informado"> </asp:RequiredFieldValidator>
    </li>
    <li class="liClear">
        <div id="divGrid" runat="server" class="divGrid">
            <label class="liBarraTitulo">ITEN(S) EMPRESTADO(S) DE BIBLIOTECA</label>
            <asp:GridView ID="grdItensEmprestados" CssClass="grdBusca" Width="720px" runat="server" 
                AutoGenerateColumns="False" OnRowDataBound="grdItensEmprestados_RowDataBound">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataTemplate>
                    <table rules="all" cellspacing="0" border="1" style="width: 720px; border-collapse: collapse; margin-left: -5px; margin-top: -2px;" class="grdBusca">
                        <tbody>
                            <tr>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 20px;" scope="col">Check</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 200px;" scope="col">ISBN</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 100px;" scope="col">Código interno</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 350px;" scope="col">Título da Obra</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 60px;" scope="col">Nº EMPR</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 60px;" scope="col">Previsão</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 40px;" scope="col">Dias</th>
                                <th style="width: 0px;" scope="col">&nbsp;</th>
                                <th style="width: 60px;" scope="col">Multa R$</th>
                            </tr>
                        </tbody>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Check">
                        <ItemStyle Width="20px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="ckSelect_CheckedChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CO_ISBN_ACER" HeaderText="ISBN">
                        <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_CTRL_INTERNO" HeaderText="Código interno">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NO_ACERVO" HeaderText="Título da Obra">
                        <ItemStyle Width="350px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CO_NUM_EMP" HeaderText="Nº EMPR">
                        <ItemStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_PREV_DEVO_ACER" DataFormatString="{0:d}" HeaderText="Previsão">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Dias">
                        <ItemStyle Width="40px" HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblDiasGrid" runat="server" Text='<%# bind("DIAS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VL_MULT_ATRASO" HeaderText="Multa R$">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </li>
    <li>
        <div id="divGridBaixa" runat="server" class="divGrid">
            <label class="liBarraTitulo">ITEN(S) DE BAIXA DE BIBLIOTECA</label>
            <asp:GridView ID="grdItensBaixa" CssClass="grdBusca" Width="720px" runat="server" AutoGenerateColumns="False" onrowdeleting="grdItensBaixa_RowDeleting">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataTemplate>
                        <table rules="all" cellspacing="0" border="1" style="width: 720px; border-collapse: collapse; margin-left: -5px; margin-top: -2px;" class="grdBusca">
                            <tbody>
                                <tr>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 100px;" scope="col">Código interno</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 370px;" scope="col">Título da Obra</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 60px;" scope="col">OBS.Empr</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 60px;" scope="col">OBS.Entrega</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 60px;" scope="col">Pag.Empr</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 60px;" scope="col">Pag.Entr</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 60px;" scope="col">Multa R$</th>
	                                <th style="width: 0px;" scope="col">&nbsp;</th>
	                                <th style="width: 20px;" scope="col">Isento</th>
                                </tr>
                            </tbody>
                        </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CodigoInterno" HeaderText="Código interno">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TituloObra" HeaderText="Título da Obra">
                        <ItemStyle Width="370px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="OBS.Empr">
                        <ItemStyle Width="60px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtOBSEmprestimo" Enabled="false" CssClass="txtOBSEmprestimo" runat="server" Text='<%# bind("ObsEmpr") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OBS.Entrega">
                        <ItemStyle Width="60px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtOBSEntrega" Enabled="true" CssClass="txtOBSEntrega" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pag.Empr">
                        <ItemStyle Width="60px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtPagEmprestimo" Enabled="false" CssClass="txtPagEmprestimo" runat="server" Text='<%# bind("NumPagEmpr") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pag.Entr">
                        <ItemStyle Width="60px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtPagEntrega" Enabled="true" CssClass="txtPagEntrega" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Multa R$">
                        <ItemStyle Width="60px" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtMultaPag" Enabled="false" CssClass="txtMultaPag" runat="server" Text='<%# bind("VlMulta") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Isento">
                        <ItemStyle Width="20px" HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckIsento" runat="server" OnCheckedChanged="ckIsento_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderStyle-Width="0px" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Library/IMG/Gestor_BtnDel.png" />
                </Columns>
            </asp:GridView>
        </div>
    </li>
    <li class="liDtBaixa">
        <label for="txtDataBaixa" title="Data de Baixa">Data Baixa</label>
        <asp:TextBox ID="txtDataBaixa" runat="server" CssClass="campoData" ToolTip="Informe a Data de Baixa" Enabled="false"></asp:TextBox>
    </li>
    <li class="litxtTotalMulta">
        <label for="txtTotalMulta" title="Total da Multa">Total Multa R$</label>
        <asp:TextBox ID="txtTotalMulta" CssClass="txtTotalMulta" Enabled="false" ToolTip="Total da Multa" runat="server">
        </asp:TextBox>
    </li>
    <li class="liValorPago">
        <label for="txtValorPago" title="Valor Pago">Valor Pago R$</label>
        <asp:TextBox ID="txtValorPago" CssClass="txtValorPago" ToolTip="Valor Pago" runat="server">
        </asp:TextBox>
    </li>
    <li class="liRespBaixa">
        <label for="txtResponsavel" title="Responsável">Responsável pelo Cadastro</label>
        <asp:TextBox ID="txtResponsavel" CssClass="txtResponsavel" Enabled="false"
            ToolTip="Responsável" runat="server">
        </asp:TextBox>
    </li>
    <li class="liTotalIsencao">
        <label for="txtTotalIsencao" title="Total de Isenção">Total Isenção R$</label>
        <asp:TextBox ID="txtTotalIsencao" CssClass="txtTotalIsencao" Enabled="false" ToolTip="Total de Isenção" runat="server">
        </asp:TextBox>
    </li>
    <li class="lichkBoleto">
        <label runat="server" id="lblckImprimeBoleto" for="ckImprimeBoleto" class="lblObrigatorio" title="Imprime Boleto">Imprimir Boleto Bancário</label>
        <asp:CheckBox ID="ckImprimeBoleto" runat="server" ToolTip="Imprimir Boleto Bancário"
            CssClass="chkBoleto" oncheckedchanged="ckImprimeBoleto_CheckedChanged" AutoPostBack="true"/>
        <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto"
            ToolTip="Selecione o Boleto Bancário">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvBoleto" runat="server" CssClass="validatorField"
            Enabled="false"
            ControlToValidate="ddlBoleto"
            ErrorMessage="Boleto deve ser informado">
        </asp:RequiredFieldValidator>
    </li>
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtValorPago").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        $(".txtPagEntrega").mask("?9999");
    });       
</script>
</asp:Content>
