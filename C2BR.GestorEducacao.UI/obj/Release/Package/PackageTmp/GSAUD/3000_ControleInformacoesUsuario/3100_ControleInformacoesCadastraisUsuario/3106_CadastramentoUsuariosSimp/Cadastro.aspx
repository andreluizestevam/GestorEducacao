<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3106_CadastramentoUsuariosSimp.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        input
        {
            height: 13px;
        }
        .DivResp
        {
            float: left;
            width: 900px !important;
            height: 207px;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
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
            font-size: 9px;
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
        .ulDados li label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="width: 1000px !important; margin: 10px 0 0 30px !important;">
        <li class="DivResp" runat="server" id="divResp">
            <ul class="ulDadosResp" style="margin-left: 0px !important; width: 997px !important;">
                <li style="clear: both; margin-left: -5px; margin-top: -0px; width: 100px;">
                    <ul>
                        <li class="liFotoColabAluno">
                            <fieldset class="fldFotoColabAluno">
                                <uc1:ControleImagem ID="upImageCadas" runat="server" />
                            </fieldset>
                        </li>
                    </ul>
                </li>
                <li style="margin: -2px 0 0 -23px;" width="900px">
                    <ul class="ulDadosPaciente">
                        <li style="margin-bottom: 2px;">
                            <label class="lblTop">
                                DADOS DO PACIENTE - USUÁRIO DE SAÚDE</label>
                        </li>
                        <li style="clear: both;">
                            <label for="txtNireAluno" title="Número do NIRE" class="lblObrigatorio">
                                N° CONTROLE</label>
                            <asp:TextBox ID="txtNirePac" runat="server" ToolTip="Informe o Número do NIRE" Width="60px"
                                CssClass="txtNireAluno"> </asp:TextBox>
                        </li>
                        <li style="">
                            <label>
                                Nº CNES/SUS</label>
                            <asp:TextBox runat="server" ID="txtNuNisPaci" Width="87px" CssClass="txtCNESSUS"
                                MaxLength="16"></asp:TextBox>
                        </li>
                        <li style="">
                            <label>
                                CPF</label>
                            <asp:TextBox runat="server" ID="txtCPFMOD" CssClass="campoCpf" Width="75px"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hidCoPac" />
                        </li>
                        <li style="">
                            <label class="lblObrigatorio">
                                Nome</label>
                            <asp:TextBox runat="server" ID="txtnompac" ToolTip="Nome do Paciente" Width="270px"
                                Style="text-transform: uppercase"></asp:TextBox>
                        </li>
                        <li style="">
                            <label class="lblObrigatorio">
                                Sexo</label>
                            <asp:DropDownList runat="server" ID="ddlSexoPaci" Width="50px">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="">
                            <label for="ddlDeficienciaAlu" title="Deficiência">
                                Deficiência</label>
                            <asp:DropDownList ID="ddlDeficienciaAlu" Width="100px" CssClass="ddlDeficienciaAlu"
                                runat="server" ToolTip="Selecione a Deficiência do Usuario">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: 85px">
                            <label for="ddlSituacaoAlu" class="lblObrigatorio" title="Situação do Paciente">
                                Situação</label>
                            <asp:Label ID="lblDtSitua" Text="" Visible="false" runat="server" Style="margin-left: -50px" />
                            <asp:DropDownList ID="ddlSituacaoAlu" CssClass="ddlSituacaoAlu" ToolTip="Informe a Situação do Paciente"
                                runat="server" Width="98px">
                                <asp:ListItem Value="A" Selected="True">Em Atendimento</asp:ListItem>
                                <asp:ListItem Value="V">Em Análise</asp:ListItem>
                                <asp:ListItem Value="E">Alta (Normal)</asp:ListItem>
                                <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both;">
                            <label class="lblObrigatorio">
                                Apelido</label>
                            <asp:TextBox runat="server" MaxLength="25" ID="txtApelidoPaciente" Width="80px"></asp:TextBox>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Nascimento</label>
                            <asp:TextBox runat="server" ID="txtDtNascPaci" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px;" class="lisobe">
                            <label>
                                Tel. Celular</label>
                            <asp:TextBox runat="server" ID="txtTelCelPaci" Width="75px" CssClass="Tel9Dig"></asp:TextBox>
                        </li>
                        <li class="lisobe">
                            <label>
                                Tel. Fixo</label>
                            <asp:TextBox runat="server" ID="txtTelResPaci" Width="75px" CssClass="campoTel"></asp:TextBox>
                        </li>
                        <li class="lisobe">
                            <label>
                                Nº WhatsApp</label>
                            <asp:TextBox runat="server" ID="txtWhatsPaci" Width="75px" CssClass="campoTel"></asp:TextBox>
                        </li>
                        <li class="lisobe">
                            <label for="txtFacebookMae" title="Facebook">
                                Facebook</label>
                            <asp:TextBox ID="txtFacebookPac" Width="80px" ToolTip="Informe o Facebook da Mãe"
                                runat="server" MaxLength="30"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px">
                            <label>
                                Data Entrada</label>
                            <asp:TextBox ID="txtDataEntrada" runat="server" CssClass="campoData"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px">
                            <label>
                                Altura</label>
                            <asp:TextBox runat="server" ID="txtAltura" Width="30px" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Peso
                            </label>
                            <asp:TextBox runat="server" ID="txtPeso" Width="30px" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li style="margin-left: 10px">
                            <label>
                                Restrição de atendimento (Tipo/Data)
                            </label>
                            <asp:DropDownList runat="server" ID="ddlRestricaoAtendimento" Width="106px" OnSelectedIndexChanged="ddlRestricaoAtendimento_OnSelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDataRestricaoAtendimento" runat="server" CssClass="campoData"></asp:TextBox>
                        </li>
                        <li></li>
                        <li class="lisobe" style="clear: both;">
                            <label>
                                Origem</label>
                            <asp:DropDownList runat="server" ID="ddlOrigemPaci" Width="87px">
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
                        <li>
                            <label for="txtNaturalidade" style="margin-left: 5px" title="Cidade de Naturalidade do Aluno">
                                Naturalidade</label>
                            <asp:TextBox ID="txtNaturalidade" CssClass="txtNaturalidade" Style="width: 75px;
                                margin-left: 5px" runat="server" ToolTip="Informe a Cidade de Naturalidade do Usuario"
                                MaxLength="40"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtEstadoCivel" style="margin-left: 5px" title="Estado Civil">
                                Estado Civil</label>
                            <asp:DropDownList ID="ddlEstadoCivil" Style="width: 75px; margin-left: 5px;" runat="server"
                                ToolTip="Estado cível">
                                <asp:ListItem Value="">Não Informado</asp:ListItem>
                                <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                <asp:ListItem Value="S">Separado Judicialmente</asp:ListItem>
                                <asp:ListItem Value="D">Divorciado(a)</asp:ListItem>
                                <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                                <asp:ListItem Value="P">Companheiro(a)</asp:ListItem>
                                <asp:ListItem Value="U">União Estável</asp:ListItem>
                                <asp:ListItem Value="O">Outro</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlEtniaAlu" style="margin-left: 5px" title="Etnia do Aluno">
                                Etnia</label>
                            <asp:DropDownList ID="ddlEtniaAlu" Width="75px" Style="margin-left: 5px" CssClass="ddlEtniaAlu"
                                runat="server" ToolTip="Informe a Etnia do Usuario">
                                <asp:ListItem Value="B">Branca</asp:ListItem>
                                <asp:ListItem Value="N">Negra</asp:ListItem>
                                <asp:ListItem Value="A">Amarela</asp:ListItem>
                                <asp:ListItem Value="P">Parda</asp:ListItem>
                                <asp:ListItem Value="I">Indígena</asp:ListItem>
                                <asp:ListItem Value="X" Selected="true">Não Informada</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="">
                            <label>
                                Pasta Controle</label>
                            <asp:TextBox runat="server" ID="txtPastaControl" Width="84px" MaxLength="15" ToolTip="Número da pasta de controle, se não preenchida, será o mesmo que o NIRE"></asp:TextBox>
                        </li>
                        <li style="">
                            <label for="txtIndicacao" title="Indicação">
                                Indicação
                            </label>
                            <asp:DropDownList ID="ddlIndicacao" Style="width: 200px;" runat="server" ToolTip="Indicação">
                            </asp:DropDownList>
                        </li>
                        <li class="lisobe" style="">
                            <label style="color: Red" title="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases">
                                Telefone*</label>
                            <asp:TextBox runat="server" ID="txtTelMigrado" Width="227px" MaxLength="200" ToolTip="Telefone em formato distinto ao do sistema e geralmente migrados de outras bases"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li class="" style="margin-left: -1px; border-right-style: solid; border-right-color: #CCC;
                    border-right-width: 1px; padding-right: 5px;">
                    <ul>
                        <li style="margin-left: 6px; margin-bottom: 2px;">
                            <label class="lblSubInfos">
                                FILIACAO</label>
                        </li>
                        <li style="clear: both">
                            <label for="txtNomeMaeAlu" title="Nome da Mãe">
                                Nome da Mãe</label>
                            <asp:TextBox ID="txtNomeMae" Style="width: 185px;" CssClass="txtNomeMaeAlu" ToolTip="Informe o Nome da Mãe"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtobito" title="Óbito Mãe">
                                Óbito
                            </label>
                            <asp:DropDownList ID="ddlObitoMae" Style="width: 40px;" runat="server" ToolTip="Óbito Mãe"
                                OnSelectedIndexChanged="ddlObitoMae_OnSelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">Não </asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: -1px">
                            <label for="ddlProfissaoNomeMae" title="Profissão">
                                Profissão
                            </label>
                            <asp:DropDownList ID="ddlProfissaoNomeMae" Style="width: 85px;" runat="server" ToolTip="Profissão">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: -1px">
                            <label>
                                Tel. Mãe</label>
                            <asp:TextBox runat="server" ID="txtTelMae" Width="70px" CssClass="campoTel"></asp:TextBox>
                        </li>
                        <li style="margin: 0px 0 0 -3px;">
                            <label title="Responsável pelo acompanhamento do Paciente" style="margin-left: 5px;">
                                RA</label>
                            <asp:CheckBox runat="server" ID="chkMaeResp" CssClass="chk" ToolTip="Responsável pelo acompanhamento do Paciente" />
                        </li>
                        <li style="margin: 0px 0 0 -3px;">
                            <label title="Responsável financeiro do Paciente" style="margin-left: 5px;">
                                RF</label>
                            <asp:CheckBox runat="server" ID="chkMaeRespFinanc" CssClass="chk" ToolTip="Ao marcar, determina que a mãe é responsável financeira"
                                OnCheckedChanged="chkMaeRespFinanc_OnCheckedChanged" AutoPostBack="true" />
                        </li>
                        <li style="clear: both">
                            <label for="txtNomePaiAlu" title="Nome do Pai">
                                Nome do Pai</label>
                            <asp:TextBox ID="txtNomePai" Style="width: 185px;" CssClass="" ToolTip="Informe o Nome do Pai"
                                runat="server" MaxLength="60"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtobito" title="Óbito Pai">
                                Óbito
                            </label>
                            <asp:DropDownList ID="ddlObitoPai" Style="width: 40px;" runat="server" ToolTip="Óbito Pai"
                                OnSelectedIndexChanged="ddlObitoPai_OnSelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="S">Sim</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">Não </asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: -1px">
                            <label for="Profissão" title="Profissão do Pai">
                                Profissão
                            </label>
                            <asp:DropDownList ID="ddlProfissaoNomePai" Style="width: 85px;" runat="server" ToolTip="Profissão">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-left: -1px">
                            <label>
                                Tel. Pai</label>
                            <asp:TextBox runat="server" ID="txtTelPai" Width="70px" CssClass="campoTel"></asp:TextBox>
                        </li>
                        <li style="margin: 0px 0 0 -3px;">
                            <label title="Responsável pelo acompanhamento do Paciente" style="margin-left: 5px;">
                                RA</label>
                            <asp:CheckBox runat="server" ID="chkPaiResp" CssClass="chk" ToolTip="Responsável pelo acompanhamento do Paciente" />
                        </li>
                        <li style="margin: 0px 0 0 -3px;">
                            <label title="Responsável financeiro do Paciente" style="margin-left: 5px;">
                                RF</label>
                            <asp:CheckBox runat="server" ID="chkPaiRespFinanc" CssClass="chk" ToolTip="Responsável financeiro do Paciente"
                                OnCheckedChanged="chkPaiRespFinanc_OnCheckedChanged" AutoPostBack="true" />
                        </li>
                    </ul>
                </li>
                <li class="" style="font: left; margin-left: -1px">
                    <ul>
                        <li style="margin-left: 5px; margin-bottom: 2px;">
                            <label class="lblSubInfos">
                                PLANO DE SAÚDE</label>
                        </li>
                        <li style="clear: both">
                            <label title="Unidade de Contrato" for="txtUnidade">
                                Operadora</label>
                            <asp:DropDownList ID="ddlOperadora" ToolTip="Selecione uma Operadora" CssClass=""
                                OnSelectedIndexChanged="ddlOperadora_CheckedChanged" AutoPostBack="True" runat="server"
                                Width="70px">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-bottom: 10px">
                            <label title="Unidade de Contrato" for="txtUnidade">
                                Plano</label>
                            <asp:DropDownList ID="ddlPlano" ToolTip="Selecione uma Operadora" CssClass="" OnSelectedIndexChanged="ddlPlano_CheckedChanged"
                                AutoPostBack="True" runat="server" Width="70px">
                                <asp:ListItem Value="">Nenhuma</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both; margin-top: -10px;">
                            <label title="Unidade de Contrato" for="txtUnidade">
                                Categoria</label>
                            <asp:DropDownList ID="ddlCategoria" ToolTip="Selecione uma Operadora" CssClass=""
                                runat="server" Width="70px">
                                <asp:ListItem Value="">Nenhuma</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: -10px;">
                            <label for="txtNúmero" title="Número">
                                Numero
                            </label>
                            <asp:TextBox ID="txtNumeroPlano" runat="server" CssClass="" Width="70px" MaxLength="22"> </asp:TextBox>
                        </li>
                        <li style="clear: both; margin: -5px 0 0 14px;">
                            <label title="Data de Vencimento do Plano">
                                Dt Vencim.</label>
                        </li>
                        <li style="margin-top: -5px;">
                            <asp:TextBox runat="server" ID="txtDtVencPlan" CssClass="campoData" ToolTip="Data de Vencimento do Plano"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="border-left-style: solid; border-left-color: #CCC; border-left-width: 1px;
                    padding-right: 5px; margin-left: -4px; padding-left: 3px;">
                    <ul>
                        <li style="margin-left: 5px; margin-bottom: 2px;">
                            <label class="lblSubInfos">
                                DOCUMENTOS</label>
                        </li>
                        <li style="clear: both">
                            <label>
                                Carteira de identidade
                            </label>
                        </li>
                        <li style="clear: both">
                            <label for="Número" title="Número">
                                Número
                            </label>
                            <asp:TextBox ID="txtNumeroIdentidade" Width=" 70px" ToolTip="Informe o Número" runat="server"
                                MaxLength="15"></asp:TextBox>
                        </li>
                        <li>
                            <label for="txtNúmero" title="Órgão emissor ">
                                Org Emiss
                            </label>
                            <asp:TextBox ID="txtOrgao" Width="40px" ToolTip="Órgão emissor " runat="server"></asp:TextBox>
                        </li>
                        <li style="clear: both; margin-bottom: -5px">
                            <label for="txtNúmero" title="Número">
                                UF
                            </label>
                            <asp:DropDownList ID="ddlUfOrgPac" ToolTip="Selecione um UF" CssClass="" runat="server"
                                Width="46px">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Emissão</label>
                            <asp:TextBox runat="server" ID="txtDataEmissao" CssClass="campoData" Width="60px"></asp:TextBox>
                        </li>
                    </ul>
                </li>
                <li style="height: 85px; margin-top: -2px; border-left: 1px solid #CCC; padding-left: -10px;
                    margin-left: 5px">
                    <ul>
                        <li style="margin-bottom: 5px; margin-left: 10px">
                            <label class="lblSubInfos">
                                INFORMAÇÕES DE SAÚDE</label>
                        </li>
                        <li style="clear: both; margin-left: 5px !important;">
                            <label title="Classificação" class="">
                                Classificações
                            </label>
                            <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstClassificacao" Height="59px"
                                Width="120px" ToolTip="Lista das Classificação"></asp:ListBox>
                        </li>
                    </ul>
                </li>
                <li style="margin-top: -10px; border-left-style: solid; border-left-color: #CCC;
                    border-left-width: 1px; padding-left: 5px;">
                    <ul style="margin-left: 0px" class="ulEndResiResp">
                        <li style="margin-left: 1px; margin-bottom: 1px;">
                            <label class="lblSubInfos">
                                ENDEREÇO RESIDENCIAL / CORRESPONDÊNCIA</label>
                        </li>
                        <li style="clear: both;">
                            <label class="lblObrigatorio">
                                CEP</label>
                            <asp:TextBox runat="server" ID="txtCEP" Width="55px" CssClass="txtCEP" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li style="margin: 9px -1px -0px 0px;">
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
                            <asp:DropDownList runat="server" ID="ddlCidade" Width="140px" OnSelectedIndexChanged="ddlCidade_OnSelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Bairro</label>
                            <asp:DropDownList runat="server" ID="ddlBairro" Width="130px">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Logradouro</label>
                            <asp:TextBox runat="server" ID="txtLogra" Width="275px" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li>
                            <label>
                                Email</label>
                            <asp:TextBox runat="server" ID="txtEmailPaci" Width="224px"></asp:TextBox>
                        </li>
                        <li style="clear: both; margin-top: -4px; border-bottom: 65px; margin-left: -20px">
                            <asp:CheckBox ID="chkPaciMoraCoResp" Style="margin-left: -5px" CssClass="chk" OnSelectedIndexChanged="chkPaciMoraCoResp_OnCheckedChanged"
                                AutoPostBack="true" Text=" O Paciente mora com o(a) responsável" runat="server"
                                OnCheckedChanged="chkPaciMoraCoResp_OnCheckedChanged" />
                        </li>
                        <li style="margin: -5px 0 -10px 25px !important;">Observações
                            <asp:TextBox ID="txtObservacoes" runat="server" Width="240px" />
                        </li>
                    </ul>
                </li>
                <li style="margin-left: 2px">
                    <ul style="float: left">
                        <li>
                            <br />
                            <label class="lblTop">
                                DADOS DO RESPONSÁVEL PELO PACIENTE</label>
                        </li>
                        <li>
                            <br />
                            <asp:CheckBox ID="chkPaciEhResp" Style="margin-left: -10px" CssClass="chk" Text="O responsável é o próprio paciente"
                                runat="server" OnSelectedIndexChanged="chkPaciEhResp_OnCheckedChanged" AutoPostBack="true"
                                OnCheckedChanged="chkPaciEhResp_OnCheckedChanged" />
                        </li>
                        <li style="margin: 10px 0 0 6px;">
                            <asp:Label Text="Grau Parentesco" runat="server" />
                            <asp:DropDownList runat="server" ID="ddlGrParen" Width="100px">
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
                        <li style="clear: both; margin-left: 2px; margin-top: 2px;">
                            <ul>
                                <li style="clear: both; margin: 9px -1px 0 0px;"><a class="lnkPesResp" href="#">
                                    <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                                        style="width: 17px; height: 17px;" />
                                </a></li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Nº CONTROLE</label>
                                    <asp:RadioButton ID="rdbPesqRespCont" runat="server" Style="margin-left: -6px;" CssClass="chk"
                                        GroupName="rdbPesqResp" />
                                    <asp:TextBox runat="server" ID="txtNumContResp" Style="margin-left: -6px; width: 65px;"
                                        ToolTip="Número de Controle do Responsável" ClientIDMode="Static"></asp:TextBox>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        CPF</label>
                                    <asp:RadioButton ID="rdbPesqRespCpf" runat="server" Style="margin-left: -6px;" CssClass="chk"
                                        GroupName="rdbPesqResp" />
                                    <asp:TextBox runat="server" ID="txtCPFResp" Style="width: 74px;" CssClass="campoCpf"
                                        ToolTip="CPF do Responsável" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hidCoResp" />
                                </li>
                                <li style="margin-top: 10px; margin-left: 0px;">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                        Width="13px" Height="13px" OnClick="imgCpfResp_OnClick" />
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Nome</label>
                                    <asp:TextBox runat="server" ID="txtNomeResp" Width="230px" ToolTip="Nome do Responsável"></asp:TextBox>
                                </li>
                                <li>
                                    <label class="lblObrigatorio">
                                        Sexo</label>
                                    <asp:DropDownList runat="server" ID="ddlSexResp" Width="50px" ToolTip="Selecione o Sexo do Responsável">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Mas" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: left">
                                    <label class="lblObrigatorio">
                                        Nascimento</label>
                                    <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" ClientIDMode="Static"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Profissão</label>
                                    <asp:DropDownList runat="server" ID="ddlFuncao" Width="100px">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label>
                                        Email</label>
                                    <asp:TextBox runat="server" ID="txtEmailResp" Width="220px"></asp:TextBox>
                                </li>
                                <li>
                                    <label>
                                        Facebook</label>
                                    <asp:TextBox runat="server" ID="txtDeFaceResp" Width="94px"></asp:TextBox>
                                </li>
                                <li style="clear: left;">
                                    <ul class="ulDadosContatosResp">
                                        <li>
                                            <asp:Label runat="server" ID="Label45" Style="font-size: 9px;">Dados de Contato</asp:Label>
                                        </li>
                                        <li style="clear: both;">
                                            <label>
                                                Tel. Fixo</label>
                                            <asp:TextBox runat="server" ID="txtTelFixResp" Width="70px" CssClass="campoTel"></asp:TextBox>
                                        </li>
                                        <li>
                                            <label>
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
                                            <asp:TextBox runat="server" ID="txtNuWhatsResp" Width="80px" CssClass="campoTel"></asp:TextBox>
                                        </li>
                                    </ul>
                                </li>
                                <li style="margin-left: 20px">
                                    <ul class="ulDadosContatosResp">
                                        <li>
                                            <asp:Label runat="server" ID="id" Style="font-size: 9px;">Carteira de Identidade</asp:Label>
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
                            </ul>
                        </li>
                    </ul>
                    <ul style="float: right; margin-top: -5px;">
                        <li style="margin-bottom: 3px">
                            <label class="lblSubInfos">
                                INFORMAÇÕES DE ATENDIMENTO AO PACIENTE
                            </label>
                        </li>
                        <li style="float: right;">
                            <label>
                                * Profissional Inativo</label>
                        </li>
                        <li style="width: 400px; height: 88px; clear: both;">
                            <div style="width: 100%; height: 88px; border: 1px solid #CCC; overflow-y: scroll !important;">
                                <asp:GridView ID="grdProfiResponsaveis" CssClass="grdBusca" runat="server" Style="width: 100%;"
                                    AutoGenerateColumns="false">
                                    <RowStyle CssClass="rowStyle" />
                                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                    <EmptyDataTemplate>
                                        Nenhum Profissional de Saúde realizando acompanhamento no momento<br />
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="NO_COL" HeaderText="Atendimento">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_CLASS_PROFI" HeaderText="">
                                            <ItemStyle Width="90px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATAULTIMO" HeaderText="último">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATAPROC" HeaderText="próximo">
                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li style="clear: both; margin-top: 180px; border-left: 1px solid #CCC; margin-left: 4px;
            padding-left: -5px">
            <ul style="float: left">
                <li style="margin-left: 10px; border-right: 1px solid #CCC; padding-left: -5px;">
                    <ul>
                        <li style="margin-left: 1px; margin-bottom: 5px;">
                            <br />
                            <label class="lblSubInfos">
                                ENDEREÇO CORRESPONDÊNCIA</label>
                        </li>
                        <li style="float: right; margin-top: 10px;">
                            <asp:Label Text="Locação" runat="server" />
                            <asp:DropDownList ID="drpLocacao" runat="server" Width="90px">
                                <asp:ListItem Value="A" Text="Ambos" />
                                <asp:ListItem Value="S" Text="Lista de Espera" />
                                <asp:ListItem Value="N" Text="Pacientes" Selected="True" />
                            </asp:DropDownList>
                        </li>
                        <li style="clear: both;">
                            <label class="lblObrigatorio">
                                CEP</label>
                            <asp:TextBox runat="server" ID="txtCEPResp" Width="55px" CssClass="txtCEP" ClientIDMode="Static"></asp:TextBox>
                        </li>
                        <li style="margin: 11px 2px 0 2px;">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                                OnClick="imgPesqCEP_OnClick" Width="13px" Height="13px" />
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                UF</label>
                            <asp:DropDownList runat="server" ID="ddlUfRep" Width="40px" OnSelectedIndexChanged="ddlUFResp_OnSelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Cidade</label>
                            <asp:DropDownList runat="server" ID="ddlCidadeResp" Width="140px" OnSelectedIndexChanged="ddlCidadeResp_OnSelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Bairro</label>
                            <asp:DropDownList runat="server" ID="ddlBairroResp" Width="140px">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label class="lblObrigatorio">
                                Logradouro</label>
                            <asp:TextBox runat="server" ID="txtLogradouroResp" Width="280px" ClientIDMode="Static"></asp:TextBox>
                        </li>
                    </ul>
                </li>
            </ul>
            <ul style="float: right">
                <li style="margin-top: -45px; margin-left: 724px; width: 280px;">
                    <ul>
                        <li>
                            <label class="lblSubInfos" style="margin-top: -15px;">
                                PENDENCIAS</label>
                        </li>
                        <li>
                            <ul>
                                <li style="margin-bottom: 5px;">
                                    <asp:CheckBox runat="server" ID="chkDocumentos" Text="DOCUMENTOS" CssClass="chk" />
                                </li>
                                <li style="clear: both;">
                                    <asp:CheckBox runat="server" ID="chkFinanceiroCar" Enabled="False" Text="FINANCEIRO CAR"
                                        CssClass="chk" />
                                </li>
                            </ul>
                        </li>
                        <li style="margin-left: 110px; margin-top: -35px">
                            <ul>
                                <li style="margin-bottom: 5px;">
                                    <asp:CheckBox runat="server" ID="chkPlanoDeSaude" Text="PLANO/CONVENIO" CssClass="chk" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkFinanceiroGer" Text="FINANCEIRO GER" CssClass="chk" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>
            <div id="divSuccessoMessage" runat="server" class="successMessageSMS" visible="false">
                <asp:Label ID="lblMsg" runat="server" Visible="false" />
                <asp:Label Style="color: #B22222 !important; display: block;" Visible="false" ID="lblMsgAviso"
                    runat="server" />
            </div>
        </li>
        <li>
            <div id="divLoadShowResponsaveis" style="display: none; height: 315px !important;" />
        </li>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            ScriptsProntos();
            CarregamentosPadroes();
            $(".txtcpf").mask("999.999.999-99");
            $("#txtAltura").mask("9,99");
            $("#txtPeso").maskMoney({ symbol: "", decimal: ",", thousands: "." });

            $(".lnkPesResp").click(function () {
                $('#divLoadShowResponsaveis').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE RESPONSÁVEIS",
                    open: function () { $('#divLoadShowResponsaveis').load("/Componentes/ListarResponsaveis.aspx"); }
                });
            });

            $(".txtCEP").mask("99999-999");
        });

        function CarregamentosPadroes() {

            //Data de Nascimento do Responsável
            $("#txtDtNascResp").focus(function () {
                if ($(this).val() == "01/01/1900")
                    $(this).val("");
            });
            $("#txtDtNascResp").blur(function () {
                if ($(this).val() == "")
                    $(this).val("01/01/1900");
            });

            //Data de Nascimento do Paciente
            $("#txtDtNascPaci").focus(function () {
                if ($(this).val() == "01/01/1900")
                    $(this).val("");
            });
            $("#txtDtNascPaci").blur(function () {
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
            //            $("#txtCEP").focus(function () {
            //                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
            //                var cep = $("#txtCEP").val().replace("-", "");

            //                if (cep == cepPad)
            //                    $(this).val();

            //                //alert(cepPad + "---------" + cep);
            //            });
            //            $("#txtCEP").blur(function () {
            //                var cepPad = $("#txtCEP_PADRAO").val().replace("-", "");
            //                var cep = $("#txtCEP").val().replace("-", "");
            //                if (cep == "")
            //                    $(this).val(cepPad);
            //            });

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

        function ScriptsProntos() {
            $(".Tel9Dig").mask("(99)9999-9999?9");
            $(".campoTel").mask("(99) 9999-9999");
            $(".txtCNESSUS").mask("999999999999999?9");
            $(".txtNumero").mask("?99999");
            $(".campoCpf").mask("999.999.999-99");
            $(".txtNireAluno").mask("?999999999");
        }
    </script>
</asp:Content>
