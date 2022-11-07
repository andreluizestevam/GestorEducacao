<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="RegistroRecepcaoDeAvaliacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao.RegistroRecepcaoDeAvaliacao" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 1120px;
            <%--width: 980px;--%>
        }
        .campoMoeda
        {
            text-align:right;
            width:70px;
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
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .ulDadosResp li
        {
            margin-top: -2px;
            margin-left: 5px;
        }
        .ulDadosPaciente li
        {
            margin-left: 0px;
        }
        .divPaciPreAtend
        {
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 120px;
            overflow-y: scroll;
            margin-left: 0px;
            margin-bottom: 5px;
            margin-top: -10px;
        }
        .divGeral
        {
            width: 1120;
            height: 240px;
            padding-top: 6px;
            margin-top: 6px;
        }
        .divDadosPaciResp
        {
            float: left;
            width: 996px;
            height: 232px;
            clear: both;
            margin-left:8px;
        }
        .DivResp
        {
            float: left;
            width: 600px;
            height: 207px;
        }
        .divEncamMedicoGeral
        {
            width: 960px;
            height: 220px;
            margin-left:8px;
        }
        .divEncamMedico
        {
            border: 1px solid #CCCCCC;
            width: 367px;
            height: 74px;
            overflow-y: scroll;
        }
        .ulIdentResp li
        {
            margin-left: 0px;
        }
        
        .ulDadosContatosResp li
        {
            margin-left: 0px;
        }
        
        .lblsub
        {
            color: #436EEE;
            font-size: 11px;
        }
        .lblTop
        {
            font-size: 9px;
            margin-bottom: 6px;
            color: #436EEE;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
        }
        
        .liFotoColab
        {
            float: left !important;
            margin-right: 10px !important;
            border: 0 none;
        }
        /*--> CSS DADOS */
        .fldFotoColab
        {
            border: none;
            width: 90px;
            height: 108px;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: left;
        }
        .chk label
        {
            display: inline;
        }
        .lblSubInfos
        {
            color: Orange;
            font-size: 8px;
        }
        .ulInfosGerais
        {
            margin-top: -3px;
        }
        .ulInfosGerais li
        {
            margin: 1px 0 3px 0px;
        }
        .ulEndResiResp
        {
        }
        .ulEndResiResp li
        {
            margin-left: 2px;
        }
        .divClassPri
        {
        }
        .divClassRed
        {
            background-color: Red;
        }
        .divClassOrange
        {
            background-color: Orange;
        }
        .divClassYellow
        {
            background-color: Yellow;
        }
        .divClassGreen
        {
            background-color: Green;
        }
        .divClassBlue
        {
            background-color: Blue;
        }
        .lisobe
        {
            margin-top: -9px !important;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
        }
        .campoDin
        {
            text-align:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoColEncAnalise" />
    <ul class="ulDados" style="margin-left: 6px !important;">
        <li class="divGeral">
            <ul class="divDadosPaciResp">
                <li class="liTituloGrid" style="width: 96.8%; height: 20px !important; margin-right: 0px;
                    background-color: #E0EEE0; text-align: center; font-weight: bold; margin-bottom: 5px;
                    padding-left: 10px;">
                    <ul style="float: left;">
                        <li>
                            <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                                DADOS PACIENTE E RESPONSÁVEL</label>
                        </li>
                    </ul>
                    <ul style="float: right; margin: 3px 5px 0 0;">
                        <li>
                            <asp:CheckBox runat="server" ID="chkEncaComPreAtend" Text="Direcionamento para Avaliação Prévia"
                                CssClass="chk" Checked="true" ToolTip="Marque caso deseje que o direcionamento Análise Prévia." />
                        </li>
                    </ul>
                </li>
                <li class="DivResp">
                    <ul class="ulDadosResp">
                        <li style="clear: both; margin-left: -5px; margin-top: 5px;">
                            <ul>
                                <li class="liFotoColab">
                                    <fieldset class="fldFotoColab">
                                        <uc1:ControleImagem ID="upImagemAluno" runat="server" />
                                    </fieldset>
                                </li>
                            </ul>
                        </li>
                        <li style="margin: 0px 0 0 -23px;">
                            <ul class="ulDadosPaciente">
                                <li style="margin-bottom: -3px;">
                                    <label class="lblTop">
                                        DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                                </li>
                                <li style="margin: 9px 3px 0 0px; clear: both"><a class="lnkPesNIRE" href="#">
                                    <img class="imgPesPac" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                        style="width: 17px; height: 17px;" /></a> </li>
                                <li>
                                    <label>
                                        Nº PRONTUÁRIO</label>
                                    <asp:RadioButton ID="rdbPesqProntuario" runat="server" CssClass="chk" GroupName="rdbPesq" />
                                    <asp:TextBox runat="server" ID="txtNuProntuario" Width="56" Style="margin-left: -6px"
                                        CssClass="campoNire"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        CPF</label>
                                    <asp:RadioButton ID="rdbPesqCPF" runat="server" CssClass="chk" GroupName="rdbPesq" />
                                    <asp:TextBox runat="server" ID="txtCpfPaci" CssClass="campoCpf" Width="75px" Style="margin-left: -6px"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hidCoPac" />
                                </li>
                                <li>
                                    <label>
                                        Nº CNES/SUS</label>
                                    <asp:RadioButton ID="rdbPesqNIS" runat="server" CssClass="chk" GroupName="rdbPesq" />
                                    <asp:TextBox runat="server" ID="txtNuNisPaci" Width="96" Style="margin-left: -6px"
                                        CssClass="campoNis" MaxLength="16"></asp:TextBox>
                                </li>
                                <li style="margin-left: -1px; margin-top: 12px;">
                                    <asp:ImageButton ID="imbPesqPaci" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imbPesqPaci_OnClick" />
                                </li>
                                <li style="clear: both; margin-top: -10px;">
                                    <label class="lblObrigatorio">
                                        Nome</label>
                                    <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="271px"></asp:TextBox>
                                </li>
                                <li style="margin-top: -10px;">
                                    <label class="lblObrigatorio">
                                        Sexo</label>
                                    <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="44px">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li class="lisobe">
                                    <label class="lblObrigatorio" style="margin-bottom: 0px;">
                                        Nascimento</label>
                                    <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData"></asp:TextBox>
                                </li>
                                <li style="margin-top: -10px; margin-left: 12px;">
                                    <label for="ddlEtniaAlu" title="Etnia do Aluno">
                                        Etnia</label>
                                    <asp:DropDownList ID="ddlEtniaAlu" CssClass="ddlEtniaAlu" runat="server" ToolTip="Informe a Etnia do Usuario"
                                        Width="90px">
                                        <asp:ListItem Value="B">Branca</asp:ListItem>
                                        <asp:ListItem Value="N">Negra</asp:ListItem>
                                        <asp:ListItem Value="A">Amarela</asp:ListItem>
                                        <asp:ListItem Value="P">Parda</asp:ListItem>
                                        <asp:ListItem Value="I">Indígena</asp:ListItem>
                                        <asp:ListItem Value="X" Selected="true">Não Informada</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li class="lisobe" style="clear: both">
                                    <label>
                                        Origem</label>
                                    <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="126px">
                                        <asp:ListItem Value="SR" Text="Sem Registro"></asp:ListItem>
                                        <asp:ListItem Value="MU" Text="Local - Escola Pública"></asp:ListItem>
                                        <asp:ListItem Value="EP" Text="Local - Escola Particular"></asp:ListItem>
                                        <asp:ListItem Value="IE" Text="Interior do Estado"></asp:ListItem>
                                        <asp:ListItem Value="OE" Text="Outro Estado"></asp:ListItem>
                                        <asp:ListItem Value="AR" Text="Área Rural"></asp:ListItem>
                                        <asp:ListItem Value="AI" Text="Área Indígena"></asp:ListItem>
                                        <asp:ListItem Value="AQ" Text="Área Quilombo"></asp:ListItem>
                                        <asp:ListItem Value="OO" Text="Outra Origem"></asp:ListItem>
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
                            </ul>
                        </li>
                        <li style="margin: -4px 0 -3px -105px">
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
                                ToolTip="CPF do Responsável" Text="000.000.000-00" ClientIDMode="Static"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hidCoResp" />
                        </li>
                        <li style="margin-top: 10px; margin-left: 0px;">
                            <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                Width="13px" Height="13px" OnClick="imgCpfResp_OnClick" />
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Nome</label>
                            <asp:TextBox runat="server" ID="txtNomeResp" Width="216px" ToolTip="Nome do Responsável"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Sexo</label>
                            <asp:DropDownList runat="server" ID="ddlSexResp" Width="44px" ToolTip="Selecione o Sexo do Responsável">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Nascimento</label>
                            <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Grau Parentesco</label>
                            <asp:DropDownList runat="server" ID="ddlGrParen" Width="99px">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pai/Mãe" Value="PM"></asp:ListItem>
                                <asp:ListItem Text="Tio(a)" Value="TI"></asp:ListItem>
                                <asp:ListItem Text="Avô/Avó" Value="AV"></asp:ListItem>
                                <asp:ListItem Text="Primo(a)" Value="PR"></asp:ListItem>
                                <asp:ListItem Text="Cunhado(a)" Value="CN"></asp:ListItem>
                                <asp:ListItem Text="Tutor(a)" Value="TU"></asp:ListItem>
                                <asp:ListItem Text="Irmão(ã)" Value="IR"></asp:ListItem>
                                <asp:ListItem Text="Outros" Value="OU" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin: -5px 0 0 0px;">
                            <ul class="ulIdentResp">
                                <li>
                                    <asp:Label runat="server" ID="lblcarteIden" Style="font-size: 9px;" CssClass="lblObrigatorio">Carteira de Identidade</asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Número</label>
                                    <asp:TextBox runat="server" ID="txtNuIDResp" Width="70px" ClientIDMode="Static"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Org Emiss</label>
                                    <asp:TextBox runat="server" ID="txtOrgEmiss" Width="50px" ClientIDMode="Static"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        UF</label>
                                    <asp:DropDownList runat="server" ID="ddlUFOrgEmis" Width="40px">
                                    </asp:DropDownList>
                                </li>
                            </ul>
                        </li>
                        <li style="margin: -5px 0 0 10px;">
                            <ul class="ulDadosContatosResp">
                                <li>
                                    <asp:Label runat="server" ID="Label1" Style="font-size: 9px;">Dados de Contato</asp:Label>
                                </li>
                                <li style="clear: both;">
                                    <label>
                                        Tel. Fixo</label>
                                    <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Tel. Celular</label>
                                    <asp:TextBox runat="server" ID="txtTelCelResp" Width="70px" CssClass="campoTel8dig"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="txtDeFaceResp" Width="91px"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-left: -189px;">
                                    <asp:Label runat="server" ID="lblFuncao">Profissão</asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlFuncao" Width="100px">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-left: -35px;">
                                    <asp:Label runat="server" ID="lblEmailPaci">Email</asp:Label>
                                    <asp:TextBox runat="server" ID="txtEmailResp" Width="210px"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblComRestricao" Visible="false" Style="color: Red">***Com Restrição Atendimento Plano***</asp:Label>
                                    <asp:Label runat="server" ID="lblSemRestricao" Visible="false" Style="color: Blue">***Sem Restrição Atendimento Plano***</asp:Label>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 0px; width: 375px; clear: none">
                    <ul>
                        <li style="margin-left: 1px; margin-bottom: 1px; margin-top: -2px;">
                            <label class="lblSubInfos">
                                ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                        </li>
                        <li style="clear: both;">
                            <label class="lblObrigatorio">
                                CEP</label>
                            <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="campoCepF" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtCEP_PADRAO" ClientIDMode="Static" Style="display: none" />
                        </li>
                        <li style="margin: 11px 2px 0 -2px;">
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
                            <asp:DropDownList runat="server" ID="ddlBairro" Width="114px">
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin-top: -8px;">
                            <label class="lblObrigatorio">
                                Logradouro</label>
                            <asp:TextBox runat="server" ID="txtLograEndResp" Width="160px" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtLograEndResp_PADRAO" ClientIDMode="Static" Style="display: none" />
                        </li>
                        <li style="margin-left: 10px; margin-top: -8px; clear: none">
                            <label>
                                Email</label>
                            <asp:TextBox runat="server" ID="txtEmailPaci" Width="190px"></asp:TextBox>
                        </li>
                        <li style="margin-left: 1px; margin-bottom: 1px; margin-top: -1px;">
                            <label class="lblSubInfos">
                                INFORMAÇÕES GERAIS</label>
                        </li>
                        <li style="border-left: 1px solid #CCC; height: 126px; position: absolute; left: 611px;
                            top: 102px; margin: 100px 10px 0 0 !important;"></li>
                        <li style="clear: both">
                            <asp:CheckBox runat="server" ID="chkPaciEhResp" OnCheckedChanged="chkPaciEhResp_OnCheckedChanged"
                                AutoPostBack="true" Text="Paciente é o Responsável" CssClass="chk" />
                        </li>
                        <li style="clear: none">
                            <asp:CheckBox runat="server" ID="chkPaciMoraCoResp" Text="Mora com Responsável" CssClass="chk" />
                        </li>
                        <li style="clear: both">
                            <asp:CheckBox runat="server" ID="chkRespFinanc" Text="Responsável financeiro" CssClass="chk" />
                        </li>
                        <li class="liTituloGrid" style="width: 98.4%; height: 16px !important; margin-right: 0px;
                            background-color: #d2ffc2; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px; margin-top: 5px;">
                            <ul>
                                <li style="margin-left: 10px;">
                                    <label style="font-family: Tahoma; font-weight: bold; color: #8B1A1A; margin-top: 1px;">
                                        PROFISSIONAL ANÁLISE PRÉVIA</label>
                                </li>
                            </ul>
                        </li>
                        <li class="divEncamMedico">
                            <ul>
                                <li>
                                    <asp:GridView ID="grdMedicosPlanto" CssClass="grdBusca" runat="server" Style="width: 100% !important;
                                        cursor: default" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                        OnRowDataBound="grdMedicosPlanto_RowDataBound" DataKeyNames="co_col">
                                        <RowStyle CssClass="rowStyle" />
                                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                        <EmptyDataTemplate>
                                            Nenhum Avaliador ativo<br />
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField Visible="false" DataField="co_col" HeaderText="Cod." SortExpression="CO_EMP"
                                                HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <HeaderStyle CssClass="noprint"></HeaderStyle>
                                                <ItemStyle Width="20px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="CK">
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hidcoCol" Value='<%# Eval("co_col") %>' runat="server" />
                                                    <asp:HiddenField ID="hidcoEmpColPla" Value='<%# Eval("co_emp_col_pla") %>' runat="server" />
                                                    <asp:HiddenField ID="hidCoEspec" Value='<%# Eval("CO_ESPEC") %>' runat="server" />
                                                    <asp:CheckBox ID="chkselect2" runat="server" OnCheckedChanged="chkselect2_OnCheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NO_COL" HeaderText="PROFISSINAL SAÚDE">
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NO_CLASS_PROFI" HeaderText="CLASS PROFI">
                                                <ItemStyle HorizontalAlign="Left" Width="115px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LOCAL" HeaderText="LOCAL">
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                                        </Columns>
                                    </asp:GridView>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <ul class="divEncamMedicoGeral">
                <li style="clear: both">
                    <ul>
                        <li class="liTituloGrid" style="width: 974px; height: 19px !important; margin-right: 0px;
                            background-color: #ADD8E6; text-align: center; font-weight: bold; margin-bottom: 5px;
                            padding-top: 2px;">
                            <ul>
                                <li style="margin: 0px 0 0 10px; float: left">
                                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                                        REGISTRO DE SOLICITAÇÕES DE PROCEDIMENTOS</label>
                                </li>
                                <li style="margin-left: 23px; float: right; margin-top: 1px">
                                    <ul>
                                        <li>
                                            <asp:Label runat="server" ID="lblGrpProc">
                                                Grupo Procedimento</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlGrupoProc" Width="140px" OnSelectedIndexChanged="ddlGrupoProc_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </li>
                                        <li>
                                            <asp:Label runat="server" ID="lblSubGrpProc">
                                                Subgrupo Procedimento</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="140px">
                                            </asp:DropDownList>
                                        </li>
                                        <li style="margin: 1px 2px 0 -2px;">
                                            <asp:ImageButton ID="imgPesqProcedimentos" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                                Width="13px" Height="13px" OnClick="imgPesqProcedimentos_OnClick" />
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <div style="width: 972px; height: 146px; border: 1px solid #CCC; overflow-y: scroll;
                                margin-bottom: 2px;">
                                <asp:GridView ID="grdSolicitacoes" CssClass="grdBusca" runat="server" Style="width: 100%;
                                    cursor: default; height: 30px !important;" AutoGenerateColumns="false" AllowPaging="false"
                                    GridLines="Vertical" OnRowDeleting="grdSolicitacoes_OnRowDeleting">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhuma solicitação inserida<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="OPERADORA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlOperadora" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Width="63px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PLANO">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlPlanoSaude" OnSelectedIndexChanged="ddlPlanoSaude_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Width="63px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CATEGORIA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlCategoria" Width="63px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nº PLANO">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNumeroPlano" Width="70px" Style="margin-bottom: -1px;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nº GUIA">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNuGuia" CssClass="nuGuia" ClientIDMode="Static"
                                                    Width="80px" Style="margin-bottom: -1px;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AUTORIZAÇÃO">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNuAutor" CssClass="nuGuia" ClientIDMode="Static"
                                                    Width="80px" Style="margin-bottom: -1px;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTDE">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtQtde" CssClass="nuQtde" ClientIDMode="Static"
                                                    Width="20px" Style="margin-bottom: -1px;" OnTextChanged="txtQtde_OnTextChanged"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROCEDIMENTO">
                                            <ItemStyle Width="200px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlProcedimento" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Width="240px">
                                                </asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hidValUnitProc" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ TOTAL">
                                            <ItemStyle Width="68px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgInfos" ImageUrl="/Library/IMG/Gestor_ComoChegar.png"
                                                    ToolTip="Visualizar maiores informações sobre o Procedimento" Style="margin: 0 0 0 0 !important;" OnClick="imgInfos_OnClick" />
                                                <asp:TextBox runat="server" ID="txtVlUnitario" Width="50px" ClientIDMode="Static"
                                                    CssClass="campoDin" Style="margin-bottom: -1px; margin-top: -1px;" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R$ DESC">
                                            <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtVlDesconto" Width="50px" ClientIDMode="Static"
                                                    CssClass="campoDin" Style="margin-bottom: -1px;" OnTextChanged="txtVlDesconto_OnTextChanged"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EX">
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgExc" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                    ToolTip="Excluir item de solicitação" OnClick="imgExc_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                        <li>
                            <ul>
                                <li style="margin-right: 70px;">
                                    <label>
                                        TOTAIS R$ SOLICITAÇÕES:</label>
                                </li>
                                <li>
                                    <asp:Label runat="server" ID="lblVlBase">Valor Base R$</asp:Label>
                                    <asp:TextBox runat="server" ID="txtVlBaseTotal" Enabled="false" CssClass="campoMoeda"></asp:TextBox>
                                </li>
                                <li style="margin-left: 30px;">
                                    <asp:Label runat="server" ID="Label2">Valor Desconto R$</asp:Label>
                                    <asp:TextBox runat="server" ID="txtVlDescontoTotal" Enabled="false" CssClass="campoMoeda"></asp:TextBox>
                                </li>
                                <li style="margin-left: 30px;">
                                    <asp:Label runat="server" ID="Label3">Valor Líquido R$</asp:Label>
                                    <asp:TextBox runat="server" ID="txtVlLiquidoTotal" Enabled="false" CssClass="campoMoeda"></asp:TextBox>
                                </li>
                                <li style="float: right; margin-top: 2px; margin-left: 104px;">
                                    <img alt="" class="imgAdd" title="Adicionar Questão" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                        height="15px" width="15px" />
                                    <asp:LinkButton runat="server" ID="btnMaisSolicitacoes" Style="margin-left: 4px;"
                                        OnClick="btnMaisSolicitacoes_OnClick"><span style="color:#FF7050">INSERIR MAIS SOLICITAÇÕES</span></asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        </li>
        <li>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
        </li>
        <li>
            <div id="divLoadShowInfoProcedimento" style="display: none; height: 180px !important;">
                <ul class="ulDados" style="width:407px;">
                    <li>
                        <label>
                            Código</label>
                        <asp:TextBox runat="server" ID="txtCodProc" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="float:right">
                        <label>
                            R$ Unitário</label>
                        <asp:TextBox runat="server" ID="txtVlUnitProc" Enabled="false" Width="70px"></asp:TextBox>
                    </li>
                    <li style="clear:both">
                        <label>
                            Nome</label>
                        <asp:TextBox runat="server" ID="txtNomeProc" Enabled="false" Width="400px"></asp:TextBox>
                    </li>
                     <li>
                        <label>
                            Observação</label>
                        <asp:TextBox runat="server" ID="txtObservacaoProc" Enabled="false" TextMode="MultiLine"
                            Width="400px" Height="50px"></asp:TextBox>
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <script type="text/javascript">

        //Inserida função apra abertura de nova janela popup com a url do relatório que apresenta as guias
        function customOpen(url) {
            var w = window.open(url);
            w.focus();
        }

        //Abre alert com mensagem 
        function AbreMensagem(msg) {
            alert(msg);
        }

        $(document).ready(function () {
            CarregamentosPadroes();
            carregaPadroes();

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });
        });

        function AbreModalInfoProcedimento() {
            $('#divLoadShowInfoProcedimento').dialog({ autoopen: false, modal: true, width: 444, height: 150, resizable: false, title: "INFORMAÇÕES DE PROCEDIMENTO DE SAÚDE",
                //                open: function () { $('#divLoadInfosCadas').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }

        function carregaPadroes() {
            $(".campoNire").mask("?999999999");
            $(".campoNis").mask("999999999999999?9");
            $(".campoCpf").mask("999.999.999-99");
            $(".nuGuia").mask("?999999999999999");
            $(".nuQtde").mask("9?99");
            $(".campoCepF").mask("99999-999");
            $(".campoTel8dig").mask("(99)9999-9999?9");
            $(".campoTel").mask("(99)9999-9999");
            $(".campoVenc").mask("99/99");
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
            $(".campoDin").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        }

        function CarregamentosPadroes() {
            //CPF do responsável
            $("#txtCPFResp").focus(function () {
                if ($(this).val() == "000.000.000-00")
                    $(this).val("");
            });
            $("#txtCPFResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("000.000.000-00");
            });

            //Data de Nascimento
            $("#txtDtNascResp").focus(function () {
                if ($(this).val() == "01/01/1900")
                    $(this).val("");
            });
            $("#txtDtNascResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("01/01/1900");
            });

            //Número RG
            $("#txtNuIDResp").focus(function () {
                if ($(this).val() == "000000")
                    $(this).val("");
            });
            $("#txtNuIDResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("000000");
            });

            //Órgão do RG do Responsável
            $("#txtOrgEmiss").focus(function () {
                if ($(this).val() == "SSP")
                    $(this).val("");
            });
            $("#txtOrgEmiss").blur(function () {
                if ($(this).val() == "")
                    $(this).val("SSP");
            });

            //CEP padrão igual o da unidade
            $("#txtCEP").focus(function () {
                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
                var cep = $("#txtCEP").val().replace("-", "");

                if (cep == cepPad)
                    $(this).val();

                //alert(cepPad + "---------" + cep);
            });
            $("#txtCEP").blur(function () {
                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
                var cep = $("#txtCEP").val().replace("-", "");
                if (cep == "")
                    $(this).val(cepPad);
            });

            //Logradouro padrão igual o da unidade
            $("#txtLograEndResp").focus(function () {
                if ($(this).val() == $("#txtLograEndResp_PADRAO").val())
                    $(this).val("");
            });
            $("#txtLograEndResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val($("#txtLograEndResp_PADRAO").val());
            });
        }

        //Função que é chamada quando se abre a página e depois dos postbacks
        function carregaCss() {
            CarregamentosPadroes();
            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".lnkPesNIRE").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });
        }

        //        Sys.Application.add_load(ApplicationLoadHandler)
        //        function ApplicationLoadHandler(sender, args) {
        //            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CheckStatus);
        //        }

        //        function CheckStatus(sender, args) {
        //            var prm = Sys.WebForms.PageRequestManager.getInstance();
        //            if(prm.get_isInAscyncPostBack() & args.get_postBackElement().id
        //        }

    </script>
</asp:Content>
