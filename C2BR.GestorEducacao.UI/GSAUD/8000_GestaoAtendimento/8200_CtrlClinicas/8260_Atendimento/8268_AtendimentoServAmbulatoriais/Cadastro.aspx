<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits=" C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8268_AtendimentoServAmbulatoriais.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin-top: 20px !important;
            margin-left: 50px !important;
        }
        
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        
        .ulDadosLog li
        {
            float: left;
            margin-left: 10px;
        }
        
        .ulPer label
        {
            text-align: left;
        }
        
        label
        {
            margin-bottom: 1px;
        }
        
        input
        {
            height: 13px !important;
        }
        
        .ulDadosGerais li
        {
            margin-left: 5px;
        }
        
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
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
        
        .chk label
        {
            display: inline;
            margin-left: -6px;
        }
        
        .liBtnConfirm
        {
            margin-top: 10px;
            margin-left: 2px;
            padding: 4px 3px 3px;
            background-color: #EE9572;
            border: 1px solid #8B8989;
        }
        
        #divCronometro
        {
            text-align: center;
            background-color: #FFE1E1;
            float: left;
            margin-left: 13px;
            margin-top: -48px;
            width: 115px;
            margin-right: -130px;
            display: none;
        }
        
        .LabelHora
        {
            margin-top: 4px;
            font-size: 10px;
        }
        
        .Hora
        {
            font-family: Trebuchet MS;
            font-size: 23px;
            color: #9C3535;
            margin-top: -3px;
        }
        
        .clear
        {
            clear: both;
        }
        
        .drp
        {
            width: 300px;
        }
        
        .liTopo
        {
            /*margin-left: 36px !important;*/
        }
        
        .libtnPesq
        {
            margin-top: 14px;
        }
        
        .divRisco
        {
            border: solid 1px #CCCCCC;
            width: 45px;
            height: 10px;
        }
        
        .lblTitulo
        {
            width: 890px;
            text-align: center;
            background-color: rgba(117, 171, 255, 0.56);
        }
        
        input[type='text']
        {
            margin-bottom: 0px;
        }
        
        .btnRelatorio
        {
            display: inline-block;
            height: 15px;
            width: 62px;
            margin-top: 12px;
            border: 1px solid #BBBBBB;
            text-align: center;
            background-color: rgb(177, 208, 255);
            color: black !important;
        }
        .noText label
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liTopo" id="">
            <ul>
                <li>
                    <label class="clear">
                        Período:</label>
                    <asp:TextBox runat="server" class="campoData" ID="IniPeriAG" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                    <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                    <asp:TextBox runat="server" class="campoData" ID="FimPeriAG" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                </li>
                <li>
                    <label class="clear">
                        Paciente:</label>
                    <asp:DropDownList CssClass="drp" runat="server" ID="drpPacientes" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <label class="clear">
                        Profissional:</label>
                    <asp:DropDownList CssClass="drp" runat="server" ID="drpProfissional" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li class="libtnPesq">
                    <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        Width="13px" Height="13px" OnClick="imgPesqAgendamentos_Click" />
                </li>
                <li class="libtnRel">
                    <asp:LinkButton ID="lnkBtnRelatorio" CssClass="btnRelatorio" runat="server" Text="Relatório"
                        Font-Bold="true" OnClick="lnkBtnRelatorio_Click" />
                </li>
            </ul>
        </li>
        <li style="clear: both">
            <label class="lblTitulo">
                <strong>GRADE DE PACIENTES COM SERVIÇOS AMBULATORIAIS SOLICITADOS</strong></label>
            <div style="width: 890px; height: 243px; border: 1px solid #CCC; overflow-y: scroll"
                id="divAgendasRecp">
                <asp:HiddenField ID="hidServ" runat="server" />
                <asp:GridView ID="grdAgendamentosPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhuma solicitação em Aberto<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="CK">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hidIdServAmbulatorial" Value='<%# Eval("idServAmbulatorial") %>'
                                    runat="server" />
                                <asp:HiddenField ID="hidIdAtenAgend" Value='<%# Eval("idAgendaAtendimento") %>' runat="server" />
                                <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("coPaciente") %>' runat="server" />
                                <asp:CheckBox ID="chkSelectPaciente" runat="server" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                    AutoPostBack="true" Text='<%# Eval("idServAmbulatorial") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PACIENTE">
                            <ItemStyle Width="525px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNomPaci" Text='<%# Eval("nomePaciente") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SX">
                            <ItemStyle Width="15px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSexoPaci" Text='<%# Eval("sexoPaciente") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IDADE">
                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblIdadePaci" Text='<%# Eval("idadePaciente") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DATA ATENDIMENTO">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="dataCadastro" Text='<%# Eval("dataCadastro") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROFISSIONAL">
                            <ItemStyle Width="300px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="nomeProfissional" Text='<%# Eval("nomeProfissional") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ESPECIALIDADE">
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEspec" Text='<%# Eval("especProfissional") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOCAL ATEND.">
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocal" Text='<%# Eval("Local") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº CONSULTA">
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="hidResgAtendimento" Text='<%# Eval("numRegistroConsulta") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CL. RISCO">
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hidClassRisco" Value='<%# Eval("classRisco") %>' runat="server" />
                                <asp:TextBox runat="server" ID="classRiscCorSelec" class="divRisco">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
        <li runat="server" id="liServAmbulatoriais" visible="false">
            <label class="lblTitulo" style="background-color: rgba(0, 255, 31, 0.31);">
                <strong>SERVIÇOS AMBULATORIAIS</strong></label>
            <div style="width: 890px; height: 127px; border: 1px solid #CCC; overflow-y: scroll">
                <asp:GridView ID="grdItensServAmbulatorias" CssClass="grdBusca" runat="server" Style="width: 100%;
                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                    ShowHeaderWhenEmpty="true">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhuma solicitação para esta recepção<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblOKHeader" ToolTip="Indica se o serviço ambulatorial foi aplicado."
                                    Font-Bold="true">OK</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hidId" Value='<%# Eval("idItem") %>' runat="server" />
                                <asp:HiddenField ID="hidIdItem" Value='<%# Eval("idItemServAmbulatorial") %>' runat="server" />
                                <asp:CheckBox ID="chkSelectServAmbulatorial" runat="server" OnCheckedChanged="chkSelectServAmbulatorial_OnCheckedChanged"
                                    Checked='<%# Bind("Efetuado") %>' Enabled='<%# Bind("DisableOk") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblTPHeader" ToolTip="Indica tipo do serviço ambulatorial."
                                    Font-Bold="true">TP</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="15px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTipo" Text='<%# Eval("tipoItemV") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblServicoCompleHeader" ToolTip="Nome e/ou complemento do serviço ambulatorial."
                                    Font-Bold="true">SERVIÇO/COMPLEMENTO</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="247px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="nomeItem" Text='<%# Eval("nomeItemV") %>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblFarmaciaHeader" ToolTip="Data em que o item da farmácia foi entregue."
                                    Font-Bold="true">FARMÁCIA</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="66px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" class="campoData" ID="txtEntrega" ToolTip="Informe a data em que o pedido foi entregue."
                                    Text='<%# Eval("dataEntregaItem") %>' Enabled='<%# Bind("DisableDTEntrega") %>'></asp:TextBox><br />
                                <asp:TextBox runat="server" Width="27px" class="campoHora" ID="txtHoraEntrega" ToolTip="Informe a hora em que o pedido foi entregue."
                                    Text='<%# Eval("horaEntregaItem") %>' Enabled='<%# Bind("DisableDTEntrega") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblExecHeader" ToolTip="Data em que o serviço ambulatorial foi finalizado."
                                    Font-Bold="true">EXECUÇÃO</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label runat="server" Text="DATA/HORA"></asp:Label>
                                <asp:Label Style="margin-left: 30px;" runat="server" Text="PROFISSIONAL/LOCAL"></asp:Label>
                                <br />
                                <asp:TextBox runat="server" class="campoData" ID="txtCadastro" ToolTip="Data, nome do Profissional e Local onde o serviço ambulatorial  foi finalizado."
                                    Text='<%# Eval("dataCadastro") %>'></asp:TextBox>
                                <asp:DropDownList Style="margin-left: 10px;" Width="128px" ID="ddlProfiAmbul" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox runat="server" Width="27px" class="campoHora" ID="txtHoraCadastro" ToolTip="Informe a hora em que o serviço foi finalizado."
                                    Text='<%# Eval("horaCadastro") %>'></asp:TextBox>
                                <asp:DropDownList Style="margin-left: 55px;" Width="128px" ID="ddlLocalAmbul" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblObsHeader" ToolTip="Observação sobre o atendimento deste serviço ambulatorial."
                                    Font-Bold="true">OBSERVAÇÃO</asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox runat="server" class="" onkeyDown="checkTextAreaMaxLength(this,event,'100');"
                                    ID="txtObsItem" Rows="4" Columns="20" ToolTip="Observação" MaxLength="100" TextMode="MultiLine"
                                    Text='<%# Eval("obsItem") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="lblSalvarHeader" ToolTip="Salvar as alterações no item."
                                    Font-Bold="true"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSalvarItem" runat="server" OnClick="lnkbtnSalvarItem_Click">
                        <img title="Salva o item atual."
                             src="/BarrasFerramentas/Icones/Gravar.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoHora").mask("99:99");
        })
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
               
    </script>
</asp:Content>
