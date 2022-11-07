<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3503_LancManutSerieTurmaMediaBim.Cadastro" %>

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
            margin-left: 418px;
            margin-bottom: 3px;
        }
        .liCabecalho2
        {
            margin-left: 25px;
            margin-bottom: 3px;
        }
        .liCabecalho3
        {
            margin-left: 30px;
            margin-bottom: 3px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 380px;
            width: 500px;
            border:1px solid #CCCCCC;
            overflow-y: scroll;
        }
        .mascaradecimal
        {
            width: 50px;
            text-align: right;
        }
        .txtFaltas
        {
            width: 50px;
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
        <li style="margin-left:36px">
            <label for="ddlAno">
                Ano</label>
            <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" Enabled="false">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlReferencia">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" Enabled="false" runat="server">
                <asp:ListItem Value="1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="4">4º Bimestre</asp:ListItem>
                <asp:ListItem Value="5">1º Trimestre</asp:ListItem>
                <asp:ListItem Value="6">2º Trimestre</asp:ListItem>
                <asp:ListItem Value="7">3º Trimestre</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlModalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" CssClass="campoModalidade" runat="server" Enabled="false"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" runat="server" AutoPostBack="true"
                Enabled="false">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" CssClass="campoTurma" runat="server" AutoPostBack="true"
                Enabled="false">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlMateria">
                Disciplina</label>
            <asp:DropDownList ID="ddlMateria" CssClass="campoMateria" Enabled="false" runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Tipo de lançamento</label>
            <asp:DropDownList ID="ddlTpLanc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTpLanc_OnSelectedIndexChanged"
                ToolTip="Tipo de lançamento ('N' - Nota, 'C' - Conceito)">
                <asp:ListItem Value="N" Text="Nota"></asp:ListItem>
                <asp:ListItem Value="C" Text="Conceito"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear" style="margin: 14px 0 0 230px">
            <div class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="grdBusca_RowDataBound" EnableViewState Width="100%">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NU_NIRE" HeaderText="NIRE"></asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Média">
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidFlHomol" Value='<%# bind("FL_HOMOL") %>' />
                                <asp:TextBox ID="txtMedia" CssClass="mascaradecimal" Enabled='<%# bind("HOMOL") %>'
                                    Visible="false" runat="server" Text='<%# bind("MEDIA") %>' ToolTip="Média do Aluno(a)"></asp:TextBox>
                                <asp:HiddenField ID="hidConceito" runat="server" Value='<%# bind("CONCEITO") %>' />
                                <asp:HiddenField ID="hidNotaMaxi" runat="server" Value='<%# bind("VL_MAX") %>' />
                                <asp:HiddenField ID="hidCoAlu" runat="server" Value='<%# bind("CO_ALU") %>' />
                                <asp:DropDownList ID="ddlConceito" runat="server" Visible="false" ToolTip="Conceito atribuído ao Aluno(a)">
                                    <asp:ListItem Value="">Selecione</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FLT">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFaltas" CssClass="txtFaltas" Enabled='<%# bind("HOMOL") %>' runat="server"
                                    Text='<%# bind("FALTAS") %>' ToolTip="Quantidade de faltas"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtConceito").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtFaltas").mask("?999");
        });   
    </script>
</asp:Content>
