<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="ReciboPagamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.Relatorios.ReciboPagamento"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin-left:355px;
        }
        .ulDados li
        {
            margin-left:5px;
            margin-top:5px;
        }
        .liUnidade, .liAno
        {
            <%--margin-top: 5px;--%>
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            <%--margin-top: 5px;--%>
        }
        .liSerie
        {
            clear: both;
            <%--margin-top: 5px;--%>
        }
        .liAluno, .liValor, .liDescValor, .liMotivo
        {
            <%--margin-top: 5px;--%>
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <ul id="ulDados" class="ulDados">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <li class="liAno">
                    <label class="lblObrigatorio" for="txtUnidade" title="Ano">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAno" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAno" Text="*" ErrorMessage="Campo Ano é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="clear:both">
                    <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" Width="180px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                        Width="140px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAluno">
                    <label title="Aluno" class="lblObrigatorio">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liValor">
                    <label title="Valor" >
                        Valor R$</label>
                    <asp:TextBox ID="txtValor" CssClass="maskDin" runat="server" Width="50px" Style="text-align: right"
                        OnTextChanged="txtValor_OnTextChanged" AutoPostBack="true" onblur="__doPostBack('','');"></asp:TextBox>
                </li>
                <li style="margin-top: 5px !Important">
                    <label title="DescValor" class="lblObrigatorio">
                        Descrição do Valor:
                    </label>
                    <asp:TextBox ID="txtDescValor" Width="310px" CssClass="txtDescValor" runat="server"
                        Enabled="false"></asp:TextBox>
                </li>
                <li class="liMotivo" style="margin-top: -5px !Important">
                    <label title="Valor" >
                        Referente à:</label>
                    <asp:TextBox ID="txtMotivo" Width="300px" CssClass="txtMotivo" runat="server" TextMode="MultiLine"></asp:TextBox>
                </li>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoNumerico").mask("?999");
            $(".campoMoeda").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".txtNumeroImpressaoSerCur").mask("?99");
            $(".txtCargaHorariaSerCur").mask("?9999");
            $(".maskDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
