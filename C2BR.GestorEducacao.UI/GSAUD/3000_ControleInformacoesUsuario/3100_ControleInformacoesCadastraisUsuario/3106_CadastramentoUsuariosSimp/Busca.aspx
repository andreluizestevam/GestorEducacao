<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3106_CadastramentoUsuariosSimp.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .liPeriodoAte
        {
            clear: none !important;
            display: inline;
            margin-left: 0px;
            margin-top: 13px;
        }
        .liAux
        {
            margin-right: 5px;
            margin-left: 0px !important;
            clear: none !important;
            display: inline;
        }
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS Dados*/
        .labelAux
        {
            margin-top: 16px;
        }
        .chk label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li style=" margin-top:-20px">
            <label for="txtNire" title="Número de Indentificação Social do Usuario">
                Nº CONTROLE</label>
            <asp:TextBox ID="txtNire" ToolTip="Informe um Número de Controle do Usuario" runat="server"
                MaxLength="10" Width="80px"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome do Aluno">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome de Aluno" Width="210px" runat="server" MaxLength="80" />
            <asp:DropDownList ID="ddlNome" runat="server" Width="212px" Visible="false" />
        </li>
        <li style="clear:none; margin-top: 11px; margin-left: 2px;">
            <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
        </li>
        <li>
            <label for="txtCpf" title="CPF">
                CPF</label>
            <asp:TextBox ID="txtCpf" ToolTip="Informe um CPF" runat="server" CssClass="campoCpf"></asp:TextBox>
        </li>
        <li>
            <label for="ddlDeficiencia" title="Deficiência">
                Deficiência</label>
            <asp:DropDownList ID="ddlDeficiencia" ToolTip="Informe uma Deficiência" runat="server"
                CssClass="ddlDeficiencia" Width="150px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="190px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="float: right; margin: -33px 0px 0 0">
            <label>
                Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="100px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Restrição</label>
            <asp:DropDownList runat="server" ID="ddlRestrAtend" Width="180px">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtPeriodoDe" title="Intervalo de Nascimento">
                Intervalo de Nascto</label>
            <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
        </li>
        <li class="liAux">
            <label class="labelAux">
                até</label>
        </li>
        <li class="liPeriodoAte">
            <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>
        <li>
            <label for="ddlAnoOri" title="Deficiência">
                Ano de Origem</label>
            <asp:DropDownList ID="ddlAnoOri" ToolTip="Informe o ano de origem" runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSituacaoAlu" class="lblObrigatorio" title="Situação do Aluno">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoAlu" CssClass="ddlSituacaoAlu" ToolTip="Informe a Situação do Aluno"
                runat="server" Width="98px">
                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                <asp:ListItem Value="A">Em Atendimento</asp:ListItem>
                <asp:ListItem Value="V">Em Análise</asp:ListItem>
                <asp:ListItem Value="E">Alta(Normal)</asp:ListItem>
                <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                PENDÊNCIAS</label>
        </li>
        <li style="clear: both; float: left; width: 200px">
            <ul>
                <li>
                    <asp:CheckBox runat="server" ID="chkPendDocs" Text="DOCUMENTOS" CssClass="chk" />
                </li>
                <li style="clear: both;">
                    <asp:CheckBox runat="server" ID="chkPenPlano" Text="PLANO/CONVÊNIO" CssClass="chk" />
                </li>
            </ul>
        </li>
        <li style="float: right; margin: -42px 50px 0 0">
            <ul>
                <li>
                    <asp:CheckBox runat="server" ID="chkPenFinancCAR" Text="FINANCEIRO CAR" CssClass="chk" />
                </li>
                <li style="clear: both;">
                    <asp:CheckBox runat="server" ID="chkPenFinancGER" Text="FINANCEIRO GER" CssClass="chk" />
                </li>
            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtNire").mask("?999999999");
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>
