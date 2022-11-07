<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarDoencas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ListarDoencas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Doenças</title>
    <style type="text/css">
        #divListarDoencaContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarDoencasContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarDoencasContent .alternateRowStyleLD td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarDoencasContent .rowStyleLD td
        {
        	padding-left: 5px;
        	padding-right: 5px;
        }
        #divListarDoencasContainer #divRodapeLD
        {
            margin-top: 20px;
            float: right;
        }
        #divListarDoencasContainer #imgLogoGestorLD
        {
            width: 80px;
            height: 30px;
            padding-right: 5px;
            margin-right: 5px;
        }
        #divHelpTxtLD
        {
            float: left;
            margin-top: 10px;
            width: 174px;
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
        #divListarDoencasContent
        {
        	height: 261px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divListarDoencasContainer">
        <form id="frmListarDoencas" runat="server">
        <div id="divListarDoencasContent">
            <asp:GridView runat="server" ID="grdListarDoencas" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" DataKeyNames="IDE_CID">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Responsável Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />                
                <AlternatingRowStyle CssClass="alternateRowStyleLD" Height="15" />
                <RowStyle CssClass="rowStyleLD" Height="15" />
                <Columns>
                    <asp:BoundField Visible="false" DataField="IDE_CID" HeaderText="Cod." SortExpression="IDE_CID" HeaderStyle-CssClass="noprint"
                        ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                        <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Código CID" DataField="CO_CID">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="Doença" DataField="NO_CID">
                        <ItemStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>                                                    
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtLD">           
            <p id="pAcesso" class="pAcesso">
                Verifique as doenças existentes no quadro acima para iniciar os trabalhos.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeLD">
            <img id="imgLogoGestorLD" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
</body>
</html>
