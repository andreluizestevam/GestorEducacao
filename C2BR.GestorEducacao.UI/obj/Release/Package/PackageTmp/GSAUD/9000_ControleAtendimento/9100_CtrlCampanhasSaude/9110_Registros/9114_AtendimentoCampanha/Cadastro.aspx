<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9114_AtendimentoCampanha.Cadastro" %>

<%@ Register Src="~/Library/Componentes/ControleImagem.ascx" TagName="ControleImagem"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 980px;
        }
        .ulDados li
        {
            <%--margin:5px 0 0 5px;--%>;
        }
        .ulDadosLocal li
        {
            margin-bottom:-4px;
        }
        
        .ulDadosUsu li
        {
            margin-bottom:-7px;
            margin-left:3px;
        }
        
        input 
        {
            height:13px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .campoTel
        {
            width: 70px;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
            margin-left: -5px;
        }
        .divGridData
        {
            overflow-y: scroll;
        }
        
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
            width: 100% !important;
            horizontal-align: center;
        }
        .lblTitu
        {
            font-weight: bold;
            color: #FFA07A;
            margin-left: 0px;
        }
        .lblSubTitu
        {
            font-weight:lighter;
            color: #FFA07A;
            margin:8px 0 6px 0 !important;
            
        }
       .grdBusca th
        {
            background-color: #CCCCCC;
            color: Black;
            text-align: center;
        }
        .maskDin
        {
            width:50px;
            text-align:right;
        }
        .camp li
        {
            margin-left:5px;
            margin-top:5px;
        }
        .chk label
        {
            display:inline;
            margin-left:-3px;
        }
        .grdHisto
        {
            font-size:8px;
        }
        .grdHistoHead
        {
            font-size:8px;
            color:White !important;
            background-color:#a6a6a6 !important;
        }
        .liTracejada
        {
            clear: both;
            border-top:3px dashed #4682B4;
            width:413px; 
            height:0px;
            margin:0px 0 5px 0;
        }
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-top: 5px;
            margin-left: 295px !important;
            padding: 2px 3px 1px 3px;
            width:95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="margin-left: 0px !important; margin-top: -13px !important; width: 545px;
            height: auto; border-right: 1px solid #CCCCCC">
            <ul class="camp">
                <li class="liTituloGrid" style="width: 97%; height: 20px !important; margin-right: 0px;
                    margin-top: -0px; background-color: #ffff99; text-align: center; font-weight: bold;
                    margin-bottom: 5px">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 5px;">
                        CAMPANHA DE SAÚDE</label>
                </li>
                <li>
                    <label style="margin-bottom: -4px;" title="Informações para pesquisa por Campanhas de Saúde"
                        class="lblTitu">
                        Pesquisa</label>
                </li>
                <li style="clear: both">
                    <label title="Tipo da Campanha de Saúde">
                        Tipo</label>
                    <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                        Width="124px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Competência da Campanha de Saúde">
                        Competência</label>
                    <asp:DropDownList runat="server" ID="ddlCompetencia" Width="80px" ToolTip="Competência da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Classificação da Campanha de Saúde">
                        Classificação</label>
                    <asp:DropDownList runat="server" ID="ddlClassCamp" Width="94px" ToolTip="Classificação da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Seleciona a situação para pesquisa por plantões já agendados">
                        Situação</label>
                    <asp:DropDownList runat="server" ID="ddlSituCampSaude" Width="80px" ToolTip="Seleciona a situação para pesquisa por plantões já agendados">
                    </asp:DropDownList>
                </li>
                <li style="margin-top: 16px; margin-left: 0px;">
                    <asp:ImageButton ID="imgPesq" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgPesq_OnClick" />
                </li>
                <%--<asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <li style="margin-top: 10px !important">
                    <div id="div3" runat="server" class="divGridData" style="height: 180px; width: 528px;
                        border: 1px solid #ccc;">
                        <asp:HiddenField runat="server" ID="hidIdCampa" />
                        <asp:GridView ID="grdCampSaude" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: 20px;" AutoGenerateColumns="false" ToolTip="Grid de Campanhas de Saúde"
                            DataKeyNames="ID_CAMPAN" OnSelectedIndexChanged="grdCampSaude_SelectedIndexChanged"
                            AutoGenerateSelectButton="false" OnRowDataBound="grdCampSaude_RowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Campanha de Saúde encontrada.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField Visible="false" DataField="ID_CAMPAN" HeaderText="Cod." SortExpression="CO_EMP"
                                    HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                    <HeaderStyle CssClass="noprint"></HeaderStyle>
                                    <ItemStyle Width="20px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="CK">
                                    <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hidCoCampan" Value='<%# Eval("ID_CAMPAN") %>' runat="server" />
                                        <asp:CheckBox ID="chkSelectCamp" runat="server" OnCheckedChanged="chkSelectCamp_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="dataValid" HeaderText="DATA">
                                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA" HeaderText="HORA">
                                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="noCampa" HeaderText="NOME">
                                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo_Valid" HeaderText="TIPO">
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="comp_Valid" HeaderText="COMP">
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="classi_Valid" HeaderText="CLASS">
                                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:CommandField HeaderText="Nome" ShowSelectButton="false" Visible="False" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li>
                    <div style="width: 530px; height: 184px; margin: 2px 0 0 0;">
                        <div style="float: left; margin-left: -5px; border-right: 1px solid #CCCCCC; height: 100%;
                            width: 265px;">
                            <ul style="margin-top: -8px;" class="ulDadosLocal">
                                <li style="margin-top: 10px">
                                    <label style="margin-bottom: -4px;" title="Informações para pesquisa por Campanhas de Saúde"
                                        class="lblTitu">
                                        Local da Campanha</label>
                                </li>
                                <li style="margin: 10px 0 0 5px;">
                                    <asp:CheckBox runat="server" ID="chkEhUnidCadas" Text="É Unidade Cadastrada" CssClass="chk"
                                        ToolTip="Marque caso o local da campanha seja uma unidade já cadastrada" OnCheckedChanged="chkEhUnidCadas_OnCheckedChanged"
                                        AutoPostBack="true" Checked="true" />
                                </li>
                                <li runat="server" id="liUnidCampa">
                                    <label title="Unidade do atendimento da Campanha de Saúde">
                                        Unidade da Campanha</label>
                                    <asp:DropDownList runat="server" ID="ddlUnidCampan" Width="227px" ToolTip="Unidade do atendimento da Campanha de Saúde">
                                    </asp:DropDownList>
                                </li>
                                <li runat="server" id="liLocalCampa" visible="false" style="margin-bottom: -14px !important;">
                                    <label title="Nome do Local do atendimento da Campanha de Saúde">
                                        Nome do Local</label>
                                    <asp:TextBox runat="server" ID="txtNomeLocal" Width="227px" ToolTip="Nome do Local do atendimento da Campanha de Saúde"></asp:TextBox>
                                </li>
                                <li style="clear: both">
                                    <label title="UF do Local de atendimento da Campanha de Saúde">
                                        UF</label>
                                    <asp:DropDownList runat="server" ID="ddlUFLocal" Width="50px" ToolTip="UF do Local de atendimento da Campanha de Saúde"
                                        OnSelectedIndexChanged="ddlUFLocal_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label title="Cidade do Local de atendimento da Campanha de Saúde">
                                        Cidade</label>
                                    <asp:DropDownList runat="server" ID="ddlCidadeLocal" Width="140px" ToolTip="Cidade do Local de atendimento da Campanha de Saúde"
                                        OnSelectedIndexChanged="ddlCidadeLocal_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <li style="clear: both">
                                    <label title="Bairro do Local de atendimento da Campanha de Saúde">
                                        Bairro</label>
                                    <asp:DropDownList runat="server" ID="ddlBairroLocal" Width="150px" ToolTip="Bairro do Local de atendimento da Campanha de Saúde">
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-left: 20px;">
                                    <label title="Telefone do Local de atendimento da Campanha de Saúde">
                                        Telefone</label>
                                    <asp:TextBox runat="server" ID="txtTelLocal" CssClass="campoTel" ToolTip="Telefone do Local de atendimento da Campanha de Saúde"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -2px;">
                                    <label title="Endereço do Local de Atendimento da Campanha de Saúde">
                                        Endereço</label>
                                    <asp:TextBox runat="server" ID="txtEndeLocal" Width="245px" ToolTip="Endereço do Local de Atendimento da Campanha de Saúde"
                                        MaxLength="100"></asp:TextBox>
                                </li>
                                <li style="clear: both; margin-top: -2px;">
                                    <label>
                                        Observação</label>
                                    <asp:TextBox runat="server" ID="txtObservacao" Width="245px" TextMode="MultiLine"
                                        Height="25px" MaxLength="300"></asp:TextBox>
                                </li>
                            </ul>
                        </div>
                        <div style="float: right; width: 265px;">
                            <ul style="margin: -12px 0 0 2px;">
                                <li class="liTituloGrid" style="width: 99%; height: 20px !important; margin-right: 0px;
                                    margin-left: 0px !important; margin-bottom: -7px; text-align: center;">
                                    <label style="font-family: Tahoma; margin-top: 3px; color: #A4A4A4" title="Grid de Usuários atendidos na Campanhas de Saúde de saúde selecionada">
                                        RELAÇÃO DE USUÁRIOS ATENDIDOS</label>
                                </li>
                                <li>
                                    <div id="div1" runat="server" class="divGridData" style="height: 171px; width: 256px;
                                        border: 1px solid #ccc;">
                                        <asp:GridView ID="grdHistUsuariosAtendidos" CssClass="grdBusca" runat="server" Style="width: 100%;
                                            background-color: Black !important; height: 20px;" AutoGenerateColumns="false"
                                            ToolTip="Grid de Usuários atendidos na Campanhas de Saúde de saúde selecionada">
                                            <RowStyle CssClass="rowStyle" />
                                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                                            <EmptyDataTemplate>
                                                Nenhum Paciente atendido na Campanha de Saúde.<br />
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="dataValid" HeaderText="DATA/HORA">
                                                    <ItemStyle Width="20px" CssClass="grdHisto" HorizontalAlign="Center" />
                                                    <HeaderStyle CssClass="grdHistoHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_EMP" HeaderText="UNID">
                                                    <ItemStyle Width="20px" CssClass="grdHisto" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="grdHistoHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NO_ALU" HeaderText="CARTÃO / USUÁRIO">
                                                    <ItemStyle Width="50px" CssClass="grdHisto" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="grdHistoHead" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DE_ACAO" HeaderText="AÇÃO">
                                                    <ItemStyle Width="25px" CssClass="grdHisto" HorizontalAlign="Left" />
                                                    <HeaderStyle CssClass="grdHistoHead" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </li>
            </ul>
        </li>
        <li style="width: 420px; float: right;">
            <ul class="ulDadosUsu">
                <li class="liTituloGrid" style="width: 99%; height: 20px !important; margin-right: 0px;
                    margin-left: 0px !important; margin-bottom: 10px; background-color: #d2ffc2;
                    text-align: center; font-weight: bold; margin-top: -13px;">
                    <label style="font-family: Tahoma; font-weight: bold; margin-top: 3px;">
                        USUÁRIO DE CAMPANHA</label>
                    <asp:HiddenField runat="server" ID="hidCoAlu" />
                    <asp:HiddenField runat="server" ID="hidCoResp" />
                </li>
                <li style="clear: both">
                    <label style="margin-bottom: 1px;" title="Informações para pesquisa por Campanhas de Saúde"
                        class="lblTitu">
                        Pesquisa Usuário</label>
                </li>
                <li style="margin: 0px 0 0 5px;">
                    <asp:CheckBox runat="server" ID="chkEhUsuarCadastrado" Text="É Cadastrado" CssClass="chk"
                        ToolTip="Marque caso o local da campanha seja uma unidade já cadastrada" Checked="true"
                        OnCheckedChanged="chkEhUsuarCadastrado_OnCheckedChanged" AutoPostBack="true" />
                </li>
                <li style="margin: 9px 10px 0 -2px; clear: both"><a class="lnkPesPaci" href="#">
                    <img class="imgPesRes" src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade"
                        style="width: 18px; height: 18px;" /></a> </li>
                <li>
                    <label>
                        Nº NIS</label>
                    <asp:CheckBox runat="server" ID="chkPesqNire" Style="margin-left: -5px" Checked="true"
                        OnCheckedChanged="chkPesqNire_OnCheckedChanged" AutoPostBack="true" ToolTip="Selecione para pesquisar por NIS" />
                    <asp:TextBox runat="server" ID="txtNirePaci" CssClass="txtNireAluno" Width="80px"
                        Style="margin-left: -7px" Enabled="true" ToolTip="NIS que sera pesquisado"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Nº Cartão Saúde</label>
                    <asp:CheckBox runat="server" ID="chkPesqCartSaude" Style="margin-left: -5px" Checked="true"
                        OnCheckedChanged="chkPesqCartSaude_OnCheckedChanged" AutoPostBack="true" ToolTip="Selecione para pesquisar pelo Número do Cartão de Saúde" />
                    <asp:TextBox runat="server" ID="txtCartSaudePesq" CssClass="txtNireAluno" Width="80px"
                        Style="margin-left: -7px" Enabled="true" ToolTip="NIS que sera pesquisado"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Nº CPF</label>
                    <asp:CheckBox runat="server" ID="chkPesqCpf" Style="margin-left: -5px" OnCheckedChanged="chkPesqCpf_OnCheckedChanged"
                        AutoPostBack="true" ToolTip="Selecione para pesquisar por CPF" />
                    <asp:TextBox runat="server" ID="txtCPFPaci" Width="75px" CssClass="campoCpf" Style="margin-left: -7px"
                        Enabled="false" ToolTip="CPF que sera pesquisado"></asp:TextBox>
                </li>
                <li style="margin-top: 9px; margin-left: -4px;">
                    <asp:ImageButton ID="imgCpfResp" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                        OnClick="imgCpfResp_OnClick" />
                </li>
                <li class="liTracejada"></li>
                <li style="clear: both">
                    <label style="margin-bottom: 1px;" title="Informações do Usuário de Campanha de Saúde"
                        class="lblSubTitu">
                        Dados do Usuário de Saúde</label>
                </li>
                <li style="clear: both" runat="server" id="liUsuario">
                    <label>
                        Nome</label>
                    <asp:DropDownList ID="ddlNomeUsu" runat="server" ToolTip="Informe o usuário" Width="220px"
                        OnSelectedIndexChanged="ddlNomeUsu_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="clear: both" runat="server" visible="false" id="liNomeUsuario">
                    <label class="lblObrigatorio" title="Nome do Usuário da Campanha de Saúde">
                        Nome</label>
                    <asp:TextBox runat="server" ID="txtNomePaciente" Width="220px" Enabled="false" ToolTip="Nome do Usuário da Campanha de Saúde"></asp:TextBox>
                </li>
                <li>
                    <label class="lblObrigatorio" title="Sexo do Usuário da Campanha de Saúde">
                        Sexo</label>
                    <asp:DropDownList runat="server" ID="ddlSexoUsu" Width="44px" Enabled="false" ToolTip="Sexo do Usuário da Campanha de Saúde">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Masc" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label runat="server" ID="lbldtNascPac">Nascimento</asp:Label><br />
                    <asp:TextBox runat="server" ID="txtDtNascUsu" CssClass="campoData" Enabled="false"
                        OnTextChanged="txtDtNasc_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                </li>
                <li style="width: 30px; margin-left: 2px;" runat="server" visible="false">
                    <label>
                        Idade</label>
                    <asp:Label runat="server" ID="lblIdade"></asp:Label>
                </li>
                <li style="clear: both">
                    <label>
                        Nº Cartão Saúde</label>
                    <asp:TextBox runat="server" ID="txtNuCarSaude" Width="70px" Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="CPF do Usuário da Campanha de Saúde">
                        CPF</label>
                    <asp:TextBox runat="server" ID="txtCPFUsuaInfo" CssClass="campoCpf" Width="70px"
                        Enabled="false" ToolTip="CPF do Usuário da Campanha de Saúde"></asp:TextBox>
                </li>
                <li runat="server" id="li34" visible="false">
                    <label for="txtNisUsu" title="Número NIS do usuário selecionado">
                        N° NIS</label>
                    <asp:TextBox ID="txtNisUsu" Enabled="false" runat="server" ToolTip="Número NIS do usuário selecionado"
                        Width="90px">
                    </asp:TextBox>
                </li>
                <li>
                    <label title="RG do Usuário da Campanha de Saúde">
                        Nº RG</label>
                    <asp:TextBox runat="server" ID="txtRGUsu" ToolTip="RG do Usuário da Campanha de Saúde"
                        Width="60px" Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="Órgão de emissão do RG do Usuário da Campanha de Saúde">
                        Órgão</label>
                    <asp:TextBox runat="server" ID="txtOrgRgUsu" Width="45px" ToolTip="Órgão de emissão do RG do Usuário da Campanha de Saúde"
                        Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="UF de emissão do RG do Usuário da Campanha de Saúde">
                        UF</label>
                    <asp:DropDownList runat="server" ID="ddlUfRgUsu" ToolTip="UF de emissão do RG do Usuário da Campanha de Saúde"
                        Width="40px" Enabled="false">
                    </asp:DropDownList>
                </li>
                <li style="margin-left: 10px;">
                    <label title="Deficiência do Usuário da Campanha de Saúde" class="lblObrigatorio">
                        Deficiência</label>
                    <asp:DropDownList runat="server" ID="ddlDeficUsu" Width="70.5px" ToolTip="Deficiência do Usuário da Campanha de Saúde"
                        Enabled="false">
                    </asp:DropDownList>
                </li>
                <li style="clear: both">
                    <label title="Telfone Fixo do Usuário da Campanha de Saúde">
                        Tel Fixo</label>
                    <asp:TextBox runat="server" ID="txtTelResUsu" CssClass="campoTel" ToolTip="Telfone Fixo do Usuário da Campanha de Saúde"
                        Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="Telefone Celular do Usuário da Campanha de Saúde">
                        Celular</label>
                    <asp:TextBox runat="server" ID="txtTelCelUsu" CssClass="campoTel" ToolTip="Telefone Celular do Usuário da Campanha de Saúde"
                        Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="Telefone do WhatsApp do Usuário da Campanha de Saúde">
                        Nº WhatsApp</label>
                    <asp:TextBox runat="server" ID="txtTelWhatsUsu" CssClass="campoTel" ToolTip="Telefone do WhatsApp do Usuário da Campanha de Saúde"
                        Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="Email do Usuário da Campanha de Saúde">
                        Email</label>
                    <asp:TextBox runat="server" ID="txtEmailUsu" ToolTip="Email do Usuário da Campanha de Saúde"
                        Width="170.5px" Enabled="false"></asp:TextBox>
                </li>
                <li style="clear: both; margin-top: -6px;">
                    <label style="margin-bottom: 1px;" title="Informações do Usuário de Campanha de Saúde"
                        class="lblSubTitu">
                        Endereço e Dados de Contato do Usuário de Saúde</label>
                </li>
                <li style="clear: both">
                    <label title="CEP do Endereço do Usuário da Campanha de Saúde">
                        CEP</label>
                    <asp:TextBox runat="server" ID="txtCepEndeUsu" Width="55px" CssClass="campoCep" Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label title="UF">
                        UF</label>
                    <asp:DropDownList ID="ddlUFUsu" ToolTip="Selecione a UF" CssClass="ddlUF" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlUFUsu_SelectedIndexChanged" Enabled="false">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Cidade do Usuário da Campanha de Saúde">
                        Cidade</label>
                    <asp:DropDownList ID="ddlCidadeUsu" Width="160" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCidadeUsu_SelectedIndexChanged" Enabled="false" ToolTip="Cidade do Usuário da Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Bairro do Usuário da Campanha de Saúde">
                        Bairro</label>
                    <asp:DropDownList ID="ddlBairroUsu" Width="131.5px" ToolTip="Bairro do Usuário da Campanha de Saúde"
                        CssClass="ddlBairroResp" runat="server" Enabled="false">
                    </asp:DropDownList>
                </li>
                <li style="clear: both">
                    <label title="Logradouro do Usuário da Campanha de Saúde">
                        Logradouro</label>
                    <asp:TextBox ID="txtLograUsu" CssClass="txtLogradouroResp" Width="240px" ToolTip="Logradouro do Usuário da Campanha de Saúde"
                        runat="server" MaxLength="40" Enabled="false"></asp:TextBox>
                </li>
                <li runat="server" id="liApenasCadNovo" style="margin: 14px 0 0 5px;" visible="false">
                    <asp:CheckBox runat="server" ID="chkUsuarioEhResponsavel" Text="Usuário é o(a) Responsável"
                        ToolTip="Marque caso o usuário em atendimento na campanha de saúde seja o próprio responsável"
                        CssClass="chk" OnCheckedChanged="chkUsuarioEhResponsavel_OnCheckedChanged" AutoPostBack="true" />
                </li>
                <li style="clear: both; margin-top: 0px;">
                    <label style="margin-bottom: 1px;" title="Informações do Responsável do Usuário de Campanha de Saúde"
                        class="lblTitu">
                        Responsável</label>
                </li>
                <li style="margin: 0px 0 0 5px;">
                    <asp:CheckBox runat="server" ID="chkEhRespCadastrado" Text="É Cadastrado" CssClass="chk"
                        ToolTip="Marque caso o Responsável já seja cadastrado" OnCheckedChanged="chkEhRespCadastrado_OnCheckedChanged"
                        AutoPostBack="true" Checked="true" />
                </li>
                <li style="clear: both" runat="server" id="liRespSelec">
                    <label title="Selecione o Responsável pelo Usuário em atendimento na Campanha de Saúde">
                        Nome</label>
                    <asp:DropDownList runat="server" ID="ddlResp" Width="187.5px" OnSelectedIndexChanged="ddlResp_OnSelectedIndexChanged"
                        AutoPostBack="true" ToolTip="Selecione o Responsável pelo Usuário em atendimento na Campanha de Saúde">
                    </asp:DropDownList>
                </li>
                <li style="clear: both" runat="server" id="liNomeResp" visible="false">
                    <label title="Nome do Responsável pelo Usuário em atendimento na Campanha de Saúde">
                        Nome</label>
                    <asp:TextBox runat="server" ID="txtNomeResp" Width="185px" ToolTip="Nome do Responsável pelo Usuário em atendimento na Campanha de Saúde"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Sexo</label>
                    <asp:DropDownList runat="server" ID="ddlSexResp" Width="50px" Enabled="false">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Masc" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Fem" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Data de Nascimento do Responsável pelo Usuário em atendimento">
                        Nascimento</label>
                    <asp:TextBox runat="server" ID="txtDtNascResp" CssClass="campoData" ToolTip="Data de Nascimento do Responsável pelo Usuário em atendimento"
                        Enabled="false"></asp:TextBox>
                </li>
                <li>
                    <label>
                        CPF</label>
                    <asp:TextBox runat="server" ID="txtCPFResp" CssClass="campoCpf" Width="75px" Enabled="false"></asp:TextBox>
                </li>
                <li class="liTracejada"></li>
                <li style="clear: both; margin: 5px 0 0 140px">
                    <label style="font-weight: bold; font-size: 10px; color: #27408b">
                        REGISTRO DE ATENDIMENTO</label>
                          
                </li>

                <li style="clear: both;">
                    <label>
                        Data</label>
                    <asp:TextBox runat="server" ID="txtDataAtend" CssClass="campoData"></asp:TextBox>
                </li>
                <li style="margin-right: 60px;">
                    <label>
                        Hora</label>
                    <asp:TextBox runat="server" ID="txtHoraAtend" Width="30px" CssClass="campoHora"></asp:TextBox>
                </li>
                <li runat="server" id="liAddVacina" visible="false">
                    <ul>
                        <li>
                            <label>
                                Vacina</label>
                          <asp:DropDownList runat="server" ID="ddlVacina" Width="180px">
                            </asp:DropDownList>
                        </li>
                        <li style="margin: 10px 0 0 -4px">
                            <asp:LinkButton ID="lnkIncVacina" runat="server" Style="margin: 0 auto;" ToolTip="Incluir o Vacina na Grid"
                                OnClick="lnkIncVacina_OnClick">
                                <img class="imgLnkInc" src='/Library/IMG/Gestor_CheckSucess.png' alt="Icone Pesquisa"
                                    title="Incluir Registro" />
                                <asp:Label runat="server" ID="Label29" Text="Incluir"></asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </li>
                <li style="clear: both" runat="server" id="liErros" visible="false">
                    <div clientidmode="Static" id="lblsErros" style="text-align: center; width: 411px;
                        margin: 22px 0 22px 0;">
                        <asp:Label runat="server" ID="lblInfoTop" Style="color: Red"></asp:Label><br />
                        <asp:Label runat="server" ID="lblInfoBot" Style="color: Blue; font-size: 9px;"></asp:Label>
                    </div>
                </li>
                <li style="margin-top: 0px !important; clear: both; height: 67px; width: 410px;"
                    runat="server" id="liGridVacinas" visible="false" clientidmode="Static">
                    <div runat="server" class="divGridData" style="height: 100%; width: 410px; border: 1px solid #ccc; margin-top:-0px !important;"
                        clientidmode="Static" id="divGrdVacinas">
                        <asp:GridView ID="grdVacinas" CssClass="grdBusca" runat="server" Style="width: 100%;
                            height: auto;" AutoGenerateColumns="false" ToolTip="Grid de vacinas administradas em campanha de saúde"
                            DataKeyNames="ID_VACINA">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhuma Vacina administrada.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="numero" HeaderText="Nº">
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sigla" HeaderText="SIGLA">
                                    <ItemStyle Width="25px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VACINA" HeaderText="VACINA">
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="EXCLUIR">
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExc" OnClientClick="if (!confirm('Deseja realmente Excluir o registro?')) return false;"
                                            runat="server" Style="margin: 0 auto;" ToolTip="Excluir Medicamento da Grid"
                                            OnClick="lnkExc_OnClick">
                                    <img class="imgLnkExc" src='/Library/IMG/Gestor_BtnDel.png' alt="Icone Pesquisa"
                                        title="Excluir Registro" />
                                        </asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hidIdVacina" Value='<%# Bind("ID_VACINA") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li id="li19" runat="server" class="liBtnAddA liPrima" style="clear: both !important;
                    margin-top: 10px !important; margin-left: 312px !important; float: left;">
                    <asp:LinkButton ID="lnkImpGuiaExame" Enabled="true" runat="server" ValidationGroup="atuEndAlu"
                        OnClick="lnkImpGuiaExame_OnClick">
                        <asp:Label runat="server" ID="Label34" Text="NOVA CAMPANHA" Style="margin-left: 4px;"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </li>
        <li>
            <div id="divLoadShowAlunos" style="display: none; height: 305px !important;" />
        </li>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            carregaCss();
        });

        function ErroVacinas() {
            //Em 5 milésimos, a grid de vacinas é escondida e o erro é apresentado
            setTimeout(function () {
                $("#liGridVacinas").fadeOut(function () {
                    $("#lblsErros").show("slow");
                    $("#ddlVacina").val(" ");
                });
            },500);

            //Executa depois de 11 segundos, para ocultar o erro e mostrar a tela normalizada
            setTimeout(function () {
                $("#lblsErros").fadeOut(function () {
                    $("#liGridVacinas").fadeIn();
//                    $("#liGridVacinas").css("margin-top", "20px");
                });
            }, 11000);
        }

        //Esconde a tela de erro
        function EscondeErro() {
            $("#lblsErros").hide();
            $("#divGrdVacinas").fadeIn();
            $("#divGrdVacinas").css("margin-top", "20px");
        }

        function carregaCss() {
            $(".campoCpf").mask("999.999.999-99");
            $(".campoTel").mask("(99)9999-9999");
            //            $(".campoData").datepicker();
            //            $(".campoData").mask("99/99/9999");
            $(".campoHora").mask("99:99");
            $(".campoCep").mask("99999-999");


            $(".lnkPesPaci").click(function () {
                $('#divLoadShowAlunos').dialog({ autoopen: false, modal: true, width: 690, height: 390, resizable: false, title: "LISTA DE PACIENTES",
                    open: function () { $('#divLoadShowAlunos').load("/Componentes/ListarPacientes.aspx"); }
                });
            });
        }

    </script>
</asp:Content>
