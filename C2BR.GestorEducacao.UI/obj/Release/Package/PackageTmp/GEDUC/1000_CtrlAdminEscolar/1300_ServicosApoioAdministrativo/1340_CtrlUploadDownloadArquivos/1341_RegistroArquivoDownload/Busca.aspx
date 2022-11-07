<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1340_CtrlUploadDownloadArquivos.F1341_RegistroArquivoDownload.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .campoNomeArquivo { width: 150px; }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li class="liNomeArquivo">
            <label for="txtNomeArquivo" title="Nome do Arquivo">
                Nome do Arquivo</label>
            <asp:TextBox ID="txtNomeArquivo" class="campoNomeArquivo" runat="server" MaxLength="100"
                ToolTip="Insira o nome ou parte do nome do arquivo para efetuar a pesquisa."></asp:TextBox>
        </li>
        <li class="liDataValidade">
            <label for="txtDataValidade" title="Data de Validade do Arquivo">
                Data Validade</label>
            <asp:TextBox ID="txtDataValidade" runat="server" MaxLength="10" CssClass="txtData campoData"
                ToolTip="Informe a data máxima de validade da publicação do arquivo que será pesquisado."></asp:TextBox>
        </li>
        <li class="liAtivo">
            <label for="chkAtivo">
                Ativo</label>
            <asp:CheckBox runat="server" ID="chkAtivo" Checked="true" ToolTip="Deixe desmarcado para visualizar também os arquivos que estão desativados." />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>