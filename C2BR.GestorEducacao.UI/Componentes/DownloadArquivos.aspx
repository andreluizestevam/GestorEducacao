<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadArquivos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.DownloadArquivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Últimos Acessos</title>
    <style type="text/css">
        #divDownloadArquivosContainer #divRodapeDA
        {
            margin-top: 0px !important;
            float: right;
        }
        #divDownloadArquivosContainer #imgLogoGestorDA
        {
            width: 127px;
            height: 30px;
            margin-top:20px !important;
        }
        #divDownloadArquivosContent { border: 1px solid #EBF0FB; height: 200px; overflow-y: auto; }
        #divDownloadArquivosContent td { padding: 2px; padding-left: 5px !important; }        
        #divDownloadArquivosContent table tr:hover { cursor: pointer; background-color: #DDDDDD; }
        #divDownloadArquivosContent .emptyDataRowStyle td
        {
            background: url("/Library/IMG/Gestor_ImgInformacao.png") no-repeat scroll 80px 20px #FFFFFF;
            padding: 0 10px 10px 45px !important;
            height: 70px;
            width: 320px;
            text-align: center;
        }
        #divHelpTxtDA
        {
            float: left;
            margin-top: 10px;
            width: 240px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFechar
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 2px;
        }
        .grdDownloadArquivos th
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #grdDownloadArquivos .alternateRowStyle td
        {
            background-color: #EEEEEE;
        }        
    </style>
</head>
<body>
    <div id="divDownloadArquivosContainer">
        <form id="frmDownloadArquivos" runat="server">
        <div id="divDownloadArquivosContent">
            <asp:GridView runat="server" ID="grdDownloadArquivos" AutoGenerateColumns="False" CssClass="grdDownloadArquivos"
                GridLines="Vertical" OnRowDataBound="grdDownloadArquivos_RowDataBound" DataKeyNames="ArquivoCompartilhadoId">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Download de arquivo disponível.<br />
                </EmptyDataTemplate>
                <AlternatingRowStyle CssClass="alternateRowStyle" />
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataFormatString = "{0:dd/MM/yyyy}" HeaderText="PUBLICAÇÃO" DataField="DataPublicacao">
                        <ItemStyle Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="NOME DO ARQUIVO" DataField="NomeArquivo">
                        <ItemStyle Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ORIGEM" DataField="SiglaUnidade">                        
                    </asp:BoundField>
                    <asp:BoundField DataFormatString = "{0:dd/MM/yyyy}" HeaderText="VALIDADE" DataField="DataValidade">
                        <ItemStyle Width="70px"></ItemStyle>
                    </asp:BoundField>                    
                </Columns>
            </asp:GridView>                      
        </div>
        <div id="divHelpTxtDA">
            <p id="pAcesso" class="pAcesso">
                Clique em uma das opções de arquivo acima para iniciar o download.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeDA">
            <img id="imgLogoGestorDA" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>        
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#ulDownloadArquivos li').length < 1) {
                $('#divDownloadArquivosEmptyData').show();
                $("#divRodape").animate({
                    marginTop: "-=28px"
                }, 0);
            }
            else {
                $('#divDownloadArquivosEmptyData').hide();
            }

            $('#divLoading').hide();
        });
    </script>
</body>
</html>
