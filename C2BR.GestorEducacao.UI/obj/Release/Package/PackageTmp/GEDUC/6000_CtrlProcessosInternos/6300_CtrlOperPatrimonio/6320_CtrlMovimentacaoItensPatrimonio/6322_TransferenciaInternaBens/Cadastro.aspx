<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6322_TransferenciaInternaBens.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 510px; }
        .ulDados table { border: none !important; }
        
        /*--> CSS LIs */
        .liTipo1
        {
            clear: both;
            margin-right: 15px !important;
        }
        .liTipo2
        {
            clear: both;
            margin-top: 10px;
        }
        .liTipo3
        {
            margin-left: 10px;
            margin-top: 10px;
        }
        .liTipo4 { margin-left: 10px; }
        .liStatus
        {
            margin-left: 15px;
            margin-top: 10px;
        }
        .liTran
        {
        	clear:both;
        	width:510px;
        	padding-left:10px;
        	padding-right: 5px;
        }
        
        /*--> CSS DADOS */
        .txtDeptoCurso { width: 195px; }
        .txtRespMovi { width: 247px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <p>
        &nbsp;</p>
    <ul id="ulDados" class="ulDados">
        <li class="liTipo1">
            <label for="ddlUnidade">
                Unidade de Locação
            </label>
            <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione a Unidade Escolar"
                CssClass="campoUnidadeEscolar" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liTipo2">
            <label for="ddlPatrimonio" class="lblObrigatorio">
                Patrimônio
            </label>
            <asp:DropDownList ID="ddlPatrimonio" ToolTip="Selecione o Patrimônio desejado" runat="server"
                CssClass="campoUnidadeEscolar" AutoPostBack="True" OnSelectedIndexChanged="ddlPatrimonio_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"
                runat="server" ControlToValidate="ddlPatrimonio" ErrorMessage="Patrimônio deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipo2">
            <label for="txtDepA">
                Departamento Atual
            </label>
            <asp:TextBox ID="txtDepA" Enabled="false" CssClass="txtDeptoCurso" runat="server"
                ToolTip="Departamento do Colaborador"></asp:TextBox>
        </li>
        <li class="liTipo3">
            <label for="txtDepO">
                Departamento de Origem
            </label>
            <asp:TextBox ID="txtDepO" Enabled="false" CssClass="txtDeptoCurso" runat="server"
                ToolTip="Departamento do Colaborador"></asp:TextBox>
        </li>
        <fieldset class="liTran" >
        <legend>Transferência</legend>        
        <li>
            <label for="ddlUnidest" class="lblObrigatorio">
                Unidade de Destino</label>
            <asp:DropDownList ID="ddlUnidest" CssClass="campoUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade de Destino"
                AutoPostBack="True" OnSelectedIndexChanged="ddlUnidest_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="validatorField"
                runat="server" ControlToValidate="ddlUnidest" ErrorMessage="Departamento de Destino ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipo4">
            <label for="ddlDepartamentoDes" class="lblObrigatorio">
                Departamento de Destino</label>
            <asp:DropDownList ID="ddlDepartamentoDes" CssClass="campoDptoCurso" runat="server"
                ToolTip="Selecione o Departamento de Destino">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"
                runat="server" ControlToValidate="ddlDepartamentoDes" ErrorMessage="Departamento de Destino deve ser informado"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liTipo2">
            <label for="ddlResponsavelP">
                Responsável pelo Patrimônio
            </label>
            <asp:DropDownList ID="ddlResponsavelP" Width="350px" ToolTip="Nome do Responsável pelo Patrimônio"
                runat="server">
            </asp:DropDownList>
        </li>
        <li class="liTipo2">
            <label for="txtDataIMov" title="Data de Inicio da Movimentação" class="lblObrigatorio">
                Inicio
            </label>
            <asp:TextBox ID="txtDataI" ToolTip="Informe a Data de Inicio da Movimentação" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"
                runat="server" ControlToValidate="txtDataI" ErrorMessage="Data Inicial do Movimento deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liStatus">
            <label for="ddlStatus">
                Status
            </label>
            <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Selecione o Status da Movimentação">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
            </asp:DropDownList>
        </li>
        </fieldset>
        <li class="liTipo2">
            <label for="txtOBS">
                Obs:
            </label>
            <asp:TextBox ID="txtObs" Width="480px" Height="30px" runat="server" ToolTip="Observação"
                onkeyup="javascript:MaxLength(this, 100);" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="liTipo2">
            <label for="txtDtCad">
                Dt Cadastro
            </label>
            <asp:TextBox ID="txtDtCad" Enabled="false" CssClass="txtData" ToolTip="Data de Cadastro do Registro"
                runat="server"></asp:TextBox>
        </li>
        <li class="liTipo3">
            <label for="txtRespMovi">
                Responsável pela Movimentação
            </label>
            <asp:TextBox ID="txtRespMovi" Enabled="false" CssClass="txtRespMovi" ToolTip="Nome do Responsável pela Movimentação"
                runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
