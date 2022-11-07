<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3505_LancManutMatNotaAluConsClasse.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 720px; }
        .ulDados input { margin-bottom: 0; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear { clear: both; }
        .liEspaco { margin-left: 2px; }
        
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 660px;
            overflow-y: auto;
        }
        .mascaradecimal
        {
            width: 40px;
            text-align: right;
        }
                
        #divBarraPadraoContent { display:none; }
        
        #divBarraLanctoConseClass { position:absolute; margin-left: 773px; margin-top:-50px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraLanctoConseClass ul { display: inline; float: left; margin-left: 10px; }
        #divBarraLanctoConseClass ul li { display: inline; margin-left: -2px; }
        #divBarraLanctoConseClass ul li img { width: 19px; height: 19px; }  
        
    </style>

<!--[if IE]>
<style type="text/css">
       #divBarraLanctoConseClass { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar" > 
            <div id="divBarraLanctoConseClass" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
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
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlAluno" title="Aluno em Conselho">
                Aluno em Conselho</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno em Conselho" CssClass="campoMateria" AutoPostBack="true" runat="server"
                OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAluno"
                ErrorMessage="Aluno deve ser informada" Display="None"></asp:RequiredFieldValidator>
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
                        <asp:BoundField DataField="NO_SIGLA_MATERIA" HeaderText="Sigla">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_MATERIA" HeaderText="Matéria">
                            <ItemStyle Width="220px" />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="CO_STA_APROV_MATERIA" HeaderText="Situação">
                            <ItemStyle Width="75px" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="SB">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtSB" CssClass="mascaradecimal" Enabled="false" runat="server" Text='<%# bind("SB") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prova Final" >
                            <ItemStyle HorizontalAlign="Center"  />                            
                            <ItemTemplate >
                                <asp:TextBox ID="txtProvaFinal" Enabled="false" CssClass="mascaradecimal" runat="server" Text='<%# bind("VL_PROVA_FINAL") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Media Final">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMediaFinal" Enabled="false" CssClass="mascaradecimal" runat="server" Text='<%# bind("VL_MEDIA_FINAL") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Conselho">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hdMat" runat="server" value='<%# bind("CO_MAT") %>'></asp:HiddenField>
                                <asp:DropDownList ID="txtNotaConselho" Width="70px" runat="server" Enabled='<%# bind("ENABLED") %>'>
                                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text = "Aprovado" Value="A"></asp:ListItem>
                                <asp:ListItem Text = "Reprovado" Value="R"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function() {
            //$(".txtMB").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
