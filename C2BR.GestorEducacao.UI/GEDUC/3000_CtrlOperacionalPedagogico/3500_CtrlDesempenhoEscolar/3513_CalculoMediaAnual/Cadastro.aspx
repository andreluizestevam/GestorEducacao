<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3513_CalculoMediaAnual.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 700px; }
        .ulDados input { margin-bottom: 0; }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear { clear: both; }
        .liEspaco { margin-left:5px; }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 695px;
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
                    <asp:LinkButton ID="btnSave" runat="server">
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
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None">*</asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None">*</asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None">*</asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None">*</asp:RequiredFieldValidator>
        </li>
 
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnCalcular" runat="server" 
                ValidationGroup="vgBtnPesqGride" onclick="btnCalcular_Click">
                <asp:Label runat="server" ID="lblCalcular" Text="Calcular Média Anual"></asp:Label>
            </asp:LinkButton>
        </li>
        <li class="liClear">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" 
                    AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="nomeMateria" HeaderText="MATÉRIA">
                            <ItemStyle Width="60px" HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="nomeAluno" HeaderText="ALUNO">
                            <ItemStyle Width="340px" HorizontalAlign="Left" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="mediaB1" HeaderText="1º BIM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>                     
                        <asp:BoundField DataField="mediaB2" HeaderText="2º BIM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mediaS1" HeaderText="1º SEM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mediaB3" HeaderText="3º BIM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mediaB4" HeaderText="4º BIM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mediaS2" HeaderText="2º SEM" NullDisplayText="*" 
                            DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mediaAnual" HeaderText="MD ANO" 
                            NullDisplayText="*" DataFormatString="{0:n2}">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
