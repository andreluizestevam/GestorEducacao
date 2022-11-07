<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcessoFacil.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.AcessoFacil" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acesso Fácil</title>
    <style type="text/css">
        #divAcessoFacilContainer #divRodapeAF
        {
            margin-top: -5px !important;
            float: right;
        }
        #divAcessoFacilContainer #imgLogoGestorDA
        {
            width: 127px;
            height: 30px;
            margin-top:20px !important;
        }
        #divAcessoFacilContent { border: 1px solid #EBF0FB; height: 280px; overflow-y: auto; }
        #divAcessoFacilContent td { padding: 2px; padding-left: 5px !important; }        
        #divAcessoFacilContent table tr:hover { cursor: pointer; background-color: #DDDDDD; }
        #divAcessoFacilContent .emptyDataRowStyle td
        {
            background: url("/Library/IMG/Gestor_ImgInformacao.png") no-repeat scroll 10px 20px #FFFFFF;
            padding: 0 10px 10px 45px !important;
            height: 70px;
        }
        #divHelpTxtAF
        {
            float: left;
            margin-top: 5px;
            width: 405px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFecharAF
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 7px;
        }
        .grdAcessoFacil th
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        #grdAcessoFacil .alternateRowStyle td
        {
            background-color: #EEEEEE;
        }        
    </style>
</head>
<body>
    <div id="divAcessoFacilContainer">
        <form id="frmAcessoFacil" runat="server">
        <div id="divAcessoFacilContent">
            <asp:GridView runat="server" ID="grdAcessoFacil" AutoGenerateColumns="False" CssClass="grdAcessoFacil"
                GridLines="Vertical" OnRowDataBound="grdAcessoFacil_RowDataBound"
                DataKeyNames="ID_CONEX_WEB">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Acesso Fácil cadastrado.<br />
                </EmptyDataTemplate>
                <AlternatingRowStyle CssClass="alternateRowStyle" />
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <Columns>                    
                    <asp:BoundField HeaderText="SERVIÇO" DataField="NO_TITULO_CONEX_WEB">
                        <ItemStyle Width="278px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="DESCRIÇÃO" DataField="DE_OBJETO_CONEX_WEB">       
                        <ItemStyle Width="530px"></ItemStyle>                 
                    </asp:BoundField>       
                    <asp:BoundField HeaderText="ORGÃO" DataField="CO_ORGAO_CONEX_WEB">
                        <ItemStyle></ItemStyle>
                    </asp:BoundField>            
                </Columns>
            </asp:GridView>                      
        </div>
        <div id="divHelpTxtAF">
            <p id="pAcesso" class="pAcesso">
                Acesse de forma rápida, simples e fácil tudo sobre Educação. <br /> Escolha na GRID acima o assunto que lhe interessa e click na linha correspondente.</p>
            <p id="pFechar" class="pFecharAF">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeAF">
            <img id="imgLogoGestorDA" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>        
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#ulAcessoFacil li').length < 1) {
                $('#divAcessoFacilEmptyData').show();
                $("#divRodape").animate({
                    marginTop: "-=28px"
                }, 0);
            }
            else {
                $('#divAcessoFacilEmptyData').hide();
            }

            $('#divLoading').hide();
        });
    </script>
</body>
</html>
