<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarCEPsEndereco.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ListarCEPsEndereco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Responsáveis</title>
    <style type="text/css">
        #divListarResponsaveisContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarCEPsEnderecoContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarCEPsEnderecoContent .alternateRowStyleLR td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarCEPsEnderecoContent .rowStyleLR td
        {
        	padding-left: 5px;
        	padding-right: 5px;
        }
        #divListarCEPsEnderecoContainer #divRodapeLR
        {
            margin-top: 10px;
            float: right;
        }
        #divListarCEPsEnderecoContainer #imgLogoGestorLR
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtLR
        {
            float: left;
            margin-top: 10px;
            width: 380px;
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
        #divListarCEPsEnderecoContent
        {
        	height: 261px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divListarCEPsEnderecoContainer">
        <form id="frmListarCEPsEndereco" runat="server">
        <div id="divListarCEPsEnderecoContent">
            <asp:GridView runat="server" ID="grdListarCEPsEndereco" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdListarCEPsEndereco_RowDataBound" DataKeyNames="CO_CEP" >
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Endereço Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />                
                <AlternatingRowStyle CssClass="alternateRowStyleLR" Height="15" />
                <RowStyle CssClass="rowStyleLR" Height="15" />
                <Columns>
                    <asp:BoundField DataField="CO_CEP" HeaderText="CEP">
                        <ItemStyle VerticalAlign="Middle" Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Tipo" DataField="DE_TIPO_LOGRA">
                        <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="Endereço" DataField="NO_ENDER_CEP">
                        <ItemStyle Width="350px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>                              
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtLR">           
            <p id="pAcesso" class="pAcesso">
                Verifique os endereços existentes no quadro acima.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeLR">
            <img id="imgLogoGestorLR" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
</body>
</html>
