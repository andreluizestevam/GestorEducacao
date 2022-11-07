<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3108_ExclusaoPacientes.Busca" %>

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
        <li>
            <label for="txtNire" title="Número de Indentificação Social do Usuario">
                Nº CONTROLE</label>
            <asp:TextBox ID="txtNire" ToolTip="Informe um Número de Controle do Usuario" runat="server"
                MaxLength="10" Width="80px"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome do Aluno">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome de Aluno" runat="server" MaxLength="80"
                CssClass="campoNomePessoa"></asp:TextBox>
        </li>
        <li>
            <label for="txtCpf" title="CPF">
                CPF</label>
            <asp:TextBox ID="txtCpf" ToolTip="Informe um CPF" runat="server" CssClass="campoCpf"></asp:TextBox>
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
 <%--       <li>
            <label>
                Restrição de Atendimento</label>
            <asp:DropDownList runat="server" ID="ddlRestrAtend" Width="180px">
            </asp:DropDownList>
        </li>--%>
        <li>
            <label for="ddlSituacaoAlu" class="" title="Situação do Aluno">
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
      <%--  <li>
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
        </li>--%>
  <%--      <li style="float: right; margin: -42px 50px 0 0">
            <ul>
                <li>
                    <asp:CheckBox runat="server" ID="chkPenFinancCAR" Text="FINANEIRO CAR" CssClass="chk" />
                </li>
                <li style="clear: both;">
                    <asp:CheckBox runat="server" ID="chkPenFinancGER" Text="FINANCEIRO GER" CssClass="chk" />
                </li>
            </ul>
        </li>--%>
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
