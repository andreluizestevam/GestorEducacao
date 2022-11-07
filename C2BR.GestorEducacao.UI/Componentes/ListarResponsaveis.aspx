<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarResponsaveis.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ListarResponsaveis" %>

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
        #divListarResponsaveisContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarResponsaveisContent .alternateRowStyleLR td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarResponsaveisContent .rowStyleLR td
        {
        	padding-left: 5px;
        	padding-right: 5px;
        }
        #divListarResponsaveisContainer #divRodapeLR
        {
            margin-top: 10px;
            float: right;
        }
        #divListarResponsaveisContainer #imgLogoGestorLR
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtLR
        {
            float: left;
            margin-top: 10px;
            width: 450px;
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
        #divListarResponsaveisContent
        {
        	height: 261px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divListarResponsaveisContainer">
        <form id="frmListarResponsaveis" runat="server">
        <div id="divListarResponsaveisContent">
            <asp:GridView runat="server" ID="grdListarResponsaveis" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdListarResponsaveis_RowDataBound" DataKeyNames="CO_RESP" >
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Responsável Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />                
                <AlternatingRowStyle CssClass="alternateRowStyleLR" Height="15" />
                <RowStyle CssClass="rowStyleLR" Height="15" />
                <Columns>
                    <asp:BoundField Visible="false" DataField="CO_RESP" HeaderText="Cod." SortExpression="CO_RESP" HeaderStyle-CssClass="noprint"
                        ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                        <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_CONTROLE" HeaderText="Nº Controle">
                        <ItemStyle VerticalAlign="Middle" Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NU_CPF_RESP" HeaderText="Nº CPF">
                        <ItemStyle VerticalAlign="Middle" Width="70px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Responsável" DataField="NO_RESP">
                        <ItemStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="Sexo" DataField="sexo">
                        <ItemStyle Width="40px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                    <asp:BoundField DataField="DT_NASC_RESP" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Nascto">
                        <ItemStyle VerticalAlign="Middle" Width="55px" HorizontalAlign="Left" />
                    </asp:BoundField>                                                                                                    
                    <asp:BoundField DataField="NU_TELE_CELU_RESP" HeaderText="Celular">
                        <ItemStyle VerticalAlign="Middle" Width="95px" HorizontalAlign="Left" />
                    </asp:BoundField>                              
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtLR">           
            <p id="pAcesso" class="pAcesso">
                Verifique os responsáveis existentes no quadro acima para iniciar os trabalhos.</p>
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
