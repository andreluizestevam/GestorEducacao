<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1311_RegistroAgendaAtividade.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 567px; }
    input[type='text'] { margin-bottom: 4px; }
    select { margin-bottom: 4px; }
    label { margin-bottom: 1px; }        
    fieldset 
    {
        margin: 5px 0 5px 5px !important;
        padding: 5px 3px 4px 8px;
    }
    
    /*--> CSS LIs */
    .liDadosControle { width: 100%; }
    .liUnidadeEmissor { margin-left: 5px; }
    .liNumTarefa { float: right !important; }
    .liResponsavel { clear: both; width: 100%; }
    .liDepartamento, .liFuncao { margin-left: 12px; }
    .liClear { clear: both; }
    .liDtCompromisso, .liDtLimite { margin-left: 3px; }
    .liPrioridade { float: right !important; }
    .liObservacao {float: right !important; margin-right: 0 !important;}
    .liStatusTarefa { margin: 15px 0 0 14px; }
    .liSMS { margin: 10px 0 0 14px; }
    
    /*--> CSS DADOS */
    .fldDadosControle { border-style: none; padding-bottom: 0px; }
    .txtEmissor { width: 210px; }    
    .txtUnidadeEmissor { width: 210px; }    
    .txtNumTarefa { text-align: right; width: 50px; }
    .txtChaveUnica { text-align: right; width: 81px; }
    .ddlUnidade { width: 240px; }    
    .ddlDepartamento { width: 100px; }    
    .ddlFuncao { width: 170px; }    
    .ddlNomeResponsavel { width: 240px; }
    .txtTitulo { width: 180px; }
    .txtDescricao { margin-bottom: 3px; width: 325px; }
    .txtDtCadastro, .txtDtCompromisso, .txtDtLimite { width: 58px; }    
    .ddlPrioridade { width: 72px; }
    .txtObservacao { width: 200px; }
    .ddlStatusTarefa { width: 148px; }
    .ddlEnviarSMS { width: 45px; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li class="liDadosControle">
    <fieldset class="fldDadosControle">
        <legend>Dados do Emissor</legend>
        <ul>
            <li id="liEmissor" class="liEmissor">
                <label for="txtEmissor">Emissor</label>
                <asp:TextBox ID="txtEmissor" CssClass="txtEmissor" Enabled="false" runat="server" ToolTip="Emissor da Tarefa"></asp:TextBox>
            </li>
            <li class="liUnidadeEmissor">
                <label for="txtUnidadeEmissor">Unidade</label>
                <asp:TextBox ID="txtUnidadeEmissor" CssClass="txtUnidadeEmissor" Enabled="false" runat="server" ToolTip="Unidade Escolar"></asp:TextBox>
            </li>
        </ul>
    </fieldset>
    </li>
    <li class="liResponsavel">
    <fieldset id="fldResponsavel" class="fldResponsavel">
    <legend>Respons&aacute;vel Tarefa</legend>
        <ul>
            <li id="liUnidade" class="liUnidade">
                <label for="ddlUnidade" class="lblObrigatorio">Unidade</label>
                <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" Enabled="false" runat="server" ToolTip="Selecione a Unidade Escolar"
                    onselectedindexchanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlUnidade" ErrorMessage="Unidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liDepartamento">
                <label for="ddlDepartamento">Departamento</label>
                <asp:DropDownList ID="ddlDepartamento" CssClass="ddlDepartamento" Enabled="false" ToolTip="Selecione o Departamento"
                    runat="server" onselectedindexchanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liFuncao">
                <label for="ddlFuncao">Fun&ccedil;&atilde;o</label>
                <asp:DropDownList ID="ddlFuncao" CssClass="ddlFuncao" Enabled="false" runat="server" ToolTip="Selecione a Função" 
                    onselectedindexchanged="ddlFuncao_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </li>
            <li class="liClear">
                <label for="ddlNomeResponsavel" class="lblObrigatorio">Respons&aacute;vel</label>
                <asp:DropDownList ID="ddlNomeResponsavel" CssClass="ddlNomeResponsavel" Enabled="false" ToolTip="Nome do Responsável"
                    runat="server" onselectedindexchanged="ddlNomeResponsavel_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNomeResponsavel" ErrorMessage="Respons&aacute;vel deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
        </ul>
    </fieldset>
    </li>
    <li class="liClear">
    <fieldset id="fldTarefa" class="fldTarefa">
    <legend>Informações da Tarefa</legend>
        <ul>
            <li id="liTitulo" class="liTitulo">
                <label for="txtTitulo" class="lblObrigatorio">T&iacute;tulo</label>
                <asp:TextBox ID="txtTitulo" CssClass="txtTitulo" Enabled="false" runat="server" MaxLength="40" ToolTip="Título da Tarefa"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitulo" ErrorMessage="T&iacute;tulo deve ser informado" Display="None"></asp:RequiredFieldValidator>
            </li>
            <asp:Panel ID="pnlChaveUnica" CssClass="liChaveUnica" Visible="false" runat="server">
                <li id="liChaveUnica" class="liChaveUnica">
                    <label for="txtChaveUnica">N° Chave &Uacute;nica</label>
                    <asp:TextBox ID="txtChaveUnica" CssClass="txtChaveUnica" Enabled="false" runat="server" ToolTip="Número da Chave Única"></asp:TextBox>
                </li>
            </asp:Panel>
            <asp:Panel ID="pnlNumTarefa" CssClass="liNumTarefa" Visible="false" runat="server">
                <li class="liNumTarefa">
                    <label for="txtNumTarefa">N° Tarefa</label>
                    <asp:TextBox ID="txtNumTarefa" CssClass="txtNumTarefa" Enabled="false" runat="server" ToolTip="Número da Tarefa"></asp:TextBox>
                </li>
            </asp:Panel>
            <li class="liClear">
                <label for="txtDescricao" class="lblObrigatorio">Descri&ccedil;&atilde;o</label>
                <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" MaxLength="200" onkeyup="javascript:MaxLength(this, 200);" ToolTip="Informe a Descrição" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descri&ccedil;&atilde;o deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liClear">
                <label for="txtDtCadastro">Data Cadastro</label>
                <asp:TextBox ID="txtDtCadastro" CssClass="txtDtCadastro" Enabled="false" runat="server" ToolTip="Data de Cadastro"></asp:TextBox>
            </li>
            <li class="liDtCompromisso">
                <label for="txtDtCompromisso" class="lblObrigatorio">Compromisso</label>
                <asp:TextBox ID="txtDtCompromisso" CssClass="txtDtCompromisso campoData" Enabled="false" runat="server" ToolTip="Data de Compromisso"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtCompromisso" ErrorMessage="Data de Compromisso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="txtDtCompromisso" ID="cvDataCompromisso" runat="server"
                    ErrorMessage="Data de Compromisso não pode ser maior que a Data Limite" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataCompromisso_ServerValidate">
                </asp:CustomValidator>
            </li>
            <li class="liDtLimite">
                <label for="txtDtLimite" class="lblObrigatorio">Limite</label>
                <asp:TextBox ID="txtDtLimite" CssClass="txtDtLimite campoData" Enabled="false" runat="server" ToolTip="Data Limite"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDtLimite" ErrorMessage="Data Limite deve ser informada" Display="None"></asp:RequiredFieldValidator>
                <asp:CustomValidator ControlToValidate="txtDtLimite" ID="cvDataLimite" runat="server"
                    ErrorMessage="Data Limite não pode ser menor que a Data de Cadastro" Display="None"
                    CssClass="validatorField" EnableClientScript="false" OnServerValidate="cvDataLimite_ServerValidate">
                </asp:CustomValidator>
            </li>
            <li class="liPrioridade">
                <label for="ddlPrioridade" class="lblObrigatorio">Prioridade</label>
                <asp:DropDownList ID="ddlPrioridade" CssClass="ddlPrioridade" Enabled="false" runat="server" ToolTip="Selecine a prioridade">
                </asp:DropDownList>
            </li>
        </ul>
    </fieldset>
    </li>
    
    <asp:Panel ID="pnlObservacao" CssClass="liObservacao" Visible="false" runat="server">
        <li id="liObservacao" class="liObservacao">
            <label for="txtObservacao">Observa&ccedil;&otilde;es</label>
            <asp:TextBox ID="txtObservacao" CssClass="txtObservacao" Enabled="false" TextMode="MultiLine" Rows="4" runat="server" MaxLength="200" onKeyup="javascript:MaxLength(this,200);" ToolTip="Informe a observação"></asp:TextBox>
        </li>
    </asp:Panel>
    
    <li class="liStatusTarefa">
        <label for="ddlStatusTarefa">Status</label>
        <asp:DropDownList ID="ddlStatusTarefa" CssClass="ddlStatusTarefa" Enabled="false" runat="server" ToolTip="Selecione o Status">
        </asp:DropDownList>
    </li>
    
    <li class="liSMS">
        <label id="lblSMS" for="ddlEnviarSMS" runat="server">Enviar Msg SMS</label>
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
</asp:Content>