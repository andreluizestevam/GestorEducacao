<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DeclaracaoAdvertencia.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2112_ContrDeDeclaracoes.DeclaracaoAdvertencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 260px;
        }
        .liUnidade, .liAno
        {
            margin-top: 5px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liAluno
        {
            margin-top: 5px;
        }
        .liMotiv
        {
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:TextBox runat="server" ID="hidIdOcorr" Visible="false"></asp:TextBox>
                <asp:TextBox runat="server" ID="hidDeOcorr" Visible="false"></asp:TextBox>
                <asp:TextBox runat="server" ID="hidAcaoOcorr" Visible="false"></asp:TextBox>
                <asp:TextBox runat="server" ID="hidRespOcorr" Visible="false"></asp:TextBox>
                <asp:TextBox runat="server" ID="hidDataOcorr" Visible="false"></asp:TextBox>
                <li class="liUnidade">
                    <label title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:TextBox ID="txtUnidadeEscola" Enabled="false" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:TextBox>
                </li>
                <li class="liAno">
                    <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAno" Text="*" ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlTurma" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liAluno">
                    <label title="Aluno" class="lblObrigatorio">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa"
                        runat="server" OnSelectedIndexChanged="ddlAlunos_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvAlunos" ControlToValidate="ddlAlunos"
                        Text="*"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 728px; margin: 10px 0 0 -254px !important; clear: both; display: none;"
                    id="liAdvertencias">
                    <ul>
                        <li style="width: 100%; margin-left: -4px;">
                            <div style="width: 100%; text-align: center; height: 17px; background-color: #A4D3eE;
                                margin: 0 0 3px 5px;">
                                <div style="float: none;">
                                    <asp:Label ID="Label3" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                                        vertical-align: middle; margin-left: 4px !important;">
                                    ADVERTÊNCIAS DO(A) ALUNO(A)</asp:Label>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div id="div3" runat="server" class="divGridData" style="height: 200px; width: 728px;
                                border: 1px solid #ccc;">
                                <asp:GridView ID="grdAdvertencias" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    height: 20px;" AutoGenerateColumns="false" ToolTip="Grade de Advertências do aluno selecionado">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma Advertência encontrada.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoAtend" Value='<%# Eval("IDE_OCORR_ALUNO") %>' runat="server" />
                                                <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckSelect_OnCheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DT_OCORR_V" HeaderText="DATA">
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DE_OCORR" HeaderText="DESCRIÇÃO">
                                            <ItemStyle Width="240px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DE_ACAO_TOMAD" HeaderText="AÇÃO TOMADA">
                                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_COL" HeaderText="RESPONSÁVEL">
                                            <ItemStyle Width="190px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

        });

        function MostraAdvertencias() {
            $("#liAdvertencias").fadeIn();
        }
        function MostraAdvertenciasSW() {
            $("#liAdvertencias").show();
        }
    </script>
</asp:Content>
