<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3031_Chat.chat" %>

<%@ Register Src="~/Library/Componentes/ChatOnline.ascx" TagName="ChatOnline" TagPrefix="uc1" %>
<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function scrollMsg() {
            if ($(".divMensagens").scrollTop() == $(".divMensagens").height()) {
                $(".divMensagens").scrollTop($(".divMensagens").height());
            }
        }
    </script>
    <style>
        .ulDados 
        {
            width: 100%;
        }
        
        .ulDados li
        {
            margin-left: 5px;
            clear: none;
        }
        
        .liTitulo
        {
            margin-left: 0px !important;
            height: 22px;
            width: 100%;
        }
        
        .liTitulo .lblTitulo
        {
            text-align: center;
            font-size: 1.2em;
            margin-top: 3px;
        }
        
        .liSalas
        {
            margin-left: 30px !important;
            margin-top: -10px;
            border: 1px solid #ccc;
            width: 340px;
            height: 220px;
        }
        
        #divSalas
        {
            width: 340px;
            height: 170px;
            overflow-y: scroll;
        }
        
        .grdSala
        {
            width: 323px;
            height: 15px;
        }
        
        .liAreaPost 
        {
            border: 1px solid #ccc;
            margin-top: -10px;
            width: 560px;
            height: 220px;
        }
        
        #divAreaPost
        {
            width: 560px;
            height: 198px;
        }
        
        .liOutros
        {
            clear: both;
            margin-top: 10px;
            margin-left: 30px !important;
            border: 1px solid #ccc;
            width: 370px;
            height: 220px;
        }
        
        #divOutros
        {
            width: 370px;
            height: 195px;
            overflow-y: scroll;
            font-size: 12px; 
        }
        
        .liMsg
        {
            border: 1px solid #ccc;
            margin-top: 10px;
            margin-left: 10px !important;
            width: 560px;
            height: 220px;
        }
        
        #divMsg
        {
            width: 560px;
            height: 198px;
            overflow-y: scroll;
            font-size: 12px; 
        }
        
        .liBtn
        {
            background-color: #000;
            border: 1px solid #f0ffff;
            text-align: center;
            clear:none;
        }   

        #divMonitores
        {
            border: 1px solid #ccc;
            overflow-y: scroll;
            width: 245px;
            height: 69px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoSala" Value="0" />
    <asp:HiddenField runat="server" ID="hidNomSala" Value="" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidNomSalaN" Value="" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
<%-------------------------------------------------------------------------------------------------------------------------------
                    ÁREA DAS SALAS
-------------------------------------------------------------------------------------------------------------------------------%>
        <li class="liSalas">
            <ul>
                <li class="liTitulo" style="background-color: #f4a460;">
                    <label class="lblTitulo" style=" font-weight: bold; font-size: 12px; font-family: Tahoma; color: #fff;">MINHAS SALAS DE MONITORIA</label>
                </li>
                <li style="margin-left: 0px !important;">
                    <div id="divSalas">
                        <asp:GridView 
                            ID="grdSala" 
                            CssClass="grdBusca grdSala" 
                            runat="server" 
                            AutoGenerateColumns="False"
                            ShowHeader="true">

                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="CK" ItemStyle-Width="10px">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hidCoSalaG" Value='<%# Eval("CO_SALA") %>' />
                                        <asp:HiddenField runat="server" ID="hidNomSalaG" Value='<%# Eval("NO_SALA") %>' />
                                        <asp:CheckBox ID="chkSelSala" runat="server" OnCheckedChanged="lnkSelSala_Click" AutoPostBack="true" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="HORA" DataField="HR_INICIO">
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="SALA" ItemStyle-Width="65px">
                                    <ItemTemplate>
<%--                                        <asp:LinkButton ID="lnkSelSala" OnClick="lnkSelSala_Click" ValidationGroup="SelSala"
                                            runat="server" ToolTip="Clique para Selecionar a Sala">
--%>                                            <asp:Label ID="Label1" runat="server"><%# Eval("NO_SALA") %></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="TEMA" DataField="DE_TEMA">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li style="margin-left: 0px !important; height: 30px; width: 100%; border-top: 1px solid #ccc;">
                    <ul>
                        <li class="liBtn" style="margin-top: 6px; margin-left: 32px !important; background-color: #6495ed !important; width: 130px !important; height: 15px !important;">
                            <asp:LinkButton ID="LinkButton1" ValidationGroup="SelSala"
                                runat="server" ToolTip="Clique para Selecionar a Sala">
                                <asp:Label ID="Label6" runat="server" style="color: #fff; font-weight: bold; font-family: Arial;">INICIAR MONITORIA</asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li class="liBtn" style="margin-top: 6px; margin-left: 7px !important; background-color: #ff7f50 !important; width: 130px !important; height: 15px !important;">
                            <asp:LinkButton ID="LinkButton2" ValidationGroup="SelSala"
                                runat="server" ToolTip="Clique para Selecionar a Sala">
                                <asp:Label ID="Label7" runat="server" style="color: #fff; font-weight: bold; font-family: Arial;">FINALIZAR MONITORIA</asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
<%-------------------------------------------------------------------------------------------------------------------------------
                    FIM DA ÁREA DAS SALAS
-------------------------------------------------------------------------------------------------------------------------------%>








        










<%-------------------------------------------------------------------------------------------------------------------------------
                    SEPARADOR DA ÁREA DE SALAS E DA ÁREA DE POSTAGEM
-------------------------------------------------------------------------------------------------------------------------------%>
        <li class="liSeparador" style=" height: 220px; height: 40px; margin-top: 30px !important;">
            <asp:Image runat="server" ID="imgSeparador" Width="25px" ImageUrl="../../../../Library/IMG/Chat/EW_Chat_SetaIndicativa.png" />
        </li>
<%-------------------------------------------------------------------------------------------------------------------------------
                    FIM DO SEPARADOR DA ÁREA DE SALAS E DA ÁREA DE POSTAGEM
-------------------------------------------------------------------------------------------------------------------------------%>



















<%-------------------------------------------------------------------------------------------------------------------------------
                    ÁREA DE POSTAGEM
-------------------------------------------------------------------------------------------------------------------------------%>
        <li class="liAreaPost">
            <ul>
                <li class="liTitulo" style="margin-left: 0px !important; background-color: #9acd32; color: #ffffff;">
                    <label class="lblTitulo" style=" font-weight: bold; font-size: 12px; font-family: Tahoma;">ÁREA DE POSTAGEM</label>
                </li>
                <li style="margin-left: 0px !important;">
                    <div id="divAreaPost">
                        <ul>
                            <li style="width: 100% !important; height: 94px !important; border-bottom: 4px solid #add8e6; margin-left: 0px !important;">
                                <ul>
                                    <li style="margin-top: 5px !important;">
                                        <asp:Image ID="imgMonitor" runat="server" Width="63" Height="84" ImageUrl="../../../../Library/IMG/Gestor_SemImagem.png" />
                                    </li>
                                    <li style="width: 220px !important; margin-top: 5px !important;">
                                        <ul>
                                            <li>
                                                <asp:Label CssClass="lblLegCampo" ID="lblLegNome" style=" font-size: 10px !important; font-weight: bold;">MONITOR RESPONSÁVEL</asp:Label>
                                            </li>
                                            <li style="margin-top: 2px !important;">
                                                <asp:Label CssClass="lblValCampo" Text="" ID="lblValNome" runat="server" style="color: #468284; font-size: 11px;"></asp:Label>
                                            </li>
                                            <li style="margin-top: 2px !important; clear: both;">
                                                <asp:Label ID="lblTitCodigo" style="color: #778899; float: left; margin-right: 2px;">Código:</asp:Label><asp:Label CssClass="lblLegCampo" ID="lblValCodigo" runat="server"></asp:Label>
                                            </li>
                                            <li style="margin-top: 3px !important; clear: both;">
                                                <asp:Label ID="lblTitUsuario" style="color: #778899; float: left; margin-right: 2px;">Usuário:</asp:Label><asp:Label CssClass="lblLegCampo" ID="lblValUsuario" runat="server"></asp:Label>
                                            </li>
                                            <li style="margin-top: 3px !important; clear: both;">
                                                <asp:Label ID="lblTitTelefone" style="color: #778899; float: left; margin-right: 2px;">Telefone:</asp:Label><asp:Label CssClass="lblLegCampo" ID="lblValTelefone" runat="server"></asp:Label>
                                            </li>
                                            <li style="margin-top: 0px !important; clear: both;">
                                                <asp:Label ID="lblTitEmail" style="color: #778899; float: left; margin-right: 2px;">Email:</asp:Label><asp:Label CssClass="lblLegCampo" ID="lblValEmail" runat="server"></asp:Label>
                                            </li>
                                        </ul>
                                    </li>
                                    <li style="width: 247px !important; margin-top: 5px !important; margin-left: 0px !important;">
                                        <ul>
                                            <li style="margin-left: 5px !important;">
                                                <asp:Label CssClass="lblLegCampo" ID="Label5" style=" font-size: 10px !important; width: 100% !important; font-weight: bold;">MONITORIA DE APOIO</asp:Label>
                                            </li>
                                            <li style="clear: both;">
                                                <div id="divMonitores">
                                                    <asp:GridView 
                                                        ID="grdMonitAuxili" 
                                                        CssClass="grdBusca grdMonitAuxili" 
                                                        runat="server" 
                                                        AutoGenerateColumns="False"
                                                        ShowHeader="false"
                                                        Width="228px"
                                                        Height="15px">

                                                        <RowStyle CssClass="grdLinha" />
                                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                                        <EmptyDataTemplate>
                                                            Nenhum registro encontrado.<br />
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="monitor">
                                                                <ItemStyle Width="234px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>

                            <li style="width: 100% !important; height: 99px !important; margin-left: 0px !important;">
                                <asp:UpdatePanel runat="server" ID="updAreaPostagem" ClientIDMode="Static" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <ul>
                                            <li style="margin-top: 5px !important;">
                                                <asp:Image ID="imgAluno" runat="server" Width="63" Height="84" ImageUrl="../../../../Library/IMG/Gestor_SemImagem.png" />
                                            </li>
                                            <li>
                                                <ul>
                                                    <li style=" width: 467px !important;">
                                                        <label class="lblTitulo" style=" font-weight: bold; font-size: 12px; font-family: Tahoma; text-align: center; color: #9acd32;">ÁREA DE POSTAGEM DO ALUNO</Label>
                                                    </li>
                                                    <li style="clear: both; margin-top: 3px !important;">
                                                        <asp:DropDownList ID="ddlAluno" runat="server" Width="270px" Height="17px" ToolTip="Selecione o Aluno Participante">
                                                        </asp:DropDownList>
                                                    </li>
                                                    <li style="margin-top: 0px !important; margin-left: 30px !important;">
                                                        <asp:LinkButton ID="lnkBloqAluno" ValidationGroup="SelSala" ToolTip="Bloquear e Desbloquear o Envio de Mensagens do Aluno"
                                                            runat="server" style="float: right;">
                                                            <asp:Image runat="server" ID="imgBloqAluno" Width="22" Height="22" ImageUrl="../../../../Library/IMG/Chat/EW_Chat_MSG_Bloqueio.png" />
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li style="margin-top: -3px !important; margin-left: 5px !important;">
                                                        <asp:LinkButton ID="lnkDesBloqAluno" ValidationGroup="SelSala" ToolTip="Bloquear e Desbloquear a Visão de Mensagens do Aluno"
                                                            runat="server" style="float: right;">
                                                            <asp:Image runat="server" ID="Image2" Width="27" Height="27" ImageUrl="../../../../Library/IMG/Chat/EW_Chat_MSG_NaoVer.png" />
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li style="margin-top: 3px !important; margin-left: 0px !important;">
                                                        <asp:LinkButton ID="lnkVisuAluno" ValidationGroup="SelSala"
                                                            runat="server" style="float: right;">
                                                            <asp:Image runat="server" ID="Image3" Width="17" Height="17" ImageUrl="../../../../Library/IMG/Chat/EW_Chat_MSG_Anexo.png" />
                                                        </asp:LinkButton>
                                                    </li>
                                                    <li style="margin-top: 2px !important; margin-left: 4px !important;">
                                                        <asp:LinkButton ID="lnkNVisuAluno" ValidationGroup="SelSala"
                                                            runat="server" style="float: right;">
                                                            <asp:Image runat="server" ID="Image4" Width="22" Height="22" ImageUrl="../../../../Library/IMG/Chat/EW_Chat_Email.png" />
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li style="margin-left: 5px !important; margin-top: 4px;">
                                                <ul>
                                                    <li>
                                                        <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Columns="90" Rows="4" Height="40px">
                                                        </asp:TextBox>
                                                    </li>

                                                    <li class="liBtn" style="margin-top: 25px; margin-left: 0px !important; width: 42px !important; height: 15px !important;">
                                                        <asp:LinkButton ID="lnkSairSala" ValidationGroup="SelSala"
                                                            runat="server" ToolTip="Clique para Selecionar a Sala">
                                                            <asp:Label ID="Label1" runat="server" style="color: #fff; font-weight: bold; font-family: Arial;">ENVIAR</asp:Label>
                                                        </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        </li>
<%-------------------------------------------------------------------------------------------------------------------------------
                    FIM DA ÁREA DE POSTAGEM
-------------------------------------------------------------------------------------------------------------------------------%>



















<%-------------------------------------------------------------------------------------------------------------------------------
                    ÁREA DE OUTROS ALUNOS OU MONITORES
-------------------------------------------------------------------------------------------------------------------------------%>
        <li class="liOutros">
            <ul>
                <li class="liTitulo" style=" background-color: #ca5;">
                    <ul>
                        <li style="margin-left: 24px !important;">
                            <label class="lblTitulo" style="color: #fff; font-weight: bold;">POSTAGEM POR MONOTOR</label>
                        </li>
                        <li style="margin-top: 3px;">
                            <asp:DropDownList ID="ddlOutrosAlunos" runat="server" Width="150px">
                            </asp:DropDownList>
                        </li>
                    </ul>                    
                </li>
                <li style="margin-left: 0px !important;">
                    <div id="divOutros">
                        <asp:UpdatePanel runat="server" ID="updMsgOutros" ClientIDMode="Static" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView 
                                    ID="grdMsgOutros" 
                                    CssClass="grdBusca grdMsgOutros" 
                                    runat="server" 
                                    AutoGenerateColumns="False"
                                    ShowHeader="true"
                                    Width="353px"
                                    Height="15px">

                                    <RowStyle CssClass="grdLinha" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
<%--                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgFotoUsu" ImageUrl="../../../../Library/IMG/Gestor_Admin.png" Width="15" Height="15" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="DE_MSG" HeaderText="MSG">
                                            <ItemStyle Width="468px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--IMPLANTACAO0 XXXXXXXXX/XXXXXXXXX/XXXXXXXXX/XXXXXXX--%>
                    </div>
                </li>
            </ul>
        </li>
<%-------------------------------------------------------------------------------------------------------------------------------
                    FIM DA ÁREA DE OUTROS ALUNOS OU MONITORES
-------------------------------------------------------------------------------------------------------------------------------%>



















<%-------------------------------------------------------------------------------------------------------------------------------
                    ÁREA DE MENSAGENS
-------------------------------------------------------------------------------------------------------------------------------%>
        <li class="liMsg">
            <ul>
                <li class="liTitulo" style=" background-color: #90ee90;">
                    <label class="lblTitulo" style=" font-weight: bold; color: #2f4f4f">HISTÓRICO DE MENSAGENS DO ALUNO</label>
                </li>
                <li style="margin-left: 0px !important;">
                    <div id="divMsg">
                        <asp:UpdatePanel runat="server" ID="updMensagens" ClientIDMode="Static" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView 
                                    ID="grdMsg" 
                                    CssClass="grdBusca grdMsg" 
                                    runat="server" 
                                    AutoGenerateColumns="False"
                                    ShowHeader="true"
                                    Width="543px"
                                    Height="15px">

                                    <RowStyle CssClass="grdLinha" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum registro encontrado.<br />
                                    </EmptyDataTemplate>
                                    <Columns>
<%--                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgFotoUsu" ImageUrl="../../../../Library/IMG/Gestor_Admin.png" Width="15" Height="15" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="DATA" HeaderText="DATA">
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HORA" HeaderText="HORA">
                                            <ItemStyle Width="25px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_USU" HeaderText="USU">
                                            <ItemStyle Width="25px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DE_MSG" HeaderText="MSG">
                                            <ItemStyle Width="468px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </li>
            </ul>
        </li>
<%-------------------------------------------------------------------------------------------------------------------------------
                    FIM DA ÁREA DE MENSAGENS
-------------------------------------------------------------------------------------------------------------------------------%>
    </ul>

</asp:Content>