<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5211_BoletoTituloPorTurma.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 900px;
        }
        .liLinhaBotao
        {
            padding-top: 10px;
        }
        .liDados
        {
            margin-top: 5px;
        }
        .liPesqAtiv
        {
            margin-top: 15px;
        }
        .liGrid
        {
            clear: both;
            margin-top: 10px;
        }
        
        .centro
        {
            text-align: center;
        }
        .direita
        {
            text-align: right;
        }
        .divGrid
        {
            height: 320px;
            overflow-y: auto;
            width: 895px;
            padding:15px;
        }
        .liTipo
        {
            float: left;
            margin-top: 30px;
            margin-left: -55px;
        }
        .liTodos
        {
            clear: both;
            margin-top: 30px;
            margin-left: 60px;
        }
        .divTipo
        {
            position: relative;
            float: left;
            margin-left: 570px;
        }
        .divTodos
        {
            position: relative;
            float: left;
        }
        .chkSelecionarTodos label
        {
            display: inline;
            margin-left: -2px;
            margin-top: 6px;
        }
        .divCamposSuperior
        {
            position: relative;
            left: 15%;
            width: 900px;
            top: 0px;
        }
        .liMesmaLinha
        {
            float: left !important;
            position: relative;
            margin-right: 15px;
            min-width: 50px;
        }
        .liNovaLinha
        {
            clear: both;
            position: relative;
            margin-right: 15px;
            min-width: 50px;
        }
        .divCampos
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divCampos">
                <div class="divCamposSuperior" style="margin-left:-37px">
                    <div class="liMesmaLinha">
                        <label id="lblPeriodo" class="lblObrigatorio" runat="server" title="Selecione o período de vencimento">
                            Período</label>
                        <asp:TextBox ID="txtPerIniVenc" Style="clear: both;" ToolTip="Informe a data de início do período de vencimento"
                            runat="server" CssClass="txtPerIniVenc campoData"></asp:TextBox>
                        <span> &nbsp à &nbsp </span>
                        <asp:TextBox ID="txtPerFimVenc" ToolTip="Informe a data de término do período de vencimento"
                            runat="server" CssClass="txtPerFimVenc campoData"></asp:TextBox>
                    </div>
                    <div class="liMesmaLinha">
                        <label id="lblUnidade" class="lblObrigatorio" runat="server" title="Selecione a Unidade/Escola">
                            Unidade/Escola</label>
                        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True"
                            ToolTip="Selecione a Unidade/Escola" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                            ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="liMesmaLinha">
                        <label for="ddlModalidade" title="Modalidade">
                            Modalidade</label>
                        <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" ToolTip="Selecione a Modalidade"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                            ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="liMesmaLinha">
                        <label for="ddlSerieCurso" title="Série/Curso">
                            Série/Curso</label>
                        <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" style="width:180px" ToolTip="Selecione a Série/Curso"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                            ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="liNovaLinha">
                    </div>
                    <div class="liMesmaLinha" style="margin-left:187px;">
                        <label id="lblTurma" for="ddlTurma" title="Turma">
                            Turma</label>
                        <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                            ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                    <div class="liMesmaLinha">
                        <label id="Label3" for="ddlAluno" title="Aluno(a)">
                            Aluno(a)</label>
                        <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa" runat="server" ToolTip="Selecione o(a) Aluno(a)">
                        </asp:DropDownList>
                    </div>
                    <div class="liMesmaLinha liLinhaBotao">
                        <asp:LinkButton ID="btnPesqGride" Style="margin-left: 5px; margin-top: 30px; position: relative;"
                            runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <img title="Clique para gerar Gride de Títulos dos Alunos."
                        alt="Icone de Pesquisa das Grides." 
                        src="/Library/IMG/Gestor_BtnPesquisa.png" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="liNovaLinha">
            </div>
            <div class="liGrid">
                <div id="divGrid" runat="server" class="divGrid">
                    <div class="divTodos">
                        <asp:CheckBox ID="chkSelecionarTodos" runat="server" AutoPostBack="True" CssClass="chkSelecionarTodos"
                            OnCheckedChanged="chkSelecionarTodos_CheckedChanged" Text="Marcar todos" />
                    </div>
                    <div class="divTipo">
                        <asp:Label ID="lbTipoEmi" runat="server" Text="Tipo de emissão do boleto: "></asp:Label>
                        <asp:RadioButton ID="rbSegunda" runat="server" CssClass="chkSelecionarTodos" GroupName="TipoBoleto"
                            Text="2º Via" AutoPostBack="True" />
                        <asp:RadioButton ID="rbNovo" runat="server" CssClass="chkSelecionarTodos" GroupName="TipoBoleto"
                            Text="Novo" AutoPostBack="True" />
                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <asp:GridView ID="grdFonte" CssClass="grdBusca" runat="server" GridLines="Vertical"
                        AutoGenerateColumns="False" Width="100%">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NU_NIRE" HeaderText="NIRE" />
                            <asp:BoundField DataField="NO_ALU" HeaderText="ALUNO" />
                            <asp:BoundField DataField="CO_ANO_MES_MAT" HeaderText="ANO" />
                            <asp:BoundField DataField="CO_SIGL_CUR" HeaderText="SÉRIE" />
                            <asp:BoundField DataField="CO_SIGLA_TURMA" HeaderText="TURMA" />
                            <asp:BoundField DataField="NU_DOC" HeaderText="DOCUMENTO" />
                            <asp:BoundField DataField="NU_PAR" HeaderText="PA" />
                            <asp:BoundField DataField="DT_VEN_DOC" DataFormatString="{0:dd/MM/yy}" HeaderText="VENCTO">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VR_PAR_DOC" DataFormatString="{0:n2}" HeaderText="VALOR (R$)">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sigla" HeaderText="UNID CONT">
                                <ItemStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="boleto" HeaderText="BOLETO">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("input.campoData").datepicker();
            $("input.campoData").mask("99/99/9999");
        });
    </script>
</asp:Content>
