<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarPacientes.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.Componentes.ListarPacientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lista de Alunos</title>
    <style type="text/css">
        #divListarAlunosContent { width: 100%; }
        #divListarAlunosContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarAlunosContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divListarAlunosContent .alternateRowStyleLA td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
            padding-right: 5px;
        }
        #divListarAlunosContent .rowStyleLA td
        {
        	padding-left: 5px;
        	padding-right: 5px;
        }
        #divListarAlunosContainer #divRodapeLA
        {
            margin-top: 10px;
            float: right;
        }
        #divListarAlunosContainer #imgLogoGestorLA
        {
            width: 80px;
            height: 30px;
            padding-right: 5px;
            margin-right: 5px;
        }
        #divHelpTxtLA
        {
            float: left;
            margin-top: 10px;
            width: 430px;
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
        #divListarAlunosContent
        {
        	height: 261px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
        .headerStyleLA {text-transform:uppercase;}
    </style>
</head>
<body>
    <div id="divListarAlunosContainer">
        <form id="frmListarAlunos" runat="server">
        <div id="divListarAlunosContent">
            <asp:GridView runat="server" ID="grdListarAlunos" AutoGenerateColumns="false" AllowPaging="false"
                OnRowDataBound="grdListarAlunos_RowDataBound" DataKeyNames="CO_ALU">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Paciente Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" CssClass="headerStyleLA" />
                <AlternatingRowStyle CssClass="alternateRowStyleLA" Height="15" />
                <RowStyle CssClass="rowStyleLA" Height="15" />
                <Columns>
                    <asp:BoundField HeaderText="PRONTUÁRIO" DataField="NU_NIRE">
                        <ItemStyle Width="60px" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CNES/SUS" DataField="NU_NIS">
                        <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Paciente" DataField="NO_ALU">
                        <ItemStyle Width="300px" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Sexo" DataField="sexo">
                        <ItemStyle Width="50px" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="cpf" HeaderText="Nº CPF">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DT_NASC_ALU" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Nascto">
                        <ItemStyle Width="70px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField Visible="false" DataField="CO_ALU" HeaderText="Cod." SortExpression="CO_ALU"
                        HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtLA">
            <p id="pAcesso" class="pAcesso">
                Verifique os Pacientes existentes no quadro acima para iniciar os trabalhos.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeLA">
            <img id="imgLogoGestorLA" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
</body>
</html>
