<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._AssociacaoDocOperadora.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 350px; }
        
        /*--> CSS LIs */
        .liModalidade
        {
            clear: both;
            margin-top:10px !important;
        }
        .liSerie
        {
            margin-left: 10px;
            margin-top: 10px;
        }
        .liGrid3
        {
            clear: both;
            background-color: #EEEEEE;
            height: 15px;
            margin-top: 20px;
            width: 320px;
            text-align: center;
            padding: 5 0 5 0;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 289px;
            width: 320px;
            overflow-y: scroll;
            margin-top: 10px;
            border-bottom: solid gray 1px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liboth">
            <label>Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" CssClass="lblObrigatorio" ToolTip="Selecione a operadora" Width="195px" AutoPostBack="true" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfvOperadora" runat="server" ControlToValidate="ddlOperadora"
                CssClass="validatorField" ErrorMessage="Operadora deve ser informada"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrid3">DOCUMENTOS ENTREGUES</li>
        <li>
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdDocumentos" CssClass="grdBusca" Width="300px" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemStyle Width="20px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ckSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DE_TP_DOC_MAT" HeaderText="Documento">
                            <ItemStyle Width="220px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sigla">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSigla" runat="server" Text='<%# bind("SIG_TP_DOC_MAT") %>'></asp:Label>
                                <asp:HiddenField ID="hdCoTpMat" runat="server" Value='<%# bind("CO_TP_DOC_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
