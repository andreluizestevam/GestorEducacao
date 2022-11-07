<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2101_CadastroFamilia.Cadastro" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 950px;
        }
        .ulDados li
        {
            margin-top: 0px;
            margin-left: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 5px !important;
        }
        .div1
        {
            width: 500px;
            border-right: solid 1px #CCCCCC;
        }
        .div2
        {
            width: 450px;
            float: right !important;
            margin-top: -295px !important;
        }
        .ulDados2
        {
            width: 470px;
        }
        .fldFamilia
        {
            border-width: 0px;
        }
        .fldFamilia legend
        {
            font-weight: bold;
            font-size: 0.9em !important;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
        .chkLocais
        {
            margin-top: 5px;
        }
        .lstView
        {
            width: 200px !important;
            height: 80px !important;
            overflow: auto !important;
        }
        .divCabecalho
        {
            margin-left: 300px;
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <div id="div1" class="div1">
            <div class="divCabecalho">
                <li>
                    <label for="txtCodigo" title="Código da Família">
                        Código</label>
                    <asp:TextBox ID="txtCodigo" ToolTip="Código da Família" MaxLength="15" Width="40px"
                        runat="server" Enabled="false"></asp:TextBox>
                </li>
                <li style="display:none;">
                    <label for="txtCpf" title="CPF do Responsável pela Família">
                        CPF</label>
                    <asp:TextBox ID="txtCpf" Visible="false" ToolTip="Informe o CPF do Responsável" runat="server" CssClass="campoCpf"></asp:TextBox>
                </li>
                <li>
                    <label for="txtNome" class="lblObrigatorio" title="Nome">
                        Nome</label>
                    <asp:TextBox ID="txtNome" Style="text-transform: uppercase;" runat="server" CssClass="campoNomePessoa"
                        ToolTip="Informe o Nome da Família" MaxLength="80"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ValidationGroup="ALUNO"
                        runat="server" ControlToValidate="txtNome" ErrorMessage="Nome deve ser informado"
                        Text="*" Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                </li>
            </div>
            <li class="liClear">
                <fieldset id="Fieldset1" class="fldFamilia">
                    <legend>ENDEREÇO RESIDENCIAL</legend>
                    <ul id="ulDados1" class="ulDados1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <li class="liCep">
                                    <label for="txtCep" title="Cep" class="lblObrigatorio">
                                        Cep</label>
                                    <asp:TextBox ID="txtCep" ToolTip="Informe o Cep" CssClass="txtCep" Width="56px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCep" ValidationGroup="fam" runat="server" ControlToValidate="txtCep"
                                        ErrorMessage="CEP deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li id="liPesqCEP" class="liPesqCEP" style="margin-top: 10px; margin-left: 0px; margin-right: 0px;"
                                    runat="server">
                                    <asp:ImageButton ID="btnPesqCEP" runat="server" Width="13px" OnClick="btnPesqCEP_Click"
                                        ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesqMat" CausesValidation="false" />
                                </li>
                                <li>
                                    <label for="ddlUf" title="UF" class="lblObrigatorio">
                                        UF</label>
                                    <asp:DropDownList ID="ddlUf" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUf" ValidationGroup="fam" runat="server" ControlToValidate="ddlUf"
                                        ErrorMessage="UF deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="ddlCidade" title="Cidade" class="lblObrigatorio">
                                        Cidade</label>
                                    <asp:DropDownList ID="ddlCidade" Width="96px" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlCidade" ValidationGroup="fam" runat="server"
                                        ControlToValidate="ddlCidade" ErrorMessage="Cidade deve ser informada" Text="*"
                                        Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="ddlBairro" title="Bairro" class="lblObrigatorio">
                                        Bairro</label>
                                    <asp:DropDownList ID="ddlBairro" Width="100px" ToolTip="Selecione o Bairro" CssClass="ddlBairro"
                                        runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlBairro" ValidationGroup="fam" runat="server"
                                        ControlToValidate="ddlBairro" ErrorMessage="Bairro deve ser informado" Text="*"
                                        Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li>
                                    <label for="txtCodMun" title="Código do Município - IBGE" class="lblObrigatorio">
                                        Cod. Mun.</label>
                                    <asp:TextBox runat="server" Width="80px" ID="txtCodMun" CssClass="txtCodMun"></asp:TextBox>
                                </li>
                                <li class="liClear">
                                    <label for="txtLogradouro" class="lblObrigatorio" title="Logradouro">
                                        Logradouro</label>
                                    <asp:TextBox ID="txtLogradouro" CssClass="txtLogradouro" Width="140px" ToolTip="Informe o Logradouro"
                                        runat="server" MaxLength="40"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLogradouro" ValidationGroup="fam" runat="server"
                                        ControlToValidate="txtLogradouro" ErrorMessage="Endereço deve ser informado"
                                        Text="*" Display="None"></asp:RequiredFieldValidator>
                                </li>
                                <li class="liNumero">
                                    <label for="txtNumero" title="Número">
                                        Nr</label>
                                    <asp:TextBox ID="txtNumero" ToolTip="Informe o Número" Width="30px" CssClass="txtNumero"
                                        runat="server" MaxLength="5"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtComplementoResp" title="Complemento">
                                        Complemento</label>
                                    <asp:TextBox ID="txtComplemento" Width="140px" ToolTip="Informe o Complemento" CssClass="txtComplemento"
                                        runat="server" MaxLength="40"></asp:TextBox>
                                </li>
                                <li>
                                    <label for="txtTelResidencial" title="Telefone Residencial">
                                        Telefone Fixo</label>
                                    <asp:TextBox ID="txtTelResidencial" Width="86px" ToolTip="Informe o Telefone Residencial"
                                        CssClass="txtTelResidencial" runat="server"></asp:TextBox>
                                </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ul>
                </fieldset>
            </li>
            <li class="liDados2 liClear">
                <div class="divLocalizacao" style="float: left; width: 150px;">
                    <fieldset id="Fieldset2" class="fldFamilia">
                        <legend>LOCALIZAÇÃO</legend>
                        <ul id="ul2">
                            <li class="liClear">
                                <label for="txtLatitude" title="Latitude">
                                    Latitude</label>
                                <asp:TextBox ID="txtLatitude" Width="118px" ToolTip="Informe a Latitude" CssClass="txtLatitude"
                                    runat="server" MaxLength="20"></asp:TextBox>
                            </li>
                            <li class="liClear">
                                <label for="txtLongitude" title="Longitude">
                                    Longitude</label>
                                <asp:TextBox ID="txtLongitude" Width="118px" ToolTip="Informe a Longitude" CssClass="txtLongitude"
                                    runat="server" MaxLength="20"></asp:TextBox>
                            </li>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <li class="liClear">
                                        <label for="ddlRegiao" title="Região" class="lblObrigatorio">
                                            Região</label>
                                        <asp:DropDownList ID="ddlRegiao" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged"
                                            ToolTip="Selecione a Região" CssClass="ddlRegiao" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear">
                                        <label for="ddlArea" title="Área" class="lblObrigatorio">
                                            Área</label>
                                        <asp:DropDownList ID="ddlArea" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                            ToolTip="Selecione a Área" CssClass="ddlArea" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="liClear">
                                        <label for="ddlSubArea" title="Subárea" class="lblObrigatorio">
                                            Subárea</label>
                                        <asp:DropDownList ID="ddlSubArea" AutoPostBack="true" Width="120px" ToolTip="Selecione a Subárea"
                                            CssClass="ddlSubArea" runat="server">
                                        </asp:DropDownList>
                                    </li>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div class="divMapa" style="float: right; width: 300px;">
                    <ul>
                        <li class="liTitInfCont" style="background-color: #DDDDDD; width: 300px; text-align: center;
                            padding: 2px 0 2px 0; clear: both;">
                            <label class="lblTitInf">
                                POSIÇÃO GEOREFERENCIAL ENDEREÇO RESIDENCIAL</label>
                        </li>
                        <li class="liClear" style="margin-top: 5px;">
                            <div style="border: 1px solid #BEBEBE; width: 298px; height: 168px; float: right">
                                <table id="tbMap" runat="server" cellpadding="0" cellspacing="0" style="width: 98%;
                                    border: 0px;">
                                    <tr>
                                        <td class="mapa">
                                            <!--API GOOGLE, ATIVA O serviço de map na tela-->
                                            <artem:GoogleMap ID="GMapa" CssClass="mapa" runat="server">
                                            </artem:GoogleMap>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </li>
                    </ul>
                </div>
            </li>
            <li style="margin-top: 10px; margin-left: 170px;">
                <asp:CheckBox runat="server" CssClass="chkLocais" ID="chkAreaRisco" Text="Área de Risco" />
            </li>
            <li style="margin-left: 410px; margin-top: -15px;">
                <label for="txtDataCadastro" title="Data de Cadastro">
                    Data Cadastro</label>
                <asp:TextBox ID="txtDataCadastro" Enabled="false" ToolTip="Informe a Longitude" CssClass="txtData"
                    runat="server"></asp:TextBox>
            </li>
        </div>
        <div id="div2" class="div2">
            <li class="liDados3">
                <fieldset id="Fieldset5" class="fldFamilia">
                    <legend>INFORMAÇÕES SÓCIO-ECONÔMICAS</legend>
                    <ul id="ul3">
                        <li>
                            <label for="ddlTipoOcup" title="Tipo de Ocupação">
                                Tipo Ocupação</label>
                            <asp:DropDownList ID="ddlTipoOcup" Width="120px" ToolTip="Selecione o Tipo de Ocupação"
                                CssClass="ddlTipoOcup" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlTipoTerreno" title="Tipo de Terreno">
                                Tipo Terreno</label>
                            <asp:DropDownList ID="ddlTipoTerreno" Width="120px" ToolTip="Selecione o Tipo de Terreno"
                                CssClass="ddlTipoTerreno" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="ddlTipoDelim" title="Tipo de Delimitação">
                                Tipo Delimitação</label>
                            <asp:DropDownList ID="ddlTipoDelim" Width="120px" ToolTip="Selecione o Tipo de Delimitação"
                                CssClass="ddlTipoDelim" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="margin-top: 5px;">
                            <label for="ddlTipoCobertura" title="Tipo de Cobertura">
                                Tipo Cobertura</label>
                            <asp:DropDownList ID="ddlTipoCobertura" Width="120px" ToolTip="Selecione o Tipo de Cobertura"
                                CssClass="ddlTipoCobertura" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="ddlAbastAgua" title="Abastecimento de Águas">
                                Abast. Água</label>
                            <asp:DropDownList ID="ddlAbastAgua" Width="120px" ToolTip="Selecione o Tipo de Abastecimento de Água"
                                CssClass="ddlAbastAgua" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="ddlRedeEsgoto" title="Rede de Esgoto">
                                Rede Esgoto</label>
                            <asp:DropDownList ID="ddlRedeEsgoto" Width="120px" ToolTip="Selecione o Tipo de Rede de Esgoto"
                                CssClass="ddlRedeEsgoto" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear" style="margin-top: 5px;">
                            <label for="ddlEnergiaEletrica" title="Energia Elétrica">
                                Energia Elétrica</label>
                            <asp:DropDownList ID="ddlEnergiaEletrica" Width="120px" ToolTip="Selecione o Tipo de Energia Elétrica"
                                CssClass="ddlEnergiaEletrica" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="txtAreaTerreno" title="Área do Terreno">
                                Área Terreno</label>
                            <asp:TextBox ID="txtAreaTerreno" Width="50px" ToolTip="Informe a Área do Terreno"
                                CssClass="txtAreaTerreno" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="txtAreaEdificada" title="Área Edificada">
                                Área Edificada</label>
                            <asp:TextBox ID="txtAreaEdificada" Width="50px" ToolTip="Informe a Área Edificada"
                                CssClass="txtAreaEdificada" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="txtQuartos" title="Quartos">
                                Quartos</label>
                            <asp:TextBox ID="txtQuartos" Width="30px" ToolTip="Informe a Quantidade de Quartos"
                                CssClass="txtQuartos" runat="server"></asp:TextBox>
                        </li>
                        <li style="margin-top: 5px;">
                            <label for="txtBanheiros" title="Banheiros">
                                Banheiros</label>
                            <asp:TextBox ID="txtBanheiros" Width="30px" ToolTip="Informe a Quantidade de Banheiros"
                                CssClass="txtBanheiros" runat="server"></asp:TextBox>
                        </li>
                        <li class="liClear">
                            <fieldset id="Fieldset3" class="fldFamilia">
                                <legend>Quantidade Pessoas</legend>
                                <ul>
                                    <li style="margin-right: 0px;">
                                        <label for="txtQtd0005" title="">
                                            00/05</label>
                                        <asp:TextBox ID="txtQtd0005" Width="20px" ToolTip="Informe a Quantidade de 0 à 5 anos"
                                            CssClass="txtQtd0005" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-right: 0px;">
                                        <label for="txtQtd0612" title="">
                                            06/12</label>
                                        <asp:TextBox ID="txtQtd0612" Width="20px" ToolTip="Informe a Quantidade de 6 à 12 anos"
                                            CssClass="txtQtd0612" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-right: 0px;">
                                        <label for="txtQtd1318" title="">
                                            13/18</label>
                                        <asp:TextBox ID="txtQtd1318" Width="20px" ToolTip="Informe a Quantidade de 13 à 18 anos"
                                            CssClass="txtQtd1318" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-right: 0px;">
                                        <label for="txtQtd18m" title="">
                                            18+</label>
                                        <asp:TextBox ID="txtQtd18m" Width="20px" ToolTip="Informe a Quantidade acima de 18 anos"
                                            CssClass="txtQtd18m" runat="server"></asp:TextBox>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li>
                            <fieldset id="Fieldset4" class="fldFamilia">
                                <legend>Renda Familiar</legend>
                                <ul>
                                    <li style="margin-right: 0px;">
                                        <label for="txtQtdRenda" title="Quantidade">
                                            Qtd.</label>
                                        <asp:TextBox ID="txtQtdRenda" Width="20px" ToolTip="Informe a Quantidade de Pessoas Financeiramente Ativas"
                                            CssClass="txtQtdRenda" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="margin-right: 0px;">
                                        <label for="ddlRendaFamiliar" title="Renda Familiar">
                                            Renda Familiar</label>
                                        <asp:DropDownList ID="ddlRendaFamiliar" CssClass="ddlRendaFamiliar" runat="server"
                                            ToolTip="Informe a Renda Familiar">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                                            <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                                            <asp:ListItem Value="3">5 a 10 SM</asp:ListItem>
                                            <asp:ListItem Value="4">+10 SM</asp:ListItem>
                                            <asp:ListItem Value="5">Sem Renda</asp:ListItem>
                                            <asp:ListItem Value="6">Não informada</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlRendaFamiliar"
                                            ErrorMessage="Renda familiar deve ser informada" ValidationGroup="ALUNO" Text="*"
                                            Display="None" CssClass="validatorField"></asp:RequiredFieldValidator>
                                    </li>
                                </ul>
                            </fieldset>
                        </li>
                        <li style="margin-top: 10px;">
                            <label for="ddlOrigem" title="Origem da Família">
                                Origem</label>
                            <asp:DropDownList ID="ddlOrigem" CssClass="ddlOrigem" runat="server" ToolTip="Informe a Origem da Família">
                            </asp:DropDownList>
                        </li>
                        <li class="liClear">
                            <div class="divInst" style="float: left; width: 200px; height: 100px; margin-right: 5px;">
                                <label for="lstInstituicoes" title="Instituições de Apoio" style="background-color: #DDDDDD;
                                    width: 200px; text-align: center; padding: 2px 0 2px 0; clear: both;">
                                    INSTITUIÇÕES DE APOIO</label>
                                <div class="lstView">
                                    <asp:ListView runat="server" ID="lstInstituicoes" DataKeyNames="IdInstituicao">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" CssClass="chkLocais" ID="chkLstInst" Checked='<%# bind("MarcarLinha") %>' />
                                            <asp:Label ID="lblInst" runat="server" Text='<%# bind("NomeInstituicao") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfInst" Value='<%# bind("IdInstituicao") %>' />
                                            <br />
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div class="divProg" style="float: right; width: 200px; height: 100px;">
                                <label for="lstProgramas" title="Programas Sociais" style="background-color: #DDDDDD;
                                    width: 200px; text-align: center; padding: 2px 0 2px 0; clear: both;">
                                    PROGRAMAS SOCIAIS</label>
                                <div class="lstView">
                                    <asp:ListView runat="server" ID="lstProgramas" DataKeyNames="IdPrograma">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" CssClass="chkLocais" ID="chkLstInst" Checked='<%# bind("MarcarLinha") %>' />
                                            <asp:Label ID="lblProg" runat="server" Text='<%# bind("NomePrograma") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfProg" Value='<%# bind("IdPrograma") %>' />
                                            <br />
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </li>
                        <li class="liClear">
                            <label for="txtObs" title="Observação">
                                Observação</label>
                            <asp:TextBox ID="txtObs" Style="width: 390px;" TextMode="MultiLine" runat="server"
                                ToolTip="Digite a Observação">
                            </asp:TextBox>
                        </li>
                        <li class="liClear">
                            <label for="ddlSituacao" title="Situação">
                                Situação</label>
                            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Informe a Situação da Família">
                                <asp:ListItem Value="A" Selected="True">Ativo</asp:ListItem>
                                <asp:ListItem Value="I">Inativo</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label for="txtDataSituacao" title="Data Situação">
                                Data Situação</label>
                            <asp:TextBox ID="txtDataSituacao" Enabled="false" CssClass="txtData" runat="server"></asp:TextBox>
                        </li>
                    </ul>
                </fieldset>
            </li>
        </div>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".txtCodMun").mask("?999999999");
            $(".txtTelResidencial").mask("?(99) 9999-9999");
            $(".txtNumero").mask("?99999");
            $(".campoCpf").mask("999.999.999-99");
            $(".txtCep").mask("99999-999");
            $(".txtData").mask("99/99/9999");

            $(".txtAreaTerreno").mask("?9999,99");
            $(".txtAreaEdificada").mask("?9999,99");

            $(".txtQuartos").mask("?99");
            $(".txtBanheiros").mask("?99");

            $(".txtQtd0005").mask("?99");
            $(".txtQtd0612").mask("?99");
            $(".txtQtd1318").mask("?99");
            $(".txtQtd18m").mask("?99");
        });

    </script>
</asp:Content>
