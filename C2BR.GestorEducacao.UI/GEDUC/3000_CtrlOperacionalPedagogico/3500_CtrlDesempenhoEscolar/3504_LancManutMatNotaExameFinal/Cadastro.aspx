<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3504_LancManutMatNotaExameFinal.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .MT
        {
        }
        .MB
        {
        }
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
            margin-left: 15px;
        }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 806px;
            border: 1px solid #CCCCCC;
            overflow-y: scroll;
        }
        .mascaradecimal
        {
            width: 40px;
            text-align: right;
        }
        
        .lilnkBolCarne
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 2px;
            margin-right: 0px;
            width: 84px;
            height: 14px;
        }
        
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 773px;
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
        .lblHomol
        {
            color: Red;
        }
    </style>
    <!--[if IE]>
<style type="text/css">
       #divBarraLanctoExameFinal { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
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
        <li>
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlDisciplina" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Disciplina deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 13px !important;">
            <asp:ImageButton ID="lbkPesq" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                CssClass="btnPesqDescMed" ToolTip="Pesquisa de acordo com as informações preenchidas nos parâmetros"
                OnClick="lbkPesq_OnClick" />
        </li>
        <li class="liClear" runat="server" visible="false" id="ligrid">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdBusca_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="CO_ALU_CAD" HeaderText="NIRE / STATUS">
                            <ItemStyle Width="160px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="340px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MB1">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMB1" runat="server" Text='<%# bind("MB1") %>'></asp:Label>
                                <asp:Label ID="lblHomol1" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_1") %>'
                                    class="lblHomol"></asp:Label>
                                <asp:HiddenField runat="server" ID="hidSituAlu" Value='<%# bind("CO_SIT_MAT") %>' />
                                <asp:HiddenField runat="server" ID="tipo" ClientIDMode="Static" Value='<%# bind("TIPO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MB2">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMB2" runat="server" Text='<%# bind("MB2") %>'></asp:Label>
                                <asp:Label ID="lblHomol2" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_2") %>'
                                    class="lblHomol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MB3">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMB3" runat="server" Text='<%# bind("MB3") %>'></asp:Label>
                                <asp:Label ID="lblHomol3" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_3") %>'
                                    class="lblHomol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MB4">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMB4" runat="server" Text='<%# bind("MB4") %>'></asp:Label>
                                <asp:Label ID="lblHomol4" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_4") %>'
                                    class="lblHomol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Trimestre--%>
                        <asp:TemplateField HeaderText="MT1">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMT1" runat="server" Text='<%# bind("MT1") %>'></asp:Label>
                                <asp:Label ID="lblHomol5" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_5") %>'
                                    class="lblHomol"></asp:Label>
                                <%--<asp:HiddenField runat="server" ID="hidSituAlu" Value='<%# bind("CO_SIT_MAT") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MT2">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMT2" runat="server" Text='<%# bind("MT2") %>'></asp:Label>
                                <asp:Label ID="lblHomol6" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_6") %>'
                                    class="lblHomol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MT3">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtMT3" runat="server" Text='<%# bind("MT3") %>'></asp:Label>
                                <asp:Label ID="lblHomol7" runat="server" Text="*" Visible='<%# bind("FL_HOMOL_VISIBLE_7") %>'
                                    class="lblHomol"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Trimestre--%>
                        <asp:TemplateField HeaderText="SB">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtSB" runat="server" Text='<%# bind("VL_MEDIA_FINAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Trimestre--%>
                        <asp:TemplateField HeaderText="ST">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="txtST" runat="server" Text='<%# bind("VL_MEDIA_FINAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Trimestre--%>
                        <asp:BoundField DataField="CO_STA_APROV_MATERIA" HeaderText="Situação">
                            <ItemStyle Width="75px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Prova Final">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtProvaFinal" CssClass="mascaradecimal" runat="server" Enabled='<%# bind("ENABLED_V") %>'
                                    Text='<%# bind("VL_PROVA_FINAL_V") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="margin-top: 10px; float: left;">
                <asp:Label ID="lblLegenBim" runat="server" Visible="false">
                    Legenda: MB(Média do Bimestre) - SB(Síntese dos Bimestres) - <span class="lblHomol">
                        *</span>(Nota não Homologada)</asp:Label>
                <asp:Label ID="lblLegenTri" runat="server" Visible="false">
                    Legenda: MT(Média do Trimestre) - ST(Síntese dos Trimestres) - <span class="lblHomol">
                        *</span>(Nota não Homologada)</asp:Label>
            </div>
            <div class="lilnkBolCarne" style="float: right; margin-top: 10px">
                <asp:LinkButton runat="server" ID="lnkImprime" OnClick="lnkImprime_OnClick" Text="Versão Impressão"
                    ToolTip="Gera a versão para impressão da grid apresentada"></asp:LinkButton>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {

            //$(".txtMB").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
