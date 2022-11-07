<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3205_FechaFinanProcePaciente.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 990px;
            margin-top: -0px !important;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .divAtendPendentes
        {
            border: 1px solid #CCCCCC;
            width: 530px;
            height: 170px;
            overflow-y: scroll;
            margin-left: 5px;
            margin-bottom: 13px;
        }
        .divItensPEnde
        {
            border: 1px solid #CCCCCC;
            width: 440px;
            height: 170px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 13px;
        }
        .totalItensPende
        {
            text-align: right;
            font-weight: bold;
            font-size: 13px;
        }
        .liBtnGrdFinan
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        .chk label
        {
            display:inline;
            margin-left:-5px;
        }
                .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:UpdatePanel ID="updPaciPende" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li>
                    <div style="width: 533px; text-align: center; height: 17px; background-color: #B0E0E6;
                        margin: 0 0 3px 5px;">
                        <div style="float: none;">
                            <asp:Label ID="Label1" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                                vertical-align: middle; margin-left: 4px !important;">
                                    GRADE DE PACIENTES COM ITENS PENDENTES PARA FECHAMENTO</asp:Label>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hidCoAlu" />
                    <asp:HiddenField runat="server" ID="hidCoResp" />
                    <asp:HiddenField runat="server" ID="hidIdAtend" />
                    <div id="divPaciEnca" class="divAtendPendentes" runat="server" clientidmode="static"
                        style="height: 229px">
                        <asp:GridView ID="grdPacientesPendentes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" ToolTip="Grid de Pacientes com Itens de Procedimentos médicos com situação financeira em Aberto">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum Paciente com itens financeiros em aberto<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="10px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelectPaciPend" Style="margin: 0px !important;"
                                            OnCheckedChanged="chkSelectPaciPend_OnCheckedChanged" AutoPostBack="true" />
                                        <asp:HiddenField runat="server" ID="hidCoAtend" Value='<%# Eval("ID_ATEND_MEDIC") %>' />
                                        <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# Eval("CO_ALU") %>' />
                                        <asp:HiddenField runat="server" ID="hidIdResp" Value='<%# Eval("CO_RESP") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UNID" HeaderText="UNID">
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ALU" HeaderText="PACIENTE">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CO_SEXO" HeaderText="SX">
                                    <ItemStyle HorizontalAlign="Left" Width="8px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_RESP" HeaderText="RESPONSÁVEL">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updItensPende" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li>
                    <div style="width: 442px; text-align: center; height: 17px; background-color: #FA8072;
                        margin: 0 0 3px 0px;">
                        <div style="float: none;">
                            <asp:Label ID="Label2" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                                vertical-align: middle; margin-left: 4px !important;">
                                    GRADE DE ITENS PENDENTES</asp:Label>
                        </div>
                    </div>
                    <div id="div1" class="divItensPEnde" runat="server" clientidmode="static" style="height: 211px">
                        <asp:GridView ID="grdItensPendentes" CssClass="grdBusca" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="false" ToolTip="Grid de Itens de Procedimentos médicos com situação financeira em Aberto para o paciente selecionado"
                            OnRowDataBound="grdItensPendentes_OnRowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum itens financeiro em aberto para o paciente selecionado<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CK">
                                    <ItemStyle Width="10px" CssClass="grdLinhaCenter" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkMarcaTodosItensPendentes" Checked="true" OnCheckedChanged="chkMarcaTodosItensPendentes_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIDProcMedicFinan" Value='<%# bind("ID_PROC_MEDIC_FINAN") %>' />
                                        <asp:CheckBox runat="server" ID="chkSelecGridDetalhada" Style="margin: 0px !important;"
                                            Checked="true" OnCheckedChanged="chkSelecGridDetalhada_OnCheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DATA" HeaderText="DATA/HR">
                                    <ItemStyle HorizontalAlign="Center" Width="72px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_ITEM" HeaderText="ITEM">
                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VALOR" HeaderText="VALOR">
                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Desconto">
                                    <ItemStyle Width="56px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescSolic" OnTextChanged="chkDescPer_ChenckedChanged" CssClass="txtDescSolic"
                                            Text='<%# bind("Desconto") %>' AutoPostBack="true" Enabled="true" runat="server"
                                            Width="25px" />
                                        <asp:CheckBox ID="chkDescPer" OnCheckedChanged="chkDescPer_ChenckedChanged" Enabled="true"
                                            runat="server" AutoPostBack="true" Text="%" CssClass="chk" style="margin-left:-4px;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="VALOR" HeaderText="R$ Item">
                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <%--<asp:TemplateField HeaderText="R$ Item">
                                    <ItemStyle Width="30px" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" Enabled="false" Text='<%# bind("VALOR") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="margin: -12px 0 0 301px;">
                        <ul>
                            <li>
                                <asp:Label runat="server" ID="lblTotal" Font-Bold="true">TOTAL</asp:Label>
                                <asp:TextBox runat="server" ID="txtTotalItensPende" Width="85px" CssClass="totalItensPende" ReadOnly="true"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="clear: both; width: 975px;">
            <div style="width: 100%; text-align: center; height: 17px; background-color: #DEB887;
                margin: 0 0 3px 5px;">
                <div style="float: none;">
                    <asp:Label ID="Label3" runat="server" Style="font-size: 1.1em; font-family: Tahoma;
                        vertical-align: middle; margin-left: 4px !important;">
                                    GRID FINANCEIRA</asp:Label>
                </div>
            </div>
        </li>
        <asp:UpdatePanel ID="updFinanceiraInfos" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <li style="clear: both; float: left; margin-left: 75px">
                    <div id="InfosFields" class="infoFiledsC" title="Selecione os Itens Solicitados">
                        <ul>
                            <li style="margin-top: 12px; clear: none; margin-left: 20px;">
                                <asp:CheckBox ID="ckbAtualizaFinancSolic" CssClass="chkIsento" Enabled="true" Checked="true"
                                    runat="server" ToolTip="Selecione se atualizará o financeiro" AutoPostBack="True"
                                    OnCheckedChanged="ckbAtualizaFinancSolic_CheckedChanged" />
                                <asp:Label runat="server" ID="lblAtuFin" Style="margin-left: -4px;">Atualiza Financeiro</asp:Label>
                            </li>
                            <li style="margin-top: 12px;">
                                <asp:CheckBox ID="chkConsolValorTitul" CssClass="chkIsento" Enabled="false" runat="server"
                                    ToolTip="Selecione se consolidará os valores em um único título financeiro" AutoPostBack="True"
                                    OnCheckedChanged="chkConsolValorTitul_CheckedChanged" />
                                <asp:Label runat="server" ID="Label4" Style="margin-left: -4px;">Valores Único Título</asp:Label>
                            </li>
                            <li style="clear: both; margin-left: 25px; margin-top: 10px;">
                                <asp:HiddenField ID="hidValorTotal" runat="server" Value="" />
                                <label for="txtValorTotal" title="Valor Total da Solicitação (R$)">
                                    Total R$</label>
                                <asp:TextBox ID="txtValorTotal" Width="61" CssClass="txtDesctoMensa" Enabled="false"
                                    runat="server"></asp:TextBox>
                            </li>
                            <li class="liEspaco" style="clear: none; margin-top: 10px;">
                                <label for="txtQtdParcelas" title="Informe em quantas parcelas será feito o parcelamento">
                                    QP</label>
                                <asp:TextBox ID="txtQtdParcelas" Width="20px" runat="server" ToolTip="Informe em quantas parcelas será feito o parcelamento" Text="1"></asp:TextBox>
                            </li>
                            <li class="liEspaco" style="clear: none; margin-top: 9px;">
                                <label for="txtDtVectoSolic" title="Data de Vencimento">
                                    Data 1ª Parcela</label>
                                <asp:TextBox ToolTip="Informe a Data de Vencimento da Solicitação" ID="txtDtVectoSolic"
                                    class="campoData" runat="server"></asp:TextBox>
                            </li>
                            <li class="liEspaco" style="clear: none; margin-top: 9px;">
                                <label for="ddlDiaVectoParcMater" title="Selecione o melhor Dia de Vencimento das Parcelas">
                                    Dia</label>
                                <asp:DropDownList ID="ddlDiaVectoParcMater" Width="40px" ToolTip="Selecione o melhor Dia de Vencimento das Parcelas"
                                    runat="server">
                                </asp:DropDownList>
                            </li>
                            <li style="margin-left: 25px; clear: both;">
                                <label title="Desconto nas Parcelas" style="color: Red;">
                                    Desconto</label>
                                <label for="ddlTipoDesctoParc" title="Tipo de desconto da mensalidade">
                                    Tipo Desconto</label>
                                <asp:DropDownList ID="ddlTipoDesctoParc" OnSelectedIndexChanged="ddlTipoDesctoParc_SelectedIndexChanged"
                                    AutoPostBack="true" ToolTip="Selecione o Tipo de Desconto" CssClass="ddlTipoDesctoMensa"
                                    runat="server">
                                    <asp:ListItem Selected="true" Text="Total" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li class="liEspaco" style="margin-top: 14px;">
                                <label for="txtQtdeMesesDesctoParc" title="Quantidade de meses de desconto">
                                    Qt Meses</label>
                                <asp:TextBox ID="txtQtdeMesesDesctoParc" Width="30px" CssClass="txtQtdeMesesDesctoMensa"
                                    runat="server" Enabled="false" ToolTip="Informe a quantidade de meses de desconto">
                                </asp:TextBox>
                            </li>
                            <li class="liEspaco" style="margin-top: 14px;">
                                <label for="txtDesctoMensaParc" title="R$ Desconto">
                                    R$ Desconto</label>
                                <asp:TextBox ID="txtDesctoMensaParc" Width="50" CssClass="txtDesctoMensa" runat="server">
                                </asp:TextBox>
                            </li>
                            <li class="liEspaco" style="margin-top: 14px;">
                                <label for="txtMesIniDescontoParc" title="Parcela de início do desconto">
                                    PID</label>
                                <asp:TextBox ID="txtMesIniDescontoParc" Enabled="false" Width="21px" ToolTip="Parcela de início do desconto"
                                    CssClass="txtMesIniDesconto" Style="text-align: right;" runat="server">
                                </asp:TextBox>
                            </li>
                            <li runat="server" id="liBtnGrdFinanMater" class="liBtnGrdFinan" style="clear: both;
                                margin-left: 25px;">
                                <asp:LinkButton ID="lnkMontaGridParcMater" OnClick="lnkMontaGridParcMaterial_Click"
                                    ValidationGroup="vgMontaGridMensa" runat="server" Style="margin: 0 auto;" ToolTip="Montar Grid com as parcelas.">
                                    <asp:Label runat="server" ID="Label166" ForeColor="GhostWhite" Text="GERAR GRID FINANCEIRA"></asp:Label>
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="updGridFinanceira" UpdateMode="Conditional">
            <ContentTemplate>
                <li style="width: 560px; float: left">
                    <div id="divMateriaisAluno" runat="server" style="height: 140px; border: 1px solid #CCCCCC;
                        overflow-y: scroll; margin-top: 10px;">
                        <asp:GridView runat="server" ID="grdParcelasMaterial" CssClass="grdBusca" ShowHeader="true"
                            ShowHeaderWhenEmpty="true" ToolTip="Grid demonstrativa das parcelas de materiais coletivos do aluno."
                            AutoGenerateColumns="False" Width="100%">
                            <RowStyle CssClass="rowStyle" />
                            <HeaderStyle CssClass="th" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="NU_DOC" HeaderText="Nº Docto">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NU_PAR" HeaderText="Nº Par">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorParcela" DataFormatString="{0:N2}" HeaderText="R$ Mensal">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorDescto" DataFormatString="{0:N2}" HeaderText="R$ Descto">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorLiquido" DataFormatString="{0:N2}" HeaderText="R$ Liquido">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorMulta" DataFormatString="{0:N2}" HeaderText="% Multa">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorJuros" DataFormatString="{0:N2}" HeaderText="% Juros">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPaciPende">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        <script type="text/javascript">

        $(document).ready(function () {
            JavscriptAtend();
        });

        function JavscriptAtend() {
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            JavscriptAtend();
        });

        </script>
</asp:Content>
