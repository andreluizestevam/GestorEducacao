<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UltimosAcessos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.UltimosAcessos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Últimos Acessos</title>
    <style type="text/css">
        #divUltimosAcessosContent
        {
        	height: 220px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
        #divUltimosAcessosContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divUltimosAcessosContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divUltimosAcessosContent .alternateRowStyle td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
        }
        #divUltimosAcessosContent .rowStyle td
        {
        	padding-left: 5px;
        }
        #divUltimosAcessosContainer #divRodape
        {
            margin-top: 10px;
            float: right;
        }
        #divUltimosAcessosContainer #imgLogoGestor
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtUA
        {
            float: left;
            margin-top: 10px;
            width: 205px;
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
    </style>
</head>
<body>
    <div id="divUltimosAcessosContainer">
        <form id="frmUltimosAcessos" runat="server">
        <div id="divUltimosAcessosContent">
            <asp:GridView runat="server" ID="grdUltimosAcessos" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdUltimosAcessos_RowDataBound">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum acesso registrado.<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                <AlternatingRowStyle CssClass="alternateRowStyle" Height="15" />
                <RowStyle CssClass="rowStyle" Height="15" />
                <Columns>                    
                    <asp:BoundField HeaderText="DATA/HORA" DataField="DT_ATIVI_LOG" HeaderStyle-HorizontalAlign="Center">                        
                        <ItemStyle Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="IP ACESSO" DataField="NR_IP_ACESS_ATIVI_LOG" HeaderStyle-HorizontalAlign="Center">                        
                        <ItemStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ATIVIDADE REALIZADA" DataField="nomModulo" HeaderStyle-HorizontalAlign="Center">                        
                        <ItemStyle Width="400px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="O QUE FEZ" DataField="ACAO_LOG" HeaderStyle-HorizontalAlign="Center">                        
                        <ItemStyle Width="65px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Sigla" HeaderText="UNIDADE" HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle Width="45px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtUA">
            <p id="pAcesso" class="pAcesso">
                Resultado dos últimos acessos realizados.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
</body>
</html>
