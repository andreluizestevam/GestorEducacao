<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8260_Atendimento._8262_AtendimentoEvolucao.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1120px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="clear: both; margin-left: 3px;">
            <ul>
                <li class="liTituloGrid" style="width: 975px; height: 20px !important; margin-right: 0px;
                    background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 2px;
                    padding-top: 2px;">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px; color: #FFF">
                                PACIENTES COM AGENDAMENTO DE </label>
                        </li>
                        <li style="margin: 3px 0 0 10px;">
                            <asp:DropDownList runat="server" ID="ddlProficional" AutoPostBack="true"  Width="240px"  OnSelectedIndexChanged="ddlProficional_SelectedIndexChanged"></asp:DropDownList>
                        </li>
                        <li style="margin-left: 0px; float: right; margin-top: 2px;">
                            <ul class="ulPer">
                                <li>
                                    <asp:TextBox runat="server" ID="txtNomePacPesqAgendAtend" Width="150px" placeholder="Nome do Paciente"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="IniPeriAG" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <%--    <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                                        ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeriAG"></asp:RequiredFieldValidator><br />--%>
                                    <asp:Label runat="server" ID="Label4"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="FimPeriAG" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                    <%--Enabled='<%# Eval("podeClicar") %>'--%>
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="Label3">
                                                Situação
                                    </asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlSituacao" Width="90px">
                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Encaminhado" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="imgPesqAgendamentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imgPesqAgendamentos_OnClick" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="clear: both">
                    <div style="width: 583px; height: 95px; border: 1px solid #CCC; overflow-y: scroll"
                        id="divAgendasRecp">
                        <input type="hidden" id="divAgendasRecp_posicao" name="divAgendasRecp_posicao" />
                        <asp:HiddenField runat="server" ID="hidIdAgenda" />
                        <asp:GridView ID="grdPacientes" CssClass="grdBusca" runat="server" Style="width: 100%;
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
                                        <asp:HiddenField ID="hidIdAgenda" Value='<%# Eval("ID_AGEND_HORAR") %>' runat="server" />
                                        <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# ("CO_ALU") %>' />
                                        <asp:CheckBox ID="chkSelectPaciente" runat="server" OnCheckedChanged="chkSelectPaciente_OnCheckedChanged"
                                            AutoPostBack="true"  /> <%--Enabled='<%# Eval("podeClicar") %>'--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DTHR" HeaderText="HORÁRIO">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
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
                                <asp:BoundField DataField="IDADE" HeaderText="IDADE">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="INF">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgInfAgenda" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                            ToolTip="Apresenta informações do agendamento/recepção de avaliação" Style="width: 13px !important;
                                            height: 13px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ST">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgSituacao" ImageUrl='<%# Eval("imagem_URL") %>'
                                            ToolTip='<%# Eval("imagem_TIP") %>' Style="width: 18px !important; height: 18px !important;
                                            margin: 0 0 0 0 !important" OnClick="imgSituacao_OnClick" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li style="margin-left: 3px;">
                    <div style="width: 380px; height: 95px; border: 1px solid #CCC; overflow-y: scroll">
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                        <asp:GridView ID="grdItensAgend" CssClass="grdBusca" runat="server" Style="width: 100%;
                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                            ShowHeaderWhenEmpty="true">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma solicitação para esta recepção<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="NO_PROCED" HeaderText="PROCEDIMENTO">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="INF">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgInfAgenda" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                                            ToolTip="Apresenta informações do agendamento/recepção de avaliação" Style="width: 13px !important;
                                            height: 13px !important; margin: 0 0 0 0 !important" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NO_PRIORI" HeaderText="PRIORI">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
            <br />
        </li>
        <li style="clear: both">
            <ul style="float: left;">
                <li class="liTituloGrid" style="width: 976px; height: 20px !important; margin-right: 0px;
                    background-color: #c1ffc1; text-align: center; font-weight: bold; margin-bottom: 2px;
                    padding-top: 2px; margin-left: 3px">
                    <ul>
                        <li style="margin: 0px 0 0 10px; float: left">
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                DEMONSTRATIVO DE AGENDA</label>
                        </li>
                        <li style="margin-left: 18px; float: right; margin-top: 2px;">
                            <ul class="ulPer">                              
                                <li>
                                    <asp:TextBox runat="server" class="campoData" ID="txtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                                    <asp:TextBox runat="server" class="campoData" ID="txtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                                </li>
                                <li style="margin: 0px 2px 0 -2px;">
                                    <asp:ImageButton ID="ConsultaDemostrativo" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" onclick="ConsultaDemostrativo_Click" />
                                </li>
                            </ul>
                            <br />
                            <br />
                        </li>
                        <li style="clear: both; margin: -18px 0 0 0px;">
                            <div id="divDemonAge" style="width: 974px; height: 250px; border: 1px solid #CCC;
                                overflow-y: scroll">
                                <input type="hidden" id="divDemonAge_posicao" name="divDemonAge_posicao" />
                                <asp:HiddenField runat="server" ID="HiddenField2" />
                                <asp:GridView ID="grdHistoricoAgenda" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                    ShowHeaderWhenEmpty="true">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum agendamento para este paciente nos parâmetros acima<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="CK">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSelectHistAge" Enabled='<%# Eval("podeClicar") %>'
                                                    OnCheckedChanged="chkSelectHistAge_OnCheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ST">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgSituacaoHistorico" Style="width: 18px !important;
                                                    height: 18px !important; margin: 0 0 0 0 !important" OnClick="imgSituacaoHistorico_OnClick"
                                                    ImageUrl='<%# Eval("imagem_URL") %>' ToolTip='<%# Eval("imagem_TIP") %>' />
                                                <asp:HiddenField runat="server" ID="hidIdAgenda" Value='<%# Eval("ID_AGENDA") %>' />
                                                  <asp:HiddenField runat="server" ID="hidIdAtendi" Value='<%# Eval("ID_ATEND_AGEND") %>' />
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <img class="imgsa" style="width: 16px; height: 16px !important" src='<%# Eval("imagem_URL_ACAO") %>'
                                                    alt="Icone" title="Ação já realizada ou não" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="dtAgenda_V" HeaderText="AGENDA">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CLASS_FUNCI" HeaderText="CLAS FUNC">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROFISSIONAL" HeaderText="PROFISSIONAL">
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="AÇÃO PANEJADA">
                                            <ItemStyle HorizontalAlign="Left" Width="210px" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtAcaoPlan" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                    height: 23px !important; width: 98%" Text='<%# Eval("DE_ACAO") %>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AÇÃO REALIZADA">
                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtAcaoReali" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                    height: 23px !important; width: 300px" Text='<%# Eval("txtAcaoReali") %>' ReadOnly="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="clear: both; margin-left: 905px; margin-top: 250px">
            <ul>
                <li id="li1" runat="server" title="Clique para Adicionar as Questões da Avaliação"
                    class="liBtnAddA" style="width: 70px; margin-left: 0px !important; text-align: center">
                    <%--<asp:LinkButton ID="lnkFinalizar" runat="server" class="btnLabel" OnClick="lnkFinalizar_OnClick">FINALIZAR</asp:LinkButton>--%>
                    <asp:Button ID="btnFinalizar" class="btnLabel"  runat="server" Text="FINALIZAR" 
                        onclick="btnFinalizar_Click" />
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowLogAgenda" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <label>
                            Paciente</label>
                        <asp:TextBox runat="server" ID="txtNomePaciMODLOG" Enabled="false" Width="220px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Sexo</label>
                        <asp:TextBox runat="server" ID="txtSexoMODLOG" Enabled="false" Width="10px"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Idade</label>
                        <asp:TextBox runat="server" ID="txtIdadeMODLOG" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdLogAgendamento" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum Questionário associado<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Data_V" HeaderText="DATA">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_PROFI" HeaderText="RESPONSÁVEL">
                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO_TIPO" HeaderText="TIPO">
                                        <ItemStyle HorizontalAlign="Left" Width="65px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <img src='<%# Eval("CAMINHO_IMAGEM") %>' alt="" style="width: 16px !important; height: 16px !important;
                                                margin: 0 0 0 0 !important" title="Representação gráfica da Ação" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TIPO" HeaderText="AÇÃO">
                                        <ItemStyle HorizontalAlign="Left" Width="240px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowImagens" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr id="Tr1" runat="server">
                                <td>
                                    <label>
                                        Imagem:</label>
                                    <asp:FileUpload ID="uploadPhoto1" runat="server" CssClass="" /><br />
                                    <div id="Parent">
                                    </div>
                                    <label>
                                        &nbsp;
                                    </label>
                                    <input type="button" onclick="addLinhaImage(); return false;" value="More" />
                                    &nbsp;
                                    <asp:Button ID="btnAddImagem" Text="Adicionar Imagem" runat="server" />
                                    <asp:Button ID="btnCancel" Text="Cancelar" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </li>
                        <%-- <li id="li5" runat="server" title="Clique para adicionar mais imagens" class="liBtnAddA"
                        style="float: right; margin: -25px -2px 3px 5px; width: 60px">
                        <img alt="" class="imgAdd" title="Adicionar imagem" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <asp:LinkButton ID="lnkAddImg" runat="server" class="btnLabel" OnClick="lnkAddImg_OnClick">Adicionar</asp:LinkButton>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdAnexoImagens" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma imagem incluída<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="IMAGEM">
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:FileUpload runat="server" ID="fupImagens" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DE_TITULO" HeaderText="TÍTULO">
                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObser" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true" Text='<%# Eval("OBS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Questionário" OnClick="imgExc_OnClick" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>--%>
                </ul>
            </div>
        </li>
        <li>
            <div id="divLoadShowAudios" style="display: none; height: 340px !important;">
                <ul class="ulDadosLog">
                    <li id="liAddAudio" runat="server" title="Clique para adicionar mais áudios" class="liBtnAddA"
                        style="float: right; width: 70px; cursor: pointer" clientidmode="Static">
                        <img alt="" class="imgAdd" title="Adicionar áudio" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                            height="15px" width="15px" />
                        <label>
                            Adicionar</label>
                    </li>
                    <li style="clear: both; margin-left: -5px !important; margin-top: -2px;">
                        <div style="width: 890px; height: 305px; border: 1px solid #CCC; overflow-y: scroll">
                            <asp:GridView ID="grdAnexoAudio" CssClass="grdBusca" runat="server" Style="width: 100%;
                                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                ShowHeaderWhenEmpty="true" ClientIDMode="Static">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhuma imagem incluída<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="ARQUIVO">
                                        <ItemStyle Width="18px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:FileUpload runat="server" ID="fupAudios" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Título">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTituloAudio" Style="margin: 0 0 0 0 !important;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtObserAudio" TextMode="MultiLine" Style="margin: 0 0 0 0 !important;
                                                height: 23px !important; width: 180px" ReadOnly="true"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EX">
                                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                ToolTip="Excluir Questionário" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">

        window.onload = function () {
            MaintainScroll1();
        }

        function MaintainScroll1() {
            var div = document.getElementById("divAgendasRecp");
            var div_position = document.getElementById("divAgendasRecp_posicao");
            var position = parseInt('<%= Request.Form["divAgendasRecp_posicao"] %>');
            if (isNaN(position)) {
                position = 0;
            }

            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

//        function MaintainScroll2() {
//            var div = document.getElementById("divDemonAge");
//            var div_position = document.getElementById("divDemonAge_posicao");
//            var position = parseInt('<%= Request.Form["divDemonAge_posicao"] %>');
//            if (isNaN(position)) {
//                position = 0;
//            }

//            div.scrollTop = position;
//            div.onscroll = function () {
//                div_position.value = div.scrollTop;
//            }
//        }

        $(document).ready(function () {

            $('#liAddAudio').click(function () {
                addLinhaAudio();
            });

            $('.lnkImagens').click(function () {
                $('#divLoadShowImagens').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "IMAGENS ANEXAS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            });

            $('.lnkAudios').click(function () {
                $('#divLoadShowAudios').dialog({ autoopen: false, modal: true, width: 430, height: 390, resizable: false, title: "ÁUDIOS ANEXOS",
                    open: function (type, data) { $(this).parent().appendTo("form"); },
                    close: function (type, data) { ($(this).parent().replaceWith("")); }
                });
            });
        });

        function AbreModalLog() {
            $('#divLoadShowLogAgenda').dialog({ autoopen: false, modal: true, width: 902, height: 340, resizable: false, title: "HISTÓRICO DO AGENDAMENTO DE ATENDIMENTO",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function AbreModalImagens() {
            $('#divLoadShowImagens').dialog({ autoopen: false, modal: true, width: 802, height: 340, resizable: false, title: "IMAGENS ANEXAS",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function addLinhaAudio() {
            var table = document.getElementById('grdAnexoAudio');
            var elemTr = document.createElement("tr");
            //            var rowCount = table.rows.length;
            //  var row = table.insertRow(0);

            var elemTD = document.createElement("td");
            var element1 = document.createElement("input");
            element1.setAttribute("type", "file");
            element1.setAttribute("ID", "fupAudios");
            element1.setAttribute("runat", "server");
            element1.setAttribute("name", "fupAudios");
            elemTD.appendChild(element1);
            elemTr.appendChild(elemTD);

            var elemTD2 = document.createElement("td");
            var element2 = document.createElement("input");
            element2.setAttribute("type", "text");
            element2.setAttribute("ID", "txtTituloAudio");
            element2.setAttribute("runat", "server");
            element2.setAttribute("name", "txtTituloAudio");
            elemTD2.appendChild(element2);
            elemTr.appendChild(elemTD2);

            var elemTD3 = document.createElement("td");
            var element3 = document.createElement("input");
            element3.setAttribute("type", "text");
            element3.setAttribute("ID", "txtObserAudio");
            element3.setAttribute("runat", "server");
            element3.setAttribute("name", "txtObserAudio");
            elemTD3.appendChild(element3);
            elemTr.appendChild(elemTD3);

            table.appendChild(elemTr);
        }

        function addLinhaImage() {
            var rownum = 1;
            var div = document.createElement("div");
            var divid = "dv" + rownum;
            div.setAttribute("ID", divid);
            rownum++;

            //label dinâmico
            var lbl = document.createElement("label");
            lbl.setAttribute("ID", "lbl" + rownum);
            lbl.setAttribute("class", "label1");
            lbl.innerHTML = "Imagem";
            rownum++;

            //FileUpload dinâmico
            var _upload = document.createElement("input");
            _upload.setAttribute("type", "file");
            _upload.setAttribute("ID", "upload" + rownum);
            _upload.setAttribute("runat", "server");
            _upload.setAttribute("name", "uploads" + rownum);
            rownum++;

            var hyp = document.createElement("a");
            hyp.setAttribute("style", "cursor:Pointer");
            hyp.setAttribute("onclick", "return RemoveDv('" + divid + "');");
            hyp.innerHTML = "Remover";
            rownum++;

            var br = document.createElement("br");
            var _pdiv = document.getElementById("Parent");
            div.appendChild(br);
            div.appendChild(lbl);
            div.appendChild(_upload);
            div.appendChild(hyp);
            _pdiv.appendChild(div);
        }

        function RemoveDv(obj) {
            var p = document.getElementById("Parent");
            var chld = document.getElementById(obj);
            p.removeChild(chld);
        }

    </script>
</asp:Content>
