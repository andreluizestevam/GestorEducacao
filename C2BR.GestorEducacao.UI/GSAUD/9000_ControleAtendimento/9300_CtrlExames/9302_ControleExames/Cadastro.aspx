<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9302_ControleExames.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=grdItensAvali.ClientID %>').Scrollable({
                ScrollHeight: 130,
                IsInUpdatePanel: false
            });
            $('#<%=grdAgendamentos.ClientID %>').Scrollable({
                ScrollHeight: 145,
                IsInUpdatePanel: true
            });
        });
    </script>
    <style type="text/css">
        .ulDados
        {
            width: 905px;
            margin-top: 45px !important;
        }
         input
        {
            height: 13px !important;
        }
        .ulDados li
        {
            margin-left: 10px;
            margin-top: -3px;
        }
        label
        {
            margin-bottom:1px;
        }
        .divPaciPreAtend
        {
            border: 1px solid #CCCCCC;
            width: 100%;
            height: 135px;
            /*overflow-y: scroll;*/
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -10px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liTituloGrid" style="width: 100%; height: 20px !important; margin-right: 0px;
            margin-top: -24px; background-color: #ffff99; text-align: center; font-weight: bold;
            margin-bottom: 5px; vertical-align: middle; margin-left: 0px;">
            <ul>
                <li style="margin-left: 362px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        GRADE DE EXAMES SOLICITADOS</label>
                </li>
                <li style="margin-left: -10px;">
                    <label style="margin-top: 7px; margin-bottom: -13px;">
                        Contratação
                    </label>
                    <asp:DropDownList runat="server" ID="ddlContratacao" OnSelectedIndexChanged="CarregaConsultasAgendadas" style="margin-left:210px;" AutoPostBack="true" Width="150px">
                    </asp:DropDownList>
                </li>
            </ul>
        </li>
        <div class="divPaciPreAtend" style="height:165px !important;">
            <asp:GridView ID="grdAgendamentos" CssClass="grdBusca" runat="server" Style="width: 100%;
                cursor: default;" AutoGenerateColumns="false" AllowPaging="false" 
                GridLines="Vertical">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Não existe nenhum exame solicitado para esta contratação<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkselectEn" runat="server"
                                AutoPostBack="true" OnCheckedChanged="chkselectEn_OnCheckedChanged"/>
                            <asp:HiddenField ID="hidProced" Value='<%# Eval("IdProced") %>' runat="server"/>
                            <asp:HiddenField ID="hidIdExameResul" Value='<%# Eval("IdExameResul") %>' runat="server"/>
                            <asp:HiddenField ID="hidCoAlu" Value='<%# Eval("CoAlu") %>' runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DataHora" HeaderText="DATA/HORA">
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Exame" HeaderText="DESCRIÇÃO/PROCEDIMENTO">
                        <ItemStyle Width="190px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NuRegistro" HeaderText="Nº REGISTRO">
                        <ItemStyle Width="70px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Tipo" HeaderText="TP">
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Paciente" HeaderText="PACIENTE">
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Contratante" HeaderText="CONTRATANTE">
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Solicitante" HeaderText="SOLICITANTE">
                        <ItemStyle Width="70px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CoCrm" HeaderText="ENTIDADE PROF">
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Situacao" HeaderText="SIT">
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </ul>
    <ul style="margin-left:50px; margin-top: 15px;">
        <div class="liTituloGrid" style="width: 907px; height: 20px !important; margin-right: 0px;
        background-color: #d2ffc2; text-align: center; font-weight: bold;
        margin-bottom: 15px; vertical-align: middle; margin-left: 0px;">
                <li>
                    <label style="font-family: Tahoma; font-weight: bold; style=margin-top: 7px; margin-bottom: -13px;">
                        REGISTRO DE RESULTADO DE ITENS DO EXAME
                    </label>
                </li>
        </div>
        <div class="divPaciPreAtend" style="height:150px !important; width:907px !important;">
            <asp:GridView ID="grdItensAvali" CssClass="grdBusca" runat="server" Style="width: 100%;
                cursor: default;" AutoGenerateColumns="false" AllowPaging="false"
                GridLines="Vertical">
                <RowStyle CssClass="rowStyle" />
                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                <EmptyDataTemplate>
                    Não existe nenhum item de avaliação para este exame<br />
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItem" runat="server"/>
                            <asp:HiddenField ID="hidTbs417" Value='<%# Eval("IdExameResulItem") %>' runat="server"/>
                            <asp:HiddenField ID="hidIdItensAvali" Value='<%# Eval("IdItensAvali") %>' runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GRUPO">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblGrupo" Text='<%# Eval("coGrupo") %>' ToolTip='<%# Eval("Grupo") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SUBGRUPO">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblSubgrupo" Text='<%# Eval("coSubgrupo") %>' ToolTip='<%# Eval("Subgrupo") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ItemAval" HeaderText="ITEM DE AVALIAÇÃO">
                        <ItemStyle HorizontalAlign="Left" Width="100px"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="RESULTADO">
                        <ItemTemplate>
                            <asp:TextBox DataField="Resultado" ID="txtResultado" runat="server" MaxLength="30" Width="70px" style="margin-bottom: 0px !important;">
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FUNC">
                        <ItemTemplate>
                            <asp:CheckBox id="flagRow" runat="server" OnCheckedChanged="flagRow_OnCheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RESP. TECNICO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRespTecnico" runat="server" DataField="RespTecnico" style="margin-bottom: 0px !important;" MaxLength="150" Width="130px">
                            </asp:TextBox>
                            <asp:DropDownList ID="ddlRespTecnico" runat="server" DataField="RespTecnico" Width="130px" OnSelectedIndexChanged="ddlRespTecnico_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ENTIDADE PROFISSIONAL" ItemStyle-Width="220px">
                        <ItemTemplate>
                            Sigla:
                            <asp:TextBox runat="server" Id="txtSigla" Width="30px" style="margin-bottom: 0px !important;" onkeyDown="checkTextAreaMaxLength(this,event,'15');">
                            </asp:TextBox>
                            Nº:
                            <asp:TextBox runat="server" Id="txtNumero" Width="40px" style="margin-bottom: 0px !important;" onkeyDown="checkTextAreaMaxLength(this,event,'15');">
                            </asp:TextBox>
                            UF:
                            <asp:DropDownList runat="server" Id="ddlUF" Width="40px" style="margin-bottom: 0px !important;">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TP RES">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:DropDownList ID="drpTpResult" Width="40px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DATA *" ItemStyle-Width="90px">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDtHr" runat="server" DataField="DataHora" class="campoData" style="margin-bottom: 0px !important;" Width="60px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDtHr" ID="rfvTxtDtHr" runat="server" ValidationGroup="Resultadoexame">
                            </asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <ul>
            <li style="float: left; margin-right: 15px; margin-top: -10px;">
                <label id="lblObser">
                    Observações:
                </label>
                <asp:TextBox runat="server" ID="txtObservacao" Width="380px" Height="35px" TextMode="MultiLine"
                onkeyDown="checkTextAreaMaxLength(this,event,'500');">
                </asp:TextBox>
            </li>
            <li style="margin-left: 5px; margin-top:15px; height:15px; cursor: pointer;">
                <font style="color: #436EEE; position: absolute; margin-top: -10px; margin-left: 50px;cursor: default;">ANEXOS DO EXAME</font>
                <asp:LinkButton ID="lnkbFotos" AccessKey="F" OnClick="lnkbAnexos_OnClick" ToolTip="Fotos associados ao Atendimento/Paciente" runat="server">
                    <img class="imgFotos" style="width: 18px; height: 18px !important; margin-left:10px; margin-top: 2px;" src="/Library/IMG/PGS_IC_Imagens.jpg" alt="Icone" />
                    <label style="margin: 3px 480px 0 5px; float: right">IMAGENS</label>
                </asp:LinkButton>
                <asp:LinkButton ID="lnkbVideos" AccessKey="V" OnClick="lnkbAnexos_OnClick" ToolTip="Videos associados ao Atendimento/Paciente" runat="server">
                    <img class="imgVideos" style="width: 18px; height: 18px !important; float: right; margin-right: 410px; margin-top: -18px;" src="/Library/IMG/PGS_IC_Imagens2.png" alt="Icone" />
                    <label style="margin: -15px 379px 0 5px; float: right">VIDEO</label>
                </asp:LinkButton>
            </li>
            <li style="margin-top: 8px; height:15px; cursor: pointer">
                <asp:LinkButton ID="lnkbAudios" AccessKey="U" OnClick="lnkbAnexos_OnClick" ToolTip="Áudios associados ao Atendimento/Paciente" runat="server">
                    <img class="imgAudios" style="width: 16px; height: 16px !important; margin-left:10px;" src="/Library/IMG/PGS_IC_ArquivoAudio.png" alt="Icone" />
                    <label style="margin: 5px 491px 0 5px; float: right">ÁUDIO</label>
                </asp:LinkButton>
                <asp:LinkButton ID="lnkbAnexos" AccessKey="A" OnClick="lnkbAnexos_OnClick" ToolTip="Anexos associados ao Atendimento/Paciente" runat="server">
                    <img class="imgAnexos" style="width: 16px; height: 16px !important; float: right; margin-right: 412px; margin-top: -18px;" src="/Library/IMG/PGS_IC_Anexo.png" alt="Icone" />
                    <label style="margin: -15px 370px 0 5px; float: right">ANEXOS</label>
                </asp:LinkButton>
            </li>
            <li style="margin-left: 610px; margin-top: -45px;">
                <font style="color: #436EEE;">RESPONSÁVEL PELO CADASTRO</font>
                <asp:CheckBox runat="server" ID="chkFuncCadas" OnCheckedChanged="chkFuncCadas_OnCheckedChanged" AutoPostBack="true" style="margin-left: 8px;"/>
                <font style="margin-left: -7px;">Responsável é funcionário?</font>
            </li>
            <li style="margin-left: 605px; margin-top: 10px;">
                <font style="position: absolute; margin-top: -10px; margin-left: 5px;">Nome</font>
                <asp:TextBox ID="txtFunciCadas" runat="server" style="margin-bottom: 0px !important; height: 14px !important; margin-left: 5px;" MaxLength="150" Width="150px">
                </asp:TextBox>
                <asp:DropDownList ID="ddlFunciCadas" runat="server" Width="160px" Height="16px">
                </asp:DropDownList>
                <font style="position: absolute; margin-top: -12px; margin-left: 15px;" class="lblObrigatorio">Data</font>
                <asp:TextBox ID="txtDtHrCadas" runat="server" class="campoData" style="margin-bottom: 0px !important; height: 14px !important; margin-left: 15px;" Width="55px">
                </asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtDtHrCadas" ID="rfvTxtDtHrCadas" runat="server" ValidationGroup="Resultadoexame">
                </asp:RequiredFieldValidator>
                <font style="position: absolute; margin-top: -12px; margin-left: 15px;" class="lblObrigatorio">Hora</font>
                <asp:TextBox ID="txtHora" runat="server" class="campoHora" style="margin-bottom: 0px !important; height: 14px !important; margin-left: 15px;" Width="28px" MaxLength="5">
                </asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtHora" ID="rfvTxtHora" runat="server" ValidationGroup="Resultadoexame">
                </asp:RequiredFieldValidator>
            </li>
        </ul>
    </ul>
    <div id="divAnexos" style="display: none; height: 310px !important;">
        <asp:HiddenField runat="server" ID="hidTpAnexo" />
        <ul class="ulDados" style="width: 530px; margin-left:-10px !important; margin-top: 0px !important">
            <li style="width:98%;">
                <label style="color:Blue">ANEXAR <asp:Label ID="lblTpAnexo" Text="ARQUIVO" runat="server" /></label>
                <hr />
            </li>
            <li style="clear:both; margin-top: 7px;">
                <label>Nome</label>
                <asp:TextBox ID="txtNomeAnexo" runat="server" Width="200px" ClientIDMode="Static" />
            </li>
            <li style="float:right; margin:20px 10px 0 0;">
                <asp:FileUpload ID="flupAnexo" runat="server" ClientIDMode="Static" />
            </li>
            <li style="clear:both; margin-top:-5px;">
                <label>Observações</label>
                <asp:TextBox ID="txtObservAnexo" TextMode="MultiLine" Width="320px" Rows="3" runat="server" />
            </li>
            <li class="liBtnAddA" style="margin:-20px 10px 0 0; float:right; width: 45px;">
                <asp:LinkButton ID="lnkbAnexar" runat="server" OnClick="lnkbAnexar_OnClick">
                    <asp:HiddenField id="hidIdExameAnexo" runat="server"/>
                    <asp:HiddenField id="hidIdCoAluAnexo" runat="server"/>
                    <label style="margin-left:5px;">ANEXAR</label>
                </asp:LinkButton>
            </li>
            <li class="liTituloGrid" style="width: 520px !important; height: 20px !important;
                background-color: #A9E2F3; margin-bottom: 2px; padding-top: 2px; clear:both; margin-top: 5px; margin-left: 5px;">
                <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;
                    float: left; margin-left: 10px;">
                    ARQUIVOS ANEXADOS</label>
            </li>
            <li style="clear: both; margin: 0 0 0 5px !important;">
                <div style="width: 518px; height: 130px; border: 1px solid #CCC; overflow-y: scroll">
                    <asp:GridView ID="grdAnexos" CssClass="grdBusca" runat="server" Style="width: 100%;
                        cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                        ShowHeaderWhenEmpty="true">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum arquivo associado<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="DT_CADAS" HeaderText="DATA">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NU_REGIS" HeaderText="RAP">
                                <ItemStyle Width="30px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NM_TITULO" HeaderText="NOME">
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="OBSERVAÇÕES">
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidIdAnexo" Value='<%# Eval("ID_ANEXO_ATEND") %>' />
                                    <asp:Label ID="Label2" Text='<%# Eval("DE_OBSER_RES") %>' ToolTip='<%# Eval("DE_OBSER") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbBxrAnexo" ImageUrl="/Library/IMG/Gestor_ServicosDownloadArquivos.png"
                                        ToolTip="Baixar Arquivo" OnClick="imgbBxrAnexo_OnClick" Width="16" Height="16" Style="margin: 0 0 0 -3px !important;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EX">
                                <ItemStyle Width="10px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgbExcAnexo" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                        ToolTip="Excluir Arquivo" OnClick="imgbExcAnexo_OnClick" Width="16" Height="16" Style="margin: 0 0 0 -3px !important;"
                                        OnClientClick="return confirm ('Tem certeza de que deseja excluir o arquivo?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        function AbreModalAnexos() {
            $('#divAnexos').dialog({ autoopen: false, modal: true, width: 530, height: 315, resizable: false, title: "ARQUIVOS ANEXADOS AO(S) ATENDIMENTO(S)",
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
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
        $(document).ready(function () {
            $(".campoHora").mask("99:99");
        });
    </script>
</asp:Content>
