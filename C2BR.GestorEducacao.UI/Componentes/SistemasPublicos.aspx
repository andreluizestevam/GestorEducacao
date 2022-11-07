<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SistemasPublicos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.SistemasPublicos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistemas Públicos</title>
    <style type="text/css">
        #divSistemasPublicosContainer #divRodapeDA
        {
            margin-top: 0px !important;
            float: right;
        }
        #divSistemasPublicosContainer #imgLogoGestorDA
        {
            width: 127px;
            height: 30px;
            margin-top:20px !important;
        }
        #divSistemasPublicosContent { border: 1px solid #EBF0FB; height: 265px; overflow-y: auto; }
        #divSistemasPublicosContent td { padding: 2px; padding-left: 5px !important; }        
        #divSistemasPublicosContent table tr:hover { cursor: pointer; background-color: #DDDDDD; }
        #divSistemasPublicosContent .emptyDataRowStyle td
        {
            background: url("/Library/IMG/Gestor_ImgInformacao.png") no-repeat scroll 10px 20px #FFFFFF;
            padding: 0 10px 10px 45px !important;
            height: 70px;
        }
        #divHelpTxtSP
        {
            float: left;
            margin-top: 10px;
            width: 480px;
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
        .grdSistemasPublicos th
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #grdSistemasPublicos .alternateRowStyle td
        {
            background-color: #EEEEEE;
        }        
    </style>
</head>
<body>
    <div id="divSistemasPublicosContainer">
        <form id="frmSistemasPublicos" runat="server">
        <div id="divSistemasPublicosContent">
            <asp:GridView runat="server" ID="grdSistemasPublicos" AutoGenerateColumns="False" CssClass="grdSistemasPublicos"
                GridLines="Vertical" OnRowDataBound="grdSistemasPublicos_RowDataBound" DataKeyNames="ID_CONEX_WEB">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Sistema Público cadastrado.<br />
                </EmptyDataTemplate>
                <AlternatingRowStyle CssClass="alternateRowStyle" />
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <Columns>                    
                    <asp:BoundField HeaderText="SISTEMA" DataField="NO_TITULO_CONEX_WEB">
                        <ItemStyle Width="178px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DESCRIÇÃO" DataField="DE_OBJETO_CONEX_WEB">       
                        <ItemStyle Width="655px"></ItemStyle>                 
                    </asp:BoundField>      
                    <asp:BoundField HeaderText="ORIGEM" DataField="CO_ORGAO_CONEX_WEB">
                        <ItemStyle></ItemStyle>
                    </asp:BoundField>             
                </Columns>
            </asp:GridView>                      
        </div>
        <div id="divHelpTxtSP">
            <p id="pAcesso" class="pAcesso">
                Faça o LOGIN DE ACESSO a Sistemas Públicos de forma fácil e rápida.<br />
                Para acessar o Sistema, posicione o Mouse e Clique na linha de seu interesse.
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
            if ($('#ulSistemasPublicos li').length < 1) {
                $('#divSistemasPublicosEmptyData').show();
                $("#divRodape").animate({
                    marginTop: "-=28px"
                }, 0);
            }
            else {
                $('#divSistemasPublicosEmptyData').hide();
            }

            $('#divLoading').hide();
        });
    </script>
</body>
</html>
