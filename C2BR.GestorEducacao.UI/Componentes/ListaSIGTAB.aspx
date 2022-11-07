<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaSIGTAB.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ListaSIGTAB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">        
        #divEnvioSMSContent  
        {
            margin:auto;
            width:340px;
        }
     </style>
</head>
<body>
    <form id="form1" runat="server">
       <div id="divEnvioSMSContent" clientidmode="Static" runat="server">
            <asp:GridView runat="server" ID="grdListarSIGTAP" AutoGenerateColumns="false" AllowPaging="false" OnSelectedIndexChanged="grdListarSIGTAP_SelectedIndexChanged">
                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Paciente Encontrado<br />
                </EmptyDataTemplate>
                <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" CssClass="headerStyleLA" />
                <AlternatingRowStyle CssClass="alternateRowStyleLA" Height="15" />
                <RowStyle CssClass="rowStyleLA" Height="15" />
                <Columns>

                    <asp:TemplateField>
                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkselectEn" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Cód. SIGTAP" DataField="CO_PROC_MEDI">
                        <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Procedimento" DataField="NM_PROC_MEDI">
                        <ItemStyle Width="500px" HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <center>
                <asp:Button runat="server" ID="btnincluir" Text=" Inserir SIGTAP no atendimento " OnClick="btnincluir_Click" />
            </center>
        </div>
        <br />
        <div id="divHelpTxtLA">
            <p id="pAcesso" class="pAcesso">
                Verifique os SIDTAP existentes no quadro acima para incluir no atendimento.</p>
            <p id="pFechar" class="pFechar">
                Clique no X para fechar a janela.</p>
        </div>

    </form>
</body>
</html>
