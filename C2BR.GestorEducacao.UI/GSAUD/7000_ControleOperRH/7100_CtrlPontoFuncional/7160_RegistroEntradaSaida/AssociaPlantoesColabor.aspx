<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="AssociaPlantoesColabor.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7160_AssociaPlantoesColabor.AssociaPlantoesColabor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 100%;
        }
        
        .ulDados li
        {
            margin-left: 5px;
        }
        
        .liPrima
        {
            clear: both;
            margin-left: 10px !important;
        }
        
        .divGridData
        {
            overflow-y: scroll;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            clear: both;
        }
        .Esp
        {
            margin-top: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ul10" class="ulDados">
        <asp:HiddenField runat="server" ID="hdCoCOl" />
        <li style="margin-left: 60px !important; margin-top: 20px !important;">
            <ul>
                <li style="margin-left: 0px;">
                    <label title="Selecione a unidade em que o plantão será realizado">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidRealPlant" OnSelectedIndexChanged="ddlUnidRealPlant_SelectedIndexChanged"
                        AutoPostBack="true" Width="200px" runat="server" ToolTip="Selecione a unidade em que o plantão será realizado">
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <li style="margin-left: 40px !important; margin-top: 10px !important;">
            <ul>
                <li style="width: 448px;">
                    <%-- Grid de profissionais --%>
                    <ul>
                        <li class="liTituloGrid" style="width: 440px; margin-right: 0px; background-color: #ffff99;
                            text-align: center; font-weight: bold;">GRID DE PROFISSIONAIS </li>
                        <li class="Esp" style="width: 180px; margin-right: 5px;">
                            <asp:Label ID="Label1" runat="server">Grupo da Especialização</asp:Label>
                            <asp:DropDownList ID="ddlGrpEspec" runat="server" Width="180px" ToolTip="Selecione o Grupo da Especialização"
                                OnSelectedIndexChanged="ddlGrpEspec_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li class="Esp">
                            <label for="ddlEspMedResCons" title="Selecione a especialidade médica solicitada pelo usuário">
                                Especialidade Médica</label>
                            <asp:DropDownList ID="ddlEspMedResCons" Width="214px" runat="server" ToolTip="Selecione a especialidade médica solicitada pelo usuário"
                                OnSelectedIndexChanged="ddlEspMedResCons_OnSelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Todas</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 10px !important;">
                            <div id="divGrdProfi" runat="server" class="divGridData" style="height: 227px; width: 440px;">
                                <asp:GridView ID="grdProfi" CssClass="grdBusca" runat="server" Style="width: 423px;
                                    height: 20px;" AutoGenerateColumns="false">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoCol" Value='<%# Eval("CO_COL") %>' runat="server" />
                                                <asp:CheckBox ID="ckSelect" runat="server" OnCheckedChanged="ckSelect_CheckedChange"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NO_COL" HeaderText="MÉDICO(A)">
                                            <ItemStyle Width="223px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DE_ESP" HeaderText="ESPECIALIDADE">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_EMP" HeaderText="UNIDADE">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                    <%-- Grid de profissionais --%>
                </li>
                <li style="clear: none !important; width: 393px; margin-left: 5px !important;">
                    <%-- Grid de horário --%>
                    <ul>
                        <li class="liTituloGrid" style="width: 100%; margin-right: 0px; background-color: #d2ffc2;
                            text-align: center; font-weight: bold;">PLANTÕES</li>
                        <li>
                            <div id="div3" runat="server" class="divGridData" style="height: 226px; width: 437px;
                                margin-top: 43px;">
                                <asp:GridView ID="grdHorario" CssClass="grdBusca" runat="server" Style="width: 420px;
                                    height: 50px;" AutoGenerateColumns="false">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="5px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hidCoPla" Value='<%# Eval("co_Plan") %>' runat="server" />
                                                <asp:CheckBox Width="20px" HorizontalAlign="Center" ID="chkSelect2" runat="server"
                                                    Checked='<%# Eval("chkpla") %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="sigla" HeaderText="SIGLA">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ch" HeaderText="CH">
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="horario" HeaderText="HORÁRIO">
                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="SEG">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeg" runat="server" Enabled="false" Checked='<%# Eval("segV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TER">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkTer" runat="server" Enabled="false" Checked='<%# Eval("terV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QUA">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkQua" runat="server" Enabled="false" Checked='<%# Eval("quaV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QUI">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkQui" runat="server" Enabled="false" Checked='<%# Eval("quiV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SEX">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSex" runat="server" Enabled="false" Checked='<%# Eval("sexV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SAB">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSab" runat="server" Enabled="false" Checked='<%# Eval("sabV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOM">
                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDom" runat="server" Enabled="false" Checked='<%# Eval("domV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                    <%-- Grid de horário --%>
                </li>
            </ul>
    </ul>
</asp:Content>
