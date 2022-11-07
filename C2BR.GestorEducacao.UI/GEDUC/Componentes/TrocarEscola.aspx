<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrocarEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.TrocarEscola" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trocar Escola</title>
    <style type="text/css">
        #divUnidadeEducacionalContent .rowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divUnidadeEducacionalContent .alternateRowStyle:hover
        {
            background-color: #EBF0FB;
            cursor: pointer;
        }
        #divUnidadeEducacionalContent .alternateRowStyleTE td
        {
            background-color: #EEEEEE;
            padding-left: 5px;
        }
        #divUnidadeEducacionalContent .rowStyleTE td
        {
        	padding-left: 5px;
        }
        #divUnidadeEducacionalContainer #divRodapeTE
        {
            margin-top: 10px;
            float: right;
        }
        #divUnidadeEducacionalContainer #imgLogoGestorTE
        {
            width: 127px;
            height: 30px;
        }
        #divHelpTxtTE
        {
            float: left;
            margin-top: 10px;
            width: 360px;
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
        #divUnidadeEducacionalContent
        {
        	height: 126px;
        	overflow-y: auto;
        	border: 1px solid #EBF0FB;
        }
    </style>
</head>
<body>
    <div id="divUnidadeEducacionalContainer">
        <form id="frmTrocarEscola" runat="server">
        <div id="divUnidadeEducacionalContent">
            <asp:GridView runat="server" ID="grdUnidadeEducacional" AutoGenerateColumns="false"
                AllowPaging="false" GridLines="Vertical" OnRowDataBound="grdUnidadeEducacional_RowDataBound" DataKeyNames="CO_EMP"
                OnSelectedIndexChanged="grdUnidadeEducacional_SelectedIndexChanged">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhuma Unidade Educacional Encontrada<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />                
                <AlternatingRowStyle CssClass="alternateRowStyleTE" Height="15" />
                <RowStyle CssClass="rowStyleTE" Height="15" />
                <Columns>
                    <asp:BoundField Visible="false" DataField="CO_EMP" HeaderText="Cod." SortExpression="CO_EMP" HeaderStyle-CssClass="noprint"
                        ItemStyle-CssClass="noprint">
                        <HeaderStyle CssClass="noprint"></HeaderStyle>
                        <ItemStyle Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SIGLA" HeaderText="Sigla">
                        <ItemStyle Width="40px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Nome" DataField="NO_FANTAS_EMP">
                        <ItemStyle Width="250px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>      
                    <asp:BoundField HeaderText="Perfil" DataField="PERFIL">
                        <ItemStyle Width="150px" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>              
                    <asp:BoundField DataField="NO_TIPOEMP" HeaderText="Tipo de Ensino">
                        <ItemStyle VerticalAlign="Middle" Width="240px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:CommandField HeaderText="Nome" ShowSelectButton="True" Visible="False" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="divHelpTxtTE">           
            <p id="pAcesso" class="pAcesso">
                Clique em uma das escolas do quadro acima para iniciar os trabalhos.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>
        <div id="divRodapeTE">
            <img id="imgLogoGestorTE" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
</body>
</html>
