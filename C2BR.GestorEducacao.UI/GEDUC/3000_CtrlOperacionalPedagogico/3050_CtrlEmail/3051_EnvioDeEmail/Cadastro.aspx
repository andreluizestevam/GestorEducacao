<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3050_CtrlEmail._3051_EnvioDeEmail.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1000px;
        }
        .ulDados li
        {
            margin-top: 10px;
            margin-left: 13px;
        }
        
        /*--> CSS LIs */
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 22px !important;
            padding: 2px 12px 1px;
        }
        .liGrid
        {
            height: 155px;
            width: 658px;
            margin-left: 175px;
        }
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .txtCidade
        {
            width: 106px;
        }
        .txtCNPJ
        {
            width: 100px;
        }
        .divGrid
        {
            height: 303px;
            width: 889px;
            border: 1px solid #CCCCCC;
            overflow-y: scroll;
            margin-left: -24px;
        }
        .divGridMatExt
        {
            height: 180px;
            width: 658px;
            border: 1px solid #CCCCCC;
            overflow-y: scroll;
        }
        .divGrid input[type="text"]
        {
            margin-bottom: 0 !important;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        .grdBusca th
        {
            white-space: normal;
        }
        .txtMedia
        {
            text-align: right;
            width: 30px;
        }
        .txtCargaHoraria
        {
            text-align: right;
            width: 30px;
        }
        .txtFaltas
        {
            text-align: right;
            width: 30px;
        }
        .txtTotalFaltas
        {
            text-align: right;
            width: 30px;
        }
        .txtTotalCargaHoraria
        {
            text-align: right;
            width: 30px;
        }
        .txtTotalDiasLetivos
        {
            text-align: right;
            width: 30px;
        }
        .txtInstituicao
        {
            width: 220px;
        }
        .lblDadosMatricula
        {
            font-weight: bold;
        }
        .ddlMateriaOutras
        {
            width: 175px;
        }
        .txtDataEnvio
        {
            width: 67px;
            text-align: center;
            margin-left: 15px;
            height: 13px;
        }
        
        .ulRegulaBaixo
        {
            margin-top: 35px;
            float: right;
        }
        
        .liAssuntoBaixo
        {
        }
        
        .liBtnEnviar
        {
            padding-top: 40px;
        }
        
        .contentCenter
        {
            padding-left: 74px;
        }
        
        .txtAssunto
        {
            width: 345px;
            margin-bottom: 0 !important;
            margin-left: -65px;
            height: 13px;
        }
        
        .lblEmissor
        {
            margin-left: -65px;
            padding-bottom: 1px;
            margin-top: -15px;
        }
        
        .txtEmissor
        {
            width: 243px;
            margin-bottom: 0 !important;
            margin-left: -65px;
            height: 13px;
        }
        
        .btnAnexo
        {
            margin-left: -65px;
            margin-top: -1px;
        }
        
        .btnEnviar
        {
            background-color: #C1FFC1;
            border: 1px solid #32CD32;
            border-radius: 0;
            font-size: 15px;
            text-decoration: none;
            margin-left: -719px;
            margin-top: 7px;
            position: absolute;
            padding: 2px 11px 2px;
            font: normal normal normal 10px Tahoma, Verdana, Arial;
        }
        .ulBuscar
        {
            margin-left: 142px;
        }
        .inputEmail
        {
            width: 200px;
        }
        .dvEmissor
        {
            float: left;
            width: 332px;
            margin-left: 40px;
        }
        .dvMensagem
        {
            float: right;
        }
        .mensagem
        {
            width: 450px;
            height: 75px;
            background-color: #FAFAD2;
            margin-left: 2px;
            margin-right: 96px;
        }
        .thResp
        {
            width: 200px;
            padding: 0;
            margin: 0;
            border: none;
        }
        .txtResponsavel
        {
            border: none;
            width: 198px;
            margin-bottom: 0 !important;
        }
        .liEmissor
        {
            margin-top: 0;
        }
        .liAssunto
        {
            margin-top: 0 !important;
        }
        table div
        {
            overflow: auto;
            height: 130px; 
            width: 830px;
        }
        #grdAluno
        {
            width: 830px;   
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul id="ulDados" class="ulDados contentCenter">
                <ul>
                    <ul class="ulBuscar">
                        <li class="liClear">
                            <label for="ddlAno" class="lblObrigatorio" title="Ano">
                                Ano</label>
                            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAno"
                                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                                Modalidade</label>
                            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlModalidade"
                                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                                Série/Curso</label>
                            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSerieCurso"
                                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                                Turma</label>
                            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
                        </li>
                        <li class="liPesqAtiv" style="margin-top: 20px;" runat="server" visible="false" id="liBtnPesquisar">
                            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                                <asp:Label runat="server" ID="lblPesquisar" Text=""></asp:Label>
                                <img title="Clique para gerar Gride de dos Alunos." alt="Icone de Pesquisa das Grides."
                                    src="/Library/IMG/Gestor_BtnPesquisa.png" />
                            </asp:LinkButton>
                        </li>
                        <li runat="server" id="liTodos" visible="false" style="margin-left: 153px;">
                            <label for="chcSelecionarTodos" class="" title="Turma">
                                Todos</label>
                            <asp:CheckBox runat="server" ID="chcSelecionarTodos" OnCheckedChanged="chcSelecionarTodos_OnCheckedChanged"
                                ToolTip="Selecionar todos os emails" AutoPostBack="True" />
                        </li>
                    </ul>
                    <li class="liGrid">
                        <div id="divGrid" runat="server" class="divGrid">
                            <asp:GridView ID="grdAluno" runat="server" CssClass="grdBusca" Width="100%" AutoGenerateColumns="False"
                                GridLines="Vertical">
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="DescNU_NIRE" HeaderText="Nire" HeaderStyle-CssClass="th">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Aluno" ControlStyle-CssClass="thResp">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNomeAlu" CssClass="txtResponsavel" runat="server" Text='<%# bind("NO_ALU") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hdCoAlu" runat="server" Value='<%# bind("CO_ALU") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Responsável" ControlStyle-CssClass="thResp">
                                        <ItemTemplate>
                                            <asp:Label ID="txtResponsavel" CssClass="txtResponsavel" runat="server" Text='<%# bind("NO_RESP") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hdCoRes" runat="server" Value='<%# bind("CO_RESP") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ControlStyle-CssClass="thResp">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmail" CssClass="inputEmail" runat="server" Text='<%# bind("EM_RESP") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chcSelecionarEmail" runat="server" AutoPostBack="True" ToolTip="Selecione o Aluno / Resposável"
                                                Checked="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
                <ul class="" style="padding-top: 368px">
                    <div class="dvEmissor">
                        <li runat="server" id="liEmissor" class="liAssunto">
                            <label for="ddlEmissor" class="lblEmissor" title="Emissor">
                                Enviado por</label>
                            <asp:DropDownList ID="ddlEmissor" ToolTip="Selecione quem é o remetente do email" CssClass="txtEmissor" runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="liDtEnvio" class="liAssunto">
                            <label title="Dados da Matrícula" style="margin-left: 14px; margin-bottom: 1px; margin-top: -15px;">
                                Data de Envio</label>
                            <asp:TextBox ID="txtDataDeEnvio" runat="server" CssClass="txtDataEnvio " Enabled="false"
                                ToolTip="Data de envio do email" Text=''>
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="liAssunto" class="liAssuntoBaixo liAssunto">
                            <label for="txtAssunto" title="Assunto do email" style="margin-left: -65px; margin-bottom: 1px; margin-top: -4px;">
                                Assunto</label>
                            <asp:TextBox ID="txtAssunto" runat="server" CssClass="txtAssunto" ToolTip="Assunto"
                                MaxLength="256">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="liAnexo" class="liAnexo">
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="btnAnexo"/>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnEnviarEmail" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </li>
                    </div>
                    <div class="dvMensagem" id="dvMensagem" runat="server">
                        <li runat="server" id="liMensagem" class="liAssunto">
                            <label for="txtMensagem" class="lblObrigatorio" title="Mensagem" style="margin-bottom: 1px; margin-top: -15px;">
                                Mensagem</label>
                            <asp:TextBox ID="txtMensagem" TextMode="multiline" Columns="102" Rows="3" runat="server"
                                ToolTip="Mensagem do corpo do email" CssClass="mensagem">
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="liBtnEnviar" class="liBtnEnviar">
                            <asp:LinkButton ID="btnEnviarEmail" CssClass="btnEnviar" runat="server" OnClick="btnEnviarEmail_Click"
                                ValidationGroup="" AutoPostBack="true">ENVIAR</asp:LinkButton>
                        </li>
                    </div>
                </ul>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(document).ready(function () {
           
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
        });      
    </script>
</asp:Content>
