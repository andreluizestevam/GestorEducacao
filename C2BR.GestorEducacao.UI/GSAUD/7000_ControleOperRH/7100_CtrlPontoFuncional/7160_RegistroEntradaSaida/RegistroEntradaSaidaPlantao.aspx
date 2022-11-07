<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="RegistroEntradaSaidaPlantao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7160_RegistroEntradaSaida.RegistroEntradaSaidaPlantao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 920px;
            margin-top: 30px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
        }
        .divGrid
        {
            width: 920px;
            height: 360px;
            border: 1px solid #CCCCCC;
            float: left;
            margin: 7px 0 15px 0;
            overflow-y: scroll;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:UpdatePanel runat="server" ID="updCenter" UpdateMode="Conditional">
            <ContentTemplate>
                <li style="width: 230px; margin-left: 50px;">
                    <asp:Label runat="server" ID="lblUnidade" class="lblObrigatorio" ToolTip="Unidade do Plantão">Unidade</asp:Label>
                    <asp:DropDownList runat="server" Width="230px" ID="ddlUnidade" ToolTip="Unidade do Plantão">
                    </asp:DropDownList>
                </li>
                <li style="width: 170px">
                    <label title="Local do Plantão">
                        Local</label>
                    <asp:DropDownList runat="server" Width="170px" ID="ddlLocal" ToolTip="Local Do Plantão">
                    </asp:DropDownList>
                </li>
                <li class="liboth">
                    <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
                    <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                        ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                    <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                    <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                        ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
                </li>
                <li style="width: 170px">
                    <asp:Label runat="server" ID="lbltppl" ToolTip="Tipo do Plantão">Tipo de Plantão</asp:Label>
                    <asp:DropDownList ID="ddlTipoPlantao" runat="server" Width="170px" ToolTip="Tipo do Plantão">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 10px;">
                    <asp:ImageButton runat="server" ID="imgPesq" OnClick="OnClick_PesqGrid" src="../../../../Library/IMG/Gestor_BtnPesquisa.png" />
                </li>
                <li style="clear: both">
                    <div class="divGrid">
                        <ul>
                            <li style="width: 100%; text-align: center; text-transform: uppercase; margin-top: 0px;
                                margin-left: auto; background-color: #40E0D0; margin-bottom: auto;">
                                <label style="font-size: 1.1em; font-family: Tahoma;">
                                    Agenda</label>
                            </li>
                            <asp:GridView ID="grdAgendaPlantoes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                AutoGenerateColumns="false" ToolTip="Grid de Plantões agendados dentro dos parâmetros informados">
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidcoPla" Value='<%# Eval("CO_AGEND") %>' runat="server" />
                                            <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_OnCheckedChanged"
                                                AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NO_COL" HeaderText="NOME">
                                        <ItemStyle HorizontalAlign="Left" Width="210px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="APEL_COL" HeaderText="APELIDO">
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ESPEC" HeaderText="ESPEC">
                                        <ItemStyle HorizontalAlign="Left" Width="45px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CHP" HeaderText="CHP">
                                        <ItemStyle HorizontalAlign="Center" Width="18px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DIP" HeaderText="DIP">
                                        <ItemStyle HorizontalAlign="Center" Width="18px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DtHrPrevConcatV" HeaderText="PLANEJADO">
                                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="REALIZAÇÃO (ENTRADA)">
                                        <ItemStyle HorizontalAlign="Center" Width="110px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDataRealiIni" CssClass="campoData" Style="margin: 0px;" runat="server"
                                                Text='<%# Eval("dtIniV") %>' Enabled="false" />
                                            <asp:TextBox ID="txtHoraRealiIni" CssClass="campoHora" Style="margin: 0px; margin-left: 7px;"
                                                Text='<%# Eval("hrIniV") %>' runat="server" Width="26px" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REALIZAÇÃO (SAÍDA)">
                                        <ItemStyle HorizontalAlign="Center" Width="110px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDataRealiFim" CssClass="campoData" Style="margin: 0px;" runat="server"
                                                Text='<%# Eval("dtRealV") %>' Enabled="false" />
                                            <asp:TextBox ID="txtHoraRealiFim" CssClass="campoHora" Style="margin: 0px; margin-left: 7px;"
                                                Text='<%# Eval("hrRealV") %>' runat="server" Width="26px" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SIT">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidCoSitu" Value='<%# Eval("situ") %>' />
                                            <asp:DropDownList ID="ddlSitu" Style="margin: 0px;" runat="server" Width="43px" Enabled="false">
                                                <asp:ListItem Text="ABE" Value="A">
                                                </asp:ListItem>
                                                <asp:ListItem Text="CAN" Value="C">
                                                </asp:ListItem>
                                                <asp:ListItem Text="REA" Value="R">
                                                </asp:ListItem>
                                                <asp:ListItem Text="PLA" Value="P">
                                                </asp:ListItem>
                                                <asp:ListItem Text="SUS" Value="S">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ul>
                    </div>
                </li>
                <li style="clear: both; margin-top: -7px">
                    <label>
                        Legenda: CHP (Carga Horária Plantão) - DIP (Descanso Intervalo Plantões) SIT (Situação)
                        - ABE (Em Aberto) - CAN (Cancelado) - REA (Realizado) - PLA (Planejado)- SUS (Suspenso)
                    </label>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            carregaCss();
        });

        function carregaCss() {
            $(".campoHora").mask("99:99");
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

    </script>
</asp:Content>
