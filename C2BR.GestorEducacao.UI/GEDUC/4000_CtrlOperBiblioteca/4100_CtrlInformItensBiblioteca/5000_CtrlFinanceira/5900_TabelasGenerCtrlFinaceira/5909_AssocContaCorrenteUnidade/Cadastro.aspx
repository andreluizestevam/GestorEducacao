<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5909_AssocContaCorrenteUnidade.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 474px; }
        * {margin-bottom:0px !important;}
        
        /*--> CSS LIs */
        .ulDados li {margin-bottom:10px !important;}
        .liTituloGrid
        {
            clear: both;
            background-color: #EEEEEE;
            height: 15px;
            margin-top: 20px;
            width: 483px;
            text-align: center;
            padding: 5 0 5 0;
        } 
        .liEspaco { margin-left:10px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 189px;
            width: 485px;
            overflow-y: scroll;
            border: solid #999999 1px;
            margin-top:-9px;
        }
        
        .ddlBanco {width:160px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola de Ensino">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar"
                ToolTip="Selecione a Unidade/Escola" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" CssClass="validatorField"
                ErrorMessage="Unidade/Escola é requerida">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlBanco" title="Banco">Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                ToolTip="Selecione o Banco" 
                onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liTituloGrid">CONTA CORRENTE</li>
        <li>
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdContas" CssClass="grdBusca" Width="465px" runat="server"
                    AutoGenerateColumns="False"
                    OnRowDataBound="grdContas_RowDataBound">
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
                                <asp:CheckBox ID="ckSelect" runat="server" 
                                    oncheckedchanged="ckSelect_CheckedChanged" AutoPostBack="true" />
                                <asp:HiddenField ID="hiddenBanco" runat="server" Value='<%# bind("IDEBANCO") %>' />
                                <asp:HiddenField ID="hiddenAgencia" runat="server" Value='<%# bind("CO_AGENCIA") %>' />
                                <asp:HiddenField ID="hiddenConta" runat="server" Value='<%# bind("CO_CONTA") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DESBANCO" HeaderText="Banco">
                            <ItemStyle Width="165px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_AGENCIA" HeaderText="Agência">
                            <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_CONTA_DIG" HeaderText="Conta">
                            <ItemStyle Width="66px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FLAG_EMITE_BOLETO_BANC" HeaderText="Boleto">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CO_STATUS" HeaderText="Situação">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
    </script>
</asp:Content>
