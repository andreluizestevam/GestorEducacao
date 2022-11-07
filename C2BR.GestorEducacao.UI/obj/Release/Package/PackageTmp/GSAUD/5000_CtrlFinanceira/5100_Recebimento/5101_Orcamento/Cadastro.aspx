<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5101_Orcamento.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1030px;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 2px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            padding: 2px 3px 1px 3px;
        }
        .liBtnFinAten
        {
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
        }
        .lilnkRelat
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            margin-bottom:4px;
            padding:2px 3px 1px;
            clear:both !important;
            width:70px;
        }
        .chk
        {
            margin-left: -5px;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <ul>
                <li class="liTituloGrid" style="width: 500px !important; height: 20px !important;
                    margin-right: 0px; background-color: #ADD8E6; text-align: center; font-weight: bold;
                    margin-bottom: 2px; padding-top: 1px;">
                    <ul>
                        <li style="margin: 0px 0 0 5px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FFF">
                                PACIENTES</label>
                        </li>
                        <li style="margin-left: 10px; float: right; margin-top: 2px;">
                            <ul class="ulPer">
                                <li style="margin-top:1px;">
                                    <asp:TextBox runat="server" ID="txtNomePacAtend" Width="110px" placeholder="Pesquise pelo Nome"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="txtIniPeriAtend" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvIniPeriAtend" CssClass="validatorField"
                                        ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtIniPeriAtend"></asp:RequiredFieldValidator>
                                    <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="txtFimPeriAtend" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvFimPeriAtend" CssClass="validatorField"
                                        ErrorMessage="O campo data Final é requerido" ControlToValidate="txtFimPeriAtend"></asp:RequiredFieldValidator><br />
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="imgPesqAtendimentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imgPesqAtendimentos_OnClick" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both">
                    <div style="width: 498px; height: 150px; border: 1px solid #CCC; overflow-y: scroll"
                        id="divAgendasRecp">
                        <input type="hidden" id="divAgendasRecp_posicao" name="divAgendasRecp_posicao" />
                        <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                            ShowHeaderWhenEmpty="true">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma paciente encontrado<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidIdAgenda" Value='<%# Eval("ID_ATEND") %>' runat="server" />
                                        <asp:CheckBox ID="chkSelectPaciente" runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DTHR" HeaderText="ATENDIMENTO">
                                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PACIENTE">
                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("PACIENTE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SX" HeaderText="SX">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="RAP" HeaderText="RAP">
                                    <ItemStyle HorizontalAlign="Left" Width="85px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PROFISSIONAL" HeaderText="PROFISSIONAL">
                                    <ItemStyle HorizontalAlign="Left" Width="85px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </li>
        <li style="margin-left:-5px;">
            <ul>
                <li>
                    <ul style="width: 485px">
                        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-left: -5px;
                            background-color: #EEEEE0; text-align: center; font-weight: bold; float: left">
                            <ul>
                                <li style="margin-left: 5px; float: left; width:60px;">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FF6347">
                                        ORÇAMENTO</label>
                                </li>
                                <li title="Clique para registrar a indicação dos procedimentos" style="float: right; margin: 3px 5px 0 0; width: 70px">
                                    <img title="Registrar Indicação" style="margin-top: -1px" src="/Library/IMG/Gestor_ico_AgendaAtividades.png" height="16px" width="16px" />
                                    <asp:LinkButton ID="lnkbIndicacao" runat="server" Enabled="false" OnClick="lnkbIndicacao_Click">Indicação</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both; margin: -1px 0 0 0;">
                    <div style="width: 483px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                        <asp:GridView ID="grdProcedOrcam" CssClass="grdBusca" runat="server" Style="width: 100%;
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
                                        <asp:CheckBox ID="chkTodosProcs" Checked="true" Width="100%" style="margin: 0 -5px 0 -5px !important;" OnCheckedChanged="chkTodosProcs_OnCheckedChanged" AutoPostBack="true" runat="server" />
                                    </HeaderTemplate>
                                    <ItemStyle Width="4px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidIdItemOrc" Value='<%# Eval("ID_ATEND_ORCAM") %>' />
                                        <asp:CheckBox ID="chkSelectProc" Checked="true" runat="server" Width="100%" style="margin: 0 0 0 -15px !important;" OnCheckedChanged="AtualizarDadosOrcamento" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CO_PROC_MEDI" HeaderText="CÓDIGO">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NM_PROC_MEDI" HeaderText="PROCEDIMENTO">
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QT_PROC" HeaderText="QTD">
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VL_PROC" HeaderText="VALOR">
                                    <ItemStyle HorizontalAlign="Right" Width="20px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="DESCTO">
                                    <ItemStyle Width="45px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtVlDescProcedOrc" Text='<%# Eval("VL_DSCTO_PROC") %>' CssClass="campoDin" OnTextChanged="txtVlDescProcedOrc_OnTextChanged" AutoPostBack="true" Width="100%" Style="margin-left: -4px; margin-bottom:0px; text-align: right;"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TOTAL">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidVlProcOrc" Value='<%# Eval("VL_PROC") %>' />
                                        <asp:Label runat="server" ID="lblValorTotal" Text='<%# Eval("TOTAL") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li style="clear: both; margin: 3px 0 0 0;">
                    <asp:TextBox runat="server" ID="txtObsOrcam" Enabled="false" TextMode="MultiLine" Style="width: 483px;
                        height: 15px;" Font-Size="13px"></asp:TextBox>
                </li>
            </ul>
        </li>
        <li style="width:990px; margin:0 0 3px 10px; background-color: #8FBC8F;
            height: 17px;">
            <div style="margin-left: 5px; margin-top: 2px;">
                <asp:Label Style="font-family: Tahoma; font-weight: bold; color: #FFF">DETALHAMENTO E FINALIZAÇÃO DO FATURAMENTO</asp:Label>
            </div>
        </li>
        <li style="clear:both;">
            <ul style="float: left; border-right:2px solid #e8e8e8; width: 300px; margin-left: 0px;">
                <li>
                    <label title="Valor Total dos Procedimentos do Orçamento" style="margin-right:5px !important;">R$ Procedimentos</label>
                    <asp:TextBox ID="txtVlTotalProcs" CssClass="campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total dos Procedimentos do Orçamento" Enabled="false"></asp:TextBox>
                </li>  
                <li>
                    <label title="Valor Total do Desconto dos Procedimentos" style="margin-right:5px !important;">R$ Descto Proced</label>          
                    <asp:TextBox ID="txtVlDescProcs" CssClass="campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total do Desconto dos Procedimentos" Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="Valor Total Total Líquido dos Procedimentos" style="margin-right:5px !important;">R$ Líquido Proced</label>          
                    <asp:TextBox ID="txtVlLiqdProcs" CssClass="campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total Total Líquido dos Procedimentos" Enabled="false"></asp:TextBox>
                </li>
                <li style="clear:both;">
                    <asp:CheckBox ID="chkNumParcelas" CssClass="chk" runat="server" Text="Alterar o nº de parcelas?" ToolTip="Marque se deverá alterar o número de parcelas." ClientIDMode="Static" />
                    <asp:TextBox ID="txtNumParcelas" Text="6" CssClass="nuQtde" style="text-align: right;margin-left: 4px;" runat="server" Width="20px" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li style="clear:both;">
                    <asp:CheckBox ID="chkAlterPrimParcela" CssClass="chk" runat="server" Text="Alterar data/valor 1ª parcela?" ToolTip="Marque se deverá informar a data/valor da primeira parcela." OnCheckedChanged="MontaGridNegociacao" AutoPostBack="true" ClientIDMode="Static" />
                </li>
                <li style="clear:both; margin-left:22px;">
                    <asp:TextBox ID="txtDtPrimParcela" ToolTip="Informa a data de pagamento da primeira parcela." CssClass="campoData" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                    <span> / R$</span>
                    <asp:TextBox ID="txtValorPrimParce" CssClass="campoDin" Width="50px" style="text-align: right;" ToolTip="Informe o valor da primeira parcela" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li style="clear:both;">
                    <asp:CheckBox ID="chkTaxaContrato" runat="server" CssClass="chk" ToolTip="Marque se o sistema deverá calcular o valor do contrato." Text="Aplicar Taxa Contratual?" ClientIDMode="Static" />
                    <asp:TextBox ID="txtTaxaContrato" CssClass="campoDin" ToolTip="Aplicar Taxa Contratual?" Width="50px" style="text-align: right;" runat="server" Enabled="false" ClientIDMode="Static" />
                </li>
                <li>
                    <asp:CheckBox ID="chkDiaParcela" CssClass="chk" runat="server" Text="Melhor dia de vencimento da parcela" ToolTip="Marque se deverá alterar o dia de vencimento da(s) parcela(s)." ClientIDMode="Static" />
                    <asp:TextBox ID="txtDiaParcela" Text="5" CssClass="nuQtde" ToolTip="Marque se deverá alterar o dia de vencimento da(s) parcela(s)." Width="15px" style="text-align: right;" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li>
                    <asp:CheckBox ID="chkBoleto" runat="server" CssClass="chk" Text="Emite Boleto Bancário?" ClientIDMode="Static" />
                    <asp:DropDownList ID="drpBoleto" runat="server" Width="150px" Enabled="false" ClientIDMode="Static" />
                </li>
                <li style="margin-top:5px; margin-bottom:2px; clear:both;">
                    <label title="Desconto Especial de Contrato" style="color: Red;">
                        Desconto Especial</label>
                </li>
                <li style="clear:both;">
                    <label title="Tipo de desconto do orçameto">
                        Tipo Desconto</label>
                    <asp:DropDownList ID="drpTipoDesctoOrcam" ToolTip="Selecione o Tipo de Desconto do Orçamento" Width="65px" OnSelectedIndexChanged="MontaGridNegociacao" AutoPostBack="true" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="Nenhum" Value="" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Total" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Mensal" Value="M"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li style="margin-left: 10px;">
                    <label title="Quantidade de meses de desconto do orçamento">
                        Qt Meses</label>
                    <asp:TextBox ID="txtQtdeMesesDesctoOrcam" CssClass="nuQtde" ToolTip="Informa a quantidade de meses de desconto do orçamento" Width="40px" style="text-align: right;" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li style="margin-left: 5px;">
                    <label style="margin-left: 15px;" title="R$ Desconto">
                        R$ Desconto</label>
                    <asp:CheckBox ID="chkTipoDesctoOrcam" CssClass="chk" runat="server" Text="%" ToolTip="Marque para alterar o valor do orçamento de R$ para %" Enabled="false" ClientIDMode="Static" />
                    <asp:TextBox ID="txtDesctoOrcam" Width="45px" CssClass="campoDin" style="text-align: right;" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li>
                    <label title="Parcela de início do desconto">
                        PID</label>
                    <asp:TextBox ID="txtMesIniDesconto" Width="20px" CssClass="nuQtde" ToolTip="Parcela de início do desconto" style="text-align: right;" runat="server" OnTextChanged="MontaGridNegociacao" AutoPostBack="true" Enabled="false" ClientIDMode="Static" />
                </li>
                <li style="margin-bottom:5px; clear:both;">
                    <asp:CheckBox ID="chkAtualizarFinanceiro" Visible="false" Checked="true" runat="server" CssClass="chk" Text="Atualizar Financeiro" />
                </li>
            </ul>
        </li>
        <li>
            <ul>
                <li style="width: 550px;">
                    <div id="divMensaAluno" runat="server" style="height: 244px; border: 1px solid #CCCCCC; overflow-y: auto; margin-top: 0px; margin-left:-10px;">
                    <asp:GridView runat="server" ID="grdNegociacao" CssClass="grdBusca" ToolTip="Grid demonstrativa das mensalidades do aluno." AutoGenerateColumns="False" Width="100%">
                        <RowStyle CssClass="rowStyle" />
                        <HeaderStyle CssClass="th" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <Columns>
                            <asp:BoundField DataField="numDoc" HeaderText="Nº Docto">
                            </asp:BoundField>
                            <asp:BoundField DataField="numParcela" DataFormatString="{0:D2}" HeaderText="Nº Par">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dtVencimento" HeaderText="Dt Vencto" DataFormatString="{0:d}">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlParcela" DataFormatString="{0:N2}" HeaderText="R$ Mensal">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlDesconto" DataFormatString="{0:N2}" HeaderText="R$ Descto">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlLiquido" DataFormatString="{0:N2}" HeaderText="R$ Liquido">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlMulta" DataFormatString="{0:N2}" HeaderText="% Multa">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vlJuros" DataFormatString="{0:N3}" HeaderText="% Juros">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>                        
                    </asp:GridView>
                    </div>
                </li>
                <li>
                    <ul>
                        <li style="clear:both;">
                            <label title="Valor Total do Orçamento" style="margin-right:5px !important;">R$ Orçamento</label>
                            <asp:TextBox ID="txtVlTotal" CssClass=" campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total do Orçamento" Enabled="false"></asp:TextBox>
                        </li>  
                        <li style="clear:both;">
                            <label title="Valor Total do Desconto no Orçamento" style="margin-right:5px !important;">R$ Desconto</label>
                            <asp:TextBox ID="txtVlDescto" CssClass=" campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total do Desconto no Orçamento" Enabled="false"></asp:TextBox>
                        </li>
                        <li style="clear:both;">
                            <label title="Valor Total Total Líquido do Orçamento" style="margin-right:5px !important;">R$ Líquido</label>
                            <asp:TextBox ID="txtVlLiqui" CssClass=" campoMoeda" style="width: 75px;" runat="server" ToolTip="Valor Total Total Líquido do Orçamento" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="liBtnFinAten" style="clear:both; height: 15px; width: 70px; margin-top: 5px; margin-bottom:20px; background-color:#5858FA;">
                            <asp:LinkButton ID="lnkbFinalizar" runat="server" OnClick="lnkbFinalizar_OnClick" >
                                <asp:Label runat="server" ID="Label18" Text="FINALIZAR" Font-Bold="true" Style="margin-left: 6px; margin-right: 5px; color:White;"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="lilnkRecMatric" runat="server" title="Clique para Imprimir Contrato de Matrícula" class="lilnkRelat">
                            <asp:LinkButton ID="lnkContrato" OnClick="lnkContrato_Click" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Recibo/Protocolo de Matrícula">
                                <img id="imgContrato" runat="server" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="RECIBO MATRIC" width="15" height="15" />
                                <asp:Label runat="server" ID="lblRecibo" Text="CONTRATO" style="margin-left: 4px;"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="lilnkBolCarne" runat="server" title="Clique para Imprimir Boleto de Mensalidades" class="lilnkRelat">
                            <asp:LinkButton ID="lnkBolCarne" OnClick="lnkBoleto_Click" runat="server" Style="margin: 0 auto;" ToolTip="Imprimir Boleto de Mensalidades">
                                <img id="imgBolCarne" runat="server" src='/Library/IMG/Gestor_IcoImpres.ico' alt="Icone Pesquisa" title="BOLETO/CARNÊ" width="15" height="15" />
                                <asp:Label runat="server" ID="lblBoleto" Text="BOLETO" style="margin-left: 4px;"></asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>
    <div id="divContrato" style="display: none;">
        <ul class="ulDados" style="width: 240px; height: 80px !important; margin-top: 0px !important">
            <li>
                Selecione
            </li>
            <li style="clear:both;">
                <label title="Tipo de Contrato" class="lblObrigatorio">Tipo de Contrato</label>
                <asp:DropDownList ID="drpTipContrato" runat="server" Width="215px" />
                <asp:RequiredFieldValidator ValidationGroup="tpContrato" runat="server" ID="rfvTipContrato" CssClass="validatorField"
                ErrorMessage="O Tipo de Contrato é obrigatório" ControlToValidate="drpTipContrato" Display="Dynamic" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 100px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbImprimirContrato" ValidationGroup="tpContrato" runat="server" OnClick="lnkbImprimirContrato_Click" ToolTip="Imprimir Contrato">
                    <asp:Label runat="server" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divBoleto" style="display: none;">
        <ul class="ulDados" style="width: 240px; height: 80px !important; margin-top: 0px !important">
            <li>
                Selecione
            </li>
            <li style="clear:both;">
                <label title="Boleto" class="lblObrigatorio">Boleto</label>
                <asp:DropDownList ID="drpBoletoPopUp" runat="server" Width="215px" />
                <asp:RequiredFieldValidator ValidationGroup="tpBoleto" runat="server" ID="rfvBoletoPopUp" CssClass="validatorField"
                ErrorMessage="O Boleto é obrigatório" ControlToValidate="drpBoletoPopUp" Display="Dynamic" />
            </li>
            <li class="liBtnAddA" style="clear: none !important; margin-left: 100px !important; margin-top: 8px !important; height: 15px;">
                <asp:LinkButton ID="lnkbBoletoPopUp" ValidationGroup="tpBoleto" runat="server" OnClick="lnkBoleto_Click" ToolTip="Imprimir Boleto(s)">
                    <asp:Label runat="server" Text="EMITIR" Style="margin-left: 4px;"></asp:Label>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div id="divIndicacao" style="display: none;">
        <div style="width: 570px; height: 200px; border: 1px solid #CCC; overflow-y: scroll">
            <asp:GridView ID="grdIndicacao" CssClass="grdBusca" runat="server" Style="width: 100%;
                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                ShowHeaderWhenEmpty="true">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Nenhum Orçamento associado<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CO_PROC_MEDI" HeaderText="CÓDIGO">
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NM_PROC_MEDI" HeaderText="PROCEDIMENTO">
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:DropDownList ID="drpIndicadorUnico" OnSelectedIndexChanged="drpIndicadorUnico_OnSelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%" ClientIDMode="Static" />
                        </HeaderTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hidIndicador" />
                            <asp:DropDownList ID="drpIndicador" runat="server" Width="100%" style="margin: 0 0 0 -5px !important;" ClientIDMode="Static" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="liBtnAddA" style="clear: none !important; margin-left: 250px !important; margin-top: 8px !important; height: 15px; width:43px;">
            <asp:LinkButton ID="lnkbSalvarIndic" runat="server" OnClick="lnkbSalvarIndic_Click" ToolTip="Salvar indicações atuais">
                <asp:Label runat="server" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
            </asp:LinkButton>
        </div>
    </div>
    <asp:HiddenField ID="hidFinalizado" runat="server" ClientIDMode="Static" Value="N" />
    <script type="text/javascript">

        function AbreModalContrato() {
            $('#divContrato').dialog({ autoopen: false, modal: true, width: 280, height: 120, resizable: false, title: "TIPO DE CONTRATO DO PACIENTE",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalBoleto() {
            $('#divBoleto').dialog({ autoopen: false, modal: true, width: 280, height: 120, resizable: false, title: "TIPO DE BOLETO",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalIndicacao() {
            $('#divIndicacao').dialog({ autoopen: false, modal: true, width: 600, height: 270, resizable: false, title: "REGISTRO DE INDICAÇÃO DE PROCEDIMENTOS",
                open: function (type, data) { $(this).parent().appendTo("form"); $(this).parents().find(".ui-dialog-titlebar-close").remove(); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        $(document).ready(function () {
            $("#divOcorrencia").show();

            $(".nuQtde").mask("?99");
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            if ($("#chkNumParcelas").attr("checked") && $("#hidFinalizado").val() == "N") {
                $("#txtNumParcelas").enable(true);
            }

            if ($("#chkAlterPrimParcela").attr("checked") && $("#hidFinalizado").val() == "N") {
                $("#txtDtPrimParcela").enable(true);
                $("#txtValorPrimParce").enable(true);
            }

            if ($("#chkTaxaContrato").attr("checked") && $("#hidFinalizado").val() == "N") {
                $("#txtTaxaContrato").enable(true);
            }

            if ($("#chkDiaParcela").attr("checked") && $("#hidFinalizado").val() == "N") {
                $("#txtDiaParcela").enable(true);
            }

            if ($("#chkBoleto").attr("checked") && $("#hidFinalizado").val() == "N") {
                $("#drpBoleto").enable(true);
            }

            TipoDescEsp();

            $("#chkNumParcelas").click(function () {
                if ($("#chkNumParcelas").attr("checked")) {
                    $("#txtNumParcelas").enable(true);
                }
                else {
                    $("#txtNumParcelas").enable(false);
                }
            });

            $("#chkAlterPrimParcela").click(function () {
                if ($("#chkAlterPrimParcela").attr("checked")) {
                    $("#txtDtPrimParcela").enable(true);
                    $("#txtValorPrimParce").enable(true);
                }
                else {
                    $("#txtDtPrimParcela").enable(false);
                    $("#txtValorPrimParce").enable(false);
                }
            });

            $("#chkTaxaContrato").click(function () {
                if ($("#chkTaxaContrato").attr("checked")) {
                    $("#txtTaxaContrato").enable(true);
                }
                else {
                    $("#txtTaxaContrato").enable(false);
                }
            });

            $("#chkDiaParcela").click(function () {
                if ($("#chkDiaParcela").attr("checked")) {
                    $("#txtDiaParcela").enable(true);
                }
                else {
                    $("#txtDiaParcela").enable(false);
                }
            });

            $("#chkBoleto").click(function () {
                if ($("#chkBoleto").attr("checked")) {
                    $("#drpBoleto").enable(true);
                }
                else {
                    $("#drpBoleto").enable(false);
                    $("#drpBoleto").val("");
                }
            });

            $("#drpTipoDesctoOrcam").change(function () {
                TipoDescEsp();
            });
        });

        function TipoDescEsp() {
            if ($("#hidFinalizado").val() == "S") {
                $("#txtQtdeMesesDesctoOrcam").enable(false);
                $("#chkTipoDesctoOrcam").enable(false);
                $("#txtDesctoOrcam").enable(false);
                $("#txtMesIniDesconto").enable(false);
            }
            else if ($("#drpTipoDesctoOrcam").val() == "") {
                $("#txtQtdeMesesDesctoOrcam").enable(false);
                $("#txtQtdeMesesDesctoOrcam").val("");
                $("#chkTipoDesctoOrcam").enable(false);
                $("#chkTipoDesctoOrcam").attr("checked", false);
                $("#txtDesctoOrcam").enable(false);
                $("#txtDesctoOrcam").val("");
                $("#txtMesIniDesconto").enable(false);
                $("#txtMesIniDesconto").val("");
            }
            else if ($("#drpTipoDesctoOrcam").val() == "T") {
                $("#txtQtdeMesesDesctoOrcam").enable(false);
                $("#txtQtdeMesesDesctoOrcam").val("");
                $("#chkTipoDesctoOrcam").enable(true);
                $("#txtDesctoOrcam").enable(true);
                $("#txtMesIniDesconto").enable(false);
                $("#txtMesIniDesconto").val("");
            }
            else if ($("#drpTipoDesctoOrcam").val() == "M") {
                $("#txtQtdeMesesDesctoOrcam").enable(true);
                $("#chkTipoDesctoOrcam").attr("checked", false);
                $("#chkTipoDesctoOrcam").enable(false);
                $("#txtDesctoOrcam").enable(true);
                $("#txtMesIniDesconto").enable(true);
            }
        }
    </script>
</asp:Content>
