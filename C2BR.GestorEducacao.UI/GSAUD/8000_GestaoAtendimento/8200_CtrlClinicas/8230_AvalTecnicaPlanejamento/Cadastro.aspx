<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8230_AvalTecnicaPlanejamento.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1120px;
        }
        input
        {
            height: 13px;
        }
        .ulPer label
        {
            text-align: left;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px !important;
        }
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        .imgAdd
        {
            margin-right: 1px;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <ul class="divEncamMedicoGeral">
                <li style="clear: both; margin-left: 15px;">
                    <ul>
                        <li class="liTituloGrid" style="width: 974px; height: 32px !important; margin-right: 0px;
                            background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 6px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        PROCEDIMENTOS SOLICITADOS</label>
                                </li>
                                <li style="margin-left: 23px; float: right">
                                    <ul class="ulPer">
                                        <li style="margin-right: 20px;">
                                            <label>
                                                Profissional de Saúde</label>
                                            <asp:DropDownList runat="server" ID="ddlProfiSaude" Width="230px">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label>
                                                Grupo Procedimento</label>
                                            <asp:DropDownList runat="server" ID="ddlGrupoProc" Width="140px" OnSelectedIndexChanged="ddlGrupoProc_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <label>
                                                Subgrupo Procedimento</label>
                                            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="140px">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin: 13px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqProcedimentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqProcedimentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <div style="width: 972px; height: 170px; border: 1px solid #CCC; overflow-y: scroll">
                                <asp:HiddenField runat="server" ID="hidIdItem" />
                                <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação em Aberto<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidItemSolic" Value='<%# Eval("ID_ITEM_SOLIC") %>' runat="server" />
                                                <asp:CheckBox ID="chkselectSolic" runat="server" OnCheckedChanged="chkselectSolic_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PACIENTE" HeaderText="PACIENTE">
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="NU_REGIS" HeaderText="Nº CONTROLE">
                                            <ItemStyle HorizontalAlign="Left" Width="140px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NU_GUIA" HeaderText="Nº GUIA">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                                            <ItemStyle HorizontalAlign="Left" Width="340px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="QTDE" HeaderText="QSA">
                                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OBSERVAÇÃO">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtObservacao" Style="margin: 2px 0px 2px 0px !important;
                                                    width: 100px; height: 26px;" TextMode="MultiLine" Enabled="false" Text='<%# Eval("DE_OBSER") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
                <li style="margin: 10px 0 0 15px;">
                    <ul>
                        <li style="float: right; width: 360px; display:none">
                            <ul>
                                <li class="liTituloGrid" style="width: 357px; height: 20px !important; margin-right: 0px;
                                    background-color: #EEEEE0; text-align: center; font-weight: bold;">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                HISTÓRICO DE PLANEJAMENTOS</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both">
                                    <div style="width: 360px; height: 140px; border: 1px solid #CCC; overflow-y: scroll">
                                        <asp:GridView ID="grdHistorico" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum Questionário associado<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="NO_TITULO" HeaderText="TÍTULO">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="IMPRIMIR">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgImp" ImageUrl="/Library/IMG/Gestor_IcoImprimir.png"
                                                            ToolTip="Imprimir Questionário" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidIdAvaliacao" Value='<%# Eval("ID_AVALIACAO") %>' runat="server" />
                                                        <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <li style="float: left; margin-left: 100px">
                            <ul>
                                <li class="liTituloGrid" style="width: 362px; height: 20px !important; margin-right: 0px;
                                    background-color: #f08080; text-align: center; font-weight: bold; margin-bottom: 3px;">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                PLANEJAMENTO DE ATIVIDADES</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both; margin-left: 80px">
                                    <label>
                                        Dt Início</label>
                                    <asp:TextBox runat="server" ID="txtDtInicio" CssClass="campoData"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Dt Prev Term</label>
                                    <asp:TextBox runat="server" ID="txtDtPrevTerm" CssClass="campoData"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Sessões</label>
                                    <asp:TextBox runat="server" ID="txtQtSessoes" Width="35px" CssClass="numero"></asp:TextBox>
                                </li>
                                <li style="clear: both">
                                    <label>
                                        Ação</label>
                                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtAcao" Width="300px" Height="100px"
                                        Style="width: 360px; height: 140px;"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li style="float: right; width: 360px">
                            <ul>
                                <li class="liTituloGrid" style="width: 357px; height: 20px !important; margin-right: 0px;
                                    background-color: #EEEEE0; text-align: center; font-weight: bold;">
                                    <ul>
                                        <li style="margin: 0 0 0 10px; float: left">
                                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                                QUESTIONÁRIOS AVALIATIVOS</label>
                                        </li>
                                    </ul>
                                </li>
                                <li style="clear: both">
                                    <label>
                                        Questionários</label>
                                    <asp:DropDownList runat="server" ID="ddlAvaliacoes" Width="360px">
                                    </asp:DropDownList>
                                </li>
                                <li id="li1" runat="server" title="Clique para Adicionar as Questões da Avaliação"
                                    class="liBtnAdd" style="clear: both; float: right; margin: 3px 0 3px 0">
                                    <img alt="" class="imgAdd" title="Adicionar Formulário" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                        height="15px" width="15px" />
                                    <asp:LinkButton ID="btnAddForm" runat="server" class="btnLabel" OnClick="btnAddForm_Click">Adicionar Formulário</asp:LinkButton>
                                </li>
                                <li style="clear: both">
                                    <div style="width: 360px; height: 140px; border: 1px solid #CCC; overflow-y: scroll">
                                        <asp:GridView ID="grdQuestionario" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum Questionário associado<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="NO_TITULO" HeaderText="TÍTULO">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="IMPRIMIR">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgImp" ImageUrl="/Library/IMG/Gestor_IcoImprimir.png"
                                                            ToolTip="Imprimir Questionário" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidIdAvaliacao" Value='<%# Eval("ID_AVALIACAO") %>' runat="server" />
                                                        <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
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
            </ul>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numero").mask("?99999999");
        });
    </script>
</asp:Content>
