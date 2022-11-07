<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroRespUsuario.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Componentes.CadastroRespUsuario" %>
<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<style type="text/css">
    .lblTop
    {
        font-size: 9px;
        margin-bottom: 6px;
        color: #436EEE;
    }
    .liFotoColabAluno
    {
        float: left !important;
        margin-right: 10px !important;
        border: 0 none;
    }
    /*--> CSS DADOS */
    .fldFotoColabAluno
    {
        border: none;
        width: 90px;
        height: 108px;
    }
    .DivResp
    {
        float: left;
        width: 500px;
        height: 207px;
    }
    .chk label
    {
        display: inline;
    }
    .ulDadosResp li
    {
        margin-top: -2px;
        margin-left: 5px;
    }
    .ulIdentResp li
    {
        margin-left: 0px;
    }
    .ulDadosContatosResp li
    {
        margin-left: 0px;
    }
    .lblSubInfos
    {
        color: Orange;
        font-size: 8px;
    }
    .ulEndResiResp
    {
    }
    .ulEndResiResp li
    {
        margin-left: 5px !important;
    }
    .ulDadosPaciente li
    {
        margin-left: 0px;
    }
    .ulInfosGerais
    {
        margin-top: 0px;
    }
    .ulInfosGerais li
    {
        margin: 1px 0 3px 0px;
    }
    .liBtnAddA
    {
        background-color: #F1FFEF;
        border: 1px solid #D2DFD1;
        margin-top: 5px;
        margin-left: 295px !important;
        padding: 2px 3px 1px 3px;
    }
