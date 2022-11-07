<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ExtratoOcorrDisc.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3649_Relatorios.ExtratoOcorrDisc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
        /*--> CSS DADOS */
        .ulDados
        {
            width:400px;
            margin:20px 0 0 350px;
        }
        .ulDados li
        {
            margin:5px 0 0 5px;
        }
        input
        {
            height:13px;
        }
        label
        {
            margin-bottom:1px;
        }
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
 <ul class="ulDados">
        <li>
            <label for="ddlUnidade" title="Unidade/Escola">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" AutoPostBack="true"
                CssClass="ddlUnidade" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li style="clear:both">
            <label>
                Categoria</label>
            <asp:DropDownList runat="server" ID="ddlCategoria" OnSelectedIndexChanged="ddlCategoria_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label for="ddlAluno" title="Aluno" runat="server" ID="lblFlex">Aluno</asp:Label><br />
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o item desejado" CssClass="ddlColaborador"
                runat="server">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label for="ddlTipoOcorrencia" title="Classificação da Ocorrência">
                Classif da Ocorr</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione a Classificação da Ocorrência"
                CssClass="ddlTipoOcorrencia" runat="server" OnSelectedIndexChanged="ddlTipoOcorrencia_OnSelectedIndexChanged"
                AutoPostBack="true" Width="100px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Tipo Ocorrência</label>
            <asp:DropDownList runat="server" ID="ddlTpOcorrTbxxx" Width="230px">
            </asp:DropDownList>
        </li>
                <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox ID="txtDtIni" runat="server" CssClass="campoData"></asp:TextBox>
            &nbsp;à&nbsp;
            <asp:TextBox ID="txtDtFim" runat="server" CssClass="campoData"></asp:TextBox>
        </li>
        <li style="margin-left:95px;">
            <label>Data Emissão</label>
            <asp:TextBox runat="server" ID="txtDtEmissao" CssClass="campoData"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
