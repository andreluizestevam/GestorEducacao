<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3520_LancNotaAtivPorMateriaSupremoInsef.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 818px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 0px;
        }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 385px;
            width: 990px;
            overflow-y: scroll;
            overflow-x: scroll;
            margin-left: -87px;
            border: 1px solid #CCC;
        }
        .mascaradecimal
        {
            width: 25px;
            text-align: right;
        }
        .ddlTrimestre
        {
            width: 75px;
        }
        .ddlClassi
        {
            width: 70px;
        }
        .liPesqAtiv
        {
            margin-top: 10px;
        }
        .liPesqAtiv img
        {
            width: 14px;
            height: 14px;
        }
        
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 749px !important;
            margin-top: -50px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 250px !important;
            padding: 3px 0;
        }
        #divBarraLanctoExameFinal ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraLanctoExameFinal ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraLanctoExameFinal ul li img
        {
            width: 19px;
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="divMensEfe" style="display: none; position: absolute; left: 50%; right: 50%;
        top: 50%; z-index: 1000; width: 150px" clientidmode="Static">
        <p id="pEfeM" clientidmode="Static" style="color: #6495ED; font-weight: bold; text-align: center;">
        </p>
        <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif"
            Style="z-index: 99"></asp:Image>
    </div>
    <div id="div1" class="bar">
        <div id="divBarraLanctoExameFinal" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 43px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 43px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                        src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static"
                        OnClientClick="geraPadrao(this);">
                        <img title="Grava o registro atual na base de dados."
                             alt="Icone de Gravar o Registro." 
                             src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 43px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela TODAS as alterações feitas no registro." alt="Icone de Cancelar Operacao Atual."
                        src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li id="btnPesquisar">
                    <img title="Volta ao formulário de pesquisa." alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 25px">
            <label for="ddlAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTrimestre" class="lblObrigatorio" title="Trimestre">
                Trimestre</label>
            <asp:DropDownList ID="ddlTrimestre" CssClass="ddlTrimestre" runat="server" ToolTip="Selecione o Trimestre"
                OnSelectedIndexChanged="ddlTrimestre_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="T1">1º Trimestre</asp:ListItem>
                <asp:ListItem Value="T2">2º Trimestre</asp:ListItem>
                <asp:ListItem Value="T3">3º Trimestre</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTrimestre"
                CssClass="validatorField" ErrorMessage="Trimestre deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlDisciplina" class="lblObrigatorio" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" Style="width: 200px"
                CssClass="campoMateria" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 10px;">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <img title="Clique para gerar Gride de Notas dos Alunos."
                        alt="Icone de Pesquisa das Grides." 
                        src="/Library/IMG/Gestor_BtnPesquisa.png" />
            </asp:LinkButton>
        </li>
        <li class="liClear" style="margin-left: -60px">
            <li style="float: right; margin:5px 175px 2px 0">
                <%--<label>
                    Legenda: * (Visualização de média apenas para efeito de referência, dependendo de
                    homologação final)
                </label>--%>
            </li>
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdBusca_RowDataBound" Width="100%">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                         <asp:TemplateField HeaderText="NIRE / Aluno">
                            <ItemStyle HorizontalAlign="Left" Width="540px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            NIRE / Aluno
                                        </label>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNireAluno" Text='<%# bind("NIREALUNO") %>'></asp:Label>
                                <asp:Label runat="server" style="font-weight:bold; color:Red" ID="lblAste" Text="*" Visible='<%# bind("MOSTRA_ASTERISCO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROVA 1">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="PROVA / TESTE 01">
                                            PRV/TST 1
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv1" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv1" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV1") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV1" Value='<%# bind("DATA_AV1") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV1" Value='<%# bind("ID_NOTA_AV1") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROVA 2">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="PROVA / TESTE 02">
                                            PRV/TST 2
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv2" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv2" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV2") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV2" Value='<%# bind("DATA_AV2") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV2" Value='<%# bind("ID_NOTA_AV2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TRABALHO">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            TRABALHO
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv3" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv3" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV3") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV3" Value='<%# bind("DATA_AV3") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV3" Value='<%# bind("ID_NOTA_AV3") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROJETO">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            PROJETO
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv4" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv4" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV4") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV4" Value='<%# bind("DATA_AV4") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV4" Value='<%# bind("ID_NOTA_AV4") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONCEITO">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            CONCEITO
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv5" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv5" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV5") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV5" Value='<%# bind("DATA_AV5") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV5" Value='<%# bind("ID_NOTA_AV5") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AVALI ESPECÍFICA">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="AVALIAÇÃO ESPECÍFICA">
                                            AVAL ESPEC.
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv6" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv6" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV6") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV6" Value='<%# bind("DATA_AV6") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV6" Value='<%# bind("ID_NOTA_AV6") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AVALI GLOBALIZADA">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="AVALIAÇÃO GLOBALIZADA">
                                            AVAL GLOBAL
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv7" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv7" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV7") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV7" Value='<%# bind("DATA_AV7") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV7" Value='<%# bind("ID_NOTA_AV7") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SIMULADO">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            SIMULADO
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv8" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv8" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV8") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV8" Value='<%# bind("DATA_AV8") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV8" Value='<%# bind("ID_NOTA_AV8") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATIVI AVALIATIVA">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="ATIVIDADE AVALIATIVA">
                                            ATIV AVAL.
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv9" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv9" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV9") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV9" Value='<%# bind("DATA_AV9") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV9" Value='<%# bind("ID_NOTA_AV9") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATIVI PRATICA">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;" title="ATIVIDADE PRÁTICA">
                                            ATIV PRÁT.
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv10" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv10" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV10") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV10" Value='<%# bind("DATA_AV10") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV10" Value='<%# bind("ID_NOTA_AV10") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REDACAO">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <ul style="text-align: center;">
                                    <li style="margin: 0px 0px 0px 10px !important;">
                                        <label style="font-weight: bold !important;">
                                            REDAÇÃO
                                        </label>
                                    </li>
                                    <li style="clear: both; margin: 0px 0px 0px 4px !important;">
                                        <asp:TextBox ID="txtDataAtiv11" CssClass="campoData" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaAv11" CssClass="mascaradecimal" MaxLength = "4" runat="server" Text='<%# bind("NOTA_AV11") %>'  Enabled='<%# bind("MEDIA_HOMOLOGADA") %>'></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidDtAV11" Value='<%# bind("DATA_AV11") %>' />
                                <asp:HiddenField runat="server" ID="hidIdNotaAV11" Value='<%# bind("ID_NOTA_AV11") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AV5 / DATA">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <label style="font-weight: bold !important;">
                                    MB *</label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMedia" runat="server" Text='<%# bind("MDVALOR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <ul>
    </ul>
    <script type="text/javascript">

        function SetarDataV1() {

            var dia = new Date();
            var dd = dia.getDate();
            var mm = dia.getMonth() + 1;
            var yyyy = dia.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm

            }

            var today = dd + '/' + mm + '/' + yyyy;
            //document.getElementById('txtDataAtiv1').attributes = today; //innerHTML
            $("#txtDataAtiv1").val(today);
            //            alert(today);
        }
        $(document).ready(function () {
            $(".mascaradecimal").maskMoney({ symbol: "", thousands: '', decimal: ',', precision: 2, allowZero: true });
            $(".mascaradecimal").attr('maxlength', '5');
            desableDatePicker();
        });

        function geraPadrao(el) {

            if (el == document.getElementById("btnPesqGride")) {
                document.getElementById("pEfeM").innerHTML = "PREENCHENDO GRID...";
            }
            if (el == document.getElementById("btnSave")) {
                document.getElementById("pEfeM").innerHTML = "SALVANDO INFORMAÇÕES...";
            }

            //Mostra a mensagem de Efetivando matrícula e a imagem de Carregando.
            $("#divMensEfe").css("display", "block");

            setInterval(function () {
                $("#pEfeM").fadeIn();
            }, 800);

            setInterval(function () {
                $("#pEfeM").fadeOut();
            }, 400);
        }

        function desableDatePicker() {
            $(".grdBusca").each(function () {
                if ($(".mascaradecimal").attr("disabled") == true) {
                    $(".ui-datepicker-trigger").hide();
                    $(".campoData").attr("disabled", "disabled");
                }
            });
        }
    </script>
</asp:Content>
