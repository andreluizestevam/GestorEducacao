<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informativos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.Informativos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informativos</title>
    <link href="/Library/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/jScrollPane.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/Jquery.UI/customtheme/default.css" rel="stylesheet" type="text/css" />
    <script src="/Library/JS/jquery.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.ui.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.form.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.corner.js" type="text/javascript"></script>
    <script src="/Library/JS/ui.datepicker-pt-BR.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.jScrollPane.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.defaults.js" type="text/javascript"></script>
    <style type="text/css">
        #divInformativosContainer
        {
            width: 690px;
            height: 170px !important;
        }
        #divInformativosContainer #divInformativosContent
        {
            height: 250px;
            border: 1px solid #E5E6E9;
        }
        #divInformativosContainer #divRodapeInf
        {
            margin-top: 15px;
            float: right;
        }
        #divInformativosContainer #imgLogoGestorInf
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtInf
        {
            float: left;
            margin-top: 10px;
            width: 350px;
            color: #DF6B0D;
            font-weight: bold;
        }
        .pAcessoInf
        {
            font-size: 1.1em;
            color: #4169E1;
        }
        .pFecharInf
        {
            font-size: 0.9em;
            color: #FF6347;
            margin-top: 2px;
        }
        
        .grdInformativos th
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        .grdInformativos td { padding: 3px 5px; }
        .emptyDataRowStyle td
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 10px center;
            padding: 10px 270px 10px 320px !important;
        }
        table tr:hover { cursor: pointer; background-color: #DDDDDD; }
        .activeLine { cursor: pointer; }
        .inactiveLine { background-color: #F4F4F4 !important; }
        #grdInformativos .alternateRowStyle td { background-color: #EEEEEE; } 
    </style>
</head>
<body>
    <div id="divInformativosContainer">
    <form id="frmInformativos" runat="server">    
        <div id="divInformativosContent">
            <asp:GridView runat="server" ID="grdInformativos" AutoGenerateColumns="False" CssClass="grdInformativos"
                GridLines="Vertical" OnRowDataBound="grdInformativos_RowDataBound" OnRowCommand="grdInformativos_RowCommand"
                DataKeyNames="ID_INFOR">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    SEM REGISTROS.<br />
                </EmptyDataTemplate>
                <AlternatingRowStyle CssClass="alternateRowStyle" />
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Data" DataFormatString='{0:dd/MM/yy}' DataField="DT_CADAS_INFOR" />
                    <asp:BoundField HeaderText="Informativo" DataField="DE_TITUL_PUBLIC" ItemStyle-Width="300px" />
                    <asp:BoundField HeaderText="Ver +" DataField="NO_TITUL_URL" ItemStyle-Width="300px" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtInf">
            <p id="pAcesso" class="pAcessoInf">
                Clique em um dos informativos acima para obter mais informações.</p>
            <p id="pFechar" class="pFecharInf">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeInf">
            <img id="imgLogoGestorInf" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>    
    </form>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divInformativosContainer #frmInformativos').ajaxForm({ target: '[id=divLoadShowInformativos]', url: '/Componentes/Informativos.aspx' });
        });
    </script>
</body>
</html>
