<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComoFazer.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.ComoFazer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Como Fazer</title>
    <style type="text/css">
        .divComoFazer
        {
            color: #FF6600;
            width: 270px;
        }
        .divComoFazer a:link, .divComoFazer a:active, .divComoFazer a:visited
        {
            color: #999999;
            text-decoration: none;
        }
        .divComoFazer a:hover
        {
            color: #FF6600;
            text-decoration: underline;
        }
        .divComoFazer span
        {
            color: #EF9D1D;
            font-family: Arial;
            margin-left: 2px;
            text-transform: uppercase;
        }
        .divComoFazer span, .divComoFazer img
        {
            cursor: pointer;
            height: 20px;
            width: 16px;
        }
        .divButtons
        {
            width: 100%;
            text-align: right;
            padding-bottom: 10px;
        }
        .divComoFazerContent
        {
            background-color: #FFFFFF;
            color: #000000;
            display: block;
            padding: 10px;
            position: absolute;
            width: 650px;
            right: -7px;
        }
        /* ESTILO DO GRID DOS COMO FAZER */
        .grdComoFazer { width: 100%; } 
        .grdComoFazer th
        {
            text-transform: uppercase;
            font-weight: bold;
        }
        .grdComoFazer td { padding: 3px 5px; }
        .SeqCol { text-align: center; }
        /* Esta classe é declarada no evento grdComoFazer_RowDataBound do Grid, assim como o link */
        .activeLine { cursor: pointer; }
        .activeLine td { color: Blue !important; }
        .activeLine td:hover { text-decoration: underline; }
        .inactiveLine { background-color: #F4F4F4 !important; }
        /*************************/
        .emptyDataRowStyle td        
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 10px center;
            padding: 10px 165px 10px 170px !important;
        }
        .divComoFazerTitle
        {
            background-color: Transparent;
            text-align: right;
        }
        .externBorder
        {
            background-color: #FFFFDF;
            border: solid 1px #667AB3;
            padding: 5px;
        }
    </style>
</head>
<body>
    <div id="divUnidadeEducacionalContainer">
        <form id="frmComoFazer" runat="server">
        <div id="divComoFazer" class="divComoFazer">
            <div id="divComoFazerContent" class="divComoFazerContent">
                <div class="externBorder">
                    <div id="divButtons" class="divButtons">
                        <a id="lnkClose" title="Fechar Como fazer" href="#">[x]</a>
                    </div>
                    <asp:GridView runat="server" ID="grdComoFazer" AutoGenerateColumns="False" CssClass="grdComoFazer"
                        GridLines="Vertical" OnRowDataBound="grdComoFazer_RowDataBound" OnRowCommand="grdComoFazer_RowCommand"
                        DataKeyNames="CO_PROXIPASSOS">
                        <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            SEM REGISTROS.<br />
                        </EmptyDataTemplate>
                        <HeaderStyle Height="20px" BackColor="#667AB3" ForeColor="White" />
                        <RowStyle BackColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Seq" DataField="CO_ORDEM_MENU" ItemStyle-CssClass="SeqCol">
                                <ItemStyle CssClass="SeqCol"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Descrição" ItemStyle-Width="370px" DataField="NO_DESCRICAO" />
                            <asp:BoundField HeaderText="Referência" ItemStyle-Width="260px" DataField="DE_ITEM_REFER" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#divComoFazerContent #lnkClose").click(function (event) {
                $("#divComoFazer").hide();
            });
        });
    </script>
</body>
</html>
