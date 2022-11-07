<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1312_RepasseAgendaAtividade.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 590px; }
    input[type='text'] { margin-bottom: 4px; }
    select { margin-bottom: 4px; }
    label { margin-bottom: 1px; }
    fieldset {margin: 5px 0 5px 5px !important;padding: 5px 3px 4px 8px;}
    
    /*--> CSS LIs */
    .liDadosControle { width: 100%; }
    .liResponsavel { clear: both; margin-top: -4px; width: 100%; }
    .liDepartamento, .liFuncao, .liDtRepasse { margin-left: 12px }
    .liClear { clear: both; }
    .liDtCadastro { margin-left: 3px }  
    .liStatusTarefa, .liPrioridade { margin: 31px 0 0 5px; }
    
    /*--> CSS DADOS */
    .txtMatricula { margin-right: 5px; width: 45px; }
    .txtEmissor, .txtUnidade, .ddlUnidade, .ddlNomeResponsavelRepasse { width: 210px; }
    .txtNumTarefa { text-align: right; width: 50px; }
    .txtChaveUnica { text-align: right; width: 81px; }    
    .ddlDepartamento, .ddlFuncao { width: 150px; }    
    .txtTitulo { width: 180px; }
    .txtDescricao { width: 325px; height: 46px; }     
    .txtDtCompromisso, .txtDtLimite, .txtDtRepasse, .txtDtLimiteRepasse { width: 58px; }    
    .ddlPrioridade, .ddlPrioridadeRepasse { width: 72px; }
    .txtMotivoRepasse { height: 40px; width: 210px; }    
    
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
                <asp:TextBox ID="txtMatEmissor" CssClass="txtMatricula" Enabled="false" runat="server"  ToolTip="Matrícula do Emissor da Tarefa"></asp:TextBox>
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
                <asp:TextBox ID="txtTitulo" CssClass="txtTitulo" Enabled="false" runat="server" MaxLength="40" onKeyup="javascript:MaxLength(this,40);" ToolTip="Título da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitulo" ErrorMessage="T&iacute;tulo deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <asp:Panel ID="pnlNumTarefa" CssClass="liNumTarefa" runat="server">
                <li>
                    <label for="txtNumTarefa">N° Tarefa</label>
                    <asp:TextBox ID="txtNumTarefa" CssClass="txtNumTarefa" Enabled="false" runat="server" ToolTip="Número da Tarefa"></asp:TextBox>
                </li>
            </asp:Panel>
            <asp:Panel ID="pnlChaveUnica" CssClass="liChaveUnica" runat="server">
                <li>
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
                <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" MaxLength="200" onKeyup="javascript:Maxlegth(this, 200);" ToolTip="Descrição da tarefa"></asp:TextBox>
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
    <legend>Informa&ccedil;&otilde;es de Repasse</legend>
        <ul>
            <li>
                <label for="ddlUnidade" class="lblObrigatorio">Unidade/Escola</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server"  ToolTip="Selecione a Unidade/Escola"
                    onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidade" ErrorMessage="Unidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liDepartamento">
                <label for="ddlDepartamento">Departamento</label>
                <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" ToolTip="Selecione o Departamento"
                    runat="server" onselectedindexchanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liFuncao">
                <label for="ddlFuncao">Fun&ccedil;&atilde;o</label>
                <asp:DropDownList ID="ddlFuncao" CssClass="ddlFuncao" runat="server" ToolTip="Selecione a Função"
                    onselectedindexchanged="ddlFuncao_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="ddlNomeResponsavelRepasse">Novo Respons&aacute;vel pela Tarefa</label>
                <asp:DropDownList ID="ddlNomeResponsavelRepasse" CssClass="ddlNomeResponsavelRepasse" runat="server" ToolTip="Nome do Novo Responsável pela Tarefa">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNomeResponsavelRepasse" ErrorMessage="Respons&aacute;vel deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="txtMotivoRepasse" class="lblObrigatorio">Motivo</label>
                <asp:TextBox ID="txtMotivoRepasse" CssClass="txtMotivoRepasse" TextMode="MultiLine" Rows="4" runat="server" OnKeyup="javascript:MaxLength(this,100);" ToolTip="Informe o motivo do Repasse da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMotivoRepasse" ErrorMessage="Motivo do Repasse deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liDtRepasse">
                <ul>
                <li class="liDtRepasse">
                    <label for="txtDtRepasse" class="lblObrigatorio">Data Repasse</label>
                    <asp:TextBox ID="txtDtRepasse" CssClass="txtDtRepasse campoData" runat="server" ToolTip="Informe a Data de Repasse"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtRepasse" ErrorMessage="Data de Repasse deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="txtDtRepasse" ID="cvDataRepasse" runat="server"
                        ErrorMessage="Data de Repasse não pode ser menor que a Data de Compromisso" Display="None"
                        CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataRepasse_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li>
                    <label for="txtDtLimiteRepasse" class="lblObrigatorio">Data Limite</label>
                    <asp:TextBox ID="txtDtLimiteRepasse" CssClass="txtDtLimiteRepasse campoData" runat="server" ToolTip="Informe a Data Limite de Repasse"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtLimiteRepasse" ErrorMessage="Data Limite deve ser informada" Display="None"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ControlToValidate="txtDtLimiteRepasse" ID="cvDataLimiteRepasse" runat="server"
                        ErrorMessage="Data Limite de Repasse deve ser maior ou igual a Data Limite original" Display="None"
                        CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLimiteRepasse_ServerValidate">
                    </asp:CustomValidator>
                </li>
                <li>
                    <label for="ddlPrioridadeRepasse" class="lblObrigatorio">Prioridade</label>
                    <asp:DropDownList ID="ddlPrioridadeRepasse" CssClass="ddlPrioridadeRepasse" runat="server" ToolTip="Selecione a Prioridade da Tarefa">
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
                <asp:DropDownList ID="ddlEnviarSMS" CssClass="ddlEnviarSMS" Enabled="false" runat="server" ToolTip="Enviar SMS">
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