</style>
<ul>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="ulDados" style="width: 400px !important;">
                <div class="DivResp" runat="server" id="divResp">
                    <ul class="ulDadosResp" style="margin-left: -100px !important; width: 600px !important;">
                        <li style="margin: 30px 0 -3px 0px">
                            <label class="lblTop">
                                DADOS DO RESPONSÁVEL PELO PACIENTE</label>
                        </li>
                        <li style="clear: both; margin: 9px -1px 0 0px;"><a class="lnkPesResp" href="#">
                            <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                style="width: 17px; height: 17px;" /></a> </li>
                        <li>
                            <label class="lblObrigatorio">
                                CPF</label>
                            <asp:TextBox runat="server" ID="txtCPFResp" Style="width: 74px;" CssClass="campoCpf"
                                ToolTip="CPF do Responsável"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="HiddenField2" />
                        </li>
                        <li style="margin-top: 10px; margin-left: 0px;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                Width="13px" Height="13px" OnClick="imgCpfResp_OnClick" />
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Nome</label>
                            <asp:TextBox runat="server" ID="txtNomeResp" Width="216px" ToolTip="Nome do Responsável"></asp:TextBox>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Sexo</label>
                            <asp:DropDownList runat="server" ID="ddlSexResp" Width="44px" ToolTip="Selecione o Sexo do Responsável">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Nascimento</label>
                            <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Grau Parentesco</label>
                            <asp:DropDownList runat="server" ID="ddlGrParen" Width="100px">
                                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                                <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                                <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                                <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                                <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                                <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                                <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                                <asp:ListItem Text="Outros" Value="OU"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin: 0px 0 0 0px;">
                            <ul class="ulIdentResp">
                                <li>
                                    <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;">Carteira de Identidade</asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Número</label>
                                    <asp:TextBox runat="server" ID="txtNuIDResp" Width="70px"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Org Emiss</label>
                                    <asp:TextBox runat="server" ID="txtOrgEmiss" Width="50px"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        UF</label>
                                    <asp:DropDownList runat="server" ID="ddlUFOrgEmis" Width="40px">
                                    </asp:DropDownList>
                                </li>
                            </ul>
                        </li>
                        <li style="margin: 0px 0 0 10px;">
                            <ul class="ulDadosContatosResp">
                                <li>
                                    <asp:Label runat="server" ID="Label45" Style="font-size: 9px;" CssClass="lblObrigatorio">Dados de Contato</asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Tel. Fixo</label>
                                    <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Tel. Celular</label>
                                    <asp:TextBox runat="server" ID="txtTelCelResp" Width="76px" CssClass="Tel9Dig"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Tel. Comercial</label>
                                    <asp:TextBox runat="server" ID="txtTelComResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Nº WhatsApp</label>
                                    <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Facebook</label>
                                    <asp:TextBox runat="server" ID="txtDeFaceResp" Width="85px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; width: 206px; border-right: 1px solid #CCCCCC; margin-left: -5px;
                            height: 65px;">
                            <ul style="margin-left: 0px" class="ulInfosGerais">
                                <li style="margin-left: 5px; margin-bottom: 1px;">
                                    <label class="lblSubInfos">
                                        INFORMAÇÕES GERAIS</label>
                                </li>
                                <li style="clear: both">
                                    <asp:CheckBox runat="server" ID="chkPaciEhResp" OnCheckedChanged="chkPaciEhResp_OnCheckedChanged"
                                        AutoPostBack="true" Text="Responsável é o próprio paciente" CssClass="chk" />
                                </li>
                                <li style="clear: both">
                                    <asp:CheckBox runat="server" ID="chkPaciMoraCoResp" Text="Paciente mora com o(a) Responsável"
                                        CssClass="chk" />
                                </li>
                                <li style="clear: both">
                                    <asp:CheckBox runat="server" ID="chkRespFinanc" Text="É o responsável financeiro"
                                        CssClass="chk" />
                                </li>
                            </ul>
                        </li>
                        <li class="ulInfosGerais">
                            <ul style="margin-left: 0px" class="ulEndResiResp">
                                <li style="margin-left: 1px; margin-bottom: 1px;">
                                    <label class="lblSubInfos">
                                        ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                                </li>
                                <li style="clear: both;">
                                    <label class="lblObrigatorio">
                                        CEP</label>
                                    <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCEP"></asp:TextBox>
                                </li>
                                <li style="margin: 11px 2px 0 2px;">
                                    <asp:ImageButton ID="imgPesqCEP" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        OnClick="imgPesqCEP_OnClick" Width="13px" Height="13px" />
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        UF</label>
                                    <asp:DropDownList runat="server" ID="ddlUF" Width="40px" OnSelectedIndexChanged="ddlUF_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Cidade</label>
                                    <asp:DropDownList runat="server" ID="ddlCidade" Width="130px" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Bairro</label>
                                    <asp:DropDownList runat="server" ID="ddlBairro" Width="115px">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both; margin-top: -4px;">
                                    <label class="lblObrigatorio">
                                        Logradouro</label>
                                    <asp:TextBox runat="server" ID="txtLograEndResp" Width="160px"></asp:TextBox>
                                </li>
                                <li style="margin-left: 10px; margin-top: -4px;">
                                    <label>
                                        Email</label>
                                    <asp:TextBox runat="server" ID="txtEmailResp" Width="197px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li style="clear: both; margin-left: -5px; margin-top: -6px;">
                            <ul>
                                <li class="liFotoColabAluno">
                                    <fieldset class="fldFotoColabAluno">
                                        <uc1:ControleImagem ID="upImageCadas" runat="server" />
                                    </fieldset>
                                </li>
                            </ul>
                        </li>
                        <li style="margin: -2px 0 0 -23px;">
                            <ul class="ulDadosPaciente">
                                <li style="margin-bottom: -2px;">
                                    <label class="lblTop">
                                        DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                </li>
                                <li style="clear: both" class="lblObrigatorio">
                                    <label>
                                        Nº NIS</label>
                                    <asp:TextBox runat="server" ID="txtNuNisPaci" Width="60" CssClass="txtNireAluno"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        CPF</label>
                                    <asp:TextBox runat="server" ID="txtCPFMOD" CssClass="campoCpf" Width="75px"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="HiddenField3" />
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Nome</label>
                                    <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="298px"></asp:TextBox>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Sexo</label>
                                    <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="44px">
                                        <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both" class="lisobe">
                                    <label class="lblObrigatorio">
                                        Nascimento</label>
                                    <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData"></asp:TextBox>
                                </li>
                                <li class="lisobe">
                                    <label>
                                        Origem</label>
                                    <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="90px">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-left: 10px;" class="lisobe">
                                    <label>
                                        Nº Cartão Saúde</label>
                                    <asp:TextBox runat="server" ID="txtNuCarSaude" Width="87px"></asp:TextBox>
                                </li>
                                <li style="margin-left: 10px;" class="lisobe">
                                    <label>
                                        Tel. Celular</label>
                                    <asp:TextBox runat="server" ID="txtTelResPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li class="lisobe">
                                    <label>
                                        Tel. Fixo</label>
                                    <asp:TextBox runat="server" ID="txtTelCelPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li class="lisobe">
                                    <label>
                                        Nº WhatsApp</label>
                                    <asp:TextBox runat="server" ID="txtWhatsPaci" Width="68px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: 0px; float: right">
                                    <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                    <asp:TextBox runat="server" ID="txtEmailPaci" Width="220px"></asp:TextBox>
                                </li>
                            </ul>
                        </li>
                        <li id="li11" runat="server" class="liBtnAddA" style="margin: -10px 0 0 548px !important;">
                            <asp:LinkButton ID="lnkSalvar" Enabled="true" runat="server" OnClick="lnkSalvar_OnClick">
                                <asp:Label runat="server" ID="Label48" Text="SALVAR" Style="margin-left: 4px;"></asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div id="divSuccessoMessage" runat="server" class="successMessageSMS" visible="false">
                    <asp:Label ID="lblMsg" runat="server" Visible="false" />
                    <asp:Label Style="color: #B22222 !important; display: block;" Visible="false" ID="lblMsgAviso"
                        runat="server" />
                </div>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
</ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        ScriptsProntos();
    });
    $(document).ready(function () {
        ScriptsProntos();
    });

    function ScriptsProntos() {
        $(".Tel9Dig").mask("(99) 9999-9999?9");
        $(".campoTel").mask("(99) 9999-9999");
        $(".txtNumero").mask("?99999");
    }
</script>
