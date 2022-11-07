<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3507_CalculoMediaEspecifico.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 818px; }
        .ulDados input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear { clear: both; }
        .liEspaco { margin-left:15px; }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 700px;
            overflow-y: auto;
        }
        .txtNota
        {
            width: 40px;
            text-align: center;
        }
        .ddlBimestre { width: 75px; }
        .liPesqAtiv { margin-top: 10px; }
         
        #divBarraPadraoContent { display:none; }
        
        #divBarraLanctoExameFinal { position:absolute; margin-left: 773px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraLanctoExameFinal ul { display: inline; float: left; margin-left: 10px; }
        #divBarraLanctoExameFinal ul li { display: inline; margin-left: -2px; }
        #divBarraLanctoExameFinal ul li img { width: 19px; height: 19px; }  
        
    </style>

<!--[if IE]>
<style type="text/css">
       #divBarraLanctoExameFinal { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar" > 
            <div id="divBarraLanctoExameFinal" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
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
                        <img title="Cancela TODAS as alterações feitas no registro."
                                alt="Icone de Cancelar Operacao Atual." 
                                src="/BarrasFerramentas/Icones/Cancelar.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li  id="btnPesquisar">
                        <img title="Volta ao formulário de pesquisa."
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
            <label for="ddlAno"  class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade"  class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso"  class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma"  class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlBimestre" class="lblObrigatorio"  title="Bimestre">Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server"
                ToolTip="Selecione o Bimestre">
                <asp:ListItem Value="B1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="B2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="B3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="B4">4º Bimestre</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="Bimestre deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAluno" title="Aluno">Aluno</label>
            <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa" runat="server"
                ToolTip="Aluno"></asp:DropDownList>
        </li>
        <li class="liEspaco">
            <label for="ddlDisciplina"  class="lblObrigatorio" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Disciplina deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>   
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <asp:Label runat="server" ID="lblBoleto" Text="Calcular Média"></asp:Label>
            </asp:LinkButton>
        </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" 
                    AutoGenerateColumns="False" onrowdatabound="grdBusca_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="DescNU_NIRE" HeaderText="NIRE">
                            <ItemStyle Width="60px" HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="340px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="CO_BIMESTRE" HeaderText="Bimestre">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>                     
                        <asp:TemplateField HeaderText="NOTA 1">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota1" CssClass="txtNota" runat="server" Text='<%# bind("NOTA1") %>' Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOTA 2">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota2" CssClass="txtNota" runat="server" Text='<%# bind("NOTA2") %>' Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SIMUL">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota3" CssClass="txtNota" runat="server" Text='<%# bind("NOTA3") %>' Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SM EXT">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNotaS" CssClass="txtNota" runat="server" Text='<%# bind("NOTA_SIMU") %>' Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MÉDIA">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMedia" CssClass="txtNota" runat="server" Text='<%# bind("MEDIA") %>' Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function() {
            //$(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
