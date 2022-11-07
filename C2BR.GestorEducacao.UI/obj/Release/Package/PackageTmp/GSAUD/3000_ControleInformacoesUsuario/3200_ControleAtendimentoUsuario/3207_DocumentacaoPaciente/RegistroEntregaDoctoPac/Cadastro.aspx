<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._RegistroEntregaDoctoPac.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 460px; }
        
        /*--> CSS LIs */
        .liAno
        {
            clear: both;
            margin-top: 7px;
        }
        .liEspaco
        {
            margin-left: 10px;
            margin-top: 7px;
        }
        .liFunc
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        .liData
        {
            clear: both;
            margin-top: 10px;
        }
        .liGridDoc
        {
            clear: both;
            margin-left: 0px;
        }
        .liGrid3Doc
        {
            background-color: #EEEEEE;
            height: 15px;
            margin-top: 20px;
            clear: both;
            margin-left: 1px;
            width: 320px;
            text-align: center;
        }
        
        /*--> CSS DADOS */
        .divGridDoc
        {
            height: 189px;
            width: 320px;
            overflow-y: scroll;
            margin-top: 10px;
            border-bottom: solid gray 1px;
            border-left: solid gray 1px; 
        }        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="clear: both;">
            <label class="lblObrigatorio">Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" Width="215" class="ddlPadrao" AutoPostBack="true" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo paciente é obrigatório" ControlToValidate="ddlPaciente" Display="Dynamic" />
        </li>
        <li>
            <label class="lblObrigatorio">Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="100" ToolTip="Selecione a operadora" AutoPostBack="true" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfvOperadora" runat="server" ControlToValidate="ddlOperadora"
                CssClass="validatorField" ErrorMessage="Operadora deve ser informada"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrid3Doc">DOCUMENTOS ENTREGUES</li>
        <li class="liGridDoc">
            <div id="divGrid" runat="server" class="divGridDoc">
                <asp:GridView ID="grdDocumentos" CssClass="grdBusca" Width="300px" runat="server"
                    AutoGenerateColumns="False">
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
                                <asp:CheckBox ID="ckSelect" Checked='<%# bind("chkSel") %>' runat="server" />
                                 <asp:HiddenField ID="hdCoTpDoc" runat="server" Value='<%# bind("CO_TP_DOC_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DE_TP_DOC_MAT" HeaderText="Documento">
                            <ItemStyle Width="220px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sigla">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSigla" runat="server" Text='<%# bind("SIG_TP_DOC_MAT") %>'></asp:Label>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
</asp:Content>
