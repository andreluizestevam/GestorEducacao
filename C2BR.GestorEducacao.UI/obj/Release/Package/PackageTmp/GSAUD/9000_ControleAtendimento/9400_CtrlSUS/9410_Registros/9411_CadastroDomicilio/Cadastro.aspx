<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9400_CtrlSUS._9410_Registros._9411_CadastroDomicilio.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
            margin-left: 264px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-top: -6px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .campoHora
        {
            width: 28px;
        }
        .campoTelefone
        {
            width: 55px;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .liTituloGrid
        {
            height: 15px;
            width: 100px;
            text-align: center;
            padding: 5 0 5 0;
            clear: both;
        }
        input
        {
            height: 13px;
        }
        .chk label
        {
            display: inline;
            margin-left: -3px;
        }
        .lisobe
        {
            margin-top: -20px !important;
        }
        .fldFotoColab
        {
            margin-left: 0px;
            border: none;
            width: 90px;
            height: 130px;
        }
        .liFotoColab
        {
            width: 200px;
            float: left !important;
            margin-right: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField runat="server" ID="hidCoCadasDomic" />
    <ul class="ulDados">
        <li style="clear: both; margin-top: -20px;">
            <label class="" title="Código do formulário">
                Código</label>
            <asp:TextBox runat="server" BackColor="Yellow" Text="FCD000001" ID="txtCodForm" Width="125px"
                ToolTip="Código do formulário" Enabled="false"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Número do cartão SUS do profissional">
                Nº Cartão SUS do Profissional</label>
            <asp:TextBox runat="server" CssClass="cartSus" ID="txtNumCartProfi" Width="135px"
                MaxLength="15" ToolTip="Insira o número do cartão SUS do profissional"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtNumCartProfi" ControlToValidate="txtNumCartProfi"
                ErrorMessage="Número do cartão SUS do profissional é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -6px;">
            <label class="lblObrigatorio" title="Código do CNES da unidade">
                Cód. CNES Unidade</label>
            <asp:TextBox runat="server" CssClass="CNES" ID="txtCnesUnid" MaxLength="7" Width="95px"
                ToolTip="Insira o código do CNES da unidade"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtCnesUnid" ControlToValidate="txtCnesUnid"
                ErrorMessage="Código do CNES da unidade é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -6px;">
            <label class="lblObrigatorio" title="Código da equipe">
                Cód. Equipe (INE)</label>
            <asp:TextBox runat="server" CssClass="equipe" ID="txtCodEquip" MaxLength="10" Width="88px"
                ToolTip="Insira o código da equipe"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtCodEquip" ControlToValidate="txtCodEquip"
                ErrorMessage="Código da equipe é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -6px;">
            <label class="lblObrigatorio" title="Código da microarea">
                Microarea</label>
            <asp:TextBox runat="server" ID="txtMicroarea" MaxLength="2" Width="45px" ToolTip="Insira o código da microarea"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtMicroarea" ControlToValidate="txtMicroarea"
                ErrorMessage="Código da microarea é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -6px;">
            <label class="lblObrigatorio" title="Data de cadastro domiciliar">
                Data de Cadastro</label>
            <asp:TextBox runat="server" ID="txtDataCadas" CssClass="campoData" ToolTip="Data do cadastro domiciliar"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtDataCadas" ControlToValidate="txtDataCadas"
                ErrorMessage="Data do cadastro domiciliar é requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label title="Tipo de Logradouro">
                Tipo de Logradouro</label>
            <asp:TextBox runat="server" ID="txtTipoLogra" Width="110px" MaxLength="15" ToolTip="Insira o tipo de logradouro"></asp:TextBox>
        </li>
        <li style="margin-left: 0px;">
            <label class="lblObrigatorio" title="Nome do Logradouro">
                Nome do Logradouro</label>
            <asp:TextBox runat="server" ID="txtLogradouro" Width="252px" MaxLength="35" ToolTip="Insira o nome do logradouro"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtLogradouro" ControlToValidate="txtLogradouro"
                ErrorMessage="O nome do logradouro é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -7px;">
            <label class="lblObrigatorio" title="Número do Logradouro">
                Nº</label>
            <asp:TextBox runat="server" CssClass="digCinco" ID="txtNumLogra" Width="35px"
                MaxLength="5" ToolTip="Insira o Número do logradouro"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtNumLogra" ControlToValidate="txtNumLogra"
                ErrorMessage="O Número do logradouro é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: -7px;">
            <label class="lblObrigatorio" title="CEP do Logradouro">
                CEP</label>
            <asp:TextBox runat="server" ID="txtCEP" Width="52px" CssClass="campoCEP" ToolTip="Insira o CEP do Logradouro"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtCEP" ControlToValidate="txtCEP"
                ErrorMessage="O CEP do logradouro é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both;">
            <label class="" title="Complemento do Logradouro">
                Complemento</label>
            <asp:TextBox runat="server" ID="txtComplemento" Width="246px" MaxLength="35" ToolTip="Insira o complemento do logradouro"></asp:TextBox>
        </li>
        <asp:UpdatePanel ID="upLocal" runat="server">
            <ContentTemplate>
                <li style="margin-left: 8px;">
                    <label class="lblObrigatorio" title="UF do logradouro">
                        UF</label>
                    <asp:DropDownList runat="server" ID="ddlUFLocal" Width="40px" OnSelectedIndexChanged="ddlUFLocal_OnSelectedIndexChanged"
                        AutoPostBack="true" ToolTip="UF do logradouro">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator InitialValue="" ID="rfvddlUFLocal" Display="Dynamic"
                        ValidationGroup="g1" runat="server" ControlToValidate="ddlUFLocal" Text="*" ErrorMessage="UF do logradouro é requerido"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-left: 7px;">
                    <label class="lblObrigatorio" title="Município do logradouro">
                        Município</label>
                    <asp:DropDownList runat="server" ID="ddlCidadeLocal" Width="160px" ToolTip="Escolha o Município do Logradouro"
                        OnSelectedIndexChanged="ddlCidadeLocal_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator InitialValue="" ID="rfvddlCidadeLocal" Display="Dynamic"
                        ValidationGroup="g1" runat="server" ControlToValidate="ddlCidadeLocal" Text="*"
                        ErrorMessage="Município do logradouro é requerido"></asp:RequiredFieldValidator>
                </li>
                <li style="clear: both;">
                    <label class="lblObrigatorio" title="Bairro do logradouro">
                        Bairro</label>
                    <asp:DropDownList runat="server" ID="ddlBairroLocal" Width="163px" ToolTip="Bairro do logradouro">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator InitialValue="" ID="rfvddlBairroLocal" Display="Dynamic"
                        ValidationGroup="g1" runat="server" ControlToValidate="ddlBairroLocal" Text="*"
                        ErrorMessage="Bairro do logradouro é requerido"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="">
            <label class="" title="Tipo de Domicílio">
                Tipo de Domicílio</label>
            <asp:DropDownList runat="server" ID="ddlTipoMoradia" Width="102px" ToolTip="Selecione o Tipo do Domicílio">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Casa</asp:ListItem>
                <asp:ListItem Value="2">Apartamento</asp:ListItem>
                <asp:ListItem Value="3">Cômodo</asp:ListItem>
                <asp:ListItem Value="4">Outro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 5px;">
            <label class="" title="Telefone residencial">
                Telefone Residencial</label>
            <asp:TextBox runat="server" CssClass="campoTelefone" ID="txtTelResid" Width="87px"
                ToolTip="Insira o telefone residencial do logradouro"></asp:TextBox>
        </li>
        <li style="">
            <label class="" title="Telefone de referência">
                Telefone Referência</label>
            <asp:TextBox runat="server" CssClass="campoTelefone" ID="txtTelRefer" Width="87px"
                ToolTip="Insira o telefone de referência"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio" title="Situação de Moradia / Posse da Terra">
                Situação de Moradia / Posse da Terra</label>
            <asp:DropDownList runat="server" ID="ddlSituMoradia" Width="163px" ToolTip="Situação de Moradia / Posse da Terra">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Próprio</asp:ListItem>
                <asp:ListItem Value="2">Financiado</asp:ListItem>
                <asp:ListItem Value="3">Alugado</asp:ListItem>
                <asp:ListItem Value="4">Arrendado</asp:ListItem>
                <asp:ListItem Value="5">Cedido</asp:ListItem>
                <asp:ListItem Value="6">Ocupação</asp:ListItem>
                <asp:ListItem Value="7">Situação de Rua</asp:ListItem>
                <asp:ListItem Value="8">Outra</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator InitialValue="" ID="rfvddlSituMoradia" Display="Dynamic"
                ValidationGroup="g1" runat="server" ControlToValidate="ddlSituMoradia" Text="*"
                ErrorMessage=" Selecione a Situação de Moradia / Posse da Terra é requerida"></asp:RequiredFieldValidator>
        </li>
        <asp:UpdatePanel ID="upCondPosseTerra" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li style="">
                    <label class="lblObrigatorio" title="Localização da Moradia">
                        Localização</label>
                    <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLocalizaMoradia_OnSelectedIndexChanged"
                        ID="ddlLocalizaMoradia" Width="121px" ToolTip="Selecione Localização da Moradia">
                        <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                        <asp:ListItem Value="1">Urbana</asp:ListItem>
                        <asp:ListItem Value="2">Rural</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator InitialValue="" ID="rfvddlLocalizaMoradia" Display="Dynamic"
                        ValidationGroup="g1" runat="server" ControlToValidate="ddlLocalizaMoradia" Text="*"
                        ErrorMessage="Localização é requerida"></asp:RequiredFieldValidator>
                </li>
                <li style="">
                    <label class="lblObrigatorio" title="Condição de Posse e Uso da Terra">
                        Condição de Posse e Uso da Terra</label>
                    <asp:DropDownList runat="server" Enabled="false" ID="ddlCondPosseTerra" Width="163px"
                        ToolTip="Condição de Posse e Uso da Terra">
                        <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                        <asp:ListItem Value="1">Proprietário</asp:ListItem>
                        <asp:ListItem Value="2">Parceiro(a) / Meeiro(a)</asp:ListItem>
                        <asp:ListItem Value="3">Assentado(a)</asp:ListItem>
                        <asp:ListItem Value="4">Posseiro</asp:ListItem>
                        <asp:ListItem Value="5">Arrendatário(a)</asp:ListItem>
                        <asp:ListItem Value="6">Comodatário(a)</asp:ListItem>
                        <asp:ListItem Value="7">Beneficiário(a) do Banco da Terra</asp:ListItem>
                        <asp:ListItem Value="8">Não se aplica</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator InitialValue="" ID="rfvddlCondPosseTerra" Display="Dynamic"
                        ValidationGroup="g1" runat="server" ControlToValidate="ddlCondPosseTerra" Text="*"
                        ErrorMessage=" Selecione a Condição de Posse e Uso da Terra"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li style="clear: both; margin-top: 18px;">
            <label style="display: inline-block;" class="" title="Número de Moradores na Moradia">
                Nº de Moradores</label>
            <asp:TextBox runat="server" CssClass="digDois" Style="display: inline-block list-item;"
                ID="txtNumMoradores" Width="15px" MaxLength="2" ToolTip="Insira o número de moradores que se encontram na moradia"></asp:TextBox>
        </li>
        <li style="margin-top: 18px;">
            <label style="display: inline-block;" class="" title="Número de Cômodos na Moradia">
                Nº de Cômodos</label>
            <asp:TextBox runat="server" CssClass="digDois" ID="txtNumComodos" Width="15px" MaxLength="2"
                ToolTip="Insira o número de cômodos que se encontram na moradia"></asp:TextBox>
        </li>
        <li style="margin-top: 18px;">
            <label style="display: inline-block;" class="" title="Disponibilidade de Energia Elétrica?">
                Energia Elétrica?</label>
            <asp:CheckBox Style="margin-left: -5px;" ID="chkEnergiaEletrica" runat="server" ToolTip="Marque se houver a disponibilidade de energia elétrica no local" />
        </li>
        <li style="margin-top: 5px; margin-left: 48px;">
            <label class="" title="Tipo de Acesso ao Domicílio">
                Tipo de Acesso ao Domicílio</label>
            <asp:DropDownList runat="server" ID="ddlAcessoMoradia" Width="119px" ToolTip="Selecione o Tipo de Acesso ao Domicílio">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Pavimento</asp:ListItem>
                <asp:ListItem Value="2">Chão Batido</asp:ListItem>
                <asp:ListItem Value="3">Fluvial</asp:ListItem>
                <asp:ListItem Value="4">Outro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label class="" title="Material Predominante na Construção das Paredes Externas do Domicílio">
                Paredes Externas do Domicílio</label>
            <asp:DropDownList runat="server" ID="ddlParedeExterna" Width="132px" ToolTip="Selecione o Material Predominante na Construção das Paredes Externas do Domicílio">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Avenaria/Tijolo Com Revestimento</asp:ListItem>
                <asp:ListItem Value="2">Avenaria/Tijolo Sem Revestimento</asp:ListItem>
                <asp:ListItem Value="3">Taipa Com Revestimento</asp:ListItem>
                <asp:ListItem Value="4">Taipa Sem Revestimento</asp:ListItem>
                <asp:ListItem Value="5">Madeira Aparelhada</asp:ListItem>
                <asp:ListItem Value="6">Material Aproveitado</asp:ListItem>
                <asp:ListItem Value="7">Palha</asp:ListItem>
                <asp:ListItem Value="8">Outro Material</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px;">
            <label class="" title="Abastecimento de Água">
                Abastecimento de Água</label>
            <asp:DropDownList runat="server" ID="ddlAbastAgua" Width="102px" ToolTip="Selecione a forma de abastecimento de água da moradia">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Rede Encanado até o Domicílio</asp:ListItem>
                <asp:ListItem Value="2">Poço / Nascente no Domicílio</asp:ListItem>
                <asp:ListItem Value="3">Cisterna</asp:ListItem>
                <asp:ListItem Value="4">Carro Pipa</asp:ListItem>
                <asp:ListItem Value="5">Outro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px;">
            <label class="" title="Tratamento de Água no Domicílio">
                Tratamento de Água</label>
            <asp:DropDownList runat="server" ID="ddlTratAgua" Width="93px" ToolTip="Selecione a forma de Tratamento de Água no Domicílio">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Filtração</asp:ListItem>
                <asp:ListItem Value="2">Fervura</asp:ListItem>
                <asp:ListItem Value="3">Clonação</asp:ListItem>
                <asp:ListItem Value="4">Sem Tratamento</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 5px;">
            <label class="" title="Forma de Escoamento do Banheiro ou Sanitário">
                Escoamento do Sanitário</label>
            <asp:DropDownList runat="server" ID="ddlEscoaSanitario" Width="113px" ToolTip="Selecione a forma de Escoamento do Banheiro ou Sanitário">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Rede Coletora de Esgoto ou Pluvial</asp:ListItem>
                <asp:ListItem Value="2">Fossa Séptica</asp:ListItem>
                <asp:ListItem Value="3">Fossa Rudimentar</asp:ListItem>
                <asp:ListItem Value="4">Direto para um Rio, Lago ou Mar</asp:ListItem>
                <asp:ListItem Value="5">Céu Aberto</asp:ListItem>
                <asp:ListItem Value="5">Outra Forma</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label class="" title="Destino do Lixo">
                Destino do Lixo</label>
            <asp:DropDownList runat="server" ID="ddlDestinoLixo" Width="93px" ToolTip="Selecione o Destino do Lixo do Domicílio">
                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                <asp:ListItem Value="1">Coletado</asp:ListItem>
                <asp:ListItem Value="2">Queimado/ Enterrado</asp:ListItem>
                <asp:ListItem Value="3">Céu Aberto</asp:ListItem>
                <asp:ListItem Value="4">Outro</asp:ListItem>
            </asp:DropDownList>
        </li>
        <asp:UpdatePanel ID="upAnimais" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li style="margin-top: 18px;">
                    <label style="display: inline-block;" class="" title="Animais no Domicílio?">
                        Possui Animais?</label>
                    <asp:CheckBox Style="margin-left: -5px;" OnCheckedChanged="chkAnimais_OnCheckedChanged"
                        AutoPostBack="true" ID="chkAnimais" runat="server" ToolTip="Marque se houver Animais no Domicílio" />
                </li>
                <li style="margin-top: 5px;">
                    <label style="" class="" title="">
                        Quais?</label>
                    <label style="display: inline-block;" class="" title="Possui gato(s)">
                        Gato</label>
                    <asp:CheckBox Style="margin-left: -5px;" Enabled="false" ID="chkGato" runat="server" />
                    <label style="display: inline-block;" class="" title="Possui Cachorro(s)">
                        Cachorro</label>
                    <asp:CheckBox Style="margin-left: -5px;" Enabled="false" ID="chkCachorro" runat="server" />
                    <label style="display: inline-block;" class="" title="Possui Pássaro(s)">
                        Pássaro</label>
                    <asp:CheckBox Style="margin-left: -5px;" Enabled="false" ID="chkPassaro" runat="server" />
                    <label style="display: inline-block;" class="" title="Possui outros animais">
                        Outros</label>
                    <asp:CheckBox Style="margin-left: -5px;" Enabled="false" ID="chkOutros" runat="server" />
                    <label style="display: inline-block;" class="" title="Quantidade de Animais">
                        Qnt</label>
                    <asp:TextBox runat="server" CssClass="digTres" Enabled="false" Style="display: inline-block list-item;"
                        ID="txtQntAnimais" Width="20px" MaxLength="3" ToolTip="Insira o número de animais que se encontram na moradia"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -8px; margin-left: 209px;">
                    <label style="display: inline-block;" class="" title="Possui algum animal de criação">
                        De Criação (porco, galinha...)</label>
                    <asp:CheckBox Style="margin-left: -5px;" Enabled="false" ID="chkCriacao" runat="server" />
                </li>
                <li style="margin-left: -360px; margin-top: 15px;">
                    <ul class="ulDados" style="width: 700px; height: 85px; margin-top: 10px !important;">
                        <li>
                            <ul style="width: 666px;">
                                <li style="height: 20px !important; width: 666px; background-color: #EEEEE0; text-align: center;
                                    float: left; margin-bottom: 7px; margin-left: 0px;">
                                    <label style="font-family: Tahoma; font-weight: bold; float: left; margin: 3px 0 0 1px;">
                                        FAMÍLIAS</label>
                                </li>
                                <li id="li11" runat="server" title="Clique para adicionar uma familia" class="liBtnAddA"
                                    style="float: right; margin: -20px 0px 3px 0px; width: 61px">
                                    <img alt="" style="margin: -1px 0 1px 0;" title="Adicionar Familia" src="../../../../../Library/IMG/Gestor_SaudeEscolar.png"
                                        height="15px" width="15px" />
                                    <asp:LinkButton ID="lnkAddFamilia" runat="server" OnClick="lnkAddFamilia_OnClick">Adicionar</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <asp:UpdatePanel ID="upGridFamilia" runat="server">
                                <ContentTemplate>
                                    <div style="width: 665px; height: 110px; border: 1px solid #CCC; overflow-y: scroll">
                                        <asp:GridView ID="grdFamilias" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            cursor: default;" AutoGenerateColumns="false" AllowPaging="false" GridLines="Vertical"
                                            ClientIDMode="Static" ShowHeaderWhenEmpty="true">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhuma família associada<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Width="0px" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hidCoFamilia" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prontuário">
                                                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumProntFamili" CssClass="" Width="100%" MaxLength="10" Style="margin-left: -4px;
                                                            margin-bottom: 0px;" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cartão SUS">
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumCartaoSUSRespo" CssClass="cartSus" MaxLength="15" Width="100%"
                                                            Style="margin-left: -4px; margin-bottom: 0px;" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Data Nascimento">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtDataNasciRespo" Width="100%" Style="margin-left: -4px;
                                                            margin-bottom: 0px;" CssClass="campoData" ToolTip="Data do cadastro domiciliar"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Renda Familiar">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlRendaFamili" Width="100%" Style="margin-left: -4px;
                                                            margin-bottom: 0px;" ToolTip="Selecione o Destino do Lixo do Domicílio">
                                                            <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                                                            <asp:ListItem Value="1">1/4</asp:ListItem>
                                                            <asp:ListItem Value="2">1/2</asp:ListItem>
                                                            <asp:ListItem Value="3">1</asp:ListItem>
                                                            <asp:ListItem Value="4">2</asp:ListItem>
                                                            <asp:ListItem Value="5">3</asp:ListItem>
                                                            <asp:ListItem Value="6">4</asp:ListItem>
                                                            <asp:ListItem Value="7">+</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quant. Membros">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtMembrosFamili" MaxLength="2" CssClass="digDois" Width="100%"
                                                            Style="margin-left: -4px; margin-bottom: 0px;" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reside Desde">
                                                    <%--<HeaderTemplate>
                                                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="QTP" ToolTip="Quantidade de Parcelas"></asp:Label>
                                            </HeaderTemplate>--%>
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtResideDesde" CssClass="campoMesAno" Width="100%" Style="margin-left: -4px;
                                                            margin-bottom: 0px;" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mudou-se">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMudou" runat="server" Style="margin-left: -4px; margin-bottom: 0px;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX">
                                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="imgExcFamilia" ImageUrl="/Library/IMG/Gestor_BtnDel.png"
                                                            ToolTip="Excluir Cartão" OnClick="imgExcFamilia_OnClick" OnClientClick="return confirm ('Tem certeza de que deseja excluir da grade ?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoHora").mask("99:99");
            $('.campoTelefone').focusout(function () {
                var phone, element;
                element = $(this);
                element.unmask();
                phone = element.val().replace(/\D/g, '');
                if (phone.length > 10) {
                    element.mask("(99) 99999-999?9");
                } else {
                    element.mask("(99) 9999-9999?9");
                }
            }).trigger('focusout');
            $(".campoCEP").mask("99999-999");
            $(".campoMesAno").mask("99/9999");
            $(".cartSus").mask("?999999999999999");
            $(".CNES").mask("?9999999");
            $(".equipe").mask("?9999999999");
            $(".digDois").mask("?99");
            $(".digTres").mask("?999");
            $(".digCinco").mask("?99999");
        });
    </script>
</asp:Content>
