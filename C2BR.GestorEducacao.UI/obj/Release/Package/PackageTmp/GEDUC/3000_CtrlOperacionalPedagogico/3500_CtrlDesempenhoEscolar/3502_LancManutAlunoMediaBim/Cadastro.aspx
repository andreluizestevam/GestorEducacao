<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3502_LancManutAlunoMediaBim.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 950px;
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
        .liCabecalho1
        {
            margin-left: 350px;
            margin-bottom: 3px;
        }
        .liCabecalho2
        {
            margin-left: 40px;
            margin-bottom: 3px;
        }
        .liCabecalho3
        {
            margin-left: 40px;
            margin-bottom: 3px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            border: 1px solid #CCCCCC;
            height: 320px;
            width: 506px;
            overflow-y: scroll;
        }
        .txtNota
        {
            width: 40px;
            text-align: right;
        }
        .txtFaltas
        {
            width: 35px;
            text-align: right;
        }
        .txtConceito
        {
            width: 50px;
            text-align: right;
        }
        .lblCabecalho
        {
            font-weight: bold;
            font-size: 1.1em;
            text-transform: uppercase;
        }
        .ddlCrit, .ddlResp, .ddlApre
        {
            width: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 20px">
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" Enabled="false" ToolTip="Ano">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlReferencia" title="Referência">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" Enabled="false" runat="server"
                ToolTip="Bimestre de Referência" />
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" Enabled="false"
                AutoPostBack="true" ToolTip="Modalidade">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                Enabled="false" ToolTip="Série/Curso">
            </asp:DropDownList>
        </li>
        <li title="Turma">
            <label for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                Enabled="false" ToolTip="Turma">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAluno" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa" Enabled="false" runat="server"
                ToolTip="Aluno">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTpLanc" title="Tipo de lançamento ('N' - Nota, 'C' - Conceito)">
                Tipo de lan&ccedil;amento</label>
            <asp:DropDownList ID="ddlTpLanc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTpLanc_OnSelectedIndexChanged"
                ToolTip="Tipo de lançamento ('N' - Nota, 'C' - Conceito)">
                <asp:ListItem Value="N" Text="Nota"></asp:ListItem>
                <asp:ListItem Value="C" Text="Conceito"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin-left: 30px; float: left; margin-top: 20px;">
            <div class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdBusca_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NO_SIGLA_MATERIA" HeaderText="Sigla">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_MATERIA" HeaderText="Disciplina">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Média">
                            <ItemStyle HorizontalAlign="Center" Width="110px" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidFlHomol" Value='<%# bind("FL_HOMOL") %>' />
                                <asp:TextBox ID="txtMedia" CssClass="txtNota" Enabled='<%# bind("HOMOL") %>' runat="server"
                                    Visible="false" Text='<%# bind("MEDIA") %>' ToolTip="Nota do aluno na matéria em contexto"></asp:TextBox>
                                <asp:HiddenField ID="hidConceito" runat="server" Value='<%# bind("CONCEITO") %>' />
                                <asp:HiddenField ID="hidNotaMaxi" runat="server" Value='<%# bind("VL_MAX") %>' />
                                <asp:HiddenField ID="hidCoMat" runat="server" Value='<%# bind("CO_MAT") %>' />
                                <asp:DropDownList ID="ddlConceito" Enabled="false" runat="server" Visible="false" ToolTip="Conceito de avaliação">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FLT">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFaltas" CssClass="txtFaltas" runat="server" Text='<%# bind("FALTAS") %>' ToolTip="Quantidade de faltas"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li style="float: right; margin-top: 20px">
            <%-- <asp:Label ID="lblAvalGeral" runat="server">
                Avaliação Geral do Aluno
            </asp:Label>--%>
            <label class="liTituloGrid" style="width: 100%; height: 15px !important; margin-right: 0px;
                background-color: #ffff99; text-align: center; font-weight: bold;
                margin-bottom: 5px">
                <label style="font-family: Tahoma; font-weight: bold;">
                    AVALIAÇÃO GERAL DO ALUNO</label>
            </label>
            <asp:TextBox TextMode="multiline" Height="200px" Width="370px" runat="server" ID="txtAvalGeral"></asp:TextBox>
        </li>
    </ul>
    <div id="edicaoResAval">
        <textarea id="textoResAval" rows="20" style="width: 800px"></textarea>
    </div>
    <asp:HiddenField ID="hfValoresAval" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtConceito").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtFaltas").mask("?999");
            $("#edicaoResAval").hide();
        });
        ///Função para o clique no botão de mostrar e alterar avaliação por matéria
        function carregarAvalClicado(codigo, valor) {
            $("#textoResAval").val("");
            $("#edicaoResAval").hide();
            $("#edicaoResAval").dialog({
                title: "Edição resumo avaliativo.",
                modal: true,
                draggable: true,
                resizable: false,
                width: 820,
                open: function () {
                    var valores = $(window).data("valoresAval");
                    if (valores == undefined || valores[codigo] == undefined) {
                        valor = valor.replace("%%", "'");
                        valor = valor.replace("*#*#", "\n\n");

                        $("#textoResAval").val(gravarRetirarValores(valor, codigo));
                    }
                    else
                        $("#textoResAval").val(gravarRetirarValores(codigo));
                },
                close: function () {
                    gravarRetirarValores($("#textoResAval").val(), codigo);
                }
            });
            return false;
        }
        ///Função para colocar ou ler valores do hiddenfield
        function gravarRetirarValores(valor, chave) {
            var valores, retorno;
            if ($(window).data("valoresAval") == undefined)
                valores = {};
            else
                valores = $(window).data("valoresAval");
            if (chave == undefined) {
                retorno = valores[valor]
            }
            else {
                valores[chave] = valor;
                retorno = valores[chave];
            }
            $(window).data("valoresAval", valores);
            $("#hfValoresAval").val(JSON.stringify(valores));
            return retorno;
        }   
    </script>
</asp:Content>
