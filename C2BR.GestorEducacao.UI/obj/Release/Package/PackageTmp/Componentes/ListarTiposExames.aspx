<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarTiposExames.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.Componentes.ListarTiposExames" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lista de Produtos</title>
    <style type="text/css">
        #divListarProdutosContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarProdutosContent .alternateRowStyleLD td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContent .rowStyleLD td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarProdutosContainer #divRodapeLD
        {
            margin-top: 20px;
            float: right;
        }
        #divListarProdutosContainer #imgLogoGestorLD
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
        #divListarProdutosContent
        {
            height: 261px;
            overflow-y: auto;
            border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divListarProdutosContainer">
        <form id="frmListarProdutos" runat="server">
        <div id="divListarProdutosContent">
            <asp:GridView runat="server" ID="grdListarExames" AutoGenerateColumns="false" AllowPaging="false"
                GridLines="Vertical" DataKeyNames="ID_PROC_MEDI_PROCE">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Tipo de Exame Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <AlternatingRowStyle CssClass="alternateRowStyleLD" Height="15" />
                <RowStyle CssClass="rowStyleLD" Height="15" />
                <Columns>
                    <asp:BoundField HeaderText="COD. EXAME" DataField="CO_PROC_MEDI">
                        <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="GRUPO" DataField="NM_PROC_MEDIC_GRUPO">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="SUBGRUPO" DataField="NM_PROC_MEDIC_SGRUP">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="EXAME" DataField="NM_PROC_MEDI">
                        <ItemStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtLD">
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
