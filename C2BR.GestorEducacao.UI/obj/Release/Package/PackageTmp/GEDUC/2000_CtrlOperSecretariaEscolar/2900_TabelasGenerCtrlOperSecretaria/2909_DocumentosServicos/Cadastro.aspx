<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2909_DocumentosServicos.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function DeleteConfirm() {
            return confirm('Deseja realmente Exlcuir?');
        }
    
    </script>
    <style type="text/css">
        #tabela
        {
            width: 500px;
            border: none;
            margin: 0 auto;
        }
        #tabela tr td
        {
            width: 300px;
            vertical-align: top;
        }
        .colPagina
        {
            display: block;
            margin: 0 0;
            float: left;
        }
        .ulDados
        {
            width: 400px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liSituacao
        {
            margin-left: 5px;
        }
        .liData
        {
            margin-left: 55px;
        }
        
        /*--> CSS DADOS */
        .txtValor
        {
            width: 40px;
            text-align: right;
        }
        .txtNomeProcessoExterno
        {
            width: 220px;
        }
        .txtDescricao
        {
            width: 237px;
        }
        .divUpload
        {
            position: relative;
            display: inline;
            cursor: pointer;
        }
        .upDocumento
        {
            text-align: right;
            -moz-opacity: 0;
            filter: alpha(opacity: 0);
            opacity: 0;
            width: 11px;
        }
        .imgUpload
        {
            margin-left: -11px;
        }
        .liBlocoCtaContabil ul
        {
            width: 100%;
        }
        .liBlocoCtaContabil ul li
        {
            display: inline;
            margin-right: 0px;
            padding-top: 2px;
            height: 16px;
        }
        .liNomeContaContabil
        {
            margin-left: 0px;
            margin-right: 5px !important;
        }
        #ddlTipoDoc, txtSiglaDoc
        {
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <table id="tabela">
        <tr>
            <td>
                <ul id="ulDados" class="ulDados">
                    <li style="margin-left: -5px">
                        <label for="ddlTipoDoc" class="lblObrigatorio" style="margin-left: 5px">
                            Tipo de Documento</label>
                        <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="campoTipoDoc" Width="100px"
                            AutoPostBack="true" ClientIDMode="Static">
                            <asp:ListItem Value="">Selecione...</asp:ListItem>
                            <asp:ListItem Value="AT">Ata</asp:ListItem>
                            <asp:ListItem Value="CE">Certidão</asp:ListItem>
                            <asp:ListItem Value="CO">Contrato</asp:ListItem>
                            <asp:ListItem Value="CC">Certificado</asp:ListItem>
                            <asp:ListItem Value="DE">Declaração</asp:ListItem>
                            <asp:ListItem Value="DA">Documentos Administrativos</asp:ListItem>
                            <asp:ListItem Value="DT">Documentos Técnicos</asp:ListItem>
                            <asp:ListItem Value="HI">Histórico</asp:ListItem>
                            <asp:ListItem Value="PR">Renovação de Matrícula</asp:ListItem>
                            <asp:ListItem Value="RE">Recibo</asp:ListItem>
                            <asp:ListItem Value="OT">Outros</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ddlTipoDoc"
                            ErrorMessage="Descrição deve ter no máximo 100 caracteres" Text="*" ValidationExpression="^(.|\s){1,100}$"
                            SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoDoc"
                            ErrorMessage="Tipo deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtNome" class="lblObrigatorio">
                            Nome</label>
                        <asp:TextBox ID="txtNome" runat="server" CssClass="txtDescricao" MaxLength="40"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtNome"
                            ErrorMessage="Descrição deve ter no máximo 100 caracteres" Text="*" ValidationExpression="^(.|\s){1,100}$"
                            SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtNome"
                            ErrorMessage="Nome deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="txtSiglaDoc" class="lblObrigatorio">
                            Sigla</label>
                        <asp:TextBox ID="txtSiglaDoc" ClientIDMode="Static" runat="server" CssClass="campoSiglaDoc"
                            Width="90px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtSiglaDoc"
                            ErrorMessage="Descrição deve ter no máximo 100 caracteres" Text="*" ValidationExpression="^(.|\s){1,100}$"
                            SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSiglaDoc"
                            ErrorMessage="Sigla deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtTitulo" class="lblObrigatorio">
                            Titulo</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="txtDescricao" MaxLength="12"
                            Width="333px" TextMode="MultiLine" Rows="2" Columns="55"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTitulo"
                            ErrorMessage="Titulo deve ter no máximo 120 caracteres" Text="*" ValidationExpression="^(.|\s){1,100}$"
                            SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitulo"
                            ErrorMessage="Titulo deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
                    </li>
                    <li class="liClear">
                        <label for="txtDescricao" class="lblSerie">
                            Descrição</label>
                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="250"
                            Width="333px" TextMode="MultiLine" Rows="5" Columns="55"></asp:TextBox>
                    </li>
                    <li class="liClear">
                        <label for="txtDtSituacao" class="lblObrigatorio">
                            Data Cadastro</label>
                        <asp:TextBox ID="txtDtSituacao" Enabled="False" ReadOnly="true" CssClass="campoData"
                            runat="server" MaxLength="8" ToolTip="Data Cadastro"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDtSituacao"
                            ErrorMessage="Data Cadastro é requerida" SetFocusOnError="true" CssClass="validatorField">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label for="ddlStatus">
                            Situação</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" AutoPostBack="True"
                            Width="60px">
                            <asp:ListItem Value="A">Ativos</asp:ListItem>
                            <asp:ListItem Value="I">Inativos</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label for="ddlLogo">
                           Exibe Cabeçalho padrão</label>
                        <asp:DropDownList ID="ddlLogo" runat="server" CssClass="ddlLogo" AutoPostBack="True"
                            Width="60px">
                            <asp:ListItem Value="S">Sim</asp:ListItem>
                            <asp:ListItem Value="N">Não</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                </ul>
            </td>
            <td>
                <ul class="ulDados">
                    <li>
                        <h3 style="font-size: 12px;">
                            Páginas do documento</h3>
                    </li>
                    <li style="display: block; margin: 10px auto;">
                        <label for="txtNomeProcessoExterno">
                            Nome do Arquivo Externo</label>
                        <asp:FileUpload ID="NomeArquivo" runat="server" CssClass="txtNomeProcessoExterno"
                            Width="280px" />
                    </li>
                    <li style="margin: 5px 0 0 0">
                        <asp:Button ID="btnUpLoad" runat="server" OnClick="btnUpload_Click" Text="Adicionar"
                            Width="60px" />
                        <asp:Button ID="btnClear" runat="server" Text="Limpar" Width="60px" OnClientClick="return DeleteConfirm();"
                            OnClick="btnClear_Click" />
                    </li>
                    <li>
                        <div style="display: block; margin-top: 5px;">
                            <asp:GridView ID="gvPaginas" runat="server" AutoGenerateColumns="False" PageSize="12"
                                Width="338px" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="NU_PAGINA" HeaderText="Págs." ReadOnly="True">
                                        <ControlStyle Width="35px" Height="10px" />
                                        <HeaderStyle Width="35px" Height="10px" />
                                        <ItemStyle Width="45px" Wrap="False" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NM_PATH" HeaderText="Caminho">
                                        <FooterStyle Height="30px" />
                                        <HeaderStyle Height="10px" />
                                        <ItemStyle Height="10px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle Wrap="False" />
                            </asp:GridView>
                        </div>
                    </li>
                </ul>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtValor").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            //Coloca a sigla para recibo de pagamento automaticamente quando a opção de documento selecionada for Recibo
            $("#ddlTipoDoc").change(function (evento) {
                var e = document.getElementById("ddlTipoDoc");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec == "RE") {
                    $("#txtSiglaDoc").val("RECPAG");
                }
                else {
                    $("#txtSiglaDoc").val("");
                }
            });

        });
        function DisplayText(control) {
            document.getElementById('ctl00_content_txtNomeProcessoExterno').value = control.value;
        }
    </script>
</asp:Content>
