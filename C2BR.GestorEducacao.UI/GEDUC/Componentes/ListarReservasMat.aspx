<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarReservasMat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ListarReservasMat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reservas de Matrícula</title>
    <style type="text/css">
        #divListarReservasMatContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarReservasMatContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarReservasMatContent .alternateRowStyleRME td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
        }
        #divListarReservasMatContent .rowStyleRME td
        {
        	padding-left: 5px;
        }
        #divListarReservasMatContainer #divRodapeRME
        {
            margin-top: 10px;
            float: right;
        }
        #divListarReservasMatContainer #imgLogoGestorRME
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtRME
        {
            float: left;
            margin-top: 10px;
            width: 330px;
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
        #divListarReservasMatContent
        {
        	height: 126px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divListarReservasMatContainer">
        <form id="frmListarReservasMat" runat="server">
        <div id="divListarReservasMatContent">
            <asp:GridView runat="server" ID="grdListarReservasMat" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdListarReservasMat_RowDataBound" DataKeyNames="NU_RESERVA" >
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhuma Reserva de Matrícula Encontrada<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />                
                <AlternatingRowStyle CssClass="alternateRowStyleRME" Height="15" />
                <RowStyle CssClass="rowStyleRME" Height="15" />
                <Columns>
                    <asp:BoundField DataField="NU_RESERVA" HeaderText="Nº Reserva">
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Nome do Aluno" DataField="aluno">
                        <ItemStyle Width="210px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>      
                    <asp:BoundField HeaderText="Modalidade" DataField="DE_MODU_CUR">
                        <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>              
                    <asp:BoundField DataField="NO_CUR" HeaderText="Série">
                        <ItemStyle VerticalAlign="Middle" Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>      
                    <asp:BoundField DataField="turno" HeaderText="Turno">
                        <ItemStyle VerticalAlign="Middle" Width="70px" HorizontalAlign="Left" />
                    </asp:BoundField>  
                    <asp:BoundField DataField="cpfResp" HeaderText="CPF Responsável">
                        <ItemStyle VerticalAlign="Middle" Width="80px" HorizontalAlign="Left" />
                    </asp:BoundField>           
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtRME">           
            <p id="pAcesso" class="pAcesso">
                Verifique as reservas existentes no quadro acima para iniciar os trabalhos.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeRME">
            <img id="imgLogoGestorRME" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>    
</body>
</html>
