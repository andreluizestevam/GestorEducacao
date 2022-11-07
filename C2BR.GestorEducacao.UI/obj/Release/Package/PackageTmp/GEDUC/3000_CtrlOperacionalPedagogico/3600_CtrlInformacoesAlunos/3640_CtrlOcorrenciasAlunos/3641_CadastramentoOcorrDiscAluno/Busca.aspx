<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3640_CtrlOcorrenciasAlunos.F3641_CadastramentoOcorrDiscAluno.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidade
        {
            width: 210px;
        }
        .ddlColaborador
        {
            width: 210px;
        }
        .ddlTipoOcorrencia
        {
            width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Categoria</label>
            <asp:DropDownList runat="server" ID="ddlCategoria" OnSelectedIndexChanged="ddlCategoria_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label for="ddlAluno" title="Aluno" runat="server" ID="lblFlex">Aluno</asp:Label><br />
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" CssClass="ddlColaborador"
                runat="server">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox ID="txtDtIni" runat="server" CssClass="campoData"></asp:TextBox>
            &nbsp;à&nbsp;
            <asp:TextBox ID="txtDtFim" runat="server" CssClass="campoData"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label for="ddlTipoOcorrencia" title="Classificação da Ocorrência">
                Classificação da Ocorrência</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione a Classificação da Ocorrência"
                CssClass="ddlTipoOcorrencia" runat="server" OnSelectedIndexChanged="ddlTipoOcorrencia_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Tipo Ocorrência</label>
            <asp:DropDownList runat="server" ID="ddlTpOcorrTbxxx" Width="230px">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
