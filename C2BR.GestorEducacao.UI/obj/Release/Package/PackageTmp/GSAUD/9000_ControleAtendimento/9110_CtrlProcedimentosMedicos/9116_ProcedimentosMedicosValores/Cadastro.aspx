<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9116_ProcedimentosMedicosValores.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 450px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        input
        {
            height: 13px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .campoDinheiro
        {
            text-align: right;
            width: 55px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label title="Código do Procedimento Médico" class="lblObrigatorio">
                Código Pro.</label>
            <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Código do Procedimento Médico"
                Width="50px" CssClass="campoCodigo" Enabled="false"></asp:TextBox>
        </li>
        <li>
            <label title="Nome do Procedimento Médico" class="lblObrigatorio">
                Nome Procedimento</label>
            <asp:TextBox runat="server" ID="txtNoProcedimento" Width="361px" MaxLength="100"
                ToolTip="Nome do Procedimento Médico" Enabled="false"></asp:TextBox>
        </li>
        <li style="margin: 0 0 20px 32px;">
            <fieldset title="Informações do novo valor que será atribuído ao Procedimento Médico selecionado"
                style="padding: 0 15px;">
                <legend>Novo Valor</legend>
                <ul>
                    <li style="clear: both; margin: 20px -5px 0 -8px">
                        <asp:CheckBox runat="server" ID="chkIncluNovoValor" ToolTip="Marque para liberar os campos e incluir um novo valor"
                            ClientIDMode="Static" />
                    </li>
                    <li>
                        <label class="lblObrigatorio">
                            Data Valores</label>
                        <asp:TextBox runat="server" ID="txtDtValores" Width="53px" CssClass="campoData" Enabled="false"
                            ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor de Custo do procedimento">
                            R$ Custo</label>
                        <asp:TextBox runat="server" ID="txtVlCusto" CssClass="campoDinheiro" ToolTip="Valor de Custo atual do procedimento"
                            Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor Base da Operadora para o procedimento">
                            R$ Base</label>
                        <asp:TextBox runat="server" ID="txtVlBase" CssClass="campoDinheiro" ToolTip="Valor Base atual do procedimento"
                            Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Valor Base da Operadora para o procedimento">
                            R$ Resti</label>
                        <asp:TextBox runat="server" ID="txtVlRestitu" CssClass="campoDinheiro" ToolTip="Valor de Restituição atual do procedimento"
                            Enabled="false" ClientIDMode="Static"></asp:TextBox>
                    </li>
                    <li>
                        <label title="Situação dos valores do procedimento">
                            Situação</label>
                        <asp:DropDownList runat="server" ID="ddlSitu" ToolTip="Situação dos valores do procedimento"
                            Enabled="false" Width="56px" ClientIDMode="Static">
                            <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                            <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
        </li>
        <li>
            
        </li>
        <li style="margin-left: 30px; width:370px">
        <div style="width:100.5% ; text-align: center; height: 17px; background-color: #CAE1FF;
                margin: 0 0 3px 0px;">
                <div style="float: none;">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                        vertical-align: middle; margin-left: 4px !important;">
                                    GRADE DE VALORES DO PROCEDIMENTO SELECIONADO</asp:Label>
                </div>
            </div>
            <div id="divServAmbu" runat="server" class="divGeralApresenta" style="overflow-y: scroll;
                width: 370px; height: 290px; border: 1px solid #CCC">
                <asp:GridView ID="grdValores" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="DT_LANC_V" HeaderText="DATA">
                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="R$ CUSTO">
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidCoStatus" Value='<%# bind("CO_STATUS") %>' />
                                <asp:HiddenField runat="server" ID="hidIdValor" Value='<%# bind("ID_VALOR") %>' />
                                <asp:Label runat="server" ID="lblVlCusto" Text='<%# bind("VL_CUSTO_V") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VL_BASE_V" HeaderText="R$ BASE">
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VL_RESTI_V" HeaderText="R$ RESTI">
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="STATUS">
                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmUs" Text="Em Uso" Style="color: #66cd00; font-weight: bold"
                                    Visible='<%# bind("SW_USO") %>'></asp:Label>
                                <asp:Label runat="server" ID="lblInativo" Style="color: #FFa07a; font-weight: bold"
                                    Text="Inativo" Visible='<%# bind("SW_INATIVO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCodigo").mask("99999999");
            $(".campoDinheiro").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //Se ao deixar o campo, o mesmo estiver vazio, insere 0
            $("#txtVlCusto").blur(function () {
                if ($("#txtVlCusto").val() == "")
                    $("#txtVlCusto").val("0,00");
            });
            $("#txtVlBase").blur(function () {
                if ($("#txtVlBase").val() == "")
                    $("#txtVlBase").val("0,00");
            });
            $("#txtVlRestitu").blur(function () {
                if ($("#txtVlRestitu").val() == "")
                    $("#txtVlRestitu").val("0,00");
            });

            //Responsável por habilitar/desabilitar os campos de novo registro quando for clicado no checkbox
            $("#chkIncluNovoValor").click(function (evento) {
                if ($("#chkIncluNovoValor").attr("checked")) {

                    //Habilita data
                    $("#txtDtValores").removeAttr('disabled');
                    $("#txtDtValores").css("background-color", "White");

                    //Habilita valor de custo
                    $("#txtVlCusto").removeAttr('disabled');
                    $("#txtVlCusto").css("background-color", "White");

                    //Habilita valor Base
                    $("#txtVlBase").removeAttr('disabled');
                    $("#txtVlBase").css("background-color", "White");

                    //Habilita valor de Restituição
                    $("#txtVlRestitu").removeAttr('disabled');
                    $("#txtVlRestitu").css("background-color", "White");

                    //Habilita situação
                    $("#ddlSitu").removeAttr('disabled');
                    $("#ddlSitu").css("background-color", "White");
                }
                else {

                    //Desabilita data
                    $("#txtDtValores").attr('disabled', true);
                    $("#txtDtValores").css("background-color", "#F5F5F5");

                    //Desabilita Valor de Custo
                    $("#txtVlCusto").attr('disabled', true);
                    $("#txtVlCusto").css("background-color", "#F5F5F5");
                    $("#txtVlCusto").val("");

                    //Desabilita Valor Base
                    $("#txtVlBase").attr('disabled', true);
                    $("#txtVlBase").css("background-color", "#F5F5F5");
                    $("#txtVlBase").val("");

                    //Desabilita Valor Restituição
                    $("#txtVlRestitu").attr('disabled', true);
                    $("#txtVlRestitu").css("background-color", "#F5F5F5");
                    $("#txtVlRestitu").val("");

                    //Desabilita Situação
                    $("#ddlSitu").attr('disabled', true);
                    $("#ddlSitu").css("background-color", "#F5F5F5");
                    $("#ddlSitu").val("A");
                }
            });
        });
    </script>
</asp:Content>
