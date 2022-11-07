<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelFuncParam.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7300_CtrlCadastralInforFunc._7399_Relatorios.RelFuncParam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mainDiv
        {
            width: 600px;
            margin-top: 25px;
            margin-left: 300px;
            float: left;
        }
        
        .divParam
        {
            width: 200px;
            height: 200px;
            margin-top: 20px;
        }
        
        .divLogistica
        {
            width: 200px;
            height: 200px;
            margin-top: 20px;
        }
        
        .ulInfos
        {
            width: 10px;
            margin-top: 65px;
            text-align: right;
        }
        .liboth
        {
            clear: both;
        }
        .lblsub
        {
            color: #436EEE;
        }
        .liEspaco
        {
            margin-left: 10px;
        }
        
        .liEspaco1
        {
            margin-left: 9px;
        }
        .lblMain
        {
            color: #FF8C00;
            clear: both;
        }
        .divUnidade
        {
            width: auto;
            height: 80px;
        }
        
        .ulDados
        {
            margin-top: 10px;
            margin-left: 31%;
            width: 500px !important;
        }
        .liUnidade, .liNumDoc
        {
            margin-top: 5px;
            width: 275px;
        }
        .liAnoRefer, .liTurma, .liStaDocumento
        {
            margin-top: 5px;
        }
        
        .Regiao
        {
            width: 140px;
        }
        .sobe
        {
            margin-top: -7px;
        }
        .sobeEsp
        {
            margin-top: -7px;
            margin-left: 10px;
        }
        .liRegiao
        {
            width: 30px;
            margin-top: 5px;
        }
        .liSerie
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .liUnidContrato
        {
            margin-top: 5px;
            width: 300px;
        }
        .ddlUnidContrato
        {
            width: 226px;
        }
        .liPesqPor
        {
            margin-top: 5px;
            width: 115px;
            display: inline-block;
        }
        .liSup
        {
            margin-top: 5px;
        }
        .liLblMain
        {
            margin-top: 0px;
        }
        .lblDivData
        {
            margin: 0 5px;
            margin-top: 0px;
            height: 27px;
        }
        .liNumDoc
        {
            display: inline-block;
            margin-top: 5px;
            width: 100px;
            height: 27px;
        }
        .ddlTipoDoctos
        {
            width: 120px;
        }
        .liAluno
        {
            margin-top: 5px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .ddlOrigPagto
        {
            width: 100px;
        }
        .chk label
        {
            display:inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liLblMain" style="width: 400px; margin-top: 15px;">
            <asp:Label class="lblMain" runat="server" ID="lblUnidPes">UNIDADES DE PESQUISA</asp:Label><br />
        </li>
        <li style="width: 400px;">
            <asp:Label runat="server" ID="lblUnidCadastro" class="lblObrigatorio">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" CssClass="ddlUnidCadastro" Width="210px" ID="ddlUnidCadastro"
                ToolTip="Selecione a UNidade de Cadastro">
            </asp:DropDownList>
        </li>
        <li class="liUnidContrato" style="margin: 5px 0 0 0; width: 265px;">
            <label id="Label3" class="lblObrigatorio" runat="server" title="Unidade de Contrato">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" ToolTip="Selecione a Unidade de Contrato"
                Width="210px" CssClass="ddlUnidContrato" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUnidade" runat="server" ControlToValidate="ddlUnidContrato"
                ErrorMessage="Informe a unidade de contrato">*</asp:RequiredFieldValidator>
        </li>
        <li style="width: 400px; margin-top: 7px;">
            <asp:Label class="lblMain" runat="server" ID="lbllogis">LOGÍSTICA</asp:Label>
        </li>
        <li class="Regiao" runat="server" id="liModalidade">
            <label title="Regiao" for="ddlReg" class="lblObrigatorio">
                Região</label>
            <asp:DropDownList ID="ddlReg" runat="server" AutoPostBack="true" Width="140px" ToolTip="Selecione a Região">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" ControlToValidate="ddlReg"
                ErrorMessage="Informe a Região">*</asp:RequiredFieldValidator>
        </li>
        <li class="liSerie" style="margin: 0 0 0 10px; width: 140px;" runat="server" id="liSerie">
            <label title="Area" for="ddlArea" class="lblObrigatorio">
                Área</label>
            <asp:DropDownList ID="ddlArea" ToolTip="Selecione a Área" CssClass="ddlSerieCurso"
                Width="140px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCurso" runat="server" ControlToValidate="ddlArea"
                ErrorMessage="Informe a Área">*</asp:RequiredFieldValidator>
        </li>
        <li class="liTurma" style="margin: 0 50px 0 15px; width: 140px;" runat="server" id="liTurma">
            <label id="lblTurma" title="Subarea" for="ddlSubArea" class="lblObrigatorio">
                Subarea</label>
            <asp:DropDownList ID="ddlSubArea" ToolTip="Selecione a Subarea" Width="140px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSubarea" runat="server" ControlToValidate="ddlSubArea"
                ErrorMessage="Informe a Subarea">*</asp:RequiredFieldValidator>
        </li>
        <li class="sobe" runat="server" id="li1" style="width: 50px;">
            <label title="UF" for="ddlReg" class="lblObrigatorio">
                UF</label>
            <asp:DropDownList ID="ddlUF" Width="50px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUF_SelectedIndexChanged"
                ToolTip="Selecione a UF">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUF" runat="server" ControlToValidate="ddlUF" ErrorMessage="Informe a UF">*</asp:RequiredFieldValidator>
        </li>
        <li class="sobeEsp" style="width: 190px;" runat="server" id="li2">
            <label title="Cidade" for="ddlCidade" class="lblObrigatorio">
                Cidade</label>
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione a Área" Width="190px" runat="server"
                OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="ddlCidade"
                ErrorMessage="Informe a Cidade">*</asp:RequiredFieldValidator>
        </li>
        <li class="sobeEsp" style="width: 190px;" runat="server" id="li3">
            <label id="Label2" title="Bairro" for="ddlSubArea" class="lblObrigatorio">
                Bairro</label>
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione o Bairro" Width="190px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rvfBairro" runat="server" ControlToValidate="ddlBairro"
                ErrorMessage="Informe o Bairro">*</asp:RequiredFieldValidator>
        </li>
        <li style="width: 400px; margin-top: -3px;">
            <asp:Label class="lblMain" runat="server" ID="Label1">PARÂMETROS</asp:Label>
        </li>
        <li class="lblsub" style="clear: both; margin: 0 0 2px 0; width: 450px;">
            <label for="ddlAgrupador" title="Contexto de Atividades Funcional">
                Contexto Funcional</label>
        </li>
        <li style="width: 137px;">
            <label title="Classificação Funcional">
                Classificação Funcional</label>
            <asp:DropDownList ID="ddlClassFuncion" ToolTip="Selecione a Classificação Funcional"
                Width="137px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDoc" runat="server" ControlToValidate="ddlClassFuncion"
                ErrorMessage="Informea Classificação Funcional">*</asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco" style="width: 147px;">
            <label title="Categoria">
                Categoria
            </label>
            <asp:DropDownList ID="ddlCategoria" Width="147px" ToolTip="Selecione a Categoria"
                CssClass="ddlOrigPagto" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="ddlCategoria"
                ErrorMessage="Informe a Categoria">*</asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco" style="width: 147px; margin: 0 10px 13px 10px;">
            <label for="ddlSituacao" title="Situação Atual">
                Situação Atual</label>
            <asp:DropDownList ID="ddlSituacao" Width="147px" ToolTip="Selecione a Situação Atual do Funcionário"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="ATI">Atividade Interna</asp:ListItem>
                <asp:ListItem Value="ATE">Atividade Externa</asp:ListItem>
                <asp:ListItem Value="FCE">Cedido</asp:ListItem>
                <asp:ListItem Value="FES">Estagiário</asp:ListItem>
                <asp:ListItem Value="LFR">Licença Funcional</asp:ListItem>
                <asp:ListItem Value="LME">Licença Médica</asp:ListItem>
                <asp:ListItem Value="LMA">Licença Maternidade</asp:ListItem>
                <asp:ListItem Value="SUS">Suspenso</asp:ListItem>
                <asp:ListItem Value="TRE">Treinamento</asp:ListItem>
                <asp:ListItem Value="FER">Férias</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="sobe" style="width: 130px;">
            <asp:Label runat="server">Grupo da Especialização</asp:Label>
            <asp:DropDownList ID="ddlGrpEspec" runat="server" Width="130px" ToolTip="Selecione o Grupo da Especialização"
                OnSelectedIndexChanged="ddlGrpEspec_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="sobeEsp" style="width: 170px;">
            <asp:Label runat="server">Especialização</asp:Label>
            <asp:DropDownList ID="ddlEspecia" Width="170px" ToolTip="Selecione a Especialização"
                runat="server">
            </asp:DropDownList>
        </li>
        <li class="lblsub" style="clear: both; width: 500px; margin: 8px 0 2px 0;">
            <label for="ddlAgrupador" title="Contexto de Atividades Funcional">
                Contexto de Atividade Funcional</label>
        </li>
        <li style="width: 120px;">
            <asp:CheckBox runat="server" ID="chkAtInter" /><asp:Label runat="server">Atividade Interna</asp:Label>
        </li>
        <li style="width: 120px;">
            <asp:CheckBox runat="server" ID="chkAtiExt" /><asp:Label ID="Label5" runat="server">Atividade Externa</asp:Label>
        </li>
        <li style="width: 120px;">
            <asp:CheckBox runat="server" ID="chkAtiDomic" /><asp:Label runat="server">Atividade Domiciliar</asp:Label>
        </li>
        <li style="width: 120px;">
            <asp:CheckBox runat="server" ID="chkPlant" /><asp:Label runat="server">Plantonista</asp:Label>
        </li>
        <li class="lblsub" style="clear: both; margin: 8px 0 2px 0; width: 500px;">
            <label for="ddlAgrupador" title="Contexto de Atividades Funcional">
                Contexto de Impressão</label>
        </li>
        <li>
            <label>
                Tipo de Relatório</label>
            <asp:DropDownList runat="server" Width="166px" ID="ddlTipoPesq">
                <asp:ListItem Text="Por Unidade de Lotação/Contrato" Value="U2" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Por Unidade Funcional" Value="U"></asp:ListItem>
                <asp:ListItem Text="Por Função" Value="F"></asp:ListItem>
                <asp:ListItem Text="Por Cidade/Bairro" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin: 13px 0 0 10px;">
            <asp:CheckBox runat="server" CssClass="chk" ID="chkEmiteGraf" Checked="true" Text="Gráfico" ToolTip="Quando selecionado, apresenta os gráficos no Relatório" />
        </li>
        <li style="margin: 13px 0 0 10px;">
            <asp:CheckBox runat="server" CssClass="chk" ID="chkComRelatorio" Checked="true" Text="Relatório"
                ToolTip="Quando selecionado, apresenta as informações do Relatório" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
