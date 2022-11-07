<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1314_ReativaAgendaAtividadeFinaCanc.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    input[type='text'] { margin-bottom: 4px; }
    select { margin-bottom: 4px; }
    label { margin-bottom: 1px; }
    
    .ulDados { width: 590px; }
    fieldset {margin: 5px 0 5px 5px !important;padding: 5px 3px 4px 8px;}
    
    .liDadosControle { width: 100%; }
    .txtMatricula { margin-right: 5px; width: 45px; }
    .txtEmissor { width: 210px; }
    .txtUnidade { width: 210px; }
    .txtNumTarefa { text-align: right; width: 50px; }
    .txtChaveUnica { text-align: right; width: 81px; }
    
    .liResponsavel { clear: both; margin-top: -4px; width: 100%; }
    .ddlUnidade { width: 210px; }
    .liDepartamento { margin-left: 12px }
    .ddlDepartamento { width: 150px; }
    .liFuncao { margin-left: 12px }
    .ddlFuncao { width: 150px; }
    .liClear { clear: both; }
    .txtTitulo { width: 180px; }
    .txtDescricao { width: 325px; height: 46px; }
    .liDtCadastro { margin-left: 3px }  
    .txtDtCompromisso { width: 58px; }
    .txtDtLimite { width: 58px; }
    .liStatusTarefa { margin: 31px 0 0 5px; }
    .liPrioridade { margin: 31px 0 0 5px; }
    .ddlPrioridade { width: 72px; } 
    .ddlNomeResponsavelReabertura { width: 210px; }
    .txtMotivoReabertura { height: 40px; width: 210px; }
    .liDtReabertura { margin-left: 12px }
    .txtDtReabertura { width: 58px; }
    .txtDtLimiteReabertura { width: 58px; }
    .ddlPrioridadeReabertura { width: 72px; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li class="liDadosControle">
    <fieldset id="fldDadosControle" class="fldDadosControle">
        <legend>Dados da Tarefa</legend>
        <ul>
            <li>
                <label for="txtMatEmissor">Matr&iacute;cula / Emissor</label>
                <asp:TextBox ID="txtMatEmissor" CssClass="txtMatricula" Enabled="false" runat="server" ToolTip="Matrícula do Emissor da Tarefa"></asp:TextBox>
                <asp:TextBox ID="txtEmissor" CssClass="txtEmissor" Enabled="false" runat="server" ToolTip="Emissor da Tarefa"></asp:TextBox>
            </li>
            <li>
                <label for="txtUnidadeEmissor">Unidade do Emissor</label>
                <asp:TextBox ID="txtUnidadeEmissor" CssClass="txtUnidade" Enabled="false" runat="server" ToolTip="Unidade/Escola do Emissor da Tarefa"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtMatResponsavel">Matr&iacute;cula / Respons&aacute;vel</label>
                <asp:TextBox ID="txtMatResponsavel" CssClass="txtMatricula" Enabled="false" runat="server" ToolTip="Matrícula do Responsável da Tarefa"></asp:TextBox>
                <asp:TextBox ID="txtNomeResponsavel" CssClass="txtEmissor" Enabled="false" runat="server" ToolTip="Nome do Responsável da Tarefa"></asp:TextBox>
            </li>
            <li>
                <label for="txtUnidadeResponsavel">Unidade do Respons&aacute;vel</label>
                <asp:TextBox ID="txtUnidadeResponsavel" CssClass="txtUnidade" Enabled="false" runat="server" ToolTip="Unidade do Responsável da Tarefa"></asp:TextBox>
            </li>
            <li class="liClear">
                <label for="txtTitulo">T&iacute;tulo da Tarefa</label>
                <asp:TextBox ID="txtTitulo" CssClass="txtTitulo" Enabled="false" runat="server" MaxLength="40" onKeyup="javascript:Maxlength(this, 40);" ToolTip="Título da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitulo" ErrorMessage="T&iacute;tulo deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <asp:Panel ID="pnlNumTarefa" CssClass="liNumTarefa" runat="server">
                <li id="liNumTarefa" class="liNumTarefa">
                    <label for="txtNumTarefa">N° Tarefa</label>
                    <asp:TextBox ID="txtNumTarefa" CssClass="txtNumTarefa" Enabled="false" runat="server" ToolTip="Número da Tarefa"></asp:TextBox>
                </li>
            </asp:Panel>
            <asp:Panel ID="pnlChaveUnica" CssClass="liChaveUnica" runat="server">
                <li id="liChaveUnica" class="liChaveUnica">
                    <label for="txtChaveUnica">Chave &Uacute;nica</label>
                    <asp:TextBox ID="txtChaveUnica" CssClass="txtChaveUnica" Enabled="false" runat="server" ToolTip="Chave Única da Tarefa"></asp:TextBox>
                </li>
            </asp:Panel>
            <li class="liDtCadastro">
                <label for="txtDtCadastro">Cadastro</label>
                <asp:TextBox ID="txtDtCadastro" CssClass="campoData" Enabled="false" runat="server" ToolTip="Data de Cadastro da Tarefa"></asp:TextBox>
            </li>
            <li>
                <label for="txtDtCompromisso">Compromisso</label>
                <asp:TextBox ID="txtDtCompromisso" CssClass="txtDtCompromisso campoData" Enabled="false" runat="server" ToolTip="Data do Compromisso da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtCompromisso" ErrorMessage="Data de Compromisso deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li>
                <label for="txtDtLimite">Limite</label>
                <asp:TextBox ID="txtDtLimite" CssClass="txtDtLimite campoData" Enabled="false" runat="server" ToolTip="Data Limite da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtLimite" ErrorMessage="Data Limite deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="txtDescricao">Descri&ccedil;&atilde;o</label>
                <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" MaxLength="200" onKeyup="javascript:MaxLength(this, 200);" ToolTip="Descrição da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descri&ccedil;&atilde;o deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liStatusTarefa">
                <label for="ddlStatusTarefa">Status</label>
                <asp:DropDownList ID="ddlStatusTarefa" CssClass="ddlStatusTarefa" Enabled="false" runat="server" ToolTip="Status da Tarefa">
                </asp:DropDownList>
            </li>
            <li class="liPrioridade">
                <label for="ddlPrioridade">Prioridade</label>
                <asp:DropDownList ID="ddlPrioridade" CssClass="ddlPrioridade" Enabled="false" runat="server" ToolTip="Prioridade da Tarefa">
                </asp:DropDownList>
            </li>
        </ul>
    </fieldset>
    </li>
    
    <li class="liResponsavel">
    <fieldset id="fldResponsavel" class="fldResponsavel">
    <legend>Informa&ccedil;&otilde;es de Reabertura</legend>
        <ul>
            <li>
                <label for="ddlUnidade" class="lblObrigatorio">Unidade/Escola</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" ToolTip="Informe a Unidade/Escola" 
                    onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidade" ErrorMessage="Unidade/Escola deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liDepartamento">
                <label for="ddlDepartamento">Departamento</label>
                <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" ToolTip="Informe o Departamento"
                    runat="server" onselectedindexchanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liFuncao">
                <label for="ddlFuncao">Fun&ccedil;&atilde;o</label>
                <asp:DropDownList ID="ddlFuncao" CssClass="ddlFuncao" runat="server"  ToolTip="Informe a Função"
                    onselectedindexchanged="ddlFuncao_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="ddlNomeResponsavelReabertura">Novo Respons&aacute;vel pela Tarefa</label>
                <asp:DropDownList ID="ddlNomeResponsavelReabertura" CssClass="ddlNomeResponsavelReabertura" ToolTip="Informe o novo Responsável pela Tarefa"
                    OnSelectedIndexChanged="ddlNomeResponsavel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNomeResponsavelReabertura" ErrorMessage="Respons&aacute;vel deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="txtMotivoReabertura" class="lblObrigatorio">Motivo</label>
                <asp:TextBox ID="txtMotivoReabertura" CssClass="txtMotivoReabertura" TextMode="MultiLine" Rows="4" runat="server" onKeyup="javascript:MaxLength(this, 100);" ToolTip="Informe o Motivo da Reabertura da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMotivoReabertura" ErrorMessage="Motivo da Reabertura deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li>
                <ul>
                <li class="liDtReabertura">
                    <label for="txtDtReabertura" class="lblObrigatorio">Data Reabertura</label>
                    <asp:TextBox ID="txtDtReabertura" CssClass="txtDtReabertura campoData" runat="server" ToolTip="Informe a Data da Reabertura da Tarefa"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtReabertura" ErrorMessage="Data de Reabertura deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="txtDtReabertura" ID="cvDataReabertura" runat="server"
                        ErrorMessage="Data de Reabertura não pode ser menor que a Data de Compromisso" Display="None"
                        CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataReabertura_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li>
                    <label for="txtDtLimiteReabertura" class="lblObrigatorio">Data Limite</label>
                    <asp:TextBox ID="txtDtLimiteReabertura" CssClass="txtDtLimiteReabertura campoData" runat="server" ToolTip="Informe a Data Limite da Reabertura da Tarefa"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtLimiteReabertura" ErrorMessage="Data Limite deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="txtDtLimiteReabertura" ID="cvDataLimiteReabertura" runat="server"
                        ErrorMessage="Data Limite de Reabertura deve ser maior ou igual a Data Limite original" Display="None"
                        CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLimiteReabertura_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li>
                    <label for="ddlPrioridadeReabertura" class="lblObrigatorio">Prioridade</label>
                    <asp:DropDownList ID="ddlPrioridadeReabertura" CssClass="ddlPrioridadeReabertura" runat="server" ToolTip="Informe a Prioridade da Tarefa">
                        <asp:ListItem Value="NEN">Nenhuma</asp:ListItem>
                        <asp:ListItem Value="NOR">Normal</asp:ListItem>
                        <asp:ListItem Value="MED">M&eacute;dia</asp:ListItem>
                        <asp:ListItem Value="CRI">Cr&iacute;tica</asp:ListItem>
                        <asp:ListItem Value="URG">Urgente</asp:ListItem>
                    </asp:DropDownList>
                </li>
                </ul>
            </li>
            
            <li>
                <label for="ddlEnviarSMS">Enviar Msg SMS</label>
                <asp:DropDownList ID="ddlEnviarSMS" CssClass="ddlEnviarSMS" Enabled="false" runat="server" ToolTip="Envio de SMS">
                    <asp:ListItem Value="False">N&atilde;o</asp:ListItem>
                    <asp:ListItem Value="True">Sim</asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ControlToValidate="ddlEnviarSMS" ID="cvCelularResponsavel" runat="server"
                    ErrorMessage="Não é possível enviar SMS. Celular não cadastrado ou inválido. Desmarque a opção" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvCelularResponsavel_ServerValidate">
                </asp:CustomValidator>
            </li>
        </ul>
    </fieldset>
    </li>
</ul>
</asp:Content>