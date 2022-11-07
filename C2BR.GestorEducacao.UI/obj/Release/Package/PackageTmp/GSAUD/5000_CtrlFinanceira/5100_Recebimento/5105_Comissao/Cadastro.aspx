<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5105_Comissao.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 910px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 0px 1px 9px !important;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio">Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="220px" ToolTip="Selecione o profissional" />
            <asp:RequiredFieldValidator runat="server" ID="rfvProfissional" CssClass="validatorField"
                ErrorMessage="O campo é requerido" ControlToValidate="ddlProfissional"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Pesquise pelo Grupo de Procedimentos Médicos">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Pesquise pelo Grupo de Procedimentos Médicos"
                OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li>
            <label title="Pesquise pelo SubGrupo de Procedimentos Médicos">
                SubGrupo</label>
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="160px" ToolTip="Pesquise pelo SubGrupo de Procedimentos Médicos" />
        </li>
        <li>
            <label title="Situação cadastral do Procedimento Comissionado">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação cadastral do Procedimento Comissionado">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="S" Text="Sem Uso"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li title="Processar arqui retorno" class="liBtnAdd" style="margin-left:20px; margin-top:17px;">
            <asp:LinkButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click"
                Width="64">PESQUISAR</asp:LinkButton>
        </li>
        <li style="clear: both; margin: 10px 0 0 0;">
            <div style="width: 900px; height: 370px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdComissoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum Orçamento associado<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkTodosProcs" Width="100%" style="margin: 0 -3px 0 -5px !important;" OnCheckedChanged="chkTodosProcs_OnCheckedChanged" AutoPostBack="true" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle Width="4px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidIdComiss" Value='<%# Eval("ID_COMISS") %>' />
                                <asp:HiddenField runat="server" ID="hidIdProced" Value='<%# Eval("ID_PROCED") %>' />
                                <asp:HiddenField runat="server" ID="hidExistente" Value='<%# Eval("Existente") %>' />
                                <asp:CheckBox ID="chkSelectProc" Checked='<%# Eval("Existente") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GRUPO" HeaderText="GRUPO">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBGRUPO" HeaderText="SUBGRUPO">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PROCEDIMENTO" HeaderText="PROCEDIMENTO">
                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Avali">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlAvaliacao" Text='<%# Eval("VL_AVALIACAO") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcAvaliacao" Checked='<%# Eval("PC_AVALIACAO") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cobrn">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlCobranca" Text='<%# Eval("VL_COBRANCA") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcCobranca" Checked='<%# Eval("PC_COBRANCA") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contr">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlContrato" Text='<%# Eval("VL_CONTRATO") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcContrato" Checked='<%# Eval("PC_CONTRATO") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ind Pac">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlIndPac" Text='<%# Eval("VL_IND_PACIENTE") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcIndPac" Checked='<%# Eval("PC_IND_PACIENTE") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ind Prc">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlIndProc" Text='<%# Eval("VL_IND_PROCEDIMENTO") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcIndProc" Checked='<%# Eval("PC_IND_PROCEDIMENTO") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Planj">
                            <ItemStyle Width="45px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtVlPlanejamento" Text='<%# Eval("VL_PLANEJAMENTO") %>' CssClass="campoDin" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                <asp:CheckBox ID="chkPcPlanejamento" Checked='<%# Eval("PC_PLANEJAMENTO") %>' runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" />%
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS">
                            <ItemStyle HorizontalAlign="Left" Width="30px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
