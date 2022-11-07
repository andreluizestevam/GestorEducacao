<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.MatriculaLancFormaPgto.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados
        {
            width:748px;
            margin-left:150px !Important;
        }
        .ulDad li
        {
            margin-left:5px;
            margin-top:3px;
        }
           .divEsquePgto
        {
            border-right: 1px solid #CCCCCC;
            margin-top:15px;
            margin-right:20px !Important;
            width: 165px;
            height: 180px;
            float: left;
        }
        .divDirePgto
        {
            margin-top:15px;
            width: 550px;
            height: 180px;
            float: right;
        }
        .divFiltrotop
        {
           border-bottom: 1px solid #CCCCCC; 
           height:75px;
           width:740px;
        }
         .chkCadBasAlu label
        {
            display: inline !important;
        }
        .lblchkPgto
        {
            font-weight:bold;
            color: #FFA07A;
            margin-left:-5px;
        }
        .divGrdChequePgto
        {
            border: 1px solid #CCCCCC;
            width: 720px;
            height: 130px;
            overflow-y: scroll;
        }
        .liMeioSobe
        {
             margin-top:-7px;
        }
        .maskDin
        {
            text-align:left;
        }
                /*--> CSS DADOS */
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraComoChegar
        {
            position: absolute;
            margin-left: 750px;
            margin-top: -45px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraComoChegar ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraComoChegar ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraComoChegar ul li img
        {
            width: 19px;
            height: 19px;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar">
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                        src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="BTNliNovo">
                <asp:LinkButton runat="server" ID="btnNovo" CssClass="btnNovo" OnClick="btnNovo_Click">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                        </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                <asp:LinkButton runat="server" ID="btnGravar" CssClass="btnGravar" OnClick="btnGravar_Click">
                    <img title="Grava o registro atual na base de dados." alt="Icone de Gravar o Registro."
                        src="/BarrasFerramentas/Icones/Gravar.png" />
                        </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <asp:LinkButton OnClientClick="if (!confirm('Deseja realmente Excluir o Registro?')) return false;"
                     ID="btnDelete" CssClass="btnDelete" runat="server" OnClick="btnDelete_Click">
                        <img title="Exclui o Registro atual selecionado."
                                alt="Icone de Excluir o Registro." 
                                src="/BarrasFerramentas/Icones/Excluir.png" />
                    </asp:LinkButton>
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <ul class="ulDados">
        <li>
            <div class="divFiltrotop">
                <ul class="ulDad">
                    <li class="liClear" style="margin-left: 80px;">
                        <asp:HiddenField runat="server" ID="hdCoEmpMatr" />
                        <asp:HiddenField runat="server" ID="hdCoAluMatr" />
                        <asp:HiddenField runat="server" ID="hdCoCurMatr" />
                        <asp:HiddenField runat="server" ID="hdCoAnoMatr" />
                        <asp:HiddenField runat="server" ID="hdCoMatrMat" />
                        <asp:HiddenField runat="server" ID="hdAuxQueryString" />
                        <asp:HiddenField runat="server" ID="hdValTotContrato" />
                        <label for="ddlAno" class="lblObrigatorio">
                            Ano</label>
                        <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liLeft">
                        <label for="ddlModalidade" class="lblObrigatorio">
                            Modalidade</label>
                        <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                            ToolTip="Selecione uma Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liClear">
                        <label for="ddlSerieCurso" class="lblObrigatorio">
                            Série/Curso</label>
                        <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                            Width="140px" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="liLeft">
                        <label for="ddlTurma" class="lblObrigatorio">
                            Turma</label>
                        <asp:DropDownList ID="ddlTurma" AutoPostBack="true" CssClass="campoTurma" runat="server"
                            Width="220px" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li style="clear: both; margin-left: 150px;">
                        <label title="Aluno" class="lblObrigatorio">
                            Aluno</label>
                        <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa"
                            Width="300px" OnSelectedIndexChanged="ddlAlunos_OnSelectedIndexChanged" AutoPostBack="true"
                            runat="server">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                            Nº NIRE</label>
                        <asp:TextBox runat="server" ID="txtNireAlunoPgto" Width="60px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Nº NIS</label>
                        <asp:TextBox runat="server" ID="txtNisAlunoPgto" Width="70px"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
        <li style="clear: both;">
            <div class="divEsquePgto">
                <ul>
                    <li>
                        <label>
                            Nº Contrato</label>
                        <asp:TextBox runat="server" ID="txtNrContraPgto"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <label>
                            R$ Contrato</label>
                        <asp:TextBox runat="server" ID="txtValContrPgto" Enabled="false" CssClass="campoMoeda"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            QtP</label>
                        <asp:TextBox runat="server" ID="txtQtPgto" Width="15px"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <asp:CheckBox Checked="true" ClientIDMode="Static" ID="chkAtuFinPgto" class="chkCadBasAlu"
                            runat="server" Text="Atualizar Financeiro" />
                    </li>
                    <li style="clear: both;">
                        <asp:CheckBox Checked="true" ClientIDMode="Static" ID="chkCadChePgto" class="chkCadBasAlu"
                            runat="server" Text="Cadastrar Cheques" />
                    </li>
                    <li style="clear: both;">
                        <asp:CheckBox ClientIDMode="Static" ID="chkDinhePgto" class="chkCadBasAlu" runat="server"
                            OnCheckedChanged="chkDinhePgto_OnCheckedChanged" AutoPostBack="true" />
                        <asp:Label runat="server" ID="lblDinPgto" class="lblchkPgto">Dinheiro</asp:Label>
                        <asp:TextBox runat="server" ID="txtValDinPgto" Width="50px" CssClass="maskDin" Enabled="false"></asp:TextBox>
                    </li>
                    <li style="clear: both;">
                        <asp:CheckBox ClientIDMode="Static" ID="chkOutrPgto" class="chkCadBasAlu" runat="server"
                            OnCheckedChanged="chkOutrPgto_OnCheckedChanged" AutoPostBack="true" />
                        <asp:Label runat="server" ID="lblvlPgto" class="lblchkPgto">Boleto</asp:Label>
                        <asp:TextBox runat="server" ID="txtValOutPgto" Width="50px" CssClass="maskDin" Enabled="false"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div class="divDirePgto">
                <ul>
                    <li style="margin-bottom: 4px; margin-left: -4px;">
                        <asp:CheckBox runat="server" ID="chkCartaoCreditoPgto" OnCheckedChanged="chkCartaoCreditoPgto_OnCheckedChanged"
                            AutoPostBack="true" />
                        <asp:Label runat="server" ID="lblCarCrePgto" class="lblchkPgto">Cartão de Crédito</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <label>
                            Bandeira</label>
                        <ul>
                            <li>
                                <asp:HiddenField runat="server" ID="hdIDCC1" />
                                <asp:DropDownList runat="server" ID="ddlBandePgto1" Enabled="false" OnSelectedIndexChanged="ddlBandePgto1_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                    <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                    <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                    <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                    <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                    <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                    <asp:ListItem Text="SoroCred" Value="SorCr"></asp:ListItem>
                                    <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both">
                                <asp:HiddenField runat="server" ID="hdIDCC2" />
                                <asp:DropDownList runat="server" ID="ddlBandePgto2" Enabled="false" OnSelectedIndexChanged="ddlBandePgto2_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                    <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                    <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                    <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                    <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                    <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                    <asp:ListItem Text="SoroCred" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both">
                                <asp:HiddenField runat="server" ID="hdIDCC3" />
                                <asp:DropDownList runat="server" ID="ddlBandePgto3" Enabled="false" OnSelectedIndexChanged="ddlBandePgto3_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Nenhum" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Visa" Value="Vis"></asp:ListItem>
                                    <asp:ListItem Text="MasterCard" Value="MasCar"></asp:ListItem>
                                    <asp:ListItem Text="HiperCard" Value="HipCar"></asp:ListItem>
                                    <asp:ListItem Text="Elo" Value="Elo"></asp:ListItem>
                                    <asp:ListItem Text="American Express" Value="AmeExp"></asp:ListItem>
                                    <asp:ListItem Text="Cartão BNDES" Value="BNDES"></asp:ListItem>
                                    <asp:ListItem Text="SoroCred" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Diners Club" Value="DinClub"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Número</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtNumPgto1" CssClass="numeroCartao" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both;" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNumPgto2" CssClass="numeroCartao" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNumPgto3" CssClass="numeroCartao" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Titular</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtTitulPgto1" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtTitulPgto2" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtTitulPgto3" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Venc</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtVencPgto1" Width="30px" CssClass="campoVenc" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtVencPgto2" Width="30px" CssClass="campoVenc" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtVencPgto3" Width="30px" CssClass="campoVenc" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            R$ Crédito</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtValCarPgto1" Width="50px" CssClass="maskDin" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtValCarPgto2" Width="50px" CssClass="maskDin" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtValCarPgto3" Width="50px" CssClass="maskDin" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li style="clear: both; margin-top: 10px; margin-bottom: 4px; margin-left: -4px;">
                        <asp:CheckBox runat="server" ID="chkDebitPgto" OnCheckedChanged="chkDebitPgto_OnCheckedChanged"
                            AutoPostBack="true" />
                        <asp:Label runat="server" ID="Label2" class="lblchkPgto">Cartão de Débito</asp:Label>
                    </li>
                    <li style="clear: both;">
                        <label>
                            Bco</label>
                        <ul>
                            <li>
                                <asp:HiddenField runat="server" ID="hdIDCDebt1" />
                                <asp:DropDownList runat="server" ID="ddlBcoPgto1" Enabled="false" OnSelectedIndexChanged="ddlBcoPgto1_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both">
                                <asp:HiddenField runat="server" ID="hdIDCDebt2" />
                                <asp:DropDownList runat="server" ID="ddlBcoPgto2" Enabled="false" OnSelectedIndexChanged="ddlBcoPgto2_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                            <li style="clear: both">
                                <asp:HiddenField runat="server" ID="hdIDCDebt3" />
                                <asp:DropDownList runat="server" ID="ddlBcoPgto3" Enabled="false" OnSelectedIndexChanged="ddlBcoPgto3_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Agência</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtAgenPgto1" Width="60px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtAgenPgto2" Width="60px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtAgenPgto3" Width="60px" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Nº Conta</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtNContPgto1" Width="60px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNContPgto2" Width="60px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNContPgto3" Width="60px" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Número</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtNuDebtPgto1" Width="80px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNuDebtPgto2" Width="80px" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNuDebtPgto3" Width="80px" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            Titular</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtNuTitulDebitPgto1" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNuTitulDebitPgto2" Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtNuTitulDebitPgto3" Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                    <li>
                        <label>
                            R$ Débito</label>
                        <ul>
                            <li>
                                <asp:TextBox runat="server" ID="txtValDebitPgto1" Width="50px" CssClass="maskDin"
                                    Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtValDebitPgto2" Width="50px" CssClass="maskDin"
                                    Enabled="false"></asp:TextBox></li>
                            <li style="clear: both" class="liMeioSobe">
                                <asp:TextBox runat="server" ID="txtValDebitPgto3" Width="50px" CssClass="maskDin"
                                    Enabled="false"></asp:TextBox></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </li>
        <li style="clear: both; margin-left: 10px;">
            <ul>
                <li style="margin-bottom: 4px; margin-left: -4px;">
                    <asp:CheckBox runat="server" ID="chkChequePgto" OnCheckedChanged="chkChequePgto_OnCheckedChanged"
                        AutoPostBack="true" />
                    <asp:Label runat="server" ID="Label22" class="lblchkPgto">Cheque</asp:Label>
                </li>
                <li style="clear: both">
                    <asp:LinkButton runat="server" ID="btnMaisLinhaChequePgto" OnClick="btnMaisLinhaChequePgto_OnClick">Adicionar Linha</asp:LinkButton>
                </li>
                <li style="clear: both;">
                    <div class="divGrdChequePgto">
                        <asp:GridView ID="grdChequesPgto" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 15px;" AutoGenerateColumns="false" Enabled="false">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkselectGridPgtoCheque" runat="server" OnCheckedChanged="chkselectGridPgtoCheque_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bco">
                                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCodPgtoChe" Value='<%# Bind("IDChePgto") %>' />
                                        <asp:HiddenField runat="server" ID="hdddlBcoChePgto" Value='<%# Bind("BCO") %>' />
                                        <asp:DropDownList runat="server" Width="50px" ID="ddlBcoChequePgto" Style="margin: 0px !Important;" Enabled="false">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agência">
                                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAgenChequePgto" Style="margin: 0px; width: 50px;" runat="server"
                                            Text='<%# Bind("AgenChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº Conta">
                                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNrContaChequeConta" Style="margin: 0px; width: 50px;" runat="server"
                                            Text='<%# Bind("nuConChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº Cheque">
                                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNrChequePgto" Style="margin: 0px; width: 60px;" runat="server"
                                            Text='<%# Bind("nuCheChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CPF">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNuCpfChePgto" CssClass="campoCpf" Style="margin: 0px; width: 75px;"
                                            runat="server" Text='<%# Bind("nuCpfChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Titular">
                                    <ItemStyle HorizontalAlign="Center" Width="125px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTitulChequePgto" Style="margin: 0px; width: 120px;" runat="server"
                                            Text='<%# Bind("noTituChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R$ Cheque">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVlChequePgto" CssClass="maskDin" Style="margin: 0px; width: 40px;"
                                            runat="server" Text='<%# Bind("vlCheChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt Venc">
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDtVencChequePgto" Style="margin: 0px;" runat="server" CssClass="campoData"
                                            Text='<%# Bind("dtVencChe") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeroCartao").mask("9999.9999.9999.9999");
            $(".campoCpf").mask("999.999.999-99");
            $(".maskDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".campoVenc").mask("99/99");
        });
    </script>
</asp:Content>
