<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3070_CtrlEmailSaude._3071_EnvioDeEmailSaude.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1000px;
        }
        .ulDados li
        {
            margin-top: 0px;
            margin-left: 0px;
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
            margin-left: -60px;
            height: 300px;
            width: 960px;
            border: 1px solid #CCCCCC;
            overflow-y: scroll;
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
        .lblEmissor
        {
            margin-left: -96px;
            padding-bottom: 1px;
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
            padding-top: 96px;
        }
        
        .contentCenter
        {
            padding-left: 74px;
        }
        
        .txtAssunto
        {
            width: 333px;
            margin-bottom: 0 !important;
            margin-left: -97px;
            height: 13px;
        }
        
        .txtEmissor
        {
            width: 243px;
            margin-bottom: 0 !important;
            margin-left: -96px;
            height: 13px;
        }
        
        .btnAnexo
        {
            margin-left: -97px;
            margin-top: 12px;
        }
        
        .btnEnviar
        {
            background-color: #C1FFC1;
            border: 1px solid #32CD32;
            border-radius: 0;
            font-size: 15px;
            text-decoration: none;
            margin-left: -692px;
            margin-top: -17px;
            position: absolute;
            padding: 2px 11px 2px;
            font: normal normal normal 10px Tahoma, Verdana, Arial;
        }
        
        .btnEnviar2
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            width: 78px;
            margin-left: 2px;
            margin-right: 0px;
            clear: both !important;
        }
        .ulBuscar
        {
            margin-left: -55px;
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
            width: 532px;
            height: 82px;
            background-color: #FAFAD2;
            margin-left: 1px;
        }
        .thNome
        {
            width: 200px;
            padding: 0;
            margin: 0;
            border: none;
        }
        .thFunc
        {
            width: 60px;
            padding: 0;
            margin: 0;
            border: none;
        }
        .thDepart
        {
            width: 120px;
            padding: 0;
            margin: 0;
            border: none;
        }
        .thTel
        {
            width: 60px;
            padding: 0;
            margin: 0;
            border: none;
        }
        .thEmail
        {
            width: 160px;
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
        
        .rblTipoEmail
        {
            margin-right: 56px;
            margin-left: 10px;
        }
        
        .rblTipoEmail.horizontal li
        {
            display: inline;
        }
        
        .rblTipoEmail label
        {
            display: inline;
        }
        
        #grdColaborador
        {
            position: absolute;
            width: 900px;
            z-index: 1;
        }
        
        #grdParceiro
        {
            position: absolute;
            width: 830px;
            z-index: 9999;
        }
        
        .grdParceiro
        {
            margin-top: -145px;
            position: absolute;
            z-index: 9999;
        }
        
        .grdColaborador
        {
            margin-top: 10px;
            position: absolute;
            margin-left: -215px;
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
                            <label style="float: left; font-size: 12px;">
                                Enviar para:
                            </label>
                            <asp:DropDownList ID="rblTipoEmail" OnSelectedIndexChanged="rblTipoEmail_SelectedChanged" ToolTip="Selecione para quem quer enviar o email" CssClass="rblTipoEmail" runat="server"
                                AutoPostBack="true">
                                <asp:ListItem Text="Selecionar" Value="" />
                                <asp:ListItem Text="Paciente" Value="P" />
                                <asp:ListItem Text="Responsável" Value="R" />
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="liTodos" visible="false" style="margin-left: 658px;">
                            <label for="chcSelecionarTodos" class="" title="Turma" style="float: left; font-size: 12px;">
                                Todos</label>
                            <asp:CheckBox runat="server" ID="chcSelecionarTodos" OnCheckedChanged="chcSelecionarTodos_OnCheckedChanged"
                                ToolTip="Selecionar todos os emails" AutoPostBack="True" />
                        </li>
                    </ul>
                    <li class="liGrid">
                        <div id="divGridC" runat="server" class="divGrid grdColaborador">
                            <asp:GridView ID="grdColaborador" runat="server" CssClass="grdBusca" Width="100%"
                                AutoGenerateColumns="False" GridLines="Vertical">
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado.<br />
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="CPF" HeaderText="CPF" HeaderStyle-CssClass="th">
                                        <ItemStyle Width="40px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nome" ControlStyle-CssClass="thNome">
                                        <ItemTemplate>
                                            <asp:Label ID="txtNomeCol"  CssClass="txtResponsavel" runat="server" Text='<%# bind("NOME") %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="hdCoCol" runat="server" Value='<%# bind("CO_COD") %>' />
                                            <asp:HiddenField ID="hdCoCol2" runat="server" Value='<%# bind("CO_COD_ALU") %>' />
                                            <asp:HiddenField ID="hdCoEmp" runat="server" Value='<%# bind("CO_EMP") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apelido" ControlStyle-CssClass="thFunc">
                                        <ItemTemplate>
                                            <asp:Label ID="txtApelido" CssClass="txtResponsavel" runat="server" Text='<%# bind("APELIDO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Situação" ControlStyle-CssClass="thDepart">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtSituacao" CssClass="txtResponsavel" runat="server" Text='<%# bind("SITUACAO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Telefone" ControlStyle-CssClass="thTel">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtTelefone" CssClass="txtResponsavel" runat="server" Text='<%# bind("TELEFONE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ControlStyle-CssClass="thEmail">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmail" CssClass="txtResponsavel" runat="server" Text='<%# bind("EMAIL") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <%--<HeaderTemplate>
                                            <asp:CheckBox runat="server" ID="chcSelecionarTodos" OnCheckedChanged="chcSelecionarTodos_OnCheckedChanged"
                                ToolTip="Selecionar todos os emails" AutoPostBack="True" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chcSelecionarEmail" runat="server" AutoPostBack="True" ToolTip="Selecione o Colaborador"
                                                Checked="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </li>                  
                </ul>
                <ul class="" style="position: absolute; margin-top: 339px;">
                    <div class="dvEmissor">
                        <li runat="server" id="liEmissor" class="liAssunto">
                            <label for="ddlEmissor" class="lblEmissor" title="Emissor">
                                Enviado por</label>
                            <%--<asp:TextBox ID="txtEmissor" runat="server" CssClass="txtEmissor" ToolTip="Usuário logado no sistema"
                                Enabled="false">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlEmissor" ToolTip="Selecione quem é o remetente do email" CssClass="txtEmissor" runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="liDtEnvio" class="liAssunto">
                            <label title="Dados da Matrícula" style="margin-left: 14px; margin-bottom: 1px;">
                                Data de Envio</label>
                            <asp:TextBox ID="txtDataDeEnvio" runat="server" CssClass="txtDataEnvio " Enabled="false"
                                ToolTip="Data de envio do email" Text=''>
                            </asp:TextBox>
                        </li>
                        <li runat="server" id="liAssunto" class="liAssuntoBaixo liAssunto">
                            <label for="txtAssunto" title="Assunto do email" style="margin-left: -96px; margin-bottom: 1px;">
                                Assunto</label>
                            <asp:TextBox ID="txtAssunto" runat="server" CssClass="txtAssunto" ToolTip="Assunto"
                                MaxLength="256">
                            </asp:TextBox>
                        </li>
                        <%--<li runat="server" id="liAnexo" class="liAnexo liAssunto">
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <label for="listBoxAnexo" title="Anexar arquivo">
                                        Anexos</label>
                                    <asp:FileUpload ID="FileUploadControl" runat="server" />
                                    <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click"/>
                                    <asp:ListBox ID="lstArquivos" runat="server" Height="30px" Width="312px" OnSelectedIndexChanged="lstArquivos_OnSelectedIndexChanged">
                                    </asp:ListBox>
                                    <asp:Button ID="Button1" runat="server" Text="Deletar" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnAdicionar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </li>--%>
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
                            <label for="txtMensagem" class="lblObrigatorio" title="Mensagem" style="margin-bottom: 1px;">
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
